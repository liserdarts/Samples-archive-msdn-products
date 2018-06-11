# PowerPoint 2010: Change Chart Locations Using PPT.InteractWithChartLocation
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Charts
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 12:45:05
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to work with the location of a chart in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Create a new presentation, add a module, and paste the following procedure into the new module. Arrange the VBA and PowerPoint windows side by side on your screen. In the PowerPoint presentation, on the first slide, select Insert
 and in the Illustrations group, select Chart. Pick a chart type, and click OK to insert the chart. Then place your cursor inside the following procedure and single-step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub InteractWithChartLocation()
    ' Use this procedure to demonstrate
    ' working with the location of a chart.
   
    Dim shp As Shape
    Dim cht As Chart
   
    ' If there isn't already a second slide, create one now:
    If ActivePresentation.Slides.Count &lt; 2 Then
        ActivePresentation.Slides.Add 2, ppLayoutBlank
    End If
   
    ' Loop through all the shapes on the first slide,
    ' looking for the first chart shape:
    For Each shp In ActivePresentation.Slides(1).Shapes
        If shp.Type = msoChart Then
            Exit For
        End If
    Next shp
   
    ' If you found a chart, keep going:
    If shp.Type = msoChart Then
        ' Cut the shape to the clipboard:
        shp.Cut
       
        Dim rng As ShapeRange
        Dim shpNew As Shape
       
        ' Paste the shape from the clipboard onto the second slide.
        ' This method returns a ShapeRange--you need a
        ' reference to the first pasted shape:
        Set rng = ActivePresentation.Slides(2).Shapes.Paste
        Set shpNew = rng.Item(1)
       
        ' Set the location of the new shape:
        shpNew.Top = 10
        shpNew.Left = 10
    End If
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;InteractWithChartLocation()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;this&nbsp;procedure&nbsp;to&nbsp;demonstrate</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;working&nbsp;with&nbsp;the&nbsp;location&nbsp;of&nbsp;a&nbsp;chart.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;there&nbsp;isn't&nbsp;already&nbsp;a&nbsp;second&nbsp;slide,&nbsp;create&nbsp;one&nbsp;now:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;ActivePresentation.Slides.Count&nbsp;&lt;&nbsp;<span class="visualBasic__number">2</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.Slides.Add&nbsp;<span class="visualBasic__number">2</span>,&nbsp;ppLayoutBlank&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Loop&nbsp;through&nbsp;all&nbsp;the&nbsp;shapes&nbsp;on&nbsp;the&nbsp;first&nbsp;slide,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;looking&nbsp;for&nbsp;the&nbsp;first&nbsp;chart&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;=&nbsp;msoChart&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you&nbsp;found&nbsp;a&nbsp;chart,&nbsp;keep&nbsp;going:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;=&nbsp;msoChart&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Cut&nbsp;the&nbsp;shape&nbsp;to&nbsp;the&nbsp;clipboard:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Cut&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ShapeRange&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shpNew&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Paste&nbsp;the&nbsp;shape&nbsp;from&nbsp;the&nbsp;clipboard&nbsp;onto&nbsp;the&nbsp;second&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;method&nbsp;returns&nbsp;a&nbsp;ShapeRange--you&nbsp;need&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;reference&nbsp;to&nbsp;the&nbsp;first&nbsp;pasted&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">2</span>).Shapes.Paste&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shpNew&nbsp;=&nbsp;rng.Item(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;location&nbsp;of&nbsp;the&nbsp;new&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shpNew.Top&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shpNew.Left&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26152" href="/site/view/file/26152/1/PPT.InteractWithChartLocation.txt">PPT.InteractWithChartLocation.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26153" href="/site/view/file/26153/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
