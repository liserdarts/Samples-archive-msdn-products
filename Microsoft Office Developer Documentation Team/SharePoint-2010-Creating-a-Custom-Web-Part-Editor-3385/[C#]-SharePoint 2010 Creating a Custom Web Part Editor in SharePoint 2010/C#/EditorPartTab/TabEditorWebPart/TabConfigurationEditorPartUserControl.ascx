<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabConfigurationEditorPartUserControl.ascx.cs"
    Inherits="MSDN.SharePoint.Samples.TabConfigurationEditorPartUserControl, MSDN.SharePoint.Samples, Version=1.0.0.0, Culture=Neutral, PublicKeyToken=3aae0e8a62e006af" %>

<asp:HiddenField runat="server" ID="hiddenFieldDetectRequest" Value="0" />
<div style="width: 250px">
    <table cellpadding="5px" cellspacing="5px">
        <tr>
            <td>
                <fieldset title="Configured Tabs">
                    <legend>Configured Tabs</legend>
                    <asp:Panel ID="panelConfiguredTabs" runat="server">
                        <table cellpadding="5px" cellspacing="5px">
                            <tr style="padding-top: 5px; padding-bottom: 5px">
                                <td>
                                    <asp:Label runat="server" ID="labelConfiguredTabs" Text="Tab list:"></asp:Label>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:DropDownList ID="dropDownConfiguredTabs" runat="server" EnableViewState="true"
                                        AutoPostBack="true" OnTextChanged="dropDownConfiguredTabs_OnTextChanged">
                                        <asp:ListItem Text="Select one" Value="Select one"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="padding-top: 5px">
                                <td align="center">
                                    <asp:Button runat="server" ID="buttonAddNewTab" Text="Add New" OnClick="ButtonAddNewTab_OnClick"
                                        CausesValidation="false" />
                                </td>
                                <td align="center">
                                    <asp:Button runat="server" ID="buttonEditTab" Text="Edit" OnClick="ButtonEditTab_OnClick"
                                        CausesValidation="false" />
                                </td>
                                <td align="center">
                                    <asp:Button runat="server" ID="buttonDeleteTab" Text="Delete" OnClick="ButtonDeleteTab_OnClick"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="panelTabItem" runat="server" Visible="false">
                    <fieldset title="Tab Data" runat="server" id="fieldSetTitle">
                        <legend runat="server" id="legendTitle">Tab Data</legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="labelName" Text="Title:" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="textBoxTitle" runat="server" MaxLength="20"></asp:TextBox>
                                    <asp:Label runat="server" ForeColor="Red" Text="*" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="textBoxTitle" ErrorMessage="Title is required."
                                        ID="requiredFieldValidatorTextBoxTitle" EnableClientScript="true"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CustomValidator runat="server" ControlToValidate="textBoxTitle" ErrorMessage="Tab with same Title already exists."
                                        ID="customValidatorTextBoxTitle"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="labelURL" Text="Content:" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="htmlBody" runat="server" TextMode="MultiLine" Rows="6" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button runat="server" ID="buttonSave" Text="Save" OnClick="ButtonSave_Click"
                                        CausesValidation="true" Visible="false" />
                                    <asp:Button runat="server" ID="buttonCancel" Text="Cancel" OnClick="ButtonCancel_Click"
                                        CausesValidation="false" />&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="buttonSaveOnEdit" Text="Save" OnClick="ButtonSaveOnEdit_Click"
                                        CausesValidation="true" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
