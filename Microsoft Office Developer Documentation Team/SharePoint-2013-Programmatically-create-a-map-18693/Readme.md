# SharePoint 2013: Programmatically create a map view with Geolocation field type
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-06 03:15:29
## Description

<p><span style="font-size:small">SharePoint 2013 introduces a new field type named
<strong>Geolocation</strong> that enables you to annotate SharePoint lists with location information. For example, you can now make lists &quot;location-aware&quot; and display latitude and longitude coordinates through Bing Maps. An entry is typically seen as a pushpin
 on a map view. Together, the Geolocation field and the Map View enable you to give spatial context to any information by integrating data from SharePoint into a mapping experience, and let your users engage in new ways in your web and mobile apps and solutions,
 as shown in Figure 1.</span></p>
<p><strong><span style="font-size:small">Figure 1. A map view with different pushpin colors</span></strong></p>
<p><strong><span style="font-size:small">&nbsp;</span></strong><strong><span style="font-size:small"><img id="66062" src="/site/view/file/66062/1/fig1.png" alt="" width="706" height="511"></span></strong></p>
<h1>Description of the sample</h1>
<p><span style="font-size:small">This sample demonstrates how to programmatically create a map view to a SharePoint 2013 list. The SharePoint 2013 list displays the location on a map powered by Bing Maps. In addition, a new view named
<strong>Map View </strong>displays the list items as pushpins on Bing Maps, and a
<strong>Silverlight</strong> control with the list items as cards on the left pane.</span><br>
<span style="font-size:small">This sample is a console application that programmatically creates a map view of a SharePoint list.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint 2013, with administrative privileges</span>
</li><li><span style="font-size:small">A valid Bing Maps key added at the farm or web level</span>
</li><li><span style="font-size:small">A SharePoint list, with a Geolocation field type column added</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The CreateMapView code sample contains the Program.cs file. Program.cs contains the logic of the console application, which creates a map view of the SharePoint 2013 list with the
<strong>Geolocation</strong> field type. For more information about the new <strong>
Geolocation</strong> field type, see <a href="http://msdn.microsoft.com/en-us/library/jj163135(v=office.15).aspx" target="_blank">
Integrating location and map functionality in SharePoint 2013</a>.</span></p>
<h1>Configure the sample</h1>
<p><span style="font-size:small">To configure the sample, make the following changes to Program.cs.</span></p>
<ol>
<li><span style="font-size:small">Replace the placeholder &quot;&lt;Site Url&gt;&quot; with the URL of your SharePoint server.</span>
</li><li><span style="font-size:small">Update the placeholder &quot;&lt;List Title&gt;&quot; with the name of the list where you want to create a map view.</span>
</li></ol>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">&bull; Choose the F5 key to build and deploy the console application.</span></p>
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
<td><span style="font-size:small">Error message appears when you open a map view page:</span>
<p><strong><span style="font-size:small">This view no longer has a geolocation field, so it cannot be displayed in a map view.
</span></strong></p>
</td>
<td><span style="font-size:small">Add a Geolocation column to the list where you want to create a map view. To learn more how to add a Geolocation column to the SharePoint list, see
<a href="http://msdn.microsoft.com/en-us/library/jj164050.aspx" target="_blank">How to: Add a Geolocation column to a list programmatically in SharePoint 2013</a>.</span></td>
</tr>
</tbody>
</table>
<h1><br>
<br>
<span style="font-size:small">&nbsp;</span></h1>
<h1><span style="font-size:small">&nbsp;</span></h1>
<p><span style="font-size:small">&nbsp;</span></p>
<h1><br>
<br>
</h1>
<h1>Change log</h1>
<p><span style="font-size:small">First version: September 10, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj656773.aspx">Create a map view for the Geolocation field in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj164050.aspx">How to: Add a Geolocation column to a list programmatically in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163283.aspx">How to: Set the Bing Maps key at the web and farm level in SharePoint 2013</a></span>
</li></ul>
<p>&nbsp;</p>
