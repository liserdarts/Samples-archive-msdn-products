# SharePoint 2013: Set the Bing Maps key by using the client object model
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Mobile
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-06 03:00:18
## Description

<p><span style="font-size:small">You can set the Bing Maps key at the farm level or web level. To set the Bing Maps key at the farm level, you need administrator rights. You can set a farm-level Bing Maps key by using Windows PowerShell commands; you can set
 a web-level key by using a console application that uses the SharePoint client object model.</span></p>
<h1>Description</h1>
<p><span style="font-size:small">This code sample shows how to set a Bing Map key at the web level in SharePoint 2013. This code sample deploys Bing Map key through a console application.</span></p>
<h1>Prerequisites</h1>
<ul>
<li><span style="font-size:small">Visual Studio 2010</span> </li><li><span style="font-size:small">A SharePoint 2013 development environment, with administrative privileges</span>
</li><li><span style="font-size:small">A valid Bing Maps key, which you can get from <a href="http://bingmapsportal.com/" target="_blank">
Bing Maps Account Center</a>.</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">SetBingMapsKeys project contains the Program.cs file.</span>
</li><li><span style="font-size:small">Program.cs contains the logic that adds the Bing Maps key at the web level.</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the sample.</span></p>
<ol>
<li><span style="font-size:small">In Program.cs, replace the &quot;http://localhost&quot; server name with the URL of your SharePoint server.</span>
</li><li><span style="font-size:small">In the project properties, set the target framework as .NET Framework 4.0 or 3.5 and run the sample.</span>
</li></ol>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">After the Bing Maps key is deployed at the web level, you can create Geolocation fields that require the Bing Map key.</span></p>
<ul>
<li><span style="font-size:small">Pressthe F5 key to build and deploy the console application.</span>
</li></ul>
<h1>Change log</h1>
<p><span style="font-size:small">First version:&nbsp;July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163283.aspx" target="_blank">How to: Set the Bing Maps key at the web and farm level in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163135.aspx" target="_blank">Integrating location and map functionality in SharePoint 2013</a></span>
</li><li><a href="http://msdn.microsoft.com/en-us/library/jj164050.aspx" target="_blank"><span style="font-size:small">How to: Add a Geolocation column to a list in SharePoint 2013 programmatically</span>
</a></li></ul>
