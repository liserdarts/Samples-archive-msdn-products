# Office 365: Handle common errors in Office 365
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* SharePoint Server 2013
## Topics
* Javascript
* Error Handling
* apps for SharePoint
* error detection and correction
## IsPublished
* True
## ModifiedDate
* 2013-03-05 04:18:50
## Description

<p id="header"><span class="label">Summary:</span> This sample demonstrates how to handle some common Office 365 errors and exceptions.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div>&nbsp;</div>
</div>
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>This sample is a SharePoint-hosted app. The sample shows some exceptions that can occur while performing basic operations by using the JavaScript client object model in SharePoint 2013. The app creates four buttons that generate different error messages.
 The app automatically recovers from the errors so that you can continue to click the buttons to generate additional error messages.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>The solution in this sample requires the following:</p>
<ul>
<li>
<div>Visual Studio 2012.</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012 (use the <a href="http://www.microsoft.com/web/downloads/platform.aspx/" target="_blank">
Microsoft Web Platform Installer</a>).</div>
</li><li>
<div>An Office 365 Developer Site. You can sign up <a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx/" target="_blank">
here</a>.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample contains the following key files:</p>
<ul>
<li>
<div>The O365_errortypes_cs project.</div>
</li><li>
<div>App.js, a JavaScript file that contains the app logic.</div>
</li><li>
<div>AppManifest.xml, which contains the permission that is required by the app to run successfully.</div>
</li><li>
<div>Default.aspx, which contains the HTML and ASP.NET controls for the user interface.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>&nbsp;</div>
<p>Use the following steps to configure the O365_errortypes_cs SharePoint app.</p>
<ol>
<li>
<div>Extract the files from O365_errortypes_cs.zip into a folder.</div>
</li><li>
<div>Open Visual Studio 2012 with administrator privileges.</div>
</li><li>
<div>On the <span class="ui">File</span> menu, click <span class="ui">Open</span>, and then click
<span class="ui">Project</span>.</div>
</li><li>
<div>Navigate to the location of the O365_errortypes_cs solution folder and select the O365_errortypes_cs.sln file.</div>
</li><li>
<div>Set the <span class="ui">Site URL</span> property of the O365_errortypes_cs solution to the URL of your SharePoint site collection.</div>
</li><li>
<div>Press Ctrl&#43;S to save all the changes.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">The following steps show how to build and deploy the app.
<ol>
<li>
<div>In Visual Studio, right-click the O365_errortypes_cs solution folder and click
<span class="ui">Build</span>.</div>
</li><li>
<div>Right-click the O365_errortypes_cs solution folder and click <span class="ui">
Publish</span>. The following window opens.</div>
<div><img id="77256" src="http://i1.code.msdn.s-msft.com/office-365-handle-common-a5c29dbe/image/file/77256/1/o365_errortypes-.jpg" alt="O365_errortypes app: Publish Summary screenshot" width="689" height="531"></div>
<div>&nbsp;</div>
</li><li>
<div>Select the <span class="ui">Open output folder after successful packaging</span> check box, and then click
<span class="ui">Finish</span>.</div>
</li><li>
<div>This will create the .app extension file, which will be used to deploy the app on the SharePoint site; note the location of this file.</div>
</li><li>
<div>Using a browser, navigate to your Office 365 Developer Site.</div>
</li><li>
<div>Click <span class="ui">Apps in testing</span>. The following window opens.</div>
<div><img id="77257" src="http://i1.code.msdn.s-msft.com/office-365-handle-common-a5c29dbe/image/file/77257/1/o365_errortypes-appsintesting.jpg" alt="O365_errortypes app: Apps in Testing screenshot" width="501" height="163"></div>
<div>&nbsp;</div>
</li><li>
<div>Click <span class="ui">new app to deploy</span>.</div>
</li><li>
<div>A dialog box opens, as shown in the following figure.</div>
<div><img id="77258" src="http://i1.code.msdn.s-msft.com/office-365-handle-common-a5c29dbe/image/file/77258/1/o365_errortypes-deployapp.jpg" alt="O365_errortypes app: Deploy App screenshot" width="1225" height="493"></div>
<div>&nbsp;</div>
</li><li>
<div>Click the <span class="ui">upload</span> link to upload your .app package file.</div>
</li><li>
<div>Browse to the file, select it, and click <span class="ui">OK</span>.</div>
</li><li>
<div>Click the <span class="ui">Deploy</span> button.</div>
</li><li>
<div>Another dialog box opens, as shown in the following figure.</div>
<div><img id="77259" src="http://i1.code.msdn.s-msft.com/office-365-handle-common-a5c29dbe/image/file/77259/1/o365_errortypes-uploadapp.jpg" alt="O365_errortypes app: Upload App screenshot" width="612" height="316"></div>
<div>&nbsp;</div>
</li><li>
<div>Click the <span class="ui">Trust It</span> button. The app will be uploaded to your Developer Site.</div>
</li></ol>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Use the following steps to run and test the O365_errortypes_cs sample app.</p>
<ol>
<li>
<div>To run the app, click the app name. The sample app opens, as shown in the following figure.</div>
<div><img id="77260" src="http://i1.code.msdn.s-msft.com/office-365-handle-common-a5c29dbe/image/file/77260/1/o365_errortypes-appinaction.jpg" alt="O365_errortypes app: App in action screenshot" width="547" height="197"></div>
<div>&nbsp;</div>
</li><li>
<div>Click any of the buttons to see an error message.</div>
</li><li>
<div>Click <span class="ui">OK</span> to recover from the error and click any of the buttons to continue testing.</div>
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
<div>Problem</div>
</th>
<th>
<div>Solution</div>
</th>
</tr>
<tr>
<td>
<div>In Visual Studio 2012, when prompted to &quot;Connect to SharePoint&quot;, an error message says, &quot;Access Denied&hellip; You are not a member of this site.&quot;</div>
</td>
<td>
<div>The credentials are not correct for the URL specified in the <span class="ui">
Site URL</span> property of the app. Correct the logon credentials. If the problem persists, try deleting the browsing history and logging on again.</div>
</td>
</tr>
<tr>
<td>
<div>You do not see the &quot;Publish Summary&quot; screen when you click <span class="ui">
Publish</span>.</div>
</td>
<td>
<div>Check to be sure you created your Office 365 subscription account and have configured your account's developer URL in the sample solution, as described in the steps for &quot;Configure the sample&quot;.</div>
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
<div>Version</div>
</th>
<th>
<div>Date</div>
</th>
</tr>
<tr>
<td>
<div>First version</div>
</td>
<td>
<div>February 28, 2013</div>
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
<div><a href="http://msdn.microsoft.com/en-us/library/jj164022.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 REST endpoints</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Get started developing apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></div>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
