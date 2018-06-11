# PowerPoint 2010: Copy Animation Using PPT.PickupAndApplyAnimation
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Animation
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 01:18:22
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to copy an animation from one shape to another in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">It's easy to copy an animation from one shape to another. The PickupAnimation method places the animation information for a specific shape in a &quot;holding area&quot;. The ApplyAnimation method allows you to apply the animation to a
 specific shape.<br>
&nbsp;&nbsp;&nbsp;<br>
Execute the following code in a new presentation. This code creates a shape and adds three animations to it, picks up the animation, and then applies the animation to two other shapes.<br>
&nbsp;&nbsp;&nbsp;&nbsp;<br>
Once you have run the code, run the presentation to view the animations.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestPickupAnimation()
   
    With ActivePresentation.Slides(1)
        Dim shp1, shp2, shp3 As Shape
        Set shp1 = .Shapes.AddShape(msoShape12pointStar, 20, 20, 100, 100)
       
        .TimeLine.MainSequence.AddEffect shp1, msoAnimEffectFadedSwivel, , msoAnimTriggerAfterPrevious
        .TimeLine.MainSequence.AddEffect shp1, msoAnimEffectPathBounceRight, , msoAnimTriggerAfterPrevious
        .TimeLine.MainSequence.AddEffect shp1, msoAnimEffectSpin, , msoAnimTriggerAfterPrevious
       
        ' Now create a second shape, and apply the same animation to it:
        shp1.PickupAnimation
       
        Set shp2 = .Shapes.AddShape(msoShapeHexagon, 100, 20, 100, 100)
        shp2.ApplyAnimation
       
        ' And one more:
        Set shp3 = .Shapes.AddShape(msoShapeCloud, 180, 20, 100, 100)
        shp3.ApplyAnimation
       
    End With
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestPickupAnimation()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp1,&nbsp;shp2,&nbsp;shp3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp1&nbsp;=&nbsp;.Shapes.AddShape(msoShape12pointStar,&nbsp;<span class="visualBasic__number">20</span>,&nbsp;<span class="visualBasic__number">20</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TimeLine.MainSequence.AddEffect&nbsp;shp1,&nbsp;msoAnimEffectFadedSwivel,&nbsp;,&nbsp;msoAnimTriggerAfterPrevious&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TimeLine.MainSequence.AddEffect&nbsp;shp1,&nbsp;msoAnimEffectPathBounceRight,&nbsp;,&nbsp;msoAnimTriggerAfterPrevious&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TimeLine.MainSequence.AddEffect&nbsp;shp1,&nbsp;msoAnimEffectSpin,&nbsp;,&nbsp;msoAnimTriggerAfterPrevious&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;create&nbsp;a&nbsp;second&nbsp;shape,&nbsp;and&nbsp;apply&nbsp;the&nbsp;same&nbsp;animation&nbsp;to&nbsp;it:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp1.PickupAnimation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp2&nbsp;=&nbsp;.Shapes.AddShape(msoShapeHexagon,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">20</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp2.ApplyAnimation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;And&nbsp;one&nbsp;more:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp3&nbsp;=&nbsp;.Shapes.AddShape(msoShapeCloud,&nbsp;<span class="visualBasic__number">180</span>,&nbsp;<span class="visualBasic__number">20</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp3.ApplyAnimation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26160" href="/site/view/file/26160/1/PPT.PickupAndApplyAnimation.txt">PPT.PickupAndApplyAnimation.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26161" href="/site/view/file/26161/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
