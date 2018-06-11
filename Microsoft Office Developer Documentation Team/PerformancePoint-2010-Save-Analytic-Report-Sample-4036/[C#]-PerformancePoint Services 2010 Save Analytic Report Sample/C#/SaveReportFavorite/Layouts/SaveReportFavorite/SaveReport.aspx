<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaveReport.aspx.cs" Inherits="SaveReportFavoriteButton.Layouts.SaveReportFavoriteButton.SaveReport" DynamicMasterPageFile="~masterurl/default.master" enableviewstate="true"%>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<input type="hidden" name="_VIEWSTATE" />
<p>Select the report that you want to save:</p>
<asp:DropDownList ID="ddlReportsOnPage" runat="server" ToolTip="Select the report to save." AutoPostBack="true" OnSelectedIndexChanged="ddlReportsOnPage_SelectedIndexChanged" >
    </asp:DropDownList> 
    <br /><br /><br />
    <p>Enter a new name, description, or folder for the new report:   (optional)</p>
    <asp:Label ID="lblName"
        runat="server" Text="Name: "/><asp:TextBox ID="txtBoxName" runat="server" ></asp:TextBox>
        <br />
    <asp:Label ID="lblDescription"
        runat="server" Text="Description: "/><asp:TextBox ID="txtBoxDescription" runat="server" TextMode="MultiLine" ></asp:TextBox>
        <br />
    <asp:Label ID="lblFolder"
        runat="server" Text="Folder: "/><asp:TextBox ID="txtBoxFolder" runat="server" ></asp:TextBox>
        <br /><br /><br />
<p>Select the PerformancePoint Content List to save the report to:</p>
<asp:DropDownList ID="ddlPerfPointLists" runat="server" ToolTip="Select the list to save to." >
    </asp:DropDownList>
    <br /><br /><br />
<asp:Button ID="btnSaveReport" runat="server" Text="Save Report" onclick="btnSaveReport_Click"/>
    <br /><br /><asp:Label ID="lblMessage" runat="server" Text="Label" Visible="false"></asp:Label>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Save Analytic Report
</asp:Content>


<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Save Analytic Report
</asp:Content>
