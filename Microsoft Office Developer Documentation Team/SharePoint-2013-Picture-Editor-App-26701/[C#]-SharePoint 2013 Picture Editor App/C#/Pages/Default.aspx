<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    SharePoint Picture Editor
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderSiteName" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- jQuery -->

    <link rel="stylesheet" href="../Content/jquerycontrol/jquery-ui.css" />
    <link rel="stylesheet" href="../Content/jquerycontrol/jquery-ui-style.css" />

    <script type="text/javascript" src="../Scripts/paintscripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/paintscripts/jquery-ui.js"></script>
    <script type="text/javascript" src="../Scripts/paintscripts/jquery.ui.touch-punch.min.js"></script>

    <!-- wColorPicker -->
    <link rel="Stylesheet" type="text/css" href="../Content/wColorPicker.css" />
    <script type="text/javascript" src="../Scripts/paintscripts/wColorPicker.js"></script>

    <!-- wPaint -->
    <link rel="Stylesheet" type="text/css" href="../Content/wPaint.css" />
    <script type="text/javascript" src="../Scripts/paintscripts/wPaint.js"></script>

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="~/_layouts/15/AssetPickers.js"></script>
    <script type="text/javascript" src="~/_layouts/15/PickerTreeDialog.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_Landing.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_FileBrowser.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_AssetPicker.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_Cam.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_CanvasEditor.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_SPDataLibrary.js"></script>

    <script type="text/javascript">
        loadUIControls();
        function hidedownloadLinkContainer() {
            jQuery("#downloadLinkContainer").hide();
        }
    </script>
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="loadingcontainer" class="ms-dlgOverlay loadingContainer" align="center">
        <span>
            <img src="../Images/paintimages/loading.gif" alt="loading" style="margin-top: 7%" />
        </span>
    </div>
    <div id="downloadLinkContainer" class="downloadBox" align="center">
        <img src="/_layouts/images/CNSREJ16.GIF" alt="close" title="close" onclick="hidedownloadLinkContainer();"
            style="right: 0px; position: absolute; cursor: hand; cursor: pointer" />
        <a id="dwlink" onclick="hidedownloadLinkContainer();" href="#" download="file.png"
            style="vertical-align: middle; padding-top: 35px; display: inline-block">Click here to download image</a>
    </div>
    <input type="hidden" id="picturefileurl" value="" />
    <input type="hidden" id="picturefilename" value="" />
    <div style="padding-left: 5px;" align="center">
        <div id="menucontainer" class="menucontainer">
            <table style="text-align: center;">
                <tr>
                    <td class="toolbar">
                        <a class="enableanchor" href="javascript:resetApplication();launchLandingPage();">
                            <img class="imgactive" src="../Images/toobar_icons/SelectNew.png" alt="New" />
                            <br />
                            <span>New</span>
                        </a>
                    </td>
                    <td class="toolbar">
                        <a id="savelink" class="disableanchor" href="javascript:saveFileToSharePoint();">
                            <img id="savelinkimg" class="imgactive disableimage" src="../Images/toobar_icons/Save.png" alt="Save" />
                            <br />
                            <span>Save</span>
                        </a>
                    </td>
                    <td class="toolbar">
                        <a id="saveaslink" class="disableanchor" href="javascript:saveFileAsToSharePoint();">
                            <img id="saveaslinkimg" class="imgactive disableimage" src="../Images/toobar_icons/SaveAs.png" alt="Save As" />
                            <br />
                            <span>Save As</span>
                        </a>
                    </td>
                    <td class="toolbar">
                        <a id="resetimglink" class="disableanchor" href="javascript:resetImage();">
                            <img id="resetimglinkimg" class="imgactive disableimage" src="../Images/toobar_icons/Refresh.png" alt="Reset" />
                            <br />
                            <span>Clear Image</span>
                        </a>
                    </td>
                    <td class="toolbar">
                        <a id="clearimglink" class="disableanchor" href="javascript:resetCanvas();">
                            <img id="clearimglinkimg" class="imgactive disableimage" src="../Images/toobar_icons/Clear.png" alt="Clear" />
                            <br />
                            <span>Reset</span>
                        </a>
                    </td>
                    <td class="toolbar">
                        <div align="left">
                            <div>
                                <button id="rerunlast">Document</button>
                                <button id="select" style="background-image: url('/_layouts/images/jsgrid-down-arrow.png'); background-repeat: no-repeat;">Select an action</button>
                            </div>
                            <ul style="list-style: none; z-index: 1000">
                                <li><a href="#" onclick="scaleImage('Orignal');">Orignal</a></li>
                                <li><a href="#" onclick="scaleImage('Document');">Document</a></li>
                                <li><a href="#" onclick="scaleImage('WebPage');">Web Page</a></li>
                                <li><a href="#" onclick="scaleImage('Email');">Email</a></li>
                            </ul>
                        </div>
                        <div class="enableanchor">Optimize Image</div>
                    </td>
                    <td class="toolbar">
                        <div>
                            <input type="checkbox" id="locationCheck" checked="checked" onchange="locationCheckChange();" />
                            <label id="locationChecklabel" for="locationCheck">Off</label>
                        </div>
                        <div class="enableanchor">Use Client Geolocation</div>
                    </td>
                    <td class="toolbar">
                        <a id="downloadlink" class="disableanchor" href="javascript:downloadImageOnClient();">
                            <img id="downloadlinkimg" class="imgactive disableimage" src="../Images/toobar_icons/Download.png" alt="Download image" />
                            <br />
                            <span class="enableanchor">Download Image</span>
                        </a>
                    </td>
                </tr>
            </table>
        </div>
        <div id="wPaint" class="wPaint"></div>
    </div>
</asp:Content>
