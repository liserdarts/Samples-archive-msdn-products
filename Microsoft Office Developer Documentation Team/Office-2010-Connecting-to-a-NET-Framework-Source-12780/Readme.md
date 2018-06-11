# Office 2010: Connecting to a .NET Framework Source Using BCS
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* Business Connectivity Services
* SharePoint Server 2010
## Topics
* .NET Framework connectivity assembly
## IsPublished
* True
## ModifiedDate
* 2011-08-12 03:17:47
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a Microsoft Office 2010 Business Connectivity Services External Content Type based on a .NET Framework source. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff394331(office.14).aspx">Connecting to a .NET Framework Source Using Business Connectivity Services in Office 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Business Connectivity Services (BCS), provided with Office 2010 and SharePoint 2010, introduces new features such as write-back support and rich client integration. Visual Studio 2010 provides a Business Data Connectivity Model project template
 and several new tools that simplify the creation of BCS models and the deployment of Office solutions that uses external data and services.</p>
<p>This sample shows how to use Visual Studio 2010 to create a BCS external content type. The external content type connects to a .NET Framework assembly that uses Linq to XML to provide read/write access to data that is stored in a simple XML data file. The
 external content type is shown being used from a SharePoint external list.</p>
<p>The sample and the accompanying article show you how to create and use a .NET Framework-based BCS external content type by using the following steps:</p>
<ol>
<li>Create an XML data file to simulate a back-end data store. </li><li>Create a SharePoint 2010 Business Data Connectivity Model project in Visual Studio 2010.
</li><li>Delete the default Visual Studio generated entity from the model. </li><li>Add the Customer Class to the project. </li><li>Add the Customer entity to the model. </li><li>Add a reference to <strong>Microsoft.BusinessData</strong> to the project. </li><li>Add code to the Customer external content type to implement the <strong>Finder</strong>,
<strong>Specific Finder</strong>, <strong>Creator</strong>, <strong>Updater</strong>, and
<strong>Deleter</strong> stereotypes. </li><li>Deploy the solution to SharePoint and test the Customer external content type by creating a SharePoint external list.
</li></ol>
