# Test Code Sample Publishing Automation
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Exchange 2013 Preview
## Topics
* Cloud
* App model
## IsPublished
* False
## ModifiedDate
* 2012-10-11 09:51:19
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Get the host web title using the cross-domain library and JSOM</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>This documentation is preliminary and is subject to change.</p>
<div class="summary">
<p>Learn how to use the cross-domain library in apps for SharePoint to read the host web title by using the JavaScript object model (JSOM).</p>
</div>
<div class="introduction">
<p>This sample developer-hosted app demonstrates how to use the cross-domain library in SharePoint 2013 Preview to read the title property of the host web. The app displays the title in a simple HTML page using the JavaScript object model (JSOM).</p>
</div>
<a name="O15Readme_Description">
<h1 class="heading">Description of the sample</h1>
<div id="sectionSection0" class="section" name="collapseableSection" style="">
<p>The code that uses the cross-domain library is in the ReadTitle.html file of the CrossDomainWeb project. Figure 1 shows the ReadTitle.html page of the app after you install and run the app.</p>
<div class="caption">Figure 1. Browser windows after running the solution</div>
<br>
<img alt="Resulting screens after going through this how to" src="CrossDomainReadTitleResult.jpg"></div>
</a><a name="O15Readme_Prereq">
<h1 class="heading">Prerequisites</h1>
<div id="sectionSection1" class="section" name="collapseableSection" style="">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2012</p>
</li><li>
<p>SharePoint development tools in Visual Studio 2012</p>
</li><li>
<p>A SharePoint 2013 Preview development environment (app isolation required for on-premises scenarios)</p>
<p>For more information, see </a><a href="http://msdn.microsoft.com/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a>.</p>
</li></ul>
</div>
<a name="O15Readme_components">
<h1 class="heading">Key components of the sample</h1>
<div id="sectionSection2" class="section" name="collapseableSection" style="">
<p>The sample contains the following:</p>
<ul>
<li>
<p>CrossDomainApp project, which contains the AppManifest.xml file</p>
<ul>
<li>
<p>AppHost.aspx page, which implements a best practice for using the cross-domain library in cross-zone scenarios. For more information, see
</a><a href="http://msdn.microsoft.com/library/3d24f916-60b2-4ea9-b182-82e33cad06e8" target="_blank">Work with the cross-domain library across different Internet Explorer security zones in apps for SharePoint</a>.</p>
</li></ul>
</li><li>
<p>CrossDomainWeb project</p>
<ul>
<li>
<p>ReadTitle.html file, which contains a reference to the cross-domain library</p>
</li><li>
<p>Web.config file</p>
</li></ul>
</li></ul>
</div>
<a name="O15Readme_config">
<h1 class="heading">Configure the sample</h1>
<div id="sectionSection3" class="section" name="collapseableSection" style="">
<p>Follow these steps to configure the sample.</p>
<ul>
<li>
<p>Update the <b>Site URL</b> property of the solution with the URL of the home page of your SharePoint website.</p>
</li></ul>
</div>
</a><a name="O15Readme_test">
<h1 class="heading">Run and test the sample</h1>
<div id="sectionSection4" class="section" name="collapseableSection" style="">
<p></p>
<ol>
<li>
<p>Press F5 to build and deploy the app.</p>
</li><li>
<p>Choose <span class="ui">Trust It</span> on the consent page to grant permissions to the app.</p>
</li></ol>
<p>You should see an HTML page with the text <b>The host web title is:</b> followed by the SharePoint website title.</p>
</div>
</a><a name="O15Readme_Troubleshoot">
<h1 class="heading">Troubleshooting</h1>
<div id="sectionSection5" class="section" name="collapseableSection" style="">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</p>
<div class="caption"></div>
<div class="tableSection">
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
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
<p>Unhandled exception <b>SP is undefined</b>.</p>
</td>
<td>
<p>Make sure you can access the SP.RequestExecutor.js file from a browser window.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
</a><a name="O15Readme_Changelog">
<h1 class="heading">Change log</h1>
<div id="sectionSection6" class="section" name="collapseableSection" style="">
<p>First version: September 2012</p>
</div>
</a><a name="O15Readme_RelatedContent">
<h1 class="heading">Related content</h1>
<div id="sectionSection7" class="section" name="collapseableSection" style="">
<ul>
<li>
<p></a><a href="http://msdn.microsoft.com/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/1534a5f4-1d83-45b4-9714-3a1995677d85" target="_blank">Working with data in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/bc37ff5c-1285-40af-98ae-01286696242d" target="_blank">How to: Access SharePoint 2013 data from remote apps using the cross-domain library</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3d24f916-60b2-4ea9-b182-82e33cad06e8" target="_blank">Work with the cross-domain library across different Internet Explorer security zones in apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" target="_blank">What you can do in an app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/bde5647a-fff1-4b51-b67b-2139de79ce4a" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/f36645da-77c5-47f1-a2ca-13d4b62b320d" target="_blank">Choose the right API set in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/0942fdce-3227-496a-8873-399fc1dbb72c" target="_blank">Three ways to think about design options for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3034f03c-2d5a-46de-9cb8-2c101ff194fa" target="_blank">Data storage options in apps for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
<img src="/site/view/file/67927/1/image.png" alt=""> 