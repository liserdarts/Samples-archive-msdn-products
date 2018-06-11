# SharePoint 2013: Create a provider-hosted app that customizes app installation
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
* Authentication
## IsPublished
* True
## ModifiedDate
* 2013-02-28 05:15:21
## Description

<p id="header">Demonstrates how to create a provider-hosted app that creates a Picture library in the host web during app installation.</p>
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
<p>The PhotoApp sample project is a provider-hosted app that customizes the app installation process using the app lifecycle event for app installation and uninstallation. The app is designed and implemented to register an app installation event handler, which
 makes it possible to write C# code that fires in the remote web whenever the app is installed in a target site (for example, the host web). In this sample, the C# code in the app installation event handler creates a Picture library in the host web to demonstrate
 what can be done using code to customize the app installation process.</p>
<p>The sample demonstrates how to create a provider-hosted app that creates a Picture library in the host web during installation. The app accomplishes this by registering the
<span><span class="keyword">InstalledEventEndpoint</span></span> and the <span>
<span class="keyword">UninstallingEventEndpoint</span></span> in the AppManifest.xml file, which causes the SharePoint host environment to call back into the remote web of the app during the
<span><span class="keyword">AppInstalled</span></span> event and the <span><span class="keyword">AppUninstalling</span></span> event using a Windows Communication Foundation (WCF) web service entry point.</p>
<p>When the PhotoApp app is installed, it creates a Picture Library (base list type 109) named
<strong>Photos</strong> in the host web and uploads several files to demonstrate something that cannot be done declaratively. This is something that can only be done using code. The app also handles the app lifecycle event for app uninstallation in order to
 demonstrate app cleanup code, which deletes the Picture library when the app is uninstalled.</p>
<p>In addition to demonstrating how to implement event handlers for app installation and uninstallation, the app also demonstrates how to add a permission request to the app, which allows the app to create a Picture library in the host web. This is not something
 an app can do with the default set of app permissions. This provider-hosted app has been designed to run within an on-premises farm, and therefore it uses server-to-server (S2S) high trust to authenticate the app when the app calls into the SharePoint host
 environment</p>
<p>Key features illustrated in the sample:</p>
<ul>
<li>
<div>Registering <span><span class="keyword">InstalledEventEndPoint</span></span> and
<span><span class="keyword">UninstallingEventEndPoint</span></span> in the app manifest</div>
</li><li>
<div>Adding permission requests to the app manifest</div>
</li><li>
<div>Filtering permission requests by base list type</div>
</li><li>
<div>Authenticating calls from the app using a server-to-server (S2S) trust</div>
</li><li>
<div>Customizing app installation using code</div>
</li><li>
<div>Using C# and the client object model (CSOM) in the remote web to create a picture library and upload photos</div>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint 2013 development environment with a local SharePoint farm.</div>
</li><li>
<div>The SharePoint farm must be configured to support apps for SharePoint.</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012.</div>
</li><li>
<div>Basic familiarity with the concepts of developing a provider-hosted app. See
<a href="http://msdn.microsoft.com/en-us/library/fp142381.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a>.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample consists of a Visual Studio 2012 solution with two projects. The first project, named
<strong>PhotoApp</strong>, represents the portion of the provider-hosted app that is installed into the SharePoint host environment. The second project, named
<strong>PhotoAppWeb</strong>, is an ASP.NET application that is used to implement the app's remote web.</p>
<p>The <strong>AppManifest.xml</strong> file in the PhotoApp project contains the permission request that is used to grant the app permission to create a Picture library. The AppManifest.xml file also contains XML elements to register event handers for
<span><span class="keyword">InstalledEventEndPoint</span></span> and <span><span class="keyword">UninstallingEventEndPoint</span></span>, which point to an entry point in the remote web named
<strong>AppEventReceiver.svc</strong>.</p>
<p>The <strong>AppEventReceiver.svc.cs</strong> file contains the C# code that uses the client object model to create a Picture library in the host web named Photos, and to upload all the photo images that have been added to the Photos folder in the PhotoAppWeb
 project.</p>
<p>The <strong>web.config</strong> file in the PhotoAppWeb project contains three
<span><span class="keyword">appSettings</span></span> that are essential in configuring the S2S trust between the app and the SharePoint host environment.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample.</div>
<ol>
<li>
<div>Run the Windows PowerShell script named <span class="ui">SetupS2STrust.ps1</span> to create an SSL certificate with both a .cer file and a .pfx file, and also to register a trusted security token issuer in the local SharePoint farm.</div>
</li><li>
<div>Open the <span class="ui">PhotoApp</span> solution.</div>
</li><li>
<div>Assign the <span><span class="keyword">Site URL</span></span> property of the PhotoApp project with a URL that points to a test site in the local farm.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Build and deploy the <span class="ui">PhotoApp</span> project.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<div class="subSection">
<ol>
<li>
<div>Deploy the PhotoApp project. After the app has been installed, inspect the host web and verify that it contains a Picture library named Photos, which contains several photos.</div>
</li><li>
<div>Uninstall the app, and verify that the Photos picture library has been deleted.</div>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<div>If the app fails to install, troubleshoot the following aspects of your development environment.</div>
<ul>
<li>
<div>Make sure that your environment supports apps. In Visual Studio 2012, create a new SharePoint-hosted app and ensure you can deploy it in a test site on your farm. If you cannot, your environment is not configured to support apps for SharePoint.</div>
</li><li>
<div>Make sure that you can run S2S trust apps. Create a provider-hosted app, and choose
<span class="ui">Use a certificate</span> when asked how you want to authenticate your app. Make sure that the certificate is configured correctly and that you have registered a trusted security token issuer. If you cannot successfully deploy and test a new
 provider-hosted app, you have a configuration issue with S2S trusts.</div>
</li><li>
<div>Inspect the <span><span class="keyword">appSettings</span></span> element in the web.config file, and compare it to the SetupS2Strust.ps1 script. Verify that all the S2S trust settings are correct for your environment.</div>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142381.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179901" target="_blank">How to: Create high-trust apps for SharePoint 2013 using the server-to-server protocol</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142384.aspx" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
