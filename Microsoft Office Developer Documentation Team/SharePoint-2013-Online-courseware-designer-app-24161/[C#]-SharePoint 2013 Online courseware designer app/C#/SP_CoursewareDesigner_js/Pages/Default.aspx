<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Courseware App
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="GeneralError" class="errorClass"></div>


    <!-- Course List View -->
    <div id="AllCoursesView" style="display: none; float: left; width: 190px;">
        <div id="ErrAllCourses" class="errorClass"></div>
        <div id="AllCoursesHeader" class="listHeading">Courses</div>
        <div id="AddNewCourse" class="clicker" onclick="addNewCourse();">+ New Course</div>
        <div id="AllCoursesList"></div>
    </div>    

        <!-- Add New Course -->
    <div id="AddCourse" style="display: none; float: left; background-color: #CECECE; width: 730px; padding: 10px">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Add New Course</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Course Name</div>
        <input type="text" id="newCourseName" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Course Objective</div>
        <input type="text" id="newCourseObjective" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelNewCourse();" style="margin-right: 15px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveNewCourse(); " unselectable="on">Save</div>
    </div>

    <!-- Edit Course -->
    <div id="CourseDetails" style="display: none; float: left; background-color: #CECECE; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle" style="margin-bottom:5px">Edit Course</div>
        <div class="formLabel">Course Name</div>
        <input type="text" id="editCourseName" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Course Objective</div>
        <input type="text" id="editCourseObjective" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="CourseList"></div>
        <div class="clear">&nbsp;</div>
        <div id="NewModule" style="display:none;background-color:#AFAFAF">
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Module Title</div>
            <input type="text" id="NewModuleTitle" style="width:400px" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Module Objective</div>
            <input type="text" id="NewModuleObjective" style="width:400px" />
            <div class="clear">&nbsp;</div>
            <div id="ErrNewModule" class="errorClass" style="float:left;width:200px;margin-left:290px;text-align:center"></div>
            <div class="buttonLite" onclick="cancelNewModule();" style="margin-right: 20px;"  unselectable="on">Cancel</div>
            <div class="buttonLite" onclick="saveNewModule();" unselectable="on">Save</div>
            <div class="clear">&nbsp;</div>
        </div>
        <div id="AddModuleClicker" class="formClicker" onclick="addNewModule();">+ New Module</div>
        <div id="EditCourseModuleList"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="deleteEditCourseButton" class="button" onclick="deleteEditCourse();" style="margin-right: 20px;" unselectable="on">Delete</div>
        <div id="cancelEditCourseButton" class="button" onclick="cancelEditCourse();" unselectable="on">Cancel</div>
        <div id="saveEditCourseButton" class="button" onclick="saveEditCourse();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
        <div id="validateCourseButton" class="button" onclick="validateAndPublishCurrentCourse();" unselectable="on" style="margin-right: 20px;width:245px;">Validate & Publish</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Edit Module -->
    <div id="ModuleDetails" style="display: none; float: left; background-color: #CECECE; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Edit Module</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Module Title</div>
        <input type="text" id="editModuleText" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Module Objective</div>
        <input type="text" id="editModuleObjective" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="TopicList"></div>
        <div class="clear">&nbsp;</div>
        <div id="NewTopic" style="display:none;background-color:#AFAFAF">
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Topic Title</div>
            <input type="text" id="NewTopicTitle" style="width:400px" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Topic Objective</div>
            <input type="text" id="NewTopicObjective" style="width:400px" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Url</div>
            <input type="text" id="NewTopicUrl" style="width: 350px" />
            <div class="buttonLite" onclick="testNewTopicUrl();" style="float:right;margin-top:0px;margin-right:30px;width:35px;" unselectable="on">[Test]</div>
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Topic Type</div>
            <select id="NewTopicType" style="width:380px;cursor:pointer">
                <option value="Concept">Concept</option>
                <option value="Fact">Fact</option>
                <option value="Principle">Principle</option>
                <option value="Procedure">Procedure</option>
                <option value="Process">Process</option>
            </select>
            <div class="buttonLite" onclick="showNewTopicTypeHelp();" style="float:right;margin-top:0px;margin-right:30px;width:20px;" unselectable="on">[?]</div>
            <div id="NewTopicTypeHelp" style="display:none;background-color:#7E7E7E;width:370px;margin-left:290px;padding:5px;color:#ffffff">
                <div class="buttonLite" onclick="hideNewTopicTypeHelp();" style="float:right;margin-top:0px;width:20px;" unselectable="on">[ X ]</div>
                <div class="clear">&nbsp;</div>
                <div>A <i>Concept</i> is a class of items, words, or ideas that are known by a common name. A concept-type topic describes the characterics of the concept.</div>
                <div class="clear">&nbsp;</div>
                <div>A <i>Fact</i> is a simple piece of information, usually about an object or concept. A fact-type topic should simply present a single fact in a clear way.</div>
                <div class="clear">&nbsp;</div>
                <div>A <i>Principle</i> is a guideline or rule, and the parameters that govern it. Principles should allow the student to make predictions or draw implications about a specific case.</div>
                <div class="clear">&nbsp;</div>
                <div>A <i>Procedure</i> is a series of step-by-step actions and decisions that result in the achievement of a task. Students should be able to follow a procedure and it should normally result in a predictable outcome.</div>
                <div class="clear">&nbsp;</div>
                <div>A <i>Process</i> is a flow of events or activities that describe how things work. They can be thought of as the 'big picture' of how something works, rather than being a set of instructions (which should be presented as a procedure-type instead).</div>
            </div>
            <div class="clear">&nbsp;</div>
            <div id="ErrNewTopic" class="errorClass" style="float:left;width:200px;margin-left:290px;text-align:center"></div>
            <div class="buttonLite" onclick="cancelNewTopic();" style="margin-right: 20px;"  unselectable="on">Cancel</div>
            <div class="buttonLite" onclick="saveNewTopic();" unselectable="on">Save</div>
            <div class="clear">&nbsp;</div>
        </div>
        <div id="AddTopicClicker" class="formClicker" onclick="addNewTopic();">+ New Topic</div>
        <div id="EditModuleTopicList"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="deleteEditModuleButton" class="button" onclick="deleteEditModule();" style="margin-right: 15px;" unselectable="on">Delete</div>
        <div id="cancelEditModuleButton" class="button" onclick="cancelEditModule();" unselectable="on">Cancel</div>
        <div id="saveEditModuleButton" class="button" onclick="saveEditModule();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Edit Topic -->
    <div id="TopicDetails" style="display: none; float: left; background-color: #CECECE; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Edit Topic</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Topic Title</div>
        <input type="text" id="editTopicTitle" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Topic Objective</div>
        <input type="text" id="editTopicObjective" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Url</div>
        <input type="text" id="editTopicUrl" style="width: 360px" />
        <div class="button" onclick="testEditTopicUrl();" style="float:right;margin-top:0px;margin-right:20px;width:40px;" unselectable="on">Test</div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Topic Type</div>
        <select id="editTopicType" style="width:390px;cursor:pointer">
            <option value="Concept">Concept</option>
            <option value="Fact">Fact</option>
            <option value="Principle">Principle</option>
            <option value="Procedure">Procedure</option>
            <option value="Process">Process</option>
        </select>
        <div class="button" onclick="showEditTopicTypeHelp();" style="float:right;margin-top:0px;margin-right:20px;width:20px;" unselectable="on">?</div>
        <div id="EditTopicTypeHelp" style="display:none;background-color:#AFAFAF;width:380px;margin-left:290px;padding:5px;color:#ffffff">
            <div class="buttonLite" onclick="hideEditTopicTypeHelp();" style="float:right;margin-top:0px;width:20px;" unselectable="on">[ X ]</div>
            <div class="clear">&nbsp;</div>
            <div>A <i>Concept</i> is a class of items, words, or ideas that are known by a common name. A concept-type topic describes the characterics of the concept.</div>
            <div class="clear">&nbsp;</div>
            <div>A <i>Fact</i> is a simple piece of information, usually about an object or concept. A fact-type topic should simply present a single fact in a clear way.</div>
            <div class="clear">&nbsp;</div>
            <div>A <i>Principle</i> is a guideline or rule, and the parameters that govern it. Principles should allow the student to make predictions or draw implications about a specific case.</div>
            <div class="clear">&nbsp;</div>
            <div>A <i>Procedure</i> is a series of step-by-step actions and decisions that result in the achievement of a task. Students should be able to follow a procedure and it should normally result in a predictable outcome.</div>
            <div class="clear">&nbsp;</div>
            <div>A <i>Process</i> is a flow of events or activities that describe how things work. They can be thought of as the 'big picture' of how something works, rather than being a set of instructions (which should be presented as a procedure-type instead).</div>
        </div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        
        <div class="button" onclick="deleteEditTopic();" style="margin-right: 15px;" unselectable="on">Delete</div>
        <div class="button" onclick="cancelEditTopic();" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditTopic();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Student Course List -->
    <div id="AllCoursesStudent" style="display: none; float: left; width: 190px;">
        <div id="ErrAllCoursesStudent" class="errorClass"></div>
        <div id="AllCoursesStudentHeader" class="listHeading">Courses</div>
        <div id="AllCoursesStudentList"></div>
    </div> 

    <!-- Student Course User Interface -->
    <div id="CourseUI" style="display: none; float: left; background-color: #0072C6; width: 1250px; padding: 10px">
        <div id="PageTitle" class="formTitle" style="margin-bottom:5px;width: 1024px;"></div>
        <div id="PageObjective" class="formLabelRight" style="margin-bottom:5px;width: 1024px;"></div>
        <div class="clear">&nbsp;</div>
        <div id="PageNavigation" style="float: left; width: 200px;border:solid 1px #ffffff;padding-bottom:5px">Nav</div>
        <div id="PageContent" style="float: left;width:1024px;height:800px;background-color:#FFFFFF;padding:5px;">Content</div>

        <div class="clear">&nbsp;</div>
    </div>


</asp:Content>
