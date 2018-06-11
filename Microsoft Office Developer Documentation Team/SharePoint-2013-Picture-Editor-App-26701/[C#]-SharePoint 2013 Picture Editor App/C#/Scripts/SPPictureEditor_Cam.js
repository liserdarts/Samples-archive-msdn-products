
/*
    Video object.
*/
var video = null;

/*
    Canvas for processing the captured image.
*/
var mediaCanvas = null;

/*
    Context for the canvas.
*/

var mediaContext = null;

/*
    Video label.
*/
var videolabel = null;

/*
    Media stream object to capture the video stream.
*/
var mediaStream = null;

/*
    True: Stop the media streaming.
*/
var isStopMedia = false;

/*
    Launch the camera dialog.
*/
function launchCameraDialog() {
    var options = {
        url: appData.AppHostUrl + "/SharePointPictureEditor/Pages/CaptureCam.aspx",
        title: "Click a picture",
        allowMaximize: false,
        showClose: true,
        height: 520,
        width: 600,
        autoSize: false,
        dialogReturnValueCallback: cameraDialogCallback
    };

    SP.UI.ModalDialog.showModalDialog(options);
}

/*
    Callback function when the dialog is closed.
*/
function cameraDialogCallback(dialogResult, returnValue) {
    if (dialogResult == SP.UI.DialogResult.OK && returnValue) {
        pictureData = returnValue;
        renderCanvas();
    }
}

/*
    Get the media canvas object.
*/
function getMediaCanvasObject() {
    jQuery('#snapshot').hide();
    video = document.getElementById('monitor');
    mediaCanvas = document.getElementById('photo');
    mediaContext = mediaCanvas.getContext('2d');
    videolabel = document.getElementById('videolabel');
}

/*
    Capture the media stream.
    stream: Stream from the web cam.
*/
function captureMediaStream(stream) {
    mediaStream = stream;

    if (window.URL) {
        video.src = window.URL.createObjectURL(mediaStream);
    } else {
        video.src = mediaStream; // Opera.
    }

    video.onerror = function (e) {
        alert('video error');
        mediaStream.stop();
    };

    mediaStream.onended = noMediaStream;

    video.onloadedmetadata = function (e) {
        //
    };

    setTimeout(function () {
        jQuery('#videolabel').text('Click Snapshot to capture the current frame');
        mediaCanvas.width = video.videoWidth;
        mediaCanvas.height = video.videoHeight;
    }, 50);
}

/*
    Callback when there is no media device, or any other error. 
    e: event object
*/
function noMediaStream(e) {
    if (!isStopMedia) {
        var msg = 'No camera available.';
        if (e.code == 1) {
            msg = 'User denied access to use camera.';
        }

        alert(msg);
    }
}

/*
    Draw the captured media stream on the canvas.
*/
function drawCapturedMedia() {
    mediaContext.drawImage(video, 0, 0);
    stopMediaStream();

    jQuery('#monitor').hide();
    jQuery('#snapshotimage').show();
    jQuery('#snapshotimage').attr('src', mediaCanvas.toDataURL());
    jQuery('#Capture').show();
    jQuery('#Snapshot').hide();

    pictureData.DataURI = mediaCanvas.toDataURL();
    pictureData.OrignalDataURI = pictureData.DataURI;

    pictureData.ImageSource = pictureSource.DeviceCamera;
    pictureData.Width = mediaCanvas.width;
    pictureData.Height = mediaCanvas.height;

    pictureData.CurrentWidth = mediaCanvas.width;
    pictureData.CurrentHeight = mediaCanvas.height;

    jQuery('#filenameblock').show();
    jQuery('#OKCameraButton').removeAttr('disabled');
}

/*
    Stop the media streaming and turn off the device camera.
*/
function stopMediaStream() {
    isStopMedia = true;
    if (mediaStream != null) {
        mediaStream.stop();
    }
}

/*
    Capture button click.
*/
function CaptureButtonClick() {
    if (!navigator.getUserMedia) {
        alert('Sorry. This functionality is not supported on your browser.');

        return;
    }
    else {
        isStopMedia = false;
        jQuery('#monitor').show();
        jQuery('#Capture').hide();
        jQuery('#Snapshot').show();
        jQuery('#snapshotimage').hide();
        jQuery('#filenameblock').hide();
        jQuery('#OKCameraButton').attr('disabled', 'disabled');
        jQuery('#Snapshot').click(function () {
            drawCapturedMedia();
        });

        navigator.getUserMedia({ video: true }, captureMediaStream, noMediaStream);
    }
}

/*
    OK button click.
*/
function cameraOKButtonClick() {
    if (jQuery.trim(jQuery('#fileName').val()) != '') {
        pictureData.FileName = jQuery.trim(jQuery('#fileName').val()) + ".jpg";
        SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, pictureData);
    }
    else {
        alert('Please provide a valid file name.');
    }
}