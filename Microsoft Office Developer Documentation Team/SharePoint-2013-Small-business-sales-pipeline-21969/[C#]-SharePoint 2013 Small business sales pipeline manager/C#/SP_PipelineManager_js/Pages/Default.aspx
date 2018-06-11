<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="VB" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Small Business Pipeline Manager
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <!--Home View-->
    <div id="Home" style="display: none; width: 100%;">
        <div class="tile tileLead" id="LeadsTile" onclick="showLeads();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Leads</div>
        </div>
        <div class="tile tileOpp" id="OppsTile" onclick="showOpps();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Opportunities</div>
        </div>
        <div class="tile tileSale" id="SalesTile" onclick="showSales();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Sale</div>
        </div>
        <div class="tile tileLostSale" id="LostSalesTile" onclick="showLostSales();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Lost Sale</div>
        </div>
        <div class="tile tileReport" id="ReportsTile" onclick="showReports();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Reports</div>
        </div>
        <div id="errGeneral" class="errorClass"></div>
    </div>
    <div class="clear">&nbsp;</div>

    <!-- Lead View -->
    <div id="AllLeads" style="display: none; float: left; width: 190px;">
        <div id="errAllLeads" class="errorClass"></div>
        <div id="LeadsHeading" class="listHeading">Leads</div>
        <div id="AddNewLead" class="clicker" onclick="addNewLead();">+ New Lead</div>
        <div id="LeadList"></div>
    </div>

    <!-- Add New Lead -->
    <div id="AddLead" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Add New Lead</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Organization Name *</div>
        <input type="text" id="newLead" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Person</div>
        <input type="text" id="newContactPerson" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Number</div>
        <input type="text" id="newContactNumber" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="newEmail" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Potential Amount *</div>
        <input type="text" id="newPotentialAmount" style="width: 150px" />
        <div class="clear" style="height: 40px;">&nbsp;</div>
        <div class="button" onclick="cancelNewLead();" style="margin-right: 15px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveNewLead(); " unselectable="on">Save</div>
    </div>

    <!-- Edit Lead -->
    <div id="LeadDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Edit Lead</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Organization Name *</div>
        <input type="text" id="editLead" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Person</div>
        <input type="text" id="editContactPerson" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Number</div>
        <input type="text" id="editContactNumber" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="editEmail" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Potential Amount *</div>
        <input type="text" id="editPotentialAmount" style="width: 150px" />
        <div class="clear" style="height: 40px;">&nbsp;</div>
        <div class="button" onclick="cancelEditLead();" style="margin-right: 15px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditLead();" unselectable="on">Save</div>
        <div class="button" id="convertToOpp" style="width: 210px;" unselectable="on">Convert to Opportunity</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Opportunity View -->
    <div id="AllOpps" style="display: none; float: left; width: 190px;">
        <div id="errAllOpps" class="errorClass"></div>
        <div id="OppHeading" class="listHeading">Opportunities</div>
        <div id="OppList"></div>
    </div>

    <!-- Edit Opportunity -->
    <div id="OppDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Edit Opportunity</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Organization Name *</div>
        <input type="text" id="editOpp" style="width: 410px;" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Person</div>
        <input type="text" id="editOppPerson" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Number</div>
        <input type="text" id="editOppNumber" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="editOppEmail" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Opportunity Amount *</div>
        <input type="text" id="editOppAmount" style="width: 150px" />
        <div class="clear" style="height: 25px">&nbsp;</div>
        <div style="float: left;">
            <div class="clear">&nbsp;</div>
            <div id="OppAttachments"></div>
        </div>
        <div class="clear">&nbsp;</div>
        <div style="width: 220px; font-size: 16px; margin-left: 50px; color: black; font-family: 'Segoe UI',sans-serif;">Upload Proposal Document:</div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <input id="oppUpload" type="file" style="width: 460px; margin-left: 50px;" />
        <div class="clear" style="height: 40px">&nbsp;</div>
        <div style="float: right;">
            <div class="button" onclick="cancelEditOpp();" style="margin-right: 15px;" unselectable="on">Cancel</div>
            <div class="button" onclick="saveEditOpp(); " style="margin-right: 15px;" unselectable="on">Save</div>
            <div class="button" id="convertToLostSale" style="width: 90px; margin-right: 15px;" unselectable="on">Lost Sale</div>
            <div class="button" id="convertToSale" style="width: 140px; margin-right: 15px;" unselectable="on">Convert to Sale</div>
            <div class="clear">&nbsp;</div>
        </div>
    </div>

    <!-- Sale View -->
    <div id="AllSales" style="display: none; float: left; width: 190px;">
        <div id="errAllSales" class="errorClass"></div>
        <div id="SaleHeading" class="listHeading">Sales</div>
        <div id="SaleList"></div>
    </div>

    <!-- Edit Sale -->
    <div id="SaleDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Sale Details</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Organization Name *</div>
        <input type="text" id="editSale" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Person</div>
        <input type="text" id="editSalePerson" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Number</div>
        <input type="text" id="editSaleNumber" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="editSaleEmail" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Sale Amount *</div>
        <input type="text" id="editSaleAmount" style="width: 150px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="button" onclick="cancelEditSale();" style="margin-right: 15px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditSale();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Lost Sale View -->
    <div id="AllLostSales" style="display: none; float: left; width: 190px;">
        <div id="errAllLostSales" class="errorClass"></div>
        <div id="LostSaleHeading" class="listHeading">Lost Sales</div>
        <div id="LostSaleList"></div>
    </div>
    <!-- Edit Lost Sale -->
    <div id="LostSaleDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Lost Sale Details</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Organization Name</div>
        <input type="text" id="lostSale" style="width: 410px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Person</div>
        <input type="text" id="lostSalePerson" style="width: 410px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Contact Number</div>
        <input type="text" id="lostSaleNumber" style="width: 410px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Email</div>
        <input type="text" id="lostSaleEmail" style="width: 410px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Lost Sale Amount</div>
        <input type="text" id="lostSaleAmount" style="width: 150px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelLostSale();" style="margin-right: 15px;" unselectable="on">Cancel</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Reports View -->
    <div id="AllReports" style="display: none; float: left; width: 870px;">
        <div id="errReports" class="errorClass"></div>
        <div class="clear">&nbsp;</div>
        <div class="listHeading" style="text-align: left;">Reports</div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="clear" />
        <div id="amountPipeline" class="reportClicker" style="float: left;" onclick="getAmount();">Pipeline Amount($)</div>
        <div class="clear">&nbsp;</div>
        <div id="countPipeline" class="reportClicker" onclick="getCount();" unselectable="on">Pipeline Number</div>
        <div class="clear">&nbsp;</div>
        <div id="pipelineName" class="nameText" style="display: none">Pipeline</div>
        <div class="clear">&nbsp;</div>
        <div id="chartArea" style="animation: ease-in forwards; display: none; background-color: #87CEEB;" class="reportChart">
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div id="pipelineReport">
                <div class="pipeText" style="margin-left: 50px;" id="leadText">Lead</div>
                <div class="pipeText" id="blankopp"></div>
                <div class="pipeText" id="saleText">Sale</div>
                <div class="clear" />
                <div class="pipeLead" id="pipeLead" style="margin-left: 50px;" onclick="drillLead();" unselectable="on">
                    <div class="tileHeadingText" id="hoverLead" unselectable="on"><span>Leads</span></div>
                </div>
                <div class="pipeOpportunity" id="pipeOpportunity" onclick="drillOpp();" unselectable="on">
                    <div class="tileHeadingText" id="hoverOpp" unselectable="on"><span>Opportunity</span></div>
                </div>
                <div class="pipeSale" id="pipeSale" onclick="drillSale();" unselectable="on">
                    <div class="tileHeadingText" id="hoverSale" unselectable="on"><span>Sale</span></div>
                </div>
                <div class="clear" />
                <div class="pipeText" style="margin-left: 50px;" id="blankLead"></div>
                <div class="pipeText" id="oppText">Opportunity</div>
            </div>
            <div class="clear" />
        </div>
        <div class="clearSpace">&nbsp;</div>
        <div id="conversionName" class="nameText" style="display: none">Won/Lost</div>
        <div class="clear">&nbsp;</div>
        <div id="conversionRate" class="conversionChart" style="background-color: #87CEEB;">
            <div class="clear" style="height: 27px"></div>
            <div class="chartLabel">Won</div>
            <div id="wonOpp" class="chartBar" onclick="drillWon();"></div>
            <div class="clear"></div>
            <div class="chartLabel">Lost</div>
            <div id="lostOpp" class="chartBar" onclick="drillLost();"></div>
            <div class="clear"></div>
        </div>
        <div id="drillDown" style="display: none; float: left; background-color: white; width: 940px; padding: 10px;">
            <div id="pipeDrillDown" style="display: none;">
                <div style="float: left; font-weight: bold" class="convLabel" id="pipeSummary"><span>Loading....</span></div>
                <div class="clear">&nbsp;</div>
                <div style="float: left; font-weight: bold" class="reportLabel">Organization Name</div>
                <div class="reportLabel" style="font-weight: bold">Contact Person</div>
                <div class="reportLabel" style="font-weight: bold">Contact Number</div>
                <div class="reportLabel" style="font-weight: bold">Email</div>
                <div class="amountLabel" style="font-weight: bold;">Deal Amount</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
            </div>
            <div id="wonLostDrillDown" style="display: none;">
                <div id="conversionSummary" class="convLabel" style="float: left; font-weight: bold;"><span>Loading....</span></div>
                <div class="clear">&nbsp;</div>
                <div class="reportLabel" style="float: left; font-weight: bold">Organization Name</div>
                <div class="reportLabel" style="font-weight: bold">Contact Person</div>
                <div class="reportLabel" style="font-weight: bold">Contact Number</div>
                <div class="reportLabel" style="font-weight: bold">Email</div>
                <div class="amountLabel" style="font-weight: bold">Amount</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
            </div>
            <div id="drillTable"></div>
        </div>
    </div>
</asp:Content>
