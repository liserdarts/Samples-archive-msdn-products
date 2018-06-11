<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="../Scripts/knockout-2.2.1.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.customers.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Simple OData Client
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<div>
        <table>
        <caption>Customers</caption>
        <thead>
            <tr>
                <th><span style="margin-right: 15px;">ID</span></th>
                <th><span style="margin-right: 15px;">Last Name</span></th>
                <th><span style="margin-right: 15px;">First Name</span></th>
                <th><span style="margin-right: 15px;">E-Mail</span></th>
            </tr>
        </thead>
        <tbody id="resultsTable" data-bind="foreach: get_customers()">
            <tr>
                <td nowrap="nowrap" data-bind="text: get_id()"></td>
                <td nowrap="nowrap" data-bind="text: get_lastName()"></td>
                <td nowrap="nowrap" data-bind="text: get_firstName()"></td>
                <td nowrap="nowrap" data-bind="text: get_emailAddress()"></td>
            </tr>
        </tbody>
    </table>
</div>
</asp:Content>
