
/*
    Render the canvas element for drawing.
*/
function renderCanvas() {
    getSPPaintCanvasReady();
    jQuery("#paintarea").show();
    jQuery("#paintarea").css("visibility", "visible");
    disableEnableAnchors(true, 'savelink');
    disableEnableAnchors(true, 'resetimglink');
    disableEnableAnchors(true, 'clearimglink');
    disableEnableAnchors(true, 'downloadlink');
}

/*
    Reset the canvas and remove the image from canvas.
*/
function resetImage() {
    jQuery("#wPaint").wPaint("clear");
    pictureData.CurrentWidth = jQuery("#wPaint").width();
    pictureData.CurrentHeight = jQuery("#wPaint").height();
    resizeCanvas();
    jQuery('#wPaint').wPaint('image', pictureData.DataURI);
}

/*
     Reset the canvas and undo all changes.
*/
function resetCanvas() {
    clearCanvas();
    jQuery("#wPaint").hide();
    jQuery("._wPaint_menu").hide();
    pictureData.DataURI = null;
    disableEnableAnchors(false, 'savelink');
    disableEnableAnchors(false, 'saveaslink');
    disableEnableAnchors(false, 'resetimglink');
    disableEnableAnchors(false, 'clearimglink');
    disableEnableAnchors(false, 'downloadlink');

    jQuery("#wPaint").width(640);
    jQuery("#wPaint").height(480);
    jQuery("#menucontainer").width(900);
    //localStorage.removeItem("sp_picedit");
    //imagesource = null;
    //localStorage.clear();
}

/*
     Draw the image data in the canvas.
*/
function getSPPaintCanvasReady() {
    resizeCanvas();
    jQuery("#wPaint").show();
    jQuery("#wPaint").wPaint({
        image: pictureData.DataURI
    });
}

/*
    Resize the canvas according to the image dimensions.
*/
function resizeCanvas() {
    if (pictureData.CurrentWidth != null) {
        jQuery("#wPaint").width(pictureData.CurrentWidth);
        if (pictureData.CurrentWidth >= 900) {
            jQuery("#menucontainer").width(pictureData.CurrentWidth);
        }
        else {
            jQuery("#menucontainer").width(900);
        }
    }
    else {
        jQuery("#wPaint").width(640);
        jQuery("#menucontainer").width(900);
    }

    if (pictureData.CurrentHeight != null) {
        jQuery("#wPaint").height(pictureData.CurrentHeight);
    }
    else {
        jQuery("#wPaint").height(480);
    }
}

/*
    Get the image drawn on the canvas.
*/
function saveCanvas() {
    var imageData = jQuery("#wPaint").wPaint("image");
    //localStorage.setItem("sp_picedit", imageData);
    pictureData.DataURI = imageData;
}

/*
    Clear all the changes done to the image.
*/
function clearCanvas() {
    jQuery("#wPaint").wPaint("clear");
}

/*
    Download the image on the client side.
*/
function downloadImageOnClient() {
    var imageData = jQuery("#wPaint").wPaint("image");
    var image_data = atob(imageData.split(',')[1]);
    var arraybuffer = new ArrayBuffer(image_data.length);
    var view = new Uint8Array(arraybuffer);
    for (var i = 0; i < image_data.length; i++) {
        view[i] = image_data.charCodeAt(i) & 0xff;
    }

    var blob = new Blob([arraybuffer], { type: 'application/octet-stream' });
    if (window.navigator.msSaveBlob) {
        jQuery('#dwlink').attr("download", pictureData.FileName);
        jQuery('#dwlink').click(function () { window.navigator.msSaveBlob(blob, pictureData.FileName); });
        jQuery('#downloadLinkContainer').show();
    }
    else {
        var url = (window.webkitURL || window.URL).createObjectURL(blob);
        jQuery('#dwlink').attr("href", url);
        jQuery('#dwlink').attr("download", pictureData.FileName);
        jQuery('#downloadLinkContainer').show();
    }
}

//function saveToLocalStorage() {
//    //localStorage.removeItem("sp_picedit");
//    imagesource = null;
//    var dataURL = canvaso.toDataURL();
//    //localStorage.setItem("sp_picedit", dataURL);
//    imagesource = dataURL;
//    pictureData.ImageSource = dataURL;
//    jQuery("#savetosp").prop('disabled', false);
//}

