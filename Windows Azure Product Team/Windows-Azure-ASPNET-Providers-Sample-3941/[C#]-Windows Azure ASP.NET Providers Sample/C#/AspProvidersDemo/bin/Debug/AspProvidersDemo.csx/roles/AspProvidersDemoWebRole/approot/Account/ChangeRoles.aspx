<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ChangeRoles.aspx.cs" Inherits="AspProvidersDemoWebRole.Account.ChangeRoles" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Add and Remove Roles for Your Account
    </h2>
    <p>
        Select the roles to associate your account with.
    </p>
    <div class="accountInfo">
        <asp:ListBox ID="AvailableRoles" runat="server" SelectionMode="Multiple" 
            Height="284px" Width="320px" ></asp:ListBox>
        <p class="submitButton">
            <asp:Button ID="ChangeRolesButton" runat="server" Text="Save Changes" OnClick="ChangeRolesButton_Click" />
         <br />
         <br />
           <asp:Button ID="RevertRolesButton" runat="server" Text="Revert Changes" OnClick="RevertRolesButton_Click" />
        </p>
        <asp:Label ID="ErrorLabel" runat="server" CssClass="failureNotification"></asp:Label>
    </div>
</asp:Content>