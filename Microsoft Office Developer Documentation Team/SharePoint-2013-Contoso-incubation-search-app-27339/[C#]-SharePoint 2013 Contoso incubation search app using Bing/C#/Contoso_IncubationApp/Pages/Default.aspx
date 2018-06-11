<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.RequestExecutor.js"></script>
    <meta name="WebPartPageExpansion" content="full" />

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- 
    Developed by:    Martin Harwar, www.Point8020.com
    Developed for:   MSDN and SharePoint Product group
    First released:  14th February, 2014   
--%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

<%-- 
    The following <div> is shown the first time the app is installed. The code in App.js will ensure the Ideas and Tags lists exist, and
    will then render a link to the Ideas list here
--%>
<div id="configPanel" style="margin-left:50px;display:none;">
<div class="clearfix">&nbsp;</div>
<div>
    <h4>The Contoso Incubation app helps you manage research for items in an 'Ideas' list.</h4>
</div>
<div class="row" style="padding-left:10px;">
    <h4 id="listsCheck"></h4>
</div>
</div>

<%-- 
    The following <div> defines the main UI for the Search panel
--%>
<div id="searchPanel" style="margin-left:50px;display:none;float:left;width:100%;" onkeypress="enterKeyPressed();">
    <div class="clearfix">&nbsp;</div>

    <%-- 
    The following <div> defines the Search UI
    --%>
    <div id="searchOptionsPanel">
    <div class="row" style="width:500px;margin-right:10px;padding-top:0px;padding-bottom:10px;">
        <div class="formBorder">
            <h3 id="sourceItemTitle"></h3>
            <h5 id="sourceItemDescription"></h5>
            <div class="clearfix">&nbsp;</div>
            <div class="point8020CheckBox">
  		        <input type="checkbox" value="1" checked="checked" id="useTitle" name="" />
	  	        <label for="useTitle"></label>
                <label for="useTitle" class="checkboxLabel" style="background:none;width:250px;height:37px;margin-top:-8px;margin-left:-10px;padding-top:5px;">Include Title </label>
  	        </div>
            <div class="clearfix" style="height:2px;">&nbsp;</div>
            <div class="point8020CheckBox">
  		        <input type="checkbox" value="1" id="useDescription" name="" />
	  	        <label for="useDescription"></label>
                <label for="useDescription" class="checkboxLabel" style="background:none;width:250px;height:37px;margin-top:-8px;margin-left:-10px;padding-top:5px;">Include Description</label>
  	        </div>
            <div class="clearfix" style="height:2px;">&nbsp;</div>
            <h5>Additional Terms:</h5>
            <input type="text" id="additionalTerms" style="width:450px;margin-left:10px;" class="textBox"/>
        </div>
        <div class="formBorder">
            <h4>Content Sources:</h4>
            <div class="clearfix" style="height:2px;">&nbsp;</div>
            <div class="point8020CheckBox">
  		        <input type="checkbox" value="1" checked="checked" id="searchSharePoint" name="" />
                <label for="searchSharePoint"></label>
                <label for="searchSharePoint" class="checkboxLabel" style="background:none;width:250px;height:37px;margin-top:-8px;margin-left:-10px;padding-top:5px;">Intranet (SharePoint)</label>
            </div>
            <div class="clearfix" style="height:2px;">&nbsp;</div>
            <div class="clearfix" style="height:2px;">&nbsp;</div>
            <div class="point8020CheckBox">
  		        <input type="checkbox" value="1" checked="checked" id="searchBing" name="" />
	  	        <label for="searchBing"></label>
                <label for="searchBing" class="checkboxLabel" style="background:none;width:250px;height:37px;margin-top:-8px;margin-left:-10px;padding-top:5px;">Internet (Bing)</label>
  	        </div>
            <div class="clearfix" style="height:2px;">&nbsp;</div>
        </div>
        <div class="ActionLinkButton" onclick="showTagsPanel()" style="float:left;margin-left:0">View Tagged Results</div>
        <div class="ActionLinkButton" onclick="searchNow();">Search Now</div>

    </div>
    </div>
    <div class="clearfix">&nbsp;</div>
    
    <%-- 
    The following <div> defines the Results UI
    --%>
    <div id="searchResultsPanel" style="display:none;">
        <div class="clearfix">&nbsp;</div>
        <div class="ActionLinkButton" onclick="refineSearch();" style="float:left;">Refine</div>
        <div class="ActionLinkButton" onclick="tagResults();" style="float:left;">Tag Selected Results</div>
        <div class="clearfix">&nbsp;</div><br/>
        <div id="sharePointResultPanel" style="display:none;width:40%;min-width:500px;float:left;border:none;margin-left:5px;padding:10px;">
            <h2>SharePoint Results</h2>
            <div id="sharPointSearchResults"></div>
        </div>
        <div id="bingResultPanel" style="display:none;width:40%;min-width:500px;float:left;margin-left:5px;border:none;padding:10px;">
            <h2>Bing Results</h2>
            <div id="bingSearchResults"></div>
        </div>
    </div>

</div>

<%-- 
    The following <div> defines the UI for the existing tags in a panel
--%>
<div id="tagsPanel" style="margin-left:50px;display:none;float:left;width:100%;"">
    <div class="clearfix">&nbsp;</div>
        <div class="row" style="width:500px;margin-right:10px;padding-top:0px;padding-bottom:10px;">
            <h3 id="taggedItemTitle"></h3>
            <h5 id="taggedItemDescription"></h5>
        <h2>Tagged Results</h2>
            <div id="taggedResults"></div>
                 <div class="clearfix">&nbsp;</div>
    <div class="ActionLinkButton" onclick="refineSearch();" style="float:left;">Find More</div>
        <div class="clearfix">&nbsp;</div>
    </div>

</div>

<div id="overlaydiv" class="overlay" style="display:none;"></div>
<div id="errorUI"  style="display:none; ">
    <h3 id ="errorMessage" style="color:#ffffff;"></h3>
    <div id="errorOK" onclick="hideError();" class="ActionLinkButtonInView">OK</div> 
</div>
<div id="confirmOverlay" class="overlay" style="display:none;"></div>
<div id="deleteUI"  style="display:none; ">
    <h3 id ="deleteMessage" style="color:#ffffff;"></h3>
    <input type="hidden" id="tagToDelete" />
    <input type="hidden" id="tagUrl" />
    <div id="DeleteYes" onclick="yesDeleteTag();" class="ActionLinkButtonInView">Yes</div>
    <div id="DeleteNo" onclick="cancelDeleteTag();" class="ActionLinkButtonInView">No</div> 
</div>

</asp:Content>
