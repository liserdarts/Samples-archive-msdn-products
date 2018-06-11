# Excel 2010: Show Properties of Exceeded Frames Using Excel.TextFrameProperties
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* text frame
## IsPublished
* True
## ModifiedDate
* 2011-08-03 02:10:38
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows the properties available for text in a text frame in Microsoft Excel 2010 when the text exceeds the frame size.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">To investigate the code, in a new workbook, copy this entire sample into the Sheet1 module in the VBA editor. Arrange the VBA window and the Excel window side by side so you can see the code and its actions simultaneously. Place
 the cursor within the TestClearHyperlinks procedure, and press F8 to single step through the code. (You must single step to see the modifications that the various property values make.)</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestTextFrame()
    Dim tf As TextFrame
    Dim shp As Shape
    Set shp = Shapes.AddShape(msoShapeRectangle, 0, 0, 90, 30)
    shp.TextFrame.Characters.Font.Color = vbBlack
    shp.Fill.BackColor.ObjectThemeColor = msoThemeColorBackground1
    Set tf = shp.TextFrame
   
    ' Horizontal overflow only has effect if the WordWrap
    ' property is set to msoFalse:
    shp.TextFrame2.WordWrap = msoFalse
    tf.Characters.Text = &quot;How will this text lay out in the text frame?&quot;
    ' This is the default value:
    tf.HorizontalOverflow = xlOartHorizontalOverflowClip
    ' Cause the text that doesn't fit to overflow the shape:
    tf.HorizontalOverflow = xlOartHorizontalOverflowOverflow
   
    ' Vertical overflow only has effect if the WordWrap property
    ' is set to msoTrue, and the AutoSize property is set to msoAutoSizeNone.
    shp.TextFrame2.WordWrap = msoTrue
    shp.TextFrame2.AutoSize = msoAutoSizeNone
    ' This is the default value:
    tf.VerticalOverflow = xlOartVerticalOverflowClip
    ' Display an ellipsis for overflow:
    tf.VerticalOverflow = xlOartVerticalOverflowEllipsis
    ' Simply allow the text to overflow the shape:
    tf.VerticalOverflow = xlOartVerticalOverflowOverflow
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestTextFrame()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;tf&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;TextFrame&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;Shapes.AddShape(msoShapeRectangle,&nbsp;<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">90</span>,&nbsp;<span class="visualBasic__number">30</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.TextFrame.Characters.Font.Color&nbsp;=&nbsp;vbBlack&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.BackColor.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorBackground1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;tf&nbsp;=&nbsp;shp.TextFrame&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Horizontal&nbsp;overflow&nbsp;only&nbsp;has&nbsp;effect&nbsp;if&nbsp;the&nbsp;WordWrap</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;property&nbsp;is&nbsp;set&nbsp;to&nbsp;msoFalse:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.TextFrame2.WordWrap&nbsp;=&nbsp;msoFalse&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tf.Characters.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;How&nbsp;will&nbsp;this&nbsp;text&nbsp;lay&nbsp;out&nbsp;in&nbsp;the&nbsp;text&nbsp;frame?&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;is&nbsp;the&nbsp;default&nbsp;value:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tf.HorizontalOverflow&nbsp;=&nbsp;xlOartHorizontalOverflowClip&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Cause&nbsp;the&nbsp;text&nbsp;that&nbsp;doesn't&nbsp;fit&nbsp;to&nbsp;overflow&nbsp;the&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tf.HorizontalOverflow&nbsp;=&nbsp;xlOartHorizontalOverflowOverflow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Vertical&nbsp;overflow&nbsp;only&nbsp;has&nbsp;effect&nbsp;if&nbsp;the&nbsp;WordWrap&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;is&nbsp;set&nbsp;to&nbsp;msoTrue,&nbsp;and&nbsp;the&nbsp;AutoSize&nbsp;property&nbsp;is&nbsp;set&nbsp;to&nbsp;msoAutoSizeNone.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.TextFrame2.WordWrap&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.TextFrame2.AutoSize&nbsp;=&nbsp;msoAutoSizeNone&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;is&nbsp;the&nbsp;default&nbsp;value:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tf.VerticalOverflow&nbsp;=&nbsp;xlOartVerticalOverflowClip&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Display&nbsp;an&nbsp;ellipsis&nbsp;for&nbsp;overflow:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tf.VerticalOverflow&nbsp;=&nbsp;xlOartVerticalOverflowEllipsis&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Simply&nbsp;allow&nbsp;the&nbsp;text&nbsp;to&nbsp;overflow&nbsp;the&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tf.VerticalOverflow&nbsp;=&nbsp;xlOartVerticalOverflowOverflow&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25946" href="/site/view/file/25946/1/Excel.TextFrameProperties.txt">Excel.TextFrameProperties.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25945" href="/site/view/file/25945/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
