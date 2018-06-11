# SharePoint 2013: Customize list views and forms using client-side rendering
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
* 2013-02-27 07:12:52
## Description

<p id="header">This SharePoint-hosted app uses client-side rendering to provide custom rendering for both the All Items view and the New Announcement form.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=52a3f2aa-710f-4496-9b78-f240eccc74ad" target="_blank">Ted Pattison</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The AnnouncementCSR sample app demonstrates how to use client-side rendering with the views and forms that are associated with a SharePoint list. When the app is installed, it creates a customizable list based on the built-in Announcements list and adds
 several announcement items. It uses the <span><span class="keyword">JSLink</span></span> attribute in the schema.xml file to add support for client-side rendering.</p>
<div>Key features illustrated in the sample:</div>
<ul>
<li>
<div>Using client-side rendering with views</div>
</li><li>
<div>Using client-side rendering with the Title field on the New Item form</div>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint 2013 development environment using Office 365 or a local SharePoint farm</div>
</li><li>
<div>Local SharePoint farm must be configured to support apps for SharePoint</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample consists of a Visual Studio 2012 project for a SharePoint-hosted app named
<strong>AnnouncementCSR</strong>.</p>
<p>The <strong>schema.xml file</strong> includes a <span><span class="keyword">JSLink</span></span> attribute for the default view and for the new item, which points to the JavaScript file named
<strong>AnnouncementCSR.js</strong>. All the code resides inside the AnnouncementCSR.js file.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample.</div>
<ol>
<li>
<div>Make sure that you have a SharePoint 2013 development environment with a local SharePoint farm.</div>
</li><li>
<div>Make sure that the SharePoint farm is configured to support apps for SharePoint.</div>
</li></ol>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<div class="subSection">
<ol>
<li>
<div>Open the solution <span class="ui">AnnouncementCSR</span> project.</div>
</li><li>
<div>Configure the project's <span><span class="keyword">Site Url</span></span> property to point to a SharePoint 2013 test site.</div>
</li><li>
<div>Press F5 to test the app in the debugger.</div>
</li><li>
<div>See the default view, which displays a custom view of announcements.</div>
</li><li>
<div>Click the <span class="ui">New item</span> link to display the <span class="ui">
New item</span> form.</div>
</li><li>
<div>On the <span class="ui">New item</span> form, click <span class="ui">Add Auto Title</span> to add a title.</div>
</li><li>
<div>Fill in content for the <span class="ui">Body</span> and <span class="ui">
Expires</span> fields, and save the item.</div>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection5">
<div>If the app fails to install, troubleshoot the following aspect of your development environment:</div>
<ul>
<li>
<div>Make sure your environment supports apps. In Visual Studio 2012, create a new SharePoint-hosted app, and ensure that you can deploy it in a test site on your farm. If you cannot, your environment is not configured to support apps for SharePoint.</div>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj220045(v=office.15).aspx" target="_blank">How to: Customize a list view in apps for SharePoint using client-side rendering</a></div>
</li><li>
<div><a href="http://code.msdn.microsoft.com/SharePoint-2013-Customize-61761017/view/SourceCode" target="_blank">SharePoint 2013: Customize a list view by using client-side rendering</a></div>
</li></ul>
</div>
</div>
</div>
</div>
