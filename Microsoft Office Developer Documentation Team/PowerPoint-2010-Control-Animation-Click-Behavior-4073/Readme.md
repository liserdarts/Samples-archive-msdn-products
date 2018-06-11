# PowerPoint 2010: Control Animation Click Behavior Using PPT.SlideShowClicks
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
* 2011-08-05 03:31:27
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>GetClickCount
</strong>method, the <strong>GetClickIndex </strong>method, and the <strong>GotoClick
</strong>method to control the click behavior for animations in Microsoft PowerPoint 2010 presentations.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy the following code into a module in an new PowerPoint presentation. Place the cursor within the CreateShapesAndAnimations procedure, and press F5 to run the procedure.</span></p>
<p><span style="font-size:small">Switch to PowerPoint, and run the presentation. Slowly click on the first slide three times to view the complete set of animations, noting that the first animation occurs before you click.</span></p>
<p><span style="font-size:small">Reset the presentation, and this time, once the first animation occurs, switch back to the VBA editor window, place your cursor in the SlideShowClicks procedure, and press F5 to run it. Note the results in the Immediate window.
 The code moves the animation to click number 2. Switch back to the running presentation, and you'll see the animation immediately progress as if you had clicked twice. (Hover the mouse over the presentation so that it updates the screen.) Click the presentation
 one time to show the final shape.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub SlideShowClicks()
    If SlideShowWindows.Count = 0 Then
        Exit Sub
    End If
   
    Dim sswView As SlideShowView
    Set sswView = SlideShowWindows(1).View
   
    ' This should return true, because the first animation occurs
    ' without requiring user input:
    Debug.Print &quot;First animation is automatic: &quot; &amp; sswView.FirstAnimationIsAutomatic
   
    Debug.Print &quot;Click animations: &quot; &amp; sswView.GetClickCount

    'Get the current index of click animations on the slide.
    Debug.Print &quot;Current click position (before GotoClick):  &quot; &amp; sswView.GetClickIndex

    'Jump to the 2nd mouse click.
    sswView.GotoClick 2
   
    'Get the current index of click animations on the slide.
    Debug.Print &quot;Current click position (after GotoClick):  &quot; &amp; sswView.GetClickIndex
   
End Sub

Sub CreateShapesAndAnimations()
    Dim sld As Slide
    Set sld = ActivePresentation.Slides(1)
    Dim shp As Shape
   
    ' Create a shape and add it to the timeline.
    Set shp = sld.Shapes.AddShape(msoShapeCloud, 10, 10, 200, 200)
    shp.Fill.ForeColor.RGB = vbRed
    sld.TimeLine.MainSequence.AddEffect shp, effectId:=msoAnimEffectBoomerang, trigger:=msoAnimTriggerWithPrevious
       
    ' Repeat for a second shape.
    Set shp = sld.Shapes.AddShape(msoShape8pointStar, 150, 240, 200, 200)
    shp.Fill.PresetGradient msoGradientHorizontal, 1, msoGradientDaybreak
    sld.TimeLine.MainSequence.AddEffect shp, effectId:=msoAnimEffectAscend
   
    ' Add a third shape.
    Set shp = sld.Shapes.AddShape(msoShapeDecagon, 500, 10, 200, 200)
    shp.Fill.Solid
    shp.Fill.ForeColor.ObjectThemeColor = msoThemeColorAccent4
    sld.TimeLine.MainSequence.AddEffect shp, effectId:=msoAnimEffectDescend
   
    ' Add a fourth shape.
    Set shp = sld.Shapes.AddShape(msoShapeCan, 500, 240, 200, 200)
    shp.Fill.Solid
    shp.Fill.ForeColor.ObjectThemeColor = msoThemeColorAccent2
    sld.TimeLine.MainSequence.AddEffect shp, effectId:=msoAnimEffectCenterRevolve
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;SlideShowClicks()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;SlideShowWindows.Count&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sswView&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;SlideShowView&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sswView&nbsp;=&nbsp;SlideShowWindows(<span class="visualBasic__number">1</span>).View&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;should&nbsp;return&nbsp;true,&nbsp;because&nbsp;the&nbsp;first&nbsp;animation&nbsp;occurs</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;without&nbsp;requiring&nbsp;user&nbsp;input:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;First&nbsp;animation&nbsp;is&nbsp;automatic:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;sswView.FirstAnimationIsAutomatic&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Click&nbsp;animations:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;sswView.GetClickCount&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Get&nbsp;the&nbsp;current&nbsp;index&nbsp;of&nbsp;click&nbsp;animations&nbsp;on&nbsp;the&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Current&nbsp;click&nbsp;position&nbsp;(before&nbsp;GotoClick):&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;sswView.GetClickIndex&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Jump&nbsp;to&nbsp;the&nbsp;2nd&nbsp;mouse&nbsp;click.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sswView.GotoClick&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Get&nbsp;the&nbsp;current&nbsp;index&nbsp;of&nbsp;click&nbsp;animations&nbsp;on&nbsp;the&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Current&nbsp;click&nbsp;position&nbsp;(after&nbsp;GotoClick):&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;sswView.GetClickIndex&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;CreateShapesAndAnimations()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;shape&nbsp;and&nbsp;add&nbsp;it&nbsp;to&nbsp;the&nbsp;timeline.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShapeCloud,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.ForeColor.RGB&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.TimeLine.MainSequence.AddEffect&nbsp;shp,&nbsp;effectId:=msoAnimEffectBoomerang,&nbsp;trigger:=msoAnimTriggerWithPrevious&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Repeat&nbsp;for&nbsp;a&nbsp;second&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShape8pointStar,&nbsp;<span class="visualBasic__number">150</span>,&nbsp;<span class="visualBasic__number">240</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.PresetGradient&nbsp;msoGradientHorizontal,&nbsp;<span class="visualBasic__number">1</span>,&nbsp;msoGradientDaybreak&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.TimeLine.MainSequence.AddEffect&nbsp;shp,&nbsp;effectId:=msoAnimEffectAscend&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;third&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShapeDecagon,&nbsp;<span class="visualBasic__number">500</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.Solid&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.ForeColor.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorAccent4&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.TimeLine.MainSequence.AddEffect&nbsp;shp,&nbsp;effectId:=msoAnimEffectDescend&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;fourth&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShapeCan,&nbsp;<span class="visualBasic__number">500</span>,&nbsp;<span class="visualBasic__number">240</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.Solid&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.ForeColor.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorAccent2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.TimeLine.MainSequence.AddEffect&nbsp;shp,&nbsp;effectId:=msoAnimEffectCenterRevolve&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26173" href="/site/view/file/26173/1/PPT.SlideShowClicks.txt">PPT.SlideShowClicks.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26174" href="/site/view/file/26174/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
