# SharePoint 2013: Access external data using an OData extension provider
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* REST
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-04-03 01:29:55
## Description

<p id="header">Demonstrates how to attach a simple token to an outgoing OData request from an external content type. Uses a custom RESTful service that returns product information in ATOM format. The custom service examines the Authorization header of each
 request for a token that contains a well-known application identifier.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<p id="sectionSection0" class="section"><span class="label">Provided by:</span>&nbsp;&nbsp;<a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=be34f5b5-a1d1-47e1-971d-cfdda319992c" target="_blank">Scot Hillier</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<div class="section">This sample uses the abstract class <span><span class="keyword">Microsoft.BusinessData.SystemSpecific.OData.ODataExtensionProvider</span></span> as the basis for creating a custom OData extension provider. The custom OData extension
 provider allows a simple token to be attached to the outgoing Business Connectivity Services (BCS) request. This scenario is important when a RESTful service requires a token passed with every request.</div>
<h1 class="heading">Prerequisites</h1>
<p id="sectionSection1" class="section">This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Basic familiarity with Business Connectivity Services</p>
</li></ul>
<p>&nbsp;</p>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<p><strong>SimpleSecuredODataSource.sln</strong> is the source code for the custom RESTful service that returns product information in ATOM format.</p>
</li><li>
<p><strong>SimpleODataExtensionProvider.sln</strong> is the source code for the OData Extension Provider that will create the simple token and attach it to the outgoing request.</p>
</li><li>
<p><strong>NewODataConnectionSetting.ps1</strong> is the Windows PowerShell for registering the OData Extension provider</p>
</li><li>
<p><strong>SimpleSecuredODataModel.bdcm</strong> is a BDC Metadata Model that references the custom OData service and the OData extension provider. This model may be installed directly in the BDC Service Application as the basis for external lists.</p>
</li><li>
<p><strong>SimpleECTAuthApp.sln</strong> contains the source code for a SharePoint-hosted app that calls the custom RESTful service.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<p id="sectionSection3" class="section">Edit the Windows PowerShell script NewODataConnectionSetting.ps1 to refer to your environment.</p>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<ul>
<li>
<p>Build SimpleODataExtensionProvider.sln.</p>
</li><li>
<p>Install the assembly <span><span class="keyword">SimpleODataExtensionProvider.dll</span></span> in the global assembly cache.</p>
</li><li>
<p>Execute <span class="ui">NewODataConnectionSetting.ps1</span>.</p>
</li><li>
<p>Install the <span class="ui">SimpleSecuredODataModel.bdcm</span> model in the BDC Service Application.</p>
</li></ul>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<div class="subSection">
<ol>
<li>
<p>Open the SimpleSecuredODataSource.sln and press F5. The service will start and be ready to handle incoming requests on
<span class="code">http://localhost:33668/</span></p>
</li><li>
<p>Set a breakpoint in the <span><span class="keyword">ValidateToken</span></span> method to see the incoming token.</p>
</li><li>
<p>Make an external list based on the SimpleSecuredODataModel.bdcm model.</p>
</li><li>
<p>View the external list and see that the breakpoint is hit.</p>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<p id="sectionSection6" class="section">If the external list fails to communicate with the service, test the service in a browser with the following URL:
<span class="code">http://localhost:33668/Products.svc/products</span></p>
<p class="section">If the token is not being passed to the service, verify that the OData Extension Provider is in the GAC and properly registered.</p>
<h1 class="heading">Change log</h1>
<p id="sectionSection7" class="section">First release: January 2013</p>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142385.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163782.aspx" target="_blank">Business Connectivity Services</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/sharepoint/ff521587.aspx" target="_blank">SharePoint Foundation REST Interface</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/sharepoint/ff521586(v=office.14).aspx" target="_blank">WCF Services in SharePoint Foundation</a></p>
</li></ul>
</div>
</div>
</div>
</div>
