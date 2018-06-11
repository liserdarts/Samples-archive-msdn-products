# SharePoint 2013: Online courseware designer app
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
* 2013-08-02 05:46:03
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Online courseware designer app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates how to use JavaScript and jQuery in an app for SharePoint for creating online courses with modules and topics, and enabling students to study the courses.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Martin%20Harwar-4025664" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>In this app, there are two types of users: courseware designers and students. Designers create courses that contain modules, where each module is composed of topics. Students select a course to review.</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent courses, modules, and topics within the modules.</p>
<p>The lists are related to each other through lookup fields, and the user interface (UI) ensures that all data operations synchronize with their list items so that the relationships are maintained. The UI is implemented with HTML elements and cascading style
 sheet (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the UI, and the solution contains no server-side code.</p>
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
<p>Either of the following:</p>
<ul>
<li>
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended). In this environment, you will be able to add multiple users to the site, and then treat those users as students.</p>
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
<p>The <strong><span class="keyword">Default.aspx</span></strong> webpage, which is used to present the test-design process for the administrator and the student test UI for taking tests.</p>
</li><li>
<p>The <strong><span class="keyword">App.js</span></strong> file in the <strong>
<span class="keyword">scripts</span></strong> folder, which is used to retrieve and manage test, question, answer, and result data by using the JavaScript implementation of the client object model (JSOM). The
<strong><span class="keyword">App.js</span></strong> file also contains the UI logic that is implemented in
<strong><span class="keyword">Default.aspx</span></strong>.</p>
</li><li>
<p>The <strong><span class="keyword">App.css</span></strong> file in the <strong>
<span class="keyword">contents</span></strong> folder, which contains style definitions used by the elements in
<strong><span class="keyword">Default.aspx</span></strong>.</p>
</li><li>
<p>List definition instances for training courses, modules, and topics.</p>
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
<p>Open the <strong><span class="keyword">SP_CoursewareDesigner_js.sln</span></strong> file in Visual Studio 2012.</p>
</li><li>
<p>In the <strong><span class="keyword">Properties</span></strong> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site Collection to the
<strong><span class="keyword">Site URL</span></strong> property. You may be prompted to provide credentials if you add a URL to an Office 365 site.</p>
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
<p>When the app appears, it determines whether you are a courseware designer or student, based on your SharePoint permissions. If your permissions include &quot;manage web,&quot; you are a designer, otherwise you are a student.</p>
<p>If you are a designer, the starting screen will resemble Figure 1. From here, you can create and configure training courses.</p>
<p>Users who are not designers will see their starting screen as described in step 19.</p>
<strong>
<div class="caption">Figure 1. Courseware designer start screen</div>
</strong><br>
&nbsp;<img src="/site/view/file/93588/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Course</span></strong> to enter a course name and overall objective as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. Add New Course form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93589/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Save</span></strong> to add your new course to the course list as shown in Figure 3.</p>
<strong>
<div class="caption">Figure 3. List of courses</div>
</strong><br>
&nbsp;<img src="/site/view/file/93590/1/image.png" alt=""> </li><li>
<p>Click a course name to edit its data or add modules, as shown in Figure 4.</p>
<strong>
<div class="caption">Figure 4. Edit Course form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93591/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Module</span></strong> to enter a title and objective for the module, as shown in Figure 5.</p>
<strong>
<div class="caption">Figure 5. Adding a module</div>
</strong><br>
&nbsp;<img src="/site/view/file/93592/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Save</span></strong> to add the module to the course, as shown in Figure 6.</p>
<strong>
<div class="caption">Figure 6. Module added</div>
</strong><br>
&nbsp;<img src="/site/view/file/93593/1/image.png" alt=""> </li><li>
<p>Click a module name to edit its data and add topics. Figure 7 shows the form for editing a module.</p>
<strong>
<div class="caption">Figure 7. Edit Module form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93594/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Topic</span></strong> to add a topic title, a topic objective, the source URL for the topic, and the topic's learning type. Use the
<strong><span class="ui">Test</span></strong> link to verify the URL.</p>
<p>If you are running the app in Office 365, it is likely that your app is encrypted with SSL. Therefore, for the best user experience you should add pages that can be accessed via the HTTPS protocol wherever possible, as shown by the Wikipedia link in Figure
 8.</p>
