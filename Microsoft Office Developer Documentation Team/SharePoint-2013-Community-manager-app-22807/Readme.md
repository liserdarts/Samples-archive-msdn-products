# SharePoint 2013: Community project manager app
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
* 2013-06-27 12:32:29
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Community project manager app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Demonstrates how to use JavaScript and jQuery in an app for SharePoint to implement a scenario for a volunteer/community initiative application.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>In this app, users sign in and then either create their own community initiative projects or contribute to someone else's. The user interface shows the status of each person's commitments and of each project's goals.</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent community initiatives and volunteers. The lists are related to each other through look-up fields, and the user interface ensures that all data operations synchronize with their list items so the relationships are maintained. The
 user interface is implemented with HTML elements and Cascading Style Sheet (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the user interface, and the solution contains no server-side code.</p>
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
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended). In this environment, you can add multiple users to the site and then treat those users as volunteers or project owners.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Using an Office 365 Developer Site is not recommended because you will probably not be able to add accounts that represent volunteers or other project owners.</p>
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
<p>The <span><strong><span class="keyword">Default.aspx</span></strong></span> webpage, which is used to present the offsite event process for the administrator and the activity/voting user interface for attendees.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.js</span></strong></span> file in the
<span><strong><span class="keyword">scripts</span></strong></span> folder, which is used to retrieve and manage offsite, attendee, and activity data by using the JavaScript implementation of the client object model (JSOM). The App.js file also contains the
 user interface logic that is implemented in Default.aspx.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.css</span></strong></span> file in the
<span><strong><span class="keyword">contents</span></strong></span> folder, which contains style definitions used by the elements in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>List definition instances for community initiatives and volunteers.</p>
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
<p>Open the <span><strong><span class="keyword">SP_CommunityInitMgr_js.sln</span></strong></span> file with Visual Studio 2012.</p>
</li><li>
<p>In the <span><strong><span class="keyword">Properties</span></strong></span> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site collection to the
<span><strong><span class="keyword">Site URL</span></strong></span> property. You may be prompted to provide credentials if you added a URL to an Office 365 site.</p>
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
<p>When the app appears, you are presented with three options, as shown in Figure 1. Use the
<strong><span class="ui">My Projects</span></strong> link to create and manage your own initiatives. Use the
<strong><span class="ui">My Roles</span></strong> link to track your volunteering and donations to other people's initiatives. Use the
<strong><span class="ui">Contribute</span></strong> link to enroll as a volunteer or donate funds to other people's initiatives.</p>
<strong>
<div class="caption">Figure 1. Start screen</div>
</strong><br>
<img src="/site/view/file/85281/1/image.png" alt=""> </li><li>
<p>When you click <strong><span class="ui">My Projects</span></strong>, you can create a new initiative or view details of any existing projects.</p>
<p>Figure 2 shows the form for creating a new project. A role creator and date pickers are provided to make it easy for you to enter relevant information. To use the role creator, type the name of a role, and then click the
<strong><span class="ui">&#43;</span></strong> (plus sign) button. Then you can enter further roles and click the
<strong><span class="ui">&#43;</span></strong> button each time.</p>
<strong>
<div class="caption">Figure 2. New project form</div>
</strong><br>
<img src="/site/view/file/85282/1/image.png" alt=""> </li><li>
<p>Figure 3 shows your projects after you have created two different initiatives.</p>
<strong>
<div class="caption">Figure 3. My Projects view</div>
</strong><br>
<img src="/site/view/file/85283/1/image.png" alt=""> </li><li>
<p>When another user signs in and clicks the <strong><span class="ui">Contribute</span></strong> link, they will see the projects that you and any other users have created. Your own projects do not appear here; instead, they appear in the
<strong><span class="ui">My Roles</span></strong> screen. When you click a project, you can see brief details as shown in Figure 4. If you decide you want to contribute, click
<strong><span class="ui">Enroll</span></strong>.</p>
<strong>
<div class="caption">Figure 4. Project enrollment screen</div>
</strong><br>
<img src="/site/view/file/85284/1/image.png" alt=""> </li><li>
<p>Figure 5 shows that when you enroll, you can select any role that has not currently been allocated, and you can specify hours of your time that you are willing to contribute. You can also record your intention to donate money if appropriate.</p>
<strong>
<div class="caption">Figure 5. Project contribution screen</div>
</strong><br>
<img src="/site/view/file/85285/1/image.png" alt=""> </li><li>
<p>When you enroll in a project, it is removed from the <strong><span class="ui">My Projects</span></strong> list.</p>
<p>The projects that you enrolled in are moved to your <strong><span class="ui">My Roles</span></strong> section. There, you can see at a glance what you have volunteered for and any donations you have promised to make, as shown in Figure 6.</p>
<strong>
<div class="caption">Figure 6. My Roles screen</div>
</strong><br>
<img src="/site/view/file/85286/1/image.png" alt=""> </li><li>
<p>The owner of a project can view details of who has enrolled, as shown in Figure 7. Until all criteria are met, the project remains open, and other volunteers can enroll.</p>
<strong>
<div class="caption">Figure 7. Project volunteers screen</div>
</strong><br>
<img src="/site/view/file/85287/1/image.png" alt=""> </li><li>
<p>Figure 8 shows that all required criteria have now been achieved. The owner can then set the project status as achieved.</p>
<strong>
<div class="caption">Figure 8. Project status screen</div>
</strong><br>
<img src="/site/view/file/85288/1/image.png" alt=""> </li><li>
<p>Later, when a user browses a project that has been achieved in their <strong><span class="ui">Contribute</span></strong> list, the user can no longer change their enrollment. They are informed that the initiative has been a success, as shown in Figure
 9.</p>
<strong>
<div class="caption">Figure 9. Volunteer view of achieved project</div>
</strong><br>
<img src="/site/view/file/85289/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Enterprise site configured to host apps.</p>
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
