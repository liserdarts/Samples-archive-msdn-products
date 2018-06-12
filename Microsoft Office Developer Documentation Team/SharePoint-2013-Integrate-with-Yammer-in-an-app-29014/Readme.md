# SharePoint 2013: Integrate with Yammer in an app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
## Topics
* Microsoft Azure
* SharePoint
## IsPublished
* True
## ModifiedDate
* 2014-05-30 07:14:28
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Integrate with Yammer in an app for SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> Learn how to integrate a app for SharePoint with Yammer.</p>
</div>
<div>
<p><strong>Last modified: </strong>May 30, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#O15Readme_Description">Description of the sample</a> <br>
<a href="#O15Readme_Prereq">Prerequisites</a> <br>
<a href="#sectionSection2">Sample for demo only: deviations from best practices</a>
<br>
<a href="#O15Readme_components">Key components of the sample</a> <br>
<a href="#O15Readme_config">Configure the sample</a> <br>
<a href="#O15Readme_build">Build, deploy, and run the sample</a> <br>
<a href="#O15Readme_Troubleshoot">Troubleshooting</a> <br>
<a href="#O15Readme_Changelog">Change log</a> <br>
<a href="#O15Readme_RelatedContent">Related content</a></p>
<p>&nbsp;</p>
</div>
<h2>Description of the sample</h2>
<div id="sectionSection0">
<p><span>Provided by:</span> <a href="http://www.canviz.com" target="_blank">Todd Baginski</a>, Canviz Consulting</p>
<p>This sample provider-hosted app for SharePoint shows how to integrate SharePoint 2013 with Yammer. For this sample to work, first you have to register a Yammer app at
<a href="https://www.yammer.com/client_applications/" target="_blank">Yammer</a> and obtain a Yammer app client ID. Then you have to create an internal Yammer group and obtain the feed ID for the group.</p>
<p>The default.aspx page of the app for SharePoint appears after you install and launch the app. After you input your client ID and feed ID, you will be able to perform the following Yammer operations in the app for SharePoint:</p>
<ul>
<li>
<p>List the feed for a Yammer group.</p>
</li><li>
<p>Post a message to a Yammer group.</p>
</li><li>
<p>Reply to a message in a Yammer group..</p>
</li><li>
<p>Like a message in a Yammer group.</p>
</li></ul>
<p>The default.aspx page of the app for SharePoint appears after you install and launch the app, as shown in Figure 1.</p>
<strong>
<div class="caption">Figure 1. The default.aspx page</div>
</strong><br>
<strong></strong><img src="/site/view/file/115834/1/image.png" alt=""></div>
<h2>Prerequisites</h2>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer</p>
</li><li>
<p>A <a href="https://manage.windowsazure.com" target="_blank">Microsoft Azure account</a> with permissions to deploy a website</p>
</li><li>
<p>Microsoft Azure SDK 1.8 for .NET</p>
</li><li>
<p>A Yammer account. You also have to create an internal Yammer group and obtain the feed ID for the group.</p>
</li></ul>
</div>
<h2>Sample for demo only: deviations from best practices</h2>
<div id="sectionSection2">
<p>The sample's focus is to demonstrate how to integrate with Yammer from a provider-hosted app for SharePoint. It does not conform to all the best practices that you should use in a production app. Especially, be aware of the following:</p>
<ul>
<li>
<p>The app has no exception handling.</p>
</li></ul>
</div>
<h2>Key components of the sample</h2>
<div id="sectionSection3">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong>IntegratewithYammer</strong> project, which contains:</p>
<ul>
<li>
<p>The <strong>AppManifest.xml</strong> file, which registers the provider-hosted application with SharePoint.</p>
</li></ul>
</li><li>
<p>The <strong>IntegratewithYammerWeb</strong> project, which contains:</p>
<ul>
<li>
<p>The <strong>Pages\Default.aspx</strong> file, which is the default page of the app for SharePoint . In this page, you provide the Yammer Client ID that you registered with Yammer and the Yammer Feed ID.</p>
</li></ul>
</li></ul>
</div>
<h2>Configure the sample</h2>
<div id="sectionSection4">
<p>Follow these steps to configure the sample:</p>
<ol>
<li>
<p>Open the <strong>IntegratewithYammer.sln</strong> file in Visual Studio 2012.</p>
<ul>
<li>
<p>In the <strong><span class="ui">Properties</span></strong> pane, change the <strong>
<span class="ui">Site URL</span></strong> property to be the absolute URL of your SharePoint test site collection on Office 365. For example, https://<span>MyTenant</span>.sharepoint.com/sites/dev.</p>
</li></ul>
</li><li>
<p>Get a Yammer Client ID:</p>
<ul>
<li>
<p>Log into Yammer, then navigate to <span>https://www.yammer.com/client_applications/</span> to register a new app.</p>
</li></ul>
</li><li>
<p>Get a Yammer Feed ID:</p>
<ul>
<li>
<p>To obtain the Feed ID, first log into Yammer. Then navigate to the Yammer group that you created. You will see the
<strong><span class="ui">feedId</span></strong> in the URL, as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. Locating the Feed ID in the URL</div>
</strong><br>
<strong></strong><img src="/site/view/file/115835/1/image.png" alt=""> </li></ul>
</li></ol>
</div>
<h2>Build, deploy, and run the sample</h2>
<div id="sectionSection5">
<p>Follow these procedures to build, deploy, and run the sample.</p>
<h3>To build and deploy the IntegratewithYammerWeb website</h3>
<div>
<ol>
<li>
<p>Create an empty website on Microsoft Azure and download the publishing profile for that site.</p>
</li><li>
<p>Register an app at the <span>/_layouts/15/appregnew.aspx</span> page of your SharePoint test site collection on Office 365. For example, https://<span>MyTenant</span>.sharepoint.com/sites/dev/_layouts/15/appregnew.aspx. Be sure to fill in the following details:</p>
<ul>
<li>
<p>Generate a client ID and a client secret. You'll need these values later for the web.config file.</p>
</li><li>
<p>For <strong><span class="ui">App Domain</span></strong>, enter the URL of the website that you created on Microsoft Azure.</p>
</li><li>
<p>Leave the <strong><span class="ui">Redirect URI</span></strong> field empty.</p>
</li></ul>
</li><li>
<p>Open the <strong>web.config</strong> file. In the <strong><span class="keyword">appSettings</span></strong> section, fill in the Client ID and the
<strong><span class="keyword">client secret</span></strong> values that you created when you registered the app.</p>
</li><li>
<p>In Solution Explorer, right-click the <strong><span class="ui">IntegratewithYammerWeb</span></strong> project, and then choose
<strong><span class="ui">Publishing</span></strong>.</p>
</li><li>
<p>Follow the instructions to import the publishing profile of your Microsoft Azure site, and publish the website to Microsoft Azure.</p>
</li></ol>
</div>
<h3>To build and deploy the app for SharePoint</h3>
<div>
<ol>
<li>
<p>In Solution Explorer, right-click the <strong><span class="ui">IntegratewithYammer</span></strong> project, and then choose
<strong><span class="ui">Publish</span></strong>.</p>
</li><li>
<p>For <strong><span class="ui">Which profile do you want to publish</span></strong>, type
<span>IntegratewithYammer</span> to create a new publishing profile. Choose <strong>
<span class="ui">Next</span></strong>.</p>
</li><li>
<p>For <strong><span class="ui">Where is your website hosted</span></strong>, type the location of the Microsoft Azure site where you published the
<strong><span class="ui">IntegratewithYammerWeb</span></strong> project earlier.</p>
</li><li>
<p>For <strong><span class="ui">Client ID</span></strong>, type the <strong><span class="keyword">clientId</span></strong> value that you created when you registered the app.</p>
</li><li>
<p>For <strong><span class="keyword">client secret</span></strong>, type the <strong>
<span class="keyword">client secret</span></strong> value that you created when you registered the app.</p>
</li><li>
<p>Choose <strong><span class="ui">Next</span></strong>.</p>
</li><li>
<p>Choose <strong><span class="ui">Finish</span></strong>. The resulting app package file has an .app extension (IntegratewithYammer.app) and is saved in the
<strong><span class="ui">app.publish</span></strong> subfolder of the <strong><span class="ui">bin\Debug</span></strong> folder of the Visual Studio solution.</p>
</li><li>
<p>In your browser, navigate to your Office 365 Developer Site. In the upper-right corner of the page, in the
<strong><span class="ui">Admin</span></strong> drop-down list, select <strong><span class="ui">SharePoint</span></strong> to go to the SharePoint admin center.</p>
</li><li>
<p>In the left panel, choose <strong><span class="ui">apps</span></strong>.</p>
</li><li>
<p>In the center column, choose <strong><span class="ui">App Catalog</span></strong>.</p>
</li><li>
<p>If you do not have an App Catalog site, follow the instructions to create a new one.</p>
</li><li>
<p>Upload the IntegratewithYammer.app file (that you created when you published the IntegratewithYammer project earlier) to the App Catalog by following these steps:</p>
<ol>
<li>
<p>In the left panel, choose <strong><span class="ui">Apps for SharePoint</span></strong>.</p>
</li><li>
<p>On the ribbon, on the <strong><span class="ui">File</span></strong> tab, choose
<strong><span class="ui">Upload Document</span></strong>, and then browse to the IntegratewithYammer.app.app file. You do not have to add any metadata or change any of the default values.</p>
</li></ol>
</li><li>
<p>Choose <strong><span class="ui">OK</span></strong>.</p>
</li><li>
<p>In your browser, navigate to the site collection in your Office 365 developer site where you want to deploy the app.</p>
</li><li>
<p>Choose the <strong><span class="ui">gear icon</span></strong> at the top-right of the page, and then select
<strong><span class="ui">Add an app</span></strong> from the drop-down menu.</p>
</li><li>
<p>You will see a new app named <strong><span class="ui">IntegratewithYammer</span></strong>. Select the name.</p>
</li><li>
<p>Choose <strong><span class="ui">Trust It</span></strong>.</p>
</li><li>
<p>Wait for the app to install. This might take several minutes.</p>
</li></ol>
</div>
<h3>To run the sample</h3>
<div>
<ol>
<li>
<p>After the app installs completely, choose the app icon to launch the app.</p>
</li><li>
<p>Type in the Yammer <strong><span class="ui">Client ID</span></strong> and <strong>
<span class="ui">Feed ID</span></strong>, and then choose <strong><span class="ui">Connect</span></strong>.</p>
<p>Your browser will open a window for authorization from Yammer. Choose <strong>
<span class="ui">Allow</span></strong>.</p>
</li><li>
<p>The page will list the feed for the Yammer group you created earlier. You can now post a message to the Yammer group, reply to a message, or like the message.</p>
</li></ol>
</div>
</div>
<h2>Troubleshooting</h2>
<div id="sectionSection6">
<p>If you see JavaScript errors when you run the app, make sure that your Azure website is on your browser's trusted sites list.</p>
<p>&nbsp;</p>
</div>
<h2>Change log</h2>
<div id="sectionSection7">
<p>First release.</p>
</div>
<h2>Related content</h2>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179933.aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj164022(v=office.15)" target="_blank">How to: Complete basic operations using SharePoint 2013 REST endpoints</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
