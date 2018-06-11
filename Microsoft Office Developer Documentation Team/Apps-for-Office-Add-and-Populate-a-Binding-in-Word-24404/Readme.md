# Apps for Office: Add and Populate a Binding in Word
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
* Binding
## IsPublished
* True
## ModifiedDate
* 2013-08-28 04:42:22
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left">&nbsp;</td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Add and populate a binding in Word</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><span>Summary:</span> This sample app shows you how to add and bind to a named rich text content control in Word, and then insert content at the bound location. It uses the
<strong>setSelectedDataAsync</strong> method with the <strong>ooxml</strong> coercion type to add the control; the
<strong>addFromNamedItemAsync</strong> method to bind to the control, and the <strong>
setDataAsync</strong> method with coercion type <strong>ooxml</strong> to populate the binding.</p>
</div>
<div>
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>The app initializes in a blank Word document. You can add other content if you choose and click the Add and Bind Control button to insert a named, rich text content control at the selection point and then bind to it. You then have two buttons for options
 of sample content that you can insert at the bound location. You can choose either button to populate the binding initially and the other to replace the contents of the binding with new content.The following screenshot shows how the task pane for the sample
 app appears when the solution starts.</p>
<p>The following screenshot shows how the task pane for the sample app appears when the solution starts.</p>
<strong>
<div class="caption">Figure 1. The Add and Populate a Binding task pane</div>
<img src="/site/view/file/94875/1/image.png" alt=""></strong><br>
&nbsp;
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Using bindings greatly expands the options for Word apps because they enable you to add content at a specified location in the document rather than only at the user's active selection point. To create a binding in Word, you always use the rich text content
 control type. Note that the control placeholder content must include at least one complete paragraph in order to enable you to populate the binding with multi-paragraph content. See the file ContentControl.xml in the Snippets_BindAndPopulate folder in this
 solution to see how to structure your content control for successful binding. To learn more about working with bindings in apps for Word, see the article
<a href="http://msdn.microsoft.com/EN-US/library/office/apps/dn423225.aspx" target="_blank">
Creating Better Apps for Word with Office Open XML</a>.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
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
<p>The PopulateBindings project, which contains:</p>
<ul>
<li>
<p>The PopulateBindings.xml manifest file.</p>
</li></ul>
</li><li>
<p>The PopulateBindingsWeb project, which contains multiple template files.</p>
<p>However, the files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>BindAndPopulate.html (in the App folder, BindAndPopulate subfolder). This contains the HTML user interface that is displayed in the task pane. It consists of three HTML buttons for inserting/binding to a rich text content control and for populating the binding
 with two interchangeable content options.</p>
</li><li>
<p>BindAndPopulate.js (in the same folder as above). This script file contains code that runs when the app is loaded. This startup wires up the click event handlers for the three buttons in BindAndPopulate.html to the functions that add and bind to the content
 control and populate the binding with the sample content options.</p>
</li><li>
<p>Three XML files containing the markup for the rich text content control and for the two content samples are located in the folder named Snippets_BindAndPopulate.</p>
</li></ul>
</li><li>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>To configure the sample, open the PopulateBindings.sln file with Visual Studio 2012. No other configuration is necessary.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>To build the sample, choose the Ctrl&#43;Shift&#43;B keys.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>To run the sample, choose the F5 key.</p>
<p>The following images show examples of the document at various stages of the process.</p>
<p>Figure 2 shows the rich text content control on the document surface after it has been inserted and the binding created. Notice that it looks like regular text because the content control's container is hidden (a feature that is new in Word 2013 for improved
 aesthetics when working with content controls). To view the content control properties, place your insertion point in the control and then, on the Developer tab of the ribbon, in the Controls group, choose Properties.</p>
<strong>
<div class="caption">Figure 2. The document showing a bound content control</div>
</strong><br>
&nbsp;<img src="/site/view/file/94876/1/image.png" alt="">
<p>Figure 3 shows the document after the binding has been populated with one of the two content options given in the task pane app UI.</p>
<strong>
<div class="caption">Figure 3. The document showing a content control populated with content.</div>
</strong><br>
&nbsp;<img src="/site/view/file/94877/1/image.png" alt=""></div>
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
