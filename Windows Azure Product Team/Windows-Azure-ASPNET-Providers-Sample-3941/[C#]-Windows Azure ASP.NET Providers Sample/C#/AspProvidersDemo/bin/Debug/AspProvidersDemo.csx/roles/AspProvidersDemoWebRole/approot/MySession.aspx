<%@ Page Title="My Session" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="MySession.aspx.cs" Inherits="AspProvidersDemoWebRole.MySession" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Session State for: <asp:LoginName ID="LoginName1" runat="server" />
    </h1>
    <div>
        <asp:Label ID="LabelAddNew" runat="server" Text="Add new Session Item"></asp:Label>
        <br />
        <asp:Label ID="LabelNewName" runat="server" Text="Name:"></asp:Label>
        <asp:TextBox ID="TextBoxNewName" runat="server"></asp:TextBox>
        <asp:Label ID="LabelNewValue" runat="server" Text="Value:"></asp:Label>
        <asp:TextBox ID="TextBoxNewValue" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonNewSessionItem" runat="server" Text="Add" OnClick="ButtonNewSessionItem_Click" />
        <br />
        <table>
             <tr>
                <td valign="top">Session:</td>
                <td><asp:ListBox ID="SessionList" Width="320px" runat="server" /></td>
            </tr>       
        </table>    
    </div>
</asp:Content>
