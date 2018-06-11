# Apps for Office: Display an animated dashboard in Excel
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
* 2013-05-08 09:39:52
## Description

<div id="header">This code sample demonstrates a task pane app that is displayed in Excel 2013 when the app is first started. The task pane contains a partially hidden menu along its left side. Moving the mouse over the dashboard causes the menu to be fully
 displayed. Each menu item also includes a button that is used to either insert the text from a text box into the worksheet or retrieve text from the worksheet and insert it into the text box.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div class="section" id="sectionSection0">
<p>Figure 1 shows the task pane with the partially displayed dashboard.</p>
<div class="caption"><strong>Figure 1. Initial state of the dashboard</strong></div>
<br>
<img src="/site/view/file/81760/1/image.png" alt="">
<p>Figure 2 shows the fully displayed dashboard.</p>
<div class="caption"><strong>Figure 2. Fully displayed dashboard</strong></div>
<br>
<img src="/site/view/file/81761/1/image.png" alt="">
<p>The sample demonstrates how to perform the following tasks:</p>
<ul>
<li>
<p>Attach event handlers to HTML elements.</p>
</li><li>
<p>Use custom JQuery functions to animate HTML elements.</p>
</li><li>
<p>Dynamically add style settings to HTML elements to change the display of the dashboard.</p>
</li><li>
<p>Retrieve selected content from the worksheet.</p>
</li><li>
<p>Insert content from a text box into the worksheet.</p>
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
<p>The AnimatedDashboard project, which contains the AnimatedDashboard.xml manifest file. The XML manifest file of an app for Office enables you to declaratively describe how the app should be activated when you install and use it with Office documents and
 applications.</p>
</li><li>
<p>The AnimatedDashboardWeb project, which contains multiple template files. However, the three files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>AnimatedDashboard.html (in the Pages folder). This file contains the HTML user interface that is displayed in the task pane when the app is started. The markup consists of a &lt;div&gt; element that contains a text box element that has an ID of
<span class="code">selectedDataTxt</span>. It also contains another &lt;div&gt; element that has the ID of
<span class="code">dashboard</span> that contains two buttons that have IDs of <span class="code">
setDataBtn</span> and <span class="code">getDataBtn</span>. The <span class="code">
setDataBtn</span> button inserts text from the text box into the worksheet. The <span class="code">
getDataBtn</span> button retrieves any selected text from the worksheet and inserts it into the text box.</p>
</li><li>
<p>App.css (in the Styles folder). This cascading style sheet (CSS) contains the code that specifies the initial look of the dashboard and the elements each menu item contains as shown in the following code. Particularly notice the
<span class="code">left: -92px</span> setting that causes the dashboard to appear partially hidden on the left side of the task pane.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#dashboard {
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
<p>The CSS also contains the style code that specifies the appearance of the two buttons.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#setDataBtn
{
margin-right: 10px; 
padding: 0px; 
width: 90px;
}

#getDataBtn
{
padding: 0px; 
width: 90px;
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>Finally, the following code formats the text box.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>&nbsp;</th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>#selectedDataTxt
{
margin-top: 10px; 
width: 210px
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>AnimatedDashboard.js (in the Scripts folder). This script file contains code that runs when the task pane app is loaded. Specifically, the script consists of commands from the JavaScript JQuery libraries named
<span><span class="keyword">jquery.easing.1.3.js</span></span> and <span><span class="keyword">jquery-1.7.1.min.js</span></span>. This startup script first attaches code to the
<span><span class="keyword">hover</span></span> event of the &lt;div&gt; element that has the ID
<span><span class="keyword">dashboard</span></span> that contains the menu items. The
<span><span class="keyword">hover</span></span> event takes two arguments that define what happens when you move the mouse over the menu and then what happens when you move the mouse off of the menu.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#dashboard').hover(</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>When the mouse is moved over the dashboard, the CSS <span class="code">left</span> attribute value is dynamically changed from a negative value (menu is partially hidden) to 0, which causes the dashboard to be displayed. Next, the code sets the duration
 for the animation to 500 milliseconds, which equates to half a second. Finally, the code sets a custom easing method from the
<span><span class="keyword">jquery.easing.1.3.js</span></span> library that causes the dashboard to appear slowly at first and then speed up.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function() {
$(this).stop().animate(
{
    left: '0',
},
500,
'easeInSine'
);</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>When the mouse is moved off of the dashboard, the following code is run. The <span class="code">
left</span> attribute is reset to a negative value, which causes it to be partially hidden on the left side of the task pane. Next, the code sets the duration for the animation to 1500 milliseconds, which equates to one and a half seconds. Finally the code
 sets a custom easing, which causes the dashboard to retract to the left and then appear to bounce before settling in.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function() {
$(this).stop().animate(
{
    left: '-92px'
},
1500,
'easeOutBounce'
);</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>When the <span class="ui">Set data</span> button is clicked, the <span><span class="keyword">click</span></span> event is activated to call the
<span><span class="keyword">setData</span></span> function, passing in the text in the text box.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#setDataBtn').click(function () { setData('#selectedDataTxt'); });</pre>
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
    Office.context.document.setSelectedDataAsync($(elementId).val());
}</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>Similar to the <span class="ui">Set data</span> button, the <span class="ui">
Get data</span> button activates the <span><span class="keyword">click</span></span> event to call the
<span><span class="keyword">getData</span></span> function, passing in the ID of the
<span class="code">selectedDataTxt</span> text box.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>$('#getDataBtn').click(function () { getData('#selectedDataTxt'); });</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
<p>The <span><span class="keyword">getData</span></span> function reads the data from current selection of the document and displays it in a text box</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>JavaScript </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>function getData(elementId) {
    Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
    function (result) {
        if (result.status === 'succeeded') {
            $(elementId).val(result.value);
        }
});</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ul>
</li></ul>
<p>This code sample also requires the use of a custom JQuery library named <span>
<span class="keyword">jquery.easing.1.3.js</span></span> that contains the functions that enable the dashboard animations. All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified
 in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, open the AnimatedDashboard.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, choose Ctrl&#43;Shift&#43;B, or on the <span class="ui">Build</span> menu, select
<span class="ui">Build Solution</span>.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run the sample, choose the F5 key. After the task pane is displayed in Excel 2013, notice that there is a text box that contains sample text. There is also a dashboard on the left side of the task pane. Moving the mouse over the dashboard causes it to
 move to the right, displaying the <span class="ui">Set data</span> and <span class="ui">
Get data</span> buttons. Click the <span class="ui">Set data</span> button. Notice that the text from the text box is inserted into the worksheet. Change the text in the worksheet and then click the
<span class="ui">Get data</span> button. Notice that the updated text appears in the text box.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app fails to install, ensure that the XML in your AnimatedDashboard.xml manifest file parses correctly. Also look for any errors in the JavaScript code that could keep the dashboard from being displayed. For example, you may have forgotten to end
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
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142145.aspx" target="_blank">Document.setSelectedDataAsync</a> method</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142294.aspx" target="_blank">Document.getSelectedDataAsync</a> method</p>
</li></ul>
</div>
</div>
</div>
</div>
