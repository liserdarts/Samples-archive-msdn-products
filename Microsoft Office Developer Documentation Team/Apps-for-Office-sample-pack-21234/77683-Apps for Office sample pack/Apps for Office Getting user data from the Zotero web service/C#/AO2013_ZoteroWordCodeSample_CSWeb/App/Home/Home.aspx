<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AO2013_ZoteroWordCodeSample_CSWeb.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title>Zotero Citation Assistant</title>
    <script src="../../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.cookie.js"></script>

    <link href="../../Content/Office.css" rel="stylesheet" type="text/css" />
    <script src="https://appsforoffice.microsoft.com/lib/1.0/hosted/office.js" type="text/javascript"></script>

    <!-- To enable offline debugging using a local reference to Office.js, use:                        -->
    <!--    <script src="../../Scripts/Office/MicrosoftAjax.js" type="text/javascript"></script>       -->
    <!--    <script src="../../Scripts/Office/1.0/office.js" type="text/javascript"></script>          -->
    <asp:Literal ID="authenticationScript" runat="server" />

    <link href="../App.css" rel="stylesheet" type="text/css" />
    <script src="../App.js" type="text/javascript"></script>

    <link href="Home.css" rel="stylesheet" type="text/css" />
    <script src="Home.js" type="text/javascript"></script>
</head>
<body>
    <form id="mainForm" runat="server">
        <div id="WrapperDiv" class="Wrapper">
            <header class="mainHeader">
                <span class="verticalCenter">Logged in as: <span id="userNameSpan"></span></span>
                <a id="logoutLink" href="#">Logout</a>
            </header>
            <div id="MainContentArea">
                <div id="SelectionArea">
                    <header id="BooksHeader" class="horizontalCenterText">Books</header>
                    <div class="relative">
                        <label id="styleLabel" for="StyleDropDown">
                            Citation Style:
                        </label>
                        <select id="StyleDropDown">
                        <option value="">CMS</option>
                        <option value="apa">APA</option>
                        <option value="mla">MLA</option>
                    </select>
                    <button id="RefreshButton" type="button"></button>
                    </div>
                </div>
                <div id="BookListDiv">
                    <img id="LoadingGif" src="../../Images/zoteroLoader.gif" />
                    <ul id="BooksList">
                        
                    </ul>
                </div>
                <button type="button" id="AddBooksButton">Add Selected</button>
            </div>
            <div id="PushDiv" class="Push"></div>
        </div>
        <footer>
            <img id="ZoteroLogo" src="../../Images/ZoteroLogo.png" />
        </footer>
    </form>
</body>
</html>
