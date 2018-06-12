
// application level objects
var vCardData = null;
var profilePicIndex = -1;
var appData = null;
var clientContext = null;
var seperator = null;
var vcfFileNameIndex = -1;
var vcffileName = null;
var postFeed = false;


// inject the required scripts and invode the settings function.
function injectScripts() {
    initializeParameters();
    //Load the required SharePoint libraries and wire events.
    jQuery(document).ready(function () {
        var scriptbase = appData.AppWebUrl + '/_layouts/15/';
        //load all appropriate scripts for the page to function
        jQuery.getScript(scriptbase + 'SP.Runtime.js', function () {
            jQuery.getScript(scriptbase + 'SP.js', function () {
                jQuery.getScript(scriptbase + 'init.js', function () {
                    jQuery.getScript(scriptbase + 'SP.UserProfiles.js', function () {
                        clientContext = new SP.ClientContext.get_current();
                        jQuery.when(initializeSettings()).then(function (d) {
                            settings = d;
                            executeProfileScript();
                        }, function (err) {
                            logError(err, false);
                        });
                    });
                });
            });
        });
    });
}

// initialize the app parameters
function initializeParameters() {
    vCardData = null;
    profileProperties = null;
    accountName = null;
    profilePicIndex = -1;
    appData = {
        "HostUrl": null,
        "AppWebUrl": null,
        "AppHostUrl": null,
        "ImageUrl": null,
        "ImageLibraryName": null,
        "ImageFolderName": null,
        "ImageName": null,
        "ImageBinary": null
    };

    appData.AppWebUrl = getvalueFromQueryString(null, 'SPAppWebUrl');
    appweburl = appData.AppWebUrl;
    appData.HostUrl = getvalueFromQueryString(null, 'SPHostUrl');
    appData.AppHostUrl = appData.AppWebUrl.substring(0, appData.AppWebUrl.indexOf("/DownloadvCard"));
}

// executing the required async calls via jquery deferreds
function executeProfileScript() {
    profileProperties = null;
    jQuery.when(getCurrentUser(), getVcardPropertyMappings())
         .then(function (a, k) {
             if (Array.isArray(a)) {
                 accountName = a[0].d.LoginName;
             }
             else {
                 accountName = a;
             }

             profileProperties = k;
             addSettingsData();
         })
         .then(getUserProfilePropertyValues)
         .then(checkAndGetProfileImage)
         .fail(function (err) {
             logError(err, false);
         });
}

// gets the current user's/visited user's login name 
function getCurrentUser() {
    var parentUrl = document.referrer;
    accountName = getvalueFromQueryString(parentUrl, 'accountname');
    if (accountName != null) {
        return accountName;
    }
    else {
        return jQuery.ajax({
            url: appData.AppWebUrl + "/_api/web/currentUser",
            method: "GET",
            headers: { "Accept": "application/json; odata=verbose" }
            /*success: function (data) {
                accountName = data.d.LoginName;
            },
            error: function (error) {
                //showloadingContainer(false);
                var errorobj = JSON.parse(error.responseText);
                logError(errorobj.error.message.value, false);
            }*/
        }).promise();
    }
}

// update the app level variables with the values read from the settings
function addSettingsData() {
    seperator = null;
    jQuery.grep(settings, function (s) {
        if (s.SettingName == "VCFSEPERATOR") {
            seperator = s.SettingValue;
        }
    });

    var fileNameProps = null;
    jQuery.grep(settings, function (s) {
        if (s.SettingName == "VCFFILENAME") {
            fileNameProps = s.SettingValue.split(';');
        }
    });

    if (fileNameProps.length > 0) {
        var configobject = new Object();
        configobject.OutlookProperty = 'VCF_FILE_NAME';
        configobject.UPPropertyNameArray = fileNameProps;
        configobject.FormatCallback = null;
        profileProperties.push(configobject);
        vcfFileNameIndex = profileProperties.length - 1;
    }

    var downloadprofile = false;
    jQuery.grep(settings, function (s) {
        if (s.SettingName == "DOWNLOADPROFILEPIC") {
            downloadprofile = (s.SettingValue == "true");
        }
    });

    if (downloadprofile) {
        var configobject = new Object();
        configobject.OutlookProperty = 'PHOTO;TYPE=JPEG;ENCODING=BASE64';
        configobject.UPPropertyNameArray = new Array();
        configobject.UPPropertyNameArray[0] = 'PictureURL';
        configobject.FormatCallback = null;
        profileProperties.push(configobject);
        profilePicIndex = profileProperties.length - 1;
    }
    else {
        profilePicIndex = -1;
    }

    postFeed = false;
    jQuery.grep(settings, function (s) {
        if (s.SettingName == "POSTNEWSFEED") {
            postFeed = (s.SettingValue == "true");
        }
    });
}

