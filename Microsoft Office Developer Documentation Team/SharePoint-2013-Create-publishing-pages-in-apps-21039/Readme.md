# SharePoint 2013: Create publishing pages in apps for SharePoint
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
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-28 09:06:19
## Description

<p id="header">This sample app demonstrates how to use the <span><span class="keyword">SP.Publishing</span></span> JavaScript namespace in an app for SharePoint to create publishing pages in a publishing web.</p>
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
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution creates publishing pages in the host SharePoint site from the app by using the JavaScript implementation of the client object model (JSOM).</p>
</div>
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
<div>SharePoint Server 2013 configured to host apps, and with a developer site collection already created; or,</div>
</li><li>
<div>Access to an Office 365 developer site configured to host apps.</div>
</li></ul>
</li></ul>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Important</strong></th>
</tr>
<tr>
<td>
<div>The SharePoint site collection must have the SharePoint Server Publishing Infrastructure feature activated at the site collection level, and the SharePoint Server Publishing feature activated at the site level.</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<div>The <strong>Default.aspx</strong> webpage, which is used to start the publishing operation.</div>
</li><li>
<div>The <strong>App.js</strong> file in the <strong>scripts</strong> folder, which is used to create publishing pages by using the JavaScript implementation of the client object model.</div>
</li><li>
<div>The <strong>SP.Publishing.js</strong> file. This file is provided by SharePoint Server, and has been added as a script link in the Default.aspx page, because that enables the script in App.js to use
<span><span class="keyword">SP.Publishing</span></span> classes.</div>
</li><li>
<div>The <strong>AppManifest.xml</strong> file, which has been edited to specify that the app requests Full Control permissions for the site collection and the website.</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_SharePointCharting_js.sln</span> file using Visual Studio 2012.</div>
</li><li>
<div>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 developer site collection or Office 365 developer site to the
<span><span class="keyword">Site URL</span></span> property.</div>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section"></div>
<p class="section">To run and test the sample, do the following:</p>
<div class="section"></div>
<div class="section">
<ol>
<li>
<div>Press F5 to run the app.</div>
</li><li>
<div>Sign in to your SharePoint Server 2013 developer site collection or Office 365 developer site if you are prompted to do so by the browser.</div>
</li></ol>
</div>
<p>Figure 1 shows an example of the resulting app startup page as it requests permissions to use Taxonomic objects.</p>
<p class="caption"><strong>Figure 1. SharePoint publishing app</strong></p>
<p><img id="76804" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-9bfa6f5c/image/file/76804/1/7-1.png" alt="Figure 1" width="564" height="460"></p>
Figure 2 depicts the app after it has been trusted and the user has logged in.</div>
<div class="introduction"></div>
<div class="introduction"><strong>Figure 2. App user interface after logging in
</strong>
<p><img id="76805" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-9bfa6f5c/image/file/76805/1/7-2.png" alt="Figure 2" width="385" height="280"></p>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
<p>Also, ensure that the SharePoint site collection has the SharePoint Server Publishing Infrastructure feature activated at the site collection level, and the SharePoint Server Publishing feature activated at the root web level.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
