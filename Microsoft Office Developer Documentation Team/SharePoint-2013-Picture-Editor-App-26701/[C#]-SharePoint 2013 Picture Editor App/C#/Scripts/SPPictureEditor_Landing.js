/*
    Object to store data required for application.
*/
var appData = {
    "HostUrl": null,
    "AppWebUrl": null,
    "AppHostUrl": null,
    "AppHostRelativeUrl": null,
    "ImageLibraryFullPath": null,
    "ImageLibraryName": null,
    "ImageLibraryDisplayTitle": null,
    "UseClientGeotagging": false,
    "GeolocationFieldName": null
};

/*
    Enumeration to specify the image source.
*/
var pictureSource = {
    "SharePoint": 1,
    "FileBrowser": 2,
    "DragDrop": 3,
    "DeviceCamera": 4
};


/*
    Object to hold the selected picture and its metadata.
*/
var pictureData = {
    "ImageSource": pictureSource,
    "OrignalDataURI": null,
    "DataURI": null,
    "FileName": null,
    "Height": null,
    "Width": null,
    "CurrentWidth": null,
    "CurrentHeight": null,
    "Latitude": null,
    "Longitude": null,
    "ImageMetaLatitude": null,
    "ImageMetaLongitude": null
};

/*
    Image scaling resolutions.
*/
var imageResolutions = {
    "Document": { "Width": 1024, "Height": 768 },
    "WebPage": { "Width": 800, "Height": 600 },
    "Email": { "Width": 640, "Height": 480 }
}

/*
    Check if drag and drop is supported on the current browser.
*/
var isValid = {
    filereader: typeof FileReader != 'undefined',
    dnd: 'draggable' in document.createElement('span'),
}

/*
    Accepted file types supported by the application.
*/
var acceptedTypes = {
    'image/png': true,
    'image/jpeg': true,
    'image/gif': true
}

/*
    Operation to perform.
*/
var sourceOperation = {
    "FETCH": 1,
    "SAVE": 2,
}

/*
    Flag to indicate if touch is supported on the current device.
*/
var isTouchDevice = false;

/*
    Get the URL of the parent site of this app web.
*/
function getAppHostUrl() {
    var host = appData.AppWebUrl.toLowerCase().replace(_spPageContextInfo.siteAbsoluteUrl.toLowerCase() + "/", '');
    firstWack = host.indexOf('/');
    if (firstWack != -1 && host.length - 1 > firstWack) {
        appData.AppHostUrl = _spPageContextInfo.siteAbsoluteUrl + "/" + host.substring(0, firstWack);
        appData.AppHostRelativeUrl = decodeURIComponent(window.location.pathname).toLowerCase().replace("/SharePointPictureEditor/Pages/Default.aspx".toLowerCase(), '');
    }
    else {
        appData.AppHostUrl = _spPageContextInfo.siteAbsoluteUrl;
        var sp = appData.HostUrl.toLowerCase();

        if (sp.indexOf("https://") != -1) {
            sp = sp.replace('https://', '');
        }
        else {
            sp = sp.replace('http://', '');
        }

        firstWack = sp.indexOf('/');
        if (firstWack != -1 && sp.length - 1 > firstWack) {
            appData.AppHostRelativeUrl = sp.substring(firstWack, sp.length);
        }
        else {
            appData.AppHostRelativeUrl = '/';
        }
    }
}

/*
    Load some of the UI components on the app surface.
*/
function loadUIControls() {
    jQuery(function () {
        jQuery("#locationCheck").button();
        jQuery("#downloadLinkContainer").draggable();
        jQuery("#rerunlast").button().click(function () {
            scaleImage('Document');
            return false;
        })
          .next()
            .button({
                text: false,
                icons: {
                    primary: "ui-icon-triangle-1-s"
                }
            })
            .click(function () {
                var menu = jQuery(this).parent().next().show().position({
                    my: "left top",
                    at: "left bottom",
                    of: this
                });

                jQuery(document).one("click", function () {
                    menu.hide();
                });

                return false;
            })
            .parent()
              .buttonset()
              .next()
                .hide()
                .menu();

        jQuery("#ctl00_onetidHeadbnnr2").attr('src', '../Images/AppIcon.png');
        jQuery("#ctl00_onetidHeadbnnr2").removeClass('ms-siteicon-img');
        jQuery("#ctl00_onetidHeadbnnr2").width(80);
    });
}

/*
    Loads when the app is launched and initializes the required objects.
*/
jQuery(document).ready(function () {
    appData.HostUrl = getvaluefromQueryString('SPHostUrl');
    appData.AppWebUrl = getvaluefromQueryString('SPAppWebUrl');
    var IsECBMode = getvaluefromQueryString('IsECB');
    var IsDlgMode = getvaluefromQueryString('IsDlg');
    //isTouchDevice();

    if (IsECBMode && !IsDlgMode) {
        var listId = getvaluefromQueryString('ListID');
        var itemId = getvaluefromQueryString('ItemID');
        getAppHostUrl();
        SP.SOD.executeFunc('sp.js', 'SP.ClientContext', function () { getImageUrl(listId, itemId); });
    }
    else if (!IsECBMode && !IsDlgMode) {
        getAppHostUrl();
        resetApplication();
        SP.SOD.executeFunc('sp.js', 'SP.ClientContext', function () { launchLandingPage(); fixLandingDialogAttributes() });
    }
});

/*
    Launch the app landing dialog to enable image source selection.
*/
function launchLandingPage() {
    var launchOptions = {
        url: appData.AppHostUrl + "/SharePointPictureEditor/Pages/PictureLanding.aspx?APPHostUrl=" + appData.AppHostUrl,
        title: "Select image from",
        allowMaximize: false,
        showClose: true,
        width: 420,
        height: 430,
        autoSize: false,
        dialogReturnValueCallback: landingDialogCallback
    };

    SP.UI.ModalDialog.showModalDialog(launchOptions);
}

/*
    Fix attributes of the landing dialog.
*/
function fixLandingDialogAttributes() {
    //jQuery(".ms-dlgTitle").hide();
    jQuery(".ms-cui-topBar2").hide();
}

/*
    Landing dialog callback function.
    Launches the dialog for the selected image source.
*/
function landingDialogCallback(dialogResult, picData) {
    if (dialogResult == SP.UI.DialogResult.OK) {
        switch (picData.ImageSource) {
            case 1:
                launchAssetPicker();
                break;
            case 2:
                launchDesktopFileBrowser();
                break;
            case 3:
                if (picData.DataURI) {
                    pictureData = picData;
                    renderCanvas();
                }
                break;
            case 4:
                launchCameraDialog();
                break;
        }
    }
    else {

    }
}

/*
    Does the current browser support getUserMedia API?
*/
function hasGetUserMedia() {
    return !!(navigator.getUserMedia || navigator.webkitGetUserMedia ||
              navigator.mozGetUserMedia || navigator.msGetUserMedia);
}

/*
    Close modal dialog.
*/
function closeModal() {
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, 'Cancel clicked', '');
}

