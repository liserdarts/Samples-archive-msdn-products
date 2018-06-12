# Apps for Office: Create a Bing Map in Excel
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* apps for Office
## Topics
* App
## IsPublished
* True
## ModifiedDate
* 2014-05-30 01:32:07
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Create a Bing Map in Excel</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p></p>
</div>
<div>
<p><b>Last modified: </b>March 18, 2014</p>
<p><b>In this article</b> <br>
<a href="#sectionSection0">Prerequisites</a> <br>
<a href="#sectionSection1">Building the sample</a> <br>
<a href="#sectionSection2">Running the sample</a> <br>
<a href="#sectionSection3">More information</a> <br>
</p>
<p>This sample demonstrates using Visual Studio 2013 to create an Excel 2013 Content Pane App that reads addresses from an Microsoft Excel worksheet and renders a Bing map to show the locations that correspond to the addresses.</p>
<p>The user types an address (or multiple addresses) in an Microsoft Excel worksheet, and selects the ones they want to visualize in a Bing Map. They then click a button in the content pane app and a map is rendered in the content pane to show the selected
 location (or locations).</p>
</div>
<h2>Prerequisites</h2>
<div id="sectionSection0" name="collapseableSection">
<p>This sample requires the following;</p>
<ul>
<li>
<p>Microsoft Visual Studio 2013, either the Ultimate or Professional version.</p>
</li><li>
<p>Microsoft Office 2013.</p>
</li><li>
<p>Microsoft Office Developer Tools for Visual Studio 2013.</p>
</li></ul>
</div>
<h2>Building the sample</h2>
<div id="sectionSection1" name="collapseableSection">
<p>Use the following procedures to open, configure, and build the sample. To run this sample app, you must sign up for a Bing Maps application key.</p>
<p></p>
<h3>To create your own Bing Maps Key</h3>
<div>
<ol>
<li>
<p>Navigate to http://www.microsoft.com/maps/create-a-bing-maps-key.aspx.</p>
</li><li>
<p>Near the bottom of the page, click <b><span class="ui">Get the Trial Key</span></b>.</p>
</li><li>
<p>Click <b><span class="ui">Sign In</span></b>.</p>
<div>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Note</b> </th>
</tr>
<tr>
<td>
<p>You will need to create a Microsoft account if you do not already have one.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>In the left-hand pane, under <b><span class="ui">My Account</span></b>, click
<b><span class="ui">Create or View Keys</span></b>.</p>
</li><li>
<p>In the <b><span class="ui">Application name</span></b> textbox, type an application name (such as 'Martin's Excel Sample').</p>
</li><li>
<p>In the <b><span class="ui">Key type</span></b> dropdown list, ensure <b><span class="ui">Trial</span></b> is selected.</p>
</li><li>
<p>In the <b><span class="ui">Application type</span></b> dropdown list, select
<b><span class="ui">Private website</span></b>.</p>
</li><li>
<p>Enter the characters displayed in the image into the corresponding text box.</p>
</li><li>
<p>Click <b><span class="ui">Submit</span></b>. </p>
<p>When the process has been completed successfully, you should see your key details near the bottom of the page.</p>
</li><li>
<p>Select the key value that has been created and copy it to the clipboard.</p>
</li></ol>
</div>
<p>Next, open the sample solution and paste your Bing Maps key into the app.js file.</p>
<h3>To open and configure the sample by using Visual Studio 2013</h3>
<div>
<ol>
<li>
<p>Open the <b><span class="ui">BingEx.sln</span></b> solution with Visual Studio 2013.</p>
</li><li>
<p>In the Solution Explorer window, expand the <b><span class="ui">BingExWeb</span></b> project.</p>
</li><li>
<p>Expand the <b><span class="ui">Scripts</span></b> folder, and then double-click
<b><span class="ui">App.js</span></b> to open it.</p>
</li><li>
<p>In the first line of code, paste the key value that you copied from the Web page inside the double quotes.</p>
</li><li>
<p>On the <b><span class="ui">File</span></b> menu, click <b><span class="ui">Save All</span></b>.</p>
</li></ol>
</div>
<p>Finally, you can debug and test the code sample.</p>
<p></p>
</div>
<h2>Running the sample</h2>
<div id="sectionSection2" name="collapseableSection">
<h3>To run the sample</h3>
<div>
<ol>
<li>
<p>Press [F5].</p>
<p>Microsoft Excel appears, and after a few seconds a new workbook with an embedded content pane app appears.</p>
</li><li>
<p>In cell A1, type <span>London</span></p>
</li><li>
<p>In cell A2, type <span>Lord's Cricket Ground</span></p>
</li><li>
<p>In cell A3, type <span>Marleybone Station</span></p>
</li><li>
<p>In cell A4, type <span>W12 8QT</span></p>
</li><li>
<p>Select cells A1 to A4.</p>
</li><li>
<p>In the content pane, click <b><span class="ui">Show Locations on Map</span></b>.</p>
<p>Four pushpins should be rendered on a map of London.</p>
</li><li>
<p>Experiment by entering other addresses in the worksheet (such as <span>One Microsoft Way</span>).</p>
</li></ol>
</div>
</div>
<h2>More information</h2>
<div id="sectionSection3" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/ff428643.aspx" target="_blank">Getting Started with Bing Maps</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/jj220060.aspx" target="_blank">Build apps for Office</a>
</p>
</li></ul>
</div>
</div>
</div>
