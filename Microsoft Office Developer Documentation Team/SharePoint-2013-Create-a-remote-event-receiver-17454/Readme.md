# SharePoint 2013: Create a remote event receiver for external data
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:28:23
## Description

<p><span style="font-size:small">This project shows how to use Visual Studio 2012 and SharePoint development tools in Visual Studio 2012 to create an app for SharePoint using Business Connectivity Services (BCS) to expose complex data from an external system.</span></p>
<p><span style="font-size:small">This sample shows how to create a remote event receiver that attaches to an external data list, and based on actions performed on that list, will add an entry to a secondary SharePoint list.</span></p>
<p><strong><span style="font-size:small">The main objectives for this sample are:</span></strong></p>
<ul>
<li><span style="font-size:small">Set up and use the simulated, self-hosted OData service to provide data that the auto-generation tools in Visual Studio 2012 can use to create external content types.</span>
</li><li><span style="font-size:small">Create a new app for SharePoint</span> </li><li><span style="font-size:small">Create an external content type .that describes data from the self-hosted OData service.</span>
</li><li><span style="font-size:small">Create two external lists: one for reading data from the external data source, and one for tracking notifications of changes to that underlying data.</span>
</li><li><span style="font-size:small">Create a remote event receiver that will monitor changes to the external list and execute conditional code that will add a record to the Notifications list.</span>
</li></ul>
<p><span style="font-size:small">When the data in the self-hosted OData service changes, you will see a new record created in the Notifications list</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">SharePoint 2013</span> </li><li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">Internet Information Services (IIS)</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The SampleBCSApp.zip and RemoteEventReceiver.zip files include the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio project files</span> </li><li><span style="font-size:small">Local OData service (CannedDataService)</span> </li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">In order to run the samples included in this project, you will need to do the following:</span></p>
<ol>
<li><span style="font-size:small">Extract the SampleBCSApp.zip and RemoteEventReceiver.zip files to your hard drive.</span>
</li><li><span style="font-size:small">Copy the RemoteEventReceiverConsoleApp to your hard drive.</span>
</li><li><span style="font-size:small">Start the simulated OData service.&nbsp;This service is hosted by a local instance of IIS.&nbsp; It attaches to a port in IIS and provides an OData endpoint that you will use in your app.</span>
</li><li><span style="font-size:small">Open the Visual Studio project files in Visual Studio.</span>
</li></ol>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press F5 to build the sample.</span></p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">When you press F5 to deploy, the app will run. Then, you can view the records displayed, create new ones, and modify existing ones.</span></p>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If you cannot get the &ldquo;Canned&rdquo; data service to work, make sure that all the files are in the same folder on your hard drive.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/11d7adb5-5388-4517-ae03-beb7be1c6981" target="_blank">External content types in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/7a87e5bf-4428-4055-b113-7665a93e7326" target="_blank">Using OData sources with Business Connectivity Services in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set&nbsp;up an on-premises development environment for apps for SharePoint</a></span>
</li><li><span style="font-size:small"><a href="http://www.odata.org" target="_blank">Open Data Protocol site</a></span>
</li></ul>
