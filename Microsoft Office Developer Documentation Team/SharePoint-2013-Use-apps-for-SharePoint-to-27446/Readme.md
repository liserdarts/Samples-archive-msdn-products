# SharePoint 2013: Use apps for SharePoint to provision on-prem site collection
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* web service
* App model
* Microsoft SharePoint technologies
* Cloud Computing
* sites and content
## IsPublished
* True
## ModifiedDate
* 2015-03-12 04:50:58
## Description

<p>We replaced this sample with newer samples on GitHub. To use or contribute to the sample on GitHub, see
<a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Provisioning.OnPrem.Async">
Provisioning.OnPrem.Async</a> and <a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Provisioning.SiteCol.OnPrem">
Provisioning.SiteCol.OnPrem</a><strong>.</strong></p>
<p>This sample demonstrates how to extend an on-premises SharePoint farm to support site collection creation from a provider-hosted app for SharePoint. This same pattern can be used to provide other extensions as well, such as exposing information management
 settings and other capabilities which are not natively available in the .NET Framework client-side object model (CSOM).</p>
<p>Remote site collection creation is supported natively in Office 365 (MT), but the same capabilities are not available in CSOM for on-premises SharePoint. Specifically for site collection creation, you could also use the site admin web service (siteadmin.svc)
 in on-premises, but this capability has been depreciated and is not supported with in Office 365 (Dedicated).</p>
<h3>Site provisioning</h3>
<p>The provider-hosted app for SharePoint provisions the actual site collection by calling the custom Windows Communication Foundation (WCF) end point which has to be deployed to the farm as an extension point. This is a good example of so called &quot;smart&quot; on-premises
 extensions, where we only use the farm solution to expose additional APIs for remote access, rather than actually place the business logic to the farm.</p>
<p>In this way, you can control the business logic without updating the farm solutions in the SharePoint farm. This means that we can adjust the behavior without breaking or otherwise impacting the SharePoint services. Actual site collection creation API is
 exposed by the WCF end point as <strong>CreateSiteCollection</strong> method. We can control the configuration of the site collection by providing different configuration options using complex data type called
<strong>SiteData</strong>.</p>
<h3>Related content</h3>
<p><a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Core.DataStorageModels">Office 365 Developer Patterns and Practices</a></p>
