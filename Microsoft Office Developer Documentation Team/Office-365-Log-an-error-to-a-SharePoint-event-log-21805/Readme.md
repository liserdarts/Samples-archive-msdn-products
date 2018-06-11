# Office 365: Log an error to a SharePoint event log and notify the account admin
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* SharePoint Server 2013
## Topics
* Error Handling
## IsPublished
* True
## ModifiedDate
* 2013-04-23 10:45:32
## Description

<div id="header"><span class="label">Summary:</span> This sample demonstrates how to log an error to a SharePoint custom list and how to send an email message notifying the Office 365 administrator about the error.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>This sample is a SharePoint-hosted app using the JavaScript object model (JSOM). It shows how to create a custom list, how to update its description, how to log an error to it, and how to send an email message to the Office 365 administrator about the error.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>The solution in this sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012.</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012 (use the <a href="http://www.microsoft.com/web/downloads/platform.aspx/" target="_blank">
Microsoft Web Platform Installer</a>).</p>
</li><li>
<p>An Office 365 Developer Site. You can sign up <a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx/" target="_blank">
here</a>.</p>
</li><li>
<p>SharePoint Designer 2013.</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>This sample contains the following key files:</p>
<ul>
<li>
<p>The O365_errorhandlingjavascript_cs project.</p>
</li><li>
<p>App.js, a JavaScript file that contains the app logic using the client-side object model.</p>
</li><li>
<p>AppManifest.xml, which contains the permission that is required by the app to run successfully.</p>
</li><li>
<p>Default.aspx, which contains the HTML and ASP.NET controls for the user interface.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to first create a custom list and a custom workflow, and then configure the O365_errorhandlingjavascript_cs SharePoint app.</p>
<p><strong>Create a custom list</strong></p>
<ol>
<li>
<p>Navigate to your Office 365 Developer Site.</p>
</li><li>
<p>Click <span class="ui">Site Contents</span>, and then click <span class="ui">
add an app</span>.</p>
</li><li>
<p>Click <span class="ui">Custom List</span>. A dialog box opens, as shown in the following figure.</p>
<p><img id="81054" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81054/1/o365customlistaddcustomlist.jpg" alt="Adding a custom list" width="639" height="212"></p>
<p>&nbsp;</p>
</li><li>
<p>In the <span class="ui">Name</span> box, type &quot;ErrorLog&quot; and click <span class="ui">
Create</span>. The ErrorLog list will be displayed in <span class="ui">Site Contents</span>.</p>
</li><li>
<p>Click the ErrorLog list to select it.</p>
</li><li>
<p>On the <span class="ui">List</span> tab, in the <span class="ui">Settings</span> group, click
<span class="ui">List Settings</span>.</p>
</li><li>
<p>Click <span class="ui">Create column</span>.</p>
</li><li>
<p>In the <span class="ui">Column name</span> field, type &quot;ErrorMessage&quot;, select
<span class="ui">Multiple lines of text</span> as the type of information, and then click
<span class="ui">OK</span>.</p>
</li><li>
<p>Similarly, create another column named &quot;Source&quot;, select <span class="ui">Single line of text</span> as the type of information, and then click
<span class="ui">OK</span>.</p>
</li><li>
<p>Set the <span class="ui">Title</span> field as not mandatory by clicking the
<span class="ui">Title</span> column.</p>
</li></ol>
<p><strong>Create a custom workflow</strong></p>
<p>Create a custom workflow in SharePoint Designer 2013 to send an email message to the Office 365 administrator when an error is logged in the ErrorLog list.</p>
<ol>
<li>
<p>In SharePoint Designer, open your Office 365 Developer Site.</p>
</li><li>
<p>In the <span class="ui">Site Objects</span> list, click <span class="ui">Workflows</span>.</p>
</li><li>
<p>On the <span class="ui">WORKFLOWS</span> tab, click <span class="ui">List Workflow</span>, and then in the
<span class="ui">Lists</span> list click <span class="ui">ErrorLog</span>.</p>
</li><li>
<p>A dialog box prompts you for the workflow <span class="ui">Name</span> and <span class="ui">
Description</span>, as shown in the following figure. Enter the name and description, and click
<span class="ui">OK</span>.</p>
<p><img id="81055" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81055/1/o365customlistcreatelistworkflow.jpg" alt="Create the list workflow for a custom list" width="539" height="362"></p>
<p>&nbsp;</p>
</li><li>
<p>The <span class="ui">Stage 1</span> window opens, as shown in the following figure.</p>
<p><img id="81056" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81056/1/o365customlist_stage1.jpg" alt="O365: adding Stage 1 information" width="556" height="130"></p>
<p>&nbsp;</p>
</li><li>
<p>Double-click the orange line shown in the previous figure.</p>
</li><li>
<p>In the text box that opens, type &quot;Email&quot;, press Enter, and then click <span class="ui">
Email these users</span>. The following window opens, where you configure the email message.</p>
<p><img id="81057" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81057/1/o365customlist_defineemail.jpg" alt="O365: Defining the parts of an email message" width="616" height="508"></p>
<p>&nbsp;</p>
<ol>
<li>
<p>Enter an address in the <span class="ui">To</span> line by clicking the &quot;open book&quot; button on the right side of this line. In the left list, select
<span class="ui">Administrator</span>, and then click <span class="ui">Add&gt;&gt;</span>.</p>
</li><li>
<p>Enter text for the <span class="ui">Subject</span> line as follows:</p>
<ol>
<li>
<p>On the right side of the <span class="ui">Subject</span> line, click the ellipsis button (<span class="ui">&hellip;</span>).</p>
</li><li>
<p>In the <span class="ui">String Builder</span> dialog box, type &quot;Microsoft Office 365 error in &quot;.</p>
</li><li>
<p>At the bottom of the dialog box, click <span class="ui">Add or Change Lookup</span>.</p>
</li><li>
<p>In the <span class="ui">Lookup for String</span> dialog box, in the <span class="ui">
Field from source</span> list, click <span class="ui">Source</span>, and then click
<span class="ui">OK</span>.</p>
</li></ol>
</li><li>
<p>To enter text for the body of the email message, type the message text as shown in the previous figure. However, any text of the form &quot;[%Current Item:&hellip;%]&quot; is inserted by placing the cursor where &quot;[%Current Item:&hellip;%]&quot; should go and then clicking
 the <span class="ui">Add or Change Lookup</span> button. Add the <span class="ui">