/*
    Select the image from drag-and-drop source.
*/
function selectfromDragDrop(fileName, _imagedata, type) {
    picData = pictureData;
    picData.FileName = fileName;
    picData.DataURI = _imagedata;
    picData.OrignalDataURI = _imagedata;
    picData.ImageSource = pictureSource.DragDrop;
    if (type == "image/jpeg") {
        picData = getLatLonFromImageData(picData);
    }

    doDefaultImageScaling(picData, passToCloseCallback);
}

/*
    Close the modal dialog with the picture data.
*/
function passToCloseCallback(picData) {
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, picData);
}

/*
    Perform the default image scaling of the selected image to Document resolution.
*/
function doDefaultImageScaling(picData, callBack) {
    var tempImg = new Image();
    tempImg.onload = function (evt) {
        var MAX_WIDTH = imageResolutions.Document.Width;
        var MAX_HEIGHT = imageResolutions.Document.Height;
        var tempW = tempImg.width;
        var tempH = tempImg.height;
        if (tempW > tempH) {
            if (tempW > MAX_WIDTH) {
                tempH *= MAX_WIDTH / tempW;
                tempW = MAX_WIDTH;
            }
        } else {
            if (tempH > MAX_HEIGHT) {
                tempW *= MAX_HEIGHT / tempH;
                tempH = MAX_HEIGHT;
            }
        }

        var resizecanvas = document.createElement('canvas');
        resizecanvas.width = tempW;
        resizecanvas.height = tempH;
        var resizectx = resizecanvas.getContext("2d");
        resizectx.drawImage(this, 0, 0, tempW, tempH);
        picData.DataURI = resizecanvas.toDataURL();

        picData.CurrentWidth = tempW;
        picData.CurrentHeight = tempH;

        picData.Width = tempImg.width;
        picData.Height = tempImg.height;

        callBack(picData);
    };

    tempImg.src = picData.DataURI;
}

