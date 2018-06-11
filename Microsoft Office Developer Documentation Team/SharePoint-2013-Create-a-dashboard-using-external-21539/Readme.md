# SharePoint 2013: Create a dashboard using external content types in an app
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-04-03 01:54:32
## Description

<div id="header">Demonstrates how to use multiple external content types in an app for SharePoint to create a dashboard.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span>&nbsp;&nbsp;&nbsp;<a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=be34f5b5-a1d1-47e1-971d-cfdda319992c" target="_blank">Scot Hillier</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>This sample uses the publicly available <a href="http://services.odata.org/Northwind/Northwind.svc" target="_blank">
Northwind OData source</a>. The Categories entity and Category Sales entities are used as the basis for external content types and external lists. These lists are accessed programmatically using both REST and client object model (CSOM) approaches, which are
 demonstrated in three different versions of the app.</p>
<p>The three different versions of the app demonstrate JavaScript/REST, JavaScript/CSOM, and C#/CSOM. In the JavaScript versions, the code that accesses the data is located in the
<span><span class="keyword">northwind.query.js</span></span> library. In the C# version, the code that accesses the data is in the Query.ca class.</p>
<p>Figure 1 shows the app running with filter criteria in a list and sales figures in a table.</p>
<div class="caption">Dashboard app showing filter criteria and sales figures</div>
<br>
<img id="79334" src="/site/view/file/79334/1/readmeImage.png" alt="" width="709" height="394">
<p>The <strong>JavaScript/REST version</strong> of the sample demonstrates how to read data from an external list that is backed by an OData source. Additionally, it demonstrates how to use jQuery Deferreds to separate the business logic and data layers of
 the app by implementing the Promises pattern. Finally, it demonstrates the use of the Knockout library to separate the user interface view from the underlying data by implementing the Model-View-ViewModel (MVVM) pattern.</p>
<p>The <strong>JavaScript/CSOM</strong> version of the sample demonstrates how to read data directly from an external content type without using an external list as an intermediate layer. This version of the sample also implements the Promises and MVVM patterns.</p>
<p>The <strong>C#/CSOM version</strong> of the sample demonstrates how to read data directly from an external content type without using an external list as an intermediate layer. This version also demonstrates the creation of a provider-hosted app that uses
 ASP.NET and server-side code.</p>
<p>For more information about the SharePoint REST APIs, see <a href="http://msdn.microsoft.com/en-us/library/sharepoint/ff521587.aspx" target="_blank">
SharePoint Foundation REST Interface</a>. For more information about working with JavaScript Object Notation (JSON), Atom, and OData, see
<a href="http://www.odata.org/documentation/json-format" target="_blank">OData: JavaScript Object Notation (JSON) Format</a> and
<a href="http://www.odata.org/documentation/atom-format" target="_blank">OData: AtomPub Format</a>.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment that is configured for app isolation and OAuth</p>
</li><li>
<p>Visual Studio 2012 and Microsoft Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Basic familiarity with Business Connectivity Services (BCS)</p>
</li><li>
<p>Basic familiarity with RESTful web services.</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The JavaScript/REST version of the sample is located in the BCSDashboardREST solution and the following key libraries:</p>
<ul>
<li>
<p><span><span class="keyword">northwind.query.js</span></span>: contains the code for accessing the external lists</p>
</li><li>
<p><span><span class="keyword">northwind.viewmodel.js</span></span>: contains the code for implementing the view model</p>
</li></ul>
<p>The JavaScript/CSOM version of the sample is located in the BCSDashboardJSOM solution:</p>
<ul>
<li>
<p><span><span class="keyword">northwind.query.js</span></span>: contains the code for accessing the external content types</p>
</li><li>
<p><span><span class="keyword">northwind.viewmodel.js</span></span>: contains the code for implementing the view model</p>
</li></ul>
<p>The C#/CSOM version of the sample is located in the BCSDashboardCSOM solution:</p>
<ul>
<li>
<p><span><span class="keyword">Query.cs</span></span>: contains the code for accessing the external content types</p>
</li><li>
<p><span><span class="keyword">Default.aspx.cs</span></span>: contains the code for implementing the view</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<ol>
<li>
<p>Update the <span><span class="keyword">SiteUrl</span></span> property of the solution with the URL of the home page of your SharePoint 2013 site.</p>
</li><li>
<p>Update the <span><span class="keyword">StartPage</span></span> element inside the
<span class="ui">Properties</span> node of the AppManifest.xml file to the value of the
<span><span class="keyword">localhost:&lt;port number&gt;</span></span> URL for the Internet Information Services (IIS) Express website that your web application is using for F5 debugging.</p>
<p>All versions of the sample use the internal principal and should run directly from Visual Studio with no additional configuration.</p>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Press F5 to build and deploy the app.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Use the app's interface to select product categories and view the sales data for the selected categories.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<p>If the app fails to install, make sure that the XML in your AppManifest.xml file and Web.config file parses correctly, and verify that the values that you have added to both files are correct.</p>
</li><li>
<p>If the app fails at run time, make sure that you have Internet access to reach the Northwind sample endpoint.</p>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142385.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/sharepoint/ff521587.aspx" target="_blank">SharePoint Foundation REST Interface</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163782.aspx" target="_blank">Business Connectivity Services</a></p>
</li><li>
<p><a href="http://www.odata.org/" target="_blank">Open Data Protocol</a></p>
</li><li>
<p><a href="http://www.odata.org/documentation/json-format" target="_blank">OData: JavaScript Object Notation (JSON) Format</a></p>
</li><li>
<p><a href="http://www.odata.org/documentation/atom-format" target="_blank">OData: AtomPub Format</a></p>
</li></ul>
</div>
</div>
</div>
</div>
