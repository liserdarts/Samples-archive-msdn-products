# SharePoint 2013: Weather Report O365 SharePoint App
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* apps for SharePoint
## Topics
* apps for SharePoint
## IsPublished
* True
## ModifiedDate
* 2014-06-10 02:46:03
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Weather Report provider-hosted app for SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Learn how to use the MVC pattern to develop a provider-hosted app for SharePoint.</p>
</div>
<div>
<p><strong>Last modified: </strong>June 05, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#O15Readme_Prereq">Prerequisites</a> <br>
<a href="#sectionSection1">Sample for demonstration purposes only</a> <br>
<a href="#O15Readme_components">Key components</a> <br>
<a href="#O15Readme_config">Configure the sample</a> <br>
<a href="#O15Readme_build">Build and deploy the sample</a> <br>
<a href="#O15Readme_test">Run and test the sample</a> <br>
<a href="#O15Readme_Changelog">Change log</a> <br>
<a href="#O15Readme_RelatedContent">Related content</a></p>
<p><span>Provided by:</span> Todd Baginski, <a href="http://www.canviz.com" target="_blank">
Canviz Consulting </a></p>
<p>This sample shows how to use the model-view-controller (MVC) pattern to develop a provider-hosted app for SharePoint that uses MSN weather services to get weather forecast data.</p>
<strong>
<div class="caption">Figure 1. Weather Report app part</div>
</strong><br>
<strong></strong><img src="/site/view/file/116598/1/image.png" alt=""></div>
<h2>Prerequisites</h2>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer</p>
</li><li>
<p>A <a href="https://manage.windowsazure.com" target="_blank">Microsoft Azure account</a> with permissions to deploy a website</p>
</li><li>
<p>The Microsoft Azure SDK for .NET installed on your development computer</p>
</li></ul>
</div>
<h2>Sample for demonstration purposes only</h2>
<div id="sectionSection1">
<p>This sample does not conform to all the best practices that you should use in a production app. Specifically, be aware of the following:</p>
<ul>
<li>
<p>The app has limited exception handling.</p>
</li><li>
<p>In a production scenario, the OAuth access token should be saved following established best practices.</p>
</li></ul>
</div>
<h2>Key components</h2>
<div id="sectionSection2">
<p>&nbsp;</p>
<ul>
<li>
<p><strong>WeatherReport</strong> project, containing the AppManifest.xml file, which registers the provider-hosted app for SharePoint.</p>
</li><li>
<p><strong>WeatherReportMVCWeb</strong> project, which contains:</p>
<ul>
<li>
<p><strong>Views\Weather\Index.cshtml</strong>. Displays weather forecast data.</p>
</li><li>
<p><strong>Web.config</strong>. Stores the client id and client secret.</p>
</li></ul>
</li></ul>
</div>
<h2>Configure the sample</h2>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the <strong>WeatherReport.sln</strong> file in Visual Studio 2012 and select the
<strong>WeatherReport</strong> project.</p>
</li><li>
<p>In the <strong><span class="ui">Properties</span></strong> pane for the project, change the
<strong><span class="ui">Site URL</span></strong> property. It is the absolute URL of your SharePoint test site collection on Office 365: https://<span>&lt;my tenant&gt;</span>.sharepoint.com/sites/dev.</p>
</li></ol>
</div>
<h2>Build and deploy the sample</h2>
<div id="sectionSection4">
<p>Follow these steps to build and deploy the sample.</p>
<h3>To build and deploy the WeatherReportMVCWeb project to Microsoft Azure</h3>
<div>
<ol>
<li>
<p>Install the missing packages to the solution.</p>
<ol>
<li>
<p>Right-click the <strong><span class="ui">WeatherReport</span></strong> solution in
<strong><span class="ui">Solution Explorer</span></strong>, and choose <strong>
<span class="ui">Manage NuGet package for Solution</span></strong>.</p>
</li><li>
<p>Choose the <strong><span class="ui">Restore</span></strong> button (Figure 2).</p>
<strong>
<div class="caption">Figure 2. Choosing the Restore button installs the missing NuGet packages</div>
</strong><br>
<strong></strong><img src="/site/view/file/116597/1/image.png" alt=""> </li><li>
<p>After Visual Studio 2012 finishes installing the missing packages, close the window.</p>
</li></ol>
</li><li>
<p>Create an empty website on <a href="https://manage.windowsazure.com" target="_blank">
Microsoft Azure</a>, and download the publishing profile for that site.</p>
</li><li>
<p>Register an app at the <strong><span class="ui">/_layouts/15/appregnew.aspx</span></strong> page of your SharePoint test site collection on Office 365: https://<span>&lt;my tenant&gt;</span>.sharepoint.com/sites/dev/_layouts/15/appregnew.aspx. Be sure
 to fill in the following details:</p>
