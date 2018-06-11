# Excel 2010: Retrieve Information About Chart Points Using Excel.PointClass
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
* 2011-08-03 12:42:28
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to retrieve information about the points in a Microsoft Excel 2010 chart such as its name, top and left positions, and width and height values.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Work with new members of the Point class. You can now retrieve information about the points in a chart, including</span><br>
<span style="font-size:small">&nbsp;- Name</span><br>
<span style="font-size:small">&nbsp;-&nbsp;Top</span><br>
<span style="font-size:small">&nbsp;-&nbsp;Left</span><br>
<span style="font-size:small">&nbsp;-&nbsp;Width</span><br>
<span style="font-size:small">&nbsp;-&nbsp;Height</span></p>
<p><span style="font-size:small">Points in a chart are numbered from left to right on the series. Given the information about the point, you could write code to place other information on the chart, or interact with the points.</span></p>
<p><span style="font-size:small">To test this code, in a new workbook, copy the entire sample into the Sheet1 class in the VBA editor. Place the cursor inside the test procedure, and press F5 to run the sample. Look in the Immediate window for the results.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestPointClass()
    ' First, create a simple chart that contains points.
    Range(&quot;A1:B1&quot;).Value = Array(&quot;Region&quot;, &quot;Sales&quot;)
    Range(&quot;A2:B2&quot;).Value = Array(&quot;North&quot;, 100)
    Range(&quot;A3:B3&quot;).Value = Array(&quot;South&quot;, 200)
    Range(&quot;A4:B4&quot;).Value = Array(&quot;East&quot;, 300)
    Range(&quot;A5:B5&quot;).Value = Array(&quot;West&quot;, 400)
   
    Dim cht As Chart
    Set cht = Shapes.AddChart.Chart
    cht.ChartType = xlLineMarkers
    cht.SetSourceData Source:=Range(&quot;A1:B5&quot;)
    With cht.SeriesCollection(1)
        .Points(1).MarkerStyle = xlMarkerStyleDiamond
        .Points(2).MarkerStyle = xlMarkerStyleCircle
        .Points(3).MarkerStyle = xlMarkerStyleDash
        .Points(4).MarkerStyle = xlMarkerStyleSquare
       
        Dim i As Integer
        For i = 1 To 4
            DisplayPointProperties .Points(i)
        Next i
    End With
   
End Sub

Sub DisplayPointProperties(pt As Point)
    ' Display information about the selected
    ' point in the Immediate window:
    Debug.Print &quot;========&quot;
    Debug.Print &quot;Name:   &quot; &amp; pt.Name
    Debug.Print &quot;Left:   &quot; &amp; pt.Left
    Debug.Print &quot;Top :   &quot; &amp; pt.Top
    Debug.Print &quot;Width:  &quot; &amp; pt.Width
    Debug.Print &quot;Height: &quot; &amp; pt.Height
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestPointClass()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;First,&nbsp;create&nbsp;a&nbsp;simple&nbsp;chart&nbsp;that&nbsp;contains&nbsp;points.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1:B1&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Region&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Sales&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A2:B2&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;North&quot;</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A3:B3&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;South&quot;</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A4:B4&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__number">300</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A5:B5&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;West&quot;</span>,&nbsp;<span class="visualBasic__number">400</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;Shapes.AddChart.Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.ChartType&nbsp;=&nbsp;xlLineMarkers&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.SetSourceData&nbsp;Source:=Range(<span class="visualBasic__string">&quot;A1:B5&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cht.SeriesCollection(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Points(<span class="visualBasic__number">1</span>).MarkerStyle&nbsp;=&nbsp;xlMarkerStyleDiamond&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Points(<span class="visualBasic__number">2</span>).MarkerStyle&nbsp;=&nbsp;xlMarkerStyleCircle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Points(<span class="visualBasic__number">3</span>).MarkerStyle&nbsp;=&nbsp;xlMarkerStyleDash&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Points(<span class="visualBasic__number">4</span>).MarkerStyle&nbsp;=&nbsp;xlMarkerStyleSquare&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">4</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayPointProperties&nbsp;.Points(i)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;DisplayPointProperties(pt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Point)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Display&nbsp;information&nbsp;about&nbsp;the&nbsp;selected</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;point&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;========&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Name:&nbsp;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pt.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Left:&nbsp;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pt.Left&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Top&nbsp;:&nbsp;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pt.Top&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Width:&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pt.Width&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Height:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pt.Height&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25919" href="/site/view/file/25919/1/Excel.PointClass.txt">Excel.PointClass.txt</a>&nbsp;- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25920" href="/site/view/file/25920/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
