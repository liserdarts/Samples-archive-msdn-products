# Office 2010: Change Chart Layouts Using Office.Chart.ModifyChartLayout
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
* 2011-08-03 03:22:14
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to change the layout or style of a chart in Microsoft Office 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Add a module to the document, and copy the following code into the new module. Add a breakpoint after the call to the AddChart method and press F5 to run the procedure. After hitting the breakpoint, single step through the code
 (press F8 for each line) to see the behavior of the chart. This will be easiest to see if you arrange the Word window and the VBA window so they're side-by-side on the screen.</span></p>
<p><span style="font-size:small">Note that this procedure creates an exceedingly ugly chart!</span></p>
<p><span style="font-size:small">You could easily modify this demonstration to work with PowerPoint: replace the reference to ActiveDocument with ActivePresentation.Slides(1) and change the enumerations that start with &quot;wd&quot; to start with &quot;mso&quot; instead.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ModifyChart()
    Dim shp As Shape
    Set shp = ActiveDocument.Shapes.AddChart(xl3DColumn)
   
    Dim cht As Chart
    Set cht = shp.Chart

    ' Now, modify the various properties of the chart.
    Dim i As Integer
   
    ' Apply numbered layouts, from 1 to 10.
    For i = 1 To 10
        cht.ApplyLayout i
        ' You can't set the ChartTitle.Text property
        ' unless the chart has a title. Not all layouts
        ' include a title. You could force the layout to
        ' include a title by setting the HasTitle property
        ' to True (it's read/write), but it's interesting
        ' to see which layouts include titles and which don't.
        If cht.HasTitle Then
            cht.ChartTitle.Text = &quot;Layout &quot; &amp; i
        End If
    Next i
   
    ' Applying all these data labels will make the chart very ugly.
    ' You can use one of the enumerated values to set simple data labels:
    cht.SeriesCollection(1).ApplyDataLabels xlDataLabelsShowLabel
    cht.SeriesCollection(2).ApplyDataLabels xlDataLabelsShowValue
   
    ' You can supply other values individually to have more control over the data labels:
    cht.SeriesCollection(3).ApplyDataLabels ShowCategoryName:=True, ShowValue:=True, Separator:=&quot;: &quot;
   
    ' Adjust the thickness of the back wall.
    cht.BackWall.Thickness = 25
   
    ' Change the chart type:
    cht.ChartType = xl3DBarStacked
   
    ' Change the 3D rotation. This property applies only to 3D charts:
    cht.Rotation = 30
   
    ' Modify the chart fill, using a theme color:
    shp.Fill.ForeColor.ObjectThemeColor = wdThemeColorAccent1
 
    ' Add a gradient:
    shp.Fill.PresetGradient msoGradientHorizontal, 1, msoGradientLateSunset
   
    ' Modify other shape settings:
    shp.Line.ForeColor.ObjectThemeColor = wdThemeColorAccent2
    shp.ThreeD.BevelTopType = msoBevelRelaxedInset
    shp.Glow.Color.ObjectThemeColor = wdThemeColorAccent3
    shp.Shadow.Type = msoShadow32
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ModifyChart()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActiveDocument.Shapes.AddChart(xl3DColumn)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;shp.Chart&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now,&nbsp;modify&nbsp;the&nbsp;various&nbsp;properties&nbsp;of&nbsp;the&nbsp;chart.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;numbered&nbsp;layouts,&nbsp;from&nbsp;1&nbsp;to&nbsp;10.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cht.ApplyLayout&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can't&nbsp;set&nbsp;the&nbsp;ChartTitle.Text&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;unless&nbsp;the&nbsp;chart&nbsp;has&nbsp;a&nbsp;title.&nbsp;Not&nbsp;all&nbsp;layouts</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;include&nbsp;a&nbsp;title.&nbsp;You&nbsp;could&nbsp;force&nbsp;the&nbsp;layout&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;include&nbsp;a&nbsp;title&nbsp;by&nbsp;setting&nbsp;the&nbsp;HasTitle&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;True&nbsp;(it's&nbsp;read/write),&nbsp;but&nbsp;it's&nbsp;interesting</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;see&nbsp;which&nbsp;layouts&nbsp;include&nbsp;titles&nbsp;and&nbsp;which&nbsp;don't.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;cht.HasTitle&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cht.ChartTitle.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Layout&nbsp;&quot;</span>&nbsp;&amp;&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Applying&nbsp;all&nbsp;these&nbsp;data&nbsp;labels&nbsp;will&nbsp;make&nbsp;the&nbsp;chart&nbsp;very&nbsp;ugly.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;use&nbsp;one&nbsp;of&nbsp;the&nbsp;enumerated&nbsp;values&nbsp;to&nbsp;set&nbsp;simple&nbsp;data&nbsp;labels:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.SeriesCollection(<span class="visualBasic__number">1</span>).ApplyDataLabels&nbsp;xlDataLabelsShowLabel&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.SeriesCollection(<span class="visualBasic__number">2</span>).ApplyDataLabels&nbsp;xlDataLabelsShowValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;supply&nbsp;other&nbsp;values&nbsp;individually&nbsp;to&nbsp;have&nbsp;more&nbsp;control&nbsp;over&nbsp;the&nbsp;data&nbsp;labels:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.SeriesCollection(<span class="visualBasic__number">3</span>).ApplyDataLabels&nbsp;ShowCategoryName:=<span class="visualBasic__keyword">True</span>,&nbsp;ShowValue:=<span class="visualBasic__keyword">True</span>,&nbsp;Separator:=<span class="visualBasic__string">&quot;:&nbsp;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Adjust&nbsp;the&nbsp;thickness&nbsp;of&nbsp;the&nbsp;back&nbsp;wall.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.BackWall.Thickness&nbsp;=&nbsp;<span class="visualBasic__number">25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;the&nbsp;chart&nbsp;type:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.ChartType&nbsp;=&nbsp;xl3DBarStacked&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;the&nbsp;3D&nbsp;rotation.&nbsp;This&nbsp;property&nbsp;applies&nbsp;only&nbsp;to&nbsp;3D&nbsp;charts:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">30</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;the&nbsp;chart&nbsp;fill,&nbsp;using&nbsp;a&nbsp;theme&nbsp;color:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent1&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;gradient:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.PresetGradient&nbsp;msoGradientHorizontal,&nbsp;<span class="visualBasic__number">1</span>,&nbsp;msoGradientLateSunset&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;other&nbsp;shape&nbsp;settings:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Line.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.ThreeD.BevelTopType&nbsp;=&nbsp;msoBevelRelaxedInset&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Glow.Color.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent3&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Shadow.Type&nbsp;=&nbsp;msoShadow32&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25961" href="/site/view/file/25961/1/Office.Chart.ModifyChartLayout.txt">Office.Chart.ModifyChartLayout.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25962" href="/site/view/file/25962/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/">Office Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
