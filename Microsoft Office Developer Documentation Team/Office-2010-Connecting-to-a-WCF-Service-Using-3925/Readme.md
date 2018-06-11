# Office 2010: Connecting to a WCF Service Using Business Connectivity Services
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Business Connectivity Services
* SharePoint Server 2010
* Word 2010
* Office 2010
* SharePoint Designer 2010
## Topics
* WCF Service
## IsPublished
* True
## ModifiedDate
* 2011-07-28 11:59:07
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a Microsoft Business Connectivity Services (BCS) external content type for Microsoft Office 2010 that is based on a Windows Communication Foundation (WCF) service. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff953200.aspx">Connecting to a WCF Service Using Business Connectivity Services in Office 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Business Connectivity Services (BCS), provided with Office 2010 and SharePoint 2010, introduces new features such as write-back support and rich client integration. Microsoft SharePoint Designer 2010 provides the External Content Type Designer
 that you can use to create Microsoft Business Connectivity Services (BCS) external content types based on Windows Communication Foundation (WCF) services.</p>
<p>This sample and the accompanying article show how to use SharePoint Designer to create a Business Connectivity Services external content type that connects to a WCF service. The service is created with Visual Studio 2010 and uses XLINQ to provide read/write
 access to data that is stored in a simple XML data file. The external content type is from external data content controls in a Microsoft Word document. The Word document is created from a SharePoint document library that exposes external data via an external
 data column.</p>
<p>The article also provides steps that show how to use Visual Studio 2010 to create a Windows Communication Foundation (WCF) service, and then use SharePoint Designer 2010 to create a Business Connectivity Services external content type based on that service.
 The WCF service can perform read/write operations on an XML data file and implements methods that can be mapped to the Create, Read Item, Read List, Update, and Delete BCS stereotypes.</p>
<p>Finally, the article describes how to surface external data in a Word document by creating a SharePoint document library that uses an external data column that is based on the external content type. A Word document is created from the document library and
 external data content controls are used to display external data.</p>