# SharePoint 2010: Creating List Definitions in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* list definitions
* list instances
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:22:18
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create list definitions and list instances for Microsoft SharePoint 2010 by using Microsoft Visual Studio 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/7c862564-4290-49ef-ac53-451c687255d4.aspx">
Creating SharePoint 2010 List Definitions in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>List definitions and list instances that are based on those definitions are a fundamental part of Microsoft SharePoint 2010. Microsoft Visual Studio 2010 provides a project type that makes it easier to create list definitions in XML and to create list instances
 that are based on those definitions. In addition, tight integration with SharePoint 2010 enables you to deploy list definitions and list instances to a SharePoint site directly from Visual Studio 2010.</p>
<p>This sample and the accompanying article use the following steps for creating and deploying a list definition in Visual Studio 2010:</p>
<ol>
<li>Create a SharePoint 2010 list definition application solution in Visual Studio 2010.
</li><li>Edit the list instances Elements.xml file to specify details for an instance of the list.
</li><li>Edit the list definition Elements.xml file to define the content type and fields that are displayed on the create page for items created in an instance of this list.
</li><li>Edit the list definition Schema.xml file to define the content and columns that appear in the views for this list.
</li></ol>
<p>In this example, you create a list definition for Equipment Availability that lets users create equipment items and specify whether the equipment is currently available.</p>
