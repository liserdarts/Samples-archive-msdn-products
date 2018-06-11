# Apps for Office: Create an Invoice Manager
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* apps for Office
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-08-08 05:51:43
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Create an invoice manager</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This sample shows how to create a task pane app for Office that manages invoices in Word 2013.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample loads order data into an invoice form in Word 2013. It writes customer data to a set of custom XML parts that are bound to content controls within a Word document. Based on user input, it populates forms in the document with customer and order
 information. To simplify this sample, the order data is stored in the same JavaScript file that creates the app for Office. However, in a real application, that data could come from a data source anywhere on the web.</p>
<p>The JavaScript code in the Home.js file includes a function for the <b><span class="keyword">initialize</span></b> event, which waits for the DOM to load, gets a reference to the current document, and then calls two other functions. The first of these,
<b><span class="keyword">setupMyOrders</span></b>, creates an array to hold the order data.</p>
<p>The second function, <b><span class="keyword">initializeOrder</span></b>, does most of the important work. When the
<b>Populate</b> button is chosen, this function first calls the <a href="http://msdn.microsoft.com/en-us/library/fp142144(office.15).aspx" target="_blank">
getByNamespaceAsync</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp142202(office.15).aspx" target="_blank">
CustomXmlParts</a> object to determine whether the packing slip form is already populated. If it is, the function calls the
<a href="http://msdn.microsoft.com/en-us/library/fp142157(office.15).aspx" target="_blank">
deleteAysnc</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp161160(office.15).aspx" target="_blank">
CustomXmlPart</a> object to delete the existing data in the form. Then it calls the
<a href="http://msdn.microsoft.com/en-us/library/fp161009(office.15).aspx" target="_blank">
addAsync</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp142202.aspx" target="_blank">
CustomXmlParts</a> object to repopulate the form with the selected data.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Word 2013.</p>
</li><li>
<p>Visual Studio 2012; App for Office 2013 project template.</p>
</li><li>
<p>Internet Explorer 9 or Internet Explorer 10.</p>
</li><li>
<p>Basic familiarity with JavaScript and HTML.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The <i>Apps for Office: Create an Invoice Manager</i> sample is created by the InvoiceManager solution, which contains the following projects and important files:</p>
<ul>
<li>
<p>The InvoiceManager project, including the following files:</p>
<ul>
<li>
<p>InvoiceManager.xml manifest file</p>
</li><li>
<p>Packing Slip Document.docx file</p>
</li></ul>
</li><li>
<p>The InvoiceManagerWeb project, including the following files:</p>
<ul>
<li>
<p>Home.html file</p>
</li><li>
<p>Home.js file</p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>No additional configuration is necessary.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Choose the F5 key in Visual Studio 2012 to build and deploy the app and open it in Word 2013.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<ol>
<li>
<p>Open the InvoiceManager.sln file in Visual Studio 2012.</p>
</li><li>
<p>Choose the F5 key in Visual Studio 2012 to build and deploy the app.</p>
</li><li>
<p>In the app task pane, select an order in the <b><span class="ui">Order ID</span></b> drop-down list.</p>
</li><li>
<p>Choose <b><span class="ui">Populate</span></b> to populate the forms in the Word document with information from the selected order.</p>
</li></ol>
<p>You can view a list of the custom XML parts in a document by opening the <b><span class="ui">XML Mapping</span></b> pane in Word (<b><span class="ui">Developer</span></b> tab).</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6" name="collapseableSection">
<p>If the app fails to respond as described, try reloading it. (In the task pane, choose the down arrow, and then choose
<b><span class="ui">Reload</span></b>.)</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection">
<p>Third release.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142185(office.15).aspx" target="_blank">JavaScript API for Office</a>
</p>
</li></ul>
</div>
</div>
</div>