// Check if the profile image is selected for reterievel.
// if, yes call the imaging.asmx and download the profile image
// else enerate the vcard markup
function checkAndGetProfileImage() {
    if (profilePicIndex != -1) {
        appData.imageurl = profileProperties[profilePicIndex].UPPropertyValueArray[0];
        if (appData.imageurl != null && jQuery.trim(appData.imageurl) != '') {
            getImageDetails();
            jQuery.when(downlodProfileImage()).then(vCardformatter).then(function () {
                if (vCardData != null || jQuery.trim(vCardData) != '') {
                    downloadvCardOnClient();
                }
            });
        }
        else {
            vCardformatter();
            downloadvCardOnClient();
        }
    }
    else {
        vCardformatter();
        downloadvCardOnClient();
    }
}

// gets the required parameters for fetching profile image
function getImageDetails() {
    var imageurl = appData.imageurl;
    appData.ImageName = imageurl.substring(imageurl.lastIndexOf("/") + 1, imageurl.length);
    imageurl = imageurl.replace("/" + appData.ImageName, '');
    appData.ImageFolderName = imageurl.substring(imageurl.lastIndexOf("/") + 1, imageurl.length);
    imageurl = imageurl.replace("/" + appData.ImageFolderName, '');
    appData.ImageLibraryName = imageurl.substring(imageurl.lastIndexOf("/") + 1, imageurl.length);

    appData.ImageName = decodeURIComponent(appData.ImageName);
    appData.ImageFolderName = decodeURIComponent(appData.ImageFolderName);
    appData.ImageLibraryName = decodeURIComponent(appData.ImageLibraryName);
}

//  Execute the web service requiest to download file
//  soapPacket: Formatted SOAP packet
function downlodProfileImage() {
    var soapPacket = generateDownloadPacket();
    var websvcurl = appData.AppHostUrl + "/_vti_bin/imaging.asmx";
    return jQuery.ajax({
        url: websvcurl,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("SOAPAction", "http://schemas.microsoft.com/sharepoint/soap/ois/Download");
        },
        type: "POST",
        dataType: "xml",
        data: soapPacket, //soap packet.
        contentType: "text/xml; charset=\"utf-8\"",
        success: function (data) {
            getImageURI(data.childNodes[0].textContent);
        },
        error: function (error) {
            var $xml = jQuery.parseXML(error.responseText);
            var $errstring = jQuery($xml).find("errorstring");
            if (jQuery($errstring).length > 0 && jQuery($errstring).text().length > 0) {
                logError(jQuery($errstring).text(), false);
            }
            else if (jQuery($xml).find("faultstring").length > 0 && jQuery($xml).find("faultstring").text().length > 0) {
                logError('Error occured in downloading user profile image.' + jQuery($xml).find("faultstring").text(), false);
            }
            else {
                logError('Error occured in downloading user profile image.', false);
            }
        }
    }).promise();
}

/*
    Generate the download image SOAP packet
*/
function generateDownloadPacket() {
    var soapPacket = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                        "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                       "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                                       "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                          "<soap:Body>" +
                            "<Download " +
                              "xmlns=\"http://schemas.microsoft.com/sharepoint/soap/ois/\">" +
                              "<strListName>{0}</strListName>" +
                              "<strFolder>{1}</strFolder>" +
                              "<itemFileNames>" +
                               "<string>{2}</string>" +
                               "</itemFileNames>" +
                              "<type>0</type>" +
                              "<fFetchOriginalIfNotAvailable>{3}</fFetchOriginalIfNotAvailable>" +
                            "</Download>" +
                          "</soap:Body>" +
                        "</soap:Envelope>";

    soapPacket = String.format(soapPacket, appData.ImageLibraryName, appData.ImageFolderName, appData.ImageName, true);

    return soapPacket;
}

// extract the image binary
function getImageURI(data) {
    try {
        appData.ImageBinary = data.replace(/\n\n/gi, '');
    }
    catch (e) {
        logError(e.message, false);
    }
}

