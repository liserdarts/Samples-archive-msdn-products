<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink Name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript" src="../Scripts/paintscripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_Landing.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_DragDrop.js"></script>
    <script type="text/javascript" src="../Scripts/EXIF_Library.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('.ms-cui-topBar2').hide();
            attachDragDropEvents();
            if (hasGetUserMedia()) {
                jQuery('#ismediaavailable').css('background-image', 'url(../Images/landingimages/camera.png)');
                jQuery('#ismediaavailable').css("cursor", "pointer");
                jQuery('#ismediaavailable').css("cursor", "hand");
                jQuery('#ismediaavailable').attr("title", "Click here to access your device camera.");
                jQuery('#ismediaavailable').click(function () {
                    selectfromDeviceCamera();
                });
            }
        });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div align="center" style="width: 100%; height: 100%">
        <table style="border-width: 1px; border-spacing: 8px">
            <tr>
                <td>
                    <div class="browsefolderop"
                        title="Click here to select image from your desktop." onclick="javascript:selectfromFileBrowser();">
                        <span class="landingcube" style="bottom: 54%; left: 6%">Desktop</span>
                    </div>
                </td>
                <td>
                    <div id="ismediaavailable" class="cameraop"
                        title="Not available">
                        <span class="landingcube" style="bottom: 54%; left: 54%">Device camera</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="spop"
                        title="Click here to select image from SharePoint." onclick="javascript:selectfromSharePoint();">
                        <span class="landingcube" style="bottom: 6%; left: 6%">SharePoint picture library</span>
                    </div>
                </td>
                <td>
                    <div id="drop_zonecontainer" class="dndop"
                        title="Drag and drop a image here.">
                        <div id="drop_zone"></div>
                        <span class="landingcube" style="bottom: 6%; left: 54%">Drag and drop here</span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
