# SharePoint 2013: Custom REST service provides data to an app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-08-19 03:17:06
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Custom REST service provides data to an app for SharePoint</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates the use of a custom REST service to provide data to an app.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample displays a list of customers retrieved from a custom Representational State Transfer (REST) service.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample consists of an MCV4 web API custom RESTful service and a SharePoint-hosted app. The primary code is in the
<b><span class="keyword">CustomerOData</span></b> project. The <b><span class="keyword">CustomerController.cs</span></b> file contains the code for the simple RESTful service.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>No special configuration is required.</p>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <b><span class="keyword">RESTCustomers.sln</span></b> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the deployment URL to point to a site in your development environment.</p>
</li><li>
<p>Press F5 to start the REST service.</p>
</li><li>
<p>Right-click the <b><span class="keyword">CustomerApp</span></b> project, and select
<b><span class="ui">Debug</span></b> &gt; <b><span class="ui">Start New Instance</span></b>.</p>
</li><li>
<p>The app will start and display customer data from the RESTful service.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Be sure the RESTful service starts before starting the app.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5" name="collapseableSection"><b>
<div class="caption"></div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>August 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179887.aspx#Self_hosted" target="_blank">SharePoint 2013 provider-hosted apps</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142381.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142380.aspx" target="_blank">Get started with the SharePoint 2013 REST service</a>
</p>
</li><li>
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC4</a> </p>
</li></ul>
</div>
</div>
</div>
