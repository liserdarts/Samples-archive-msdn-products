# SharePoint 2013: Test Center education app using JSOM and JQuery
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-06-27 12:30:08
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Test Center education app using JSOM and JQuery</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Demonstrates how to use JavaScript and jQuery in an app for SharePoint to implement a scenario for creating tests and quizzes, enabling students to take the tests, and reporting on student performance in the tests.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>In this app, there are two types of users: test creators (administrators) and test takers (students).</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent quizzes, their questions, their answers, and results of students who have taken tests.</p>
<p>The lists are related to each other through look-up fields, and the user interface ensures that all data operations synchronize with their list items so that the relationships are maintained. The user interface is implemented with HTML elements and Cascading
 Style Sheet (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the user interface, and the solution contains no server-side code.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either:</p>
<ul>
<li>
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended). In this environment, you will be able to add multiple users to the site, and you can then treat those users as adminstrators or students.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Using an Office 365 Developer Site is not recommended because you will probably not be able to add accounts that represent students.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>SharePoint Server 2013 configured to host apps, and with a Developer Site Collection already created.</p>
</li></ul>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <span><strong><span class="keyword">Default.aspx</span></strong></span> webpage, which is used to present the test-design process for the administrator and the student test user interface for attempting tests.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.js</span></strong></span> file in the
<span><strong><span class="keyword">scripts</span></strong></span> folder, which is used to retrieve and manage test, question, answer, and result data by using the JavaScript implementation of the client object model (JSOM). The
<span><strong><span class="keyword">App.js</span></strong></span> file also contains the user interface logic that is implemented in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.css</span></strong></span> file in the
<span><strong><span class="keyword">contents</span></strong></span> folder, which contains style definitions used by the elements in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>List definition instances for tests, questions, answers, and results.</p>
</li><li>
<p>All other files are automatically provided by the Visual Studio 2012 project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to configure the sample.</p>
<div>
<ol>
<li>
<p>Open the <span><strong><span class="keyword">SP_TestCenter_js.sln</span></strong></span> file with Visual Studio 2012.</p>
</li><li>
<p>In the <span><strong><span class="keyword">Properties</span></strong></span> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site Collection to the
<span><strong><span class="keyword">Site URL</span></strong></span> property. You may be prompted to provide credentials if you have added a URL to an Office 365 site.</p>
</li><li>
<p>No other configuration is necessary.</p>
</li></ol>
</div>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3">
<p>&nbsp;</p>
<div>
<ol>
<li>
<p>Press Ctrl&#43;Shift&#43;B to build the solution.</p>
</li><li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 or Office 365 Enterprise site if you are prompted to do so by the browser.</p>
</li><li>
<p>When the app appears, it determines whether you are an administrator or student based on your SharePoint permissions. If your permissions include &quot;manage web&quot;, you are an administrator, otherwise you are a student.</p>
<p>If you are an administrator, the starting screen will resemble Figure 1. From here, you can start creating tests.</p>
<p>Users who are not administrators will see their starting screen as described in step 15.</p>
<strong>
<div class="caption">Figure 1. Administrator start screen</div>
</strong><br>
&nbsp;<img src="/site/view/file/85461/1/image.png" alt=""> </li><li>
<p>When you click <strong><span class="ui">New Test</span></strong>, you are presented with a form to enter a title and a level for the test, and to specify whether a student can take the test more than once. Figure 2 shows the form.</p>
<strong>
<div class="caption">Figure 2. New test form</div>
</strong><br>
&nbsp;<img src="/site/view/file/85462/1/image.png" alt=""> </li><li>
<p>When you save a new test, its name appears in the list of available tests.Click a test to edit its properties and add questions to the test, as shown in Figure 3.</p>
<strong>
<div class="caption">Figure 3. Edit test form</div>
</strong><br>
&nbsp;<img src="/site/view/file/85463/1/image.png" alt=""> </li><li>
<p>Click the <strong><span class="ui">New Question</span></strong> link to add a question to a test. The
<strong><span class="ui">Question Text</span></strong> will be the question that students see when they take the test, and the two feedback fields will be displayed in the report that users see after they have completed the test. Figure 4 shows the new question
 editing form. Figure 17 shows the student test feedback report.</p>
