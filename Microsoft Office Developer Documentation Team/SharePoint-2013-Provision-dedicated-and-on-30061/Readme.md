# SharePoint 2013: Provision dedicated and on-premises sites with the app model
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* C#
* Sharepoint Online
* SharePoint Server 2013
## Topics
* Client Object Model
* site collection
## IsPublished
* True
## ModifiedDate
* 2015-03-11 02:26:02
## Description

<p>We replaced this sample with a newer sample on GitHub. To use or contribute to the sample on GitHub, see
<a href="https://github.com/Lauragra/PnP/tree/master/Samples/Provisioning.OnPrem.Async">
Provisioning.Cloud.Async</a>.</p>
<p>This sample provider-hosted app for SharePoint shows how to create site collections in an on-premises installation of SharePoint 2013 or SharePoint Online Dedicated by using the client object model. This app for SharePoint has two pieces: a web application
 with a form for submitting site collection creation requests and a console application that you can host remotely. The app uses the app only permission policy and requires full control at the tenant scope.</p>
<p>This sample requires the following:</p>
<ul>
<li>A SharePoint 2013 on-premises SharePoint site or a SharePoint Online Dedicated site. This sample is configured to use the Azure Access Control Service (ACS) for authenticating and authorizing the app. If you're using an on-premises SharePoint site, you'll
 need to follow the instructions in <a href="http://msdn.microsoft.com/EN-US/library/office/dn155905(v=office.15).aspx" target="_blank">
How to: Use an Office 365 SharePoint site to authorize provider-hosted apps on an on-premises SharePoint site</a> so that it can use ACS. This means that you'll need an Office 365 SharePoint site in addition to your on-premises site.
</li><li>Visual Studio 2013 and Office Developer Tools for Visual Studio 2013 installed on your development computer.
</li><li>A local deployment of Windows Server with IIS or a server hosted in Microsoft Azure IAAS linked to on-premises SharePoint site by using a point to point VPN.
</li></ul>
<h3>Related content</h3>
<p><a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Core.DataStorageModels">Office 365 Developer Patterns and Practices</a></p>
