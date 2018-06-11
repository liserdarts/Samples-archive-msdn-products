# Office 365 APIs:Create a calendar session in Exchange using data from SharePoint
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* SharePoint Foundation 2013
## Topics
* Calendar
## IsPublished
* True
## ModifiedDate
* 2014-05-07 03:49:40
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Office 365 APIs:Create a calendar session in Exchange using data from SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This solution is a Windows app that reads an Office365 SharePoint list of Sessions for an event and allows users to Add, Update, and Delete session in their Exchange Calendar.</p>
</div>
<div>
<p><strong>Last modified: </strong>March 13, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#sectionSection0">Description</a> <br>
<a href="#sectionSection1">Prerequisites</a> <br>
<a href="#sectionSection2">Creating the Session List in Office 365 SharePoint online</a>
<br>
<a href="#sectionSection3">Key components of the sample</a> <br>
<a href="#sectionSection4">Configure the sample</a> <br>
<a href="#sectionSection5">Register app to consume Office 365 APIs via Visual Studio</a>
<br>
<a href="#sectionSection6">Build the sample</a> <br>
<a href="#sectionSection7">Run and test the sample</a> <br>
<a href="#sectionSection8">Change log</a> <br>
<a href="#sectionSection9">Related content</a></p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Important</strong> </th>
</tr>
<tr>
<td>
<p><strong>Prerelease content</strong></p>
<p>The features and APIs documented in this article are in preview and are subject to change. Do not use them in production.Your feedback about these features and APIs is important.
<a href="http://officespdev.uservoice.com/" target="_blank">Let us know</a> what you think. Have questions? Connect with us on
<a href="https://stackoverflow.com/users/login?returnurl=%2fquestions%2fask%3ftags%3dms-office%2cpreview" target="_blank">
Stack</a> Tag your questions with [ms-office].</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Description</h2>
<div id="sectionSection0">
<p>The app describes how an application can authenticate to and use various Office 365 APIs to build integrated experience for the user working with Office 365.</p>
</div>
<h2>Prerequisites</h2>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2013</p>
</li><li>
<p>Microsoft account</p>
</li><li>
<p>Session List in the Office 365 SharePoint Online.</p>
</li><li>
<p><strong>Optional :</strong> Registered to Windows app development.</p>
</li></ul>
</div>
<h2>Creating the Session List in Office 365 SharePoint online</h2>
<div id="sectionSection2">
<p>In Office 365SharePoint Online select <strong><span class="ui">Site Settings</span></strong> and choose a Custom List with following fields:</p>
<strong>
<div class="caption">Session List</div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Field name</p>
</th>
<th>
<p>Field Type</p>
</th>
<th>
<p>Required</p>
</th>
</tr>
<tr>
<td>
<p>Title</p>
</td>
<td>
<p>Single line of text</p>
</td>
<td>
<p>Yes</p>
</td>
</tr>
<tr>
<td>
<p>Room</p>
</td>
<td>
<p>Single line of text</p>
</td>
<td>
<p>Yes</p>
</td>
</tr>
<tr>
<td>
<p>Start</p>
</td>
<td>
<p>Date and Time</p>
</td>
<td>
<p>Yes</p>
</td>
</tr>
<tr>
<td>
<p>End</p>
</td>
<td>
<p>Date and Time</p>
</td>
<td>
<p>Yes</p>
</td>
</tr>
<tr>
<td>
<p>Code</p>
</td>
<td>
<p>Single line of text</p>
</td>
<td>
<p>Yes</p>
</td>
</tr>
<tr>
<td>
<p>Description</p>
</td>
<td>
<p>Multiple lines of text</p>
</td>
<td>
<p>Yes</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>Add some data to the session list.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Make sure that the field names are exactly like in the table above. In case field names are different in the OData response from SharePoint the application will not work.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Key components of the sample</h2>
<div id="sectionSection3">
<p>The sample app contains the following:</p>
<ul>
<li>
<p><strong>MainPage.xaml.cs</strong> a code-behind file of the main page containing the logic behind all the steps such as First Sign-In, Get Session List from SharePoint, Get/Add/Update/Delete Events from Exchange, and Get User Profile Information from Windows
 Azure Active Directory.</p>
