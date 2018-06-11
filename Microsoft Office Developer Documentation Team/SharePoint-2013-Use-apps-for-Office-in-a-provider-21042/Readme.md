# SharePoint 2013: Use apps for Office in a provider-hosted app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
* Office 2013
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-28 09:38:23
## Description

<p>This sample app demonstrates how to use the client object model (CSOM) in an app for SharePoint to retrieve and render various actions for Office documents, and have those actions performed by the Office Web App companion version of the originating Office
 application.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>The solution is based on the provider-hosted app template provided by Visual Studio 2012. The solution authenticates against SharePoint Online to retrieve names and URLs of Office documents from document libraries in the hosting SharePoint site. The app
 renders a tile-based user interface that includes various actions for each document, such as viewing or editing the document in the browser. When the user chooses an action, the app invokes the Office Web App functionality that is included with SharePoint
 Server 2013.</p>
<p>Figure 1 shows the main view of the app, showing tiles for each of the files on the hosting SharePoint site, along with icon buttons for viewing or editing the file with the Office Web app</p>
<p class="caption"><strong>Figure 1. Main view of the app</strong></p>
<br>
<img id="76816" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-apps-271950a3/image/file/76816/1/2b-1.png" alt="Figure 1" width="580" height="589"></div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>Visual Studio 2012</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Either of the following:</div>
<ul>
<li>
<div>SharePoint Server 2013 configured to host apps, and with a Developer Site collection already created; or,</div>
</li><li>
<div>Access to an Office 365 developer site configured to host apps.</div>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<div>The <strong>Default.aspx</strong> webpage, which is used to render tiles for Office documents. The tiles enable the user to click various actions, such as viewing or editing the documents the Office Web Apps browser.</div>
</li><li>
<div>The <strong>point8020Metro.css</strong> style sheet in the CSS folder, which is used to display files as tiles.</div>
</li><li>
<div>Various image files in the Images folder.</div>
</li><li>
<div>The <strong>AppManifest.xml</strong> file, which has been edited to specify that the app requests full control permissions for the hosting web.</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_ProviderApp_WAC_cs.sln</span> file using Visual Studio 2012.</div>
</li><li>
<div>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site to the
<span><span class="keyword">Site URL</span></span> property.</div>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<div>Press F5 to run the app.</div>
</li><li>
<div>Sign in to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site if you are prompted to do so by the browser.</div>
</li><li>
<div>Trust the app when prompted to do so.</div>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have signed up for an Office 365 Developer Site configured to host apps, or that you have access to a correctly configured SharePoint 2013 farm.</p>
<p>Note that it is much easier to sign up for an Office 365 Developer Site than it is to troubleshoot local farm configurations if you just want to run this sample. The key point is that Office Web Apps are automatically configured for you in an Office 365
 Developer Site).</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp161507.aspx" target="_blank">Apps for Office and SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142381.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></div>
</li></ul>
</div>
</div>
</div>
</div>
