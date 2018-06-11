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
    <style type="text/css">
        #videosInPlaylist, #availableVideos { list-style-type: none; margin: 0; padding: 0 0 2.5em; float: left; margin-right: 10px; }
        #videosInPlaylist div, #availableVideos div { margin:2px; padding: 5px; font-size: 1.2em; cursor:move; }
    </style>
    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link rel="stylesheet" href="../Content/jquery-ui.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Drag'n'Drop Video Playlist
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <!-- Playlists-->
    <div id="AllPlaylists" style="float:left;width:200px;">
        <div id="ErrorArea" class="errorClass"></div>
        <div id="AllPlaylistsHeader" class="listHeading">Playlists</div>
        <div id="AddNewPlaylist" class="clicker" onclick="addNewPlaylist();">+ New Playlist</div>
        <div id="Playlists"></div>
    </div> 
    
    <!-- Add New Playlist -->
    <div id="AddPlaylist" style="display: none; float: left; background-color: #CECECE; width: 800px; padding: 10px">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">New Playlist</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Playlist Name</div>
        <input type="text" id="newPlaylistName" style="width: 480px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelNewPlaylist();" style="margin-right: 20px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveNewPlaylist();" unselectable="on">Save</div>
    </div>

    <!-- Edit Playlist -->
    <div id="PlaylistDetails" style="display: none; float: left; background-color: #CECECE; width: 800px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle" style="margin-bottom:5px">Edit Playlist</div>
        <div class="formLabel">Playlist Name</div>
        <input type="text" id="editPlaylistName" style="width: 480px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel" style="width:300px;float:left;margin-right:10px;">Videos in playlist</div>
        <div class="formLabel" style="width:300px;float:left;padding-left:30px">Available videos</div>
        <div class="clear">&nbsp;</div>
        <div id="videosInPlaylist" class="connectedSortable" style="width:330px;float:left;min-height:300px;background-color:#FFFFFF;margin-right: 10px;margin-left:50px;">Loading...</div>
        <div id="availableVideos" class="connectedSortable" style="width:330px;float:left;min-height:300px;background-color:#FFFFFF;margin-left:50px;">Loading...</div>
        <div style="cursor:pointer;float:right;margin-right:25px;color:blue" onclick="uploadFiles()">Upload Files</div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="deleteEditPlaylist();" style="margin-right: 20px;" unselectable="on">Delete</div>
        <div class="button" onclick="cancelEditPlaylist();" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditPlaylist();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Playlist Player-->
    <div id="PlaylistPlayer" style="display: none; float: left; background-color: #CECECE; width: 800px; padding: 10px;">
        <video id="Player" width="800" height="600" controls="controls" autoplay="false">
            Sorry --- your browser does not support HTML5 Video.
        </video>
    </div>

</asp:Content>
