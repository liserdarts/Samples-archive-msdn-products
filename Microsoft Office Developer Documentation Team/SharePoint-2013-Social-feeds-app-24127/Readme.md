# SharePoint 2013: Social feeds app
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
* 2013-07-31 12:16:23
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Social feeds app</span> </td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates using the client object model (CSOM) to access social feeds in an app.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample presents a columnar view of key social feed information similar to the &quot;Tweet Deck&quot; application for Twitter. The app has three columns, &quot;Timelines&quot;, &quot;Interactions&quot;, and &quot;People&quot;. The Timelines column shows the current timeline for the current
 user and followed people. The Interactions column shows &quot;@&quot; mentions for the current user. The People column shows the people followed by the current user.</p>
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
<p>The sample is a SharePoint-hosted app using CSOM to access the social feed. The primary code is in the
<b><span class="keyword">wingtip.social.js</span></b> library.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The sample requires no special configuration; however, you may want to create some test activities and followers to experience the full functionality of the app.</p>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <b><span class="keyword">FeedDeck.sln</span></b> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the <b><span class="keyword">Site URL</span></b> property to refer to a test site where you will deploy the solution.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When prompted, grant permissions for the app.</p>
</li><li>
<p>When the app appears, you will see the three columns displaying social feed information.</p>
</li><li>
<p>Use the app to post a new message to the feed.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>If you receive errors, navigate to the newsfeed in your personal site to initialize the newsfeed infrastructure.</p>
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
<p>July 2013</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app for SharePoint</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142384.aspx" target="_blank">App-only permissions</a>
</p>
</li><li>
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC4</a> </p>
</li></ul>
</div>
</div>
</div>
