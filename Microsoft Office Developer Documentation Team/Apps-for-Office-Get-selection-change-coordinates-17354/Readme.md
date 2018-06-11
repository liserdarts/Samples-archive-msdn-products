# Apps for Office: Get selection-change coordinates in an Excel table
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Excel 2013
* apps for Office
## Topics
* Binding
## IsPublished
* True
## ModifiedDate
* 2013-07-11 01:01:33
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Get selection-change coordinates in an Excel table</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p><span>Summary:</span> This sample app shows how to detect when the selection changes in a table (matrix) in Excel 2013, and then how to display the table columns and rows included in the selection.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0" name="collapseableSection">
<p>The <i>Apps for Office: Get selection-change coordinates in an Excel table</i> sample opens a blank Excel 2013 file. The user must first enter values in a contiguous rectangular collection of cells, thereby creating a table (matrix); enter values in the
 table; and then select the table. When the user then chooses <b><span class="ui">Set Binding</span></b>, the app binds to that table.</p>
<p>Choosing <b><span class="ui">Set Binding</span></b> executes the <span value="bindNamedItem">
<b><span class="keyword">bindNamedItem</span></b></span> function, in the Home.js file. This function uses the
<a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142282(v=office.15)" target="_blank">
addFromSelectionAsync</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp160966(v=office.15)" target="_blank">
Bindings</a> object of the JavaScript API for Office to create a new binding of coercion type &quot;matrix.&quot; The function then calls the
<b>addEventHandler</b> function, which uses the <a href="http://msdn.microsoft.com/en-us/library/fp161008(v=office.15)" target="_blank">
getByIdAsync</a> method of the <span value="Bindings"><b><span class="keyword">Bindings</span></b></span> object to identify the binding by its ID (&quot;myMatrix&quot;) and add a handler for the
<a href="http://msdn.microsoft.com/en-us/library/fp161088(v=office.15)" target="_blank">
bindingSelectionChanged</a> event of the <a href="http://msdn.microsoft.com/en-us/library/fp161045.aspx" target="_blank">
Binding</a> object to the binding. Then, when the user makes a new selection in the table, the event handler uses the
<a href="http://msdn.microsoft.com/en-US/library/fp179809" target="_blank">startRow</a>,
<a href="http://msdn.microsoft.com/en-US/library/fp179837" target="_blank">startColumn</a>,
<a href="http://msdn.microsoft.com/en-US/library/fp179813" target="_blank">columnCount</a>, and
<a href="http://msdn.microsoft.com/en-US/library/fp179805" target="_blank">rowCount</a> properties of the
<a href="http://msdn.microsoft.com/library/9b879ce5-e59c-4059-b488-c51eddfdca5b" target="_blank">
BindingSelectionChangedEventArgs</a> object to display information about the new selection in the task pane.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Excel 2013.</p>
</li><li>
<p>Visual Studio 2012; apps for Office project template.</p>
</li><li>
<p>Internet Explorer 9 or Internet Explorer 10.</p>
</li><li>
<p>Basic familiarity with JavaScript and HTML.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The <i>Apps for Office: Get selection-change coordinates in an Excel table</i> sample is created by the Display Excel Selection Coordinates solution, which contains the following projects and important files:</p>
<ul>
<li>
<p>The Display Excel Selection Coordinates project, which contains the following file:</p>
<ul>
<li>
<p>Display Excel Selection Coordinates.xml manifest file</p>
</li></ul>
</li><li>
<p>The Display Excel Selection CoordinatesWeb project, which contains the following files:</p>
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
<p>Choose the F5 key in Visual Studio 2012 to build and deploy the app and open it in Excel 2013.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<p></p>
<ol>
<li>
<p>Open the Display Excel Selection Coordinates.sln file in Visual Studio 2012.</p>
</li><li>
<p>Choose the F5 key to build and deploy the app.</p>
</li><li>
<p>Create a table by entering values in several contiguous cells occupying at least three rows and three columns.</p>
</li><li>
<p>Select the entire table</p>
</li><li>
<p>In the app task pane, choose <b><span class="ui">Set Binding</span></b>.</p>
</li><li>
<p>Select one or more contiguous cells in the table.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6" name="collapseableSection">
<p>If the app fails to respond as described, try reloading it. (In the task pane, choose the down arrow, and then choose the
<b><span class="ui">Reload</span></b> button.)</p>
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
