
/********************** Download Image **********************/


/*
    Generate the download image SOAP packet.
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

    var folderPath = appData.ImageLibraryFullPath.toLowerCase().replace(appData.ImageLibraryName.toLowerCase(), '').toLowerCase().trimStart("/").replace(pictureData.FileName.toLowerCase(), '').trimEnd("/");


    soapPacket = String.format(soapPacket, appData.ImageLibraryName, folderPath, pictureData.FileName, true);

    return soapPacket;
}

/*
    Execute the web service request to download file.
    soapPacket: Formatted SOAP packet
*/
function executeDownloadServiceRequest(soapPacket) {
    showLoadingAnimation(true);
    var websvcurl = appData.AppHostUrl + "/_vti_bin/imaging.asmx";
    jQuery(document).ready(function () {
        jQuery.ajax({
            url: websvcurl,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("SOAPAction", "http://schemas.microsoft.com/sharepoint/soap/ois/Download");
            },
            type: "POST",
            dataType: "xml",
            data: soapPacket, //soap packet.
            contentType: "text/xml; charset=\"utf-8\"",
            success: handleDownloadImageResponse, // Invoke when the web service call is successful.
            error: handleDownloadImageError// Invoke when the web service call fails.
        });
    });
}

/*
    Invoked when the web service call succeeds.
*/
function handleDownloadImageResponse(data, textStatus, jqXHR) {
    showLoadingAnimation(false);
    var $xml = jQuery.parseXML(jqXHR.responseText);
    var $x64string = jQuery($xml).find("File");
    if (jQuery($x64string).text().length > 0) {
        var picencoding = "data:image/png;base64," + jQuery($x64string).text();
        processWebSvsResponse(picencoding);
        //localStorage.setItem("sp_picedit", picencoding);        
    }
}

/*
     Invoked when the web service call fails.
*/
function handleDownloadImageError(jqXHR, textStatus, errorThrown) {
    //Custom code...
    showLoadingAnimation(false);
    alert(errorThrown);
}

/********************** Upload Image **********************/

/*
    Generate the upload image SOAP packet.
*/
function generateUploadPacket(dataURL) {
    var soapPacket = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                        "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                       "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                                       "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                          "<soap:Body>" +
                            "<Upload " +
                              "xmlns=\"http://schemas.microsoft.com/sharepoint/soap/ois/\">" +
                              "<strListName>{0}</strListName>" +
                              "<strFolder>{1}</strFolder>" +
                              "<bytes>{2}</bytes>" +
                              "<fileName>{3}</fileName>" +
                              "<fOverWriteIfExist>{4}</fOverWriteIfExist>" +
                            "</Upload>" +
                          "</soap:Body>" +
                        "</soap:Envelope>";


        soapPacket = String.format(soapPacket, appData.ImageLibraryName,
        appData.ImageLibraryFullPath.replace(appData.ImageLibraryName, '')
       .replace('/' + pictureData.FileName.toLowerCase(), '').trimEnd("/").trimStart("/"),
        dataURL, pictureData.FileName, true);

    return soapPacket;
}

/*
    Execute the upload service request.
*/
function executeUploadServiceRequest(soapPacket) {
    showLoadingAnimation(true);
    var websvcurl = appData.AppHostUrl + "/_vti_bin/imaging.asmx";
    jQuery(document).ready(function () {
        jQuery.ajax({
            url: websvcurl,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("SOAPAction", "http://schemas.microsoft.com/sharepoint/soap/ois/Upload");
            },
            type: "POST",
            dataType: "xml",
            data: soapPacket, //soap packet.
            contentType: "text/xml; charset=\"utf-8\"",
            success: handleUploadResponse, // Invoke when the web service call is successful.
            error: handleUploadError// Invoke when the web service call fails.
        });
    });
}

/*
    Invoked when the web service call succeeds.
*/
function handleUploadResponse(data, textStatus, jqXHR) {
    // Custom code...
    if (appData.GeolocationFieldName != null && pictureData.Latitude != null && pictureData.Longitude != null) {
        setGeolocationFieldValue();
    }
    else {
        showLoadingAnimation(false);
        alert('Successfully saved image to SharePoint...');
    }
}

