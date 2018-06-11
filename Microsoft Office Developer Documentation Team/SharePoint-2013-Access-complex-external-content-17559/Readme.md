# SharePoint 2013: Access complex external content types with CSOM
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
* 2013-02-06 02:30:49
## Description

<p><span style="font-size:small">This project will show how to use Visual Studio 2012 and SharePoint development tools in Visual Studio 2012 to create an app for SharePoint using Business Connectivity Services (BCS) to expose complex data from an external system.</span></p>
<p><span style="font-size:small">The main objectives for this sample are:</span></p>
<ul>
<li><span style="font-size:small">Set up and use the simulated, self-hosted OData service to provide data that the auto-generation tools in Visual Studio 2012 can use to create external content types</span>
</li><li><span style="font-size:small">Create a new app for SharePoint</span> </li><li><span style="font-size:small">Create an external content type that describes data from the self-hosted OData service.</span>
</li><li><span style="font-size:small">Use the client object model that has been extended for BCS in SharePoint 2013 to retrieve data by directly calling into the external content type.</span>
</li><li><span style="font-size:small">Use JQuery and JQuery-UI to display external data in your app.</span>
</li></ul>
<p><span style="font-size:small">This sample will use the Employee data entity found in the self-hosted OData service to 1) display the successful retrieval of the client content from SharePoint, 2) to retrieve the number of records found in the Employee entity,
 and 3) to display a detailed list of the records found in the Employee entity.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">SharePoint 2013</span> </li><li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">Internet Information Services (IIS)</span> </li><li><span style="font-size:small">JQuery and JQuery UI.&nbsp; These can be downloaded from
<a href="http://jquery.com">http://jquery.com</a> and <a href="http://jquery-ui.com">
http://jquery-ui.com</a>.</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The BCSComplexTypeSample.zip file includes the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio project files</span> </li><li><span style="font-size:small">Local OData service (CannedDataService)</span> </li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">In order to run the samples included in this project, you will need to do the following:</span></p>
<ol>
<li><span style="font-size:small">Extract the SampleBCSApp.zip file to your hard drive.</span>
</li><li><span style="font-size:small">Start the simulated OData service.&nbsp; This service is hosted by a local instance of IIS.&nbsp; It simply attaches to a port in IIS and provides an OData endpoint that you will use in your app</span>
</li><li><span style="font-size:small">Load Visual Studio project files</span> </li><li><span style="font-size:small">Build and deploy the project to SharePoint</span>
</li></ol>
<h1>Build the project</h1>
<p><span style="font-size:small">To build and deploy, press F5.</span></p>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If you cannot get the &ldquo;Canned&rdquo; data service to work, make sure that all the files are in the same folder on your hard drive.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/11d7adb5-5388-4517-ae03-beb7be1c6981" href="http://msdn.microsoft.com/en-us/library/11d7adb5-5388-4517-ae03-beb7be1c6981" target="_blank">External content types in SharePoint
 2013</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/7a87e5bf-4428-4055-b113-7665a93e7326" href="http://msdn.microsoft.com/en-us/library/7a87e5bf-4428-4055-b113-7665a93e7326" target="_blank">Using OData sources with Business Connectivity
 Services in SharePoint 2013</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set up an on-premises&nbsp;development
 environment for apps for SharePoint</a></span> </li><li><span style="font-size:small"><a href="http://www.odata.org" target="_blank">Open Data Protocol site</a></span>
</li></ul>
