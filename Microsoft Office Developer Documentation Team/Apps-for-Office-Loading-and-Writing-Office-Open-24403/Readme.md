# Apps for Office: Loading and Writing Office Open XML
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* Word
* apps for Office
## Topics
* Open XML
## IsPublished
* True
## ModifiedDate
* 2013-08-28 04:37:35
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Loading and writing Office Open XML</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This sample app shows you how to add a variety of rich content types to a Word document using the
<strong>setSelectedDataAsync</strong> method with <strong>ooxml</strong> coercion type. The app also gives you the ability to show the Office Open XML markup for each sample content type right on the page.</p>
</div>
<div>
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>The app initializes in a blank Word document. You choose an option to insert the content or its markup at the selection point in the active Word document and then click the object type you want from the following options:</p>
<ul>
<li>
<p>formatted text</p>
</li><li>
<p>styled text</p>
</li><li>
<p>a simple image</p>
</li><li>
<p>a formatted image</p>
</li><li>
<p>a text box</p>
</li><li>
<p>an Office drawing shape</p>
</li><li>
<p>a content control</p>
</li><li>
<p>a formatted table</p>
</li><li>
<p>a styled table</p>
</li><li>
<p>a SmartArt diagram</p>
</li><li>
<p>a chart</p>
</li></ul>
<p>Figure 1 shows how the task pane for the sample app appears when the solution starts.</p>
<strong>
<div class="caption">Figure 1. The Loading and Writing OOXML task pane</div>
<img src="/site/view/file/94879/1/image.png" alt=""></strong><br>
&nbsp;
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>When you choose the option to see the markup for a selected type of content, what you're seeing is the Office Open XML edited to remove unnecessary markup, along with a few tips for additional guidance. You can also review any piece of markup used in the
 app (with formatting to make it easier to navigate) directly in the Visual Studio solution. For further help interpreting, editing, and simplifying your work with Office Open XML for apps for Word, see
<a href="http://msdn.microsoft.com/EN-US/library/office/apps/dn423225.aspx" target="_blank">
Creating Better Apps for Word with Office Open XML</a>.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>Figures 2a - 2b show how the document surface and task pane appear after extracting Office Open XML from the selection.</p>
<strong>
<div class="caption">Figure 2a. Document surface appearance after using the 'Get&hellip;' button to extract Office Open XML for selected content</div>
</strong><br>
&nbsp;<img src="/site/view/file/94880/1/image.png" alt="">
<p><strong>&nbsp;</strong></p>
<strong>
<div class="caption">Figure 2b. Task pane appearance after using the 'Get&hellip;' button to extract Office Open XML for selected content</div>
</strong><br>
&nbsp;<img src="/site/view/file/94878/1/image.png" alt=""></div>
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
<p>The LoadingAndWritingOOXML project, which contains:</p>
<ul>
<li>
<p>The LoadingAndWritingOOXML.xml manifest file</p>
</li><li>
<p>The LoadingAndWritingOOXML Web project, which contains multiple template files</p>
</li></ul>
</li><li>
<p>However, the files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>LoadingAndWritingOOXML.html (in the App folder, LoadingAndWritingOOXML subfolder). This contains the HTML user interface that is displayed in the task pane. It consists of two HTML radio buttons for choosing the option to insert a selected content type or
 display its markup in Word, several buttons for selecting a content type, and instructional text</p>
</li><li>
<p>LoadingAndWritingOOXML.js (in the same folder as above). This script file contains code that runs when the app is loaded. This startup wires up the Click event handlers for the eleven buttons in LoadingAndWritingOOXML.html that represent different content
 types. The handler in the JavaScript connects each button to the correct function based on the actively-selected radio button, to either write the content or its markup into the document.</p>
</li><li>
<p>Several XML files containing the markup for each of the content types you can insert via the app. These are located in the folder named OOXMLSamples. (Note that some content types have a separate XML file for the markup when inserting the object vs. displaying
 the markup on the page because chunks of binary data where applicable (i.e., for pictures and charts) are removed from the markup displayed on the page for ease of review. To learn more about the binary data contained in some types of Office Open XML markup,
 see the previously-referenced article <a href="http://msdn.microsoft.com/EN-US/library/office/apps/dn423225.aspx" target="_blank">
Creating Better Apps for Word with Office Open XML</a></p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>To configure the sample, open the LoadingAndWritingOOXML.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>To build the sample, choose the Ctrl&#43;Shift&#43;B keys.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>To run the sample, choose the F5 key.</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>If the app fails to respond as described, try reloading it. (In the task pane, choose the down arrow, and then choose Reload.)</p>
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
&nbsp;</div>
