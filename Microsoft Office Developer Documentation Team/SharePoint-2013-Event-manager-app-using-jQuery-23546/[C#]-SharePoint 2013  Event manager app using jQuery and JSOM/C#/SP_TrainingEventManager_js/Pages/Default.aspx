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
    <script type="text/javascript" src="../Scripts/jquery-ui-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui1.10.2.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-timepicker.js"></script>
    <script type="text/javascript" src="../Scripts/date.js"></script>

</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink1" name="clienttemplates.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink2" name="clientforms.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink3" name="clientpeoplepicker.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink4" name="autofill.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink5" name="sp.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink6" name="sp.runtime.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink7" name="sp.core.js" runat="server" LoadAfterUI="true" Localizable="false" />

    <!--Event Manager View-->
    <div id="Home" style="display: none; width: 100%;">
        <div class="tile tileEvent" id="EventsTile" onclick="showEvents();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Events</div>
        </div>
        <div class="tile tileEvent" id="AssetsTile" onclick="showTrainingAssets();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Training Assets</div>
        </div>
        <div id="errGeneral" class="errorClass"></div>
    </div>
    <div class="clear">&nbsp;</div>
    <div style="margin-left: 10px;">

        <!-- Assets View -->
        <div id="AllAssets" style="display: none; float: left; width: 100%">
            <div id="errAllAssets" class="errorClass"></div>
            <div id="AssetsHeading" class="listHeading">Assets</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div id="AssetList" style="display: none;"></div>
            <div id="AddNewRoom" class="resourceClicker" onclick="addAssets();">+ Add Assets</div>
            <div id="assetList"></div>

        </div>

        <!-- Add New Asset -->
        <div id="AddAsset" title="Add New Assets" style="display: none; float: left;">
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Room Name*</div>
            <input type="text" id="addRoom" style="width: 400px" />
            <div class="clear" style="height: 30px">&nbsp;</div>
            <div class="formLabel">Projectors*</div>
            <input type="text" id="addProjectors" style="width: 400px" />
            <div class="clear" style="height: 30px">&nbsp;</div>
            <div class="formLabel">State Quantity of Student PCs*</div>
            <input type="text" id="addStudentPC" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="formLabel">State Quantity of Instructor PCs*</div>
            <input type="text" id="addInstructorPC" style="width: 400px" />
            <div class="clear" style="height: 30px">&nbsp;</div>
            <div class="button" onclick="cancelNewAssets();" style="margin-right: 15px;">Cancel</div>
            <div class="button" onclick="saveNewAssets();">Save</div>
        </div>

        <!-- Edit Asset -->
        <div id="EditAsset" title="Edit Assets" style="display: none; float: left;">
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Room Name*</div>
            <input type="text" id="editRoom" style="width: 400px" />
            <div class="clear" style="height: 30px">&nbsp;</div>
            <div class="formLabel">Projectors*</div>
            <input type="text" id="editProjectors" style="width: 400px" />
            <div class="clear" style="height: 30px">&nbsp;</div>
            <div class="formLabel">State Quantity of Student PCs*</div>
            <input type="text" id="editStudentPC" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="formLabel">State Quantity of Instructor PCs*</div>
            <input type="text" id="editInstructorPC" style="width: 400px" />
            <div class="clear" style="height: 30px">&nbsp;</div>
            <div class="button" onclick="cancelEditAssets();" style="margin-right: 15px;">Cancel</div>
            <div class="button" onclick="saveEditAssets();">Save</div>
        </div>


        <!-- Events View -->
        <div id="AllEvents" style="display: none; float: left; width: 100%">
            <div id="errAllEvents" class="errorClass"></div>
            <div id="EventsHeading" class="listHeading">Events</div>
            <div id="AddNewEvent" class="clicker" onclick="addNewEvent();">+ New Event</div>
            <div id="EventList">
            </div>
        </div>

        <!-- Add New Event -->
        <div id="AddEventDetails" title="Add New Event" style="display: none; float: left; background-color: white; padding: 10px;">
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Event *</div>
            <input type="text" id="newEvent" style="width: 300px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Start Date</div>
            <input type="text" id="newStartDate" style="width: 150px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">End Date</div>
            <input type="text" id="newEndDate" style="width: 150px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div id="assignResource" class="clicker" style="float: left; width: 150px;" onclick="assignResource()">+ Assign resource</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div id="resource" style="display: none;">
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Location</div>
                <select id="location" style="width: 150px"></select>

                <div class="button" onclick="fillOtherData();" id="fillOtherData">Fill Data</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Total Instructors</div>
                <input type="text" id="newInstructors" style="width: 150px" disabled="disabled" />
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Projectors</div>
                <input type="text" id="projectors" style="width: 150px" disabled="disabled" />
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Max Students</div>
                <input type="text" id="studentPCs" style="width: 150px" disabled="disabled" />
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Attendees</div>
                <div id="peoplePickerDiv" style="float: left;"></div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="button" onclick="cancelNewEvent();" id="cancelButton" style="margin-right: 15px;">Cancel</div>
                <div class="button" onclick="saveNewEvent();" id="saveButton">Save</div>
            </div>
        </div>

        <!-- Edit Event -->
        <div id="editEventDetails" title="Edit Event" style="display: none; float: left; background-color: white; padding: 10px;">
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div id="assignEditResource" class="clicker" style="float: left; width: 150px;" onclick="assignEditResource()">+ Edit Event</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Event </div>
            <input type="text" id="editEvent" style="width: 300px" disabled="disabled"/>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Start Date</div>
            <input type="text" id="editStartDate" style="width: 150px" disabled="disabled"/>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">End Date</div>
            <input type="text" id="editEndDate" style="width: 150px" disabled="disabled"/>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Location</div>
            <input id="editLocation" style="width: 150px; display:none;" disabled="disabled">
            <select id="editResources" style="width: 150px; display:none;"></select>
            <div class="button" onclick="fillEditOtherData();" style="display:none;" id="fillEditOtherData">Fill Data</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Total Instructors</div>
            <input type="text" id="editInstructors" style="width: 150px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Projectors</div>
            <input type="text" id="editProjector" style="width: 150px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Max Students</div>
            <input type="text" id="editStudentPCs" style="width: 150px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Attendees</div>
            <div id="addNewAttendee" class="clicker" onclick="addNewAttendees();">+ New Attendees</div>
            <div class="clear">&nbsp;</div>
            <div id="showAttendees"></div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="button" onclick="deleteEditEvent();" style="margin-right: 15px;">Delete</div>
            <div class="button" onclick="cancelEditEvent();" style="margin-right: 15px;">Cancel</div>
            <div class="button" onclick="saveEditEvent();" style="display:none;" id="saveEditEvent">Save</div>
        </div>
    </div>

    <!-- Add New Attendees in Edit event Form -->
    <div id="editAttendees" title="Add New Attendee" style="display: none; width: 500px">
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Attendee Name</div>
        <div id="editPeoplePicker" style="float: left; width: 200px;"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelAttendee();" style="margin-right: 15px;">Cancel</div>
        <div class="button" onclick="saveEditAttendee();">Save</div>
    </div>

    <div id="editReservedAssets" title="Edit Resources" style="display: none">
        <div class="formLabel">Location</div>
        
        <div class="button" onclick="fillOtherData();" id="editFillData">Fill Data</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Employee View-->
    <div id="EmployeeHome" style="display: none; float: left; width: 100%;">
        <div class="tile" onclick="showEnrllEvents();" id="showEnrllEvents" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Enrolled Events</div>
        </div>
        <div class="tile" onclick="showAllEvents();" id="showAllEvents" unselectable="on">
            <div class="tileHeadingText" unselectable="on">All Events</div>
        </div>
        <div class="errorClass" id="errEvents">
        </div>
    </div>
    <div class="clear">&nbsp;</div>

    <!-- Enrolled Events View -->
    <div id="AllEnrllEvents" style="display: none; float: left; width: 100%;">
        <div id="errEmpEvents" class="errorClass"></div>
        <div id="EnrllHeading" class="listHeading">Enrolled Events</div>
        <div id="EnrllEventList"></div>
    </div>
    <div id="OtherEvents" style="display: none; float: left; width: 100%;">
        <div id="otherHeading" class="listHeading">All Events</div>
        
    </div>
    <div id="AllEventsList" style="width: 1024px;"></div>

    <!-- Event Details View -->
    <div id="eventDetails" title="Event Details" style="float: left; display: none; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Event Name</div>
        <input type="text" id="eventName" style="width: 300px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Location</div>
        <input type="text" id="eventLocation" style="width: 300px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Start Date</div>
        <input type="text" id="eventStartDate" readonly="readonly" style="width: 150px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">End Date</div>
        <input type="text" id="eventEndDate" readonly="readonly" style="width: 150px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="button" onclick="cancelEvent();">Close</div>
        <div class="button" onclick="deleteEnrollEvent();" id="deleteEnroll" style="display: none">Delete</div>
    </div>

    <!-- Enrolled Event Details-->
    <div id="enrollEventDetails" title="Event Details" style="float: left; display: none; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Event Name</div>
        <input type="text" id="enrollEventName" style="width: 300px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Location</div>
        <input type="text" id="enrollEventLocation" style="width: 300px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">Start Date</div>
        <input type="text" id="enrollStartDate" readonly="readonly" style="width: 150px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="formLabel">End Date</div>
        <input type="text" id="enrollEndDate" readonly="readonly" style="width: 150px; color: black;" disabled="disabled" unselectable="on" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>

        <div class="button" onclick="cancelEnrollEvent();">Close</div>
        <div class="button" onclick="enrollEvent();">Enroll</div>
    </div>
</asp:Content>
