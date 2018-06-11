# SharePoint 2013: Open a remote app webpage using a ribbon custom action
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
* User Experience
## IsPublished
* True
## ModifiedDate
* 2014-06-24 11:13:36
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Open a remote app webpage using a ribbon custom action</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p><span class="label">Summary:</span>&nbsp;&nbsp;This code sample includes an ECB (Edit Control Block) custom action that passes the SharePoint website URL, List ID, and selected Item IDs from where it is invoked to a remote webpage.</p>
</div>
<div class="introduction">
<p><strong>Last modified: </strong>June 16, 2014</p>
<p><strong>In this article</strong><br>
<a href="#O15Readme_Description">Description of the sample</a><br>
<a href="#O15Readme_Prereq">Prerequisites</a><br>
<a href="#O15Readme_components">Key components of the sample</a><br>
<a href="#O15Readme_config">Configure the sample</a><br>
<a href="#O15Readme_test">Run and test the sample</a><br>
<a href="#O15Readme_Troubleshoot">Troubleshooting</a><br>
<a href="#O15Readme_Changelog">Change log</a><br>
<a href="#O15Readme_RelatedContent">Related content</a></p>
<p>This sample provider-hosted app demonstrates how to add a ribbon custom action to your app for SharePoint. After the app is deployed, the custom action is displayed in every document library's ribbon. When end users click the custom action, the
<span class="keyword">ItemId</span> along with other contextual parameters are passed to the remote webpage through the query string. The remote webpage displays the parameters and values.</p>
</div>
<a name="O15Readme_Description"></a>
<h2 class="heading">Description of the sample</h2>
<div class="section" id="sectionSection0">
<p>The markup that declares the custom action is in the CustomAction\Elements.xml file in the CustomActionsApp project. The CustomActionTarget.html webpage in the CustomActionsWeb project displays the parameters that are passed by the custom action. Figure
 1 shows the ribbon custom action in a document library.</p>
<div class="caption">Figure 1. Document library with a ribbon custom action</div>
<br>
<img id="117515" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-open-e0ca1826/image/file/117515/1/customactionsribbon_result.jpg" alt="A document library with a Ribbon custom action" width="558" height="467"></div>
<a name="O15Readme_Prereq"></a>
<h2 class="heading">Prerequisites</h2>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>A SharePoint 2013 development environment (app isolation required for on-premises scenarios)</p>
</li></ul>
</div>
<a name="O15Readme_components"></a>
<h2 class="heading">Key components of the sample</h2>
<div class="section" id="sectionSection2">
<p>The sample contains the following:</p>
<ul>
<li>
<p>CustomActionsApp project</p>
<ul>
<li>
<p>CustomAction\Elements.xml file, which contains the rendering logic</p>
</li><li>
<p>AppManifest.xml file</p>
</li></ul>
</li><li>
<p>CustomActionsWeb project</p>
<ul>
<li>
<p>CustomActionTarget.html file, which renders the query string parameters</p>
</li><li>
<p>Web.config file</p>
</li></ul>
</li></ul>
</div>
<a name="O15Readme_config"></a>
<h2 class="heading">Configure the sample</h2>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ul>
<li>
<p>Update the <strong>SiteUrl</strong> property of the solution with the URL of the home page of your SharePoint website.</p>
</li></ul>
</div>
<a name="O15Readme_test"></a>
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection4">
<ol>
<li>
<p>Press F5 to build and deploy the app.</p>
</li><li>
<p>Choose <span class="ui">Trust It</span> on the consent page to grant permissions to the app.</p>
<p>You should see a SharePoint page with additional instructions.</p>
</li><li>
<p>Go to any document library in the host web.</p>
</li><li>
<p>On the ribbon, choose the <span class="ui">Invoke custom action</span> button on the
<span class="ui">Files</span> tab.</p>
<p>Figure 2 shows the webpage with parameters passed by the custom action.</p>
<div class="caption">Figure 2. Webpage with parameters from a custom action</div>
<br>
<img id="117516" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-open-e0ca1826/image/file/117516/1/customactionsribbon_target.jpg" alt="Web page with parameters from a custom action" width="606" height="383">
</li></ol>
</div>
<a name="O15Readme_Troubleshoot"></a>
<h2 class="heading">Troubleshooting</h2>
<div class="section" id="sectionSection5">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</p>
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
<p>Visual Studio does not open the browser after you press the F5 key.</p>
</td>
<td>
<p>Set the app for SharePoint project as the startup project.</p>
</td>
</tr>
<tr>
<td>
<p>HTTP error 405 <strong>Method not allowed</strong>.</p>
</td>
<td>
<p>Locate the applicationhost.config file in <span class="placeholder">%userprofile%</span>\Documents\IISExpress\config.</p>
<p>Locate the handler entry for <strong>StaticFile</strong>, and add the verbs <span class="keyword">
GET</span>, <span class="keyword">HEAD</span>, <span class="keyword">POST</span>,
<span class="keyword">DEBUG</span>, and <span class="keyword">TRACE</span>.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_Changelog"></a>
<h2 class="heading">Change log</h2>
<div class="section" id="sectionSection6">
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
<p>July 16, 2012</p>
</td>
</tr>
<tr>
<td>
<p>2nd version</p>
</td>
<td>
<p>June 2014</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_RelatedContent"></a>
<h2 class="heading">Related content</h2>
<div class="section" id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">Setting up a SharePoint 2013 development environment for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/bfdd0a58-2cc5-4805-ac89-4bd2fe6f3b09" target="_blank">Create UX components</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/d60f409a-b292-4c06-8128-88629091b753" target="_blank">UX design for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/bbd11f94-1798-453e-bbb0-e5eb0df8dc75" target="_blank">How to: Create a custom action to deploy with your app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" target="_blank">What you can do in an app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/0942fdce-3227-496a-8873-399fc1dbb72c" target="_blank">Design considerations for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" target="_blank">Critical aspects of the app for SharePoint architecture and development landscape</a></p>
</li></ul>
</div>
</div>
</div>
