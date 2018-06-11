# SharePoint 2013: Create an external content type that supports notifications
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* REST
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-28 05:27:47
## Description

<p id="header">Demonstrates how to create an external content type that supports notifications in SharePoint 2013.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=be34f5b5-a1d1-47e1-971d-cfdda319992c" target="_blank">Scot Hillier</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>This sample includes a SQL Server database that contains customer data to display in an external content type and a table to manage notification subscriptions. The external content type that accompanies the sample implements the Subscribe and Unsubscribe
 stereotypes to support notifications. The sample also includes a custom event receiver that assigns a task in a task list when a new item is added to the database. Figure 1 illustrates the sample architecture.</p>
<p class="caption"><strong>Figure 1. Architecture of the sample</strong></p>
<br>
<img id="76753" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-an-f23e0c1b/image/file/76753/1/readmeimage.png" alt="Architecture of the sample" width="728" height="473">
<p>The sample uses a simple database (MiniCRM) with a table full of customer information as the basis for an external content type. The external content type is used to create an external list in SharePoint 2013. The external content type implements the Subscribe
 and Unsubscribe stereotypes, which are used to store notification endpoints in the same database where the customer data is located. The sample also includes a simple Windows application for adding new customers to the database. The Windows application simulates
 the line-of-business (LOB) system associated with the customer data. When a new customer is added, the Windows application calls all the subscribed endpoints that are stored in the database. A custom event receiver fires in response to the notification and
 assigns a new task in a task list.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>A SharePoint 2013 development environment</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Basic familiarity with Business Connectivity Services (BCS)</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<div><strong>MiniCRM.bak</strong> is the backup of the database used in the sample.</div>
</li><li>
<div><strong>ExternalListEvents.csproj</strong> is the project that contains the following:</div>
<ul>
<li>
<div>The custom event receiver for the external list</div>
</li><li>
<div>The &quot;Activities&quot; list, which is a Task list that is updated by the custom event receiver.</div>
</li><li>
<div>A feature receiver that binds the custom event receiver to the external list</div>
</li></ul>
</li><li>
<div><strong>ExternalApplication.csproj</strong> is the project that contains the Windows application.</div>
</li><li>
<div><strong>MiniCRMModel.xml</strong> is the BDC Metadata Model for the Customers external list.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample app:</p>
<div class="subSection">
<ol>
<li>
<div>Restore the <span class="ui">MiniCRM.bak</span> file into SQL Server to create the database &quot;MiniCRM.&quot; Examine the
<span class="ui">Subscriptions</span> table and ensure that it is empty.</div>
</li><li>
<div>Import the <span class="ui">MiniCRMModel.xml</span> file into the BDC Service application. Examine the associated external system definition, and adjust the settings for your environment.</div>
</li><li>
<div>Create an external list in SharePoint 2013 based on the Customer external content type that is contained in the BDC Metadata Model.</div>
</li><li>
<div>Open the solution <span class="ui">ExternalListEvents.sln</span> in Visual Studio 2012.</div>
<div class="subSection">
<ol>
<li>
<div>In the <span class="ui">ExternalListEvents</span> project, Feature1.EventReceiver.cs, update the URLs to refer to your environment.</div>
</li><li>
<div>Update the project deployment URL to refer to the SharePoint site where you made the external list.</div>
</li><li>
<div>In the <span class="ui">ExternalApplication</span> project, in the App.Config file, update the URL to refer to your environment.</div>
</li></ol>
</div>
</li></ol>
</div>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Build and deploy the <span class="ui">ExternalListEvents</span> project.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<ol>
<li>
<div>Run the <span class="ui">ExternalApplication</span> project.</div>
</li><li>
<div>Add a new customer.</div>
</li><li>
<div>Examine the Activities list for a new task assignment resulting from adding the new customer.</div>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<div>If the app fails to install, make sure that you have properly updated the URLs in the Feature Receiver.</div>
</li><li>
<div>If the app fails at run time, make sure that the Subscriptions table in the database contains valid REST endpoints. Make sure that the App.Config file is properly updated for you environment. Make sure all of the settings for the external content type
 are correct in the BDC Service application.</div>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/ee554869.aspx" target="_blank">Start: Set up the development environment for SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163139(v=office.15).aspx" target="_blank">External content types in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/sharepoint/ff521587.aspx" target="_blank">SharePoint Foundation REST Interface</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142385.aspx" target="_blank">Programming using the SharePoint 2013 REST service</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163782.aspx" target="_blank">Business Connectivity Services</a></div>
</li></ul>
</div>
</div>
</div>
</div>
