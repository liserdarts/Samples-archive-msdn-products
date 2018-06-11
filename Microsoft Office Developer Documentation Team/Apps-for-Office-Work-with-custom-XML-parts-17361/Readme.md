# Apps for Office: Work with custom XML parts
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* apps for Office
## Topics
* Binding
## IsPublished
* True
## ModifiedDate
* 2013-08-08 05:54:30
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Work with custom XML parts</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This sample writes user-specified text to a custom XML part that is bound to a content control within a Word 2013 document.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0" name="collapseableSection">
<p>The <i>Apps for Office: Work with custom XML parts</i> sample opens a Word file, CustomXMLPartSample.docx, which already contains a custom XML part that has the namespace
<b>TimeSummary</b>. When the user enters text in the <b><span class="ui">Client Name</span></b> text box and chooses
<b><span class="ui">Update Client</span></b>, the <b><span class="keyword">cmdUpdateClient_onClick</span></b> function is executed. This function uses the
<a href="http://msdn.microsoft.com/en-us/library/fp142144(office.15).aspx" target="_blank">
getByNamespaceAsync</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp142202(office.15).aspx" target="_blank">
CustomXmlParts</a> object to identify the custom XML part whose namespace is <b><span class="keyword">TimeSummary</span></b>. The function is passed a callback function,
<b><span class="keyword">onGotXml</span></b>, which uses the <a href="http://msdn.microsoft.com/en-us/library/fp161017(office.15).aspx" target="_blank">
getNodesAsync</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp161160(office.15).aspx" target="_blank">
CustomXmlPart</a> object to get the Client node of the XML part. That function, in turn, is passed another callback function,
<b><span class="keyword">onGotNode</span></b>, which uses the <a href="http://msdn.microsoft.com/en-us/library/fp161052(office.15).aspx" target="_blank">
setXmlAsync</a> method of the <a href="http://msdn.microsoft.com/en-us/library/fp142260(office.15).aspx" target="_blank">
CustomXmlNode</a> object to set the text in the content control that is bound to the client node of the Time Summary custom XML part to the new text.</p>
<p>Choosing <b><span class="ui">Is Part Built In</span></b> executes the <b><span class="keyword">showXMLPartBuiltIn</span></b> function, which uses the
<a href="http://msdn.microsoft.com/en-us/library/fp161022(office.15).aspx" target="_blank">
getByIdAsync</a> method of the <b><span class="keyword">CustomXmlParts</span></b> object to get the Time Summary part, and then it uses the
<a href="http://msdn.microsoft.com/en-us/library/fp161095(office.15).aspx" target="_blank">
builtIn</a> property of the <b><span class="keyword">CustomXmlPart</span></b> object to test whether the part is built-in or custom.</p>
<p>Choosing <b><span class="ui">Add Part</span></b> executes the <b><span class="keyword">addCustomXMLPart</span></b> function, which uses the
<a href="http://msdn.microsoft.com/en-us/library/fp161009(office.15).aspx" target="_blank">
addAsync</a> method of the <b><span class="keyword">CustomXmlParts</span></b> object to add a new custom XML part named NewCustomXmlPart to the document. Choosing
<b><span class="ui">Delete Part</span></b> executes the <b><span class="keyword">deletePart</span></b> function, which uses the
<a href="http://msdn.microsoft.com/en-us/library/fp142157(office.15).aspx" target="_blank">
deleteAysnc</a> method of the <b><span class="keyword">CustomXmlPart</span></b> object to delete the part.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Word 2013.</p>
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
<p>The <i>Apps for Office: Work with custom XML parts</i> sample is created by the CustomXMLApp solution, which contains the following projects and important files:</p>
<ul>
<li>
<p>The CustomXMLApp project, including the following files:</p>
<ul>
<li>
<p>CustomXMLApp.xml manifest file</p>
</li><li>
<p>CustomXMLPartSample.docx file</p>
</li></ul>
</li><li>
<p>The CustomXMLAppWeb project, including the following files:</p>
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
<p>Open the CustomXMLApp.sln file in Visual Studio 2012.</p>
</li><li>
<p>Choose the F5 key in Visual Studio 2012 to build and deploy the app.</p>
</li><li>
<p>In the app task pane, enter text into the <b><span class="ui">Client name</span></b> box, and then choose
<b><span class="ui">Update client name</span></b>.</p>
</li><li>
<p>Choose <b><span class="ui">Is Custom part built in?</span></b> to determine whether the Time Summary part of the document is built in.</p>
</li><li>
<p>Choose <b><span class="ui">Add CustomXML part</span></b> to add a new part to the document, and choose
<b><span class="ui">Delete CustomXML part(s)</span></b> to delete the part or parts.</p>
</li></ol>
<p>You can view a list of the custom XML parts in a document by opening the <b><span class="ui">XML Mapping</span></b> pane in Word (<b><span class="ui">Developer</span></b> tab).</p>
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