<ul>
<li>
<p>Generate a client ID and client secret. Save these two fields for later input.</p>
</li><li>
<p>Enter the URL of the website that you created on Microsoft Azure for <strong><span class="ui">App Domain</span></strong>.</p>
</li><li>
<p>Leave the <strong><span class="ui">Redirect URI</span></strong> field empty.</p>
</li></ul>
</li><li>
<p>Open the <strong>Web.config</strong> file. In the <strong><span class="keyword">appSettings</span></strong> section, assign values for the following keys:</p>
<ul>
<li>
<p><strong>ClientID</strong>: The value of the client ID that you created when you registered the app with Office 365.</p>
</li><li>
<p><strong>ClientSecret</strong>: The value of the client secret that you created when you registered the app with Office 365.</p>
</li></ul>
</li><li>
<p>Right-click the <strong><span class="ui">WeatherReportMvcWeb</span></strong> project in
<strong><span class="ui">Solution Explorer</span></strong>, and choose <strong>
<span class="ui">Publish&hellip;</span></strong>.</p>
</li><li>
<p>Follow the instructions to import the publishing profile of your Microsoft Azure site, and publish the project to Microsoft Azure.</p>
</li></ol>
</div>
<h3>To build and deploy the app for SharePoint</h3>
<div>
<ol>
<li>
<p>Right-click the <strong><span class="ui">WeatherReport</span></strong> project in
<strong><span class="ui">Solution Explorer</span></strong>, and choose <strong>
<span class="ui">Publish&hellip;</span></strong>.</p>
</li><li>
<p>For <strong><span class="ui">Which profile do you want to publish</span></strong>, enter
<span>WeatherReport</span> to create a publishing profile. Choose <strong><span class="ui">Next</span></strong>.</p>
</li><li>
<p>For <strong><span class="ui">Where is your website hosted</span></strong>, enter the location of the Microsoft Azure site where you published the
<strong><span class="ui">WeatherReportMVCWeb</span></strong> project.</p>
</li><li>
<p>For Client ID, enter the client ID value that you created when you registered the app with Office 365.</p>
</li><li>
<p>For Client Secret, enter the client secret value that you created when you registered the app with Office 365.</p>
</li><li>
<p>Choose <strong><span class="ui">Next</span></strong>, and then choose <strong>
<span class="ui">Finish</span></strong>.</p>
<p>The resulting app package file has an .app extension (<strong>WeatherReport.app</strong>) and is saved in the
<strong><span class="ui">app.publish</span></strong> subfolder of the <strong><span class="ui">bin\Debug</span></strong> folder of the Visual Studio solution.</p>
</li><li>
<p>In your browser, navigate to the home page of your Office 365 Developer Site. In the left panel, choose the
<strong><span class="ui">Apps in Testing</span></strong> link.</p>
</li><li>
<p>Choose <strong><span class="ui">new app to deploy</span></strong>, and follow the instructions to upload the WeatherReport.app package file and deploy it to your Developer Site.</p>
</li><li>
<p>Choose <strong><span class="ui">Trust It</span></strong>, and wait for the app to install.</p>
</li></ol>
</div>
</div>
<h2>Run and test the sample</h2>
<div id="sectionSection5">
<ol>
<li>
<p>In the web browser, navigate to the home page of your Office 365 Developer Site.</p>
</li><li>
<p>Edit the page, and add the <strong><span class="ui">Weather Report Client Web Part</span></strong> from the Web Part gallery.</p>
</li></ol>
</div>
<h2>Change log</h2>
<div id="sectionSection6"><strong>
<div class="caption"></div>
</strong>
<div>
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
<p>June 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h2>Related content</h2>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179933.aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a></p>
</li><li>
<p><a href="http://www.asp.net/mvc" target="_blank">Getting Started with ASP.NET MVC</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
