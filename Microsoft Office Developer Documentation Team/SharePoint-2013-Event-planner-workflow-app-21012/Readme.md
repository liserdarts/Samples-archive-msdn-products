# SharePoint 2013: Event planner workflow app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Workflows
## IsPublished
* True
## ModifiedDate
* 2013-03-08 04:24:00
## Description

<p id="header">Demonstrates some of the tasks that need to be performed in a real-world workflow&mdash;including deploying the workflow in an app; reading, updating, and creating SharePoint list items; waiting for events to occur; using the new
<span><span class="keyword">DynamicValue</span></span> data type; sending email messages; and working with people fields.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=66531e5c-6ee1-4a68-87b5-c3b2f93db465" target="_blank">Andrew Connell</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>This sample implements an event presentation submission process. A user creates a new event proposal that includes an event planner, speaker, and attendee. When the item is created, the workflow starts and collects information from the submission. It then
 creates a task assigned to the event planner to review the event suggestion. After the event is approved, the workflow creates an item in the Calendar list and a task is assigned to the presenter to upload a slide deck. Finally, once the event concludes, the
 attendee of the presentation is notified via email to submit a survey grading the event.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint 2013 development environment</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Workflow Manager 1.0 installed, configured, and connected to the SharePoint development environment</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<div><strong>EventPlannerApp.sln</strong> is the Visual Studio solution that contains the entire SharePoint-hosted app and workflow.</div>
</li><li>
<div>The <strong>Event Planner List</strong> folder in the Visual Studio solution contains multiple project items for columns and the primary list that triggers the workflow.</div>
</li><li>
<div>The <strong>Event Resources</strong> folder contains site columns, content types, and lists that are used by the workflow.</div>
</li><li>
<div>The <strong>EventWorkflows</strong> folder contains the workflow (<strong>EvaluateEventWorkflow</strong>), the workflow-specific lists, and a site column for a content type that is used for a custom task outcome.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample app:</div>
<div class="subSection">
<ol>
<li>
<div>Ensure you have a site collection created using the Developer Site template.</div>
</li><li>
<div>Open the solution in Visual Studio 2012.</div>
</li><li>
<div>Change the <span><span class="keyword">Site URL</span></span> property on the project to be the URL of the Developer Site.</div>
</li><li>
<div>Ensure you have three people in Active Directory (Ken Sanchez, David Galvin, and Rob Walters).</div>
</li><li>
<div>Run a full synchronization of the User Profile application.</div>
</li><li>
<div>Grant all three users access to the Developer Site.</div>
</li></ol>
</div>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Build EventPlannerApp.sln.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<div class="subSection">
<ol>
<li>
<div>Open the EventPlannerApp.sln and press F5 (ensuring it has been configured to point to the Developer Site).</div>
</li><li>
<div>When the app deploys, it opens the homepage that contains links to three lists.</div>
</li><li>
<div>Click the <span class="ui">Event Suggestion List</span> link, add a new event filling in all fields, and click
<span class="ui">Save</span>.</div>
</li><li>
<div>Saving the item starts the workflow. Navigate to the workflow status page for this item. You should see a new task created.</div>
</li><li>
<div>Click the task, and then click <span class="ui">Approve</span> to approve the suggestion.</div>
</li><li>
<div>After you approve the suggestion, another task is assigned to the presenter to upload the presentation.</div>
</li><li>
<div>Go back to the workflow status page to navigate to the new task, and click <span class="ui">
Complete</span>, which demonstrates a custom task outcome.</div>
</li><li>
<div>Finally, go back to the home page of the app to navigate to the calendar list and find the item that was created by the workflow. Click the item and check the field at the end of the item to indicate the event has concluded. This triggers an email message
 to be sent to the attendee of the event to submit a survey</div>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the workflow fails, it is likely due to one of two things: either the users have not been imported into the User Profile Application, or the environment has not been configured to send email messages. The latter will report errors, but it will not keep
 the workflow from running.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj164084(v=office.15).aspx" target="_blank">SharePoint 2013 development overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/ee554869.aspx" target="_blank">Start: Set up the development environment for SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163986.aspx" target="_blank">Workflows in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163276.aspx" target="_blank">Start: Set up and configure SharePoint 2013 Workflow Manager</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj193528(v=azure.10).aspx" target="_blank">Workflow Manager 1.0</a></div>
</li></ul>
</div>
</div>
</div>
</div>