User</span>, <span class="ui">Source App</span>, and <span class="ui">Error Message</span> information as necessary:</p>
<ul>
<li>
<p><strong>User information</strong>. In the <span class="ui">Field from source</span> list, click
<span class="ui">Created By</span>, and in the <span class="ui">Return field as</span> list, click
<span class="ui">Display Name</span>. Click <span class="ui">OK</span>.</p>
</li><li>
<p><strong>Source App information</strong>. In the <span class="ui">Field from source</span> list, click
<span class="ui">Source</span>, and then click <span class="ui">OK</span>.</p>
</li><li>
<p><strong>Error Message information</strong>. In the <span class="ui">Field from source</span> list, click
<span class="ui">ErrorMessage</span>, and then click <span class="ui">OK</span>.</p>
</li></ul>
</li></ol>
</li><li>
<p>Add a new step in the workflow for logging a message to the history list:</p>
<ol>
<li>
<p>In the upper <span class="ui">Stage: Stage 1</span> section, point to the area under the &quot;Email&hellip;&quot; line to see the orange line for inserting another step in the workflow.</p>
</li><li>
<p>Double-click the line. In the text box, type &quot;Log to history list&quot; and press Enter.</p>
</li><li>
<p>Click the <span class="ui">message</span> link to open a text box. Type &quot;Send mail successful&quot;, and then press Enter.</p>
</li></ol>
</li><li>
<p>Add a step in the workflow for the transition stage:</p>
<ol>
<li>
<p>Point to the bottom of the <span class="ui">Transition to stage</span> section to see the orange line, and then double-click the line.</p>
</li><li>
<p>In the text box, type &quot;Go to a stage&quot;, and then press Enter.</p>
</li><li>
<p>Click the <span class="ui">a stage</span> link. In the drop-down list that opens, click
<span class="ui">End of Workflow</span>.</p>
</li><li>
<p>On the <span class="ui">WORKFLOW</span> tab, click <span class="ui">Save</span>. The ErrorLog workflow name is displayed in the
<span class="ui">Workflows</span> list under <span class="ui">List Workflow</span>.</p>
<p>&nbsp;</p>
<p>A screen shot of the complete workflow is provided in the following figure.</p>
<p><img id="81058" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81058/1/o365customlist_completedworkflow.jpg" alt="O365: Showing the completed workflow" width="619" height="247"></p>
<p>&nbsp;</p>
</li></ol>
</li><li>
<p>Click the workflow name in the <span class="ui">Navigation</span> list, and then, in the
<span class="ui">Start Options</span> section, select the <span class="ui">Start Workflow automatically when an item is created</span> check box.</p>
<p><img id="81059" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81059/1/o365customlist_workflowstartoptions.jpg" alt="O365: Set the workflow start options" width="603" height="298"></p>
<p>&nbsp;</p>
</li><li>
<p>On the <span class="ui">WORKFLOW_SETTINGS</span> tab, click <span class="ui">
Save</span>, and then click <span class="ui">Publish</span>.</p>
</li></ol>
<p><strong>Configure the O365_errortypes_cs SharePoint app</strong></p>
<ol>
<li>
<p>Extract the files from O365_errorhandlingjavascript_cs.zip into a folder.</p>
</li><li>
<p>Open Visual Studio 2012 with administrator privileges.</p>
</li><li>
<p>On the <span class="ui">File</span> menu, click <span class="ui">Open</span>, and then click
<span class="ui">Project</span>.</p>
</li><li>
<p>Navigate to the location of the O365_errorhandlingjavascript_cs solution folder and select the O365_errorhandlingjavascript_cs.sln file.</p>
</li><li>
<p>Set the <span class="ui">Site URL</span> property of the solution to the URL of your SharePoint site collection.</p>
</li><li>
<p>Press Ctrl&#43;S to save all the changes.</p>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Follow these steps to build and deploy the app.</p>
<ol>
<li>
<p>In Visual Studio, right-click the O365_errorhandlingjavascript_cs file, and then click
<span class="ui">Build</span>.</p>
</li><li>
<p>Right-click the solution, and then click <span class="ui">Publish</span>. The following window opens.</p>
<p><img id="81060" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81060/1/o365customlist_publishsummary.jpg" alt="Publish summary dialog" width="473" height="365"></p>
<p>&nbsp;</p>
</li><li>
<p>Select the <span class="ui">Open output folder after successful packaging</span> check box, and then click
<span class="ui">Finish</span>.</p>
</li><li>
<p>This will create the .app extension file, which will be used to deploy the app on the SharePoint site; note the location of this file.</p>
</li><li>
<p>Using a browser, navigate to your Office 365 Developer Site.</p>
</li><li>
<p>Click <span class="ui">Apps in testing</span>. The following window opens.</p>
<p><img id="81061" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81061/1/o365_errortypes-appsintesting.jpg" alt="O365_errortypes app: Apps in Testing screenshot" width="501" height="163"></p>
<p>&nbsp;</p>
</li><li>
<p>Click <span class="ui">new app to deploy</span>.</p>
</li><li>
<p>A dialog box opens, as shown in the following figure.</p>
<p><img id="81062" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81062/1/o365customlist_deployupload.jpg" alt="Dialog to upload and deploy app" width="608" height="405"></p>
<p>&nbsp;</p>
</li><li>
<p>Click the <span class="ui">upload</span> link to upload your .app package file.</p>
</li><li>
<p>Browse to the file, select it, and then click <span class="ui">OK</span>.</p>
</li><li>
<p>Click the <span class="ui">Deploy</span> button.</p>
</li><li>
<p>Another dialog box opens, as shown in the following figure.</p>
<p><img id="81063" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81063/1/o365customlist_trustitdialog.jpg" alt="O365: Trust it dialog for any app" width="588" height="295"></p>
<p>&nbsp;</p>
</li><li>
<p>Click the <span class="ui">Trust It</span> button. The app will be uploaded to your Developer Site.</p>
</li></ol>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Follow these steps to run and test the O365_errorhandlingjavascript_cs sample app.</p>
<ol>
<li>
<p>On your Developer Site, click <span class="ui">Site Contents</span>, and then click the app name
<span class="ui">List Description Update App</span>, which opens a window as follows.</p>
<p><img id="81064" src="http://i1.code.msdn.s-msft.com/office-365-log-an-error-to-23a978ac/image/file/81064/1/o365customlist_runningapp.jpg" alt="O365: How the app looks when it runs" width="538" height="310"></p>
<p>&nbsp;</p>
</li><li>
<p>Enter the list name and an updated description for the list. Click <span class="ui">
Update</span> to update the list description.</p>
<ul>
<li>
<p>If you use the list name created earlier (ErrorLog), a message will say, &quot;List description updated successfully.&quot; No error will be logged to the ErrorLog list and no email will be sent.</p>
</li><li>
<p>If a nonexistent list name is used or if there is any exception while updating the description of the list, a message will say &quot;Failed to update list description. Please check ErrorLog list.&quot; An error will be logged to the ErrorLog list and an email message
 will be sent to the administrator.</p>
