# SharePoint 2013: Implement SharePoint Machine Translation Services
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
* data and storage
## IsPublished
* True
## ModifiedDate
* 2014-05-13 04:32:12
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Implement SharePoint Machine Translation Services</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>Demonstrates how to implement the SharePoint 2013<a href="http://msdn.microsoft.com/en-us/library/jj163145.aspx" target="_blank">Machine Translation Services</a> in a provider-hosted app for SharePoint that will translate a Microsoft Word file.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scott Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample is written as a provider-hosted app that uses <a href="http://www.asp.net/mvc/mvc4" target="_blank">
ASP.NET MVC4</a> and the managed client object model. The sample places a <b><span class="ui">Translate</span></b> button in the
<b><span class="ui">Manage</span></b> group of the <b><span class="ui">Files</span></b> tab on document libraries in the host web. Clicking the
<b><span class="ui">Translate</span></b> button invokes the translation service and translates an entire library. The app also places a
<b><span class="ui">Translate</span></b> menu item in the edit control block to use for translating a single document.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>The <a href="http://msdn.microsoft.com/en-us/library/jj163145.aspx" target="_blank">
Machine Translation Services</a> properly set up and configured</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>The sample is a provider-hosted app written in C# using the managed client object mode (CSOM). The app is structured using the Model-View-Controller (MVC) design pattern using
<a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC4</a>.</p>
<p>The key code can be found in the <span value="TranslateController"><b><span class="keyword">TranslateController</span></b></span> class, which is implemented in the source file TranslateController.cs. The
<span value="TranslateController"><b><span class="keyword">TranslateController</span></b></span> class provides all of the translation functionality and settings management.</p>
<p>The <span value="Index()"><b><span class="keyword">Index()</span></b></span> and
<span value="SaveSetting()"><b><span class="keyword">SaveSetting()</span></b></span> methods work together to read and write settings for the app. You can select a target translation language and specify the name of a library in the host web where translated
 documents will be placed. This is a good demonstration of app setting management.</p>
<p>The <span value="File()"><b><span class="keyword">File()</span></b></span> method contains the code to translate a single file synchronously. The
<span value="Library()"><b><span class="keyword">Library()</span></b></span> method contains the code to translate an entire document library asynchronously.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The app requires that you provide the following certificate information:</p>
<ul>
<li>
<p><span value="ClientSigningCertificatePath"><b><span class="keyword">ClientSigningCertificatePath</span>
</b></span>- the path to your test certificate .pfx file</p>
</li><li>
<p><span value="ClientSigningCertificatePassword"><b><span class="keyword">ClientSigningCertificatePassword</span>
</b></span>- the password for your test certificate</p>
</li><li>
<p><span value="IssuerId"><b><span class="keyword">IssuerId</span> </b></span>- the Issuer ID for your test certificate</p>
</li></ul>
<p>You must also create a document library in the host web and name it &quot;Translated Documents&quot;.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p></p>
<div>
<ol>
<li>
<p>Open TranslateDocApp.sln in Visual Studio 2012.</p>
</li><li>
<p>Edit the <span value="SiteURL"><b><span class="keyword">SiteURL</span></b></span> property to refer to a test site where you will deploy the solution.</p>
</li><li>
<p>Edit the web.config file to provide your test certificate information.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When prompted, grant the app permissions.</p>
</li><li>
<p>When the app appears, select a language from the drop-down list.</p>
</li><li>
<p>Edit the destination library name, if required.</p>
</li><li>
<p>Navigate to a library in the host web.</p>
</li><li>
<p>Using the edit control block, select <b><span class="ui">Translate</span></b> for a single document.</p>
</li><li>
<p>Using the <b><span class="ui">File</span></b> tab on the ribbon, click the <b>
<span class="ui">Translate</span></b> button to process the entire library.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>The Machine Translation Service will not run on a domain controller.</p>
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
<p>May 2013</p>
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
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC 4</a> </p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163145.aspx" target="_blank">Machine Translation Services in SharePoint 2013</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ff798388.aspx" target="_blank">Using the Client Object Model</a>
</p>
</li><li>
<p><a href="http://blogs.msdn.com/b/steve_fox/archive/2013/02/18/building-your-first-provider-hosted-app-for-sharepoint-using-windows-azure-part-1.aspx" target="_blank">Building your first Provider-Hosted App for SharePoint using Microsoft Azure - Part 1</a>
</p>
</li></ul>
</div>
</div>
</div>
