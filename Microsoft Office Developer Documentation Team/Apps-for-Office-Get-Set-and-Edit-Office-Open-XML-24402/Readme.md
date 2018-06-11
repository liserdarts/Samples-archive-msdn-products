# Apps for Office: Get, Set, and Edit Office Open XML
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* Word
* apps for Office
## Topics
* Data Access
* Open XML
* file format
## IsPublished
* True
## ModifiedDate
* 2013-09-18 12:02:55
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left">&nbsp;</td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Get, set, and edit Office Open XML in a Word document</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This sample app shows you how to use the JavaScript methods
<strong>getSelectedDataAsync</strong> and <strong>setSelectedDataAsync</strong> to get or set a variety of rich content types in a Word document. It also can act as a scratch pad to provide you with an easy way to grab the Office Open XML for your own content
 and test your own edited Office Open XML snippets.</p>
</div>
<div>
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>The SampleDoc.docx file is set as the <span>StartAction</span> property of the task pane app. The document contains a range of common rich content types for Word, including the following:</p>
<ul>
<li>
<p>formatted text</p>
</li><li>
<p>styled text</p>
</li><li>
<p>a formatted image</p>
</li><li>
<p>a text box using WordArt formatting</p>
</li><li>
<p>an Office drawing shape</p>
</li><li>
<p>a content control that can be used for binding to a specific location in the document</p>
</li><li>
<p>a formatted table</p>
</li><li>
<p>a styled table</p>
</li><li>
<p>a dynamic SmartArt graphic</p>
</li><li>
<p>a chart</p>
</li></ul>
<p>The following screenshot (Figures 1a and Figure 1b) show how the document surface appears when the solution starts and the SampleDoc.docx file is opened.</p>
<strong>
<div class="caption">Figure 1a. SampleDoc.docx document contents.</div>
</strong><br>
&nbsp;<img id="94884" src="http://i1.code.msdn.s-msft.com/apps-for-office-get-set-69822f25/image/file/94884/1/cg_getsetooxml_fig01a.gif" alt="" width="584" height="766">
<strong>
<div class="caption">Figure 1b. More SampleDoc.docx document contents.</div>
</strong><br>
&nbsp;<img id="94885" src="http://i1.code.msdn.s-msft.com/apps-for-office-get-set-69822f25/image/file/94885/1/cg_getsetooxml_fig01b.gif" alt="" width="581" height="766">
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>The sample uses Office Open XML (the <strong>ooxml</strong> coercion type) rather than HTML or plain text because OOXML coercion enables you to interact with virtually every type of content a user can insert in their document, such as in the examples you
 see in Figures 1a - 1b.</p>
</td>
</tr>
</tbody>
</table>
</div>
<strong>
<div class="caption">Figure 2. Task pane appearance after using the Get&hellip; button to extract Office Open XML for selected content</div>
</strong><br>
&nbsp;<img id="94886" src="http://i1.code.msdn.s-msft.com/apps-for-office-get-set-69822f25/image/file/94886/1/cg_getsetooxml_fig02b.gif" alt="" width="349" height="820">
<p>When you select content and then click the 'Get&hellip;' button, the app uses the JavaScript
<strong>getSelectedDataAsync</strong> method to generate a complete Office Open XML document package that includes the selected document content, and places it in the text area of the task pane (as shown in Figure 2).You can use the text area in that task pane
 as a scratch pad. Copy the Office Open XML markup you retrieve into an XML file, edit it to include just the information you need, and paste it back into the text area of the task pane to test your edited markup.</p>
<p>After you paste or edit the content in the text area of the task pane, you can click in a blank area of the document and then click the
<strong><span class="ui">Insert&hellip;</span></strong> button on the task pane to test the integrity of your markup. That button uses the JavaScript
<strong>setSelectedDataAsync</strong> method to insert the contents of the task pane text area as rich content in Word, using the
<strong>ooxml</strong> coercion type.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office 2013 tools for Visual Studio 2012</p>
</li><li>
<p>Word 2013</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample app contains:</p>
<ul>
<li>
<p>The WD_OpenXML_js project, which contains:</p>
<ul>
<li>
<p>The WD_OpenXML_js.xml manifest file</p>
</li><li>
<p>The SampleDoc.docx document, which is prepopulated with various types of rich content</p>
</li></ul>
</li><li>
<p>The WD_OpenXML_js Web project, which contains multiple template files. However, the two files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>WD_OpenXML_js.html (in the Pages folder). This contains the HTML user interface that is displayed in the task pane. It consists of two HTML buttons that extract and insert Office Open XML, a DIV where status messages will be written, a textarea HTML control
 that is used to show you Office Open XML markup, and instructional text</p>
</li><li>
<p>WD_OpenXML_js.js (in the Scripts folder). This script file contains code that runs when the app is loaded. This startup wires up the Click event handlers for the two buttons in WD_OpenXML_js.html. One of these buttons retrieves the selected document content
 as Office Open XML, and the other button inserts content into the document via OOXML coercion, using the contents of the text area in the task pane.All other files are automatically provided by the Visual Studio project template for apps for Office, and they
 have not been modified in the development of this sample app</p>
</li></ul>
</li></ul>
<p>&nbsp;</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>To configure the sample, open the WD_OpenXML_js.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>To build the sample, choose the Ctrl&#43;Shift&#43;B keys.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>To run the sample, choose the F5 key.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>When you use JavaScript to generate the Office Open XML markup for selected content, it creates an entire document package, which is a far larger payload than you need for inserting just your content. For help interpreting, editing, and simplifying your
 work with Office Open XML for Word apps, see <a href="http://msdn.microsoft.com/EN-US/library/office/apps/dn423225.aspx" target="_blank">
Creating Better Apps for Word with Office Open XML</a>.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>If the app starts with a blank document instead of the one shown in Figure 1, ensure the StartAction property of the WD_OpenXML_js project is set to SampleDoc.docx and not just to Word.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release: Aug 2013.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/jj220060.aspx" target="_blank">Build apps for Office</a></p>
</li><li>
<p><a href="http://www.ecma-international.org/publications/standards/Ecma-376.htm" target="_blank">Standard ECMA-376: Office Open XML File Formats</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/EN-US/library/office/apps/dn423225.aspx" target="_blank">Creating Better Apps for Word with Office Open XML</a></p>
</li></ul>
</div>
</div>
</div>
</div>
