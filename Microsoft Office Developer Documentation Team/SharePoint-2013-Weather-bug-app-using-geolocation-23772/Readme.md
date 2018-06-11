# SharePoint 2013: Weather bug app using geolocation making cross-domain calls
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-07-08 06:17:00
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Weather bug app using geolocation making cross-domain calls</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>Demonstrates how to use HTML5 geolocation and JSON with Padding (JSONP) to make cross-domain calls from this app to a secured RESTful service in the cloud (WeatherBug).</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scot Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample app displays a weather forecast for the user's current location.</p>
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
<p>A developer account and key obtained from <a href="http://developer.weatherbug.com" target="_blank">
developer.weatherbug.com</a></p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>The sample consists of a SharePoint-hosted app that uses JavaScript Object Notation with Padding (JSONP) to make cross-domain calls to the WeatherBug API. The key code is found in the
<span value="wingtip.weatherbug.js"><b><span class="keyword">wingtip.weatherbug.js</span></b></span> library. The
<span value="wingtip.geolocation.js"><b><span class="keyword">wingtip.geolocation.js</span></b></span> library is used to obtain the user's current location. If permission to access geolocation data is denied, the app defaults to showing the weather forecast
 for Seattle, Washington.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>Obtain a developer key from <a href="http://developer.weatherbug.com" target="_blank">
developer.weatherbug.com</a>, and enter the key into the <span value="wingtip.weatherbug.js">
<b><span class="keyword">wingtip.weatherbug.js</span></b></span> library in the following line:</p>
<p><span>key = &quot;&quot;, //Obtain a key from http://developer.weatherbug.com</span> </p>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <span value="WeatherBug.sln"><b><span class="keyword">WeatherBug.sln</span></b></span> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the deployment URL to point to a site in your development environment.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When the app appears, you will be prompted to authorize access to your geolocation information.</p>
</li><li>
<p>If you authorize access, the weather forecast for your location will appear. Otherwise, the weather forecast for Seattle, Washington is displayed.
</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>The browser must support HTML5. (For compatibility information, see <a href="http://www.webresourcesdepot.com/html5-and-css3-browser-compatibility-chart/" target="_blank">
HTML5 And CSS3 Browser Compatibility Chart</a>.)</p>
<p>You must have a WeatherBug developer key properly entered into the <span value="wingtip.weatherbug.js">
<b><span class="keyword">wingtip.weatherbug.js</span></b></span> library.</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/fp179887.aspx#Self_hosted" target="_blank">SharePoint 2013 provider-hosted apps</a>
</p>
</li><li>
<p><a href="http://bing.com?q=HTML5" target="_blank">HTML5</a> </p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ee834511.aspx" target="_blank">JSON with Padding (JSONP)</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a>
</p>
</li></ul>
</div>
</div>
</div>