</li><li>
<p><strong>SessionList.cs</strong> - Retrieves the sessions from the Office 365 SharePoint list.</p>
</li><li>
<p><strong>UserProfileInfo.cs</strong>- Retrieves the user profile from Windows Azure Active Directory.</p>
</li><li>
<p><strong>CalendarEvents.cs</strong>- Retrieves the calendar events from the Office 365 Exchange Calendar performs Add/Update/Delete operations on the Office 365 Exchange Calendar.</p>
</li><li>
<p><strong>Mail.cs</strong>- contains the logic to sends e-mail using Office 365 Exchange.</p>
</li></ul>
</div>
<h2>Configure the sample</h2>
<div id="sectionSection4">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the SessionPickerDemo.sln file using Visual Studio 2013.</p>
</li><li>
<p>In <strong>AppSettings.cs</strong>, update <strong>SharePointSessionListUri</strong> to point to the session list created in the earlier step.</p>
</li><li>
<p>Update <strong>SharePointHostResourceId </strong>to reflect the hostname of your Office 365 SharePoint site that contains the list.</p>
</li><li>
<p>Register the app to consume Office 365 APIs.</p>
</li></ol>
</div>
<h2>Register app to consume Office 365 APIs via Visual Studio</h2>
<div id="sectionSection5">
<p>You can do this via the Office 365 API Tools for Visual Studio (which will automate the registration process). Download Office 365 API tools from the
<a href="http://visualstudiogallery.msdn.microsoft.com/7e947621-ef93-4de7-93d3-d796c43ba34f" target="_blank">
Visual Studio Gallery</a>. Close Visual Studio 2013, then choose the .VSIX file and begin setup. A few moments later, the extension will be installed. Now, just open Visual Studio, and follow along in the walkthrough below.</p>
<ul>
<li>
<p>In the Solution Explorer, choose the project and then choose <strong><span class="ui">Add</span></strong> and choose
<strong><span class="ui">Connected Service</span></strong> as shown in Figure 1.</p>
<strong>
<div class="caption">Figure 1. Add Connected Service</div>
</strong><br>
<img src="/site/view/file/114209/1/image.png" alt=""> </li><li>
<p>A Services Manager dialog box will appear as shown in Figure 2. Choose Office 365 and Sign in.</p>
<strong>
<div class="caption">Figure 2. Services Manager</div>
</strong><br>
<img src="/site/view/file/114210/1/image.png" alt=""> </li><li>
<p>On the sign-in dialog box, enter the username and password for your Office 365 tenant as shown in Figure 3. We recommend that you use your Office 365 Developer Site. Often, this user name will follow the pattern &lt;your-name&gt;@&lt;tenant-name&gt;.onmicrosoft.com.
 If you do not have a developer site, you can get a free Developer Site as part of your
<a href="http://msdn.microsoft.com/library/fp179924%28v=office.15%29.aspx" target="_blank">
MSDN Benefits</a> or sign up for a free trial. Be aware that the user must be an admin user&mdash;but for tenants created as part of an off365devsitelong, this is likely to be the case already.</p>
<strong>
<div class="caption">Figure 3. Windows Azure Active Directory</div>
</strong><br>
<img src="/site/view/file/114208/1/image.png" alt=""> </li><li>
<p>After you're signed in, you will see a list of all the services. Initially, no permissions will be selected &mdash;as the app is not registered to consume any services yet.</p>
<p>&nbsp;</p>
</li><li>
<p>Choose the following permissions for this sample -</p>
<ul>
<li>
<p>Windows Azure Active Directory Graph permissions - User Profile permission</p>
</li><li>
<p>Exchange Server permissions - Contacts, Calendar, and Mail permissions</p>
</li><li>
<p>SharePoint permissions - use AllSites, and ONLY &quot;Read items in all site collections&quot; or &quot;Edit or delete items in all site collections&quot; permissions.</p>
</li></ul>
</li></ul>
</div>
<h2>Build the sample</h2>
<div id="sectionSection6">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h2>Run and test the sample</h2>
<div id="sectionSection7">
<p>Run the solution and Sign-In with your Organizational Account to Office 365.</p>
<p>Add Sessions to the Office 365 Exchange Calendar of the Organization Account that is signed into the applications by either selecting &quot;Add-to or Update Calendar&quot; or simply dragging sessions from the Session list onto the Calendar event list. Check the Office
 365 Exchange Calendar for the added sessions. Check the Office 365 Exchange Calendar for the added sessions.</p>
<strong>
<div class="caption">Figure 1. Windows 8 app, creating Exchange session with the data from SharePoint</div>
</strong><br>
<img src="/site/view/file/114207/1/image.png" alt=""></div>
<h2>Change log</h2>
<div id="sectionSection8">
<p>Second release: May, 2014.</p>
<p>Original release: March, 2014.</p>
</div>
<h2>Related content</h2>
<div id="sectionSection9">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/dn605894.aspx" target="_blank">How to: Integrate O365 with a web server app using Common Consent Framework</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/dn605896.aspx" target="_blank">Using the Mail, Calendar, and Contact REST APIs to work with emails, calendar items, and contacts
</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/EN-US/library/office/376090cb-f038-4b93-bddf-231403dc4a09" target="_blank">Overview of Office 365 APIs Preview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/dn605894.aspx" target="_blank">How to: Integrate O365 with a web server app using Common Consent Framework</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/office/aa905340" target="_blank">Office dev center - Introducing Office 365 APIs</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
