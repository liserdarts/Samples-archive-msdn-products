<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AspProvidersDemoWebRole._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <p>
            This is a simple sample that shows how to make use of the ASP.NET providers for 
            Windows Azure. It is based on a set of standard ASP.NET controls 
            that access the providers in the background. &nbsp;
        </p>
        <p>
            To use this sample first login to the system, or create a new user and then login with the newly created credentials. 
            Your login status is shown at the 
            top of this site.
        </p>
        <p>
            There are several pages on this site:
        </p>
        <p>
            <a href="Account\Login.aspx">Login.aspx</a> -- this page uses the asp:login control to
            allow users to login to the system</p>
        <p>
            <a href="Account\Register.aspx">Register.aspx</a> -- this page allows a user
            to create a new account and set some profile settings</p>
        <p>
            <a href="Account\ChangeRoles.aspx">ChangeRoles.aspx</a> -- this page allows a user
            to join some roles</p>
        <p>
            <a href="MyProfile.aspx">MyProfile.aspx</a> -- this page allows a logged in user to
            view their profile settings</p>
        <p>
            <a href="Account\ChangePassword.aspx">ChangePassword.aspx</a> -- this page allows a user
            to change their password</p>
        <p>
            <a href="ManageRoles.aspx">ManageRoles.aspx</a> -- this page creates new roles
            </p>
        <p>
            <a href="MySession.aspx">MySession.aspx</a> -- this page allows users to view and add data to their session state
            </p>
</asp:Content>
