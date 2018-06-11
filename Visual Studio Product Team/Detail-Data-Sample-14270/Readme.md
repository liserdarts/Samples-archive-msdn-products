# Master Detail Data Sample
## Requires
* Visual Studio 2010
## License
* MS-LPL
## Technologies
* Office
## Topics
* Office Automation
## IsPublished
* True
## ModifiedDate
* 2011-11-28 06:08:39
## Description

<div id="header">
<table width="100%" id="topTable">
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p><span class="style1">&#39; Copyright © Microsoft Corporation. All Rights Reserved.
</span><span class="style1">&#39; This code released under the terms of the </span>
<br>
</p>
<div class="introduction">
<div class="alert">
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Note:</b></th>
</tr>
<tr>
<td>
<p>This sample runs in Microsoft Office Excel 2007 and higher.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>This sample demonstrates how to retrieve data from a relational database or XML file and use that data in Excel.</p>
<p>The sample is an order fulfillment worksheet. The worksheet displays details of customer orders so that the items can be packed for shipping. The worksheet also displays current inventory for each item so that the user knows if the order can be fulfilled.</p>
<div class="alert">
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Security Note:</b></th>
</tr>
<tr>
<td>
<p>This sample code is intended to illustrate a concept, and it shows only the code that is relevant to that concept. It may not meet the security requirements for a specific environment, and it should not be used exactly as shown. We recommend that you add
 security and error-handling code to make your projects more secure and robust. Microsoft provides this sample code &quot;AS IS&quot; with no warranties.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>For information about installing the sample project on your computer, see How to: Install and Use Sample Files Found in Help.</p>
</div>
<h3 class="procedureSubHeading">To run the sample</h3>
<div class="subSection">
<ol>
<li>
<p>Press F5.</p>
</li><li>
<p>Select a customer name in the <span class="ui">Select a Customer</span> list.</p>
<p>The <span class="ui">Unfulfilled Orders</span> list is populated with the numbers of orders that have not been marked
<span class="ui">Fulfilled</span>.</p>
</li><li>
<p>Select a number in the <span class="ui">Unfulfilled Orders</span> list.</p>
<p>The worksheet is populated with details from the order.</p>
</li><li>
<p>In the <span class="ui">Indicate Order Status</span> list, click <span class="ui">
Fulfilled</span>.</p>
<p>After the status is set to <span class="ui">Fulfilled</span>:</p>
<ul>
<li>
<p>The order status is saved back to the in-memory dataset.</p>
</li><li>
<p>The quantity ordered for each product in the order is subtracted from the <span class="ui">
UnitsInStock</span> element of that same product in the in-memory dataset. This ensures that the proper
<span class="ui">UnitsInStock</span> value of each product is available for the next order.</p>
</li><li>
<p>The order is removed from the <span class="ui">Unfulfilled Orders</span> list.</p>
</li></ul>
</li></ol>
</div>
<h1 class="heading"><span tabindex="0" style="">Demonstrates</span></h1>
<div id="demonstratesSection" class="section">
<p>This sample demonstrates how to: </p>
<ul>
<li>
<p>Retrieve and use data from an XML file that was extracted from a SQL Server database.
</p>
</li><li>
<p>Display the data using data-bound Excel controls.</p>
</li><li>
<p>Work with multiple sets of master/detail data.</p>
</li></ul>
</div>
</div>
</div>
