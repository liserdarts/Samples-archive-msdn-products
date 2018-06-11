# SharePoint 2013: Call a workflow in an app using a remote event receiver
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
* 2013-04-23 02:29:36
## Description

<div id="header">Illustrates how to start a SharePoint workflow in an app for SharePoint by using a remote event receiver. In this scenario, an external site uses a remote event receiver to call a workflow in an app.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p><span class="label">Provided by: </span><a href="http://social.msdn.microsoft.com/profile/andrew%20connell%20%5bmvp%5d/" target="_blank">Andrew Connell</a>,
<a href="http://www.andrewconnell.com" target="_blank">www.AndrewConnell.com</a></p>
</div>
<div class="section" id="sectionSection0">
<p>This sample demonstrates two core concepts. First, it demonstrates how to create a remote event receiver that calls out to a remote service when a list item is created. It then demonstrates how the remote event receiver service can use the client object
 model (CSOM) to find a workflow association and start a new instance of the workflow on a specific list item while also passing values into the workflow.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>SharePoint 2013 installation that is connected to a configured Workflow Manager 1.0 farm.</p>
</li><li>
<p>Workflow Manager 1.0 that has the March 2013 cumulative update applied.</p>
</li><li>
<p>Service Bus 1.0, with the Mach 2013 cumulative update applied.</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<p><strong>RERWorkflowApp.sln</strong> is the Visual Studio solution that contains the entire app for SharePoint and workflow, in addition to the remote web application containing the remote event receiver implementation.</p>
</li><li>
<p>The <strong>AnnouncementList</strong> in the Visual Studio solution contains a custom list schema and instance of an Announcement list.</p>
</li><li>
<p>The <span><span class="keyword">RemoteEventReceiver</span></span> in the Visual Studio solution contains the registration of the custom remote event receiver on the custom Announcement list.</p>
</li><li>
<p>The <span><span class="keyword">UpdateAnnoncementWorkflow</span></span> in the Visual Studio solution contains the workflow that, when initiated, updates the list item it was started by.</p>
</li><li>
<p>The <span><span class="keyword">RerSpHostedAppWeb</span></span> project in the Visual Studio solution contains the web project and remote event receiver service implementation.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample app:</p>
<ol>
<li>
<p>Ensure that you have a site collection created using the <span class="ui">Developer Site</span> template.</p>
</li><li>
<p>Created a self-signed certificate, a trusted security token issuer, and then update the remote web with the credentials:</p>
<ul>
<li>
<p>Open the file <strong>01_CreateSSLCertificateForAppServer.ps1</strong> and address any &quot;TODO&quot; comments.</p>
</li><li>
<p>Run the Windows PowerShell script, <strong>01_CreateSSLCertificateForAppServer.ps1</strong>.</p>
</li><li>
<p>Open the file <strong>02_CreateTrustedSecurityTokenIssuer.ps1</strong> and address any &quot;TODO&quot; comments</p>
</li><li>
<p>Run the script <strong>02_CreateTrustedSecurityTokenIssuer.ps1</strong>.</p>
</li><li>
<p>Open the web.config file that is located in the <span><span class="keyword">RerWorkflowAppWeb</span></span>.</p>
</li><li>
<p>Copy the following into the <span class="code">&lt;appSettings&gt;</span> node:
<br>
<span class="code">&lt;add key=&quot;ClientSigningCertificatePath&quot; value=&quot; &quot; /&gt;</span><br>
<span class="code">&lt;add key=&quot;ClientSigningCertificatePassword&quot; value=&quot;Password1&quot; /&gt;</span><br>
<span class="code">&lt;add key=&quot;IssuerId&quot; value=&quot;11111111-1111-1111-1111-111111111111&quot; /&gt;</span></p>
</li><li>
<p>Update the key <span class="code">ClientSigningCertificatePath</span> to point to the full path of the password protected private key (<span><span class="keyword">*.pfx</span></span>)</p>
</li></ul>
</li><li>
<p>Change the <span><span class="keyword">Site URL</span></span> property on the project to be the URL of the Developer Site.</p>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Build <span class="ui">RERWorkflowApp.sln</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<ol>
<li>
<p>Open the <span class="ui">RERWorkflowApp.sln</span> and press F5 (ensuring that it has been configured to point to the Developer Site).</p>
</li><li>
<p>When the app deploys, it opens the home page, which contains a link to the <span class="ui">
Announcements</span> list.</p>
</li><li>
<p>Click the <span class="ui">Announcements list</span> link, add a new item, and then click
<span class="ui">Save</span>.</p>
</li><li>
<p>After a moment, the workflow starts by being called from the remote event receiver (which you can observe in the workflow debugging console window launched by Visual Studio).</p>
</li><li>
<p>Go back to the list item to see that the <span class="ui">Body</span> field was updated by the workflow.</p>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the workflow fails, it is likely due to a misconfiguration of the certificate, registering the new trusted security token issuer, registering a new app principal, or when updating the AppManifest.xml or web.config.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<td>
<p>First release</p>
</td>
<td>
<p>January 2013</p>
</td>
</tr>
<tr>
<td>
<p>Revised</p>
</td>
<td>
<p>April 2013</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163986.aspx" target="_blank">Workflows in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163276.aspx" target="_blank">Start: Set up and configure SharePoint 2013 Workflow Manager</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj193528(v=azure.10).aspx" target="_blank">Workflow Manager 1.0</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj220043(v=office.15).aspx" target="_blank">How to: Create a remote event receiver</a></p>
</li></ul>
</div>
</div>
</div>
