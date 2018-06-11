<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.7.1.min.js"></script>
    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Community Initiative Manager
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <!-- Home View-->
    <div id="Home" style="display: none; width: 100%;">
        <div class="tile" id="MyInitiativesTile" onclick="showMyInitiatives();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">My Projects</div>
        </div>
        <div class="tile" id="MyRolesTile" onclick="showMyRoles();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">My Roles</div>
        </div>
        <div class="tile" id="AllInitiativesTile" onclick="showAllInitiatives();" unselectable="on">
            <div class="tileHeadingText" unselectable="on">Contribute</div>
        </div>
        <div id="errGeneral" class="errorClass"></div>
    </div>

    <div class="clear">&nbsp;</div>
    <div id="MyInitiative" style="display: none; background-color: burlywood; width: 900px; height: 730px; margin-bottom: 20px;">
        <!-- My Initiatives View -->
        <div id="MyInitiatives" style="float: left; width: 210px;">
            <div id="errMyInitiatives" class="errorClass"></div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div id="AddNewInitiative" class="clicker" onclick="addNewInitiative();">+ New Project</div>
            <div id="MyInitiativeList"></div>
        </div>

        <!-- Add New Initiative -->
        <div id="AddInitiative" style="display: none; float: left; background-color: cornsilk; width: 630px; padding: 10px; margin-top: 40px;">
            <div class="formTitle">Add New Project</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Project Name *</div>
            <input type="text" id="newInitiative" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Fund Targets</div>
            <input type="text" id="newFundTargets" style="width: 200px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Man Hours</div>
            <input type="text" id="newManHours" style="width: 200px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">No of Volunteers Required</div>
            <input type="text" id="newVolunteers" style="width: 200px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Roles</div>
            <input type="text" id="newRoles" style="width: 200px; float: left" />
            <div class="roleButton" onclick="addRoles();">+</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Dates</div>
            <div class="formLabel" style="width: 200px">Start Date</div>
            <div class="formLabel" style="width: 200px">End Date</div>
            <div class="formLabel">&nbsp;</div>
            <input type="text" id="newStartDate" readonly="readonly" style="width: 172.5px" />
            <input type="text" id="newEndDate" readonly="readonly" style="width: 172.5px" />
            <div class="clear" style="height: 145px;">&nbsp;</div>

            <div class="myInitbutton" onclick="cancelNewInitiative();" style="margin-right: 15px;">Cancel</div>
            <div class="myInitbutton" onclick="saveNewInitiative();">Save</div>
        </div>

        <!-- Edit Initiative -->
        <div id="editInitiative" style="display: none; float: left; background-color: cornsilk; width: 630px; padding: 10px; margin-top: 40px;">
            <div class="formTitle">Edit Project</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Project Name *</div>
            <input type="text" id="editInitiativeName" style="width: 400px" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div style="margin-left: 180px; float: left">&nbsp;</div>
            <div style="color: black; margin-left: 28px; float: left; font-size: 16px; color: maroon">Required</div>
            <div style="color: black; margin-left: 168px; float: left; font-size: 16px; color: maroon">Achieved</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Fund Targets</div>
            <input type="text" id="editFundTargets" style="width: 150px" />
            <input type="text" id="achievedFundTargets" style="width: 150px; margin-left: 70px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Man Hours</div>
            <input type="text" id="editManHours" style="width: 150px" />
            <input type="text" id="achievedManHours" style="width: 150px; margin-left: 70px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">No of Volunteers Required</div>
            <input type="text" id="editVolunteers" style="width: 150px" />
            <input type="text" id="achievedVolunteers" style="width: 150px; margin-left: 70px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel" style="float: left;">Volunteer Names</div>
            <div id="achievedVolunteer"></div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Remaining Roles</div>
            <div class="formLabel" id="remainingRoles" style="float: left"></div>
            <select id="editRoles" style="width: 150px"></select>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Dates</div>
            <div class="formLabel" style="width: 200px">Start Date</div>
            <div class="formLabel" style="width: 200px">End Date</div>
            <div class="formLabel">&nbsp;</div>
            <input type="text" id="editStartDate" readonly="readonly" style="width: 172.5px" />
            <input type="text" id="editEndDate" readonly="readonly" style="width: 172.5px" />
            <div class="clear" style="height: 145px;">&nbsp;</div>
            <div id="initAchieved" class="achieveLabel" style="display: none">This initiative is Achieved!!!</div>

            <div class="myInitbutton" onclick="cancelEditInitiative();" style="margin-right: 15px;">Cancel</div>
            <div class="myInitbutton" id="saveEditInitiative" onclick="saveEditInitiative();">Save</div>
            <div class="achievebutton" id="achieveInitiative" onclick="achieveInitiative();" style="display: none;">Set to Achieved</div>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
    </div>
    <!-- All Initiatives View -->
    <div id="AllInitiative" style="display: none; background-color: skyblue; width: 900px; height: 730px;">
        <div id="AllInitiatives" style="float: left; width: 210px;">
            <div id="errAllInitiatives" class="errorClass"></div>
            <div id="AllInitiativesHeading" class="contributeHeading">Following projects are set up by other users</div>
            <div id="AllInitiativeList"></div>
        </div>

        <!-- Initiative Details-->
        <div id="InitiativeDetails" style="display: none; float: left; background-color: #C3F0FF; width: 630px; padding: 10px; margin-top: 60px;">
            <div class="formTitle">Project Details</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Project Name</div>
            <input type="text" id="initiativeName" style="width: 400px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Dates</div>
            <div class="formLabel" style="width: 200px">Start Date</div>
            <div class="formLabel" style="width: 200px">End Date</div>
            <div class="formLabel">&nbsp;</div>
            <input type="text" id="startDate" readonly="readonly" style="width: 172.5px" disabled="disabled" />
            <input type="text" id="endDate" readonly="readonly" style="width: 172.5px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Fund Targets</div>
            <input type="text" id="fundTargets" style="width: 172.5px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Man Hours</div>
            <input type="text" id="manHours" style="width: 172.5px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div class="formLabel">Volunteers</div>
            <input type="text" id="volunteers" style="width: 172.5px" disabled="disabled" />
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>

            <div id="enrollInitiative" style="display: none">
                <div class="formLabel">Roles</div>
                <select id="roles" style="width: 180px"></select>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Work Hours</div>
                <input type="text" id="workHours" style="width: 172.5px" />
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="formLabel">Donation</div>
                <input type="text" id="donation" style="width: 172.5px" />
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>
                <div class="clear">&nbsp;</div>

                <div class="allInitButton" id="saveEnrollInitiative" onclick="saveEnrollInitiative();" style="display: none; margin-right:15px">Save</div>
            </div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div class="achieveLabel" id="achieveLabel" style="display: none">This initiative is Achieved!!!</div>
            <div class="allInitButton" onclick="cancelInitiative();" style="margin-right: 15px;">Cancel</div>
            <div class="allInitButton" id="enrollToInitiative" onclick="enrollToInitiative();" style="display: none">Enroll</div>

        </div>
    </div>
    <!-- My Roles View -->
    <div id="MyRole" style="display: none; background-color: #FFCCCC; width: 900px; height: 730px;">
        <div id="MyRoles" style="float: left; width: 900px;">
            <div id="errMyRoles" class="errorClass"></div>
            <div class="clear" style="height: 30px;">&nbsp;</div>
            <div style="float: left" class="tableLabel">Project Name</div>
            <div style="float: left" class="tableLabel">Selected Role</div>
            <div style="float: left" class="tableLabel">Work Hours</div>
            <div style="float: left" class="tableLabel">Donation</div>
            <div class="clear">&nbsp;</div>
            <div class="clear">&nbsp;</div>
            <div id="MyRoleList"></div>
        </div>

    </div>

</asp:Content>
