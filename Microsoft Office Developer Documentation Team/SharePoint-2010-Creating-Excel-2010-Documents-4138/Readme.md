# SharePoint 2010: Creating Excel 2010 Documents with Custom SharePoint Workflows
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* Open XML SDK 2.0
* SharePoint Server 2010
* Excel 2010
* SharePoint Foundation 2010
## Topics
* custom workflows
## IsPublished
* True
## ModifiedDate
* 2011-08-09 11:46:52
## Description

<h2><strong>Introduction</strong></h2>
<p>SharePoint Server 2010 provides a rich platform to create line of business applications that integrate with workflow processes and publish information to intranet, Internet, and extranet websites. Open XML file formats make it possible to generate Microsoft
 Office documents, spreadsheets, and presentations programmatically. By combining the two technologies, you can easily automate common business processes such as document generation and publishing. Learn to generate Excel 2010 spreadsheets inside SharePoint
 workflows and publish them to SharePoint sites. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg317441.aspx">Creating and Publishing Excel 2010 Documents with Custom SharePoint 2010 Workflows</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft SharePoint Server 2010 workflows are based on the <a href="http://msdn.microsoft.com/en-us/netframework/aa663328.aspx" target="blank">
Windows Workflow Foundation</a>. You can create SharePoint workflows within SharePoint sites by using a web browser, in Microsoft SharePoint Designer 2010, or with Microsoft Visual Studio 2010. Microsoft Visual Studio 2010 is required to create workflows that
 use the Open XML APIs. It is also possible to create custom workflow activities that use the Open XML APIs in Visual Studio, and then publish them to SharePoint servers so the activities may be used inside workflows created by using SharePoint Designer 2010.
 This sample and the accompanying article focus on creating custom workflows with Visual Studio 2010.</p>
<p>Microsoft SharePoint Foundation 2010 provides an extensible platform that integrates with SharePoint Workflows. This integration enables you to associate workflows with data that is stored in SharePoint lists and document libraries. The workflows can create,
 read, update, and delete items and documents in SharePoint lists. The accompanying article describes how to create a SharePoint workflow that reads information from SharePoint list items that are participating in the workflow, and how to use the list item
 information to generate an Excel document.</p>
<p>SharePoint 2010 document management capabilities let you securely store and manage documents. After the workflow generates the Excel document, it publishes the document to a SharePoint document library.</p>
