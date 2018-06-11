# SharePoint 2010: Creating Sequential Workflows in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* Sequential Workflow
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:32:02
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a SharePoint 2010 Sequential Workflow in Visual Studio 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/a424af62-a7ed-42b6-a62e-6e8f5ee97f92.aspx">
Creating SharePoint 2010 Sequential Workflows in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Visual Studio 2010 provides a template for creating a Sequential Workflows that enables developers the opportunity to build workflow solutions within SharePoint using a graphical design surface. Sequential Workflows have a predetermined order of activities
 that define the workflow.</p>
<p>This sample demonstrates the following tasks:</p>
<ol>
<li>Creating a prerequisite document library called Projects. </li><li>Adding additional fields to the Projects folder, including a Document Status choice column.
</li><li>Creating a Sequential Workflow with a <strong>While</strong> activity. </li><li>Adding code to complete the <strong>While</strong> loop when the Document Status is set to Review Completed.
</li></ol>
<p>This workflow requires a specific document library and three columns added to that library. The columns will maintain the document status, the name of the person who is assigned the next task in the workflow, and a record of any review comments.</p>
<p>To create the Projects SharePoint document library, use the following steps:</p>
<ol>
<li>On the <strong>Site Actions</strong> menu, click <strong>More Options</strong>.
</li><li>In the installed items list, click <strong>Document Library</strong>. </li><li>On the right side of the screen, in the name box, type <strong>Projects</strong> and then click
<strong>Create</strong>. </li></ol>
