# SharePoint 2013: Mix remote web applications with SharePoint components in apps
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
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:31:41
## Description

<p><span style="font-size:small">This sample hybrid app shows how to include a remote ASP.NET web application and classic SharePoint components in the same app for SharePoint.</span></p>
<p><span style="font-size:small">It also shows how to use the SharePoint REST APIs to perform Read operations on SharePoint lists and list items, and how to retrieve only selected fields from the list. The app displays all of the items in a custom list that
 the app deploys to the app web.</span></p>
<p><span style="font-size:small">The default.aspx page of the app appears after you launch the app and choose the
<strong>Get the Cast</strong> button.</span></p>
<p><strong><span style="font-size:small">Figure 1. default.aspx page in the app</span></strong></p>
<p><img id="61254" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-mix-remote-ead77c56/image/file/61254/1/fig1.jpg" alt="" width="614" height="416">&nbsp;</p>
<p><span style="font-size:small">The sample demonstrates how to read data using the OData protocol from REST endpoints. Additionally, it demonstrates how to parse Atom-formatted XML returned from these endpoints by using the classes of the
<strong>System.Xml.Linq </strong>namespace. (Other samples of apps for SharePoint show how to do this by using the classes of the
<strong>System.Xml </strong>namespace.)</span></p>
<p><span style="font-size:small">For more information about the SharePoint REST APIs, see the topic
<strong>Programming using the SharePoint 2013 Preview REST service </strong>in the SharePoint 2013 Preview developer documentation. For more information about working with Atom and OData, see
<a href="http://www.odata.org/developers/protocols/atom-format">OData: AtomPub Format</a>.</span></p>
<h1>Prerequisites</h1>
<ul>
<li><span style="font-size:small">A&nbsp;SharePoint 2013 development environment that is configured for app isolation and OAuth.</span>
</li><li><span style="font-size:small">Visual Studio 2012 and SharePoint development tools in Visual Studio 2012.</span>
</li></ul>
<h1>Important contents</h1>
<ul>
<li><span style="font-size:small">TheaterCompany project, which contains the AppManifest.xml file, the feature.xml file, and various XML files that define the SharePoint components.</span>
</li><li><span style="font-size:small">TheaterCompanyWeb project.</span>
<ul>
<li><span style="font-size:small">Default.aspx file, which contains the HTML and ASP.NET controls for the user interface of the app.</span>
</li><li><span style="font-size:small">Default.aspx.cs file, which contains the C# code that uses the REST APIs to read data.</span>
</li><li><span style="font-size:small">Web.config, web.debug.config, and web.release,config files. (The web.config file that is packaged with the app is a merger of web.config and either web.debug.config or web.release,config.)</span>
</li></ul>
</li></ul>
<h1>Configuration instructions</h1>
<ul>
<li><span style="font-size:small">Open the TheaterCompany.sln file in Visual Studio.
</span></li><li><span style="font-size:small">In <strong>Properties</strong> pane of Visual Studio, change the
<strong>Site URL </strong>property of the app for SharePoint project in Visual Studio to the absolute URL of your SharePoint 2013 Preview developer test site. For example, &quot;<a href="http://MyDevServer/sites/MyTestSite">http://MyDevServer/sites/MyTestSite</a>&quot;.</span>
</li></ul>
<h1>Build instructions</h1>
<p><span style="font-size:small">&bull;&nbsp;Choose the top solution node <strong>
TheaterCompany</strong> in <strong>Solution Explorer</strong>. On the menu bar, choose
<strong>Build</strong>, <strong>Build Solution</strong>.</span></p>
<h1>Deploying and testing the sample</h1>
<ul>
<li><span style="font-size:small">Choose the F5 key. The web application, <strong>
TheaterCompanyWeb</strong>, is deployed to IIS Express at the URL specified in the SSL URL property of the project. The
<strong>TheaterCompany</strong> app is installed to your test SharePoint website. (The remote app does not try to interact with the host web, and the app principal automatically has permissions to the app web, so you are not prompted to grant permissions.)
 The <strong>Site Contents </strong>page of your target SharePoint website opens, and you can see the new app listed there.</span>
</li><li><span style="font-size:small">Choose the <strong>Theater Company </strong>app, and the remote web application opens to its default page. Depending on your browser, there may or may not be a background with the classic comedy and tragedy masks as shown in
 Figure 1. If the browser cannot open the background file, a sea green background is seen. Choose the
<strong>Get the Cast </strong>button. A grid that is partially populated with data should open, as shown in Figure 1.&nbsp;</span>
</li></ul>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://www.odata.org/">Open Data Protocol</a></span>
</li><li><span style="font-size:small"><a href="http://www.odata.org/developers/protocols/atom-format">OData: AtomPub Format</a></span>
</li><li><span style="font-size:small">SharePoint 2013 Preview SDK topics:</span>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp179936.aspx">How to: Create a cloud-hosted app that includes a custom SharePoint list and content type</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142385(v=office.15)">Programming using the SharePoint 2013 Preview REST service</a></span>
</li></ul>
</li></ul>
<h1>Contact info</h1>
<p><span style="font-size:small"><a href="mailto:DocThis@microsoft.com">DocThis@microsoft.com</a></span></p>
