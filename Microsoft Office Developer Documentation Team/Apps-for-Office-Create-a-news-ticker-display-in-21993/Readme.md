# Apps for Office: Create a news ticker display in Excel
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* Excel 2013
* apps for Office
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-05-08 09:37:39
## Description

<div id="header">This code sample demonstrates a task pane app that is displayed in Excel 2013 when the app is first started. The task pane displays a number of lines of text (with anchor elements) that scroll upward. As the top line of text disappears, another
 line of text appears at the bottom of the task pane. This type of action is suited to displaying news headlines from RSS feeds or other information that scrolls when size prohibits more details.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>Figure 1 shows the task pane with the lines of news headlines.</p>
<div class="caption"><strong>Figure 1. List of news headlines</strong></div>
<br>
<img src="/site/view/file/81778/1/image.png" alt="">
<p>The sample demonstrates how to perform the following tasks:</p>
<ul>
<li>
<p>Attach event handlers to HTML elements.</p>
</li><li>
<p>Use custom JQuery functions to animate HTML elements.</p>
</li><li>
<p>Dynamically add style settings to HTML elements to change the display of the list.</p>
</li><li>
<p>Chaining JQuery functions together to make the code more efficient.</p>
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
<p>The News_Ticker project, which contains the News_Ticker.xml manifest file. The XML manifest file of an app for Office enables you to declaratively describe how the app should be activated when you install and use it with Office documents and applications.</p>
</li><li>
<p>The News_TickerWeb project, which contains multiple template files. However, the three files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>News_Ticker.html (in the Pages folder). This file contains the HTML user interface that is displayed in the task pane when the app is started. The markup consists of an unordered list element with the ID of
<span class="code">listticker</span> that contains a list of anchor elements enclosed in paragraph elements. There is also an animated image that precedes each anchor element.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>HTML </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>&lt;ul id=&quot;listticker&quot;&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 1&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 2&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 3&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 4&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 5&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 6&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 7&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
    &lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline 8&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;
&lt;/ul&gt;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>App.css (in the Styles folder). This cascading style sheet (CSS) contains the code that specifies the look of each line of the unordered list and the image.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#listticker {
font-weight:900;
font-size: 20px;
}

#listticker li a {
color: blue;
}

img {
display: inline;
width: 25px;
height: 22px;
padding: 0 10px 0 0; 
}#dashboard {
width: 70px;
background-color: rgb(110,138,195);
padding: 20px 20px 20px 20px;
position: absolute;
left: -92px;
z-index: 100;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>News_Ticker.js (in the Scripts folder). This script file contains code that runs when the task pane app is loaded. Specifically, the script consists of commands from the JavaScript JQuery library named
<span><span class="keyword">jquery-1.7.1.min.js</span></span>. This code first sets variables that determine various intervals such as that used by the first item as it fades out to make room for an additional item that is added to the bottom of the list
 and how much time to takes before the next item is displayed.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>var first = 0;
var speed = 700;
var pause = 3000;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>Next, the <span class="code">removeFirst</span> function causes the first item at the top of the list to fade out at a specified speed. The code uses the
<span class="code">first</span> pseudo code operator to choose the first list item. It then uses chaining to string together the
<span class="code">animate</span> and <span class="code">fadeout</span> functions to accomplish the fade out. And finally, the code calls the
<span class="code">addLast</span> function to add an item to the bottom of the list.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function removeFirst() {
    first = $('ul#listticker li:first').html();
    $('ul#listticker li:first')
    .animate({ opacity: 0 }, speed)
    .fadeOut('slow', function () { $(this).remove(); });
    addLast(first);
    } 
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The code next sets the <span class="code">i</span> counter that is appended to each line item as it is added at the bottom of the list. The
<span class="code">addLast</span> function then adds an item to the bottom of the list. The function uses the intervals that were specified previously to affect the speed at which the items fade into view. The time it takes before the item is displayed is
 also set by using the <span class="code">setInterval</span> function.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>var i = 9;

function addLast(first) {
    last = '&lt;li&gt;&lt;p&gt;&lt;img src=&quot;../images/news.gif&quot; /&gt;&lt;a href=&quot;#&quot;&gt;News Headline ' &#43; (i&#43;&#43;) &#43; '&lt;/a&gt;&lt;/p&gt;&lt;/li&gt;';

    first &#43; '';
    $('ul#listticker').append(last)
    $('ul#listticker li:last')
    .animate({ opacity: 1 }, speed)
    .fadeIn('slow')
}
interval = setInterval(removeFirst, pause);</pre>
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
<p>To configure the sample, open the News_Ticker.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose Ctrl&#43;Shift&#43;B, or on the <span class="ui">Build</span> menu, select
<span class="ui">Build Solution</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run the sample, choose the F5 key. After the task pane is displayed in Excel 2013, notice that there are eight lines of text displayed preceded by an image. After an interval of time passes, the top line item starts to fade out. After the top line item
 disappears, the entire list moves up one position to take the place of the first item. After another interval of time passes, a line item fades in at the bottom of the list. The process then repeats itself.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your AnimatedDashboard.xml manifest file parses correctly. Also look for any errors in the JavaScript code that could keep the list from being displayed or the list items from properly fading in or fading
 out. For example, you may have forgotten to end a statement with a semicolon, or you may have misspelled a method name or keyword. If the components in the task pane do not look as you think they should, check the CSS styles to ensure that you didn't forget
 a colon between the style and its value, or leave off a semicolon at the end of a style statement.</p>
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
