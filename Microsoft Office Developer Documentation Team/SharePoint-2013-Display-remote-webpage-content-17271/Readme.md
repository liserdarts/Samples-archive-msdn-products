# SharePoint 2013: Display remote webpage content using the coffeemaker app part
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
* 2013-07-25 04:17:39
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Coffeemaker app, display remote webpage content using an app part</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This SharePoint 2013 code sample includes an app part that displays a remote webpage's content. The remote page content consists of labels and images for a coffee cup. End users can further customize their coffee through the custom
 properties in the app part.</p>
</div>
<div>
<p>&nbsp;</p>
</div>
<div id="sectionSection0">
<p>This sample app demonstrates how to add an app part to your app for SharePoint that users can add to their Web Parts pages. After the app is deployed, the app part is added to the Web Part gallery in the host web. End users can add the app part to their
 Web Parts pages. The app demonstrates the following scenarios:</p>
<ul>
<li>
<p>Declaration and usage of custom properties</p>
</li><li>
<p>Resize the app part</p>
</li><li>
<p>Detect when the app part is in edit mode</p>
</li><li>
<p>Load the SharePoint style sheet</p>
</li></ul>
<p>The markup that declares the app part is in the CoffeemakerPart\Elements.xml file in the CoffeemakerApp project. The rendering logic is in the Coffeemaker.js file in the CoffeemakerWeb project. Figure 1 shows a SharePoint page with the Coffeemaker app part.</p>
<strong>
<div class="caption">Figure 1. Coffeemaker app part in a SharePoint page.</div>
</strong><br>
<img src="/site/view/file/92867/1/image.png" alt=""></div>
<h1>Prerequisites</h1>
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
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample contains the following:</p>
<ul>
<li>
<p>CoffemakerApp project</p>
<ul>
<li>
<p>Elements.xml feature file, which contains the declaration of the app part and its custom properties</p>
</li></ul>
</li><li>
<p>CoffeemakerWeb project</p>
<ul>
<li>
<p>Coffeemaker.html file, which contains the HTML placeholders</p>
</li><li>
<p>Common.js, which contains support functions</p>
</li><li>
<p>StyleLoader.js, which loads the style sheet from the SharePoint site</p>
</li><li>
<p>Coffeemaker.js file, which contains the rendering logic including when the app part is in edit mode</p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ul>
<li>
<p>Update the <strong>SiteUrl</strong> property of the solution with the URL of the home page of your SharePoint website.</p>
</li></ul>
</div>
<h1>Run and test the sample</h1>
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
<p>Edit the page, and add the Coffeemaker app part from the Web Part gallery.</p>
</li></ol>
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
<p>&nbsp;</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Change log</h1>
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
<p>Added resize functionality</p>
</td>
<td>
<p>April, 2013</p>
</td>
</tr>
<tr>
<td>
<p>Added style sheet functionality</p>
</td>
<td>
<p>July, 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">Setting up a SharePoint 2013 development environment for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/bfdd0a58-2cc5-4805-ac89-4bd2fe6f3b09" target="_blank">Create UX components</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/d60f409a-b292-4c06-8128-88629091b753" target="_blank">UX design for apps</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/a2664289-6c56-4cb1-987a-22367fad55eb" target="_blank">How to: Create an app part to deploy with your app for SharePoint</a></p>
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
