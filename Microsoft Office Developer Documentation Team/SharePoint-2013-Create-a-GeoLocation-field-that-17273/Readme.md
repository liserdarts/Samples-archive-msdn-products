# SharePoint 2013: Create a GeoLocation field that renders maps using Nokia Maps
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
* 2013-02-06 02:19:37
## Description

<p><span style="font-size:small">SharePoint 2013 introduces a new field type named Geolocation that enables you to annotate SharePoint lists with location information. In columns of type Geolocation, you can enter location information as a pair of latitude
 and longitude coordinates in decimal degrees or retrieve the coordinates of the user's current location from the browser if it implements the W3C Geolocation API. For more information about Geolocation field, see
<a href="http://msdn.microsoft.com/en-us/library/jj163135.aspx">Integrating location and map functionality in SharePoint 2013</a>. The Geolocation field type is not available in the default content type of any list or document library in SharePoint 2013. To
 make this field type available, you need to develop and deploy custom field type controls to your SharePoint sites. The Geolocation field is not user-creatable by default in SharePoint 2013; you must programmatically add the Geolocation field type to SharePoint.
 For more information about how to add a Geolocation column programmatically, see
<a href="http://msdn.microsoft.com/en-us/library/jj164050.aspx">How to: Add a Geolocation column to a list programmatically in SharePoint 2013</a>. You can render four SharePoint list views (View, DisplayForm, EditForm, and NewForm) from Nokia maps, as shown
 in Figure 1.</span></p>
<p><strong><span style="font-size:small">Figure 1. Custom views of new custom field</span></strong></p>
<p><span style="font-size:small">&nbsp;</span><img id="60997" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-d9a91551/image/file/60997/1/fig2.png" alt="" width="645" height="506"></p>
<h1>Description</h1>
<p><span style="font-size:small">After the Geolocation field type is added to SharePoint 2013 Preview, it renders maps by using Bing Maps. By default, the Geolocation field can render only with Bing Maps. You can create a custom field based on Geolocation that
 provides its own rendering from Nokia Maps. Custom rendering is provided through the JSLink property in the client-side rendering framework, which is introduced in Microsoft SharePoint 2013 Preview. For more information about client-side rendering, see
<a href="http://msdn.microsoft.com/en-us/library/jj220061.aspx">How to: Customize a field type using client-side rendering</a>. This code sample demonstrates how to create a custom field by using Geolocation as parent, and also demonstrates how field values
 can be shown on Nokia Maps.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">A SharePoint 2013 development environment</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">NokiaMapsCustomField project</span>
<ul>
<li><span style="font-size:small"><strong>NokiaMapsControl.js</strong>&nbsp;&nbsp; The JavaScript file that provides the logic to render from Nokia Maps. This file needs to be in the LAYOUT folder, which is a mapped SharePoint folder.</span>
</li><li><span style="font-size:small"><strong>fldtypes_NokiaMapsControl.xml</strong>&nbsp;&nbsp; The XML file that stores the definition of the new custom field type. This file needs to be in the XML folder, which is a SharePoint mapped folder.</span>
</li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the sample.</span></p>
<ol>
<li><span style="font-size:small">Set the <strong>SiteUrl</strong> property of the project to the URL of your SharePoint server.</span>
</li><li><span style="font-size:small">Rebuild your solution to map all SharePoint mapped folders to your SharePoint server.</span>
</li></ol>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">Follow these steps to run and test the sample.</span></p>
<ol>
<li><span style="font-size:small">Choose the F5 key to build the solution.</span>
</li><li><span style="font-size:small">Deploy your solution.</span> </li><li><span style="font-size:small">Navigate to your SharePoint site, create a column in a SharePoint custom list, and choose the newly created custom field as the field type.</span>
</li><li><span style="font-size:small">Now add a new item in the list. You should be able to search for a location in the newly created extended Geolocation field.</span>
</li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</span></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:601px; height:212px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Problem </span>
</strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Solution</span></strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">Search box does not appear for field created from the extended Geolocation Nokia field type.</span></span></td>
<td><span style="font-size:small">Clear browser cache.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">Error message: <strong>Geolocation API is not supported</strong>.<br>
<br>
</span></td>
<td><span style="font-size:small">The Geolocation feature is supported only for browsers in which W3C Geolocation API is enabled.
</span></td>
</tr>
</tbody>
</table>
<h1><br>
<br>
<span style="font-size:small">&nbsp;</span><br>
<br>
<br>
</h1>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/bb861799%28v=office.14%29.aspx"><span style="font-size:small">Walkthrough: Creating a Custom Field Type</span>
</a></li></ul>
