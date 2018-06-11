# SharePoint 2010: Creating Custom Business Connectivity Services Connectors
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Business Connectivity Services
* SharePoint Server 2010
## Topics
* custom connectors
## IsPublished
* True
## ModifiedDate
* 2011-08-08 03:02:46
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a Microsoft Business Connectivity Services (BCS) custom connector to integrate a variety of data sources into Microsoft SharePoint Server 2010. This sample accompanies the Visual How To
<a href="http://msdn.microsoft.com/en-us/library/ff953161.aspx">Creating Custom Business Connectivity Services Using SharePoint Server 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft SharePoint Server 2010 offers integration with external systems (also known as line-of-business systems) through Microsoft Business Connectivity Services (BCS). However, some external systems require more flexibility and powerful mechanisms to
 correctly consume the data. Business Connectivity Services custom connectors offer you this flexibility and power.</p>
<p>This sample and the accompanying article address the main steps to help you create a Business Connectivity Services custom connector, as follows:</p>
<ul>
<li>Coding the assembly. </li><li>Creating the model. </li><li>Using the custom connector. </li></ul>
<p>This sample helps you construct a custom connector that interacts with the file system. Basically, it provides interaction with a specified folder through the following method types:</p>
<ul>
<li><strong>Finder</strong>&mdash;Returns a list of files that match the wildcard criteria that is specified.
</li><li><strong>SpecificFinder</strong>&mdash;Returns file information for the file name that is specified.
</li><li><strong>StreamAccessor</strong>&mdash;Returns a read-only file stream for the file name that is specified.
</li></ul>
