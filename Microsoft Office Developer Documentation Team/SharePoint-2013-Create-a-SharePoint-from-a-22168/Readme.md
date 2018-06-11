# SharePoint 2013: Create a SharePoint team site from a provider-hosted app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-05-20 08:28:13
## Description

<div id="header">Learn how to create a team site by using the .NET client object model from a provider-hosted app for SharePoint.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1>Description</h1>
</div>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span>&nbsp;&nbsp;Todd Baginski, <a href="http://www.canviz.com" target="_blank">
Canviz Consulting</a></p>
<p>This sample shows how to create a SharePoint team site from a provider-hosted app for SharePoint. After you install and launch the app, the Default.aspx page of the app for SharePoint opens. You can use the app to create a team site.</p>
<p>The Default.aspx page contains the page controls that you use to create a site.</p>
<div class="caption"><strong>Figure 1. The page controls on the Default.aspx page include a Message label, a Site Name textbox, a Create Site button, and a New Site Link hyperlink</strong></div>
<br>
<img id="82411" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-df633e28/image/file/82411/1/sp15_app_createteamsite.jpg" alt="" width="246" height="95"></div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer</p>
</li><li>
<p>A Windows Azure account with permissions to deploy a website</p>
</li><li>
<p>Windows Azure SDK for .NET (VS 2012) 1.8</p>
</li></ul>
</div>
<h1 class="heading">Sample code is for demonstration purposes only</h1>
<div class="section" id="sectionSection2">
<p>The focus of this sample is to demonstrate how to create a site from a provider-hosted app. It does not conform to all the best practices that you should use in a production app. In particular, be aware of the following:</p>
<ul>
<li>
<p>The app has no exception handling.</p>
</li><li>
<p>The access token for OAuth should be saved.</p>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection3">
<p>The sample app contains the following key components:</p>
<ul>
<li>
<p><strong>CreateTeamSite</strong> project</p>
<ul>
<li>
<p><strong>AppManifest.xml</strong>: Stores information about the app that SharePoint 2013 requires, such as internal name, product ID, version, and the start page URL.</p>
</li></ul>
</li></ul>
<ul>
<li>
<p><strong>CreateTeamSiteWeb</strong> project</p>
<ul>
<li>
<p><strong>Pages\Default.aspx</strong> and <strong>Pages\Default.aspx\Default.aspx.cs</strong>: Contain the UI and logic for the app.</p>
</li><li>
<p><strong>Web.config</strong>: Stores the app settings.</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Build, deploy, and run the sample</h1>
<div class="section" id="sectionSection4">
<p>Follow these steps to build, deploy, and run the sample.</p>
<div class="subSection">
<ol>
<li>
<p>Create an empty website from the <a href="https://manage.windowsazure.com" target="_blank">
Windows Azure Management Portal</a> (<span class="code">https://manage.windowsazure.com/</span>), and then download the website's publishing profile. You'll import this profile from Visual Studio later when you publish the
<span class="ui">CreateTeamSiteWeb</span> project.</p>
</li><li>
<p>Register your app at the <span class="code">/_layouts/15/appregnew.aspx</span> page of your SharePoint test site collection on Office 365 (for example,
<span class="code">https://MyTenant.sharepoint.com/sites/dev/_layouts/15/appregnew.aspx</span>).</p>
<ul>
<li>
<p>Generate an <span class="ui">App Id</span> and an <span class="ui">App Secret</span>.</p>
</li><li>
<p>For <span class="ui">App Domain</span>, enter the website that you created on Windows Azure (for example,
<span class="code">domainName.azurewebsites.net</span>).</p>
</li><li>
<p>Leave <span class="ui">Redirect URI</span> blank.</p>
</li></ul>
<p>Save these values. You'll use them later when you modify the Web.config file and publish the app.</p>
</li><li>
<p>Open the <span class="ui">CreateTeamSite.sln</span> file in Visual Studio 2012.</p>
</li><li>
<p>In <span class="ui">Solution Explorer</span>, choose the <span class="ui">
CreateTeamSite</span> project. In the <span class="ui">Properties</span> window, in the
<span class="ui">Site URL</span> property, enter the absolute URL of your SharePoint test site collection on Office 365 (for example,
<span class="code">https://MyTenant.sharepoint.com/sites/dev</span>).</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">To build and deploy CreateTeamSiteWeb to your website</h3>
<div class="subSection">
<ol>
<li>
<p>Expand the <span class="ui">CreateTeamSiteWeb</span> project, and open the <span class="ui">
Web.config</span> file. In the <span><span class="keyword">appSettings</span></span> section, fill in the
<span><span class="keyword">ClientId</span></span> and <span><span class="keyword">ClientSecret</span></span> values with the
<span><span class="keyword">App Id</span></span> and <span><span class="keyword">App Secret</span></span> values that you generated when you registered the app.</p>
</li><li>
<p>Open the shortcut menu for the <span class="ui">CreateTeamSiteWeb</span> project, and choose
<span class="ui">Publish</span>.</p>
</li><li>
<p>Import the publishing profile of your Windows Azure website, and then publish the web application to Windows Azure with the
<span class="ui">Web Deploy</span> publishing method.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">To build and deploy the app for SharePoint</h3>
<div class="subSection">
<ol>
<li>
<p>Open the shortcut menu for the <span class="ui">CreateTeamSite</span> project, and choose
<span class="ui">Publish</span>.</p>
</li><li>
<p>For <span class="ui">Which profile do you want to publish</span>, create a new publishing profile named
<span class="input">CreateTeamSite</span>, and then choose <span class="ui">Next</span>.</p>
</li><li>
<p>For <span class="ui">Where is your website hosted</span>, enter the location of the Windows Azure website where you published the
<span class="ui">CreateTeamSiteWeb</span> web application.</p>
<p>Use the <span class="ui">App Domain</span> that you entered when you registered the app, but use the
<span><span class="keyword">https</span></span> protocol (for example, <span class="code">
https://domainName.azurewebsites.net</span>).</p>
</li><li>
<p>For the <span class="ui">Client ID</span> identity, enter the <span><span class="keyword">App Id</span></span> value that you generated when you registered the app.</p>
</li><li>
<p>For the <span><span class="keyword">Client Secret</span></span> identity, enter the
<span><span class="keyword">App Secret</span></span> value that you generated when you registered the app, choose
<span class="ui">Next</span>, and then choose <span class="ui">Finish</span>.</p>
<p>The app package file (CreateTeamSite.app) is created and saved in the <span><span class="keyword">CreateTeamSite\bin\Debug\app.publish\1.0.0.0</span></span> folder in the Visual Studio solution files. You'll upload this app package later from the
<span class="ui">SharePoint admin center</span> page.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">To add the app to the App Catalog</h3>
<div class="subSection">
<ol>
<li>
<p>In your browser, navigate to your Office 365 Developer Site. In the upper-right corner of the page, in the
<span class="ui">Admin</span> drop-down list, choose <span class="ui">SharePoint</span>.</p>
</li><li>
<p>In the navigation panel of the <span class="ui">SharePoint admin center</span> page, choose
<span class="ui">apps</span>.</p>
</li><li>
<p>On the <span class="ui">apps</span> page, choose <span class="ui">App Catalog</span>.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>If you don't have an App Catalog site, follow the instructions to create one.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>In the navigation panel on the App Catalog <span class="ui">Home</span> page, choose
<span class="ui">Apps for SharePoint</span>.</p>
</li><li>
<p>On the ribbon, on the <span class="ui">Files</span> tab, choose <span class="ui">
Upload Document</span>, and then browse to the CreateTeamSite.app file in the <span>
<span class="keyword">CreateTeamSite\bin\Debug\app.publish\1.0.0.0</span></span> folder. Choose
<span class="ui">OK</span>.</p>
</li><li>
<p>On the <span class="ui">Apps for SharePoint</span> page, choose <span class="ui">
Save</span>. You don't have to add any metadata or change any of the default values.</p>
</li></ol>
</div>
<h3 class="procedureSubHeading">To install and run the app</h3>
<div class="subSection">
<ol>
<li>
<p>In your browser, navigate to the site collection in your Office 365 Developer Site where you want to install the app.</p>
</li><li>
<p>Choose the gear icon at the top-right of the page, and then choose <span class="ui">
Add an app</span> from the drop-down menu.</p>
</li><li>
<p>Choose the <span class="ui">CreateTeamSite</span> app.</p>
</li><li>
<p>Choose <span class="ui">Trust It</span>.</p>
</li><li>
<p>Wait for the app to install. This might take several minutes.</p>
</li><li>
<p>After the app installs completely, choose the app icon to launch the app.</p>
</li><li>
<p>Enter a name for the new site in the <span class="ui">Site Name</span> box, and then choose the
<span class="ui">Create Site</span> button.</p>
</li></ol>
</div>
<p>If the site is successfully created, a link to the new site is shown.</p>
<div class="caption"><strong>Figure 2. New Site Link hyperlink displays a link to the new site if the site is created</strong></div>
<br>
<img id="82412" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-df633e28/image/file/82412/1/sp15_app_createteamsite_success.jpg" alt="" width="247" height="112">
<p>If the site is not created, an error message is shown.</p>
<div class="caption"><strong>Figure 3. Message label displays an error message if the site is not created</strong></div>
<br>
<img id="82413" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-a-df633e28/image/file/82413/1/sp15_app_createteamsite_failure.jpg" alt="" width="516" height="89"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection5">
<p>If you see JavaScript errors when you run the app, make sure that your Windows Azure website is in your browser's list of trusted sites.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
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
<p>May 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179933.aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163980.aspx" target="_blank">Get started developing apps for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
