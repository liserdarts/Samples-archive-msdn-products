# Office 365: Migrate a SharePoint 2010 farm solution to SharePoint Online
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* Office 365
## Topics
* Migration
* on-premises
* cloud development
## IsPublished
* True
## ModifiedDate
* 2013-02-28 01:26:54
## Description

<p id="header"><span class="label">Summary:</span>&nbsp;&nbsp;The two solutions in this sample demonstrate how to create a SharePoint 2010farm solution and then a SharePoint-hosted app for SharePoint to create a custom list, add items to it, and then retrieve
 those items. The two different approaches illustrate how a farm solution can be migrated to a SharePoint-hosted app for SharePoint.</p>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the sample</h1>
<p id="sectionSection0" class="section">This sample contains two solutions that demonstrate the approach used by a SharePoint 2010farm solution and a SharePoint-hosted app for SharePoint to create a custom list. The SharePoint 2010farm solution uses the
 SharePoint server-side object model. The SharePoint-hosted app for SharePoint uses the JavaScript API for Office and Microsoft Silverlight to develop a solution. The SharePoint 2010farm solution adds a custom list to a visual Web Part, and then retrieves the
 list items by using the listdata.svc service. The app for SharePoint achieves similar functionality by using the JavaScript API for Office. The list items are retrieved by using the _vti_bin/client.svc service.</p>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>The two solutions in this sample require the following:</p>
<ul>
<li>
<p>SharePoint 2010 server with Visual Studio 2012 installed on the same computer.</p>
</li><li>
<p>An Office 365 Developer Site.</p>
</li><li>
<p>Visual Studio 2012.</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012.</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The following solutions are included:</p>
<ul>
<li>
<p>A Visual Studio 2012 solution named SP2010application_cs that contains the SharePoint 2010farm solution.</p>
</li><li>
<p>A Visual Studio 2012 solution named O365_sp2013app_cs that contains the code for the SharePoint-hosted app for SharePoint.</p>
</li></ul>
</div>
<h1 class="heading">Configure the solutions</h1>
<div class="section" id="sectionSection3">
<p><strong>Configure the SP2010application_cs solution</strong></p>
<ol>
<li>
<p>Extract the files from SP2010application_cs.zip into a folder on the SharePoint 2010 server computer.</p>
</li><li>
<p>Open Visual Studio 2012 with administrator privileges, and then open SP2010application_cs.sln. The SP2010Application project contains the CreateListAndGetListItemsWebPart Web Part. This visual Web Part creates a list using the server-side object model, adds
 an item to the list, and retrieves the list items by using the REST API.</p>
