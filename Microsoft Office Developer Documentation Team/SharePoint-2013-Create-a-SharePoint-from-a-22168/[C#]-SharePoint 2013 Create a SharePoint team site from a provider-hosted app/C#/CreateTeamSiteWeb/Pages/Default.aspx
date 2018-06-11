<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CreateTeamSiteWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            Message: <asp:Label ID="lbMessage" runat="server"></asp:Label>
        </div>
        <div>
            Site Name:<asp:TextBox ID="txtSiteTitle" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnCreateSite" runat="server" OnClick="btnCreateSite_Click" Text="Create Site" />
        </div>
        <div>
            New Site Link:<asp:HyperLink ID="lnkNewSite" runat="server" ></asp:HyperLink>
        </div>
    </div>
    </form>
</body>
</html>
