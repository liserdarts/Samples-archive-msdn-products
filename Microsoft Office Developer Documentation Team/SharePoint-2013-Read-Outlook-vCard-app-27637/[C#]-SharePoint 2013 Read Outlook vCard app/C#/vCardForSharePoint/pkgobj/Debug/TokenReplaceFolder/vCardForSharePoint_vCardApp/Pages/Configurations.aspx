<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.UI.Dialog.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.UserProfiles.js"></script>
    <link type="text/css" rel="Stylesheet" href="/_layouts/15/1033/styles/forms.css" />
    <link rel="Stylesheet" type="text/css" href="../Content/jquery-ui.css" />
    <link rel="Stylesheet" type="text/css" href="../Content/jquery.multiselect.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <table width="100%" class="ms-formtable" style="margin-top: 8px;" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>
                            Download Profile Picture
                        </h3>
                    </td>
                    <td width="350" class="ms-formbody" valign="top">
                        <input id="downloadProfilePic" type="checkbox" checked="checked" />
                        <span>Default image property: PictureURL</span>
                    </td>
                </tr>
                <tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>
                            VCF File Name: Properties
                        </h3>
                    </td>
                    <td width="350" class="ms-formbody" valign="top">
                        <span dir="none">
                            <div id="outervcffilediv">
                            </div>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>
                            VCF File Name: Seperator
                        </h3>
                    </td>
                    <td width="350" class="ms-formbody" valign="top">
                        <span dir="none">
                            <input title="VCF File Name: Seperator" class="ms-input" style="width: 20px" id="propertySeperator" maxlength="1" />
                            <span>Leave empty for single space.</span>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>
                            Allow Post to News Feed
                        </h3>
                    </td>
                    <td width="350" class="ms-formbody" valign="top">
                        <span dir="none">
                            <input id="postToFeed" type="checkbox" checked="checked" />
                            <span>Allow the app to post message on the current user's news feed.<br />e.g. Hey Robert Lyon, I just downloaded your virtual business card</span>
                        </span>
                    </td>
                </tr>
                <!--<tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>
                            Display Order
                        </h3>
                    </td>
                    <td width="350" class="ms-formbody" valign="top">
                        <span dir="none">
                            <input title="Display Order" class="ms-input" id="vOrder_cd18dd7e-27e1-40af-80bb-d1427f4d9a09_$NumberField" style="-ms-ime-mode: inactive;" type="text" size="11" value=""><br>
                        </span></td>
                </tr>
                <tr>
                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                        <h3 class="ms-standardheader">
                            <nobr>Formatting Rule</nobr>
                        </h3>
                    </td>
                    <td width="350" class="ms-formbody" valign="top">
                        <input class="ms-long ms-spellcheck-true" id="formatFunctionInput" type="text" value="">
                    </td>
                </tr>-->
            </tbody>
        </table>
        <p></p>
        <table width="100%" class="ms-formtoolbar" border="0" cellspacing="0" cellpadding="2">
            <tbody>
                <tr>
                    <td width="99%" class="ms-toolbar" nowrap="nowrap">
                        <img width="1" height="18" alt="" src="/_layouts/15/images/blank.gif?rev=23"></td>
                    <td class="ms-toolbar" nowrap="nowrap">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td width="100%" align="right" nowrap="nowrap">
                                        <input name="save" class="ms-ButtonHeightWidth" id="savebutton" accesskey="O" onclick="javascript: acceptChangeAndClose()" type="button" value="Save" target="_self">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="ms-separator">&nbsp;</td>
                    <td class="ms-toolbar" nowrap="nowrap">
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td width="100%" align="right" nowrap="nowrap">
                                        <input name="cancel" class="ms-ButtonHeightWidth" id="cancelbutton" accesskey="C" onclick='javascript: closeDialog();' type="button" value="Cancel" target="_self">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
