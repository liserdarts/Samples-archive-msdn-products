# SharePoint 2013: Use an app for SharePoint to apply a theme to a SharePoint site
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* App model
* User Experience
* apps for SharePoint
* Microsoft SharePoint technologies
* sites and content
## IsPublished
* True
## ModifiedDate
* 2015-03-11 02:23:10
## Description

<p>We replaced this sample with a newer sample on GitHub. To use or contribute to the sample on GitHub, see
<a href="https://github.com/Lauragra/PnP/tree/master/Samples/Provisioning.OnPrem.Async">
Branding.Themes</a><strong>.</strong></p>
<p>This provider-hosted sample app for SharePoint demonstrates using the Client-Side Object Model (CSOM) to obtain information about and make changes to a design theme or Composed Look by uploading component files and using APIs to apply them. This pattern
 can be used to brand remote-provisioned sites of all types.</p>
<h3>Deviations from good practices</h3>
<p>The sample demonstrates how to use CSOM to obtain information about and make changes to a Theme or Composed Look by uploading component files and using APIs to apply them. It does not conform to all the good practices that should be used in a production
 app:</p>
<ul>
<li>The app uses files stored on the local file system of a provider-hosted app for SharePoint and hardcodes references to them in client code. A robust app for SharePoint would use an alternative storage mechanism and configuration store.
</li><li>The app lacks proper error and exception handling.&nbsp; </li></ul>
<h3>Prerequisites</h3>
<p>This sample requires the following:</p>
<ul>
<li>Visual Studio 2012 or Visual Studio 2013 </li><li>Microsoft Office Developer Tools for Visual Studio </li><li>A SharePoint 2013 development environment </li></ul>
<h3>Related content</h3>
<p><a href="https://github.com/OfficeDev/PnP/tree/master/Samples/Core.DataStorageModels">Office 365 Developer Patterns and Practices</a></p>
