<%@ Page Title="Import" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="AddressBookWebRole.Import" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" 
    runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Upload a comma-delimited .csv file containing your contacts. See table below for 
        file guidelines.</p>
    <br />
    <br />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                <asp:FileUpload ID="Uploader" runat="server" style="margin-top: 0px" Height="24px" Width="472px" />&nbsp;
            </td>
            <td valign="top">
                <asp:Button ID="cmdUpload" runat="server" Text="Import" 
                    onclick="cmdUpload_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lblInfo" runat="server" EnableViewState="False" 
    Font-Bold="True"></asp:Label>
    <br />
    <br />
    <strong>File Guidelines:</strong>
    <br />
    <ul>
        <li>File must include column headers.</li>
        <li>Column headers should be named according to the list below. Note that only columns matching these names will be imported into the Table service.</li>
    </ul>
    <table style="width: 44%;" border="0" cellpadding="2">
        <tr>
            <td>
                <strong>Column Name</strong></td>
            <td>
                <strong>Required/Recommended</strong></td>
        </tr>
        <tr>
            <td>
                FirstName</td>
            <td>
                Required</td>
        </tr>
        <tr>
            <td>
                LastName</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                Email</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                CellPhone</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                HomePhone</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                StreetAddress</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                City</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                State</td>
            <td>
                Recommended</td>
        </tr>
        <tr>
            <td>
                ZipCode</td>
            <td>
                Recommended</td>
        </tr>
    </table>
    <br />
</asp:Content>
