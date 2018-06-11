# SharePoint 2013:  Offsite event planner app
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
* 2013-07-31 01:46:44
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Offsite event planner app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates how to use JavaScript and jQuery in an app for SharePoint to implement a scenario for creating team-building offsite events, and enabling attendees to suggest and vote for offsite activities.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Martin%20Harwar-4025664" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>In this app, there are two types of users: event planners and event attendees. A planner creates and organizes an offsite event for a set of attendees. Attendees can then suggest activities for that event, which other attendees can vote for. Finally, the
 planner approves some or all of the activities.</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent offsite events, their attendees, and activities proposed by the attendees.</p>
<p>The lists are related to each other through look-up fields, and the user interface (UI) ensures that all data operations synchronize with their list items so the relationships are maintained. The UI is implemented with HTML elements and cascading style sheet
 (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the UI, and the solution contains no server-side code.</p>
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
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended). In this environment, you will be able to add multiple users to the site, and can then treat those users as attendees.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Using an Office 365 Developer Site is not recommended because you will probably not be able to add accounts that represent attendees.</p>
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
<p>The <span><strong><span class="keyword">Default.aspx</span></strong></span> webpage, which is used to present the offsite event process for the planner, and the activity and voting UI for attendees.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.js</span></strong></span> file in the
<span><strong><span class="keyword">scripts</span></strong></span> folder, which is used to retrieve and manage offsite, attendee, and activity data by using the JavaScript implementation of the client object model (JSOM). The
<span><strong><span class="keyword">App.js</span></strong></span> file also contains the UI logic that is implemented in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.css</span></strong></span> file in the
<span><strong><span class="keyword">contents</span></strong></span> folder, which contains style definitions used by the elements in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>List definition instances for offsite events, attendees, and activities.</p>
</li><li>
<p>All other files are automatically provided by the Visual Studio 2012 project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to configure the sample:</p>
<div>
<ol>
<li>
<p>Open the <span><strong><span class="keyword">SP_OffsiteEventPlanner_js.sln</span></strong></span> file in Visual Studio 2012.</p>
</li><li>
<p>In the <span><strong><span class="keyword">Properties</span></strong></span> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site collection to the
<span><strong><span class="keyword">Site URL</span></strong></span> property. You may be prompted to provide credentials if you add a URL to an Office 365 site.</p>
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
<p>When the app appears, it determines whether you are an event planner or attendee based on your SharePoint permissions. If your permissions include &quot;manage web,&quot; you are a planner, otherwise you are an attendee.</p>
<p>If you are a planner, the starting screen will resemble Figure 1. From here, you can create offsite events.</p>
<p>Users who are not planners will see their starting screen as described in steps 11 or 15.</p>
<strong>
<div class="caption">Figure 1. Event planner start screen</div>
</strong><br>
&nbsp;<img src="/site/view/file/93363/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Offsite</span></strong> to enter details of an offsite event that you are planning, as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. New Offsite event form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93364/1/image.png" alt=""> </li><li>
<p>Figure 3 shows the same form with data entered for an offsite event. A people picker and date pickers are provided.</p>
<strong>
<div class="caption">Figure 3. Offsite event form filled in</div>
</strong><br>
&nbsp;<img src="/site/view/file/93365/1/image.png" alt=""> </li><li>
<p>Figure 4 shows that two offsite events have been saved.</p>
<strong>
<div class="caption">Figure 4. Saved offsite events</div>
</strong><br>
&nbsp;<img src="/site/view/file/93366/1/image.png" alt=""> </li><li>
<p>Click an offsite event name to edit its data, as shown in Figure 5. For example, you can remove an attendee by clicking the
<strong><span class="ui">X</span></strong> next to the name.</p>
<strong>
<div class="caption">Figure 5. Edit Offsite form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93367/1/image.png" alt=""> </li><li>
<p>Figure 6 shows that an attendee was deleted from the <strong><span class="ui">Attendees</span></strong> list.</p>
<strong>
<div class="caption">Figure 6. Revised attendee list</div>
</strong><br>
&nbsp;<img src="/site/view/file/93368/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Attendee</span></strong> to add users. Figure 7 shows the form for adding an attendee. As with the
<strong><span class="ui">New Offsite</span></strong> form, the dialog box provides a people picker.</p>
<strong>
<div class="caption">Figure 7. Add an attendee</div>
</strong><br>
&nbsp;<img src="/site/view/file/93369/1/image.png" alt=""> </li><li>
<p>When attendees sign in, they are presented with a list of offsite events to which they are invited. For example, Figure 8 shows that the current attendee has been invited to two offsites.</p>
<strong>
<div class="caption">Figure 8. Attendee start screen</div>
</strong><br>
&nbsp;<img src="/site/view/file/93370/1/image.png" alt=""> </li><li>
<p>When an attendee clicks an offsite event name, some basic details appear, as shown in Figure 9. The attendee can also propose an activity by clicking
<strong><span class="ui">New Activity</span></strong>.</p>
<strong>
<div class="caption">Figure 9. Attendee view of an offsite event</div>
</strong><br>
&nbsp;<img src="/site/view/file/93371/1/image.png" alt=""> </li><li>
<p>Figure 10 shows the dialog box where an attendee can enter the details of a proposed activity.</p>
<strong>
<div class="caption">Figure 10. New Activity form</div>
</strong><br>
&nbsp;<img src="/site/view/file/93372/1/image.png" alt=""> </li><li>
<p>Figure 11 shows the activity has been saved and added to the <strong><span class="ui">List of Suggestions</span></strong>.</p>
<strong>
<div class="caption">Figure 11. Saved activities list</div>
</strong><br>
&nbsp;<img src="/site/view/file/93373/1/image.png" alt=""> </li><li>
<p>When a different attendee signs in, only one offsite event appears because that is all they were invited to. This attendee can then click an activity for the offsite and vote for it. Figure 12 shows the activity dialog box with a voting button.</p>
<strong>
<div class="caption">Figure 12. Vote for an activity</div>
</strong><br>
&nbsp;<img src="/site/view/file/93374/1/image.png" alt=""> </li><li>
<p>When an event planner clicks an offsite event, the page shows the activities that have been proposed and the votes that have been cast, as shown in Figure 13.</p>
<strong>
<div class="caption">Figure 13. Planner view of proposed activities</div>
</strong><br>
&nbsp;<img src="/site/view/file/93375/1/image.png" alt=""> </li><li>
<p>An event planner can click a proposed activity and decide whether or not to approve it. Figure 14 shows the dialog box for approving an activity.</p>
<strong>
<div class="caption">Figure 14. Approve an activity</div>
</strong><br>
&nbsp;<img src="/site/view/file/93376/1/image.png" alt=""> </li><li>
<p>Figure 15 shows the result of approving an activity. The activity has been moved to the
<strong><span class="ui">Approved Suggestions</span></strong> list.</p>
<strong>
<div class="caption">Figure 15. Approved activities</div>
</strong><br>
&nbsp;<img src="/site/view/file/93377/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Enterprise site configured to host apps.</p>
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
<p>July 2013</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/jj713593.aspx" target="_blank">SharePoint People Picker control</a></p>
</li><li>
<p><a href="http://www.jQuery.com" target="_blank">jQuery</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
