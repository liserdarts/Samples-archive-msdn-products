# Apps for Office: Slicing a Word document into chunks
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* apps for Office
## Topics
* Cloud
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:52:08
## Description

<div id="header">The DocumentForEditing.docx file is set as the StartAction property of the task pane app. The document is large enough (500 pages) to be sliced into a number of discrete chunks of data. The following screen shot shows how the document and
 the app will appear after you start the solution.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>Figure 1 shows the UI for the WD_WordDocumentEmitter app.</p>
<div class="caption">Figure 1. The WD_WordDocumentEmitter running in the task pane.</div>
<br>
<img id="79592" src="/site/view/file/79592/1/CG_WDChunks_fig01.gif" alt="DocumentForEditing.docx Word document UI" width="350" height="831">
<p>The sample shows:</p>
<ul>
<li>
<p>How to use JavaScript to retrieve the selected value from the drop-down list shown in Figure 1.</p>
</li><li>
<p>How to use the <span><span class="keyword">getFileAsync</span></span> method to slice the file into chunks of data of the size specified in the drop-down list.</p>
</li><li>
<p>How to retrieve the data from each slice of the file by using the <span><span class="keyword">getSliceAsync</span></span> method.</p>
</li></ul>
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
<p>The WD_WordDocumentEmitter_js project, which contains:</p>
<ul>
<li>
<p>The WD_WordDocumentEmitter_js.xml manifest file.</p>
</li><li>
<p>The DocumentForEditing document, which is a relatively large file that contains 500 pages of text.</p>
</li></ul>
</li><li>
<p>The WD_WordDocumentEmitter_jsWeb project, which contains multiple template files. However the files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>WD_WordDocumentEmitter_js.html (in the Pages folder). This contains the HTML user interface that is displayed in the task pane. It consists of a &lt;div&gt; with an ID of
<span><span class="keyword">transmissionReport</span></span>, a button with an ID of
<span><span class="keyword">sendData</span></span>, and a &lt;SELECT&gt; drop-down list with an ID of
<span><span class="keyword">chunkSize</span></span>.</p>
</li><li>
<p>WD_WordDocumentEmitter.js (in the Scripts folder). This script file contains code that runs when the app is loaded.</p>
</li><li>
<p>jQuery.ui.js (in the Scripts folder). This script file contains code that allows the app to show data in a jQuery dialog box.</p>
</li><li>
<p>jQuery.ui.css (in the Content folder). This CSS file contains styles that show data in a jQuery dialog box.</p>
</li><li>
<p>An Images folder (in the Content folder) that contains images for use in the jQuery dialog box.</p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the WD_WordDocumentEmitter_js.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose the Ctrl&#43;Shift&#43;B keys to build the solution.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Choose the F5 key to run the app.</p>
<p>The following figures show examples of the document at various stages of the process:</p>
<p>Figure 2 shows the document open, and a value for the chunk size that the app will use to slice the document selected in the dropdown box.</p>
<div class="caption">Figure 2. The UI for the app displayed with a chunk size selected.</div>
<br>
<img id="79593" src="/site/view/file/79593/1/CG_WDChunks_fig02.gif" alt="Document open and chunk size selected" width="351" height="832">
<p>Figure 3 shows that the user has chosen the [Publish Now] button and that the document has been sliced into chunks of the size specified.</p>
<div class="caption">Figure 3. The document has been divided into chunks.</div>
<br>
<img id="79594" src="/site/view/file/79594/1/CG_WDChunks_fig03.gif" alt="Document open and Publish Now button chosen" width="350" height="833">
<p>Figure 4 shows the chunk data from one of the document chunks.</p>
<div class="caption">Figure 4. Indvidual chunk data displayed in a floating dialog box.</div>
<br>
<img id="79595" src="/site/view/file/79595/1/CG_WDChunks_fig04.gif" alt="View raw data buttons clicked and raw data shown" width="350" height="832">
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>In your solution, you probably would not show the user the sliced text, but would instead have sent the slice to a web service when the user chose
<span class="ui">Publish Now</span> (as in the previous step). That web service would rebuild the document from its various slices and process it for publishing, editing, or translation, for example.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app starts with a blank document instead of the one shown in Figure 1, ensure that the
<span><span class="keyword">StartAction</span></span> property of the WD_WordDocumentEmitter_js project is set to DocumentForEditing.docx and not just to Word.</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/jj715284.aspx" target="_blank">Document.getFileAsync method</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/jj715281.aspx" target="_blank">File.getSliceAsync method</a></p>
</li></ul>
</div>
</div>
</div>
</div>
