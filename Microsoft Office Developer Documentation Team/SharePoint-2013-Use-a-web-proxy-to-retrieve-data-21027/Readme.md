# SharePoint 2013: Use a web proxy to retrieve data from an external data source
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-28 05:17:54
## Description

<p id="header">This SharePoint-hosted app uses the web proxy architecture to retrieve data from an external data source (MusicBrainz.org) using client-side JavaScript.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=52a3f2aa-710f-4496-9b78-f240eccc74ad" target="_blank">Ted Pattison</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The WebProxyMusicBrainzDemo sample project demonstrates writing client-side JavaScript code using the
<span><span class="keyword">SP.WebProxy.invoke</span></span> method to make Representational State Transfer (REST) calls to the MusicBrainz.org web service. The data is returned from MusicBrainz.org in JavaScript Object Notation (JSON) format and parsed into
 an HTML table using the <span><span class="keyword">jsRender</span></span> library. The sample project also uses both the jQuery and the jQuery UI libraries to create the user experience.</p>
<div>Key features illustrated in the sample:</div>
<ul>
<li>
<div>Using the <span><span class="keyword">SP.WebProxy.invoke</span></span> method to retrieve data from an external data source</div>
</li><li>
<div>Using the <span><span class="keyword">jsRender</span></span> library to build an HTML table from a JSON result</div>
</li><li>
<div>Creating user interface experiences using jQuery UI tabs</div>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint 2013 development environment using Office 365 or a local SharePoint farm</div>
</li><li>
<div>SharePoint farm must be configured to support apps for SharePoint</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<div>The sample consists of a Visual Studio 2012 project for a SharePoint-hosted app named
<strong>WebProxyMusicBrainzDemo</strong>.</div>
</li><li>
<div>The start page includes a parent &lt;div&gt; element that is used to host jQuery UI tabs.</div>
<div>Each tab has a different musical artist. Whenever the user clicks a tab, the app makes a call to the MusicBrainz.org web service to return a set of albums associated with that artist, and this set of albums is displayed to the user.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample.</div>
<ul>
<li>
<div>Make sure that you have a development environment with Office 365 or a local SharePoint farm.</div>
</li><li>
<div>Make sure that this SharePoint farm is configured to support apps for SharePoint.</div>
</li></ul>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<div class="subSection">
<ol>
<li>
<div>Open the solution <span class="ui">WebProxyMusicBrainzDemo</span> project.</div>
</li><li>
<div>Configure the project's <span><span class="keyword">Site Url</span></span> property to point to a SharePoint 2013 test site.</div>
</li><li>
<div>Press F5 to test the app in the debugger.</div>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection5">
<p>If the app fails to install, troubleshoot the following aspect of your development environment:</p>
<p>Make sure your environment supports apps. In Visual Studio 2012, create a new SharePoint-hosted app, and ensure you can deploy it in a test site on your farm. If you cannot, your environment is not configured to support apps for SharePoint.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj850796.aspx" target="_blank">SP.WebProxy object</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179893.aspx" target="_blank">Work with data in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
