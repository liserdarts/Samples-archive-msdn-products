# Apps for Office: Getting and setting complex document content using Open XML
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
* 2013-05-08 09:49:56
## Description

<div id="header">The ComplexDoc.docx file is set as the <span><span class="keyword">StartAction</span></span> property of the task pane app. The document contains a mixture of images with various layout options and text. The following screenshot shows how
 the document surface appears when the solution starts and the ComplexDoc.docx file is opened.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<div class="caption">Figure 1. ComplexDoc.docx document surface.</div>
<br>
<img id="79586" src="/site/view/file/79586/1/CG_RetrieveInsertOOXM_fig01.gif" alt="ComplexDoc.docx showing the task pane app" width="718" height="592">
<p>The sample shows how to use JavaScript to extract Open XML from a potentially complex document. The sample also shows how to insert a fragment of Open XML into a document.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>The sample uses Open XML instead of HTML or plain text, because only Open XML is capable of handling the Base64 data that represents the images in the document. Also, Open XML is potentially more powerful at describing text flows with images (such as those
 shown in Figure 1).</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012 (RTM).</p>
</li><li>
<p>Office 2013 tools for Visual Studio 2012 (RTM).</p>
</li><li>
<p>Word 2013 (RTM).</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains:</p>
<ul>
<li>
<p>The WD_OpenXML_js project, which contains:</p>
<ul>
<li>
<p>The WD_OpenXML_js.xml manifest file.</p>
</li><li>
<p>The ComplexDoc.docx document, which is prepopulated with various images, tables, and formatted textual content.</p>
</li></ul>
</li><li>
<p>The WD_OpenXML_js Web project, which contains multiple template files. However, the two files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>WD_OpenXML_js.html (in the Pages folder). This contains the HTML user interface that is displayed in the task pane. It consists of two HTML buttons that extract and insert Open XML, a DIV where status messages will be written, and a textarea HTML control
 that is used to show you Open XML fragments.</p>
</li><li>
<p>WD_OpenXML_js.js (in the Scripts folder). This script file contains code that runs when the app is loaded. This startup wires up the Click event handlers for the two buttons in WD_OpenXML_js.html. One of these buttons retrieves the selected area of the document
 as Open XML, and the other button inserts Open XML into the document.</p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the WD_OpenXML_js.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose the Ctrl&#43;Shift&#43;B keys.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run the app, choose the F5 key.</p>
<p>The following images show examples of the document at various stages of the process.</p>
<p>Figure 2 shows a selection of content in the complex document for which to extract Open XML.</p>
<div class="caption">Figure 2. The document with complex content selected.</div>
<br>
<img id="79587" src="/site/view/file/79587/1/CG_RetrieveInsertOOXM_fig02.gif" alt="ComplexDoc.docx showing the task pane app" width="706" height="500">
<p>&nbsp;</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>It is recommended that you select all the content between the two instructions as shown the first time you run the sample, so that you can see the full power of Open XML. You can then experiment with selecting smaller sections after that.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>Figure 3 shows the Open XML for the selected section of the document.</p>
<div class="caption">Figure 3. The Open XML for the content you have selected.</div>
<br>
<img id="79588" src="/site/view/file/79588/1/CG_RetrieveInsertOOXM_fig03.gif" alt="ComplexDoc.docx showing the task pane app" width="350" height="832">
<p>&nbsp;</p>
<p>Figure 4 shows the result of inserting the Open XML that you extracted back into the document surface at the insertion point.</p>
<div class="caption">Figure 4. The document surface after inserting Open XML.</div>
<br>
<img id="79589" src="/site/view/file/79589/1/CG_RetrieveInsertOOXM_fig05.gif" alt="ComplexDoc.docx showing the task pane app" width="686" height="608"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app starts with a blank document instead of the one shown in Figure 1, ensure the
<span><span class="keyword">StartAction</span></span> property of the WD_OpenXML_js project is set to ComplexDoc.docx and not just to Word.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: March 15, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/jj220060.aspx" target="_blank">Build apps for Office</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/bb448854.aspx" target="_blank">Open XML SDK 2.5 CTP for Office</a></p>
</li></ul>
</div>
</div>
</div>
</div>
