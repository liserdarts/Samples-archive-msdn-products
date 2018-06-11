# Apps for Office: Bind an app to a table inserted into Excel or Word
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* Excel 2013
* apps for Office
## Topics
* App model
* apps for Office
* Binding
## IsPublished
* True
## ModifiedDate
* 2014-03-25 01:22:57
## Description

<p id="header"><span class="label">Summary:</span> Demonstrates how to use the JavaScript API for Office to bind an app to a named table in Microsoft Excel 2013 or Microsoft Word 2013, extract data from the table, react to events in the table, and set data
 back into the table.</p>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description</h1>
<div id="sectionSection0" class="section">
<p>This app code sample gets binds a Stock Ticker app to a named table that is included in the solution project. The code that uses the Binding object of the JavaScript API for Office is included in the UpdateTable.js file of the solution. The code sample binds
 to a table named &quot;Stocks&quot; on the worksheet labeled &quot;Sheet1.&quot;</p>
<p>The sample demonstrates how to establish a binding between a region in an Office file&mdash;a named table, in this case&mdash;and an app for Office. Once the binding has been established, the app adds a handler to the
<span><span class="keyword">BindingDataChanged</span></span> event of the binding. When the event handler executes, the app retrieves a selection of the data from the table. It then removes the event handler from the binding, updates the table, and then adds
 the event handler back to the binding.</p>
<p>For more information about the JavaScript API for Office and working with bindings, see
<a href="http://msdn.microsoft.com/en-us/library/office/apps/fp123511.aspx" target="_blank">
Binding to regions in a document or spreadsheet</a>.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div id="sectionSection1" class="section">
<div>This sample requires the following:</div>
<ul>
<li>
<div>Word 2013 or Excel 2013</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Internet Explorer 9 or Internet Explorer 10.</div>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div id="sectionSection2" class="section">
<div>The Stock Ticker sample app contains the following:</div>
<ul>
<li>
<div><strong>CodeSample_BindingAppToXLTable</strong> project.</div>
<ul>
<li>
<div><strong>CodeSample_BindingAppToXLTable.xml</strong> manifest file</div>
</li><li>
<div><strong>Book1.xlsx</strong> file that contains a table named &quot;Stocks&quot; on a spreadsheet named &quot;Sheet1.&quot;</div>
</li></ul>
</li><li>
<div><strong>CodeSample_BindingAppToXLTableWeb</strong> project</div>
<ul>
<li>
<div><strong>Home.html</strong> file, which contains the HTML control for the app's user interface.</div>
</li><li>
<div><strong>Home.js</strong> file, which contains the event handler for the <span>
<span class="keyword">Office.initialize</span></span> event of the app.</div>
</li><li>
<div><strong>UpdateTable.js</strong> file, which contains the self-executing anonymous function that creates the binding, adds an event handler to the binding, and contains all of the methods for getting and setting data from the binding.</div>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div id="sectionSection3" class="section">
<p>To configure the Stock Ticker, set the <span class="ui">StartAction</span> property of the
<strong>CodeSample_BindingAppToTable</strong> project to 'Book1.xslx'.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div id="sectionSection4" class="section">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div id="sectionSection5" class="section">
<ol>
<li>
<div>Choose the F5 key to build and deploy the app.</div>
</li><li>
<div>Insert the app into the Book1.xlsx file when you debug the sample (<span class="ui">Insert</span> tab,
<span class="ui">Apps for Office</span> button).</div>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<div>If you save the workbook while debugging, the app is persisted in the workbook. If you do so, you won't have to reinsert the app into the workbook during future debugging sessions.</div>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<div>In the app, choose <span class="ui">Set binding</span>.</div>
</li><li>
<div>In the table on Book1.xlsx, make a change to one of the values in the first column in the right.</div>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div id="sectionSection6" class="section">
<p>If the app fails to install, ensure that the XML in your <strong>AppManifest.xml</strong> file parses correctly.</p>
<p>If you change the code in the <span class="code">StockTicker.getStockQuotes</span> method to call an external stock quote service, be aware that cross-domain scripting restrictions still apply.</p>
<p>If the app generates errors whenever you try to update the table, ensure that you have entered correct values for the
<span class="code">tableName</span> and <span class="code">bindingName</span> variables in the
<strong>UpdateTable.js</strong> file.</p>
</div>
<h1 class="heading">Change log</h1>
<div id="sectionSection7" class="section">
<p>First release. March 8, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div id="sectionSection8" class="section">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp123511.aspx" target="_blank">Binding to regions in a document or spreadsheet</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp160966.aspx" target="_blank">Bindings object</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161045.aspx" target="_blank">Binding object</a></div>
</li></ul>
</div>
</div>
</div>
