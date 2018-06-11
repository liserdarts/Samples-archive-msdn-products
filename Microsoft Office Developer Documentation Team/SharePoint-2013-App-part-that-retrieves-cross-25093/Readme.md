# SharePoint 2013: App part that retrieves cross-domain data by using JSONP
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* apps for SharePoint
## Topics
* Cloud
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-09-27 04:36:43
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: App part that retrieves cross-domain data by using JSONP</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Learn how to create an app part that gets cross-domain data from Windows Azure by using JSONP.</p>
</div>
<div>
<p><span>Provided by:</span> Todd Baginski, <a href="http://www.canviz.com" target="_blank">
Canviz Consulting</a></p>
<p>This sample creates an app part in a SharePoint-hosted app for SharePoint that uses JSON with Padding (JSONP) to retrieve data from a proxy page on a Windows Azure Web Site. The sample contains two solutions, one for the app and one for the website.</p>
<p>The app deploys the <strong>JSONPClientWebPart</strong> app part to SharePoint. You add the app part to a page and then enter the URL of the proxy page and the URL of a feed. The proxy page gets data from the feed that's specified in the app part, and returns
 the data in JavaScript Object Notation (JSON) format. The app part gets the feed data from the proxy page by using JSONP, and then displays the data.</p>
<strong>
<div class="caption">Figure 1. The JSONPClientWebPart app part page displays data from the specified feed</div>
</strong><br>
<img id="97160" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-app-part-3a7208fd/image/file/97160/1/sp15_app_jsonpapppart.gif" alt="" width="800" height="416"></div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer</p>
</li><li>
<p>A Windows Azure account that has permissions to deploy a website</p>
</li></ul>
</div>
<h1>Sample code is for demonstration only</h1>
<div id="sectionSection1">
<p>The focus of this sample is to show how to retrieve cross-domain data from Windows Azure by using JSONP. It does not comply with all the best practices that you should use in a production app. In particular, be aware of the following:</p>
<ul>
<li>
<p>The app has no exception handling.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2">
<p>The sample app contains the following key components:</p>
<ul>
<li>
<p><strong>JSONPApp project in the AppforSharePoint solution:</strong> Contains app settings and components that are deployed to the app web.</p>
<ul>
<li>
<p><strong>Pages\JSONPClientWebPart.aspx</strong> defines the app part UI.</p>
</li><li>
<p><strong>Scripts\JSONP.js</strong> defines the app part logic.</p>
</li><li>
<p><strong>AppManifest.xml</strong> stores information about the app that SharePoint 2013 requires, such as internal name, product ID, version, permission scopes, and the start page URL.</p>
</li></ul>
</li></ul>
<ul>
<li>
<p><strong>JSONPWebsite project in the AzureWebsite solution:</strong> Contains the logic for the proxy.</p>
<ul>
<li>
<p><strong>proxy.aspx.cs</strong> serializes the XML feed data to a JSON string by using the Json.NET API and returns the data in JSON format.</p>
</li></ul>
</li></ul>
</div>
<h1>Configure, deploy, and run the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to build, deploy, and run the sample.</p>
<h3>To create a Windows Azure website and publish the JSONPWebsite project</h3>
<div>
<ol>
<li>
<p>In the Windows Azure Management Portal, create a website.</p>
</li><li>
<p>Download the website's publishing profile. You'll import this profile from Visual Studio when you publish the
<strong>JSONPWebsite</strong> project.</p>
</li><li>
<p>Open the <strong>AzureWebsite</strong> &gt; <strong>JSONPWebsite.sln</strong> sample file in Visual Studio.</p>
</li><li>
<p>In <strong><span class="ui">Solution Explorer</span></strong>, open the shortcut menu for the
<strong>JSONPWebsite</strong> project, and choose <strong><span class="ui">Publish</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Publish Web</span></strong> dialog box, on the
<strong><span class="ui">Profile</span></strong> page, import the publish profile for the new website, and then choose
<strong><span class="ui">Publish</span></strong> to publish the website to Windows Azure.</p>
</li></ol>
</div>
<h3>To build and deploy the app for SharePoint</h3>
<div>
<ol>
<li>
<p>Download and extract the JSONPAppforSharePoint.zip file attachment from the <strong>
SharePoint 2013: App part that retrieves cross-domain data by using JSONP</strong> sample page on Code Gallery.</p>
</li><li>
<p>Open the <strong>JSONPAppforSharePoint</strong> &gt; <strong>JSONPApp.sln</strong> sample file in Visual Studio.</p>
</li><li>
<p>In <strong><span class="ui">Solution Explorer</span></strong>, choose the <strong>
JSONPApp</strong> project. In the <strong><span class="ui">Properties</span></strong> window, in the
<strong><span class="ui">Site URL</span></strong> property, enter the absolute URL of your SharePoint test site collection on Office 365 (for example,
<span>https://mytenant.sharepoint.com/sites/dev</span>).</p>
</li><li>
<p>Open the shortcut menu for the <strong><span class="ui">JSONPApp</span></strong> project, choose
<strong><span class="ui">Publish</span></strong>, and then choose <strong><span class="ui">Finish</span></strong>.</p>
<p>The app package file (JSONPApp.app) is created and saved in the <strong><span class="keyword">&hellip;\JSONPApp\bin\Debug\app.publish\1.0.0.0</span></strong> folder in the Visual Studio solution files. You'll upload the app package to the App Catalog.</p>
</li></ol>
</div>
<h3>To add the app to the App Catalog</h3>
<div>
<ol>
<li>
<p>In your browser, browse to your Developer Site. In the upper-right corner of the page, in the
<strong><span class="ui">Admin</span></strong> drop-down list, choose <strong><span class="ui">SharePoint</span></strong>.</p>
</li><li>
<p>In the navigation panel of the <strong><span class="ui">SharePoint admin center</span></strong> page, choose
<strong><span class="ui">apps</span></strong>.</p>
</li><li>
<p>On the <strong><span class="ui">apps</span></strong> page, choose <strong><span class="ui">App Catalog</span></strong>.</p>
<p>If you don't have an App Catalog site, follow the instructions to create one.</p>
</li><li>
<p>In the navigation panel on the App Catalog <strong><span class="ui">Home</span></strong> page, choose
<strong><span class="ui">Apps for SharePoint</span></strong>.</p>
</li><li>
<p>On the ribbon, on the <strong><span class="ui">Files</span></strong> tab, choose
<strong><span class="ui">Upload Document</span></strong>, and then browse to the JSONPApp.app file in the
<strong><span class="keyword">&hellip;\JSONPApp\bin\Debug\app.publish\1.0.0.0</span></strong> folder. Choose the file, and then choose
<strong><span class="ui">OK</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Apps for SharePoint</span></strong> dialog box, choose
<strong><span class="ui">Save</span></strong>. You don't have to add any metadata or change any of the default values.</p>
</li></ol>
</div>
<h3>To install and run the app</h3>
<div>
<ol>
<li>
<p>In your browser, browse to the site collection in your Developer Site where you want to install the app.</p>
</li><li>
<p>Choose the gear icon at the upper-right corner of the page, and then choose <strong>
<span class="ui">Add an app</span></strong> from the drop-down menu.</p>
</li><li>
<p>Choose the <strong><span class="ui">JSONPApp</span></strong> app.</p>
</li><li>
<p>Choose <strong><span class="ui">Trust It</span></strong>.</p>
</li><li>
<p>After the JSONPApp app installs completely, add the <strong><span class="ui">JSONPClientWebPart</span></strong> app part to the
<strong><span class="ui">Home</span></strong> page on the SharePoint site, as follows:</p>
<ul>
<li>
<p>On the upper-right corner of the page, choose <strong><span class="ui">Edit</span></strong> to open the page in
<strong>Edit</strong> mode.</p>
</li><li>
<p>On the ribbon, on the <strong><span class="ui">Insert</span></strong> tab, choose
<strong><span class="ui">App Part</span></strong>.</p>
</li><li>
<p>Add the <strong><span class="ui">JSONPClientWebPart</span></strong> to the page, and then save your changes.</p>
</li></ul>
</li><li>
<p>In the app part, enter values for the <strong><span class="ui">JSONP Service URL</span></strong> and
<strong><span class="ui">Feed URL</span></strong> (by default, the feed URL points to the
<strong><span class="ui">Microsoft Office Blog</span></strong> RSS feed), and then choose
<strong><span class="ui">GET FEEDS</span></strong>.</p>
<p>The data from the specified feed appears on the page, as shown in Figure 1.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p><strong>&nbsp;</strong></p>
<strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Issue</p>
</th>
<th>
<p>Workaround</p>
</th>
</tr>
<tr>
<td>
<p>You see JavaScript errors when you run the app.</p>
</td>
<td>
<p>Make sure that your Windows Azure website is in your browser's list of trusted sites.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Change log</h1>
<div id="sectionSection5"><strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>September 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163980.aspx" target="_blank">Get started developing apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179921.aspx" target="_blank">How to: Create app parts to install with your app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179933.aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a></p>
</li><li>
<p><a href="http://www.windowsazure.com/en-us/documentation/" target="_blank">Windows Azure documentation</a></p>
</li></ul>
</div>
</div>
</div>
