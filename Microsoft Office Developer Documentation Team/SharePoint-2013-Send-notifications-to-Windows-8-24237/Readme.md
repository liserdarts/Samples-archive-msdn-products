# SharePoint 2013: Send notifications to Windows 8 apps from an app for SharePoint
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* SharePoint
* Windows 8 Apps
## Topics
* apps for SharePoint
## IsPublished
* True
## ModifiedDate
* 2014-06-10 03:56:18
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Send Notifications to a Windows 8 app from an app for SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p>Learn how to send toast and tile notifications to a Windows 8 app from a provider-hosted app for SharePoint.</p>
</div>
<div class="introduction">
<p><strong>Last modified: </strong>April 07, 2014</p>
<p><strong>In this article</strong><br>
<a href="#sectionSection0">Prerequisites</a><br>
<a href="#sectionSection1">Sample for demo only: deviations from best practices</a><br>
<a href="#sectionSection2">Key components</a><br>
<a href="#sectionSection3">Build and deploy the sample</a><br>
<a href="#sectionSection4">Run and test the sample</a><br>
<a href="#sectionSection5">Change log</a><br>
<a href="#sectionSection6">Related content</a></p>
<p><span class="label">Provided by:</span>&nbsp;&nbsp;Todd Baginski, <a href="http://www.canviz.com" target="_blank">
Canviz Consulting </a></p>
<p>This sample app for SharePoint shows how to send Toast and Tile notifications to Windows 8 applications from SharePoint via a Microsoft Azure website. This sample contains two solutions, one is a provider-hosted app for SharePoint, and the other is a Windows
 8 app.</p>
<p>The default.aspx page of the app for SharePoint appears after you install and start the app. After you set the channel Url in the Windows 8 app, you will be able to send tile and toast notifications from the app for SharePoint.</p>
<div class="caption">Figure 1. Default.aspx page in the provider-hosted app for SharePoint</div>
<br>
<img id="116601" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-send-9d68a772/image/file/116601/1/sp15_toasttilenotifications_start.png" alt="The app's interface lets you send both toast and tile notifications to a Windows 8 app." width="595" height="623"></div>
<a name="O15Readme_Prereq"></a><a name="sectionSection0"></a>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer</p>
</li><li>
<p>A <a href="https://manage.windowsazure.com" target="_blank">Microsoft Azure account</a> with permissions to deploy a website</p>
</li><li>
<p>Microsoft Azure SDK for .NET (VS 2012) 1.8</p>
</li><li>
<p>A <a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh974578.aspx" target="_blank">
Windows Store developer license</a></p>
</li><li>
<p>A <a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh868184.aspx" target="_blank">
Windows Store developer account</a></p>
</li></ul>
</div>
<a name="sectionSection1"></a>
<h1 class="heading">Sample for demo only: deviations from best practices</h1>
<div class="section" id="sectionSection1">
<p>The sample is focused on demonstrating how to send toast and tile notifications to Windows 8 apps from a provider-hosted app for SharePoint that is hosted on a Microsoft Azure website, so it doesn't conform to all the best practices that you should use in
 a production app. Specifically, be aware of the following:</p>
