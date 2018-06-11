<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderItemVisualWebPartUserControl.ascx.cs" Inherits="DuetSalesOrderSolution.SalesOrderItemVisualWebPart.SalesOrderItemVisualWebPartUserControl" %>

<asp:Table ID="Table1" runat="server">
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel1" FieldName="ItemNumber"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldItemNumber" FieldName="ItemNumber" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel2" FieldName="Sales Order Header"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldSalesOrderSclKey" FieldName="Sales Order Header" />
            <asp:TextBox ID="computedSalesOrderHeader" runat="server" Visible="false" ReadOnly="true" CssClass="ms-long"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel3" FieldName="Product"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldProduct" FieldName="Product" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel5" FieldName="MaterialShortText"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldMaterialShortText" FieldName="MaterialShortText" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel6" FieldName="Quantity"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldQuantity" FieldName="Quantity" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel7" FieldName="SalesUnit"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldSalesUnit" FieldName="SalesUnit" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel8" FieldName="NetPrice"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldNetPrice" FieldName="NetPrice" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel9" FieldName="Currency"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldCurrency" FieldName="Currency" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel10" FieldName="NetValue"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldNetValue" FieldName="NetValue" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel11" FieldName="Plant"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldPlant" FieldName="Plant" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
