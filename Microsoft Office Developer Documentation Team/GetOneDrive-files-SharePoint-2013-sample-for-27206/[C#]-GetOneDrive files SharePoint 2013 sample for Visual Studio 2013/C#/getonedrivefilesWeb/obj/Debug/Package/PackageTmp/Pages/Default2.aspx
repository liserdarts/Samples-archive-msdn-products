<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="getonedrivefiles.Pages.Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Get OneDrive Files</title>
    <script 
        src="http://ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js" 
        type="text/javascript">
    </script>
    <script
        src="http://ajax.aspnetcdn.com/ajax/knockout/knockout-2.2.1.js"
        type="text/javascript">
    </script>
    <script 
        type="text/javascript" 
        src="../Scripts/jquery-1.8.2.min.js">
    </script>      
    <script 
        type="text/javascript"
        src="../Scripts/ChromeLoader.js">
    </script>
</head>
<body>
    <div id="chrome_ctrl_placeholder"></div>    
    <div id="mainContent" runat="server">
        <form id="form1" runat="server">
            <div>
                <h1 class="ms-accentText">Your files</h1>              
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" CssClass="ms-tableCell ms-verticalAlignMiddle" AutoGenerateColumns="False" CellSpacing="3"></asp:GridView>
                <br /><br />       
            </div>    
            <div>
                <h1 class="ms-accentText">Notification area</h1>              
                <asp:GridView ID="GridView2" runat="server" AllowSorting="False" CssClass="ms-tableCell ms-verticalAlignMiddle" AutoGenerateColumns="False" CellSpacing="3"></asp:GridView>
                <br /><br />
            </div>
        </form>
    </div>
    <!--<div id="notificationArea" runat="server"></div> -->
</body>
</html>