<ul>
<li>
<p>The app has no exception handling.</p>
</li></ul>
</div>
<a name="O15Readme_components"></a><a name="sectionSection2"></a>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection2">
<p>&nbsp;</p>
<ul>
<li>
<p><strong>SP2013.Win8.WNS.Metro</strong> solution (a Windows 8 app), which contains:</p>
<ul>
<li>
<p><strong>default.html</strong>. Contains the HTML form that lets you set the channel URL.</p>
</li><li>
<p><strong>js\default.js</strong>. Uses the channel URL to configure the app for push notifications.</p>
</li></ul>
</li><li>
<p><strong>SendWin8AppNotification</strong> solution (a provider-hosted app for SharePoint), which contains:</p>
<ul>
<li>
<p><strong>SendWin8AppNotification</strong> project. The SharePoint components of the app for SharePoint.</p>
</li><li>
<p><strong>SendWin8AppNotificationWeb</strong> project. The remotely hosted components of the app for SharePoint. This project contains:</p>
</li><li>
<p><strong>Pages\Default.aspx</strong>. The forms that send the tile and toast notifications.</p>
</li><li>
<p><strong>Web.config</strong>. Stores the app for SharePoint client id and client secret as well as the package security identifier (SID) and client secret for the push notifications service.</p>
</li></ul>
</li></ul>
</div>
<a name="O15Readme_config"></a><a name="sectionSection3"></a>
<h1 class="heading">Build and deploy the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<h3 class="procedureSubHeading">To build and deploy the SP2013.Win8.WNS.Metro project</h3>
<div class="subSection">
<ol>
<li>
<p>Open the <strong>SP2013.Win8.WNS.Metro.sln</strong> file in Visual Studio 2012.</p>
</li><li>
<p>In your browser, go to the <a href="http://dev.windows.com" target="_blank">Windows Developer Center</a> Click the
<span class="ui">Submit an app</span> link and follow the instructions to create an app name.</p>
</li><li>
<p>Click the <span class="ui">Services</span> link in the left column, and then click the
<span class="ui">Live Services site</span> link on the <span class="ui">Services</span> page.</p>
<div class="caption">Figure 2. Click the Live Services site link to get the registration information for the push notifications service</div>
<br>
<img id="116602" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-send-9d68a772/image/file/116602/1/sp15_toasttilenotifications_creds.png" alt="Click on the Live Services site link to get the authentication information for your app." width="593" height="224">
</li><li>
<p>Click the <span class="ui">Authenticating your service</span> link on the <span class="ui">
Push notifications and Live Connect services info</span> page. Copy the values for the client secret and the package security identifier (SID).</p>
</li><li>
<p>In Visual Studio 2012 right-click the <span class="ui">SP2013.Win8.WNS.Metro</span> project in
<span class="ui">Solution Explorer</span>, click the <span class="ui">Store</span> menu, and then choose
<span class="ui">Associate App with the Store</span>.</p>
</li><li>
<p>Sign in with your Windows Store credentials.</p>
</li><li>
<p>Select the app name that you created for your Windows 8 app. Choose the <span class="ui">
Next</span> button to verify the information that will be added to your app's manifest file, and then click the
<span class="ui">Associate</span> button.</p>
</li><li>
<p>Open the <strong>js\package.appmanifest</strong> file and select the <span class="ui">
Packaging</span> tab. Click the <span class="ui">Choose Certificate</span> button and select
<span class="ui">Create test certificate</span> from the <span class="ui">Configure Certificate</span> dropdown menu. Click the
<span class="ui">OK</span> button.</p>
</li><li>
<p>Right-click the <span class="ui">SP2013.Win8.WNS.Metro</span> project in <span class="ui">
Solution Explorer</span>, and choose <span class="ui">Deploy</span>.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">To build and deploy the SendWin8AppNotificationWeb project</h3>
<div class="subSection">
<ol>
<li>
<p>Open the <strong>SendWin8AppNotification.sln</strong> file in Visual Studio 2012.</p>
</li><li>
<p>In the <span class="ui">Properties</span> pane, change the <span class="ui">
Site URL</span> property. It is the absolute URL of your SharePoint test site collection on Office 365: https://<span class="placeholder">&lt;my tenant&gt;</span>.sharepoint.com/sites/dev.</p>
</li><li>
<p>Create an empty website on <a href="https://manage.windowsazure.com" target="_blank">
Microsoft Azure</a>, and download the publishing profile for that site.</p>
</li><li>
<p>Register an app at the <span class="ui">/_layouts/15/appregnew.aspx</span> page of your SharePoint test site collection on Office 365: https://<span class="placeholder">&lt;my tenant&gt;</span>.sharepoint.com/sites/dev/_layouts/15/appregnew.aspx. Be
 sure to fill in the following details:</p>
