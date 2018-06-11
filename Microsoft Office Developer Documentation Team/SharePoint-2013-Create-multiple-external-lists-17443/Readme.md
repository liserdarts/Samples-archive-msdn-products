# SharePoint 2013: Create multiple external lists with associations
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
* 2013-02-06 02:24:35
## Description

<p><span style="font-size:small">This project will show how to use Visual Studio 2012 and SharePoint development tools in Visual Studio 2012 to create an app for SharePoint using Business Connectivity Services (BCS) to expose complex data from an external system.</span></p>
<p><span style="font-size:small">The main objectives for this sample are:</span></p>
<ul>
<li><span style="font-size:small">Set up and use the simulated OData service to provide data that the auto-generation tools in Visual Studio 2012 can use to create external content types</span>
</li><li><span style="font-size:small">Create a new app for SharePoint </span></li><li><span style="font-size:small">Create an external content type that associates two data entities</span>
</li><li><span style="font-size:small">Create external lists that will display the external data, and can be navigated to see the relational data represented by the external content type.</span>
</li></ul>
<p><span style="font-size:small">This sample will use two data entities: <strong>
Employee</strong> and <strong>SimpleCustomer</strong>. The <strong>Employee</strong> contains information about individual employees in an organization.&nbsp; In the case of this scenario, it is required that the app be able to show which customers are responsible
 for which customers.&nbsp; This scenario would be similar to customers that are serviced by a specified salesperson.</span></p>
<p><span style="font-size:small">By describing the association to BCS, and using the external lists that will surface that data, this sample will allow you to click on each individual employee in the list and be able to see the customers that are associated
 with them.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Completion of steps in <a href="http://code.msdn.microsoft.com/SharePoint-2013-Create-ffc9af9f">
SharePoint 2013: Create external list based on app-scoped external content type</a></span>
</li><li><span style="font-size:small">SharePoint 2013</span> </li><li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">Internet Information Services (IIS)</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The SampleBCSApp.zip file includes the following</span></p>
<ul>
<li><span style="font-size:small">Visual Studio project files</span> </li><li><span style="font-size:small">Local OData service (CannedDataService)</span> </li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">In order to run the samples included in this project, you will need to do the following:</span></p>
<ul>
<li><span style="font-size:small">Extract the SampleBCSApp.zip file to your hard drive.</span>
</li><li><span style="font-size:small">Start the simulated OData service.&nbsp; This service is hosted by a local instance of IIS.&nbsp; It simply attaches to a port in IIS and provides an OData endpoint that you will use in your app</span>
</li><li><span style="font-size:small">Open project files in Visual Studio</span><span style="font-size:small">.</span>
</li></ul>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press F5 to build the sample.</span></p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">After the app is deployed, you can view the records of Employees and be able to also list the Customers associated with them.</span></p>
<ul>
<li><span style="font-size:small">Click the ellipsis (...)&nbsp;in the Employees list to see&nbsp;a custom action
<strong>Customers for Employee</strong>.&nbsp; This will show you the customers associated with a particular employee.</span>
</li></ul>
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
