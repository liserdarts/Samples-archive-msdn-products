# SharePoint 2013: Access external data using OData extension provider and OAuth
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Authentication
* data and storage
## IsPublished
* True
## ModifiedDate
* 2014-05-13 04:38:09
## Description

<div id="header">Demonstrates how to attach an OAuth token to an outgoing OData request from an external content type. The sample uses Microsoft Azure Media Services, which provides an OAuth-secured RESTful endpoint. This endpoint can be used to manage media
 assets.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span>&nbsp;&nbsp;<a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=be34f5b5-a1d1-47e1-971d-cfdda319992c" target="_blank">Scot Hillier</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The sample uses the abstract class <span><span class="keyword">Microsoft.BusinessData.SystemSpecific.OData.ODataExtensionProvider</span></span> as the basis for creating a custom OData extension provider. The custom OData extension provider allows an OAuth
 token to be attached to the outgoing Business Connectivity Services (BCS) request. This scenario is important when a RESTful service requires an OAuth token passed with every request. Microsoft Azure Media Services requires such a token, which must first be
 obtained from Microsoft Azure Access Control Services (ACS) before making a call to the Microsoft Azure Media Services.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Basic familiarity with Business Connectivity Services</p>
</li><li>
<p>A configured Microsoft Azure Media Services account with a media service and access key available</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<p><strong>AzureODataExtensionProvider.sln</strong> contains the solution with the custom OData extension provider.</p>
</li><li>
<p><strong>NewODataConnectionSetting.ps1</strong> is the Windows PowerShell script for registering the Custom OData extension provider.</p>
</li><li>
<p><strong>WindowsMediaServicesModel.xml</strong> is the BDC Metadata Model containing the definition for the
<span><span class="keyword">LOBSystemInstance</span></span> associated with the Custom OData extension provider.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<ul>
<li>
<p>Open the solution <span class="ui">AzureODataExtensionProvider.sln</span> in Visual Studio 2012.</p>
</li><li>
<p>Enter your media service account name for Microsoft Azure Media Services in the code.</p>
</li><li>
<p>Enter your secret for Microsoft Azure Media Services in the code.</p>
</li><li>
<p>Edit the Windows PowerShell script <span class="ui">NewODataConnectionSetting.ps1</span> to refer to your environment.</p>
</li></ul>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<ul>
<li>
<p>Build <span class="ui">AzureODataExtensionProvider.sln.</span></p>
</li><li>
<p>Add the assembly <span><span class="keyword">AzureODataExtensionProvider.dll</span></span> in the global assembly cache.</p>
</li><li>
<p>Execute <span class="ui">NewODataConnectionSetting.ps1</span>.</p>
</li><li>
<p>Install the <span class="ui">WindowsMediaServicesModel.xml</span> model in the BDC Service application.</p>
</li></ul>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Make an external list based on the WindowsMediaServicesModel.xml model.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails at run time, make sure that you have provided the correct values for the account and secret you are using with Microsoft Azure Media Services.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/windowsazure/hh973629" target="_blank">Microsoft Azure Media Services</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142385.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163782.aspx" target="_blank">Business Connectivity Services</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/sharepoint/ff521587.aspx" target="_blank">SharePoint Foundation REST Interface</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms457529.aspx" target="_blank">Authentication, authorization, and security in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
</div>
