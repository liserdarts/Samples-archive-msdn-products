# SharePoint 2010: Consuming External Data Using BCS and Excel Add-Ins
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* Excel 2010
* SharePoint Designer 2010
## Topics
* consuming data
## IsPublished
* True
## ModifiedDate
* 2011-08-09 03:24:42
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use Microsoft Business Connectivity Services (BCS) in Microsoft SharePoint Server 2010 to access and update external data by using Microsoft Excel 2010 as a client. This sample is accompanied by the article
<a href="http://msdn.microsoft.com/en-us/library/ff677562(office.14).aspx">Consuming External Data Using SharePoint Server 2010 Business Connectivity Services and Excel 2010 Add-Ins</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Microsoft Business Connectivity Services (BCS) feature in Microsoft SharePoint Server 2010 provides many ways to consume data from an external system, and use it when offline or connected. You can create add-ins, task panes, and other Microsoft Office
 extensibility features that can help you to interact with external data by using the user-friendly Office client applications.</p>
<p>This sample and the accompanying article demonstrate a scenario in which the AdventureWorks sample database that is hosted in Microsoft SQL Server 2008 is used as an external system. You will create an external content type by using Microsoft SharePoint
 Designer 2010. Then, you will create a Microsoft Excel 2010 add-in to interact with an external system to read external items, navigate associations, and update external items by using the Business Connectivity Services client object model. The Excel add-in
 is deployed to the client by using a ClickOnce package. The metadata model will be deployed separately to the client by using a ClickOnce package created with the BCS Solution Packaging Tool.</p>
<p>To run this scenario, you must install the following software in your server environment, and you must create the AdventureWorks database.</p>
<ul>
<li>Microsoft SQL Server 2008 </li><li>Microsoft SharePoint Server 2010 </li><li>Microsoft SharePoint Designer 2010 </li><li>Microsoft Excel 2010 </li><li>Microsoft Visual Studio 2010 </li></ul>
