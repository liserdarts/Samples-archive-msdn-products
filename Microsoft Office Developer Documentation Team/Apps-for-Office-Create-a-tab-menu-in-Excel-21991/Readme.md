# Apps for Office: Create a tab menu in Excel
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* Excel 2013
* apps for Office
## Topics
* User Experience
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:45:02
## Description

<div id="header">This code sample demonstrates a task pane app that is displayed in Excel 2013 when the app is first started. The task pane contains three tabs that are presented horizontally, each with a tab panel that contains some random text. Each tab
 also includes a button that is used to insert the text just from that tab into the worksheet.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>Figure 1 shows the task pane with the three-tab menu displayed.</p>
<div class="caption">Figure 1. Tab menu</div>
<br>
<img src="/site/view/file/81763/1/image.png" alt="">
<p>The sample demonstrates how to perform the following tasks:</p>
<ul>
<li>
<p>Attach event handlers to HTML elements.</p>
</li><li>
<p>Use JavaScript to hide HTML elements in the task pane.</p>
</li><li>
<p>Dynamically add style settings to HTML elements to display the tab menu at a particular location on the screen.</p>
</li><li>
<p>Use descendent qualifiers to select HTML elements.</p>
</li><li>
<p>Insert content into the worksheet.</p>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012.</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Excel 2013.</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following components:</p>
<ul>
<li>
<p>The TabMenus project, which contains the TabMenus.xml manifest file. The XML manifest file of an app for Office enables you to declaratively describe how the app should be activated when you install and use it with Office documents and applications.</p>
</li><li>
<p>The TabMenusWeb project, which contains multiple template files. However, the three files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>TabMenus.html (in the Pages folder). This file contains the HTML user interface that is displayed in the task pane when the app is started. The markup consists of a &lt;ul&gt; (unordered list) element with a class name of
<span><span class="keyword">tabs</span></span>, where each &lt;li&gt; (list item) element is a tab in the menu. It also contains three &lt;div&gt; elements that have the IDs of
<span class="code">panel1</span>, <span class="code">panel2</span>, and <span class="code">
panel3</span>, which are the individual panels, each of which contains random text. It also contains an &lt;input&gt; element of type
<span><span class="keyword">button</span></span> that inserts the text from a particular panel into the worksheet when the button is chosen.</p>
</li><li>
<p>App.css (in the Styles folder). This cascading style sheet (CSS) contains the code that specifies the look of the tabs and the elements each tab contains, as shown in the following code. Particularly notice the
<span class="code">display: block</span> setting that causes the tabs to appear horizontally.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>.tabs {
margin: 0;
padding: 0;
zoom : 1;
}
.tabs li {
float: left;
list-style: none;
padding: 0;
margin: 0;
}
.tabs a {
display: block;
text-decoration: none;
padding: 3px 5px;
background-color:aqua;
margin-right: 10px;
border: 1px solid rgb(153,153,153);
margin-bottom: -1px;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The CSS also contains the style code that changes the appearance of the tab when it becomes the active tab.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>.tabs .active {
border-bottom: 1px solid white;
background-color: white;
color: rgb(51,72,115);
position: relative;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The following style code defines the default appearance of each panel.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>.panelContainer {
clear: both;
margin-bottom: 25px;
border: 1px solid rgb(153,153,153);
background-color: white;
padding: 10px;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>Finally, the following code formats the <span class="ui">Insert Data</span> button.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#setDataBtn {
    margin-right: 10px; 
    padding: 0px; 
    width: 100px;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>TabMenus.js (in the Scripts folder). This script file contains code that runs when the task pane app is loaded. Specifically, the script consists of commands from the JavaScript JQuery library. This startup script displays the first tab and panel.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('.tabs li:first a').click();</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>When a tab is chosen, the script attaches a <span><span class="keyword">click</span></span> event to the anchor tags in the tab menu, which is then executed.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('.tabs a').click(function ()</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The script then stores the calling object (represented by the <span><span class="keyword">this</span></span> keyword) into a variable so that it can be used later.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>var $this = $(this);</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>Next, the script hides the panels and then clears the active state from the tabs. Then, the calling tab is set as active and its URL is retrieved and stored in the
<span><span class="keyword">panel</span></span> variable. The URL will be the address on the panel that is associated with that tab. Finally, the panel is relatively slowly faded in until it is displayed. The number passed in to the method is the duration,
 in milliseconds, of the animation.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('.panel').hide();
$('.tabs a.active').removeClass('active');

$this.addClass('active').blur();

panel = $this.attr('href'); 

$(panel).fadeIn(250);
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>When the <span class="ui">Insert data</span> button is chosen, the <span><span class="keyword">click</span></span> event is activated to call the
<span><span class="keyword">setData</span></span> function, passing in the paragraph of text that is associated with the active tab.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#setDataBtn').click(function () { setData($(panel).children('p')); });</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The <span><span class="keyword">setData</span></span> function calls the <span>
<span class="keyword">setSelectedDataAsync</span></span> method to insert the text from the active panel into the worksheet. The
<span><span class="keyword">setSelectedDataAsync</span></span> method asynchronously writes data to the current selection in the document.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function setData(elementId) {
Office.context.document.setSelectedDataAsync($(elementId).text());
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the TabMenus.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose Ctrl&#43;Shift&#43;B, or on the <span class="ui">Build</span> menu, select
<span class="ui">Build Solution</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run the sample, choose the F5 key. After the task pane is displayed in Excel 2013, notice that there are three tabbed panels that contain text. Choosing each tab displays a different panel. Choose the
<span class="ui">Insert data</span> button. Notice the text that is inserted into the worksheet. Select another panel and then choose the
<span class="ui">Insert data</span> button. Notice that the text that is inserted into the worksheet changes.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your TabMenus.xml manifest file parses correctly. Also look for any errors in the JavaScript code that could keep the tabs from being displayed. For example, you may have forgotten to end a statement with
 a semicolon, or you may have misspelled a method name or keyword. If the tabs and panels in the task pane do not look as you think they should, check the CSS styles to ensure that you didn't forget a colon between the style and its value, or leave off a semicolon
 at the end of a style statement.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: April 29, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj220060.aspx" target="_blank">Build apps for Office</a></p>
</li><li>
<p><a href="http://www.w3schools.com/html/" target="_blank">HTML Tutorial</a></p>
</li><li>
<p><a href="http://jquery.com/" target="_blank">What is jQuery?</a></p>
</li><li>
<p><a href="http://www.w3schools.com/css/css_intro.asp" target="_blank">CSS Introduction</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142145.aspx" target="_blank">Document.setSelectedDataAsync</a> method</p>
</li></ul>
</div>
</div>
</div>
</div>
