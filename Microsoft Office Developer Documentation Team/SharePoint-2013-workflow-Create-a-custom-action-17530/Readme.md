# SharePoint 2013 workflow: Create a custom action
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Workflows
## IsPublished
* True
## ModifiedDate
* 2013-04-09 03:37:42
## Description

<div id="header"><span class="label">Provided by:</span>&nbsp;&nbsp;<a href="http://social.msdn.microsoft.com/profile/andrew%20connell%20%5bmvp%5d/" target="_blank">Andrew Connell</a>,
<a href="http://www.andrewconnell.com" target="_blank">www.AndrewConnell.com</a></div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>The sample workflow is bound to a list named <span class="ui">Customers</span>. After the user enters a value for
<span><span class="keyword">CustomerID</span></span>, they can manually run the workflow. The workflow uses the
<span><span class="keyword">CustomerID</span></span> to search for the customer using the public Northwind sample OData service (<a href="http://services.odata.org/Northwind/Northwind.svc/" target="_blank">http://services.odata.org/Northwind/Northwind.svc/</a>).
 When it finds the customer, it adds the details it retrieves from the service to the list item and then concludes.</p>
<p>The part of the workflow that calls the web service and extracts the details from the response is contained within a custom activity,
<span><span class="keyword">GetNWCustomerDetailsWorkflow</span></span>.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following;</p>
<ul>
<li>
<p>Installed and configured SharePoint 2013 RTM environment that is connected to a configured Workflow Manager 1.0 farm</p>
</li><li>
<p>Service Bus 1.0 February 2013 Cumulative Update applied</p>
</li><li>
<p>Workflow Manager 1.0 February 2013 Cumulative Update applied</p>
</li><li>
<p>SharePoint 2013 March 2013 Public Update applied</p>
</li><li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li></ul>
</div>
<h1 class="heading">Run the sample</h1>
<div class="section" id="sectionSection2">
<p>Follow these steps to run the sample.</p>
<div class="subSection">
<ol>
<li>
<p>Start Visual Studio 2012, and open the solution file <span class="ui">CompleteCustomerDetails.sln</span>.</p>
</li><li>
<p>In the <span class="ui">Property</span> window, edit the <span class="ui">
Site URL</span> property to point to the site to which you wish to deploy.</p>
</li><li>
<p>Press F5 to build and deploy the workflow.</p>
</li><li>
<p>When the browser opens, navigate to the <span class="ui">Customers</span> list and create a new customer list item. Enter only the
<span class="ui">CustomerID</span> field using the value <span class="input">
EASTC</span>.</p>
</li><li>
<p>Open the item's workflow settings, and start the only workflow option.</p>
<p>After a brief pause, the workflow starts, and you are redirected to the <span class="ui">
Customer</span> list view. Navigate to the list item's <span class="ui">Workflow Status</span> page and keep refreshing it to see the progress of the workflow. It should take about 10-20 seconds to complete.</p>
</li><li>
<p>When the browser opens, navigate to the <span class="ui">Customers</span> list and create a new customer item. Provide a value only for the
<span class="ui">CustomerID</span> field: <strong>EASTC</strong>.</p>
</li><li>
<p>After creating the item, go to the item and manually start the &quot;Complete Customer Details&quot; workflow.</p>
</li><li>
<p>After a few seconds, the workflow starts and you are redirected to the <span class="ui">
Customer</span> list view. Navigate to the list item's workflow status page and keep refreshing it to see the progress of the workflow. It should take about 10-20 seconds to complete.</p>
</li><li>
<p>Once the workflow is complete, navigate to the item and note how the item fields have been updated. You should see the following values:</p>
<ul>
<li>
<p><span class="ui">CustomerID</span> = EASTC</p>
</li><li>
<p><span class="ui">Contact Name</span> = Ann Devon</p>
</li><li>
<p><span class="ui">Job Title</span> = Sales Agent</p>
</li><li>
<p><span class="ui">Address</span> = 35 King George / London</p>
</li><li>
<p><span class="ui">Country/Region</span> = UK</p>
</li><li>
<p><span class="ui">Business Phone</span> = (171) 555-0297</p>
</li><li>
<p><span class="ui">Fax Number</span> = (171) 555-3373</p>
</li></ul>
</li></ol>
</div>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection3">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First release</p>
</td>
<td>
<p>July 16, 2012</p>
</td>
</tr>
<tr>
<td>
<p>Revised</p>
</td>
<td>
<p>April 3, 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection4">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/ffaccd6b-426d-4ca0-b62f-bc7b14641a49" target="_blank">SharePoint 2013 workflow samples</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163917.aspx" target="_blank">Get started with workflows in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163181.aspx" target="_blank">SharePoint 2013 workflow fundamentals</a></p>
</li></ul>
</div>
</div>
</div>
</div>
