# PowerPoint 2010: Work with FillFormat Texture Settings Using PPT.ShapeTexture
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* formatting tables
## IsPublished
* True
## ModifiedDate
* 2011-08-05 03:26:30
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to work with the <strong>FillFormat
</strong>class&rsquo; texture settings in a Microsoft PowerPoint 2010 presentation containing a table.</span></p>
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
<pre class="hidden">Sub TextureStyleDemo()
    ' Create a new slide with a simple table:
    Dim sld As Slide
    Set sld = ActivePresentation.Slides.Add(2, ppLayoutBlank)
    sld.Select
   
    Dim shp As Shape
    Set shp = sld.Shapes.AddShape(msoShapeSnipRoundRectangle, 50, 50, 400, 400)
    shp.Fill.PresetTextured msoTextureFishFossil
   
    With shp.Fill
        ' Set the alignment. There are other options, as well:
        .TextureAlignment = msoTextureTop
        .TextureAlignment = msoTextureBottom
        .TextureAlignment = msoTextureLeft
        .TextureAlignment = msoTextureRight
       
        ' TextureHorizontalScale is a percentage, as a fraction.
        ' This corresponds to the Scale X setting in the user interface:
        .TextureHorizontalScale = 1
        .TextureHorizontalScale = 0.75
        .TextureHorizontalScale = 0.5
        .TextureHorizontalScale = 0.25
        .TextureHorizontalScale = 1
       
        ' TextureVerticalScale is a percentage, as a fraction:
        ' This corresponds to the Scale Y setting in the user interface:
        .TextureVerticalScale = 1
        .TextureVerticalScale = 0.75
        .TextureVerticalScale = 0.5
        .TextureVerticalScale = 0.25
        .TextureVerticalScale = 1
               
        ' TextureOffsetX is measured in points:
        .TextureOffsetX = 10
        .TextureOffsetX = 50
        .TextureOffsetX = 100
       
        ' TextureOffsetY is measured in points:
        .TextureOffsetY = 10
        .TextureOffsetY = 50
        .TextureOffsetY = 100
       
        ' If you don't tile the texture, it only appears
        ' once inside the shape. The texture
        ' then stretches to fill the entire shape.
        .TextureTile = msoFalse
       
        ' Reset the texture so that it tiles. Notice that this also
        ' resets the tiling--it now looks different than it did
        ' previously!
        .TextureTile = msoTrue
       
        ' Reset the texture:
        .PresetTextured msoTextureFishFossil
       
        ' You can determine whether the texture rotates
        ' with the shape. First, try rotating the shape
        ' without rotating the texture:
        .RotateWithObject = msoFalse
        shp.Rotation = 10
               
        ' Changing RotateWithObject immediately rotates
        ' the texture to match the existing rotation:
        .RotateWithObject = msoTrue
        shp.Rotation = 30
    End With
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TextureStyleDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;new&nbsp;slide&nbsp;with&nbsp;a&nbsp;simple&nbsp;table:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides.Add(<span class="visualBasic__number">2</span>,&nbsp;ppLayoutBlank)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShapeSnipRoundRectangle,&nbsp;<span class="visualBasic__number">50</span>,&nbsp;<span class="visualBasic__number">50</span>,&nbsp;<span class="visualBasic__number">400</span>,&nbsp;<span class="visualBasic__number">400</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Fill.PresetTextured&nbsp;msoTextureFishFossil&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.Fill&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;alignment.&nbsp;There&nbsp;are&nbsp;other&nbsp;options,&nbsp;as&nbsp;well:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureAlignment&nbsp;=&nbsp;msoTextureTop&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureAlignment&nbsp;=&nbsp;msoTextureBottom&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureAlignment&nbsp;=&nbsp;msoTextureLeft&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureAlignment&nbsp;=&nbsp;msoTextureRight&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;TextureHorizontalScale&nbsp;is&nbsp;a&nbsp;percentage,&nbsp;as&nbsp;a&nbsp;fraction.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;corresponds&nbsp;to&nbsp;the&nbsp;Scale&nbsp;X&nbsp;setting&nbsp;in&nbsp;the&nbsp;user&nbsp;interface:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureHorizontalScale&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureHorizontalScale&nbsp;=&nbsp;<span class="visualBasic__number">0.75</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureHorizontalScale&nbsp;=&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureHorizontalScale&nbsp;=&nbsp;<span class="visualBasic__number">0.25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureHorizontalScale&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;TextureVerticalScale&nbsp;is&nbsp;a&nbsp;percentage,&nbsp;as&nbsp;a&nbsp;fraction:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;corresponds&nbsp;to&nbsp;the&nbsp;Scale&nbsp;Y&nbsp;setting&nbsp;in&nbsp;the&nbsp;user&nbsp;interface:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureVerticalScale&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureVerticalScale&nbsp;=&nbsp;<span class="visualBasic__number">0.75</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureVerticalScale&nbsp;=&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureVerticalScale&nbsp;=&nbsp;<span class="visualBasic__number">0.25</span>&nbsp;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureVerticalScale&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;TextureOffsetX&nbsp;is&nbsp;measured&nbsp;in&nbsp;points:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureOffsetX&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureOffsetX&nbsp;=&nbsp;<span class="visualBasic__number">50</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureOffsetX&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;TextureOffsetY&nbsp;is&nbsp;measured&nbsp;in&nbsp;points:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureOffsetY&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureOffsetY&nbsp;=&nbsp;<span class="visualBasic__number">50</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureOffsetY&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you&nbsp;don't&nbsp;tile&nbsp;the&nbsp;texture,&nbsp;it&nbsp;only&nbsp;appears</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;once&nbsp;inside&nbsp;the&nbsp;shape.&nbsp;The&nbsp;texture</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;then&nbsp;stretches&nbsp;to&nbsp;fill&nbsp;the&nbsp;entire&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureTile&nbsp;=&nbsp;msoFalse&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reset&nbsp;the&nbsp;texture&nbsp;so&nbsp;that&nbsp;it&nbsp;tiles.&nbsp;Notice&nbsp;that&nbsp;this&nbsp;also</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;resets&nbsp;the&nbsp;tiling--it&nbsp;now&nbsp;looks&nbsp;different&nbsp;than&nbsp;it&nbsp;did</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;previously!</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TextureTile&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reset&nbsp;the&nbsp;texture:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PresetTextured&nbsp;msoTextureFishFossil&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;determine&nbsp;whether&nbsp;the&nbsp;texture&nbsp;rotates</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;with&nbsp;the&nbsp;shape.&nbsp;First,&nbsp;try&nbsp;rotating&nbsp;the&nbsp;shape</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;without&nbsp;rotating&nbsp;the&nbsp;texture:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.RotateWithObject&nbsp;=&nbsp;msoFalse&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Changing&nbsp;RotateWithObject&nbsp;immediately&nbsp;rotates</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;texture&nbsp;to&nbsp;match&nbsp;the&nbsp;existing&nbsp;rotation:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.RotateWithObject&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Rotation&nbsp;=&nbsp;<span class="visualBasic__number">30</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26171" href="/site/view/file/26171/1/PPT.ShapeTexture.txt">PPT.ShapeTexture.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26172" href="/site/view/file/26172/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
