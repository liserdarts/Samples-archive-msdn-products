# SharePoint 2013: Use sideloading to provision a provider-hosted app
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* Console Applications
* apps for SharePoint
* Simple console application
## IsPublished
* True
## ModifiedDate
* 2015-03-11 02:24:21
## Description

<p>We replaced this sample with a newer sample on GitHub. To use or contribute to the sample on GitHub, see
<a href="https://github.com/Lauragra/PnP/tree/master/Samples/Core.SideLoading">Core.SideLoading</a>.</p>
<p>This sample console application shows how to use the SharePoint 2013 sideloading feature to install a provider-hosted app. This method bypasses the regular governance measures associated with deploying apps and doesn't require site owners to deploy apps
 from the app catalog. Instead, it allows a tenant administrator to deploy apps directly to site collections. This approach is not recommended for SharePoint-hosted apps (because of the risk of data loss), and it should be used in production environments only
 in cases where deploying from the app catalog doesn't meet your needs.</p>
<p>This application enables side loading on your site collection, installs your app, and then disables the side loading feature when it is done. You should always disable the side loading feature after you're done installing an app in this way. The application
 must be run by a user who has tenant admin rights or farm admin rights if you're using an on-prem SharePoint 2013 site.</p>
<p>This sample requires the following:</p>
<ul>
<li>A SharePoint 2013 site. In order to run this sample, you must have tenant administrator privileges.
</li><li>Visual Studio 2013 and Office Developer Tools for Visual Studio 2013 installed on your development computer.
</li></ul>
<h3>Related content</h3>
<p><a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Core.DataStorageModels">Office 365 Developer Patters and Practices</a></p>
