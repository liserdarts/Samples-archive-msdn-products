<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SQLReportingAdminPage.aspx.cs"
    Inherits="ManagePermission.SQLReportingAdminPage" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SQL Reporting : Manage Permissions Sample</title>
    <style type="text/css">
        #tbl
        {
            height: 116px;
            width: 902px;
        }
        .modalBackground
        {
            background-color: Gray;
            -ms-filter: "Alpha(Opacity=70)";
        }
        .modalPopup
        {
            background-color: #EEEEEE;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            font-family: Verdana;
            font-size: medium;
            padding: 3px;
            width: 250px;
        }
        .style2
        {
            width: 803px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p style="font-family: 'MS UI Gothic'; font-size: large; font-weight: bold; font-style: normal;text-align: center">SQL Reporting sample : Manage Permissions</p>
    <p style="font-family: 'MS UI Gothic'; font-size: medium; font-weight: bold; font-style: normal;text-align: center">
        <asp:Label ID="lblLoggedInUser" runat="server"/>
    </p>
                               
    <div style="height: 544px; width: 912px">
        <table id="tbl" cellpadding="0px" cellspacing="0px">
            <tr>
                <asp:Button runat="server" ID="btnItemPermissions" Text="Permissions" OnClick="btnItemPermissions_Click" />
                <asp:Button ID="btnHidden" runat="server" Style="display: none;" Text="Button" />
                <asp:Button ID="btnException" runat="server" Style="display: none;" Text="Button" />                  
            </tr>
            <tr>
                <td class="style2">
                    <asp:GridView ID="gvListChildren" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="901px" Height="16px" AutoGenerateSelectButton="True"
                        Style="margin-top: 5px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Item Name" DataField="Name" />
                            <asp:BoundField HeaderText="Item Path" DataField="Path" />
                            <asp:BoundField HeaderText="Item Type" DataField="TypeName" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <br />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="pnlException" runat="server" Width="600px" Height="200px" ScrollBars="Auto"
            BackColor="#E6E6E6">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblException" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <asp:Button ID="btnExceptionCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlEditCustomer" Style="display: none" Width="600px"
            Font-Names="@MS PGothic" BackColor="#E6E6E6" BorderStyle="Groove" Height="400px"
            CssClass="modalPopup" ScrollBars="Both">
            <span style="color: #cc0000"><strong><span style="font-family: 'MS UI Gothic'; font-weight: bold;
                text-align: center;">Manage Permissions<br />
            </span></strong>
                <br />
            </span>
            <table align="center">
                <tr>
                    <td align="center">
                        View and update permissions for the selected report item. Any changes you make to
                        the policies cause the item to no longer inherit from its parent policies.<br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvItemPermissions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Style="margin-left: 24px" AutoGenerateEditButton="True" OnPageIndexChanging="gvItemPermissions_PageIndexChanging"
                            OnRowCancelingEdit="gvItemPermissions_RowCancelingEdit" OnRowEditing="gvItemPermissions_RowEditing"
                            OnRowUpdating="gvItemPermissions_RowUpdating" BackColor="White" BorderColor="#3366CC"
                            BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnOk" runat="server" Text="Save" OnClick="btnOk_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblInfo" runat="server" Text="" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnHidden"
            PopupControlID="pnlEditCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnException"
            PopupControlID="pnlException" BackgroundCssClass="modalBackground" CancelControlID="btnExceptionCancel"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <br />
    </div>
    </form>
</body>
</html>