/*
    Select image from SharePoint.
*/
function selectfromSharePoint() {
    picData = pictureData;
    picData.ImageSource = pictureSource.SharePoint;
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, picData);
}

/*
     Select image from file system browser.
*/
function selectfromFileBrowser() {
    picData = pictureData;
    picData.ImageSource = pictureSource.FileBrowser;
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, picData);
}

/*
     Capture image from device's web cam.
*/
function selectfromDeviceCamera() {
    picData = pictureData;
    picData.ImageSource = pictureSource.DeviceCamera;
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, picData);
}

/*
     Launch the SharePoint folder picker control.
     subcallback: Callback to execute after the user selects the folder.
*/
function launchFolderSelector(subcallback) {
    var hosturl = appData.AppHostUrl;
    var callback = function (dest) {
        if (dest != null && dest != undefined && dest[3] != null) {
            var listPath = dest[3].replace(dest[1], '');
            if (listPath.toLowerCase().indexOf('lists/') != -1) {
                alert('The current selection is not valid.\nPlease select a picture library to continue saving the image.');
                launchFolderSelector(null);
            }
            else {
                setImageLibraryFullPath(listPath);
                if (subcallback != null) {
                    subcallback();
                }
            }
        }
    };

    var iconUrl = "/_layouts/15/images/smt_icon.gif?rev=23";
    LaunchPickerTreeDialog('', '', 'listsOnly', '', hosturl, '', '', '', iconUrl, '', callback, true, '');
}

/*
     Read the query string value for the given parameter.
     param: Parameter to read value from.
*/
function getvaluefromQueryString(param) {
    var pageURL = window.location.search.substring(1);
    var urlVariables = pageURL.split('&');
    for (var i = 0; i < urlVariables.length; i++) {
        var parameterName = urlVariables[i].split('=');
        if (parameterName[0] == param) {
            return decodeURIComponent(parameterName[1]);
        }
    }

    return null;
}

/*
     Set the full URL of the SharePoint picture library.
     new_value: URL of the picture library.
*/
var setImageLibraryFullPath = function (libraryPath) {
    appData.ImageLibraryFullPath = libraryPath;
    setLibraryNameFromFullPath();
}

/*
     Extract the library name from the full URL of the SharePoint picture library.
*/
function setLibraryNameFromFullPath() {
    if (appData.ImageLibraryFullPath != null || appData.ImageLibraryFullPath != '') {
        var firstslash = appData.ImageLibraryFullPath.indexOf("/");
        if (firstslash != -1 && firstslash == 0) {
            var subpath = appData.ImageLibraryFullPath.substring(1, appData.ImageLibraryFullPath.length);
        }
        else {
            var subpath = appData.ImageLibraryFullPath;
        }

        firstslash = subpath.indexOf("/");
        if (firstslash != -1) {
            appData.ImageLibraryName = decodeURIComponent(subpath.substring(0, firstslash));
        }
        else {
            appData.ImageLibraryName = decodeURIComponent(subpath);
        }
    }
}

