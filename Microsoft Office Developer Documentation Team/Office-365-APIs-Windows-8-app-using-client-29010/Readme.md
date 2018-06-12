# Office 365 APIs: Windows 8 app using client libraries
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* SharePoint Foundation 2013
## Topics
* App
## IsPublished
* True
## ModifiedDate
* 2014-05-30 03:10:36
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Office 365 APIs: Windows 8 app using client libraries</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Client libraries make development much simpler by exposing a rich object model - complete with IntelliSense, language-integrated queries, and simpler API calls.</p>
</div>
<div>
<p><strong>Last modified: </strong>May 27, 2014</p>
<p><em><strong>Applies to: </strong>SharePoint Foundation 2013 | SharePoint Online</em></p>
<p><strong>In this article</strong> <br>
<a href="#O15Readme_Prereq">Prerequisites</a> <br>
<a href="#O15Readme_config">Configure the sample</a> <br>
<a href="#sectionSection2">Register app to consume Office 365 APIs</a> <br>
<a href="#O15Readme_build">Build the sample</a> <br>
<a href="#O15Readme_test">Run and test the sample</a> <br>
<a href="#O15Readme_Changelog">Change log</a> <br>
<a href="#O15Readme_RelatedContent">Related content</a></p>
<p>This sample demonstrate how you can use Office 365 APIs with client libraries in order to get contacts (Microsoft Azure Active Directory), documents (SharePoint), mail, and calendar (Exchange Server) in a Windows 8 application.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>In addition to being used with client libraries, Office 365 APIs can be used with standard REST calls. For more information see
<a href="http://code.msdn.microsoft.com/Office-365-APIs-Get-41eebcdf" target="_blank">
Office 365 APIs: Windows 8 app using REST calls</a></p>
</td>
</tr>
</tbody>
</table>
</div>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Important</strong> </th>
</tr>
<tr>
<td>
<p><strong>Prerelease content</strong></p>
<p>The features and APIs documented in this article are in preview and are subject to change. Do not use them in production. Your feedback about these features and APIs is important.
<a href="http://officespdev.uservoice.com/" target="_blank">Let us know</a> what you think. Have questions? Connect with us on
<a href="https://stackoverflow.com/users/login?returnurl=%2fquestions%2fask%3ftags%3dms-office%2cpreview" target="_blank">
Stack</a>. Tag your questions with [ms-office].</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Prerequisites</h2>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2013</p>
</li><li>
<p>Microsoft Azure account with Office 365 tenant.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>For more information to get Office 365 Developer site, see <a href="http://msdn.microsoft.com/library/office/fp179924(v=office.15)" target="_blank">
Sign up for an Office 365 Developer Site</a></p>
</td>
</tr>
</tbody>
</table>
</div>
</li></ul>
</div>
<h2>Configure the sample</h2>
<div id="sectionSection1">
<p>Follow these steps to configure the sample.</p>
<h3>To configure the sample</h3>
<div>
<ol>
<li>
<p>Open the Windows 8 version of the O365APIsWin8Sample.sln file using Visual Studio 2013.</p>
</li><li>
<p>Register and configure the app to consume Office 365 services.</p>
</li></ol>
</div>
</div>
<h2>Register app to consume Office 365 APIs</h2>
<div id="sectionSection2">
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
<strong></strong><img src="/site/view/file/115817/1/image.png" alt=""> </li><li>
<p>A Services Manager dialog box will appear as shown in Figure 2. Choose <strong>
<span class="ui">Office 365</span></strong> and sign in.</p>
<strong>
<div class="caption">Figure 2. Services Manager</div>
</strong><br>
<strong></strong><img src="/site/view/file/115818/1/image.png" alt=""> </li><li>
<p>On the sign-in dialog box, enter the username and password for your Office 365 tenant as shown in Figure 3. We recommend that you use your Office 365 Developer Site. Often, this user name will follow the pattern &lt;your-name&gt;@&lt;tenant-name&gt;.onmicrosoft.com.
 If you do not have a developer site, you can get a free Developer Site as part of your
<a href="http://msdn.microsoft.com/library/fp179924%28v=office.15%29.aspx" target="_blank">
MSDN Benefits</a> or sign up for a free trial. Be aware that the user must be an admin user&mdash;but for tenants created as part of an Office 365 Developer Site, this is likely to be the case already.</p>
<strong>
<div class="caption">Figure 3. Windows Azure active directory</div>
</strong><br>
<strong></strong><img src="/site/view/file/115819/1/image.png" alt=""> </li><li>
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
<div id="sectionSection3">
<p>Press <strong><span class="ui">F5</span></strong> to build and debug.</p>
</div>
<h2>Run and test the sample</h2>
<div id="sectionSection4">
<p>The final step is to run the sample and test the results.</p>
<h3>To run the sample:</h3>
<div>
<ul>
<li>
<p>Run the solution and Sign-In with your Organizational Account to Office 365.</p>
</li></ul>
</div>
</div>
<h2>Change log</h2>
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
<p>May 2014</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Related content</h2>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/d93d8591-c312-470a-b5f2-fa7a0fd57021" target="_blank">Overview of Discovery Service</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/EN-US/library/office/376090cb-f038-4b93-bddf-231403dc4a09" target="_blank">Overview of Office 365 APIs Preview</a></p>
</li><li>
<p><a href="http://visualstudiogallery.msdn.microsoft.com/7e947621-ef93-4de7-93d3-d796c43ba34f" target="_blank">Office 365 API Tools - Preview</a></p>
</li><li>
<p><a href="http://blogs.msdn.com/b/officeapps/archive/2014/03/12/announcing-office-365-api-tools-for-visual-studio-preview.aspx" target="_blank">(Blog)Announcing Office 365 API Tools for Visual Studio &mdash; Preview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/dn605894.aspx" target="_blank">How to: Integrate O365 with a web server app using Common Consent Framework</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/dn605896.aspx" target="_blank">Using the Mail, Calendar, and Contact REST APIs to work with emails, calendar items, and contacts
</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/office/aa905340" target="_blank">Office dev center - Introducing Office 365 APIs</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
