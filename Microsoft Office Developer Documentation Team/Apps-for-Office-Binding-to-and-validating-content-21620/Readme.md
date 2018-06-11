# Apps for Office: Binding to and validating content controls in Word 2013
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* apps for Office
## Topics
* Binding
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:53:33
## Description

<div id="header">The CorporateBio.docx file is set as the <span><span class="keyword">StartAction</span></span> property of the task pane app. The document has three content controls (Name, Position, and About Me) that the user should provide values for.
 The following screen shot shows how the document surface when the document is first opened.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<div class="caption">Figure 1. CorporateBio.docx showing the task pane app</div>
<br>
<img id="79581" src="/site/view/file/79581/1/CG_CorpBioWd_fig01.gif" alt="CorporateBio.docx showing the task pane app" width="717" height="619">
<p>The sample shows the following:</p>
<ul>
<li>
<p>How to use JavaScript to add bindings to the content controls in the document.</p>
</li><li>
<p>How to verify that bindings are in place before attempting to retrieve the values from them.</p>
</li><li>
<p>How to retrieve values from content controls and validate that the user has entered required data.</p>
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
<p>The CorporateBio project, which contains:</p>
<ul>
<li>
<p>The CorporateBio.xml manifest file.</p>
</li><li>
<p>The CorporateBio.docx document, which is prepopulated with three RichTextContentControl objects.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>Each object has had its <span><span class="keyword">Title</span></span> property set, which enables it to be bound to in JavaScript.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li></ul>
</li><li>
<p>The CorporateBioWeb project, which contains multiple template files. However, the two files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>CorporateBio.html (in the Pages folder). This contains the HTML user interface that is displayed in the task pane. It consists of a &lt;div&gt; with an id of validationReport, and two buttons.</p>
</li><li>
<p>CorporateBio.js (in the Scripts folder). This script file contains code that runs when the app is loaded. This startup script attempts to add bindings to the content controls in the document. The success or failure of this operation is reported back to the
 CorporateBio.html page. The script file also includes the <span><span class="keyword">Click</span></span> event handlers for the two buttons in CorporateBio.html. One of these buttons retrieves and validates the content in the content controls by accessing
 the bindings that were added in the startup script. The other button provides a stub procedure that simulates submitting the data from the bindings to a back-end system or process, but only if the values from the bindings have been retrieved and validated
 by the first button. In all cases, a suitable report is added to the CorporateBio.html page.</p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the CorporateBio.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose the Ctrl&#43;Shift&#43;B keys.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run and test the sample, choose the F5 key.</p>
<p>The following screen shots show examples of the document at various stages of the process. Figure 2 shows a document opened with content controls successfully bound to a custom XML part.<br>
<br>
</p>
<div class="caption">Figure 2. The status for each binding has been reported in the task pane.</div>
<br>
<img id="79582" src="/site/view/file/79582/1/CG_CorpBioWd_fig02.gif" alt="Open Word document showing successful binding" width="349" height="325">
<p><br>
<br>
Figure 3 shows the task pane app UI after the Validate button has been chosen.</p>
<div class="caption">Figure 3. The bindings have been retrieved.</div>
<br>
<img id="79583" src="/site/view/file/79583/1/CG_CorpBioWd_fig03.gif" alt="Validate button has been chosen" width="351" height="337">
<p><br>
<br>
Figure 4 shows the task pane app UI after the Submit button has been chosen.</p>
<div class="caption">Figure 4. The data has passed validation.</div>
<br>
<img id="79584" src="/site/view/file/79584/1/CG_CorpBioWd_fig04.gif" alt="Data has been validated" width="349" height="308"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app starts with a blank document instead of the one shown in Figure 1, ensure that the
<span><span class="keyword">StartAction</span></span> property of the CorporateBio project is set to CorporateBio\CorporateBio.docx and not just to Word.</p>
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
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp160966.aspx" target="_blank">Bindings object (apps for Office)</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp123511(v=office.15).aspx" target="_blank">Binding to regions in a document or spreadsheet</a></p>
</li></ul>
</div>
</div>
</div>
</div>
