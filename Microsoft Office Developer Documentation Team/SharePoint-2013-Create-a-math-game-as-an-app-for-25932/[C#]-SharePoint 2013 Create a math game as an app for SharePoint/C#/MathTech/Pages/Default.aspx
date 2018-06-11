<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".ms-siteicon-a img").attr('src', '../Images/MathTech.png');
        });

    </script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Math+Technology=MathTech!
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <h1>Who's better at Math, SharePoint or You.</h1>
  
 <div class="home">
<table class="tableHome">
<tr>
<td align="center" valign="middle" class="mode"><a href="../Pages/Instructions.aspx">
    <img src="../images/Instructions/instructions.png" width="200" height="130" border="0" / alt=""></a><br /> Instructions<br /></td>
<td align="center" valign="middle" class="mode"><a href="../Pages/Equation.aspx"><img src="../images/equation.png" width="200" height="130" border="0" /></a><br /> Equation<br />Mode</td>
<td align="center" valign="middle" class="mode"><a href="../Pages/Answer.aspx"><img src="../images/answer.png" width="200" height="130" border="0" /></a><br /> Answer<br />Mode</td>
</tr>
<tr>
    <td colspan="3" align="right" valign="middle" class="mode"><a href="../Lists/Score" target="_blank" style="margin-right:35px;color:#ffffff;">View Scores</a></td>
</tr>
</table>
</div>    
    

</asp:Content>
