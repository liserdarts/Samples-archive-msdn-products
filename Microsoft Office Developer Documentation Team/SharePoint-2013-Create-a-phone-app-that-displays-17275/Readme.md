# SharePoint 2013: Create a phone app that displays Maps for SharePoint 2013
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Mobile
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:50:10
## Description

<p><span style="font-size:small">SharePoint 2013 introduces a new field type named Geolocation that enables you to annotate SharePoint lists with location information. In columns of type Geolocation, you can enter location information as a pair of latitude
 and longitude coordinates in decimal degrees or retrieve the coordinates of the user&rsquo;s current location from the browser if it implements the W3C Geolocation API. In the list, SharePoint 2013 displays the location on a map powered by Bing Maps. Together,
 the Geolocation field and the Map View enable you to give a spatial context to any information by integrating data from SharePoint into a mapping experience, and let your users engage in new ways in your web and mobile apps and solutions.</span></p>
<h1>Description</h1>
<p><span style="font-size:small">This code sample contains two projects: GeoApp and GeoList. GeoApp is a client solution that can be built and run on client computers by using Visual Studio 2010 Express with the new SharePoint templates. GeoApp creates a mobile
 app that accesses GeoList and displays a location field on maps. GeoList is a server solution that runs and deploys on SharePoint Server. GeoList creates a SharePoint 2013 list that includes a Geolocation field.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2010 Express with the new SharePoint templates (to build and run the GeoApp solution)</span>
</li><li><span style="font-size:small">A SharePoint 2013 installation with administrative privileges</span>
</li><li><span style="font-size:small">A valid Bing Maps key added at the farm or web level</span>
</li><li><span style="font-size:small">Visual Studio 2012 on SharePoint Server (to build and run the GeoList solution)</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">WindowsPhoneAppForMaps project, which contains two solutions: GeoApp and GeoList</span>
</li><li><span style="font-size:small">GeoApp is the client solution, built with Visual Studio 2010 Express with the new SharePoint templates, which contains references to SharePoint Server and the list title</span>
</li><li><span style="font-size:small">GeoList is the server solution, built on Visual Studio, which creates a SharePoint list with a Geolocation column named Location</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the sample.</span></p>
<ol>
<li><span style="font-size:small">Set the site URL property of the project (GeoList). In the properties window of the solution, enter the URL for a SharePoint server.</span>
</li><li><span style="font-size:small">In the App.xaml.cs file of the GeoApp solution, replace the &lt;List Title&gt; element with the title of the list created by the GeoList solution.</span>
</li><li><span style="font-size:small">In the App.xaml.cs file of the GeoApp solution, replace the URL http://localhost with the URL of the home page of your SharePoint site.</span>
</li></ol>
<h1>Run and test the sample</h1>
<ul>
<li><span style="font-size:small">Choose the F5 key to build and run the app.</span>
</li></ul>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</span></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:640px; height:212px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Problem </span>
</strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Solution</span></strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small">While running the SharePoint List Wizard from Visual Studio 2010 Express, an error may occur if developer does not have sufficient privilege on SharePoint site.<br>
<img id="60999" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-88800202/image/file/60999/1/fig2sm.png" alt="" width="500" height="365"></span></td>
<td><span style="font-size:small">Give sufficient privilege to the user account with which developer is running the wizard.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">Form-based authentication error. <br>
<br>
<img id="60943" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-a619f634/image/file/60943/1/fig1.png" alt="" width="182" height="339"></span></td>
<td><span style="font-size:small">Form-based authentication is not enabled by default. To enable basic form-based authentication for the web application, follow these steps.</span>
<ul>
<li><span style="font-size:small">Navigate to Central Administration and ensure you have administrator rights on the server.</span>
</li><li><span style="font-size:small">Under <strong>Application Management</strong>, choose<strong>Manage Web Applications</strong>.</span>
</li><li><span style="font-size:small">Choose your web application (on which you have your SharePoint site, which you are accessing from your mobile app).</span>
</li><li><span style="font-size:small">From the ribbon, choose <strong>Authentication Providers</strong>.</span>
</li><li><span style="font-size:small">In the <strong>Authentication Provider </strong>
dialog box, choose <strong>Default</strong> to edit the authentication.</span> </li><li><span style="font-size:small">In the <strong>Edit Authentication Model </strong>
window under <strong>Claims Authentication Types</strong>, choose <strong>Basic Authentication</strong>.</span>
</li></ul>
</td>
</tr>
<tr valign="top">
<td><span style="font-size:small">The <strong>Specify location</strong>&nbsp;option is not visible when entering data from the mobile app.</span></td>
<td><span style="font-size:small">This behavior is by design. The Geolocation field has different behavior in the browser than in mobile apps.</span></td>
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
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1>&nbsp;</h1>
<h1>&nbsp;</h1>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/jj163813.aspx" target="_blank"><span style="font-size:small">How to: Integrate maps with Windows Phone apps and SharePoint 2013 lists</span>
</a></li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/jj163259.aspx" target="_blank">How to: Create a Windows Phone SharePoint 2013 list app</a></span>
</li><li><a href="http://msdn.microsoft.com/en-us/library/jj163786.aspx" target="_blank"><span style="font-size:small">Overview of Windows Phone SharePoint 2013 application templates in Visual Studio</span>
</a></li></ul>
