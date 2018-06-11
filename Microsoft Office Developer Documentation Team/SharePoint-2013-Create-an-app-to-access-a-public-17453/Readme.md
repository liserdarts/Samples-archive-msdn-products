# SharePoint 2013: Create an app to access a public OData source
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* REST
* Javascript
* SharePoint Server 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:26:26
## Description

<p><span style="font-size:small"><strong>Provided by</strong>:&nbsp;&nbsp; <a href="http://www.shillier.com/default.aspx">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx">
Critical Path Training</a></span></p>
<p><span style="font-size:small">This sample demonstrates how to create an external content type contained within an app for SharePoint, and then surface that data using an external list. The OData source that is used for this sample is the public Northwind
 OData source located at <a href="http://services.odata.org/Northwind/Northwind.svc">
http://services.odata.org/Northwind/Northwind.svc</a>. This OData source is used to create an external list named
<strong>Customers</strong> in the app. Data from the external list is then displayed on the home page of the app by using the RESTful API to query the list. The following image shows the data that will be displayed on the home page of the app.</span></p>
<p><strong><span style="font-size:small">Figure 1. Data displayed on the home page of the app</span></strong></p>
<p><span style="font-size:small"><img id="60263" src="/site/view/file/60263/1/fig1.jpg" alt="" width="332" height="389"></span></p>
<p><span style="font-size:small">The Visual Studio 2012 solution contained in this sample download consists of the following:&nbsp;</span></p>
<ul>
<li><span style="font-size:small">BCS Module (containing the BDC Metadata Model and External List definition)</span>
</li><li><span style="font-size:small">Pages Module (containing the app home page)</span>
</li><li><span style="font-size:small">Scripts Module (containing the JavaScript and jQuery code for querying the External List).</span>
</li></ul>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">SharePoint 2013 development environment that is configured for app isolation</span>
</li><li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The following are included in the sample project:</span></p>
<ul>
<li><span style="font-size:small">A Visual Studio 2012 solution named <strong>AppLevelECT</strong>, which contains all the required artifacts.</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the sample.</span></p>
<ol>
<li><span style="font-size:small">In Visual Studio, select the AppLevelECT project (which is contained in the .zip file that you downloaded from the MSDN Samples Gallery), and change the Site URL property to reference a SharePoint 2013 Preview site where you
 will deploy the app.</span> </li><li><span style="font-size:small">In Visual Studio, open the AppLevelODataModel.xml file for editing.</span>
</li><li><span style="font-size:small">Replace all instances of the text &ldquo;contoso\Domain Users&rdquo; with the name of a group from the target domain. The group used will be granted rights to use the BDC Metadata Model in the App.</span>
</li></ol>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press <strong>F5</strong> to build and deploy the app.</span></p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">To run and test the sample:</span></p>
<ul>
<li><span style="font-size:small">Press <strong>F5</strong> to build and deploy the app for SharePoint.</span>
</li></ul>
<p><span style="font-size:small">Data from the external data list will appear on the home page of the app.</span></p>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If the app fails to install, ensure that the group referenced in the BDC Metadata Model is correct.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/64b7d032-4b83-4e9e-bc08-f0a161af5457" target="_blank">Business Connectivity Services in SharePoint 2013</a></span>
<span style="font-size:small"></span>&nbsp; </li><li><span style="font-size:small"><a title="http://www.odata.org/" href="http://www.odata.org/" target="_blank">Open Data Protocol</a></span>
</li><li><span style="font-size:small"><a title="http://www.odata.org/developers/protocols/json-format" href="http://www.odata.org/developers/protocols/json-format" target="_blank">OData: JavaScript Object Notation (JSON) Format</a></span>
</li><li><span style="font-size:small"><a title="http://www.odata.org/developers/protocols/atom-format" href="http://www.odata.org/developers/protocols/atom-format" target="_blank">OData: AtomPub Format</a></span>
</li></ul>
<p>&nbsp;</p>
