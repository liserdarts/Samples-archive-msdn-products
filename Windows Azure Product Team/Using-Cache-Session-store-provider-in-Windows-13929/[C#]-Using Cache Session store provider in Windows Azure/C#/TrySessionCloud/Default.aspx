<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body bgcolor="#ccff99">
    <form id="form1" runat="server">
    <div>
    
        <strong>Sample SessionState App</strong></div>
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Add To Session Object" />        
    </p>
    <strong>Items in the cart</strong><asp:Table ID="Table1" runat="server">
    </asp:Table>
    <asp:Label ID="Label1" runat="server" 
        Text="** When you add items to session object, the current contents of this object are shown in the table above  " 
        style="text-decoration: underline; font-style: italic"></asp:Label>
    </form>
</body>
</html>
