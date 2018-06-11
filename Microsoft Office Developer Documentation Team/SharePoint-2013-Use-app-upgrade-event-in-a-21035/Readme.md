# SharePoint 2013: Use app upgrade event in a provider-hosted app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-28 07:01:41
## Description

<p id="header">This sample project is a provider-hosted app with two versions. One version adds a simple custom list titled Books with a few items to the app web during app installation. The second version uses the app upgrade event to supply C# code that
 executes during the app upgrade process and adds a new column to the Books lists and updates that column in existing items.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=52a3f2aa-710f-4496-9b78-f240eccc74ad" target="_blank">Ted Pattison</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The first version of the UpgradeEventDemo sample project (version 1.0.0.0) demonstrates how to create a provider-hosted app that creates a new list in the app web during installation. The app accomplishes it in a declarative fashion by adding a
<span><span class="keyword">ListInstance</span></span> element to the feature, which activates in the scope of the app web during app installation.</p>
<p>The second version of the UpgradeEventDemo sample project (version 2.0.0.0) demonstrates how to register the
<span><span class="keyword">UpgradedEventEndpoint</span></span> in the AppManifest.xml file, which causes the SharePoint host environment to call back into the remote web of the app during the
<span><span class="keyword">AppUpgraded</span></span> event using a Windows Communication Foundation (WCF) web service entry point.</p>
<p>Key features illustrated in the sample:</p>
<ul>
<li>
<div>Creating a new list in the app web during app installation</div>
</li><li>
<div>Adding new list items to the new list during installation</div>
</li><li>
<div>Registering <span><span class="keyword">UpgradededEventEndPoint</span></span> in the app manifest</div>
</li><li>
<div>Authenticating calls from the app using a server-to-server (S2S) trust</div>
</li><li>
<div>Customizing app upgrade using code</div>
</li><li>
<div>Using C# and the client object model (CSOM) in the remote web to add a new column to a list and to update items as part of the upgrade process</div>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>A SharePoint 2013 development environment with a local SharePoint farm.</div>
</li><li>
<div>SharePoint farm must be configured to support apps for SharePoint.</div>
</li><li>
<div>Visual Studio 2012 and the Office Developer Tools for Visual Studio 2012.</div>
</li><li>
<div>Basic familiarity with the concepts of developing a provider-hosted app. See</div>
</li></ul>
</div>
<div class="section" id="sectionSection1">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142381(v=office.15)" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a>.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<p id="sectionSection2" class="section">The sample consists of two versions of the same Visual Studio 2012 solution that contains two projects. The first project, named
<strong>UpgradeEventDemo</strong>, represents the portion of the provider-hosted app that is installed into the SharePoint host environment. The second project, named
<strong>UpgradeEventDemoWeb</strong>, is an ASP.NET application that is used to implement the app's remote web.</p>
<ul>
<li>
<p>Version 1.0.0.0 of the app creates the Books list in the app web. When the app is started, the start page in the remote web provides a link to navigate to the default view of the Book list in the app web, which looks like Figure 1.</p>
<p class="caption"><strong>Figure 1. Book list in the app web</strong></p>
<br>
<img id="76762" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-app-b15dc651/image/file/76762/1/image2.png" alt="Book list in the app web" width="527" height="372">
</li><li>
<p>In version 2.0.0.0 of the UpgradeEventDemo app, the AppManifest.xml file has been modified with an XML element to register an event hander for
<span><span class="keyword">UpgradedEventEndpoint</span></span>, which points to entry point in the remote web named AppEventReceiver.svc, as shown in the following example.</p>
<div class="code"><span>&nbsp;</span></div>
</li></ul>
<p>&nbsp;</p>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>XML </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>&lt;Properties&gt;
  &lt;Title&gt;Upgrade Event Demo (MSDN)&lt;/Title&gt;
  &lt;StartPage&gt;~remoteAppUrl/Pages/Default.aspx?{StandardTokens}&lt;/StartPage&gt;
  &lt;UpgradedEventEndpoint&gt;~remoteAppUrl/AppEventReceiver.svc&lt;/UpgradedEventEndpoint&gt;
