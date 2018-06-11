# SharePoint 2013: Deploy a web service and call it securely through a web proxy
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* web service
* Workflows
## IsPublished
* False
## ModifiedDate
* 2014-05-13 04:14:26
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Deploy a web service and call it securely through a web proxy</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Learn how to deploy and call a web service using the JavaScript client object model (CSOM) and using OData calls from both script and from a workflow in SharePoint 2013.</p>
</div>
<div>
<h1>Description</h1>
<div id="sectionSection0">
<p>This sample autohosted app demonstrates how to call a web service provisioned by the app using three different methods&mdash;using the JavaScript client object model (CSOM), using OData calls from a script, and using OData calls from a workflow&mdash;and
 display the result (sets the title of the list item to the returned string).</p>
<p>The call is protected by OAuth, so it is neither anonymous nor is it authenticated by a clear-text secret in the workflow or in the script. The workflow and the script call through the
<span><strong><span class="keyword">WebProxy</span></strong></span>, which sends the context token along to the web service. The web service then uses the context token to connect back to SharePoint and return data to the caller. Alternatively, after validating
 the authenticity of the context token, the web service could return some sensitive data stored in the web application or call other services.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Office 365 Developer Site</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample contains the following:</p>
<ul>
<li>
<p>WebProxySample project</p>
<ul>
<li>
<p><strong>Default.aspx</strong> file, which displays the results from the web service and also intermediate results.</p>
</li><li>
<p><strong>Workflow file</strong>, which first uses the <span><strong><span class="keyword">GetRemoteWebApp</span></strong></span> custom action to determine the URL of the web service and then uses the
<span><strong><span class="keyword">WebProxy</span></strong></span> to call the web service to return results to SharePoint</p>
</li><li>
<p><span><strong><span class="keyword">GetRemoteWebApp</span> </strong></span>workflow custom action, which determines the URL of the autoprovisioned web application</p>
</li><li>
<p>SharePoint list named <span><strong><span class="keyword">List1</span></strong></span></p>
</li><li>
<p><strong>AppManifest.xml</strong> file</p>
</li></ul>
</li><li>
<p>WebProxySampleWeb project</p>
<ul>
<li>
<p><strong>MyController.cs</strong>, which contains the web service</p>
</li><li>
<p><strong>TokenHelper.cs</strong></p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>To configure the sample:</p>
<ul>
<li>
<p>Update the <span><strong><span class="keyword">SiteUrl</span></strong></span> property of the WebProxySample project with the URL of your SharePoint website.</p>
</li></ul>
</div>
<h1>Deploy and test the sample</h1>
<div id="sectionSection4">
<div>
<ol>
<li>
<p>Choose <strong><span class="ui">Deploy solution</span></strong> on the <strong>
<span class="ui">Build</span></strong> menu to deploy the app.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Caution</strong> </th>
</tr>
<tr>
<td>
<p>Do not use the <strong><span class="ui">Start Debugging</span></strong> or <strong>
<span class="ui">Start Without Debugging</span></strong> commands. Neither of these commands will provision a website in Microsoft Azure, as required by this sample.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>Choose <strong><span class="ui">Yes</span></strong> to trust the self-signed Localhost certificate.</p>
</li><li>
<p>Choose <strong><span class="ui">Yes</span></strong> to install the certificate.</p>
</li><li>
<p>Choose <strong><span class="ui">Trust It</span></strong> on the consent page to grant permissions to the app.</p>
</li><li>
<p>Add a new item to the list displayed on the home page by choosing the <strong>
<span class="ui">Add Item</span></strong> link.</p>
</li><li>
<p>In the <strong><span class="ui">Title</span></strong> field, enter the name of the item, for example, &quot;<span>Test</span>&quot;.</p>
</li><li>
<p>Choose <strong><span class="ui">Save</span></strong> to add a new item to the list and navigate back to the home page. You will see the item added to the list.</p>
</li><li>
<p>Refresh the browser window (in Internet Explorer, you can press the F5 key). You will see that the title of the newly created item was changed by the workflow to &quot;WebProxySample&quot;.</p>
</li></ol>
</div>
<p>You should see a SharePoint webpage that displays the App Web Url, App Instance Id, Server Relative Url, Remote App Url, Invocation Result using CSOM API, and ODATA API.</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection5">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</p>
<strong>
<div class="caption"></div>
</strong>
<div>
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
<p>Visual Studio does not open the browser after you deploy the solution.</p>
</td>
<td>
<p>Set the app for SharePoint project as the startup project.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Change log</h1>
<div id="sectionSection6">
<p>First release: January 2013</p>
<p>Updated: February 2013</p>
</div>
<h1>Related content</h1>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/1534a5f4-1d83-45b4-9714-3a1995677d85" target="_blank">Work with data in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/16913e6d-4fc6-4c5e-84a4-6c2688703798" target="_blank">How to: Query a remote service using the web proxy in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" target="_blank">What you can do in an app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/bde5647a-fff1-4b51-b67b-2139de79ce4a" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/0942fdce-3227-496a-8873-399fc1dbb72c" target="_blank">Three ways to think about design options for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/3034f03c-2d5a-46de-9cb8-2c101ff194fa" target="_blank">Data storage options in apps for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
</div>