<ul>
<li>
<p>Generate a client ID and client secret. You'll need to add the client secret to the web.config file in the project. You'll also need to provide the client secret to the publishing wizard.</p>
</li><li>
<p>Enter the URL of the website that you created on Microsoft Azure for <span class="ui">
App Domain</span>.</p>
</li><li>
<p>Leave the <span class="ui">Redirect URI</span> field empty.</p>
</li></ul>
</li><li>
<p>Open the web.config file. In the <span class="keyword">appSettings</span> section, assign values for the following keys:</p>
<ul>
<li>
<p><strong>ClientID</strong>: The value of the client ID that you created when you registered the app with Office 365.</p>
</li><li>
<p><strong>ClientSecret</strong>: The value of the client secret that you created when you registered the app with Office 365.</p>
</li><li>
<p><strong>WNS.Clientsecret</strong>: The value of the client secret that appears on the
<span class="ui">Push notifications and Live Connect services info page</span> for the app that you created in the Windows Developer Center.</p>
</li><li>
<p><strong>WNS.SID</strong>: The value of the package SID that appears on the <span class="ui">
Push notifications and Live Connect services info page</span> for the app that you created in the Windows Developer Center.</p>
</li></ul>
</li><li>
<p>Right-click the <span class="ui">SendWin8AppNotificationWeb</span> project in
<span class="ui">Solution Explorer</span>, and choose <span class="ui">Publishing</span>.</p>
</li><li>
<p>Follow the instructions to import the publishing profile of your Microsoft Azure site, and publish the project to Microsoft Azure.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">To build and deploy the SendWin8AppNotification app for SharePoint</h3>
<div class="subSection">
<ol>
<li>
<p>Right-click the <span class="ui">SendWin8AppNotification</span> project in <span class="ui">
Solution Explorer</span>, and choose <span class="ui">Publish</span>.</p>
</li><li>
<p>For <span class="ui">Which profile do you want to publish</span>, enter <span class="input">
SendWin8AppNotification</span> to create a publishing profile. Choose <span class="ui">
Next</span>.</p>
</li><li>
<p>For <span class="ui">Where is your website hosted</span>, enter the location of the Microsoft Azure site where you published the
<span class="ui">SendWin8AppNotificationWeb</span> project.</p>
</li><li>
<p>For client ID, enter the client ID value that you created when you registered the app with Office 365.</p>
</li><li>
<p>For client secret, enter the client secret value that you created when you registered the app with Office 365.</p>
</li><li>
<p>Choose <span class="ui">Next</span>, and then choose <span class="ui">Finish</span>.</p>
<p>The resulting app package file has an .app extension (SendWin8AppNotificationWeb.app) and is saved in the
<span class="ui">app.publish</span> subfolder of the <span class="ui">bin\Debug</span> folder of the Visual Studio solution.</p>
</li><li>
<p>In your browser, navigate to the home page of your Office 365 Developer Site. In the left panel, choose the
<span class="ui">Apps in Testing</span> link.</p>
</li><li>
<p>Choose <span class="ui">new app to deploy</span>, and follow the instructions to upload the BingOnlineTranslator.app package file and deploy it to your Developer Site.</p>
</li><li>
<p>Choose <span class="ui">Trust It</span>, and wait for the app to install.</p>
</li></ol>
</div>
</div>
<a name="O15Readme_test"></a><a name="sectionSection4"></a>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<ol>
<li>
<p>In Windows 8 start screen, click the <span class="ui">NotificationSample</span> app to launch the app.</p>
<div class="caption">Figure 3. Tile for your Windows 8 app</div>
<br>
<img id="116603" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-send-9d68a772/image/file/116603/1/sp15_toastilenotifications_win8.png" alt="Tap the tile for your Windows 8 app." width="346" height="228">
</li><li>
<p>Type the URL of your Microsoft Azure website, and then click <span class="ui">
Send Channel Url</span>.</p>
</li><li>
<p>In your browser, navigate to the home page of your Office 365 Developer Site. Click on the
<span class="ui">SendWin8AppNotification</span> link in the <span class="ui">
Apps in Testing</span> list to run your app for SharePoint. Click the <span class="ui">
Send</span> buttons to send tile and toast notifications to your Windows 8 app.</p>
</li></ol>
</div>
<a name="O15Readme_Changelog"></a><a name="sectionSection5"></a>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection5">
<div class="caption"></div>
<div class="tableSection">
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
<p>August 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_RelatedContent"></a><a name="sectionSection6"></a>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179933.aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a></p>
</li><li>
<p><a href="http://www.asp.net/mvc" target="_blank">Getting Started with ASP.NET MVC</a></p>
</li></ul>
</div>
</div>
</div>
