# SharePoint 2013: Use the SharePoint style sheet in an app
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
* 2014-06-24 11:14:13
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Use a SharePoint website's style sheet in an app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p><span class="label">Summary:</span>&nbsp;&nbsp;This code sample references the SharePoint 2013 website's style sheet in a remote webpage and uses the available styles.</p>
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
<p>This sample provider-hosted app demonstrates how to reference the style sheet in your app for SharePoint. When you reference the style sheet in your app, you can use the referenced SharePoint website's styles in your webpages.</p>
</div>
<a name="O15Readme_Description"></a>
<h2 class="heading">Description of the sample</h2>
<div class="section" id="sectionSection0">
<p>The markup that references the style sheet is in the StyleConsumer.html file in the StyleSheetWeb project. Figure 1 shows the sample webpage that uses the style sheet.</p>
<div class="caption">Figure 1. Webpage using the style sheet</div>
<br>
<img id="117514" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-the-7a8684e2/image/file/117514/1/stylesheetcontrol_result2.png" alt="Stylesheet control used in a web page" width="633" height="300"></div>
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
<p>StyleSheetApp project</p>
<ul>
<li>
<p>AppManifest.xml file</p>
</li><li>
<p>Blank.html file</p>
</li></ul>
</li><li>
<p>StyleSheetWeb project</p>
<ul>
<li>
<p>StyleConsumer.html file, which references the style sheet and uses the available styles</p>
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
<p>Update the <strong>SiteUrl</strong> property of the solution with the URL of your SharePoint website.</p>
</li></ul>
</div>
<a name="O15Readme_test"></a>
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection4">
<p>&nbsp;</p>
<ol>
<li>
<p>Press F5 to build and deploy the app.</p>
</li><li>
<p>Choose <span class="ui">Trust It</span> on the consent page to grant permissions to the app.</p>
</li><li>
<p>Click the <span class="ui">StyleSheetBasic</span> app icon.</p>
</li></ol>
</div>
<a name="O15Readme_Troubleshoot"></a>
<h2 class="heading">Troubleshooting</h2>
<div class="section" id="sectionSection5">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</p>
<div class="caption">Table 2. Troubleshooting the solution</div>
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
<p>HTTP error 405. Method not allowed.</p>
</td>
<td>
<p>Make sure the <span class="keyword">AppPrincipal</span> property in the app manifest is set to
<span class="keyword">Internal</span>. Alternatively, in your web project, use an ASPX page instead of an HTML page.</p>
</td>
</tr>
<tr>
<td>
<p>Certificate error.</p>
</td>
<td>
<p>Set the <strong>SSL Enabled</strong> property of your web project to false. In the app for SharePoint project, set the
<strong>Web Project</strong> property to <span class="input">None</span>, and then set the property back to your web project's name.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_Changelog"></a>
<h2 class="heading">Change log</h2>
<div class="section" id="sectionSection6">
<ul>
<li>
<p>First version: September 2012</p>
</li><li>
<p>2nd version: June 2014</p>
</li></ul>
</div>
<a name="O15Readme_RelatedContent"></a>
<h2 class="heading">Related content</h2>
<div class="section" id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">Setting up a SharePoint 2013 development environment for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/bfdd0a58-2cc5-4805-ac89-4bd2fe6f3b09" target="_blank">Create UX components</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d60f409a-b292-4c06-8128-88629091b753" target="_blank">UX design for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/25d84ac5-d2b3-40c7-962d-1408aacf14ed" target="_blank">How to: Use a SharePoint website's style sheet in apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" target="_blank">What you can do in an app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/0942fdce-3227-496a-8873-399fc1dbb72c" target="_blank">Design considerations for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" target="_blank">Critical aspects of the app for SharePoint architecture and development landscape</a></p>
</li></ul>
</div>
</div>
</div>
