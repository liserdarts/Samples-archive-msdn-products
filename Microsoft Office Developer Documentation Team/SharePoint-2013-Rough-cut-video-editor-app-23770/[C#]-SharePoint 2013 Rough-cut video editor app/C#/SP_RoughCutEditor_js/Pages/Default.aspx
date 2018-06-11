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
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.js"></script>
    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link rel="stylesheet" href="../Content/jquery-ui.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
    'Rough-Cut' Video Editor
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <!-- Productions-->
    <div id="AllProductions" style="float:left;width:200px;">
        <div id="ErrorArea" class="errorClass"></div>
        <div id="AllProductionsHeader" class="listHeading">Productions</div>
        <div id="AddNewProduction" class="clicker" onclick="addNewProduction();">+ New Production</div>
        <div id="Productions"></div>
    </div> 
    
    <!-- Add New Production -->
    <div id="AddProduction" style="display: none; float: left; background-color: #0A0A0A; width: 800px; padding: 10px">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">New Production</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Production Name</div>
        <input type="text" id="newProductionName" style="width: 480px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelNewProduction();" style="margin-right: 20px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveNewProduction();" unselectable="on">Save</div>
    </div>

    <!-- Edit Production -->
    <div id="ProductionDetails" style="display: none; float: left; background-color: #0A0A0A; width: 800px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle" style="margin-bottom:5px">Edit Production</div>
        <div class="formLabel">Production Name</div>
        <input type="text" id="editProductionName" style="width: 480px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel" style="float:left;padding-left:0px">Video Bin</div>
        <div class="clear">&nbsp;</div>
        <div id="availableVideos" style="width:640px;float:left;height:100px;background-color:#FFFFFF;margin-left:50px;overflow-y:scroll;padding:5px;">Loading...</div>
        <div class="button" onclick="uploadFiles();" style="margin-right: 20px;" unselectable="on">Upload</div>
        <div class="formLabel" style="width:300px;float:left;margin-right:10px;">Production Timeline</div>
        <div class="clear">&nbsp;</div>
        <div id="wrapper">
            <div id="videosInProduction">Loading...</div>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="deleteEditProduction();" style="margin-right: 20px;" unselectable="on">Delete</div>
        <div class="button" onclick="cancelEditProduction();" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditProduction();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Production Player-->
    <div id="ProductionPlayer" style="display: none; float: left; background-color: #0A0A0A; width: 800px; padding: 10px;">
        
    </div>

    <!-- Edit Player-->
    <div id="EditPlayer" style="display: none; float: left; background-color: #0A0A0A; width: 800px; padding: 10px;">
        <div class="spinnerLabel" style="width:100px;">Edit Clip Time:</div>
        <div class="clear">&nbsp;</div>
        <div id="sliderStart" style="width:700px;margin-left:50px;">&nbsp;</div>
        <div class="clearSpace">&nbsp;</div>
        <div class="button" onclick="cancelEditCut();" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditCut();" unselectable="on">Save</div>
    </div>
</asp:Content>
