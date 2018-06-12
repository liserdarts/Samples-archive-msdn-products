<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jsrender.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.UI.Dialog.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.UserProfiles.js"></script>
    <script type="text/javascript" src="../Scripts/vCard_viewDataMappings.js"></script>
    <link type="text/css" rel="Stylesheet" href="/_layouts/15/1033/styles/forms.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    View Mappings with User Profile Data
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink5" Name="clienttemplates.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink6" Name="clientforms.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink7" Name="clientpeoplepicker.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink8" Name="autofill.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <script id="propertytemplate" type="text/x-jsrender">
        {{if #index == 0 }}
        <tr>
            <td width="400" class="ms-formlabel" valign="top" style="background-color:#6198b6;">
                <h3 class="ms-standardheader" style="text-align: center;color:white">
                    <nobr><b>
                    UserProfile Properties</b>
                </h3>
            </td>
            <td width="650" class="ms-formbody" valign="top" style="background-color:#6198b6;">
                <h3 class="ms-standardheader" style="text-align: center;color:white">
                    <nobr><b>
                    UserProfile Property Values</b>
                </h3>
            </td>
        </tr>
        {{/if}}
        <tr>
            <td width="400" class="ms-formlabel" valign="top" style="background-color: #f3f3f3">
                <h3 class="ms-standardheader" style="padding-left: 5px; padding-right: 5px">
                   
                    {{>#data.UPPropertyNameArray}}
                </h3>
            </td>
            <td width="650" class="ms-formbody" valign="top" style="background-color: #f3f3f3; padding-left: 5px; padding-right: 5px">{{>#data.UPPropertyValueArray}}
            </td>
        </tr>

    </script>
    <div style="height: 500px">
        <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>
                            Person
                        </h3>
                    </td>
                    <td width="250" class="ms-formbody" valign="top">
                        <span id="peoplePicker"></span>
                    </td>
                    <td width="150" class="ms-formbody" valign="top">
                        <input class="ms-ButtonHeightWidth" id="executeactions" type="button" value="Load Properties" onclick="executeViewScripts();" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="1" cellspacing="0" cellpadding="0">
            <tbody id="propertytable">
            </tbody>
        </table>
    </div>
    <table width="600px" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td width="100%" align="right" nowrap="nowrap">
                    <input name="close" class="ms-ButtonHeightWidth" id="Button1" accesskey="O" onclick=" SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, 'Cancelled');" type="button" value="Close" target="_self">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
