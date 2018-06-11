<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderHeaderVisualWebPartUserControl.ascx.cs" Inherits="DuetSalesOrderSolution.SalesOrderHeaderVisualWebPart.SalesOrderHeaderVisualWebPartUserControl" %>

<asp:Table ID="Table1" runat="server">
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel1" FieldName="SalesOrderNumber"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldSalesOrderNumber" FieldName="SalesOrderNumber" />
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel2" FieldName="PurchaseOrderNumber"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldPurchaseOrderNumber" FieldName="PurchaseOrderNumber" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel3" FieldName="SoldToParty"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldSoldToParty" FieldName="SoldToParty" />
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel4" FieldName="SalesOrganization"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldSalesOrganization" FieldName="SalesOrganization" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel5" FieldName="DeliveryDate"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldDeliveryDate" FieldName="DeliveryDate" />
            <%--Adding DateTime Control to be displayed in Edit mode.--%>
            <SharePoint:DateTimeControl ID="dateTimeDeliveryDate" runat="server" DateOnly="true"  />
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel6" FieldName="OrderType"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldOrderType" FieldName="OrderType" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel7" FieldName="Currency"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldCurrency" FieldName="Currency" />
            <%--Adding drop-down list box for currencies to be displayed in Edit mode.--%>
            <asp:DropDownList ID="dropDownCurrency" runat="server" />
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formlabel">
            <SharePoint:FieldLabel runat="server" ID="FieldLabel8" FieldName="NetValue"  DisplaySize="5"/>
        </asp:TableCell>
        <asp:TableCell CssClass="ms-formbody">
            <SharePoint:FormField runat="server" ID="ffldNetValue" FieldName="NetValue" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>


<script language="ecmascript" type="text/ecmascript">
    
    function PreSaveAction() {
        // Validate that delivery date is in future.
        return checkDeliveryDate("<%=dateTimeDeliveryDate.ClientID%>");
    }

    function checkDeliveryDate(identifier) {

        var deliveryDateElement = getElementByTagAndId('INPUT', identifier);

        if (deliveryDateElement.value == null || deliveryDateElement.value == "") {
            alert('Delivery date cannot be left blank.');
            return false;
        }
        else {
            var now = new Date();
            var enteredDate = Date.parse(deliveryDateElement.value);
            if (enteredDate < now) {
                alert('The specified Delivery Date must be in the future.');
                return false;
            }
        }
        return true;
    }

    function getElementByTagAndId(tagName, identifier) {
        var inputTagElements = document.getElementsByTagName(tagName);

        var requiredElement;
        for (var i = 0; i < inputTagElements.length; i++) {
            if (inputTagElements[i].id.indexOf(identifier) != -1) {
                requiredElement = inputTagElements[i];
                break;
            }
        }
        return requiredElement;
    }

</script>

