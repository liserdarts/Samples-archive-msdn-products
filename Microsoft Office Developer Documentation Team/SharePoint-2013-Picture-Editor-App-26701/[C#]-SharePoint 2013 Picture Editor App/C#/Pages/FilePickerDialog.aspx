<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <SharePoint:ScriptLink Name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <script type="text/javascript" src="../Scripts/paintscripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_Landing.js"></script>
    <script type="text/javascript" src="../Scripts/SPPictureEditor_FileBrowser.js"></script>
    <script type="text/javascript" src="../Scripts/EXIF_Library.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            document.getElementById('browseFile').addEventListener('change', handleFileSelect, false);
        });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="loadingfilecontainer" class="ms-dlgOverlay fileloading" align="center">
        <span>
            <img src="../_layouts/images/loading16.gif" alt="loading" align="middle" />&nbsp;<span>Loading image, please wait...</span>
        </span>
    </div>
    <div align="right">
        <span style="width: 30%;">Choose a file:</span>&nbsp;&nbsp;&nbsp;
            <input type="file" id="browseFile" style="width: 65%; margin-left: 15%" />
    </div>
    <br />
    <table align="right">
        <tr>
            <td>&nbsp;</td>
            <td nowrap="nowrap">
                <input name="" class="ButtonHeightWidth" id="OKFileBrowser" disabled="disabled" accesskey="O" type="button" value="OK" onclick="modelOKButtonClick()" />
                <input name="" class="ButtonHeightWidth" id="Cancel" accesskey="C" type="button" value="Cancel" onclick="closeModal()" />
            </td>
        </tr>
    </table>
</asp:Content>
