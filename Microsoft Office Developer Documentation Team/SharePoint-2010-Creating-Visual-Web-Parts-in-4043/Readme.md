# SharePoint 2010: Creating Visual Web Parts in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* Visual Web Parts
## IsPublished
* True
## ModifiedDate
* 2011-08-04 11:57:59
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a Visual Web Part for SharePoint 2010 by using Visual Studio 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg413295.aspx">Creating SharePoint 2010 Visual Web Parts in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Visual Web Parts enable developers to build Microsoft SharePoint 2010 Web Parts by using a design surface in Microsoft Visual Studio 2010. This functionality enables developers to drag user controls from the Toolbox to build the Visual Web Part user interface.</p>
<p>This sample and the accompanying article demonstrate the following steps for creating and deploying a SharePoint Visual Web Part that uses LINQ to SharePoint:</p>
<ul>
<li>The solution uses LINQ to SharePoint to retrieve the contents of two lists, an
<strong>Interview</strong> list and a <strong>Candidate</strong> list. </li><li>The lists are linked in SharePoint through a lookup field, with the <strong>Interviews</strong> list linking back to the
<strong>Candidates</strong> list. This provides the information needed to join the lists in the LINQ query.
</li><li>To use LINQ, the <strong>SPMetal</strong> command-line tool is used to generate an entity class, which provides an object-oriented interface to the lists and libraries in the SharePoint deployment. The entities file that is generated is added to the Visual
 Web Part project, and a reference is added to the <strong>Microsoft.SharePoint.Linq</strong> assembly through the using or imports statements to
<strong>System.Linq</strong> and <strong>Microsoft.SharePoint.Linq</strong>. </li><li>The code creates a <strong>DataContext</strong>, and then uses a LINQ to SharePoint query to retrieve data from the lists.
</li><li>An implicitly typed object is used in the <strong>foreach</strong> statement to build the
<strong>Treeview</strong> control. </li></ul>
