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
    SharePoint Test Center
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="GeneralError" class="errorClass"></div>


    <!-- Admin Test View -->
    <div id="AllTestsAdmin" style="display: none; float: left; width: 190px;">
        <div id="ErrAllTestsAdmin" class="errorClass"></div>
        <div id="AllTestsAdminHeader" class="listHeading">Tests</div>
        <div id="AddNewTest" class="clicker" onclick="addNewTest();">+ New Test</div>
        <div id="AllTestsAdminList"></div>
    </div>    

        <!-- Add New Test -->
    <div id="AddTest" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Add New Test</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Test Name</div>
        <input type="text" id="newTestName" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Level</div>
        <select id="newTestLevel" style="width:200px;cursor:pointer">
            <option value="100">100</option>
            <option value="200">200</option>
            <option value="300">300</option>
            <option value="400">400</option>
        </select>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Allow Multiple Attempts</div>
        <input type="checkbox" id="newTestAllowMultipleAttempts" style="cursor:pointer"/>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="button" onclick="cancelNewTest();" style="margin-right: 15px;" unselectable="on">Cancel</div>
        <div class="button" onclick="saveNewTest(); " unselectable="on">Save</div>
    </div>

    <!-- Edit Test -->
    <div id="TestDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle" style="margin-bottom:5px">Edit Test</div>
        <div id="showResultsButton" class="button" style="width:125px;margin-right: 20px;" onclick="showTestResults();" unselectable="on">View Results</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Test Name</div>
        <input type="text" id="editTestName" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Level</div>
        <select id="editTestLevel" style="width:200px;cursor:pointer">
            <option value="100">100</option>
            <option value="200">200</option>
            <option value="300">300</option>
            <option value="400">400</option>
        </select>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Allow Multiple Attempts</div>
        <input type="checkbox" id="editTestAllowMultipleAttempts" style="cursor:pointer"/>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="QuestionList"></div>
        <div class="clear">&nbsp;</div>
        <div id="NewQuestion" style="display:none;background-color:#AFAFAF">
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Question Text</div>
            <input type="text" id="NewQuestionText" style="width:400px" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Correct Feedback</div>
            <input type="text" id="NewQuestionCorrectFeedback" style="width:400px" value="That is correct!" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Incorrect Feedback</div>
            <input type="text" id="NewQuestionIncorrectFeedback" style="width:400px" value="That is NOT correct!" />
            <div class="clear">&nbsp;</div>
            <div id="ErrNewQuestion" class="errorClass" style="float:left;width:200px;margin-left:290px;text-align:center"></div>
            <div class="buttonLite" onclick="cancelNewQuestion();" style="margin-right: 20px;"  unselectable="on">Cancel</div>
            <div class="buttonLite" onclick="saveNewQuestion();" unselectable="on">Save</div>
            <div class="clear">&nbsp;</div>
        </div>
        <div id="AddQuestionClicker" class="formClicker" onclick="addNewQuestion();">+ New Question</div>
        <div id="EditTestQuestionList"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="deleteEditTestButton" class="button" onclick="deleteEditTest();" style="margin-right: 20px;" unselectable="on">Delete</div>
        <div id="cancelEditTestButton" class="button" onclick="cancelEditTest();" unselectable="on">Cancel</div>
        <div id="saveEditTestButton" class="button" onclick="saveEditTest();" unselectable="on">Save</div>
        <div id="validateTestButton" class="button" onclick="validateCurrentTest();" unselectable="on">Validate</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Edit Question -->
    <div id="QuestionDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Edit Question</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Question text</div>
        <input type="text" id="editQuestionText" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Correct feedback</div>
        <input type="text" id="editQuestionCorrectFeedback" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Incorrect Feedback</div>
        <input type="text" id="editQuestionIncorrectFeedback" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="AnswerList"></div>
        <div class="clear">&nbsp;</div>
        <div id="NewAnswer" style="display:none;background-color:#AFAFAF">
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Answer Text</div>
            <input type="text" id="NewAnswerText" style="width:400px" />
            <div class="clear">&nbsp;</div>
            <div class="formLabel">Is Correct?</div>
            <input type="checkbox" id="AnswerIsCorrect" style="cursor:pointer" />
            <div class="clear">&nbsp;</div>
            <div id="ErrNewAnswer" class="errorClass" style="float:left;width:200px;margin-left:290px;text-align:center"></div>
            <div class="buttonLite" onclick="cancelNewAnswer();" style="margin-right: 20px;"  unselectable="on">Cancel</div>
            <div class="buttonLite" onclick="saveNewAnswer();" unselectable="on">Save</div>
            <div class="clear">&nbsp;</div>
        </div>
        <div id="AddAnswerClicker" class="formClicker" onclick="addNewAnswer();">+ New Answer</div>
        <div id="EditQuestionAnswerList"></div>
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div id="deleteEditQuestionButton" class="button" onclick="deleteEditQuestion();" style="margin-right: 15px;" unselectable="on">Delete</div>
        <div id="cancelEditQuestionButton" class="button" onclick="cancelEditQuestion();" unselectable="on">Cancel</div>
        <div id="saveEditQuestionButton" class="button" onclick="saveEditQuestion();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Edit Answer -->
    <div id="AnswerDetails" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle">Edit Answer</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Answer text</div>
        <input type="text" id="editAnswerText" style="width: 410px" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel">Is Correct</div>
        <input type="checkbox" id="editAnswerIsCorrect" style="cursor:pointer" />
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        
        <div class="button" onclick="deleteEditAnswer();" style="margin-right: 15px;" unselectable="on">Delete</div>
        <div class="button" onclick="cancelEditAnswer();" unselectable="on">Cancel</div>
        <div class="button" onclick="saveEditAnswer();" unselectable="on">Save</div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Test Reports-->
    <div id="ScoreReports" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px;">
        <div class="clear">&nbsp;</div>
        <div class="clear">&nbsp;</div>
        <div class="formTitle" id="ScoreReportsTitle"></div>
        <div class="clear">&nbsp;</div>
        <div class="formLabel" id="TestReportList" style="width:600px"></div>
        <div class="clear">&nbsp;</div>
    </div>

    <!-- Student Test View -->
    <div id="AllTestsStudent" style="display: none; float: left; width: 190px;">
        <div id="ErrAllTestsStudent" class="errorClass"></div>
        <div id="AllTestsStudentHeader" class="listHeading">Tests</div>
        <div id="AllTestsStudentList"></div>
    </div> 

    <!-- Test User Interface -->
    <div id="TestUI" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px">
        <div id="TestName" class="formTitle" style="margin-bottom:5px;"></div>
        <div id="TestPosition" class="formLabelRight" style="margin-bottom:5px;"></div>
        <div id="TestQuestionText" class="formLabelWide"></div>
        <div class="clear">&nbsp;</div>
        <div id="TestAnswerList" class="formLabelWide" style="width:650px;"></div>
        <div class="clear">&nbsp;</div>
        <div id="EndTest" class="button" onclick="endTest(); " unselectable="on" style="display:none;">Finish</div>
        <div id="QuitTest" class="button" onclick="quitTest(); " unselectable="on">Quit</div>
        <div id="Next" class="button" onclick="showNextQuestion();" style="margin-right: 15px;" unselectable="on">Next</div>
        <div id="Previous" class="button" onclick="showPreviousQuestion(); " unselectable="on">Previous</div>
    </div>

     <!-- Test Report -->
    <div id="TestReport" style="display: none; float: left; background-color: #87CEEB; width: 730px; padding: 10px">
        <div id="TestReportTitle" class="formTitle" style="margin-bottom:5px;"></div>
        <div id="TestReportScore" class="formLabelRight" style="margin-bottom:5px;"></div>
        <div class="clear">&nbsp;</div>
        <div id="topOK" class="button" onclick="closeTestReport();" style="margin-right: 15px;" unselectable="on">OK</div>
        <div class="clear">&nbsp;</div>
        <div id="TestReportDetails" class="formLabelWide" style="width:650px;"></div>
        <div class="clear">&nbsp;</div>
        <div id="bottomOK" class="button" onclick="closeTestReport();" style="margin-right: 15px;" unselectable="on">OK</div>
        
    </div>
</asp:Content>
