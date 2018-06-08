<%@ Page Title="My Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="MyProfile.aspx.cs" Inherits="AspProvidersDemoWebRole.MyProfile" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>
            Profile Settings for: <asp:LoginName ID="LoginName1" runat="server" />
        </h1>
        <table>
            <tr>
                <td>Country:</td>
                <td><asp:Label ID="Country" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Gender:</td>
                <td><asp:Label ID="Gender" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Age:</td>
                <td><asp:Label ID="Age" runat="server" Text="Label"></asp:Label></td>
            </tr>        
             <tr>
                <td valign="top">Roles:</td>
                <td><asp:ListBox ID="RoleList" runat="server" Width="320" /></td>
            </tr>       
        </table>
    </div>
</asp:Content>
