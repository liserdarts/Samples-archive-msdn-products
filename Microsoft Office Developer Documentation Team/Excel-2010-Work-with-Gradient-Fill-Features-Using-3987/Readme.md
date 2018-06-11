# Excel 2010: Work with Gradient Fill Features Using Excel.Gradient Method
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Conditional Formatting
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:30:03
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the gradient fill features of the
<strong>FillFormat </strong>object in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the GradientDemo procedure, and then press F8 to single-step through the code. Arrange the VBA and Excel windows side by
 side so you can follow the instructions in the comments.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub GradientDemo()
    Dim rng As Range
  
    Set rng = Range(&quot;B2:C10&quot;)
    rng.Cells.Merge
    Dim grd1 As LinearGradient
  
    rng.Interior.Pattern = XlPattern.xlPatternLinearGradient
    Set grd1 = rng.Interior.Gradient
  
    Dim cs As ColorStop
    ' Set the gradient to be tilted by 10 degrees:
    grd1.Degree = 10
  
    ' Add a color stop at 25% of the width:
    Set cs = grd1.ColorStops.Add(0.25)
    cs.Color = vbYellow
    ' Set the shading: -1 is black, 1 is white, 0 is the full color.
    ' Setting this to 0.25 makes it a little paler.
    cs.TintAndShade = 0.25
  
    ' Add a color stop at 100% of the width:
    Set cs = grd1.ColorStops.Add(1)
    cs.Color = vbRed
  
    ' Repeat with a rectangular gradient:
  
    Set rng = Range(&quot;E2:F10&quot;)
    Dim grd2 As RectangularGradient
    rng.Interior.Pattern = XlPattern.xlPatternRectangularGradient
    rng.Cells.Merge
  
    Set grd2 = rng.Interior.Gradient
    ' Set the offset for the rectangular gradient
    ' to the center of the region:
    grd2.RectangleLeft = 0.5
    grd2.RectangleTop = 0.5
  
    ' Add color stops at 25% and 100%:
    Set cs = grd2.ColorStops.Add(0.25)
    cs.Color = vbRed
  
    Set cs = grd2.ColorStops.Add(1)
    cs.Color = vbYellow
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;GradientDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;B2:C10&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Cells.Merge&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;grd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;LinearGradient&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Interior.Pattern&nbsp;=&nbsp;XlPattern.xlPatternLinearGradient&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;grd1&nbsp;=&nbsp;rng.Interior.Gradient&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ColorStop&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;gradient&nbsp;to&nbsp;be&nbsp;tilted&nbsp;by&nbsp;10&nbsp;degrees:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;grd1.Degree&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;color&nbsp;stop&nbsp;at&nbsp;25%&nbsp;of&nbsp;the&nbsp;width:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cs&nbsp;=&nbsp;grd1.ColorStops.Add(<span class="visualBasic__number">0.25</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cs.Color&nbsp;=&nbsp;vbYellow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;shading:&nbsp;-1&nbsp;is&nbsp;black,&nbsp;1&nbsp;is&nbsp;white,&nbsp;0&nbsp;is&nbsp;the&nbsp;full&nbsp;color.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Setting&nbsp;this&nbsp;to&nbsp;0.25&nbsp;makes&nbsp;it&nbsp;a&nbsp;little&nbsp;paler.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cs.TintAndShade&nbsp;=&nbsp;<span class="visualBasic__number">0.25</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;color&nbsp;stop&nbsp;at&nbsp;100%&nbsp;of&nbsp;the&nbsp;width:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cs&nbsp;=&nbsp;grd1.ColorStops.Add(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cs.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Repeat&nbsp;with&nbsp;a&nbsp;rectangular&nbsp;gradient:</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;E2:F10&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;grd2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;RectangularGradient&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Interior.Pattern&nbsp;=&nbsp;XlPattern.xlPatternRectangularGradient&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Cells.Merge&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;grd2&nbsp;=&nbsp;rng.Interior.Gradient&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;offset&nbsp;for&nbsp;the&nbsp;rectangular&nbsp;gradient</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;the&nbsp;center&nbsp;of&nbsp;the&nbsp;region:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;grd2.RectangleLeft&nbsp;=&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;grd2.RectangleTop&nbsp;=&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;color&nbsp;stops&nbsp;at&nbsp;25%&nbsp;and&nbsp;100%:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cs&nbsp;=&nbsp;grd2.ColorStops.Add(<span class="visualBasic__number">0.25</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cs.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cs&nbsp;=&nbsp;grd2.ColorStops.Add(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cs.Color&nbsp;=&nbsp;vbYellow&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25887" href="/site/view/file/25887/1/Excel.Gradient.txt">Excel.Gradient.txt</a>- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25888" href="/site/view/file/25888/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
