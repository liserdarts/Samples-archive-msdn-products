# Apps for Office: Slicing a PowerPoint presentation into chunks
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* apps for Office
* PowerPoint 2013
## Topics
* Cloud
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:49:13
## Description

<div id="header">The Adventure Works.pptx file is set as the <span><span class="keyword">StartAction</span></span> property of the task pane app. The presentation is large enough to be sliced into a number of discrete chunks of data. The following screen
 shot shows how the document and the app will appear after you start the solution.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<div class="caption">Figure 1. Adventure Works.pptx showing the task pane app</div>
<br>
<img id="79576" src="/site/view/file/79576/1/CG_ReadMePPChunks_fig01.gif" alt="Adventure Works.pptx showing the task pane app" width="596" height="241">
<p><br>
<br>
The sample shows how to use JavaScript to retrieve the selected value from the drop-down list shown in Figure 1.</p>
<p>The sample then shows how to use the <span><span class="keyword">getFileAsync</span></span> method to slice the file into chunks of data of the size specified in the drop-down list.</p>
<p>Finally, the sample shows how to retrieve the data from each slice of the file by using the
<span><span class="keyword">getSliceAsync</span></span> method.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012 (RTM)</p>
</li><li>
<p>Office 2013 tools for Visual Studio 2012 (RTM)</p>
</li><li>
<p>PowerPoint 2013 (RTM)</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains:</p>
<ul>
<li>
<p>The PowerPointEBookPublisher project, which contains:</p>
<ul>
<li>
<p>The PowerPointEBookPublisher.xml manifest file.</p>
</li><li>
<p>The Adventure Works.pptx document, which is a relatively large file that contains multiple pictures.</p>
</li></ul>
</li><li>
<p>The PowerPointEBookPublisherWeb project, which contains multiple template files. However, the files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>PowerPointEBookPublisher.html (in the Pages folder). This contains the HTML user interface that is displayed in the task pane. It consists of a &lt;div&gt; with an ID of
<span><span class="keyword">transmissionReport</span></span>, a button with an ID of
<span><span class="keyword">sendData</span></span>, and a &lt;SELECT&gt; drop-down list with an ID of
<span><span class="keyword">chunkSize</span></span>.</p>
</li><li>
<p>PowerPointEBookPublisher.js (in the Scripts folder). This script file contains code that runs when the app is loaded.</p>
</li><li>
<p>jQuery.ui.js (in the Scripts folder). This script file contains code that allows the app to show data in a jQuery dialog box.</p>
</li><li>
<p>jQuery.ui.css (in the Content folder). This CSS file contains styles that show data in a jQuery dialog box.</p>
</li><li>
<p>An Images folder (in the Content folder) that contains images for use in the jQuery dialog box.</p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for Office Apps, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the PowerPointEBookPublisher.sln file with Visual Studio 2012. No other configuration is necessary..</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Choose the Ctrl&#43;Shift&#43;B keys to build the solution.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Choose the F5 key to run the app.</p>
<p>The following screen shots show examples of the document at various stages of the process.</p>
<p>Figure 2 shows the PowerPoint presentation opened and the task pane app UI with a chunk size value selected.</p>
<div class="caption">Figure 2. The chunk size is selected.</div>
<br>
<img id="79577" src="/site/view/file/79577/1/CG_ReadMePPChunks_fig02.gif" alt="Task Pane app showing file chunk size dropdown" width="349" height="382">
<p>Figure 3 shows the task pane UI after the <span class="ui">Publish Now</span> button has been chosen and the presentation has been sliced into chunks of the size specified.</p>
<div class="caption">Figure 3. The Publish Now button has been chosen.</div>
<br>
<img id="79578" src="/site/view/file/79578/1/CG_ReadMePPChunks_fig03.gif" alt="Task Pane app showing Publish Now button chosen" width="348" height="432">
<p>Figure 4. shows the task pane UI after one of the [View raw data] buttons has been chosen, and the actual data from the given slice is shown in a jQuery dialog box.</p>
<div class="caption">Figure 4. The raw data displayed in a jQuery dialog box.</div>
<br>
<img id="79579" src="/site/view/file/79579/1/CG_ReadMePPChunks_fig04.gif" alt="Task Pane app showing Chunk Data" width="348" height="409">
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>In your solution, you probably would not show the user the sliced text, but would instead have sent the slice to a web service when the user chose
<span class="ui">Publish Now</span> (as in the previous step). That web service would rebuild the presentation from its various slices.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app starts with a blank document instead of the one shown in Figure 1, ensure that the
<span><span class="keyword">StartAction</span></span> property of the PowerPointEBookPublisher project is set to Adventure Works.pptx and not just to PowerPoint.</p>
<p>&nbsp;</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: March, 2012.</p>
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