<strong>
<div class="caption">Figure 8. Adding a topic</div>
</strong><br>
&nbsp;<img src="/site/view/file/93595/1/image.png" alt=""> </li><li>
<p>Figure 9 shows the available topic types. These represent industry-standard learning types, and each has a specific meaning.</p>
<strong>
<div class="caption">Figure 9. Topic types</div>
</strong><br>
&nbsp;<img src="/site/view/file/93596/1/image.png" alt=""> </li><li>
<p>Click the <strong><span class="ui">[?]</span></strong> button to see a description of all the topic types, as shown in Figure 10.</p>
<strong>
<div class="caption">Figure 10. Topic type description</div>
</strong><br>
&nbsp;<img src="/site/view/file/93597/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Save</span></strong> to save the topic in its parent module's topic list, as shown in Figure 11.</p>
<strong>
<div class="caption">Figure 11. Topic added</div>
</strong><br>
&nbsp;<img src="/site/view/file/93598/1/image.png" alt=""> </li><li>
<p>Click a topic name to edit its data as shown in Figure 12.</p>
<strong>
<div class="caption">Figure 12. Edit Topic form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93599/1/image.png" alt=""> </li><li>
<p>To make a course available to students, click the <strong><span class="ui">Validate &amp; Publish</span></strong> button. The sample app contains validation logic that ensures your course, modules, and topics conform to some simple rules. The rules are:</p>
<ul>
<li>
<p>Each course must have at least two modules. There is no upper limit.</p>
</li><li>
<p>Each module in the course must have at least two topics. There is no upper limit.</p>
</li></ul>
<p>Figure 13 shows a course that has currently failed validation because there is only one module.</p>
<strong>
<div class="caption">Figure 13. Course validation error</div>
</strong><br>
&nbsp;<img src="/site/view/file/93600/1/image.png" alt=""> </li><li>
<p>Figure 14 shows a course that has currently failed validation because at least one module does not contain the minimum requirement of two topics.</p>
<strong>
<div class="caption">Figure 14. Module validation error</div>
</strong><br>
&nbsp;<img src="/site/view/file/93601/1/image.png" alt=""> </li><li>
<p>Figure 15 shows a course that has passed the validation tests.</p>
<strong>
<div class="caption">Figure 15. Validated course</div>
</strong><br>
&nbsp;<img src="/site/view/file/93602/1/image.png" alt=""> </li><li>
<p>If you are logged in as a user who does not have &quot;manage web&quot; permissions, you are considered to be a student and are presented with a list of courses. You cannot edit a course or create new courses.</p>
<p>When a student clicks on a course name, that course is re-validated, as described in step 16. If the course is not valid, an alert is displayed as shown in Figure 16.</p>
<strong>
<div class="caption">Figure 16. Student course validation error</div>
</strong><br>
&nbsp;<img src="/site/view/file/93603/1/image.png" alt=""> </li><li>
<p>After a course passes validation, the student can view the course as shown in Figure 17. Note that the course objective is displayed near the top of the course area, and that the modules' objectives are rolled up and presented in the main course area.</p>
<strong>
<div class="caption">Figure 17. Student view of course</div>
</strong><br>
&nbsp;<img src="/site/view/file/93604/1/image.png" alt=""> </li><li>
<p>When a student clicks a module-level link, the content area displays each of the objectives for the topics in that module, as shown in Figure 18.</p>
<strong>
<div class="caption">Figure 18. Student view of module</div>
</strong><br>
&nbsp;<img src="/site/view/file/93605/1/image.png" alt=""> </li><li>
<p>When a student clicks a topic link, that topic's URL is fetched and displayed in the main content area. Figure 19 shows the Wikipedia page whose URL was entered for the &quot;SharePoint Features&quot; topic in step 11.</p>
<strong>
<div class="caption">Figure 19. Example topic</div>
</strong><br>
&nbsp;<img src="/site/view/file/93606/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure that you have SharePoint Server 2013 properly configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Enterprise site configured to host apps.</p>
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
<p>August 2013</p>
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
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
