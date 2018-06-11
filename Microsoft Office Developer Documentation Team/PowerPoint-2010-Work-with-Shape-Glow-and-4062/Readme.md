# PowerPoint 2010: Work with Shape Glow and Reflection Properties
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
* 2011-08-05 12:41:35
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates the <strong>Glow </strong>
property, and <strong>Reflection </strong>property of the <strong>GlowFormat </strong>
class and <strong>ReflectionFormat </strong>class in Microsoft PowerPoint 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Are Copy this code into a module in a new presentation. Display the VBA window side-by-side with the PowerPoint window and press F8 (and then Shift&#43;F8) to single step through this code for the most effective use of this demonstration.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub GlowAndReflectionDemo()
    ' Work with Shape Glow and Reflection properties.
   
    ' Create a blank slide.
    Dim sld As Slide
    Set sld = ActivePresentation.Slides.Add(2, ppLayoutBlank)
   
    ' Add a shape to the new slide.
    Dim shp As Shape
    Set shp = sld.Shapes.AddShape(msoShape5pointStar, 100, 100, 200, 200)
   
    sld.Select
   
    With shp.Glow
        ' This code is only meaningful if you single-step through it.
        ' Note that the user interface offers presets, which aren't exposed
        ' through the object model.
       
        ' Pick a starting color.
        .Color.ObjectThemeColor = msoThemeColorAccent2
       
        ' Radius corresponds to the Size property in the user interface, and is
        ' measured in points.
       
        ' Try a few sizes:
        .Radius = 8
        .Radius = 20
        .Radius = 50
       
        ' Try varying transparencies:
        .Transparency = 0
        .Transparency = 0.25
        .Transparency = 0.5
        .Transparency = 0.75
       
        ' Try a few colors:
        .Color.ObjectThemeColor = msoThemeColorAccent5
        .Color.ObjectThemeColor = msoThemeColorAccent6
        .Color.RGB = RGB(255, 255, 128)
    End With
   
    ' Work with the shape's reflection.
    ' Single step through this code to see the changes.
    With shp.Reflection
        ' Cycle throught the 9 built-in reflection types:
        .Type = msoReflectionType1
        .Type = msoReflectionType2
        .Type = msoReflectionType3
        .Type = msoReflectionType4
        .Type = msoReflectionType5
        .Type = msoReflectionType6
        .Type = msoReflectionType7
        .Type = msoReflectionType8
        .Type = msoReflectionType9
       
        ' Transparency is treated as a percentage, with 1 being completely transparent, and 0 being nearly opaque:
        .Transparency = 0.9
        .Transparency = 0.7
        .Transparency = 0.5
        .Transparency = 0.3
       
        ' Size is measured as a percentage of the original shape, times 100:
        .Size = 10
        .Size = 25
        .Size = 50
        .Size = 75
        .Size = 100
        .Size = 60
       
        ' Offset corresponds to the Distance property in the user interface, measured in points.
        ' It indicates how far from the shape to draw the reflection:
        .Offset = 0
        .Offset = 10
        .Offset = 20
       
        ' Blur is measured in points, indicating the amount of blur to add to the reflection:
        .Blur = 2
        .Blur = 5
        .Blur = 10
       
    End With
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;GlowAndReflectionDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;Shape&nbsp;Glow&nbsp;and&nbsp;Reflection&nbsp;properties.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;blank&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides.Add(<span class="visualBasic__number">2</span>,&nbsp;ppLayoutBlank)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;shape&nbsp;to&nbsp;the&nbsp;new&nbsp;slide.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShape5pointStar,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.Glow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;code&nbsp;is&nbsp;only&nbsp;meaningful&nbsp;if&nbsp;you&nbsp;single-step&nbsp;through&nbsp;it.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;the&nbsp;user&nbsp;interface&nbsp;offers&nbsp;presets,&nbsp;which&nbsp;aren't&nbsp;exposed</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;through&nbsp;the&nbsp;object&nbsp;model.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Pick&nbsp;a&nbsp;starting&nbsp;color.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorAccent2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Radius&nbsp;corresponds&nbsp;to&nbsp;the&nbsp;Size&nbsp;property&nbsp;in&nbsp;the&nbsp;user&nbsp;interface,&nbsp;and&nbsp;is</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;measured&nbsp;in&nbsp;points.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;a&nbsp;few&nbsp;sizes:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Radius&nbsp;=&nbsp;<span class="visualBasic__number">8</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Radius&nbsp;=&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Radius&nbsp;=&nbsp;<span class="visualBasic__number">50</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;varying&nbsp;transparencies:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.75</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;a&nbsp;few&nbsp;colors:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorAccent5&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.ObjectThemeColor&nbsp;=&nbsp;msoThemeColorAccent6&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.RGB&nbsp;=&nbsp;RGB(<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">128</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;shape's&nbsp;reflection.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Single&nbsp;step&nbsp;through&nbsp;this&nbsp;code&nbsp;to&nbsp;see&nbsp;the&nbsp;changes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.Reflection&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Cycle&nbsp;throught&nbsp;the&nbsp;9&nbsp;built-in&nbsp;reflection&nbsp;types:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType3&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType4&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType5&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType6&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType7&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType8&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;msoReflectionType9&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Transparency&nbsp;is&nbsp;treated&nbsp;as&nbsp;a&nbsp;percentage,&nbsp;with&nbsp;1&nbsp;being&nbsp;completely&nbsp;transparent,&nbsp;and&nbsp;0&nbsp;being&nbsp;nearly&nbsp;opaque:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.9</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.7</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Transparency&nbsp;=&nbsp;<span class="visualBasic__number">0.3</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Size&nbsp;is&nbsp;measured&nbsp;as&nbsp;a&nbsp;percentage&nbsp;of&nbsp;the&nbsp;original&nbsp;shape,&nbsp;times&nbsp;100:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">50</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">75</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">60</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Offset&nbsp;corresponds&nbsp;to&nbsp;the&nbsp;Distance&nbsp;property&nbsp;in&nbsp;the&nbsp;user&nbsp;interface,&nbsp;measured&nbsp;in&nbsp;points.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;It&nbsp;indicates&nbsp;how&nbsp;far&nbsp;from&nbsp;the&nbsp;shape&nbsp;to&nbsp;draw&nbsp;the&nbsp;reflection:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Offset&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Offset&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Offset&nbsp;=&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Blur&nbsp;is&nbsp;measured&nbsp;in&nbsp;points,&nbsp;indicating&nbsp;the&nbsp;amount&nbsp;of&nbsp;blur&nbsp;to&nbsp;add&nbsp;to&nbsp;the&nbsp;reflection:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Blur&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Blur&nbsp;=&nbsp;<span class="visualBasic__number">5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Blur&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26150" href="/site/view/file/26150/1/PPT.GlowAndReflection.txt">PPT.GlowAndReflection.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26151" href="/site/view/file/26151/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
