<%@ Page Title="Manage Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ManageRoles.aspx.cs" Inherits="AspProvidersDemoWebRole.ManageRoles" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Manage Roles
    </h2>
    <div>
        <asp:ListBox ID="AvailableRoles" runat="server" SelectionMode="Multiple" 
            Height="284px" Width="320px"></asp:ListBox>
    </div>
    <div>
        <asp:Label ID="LabelNewRole" runat="server" Text="Add new role:"></asp:Label>
        <asp:TextBox ID="TextBoxNewRole" runat="server"></asp:TextBox>
        <asp:Button ID="ButtonNewRole" runat="server" Text="Add New Role" OnClick="ButtonNewRole_Click" />
        <br />
        <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
    </div>
</asp:Content>
