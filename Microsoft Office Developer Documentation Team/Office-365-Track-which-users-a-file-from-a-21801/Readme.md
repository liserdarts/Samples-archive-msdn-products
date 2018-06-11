# Office 365: Track which users download a file from a SharePoint 2013 site
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
## Topics
* Data Access
## IsPublished
* True
## ModifiedDate
* 2013-04-23 12:01:18
## Description

<div id="header"><span class="label">Summary:</span> This code sample shows which user downloaded a file from a SharePoint 2013 site.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>This is a SharePoint-hosted app for SharePoint 2013. The app displays the name of the file that is downloaded and the name of the user that downloaded it.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Windows 8 and Internet Explorer 10</p>
</li><li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012 (use the <a href="http://www.microsoft.com/web/downloads/platform.aspx/" target="_blank">
Microsoft Web Platform Installer</a>)</p>
</li><li>
<p>An Office 365 Developer Site (<a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx/" target="_blank">Sign up for an Office 365 Developer Site</a>)</p>
</li><li>
<p><a href="http://www.microsoft.com/en-us/download/details.aspx?id=30355" target="_blank">SharePoint Server 2013 Client Components SDK Preview</a></p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>This sample contains the following key files:</p>
<ul>
<li>
<p>The UpdateListCA project.</p>
</li><li>
<p>App.js, a JavaScript file that contains the app logic.</p>
</li><li>
<p>AppManifest.xml, which contains the permission that is required by the app to run successfully.</p>
</li><li>
<p>Default.aspx, which contains the HTML and ASP.NET controls for the user interface.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>&nbsp;</p>
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Extract the files from UpdateListCA.zip into a folder.</p>
</li><li>
<p>Open Visual Studio 2012 with administrator privileges.</p>
</li><li>
<p>On the <span class="ui">File</span> menu, click <span class="ui">Open</span>, and then click
<span class="ui">Project</span>.</p>
</li><li>
<p>Navigate to the location of the UpdateListCA solution folder and select the UpdateListCA.sln file.</p>
</li><li>
<p>Set the <span class="ui">Site URL</span> property of the UpdateListCA solution to the URL of your SharePoint site collection.</p>
</li><li>
<p>Press Ctrl&#43;S to save all the changes.</p>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Follow these steps to build the sample.</p>
<ol>
<li>
<p>Right-click the UpdateListCA solution folder and click <span class="ui">Publish</span>. The following window opens.</p>
<p><img id="81065" src="http://i1.code.msdn.s-msft.com/office-365-track-which-f4d06334/image/file/81065/1/o365customlist_publishsummary.jpg" alt="Publish summary dialog" width="473" height="365"></p>
<p>&nbsp;</p>
</li><li>
<p>Select the <span class="ui">Open output folder after successful packaging</span> check box, and then click
<span class="ui">Finish</span>. This will create the .app extension file, which will be used to deploy the app on the SharePoint site; note the location of this file.</p>
</li><li>
<p>Using a browser, navigate to your Office 365 Developer Site.</p>
</li><li>
<p>Click <span class="ui">Apps in testing</span>. A window similar to the following opens.</p>
<p><img id="81066" src="http://i1.code.msdn.s-msft.com/office-365-track-which-f4d06334/image/file/81066/1/o365_errortypes-appsintesting.jpg" alt="O365_errortypes app: Apps in Testing screenshot" width="501" height="163"></p>
<p>&nbsp;</p>
</li><li>
<p>Click <span class="ui">new app to deploy</span>. A dialog box opens, as shown in the following figure.</p>
<p><img id="81068" src="http://i1.code.msdn.s-msft.com/office-365-track-which-f4d06334/image/file/81068/1/o365_trackuserdownloading_file_deployapp.jpg" alt="O365 Upload App to DevSite" width="608" height="405"></p>
<p>&nbsp;</p>
</li><li>
<p>Click the <span class="ui">upload</span> link to upload your .app package file.</p>
</li><li>
<p>Browse to the file, select it, and click <span class="ui">OK</span>.</p>
</li><li>
<p>Click the <span class="ui">Deploy</span> button.</p>
</li><li>
<p>Another dialog box opens, as shown in the following figure.</p>
<p><img id="81069" src="http://i1.code.msdn.s-msft.com/office-365-track-which-f4d06334/image/file/81069/1/o365_trackuserdownloading_file_uploadapp.jpg" alt="Upload App to Dev Site" width="548" height="289"></p>
<p>&nbsp;</p>
</li><li>
<p>Click the <span class="ui">Trust It</span> button. The app will be uploaded to your Developer Site.</p>
</li></ol>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Use the following steps to run and test the sample app.</p>
<ol>
<li>
<p>In the left navigation panel, click <span class="ui">Documents</span>. If there are no documents listed, select the
<span class="ui">FILES</span> tab and create one.</p>
</li><li>
<p>For any document in the list, click the ellipsis button (<span class="ui">&hellip;</span>) to the right of the document name and, in the menu that appears, click the ellipsis button (<span class="ui">&hellip;</span>) at the bottom to get the context
 menu.</p>
</li><li>
<p>In the context menu, click &quot;Track Download&quot;. Your item will be downloaded and the message &quot;Successfully downloaded&quot; will appear.</p>
</li><li>
<p>Click the &quot;MyList&quot; link to view the file name of the selected item and the name of the person who downloaded it.</p>
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
<p><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Get started developing apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></p>
</li><li>
<p><a href="http://blogs.technet.com/b/wbaer/archive/2012/10/10/setting-up-a-sharepoint-2013-development-environment-101.aspx" target="_blank">Setting up a SharePoint 2013 Development Environment 101</a></p>
</li></ul>
</div>
</div>
</div>
