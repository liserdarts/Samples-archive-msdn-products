# SharePoint 2013:  Event manager app using jQuery and JSOM
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
* 2013-08-29 04:13:30
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Event manager app using jQuery and JSOM</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Demonstrates how to use JavaScript and jQuery in an app for SharePoint to implement a scenario for creating and configuring training rooms, and creating training events with attendee management features.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>In this app, there are two types of users: organizers and attendees. An organizer creates a training room as an
<strong><span class="ui">asset</span></strong>, with data such as the class size and the number of available computers and projectors. An organizer then creates training events for a particular asset and timeframe, and invites attendees. An attendee has the
 choice to enroll in an offered event, and to list all the events they are enrolled in.</p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The lists included
 in this solution represent training resources (such as rooms, projectors, student PCs, and instructor PCs), training events that use training rooms, and training event attendees.</p>
<p>The lists are related to each other through look-up fields, and the user interface ensures that all data operations synchronize with their list items so the relationships are maintained. The user interface is implemented with HTML elements and cascading
 style sheet (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the user interface, and the solution contains no server-side code.</p>
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
<p>The <span><strong><span class="keyword">Default.aspx</span></strong></span> webpage, which is used to present the training event creation process for the administrator and the event enrollment interface for attendees.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.js</span></strong></span> file in the
<span><strong><span class="keyword">scripts</span></strong></span> folder, which is used to retrieve and manage event, room, and attendee data by using the JavaScript implementation of the client object model (JSOM). The
<span><strong><span class="keyword">App.js</span></strong></span> file also contains the user interface logic that is implemented in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>The <span><strong><span class="keyword">App.css</span></strong></span> file in the
<span><strong><span class="keyword">contents</span></strong></span> folder, which contains style definitions used by the elements in
<span><strong><span class="keyword">Default.aspx</span></strong></span>.</p>
</li><li>
<p>List definition instances for training events, attendees, and training rooms and resources.</p>
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
<p>Open the <span><strong><span class="keyword">SP_TrainingEventManager_js.sln</span></strong></span> file in Visual Studio 2012.</p>
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
<p>When the app appears, it determines whether you are an event organizer or attendee based on your SharePoint permissions. If your permissions include &quot;manage web,&quot; you are an organizer, otherwise you are an attendee.</p>
<p>If you are an organizer, the starting screen will resemble Figure 1. From here, you can create training events and configure training resources such as rooms, projectors, and PCs.</p>
<p>Users who are not organizers will see their starting screen as described in step 18.</p>
<strong>
<div class="caption">Figure 1. Organizer start screen</div>
</strong><br>
<img src="/site/view/file/86883/1/image.png" alt=""> </li><li>
<p>The first step is to add training assets. Click <strong><span class="ui">Training Assets</span></strong> to see any existing assets. To create a new asset/room configuration, click
<strong><span class="ui">Add Assets</span></strong>, as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. Training Assets form</div>
</strong><br>
<img src="/site/view/file/86884/1/image.png" alt=""> </li><li>
<p>Figure 3 shows how to add a room configuration as an asset.</p>
<strong>
<div class="caption">Figure 3. Add new assets form</div>
</strong><br>
<img src="/site/view/file/86885/1/image.png" alt="">
<p>Figure 4 shows the result of adding two training room assets.</p>
<strong>
<div class="caption">Figure 4. Training room assets added</div>
</strong><br>
<img src="/site/view/file/86886/1/image.png" alt=""> </li><li>
<p>Click a name in the <strong><span class="ui">Assets</span></strong> list to edit a room configuration, as shown in Figure 5.</p>
<strong>
<div class="caption">Figure 5. Edit asset form</div>
</strong><br>
<img src="/site/view/file/86887/1/image.png" alt=""> </li><li>
<p>Now that assets have been created, you can create training events that use them. Figure 6 shows the
<strong><span class="ui">Events</span></strong> list before any events have been scheduled.</p>
<strong>
<div class="caption">Figure 6. Initial Events screen</div>
</strong><br>
<img src="/site/view/file/86888/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">New Event</span></strong> to enter details such as the event title, start date and time, and end date and time, as shown in Figure 7.</p>
<strong>
<div class="caption">Figure 7. Add new event form</div>
</strong><br>
<img src="/site/view/file/86889/1/image.png" alt=""> </li><li>
<p>You can then use the same dialog box to assign resources for the event. Click <strong>
<span class="ui">Assign resource</span></strong> to choose an event location. Locations correspond to the training rooms that you already set up in the
<strong><span class="ui">Training Assets</span></strong> section.</p>
<strong>
<div class="caption">Figure 8. Assign resource form</div>
</strong><br>
<img src="/site/view/file/86890/1/image.png" alt=""> </li><li>
<p>After you choose a location asset from the drop-down list, you can then click <strong>
<span class="ui">Fill Data</span></strong> to fill in the asset's information. Figure 9 shows how the assets for training room TR #2 have been assigned to the event.</p>
<p>This form also includes a people picker that you can use to invite attendees to the event.</p>
<strong>
<div class="caption">Figure 9. Resource data filled in</div>
</strong><br>
<img src="/site/view/file/86891/1/image.png" alt="">
<p>Figure 10 shows the newly saved event.</p>
<strong>
<div class="caption">Figure 10. Saved events list</div>
</strong><br>
<img src="/site/view/file/86892/1/image.png" alt=""> </li><li>
<p>Click an event name to manage attendees. Figure 11 shows how you could remove the existing attendee by clicking the
<strong><span class="ui">x</span></strong> next to the name, and how you could invite more attendees by clicking
<strong><span class="ui">New Attendees</span></strong>.</p>
<strong>
<div class="caption">Figure 11. Edit event attendees form</div>
</strong><br>
<img src="/site/view/file/86893/1/image.png" alt=""> </li><li>
<p>Figure 12 shows that the previous attendee has been removed, and two new attendees have been added.</p>
<strong>
<div class="caption">Figure 12. Revised attendee list</div>
</strong><br>
<img src="/site/view/file/86894/1/image.png" alt=""> </li><li>
<p>Cick the <strong><span class="ui">Edit Event</span></strong> link to change other event details, such as its location or timing. Figure 13 shows the editing form that appears. For example, you could move the event to a different location, such as TR #1.</p>
<strong>
<div class="caption">Figure 13. Edit event details form</div>
</strong><br>
<img src="/site/view/file/86895/1/image.png" alt=""> </li><li>
<p>If you do choose a new location, update its information by clicking <strong><span class="ui">Fill Data</span></strong>. Figure 14 shows the updated asset details.</p>
<strong>
<div class="caption">Figure 14. Revised location data filled in</div>
</strong><br>
<img src="/site/view/file/86896/1/image.png" alt=""> </li><li>
<p>Figure 15 shows that an additional event has been added.</p>
<p>Note that when you schedule a new event that overlaps with an existing one, you will not be able to choose any locations that are already in use.</p>
<strong>
<div class="caption">Figure 15. Revised event list</div>
</strong><br>
<img src="/site/view/file/86897/1/image.png" alt=""> </li><li>
<p>When a user who is not an event organizer signs in, they see the user interface shown in Figure 16.</p>
<strong>
<div class="caption">Figure 16. Attendee start screen</div>
</strong><br>
<img src="/site/view/file/86898/1/image.png" alt=""> </li><li>
<p>When an attendee clicks <strong><span class="ui">Enrolled Events</span></strong>, any training sessions that they have currently been invited to are shown, as in Figure 17.</p>
<strong>
<div class="caption">Figure 17. Attendee event invitation list</div>
</strong><br>
<img src="/site/view/file/86899/1/image.png" alt=""> </li><li>
<p>An attendee can click an event name to see its details, as shown in Figure 18.</p>
<strong>
<div class="caption">Figure 18. Attendee view of event details</div>
</strong><br>
<img src="/site/view/file/86900/1/image.png" alt=""> </li><li>
<p>If an attendee clicks <strong><span class="ui">All Events</span></strong>, they will see the invited events for which they are not currently enrolled. To attend an event, an attendee clicks the
<strong><span class="ui">Enroll</span></strong> button shown in Figure 19.</p>
<strong>
<div class="caption">Figure 19. Event enrollment form</div>
</strong><br>
<img src="/site/view/file/86901/1/image.png" alt=""> </li><li>
<p>The enrolled event is moved from the <strong><span class="ui">All Events</span></strong> view to the
<strong><span class="ui">Enrolled Events</span></strong> list, as shown in Figure 20.</p>
<strong>
<div class="caption">Figure 20. Enrolled events list</div>
</strong><br>
<img src="/site/view/file/86902/1/image.png" alt=""> </li></ol>
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
