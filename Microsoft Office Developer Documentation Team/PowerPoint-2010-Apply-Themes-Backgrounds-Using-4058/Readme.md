# PowerPoint 2010: Apply Themes & Backgrounds Using PPT.ApplyTheme.BackgroundStyle
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* themes
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 12:21:53
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>ApplyTheme
</strong>method and the <strong>BackgroundStyle </strong>property in Microsoft PowerPoint 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy this code into a module in a new presentation. Display the VBA window side-by-side with the PowerPoint window and press F8 to single step through this code for the most effective use of this demonstration.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestBackgroundStyle()
    Dim sld As Slide
    Set sld = ActivePresentation.Slides(1)
   
   
    ' Modify this to match your own environment:
    Const themePath As String = &quot;C:\Program Files (x86)\Microsoft Office\Document Themes 14\&quot;
    Const themeName1 As String = themePath &amp; &quot;Angles.thmx&quot;
    Const themeName2 As String = themePath &amp; &quot;Perspective.thmx&quot;
    Const themeName3 As String = themePath &amp; &quot;Waveform.thmx&quot;
   
    ActivePresentation.ApplyTheme themeName1
    CycleThroughStyles sld
   
    ActivePresentation.ApplyTheme themeName2
    CycleThroughStyles sld

    ActivePresentation.ApplyTheme themeName3
    CycleThroughStyles sld
End Sub

Private Sub CycleThroughStyles(sld As Slide)
    ' This exercise will only be meaningful if you single step through this code.
    ' Each style looks slightly different:
    sld.BackgroundStyle = msoBackgroundStylePreset1
    sld.BackgroundStyle = msoBackgroundStylePreset2
    sld.BackgroundStyle = msoBackgroundStylePreset3
    sld.BackgroundStyle = msoBackgroundStylePreset4
    sld.BackgroundStyle = msoBackgroundStylePreset5
    sld.BackgroundStyle = msoBackgroundStylePreset6
    sld.BackgroundStyle = msoBackgroundStylePreset7
    sld.BackgroundStyle = msoBackgroundStylePreset8
    sld.BackgroundStyle = msoBackgroundStylePreset9
    sld.BackgroundStyle = msoBackgroundStylePreset10
    sld.BackgroundStyle = msoBackgroundStylePreset11
    sld.BackgroundStyle = msoBackgroundStylePreset12
   
    ' Note that these options are not valid:
    ' sld.BackgroundStyle = msoBackgroundStyleMixed
    ' sld.BackgroundStyle = msoBackgroundStyleNotAPreset
   
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestBackgroundStyle()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;this&nbsp;to&nbsp;match&nbsp;your&nbsp;own&nbsp;environment:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;themePath&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;&quot;C:\Program&nbsp;Files&nbsp;(x86)\Microsoft&nbsp;Office\Document&nbsp;Themes&nbsp;<span class="visualBasic__number">14</span>\&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;themeName1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;themePath&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Angles.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;themeName2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;themePath&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Perspective.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;themeName3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;themePath&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Waveform.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.ApplyTheme&nbsp;themeName1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CycleThroughStyles&nbsp;sld&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.ApplyTheme&nbsp;themeName2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CycleThroughStyles&nbsp;sld&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.ApplyTheme&nbsp;themeName3&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CycleThroughStyles&nbsp;sld&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;CycleThroughStyles(sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;exercise&nbsp;will&nbsp;only&nbsp;be&nbsp;meaningful&nbsp;if&nbsp;you&nbsp;single&nbsp;step&nbsp;through&nbsp;this&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Each&nbsp;style&nbsp;looks&nbsp;slightly&nbsp;different:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset3&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset4&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset5&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset6&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset7&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset8&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset9&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset10&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset11&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStylePreset12&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;these&nbsp;options&nbsp;are&nbsp;not&nbsp;valid:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStyleMixed</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;sld.BackgroundStyle&nbsp;=&nbsp;msoBackgroundStyleNotAPreset</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26142" href="/site/view/file/26142/1/PPT.ApplyTheme.BackgroundStyle.txt">PPT.ApplyTheme.BackgroundStyle.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26143" href="/site/view/file/26143/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
