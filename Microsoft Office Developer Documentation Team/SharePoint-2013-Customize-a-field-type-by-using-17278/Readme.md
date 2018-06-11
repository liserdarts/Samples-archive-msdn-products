# SharePoint 2013: Customize a field type by using client-side rendering
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
* User Experience
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:46:52
## Description

<p><span style="font-size:small">This sample demonstrates how to customize the rendering process for a custom field type in SharePoint 2013 . You can provide custom logic to control the rendering process of the field when it is displayed in the View, Edit,
 New, and Display forms.</span></p>
<p><span style="font-size:small">The JavaScript code that controls the rendering process is in the CSRFieldType.js file.</span></p>
<p><span style="font-size:small">Figure 1 shows the custom field in the View form.</span></p>
<p><strong><span style="font-size:small">Figure 1. Custom client-side rendered field in a View form</span></strong></p>
<p><span style="font-size:small"><img id="60495" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-customize-0c9698e1/image/file/60495/1/fig2.png" alt="" width="627" height="361"></span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">SharePoint 2013 development environment</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">FavoriteColorFieldType project, which contains these files:</span>
</li><li><span style="font-size:small">fldtypes_FavoriteColorFieldType.xml, which contains the custom field type definition</span>
</li><li><span style="font-size:small">FavoriteColorFiledType.cs, which contains the class declaration for the custom field type</span>
</li><li><span style="font-size:small">CSRFieldType.js, which contains the rendering logic</span>
</li><li><span style="font-size:small">CustomFieldList list definition, which contains a reference to the FavoriteColorFieldType custom field</span>
</li></ul>
<h1>Configure the sample</h1>
<ul>
<li><span style="font-size:small">Update the SiteUrl property of the solution with the URL of your SharePoint website.</span>
</li></ul>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Press F5 to build and deploy the solution.</span>
</li><li><span style="font-size:small">Click CustomFieldList in the left navigation pane.</span>
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
<td><span style="font-size:small">Field type FavoriteColorField is not installed properly. Go to the list settings page to delete this field.</span></td>
<td><span style="font-size:small">Execute the following command from an elevated command prompt:
<strong>iisreset /noforce</strong>.<br>
<br>
<strong>! Caution</strong><br>
If you are deploying the solution to a production environment, wait for an appropriate time to reset the web server using
<strong>iisreset /noforce</strong>.</span></td>
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
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06">SharePoint 2013 development overview</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/08e4e4e1-d960-43fa-85df-f3c279ed6927">Start: Set up the development environment for SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/061985ec-6129-4e91-991b-a72488ce1d34">Develop in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/8d5cabb2-70d0-46a0-bfe0-9e21f8d67d86">How to: Customize a list view in an app for SharePoint using client-side rendering</a></span>
</li></ul>
