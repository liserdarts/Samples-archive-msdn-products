<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div>
        <p id="message">
            <!-- The following content will be replaced with the user name when you run the app - see App.js -->
            initializing...
        </p>
        <table style="width: 100%;">
            <tr>
                <th>&nbsp;</th>
                <th>Use ODATA API</th>
                <th>Use CSOM API</th>
            </tr>
            <tr>
                <td>App Web Url</td>
                <td id="appWebUrl">&nbsp;</td>
                <td id="appWebUrlCsom">&nbsp;</td>
            </tr>
            <tr>
                <td>App Instance Id</td>
                <td id="appInstanceId">&nbsp;</td>
                <td id="appInstanceIdCsom">&nbsp;</td>
            </tr>
            <tr>
                <td>Server Relative Url</td>
                <td id="serverRelativeUrl">&nbsp;</td>
                <td id="serverRelativeUrlCsom">&nbsp;</td>
            </tr>
            <tr>
                <td>Remote app Url</td>
                <td id="remoteAppUrl">&nbsp;</td>
                <td id="remoteAppUrlCsom">&nbsp;</td>
            </tr>
            <tr>
                <td>Invocation result</td>
                <td id="invocationResult">&nbsp;</td>
                <td id="invocationResultCsom">&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <h2>Click <a href="../lists/list1">here</a> to go to List1 or access it below.</h2>
        <br />
        <br />
        <WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="full" Title="loc:full">
            <WebPartPages:XsltListViewWebPart runat="server"
                ListUrl="Lists/List1"
                IsIncluded="True"
                Title="Add an item to a list to start a workflow. Refresh the page to see the item renamed."
                JsLink="clientTemplate.js"
                NoDefaultStyle="TRUE"
                PageType="PAGE_NORMALVIEW"
                Default="False"
                ViewContentTypeId="0x">
            </WebPartPages:XsltListViewWebPart>
        </WebPartPages:WebPartZone>
    </div>
</asp:Content>
