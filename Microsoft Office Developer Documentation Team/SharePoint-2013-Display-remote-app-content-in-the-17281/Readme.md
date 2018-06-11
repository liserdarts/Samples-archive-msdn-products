# SharePoint 2013: Display remote app content in the host web using an app part
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* User Experience
## IsPublished
* True
## ModifiedDate
* 2014-06-19 03:49:03
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Display remote app content in the host web using an app part</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This SharePoint 2013 code sample includes a simple page hosted in a remote app. The app part displays the contents of the remote webpage and sends the value of the custom properties through the query string.</p>
</div>
<div>
<p><strong>Last modified: </strong>June 12, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#O15Readme_Description">Description of the sample</a> <br>
<a href="#O15Readme_Prereq">Prerequisites</a> <br>
<a href="#O15Readme_components">Key components of the sample</a> <br>
<a href="#O15Readme_config">Configure the sample</a> <br>
<a href="#O15Readme_test">Run and test the sample</a> <br>
<a href="#O15Readme_Troubleshoot">Troubleshooting</a> <br>
<a href="#O15Readme_Changelog">Change log</a> <br>
<a href="#O15Readme_RelatedContent">Related content</a></p>
<p>This sample provider-hosted app demonstrates how to add an app part to your app for SharePoint that users can add to their Web Parts pages. After the app is deployed, the app part is added to the Web Part gallery in the host web. When end users add the Web
 Part to their Web Parts pages, the property values are passed to the remote webpage through the query string. The remote webpage customizes the rendering process based on the property values.</p>
</div>
<h2>Description of the sample</h2>
<div id="sectionSection0">
<p>The markup that declares the app part is in the AppPart\Elements.xml file in the AppPartsApp project. The rendering logic is in the AppPartContent.html webpage in the AppPartsWeb project. Figure 1 shows the Web Parts page with a Basic app part.</p>
<strong>
<div class="caption">Figure 1. Basic app part in a Web Parts page</div>
</strong><br>
<strong></strong><img src="/site/view/file/117122/1/image.png" alt=""></div>
<h2>Prerequisites</h2>
<div id="sectionSection1">
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
<h2>Key components of the sample</h2>
<div id="sectionSection2">
<p>The sample contains the following:</p>
<ul>
<li>
<p>AppPartsApp project, which contains the AppManifest.xml file</p>
</li><li>
<p>AppPartsWeb project</p>
<ul>
<li>
<p>AppPartContent.html file, which contains the rendering logic</p>
</li><li>
<p>Web.config file</p>
</li></ul>
</li></ul>
</div>
<h2>Configure the sample</h2>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ul>
<li>
<p>Update the <strong>SiteUrl</strong> property of the solution with the URL of the home page of your SharePoint website.</p>
</li></ul>
</div>
<h2>Run and test the sample</h2>
<div id="sectionSection4">
<p>&nbsp;</p>
<ol>
<li>
<p>Press F5 to build and deploy the app.</p>
</li><li>
<p>Choose <strong><span class="ui">Trust It</span></strong> on the consent page to grant permissions to the app.</p>
<p>You should see a SharePoint page with additional instructions.</p>
</li><li>
<p>Go to any wiki page or Web Parts page in the host web.</p>
</li><li>
<p>Edit the page and add the Basic app part from the Web Part gallery. Figure 2 shows the Basic app part in the Web Part gallery.</p>
<strong>
<div class="caption">Figure 2. Basic app part in the Web Part gallery</div>
</strong><br>
<strong></strong><img src="/site/view/file/117123/1/image.png" alt=""> </li></ol>
</div>
<h2>Troubleshooting</h2>
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
<p>Visual Studio does not open the browser after you press the F5 key.</p>
</td>
<td>
<p>Set the app for SharePoint project as the startup project.</p>
</td>
</tr>
<tr>
<td>
<p>The app part does not display any content. The app part displays the following error:
<strong>Navigation to the webpage was canceled</strong>.</p>
</td>
<td>
<p>The browser blocked the content page. The solution might be different depending on the browser you are using:</p>
<ul>
<li>
<p>Internet Explorer 10 and 9 display the following message at the bottom of the page:
<strong>Only secure content is displayed</strong>. Click <strong><span class="ui">Show all content</span></strong> to display the app part content.</p>
</li><li>
<p>Internet Explorer 8 shows a dialog box with the following message: <strong>Do you want to view only the webpage content that was delivered securely?</strong>. Click
<strong><span class="ui">No</span></strong> to display the app part content.</p>
</li></ul>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Change log</h2>
<div id="sectionSection6"><strong>
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
<p>July 16, 2012</p>
</td>
</tr>
<tr>
<td>
<p>Updated version</p>
</td>
<td>
<p>June 2014</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Related content</h2>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">Setting up a SharePoint 2013 development environment for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/bfdd0a58-2cc5-4805-ac89-4bd2fe6f3b09" target="_blank">Create UX components</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/d60f409a-b292-4c06-8128-88629091b753" target="_blank">UX design for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/a2664289-6c56-4cb1-987a-22367fad55eb" target="_blank">How to: Create an app part to deploy with your app for SharePoint</a>
<a href="http://msdn.microsoft.com/library/a2664289-6c56-4cb1-987a-22367fad55eb(Office.15).aspx" target="_blank">
How to: Create app parts to install with your app for SharePoint</a></p>
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
<p>&nbsp;</p>
