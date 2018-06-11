# App for Office: Set simple document data
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* Excel 2013
* PowerPoint 2013
## Topics
* apps for Office
## IsPublished
* True
## ModifiedDate
* 2014-02-26 05:40:10
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Set simple document data</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> Demonstrates an app for Excel, Word, or PowerPoint that inserts simple document data (text, matrix, table, or HTML) into a document.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0">
<p>The app in this code sample inserts 4 types of simple document data into an Office document: text, matrix, table, or HTML. The user chooses the button corresponding to the kind of data that should be inserted. You can customize the content for each type
 of data by editing the code in the Home.js file that is in the Visual Studio solution file. This code sample is useful for those just getting started with the apps for Office API.</p>
<p>Figure 1 shows the completed app after it has been inserted into the Office application.</p>
<strong></strong>
<div class="caption"><strong>Figure 1. The Simple Content Insertion sample app running in Word 2013.
</strong></div>
<strong></strong><br>
<img src="/site/view/file/108542/1/image.png" alt=""><br>
<br>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Only Microsoft Word supports inserting each one of the types of data listed above into a document. If you use the sample app to try to insert a type of data that isn't supported into one of the other Office document types, such as a PowerPoint slide, an
 error message will be displayed.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>&nbsp;</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Word 2013, Excel 2013, or PowerPoint 2013</p>
</li><li>
<p>Visual Studio 2012 with Office Developer Tools for Visual Studio 2012, or vsdev12short</p>
</li><li>
<p>Internet Explorer 9 or higher</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2">
<p>&nbsp;</p>
<ul>
<li>
<p>data_insertion project</p>
<ul>
<li>
<p>data_insertion.xml manifest file</p>
</li></ul>
</li><li>
<p>data_insertionWeb project</p>
<ul>
<li>
<p>Home.html file, which contains the HTML control for the app's user interface.</p>
</li><li>
<p>Home.js file, which contains the event handler for the <strong><span class="keyword">Office.initialize</span></strong> event of the app, and handles the button click event for the app's button.</p>
</li><li>
<p>Home.css file, which contains the styling for the elements shown in the app UI.</p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>No configuration is necessary.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your <strong>AppManifest.xml</strong> file parses correctly.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release: Feb 2014</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp123513.aspx" target="_blank">Reading and writing data to the active selection in a document or spreadsheet</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
