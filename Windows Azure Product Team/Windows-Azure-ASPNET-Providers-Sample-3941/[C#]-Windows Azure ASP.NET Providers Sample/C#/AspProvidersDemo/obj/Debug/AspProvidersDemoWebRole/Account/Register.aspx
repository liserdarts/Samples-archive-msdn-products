<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="AspProvidersDemoWebRole.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard 
        ID="RegisterUser" 
        runat="server"
        EnableViewState="False"  
        OnCreatedUser="RegisterUser_CreatedUser"
        ContinueDestinationPageUrl="~/Default.aspx" >
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server" >
                <ContentTemplate>
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="UserNameRequired" 
                                    runat="server" 
                                    ControlToValidate="UserName" 
                                    CssClass="failureNotification" 
                                    ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="EmailRequired" 
                                    runat="server" 
                                    ControlToValidate="Email" 
                                    CssClass="failureNotification" 
                                    ErrorMessage="E-mail is required." 
                                    ToolTip="E-mail is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="PasswordRequired" 
                                    runat="server" 
                                    ControlToValidate="Password" 
                                    CssClass="failureNotification" 
                                    ErrorMessage="Password is required." 
                                    ToolTip="Password is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ControlToValidate="ConfirmPassword" 
                                    CssClass="failureNotification" 
                                    Display="Dynamic" 
                                    ErrorMessage="Confirm Password is required." 
                                    ID="ConfirmPasswordRequired" 
                                    runat="server" 
                                    ToolTip="Confirm Password is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator 
                                    ID="PasswordCompare" 
                                    runat="server" 
                                    ControlToCompare="Password" 
                                    ControlToValidate="ConfirmPassword" 
                                    CssClass="failureNotification" 
                                    Display="Dynamic" 
                                    ErrorMessage="The Password and Confirmation Password must match."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                        </fieldset>
                        <fieldset class="register">
                            <legend>If You Forget Your Password</legend>
                            <p>
                                <asp:Label ID="SecurityQuestionLabel" runat="server" AssociatedControlID="Question">Security question:</asp:Label>
                                <asp:DropDownList ID="Question" runat="server" CssClass="textEntry">
                                    <asp:ListItem Text="[Select a Question]" />
                                    <asp:ListItem Text="Favorite Pet" />
                                    <asp:ListItem Text="Mother's Maiden Name"  />
                                    <asp:ListItem Text="Who was your childhood hero?"  />
                                    <asp:ListItem Text="Your favorite pastime?"  />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                    ID="SecurityQuestionRequired" 
                                    runat="server" 
                                    InitialValue="[Select a Question]" 
                                    ControlToValidate="Question"
                                    CssClass="failureNotification" 
                                    Display="Dynamic" 
                                    ErrorMessage="A security question is required." 
                                    ToolTip="A security question is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Your answer:</asp:Label>
                                <asp:TextBox ID="Answer" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="AnswerRequired" 
                                    runat="server" 
                                    ControlToValidate="Answer"
                                    CssClass="failureNotification" 
                                    Display="Dynamic" 
                                    ErrorMessage="A security question answer is required." 
                                    ToolTip="A security question answer is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                        </fieldset>
                        <fieldset class="register">
                            <legend>Help Us Customize Your Experience</legend>
                            <p>
                                <asp:Label ID="CountryLabel" runat="server" AssociatedControlID="Country" >Country:</asp:Label>
                                <asp:DropDownList ID="Country" runat="server" CssClass="textEntry">
                                    <asp:ListItem Text="[Select a Country]" />
                                    <asp:ListItem Text="Austria"  />
                                    <asp:ListItem Text="Australia"  />
                                    <asp:ListItem Text="China"  />
                                    <asp:ListItem Text="France"  />
                                    <asp:ListItem Text="Germany"  />
                                    <asp:ListItem Text="India"  />
                                    <asp:ListItem Text="Italy"  />
                                    <asp:ListItem Text="Russia"  />
                                    <asp:ListItem Text="Spain"  />
                                    <asp:ListItem Text="Switzerland"  />
                                    <asp:ListItem Text="UK"  />
                                    <asp:ListItem Text="USA" />
                                    <asp:ListItem Text="Other" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator
                                    ID="CountryRequired"
                                    runat="server"
                                    ControlToValidate="Country"
                                    Display="Dynamic" 
                                    InitialValue="[Select a Country]"
                                    CssClass="failureNotification" 
                                    ErrorMessage="A country selection is required." 
                                    ToolTip="A country selection is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="GenderLabel" runat="server" AssociatedControlID="Gender" >Gender:</asp:Label>
                                <asp:DropDownList ID="Gender" runat="server" CssClass="textEntry">
                                    <asp:ListItem Text="[Select Gender]" />
                                    <asp:ListItem Text="Male" />
                                    <asp:ListItem Text="Female"  />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                    ID="GenderRequired"
                                    runat="server"
                                    InitialValue="[Select Gender]"
                                    ControlToValidate="Gender"
                                    Display="Dynamic" 
                                    CssClass="failureNotification" 
                                    ErrorMessage="A gender selection is required." 
                                    ToolTip="A gender selection is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="AgeLabel" runat="server" AssociatedControlID="Age">Age:</asp:Label>
                                <asp:TextBox ID="Age" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                    ID="AgeRequired" 
                                    runat="server" 
                                    ControlToValidate="Age"
                                    Display="Dynamic" 
                                    CssClass="failureNotification" 
                                    ErrorMessage="An age is required." 
                                    ToolTip="An age is required." 
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator 
                                    ID="AgeRangeValidator" 
                                    runat="server" 
                                    ErrorMessage="Age must be between 0 and 120."
                                    Type="Integer" 
                                    MinimumValue="0" 
                                    MaximumValue="120" 
                                    CssClass="failureNotification" 
                                    Display="Dynamic" 
                                    ControlToValidate="Age"
                                    ValidationGroup="RegisterUserValidationGroup"></asp:RangeValidator>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup"/>
                        </p>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
