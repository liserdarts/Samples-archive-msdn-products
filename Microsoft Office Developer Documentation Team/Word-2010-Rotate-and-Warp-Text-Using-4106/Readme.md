# Word 2010: Rotate and Warp Text Using Word.WorkWithTextFrame
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Textframe objects
## IsPublished
* True
## ModifiedDate
* 2011-08-05 06:13:43
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use new properties of the
<strong>TextFrame </strong>object in Microsoft Word 2010 such as text rotation and warping.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy this code into a module in a new document. Then, in the VBA editor, place the cursor within this procedure and press F8 to single step through the code. Arrange the VBA and Word windows side by side on screen so you can
 view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WorkWithTextFrame()
    Dim shp As Shape
    With ActiveDocument.Shapes
        Set shp = .AddShape(MsoAutoShapeType.msoShapeOval, 10, 10, 400, 200)
        With shp.TextFrame
            .TextRange.Text = &quot;Here's my shape!&quot;
           
            ' Set the location of the anchor: in Word
            ' note that the anchor is at the top center.
            .HorizontalAnchor = msoAnchorCenter
            .VerticalAnchor = msoAnchorTop
           
            ' Note that the text doesn't rotate with the shape.
            ' Unless you set this to msoTrue, when you rotate the
            ' shape, the text rotates as well.
            ' Try commenting out this line and running the code
            ' to see how it affects the behavior.
            .NoTextRotation = msoTrue
           
            ' Set the path format. You can use only one of
            ' the built-in formats, or msoPathTypeNone, which
            ' removes the path:
            .PathFormat = msoPathType2
           
            ' Modify the warping of the text, using
            ' one of the built-in warp formats:
            .WarpFormat = msoWarpFormat12
        End With
        shp.Rotation = 45
        shp.Top = 100
    End With
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithTextFrame()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActiveDocument.Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;.AddShape(MsoAutoShapeType.msoShapeOval,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">400</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.TextFrame&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextRange.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Here's&nbsp;my&nbsp;shape!&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;location&nbsp;of&nbsp;the&nbsp;anchor:&nbsp;in&nbsp;Word</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;note&nbsp;that&nbsp;the&nbsp;anchor&nbsp;is&nbsp;at&nbsp;the&nbsp;top&nbsp;center.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.HorizontalAnchor&nbsp;=&nbsp;msoAnchorCenter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.VerticalAnchor&nbsp;=&nbsp;msoAnchorTop&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;the&nbsp;text&nbsp;doesn't&nbsp;rotate&nbsp;with&nbsp;the&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Unless&nbsp;you&nbsp;set&nbsp;this&nbsp;to&nbsp;msoTrue,&nbsp;when&nbsp;you&nbsp;rotate&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;shape,&nbsp;the&nbsp;text&nbsp;rotates&nbsp;as&nbsp;well.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;commenting&nbsp;out&nbsp;this&nbsp;line&nbsp;and&nbsp;running&nbsp;the&nbsp;code</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;see&nbsp;how&nbsp;it&nbsp;affects&nbsp;the&nbsp;behavior.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.NoTextRotation&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;path&nbsp;format.&nbsp;You&nbsp;can&nbsp;use&nbsp;only&nbsp;one&nbsp;of</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;built-in&nbsp;formats,&nbsp;or&nbsp;msoPathTypeNone,&nbsp;which</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;removes&nbsp;the&nbsp;path:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PathFormat&nbsp;=&nbsp;msoPathType2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;the&nbsp;warping&nbsp;of&nbsp;the&nbsp;text,&nbsp;using</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;one&nbsp;of&nbsp;the&nbsp;built-in&nbsp;warp&nbsp;formats:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.WarpFormat&nbsp;=&nbsp;msoWarpFormat12&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">45</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Top&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26248" href="/site/view/file/26248/1/Word.WorkWithTextFrame.txt">Word.WorkWithTextFrame.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26249" href="/site/view/file/26249/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
