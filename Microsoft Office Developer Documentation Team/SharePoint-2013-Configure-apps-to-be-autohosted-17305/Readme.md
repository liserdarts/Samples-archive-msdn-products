# SharePoint 2013: Configure apps to be autohosted in SharePoint Online
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* REST
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Cloud
* data and storage
## IsPublished
* False
## ModifiedDate
* 2013-03-19 04:26:03
## Description

<p><span style="font-size:small">This sample autohosted app shows how to set values in the AppManifest.xml file and the web.config file to configure an app for SharePoint and a remote ASP.NET web application to be autohosted on a SharePoint Online site with
 an associated Windows Azure Web Site.</span></p>
<p><span style="font-size:small">It also shows how to use the SharePoint REST APIs to perform Read operations on SharePoint lists and list items, and how to retrieve only selected fields from the list. The app displays all of the items in the Composed Looks
 list of the host web.</span></p>
<p><span style="font-size:small">The default.aspx page of the app appears after you install and launch the app</span><span style="font-size:small">.</span></p>
<p><strong><span style="font-size:small">Figure 1. default.aspx page in the app</span></strong></p>
<p><img id="60090" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-configure-41146212/image/file/60090/1/autohostapp-small.png" alt="" width="645" height="259"></p>
<p><span style="font-size:small">When the Metro-style button is chosen, a grid displays the items in the
<strong>Composed Looks </strong>list. Only three columns are shown.</span></p>
<p><span style="font-size:small">The sample demonstrates how to read data by using the OData protocol from REST endpoints. Additionally, it demonstrates how to parse Atom-formatted XML returned from these endpoints by using the classes of the
<strong>System.Xml.Linq</strong> namespace. (Other samples of apps for SharePoint show how to do this by using the classes of the
<strong>System.Xml </strong>namespace.)</span></p>
<p><span style="font-size:small">For more information about the SharePoint REST APIs, see the topic
<a href="http://msdn.microsoft.com/en-us/library/fp142385(v=office.15)" target="_blank">
Programming using the SharePoint 2013 Preview REST service</a> in the SharePoint 2013 developer documentation. For more information about working with Atom and OData, see
</span><span style="font-size:small"><a href="http://www.odata.org/developers/protocols/atom-format">OData: AtomPub Format</a>.</span></p>
<h1>Prerequisites</h1>
<ol>
<li><span style="font-size:small">A&nbsp;SharePoint 2013 development environment that is configured for app isolation and OAuth.</span>
</li><li><span style="font-size:small">Visual Studio 2012 and SharePoint development tools in Visual Studio 2012.</span>
</li><li><span style="font-size:small"><span style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a href="http://www.iis.net/download/WebDeploy">Web Deploy 2.0</a></span> installed on the computer with Visual Studio. The version of Visual
 Studio and its SharePoint tools available for SharePoint 2013 should install this automatically.</span>
</li></ol>
<h1>Important contents</h1>
<ul>
<li><span style="font-size:small">FirstAutohostedApp project, which contains the AppManifest.xml file.</span>
</li><li><span style="font-size:small">FirstAutohostedAppWeb project.</span>
<ul>
<li><span style="font-size:small">Default.aspx file, which contains the HTML and ASP.NET controls for the user interface of the app.</span>
</li><li><span style="font-size:small">Default.aspx.cs file, which contains the C# code that uses the REST APIs to read data.</span>
</li><li><span style="font-size:small">TokenHelper.cs file, which is added to the project by Visual Studio and is used to access the OAuth infrastructure.</span>
</li><li><span style="font-size:small">Web.config, web.debug.config, and web.release,config files. (The web.config file that is packaged with the app is a merger of web.config and either web.debug.config or web.release.config.)<br>
</span></li></ul>
</li></ul>
<h1>Configuration instructions</h1>
<p><span style="font-size:small">Open the FirstAutohostedApp.sln file in Visual Studio 2012. In
<strong>Properties</strong> pane of Visual Studio, change the <strong>Site URL </strong>
property of the app for SharePoint project in Visual Studio to the absolute URL of your SharePoint 2013 developer test site. For example, &quot;http://MyDevServer/&quot;.</span></p>
<h1>Build instructions</h1>
<ol>
<li><span style="font-size:small">Choose the <strong>FirstAutohostedApp</strong> project in
<strong>Solution Explorer </strong>(not the top node for the whole Visual Studio solution). On the menu bar, choose
<strong>Publish</strong>, <strong>Build</strong>. (Do not choose the F5 key.) </span>
</li><li><span style="font-size:small">In the <strong>Publish</strong> dialog box, choose the
<strong>Publish</strong> button. The resulting app package file (which has the Windows Azure Web Sites package inside) has an .app extension and is saved in the app.publish subfolder of the bin\Debug folder of the Visual Studio project.</span>&nbsp;
</li></ol>
<h1>Deploying and testing the sample</h1>
<ol>
<li><span style="font-size:small">Log into SharePoint Online 2013 as a tenant administrator.<br>
2.&nbsp;At the top of the page, choose <strong>Admin</strong>, <strong>SharePoint</strong>.<br>
3.&nbsp;On the <strong>SharePoint Administration Center </strong>page, choose <strong>
apps</strong>, and then choose <strong>Corporate Catalog</strong>.<br>
4.&nbsp;On the <strong>App Catalog </strong>page, choose the <strong>upload</strong> link.<br>
5.&nbsp; On the <strong>Add a document </strong>form, browse to your app for SharePoint package and choose the
<strong>OK</strong> button. A property form for new items opens.<br>
6.&nbsp;Fill out the form as needed and choose the <strong>Save</strong> button. The app for SharePoint is saved in the catalog.<br>
7.&nbsp;Browse to any website in the tenancy and choose <strong>Site Contents </strong>
to open the <strong>Site Contents </strong>page.<br>
8.&nbsp;Choose <strong>Add an App</strong>, and on the <strong>Your Apps </strong>
page, find the app. If there are too many to scroll through, you can enter any part of the app title (<strong>First Autohosted App</strong>) into the search box.<br>
9.&nbsp;When you find the app, choose the <strong>Details</strong> link beneath it, and then on the app details page that opens, choose
<strong>Add It</strong>.<br>
10.&nbsp;You are prompted to grant permissions to the app. Choose <strong>Trust It</strong>.<br>
11.&nbsp;The <strong>Site Contents </strong>page opens and the app is listed. For a short time, a message below the title indicates that it is being added. When this message disappears, you can choose the app title to launch the app. (You may need to refresh
 the page to make the message disappears.) <br>
12.&nbsp;Choose the <strong>GET THE COMPOSED LOOKS </strong>button to display a grid with three fields from the Composed Looks list of the host web.</span>
</li></ol>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://www.odata.org/">Open Data Protocol</a></span>
</li><li><span style="font-size:small"><a href="http://www.odata.org/developers/protocols/atom-format">OData: AtomPub Format</a></span>
</li><li><span style="font-size:small">SharePoint 2013 developer documentation topics:</span>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/fp179886.aspx" target="_blank"><span style="font-size:small">How to: Create a basic autohosted app</span>
</a></li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142385(v=office.15)" target="_blank">Programming using the SharePoint 2013 Beta REST service</a></span>
</li></ul>
</li></ul>
<h1>Contact info</h1>
<p><span style="font-size:small"><a href="mailto:DocThis@microsoft.com">DocThis@microsoft.com</a></span></p>
