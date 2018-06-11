# SharePoint 2010: Creating a .NET Connectivity Assembly for BCS
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* Business Connectivity Services
## Topics
* .NET Framework connectivity assembly
## IsPublished
* True
## ModifiedDate
* 2011-08-09 03:06:57
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a Microsoft .NET Framework connectivity assembly for Microsoft Business Connectivity Services (BCS) by using Microsoft Visual Studio 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg585180.aspx">Creating a .NET Connectivity Assembly for Business Connectivity Services by Using Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Visual Studio 2010 provides a project type that developers can use to build .NET Framework connectivity assemblies for Business Connectivity Services.</p>
<p>This sample and the accompanying article show how to create a .NET Framework connectivity assembly for Business Connectivity Services by using the following steps:</p>
<ol>
<li>Create the Business Data Connectivity model. </li><li>Define type descriptors for the <strong>ReadItem</strong> and <strong>ReadList</strong> methods.
</li><li>Create additional data operation methods. </li><li>Implement the data operation methods with code. </li><li>Add permission on the new entity. </li><li>Set up and use external lists in Microsoft SharePoint 2010. </li><li>Review changes to the back-end data repository. </li></ol>
<p>In this example, you create a BDC model that uses BCS entities in Visual Studio to access a &quot;Training&quot; matrix in Microsoft SQL Server. The matrix consists of a many-to-many relationship between People and the Courses that those people have attended or that
 they are scheduled to attend. The solution includes adding, updating, and deleting entities that comprise several database tables.</p>