// iterate through the propety-value collection and build a vcf body
function vCardPropertyformatter() {
    try {
        var vCardPropBuilder = new Array();
        if (profileProperties[vcfFileNameIndex].UPPropertyValueArray.length > 0) {
            vcffileName = profileProperties[vcfFileNameIndex].UPPropertyValueArray.join(seperator);
            vCardPropBuilder.push(String.format('N;LANGUAGE=en-us:{0}', profileProperties[vcfFileNameIndex].UPPropertyValueArray.join(';')));
        }
        else {
            vcffileName = profileProperties[vcfFileNameIndex].UPPropertyValueArray[0];
            vCardPropBuilder.push(String.format('N;LANGUAGE=en-us:{0}', vcffileName));
        }

        if (vcffileName == null || jQuery.trim(vcffileName) == '') {
            vcffileName = "No Name";
            vCardPropBuilder.push(String.format('N;LANGUAGE=en-us:{0}', "No;Name"));
        }

        for (i = 0; i < profileProperties.length; i++) {
            if (i == profilePicIndex) {
                profileProperties[i].UPPropertyValueArray[0] = appData.ImageBinary + "\n";
            }

            var config = String.format("{0}:{1}", profileProperties[i].OutlookProperty, profileProperties[i].UPPropertyValueArray.join(" "));
            vCardPropBuilder.push(config);
        }

        vcffileName = vcffileName.replace(/[\\/:*?"<>|]/g, ' ');
        vcffileName += ".vcf";

        return vCardPropBuilder;
    } catch (e) {
        logError(e.message, false);
    }
}

// build the vcf card
function vCardformatter() {
    var vCardBuilder = new Array();
    var date = new Date();
    var month = date.getMonth();
    month = month + 1;
    var revision = date.getFullYear() + "" + month + "" + date.getDate() + "T" + date.getHours() + "" + date.getMinutes() + "" + date.getSeconds();

    vCardBuilder.push("BEGIN:VCARD");
    vCardBuilder.push(vCardPropertyformatter().join('\n'))
    vCardBuilder.push("REV:" + revision);
    vCardBuilder.push("VERSION:2.1");
    vCardBuilder.push("END:VCARD");

    vCardData = vCardBuilder.join('\n');
}

// push the vcf on the client
function downloadvCardOnClient() {
    try {
        var arraybuffer = new ArrayBuffer(vCardData.length * 2);
        var view = new Uint8Array(arraybuffer);
        for (var i = 0; i < vCardData.length; i++) {
            view[i] = vCardData.charCodeAt(i) & 0xff;
        }

        var blob = new Blob([arraybuffer], { type: 'application/octet-stream' });
        var url = (window.webkitURL || window.URL).createObjectURL(blob);
        var a = document.createElement('a');
        if (window.navigator.msSaveBlob) {
            jQuery('#vcflinkhiden').attr("download", vcffileName);
            jQuery('#vcflinkhiden').click(function () { window.navigator.msSaveBlob(blob, vcffileName); });
            jQuery('#vcflinkhiden').click();
        }
        else {
            a.href = url;
            a.download = vcffileName;
            a.click();
        }

        if (postFeed) {
            postToNewsFeed();
        }
    }
    catch (e) {
        logError(e.message, false);
    }
}

function initiatedownload() {
    jQuery('#hiddenvcfbldbtn').click();
}

// mention the visited user in the current user's profile
function postToNewsFeed() {
    var feedManager = new SP.Social.SocialFeedManager(clientContext);

    var userDataItem = new SP.Social.SocialDataItem();
    userDataItem.set_itemType(SP.Social.SocialDataItemType.user);
    userDataItem.set_text(accountName);
    userDataItem.set_accountName(accountName);

    var imgAttachment = new SP.Social.SocialAttachment();
    imgAttachment.set_attachmentKind(SP.Social.SocialAttachmentKind.Image);
    imgAttachment.set_uri(appData.AppWebUrl + "/Images/posticon.png");

    // Create the post content with the message and add the data item.
    var postData = new SP.Social.SocialPostCreationData();
    postData.set_contentText("Hey {0}, I just downloaded your virtual business card");
    postData.set_contentItems([userDataItem]);
    postData.set_attachment(imgAttachment);

    // Publish the post. Pass null for the "targetId" parameter because this is a root post.
    var resultThread = feedManager.createPost(null, postData);

    clientContext.executeQueryAsync(onPostToFeedSuccess, onFail);
}

function onPostToFeedSuccess(sender, args) {
}

function onFail(sender, args) {
    var error = args.get_message() + '\n' + args.get_stackTrace();
    logError(args.get_message(), false);
}

// extract value from query string
function getvalueFromQueryString(urltosearch, param) {
    var pageURL = null;
    if (urltosearch == null || jQuery.trim(urltosearch) == '') {
        pageURL = window.location.search.substring(1);
    }
    else {
        seperator = urltosearch.indexOf('?');
        if (seperator != -1) {
            pageURL = urltosearch.substring(seperator + 1, urltosearch.length);
        }
        else {
            return null;
        }
    }

    var urlVariables = pageURL.split('&');
    for (var i = 0; i < urlVariables.length; i++) {
        var parameterName = urlVariables[i].split('=');
        if (parameterName[0] == param) {
            return decodeURIComponent(parameterName[1]);
        }
    }

    return null;
}