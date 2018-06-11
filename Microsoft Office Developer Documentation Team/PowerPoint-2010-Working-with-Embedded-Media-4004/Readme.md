# PowerPoint 2010: Working with Embedded Media
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* embedding media
## IsPublished
* True
## ModifiedDate
* 2011-08-03 01:18:26
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to programmatically embed media files into a Microsoft PowerPoint 2010 presentation. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff601857.aspx">Working with Embedded Media in PowerPoint 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>By using Microsoft PowerPoint 2010, you can effectively engage your audience by embedding and customizing videos within your presentation. This sample shows how to use the PowerPoint 2010 Primary Interop Assembly (PIA) to programmatically create a new PowerPoint
 2010 presentation, add a new slide to the presentation, embed a video into the new slide, and format how the video will be displayed.</p>
<p>The following excerpt from the code sample shows how to use the PowerPoint 2010 PIA to embed a video into a slide and format how it will be displayed. The code embeds the video by using the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.shapes.addmediaobject2.aspx">
AddMediaObject2</a> method. The <em>FileName</em> parameter is the name of the video file to embed. The
<em>LinkToFile</em> parameter is set to <strong>false</strong>, and the <em>SaveWithDocument</em> parameter is set to
<strong>true</strong> to embed the file within the presentation. The <em>Left</em> and
<em>Top</em> parameters are used to define the location of the upper-left corner for the video display area. Named arguments are used because all of the parameters are not passed to the
<strong>AddMediaObject2</strong> method. For more information about named arguments, see
<a href="http://msdn.microsoft.com/en-us/library/dd264739.aspx">Named and Optional Arguments</a>.</p>
<p>The video file is then reset to its original size by calling the <a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.shape.scaleheight.aspx">
ScaleHeight</a> and <a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.shape.scalewidth.aspx">
ScaleWidth</a> methods. The first parameter is the factor to use when resizing, and the second parameter defines that resizing should be based on the original size.</p>
<p>The video file is then formatted to be displayed with a beveled edge by using the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.shape.threed.aspx">
ThreeD</a>.<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.threedformat.beveltopdepth.aspx">BevelTopDepth</a>,
<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.threedformat.beveltopinset.aspx">
BevelTopInset</a>, and <a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.threedformat.bevelbottomtype.aspx">
BevelBottomType</a> properties. The video file is also formatted to display in a rounded rectangle shape by setting the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.powerpoint.shape.autoshapetype.aspx">
AutoShapeType</a> property to the <strong>msoAutoShapeType.msoShapeRoundedRectangle</strong> value.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// Embed the video file into the slide.
mediaFile = slide.Shapes.AddMediaObject2(
    FileName: mediaFileName,
    LinkToFile: MsoTriState.msoFalse,
    SaveWithDocument: MsoTriState.msoCTrue,
    Left: 90f,
    Top: 114f);

// Resize the video to its original size.
mediaFile.ScaleHeight(1f, MsoTriState.msoCTrue);
mediaFile.ScaleWidth(1f, MsoTriState.msoCTrue);

// Format to have a beveled edge.
mediaFile.ThreeD.BevelTopDepth = 8;
mediaFile.ThreeD.BevelTopInset = 6;
mediaFile.ThreeD.BevelBottomType = 
    MsoBevelType.msoBevelSoftRound;

// Format to display in a rounded rectangle shape.
mediaFile.AutoShapeType = 
    MsoAutoShapeType.msoShapeRoundedRectangle;
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Embed&nbsp;the&nbsp;video&nbsp;file&nbsp;into&nbsp;the&nbsp;slide.</span>&nbsp;
mediaFile&nbsp;=&nbsp;slide.Shapes.AddMediaObject2(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FileName:&nbsp;mediaFileName,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;LinkToFile:&nbsp;MsoTriState.msoFalse,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SaveWithDocument:&nbsp;MsoTriState.msoCTrue,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Left:&nbsp;90f,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Top:&nbsp;114f);&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Resize&nbsp;the&nbsp;video&nbsp;to&nbsp;its&nbsp;original&nbsp;size.</span>&nbsp;
mediaFile.ScaleHeight(1f,&nbsp;MsoTriState.msoCTrue);&nbsp;
mediaFile.ScaleWidth(1f,&nbsp;MsoTriState.msoCTrue);&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Format&nbsp;to&nbsp;have&nbsp;a&nbsp;beveled&nbsp;edge.</span>&nbsp;
mediaFile.ThreeD.BevelTopDepth&nbsp;=&nbsp;<span class="cs__number">8</span>;&nbsp;
mediaFile.ThreeD.BevelTopInset&nbsp;=&nbsp;<span class="cs__number">6</span>;&nbsp;
mediaFile.ThreeD.BevelBottomType&nbsp;=&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MsoBevelType.msoBevelSoftRound;&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Format&nbsp;to&nbsp;display&nbsp;in&nbsp;a&nbsp;rounded&nbsp;rectangle&nbsp;shape.</span>&nbsp;
mediaFile.AutoShapeType&nbsp;=&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MsoAutoShapeType.msoShapeRoundedRectangle;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>There are two attachments to this description:</p>
<ul>
<li><a id="25932" href="/site/view/file/25932/1/Butterfly.wmv">Butterfly.wmv</a>&mdash;A video file that you can use with this sample.
</li><li>
<p><a id="25934" href="/site/view/file/25934/1/PP2010EmbeddedVideo.pptx">PP2010EmbeddedVideo.pptx</a>&mdash;A sample PowerPoint presentation that contains the embedded video.</p>
</li></ul>