/*
     Initiate saving data to SharePoint.
*/
function saveFileToSharePoint() {
    if (pictureData.DataURI &&
        pictureData.FileName) {
        if (!appData.ImageLibraryFullPath) {
            launchFolderSelector(null);
        }

        if (appData.ImageLibraryFullPath) {
            validateLibrary(2);
            disableEnableAnchors(true, 'saveaslink');
        }
    }
}

/*
     Initiate saving data to SharePoint but to a different folder.
*/
function saveFileAsToSharePoint() {
    if (pictureData.DataURI &&
        pictureData.FileName) {
        appData.ImageLibraryFullPath = null;
        appData.ImageLibraryFullPath = null;
        launchFolderSelector(null);
    }

    if (appData.ImageLibraryFullPath) {
        validateLibrary(2);
    }
}

/*
     Reset the application state.
*/
function resetApplication() {
    appData.ImageLibraryFullPath = null;
    appData.ImageLibraryName = null;
    pictureData.DataURI = null;
    pictureData.OrignalDataURI = null;
    pictureData.FileName = null;
    pictureData.CurrentWidth = null
    pictureData.CurrentHeight = null;
    pictureData.Height = null;
    pictureData.Width = null
    pictureData.Latitude = null;
    pictureData.Longitude = null;
    pictureData.ImageMetaLatitude = null;
    pictureData.ImageMetaLongitude = null;
    pictureData.ImageSource = null;

    resetCanvas();
}

/*
     Enable or disable the links on the toolbar.
*/
function disableEnableAnchors(enable, id) {
    if (!enable) {
        jQuery("#" + id).click(function () {
            return false;
        });

        jQuery("#" + id).addClass('disableanchor').removeClass('enableanchor');
        jQuery("#" + id + "img").addClass('disableimage');
    }
    else {
        jQuery("#" + id).removeAttr("onclick").unbind('click');
        jQuery("#" + id).removeClass('disableanchor').addClass('enableanchor');
        jQuery("#" + id + "img").removeClass('disableimage');
    }
}

/*
     Show the loading animation.
     isvisible: True to show animation, or False to hide animation.
*/
function showLoadingAnimation(isvisible) {
    if (isvisible) {
        jQuery("#loadingcontainer").show();
    }
    else {
        jQuery("#loadingcontainer").hide();
    }
}

/*
     Extract latitude and longitude from the image data.
     pictureData: Picture data object.
*/
function getLatLonFromImageData(pictureData) {
    var byteString = atob(pictureData.DataURI.split(',')[1]);
    var binaryFile = new EXIF.BinaryFile(byteString, 0, byteString.length);
    var exif = EXIF.findEXIFinJPEG(binaryFile);
    if (exif != null && exif.GPSLatitude != null && exif.GPSLongitude != null && exif.GPSLatitude.length > 0 && exif.GPSLongitude.length > 0) {
        pictureData.ImageMetaLatitude = exif.GPSLatitude[0].toString();
        pictureData.ImageMetaLongitude = exif.GPSLongitude[0].toString();
    }

    return pictureData;
}

/*
     Scale image to the selected resolution.
     optimization: Selected image scaling.
*/
function scaleImage(optimization) {
    if (pictureData != null && pictureData.OrignalDataURI != null && pictureData.DataURI != null) {
        switch (optimization) {
            case 'Document':
            default:
                pictureData.CurrentWidth = imageResolutions.Document.Width;
                pictureData.CurrentHeight = imageResolutions.Document.Height;
                break;

            case 'WebPage':
                pictureData.CurrentWidth = imageResolutions.WebPage.Width;
                pictureData.CurrentHeight = imageResolutions.WebPage.Height;
                break;

            case 'Email':
                pictureData.CurrentWidth = imageResolutions.Email.Width;
                pictureData.CurrentHeight = imageResolutions.Email.Height;
                break;

            case 'Orignal':
                pictureData.CurrentWidth = pictureData.Width;
                pictureData.CurrentHeight = pictureData.Height;
                break;
        }

        var result = confirm('Your current progress will be lost!. Do you want to continue?');
        if (result) {
            scaleImageImplementation();
        }
    }
}

