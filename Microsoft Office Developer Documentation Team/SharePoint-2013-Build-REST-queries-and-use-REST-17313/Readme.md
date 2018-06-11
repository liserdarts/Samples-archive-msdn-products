# SharePoint 2013: Build REST queries and use REST to traverse a site
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* REST
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-03-06 01:03:59
## Description

<p><span style="font-size:small">The sample demonstrates how to read from and load data that conforms with the OData protocol to the REST endpoints where the list and list item entities are exposed. Additionally, it demonstrates how to parse Atom-formatted
 XML returned from these endpoints and how to construct JSON-formatted representations of basic SharePoint entities so that you can perform
<strong>Create</strong> and <strong>Update</strong> operations on them.</span></p>
<p><span style="font-size:small">The sample also demonstrates best practices for retrieving form digest and eTag values that are required for
<strong>Create </strong>and <strong>Update</strong> operations on lists and list items.</span></p>
<p><span style="font-size:small"><span style="color:black; line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt">For more information about the SharePoint REST APIs, see
</span><span style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a title="http://msdn.microsoft.com/library/d4b5c277-ed50-420c-8a9b-860342284b72.aspx" href="http://msdn.microsoft.com/library/d4b5c277-ed50-420c-8a9b-860342284b72.aspx" target="_blank"><span style="line-height:115%; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;; font-size:10pt">Programming
 using the SharePoint 2013 REST service</span></a></span><span style="color:black; line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt">. For more information about working with JSON, Atom, and OData, see
</span><span style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a title="http://www.odata.org/developers/protocols/json-format" href="http://www.odata.org/developers/protocols/json-format" target="_blank"><span style="line-height:115%; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;; font-size:10pt">OData:
 JavaScript Object Notation (JSON) Format</span></a></span><span style="color:black; line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"> and
</span><span style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a title="http://www.odata.org/developers/protocols/atom-format" href="http://www.odata.org/developers/protocols/atom-format" target="_blank"><span style="line-height:115%; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;; font-size:10pt">OData:
 AtomPub Format</span></a></span><span style="color:black; line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt">.</span></span></p>
<p><span style="font-size:small">The code that uses the REST APIs and that builds the interface is located in the Home.aspx.cs file of the RESTHelperAppWeb project.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">A SharePoint 2013 development environment that is configured for app isolation and OAuth</span>
</li><li><span style="font-size:small">Visual Studio 2012 and SharePoint development tools in Visual Studio 2012 installed on your developer computer</span>
</li><li><span style="font-size:small">Basic familiarity with RESTful web services</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample app contains the following:</span></p>
<ul>
<li><span style="font-size:small">RESTHelperApp project, which contains the AppManifest.xml file</span>
</li><li><span style="font-size:small">RESTHelperAppWeb project, which contains the following:</span>
<ul>
<li><span style="font-size:small">Home.aspx file, which contains the HTML and ASP.NET controls for the app&rsquo;s user interface</span>
</li><li><span style="font-size:small">Home.aspx.cs file, which contains the C# code that uses the REST APIs to read and write data</span>
</li><li><span style="font-size:small">web.config file</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">To configure the Build REST queries and use REST to traverse a site sample app, update the
<strong>SiteUrl</strong> property of the solution with the URL of the home page of your SharePoint 2013 Preview site.</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press the F5 key to build and deploy the app.</span></p>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Choose <strong>Trust It</strong> on the consent page to grant permissions to the app.</span>
</li><li><span style="font-size:small">Use the app&rsquo;s interface to drill into the parent web&rsquo;s site structure and find basic SharePoint entities, such as webs and lists, and the endpoints where those entities are exposed. You can also jump directly to
 pages that display the endpoints for all of the parent web&rsquo;s lists and users.</span>
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
<td><span style="font-size:small">Visual Studio does not open the browser after you press the F5 key.</span></td>
<td><span style="font-size:small">Set the app for SharePoint project as the startup project.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">HTTP error 405 <strong>Method not allowed</strong>.</span></td>
<td><span style="font-size:small">Locate the applicationhost.config file in <em>%userprofile%</em>\Documents\IISExpress\config.</span>
<p><span style="font-size:small">Locate the handler entry for <strong>StaticFile</strong>, and add the verbs
<strong>GET</strong>, <strong>HEAD</strong>, <strong>POST</strong>, <strong>DEBUG</strong>, and
<strong>TRACE</strong>.</span></p>
</td>
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
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/d4b5c277-ed50-420c-8a9b-860342284b72.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/bc37ff5c-1285-40af-98ae-01286696242d.aspx" target="_blank">How to: Access SharePoint 2013 data from your remote app using the cross-domain library</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/1534a5f4-1d83-45b4-9714-3a1995677d85.aspx" target="_blank">Work with data in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/2148980b-c2b6-4294-b8f7-cfc07f925091.aspx" target="_blank">Data access options for apps in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><span style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a title="http://www.odata.org/" href="http://www.odata.org/" target="_blank"><span style="line-height:115%; font-family:&quot;Arial&quot;,&quot;sans-serif&quot;; font-size:10pt">Open
 Data Protocol</span></a></span></span> </li><li><span style="font-size:small"><a title="http://www.odata.org/developers/protocols/json-format" href="http://www.odata.org/developers/protocols/json-format" target="_blank">OData: JavaScript Object Notation (JSON) Format</a>
</span></li><li><span style="font-size:small"><a href="http://www.odata.org/developers/protocols/atom-format" target="_blank">OData: AtomPub Format</a></span>
</li></ul>
