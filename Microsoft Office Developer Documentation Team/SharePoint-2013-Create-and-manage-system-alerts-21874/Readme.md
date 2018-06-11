# SharePoint 2013: Create and manage system alerts
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-04-26 01:18:12
## Description

<div id="header">Demonstrates how to create and manage a list of alerts that you can then display in the host web. This allows, for example, the IT department to manage a set of alerts and then display them to users programmatically.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p><span class="label">Provided by: </span><a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">Scott Hillier</a>,
<a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample uses an announcements list to manage a list of alert messages and expiration dates. We use an app part to display the currently active system alerts in the host web. The app is constructed as an
<a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC4</a> app that is deployed to SharePoint online as an autohosted app.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>An Office 365 Developer Site</p>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection1">
<p>This sample provides the following central components:</p>
<ul>
<li>
<p>The sample is an autohosted app that uses the Visual Studio MVC4 project template as the development starting point.</p>
</li><li>
<p>The alerts list is a SharePoint announcements list hosted in the hidden <a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.spweb.aspx" target="_blank">
SPWeb</a> instance that is associated with the app.</p>
</li><li>
<p>The main page of the MVC4 app displays the list of alerts and provides a link that lets you manage the alerts.</p>
</li><li>
<p>There is a client app part that refers to a specific view in the MVC4 app. The view displays the currently active alerts and is formatted to match the styling of the host web.</p>
</li><li>
<p>The key code for the sample can be found in the <span><span class="keyword">AlertsController.cs</span></span> class, which is associated with the
<span><span class="keyword">Index.cshtml</span></span> view and the <span><span class="keyword">AppPart.cshtml</span></span> view.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection2">
<p>The app requires that you have an Office 365 Developer Site available. If you don't have one, you can
<a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">sign up for an Office 365 Developer Site</a>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection3">
<div class="subSection">
<ol>
<li>
<p>Open SystemAlerts.sln in Visual Studio 2012.</p>
</li><li>
<p>Edit the Site URL property so that it refers to the test site where you will deploy the solution.</p>
</li><li>
<p>Press F5 to build the sample.</p>
</li><li>
<p>When prompted, grant permissions for the app.</p>
</li><li>
<p>When the app appears, you will see the alerts list pre-populated with several alerts.</p>
</li><li>
<p>Click the <span class="ui">Manage Alerts</span> link to add, edit, or delete alerts.</p>
</li><li>
<p>Navigate to the host web and place that page into <span><span class="keyword">Edit</span></span> mode.</p>
</li><li>
<p>Select the option to insert an <span class="ui">App Part</span>.</p>
</li><li>
<p>Add the System Alerts app part to the page. You should see the currently active alerts nicely formatted on the page.</p>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection4">
<p>Your app will not deploy unless the target site is an Office 365 Developer Site.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection5">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
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
<p>April 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC 4</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179886.aspx" target="_blank">How to: Create a basic autohosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li></ul>
</div>
</div>
</div>
