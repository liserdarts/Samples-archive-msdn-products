# Provision sites in batches with the app model
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* apps for SharePoint
## IsPublished
* True
## ModifiedDate
* 2015-03-12 05:08:10
## Description

<p>We replaced this sample with a newer sample on GitHub. To use or contribute to the sample on GitHub, see
<a href="https://github.com/OfficeDev/PnP/tree/dev/Samples/Provisioning.Batch">Provisioning.Batch</a>.</p>
<p>This sample provider-hosted app for SharePoint shows how to create SharePoint 2013 site collections by using the client object model. This app for SharePoint runs as a console application that you can host remotely, and it uses the app only permission policy.
 The app requires full control at the tenant scope.</p>
<p>This sample requires the following:</p>
<ul>
<li>An Office 365 SharePoint site
<ul>
<strong>Note:</strong> The code that creates a site collection will not work on an Office 365 Developer Site.
</ul>
</li><li>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer
</li><li>A <a href="https://manage.windowsazure.com/" target="_blank">Windows Azure account</a> with permissions to deploy a cloud service
</li><li>Windows Azure SDK for .NET (VS 2012) 1.8 </li></ul>
<p>The sample app contains the following:</p>
<ul>
<li><strong>Batch Provisioning project</strong>, which contains the AppManifest.xml file. This file registers the provider-hosted app with SharePoint.
</li><li><strong>Batch Provisioning Console project</strong>, which contains:
<ul>
<li><strong>App.config file:</strong> Contains the client id and secret for the app
</li><li><strong>Sites.xml:</strong> Contains a list of the names of the site collections that the app will create
</li><li><strong>Program.cs:</strong> Contains the code that provisions the site collections listed in Sites.xml
</li><li><strong>TokenHelper.cs:</strong> Contains the helper code that enables the app to get the required permissions for creating site collections. This file is not a default component of the console application template in Visual Studio 2013. You can get this
 file by creating a provider-hosted or autohosted app for SharePoint and copying the file from the remote web application to the console app project.
</li></ul>
</li><li><strong>Batch ProvisioningWeb project</strong>, which contains the TokenHelper.cs file and also the default components of a provider-hosted app. You can deploy this web application to a Windows Azure site to make sure that your app's credentials are working.
</li></ul>
<h3>Related content</h3>
<p><a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Core.DataStorageModels">Office 365 Developer Patterns and Practices</a></p>
