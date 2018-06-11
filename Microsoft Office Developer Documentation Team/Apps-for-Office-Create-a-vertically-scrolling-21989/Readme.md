# Apps for Office: Create a vertically scrolling menu in Excel
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
* 2013-05-08 09:43:11
## Description

<div id="header">This code sample demonstrates a task pane app that is displayed in Excel 2013 when the app is first started. The task pane contains a stacked tabbed menu along its right side. Moving the mouse over the menu causes the menu item under the
 mouse to move to the left. Moving the mouse off of the item causes it to move back to the right. Moving the mouse downwards causes the menu to vertically scroll up to reveal more menu options.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>Figure 1 shows the task pane with the tabbed menu.</p>
<div class="caption"><strong>Figure 1. Task pane with the tabbed menu</strong></div>
<br>
<img src="/site/view/file/81757/1/image.png" alt="">
<p>The sample demonstrates how to perform the following tasks:</p>
<ul>
<li>
<p>Attach event handlers to HTML elements.</p>
</li><li>
<p>Use custom JQuery functions to animate HTML elements.</p>
</li><li>
<p>Dynamically add style settings to HTML elements to change the display of the dashboard.</p>
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
<p>The VerticallyScrollingMenu project, which contains the VerticallyScrollingMenu.xml manifest file. The XML manifest file of an app for Office enables you to declaratively describe how the app should be activated when you install and use it with Office documents
 and applications.</p>
</li><li>
<p>The VerticallyScrollingMenuWeb project, which contains multiple template files. However, the three files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>VerticallyScrollingMenu.html (in the Pages folder). This file contains the HTML user interface that is displayed in the task pane when the app is started. The markup consists of a &lt;div&gt; element containing a paragraph element which contains some random
 sample text. It also contains another &lt;div&gt; element that has the ID of <span class="code">
sidebar</span> which contains an unordered list with an ID of <span class="code">
menu</span>. The list contains a series of items consisting of an anchor element and a span element with some text. The list items are the tabbed menu options.</p>
</li><li>
<p>App.css (in the Styles folder). This cascading style sheet (CSS) contains the code that specifies the look of the sample text and the elements that make up the tabbed menu. Particularly notice the
<span class="code">overflow:hidden;</span> setting that causes any content that is greater than the top attribute setting of the
<span class="code">sidebar</span> div to be hidden.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#sidebar {
height:400px;
overflow:hidden;
position:relative;
background-color:#eee;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>VerticallyScrollingMenu.js (in the Scripts folder). This script file contains code that runs when the task pane app is loaded. Specifically, the script consists of commands from the JavaScript JQuery libraries named
<span><span class="keyword">jquery-ui.js</span></span> and <span><span class="keyword">jquery-1.7.1.min.js</span></span>. This startup script first sets variables with the attributes for the tabbed menu when you move the mouse over each item.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>var colorOver = '#31b8da';
var colorOut = '#1f1f1f';

//Padding, mouseover
var padLeft = '20px';
var padRight = '20px'

//Default Padding
var defpadLeft = $('#menu li a').css('paddingLeft');
var defpadRight = $('#menu li a').css('paddingRight');</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The next code animates the tabbed menu when the mouse is moved over and off of the menu items. This animation causes the tabs to move to the left when the mouse moves over the item (the
<span><span class="keyword">mouseover</span></span> event) and back to the right when the mouse is moved off of the item (the
<span><span class="keyword">mouseout</span></span> event). The variables that you set previously are dynamically added to the list item's attributes to cause this effect.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#menu li').click(function () {
    //Make the LI clickable
    window.location = $(this).find('a').attr('href');

    }).mouseover(function () {

    //Mouse over the LI and look for an element 
    //for transition
    $(this).find('a')
    .animate({ paddingLeft: padLeft, paddingRight: padRight }, { queue: false, duration: 100 })
    .animate({ backgroundColor: colorOver }, { queue: false, duration: 200 });

    }).mouseout(function () {

    //Mouse out from the LI and look for an element 
    //and discard the mouse over transition
    $(this).find('a')
    .animate({ paddingLeft: defpadLeft, paddingRight: defpadRight }, { queue: false, duration: 100 })
    .animate({ backgroundColor: colorOut }, { queue: false, duration: 200 });
    });</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The following code animates the menu so that it vertically scrolls up above the outer top limits of the
<span class="code">sidebar</span> div element. This makes it appear that the menu disappears as it moves upward.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#sidebar').mousemove(function (e) {

    //Sidebar Offset, Top value
    var s_top = parseInt($('#sidebar').offset().top);

    //Sidebar Offset, Bottom value
    var s_bottom = parseInt($('#sidebar').height() &#43; s_top);

    //Roughly calculate the height of the menu by 
    //multiplying the height of a single LI with 
    //the total number of LIs
    var mheight = parseInt($('#menu li').height() * $('#menu li').length);

    //Calculate the top value
    var top_value = Math.round(((s_top - e.pageY) / 100) * mheight / 2)

    //Animate the #menu by changing the top value
    $('#menu').animate({ top: top_value }, { queue: false, duration: 500 });
    });</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ul>
</li></ul>
<p>This code sample also requires the use of a custom JQuery library named <span>
<span class="keyword">jquery-ui.js</span></span> that contains the functions that enable the menu animations. All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development
 of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the VerticallyScrollingMenu.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose Ctrl&#43;Shift&#43;B, or on the <span class="ui">Build</span> menu, select
<span class="ui">Build Solution</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run the sample, choose the F5 key. After the task pane is displayed in Excel 2013, notice the stacked tabbed menu on the right side of the task pane. Moving the mouse over a particular tab menu causes the tab to move to the left. Moving the mouse off
 of the tabbed item moves the item back to the right. Moving the mouse down causes the menu to move upwards and moving the mouse upwards causes the menu to move down.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your AnimatedDashboard.xml manifest file parses correctly. Also look for any errors in the JavaScript code that could keep the tabbed menu from being displayed. For example, you may have forgotten to end
 a statement with a semicolon, or you may have misspelled a method name or keyword. If the components in the task pane do not look as you think they should, check the CSS styles to ensure that you didn't forget a colon between the style and its value, or leave
 off a semicolon at the end of a style statement.</p>
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
