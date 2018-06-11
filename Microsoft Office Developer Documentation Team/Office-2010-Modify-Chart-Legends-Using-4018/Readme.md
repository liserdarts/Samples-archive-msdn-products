# Office 2010: Modify Chart Legends Using Office.Chart.WorkWithLegend
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
* 2011-08-03 03:22:51
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to modify a chart legend in Microsoft Office 2010 by changing such properties as its fill, shadow, and line type.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Microsoft Word 2010/Charting</span></p>
<p><span style="font-size:small">Modify the chart legend.</span></p>
<p><span style="font-size:small">Add a module to the document, and copy the following code into the new module. Add a breakpoint where directed in the code and press F5 to run the procedure. After hitting the breakpoint, look at the chart, and then press F5
 run again to see the behavior of the chart. This will be easiest to see if you arrange the Word window and the VBA window so they're side-by-side on the screen.</span></p>
<p><span style="font-size:small">You could easily modify this demonstration to work with PowerPoint by simply replacing the reference to ActiveDocument with ActivePresentation.Slides(1) and changing the enumerations that start with &quot;wd&quot; to start with &quot;mso&quot;
 instead.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ModifyChartLegend()
    Dim shp As Shape
    Set shp = ActiveDocument.Shapes.AddChart(xl3DPie)
   
    Dim cht As Chart
    Set cht = shp.Chart
   
    With cht.Legend
        .Position = xlLegendPositionTop
        .IncludeInLayout = True
        ' The next sections make arbitrary (and perhaps unattractive)
        ' changes to the formatting of the legend.
        With .Format
            With .Fill
                .ForeColor.ObjectThemeColor = wdThemeColorBackground2
            End With
            With .Line
                .ForeColor.ObjectThemeColor = wdThemeColorAccent1
                .Style = msoLineThinThin
                .Weight = 2
            End With
            With .Shadow
                .Type = msoShadow21
                .Style = msoShadowStyleOuterShadow
                .ForeColor.ObjectThemeColor = wdThemeColorAccent4
            End With
            With .Glow
                .Color.ObjectThemeColor = wdThemeColorAccent2
                .Radius = 11
            End With
        End With
    End With
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ModifyChartLegend()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActiveDocument.Shapes.AddChart(xl3DPie)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;shp.Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cht.Legend&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Position&nbsp;=&nbsp;xlLegendPositionTop&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.IncludeInLayout&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;next&nbsp;sections&nbsp;make&nbsp;arbitrary&nbsp;(and&nbsp;perhaps&nbsp;unattractive)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;changes&nbsp;to&nbsp;the&nbsp;formatting&nbsp;of&nbsp;the&nbsp;legend.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Format&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Fill&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorBackground2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Line&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;msoLineThinThin&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Weight&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Shadow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoShadow21&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;msoShadowStyleOuterShadow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent4&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Glow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Radius&nbsp;=&nbsp;<span class="visualBasic__number">11</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25967" href="/site/view/file/25967/1/Office.Chart.WorkWithLegend.txt">Office.Chart.WorkWithLegend.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25968" href="/site/view/file/25968/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/">Office Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
