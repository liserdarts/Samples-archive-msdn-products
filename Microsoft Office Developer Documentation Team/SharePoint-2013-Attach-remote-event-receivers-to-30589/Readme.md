# SharePoint 2013: Attach remote event receivers to lists in the host web
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* apps for SharePoint
## Topics
* event receivers
* SharePoint Apps
* AppInstalled event
## IsPublished
* True
## ModifiedDate
* 2015-03-12 04:56:10
## Description

<p>We replaced this sample with a newer sample on GitHub. To use or contribute to the sample on GitHub, see
<a href="https://github.com/Lauragra/PnP/tree/master/Samples/Provisioning.OnPrem.Async">
Core.EventReceiversBasedModifications</a>.</p>
<p>The scenario for this sample shows how a provider-hosted SharePoint app can use the App Installed event to perform additional work in the host web. This is accomplished using remote event receivers, which are only supported on provider-hosted apps.</p>
<p>This sample code handles the&nbsp;AppInstalled&nbsp;event by adding a SharePoint list, creating an&nbsp;ItemAdded&nbsp;remote event receiver for the list, and handling the&nbsp;AppUninstalling&nbsp;and&nbsp;ItemAdded&nbsp;events. The app project for this
 solution is run on the local machine in IIS Express, and communicates with the host web through Microsoft Azure Service Bus; this provides a means for debugging the creation and handling of the list remote event receiver. A fuller implementation of the scenario
 would require publishing the app to a Microsoft Azure web site, generating an .app package which is then installed to either the tenant App Catalog or the Marketplace. Only in this fuller implementation can the&nbsp;HandleAppUninstalling&nbsp;method provided
 in this sample be run.</p>
<h3>Releated content</h3>
<p><a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Core.DataStorageModels">Office 365 Developer Patterns and Practices</a></p>
