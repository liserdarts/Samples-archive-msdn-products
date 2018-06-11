<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/knockout-2.1.0.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.lists.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.listitems.js"></script>
    <script type="text/javascript" src="../Scripts/wingtip.viewmodel.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    How Do I...?
</asp:Content>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="appStatusDiv" style="float:left;width:30%">
        <table>
            <caption>Provisioning Status</caption>
            <tr>
                <td>Create site columns 'Question' and 'Answer'...</td><td><span id="step1Status"></span></td>
            </tr>
            <tr>
                <td>Create content type 'HowDoI'...</td><td><span id="step2Status"></span></td>
            </tr>
            <tr>
                <td>Add 'Question' and 'Answer' columns to 'HowDoI' content type...</td><td><span id="step3Status"></span></td>
            </tr>
            <tr>
                <td>Create list named 'FAQ'...</td><td><span id="step4Status"></span></td>
            </tr>
            <tr>
                <td>Bind 'HowDoI' content type to 'FAQ' list...</td><td><span id="step5Status"></span></td>
            </tr>
            <tr>
                <td>Populate 'FAQ' list from 'Questions.xml' file...</td><td><span id="step6Status"></span></td>
            </tr>
        </table>
    </div>
    <div id="mainDiv" style="float:right;width:70%">
        <table id="listTable" style="width:400px;">
            <caption><a href="../Lists/FAQ/AllItems.aspx">Frequently-Asked Questions</a></caption>
            <thead>
                <tr>
                    <th>Question</th>
                    <th>Answer</th>
                </tr>
            </thead>
            <tbody data-bind="foreach: get_items()">
                <tr>
                    <td data-bind="text: get_question()"></td>
                    <td data-bind="text: get_answer()"></td>
                </tr>
            </tbody>
        </table>
</div>
</asp:Content>
