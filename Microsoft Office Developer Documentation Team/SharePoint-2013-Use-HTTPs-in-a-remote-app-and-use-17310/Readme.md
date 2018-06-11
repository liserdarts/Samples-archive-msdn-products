# SharePoint 2013: Use HTTPs in a remote app and use OAuth for callbacks
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
* Authentication
* Cloud
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:21:01
## Description

<p><span style="font-size:small">The code that uses the REST API and the CSOM is located in the Home.aspx.cs file of the RemoteAppUsingHttpsWeb project. The sample uses HTTPS. It reads and displays OAuth tokens. It also reads data that conforms with the OData
 protocol from the REST endpoints where the basic SharePoint entities, such as users and site information, are exposed. It also reads data using the SharePoint CSOM.</span></p>
<p style="padding-left:30px"><strong><span style="font-size:small">Note</span></strong><br>
<span style="font-size:small">For more information about the SharePoint REST APIs, see
<a href="http://msdn.microsoft.com/en-us/library/d4b5c277-ed50-420c-8a9b-860342284b72">
Programming using the SharePoint 2013 REST service</a>. For more information about working with JSON, Atom, and OData, see
<a href="http://www.odata.org/developers/protocols/json-format">OData: JavaScript Object Notation (JSON) Format</a> and
<a href="http://www.odata.org/developers/protocols/atom-format">OData: AtomPub Format</a>.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small"><span style="font-size:small">You must know how to create a provider-hosted app for SharePoint. For more information about how to create a provider-hosted app for SharePoint, see
<a href="http://msdn.microsoft.com/en-us/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b">
How to: Create a basic provider-hosted app for SharePoint</a>.</span></span> </li><li><span style="font-size:small">You must know how to use a certificate.</span> </li><li><span style="font-size:small">A SharePoint 2013 development environment that is configured for app isolation and OAuth.</span>
</li><li><span style="font-size:small">Visual Studio 2012 and SharePoint development tools in Visual Studio 2012.</span>
</li><li><span style="font-size:small">Basic familiarity with REST web services.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample app contains the following:</span></p>
<ul>
<li><span style="font-size:small">The RemoteAppUsingHttps project, which contains the AppManifest.xml file.</span>
</li><li><span style="font-size:small">The RemoteAppUsingHttpsWeb project, which contains:</span>
<ul>
<li><span style="font-size:small">The Home.aspx file, which contains the HTML and ASP.NET controls for the user interface of the app.</span>
</li><li><span style="font-size:small">The Home.aspx.cs file, which contains the C# code that uses the CSOM and REST to read and write data.</span>
</li><li><span style="font-size:small">The web.config file.</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">To configure the basic data operations sample app:</span></p>
<ul>
<li><span style="font-size:small">Update the <strong>SiteUrl</strong> property of the solution with the URL of the home page of your SharePoint 2013 site.</span>
</li></ul>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press <strong>F5</strong> to build and deploy the app.</span></p>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Press F5 to build and deploy the app.</span> </li><li><span style="font-size:small">On the consent page, choose <strong>Trust It </strong>
to grant permissions to the app.</span> </li></ol>
<h1>Example</h1>
<p><span style="font-size:small">Figure 1 shows the sample URL you see when you launch the app.&nbsp;</span></p>
<p><strong><span style="font-size:small">Figure 1. Start page URL of the app after you launch it</span></strong></p>
<p><span style="font-size:small"><img id="60397" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-https-dc7227a7/image/file/60397/1/fig1sm.gif" alt="" width="650" height="19"></span></p>
<p><span style="font-size:small">Figure 2 shows an example of the kinds of information that this sample app reads and displays.</span></p>
<p><strong><span style="font-size:small">Figure 2. View of the populated page</span></strong></p>
<p><span style="font-size:small"><img id="60398" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-https-dc7227a7/image/file/60398/1/fig1.gif" alt="" width="377" height="734"></span></p>
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
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b" href="http://msdn.microsoft.com/en-us/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b">How to: Create a basic provider-hosted app for SharePoint</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/5f7a8440-3c09-4cf8-83ec-c236bfa2d6c4" href="http://msdn.microsoft.com/en-us/library/5f7a8440-3c09-4cf8-83ec-c236bfa2d6c4">App permissions in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/526c8c4a-5cbb-4efc-87d9-23ac73655cf4" href="http://msdn.microsoft.com/en-us/library/526c8c4a-5cbb-4efc-87d9-23ac73655cf4">OAuth authentication and authorization flow for cloud-hosted
 apps</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/cf2bdd88-4b04-47f6-a876-322f734a6af2" href="http://msdn.microsoft.com/en-us/library/cf2bdd88-4b04-47f6-a876-322f734a6af2">Tips and FAQs: OAuth and remote apps for SharePoint
 2013</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/124879c7-a746-4c10-96a7-da76ad5327f0" href="http://msdn.microsoft.com/en-us/library/124879c7-a746-4c10-96a7-da76ad5327f0">App authorization policy types</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06" href="http://msdn.microsoft.com/en-us/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06">SharePoint 2013 development overview</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/d07e0a13-1e74-4128-857a-513dedbfef33" href="http://msdn.microsoft.com/en-us/library/d07e0a13-1e74-4128-857a-513dedbfef33">Get started developing apps for SharePoint</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/1b992485-6efe-4ea4-a18c-221689b0b66f" href="http://msdn.microsoft.com/en-us/library/1b992485-6efe-4ea4-a18c-221689b0b66f">How to: Create a basic SharePoint-hosted app</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/bde5647a-fff1-4b51-b67b-2139de79ce4a" href="http://msdn.microsoft.com/en-us/library/bde5647a-fff1-4b51-b67b-2139de79ce4a">Authorization and authentication for apps in SharePoint
 2013</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" href="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88">Critical aspects of the app for SharePoint 2013 architecture
 and development landscape</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/0e9efadb-aaf2-4c0d-afd5-d6cf25c4e7a8" href="http://msdn.microsoft.com/en-us/library/0e9efadb-aaf2-4c0d-afd5-d6cf25c4e7a8">Apps for SharePoint compared with SharePoint solutions</a></span>
</li></ul>