/*
     Scale image using canvas element.
*/
function scaleImageImplementation() {
    var tempImg = new Image();
    tempImg.onload = function (evt) {
        resetCanvas();

        var MAX_WIDTH = pictureData.CurrentWidth;
        var MAX_HEIGHT = pictureData.CurrentHeight;

        var tempW = tempImg.width;
        var tempH = tempImg.height;
        if (tempW > tempH) {
            if (tempW >= MAX_WIDTH) {
                tempH *= MAX_WIDTH / tempW;
                tempW = MAX_WIDTH;
            }
        } else {
            if (tempH >= MAX_HEIGHT) {
                tempW *= MAX_HEIGHT / tempH;
                tempH = MAX_HEIGHT;
            }
        }

        pictureData.CurrentWidth = tempW;
        pictureData.CurrentHeight = tempH;

        var resizecanvas = document.createElement('canvas');
        resizecanvas.width = tempW;
        resizecanvas.height = tempH;
        var resizectx = resizecanvas.getContext("2d");
        resizectx.drawImage(this, 0, 0, tempW, tempH);
        pictureData.DataURI = resizecanvas.toDataURL();
        renderCanvas();
    };

    tempImg.src = pictureData.OrignalDataURI;
}

/*
     Fires when the location checkbox state changes.
*/
function locationCheckChange() {
    if (pictureData.DataURI != null) {
        if (!jQuery("#locationCheck").is(':checked')) {
            jQuery("#locationChecklabel").html('<span class="ui-button-text">On</span>');
            if (pictureData.ImageMetaLatitude != null && pictureData.ImageMetaLongitude != null) {
                var result = confirm("The selected image already contains geotagging information.\nDo you want to overwrite it with current location information?")
                if (result) {
                    getLocationfromBrowser();
                }
                else {
                    pictureData.Latitude = pictureData.ImageMetaLatitude;
                    pictureData.Longitude = pictureData.ImageMetaLongitude;
                    //jQuery("#locationCheck").prop('checked', false);
                    //jQuery("#locationChecklabel").html('<span class="ui-button-text">Off</span>');
                }
            } else {
                getLocationfromBrowser();
            }
        }
        else {
            pictureData.Latitude = null;
            pictureData.Longitude = null;
            jQuery("#locationChecklabel").html('<span class="ui-button-text">Off</span>');
        }
    }
    else {
        alert('Please select a picture.');
        jQuery("#locationChecklabel").addClass('ui-state-active');
    }
}

/*
     Read geolocation from the browser capability.
*/
function getLocationfromBrowser() {
    if (navigator.geolocation) {
        var geolocation = navigator.geolocation;
        geolocation.getCurrentPosition(showLocation, geoErrorHandler);
    }
    else {
        alert('Geolocation is not supported by this browser.');
    }
}

/*
     Read geolocation from browser callback.
     position: geolocation object.
*/
function showLocation(position) {
    var latitude = position.coords.latitude;
    var longitude = position.coords.longitude;
    if (latitude != null && longitude != null) {
        pictureData.Latitude = latitude;
        pictureData.Longitude = longitude;
    }
}

/*
     Geolocation error callback.
*/
function geoErrorHandler(error) {
    var msg = null;
    switch (error.code) {
        case error.PERMISSION_DENIED:
            msg = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            msg = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            msg = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            msg = "An unknown error occurred."
            break;
    }

    alert(msg);
}

// Detect whether the current device supports touch.
// Note: This function is not getting used right now.
function isTouchDevice() {
    isTouchDevice = (('ontouchstart' in window) ||
        (navigator.msMaxTouchPoints > 0));
}

