<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.10.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/knockout-2.2.1.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/SP.UserProfiles.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.search.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.peoplemanager.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.viewmodel.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Due This Week
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div><a href="../Lists/Settings/AllItems.aspx">Settings</a></div>
    <div style="width:400px;" id="tasksDisplay" data-bind="template: { name: 'accordion-template', afterRender: animate, foreach: get_people() }"></div>

    <script type="text/html" id="accordion-template">
        <h3 style="margin:10px;padding:10px;background-color:#eee;cursor:pointer" data-bind="text: get_displayName()"></h3>
        <div style="margin:10px;padding:10px;">
            <p>
                <img src="/_layouts/15/images/PersonPlaceholder.42x42x32.png" data-bind="attr: { src: get_photoUrl() }" alt="photo" />
                <ul style="list-style: none" data-bind="foreach: get_tasks()">
                    <li>
                        <div style="font-weight: bold"><span data-bind="text: get_title()"></div>
                        <div style="margin: 5px"><span data-bind="text: get_description()"></div>
                    </li>
                </ul>
            </p>
        </div>
    </script>

</asp:Content>
