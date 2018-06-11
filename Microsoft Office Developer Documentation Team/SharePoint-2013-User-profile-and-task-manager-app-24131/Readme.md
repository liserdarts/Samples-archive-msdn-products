# SharePoint 2013: User profile and task manager app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-07-31 12:18:34
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: User profile and task manager app</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates using the client object model (CSOM) in SharePoint 2013 to search and retrieve user profile information and organize tasks.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample displays tasks that are due in the next week. The tasks are displayed for the current user and optionally for the user's direct reports. Organizational relationships are retrieved from the current user's profile, and tasks are retrieved using
 enterprise search.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>The sample consists of a SharePoint-hosted app using the client object model (CSOM). The
<span value="wingtip.peoplemanager.js"><b><span class="keyword">wingtip.peoplemanager.js</span></b></span> library contains the functions that retrieve profile and task information. The
<span value="wingtip.viewmodel.js"><b><span class="keyword">wingtip.viewmodel.js</span></b></span> library contains the functions to request data and bind it to the user interface.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>Before you can run the sample, you must create two new managed properties in the search service application (SSA).</p>
<ol>
<li>
<p>A new managed property named <span value="PercentComplete"><b><span class="keyword">PercentComplete</span></b></span> must be created and mapped to the following crawled properties (depending upon their availability).</p>
<ul>
<li>
<p>ows_Percent_x0020_Complete</p>
</li><li>
<p>ows_Percent_x0020_Completed</p>
</li><li>
<p>ows_PercentComplete</p>
</li><li>
<p>ows_q_NMBR_PercentComplete</p>
</li></ul>
</li><li>
<p>A new managed property named <span value="DueDate"><b><span class="keyword">DueDate</span></b></span> must be created and mapped to the following crawled property.</p>
<ul>
<li>
<p>ows_DueDate</p>
</li></ul>
</li><li>
<p>Optionally, you may want to create some tasks that are due within the next week.</p>
</li><li>
<p>Perform a full crawl.</p>
</li></ol>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <span value="DueThisWeek.sln"><b><span class="keyword">DueThisWeek.sln</span></b></span> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the deployment URL to point to a site in your development environment.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When the app appears, you will see your tasks due this week along with your direct reports.</p>
</li><li>
<p>Click the <b><span class="ui">Settings</span></b> link.</p>
</li><li>
<p>You may set the <b><span class="ui">ShowDirectReports</span></b> setting to either
<b><span class="ui">Yes</span></b> or <b><span class="ui">No</span></b>.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>The search queries will not function correctly unless you set up the appropriate managed properties.</p>
<p>Ensure that you have performed a full crawl before testing the app.</p>
<p>Ensure that you have defined some tasks to be due within the next week.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5" name="collapseableSection"><b>
<div class="caption"></div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
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
<div id="sectionSection6" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app for SharePoint</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.userprofiles.peoplemanager.aspx" target="_blank">SharePoint PeopleManager</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.search.query.aspx" target="_blank">SharePoint Search</a>
</p>
</li></ul>
</div>
</div>
</div>
