# SharePoint 2010: Creating Site Definitions in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* site definitions
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:19:10
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create site definitions for Microsoft SharePoint 2010 by using Microsoft Visual Studio 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/7cc22fbe-fd69-4fcf-9428-cd03107b76bd.aspx">
Creating SharePoint 2010 Site Definitions in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>In Microsoft SharePoint 2010, you can create new sites from site definitions by clicking
<strong>New Site</strong> on the <strong>Site Actions</strong> menu. You can create new site definitions in Microsoft Visual Studio 2010 and then deploy them to SharePoint 2010. This SharePoint sample demonstrates how to create a new site definition and then
 add a Web Part to the site's default.aspx page. The Web Part filters tasks based on their due dates.</p>
<p>This sample demonstrates the following tasks:</p>
<ul>
<li>Creating a task list named Project Tasks and adding it to the Quick Launch navigation bar.
</li><li>Adding a Web Part to the project that filters tasks based on their due dates.
</li><li>Editing the default.aspx page so that it includes the new Web Part. </li><li>Deploying and testing the site definition. </li></ul>
<p>To create a SharePoint 2010 Site Definition project in Visual Studio 2010, use the following steps:</p>
<ul>
<li>Start Visual Studio 2010. On the <strong>File</strong> menu, click <strong>New</strong>, and then click
<strong>Project</strong>. </li><li>In the <strong>New Project</strong> dialog box, in the <strong>Installed Templates</strong> section, expand either
<strong>Visual Basic</strong> or <strong>Visual C#</strong>, expand <strong>SharePoint</strong>, and then click
<strong>2010</strong>. </li><li>In the template list, click <strong>Site Definition</strong>. </li><li>In the <strong>Name</strong> box at the bottom, type <strong>FilteredTaskSite</strong>.
</li><li>Leave the default values in the other fields, and click <strong>OK</strong>. </li><li>Under <strong>What local site do you want to use for debugging?</strong>, select your site. Click
<strong>Finish</strong>. </li></ul>