&lt;/Properties&gt;</pre>
</td>
</tr>
</tbody>
</table>
<p id="sectionSection2" class="section">&nbsp;</p>
<ul>
<li>
<div class="code">The <strong>AppEventReceiver.svc.cs</strong> file contains the C# code that uses the client object model (CSOM) to add a new text column to the Books list for tracking the Book Genre. This code also updates existing items.</div>
<p>The web.config file in the UpgradeEventDemo project contains three <span><span class="keyword">appSettings</span></span> that are essential in configuring the S2S trust between the app and the SharePoint host environment.&nbsp;</p>
</li></ul>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Run the Windows PowerShell script named <span class="ui">SetupS2STrust.ps1</span> to create an SSL certificate with both a .cer file and a .pfx file, and also to register a trusted security token issuer in the local SharePoint farm.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<div class="subSection">
<ol>
<li>
<div>Open version 1.0.0.0 of the <span class="ui">UpgradeEventDemo</span> solution in Visual Studio 2012.</div>
</li><li>
<div>Test the app through the debugger by running the <strong>Deploy</strong> command on the app project. The app should deploy and allow you to see the start page. Click the link on the start page to see the Books list.</div>
</li><li>
<div>Use the <strong>Publish</strong> command on the UpgradeEventDemoWeb project to deploy the remote web as an ASP.NET website using a well-known URL (for example,
<span class="code">http://remoteweb.contoso.com</span>).</div>
</li><li>
<div>Use the <strong>Publish</strong> command on the UpgradeEventDemo project to create a new app package for version 1 of the app, which uses the URL used to deploy the remote web.</div>
</li><li>
<div>Close version 1.0.0.0 of the UpgradeEventDemo project in Visual Studio.</div>
</li><li>
<div>Create a new app catalog site.</div>
</li><li>
<div>Publish the app package for version 1.0.0.0 in the app catalog.</div>
</li><li>
<div>Install version 1.0.0.0 of the app in a test site. Make sure that you can launch the app and see the start page with the link to the Books list in the app web.</div>
</li><li>
<div>Open version 2.0.0.0 of the UpgradeEventDemo project in Visual Studio.</div>
</li><li>
<div>Use the <strong>Publish</strong> command on the UpgradeEventDemoWeb project to deploy version 2 of the remote web, which should effectively replace version 1.0.0.0 using the same well-known URL (for example,
<span class="code">http://remoteweb.contoso.com</span>).</div>
</li><li>
<div>Use the <strong>Publish</strong> command on the UpgradeEventDemo project to create a new app package for version 2 of the app, which uses the URL used to deploy the remote web.</div>
</li><li>
<div>Publish version 2.0.0.0 of the app package in the app catalog.</div>
</li><li>
<div>Go to the test site and examine the title for the app. Wait until the upgrade notification is showing. Once you have the option to upgrade, go through the upgrade process, which should trigger the code to add a new column. When the upgrade code runs successfully,
 the new Books list looks like Figure 2.</div>
<div class="caption">Figure 2. New Books list when the upgrade code runs successfully</div>
<br>
<img id="76763" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-app-b15dc651/image/file/76763/1/image1.png" alt="New Books list after the upgrade code runs" width="439" height="307">
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection5">
<p>If the app fails to install, troubleshoot the following aspects of your development environment:</p>
<ul>
<li>
<div>Make sure your environment supports apps. In Visual Studio 2012, create a new SharePoint-hosted app and ensure that you can deploy it in a test site on your farm. If you cannot, your environment is not configured to support apps for SharePoint.</div>
</li><li>
<div>Make sure you can run S2S trust apps. Create a provider-hosted app and select
<span class="ui">Use a certificate</span> when asked how you want to authenticate your app. Ensure the certificate is configured correctly and that you have registered a trusted security token issuer. If you cannot successfully deploy and test a new provider-hosted
 app, you have a configuration issue with S2S trusts,</div>
</li><li>
<div>Inspect the <span><span class="keyword">appSettings</span></span> elements in the web.config file, and compare it to the SetupS2Strust.ps1 script. Verify that all the S2S trust settings are correct for your environment.</div>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142381(v=office.15)" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179901.aspx" target="_blank">How to: Create high-trust apps for SharePoint 2013 using the server-to-server protocol</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142384.aspx" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
