<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeDataSource.aspx.cs" Inherits="ChangeDataSourceButton.Layouts.ChangeDataSource.ChangeDataSource" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server"><br />
    <asp:Label ID="lblSelectDataSource" runat="server" Text="Choose the new data source to use for the selected reports:" Height="0"></asp:Label><br />
    <asp:DropDownList ID="ddlDataSources" runat="server" Width="450"></asp:DropDownList><br /><br />
    <asp:Label ID="lblReportsToChange" runat="server" Text="The following reports will be changed to use the selected data source:"></asp:Label><br />
    <asp:TextBox ID="txtBoxReportsToChange" runat="server" TextMode="MultiLine" ReadOnly="True" Rows="9" Width="450" Wrap="False"></asp:TextBox><br /><br />
    <asp:Label ID="lblNotAnalyticReports" runat="server" Visible="false" Text="The following items are not analytic reports and will not be changed:"></asp:Label><br />
    <asp:TextBox ID="txtBoxNotAnalyticReports" runat="server" TextMode="MultiLine"  Visible="false" ReadOnly="True" Rows="5" Width="450" Wrap="False"></asp:TextBox><br /><br /><br />
    <asp:Button ID="btnChangeDataSource" runat="server" Text="OK" OnClick="btnChangeDataSource_Click" /><br />
    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Change Analytic Report Data Source
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Change Analytic Report Data Source
</asp:Content>
