# SharePoint 2013: Use the REST and CSOM Social APIs in apps for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* REST
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* social computing
## IsPublished
* True
## ModifiedDate
* 2014-06-13 03:30:03
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Use the REST and CSOM Social APIs in apps for SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>Demonstrates how to use the REST and CSOM Social APIs in a provider-hosted app for SharePoint.</p>
</div>
<div>
<h1>Description</h1>
<div id="sectionSection0">
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=be34f5b5-a1d1-47e1-971d-cfdda319992c" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The sample creates an app that tiles the pictures of people who are following, or are being followed by, the current user. The sample offers some simple filtering of the users based on characteristics or activities. Clicking an image displays that person's
 feed list.</p>
<p>There are two versions of the sample: Representational State Transfer (REST) and client object model (CSOM). Each version is designed as a provider-hosted app for SharePoint Online. Both versions of the sample interact with the following and feed portions
 of the Social API and request appropriate permissions. The sample can be used as a starting point for many social apps.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint Online Developer Site collection</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Basic familiarity with social capabilities and user profiles</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The Visual Studio solution <strong>PeoplePivots.sln</strong> contains both the REST and CSOM version of the app.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the sample app.</p>
<ol>
<li>
<p>Edit the <span><strong><span class="keyword">Site URL</span></strong></span> property for the
<strong><span class="ui">PeoplePivotsREST</span></strong> project to refer to your SharePoint Online environment.</p>
</li><li>
<p>Edit the <span><strong><span class="keyword">Site URL</span></strong></span> property for the
<strong><span class="ui">PeoplePivotsCSOM</span></strong> project to refer to your SharePoint Online environment.</p>
</li></ol>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Build the entire solution.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<ol>
<li>
<p>Select either <strong><span class="ui">PeoplePivotsREST</span></strong> or <strong>
<span class="ui">PeoplePivotsCSOM</span></strong> as the startup project.</p>
</li><li>
<p>Press F5.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>If the project does not deploy properly, make sure that the target environment is a developer-enabled site collection in SharePoint Online.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<p>&nbsp;</p>
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163280.aspx" target="_blank">Social and collaboration features in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163130.aspx" target="_blank">Follow people in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj193046.aspx" target="_blank">Social client class library</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/gg317460(v=office.14).aspx" target="_blank">SharePoint Online: An Overview for Developers</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142385.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></p>
</li></ul>
</div>
</div>
</div>
</div>
<div id="_mcePaste" class="mcePaste" style="left:-10000px; top:0px; width:1px; height:1px; overflow:hidden">
</div>
