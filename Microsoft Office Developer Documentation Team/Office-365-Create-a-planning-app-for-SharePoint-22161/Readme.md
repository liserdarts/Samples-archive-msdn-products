# Office 365: Create a project planning app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* apps for SharePoint
## Topics
* SharePoint
## IsPublished
* True
## ModifiedDate
* 2013-05-20 12:08:57
## Description

<div id="header"><span>Summary:</span> Learn how to create a basic app for SharePoint that allows you to track project status visually in SharePoint. The app is hosted on an Office 365 Developer Site.</div>
<div id="mainSection">
<div id="mainBody">
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>This sample shows you how to build and upload a project-tracking app for SharePoint where the project status, category, and resources can be tracked visually. This project is built using a combination of jQuery, XSLT dataview, and ASP.NET controls.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site. To set up a trial developer account that is already provisioned for apps, see
<a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a>.</p>
</li><li>
<p>Visual Studio 2012 installed on your computer.</p>
</li><li>
<p>The <a href="http://www.microsoft.com/en-us/download/details.aspx?id=35585" target="_blank">
SharePoint Server 2013 Client Components SDK</a>.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample zip file contains the following:</p>
<ul>
<li>
<p><strong><span class="ui">Default.aspx page</span> </strong>. This page contains the project app.</p>
</li><li>
<p><strong><span class="ui">Project List Definition</span> </strong>. The list will generate a new module when a
<strong><span class="ui">new item</span></strong> form is filled out on page refresh. Project documents can be added to the library and linked in the project list by using the
<strong><span class="ui">Project Documents</span></strong> column.</p>
</li><li>
<p><strong><span class="ui">Images folder</span> </strong>. Contains image resources.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Extract the sample zip file.</p>
</li><li>
<p>Start Visual Studio using <strong><span class="ui">Run as Administrator</span></strong>.</p>
</li><li>
<p>In Visual Studio, browse to and open the solution file.</p>
</li><li>
<p>In Solution Explorer, select the project name and open the <strong>Properties</strong> tab.</p>
</li><li>
<p>In the <strong>Site URL</strong> field, enter the name of the remote site you want to deploy.</p>
</li></ol>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Follow these steps to build the sample.</p>
<ul>
<li>
<p>In Visual Studio, press the F5 key to build the application.</p>
</li></ul>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<ol>
<li>
<p>In Visual Studio, on the toolbar, click the <strong><span class="ui">Start</span></strong> button. The application will be deployed to your Developer Site.</p>
</li><li>
<p>After a short while your browser will open and display the page associated with the app. You may need to give the app permissions to run as a trusted app.</p>
</li><li>
<p>The legend colors are defined by the <strong><span class="ui">Category</span></strong> column, and the status icon is controlled by the
<strong><span class="ui">Project Status</span></strong> in the <strong><span class="ui">Project List</span></strong>.</p>
</li><li>
<p>Hover the pointer over the icons to show linked display views, email links, document library, and edit form.</p>
</li><li>
<p>To add documents to the Project Documents library, click the text <strong><span class="ui">There are no documents in this view</span></strong>. This will cause the ribbon to appear. On the
<strong><span class="ui">Files</span></strong> tab, click <strong><span class="ui">Upload Document</span></strong> to add a document, or browse to a document and click
<strong><span class="ui">OK</span></strong>.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how you can solve them.</p>
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
<p>The app fails to upload to the Developer Site.</p>
</td>
<td>
<p>Make sure you entered the site URL correctly in the configuration steps.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Change log</h1>
<div id="sectionSection7"><strong>
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
<p>May 14, 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Getting started developing apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></p>
</li></ul>
</div>
</div>
</div>
