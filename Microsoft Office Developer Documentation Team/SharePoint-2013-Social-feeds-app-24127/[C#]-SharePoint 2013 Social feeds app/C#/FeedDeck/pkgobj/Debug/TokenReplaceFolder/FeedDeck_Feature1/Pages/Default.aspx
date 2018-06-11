<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.UserProfiles.js"></script>
    <script type="text/javascript" src="../Scripts/knockout-2.2.1.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.social.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.viewmodel.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Feed Deck
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div><input id="postMessage" type="text" style="width:400px;" /><button id="postButton">Post a Message</button></div>
    <div id="displayDiv" style="padding:25px;">
        <div class="column">
            <h1>Timeline</h1>
            <div class="columnData" data-bind="foreach: get_posts()">
                <div>
                    <div style="padding-top:10px;">
                        <div style="display:inline-block;width:50px"><img src="/_layouts/15/images/PersonPlaceholder.42x42x32.png" data-bind="attr: { src: get_picture() }" alt="photo" /></div>
                        <div style="display:inline-block"><h3 class="authorContainer" data-bind="text: get_poster()"></h3></div>
                    </div>
                    <div class="postContainer">
                        <p data-bind="text: get_date()"></p>
                        <p class="postBody" data-bind="text: get_body()"></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="column">
            <h1>Interactions</h1>
            <div class="columnData" data-bind="foreach: get_mentions()">
                <div>
                    <div style="padding-top:10px;">
                        <div style="display:inline-block"><img src="/_layouts/15/images/PersonPlaceholder.42x42x32.png" data-bind="attr: { src: get_picture() }" alt="photo" /></div>
                        <div style="display:inline-block"><h3 class="authorContainer" data-bind="text: get_poster()"></h3></div>
                    </div>
                    <div class="postContainer">
                        <p data-bind="text: get_date()"></p>
                        <p class="postBody" data-bind="text: get_body()"></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="column">
            <h1>People</h1>
            <div class="columnData" data-bind="foreach: get_followed()">
                <div>
                    <div style="padding-top:10px;border-bottom:1px solid #808080">
                        <div style="display:inline-block"><img src="/_layouts/15/images/PersonPlaceholder.42x42x32.png" data-bind="attr: { src: get_picture() }" alt="photo" /></div>
                        <div style="display:inline-block"><h3 style="margin:10px;padding:10px;" data-bind="text: get_name()"></h3></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
