# PowerPoint 2010: List SmartArt Names Using PPT.WorkWithSmartArt
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
* 2011-08-05 04:05:56
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to list all of the index values and SmartArt names in Microsoft PowerPoint 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Unfortunately, there's no simple way to map from an index in the collection of smart art layouts to the names of the layouts (which you can see in the Insert | SmartArt ribbon item's list of smart art layouts. To get around
 this, place your cursor into the CreateSmartArtList procedure, and press F5. This procedure lists all the index values and smart art names into the Debug window. Copy this list from the Debug window to a text file, and use it for reference.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub CreateSmartArtList()
    Dim i As Integer
    For i = 1 To Application.SmartArtLayouts.Count
        Debug.Print i, Application.SmartArtLayouts(i).Name
    Next i
End Sub


' Place the cursor in the following procedure, and press F8 (and then Shift&#43;F8)
' to single step through the code (press Shift&#43;F8 to step over the
' calls to the CreateSmartArt procedure, unless you want to step
' through it several times. Go through it at least once to see the
' AddSmartArt method at work.

' Once you're done, note the shapes in the first slide, and then look in the
' Immediate window to see the output results.
Sub DemonstrateSmartArt()
    With ActivePresentation.Slides(1)
        Dim i As Integer
        ' Clear out existing shapes:
        For i = .Shapes.Count To 1 Step -1
            .Shapes(i).Delete
        Next i
       
        Dim shp As Shape
       
        Call CreateSmartArt(1, 10, 10, 100, 100)
        Call .Shapes.AddShape(msoShape12pointStar, 10, 120, 100, 100)
        Call CreateSmartArt(4, 120, 10, 100, 100)
        Call .Shapes.AddShape(msoShapeCloud, 120, 120, 100, 100)
        Call CreateSmartArt(13, 240, 10, 400, 100)
       
        For Each shp In .Shapes
            Debug.Print &quot;Shape &quot; &amp; i &amp; &quot; has smart art: &quot; &amp; shp.HasSmartArt
        Next shp
       
        ' We know that shape 1 has smart art--copy it to a new shape.
        Call .Shapes.AddSmartArt(.Shapes(1).SmartArt.Layout, 360, 120, 100, 100)
       
    End With
End Sub

Sub CreateSmartArt(smartArtLayoutID As Integer, left As Integer, top As Integer, _
  width As Integer, height As Integer)
   
    ' Create Basic Blocks SmartArt
    Dim shp As Shape
    Set shp = ActivePresentation.Slides(1).Shapes. _
      AddSmartArt(Application.SmartArtLayouts(smartArtLayoutID), left, top)
    shp.width = width
    shp.height = height
    Dim i As Integer
    With shp.SmartArt
        ' Add some nodes, then set the text for all of them.
        .Nodes.Add
        .Nodes.Add
        .Nodes.Add
        .Nodes.Add
        For i = 1 To .Nodes.Count
            .Nodes(i).TextFrame2.TextRange.Text = &quot;Cell &quot; &amp; i
        Next i
    End With
    Set CreateBasicBlocks = shp
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;CreateSmartArtList()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;Application.SmartArtLayouts.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;i,&nbsp;Application.SmartArtLayouts(i).Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;Place&nbsp;the&nbsp;cursor&nbsp;in&nbsp;the&nbsp;following&nbsp;procedure,&nbsp;and&nbsp;press&nbsp;F8&nbsp;(and&nbsp;then&nbsp;Shift&#43;F8)</span>&nbsp;
<span class="visualBasic__com">'&nbsp;to&nbsp;single&nbsp;step&nbsp;through&nbsp;the&nbsp;code&nbsp;(press&nbsp;Shift&#43;F8&nbsp;to&nbsp;step&nbsp;over&nbsp;the</span>&nbsp;
<span class="visualBasic__com">'&nbsp;calls&nbsp;to&nbsp;the&nbsp;CreateSmartArt&nbsp;procedure,&nbsp;unless&nbsp;you&nbsp;want&nbsp;to&nbsp;step</span>&nbsp;
<span class="visualBasic__com">'&nbsp;through&nbsp;it&nbsp;several&nbsp;times.&nbsp;Go&nbsp;through&nbsp;it&nbsp;at&nbsp;least&nbsp;once&nbsp;to&nbsp;see&nbsp;the</span>&nbsp;
<span class="visualBasic__com">'&nbsp;AddSmartArt&nbsp;method&nbsp;at&nbsp;work.</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;Once&nbsp;you're&nbsp;done,&nbsp;note&nbsp;the&nbsp;shapes&nbsp;in&nbsp;the&nbsp;first&nbsp;slide,&nbsp;and&nbsp;then&nbsp;look&nbsp;in&nbsp;the</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Immediate&nbsp;window&nbsp;to&nbsp;see&nbsp;the&nbsp;output&nbsp;results.</span>&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;DemonstrateSmartArt()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Clear&nbsp;out&nbsp;existing&nbsp;shapes:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;.Shapes.Count&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Step</span>&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Shapes(i).Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Call</span>&nbsp;CreateSmartArt(<span class="visualBasic__number">1</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Call</span>&nbsp;.Shapes.AddShape(msoShape12pointStar,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">120</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Call</span>&nbsp;CreateSmartArt(<span class="visualBasic__number">4</span>,&nbsp;<span class="visualBasic__number">120</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Call</span>&nbsp;.Shapes.AddShape(msoShapeCloud,&nbsp;<span class="visualBasic__number">120</span>,&nbsp;<span class="visualBasic__number">120</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Call</span>&nbsp;CreateSmartArt(<span class="visualBasic__number">13</span>,&nbsp;<span class="visualBasic__number">240</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">400</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;.Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Shape&nbsp;&quot;</span>&nbsp;&amp;&nbsp;i&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;has&nbsp;smart&nbsp;art:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.HasSmartArt&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;We&nbsp;know&nbsp;that&nbsp;shape&nbsp;1&nbsp;has&nbsp;smart&nbsp;art--copy&nbsp;it&nbsp;to&nbsp;a&nbsp;new&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Call</span>&nbsp;.Shapes.AddSmartArt(.Shapes(<span class="visualBasic__number">1</span>).SmartArt.Layout,&nbsp;<span class="visualBasic__number">360</span>,&nbsp;<span class="visualBasic__number">120</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;CreateSmartArt(smartArtLayoutID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;left&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;top&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;width&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;height&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;Basic&nbsp;Blocks&nbsp;SmartArt</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes.&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AddSmartArt(Application.SmartArtLayouts(smartArtLayoutID),&nbsp;left,&nbsp;top)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.width&nbsp;=&nbsp;width&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.height&nbsp;=&nbsp;height&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.SmartArt&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;some&nbsp;nodes,&nbsp;then&nbsp;set&nbsp;the&nbsp;text&nbsp;for&nbsp;all&nbsp;of&nbsp;them.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Nodes.Add&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Nodes.Add&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Nodes.Add&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Nodes.Add&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;.Nodes.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Nodes(i).TextFrame2.TextRange.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Cell&nbsp;&quot;</span>&nbsp;&amp;&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;CreateBasicBlocks&nbsp;=&nbsp;shp&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26193" href="/site/view/file/26193/1/PPT.WorkWithSmartArt.txt">PPT.WorkWithSmartArt.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26194" href="/site/view/file/26194/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