</li><li>
<p>Verify that the Microsoft.SharePoint.dll reference is added in the solution; if it is not, then add it from this location:
<span class="placeholder">&lt;Your SharePoint installation drive&gt;</span>\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\ISAPI\Microsoft.SharePoint.dll.</p>
</li><li>
<p>Select the SP2010application_cs project in <span class="ui">Solution Explorer</span> to display the
<span class="ui">Properties</span> pane.</p>
</li><li>
<p>In the <span class="ui">Properties</span> pane, change the <span class="ui">
Site URL</span> property of the project to the URL of your SharePoint 2010 site URL.</p>
</li></ol>
<p><strong>Configure the O365_sp2013app_cs solution</strong></p>
<ol>
<li>
<p>Extract the files from O365_sp2013app_cs.zip into a folder.</p>
</li><li>
<p>Open Visual Studio 2012 with administrator privileges, and then open O365_sp2013app_cs.sln. The O365_SP2013App_cs solution contains the following files:</p>
<ul>
<li>
<p>The App.js file contains the code for creating the list, adding an item to the list, and then retrieving items from the list.</p>
</li><li>
<p>The AppManifest.xml file contains the permission that is required for the app for SharePoint to run successfully. To create a new custom list, the
<span class="label">Manage</span> permission is required on the site collection level.</p>
</li><li>
<p>The Default.aspx file contains the HTML and ASP.NET controls for the user interface of the app for SharePoint.</p>
</li></ul>
</li><li>
<p>Select the O365_SP2013App project in <span class="ui">Solution Explorer</span> to display the
<span class="ui">Properties</span> pane.</p>
</li><li>
<p>In the <span class="ui">Properties</span> pane, change the <span class="ui">
Site URL</span> property of the project to the URL of your Developer Site.</p>
</li></ol>
</div>
<h1 class="heading">Build the solutions</h1>
<div class="section" id="sectionSection4">
<p><strong>Build the SP2010application_cs solution</strong></p>
<ul>
<li>
<p>Right-click the SP2010application_cs project, and then click <span class="ui">
Deploy</span>.</p>
</li></ul>
<p><strong>Build the O365_sp2013app_cs solution</strong></p>
<ol>
<li>
<p>Right-click the O365_sp2013app_cs project and click <span class="ui">Publish</span>.</p>
</li><li>
<p>On the <span class="ui">Publish Summary</span> page, select the <span class="ui">
Open output folder after successful packaging</span> option, and then click <span class="ui">
Finish</span>.</p>
</li><li>
<p>A folder that contains the O365_SharePointAutoHosted.app file opens; note the file location. The file path will be similar to
<span class="placeholder">&lt;Your application root folder name&gt;</span>\ O365_MigrateSP2010Solution\O365_SP2013App_cs\O365_SP2013App_cs\O365_SP2013App_cs\bin\Debug\app.publish\1.0.0.0\ O365_SP2013App.app.</p>
</li></ol>
</div>
<h1 class="heading">Run and test the solutions</h1>
<div class="section" id="sectionSection5">
<p><strong>Run and test the SP2010application_cs solution</strong></p>
<ol>
<li>
<p>Create a Web Part page on your SharePoint 2010 site and add the visual Web Part you created on the page, by using the following steps:</p>
<ol>
<li>
<p>Open your SharePoint 2010 site.</p>
</li><li>
<p>Click <span class="ui">Site Actions</span>, and then click <span class="ui">
More Options</span>.</p>
</li><li>
<p>On the <span class="ui">Installed Items</span> page, scroll and select the <span class="label">
Web Part Page</span>, and then click <span class="ui">Create</span>.</p>
</li><li>
<p>On the <span class="ui">New Web Part Page</span> page, provide a name, for example, &quot;WebPartPage&quot; and select any layout template for the page.</p>
</li><li>
<p>Click <span class="ui">Create</span>.</p>
</li><li>
<p>On the Web Part page, click any of the links with the text <span class="ui">
Add a Web Part</span>.</p>
</li><li>
<p>On the <span class="ui">Categories</span> list, select <span class="ui">Custom</span>, and on the
<span class="ui">Web Parts</span> list select <span class="ui">SP2010application_cs - CreateListAndGetListItemsWebPart</span>.</p>
</li><li>
<p>Click <span class="ui">Add</span>. The Web Part will be added to the Web Part page.</p>
</li><li>
<p>On the ribbon, click <span class="ui">Stop Editing</span>.</p>
</li></ol>
</li><li>
<p>Click the <span class="ui">Create ExpenseReport List</span> button to create the list, and then click
<span class="ui">Get ExpenseReport List Items</span> to retrieve the list data, as shown in the following figure.</p>
</li></ol>
<img id="76594" src="http://i1.code.msdn.s-msft.com/office-365-migrate-a-2d581a80/image/file/76594/1/o365expensereportlistsp2010.jpg" alt="Expense Report webpart." width="507" height="312">
<p>&nbsp;</p>
<p><strong>Run and test the O365_sp2013app_cs solution</strong></p>
<ol>
<li>
<p>Open your Office 365 Developer Site.</p>
</li><li>
<p>On the Developer Site, in the <span class="ui">Apps in Testing</span> list, click the plus sign next to the text
<span class="ui">new app to deploy</span>.</p>
</li><li>
<p>On the <span class="ui">Deploy App</span> page, click the <span class="ui">
upload</span> link.</p>
</li><li>
<p>Navigate to the location of O365_SP2013App.app, select it, and click <span class="ui">
OK</span>.</p>
</li><li>
<p>Click <span class="ui">Deploy</span>, and on the next dialog box click <span class="ui">
Trust it</span>.</p>
</li><li>
<p>The application will be uploaded to your Office 365 Developer Site.</p>
</li><li>
<p>On your Developer Site, in the <span class="ui">Apps in Testing</span> folder, click the O365_sp2013app.cs app to run it.</p>
</li><li>
<p>Click the <span class="ui">Create ExpenseReport List</span> button to create the list, and then click
<span class="ui">Get ExpenseReport List Items</span> to retrieve the list data, as shown in the following figure.</p>
</li></ol>
<img id="76595" src="http://i1.code.msdn.s-msft.com/office-365-migrate-a-2d581a80/image/file/76595/1/o365listviewexpensereport.jpg" alt="Expense report." width="403" height="275"></div>
<a name="O15Readme_Troubleshoot"></a>
<h1 class="heading">Troubleshooting</h1>
<p id="sectionSection6" class="section">The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how you can solve them.</p>
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
<p>The SP2010application_cs solution does not build.</p>
</td>
<td>
<p>Make sure you have added a reference to Microsoft.SharePoint.dll.</p>
</td>
</tr>
<tr>
<td>
<p>The SP2010application_cs solution gives an error that states &quot;The specified path, File name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters&quot; when you deploy it.</p>
</td>
<td>
<p>Move the SP2010application_cs folder that contains the solution closer to the root folder of the computer.</p>
</td>
</tr>
<tr>
<td>
<p>The app for SharePoint fails to upload to the Developer Site.</p>
</td>
<td>
<p>Make sure you entered the <span class="ui">Site URL</span> correctly in the configuration steps.</p>
</td>
</tr>
</tbody>
</table>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
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
<p>February 28, 2013</p>
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
<p><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Getting started developing apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
