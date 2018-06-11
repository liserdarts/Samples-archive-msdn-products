# Excel 2010: Create Charts Events Programmatically
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Charts
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:34:26
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>WorkbookNewChart
</strong>event and <strong>WorkbookAfterSave </strong>event in Microsoft Excel 2010 to create chart events programmatically upon saving the file.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Excel 2010 adds a series of new Application-level events. Several of these events deal with events surrounding the use of protected-view windows. Those events have been covered in a different sample for Microsoft Word, and they
 work equivalently in Microsoft Excel. This sample focuses on these Excel-specific events:</span><br>
<span style="font-size:small">&nbsp;- WorkbookNewChart</span><br>
<span style="font-size:small">&nbsp;- WorkbookAfterSave</span></p>
<p><span style="font-size:small">The following events, new in Excel 2010, deal with pivot tables and won't be covered here:</span><br>
<span style="font-size:small">&nbsp;- SheetPivotTableBeforeCommitChanges</span><br>
<span style="font-size:small">&nbsp;- SheetPivotTableBeforeDiscardChanges</span><br>
<span style="font-size:small">&nbsp;- SheetPivotTableAfterValueChange</span><br>
<span style="font-size:small">&nbsp;- SheetPivotTableBeforeAllocateChanges</span><br>
<br>
<span style="font-size:small">In order to test these events, first create a new workbook and in the VBA editor, add all the following code to the ThisWorkbook class module. Close the workbook, and reopen it (running the code in the workbooks's Open event, which
 sets up the necessary event hooks).</span></p>
<p><span style="font-size:small">With the workbook open, try creating a new worksheet, and creating a new chart. Both actions should trigger new events, and you can come back to the VBA editor to see the results in the Immediate window.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Private WithEvents app As Excel.Application
Private wbk As Excel.Workbook

Private Sub app_WorkbookNewChart(ByVal Wb As Workbook, ByVal Ch As Chart)
    Debug.Print &quot;WorkbookNewChart event&quot;
    Debug.Print &quot;  &quot; &amp; Wb.Name
    If Ch.HasTitle Then
        Debug.Print &quot;  &quot; &amp; Ch.ChartTitle.text
    End If
End Sub

Private Sub app_WorkbookNewSheet(ByVal Wb As Workbook, ByVal Sh As Object)
    Debug.Print &quot;WorkbookNewSheet event&quot;
    Debug.Print &quot;  &quot; &amp; Wb.Name
    Debug.Print &quot;  &quot; &amp; Sh.Name
End Sub

Private Sub Workbook_Open()
    Set wbk = Excel.ActiveWorkbook
    Set app = Excel.Application
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">WithEvents</span>&nbsp;app&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Excel.Application&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;wbk&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Excel.Workbook&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_WorkbookNewChart(<span class="visualBasic__keyword">ByVal</span>&nbsp;Wb&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Workbook,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;Ch&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;WorkbookNewChart&nbsp;event&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Wb.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Ch.HasTitle&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Ch.ChartTitle.text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_WorkbookNewSheet(<span class="visualBasic__keyword">ByVal</span>&nbsp;Wb&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Workbook,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;Sh&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;WorkbookNewSheet&nbsp;event&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Wb.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Sh.Name&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Workbook_Open()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wbk&nbsp;=&nbsp;Excel.ActiveWorkbook&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;app&nbsp;=&nbsp;Excel.Application&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25900" href="/site/view/file/25900/1/Excel.NewApplicationEvents.txt">Excel.NewApplicationEvents.txt</a>&nbsp;- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25901" href="/site/view/file/25901/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
