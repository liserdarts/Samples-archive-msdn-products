# SharePoint 2010: Aggregating Data Using BCS and .NET Assemblies
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Business Connectivity Services
* SharePoint Server 2010
## Topics
* aggregating data
* .NET framework assemblies
## IsPublished
* True
## ModifiedDate
* 2011-08-03 02:48:03
## Description

<h2><strong>Introduction</strong></h2>
<p>Create and use a .NET Framework assembly with Business Connectivity Services in SharePoint Server 2010 to aggregate data from two external systems: a SQL Server database and a SharePoint document library. Use external content types to display the data in
 SharePoint and learn ways to retrieve credentials from the Secure Store Service. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff728359.aspx">Creating .NET Assemblies That Aggregate Data from Multiple External Systems for Business Connectivity Services in SharePoint Server 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Business Connectivity Services (BCS) provides a platform known as the Business Data Connectivity (BDC) service to pull data from external systems into Microsoft SharePoint Server 2010 or Microsoft Office 2010. To do this, BDC provides various connectors
 for connecting to, and retrieving data from, web services, Windows Communication Foundation (WCF) services, and databases. Another connector uses Microsoft .NET Framework assemblies to consume external data. This is a good option if you must aggregate, calculate,
 or clean the external data before it can be consumed by SharePoint Server 2010.</p>
<p>This code sample and the accompanying article show a quick four-step process for creating a .NET assembly that Business Connectivity Services can use to retrieve external data for Microsoft SharePoint Server 2010 by using Microsoft Visual Studio 2010. They
 also show how to obtain credentials from the Secure Store Service, and how to stream BLOB data into SharePoint Server 2010 by using Business Connectivity Services.</p>
