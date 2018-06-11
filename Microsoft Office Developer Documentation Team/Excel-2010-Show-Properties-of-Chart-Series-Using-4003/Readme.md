# Excel 2010: Show Properties of Chart Series Using Excel.SeriesProperties
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
* 2011-08-03 01:05:25
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows the properties of the <strong>
Series </strong>class for a chart in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, copy this code into the Sheet1 class. Place the cursor inside the TestSeriesProperties procedure, and press F8 to single over each line through the code. Place the VBA and Excel windows
 side by side so you can see the results of running the code as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestSeriesProperties()
    ' Demonstrate Series.
    '   InvertColorIndex
    '   InvertColor
    '   InvertIfNegative (not new in 2010)
   
    Dim rng As Range
    ' Press Shift&#43;F8 to step over this procedure:
    Set rng = FillRange(-25, 25, 20)
       
    ' Create the chart, set its source data, and turn off tick labels--they just get in the way here:
    Dim cht As Chart
    Set cht = Shapes.AddChart(XlChartType.xl3DColumn, Width:=500, Height:=300).Chart
    cht.SetSourceData rng
    cht.Axes(xlCategory).TickLabelPosition = xlNone
              
    With cht.SeriesCollection(1)
        ' Invert negative values:
        .InvertIfNegative = True
       
        ' Set a color:
        .InvertColor = vbRed
       
        ' Or select a color index:
        .InvertColorIndex = 13
    End With
   
End Sub

Function FillRange(minValue As Integer, maxValue As Integer, count As Integer) As Range
    Dim i As Integer
  
    For i = 1 To count
        ' Generate random numbers between minValue and maxValue
        Me.Range(&quot;A&quot; &amp; i, &quot;B&quot; &amp; i).Value = Array(&quot;Value &quot; &amp; i, Int((maxValue - minValue &#43; 1) * Rnd &#43; minValue))
    Next i
    Set FillRange = Me.Range(&quot;A1&quot;, &quot;B&quot; &amp; count)
End Function
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestSeriesProperties()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Demonstrate&nbsp;Series.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;InvertColorIndex</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;InvertColor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;InvertIfNegative&nbsp;(not&nbsp;new&nbsp;in&nbsp;2010)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Press&nbsp;Shift&#43;F8&nbsp;to&nbsp;step&nbsp;over&nbsp;this&nbsp;procedure:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;FillRange(-<span class="visualBasic__number">25</span>,&nbsp;<span class="visualBasic__number">25</span>,&nbsp;<span class="visualBasic__number">20</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;the&nbsp;chart,&nbsp;set&nbsp;its&nbsp;source&nbsp;data,&nbsp;and&nbsp;turn&nbsp;off&nbsp;tick&nbsp;labels--they&nbsp;just&nbsp;get&nbsp;in&nbsp;the&nbsp;way&nbsp;here:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;Shapes.AddChart(XlChartType.xl3DColumn,&nbsp;Width:=<span class="visualBasic__number">500</span>,&nbsp;Height:=<span class="visualBasic__number">300</span>).Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.SetSourceData&nbsp;rng&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.Axes(xlCategory).TickLabelPosition&nbsp;=&nbsp;xlNone&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cht.SeriesCollection(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Invert&nbsp;negative&nbsp;values:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.InvertIfNegative&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;a&nbsp;color:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.InvertColor&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Or&nbsp;select&nbsp;a&nbsp;color&nbsp;index:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.InvertColorIndex&nbsp;=&nbsp;<span class="visualBasic__number">13</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Function</span>&nbsp;FillRange(minValue&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;maxValue&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;count&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Generate&nbsp;random&nbsp;numbers&nbsp;between&nbsp;minValue&nbsp;and&nbsp;maxValue</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Range(<span class="visualBasic__string">&quot;A&quot;</span>&nbsp;&amp;&nbsp;i,&nbsp;<span class="visualBasic__string">&quot;B&quot;</span>&nbsp;&amp;&nbsp;i).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Value&nbsp;&quot;</span>&nbsp;&amp;&nbsp;i,&nbsp;Int((maxValue&nbsp;-&nbsp;minValue&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>)&nbsp;*&nbsp;Rnd&nbsp;&#43;&nbsp;minValue))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;FillRange&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;B&quot;</span>&nbsp;&amp;&nbsp;count)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25928" href="/site/view/file/25928/1/Excel.SeriesProperties.txt">Excel.SeriesProperties.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25929" href="/site/view/file/25929/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
