# SharePoint 2013: Register apps that dynamically request permissions using OAuth
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
* 2013-02-28 05:20:40
## Description

<p id="header">This sample demonstrates how to register an app and have the app dynamically request permissions on the fly on behalf of a user.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=66531e5c-6ee1-4a68-87b5-c3b2f93db465" target="_blank">Andrew Connell</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The sample demonstrates the OAuth on-the-fly requesting of permissions for SharePoint 2013. In the scenario, consider a photo-printing website offered by Contoso. It has been registered with Office 365 ahead of time, but it doesn't require the user to install
 anything. A user wants to give consent to a Contoso photo printing service to access and print photos from a set of photo libraries that the user keeps on any Office 365 site.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint 2013 development environment</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>A public website that has been configured with a certificate to support HTTPS requests</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<div><strong>PhotoSharingApp.sln</strong> is the Visual Studio solution that contains the remote web application.</div>
</li><li>
<div>The <strong>NuGet package App for SharePoint Web Toolkit</strong> has been added to the project to get the TokenHelper.cs file and client object model assemblies.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample app:</div>
<div class="subSection">
<ol>
<li>
<div>Ensure that you have a site collection created using the <span class="ui">
Developer Site</span> template.</div>
</li><li>
<div>Register a new application using <span class="code">http://[..].sharepoint.com/_layouts/15/AppRegNew.aspx</span></div>
</li><li>
<div>Specify the URL of the application (for example, <span class="code">http://fabrikam.com</span>).</div>
</li><li>
<div>Specify the HTTPS app redirect URL for the redirection page (for example, <span class="code">
https://fabrikam.com/RedirectAccept.aspx</span>).</div>
</li><li>
<div>Open the solution in Visual Studio 2012.</div>
</li><li>
<div>Update the web.config file to have the correct app ID and secret in the <span>
<span class="keyword">&lt;appSettings&gt;</span></span> section.</div>
</li><li>
<div>Update the Default.aspx.cs and RedirectApp.aspx.cs files to point to the URLs you are using in your testing (these are indicated by the code comments).</div>
</li></ol>
</div>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Build <span class="ui">PhotoSharingApp.sln</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<div class="subSection">
<ol>
<li>
<div>Deploy the website from the PhotoSharingApp project to the site that supports HTTPS requests.</div>
</li><li>
<div>Navigate to the site's home page (for example, <span class="code">http://fabrikam.com/Default.aspx</span>).</div>
<div>The site should redirect you back to the Office 365 site that you specified, requesting you to sign in and to grant the application the permissions it needs.</div>
</li><li>
<div>After the application is given the permissions it needs, Office 365 redirects the browser back to the site's registered redirection page to validate the access code provided and then create a client context for calling back into SharePoint.</div>
</li></ol>
</div>
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
<div><a href="http://msdn.microsoft.com/en-us/library/jj163980.aspx" target="_blank">Get started developing apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142383.aspx" target="_blank">App permissions in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142384.aspx" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj687470(v=office.15).aspx" target="_blank">OAuth authentication and authorization flow for apps that ask for access permissions on the fly in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
