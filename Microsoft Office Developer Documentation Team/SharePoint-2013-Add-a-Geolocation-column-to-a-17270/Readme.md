# SharePoint 2013: Add a Geolocation column to a list programmatically
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
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-03-20 11:54:20
## Description

<p><span style="font-size:small">SharePoint 2013 introduces a new field type named Geolocation that enables you to annotate SharePoint lists with location information. In columns of type Geolocation, you can enter location information as a pair of latitude
 and longitude coordinates in decimal degrees or retrieve the coordinates of the user&rsquo;s current location from the browser if it implements the W3C Geolocation API. (For more information on the Geolocation column, see
<a href="http://msdn.microsoft.com/en-us/library/jj163135(v=office.15).aspx" target="_blank">
Integrating location and map functionality in SharePoint 2013</a>.) The Geolocation column is not available by default in SharePoint lists. To add the column to a SharePoint list, you need to write code. This sample shows you how to add the Geolocation field
 to a list programmatically by using the SharePoint client object model.</span></p>
<h1>Description</h1>
<p><span style="font-size:small">The sample demonstrates how to programmatically add a Geolocation column to a SharePoint 2013 list. In the list, SharePoint 2013 displays the location on a map powered by Bing Maps. In addition, a new view named Map View displays
 the list items as pushpins on a Bing Maps Silverlight control with the list items as cards on the left pane. Figure 1 summarizes the default location and map features in SharePoint 2013 . Together, the Geolocation field and the Map View enable you to give
 spatial context to any information by integrating data from SharePoint into a mapping experience, and let your users engage in new ways in your web and mobile apps and solutions.</span></p>
<p><span style="font-size:small">Figure 1. Summarized view of the default location and map features</span></p>
<p><span style="font-size:small"><img id="60994" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-add-a-d3fa8288/image/file/60994/1/fig2.png" alt="" width="645" height="375"></span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2010</span> </li><li><span style="font-size:small">An installation of SharePoint 2013 with administrative privileges</span>
</li><li><span style="font-size:small">A valid Bing Maps key added at farm or web level</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The AddGeolocationFieldSample code sample contains the Program.cs file. Program.cs contains the logic of the console application, which deploys the Geolocation field to a specific list.</span></p>
<h1>Configure the sample</h1>
<p><span style="font-size:small">To configure the sample, make the following changes to Program.cs.</span></p>
<ol>
<li><span style="font-size:small">Replace the &quot;http://localhost&quot; server name with the URL of your SharePoint server.</span>
</li><li><span style="font-size:small">U</span><span style="font-size:small">pdate the &lt;List Title&gt; element with the title of the list name where you want to deploy the Geolocation column.</span>
</li></ol>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">&bull;&nbsp;Choose the F5 key to build and deploy the console application.</span></p>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following table lists one common configuration and environment error that might prevent the sample from running or deploying properly and how to solve it.</span></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:601px; height:135px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Problem </span>
</strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Solution</span></strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small">After adding an item to the list, Bing Maps shows an error.</span></td>
<td><span style="font-size:small">Set the Bing map key at farm or web level. To learn more how to set the Bing Maps key, see
<strong>How to: Set the Bing Maps key at the web and farm level in SharePoint 2013</strong>.</span></td>
</tr>
</tbody>
</table>
<h1><br>
<br>
<span style="font-size:small">&nbsp;</span></h1>
<h1><span style="font-size:small">&nbsp;</span></h1>
<h1><span style="font-size:small">&nbsp;</span><br>
<br>
<br>
</h1>
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj164050.aspx" target="_blank">How to: Add a Geolocation column to a list in SharePoint 2013 programmatically</a></span>
</li><li><a href="http://msdn.microsoft.com/en-us/library/jj163283.aspx" target="_blank"><span style="font-size:small">How to: Set the Bing Maps key at the web and farm level in SharePoint 2013</span>
</a></li></ul>