/*
     Invoked when the web service call fails.
*/
function handleUploadError(jqXHR, textStatus, errorThrown) {
    //Custom code...
    showLoadingAnimation(false);
    alert("Error occured while saving the image to SharePoint: " + errorThrown);
}

/*
     Set the geolocation field value of the uploaded image.
*/
function setGeolocationFieldValue() {
    var value = "POINT (" + pictureData.Longitude + " " + pictureData.Latitude + ")";
    var geoid = appData.GeolocationFieldName;
    var fileurl = "/" + appData.AppHostRelativeUrl.trimStart("/").trimEnd("/") + "/" + appData.ImageLibraryFullPath.trimStart("/").trimEnd("/") + "/" + pictureData.FileName;

    var clientContext = new SP.ClientContext.get_current();
    var parentCtx = new SP.AppContextSite(clientContext, appData.AppHostUrl);
    var file = parentCtx.get_web().getFileByServerRelativeUrl(fileurl);
    var targetListItem = file.get_listItemAllFields();
    targetListItem.set_item(geoid, value);
    targetListItem.update();

    clientContext.load(targetListItem);
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onsetLocationQuerySucceeded),
        Function.createDelegate(this, this.onsetLocationQueryFailure)
    );
}

/*
    Executes when the item update operation succeeds.
*/
function onsetLocationQuerySucceeded() {
    showLoadingAnimation(false);
    alert('Successfully saved image (with geotag) to SharePoint.');
}

/*
    Executes when the item update operation fails.
*/
function onsetLocationQueryFailure(error) {
    showLoadingAnimation(false);
    alert('Successfully saved image to SharePoint, but error occured while saving the image geotag.');
}

/*
    Save the image data to SharePoint.
*/
function saveImageDataToSharePoint() {
    //var dataURL = localStorage.getItem("sp_picedit");
    //localStorage.removeItem("sp_picedit");
    saveCanvas();
    var dataURL = pictureData.DataURI;
    if (dataURL) {
        var imgFormatindex = dataURL.indexOf(",");
        if (imgFormatindex != -1) {
            dataURL = dataURL.substring(imgFormatindex + 1, dataURL.length);
        }

        var sopaPacket = generateUploadPacket(dataURL);
        executeUploadServiceRequest(sopaPacket);
        jQuery("#savetosp").prop('disabled', true);
    }

    imagesource = null;
}

/********************** SharePoint Library Validation **********************/

/*
    Validate the selected SharePoint library type and perform the given operation.
    optype: Save or Fetch operation
*/
function validateLibrary(optype) {
    sourceOperation = optype;
    var clientContext = new SP.ClientContext.get_current();
    var parentCtx = new SP.AppContextSite(clientContext, appData.HostUrl);
    var parentWeb = parentCtx.get_web();
    this.collList = parentWeb.get_lists();
    clientContext.load(collList, 'Include(Title, DefaultViewUrl, BaseTemplate)');
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onSiteQuerySucceeded),
        Function.createDelegate(this, this.onSiteQueryFailed)
    );
}

/*
    Success callback.
*/
function onSiteQuerySucceeded() {
    var listInfo = '';
    var listEnumerator = collList.getEnumerator();
    while (listEnumerator.moveNext()) {
        var list = listEnumerator.get_current();
        if (list.get_defaultViewUrl().toLowerCase().indexOf(appData.ImageLibraryName.toLowerCase() + '/forms/') != -1) {
            var templateid = list.get_baseTemplate();
            if (templateid != 109) {
                appData.ImageLibraryFullPath = null;
                appData.ImageLibraryName = null;
                if (sourceOperation == 2) {
                    alert('The selected library ' + list.get_title() + ' is not a picture or image library.\nPlease select a picture library to continue saving the image.');
                    launchFolderSelector(function () { validateLibrary(2); });
                }
                else if (sourceOperation == 1) {
                    alert('The selected library ' + list.get_title() + ' is not a picture or image library.\nPlease select image from a picture library.');
                    createAssetPickerControl();
                }
            } else {
                if (sourceOperation == 2) {
                    appData.ImageLibraryDisplayTitle = list.get_title();
                    getGelolocationFieldFromList();
                }
                else if (sourceOperation == 1) {
                    processAssetPickerData();
                }
            }

            break;
        }
    }

    return false;
}

