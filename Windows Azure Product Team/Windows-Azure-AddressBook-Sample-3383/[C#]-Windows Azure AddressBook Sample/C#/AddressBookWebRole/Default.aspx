<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AddressBookWebRole._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <asp:ObjectDataSource ID="sourceContacts" runat="server" SelectMethod="GetContact"
        UpdateMethod="UpdateContact" InsertMethod="InsertContact" DeleteMethod="DeleteContact" ConflictDetection="OverwriteChanges" TypeName="AddressBookWebRole.DataLayer">
        <SelectParameters>
            <asp:ControlParameter Name="RowKey" ControlID="lstContactNames" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:FormParameter Name="rowKey"  FormField="frmDetail$hdnRowKey" />
            <asp:ControlParameter Name="firstName" ControlID="frmDetail$txtFirstNameUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="lastName" ControlID="frmDetail$txtLastNameUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="email" ControlID="frmDetail$txtEmailUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="cellPhone" ControlID="frmDetail$txtCellPhoneUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="homePhone" ControlID="frmDetail$txtHomePhoneUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="streetAddress" ControlID="frmDetail$txtStreetAddressUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="city" ControlID="frmDetail$txtCityUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="state" ControlID="frmDetail$txtStateUpdate" PropertyName="Text" />
            <asp:ControlParameter Name="zipCode" ControlID="frmDetail$txtZipCodeUpdate" PropertyName="Text" />
        </UpdateParameters>
        <InsertParameters>
            <asp:ControlParameter Name="firstName" ControlID="frmDetail$txtFirstNameInsert" PropertyName="Text" />
            <asp:ControlParameter Name="lastName" ControlID="frmDetail$txtLastNameInsert" PropertyName="Text" />
            <asp:ControlParameter Name="email" ControlID="frmDetail$txtEmailInsert" PropertyName="Text" />
            <asp:ControlParameter Name="cellPhone" ControlID="frmDetail$txtCellPhoneInsert" PropertyName="Text" />
            <asp:ControlParameter Name="homePhone" ControlID="frmDetail$txtHomePhoneInsert" PropertyName="Text" />
            <asp:ControlParameter Name="streetAddress" ControlID="frmDetail$txtStreetAddressInsert" PropertyName="Text" />
            <asp:ControlParameter Name="city" ControlID="frmDetail$txtCityInsert" PropertyName="Text" />
            <asp:ControlParameter Name="state" ControlID="frmDetail$txtStateInsert" PropertyName="Text" />
            <asp:ControlParameter Name="zipCode" ControlID="frmDetail$txtZipCodeInsert" PropertyName="Text" />
        </InsertParameters>
        <DeleteParameters>
            <asp:ControlParameter Name="RowKey" ControlID="lstContactNames" PropertyName="SelectedValue" />
        </DeleteParameters>
    </asp:ObjectDataSource>

    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                <asp:Panel ID="pnlFilter" runat="server" Width="100%" >
                    <asp:Repeater ID="ButtonRepeater" runat="server" OnItemCommand="ClickFilter">
                        <ItemTemplate>
                            &nbsp;<asp:Button ID="Button1" runat="server" Text='<%# Container.DataItem %>' 
                                Width="38" BorderWidth='<%# (this.ViewState["filter"] ?? "").ToString() == Container.DataItem.ToString() ? 2 : 1 %>' 
                                CommandName="Filter" CommandArgument='<%# Container.DataItem %>' /><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

            <td valign="top">
                <asp:ListBox ID="lstContactNames" runat="server" AutoPostBack="true" Height="523" Width="150"></asp:ListBox>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

            <td valign="top">
                <asp:FormView ID="frmDetail" runat="server" DataSourceID="sourceContacts" 
                    Width="440px" oniteminserted="frmDetail_ItemInserted" 
                    onitemupdated="frmDetail_ItemUpdated" 
                    onitemdeleted="frmDetail_ItemDeleted" DataKeyNames="RowKey">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnRowKey" runat="server" Value='<%# Eval("RowKey") %>' />
                        <table border="1" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td style="font-family:verdana;font-size:x-large;color:Gray"><%# Eval("LastName") %>, <%# Eval("FirstName") %></td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td>
                                                <b>First Name</b>
                                            </td>
                                            <td>
                                                <%# Eval("FirstName") %>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>Street Address</b>
                                            </td>
                                            <td>
                                                <%# Eval("StreetAddress") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Last Name</b>
                                            </td>
                                            <td>
                                                <%# Eval("LastName") %>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>City</b>
                                            </td>
                                            <td>
                                                <%# Eval("City") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Email</b>
                                            </td>
                                            <td>
                                                <%# Eval("Email") %>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>State</b>
                                            </td>
                                            <td>
                                                <%# Eval("State") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Cell Phone</b>
                                            </td>
                                            <td>
                                                <%# Eval("CellPhone") %>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>Zip Code</b>
                                            </td>
                                            <td>
                                                <%# Eval("ZipCode") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Home Phone</b>
                                            </td>
                                            <td colspan="4">
                                                <%# Eval("HomePhone") %>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit Contact" CommandName="Edit" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete Contact" CommandName="Delete" />
                        <br />
                        <br />
                        <asp:Button ID="btnNew" runat="server" Text="New Contact..." CommandName="New" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdnRowKey" runat="server" Value='<%# Bind("RowKey") %>' />

                        <table border="1" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td style="font-family:verdana;font-size:x-large;color:Gray"><%# Eval("LastName") %>, <%# Eval("FirstName") %></td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td>
                                                <b>First Name</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtFirstNameUpdate" text='<%# Bind("FirstName") %>' runat="server" TabIndex="1"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>Street Address</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtStreetAddressUpdate" text='<%# Bind("StreetAddress") %>' runat="server"  TabIndex="6"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Last Name</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtLastNameUpdate" text='<%# Bind("LastName") %>' runat="server"  TabIndex="2"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>City</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtCityUpdate" text='<%# Bind("City") %>' runat="server"  TabIndex="7"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Email</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtEmailUpdate" text='<%# Bind("Email") %>' runat="server"  TabIndex="3"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>State</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtStateUpdate" text='<%# Bind("State") %>' runat="server" TabIndex="8"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Cell Phone</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtCellPhoneUpdate" text='<%# Bind("CellPhone") %>' runat="server" TabIndex="4"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>Zip Code</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtZipCodeUpdate" text='<%# Bind("ZipCode") %>' runat="server"  TabIndex="9"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Home Phone</b>
                                            </td>
                                            <td colspan="4">
                                                <asp:textbox id="txtHomePhoneUpdate" text='<%# Bind("HomePhone") %>' runat="server" TabIndex="5"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                        <br />
                        <br />
                    </EditItemTemplate>

                    <InsertItemTemplate>
                        <table border="1" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td>
                                                <b>First Name</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtFirstNameInsert" text='<%# Bind("FirstName") %>' runat="server" TabIndex="1"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>Street Address</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtStreetAddressInsert" text='<%# Bind("StreetAddress") %>' runat="server" TabIndex="6"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Last Name</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtLastNameInsert" text='<%# Bind("LastName") %>' runat="server" TabIndex="2"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>City</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtCityInsert" text='<%# Bind("City") %>' runat="server" TabIndex="7"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Email</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtEmailInsert" text='<%# Bind("Email") %>' runat="server" TabIndex="3"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>State</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtStateInsert" text='<%# Bind("State") %>' runat="server" TabIndex="8"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Cell Phone</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtCellPhoneInsert" text='<%# Bind("CellPhone") %>' runat="server" TabIndex="4"/>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <b>Zip Code</b>
                                            </td>
                                            <td>
                                                <asp:textbox id="txtZipCodeInsert" text='<%# Bind("ZipCode") %>' runat="server" TabIndex="9"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Home Phone</b>
                                            </td>
                                            <td colspan="4">
                                                <asp:textbox id="txtHomePhoneInsert" text='<%# Bind("HomePhone") %>' runat="server" TabIndex="5"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnInsert" runat="server" Text="Insert" CommandName="Insert" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                        <br />
                        <br />
                    </InsertItemTemplate>
                </asp:FormView>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td valign="top">
                <asp:Button ID="btnImportPage" runat="server" Text="Import Contacts..." 
                    onclick="btnImportPage_Click" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