</li><li>
<p>To see the ErrorLog list, on your Office 365 Developer Site, click <span class="ui">
Site Contents</span>, and then click <span class="ui">ErrorLog</span>.</p>
</li></ul>
</li><li>
<p>The app recovers from any error so you can continue the test with other list names.</p>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how you can solve them.</p>
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Problem</p>
</th>
<th>
<p>Solution</p>
</th>
</tr>
<tr>
<td>
<p>In Visual Studio 2012, when prompted to &quot;Connect to SharePoint&quot;, an error message says, &quot;Access Denied&hellip; You are not a member of this site.&quot;</p>
</td>
<td>
<p>The credentials are not correct for the URL specified in the <span class="ui">
Site URL</span> property of the app. Correct the logon credentials. If the problem persists, try deleting the browsing history and logging on again.</p>
</td>
</tr>
<tr>
<td>
<p>You do not see the &quot;Publish Summary&quot; screen when you click <span class="ui">
Publish</span>.</p>
</td>
<td>
<p>Check to be sure you created your Office 365 subscription account and have configured your account's developer URL in the sample solution, as described in the steps for &quot;Configure the sample&quot;.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<div class="caption"></div>
<div class="tableSection">
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
<p>April 23, 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/e3000415-50a0-426e-b304-b7de18f2f7d9.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 REST endpoints</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Get started developing apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></p>
</li></ul>
</div>
</div>
</div>