/*
    Error callback.
*/
function onSiteQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() +
        '\n' + args.get_stackTrace());
}

/*
    Retrieve the geolocation field from the selected SharePoint list.
*/
function getGelolocationFieldFromList() {
    var clientContext = new SP.ClientContext.get_current();
    var parentCtx = new SP.AppContextSite(clientContext, appData.AppHostUrl);
    var parentWeb = parentCtx.get_web();
    var targetList = parentWeb.get_lists().getByTitle(appData.ImageLibraryDisplayTitle);
    this.listFields = targetList.get_fields();
    clientContext.load(this.listFields, 'Include(InternalName, TypeAsString)');
    clientContext.executeQueryAsync(Function.createDelegate(this,
        this.onListGeolocationFieldsQuerySucceeded), Function.createDelegate(this,
        this.onSiteQueryFailed));
}

/*
    Executes when the list fields are retrieved.
*/
function onListGeolocationFieldsQuerySucceeded() {
    var fieldEnumerator = listFields.getEnumerator();
    while (fieldEnumerator.moveNext()) {
        var field = fieldEnumerator.get_current();
        var fType = field.get_typeAsString();
        if (fType.toLowerCase() == "geolocation") {
            appData.GeolocationFieldName = field.get_internalName();

            break;
        }
    }

    saveImageDataToSharePoint();
}

/*
    Get the server-relative url of the selected image.
    listId: SharePoint list ID
    itemId: Selected item ID
*/
function getImageUrl(listId, itemId) {
    var clientContext = new SP.ClientContext.get_current();
    var parentCtx = new SP.AppContextSite(clientContext, appData.AppHostUrl);
    this.parentWeb = parentCtx.get_web();
    this.targetList = this.parentWeb.get_lists().getById(listId);
    targetListItem = targetList.getItemById(itemId);
    this.targetFile = targetListItem.get_file();

    clientContext.load(this.parentWeb, 'ServerRelativeUrl');
    clientContext.load(this.targetFile, 'ServerRelativeUrl', 'Name');
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.ongetImageUrlQuerySucceeded),
        Function.createDelegate(this, this.onSiteQueryFailed)
    );
}

/*
    Executes when the image url is retrieved.
*/
function ongetImageUrlQuerySucceeded() {
    var serverrelativeurl = this.parentWeb.get_serverRelativeUrl();
    var imagepath = this.targetFile.get_serverRelativeUrl();
    var filename = this.targetFile.get_name();
    if (serverrelativeurl && filename && imagepath) {
        appData.ImageLibraryFullPath = imagepath.toLowerCase().replace(serverrelativeurl.toLowerCase(), '').replace(filename.toLowerCase(), '');
        var slashtrimmedstring = appData.ImageLibraryFullPath.trimStart("/").trimEnd("/");
        var firstSlash = slashtrimmedstring.indexOf("/");
        if (firstSlash != -1) {
            appData.ImageLibraryName = slashtrimmedstring.substring(0, firstSlash);
        }
        else {
            appData.ImageLibraryName = slashtrimmedstring;
        }

        pictureData.imagesource = pictureSource.SharePoint;
        pictureData.FileName = filename;

        processAssetPickerData();
    }
}


/********************** Helper Functions **********************/

/*
    String format utility function.
*/
String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }

    return s;
}

/*
    String trim utility function.
*/
String.prototype.trimStart = function (c) {
    if (this.length == 0)
        return this;
    c = c ? c : ' ';
    var i = 0;
    var val = 0;
    for (; this.charAt(i) == c && i < this.length; i++);
    return this.substring(i);
}

/*
    String trim utility function.
*/
String.prototype.trimEnd = function (c) {
    c = c ? c : ' ';
    var i = this.length - 1;
    for (; i >= 0 && this.charAt(i) == c; i--);
    return this.substring(0, i + 1);
}

/*
    String trim utility function.
*/
String.prototype.trim = function (c) {
    return this.trimStart(c).trimEnd(c);
}