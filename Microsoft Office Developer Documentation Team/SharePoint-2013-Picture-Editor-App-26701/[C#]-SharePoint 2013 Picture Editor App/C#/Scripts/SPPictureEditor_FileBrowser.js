
/*
    Launch the file browser dialog.
*/
function launchDesktopFileBrowser() {
    var options = {
        url: appData.AppHostUrl + "/SharePointPictureEditor/Pages/FilePickerDialog.aspx",
        title: "Select a picture",
        allowMaximize: false,
        showClose: true,
        height: 150,
        autoSize: false,
        dialogReturnValueCallback: dialogCallback
    };

    SP.UI.ModalDialog.showModalDialog(options);
}

/*
    File browser callback.
*/
function dialogCallback(dialogResult, returnValue) {
    if (dialogResult == SP.UI.DialogResult.OK && returnValue) {
        pictureData = returnValue;
        renderCanvas();
    }
}

/*
    Handle the file select event.
*/
function handleFileSelect(evt) {
    jQuery('#loadingfilecontainer').show();
    var file = evt.target.files[0];
    if (isValid.filereader === true && acceptedTypes[file.type] === true) {
        var reader = new FileReader();
        reader.onload = function (event) {
            var image = new Image();
            image.src = reader.result;
            pictureData.FileName = file.name;
            pictureData.DataURI = reader.result;
            pictureData.OrignalDataURI = reader.result;
            pictureData.ImageSource = pictureSource.FileBrowser;
            if (file.type == "image/jpeg") {
                pictureData = getLatLonFromImageData(pictureData);
            }

            doDefaultImageScaling(pictureData, function () { jQuery('#OKFileBrowser').removeAttr('disabled'); })
            jQuery('#loadingfilecontainer').hide();
        };
    }

    reader.readAsDataURL(file);
}

/*
    File browser OK button click.
*/
function modelOKButtonClick() {
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, pictureData);
}