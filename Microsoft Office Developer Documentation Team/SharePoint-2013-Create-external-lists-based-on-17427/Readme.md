# SharePoint 2013: Create external lists based on app-scoped external content type
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:25:20
## Description

<p><span style="font-size:small">This sample demonstrates how to use SharePoint 2013 and the new tools in Visual Studio 2012 to specify a local OData source and then discover the details like connection information, table structures, data types, and methods
 with which you can interact with the underlying data (create, read, update, delete, as well as other custom methods).</span></p>
<p><span style="font-size:small">From that data source, Visual Studio 2012 will automatically generate an external content type that can be deployed to SharePoint 2013 as an app-scoped external content type that can be used within the app only.</span></p>
<p><span style="font-size:small">Once you create the external content type, this sample will demonstrate how to create an external list programmatically with Visual Studio 2012, based on the external system data. The external list allows SharePoint 2013 to
 surface the data from the external system within a familiar SharePoint list format.</span></p>
<p><span style="font-size:small">Finally, you will use the built-in SharePoint 2013 list editing forms to perform create, read, update and delete functions on the underlying data in the external system.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">SharePoint 2013</span> </li><li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">Internet Information Services (IIS)</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The SampleBCSApp.zip file includes the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio project files</span> </li><li><span style="font-size:small">Local OData service (CannedDataService)</span> </li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">To run the samples included in this project, do the following:</span></p>
<ol>
<li><span style="font-size:small">Extract the SampleBCSApp.zip file to your hard drive.</span>
</li><li><span style="font-size:small">Start the simulated OData service.&nbsp; This service is hosted by a local instance of IIS.&nbsp; It simply attaches to a port in IIS and provides an OData endpoint that you will use in your app</span>
</li><li><span style="font-size:small">Open the Visual Studio project</span> </li><li><span style="font-size:small">Build and deploy the sample</span> </li><li><span style="font-size:small">Test the app by creating and modifying records</span><span style="font-size:small">&nbsp;</span>
</li></ol>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press F5 to build the sample.</span></p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">When you press F5 to deploy, it will run the new app.&nbsp; Then you can view the records displayed, create new ones, and modify existing ones.</span></p>
<ol>
<li><span style="font-size:small">Add a new item to the list by clicking on the add link.</span>
</li><li><span style="font-size:small">Select a record and delete it.</span> </li><li><span style="font-size:small">Modify a record.</span> </li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">If you cannot get the &ldquo;Canned&rdquo; data service to work, make sure that all the files are in the same folder on your hard drive.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/11d7adb5-5388-4517-ae03-beb7be1c6981" target="_blank">External content types in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/7a87e5bf-4428-4055-b113-7665a93e7326" target="_blank">Using OData sources with Business Connectivity Services in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set&nbsp;up an on-premises development environment for apps for SharePoint</a></span>
</li><li><span style="font-size:small"><a href="http://www.odata.org" target="_blank">Open Data Protocol</a></span>
</li></ul>
