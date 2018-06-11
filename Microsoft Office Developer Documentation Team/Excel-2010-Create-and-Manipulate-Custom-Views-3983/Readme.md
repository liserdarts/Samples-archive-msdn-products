# Excel 2010: Create and Manipulate Custom Views Using Excel.CustomView Method
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Custom Views
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:27:49
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>CustomView
</strong>class and <strong>WorksheetView </strong>class to create and manipulate custom views in a Microsoft Excel 2010 workbook.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the TestWorksheetView procedure, and then press F8 to single-step through the code. You'll be able to see the behavior better
 if you arrange the VBA and Excel windows side by side.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestWorksheetView()
  Dim wnd As Window
  Set wnd = ActiveWindow
 
  Dim wbk As Workbook
  Set wbk = ActiveWorkbook
 
  Dim cv1 As CustomView
  Dim cv2 As CustomView
  ' Create a new view, using the print settings and row/col settings from the workbook.
  Set cv1 = wbk.CustomViews.Add(&quot;View1&quot;, True, True)
  ' Create a new view, bypassing the print settings and row/col settings from the workbook.
  Set cv2 = wbk.CustomViews.Add(&quot;View2&quot;, False, False)
 
  ' Create a worksheet view, and then
  ' turn off some display settings:
  Dim wsv As WorksheetView
  Set wsv = wnd.SheetViews(1)
 
  ' Display formulas and zeros, but hide
  ' gridlines, headings, and outlines:
  wsv.DisplayFormulas = True
  wsv.DisplayGridlines = False
  wsv.DisplayHeadings = False
  wsv.DisplayOutline = False
  wsv.DisplayZeros = True
 
  ' Display the view that doesn't take into account the current
  ' print or row/col settings. This puts back the row/col settings:
  cv2.Show
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestWorksheetView()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;wnd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Window&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wnd&nbsp;=&nbsp;ActiveWindow&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;wbk&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Workbook&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wbk&nbsp;=&nbsp;ActiveWorkbook&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cv1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CustomView&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cv2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CustomView&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;new&nbsp;view,&nbsp;using&nbsp;the&nbsp;print&nbsp;settings&nbsp;and&nbsp;row/col&nbsp;settings&nbsp;from&nbsp;the&nbsp;workbook.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cv1&nbsp;=&nbsp;wbk.CustomViews.Add(<span class="visualBasic__string">&quot;View1&quot;</span>,&nbsp;<span class="visualBasic__keyword">True</span>,&nbsp;<span class="visualBasic__keyword">True</span>)&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;new&nbsp;view,&nbsp;bypassing&nbsp;the&nbsp;print&nbsp;settings&nbsp;and&nbsp;row/col&nbsp;settings&nbsp;from&nbsp;the&nbsp;workbook.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cv2&nbsp;=&nbsp;wbk.CustomViews.Add(<span class="visualBasic__string">&quot;View2&quot;</span>,&nbsp;<span class="visualBasic__keyword">False</span>,&nbsp;<span class="visualBasic__keyword">False</span>)&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;worksheet&nbsp;view,&nbsp;and&nbsp;then</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;turn&nbsp;off&nbsp;some&nbsp;display&nbsp;settings:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;wsv&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;WorksheetView&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wsv&nbsp;=&nbsp;wnd.SheetViews(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Display&nbsp;formulas&nbsp;and&nbsp;zeros,&nbsp;but&nbsp;hide</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;gridlines,&nbsp;headings,&nbsp;and&nbsp;outlines:</span>&nbsp;
&nbsp;&nbsp;wsv.DisplayFormulas&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;wsv.DisplayGridlines&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;wsv.DisplayHeadings&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;wsv.DisplayOutline&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;wsv.DisplayZeros&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Display&nbsp;the&nbsp;view&nbsp;that&nbsp;doesn't&nbsp;take&nbsp;into&nbsp;account&nbsp;the&nbsp;current</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;print&nbsp;or&nbsp;row/col&nbsp;settings.&nbsp;This&nbsp;puts&nbsp;back&nbsp;the&nbsp;row/col&nbsp;settings:</span>&nbsp;
&nbsp;&nbsp;cv2.Show&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25879" href="/site/view/file/25879/1/Excel.CustomView.txt">Excel.CustomView.txt</a>- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25880" href="/site/view/file/25880/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
