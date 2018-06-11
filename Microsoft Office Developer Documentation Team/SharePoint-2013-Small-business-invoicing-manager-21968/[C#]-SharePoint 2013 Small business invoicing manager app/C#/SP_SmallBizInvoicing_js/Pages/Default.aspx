<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>
<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>
<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>
<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <!-- Home View-->
    <div id="Home" style="display:none;width:100%;">
       <div class="tile tileCustomer" id="CustomersTile" onclick="showCustomers();" unselectable="on">
           <div class="tileHeadingText" unselectable="on">Customers</div>
       </div>
       <div class="tile tilePO" id="POsTile"  onclick="showPOs();" unselectable="on">
           <div class="tileHeadingText" unselectable="on">Purchase Orders</div>
       </div>
       <div class="tile tileInvoice" id="InvoicesTile" onclick="showInvoices();" unselectable="on">
           <div class="tileHeadingText" unselectable="on">Invoices</div>
       </div>
       <div class="tile tileReports" id="ReportsTile" onclick="showReports();" unselectable="on">
           <div class="tileHeadingText" unselectable="on">Reports</div>
       </div>
       <div id="errGeneral" class="errorClass"></div>
    </div>
    <div class="clear">&nbsp;</div>
    <div style="margin-left:10px;">
    <!-- Customer View -->
    <div id="AllCustomers" style="display:none;float:left;width:210px;">
        <div id="errAllCustomers" class="errorClass"></div>
        <div id="CustomerHeading" class="listHeading">Customers</div>
        <div id="AddNewCustomer" class="clicker" onclick="addNewCustomer();">+ New Customer</div>
        <div id="CustomerList"></div>
    </div>
    <!-- Add New Customer -->
    <div id="AddCustomer" style="display:none;float:left;background-color:#C6C6C6;width:630px;padding:10px;">
        <div class="formTitle">Add New Customer</div>
        <div class="formLabel">Customer *</div>
        <input type="text" id="newCustomer" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Address</div>
        <textarea id="newAddress" style="width:400px" rows="4" cols="75"></textarea>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Business Phone</div>
        <input type="text" id="newPhone" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="newEmail" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Web Site</div>
        <input type="text" id="newWeb" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelNewCustomer();" style="margin-right:15px;">Cancel</div>
        <div class="button" onclick="saveNewCustomer();">Save</div>
    </div>
    <!-- Edit Customer -->
    <div id="CustomerDetails" style="display:none;float:left;background-color:#C6C6C6;width:630px;padding:10px;">
        <div class="formTitle">Edit Customer</div>
        <div class="formLabel">Customer *</div>
        <input type="text" id="editCustomer" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Address</div>
        <textarea id="editAddress" style="width:400px" rows="4" cols="75"></textarea>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Business Phone</div>
        <input type="text" id="editPhone" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="editEmail" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Web Site</div>
        <input type="text" id="editWeb" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelEditCustomer();" style="margin-right:15px;">Cancel</div>
        <div class="button" onclick="deleteEditCustomer();">Delete</div>
	    <div class="button" onclick="saveEditCustomer();">Save</div>
        <div class="clear">&nbsp;</div>
        <div class="button" id="addPOToCustomer" style="float:right;width:245px;margin-right:15px;">Add New Purchase Order</div>
        <div class="clear">&nbsp;</div>
        <div class="button" id="viewPOsForCustomer" style="float:right;width:245px;margin-right:15px;">View Purchase Orders</div>
        <div class="clear">&nbsp;</div>
        <div class="button" id="viewInvoicesForCustomer" style="float:right;width:245px;margin-right:15px;">View Invoices</div>
    </div>
    <!-- PO View-->
    <div id="AllPOs" style="display:none;float:left;width:210px;">
        <div id="errAllPOs" class="errorClass"></div>
        <div id="POHeading" class="listHeading">Purchase Orders</div>
        <div id="POList"></div>
    </div>
    <!-- Add New PO -->
    <div id="AddPO" style="display:none;float:left;background-color:#C6C6C6;width:630px;padding:10px;">
        <div class="formTitle">Add New Purchase Order</div>
        <div class="formLabel">Customer</div>
        <input type="text" id="newPOCustomer" disabled="disabled" style="width:400px;"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">PO Number</div>
        <input type="text" id="newPONumber" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">PO Amount</div>
        <input type="text" id="newPOAmount" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Dates</div>
        <div class="formLabel" style="width:200px">PO Date</div>
        <div class="formLabel" style="width:200px">Due Date</div>
        <div class="formLabel">&nbsp;</div>
        <input type="text" id="newPODate" readonly="readonly" style="width:172.5px"/>
        <input type="text" id="newPODueDate" readonly="readonly" style="width:172.5px"/>
        <div class="clear" style="height:145px;">&nbsp;</div>
        <div class="button" onclick="cancelNewPO();" style="margin-right:15px;">Cancel</div>
        <div class="button" onclick="saveNewPO();">Save</div>
    </div>
    <!-- Edit PO -->
    <div id="PODetails" style="display:none;float:left;background-color:#C6C6C6;width:630px;padding:10px;">
        <div class="formTitle">Edit Purchase Order</div>
        <div class="formLabel">Customer</div>
        <input type="text" id="editPOCustomer" disabled="disabled" style="width:400px;"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">PO Number</div>
        <input type="text" id="editPONumber" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">PO Amount</div>
        <input type="text" id="editPOAmount" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Dates</div>
        <div class="formLabel" style="width:200px">PO Date</div>
        <div class="formLabel" style="width:200px">Due Date</div>
        <div class="formLabel">&nbsp;</div>
        <input type="text" id="editPODate" readonly="readonly" style="width:172.5px"/>
        <input type="text" id="editPODueDate" readonly="readonly" style="width:172.5px"/>
        <div class="clear" style="height:145px;">
            &nbsp;
        </div>
        <div style="float:right;">
            <div class="button" onclick="cancelEditPO();" style="margin-right:15px;">Cancel</div>
            <div class="button" onclick="deleteEditPO();">Delete</div>
            <div class="button" onclick="saveEditPO();">Save</div>
            <div class="clear">&nbsp;</div>
            <div class="button" id="addInvoiceToPO" style="float:right;width:245px;margin-right:15px;">Raise Invoice for this PO</div>
            <div class="clear">&nbsp;</div>
            <div class="button" id="viewInvoicesForPO" style="float:right;width:245px;margin-right:15px;">View Invoices</div>
            <div class="clear">&nbsp;</div>
        </div>
        <div style="float:left;">
            <div class="formLabel">Documents:</div>
            <div class="clear">&nbsp;</div>
            <div id="POAttachments"></div>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Upload Document:</div>
                <input id="poUpload" type="file" style="width:460px"/>
        </div>
    <!-- Invoice View-->
    <div id="AllInvoices" style="display:none;float:left;width:210px;">
        <div id="errAllInvoices" class="errorClass"></div>
        <div id="InvoiceHeading" class="listHeading">Invoices</div>
        <div id="InvoiceList"></div>
    </div>
    <!-- Add New Invoice -->
    <div id="AddInvoice" style="display:none;float:left;background-color:#C6C6C6;width:630px;padding:10px;">
        <div class="formTitle">Add New Invoice</div>
        <div class="formLabel">Customer</div>
        <input type="text" id="newInvoiceCustomer" disabled="disabled" style="width:400px;"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">PO Number</div>
        <input type="text" id="newInvoicePO" disabled="disabled" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Invoice Number</div>
        <input type="text" id="newInvoiceNumber" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Invoice Amount</div>
        <input type="text" id="newInvoiceAmount" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Details</div>
        <div class="formLabel">Date</div>
        <div class="formLabel">Terms (days)</div>
        <div class="formLabel">Status</div>
        <div class="formLabel">&nbsp;</div>
        <input type="text" id="newInvoiceDate" readonly="readonly" style="width:120px"/>
        <input type="text" id="newInvoiceTerms" value="30" style="width:120px"/>
        <select id="newInvoiceStatus" style="width:120px">
            <option value="Open" selected="selected">Open</option>
            <option value="Paid">Paid</option>
            <option value="Canceled">Canceled</option>
            <option value="Rejected">Rejected</option>
        </select>
        <div class="clear" style="height:120px;">&nbsp;</div>
        <div class="button" onclick="cancelNewInvoice();" style="margin-right:15px;">Cancel</div>
        <div class="button" onclick="saveNewInvoice();">Save</div>
    </div>
    <!-- Edit Invoice -->
    <div id="InvoiceDetails" style="display:none;float:left;background-color:#C6C6C6;width:630px;padding:10px;">
        <div class="formTitle">Edit Invoice</div>
        <div class="formLabel">Customer</div>
        <input type="text" id="editInvoiceCustomer" disabled="disabled" style="width:400px;"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">PO Number</div>
        <input type="text" id="editInvoicePO" disabled="disabled" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Invoice Number</div>
        <input type="text" id="editInvoiceNumber" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Invoice Amount</div>
        <input type="text" id="editInvoiceAmount" style="width:400px"/>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Details</div>
        <div class="formLabel">Date</div>
        <div class="formLabel">Terms (days)</div>
        <div class="formLabel">Status</div>
        <div class="formLabel">&nbsp;</div>
        <input type="text" id="editInvoiceDate" readonly="readonly" style="width:120px"/>
        <input type="text" id="editInvoiceTerms" style="width:120px"/>
        <select id="editInvoiceStatus" style="width:120px">
            <option value="Open">Open</option>
            <option value="Paid">Paid</option>
            <option value="Canceled">Canceled</option>
            <option value="Rejected">Rejected</option>
        </select>
        <div class="clear" style="height:120px;">&nbsp;</div>
        <div class="button" onclick="cancelEditInvoice();" style="margin-right:15px;">Cancel</div>
        <div class="button" onclick="deleteEditInvoice();">Delete</div>
        <div class="button" onclick="saveEditInvoice();">Save</div>
        <div class="clear">&nbsp;</div>
        <div>
            <div class="formLabel">Documents:</div>
            <div class="clear">&nbsp;</div>
            <div id="InvoiceAttachments"></div>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Upload Document:</div>
                <input id="invUpload" type="file" style="width:460px"/>
        </div>
    </div>
    <!-- Reports View -->
    <div id="AllReports" style="display:none;float:left;width:870px;background-color:#C6C6C6">
        <div id="errReports" class="errorClass"></div>
        <div class="clear"></div>
        <div id="chartArea" class="reportChart">
            <div class="chartLabel">Total Invoiced</div>
            <div id="totalInvoicedBar" class="chartBar"></div>
            <div class="clear"></div>
            <div class="chartLabel">Total Received</div>
            <div id="totalReceivedBar" class="chartBar"></div>
            <div class="clear"></div>
            <div class="chartLabel">Amount Becoming Due</div>
            <div id="totalDueBar" class="chartBar"></div>
            <div class="clear"></div>
            <div class="chartLabel">Amount Overdue</div>
            <div id="totalOverdueBar" class="chartBar"></div>
        </div>
    </div>
</asp:Content>
