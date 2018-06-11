
/*
    Variable with the ID of the field which contains the file name.
*/
var itemSelectedFileName = 'picturefilename';

/*
   Variable with the ID of the field which contains the file url.
*/
var itemSelectedFileURL = 'picturefileurl';

/*
    Create the SharePoint asset picker control.
*/
function createAssetPickerControl() {
    var assetPicker = new AssetPickerConfig('SPPictureEditor_AssetPickerObj');
    assetPicker.DefaultAssetImageLocation = '';
    assetPicker.CurrentWebBaseUrl = appData.HostUrl;
    assetPicker.OverrideDialogFeatures = '';
    assetPicker.OverrideDialogTitle = 'Select a picture';
    assetPicker.OverrideDialogDesc = 'Select a picture';;
    assetPicker.OverrideDialogImageUrl = '';
    assetPicker.AssetUrlClientID = itemSelectedFileURL;
    assetPicker.AssetTextClientID = itemSelectedFileName;
    assetPicker.UseImageAssetPicker = true;
    assetPicker.DefaultToLastUsedLocation = true;
    assetPicker.DisplayLookInSection = false;
    assetPicker.AllowExternalUrls = false;
    assetPicker.ReturnCallback = assetPickerCallback;

    APD_LaunchAssetPickerUseConfigCurrentUrl('SPPictureEditor_AssetPickerObj');
}

/*
    Launch the asset picker.
*/
function launchAssetPicker() {
    createAssetPickerControl();
    jQuery('#' + itemSelectedFileName).val("");
    jQuery('#' + itemSelectedFileURL).val("");
    resetCanvas();
}

/*
    Executes when the user selects the file and closes the asset picker.
*/
function assetPickerCallback() {
    if (jQuery('#' + itemSelectedFileName) != null && jQuery('#' + itemSelectedFileURL) != null) {
        var oLinktext = jQuery('#' + itemSelectedFileName).val();
        var oLinkurl = jQuery('#' + itemSelectedFileURL).val();
        if (jQuery.trim(oLinktext) != '' && jQuery.trim(oLinkurl) != '') {
            getSPImageParamsForSharePointMode();
            validateLibrary(1);
        }
        else {
            //alert("No item is selected from the Library.");
        }
    }
    else {
        alert("Invalid Item");
    }
}

/*
    Process the selected item for downloading in the app.
*/
function processAssetPickerData() {
    var soapPacket = generateDownloadPacket();
    executeDownloadServiceRequest(soapPacket);
}

/*
    Process the data returned from the web service response (handleDownloadImageResponse).
*/
function processWebSvsResponse(imagedatauri) {
    pictureData.DataURI = imagedatauri;
    pictureData.OrignalDataURI = pictureData.DataURI;
    doDefaultImageScaling(pictureData, function () { renderCanvas() })
}

/*
    Get the SharePoint image parameters.
*/
function getSPImageParamsForSharePointMode() {
    var oLinkurl = jQuery('#' + itemSelectedFileURL).val();
    setImageLibraryFullPath(oLinkurl.toLowerCase().replace(appData.AppHostRelativeUrl.toLowerCase(), ''));
    pictureData.FileName = jQuery('#' + itemSelectedFileName).val();
}
