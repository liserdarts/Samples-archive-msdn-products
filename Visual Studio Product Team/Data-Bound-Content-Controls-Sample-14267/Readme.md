# Data-Bound Content Controls Sample
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
* 2011-11-28 06:08:03
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
<p>This sample runs in Microsoft Office Word 2007 and higher.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>This sample demonstrates how to bind content controls on a Word document to fields in a Microsoft Office Access database. Each content control on the document is bound to a different field in the Northwind sample database. The document also displays an actions
 pane that you can use to navigate through employee records, search for a specific employee record, and save changes that you make in one of the content controls to the database.</p>
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
<p>For information about how to install the sample project on your computer, see How to: Install and Use Sample Files Found in Help.</p>
</div>
<h3 class="procedureSubHeading">To run this sample</h3>
<div class="subSection">
<ol>
<li>
<p>Press F5.</p>
<p>Verify that the content controls in the table display information about an employee named Nancy Davolio.</p>
</li><li>
<p>In the actions pane, click the <span class="ui">Next</span> and <span class="ui">
Previous</span> buttons to view different employee records. </p>
<p>Verify that the content controls display information from the currently selected employee record.</p>
</li><li>
<p>In the actions pane, enter a number from 1 through 9 in the <span class="ui">
Input ID</span> text box, and click the <span class="ui">Search by ID</span> button.</p>
<p>Verify that the content controls display information from the employee record that matches the ID.</p>
</li><li>
<p>Click in the cell to the right of the <span class="ui">Title</span> cell and type a new title for the current employee.</p>
</li><li>
<p>In the actions pane, click the <span class="ui">Submit change to Title</span> button to save the new title to the database.</p>
</li><li>
<p>Open the Northwind.mdb database in Access, and verify that the <span class="ui">
Title</span> field of the employee has the new title.</p>
</li></ol>
</div>
<h1 class="heading"><span tabindex="0" style="">Requirements</span></h1>
<div id="requirementsTitleSection" class="section">
<p>This sample requires the following applications:</p>
<ul>
<li>
<p>Visual Studio Tools for Office.</p>
</li><li>
<p>Microsoft Office Word 2007.</p>
</li><li>
<p>Microsoft Office Access.</p>
</li><li>
<p>The Northwind sample database for Microsoft Office Access (included in the sample).</p>
</li></ul>
</div>
<h1 class="heading"><span tabindex="0" style="">Demonstrates</span></h1>
<div id="demonstratesSection" class="section">
<p>This sample demonstrates the following concepts:</p>
<ul>
<li>
<p>Binding the following content controls to database fields:</p>
<ul>
<li>
<p>PlainTextContentControl</p>
</li><li>
<p>PictureContentControl</p>
</li><li>
<p>DatePickerContentControl</p>
</li></ul>
</li><li>
<p>Saving data in a data-bound content control to the database (this is also named two-way data binding).</p>
</li><li>
<p>Using the actions pane.</p>
</li></ul>
</div>
</div>
</div>
