# Apps for Office: Display tooltips in Excel
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
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:41:18
## Description

<div id="header">This code sample demonstrates a task pane app that is displayed in Excel 2013 when the app is first started. The task pane contains sample text that includes two highlighted keywords. Moving the mouse over either of the keywords displays
 a tooltip that is specific to that keyword. After a few seconds, the tooltip slowly fades from view as its opacity changes.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>Figure 1 shows the workbook opened with a tooltip displayed.</p>
<div class="caption"><strong>Figure 1. Tooltip is displayed for a keyword</strong></div>
<br>
<img src="/site/view/file/81773/1/image.png" alt="">
<p>&nbsp;</p>
<p>Figure 2 shows the tooltip starting to fade from view.</p>
<div class="caption"><strong>Figure 2. Tooltip as it starts to fade from view</strong></div>
<br>
<img src="/site/view/file/81774/1/image.png" alt="">
<p>&nbsp;</p>
<p>The sample demonstrates how to perform the following tasks:</p>
<ul>
<li>
<p>Use JavaScript to hide HTML elements in the task pane.</p>
</li><li>
<p>Retrieve the position of selected HTML elements in the task pane.</p>
</li><li>
<p>Use the <span><span class="keyword">Mouseover</span></span> event to trigger the display of a tooltip.</p>
</li><li>
<p>Dynamically add style settings to HTML elements to display the tooltip at a particular location on the screen.</p>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012.</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012.</p>
</li><li>
<p>Excel 2013.</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following components:</p>
<ul>
<li>
<p>The Tooltips project, which contains the Tooltips.xml manifest file. The XML manifest file of an app for Office enables you to declaratively describe how the app should be activated when you install and use it with Office documents and applications.</p>
</li><li>
<p>The TooltipsWeb project, which contains multiple template files. However, the three files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>Tooltips.html (in the Pages folder). This file contains the HTML user interface that is displayed in the task pane when the app is started. The markup consists of a &lt;div&gt; element that contains the random text shown in the task pane. It also contains
 two &lt;div&gt; elements that have the IDs <span class="code">span_help_01</span> and
<span class="code">span_help_02</span>, each containing the tooltip text. Within the sample text are two &lt;span&gt; elements that have the IDs
<span class="code">keyword01</span> and <span class="code">keyword02</span>, encompassing the keywords that will be used to trigger the tooltips.</p>
</li><li>
<p>App.css (in the Styles folder). This cascading style sheet (CSS) contains the code that specifies the look of the tooltips, as shown in the following code.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#span_help_01, #span_help_02 {
    width: 25%;
    padding: 5px;
    background-color: white;
    border: 3px solid rgb(195,151,51);
    border-radius: 5px;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The CSS also contains the style code for each keyword that sets the font color to
<span class="code">red</span>.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#keyword01, #keyword02 {
    color: red;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Tooltips.js (in the Scripts folder). This script file contains code that runs when the task pane app is loaded. Specifically, the script consists of commands from the JavaScript JQuery library. This startup script first hides the two &lt;div&gt; elements
 that contain the tooltip text.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#span_help_01').hide();
$('#span_help_02').hide();</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The script then determines the <strong>top</strong> and <strong>left</strong> positions of each keyword in the task pane by using the
<span><span class="keyword">offset</span></span> method and then stores those values in variables for later use.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>var tooltip01Position = $('#keyword01').offset();
var tooltip02Position = $('#keyword02').offset();</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The script file also includes the <span><span class="keyword">Mouseover</span></span> event handlers for the two keywords in the Tooltips.html file. The following code shows the event handler for
<span class="code">keyword01</span>. The event handler for <span class="code">
keyword02</span> is similar.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#keyword01').mouseover(function () {
    // Show the tooltip at the specified position
    // and then slowly fade away.
    $('#span_help_01').show();
    $('#span_help_01').css({
         'position': 'absolute',
         'left': tooltip01Position.left,
         'top': tooltip01Position.top &#43; 20
    }).fadeOut(4500);</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>As the mouse moves over either of the keywords, the &lt;div&gt; element that contains the tooltip for that keyword is displayed. Next, CSS styling is dynamically added to that &lt;div&gt; element to cause the tooltip to display just below the keyword. This
 is accomplished by adding 20 to the coordinate of the keyword's <span><span class="keyword">top</span></span> attribute value.</p>
<p>Finally, the <span><span class="keyword">FadeOut</span></span> method is called to hide the tooltip by fading it to transparent. The number passed into the method is the duration, in milliseconds, of the animation. In this case, the number
<strong>4500</strong> translates to 4.5 seconds. To give the reader more time to read the text, you may want to change this value to a longer delay if the tooltip contains more text than that used here.</p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the Tooltips.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose Ctrl&#43;Shift&#43;B, or on the <span class="ui">Build</span> menu, choose
<span class="ui">Build Solution</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run the sample, choose the F5 key. After the task pane is displayed in Excel 2013, move the mouse over the underlined
<strong>consectetur</strong> keyword. A tooltip that is specific to that keyword is displayed just under the keyword. Notice that the tooltip starts to fade away after a few seconds. Move the mouse to the underlined
<strong>ultricies</strong> keyword. A tooltip that is specific to that keyword is displayed and then starts to fade away after a few seconds.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your Tooltips.xml manifest file parses correctly. Also look for any errors in the JavaScript code that could keep the tooltips from being displayed. For example, you may have forgotten to end a statement
 with a semicolon, or you may have misspelled a method name or keyword. If the text in the task pane does not look as you think it should or if the tooltips are not displayed just below the keywords, check the CSS styles to ensure that you didn't forget a colon
 between the style and its value, or leave off a semicolon at the end of a style statement.</p>
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
</li></ul>
</div>
</div>
</div>
</div>
