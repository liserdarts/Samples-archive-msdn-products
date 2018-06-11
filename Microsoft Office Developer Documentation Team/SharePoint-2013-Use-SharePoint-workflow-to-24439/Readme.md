# SharePoint 2013: Use SharePoint workflow to support student housing app
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
* Workflows
## IsPublished
* True
## ModifiedDate
* 2013-08-19 03:16:45
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Use SharePoint workflow to support student housing app</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates how to use a SharePoint workflow and custom REST service to power an app that manages student housing applications.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample demonstrates a provider-hosted app written in ASP.NET MVC 4 using a SharePoint workflow that calls a custom Representational State Transfer (REST) service.</p>
<p>This sample simulates a college student applying for housing. The student provides some personal information and selects a housing facility. The SharePoint workflow calls the REST service to get the applicant's completed credit hours. The workflow then compares
 the completed credit hours with the required standing for the requested facility. Approval or rejection of the request is based on the applicant's current standing compared to the required standing.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>The sample is a provider-hosted app written using the <b><span class="ui">ASP.NET MVC 4 Web Application</span></b> project template in Visual Studio. The app consists of a user interface and a REST service. This demonstrates how a provider-hosted app can
 also host a REST service, which in turn can be called by a workflow defined in the app.
</p>
<p>The REST service in the sample assigns a random number of completed credits. The
<b><span class="keyword">HousingApplicationWeb</span></b> project is the MVC 4 application, which contains the key code in the
<b><span class="keyword">Controllers</span></b> folder. The <b><span class="keyword">HousingApplication</span></b> project contains the list definitions and the approval workflow.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>No special configuration is required. The REST call made from the workflow is hard-coded to call into the debugging URI of the
<b><span class="keyword">HousingApplicationWeb</span></b> project.</p>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <b><span class="keyword">HousingApplication.sln</span></b> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the <b><span class="keyword">Site URL</span></b> property to refer to a test site where you will deploy the solution.</p>
</li><li>
<p>Edit the <b><span class="keyword">web.config</span></b><b><span class="keyword">appSettings</span></b> to refer to your test certificate.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When prompted, grant permissions for the app.</p>
</li><li>
<p>When the app appears, you will see a welcome page.</p>
</li><li>
<p>Use the menu associated with the gear icon and select <b><span class="ui">Apply</span></b>.</p>
</li><li>
<p>Fill out the application and submit. The app then displays the <b><span class="ui">Status</span></b> page.</p>
</li><li>
<p>Refresh the <b><span class="ui">Status</span></b> page several times until the workflow finishes. You will see an acceptance or rejection notice.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>If the workflow is not called properly, check the hard-coded URI in the workflow.</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/jj163986.aspx" target="_blank">SharePoint 2013 workflows</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142380.aspx" target="_blank">Get started with the SharePoint 2013 REST service</a>
</p>
</li><li>
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC 4</a> </p>
</li></ul>
</div>
</div>
</div>
