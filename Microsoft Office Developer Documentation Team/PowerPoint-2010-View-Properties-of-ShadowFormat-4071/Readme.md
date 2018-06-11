# PowerPoint 2010: View Properties of ShadowFormat Class Using PPT.Shadow
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* formatting slides
## IsPublished
* True
## ModifiedDate
* 2011-08-05 03:22:34
## Description

<h1>Introduction</h1>
<p>This sample shows how to investigate the <strong>Size </strong>property, the <strong>
Blur </strong>property, and the <strong>RotateWithShape </strong>property of the <strong>
ShadowFormat </strong>class in a Microsoft PowerPoint 2010 presentation.</p>
<p>This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</p>
<p>Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the sample, and setup code so that
 you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</p>
<p>Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions or as a starting point
 to create more complex solutions.</p>
<h1><span>Building the Sample</span></h1>
<p><em>Copy this code into a module in a new presentation. Display the VBA window side-by-side with the PowerPoint window and press F8 to single step through this code for the most effective use of this demonstration.</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ShadowFormatDemo()
    ' Work with new ShadowFormat features.
   
    ' Create a blank slide.
    Dim sld As Slide
    Set sld = ActivePresentation.Slides.Add(2, ppLayoutBlank)
  
    ' Add a shape to the new slide.
    Dim shp As Shape
    Set shp = sld.Shapes.AddShape(msoShapeGear9, 100, 100, 200, 200)
  
    sld.Select
  
    With shp.Shadow
        ' Set values to demonstrate blur:
        .Size = 120
        .ForeColor.ObjectThemeColor = msoThemeColorAccent6
        .Transparency = 0.8
       
        .OffsetX = 20
        .OffsetY = 40
       
        ' Work with the Blur property, which indicates the
        ' amount of blur in the shadow, measured in points:
        .Blur = 1
        .Blur = 5
        .Blur = 10
       
        ' Now try varying the Size property, which measures the
        ' size as a percentage of the size of the shape, times 100:
        .Size = 100
        .Size = 120
        .Size = 140
        .Size = 160
        .Size = 180
    End With
   
    ' The RotateWithShape property is misleading:
    ' The shadow always rotates with the shape. The behavior
    ' is subtly different, depending on the shape and the
    ' offset of the shadow. Try it both ways to compare.
    ' You should see a different when stepping through
    ' these two versions of the code. When the property is true,
    ' it looks like the source of the light casting the shadow
    ' rotates with the shape. When false, it looks like the source
    ' of the light doesn't move.
    shp.Shadow.RotateWithShape = msoTrue
    shp.Rotation = 30
    shp.Rotation = 60
    shp.Rotation = 90
    shp.Rotation = 120
    shp.Rotation = 150
    shp.Rotation = 180
    shp.Rotation = 210
    shp.Rotation = 240
    shp.Rotation = 270
    shp.Rotation = 300
    shp.Rotation = 330
   
    shp.Shadow.RotateWithShape = msoFalse
    shp.Rotation = 30
    shp.Rotation = 60
    shp.Rotation = 90
    shp.Rotation = 120
    shp.Rotation = 150
    shp.Rotation = 180
    shp.Rotation = 210
    shp.Rotation = 240
    shp.Rotation = 270
    shp.Rotation = 300
    shp.Rotation = 330
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ShadowFormatDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;new&nbsp;ShadowFormat&nbsp;features.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;blank&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides.Add(<span class="visualBasic__number">2</span>,&nbsp;ppLayoutBlank)&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;shape&nbsp;to&nbsp;the&nbsp;new&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShapeGear9,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.Shadow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;values&nbsp;to&nbsp;demonstrate&nbsp;blur:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">120</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ForeColor.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorAccent6&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.8</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OffsetX&nbsp;=&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OffsetY&nbsp;=&nbsp;<span class="visualBasic__number">40</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;Blur&nbsp;property,&nbsp;which&nbsp;indicates&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;amount&nbsp;of&nbsp;blur&nbsp;in&nbsp;the&nbsp;shadow,&nbsp;measured&nbsp;in&nbsp;points:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Blur&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Blur&nbsp;=&nbsp;<span class="visualBasic__number">5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Blur&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;try&nbsp;varying&nbsp;the&nbsp;Size&nbsp;property,&nbsp;which&nbsp;measures&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;size&nbsp;as&nbsp;a&nbsp;percentage&nbsp;of&nbsp;the&nbsp;size&nbsp;of&nbsp;the&nbsp;shape,&nbsp;times&nbsp;100:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">120</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">140</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">160</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">180</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;RotateWithShape&nbsp;property&nbsp;is&nbsp;misleading:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;shadow&nbsp;always&nbsp;rotates&nbsp;with&nbsp;the&nbsp;shape.&nbsp;The&nbsp;behavior</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;is&nbsp;subtly&nbsp;different,&nbsp;depending&nbsp;on&nbsp;the&nbsp;shape&nbsp;and&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;offset&nbsp;of&nbsp;the&nbsp;shadow.&nbsp;Try&nbsp;it&nbsp;both&nbsp;ways&nbsp;to&nbsp;compare.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;should&nbsp;see&nbsp;a&nbsp;different&nbsp;when&nbsp;stepping&nbsp;through</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;these&nbsp;two&nbsp;versions&nbsp;of&nbsp;the&nbsp;code.&nbsp;When&nbsp;the&nbsp;property&nbsp;is&nbsp;true,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;it&nbsp;looks&nbsp;like&nbsp;the&nbsp;source&nbsp;of&nbsp;the&nbsp;light&nbsp;casting&nbsp;the&nbsp;shadow</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;rotates&nbsp;with&nbsp;the&nbsp;shape.&nbsp;When&nbsp;false,&nbsp;it&nbsp;looks&nbsp;like&nbsp;the&nbsp;source</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;of&nbsp;the&nbsp;light&nbsp;doesn't&nbsp;move.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Shadow.RotateWithShape&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">30</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">60</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">90</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">120</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">150</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">180</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">210</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">240</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">270</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">300</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">330</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Shadow.RotateWithShape&nbsp;=&nbsp;msoFalse&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">30</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">60</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">90</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">120</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">150</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">180</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">210</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">240</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">270</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">300</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">330</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><em><a id="26168" href="/site/view/file/26168/1/PPT.Shadow.txt">PPT.Shadow.txt</a>&nbsp;- Download this sample only.<br>
</em></em></li><li><em><em><a id="26170" href="/site/view/file/26170/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em>
</li></ul>
<h1>More Information</h1>
<p><em>For more information on X, see ...?</em></p>
