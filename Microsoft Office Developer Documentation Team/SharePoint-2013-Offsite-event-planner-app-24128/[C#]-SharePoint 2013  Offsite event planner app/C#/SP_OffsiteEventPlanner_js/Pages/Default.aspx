<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script src="../Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link rel="stylesheet" type="text/css" href="../Content/jquery-ui-1.10.2.custom.css" />
    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.10.2.js"></script>


</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink1" Name="clienttemplates.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink2" Name="clientforms.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink3" Name="clientpeoplepicker.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink4" Name="autofill.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink5" Name="sp.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink6" Name="sp.runtime.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink7" Name="sp.core.js" runat="server" LoadAfterUI="true" Localizable="false" />

    <!-- Manager View -->
    <div id="AllOffsites" style="display: none; float: left; width: 210px;">
        <div id="errAllOffsites" class="errorClass"></div>
        <div id="OffsitesHeading" class="listHeading">Offsites</div>
        <div id="AddNewOffsite" class="offsiteClicker" onclick="addNewOffsite();">+ New Offsite</div>
        <div id="OffsiteList"></div>
    </div>

    <!-- Add New Offsite -->
    <div id="AddOffsite" style="display: none; float: left; background-color: #BDEDFF; width: 600px; padding: 10px; margin-top: 35px; margin-bottom: 35px;">
        <div class="addOffsiteTitle">Add New Offsite</div>

        <div class="button" id="cancelNewOffsite" onclick="cancelNewOffsite();" style="margin-right: 20px;">Cancel</div>
        <div class="button" id="saveNewOffsite" onclick="saveNewOffsite();">Save</div>
        <div class="clear" style="height: 50px;">&nbsp;</div>

        <div class="formLabel">Offsite Name *</div>
        <input type="text" id="newOffsite" style="width: 400px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Budget</div>
        <input type="text" id="newOffsiteBudget" style="width: 400px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Attendees</div>
        <div id="peoplePickerDiv" style="float: left; width: 200px;"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Dates</div>
        <div class="formLabel" style="width: 200px">Start Date</div>
        <div class="formLabel" style="width: 200px">End Date</div>
        <div class="formLabel">&nbsp;</div>
        <input type="text" id="newStartDate" readonly="readonly" style="width: 172.5px" />
        <input type="text" id="newEndDate" readonly="readonly" style="width: 172.5px" />
        <div class="clear" style="height: 145px;">&nbsp;</div>
    </div>

    <!-- Edit Offsite -->
    <div id="OffsiteDetails" style="display: none; float: left; background-color: #BDEDFF; width: 600px; padding: 10px; margin-top: 35px;">
        <div class="formTitle">Edit Offsite</div>

        <div class="button" id="cancelEditOffsite" onclick="cancelEditOffsite();" style="margin-right: 15px;">Cancel</div>
        <div class="button" id="saveEditOffsite" onclick="saveEditOffsite();">Save</div>
        <div class="button" id="deleteEditOffsite" onclick="deleteEditOffsite();">Delete</div>
        <div class="clear" style="height: 50px;">&nbsp;</div>

        <div class="formLabel">Offsite Name *</div>
        <input type="text" id="editOffsite" style="width: 400px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Budget</div>
        <input type="text" id="editBudget" style="width: 400px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Attendees</div>
        <div id="addNewAttendee" class="clicker" onclick="addNewAttendees();">+ New Attendees</div>
        <div class="clear">&nbsp;</div>
        <div id="showAttendees"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Dates</div>
        <div class="formLabel" style="width: 200px">Start Date</div>
        <div class="formLabel" style="width: 200px">End Date</div>
        <div class="formLabel">&nbsp;</div>

        <input type="text" id="editStartDate" readonly="readonly" style="width: 172.5px" />
        <input type="text" id="editEndDate" readonly="readonly" style="width: 172.5px" />
        <div class="clear" style="height: 35px;">&nbsp;</div>

        <div class="clear">&nbsp;</div>
        <div class="suggestLabel">Approved Suggestions</div>
        <div class="clear">&nbsp;</div>
        <div id="errorAppSuggestion" style="margin-left: 15px;"></div>
        <div class="itemTable" style="color: black">Activity Name</div>
        <div class="clearWidth">&nbsp;</div>
        <div class="itemTable" style="color: black">Budget</div>
        <div class="clearWidth">&nbsp;</div>

        <div class="itemTable" style="color: black">Votes</div>
        <div class="clear">&nbsp;</div>
        <div id="approvedSuggestion"></div>
        <div class="clear" style="height: 30px;">&nbsp;</div>

        <div class="suggestLabel">List of Suggestions</div>
        <div class="clear">&nbsp;</div>
        <div id="errorAllSuggestions" style="margin-left: 15px;"></div>
        <div class="itemTable" style="color: black">Activity Name</div>
        <div class="clearWidth">&nbsp;</div>
        <div class="itemTable" style="color: black">Budget</div>
        <div class="clearWidth">&nbsp;</div>
        <div class="clearWidth"></div>
        <div class="itemTable" style="color: black">Votes</div>
        <div class="clear">&nbsp;</div>
        <div id="allSuggestions"></div>
    </div>


    <!-- Add New Attendees in Edit offsite Form -->
        <div id="editAttendees" title="Add New Attendee" style="display:none; width:500px">
                <div class="clear">&nbsp;</div>
                <div class="formLabel">Attendee Name</div>
              <div id="editPeoplePicker" style="float: left; width: 200px;"></div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
                <div class="button" onclick="cancelAttendee();" style="margin-right: 15px;">Cancel</div>
                <div class="button" onclick="saveEditAttendee();">Save</div>
            </div>

    <!-- Approved Activity Details -->
    <div id="appSuggestionDetails" title="Activity Details" style="display: none; width: 100%">
        <div class="formLabel">Activity Name *</div>
        <input type="text" id="appActivity" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Description</div>
        <input type="text" id="appDescription" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Budget</div>
        <input type="text" id="appBudget" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Location</div>
        <input type="text" id="appLocation" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Web Site</div>
        <input type="text" id="appWebSite" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Votes</div>
        <input type="text" id="appVotes" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="button" onclick="closeAppSuggestDialog();">Close</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- All Activity Details -->
    <div id="allSuggestionDetails" title="Activity Details" style="display: none; width: 100%">
        <div class="formLabel">Activity Name *</div>
        <input type="text" id="allActivity" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Description</div>
        <input type="text" id="allDescription" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Budget</div>
        <input type="text" id="allBudget" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Location</div>
        <input type="text" id="allLocation" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Web Site</div>
        <input type="text" id="allWebSite" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Votes</div>
        <input type="text" id="allVotes" style="width: 300px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="button" onclick="approveActivity();">Approve</div>
        <div class="button" onclick="closeApproveDialog();">Close</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Employee View -->
    <div id="EmpOffsite" style="display: none; float: left; width: 210px;">
        <div id="errEmpOffsite" class="errorClass"></div>
        <div id="EmpOffsiteHeading" class="listHeading">Offsites</div>
        <div id="EmpOffsiteList"></div>
    </div>

    <!-- Show Employee Offsite Details -->
    <div id="empOffsiteDetails" style="display: none; float: left; background-color: #BDEDFF; width: 630px; padding: 10px;">
        <div class="formTitle">Offsite Details</div>
        <div class="cancelButton" onclick="cancelEditOffsite();" style="margin-right: 15px;"></div>
        <div class="clear" style="height: 50px;">&nbsp;</div>

        <div class="formLabel">Offsite Name </div>
        <input type="text" id="empOffsiteName" style="width: 400px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Budget</div>
        <input type="text" id="empOffsiteBudget" style="width: 400px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Dates</div>
        <div class="formLabel" style="width: 200px">Start Date</div>
        <div class="formLabel" style="width: 200px">End Date</div>
        <div class="formLabel">&nbsp;</div>
        <input type="text" id="empStartDate" readonly="readonly" style="width: 172.5px" disabled="disabled" />
        <input type="text" id="empEndDate" readonly="readonly" style="width: 172.5px" disabled="disabled" />
        <div class="clear">&nbsp;</div>
        <div class="suggestLabel">Activities</div>
        <div class="clear">&nbsp;</div>
        <div id="addActivity" class="clicker" onclick="addNewActivity();">+ New Activity</div>
        <div id="activityList"></div>
        </div>

    <!-- New Activity Form -->
        <div id="NewActivityDetails" title="New Activity" style="display: none; width: 100%">
            <div class="formLabel">Activity Name *</div>
            <input type="text" id="activityName" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Description</div>
            <input type="text" id="description" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Location</div>
            <input type="text" id="location" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Web Site</div>
            <input type="text" id="website" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Budget</div>
            <input type="text" id="activityBudget" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="button" onclick="cancelNewActivity();" style="margin-right: 15px;">Cancel</div>
            <div class="button" onclick="saveNewActivity();">Save</div>
        </div>

        <!-- Edit Activity Form -->
        <div id="EditActivityDetails" title="Edit Activity" style="display: none; width: 100%">
            <div class="formLabel">Activity Name *</div>
            <input type="text" id="editActName" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Description</div>
            <input type="text" id="editDescription" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Location</div>
            <input type="text" id="editLocation" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Web Site</div>
            <input type="text" id="editWebSite" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Budget</div>
            <input type="text" id="editActBudget" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Votes</div>
            <div class="voteButton" onclick="addVotes();">+1</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="button" onclick="cancelEditActivity();" style="margin-right: 15px;">Cancel</div>
            <div class="button" onclick="saveEditActivity();">Save</div>
        </div>
</asp:Content>
