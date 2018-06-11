# SharePoint 2013: Use RESTful service to map airline flight and show status
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
* 2013-07-08 04:31:21
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Use RESTful service to map airline flight and show status</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>Demonstrates using a secure, cloud-based RESTful service to provide data to an app that displays the status and maps the route of a commercial airline flight.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample displays the status and maps the route of a commercial airline flight.</p>
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
<p>A FlightStats developer account identifier and key obtained from <a href="https://developer.flightstats.com" target="_blank">
developer.flightstats.com</a></p>
</li><li>
<p>A Bing Maps developer key obtained from <a href="http://www.bingmapsportal.com" target="_blank">
www.bingmapsportal.com</a></p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>The sample consists of a SharePoint-hosted app that calls the FlightStats API. After retrieving flight information, the route is plotted on a map using the Bing Maps service.
</p>
<p>The UI code is found in the <span value="App.js"><b><span class="keyword">App.js</span></b></span> library. The
<span value="wingtip.flightstatus.js"><b><span class="keyword">wingtip.flightstatus.js</span></b></span> library is used to obtain the flight information.
</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<ol>
<li>
<p>Obtain a FlightStats developer account identifier and key from <a href="https://developer.flightstats.com" target="_blank">
developer.flightstats.com</a>, and enter the identifier and key into the <span value="wingtip.flightstatus.js">
<b><span class="keyword">wingtip.flightstatus.js</span></b></span> library in the following lines:</p>
<p><span>var appId = &quot;&quot;, //Obtained from https://developer.flightstats.com</span>
</p>
<p><span>appKey = &quot;&quot;, //Obtained from https://developer.flightstats.com</span> </p>
</li><li>
<p>Obtain a Bing Maps developer key from <a href="http://www.bingmapsportal.com" target="_blank">
www.bingmapsportal.com</a>, and enter the key into the <span value="App.js"><b><span class="keyword">App.js</span></b></span> library in the following line:</p>
<p><span>var mapsKey = &quot;&quot;; //Obtained from http://www.bingmapsportal.com</span> </p>
</li></ol>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <span value="FlightStatus.sln"><b><span class="keyword">FlightStatus.sln</span></b></span> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the deployment URL to point to a site in your development environment.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When the app appears, it will load the airport codes.</p>
</li><li>
<p>When ready, select an airport, carrier, and enter a flight number and date.</p>
</li><li>
<p>Click the <b><span class="ui">Get Status</span></b> button.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>You must have a FlightStats application identifier and application key properly entered into the
<span value="wingtip.flightstatus.js"><b><span class="keyword">wingtip.flightstatus.js</span></b></span> library.</p>
<p>You must have a Bing Maps developer key properly entered into the <span value="App.js">
<b><span class="keyword">App.js</span></b></span> library.</p>
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
</li></ul>
</div>
</div>
</div>
