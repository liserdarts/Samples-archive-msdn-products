<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs" Inherits="CustomerWebPart.VisualWebPart1.VisualWebPart1UserControl" %>

<style type="text/css">
  .style3
  {
    height: 81px;
  }
  .style4
  {
    width: 119px;
  }
  .style5
  {
    width: 559px;
  }
  .style6
  {
    height: 81px;
    width: 119px;
  }
  .style7
  {
   color : #FF3300;
  }
</style>

<table>
  <tr>
    <td colspan="2">
      To work with the AdventureWorks Customer External Content Type
      (ECT) enter values for the ECT name and namespace, then do the
      following:
        
      <ul style="padding-left: 0px; margin-left: 14px">
        <li>To create a new customer leave the customer ID blank,
            enter values in the other fields, and select
            <strong>Create New</strong>.
        </li>
        <li>To find a specific customer enter the customer&#39;s ID
            and select <strong>Find by ID</strong>.
        </li>
        <li>To update a customer enter the customer&#39;s ID and
            updated values for the other fields and select
            <strong>Update</strong>.
        </li>   
      </ul>
            
      <asp:Label ID="StatusLabel" runat="server"
       ForeColor="#FF3300"></asp:Label>
            
      <br />
      <br />
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      ECT Name<span class="style7">*</span></td>
    <td class="style5">
      <asp:TextBox ID="ECTName" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      ECT Namespace<span class="style7">*</span></td>
    <td class="style5">
      <asp:TextBox ID="ECTNamespace" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">&nbsp;</td>
    <td class="style5">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" class="style4">
      Customer ID</td>
    <td class="style5">
      <asp:TextBox ID="CustomerID" runat="server"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      Title</td>
    <td class="style5">
      <asp:TextBox ID="Title" runat="server"></asp:TextBox>
     </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      First Name</td>
    <td class="style5">
      <asp:TextBox ID="FirstName" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      Middle Name</td>
    <td class="style5">
      <asp:TextBox ID="MiddleName" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      Last Name</td>
    <td class="style5">
      <asp:TextBox ID="LastName" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      Email</td>
    <td class="style5">
      <asp:TextBox ID="Email" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="left" class="style4">
      Phone</td>
    <td class="style5">
      <asp:TextBox ID="Phone" runat="server"
       Width="100%"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="center" class="style6">
      &nbsp;
    </td>
    <td align="center" class="style3">
      <asp:Button ID="CreateNewCustomer" runat="server" 
        OnClick="CreateNewCustomer_Click" Text="Create New"
        Width="120px" />
        &nbsp;
      <asp:Button ID="FindCustomerByID" runat="server" 
        OnClick="FindCustomerByID_Click" Text="Find by ID"
        Width="120px" />
        &nbsp;
      <asp:Button ID="UpdateCustomer" runat="server"
        OnClick="UpdateCustomer_Click" Text="Update"
        Width="120px" />
        &nbsp;
      <asp:Button ID="ClearAllFields" runat="server"
        onclick="ClearAllFields_Click" 
        Text="Clear All Fields" Width="120px" />
    </td>
  </tr>
</table>