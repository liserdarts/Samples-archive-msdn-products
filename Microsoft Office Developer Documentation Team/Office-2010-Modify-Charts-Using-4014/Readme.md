# Office 2010: Modify Charts Using Office.Chart.ModifyChartData
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
## Topics
* Charts
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:21:54
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to modify a chart in Microsoft Office 2010 by dynamically increasing the data it contains.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Add a module to the document, and copy the following code into the new module. Add a breakpoint where directed in the code and press F5 to run the procedure. After hitting the breakpoint, look at the chart, and then press F5
 run again to see the behavior of the chart. This will be easiest to see if you arrange the Word window and the VBA window so they're side-by-side on the screen.</span></p>
<p><span style="font-size:small">In order to use this code, within the VBA editor you must also set a reference to the Excel object model: Select Tools and then References, and select Microsoft Excel 14.0 Object Library from the list of references.</span></p>
<p><span style="font-size:small">In order to use this code with PowerPoint 2010, simply replace the reference to ActiveDocument with ActivePresentation.Slides(1) in the second line of code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ModifyChartData()
    Dim shp As Shape
    Set shp = ActiveDocument.Shapes.AddChart(xl3DColumn)
   
    Dim cht As Chart
    Set cht = shp.Chart

    Dim wb As Excel.Workbook
    Dim ws As Excel.Worksheet
   
    Set wb = cht.ChartData.Workbook
    Set ws = wb.Worksheets(1)
   
    ' The size and shape of the data will vary depending
    ' on the type of chart you have created. You will need
    ' to modify this code, depending on your chart. In every
    ' case, Excel creates a ListObject named Table1 for your data.
   
    ' Add a breakpoint on the next line of code.
    ws.Range(&quot;A2&quot;).Value = &quot;North&quot;
    ws.Range(&quot;A3&quot;).Value = &quot;South&quot;
    ws.Range(&quot;A4&quot;).Value = &quot;East&quot;
    ws.Range(&quot;A5&quot;).Value = &quot;West&quot;
   
    ws.Range(&quot;B1&quot;).Value = &quot;2009&quot;
    ws.Range(&quot;C1&quot;).Value = &quot;2010&quot;
    ws.Range(&quot;D1&quot;).Value = &quot;2011&quot;
   
    ' Now expand the table and add more data:
    ws.ListObjects(&quot;Table1&quot;).Resize ws.Range(&quot;A1:E6&quot;)
    ws.Range(&quot;A6&quot;).Value = &quot;Canada&quot;
    ws.Range(&quot;B6&quot;).Value = &quot;5&quot;
    ws.Range(&quot;C6&quot;).Value = &quot;4&quot;
    ws.Range(&quot;D6&quot;).Value = &quot;3&quot;
   
    ws.Range(&quot;E1&quot;).Value = &quot;2012&quot;
    ws.Range(&quot;E2&quot;).Value = &quot;4&quot;
    ws.Range(&quot;E3&quot;).Value = &quot;5&quot;
    ws.Range(&quot;E4&quot;).Value = &quot;2&quot;
    ws.Range(&quot;E5&quot;).Value = &quot;3&quot;
    ws.Range(&quot;E6&quot;).Value = &quot;6&quot;
   
    ' Note that the chart now contains more data!
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ModifyChartData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActiveDocument.Shapes.AddChart(xl3DColumn)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;shp.Chart&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;wb&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Excel.Workbook&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ws&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Excel.Worksheet&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wb&nbsp;=&nbsp;cht.ChartData.Workbook&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ws&nbsp;=&nbsp;wb.Worksheets(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;size&nbsp;and&nbsp;shape&nbsp;of&nbsp;the&nbsp;data&nbsp;will&nbsp;vary&nbsp;depending</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;on&nbsp;the&nbsp;type&nbsp;of&nbsp;chart&nbsp;you&nbsp;have&nbsp;created.&nbsp;You&nbsp;will&nbsp;need</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;modify&nbsp;this&nbsp;code,&nbsp;depending&nbsp;on&nbsp;your&nbsp;chart.&nbsp;In&nbsp;every</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;case,&nbsp;Excel&nbsp;creates&nbsp;a&nbsp;ListObject&nbsp;named&nbsp;Table1&nbsp;for&nbsp;your&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;

&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;breakpoint&nbsp;on&nbsp;the&nbsp;next&nbsp;line&nbsp;of&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A2&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;North&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A3&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;South&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A4&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;East&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A5&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;West&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;B1&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;2009&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;C1&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;2010&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;D1&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;2011&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;expand&nbsp;the&nbsp;table&nbsp;and&nbsp;add&nbsp;more&nbsp;data:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.ListObjects(<span class="visualBasic__string">&quot;Table1&quot;</span>).Resize&nbsp;ws.Range(<span class="visualBasic__string">&quot;A1:E6&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A6&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Canada&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;B6&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;5&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;C6&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;4&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;D6&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;3&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;E1&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;2012&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;E2&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;4&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;E3&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;5&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;E4&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;2&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;E5&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;3&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;E6&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;6&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;the&nbsp;chart&nbsp;now&nbsp;contains&nbsp;more&nbsp;data!</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25959" href="/site/view/file/25959/1/Office.Chart.ModifyChartData.txt">Office.Chart.ModifyChartData.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25960" href="/site/view/file/25960/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/">Office Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
