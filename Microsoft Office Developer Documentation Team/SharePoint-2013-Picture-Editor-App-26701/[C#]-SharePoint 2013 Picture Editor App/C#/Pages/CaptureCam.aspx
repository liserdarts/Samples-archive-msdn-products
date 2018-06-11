<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Capture snapshot</title>
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="../_layouts/15/CUI.debug.js"></script>
    <script type="text/javascript" src="../_layouts/15/INIT.debug.js"></script>
    <script type="text/javascript" src="../_layouts/15/SP.Init.debug.js"></script>
    <script type="text/javascript" src="../_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="../_layouts/15/SP.debug.js"></script>
    <script type="text/javascript" src="../_layouts/15/SP.Core.debug.js"></script>
    <script type="text/javascript" src="../_layouts/15/SP.UI.Dialog.debug.js"></script>

    <script type="text/javascript" src="../Scripts/paintscripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/paintscripts/jquery-ui.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_Landing.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_Cam.js"></script>
    <script type="text/javascript">
        navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.getUserMedia;
        window.URL = window.URL || window.webkitURL;

        jQuery(document).ready(function () {
            getMediaCanvasObject();
            jQuery('#filenameblock').draggable();
        });
    </script>
</head>
<body style="height: 500px">
    <div id="videocontainer" align="center">
        <div id="videolabel" class="fontcss">Click Capture to start the video</div>
        <video id="monitor" autoplay="autoplay" width="500" height="400" class="videotag"></video>
        <img id="snapshotimage" alt="noimage" width="500" height="400" style="display: none" class="videotag" />
        <br />
        <input type="button" class="msbtn" value="Capture" onclick="CaptureButtonClick();" id="Capture" />
        <input type="button" class="msbtn" value="Snapshot" style="display: none" id="Snapshot" />
    </div>
    <canvas id="photo" style="display: none"></canvas>
    <div id="filenameblock" class="filenamebox" align="center">
        <p>
            <span style="font-weight: normal">File Name:</span>
            <input type="text" id="fileName" />
            <span>.jpg</span>
        </p>
    </div>
    <table align="right">
        <tr>
            <td>&nbsp;</td>
            <td nowrap="nowrap">
                <input name="" class="msbtn" id="OKCameraButton" accesskey="O" type="button" disabled="disabled" value="OK" onclick="javascript: cameraOKButtonClick();" />
                <input name="" class="msbtn" id="CancelCameraButton" accesskey="C" type="button" value="Cancel" onclick="stopMediaStream(); closeModal()" />
            </td>
        </tr>
    </table>
</body>
</html>