<strong>
<div class="caption">Figure 4. New question form</div>
</strong><br>
&nbsp;<img src="/site/view/file/85464/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Save</span></strong> to add the question to the test, as shown in Figure 5.</p>
<strong>
<div class="caption">Figure 5. New question added</div>
</strong><br>
&nbsp;<img src="/site/view/file/85465/1/image.png" alt="">
<p>Figure 6 shows a test with multiple questions.</p>
<strong>
<div class="caption">Figure 6. Test with multiple questions</div>
</strong><br>
&nbsp;<img src="/site/view/file/85466/1/image.png" alt=""> </li><li>
<p>Click a question to edit its data. You can also add answers to the question. Figure 7 shows the form for editing a question.</p>
<strong>
<div class="caption">Figure 7. Edit question form</div>
</strong><br>
&nbsp;<img src="/site/view/file/85467/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Answer</span></strong> to add an answer's text, and also to specify whether the answer is correct or not.</p>
<strong>
<div class="caption">Figure 8. Edit answer form</div>
</strong><br>
&nbsp;<img src="/site/view/file/85468/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Save</span></strong> to add the answer to the list for the specific question, as shown in Figure 9.</p>
<strong>
<div class="caption">Figure 9. New answer added</div>
</strong><br>
&nbsp;<img src="/site/view/file/85469/1/image.png" alt=""> </li><li>
<p>Before saving a test, you should validate it. The sample app contains validation logic that ensures your test, questions, and answers conform to the following rules:</p>
<ul>
<li>
<p>There must be at least one question. There is no upper limit to the number of questions.</p>
</li><li>
<p>Each question in the test must have at least two answers. There is no upper limit to the number of answers.</p>
</li><li>
<p>Each question in the test must have at least one answer that is marked as correct. A question can have multiple correct answers, including the case when all the answers are correct.</p>
</li></ul>
<p>Figure 10 shows a test that has currently failed validation because one of the questions does not have at least two answers.</p>
<strong>
<div class="caption">Figure 10. Test validation error for insufficient answers</div>
</strong><br>
&nbsp;<img src="/site/view/file/85470/1/image.png" alt=""> </li><li>
<p>Figure 11 shows a test that has currently failed validation because one of the questions does not have at least one answer that is marked as correct.</p>
<strong>
<div class="caption">Figure 11. Test validation error for missing correct answer</div>
</strong><br>
&nbsp;<img src="/site/view/file/85471/1/image.png" alt=""> </li><li>
<p>Figure 12 shows a test that has passed all validation checks. This test is now listed for students, as shown in Figure 13.</p>
<strong>
<div class="caption">Figure 12. Test validated</div>
</strong><br>
&nbsp;<img src="/site/view/file/85472/1/image.png" alt=""> </li><li>
<p>When a student logs on, the starting screen resembles Figure 13. From here, the student can take any existing test but cannot create or edit tests. An administrator can view all test results, as described in step 21.</p>
<strong>
<div class="caption">Figure 13. Student start screen</div>
</strong><br>
&nbsp;<img src="/site/view/file/85473/1/image.png" alt=""> </li><li>
<p>Before a student can start a test, the app re-runs the test validation rules described in step 12. If the test contains invalid or missing data, an alert is displayed, as shown in Figure 14.</p>
<strong>
<div class="caption">Figure 14. Test validation error for a student test</div>
</strong><br>
&nbsp;<img src="/site/view/file/85474/1/image.png" alt=""> </li><li>
<p>After the test passes validation, the student can navigate through the test questions as shown in Figure 15.</p>
<p>The app randomizes the order of questions in a test each time a student starts the test, and it also randomizes the order of the answers within each question. This prevents students from simply remembering question and answer sequences.</p>
<strong>
<div class="caption">Figure 15. Test question form</div>
</strong><br>
&nbsp;<img src="/site/view/file/85475/1/image.png" alt=""> </li><li>
<p>A student navigates through the questions in a test with the <strong><span class="ui">Next</span></strong> and
<strong><span class="ui">Previous</span></strong> buttons. They can also exit the test without calculating or storing their score by clicking the
<strong><span class="ui">Quit</span></strong> button.When a student has been through all the questions in a test, the
<strong><span class="ui">Finish</span></strong> button appears as shown in Figure 16.</p>
<strong>
<div class="caption">Figure 16. Final test question</div>
</strong><br>
&nbsp;<img src="/site/view/file/85476/1/image.png" alt=""> </li><li>
<p>When a student clicks <strong><span class="ui">Finish</span></strong>, their score is calculated and the feedback for each question is displayed, as shown in Figure 17. The app also stores each student's results for the administrator's summary, as shown
 in Figure 19.</p>
<strong>
<div class="caption">Figure 17. Finished test score</div>
</strong><br>
<img src="/site/view/file/85477/1/image.png" alt="">&nbsp; </li><li>
<p>Each test specifies whether a student can take a test only once, or repeatedly. If a student tries to re-take a test that does not allow multiple attempts, they see the message shown in Figure 18.</p>
<strong>
<div class="caption">Figure 18. Error when a test can only be taken once</div>
</strong><br>
<img src="/site/view/file/85478/1/image.png" alt="">&nbsp; </li><li>
<p>After a student finishes a test, an administrator can select a test and view the results of students who have completed it. Figure 19 shows a test that was taken by two different users, including their scores and the times when each test was completed.</p>
<strong>
<div class="caption">Figure 19. Administrator view of test scores</div>
</strong><br>
&nbsp;<img src="/site/view/file/85479/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure that you have SharePoint Server 2013 properly configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Enterprise site configured to host apps.</p>
<p>&nbsp;</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5"><strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>June 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://www.jQuery.com" target="_blank">jQuery</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
