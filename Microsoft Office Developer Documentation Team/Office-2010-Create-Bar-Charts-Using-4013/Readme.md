# Office 2010: Create Bar Charts Using Office.Chart.CreateSimpleChart
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
* 2011-08-03 03:21:33
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to programmatically create a simple bar chart in Microsoft Office 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Paste the following code into a module in a new presentation.&nbsp;Select Tools, then Reference, and add a reference to the Microsoft Excel 14.0 Object Model.</span></p>
<p><span style="font-size:small">(You could easily modify it to work in a Word document, as well:&nbsp;simply replace ActivePresentation.Slides(1) with ActiveDocument. All the remainder of the code works exactly the same in Word.)</span></p>
<p><span style="font-size:small">Run the CreateChart procedure to create a simple bar chart.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub CreateChart()
    ' Create a very simple chart.
    Dim cht As Chart
    Dim chtData As ChartData
    Dim wb As Excel.Workbook
    Dim ws As Excel.Worksheet

    ' Create the chart and set a reference to the chart data.
    Dim shp As Shape
    Set shp = ActivePresentation.Slides(1).Shapes.AddChart(xlBarClustered, 10, 10, 500, 200)
   
    ' You can interact with the Shape object here, if you need to.
   
    ' Retrieve the chart contained within the shape. Although you know the
    ' shape contains a chart in this example, you can always
    ' use the Type property to verify that the shape you're working
    ' with is indeed a chart before you try to retrieve its Chart
    ' property:
    If shp.Type &lt;&gt; msoChart Then
        Exit Sub
    End If
   
    Set cht = shp.Chart
   
    ' Every new chart has an Excel workbook that contains its data.
    Set wb = cht.ChartData.Workbook
    Set ws = wb.Worksheets(1)

     ' Add the data to the workbook.
    
    ' Resize the table, which is always called Table 1:
    ws.ListObjects(&quot;Table1&quot;).Resize ws.Range(&quot;A1:B5&quot;)
   
    ' Set the title for the series:
    ws.Range(&quot;B1&quot;).Value = &quot;Regional Sales&quot;
   
    ' Put the data in the rows of the worksheet:
    ws.Range(&quot;A2:B2&quot;).Value = Array(&quot;North&quot;, 125)
    ws.Range(&quot;A3:B3&quot;).Value = Array(&quot;South&quot;, 12)
    ws.Range(&quot;A4:B4&quot;).Value = Array(&quot;East&quot;, 97)
    ws.Range(&quot;A5:B5&quot;).Value = Array(&quot;West&quot;, 150)
   
    ' Force the chart to retrieve its data and redraw itself:
    cht.ApplyDataLabels xlDataLabelsShowValue
   
    ' If you want to quit Excel, uncomment this line:
    ' wb.Application.Quit
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;CreateChart()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;very&nbsp;simple&nbsp;chart.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;chtData&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ChartData&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;wb&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Excel.Workbook&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ws&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Excel.Worksheet&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;the&nbsp;chart&nbsp;and&nbsp;set&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;chart&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes.AddChart(xlBarClustered,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">500</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;interact&nbsp;with&nbsp;the&nbsp;Shape&nbsp;object&nbsp;here,&nbsp;if&nbsp;you&nbsp;need&nbsp;to.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;the&nbsp;chart&nbsp;contained&nbsp;within&nbsp;the&nbsp;shape.&nbsp;Although&nbsp;you&nbsp;know&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;shape&nbsp;contains&nbsp;a&nbsp;chart&nbsp;in&nbsp;this&nbsp;example,&nbsp;you&nbsp;can&nbsp;always</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;use&nbsp;the&nbsp;Type&nbsp;property&nbsp;to&nbsp;verify&nbsp;that&nbsp;the&nbsp;shape&nbsp;you're&nbsp;working</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;with&nbsp;is&nbsp;indeed&nbsp;a&nbsp;chart&nbsp;before&nbsp;you&nbsp;try&nbsp;to&nbsp;retrieve&nbsp;its&nbsp;Chart</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;property:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;&lt;&gt;&nbsp;msoChart&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;shp.Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Every&nbsp;new&nbsp;chart&nbsp;has&nbsp;an&nbsp;Excel&nbsp;workbook&nbsp;that&nbsp;contains&nbsp;its&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wb&nbsp;=&nbsp;cht.ChartData.Workbook&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ws&nbsp;=&nbsp;wb.Worksheets(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;the&nbsp;data&nbsp;to&nbsp;the&nbsp;workbook.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Resize&nbsp;the&nbsp;table,&nbsp;which&nbsp;is&nbsp;always&nbsp;called&nbsp;Table&nbsp;1:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.ListObjects(<span class="visualBasic__string">&quot;Table1&quot;</span>).Resize&nbsp;ws.Range(<span class="visualBasic__string">&quot;A1:B5&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;title&nbsp;for&nbsp;the&nbsp;series:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;B1&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Regional&nbsp;Sales&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;the&nbsp;data&nbsp;in&nbsp;the&nbsp;rows&nbsp;of&nbsp;the&nbsp;worksheet:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A2:B2&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;North&quot;</span>,&nbsp;<span class="visualBasic__number">125</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A3:B3&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;South&quot;</span>,&nbsp;<span class="visualBasic__number">12</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A4:B4&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__number">97</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ws.Range(<span class="visualBasic__string">&quot;A5:B5&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;West&quot;</span>,&nbsp;<span class="visualBasic__number">150</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Force&nbsp;the&nbsp;chart&nbsp;to&nbsp;retrieve&nbsp;its&nbsp;data&nbsp;and&nbsp;redraw&nbsp;itself:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.ApplyDataLabels&nbsp;xlDataLabelsShowValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you&nbsp;want&nbsp;to&nbsp;quit&nbsp;Excel,&nbsp;uncomment&nbsp;this&nbsp;line:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;wb.Application.Quit</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25957" href="/site/view/file/25957/1/Office.Chart.CreateSimpleChart.txt">Office.Chart.CreateSimpleChart.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25958" href="/site/view/file/25958/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/">Office Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
