# PowerPoint 2010: Convert Text into SmartArt Using PPT.ConvertTextToSmartArt
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* SmartArt
## IsPublished
* True
## ModifiedDate
* 2011-08-05 12:33:16
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to convert text programmatically into different built-in SmartArt layouts and how to create a list of all of the index values and smart art names in Microsoft PowerPoint 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">PowerPoint 2010 supports converting text programmatically into one of the many built-in smart art layouts. Unfortunately, there's no simple way to map from an index in the collection of smart art layouts to the names of the
 layouts (which you can see in the Insert | SmartArt ribbon item's list of smart art layouts. To get around this, place your cursor int the CreateSmartArtList procedure, and press F5. This procedure lists all the index values and smart art names into the Debug
 window. Copy this list from the Debug window to a text file, and use it for reference. Then, in the ribbon, you can locate the smart art layout by name, and convert it to its matching index in the list you just created.<br>
&nbsp;&nbsp;&nbsp;<br>
This sample procedure creates a new slide, then inserts text into a shape, and finally converts the text into a smart art layout. This code assumes that you have pasted this sample code into a PowerPoint presentation.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ConvertToSmartArtLayoutTest()
  
    Dim sld As Slide
    Set sld = ActivePresentation.Slides.Add(1, ppLayoutText)
    Dim shp As Shape
    Set shp = sld.Shapes(2)
   
    ' Create text with Main at the top level, and
    ' NW, NE, SW, SE as the four child paragraphs.
    Dim sampleText As String
    sampleText = &quot;Main.NW.NE.SW.SE&quot;
    sampleText = Replace(text, &quot;.&quot;, vbCrLf)
   
    ' Insert the text into the shape:
    With shp.TextFrame.TextRange.Paragraphs
        .Text = sampleText
        .Lines(2).IndentLevel = 2
        .Lines(3).IndentLevel = 2
        .Lines(4).IndentLevel = 2
        .Lines(5).IndentLevel = 2
    End With
   
    ' Now that the shape includes text that is appropriate
    ' for the smart art layout you'll use, convert it to
    ' the appropriate layout (121, Titled Matrix):
    shp.ConvertTextToSmartArt Application.SmartArtLayouts(121)
End Sub

Sub CreateSmartArtList()
    Dim i As Integer
    For i = 1 To Application.SmartArtLayouts.Count
        Debug.Print i, Application.SmartArtLayouts(i).Name
    Next i
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ConvertToSmartArtLayoutTest()&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides.Add(<span class="visualBasic__number">1</span>,&nbsp;ppLayoutText)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes(<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;text&nbsp;with&nbsp;Main&nbsp;at&nbsp;the&nbsp;top&nbsp;level,&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;NW,&nbsp;NE,&nbsp;SW,&nbsp;SE&nbsp;as&nbsp;the&nbsp;four&nbsp;child&nbsp;paragraphs.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sampleText&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sampleText&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Main.NW.NE.SW.SE&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sampleText&nbsp;=&nbsp;Replace(text,&nbsp;<span class="visualBasic__string">&quot;.&quot;</span>,&nbsp;vbCrLf)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Insert&nbsp;the&nbsp;text&nbsp;into&nbsp;the&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.TextFrame.TextRange.Paragraphs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Text&nbsp;=&nbsp;sampleText&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Lines(<span class="visualBasic__number">2</span>).IndentLevel&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Lines(<span class="visualBasic__number">3</span>).IndentLevel&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Lines(<span class="visualBasic__number">4</span>).IndentLevel&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Lines(<span class="visualBasic__number">5</span>).IndentLevel&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;that&nbsp;the&nbsp;shape&nbsp;includes&nbsp;text&nbsp;that&nbsp;is&nbsp;appropriate</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;the&nbsp;smart&nbsp;art&nbsp;layout&nbsp;you'll&nbsp;use,&nbsp;convert&nbsp;it&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;appropriate&nbsp;layout&nbsp;(121,&nbsp;Titled&nbsp;Matrix):</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.ConvertTextToSmartArt&nbsp;Application.SmartArtLayouts(<span class="visualBasic__number">121</span>)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;CreateSmartArtList()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;Application.SmartArtLayouts.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;i,&nbsp;Application.SmartArtLayouts(i).Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26146" href="/site/view/file/26146/1/PPT.ConvertTextToSmartArt.txt">PPT.ConvertTextToSmartArt.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26147" href="/site/view/file/26147/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
