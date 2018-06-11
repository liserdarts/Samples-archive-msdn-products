# PowerPoint 2010: Add and Format Shapes Using PPT.ColorFormat.Brightness
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
* 2011-08-05 12:26:02
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to select the first slide in a Microsoft PowerPoint 2010 presentation, add a shape to it, and then change the brightness of the shape's foreground.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Start this procedure in a new PowerPoint presentation. The code selects the first slide, adds a shape to it, and then changes the brightness of the shape's foreground. You'll find it most interesting to watch if you put the
 VBA editor and PowerPoint itself side by side on your monitor. Set a breakpoint, run the code, and then hold down&nbsp;the F8 key to step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestBrightness()
    Dim i As Integer
    Dim shp As Shape
    Dim sld As Slide
   
    Set sld = ActivePresentation.Slides(1)
   
    ' Add a new shape: A 200x100 pixel balloon, and set its color:
    Set shp = sld.Shapes.AddShape(msoShapeBalloon, 10, 10, 200, 100)
    shp.Fill.ForeColor.RGB = 3487637
   
    ' Finally, alter the Brightness of the color. Do not use
    ' this technique to create animations--PowerPoint handles
    ' that itself. This is meant only as instructive code that
    ' demonstrates how modifying the Brightness property
    ' changes the way a shape looks.
    For i = 0 To 100
        SetBrightness shp, i / 100
        ' Wait 1/10 second or so.
        Pause 0.1
    Next i
End Sub

Sub SetBrightness(shp As Shape, brightnessValue As Single)
    ' Set the Brightness property of a ColorFormat object.
    ' You can retrieve a ColorFormat in a number of ways.
    ' See this page for more information on ways to retrieve
    ' a reference to a ColorFormat object:
    ' http://msdn.microsoft.com/en-us/library/ff744611.aspx
   
    Dim cf As ColorFormat
    Set cf = shp.Fill.ForeColor
    cf.brightness = brightnessValue
End Sub

Function Pause(numberOfSeconds As Variant)
    Dim startTime, endTime As Variant

    startTime = Timer
    endTime = startTime &#43; numberOfSeconds
   
    Do While Timer &lt; endTime
        DoEvents
    Loop
End Function
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestBrightness()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;new&nbsp;shape:&nbsp;A&nbsp;200x100&nbsp;pixel&nbsp;balloon,&nbsp;and&nbsp;set&nbsp;its&nbsp;color:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShapeBalloon,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.ForeColor.RGB&nbsp;=&nbsp;<span class="visualBasic__number">3487637</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Finally,&nbsp;alter&nbsp;the&nbsp;Brightness&nbsp;of&nbsp;the&nbsp;color.&nbsp;Do&nbsp;not&nbsp;use</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;this&nbsp;technique&nbsp;to&nbsp;create&nbsp;animations--PowerPoint&nbsp;handles</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;that&nbsp;itself.&nbsp;This&nbsp;is&nbsp;meant&nbsp;only&nbsp;as&nbsp;instructive&nbsp;code&nbsp;that</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;demonstrates&nbsp;how&nbsp;modifying&nbsp;the&nbsp;Brightness&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;changes&nbsp;the&nbsp;way&nbsp;a&nbsp;shape&nbsp;looks.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetBrightness&nbsp;shp,&nbsp;i&nbsp;/&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Wait&nbsp;1/10&nbsp;second&nbsp;or&nbsp;so.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pause&nbsp;<span class="visualBasic__number">0.1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;SetBrightness(shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape,&nbsp;brightnessValue&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Single</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;Brightness&nbsp;property&nbsp;of&nbsp;a&nbsp;ColorFormat&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;retrieve&nbsp;a&nbsp;ColorFormat&nbsp;in&nbsp;a&nbsp;number&nbsp;of&nbsp;ways.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;See&nbsp;this&nbsp;page&nbsp;for&nbsp;more&nbsp;information&nbsp;on&nbsp;ways&nbsp;to&nbsp;retrieve</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;reference&nbsp;to&nbsp;a&nbsp;ColorFormat&nbsp;object:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;http://msdn.microsoft.com/en-us/library/ff744611.aspx</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cf&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ColorFormat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cf&nbsp;=&nbsp;shp.Fill.ForeColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cf.brightness&nbsp;=&nbsp;brightnessValue&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Function</span>&nbsp;Pause(numberOfSeconds&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;startTime,&nbsp;endTime&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;startTime&nbsp;=&nbsp;Timer&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;endTime&nbsp;=&nbsp;startTime&nbsp;&#43;&nbsp;numberOfSeconds&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;<span class="visualBasic__keyword">While</span>&nbsp;Timer&nbsp;&lt;&nbsp;endTime&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DoEvents&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Loop</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26144" href="/site/view/file/26144/1/PPT.ColorFormat.Brightness.txt">PPT.ColorFormat.Brightness.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26145" href="/site/view/file/26145/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
