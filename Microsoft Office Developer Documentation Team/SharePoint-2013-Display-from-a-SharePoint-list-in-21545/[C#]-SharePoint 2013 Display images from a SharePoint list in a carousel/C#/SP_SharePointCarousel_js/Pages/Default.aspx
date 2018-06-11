<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.debug.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.debug.js"></script>
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    
    <!-- Custom CSS File for styles used in the carousel -->
    <link rel="Stylesheet" type="text/css" href="../Content/liquidcarousel.css" />
    
    <script type="text/javascript" src="../Scripts/App.js"></script>

    <!-- Custom, open source JavaScript file used to render an unordered list as a carousel -->
    <script type="text/javascript" src="../Scripts/jquery.liquidcarousel.pack.js"></script>	
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div>
        <div id="productCarousel" class="carousel" style="width:750px">
            <span class="previous"></span>
	        <div id="gallery" class="wrapper">

		       <!-- an unordered list (ul) of images will be added here in JavaScript --> 

	        </div>
            <span class="next"></span>
        </div>
    </div>

</asp:Content>
