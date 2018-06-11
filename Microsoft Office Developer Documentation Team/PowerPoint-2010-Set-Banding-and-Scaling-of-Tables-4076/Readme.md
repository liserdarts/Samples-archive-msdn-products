# PowerPoint 2010: Set Banding and Scaling of Tables Using PPT.TableProperties
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
* 2011-08-05 03:43:27
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use various settings to include banding and scaling of a table in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy this code into a module in a new presentation. Display the VBA window side-by-side with the PowerPoint window, place the cursor into the TableProperties procedure, and press F8 (and then Shift&#43;F8) to single step through
 this code for the most effective use of this demonstration.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TableProperties()
    Dim pres As Presentation
    Set pres = ActivePresentation
   
    Dim sld As Slide
    Set sld = pres.Slides.Add(2, ppLayoutTable)
    sld.Select
    Dim tbl As Table
    Set tbl = sld.Shapes.AddTable(4, 4).Table
    FillTable tbl
   
    ' Variables to maintain the state:
    Dim savedFirstRow As Boolean
    Dim savedFirstCol As Boolean
    Dim savedHorizBanding As Boolean
    Dim savedLastCol As Boolean
    Dim savedLastRow As Boolean
    Dim savedVertBanding As Boolean
   
    ' Store the current state:
    savedFirstRow = tbl.FirstRow
    savedFirstCol = tbl.FirstCol
    savedHorizBanding = tbl.HorizBanding
    savedLastCol = tbl.LastCol
    savedLastRow = tbl.LastRow
    savedVertBanding = tbl.VertBanding
   
    ' Alter all settings:
    tbl.HorizBanding = Not savedHorizBanding
    tbl.VertBanding = Not savedVertBanding
    tbl.FirstRow = Not savedFirstRow
    tbl.FirstCol = Not savedFirstCol
    tbl.LastCol = Not savedLastCol
    tbl.LastRow = Not savedLastRow
   
    ' Scale the table to half size:
    tbl.ScaleProportionally 0.5
   
    ' Put it back the way it was (twice as big as it is currently):
    tbl.ScaleProportionally 2

    ' Put things back the way they were:
    tbl.HorizBanding = savedHorizBanding
    tbl.VertBanding = savedVertBanding
    tbl.FirstRow = savedFirstRow
    tbl.FirstCol = savedFirstCol
    tbl.LastCol = savedLastCol
    tbl.LastRow = savedLastRow
   
End Sub

Sub FillTable(tbl As Table)
    ' Fill a table with sample data.
    Dim row As Integer
    Dim col As Integer
   
    For col = 1 To tbl.Columns.Count
        tbl.Cell(1, col).Shape.TextFrame.TextRange.Text = &quot;Heading &quot; &amp; col
    Next col
   
    For row = 2 To tbl.Rows.Count
        For col = 1 To tbl.Columns.Count
            tbl.Cell(row, col).Shape.TextFrame.TextRange.Text = &quot;Cell &quot; &amp; row &amp; &quot;, &quot; &amp; col
        Next col
    Next row
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TableProperties()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pres&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Presentation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pres&nbsp;=&nbsp;ActivePresentation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;pres.Slides.Add(<span class="visualBasic__number">2</span>,&nbsp;ppLayoutTable)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;tbl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Table&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;tbl&nbsp;=&nbsp;sld.Shapes.AddTable(<span class="visualBasic__number">4</span>,&nbsp;<span class="visualBasic__number">4</span>).Table&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FillTable&nbsp;tbl&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Variables&nbsp;to&nbsp;maintain&nbsp;the&nbsp;state:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;savedFirstRow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;savedFirstCol&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;savedHorizBanding&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;savedLastCol&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;savedLastRow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;savedVertBanding&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Store&nbsp;the&nbsp;current&nbsp;state:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;savedFirstRow&nbsp;=&nbsp;tbl.FirstRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;savedFirstCol&nbsp;=&nbsp;tbl.FirstCol&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;savedHorizBanding&nbsp;=&nbsp;tbl.HorizBanding&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;savedLastCol&nbsp;=&nbsp;tbl.LastCol&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;savedLastRow&nbsp;=&nbsp;tbl.LastRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;savedVertBanding&nbsp;=&nbsp;tbl.VertBanding&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Alter&nbsp;all&nbsp;settings:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.HorizBanding&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;savedHorizBanding&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.VertBanding&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;savedVertBanding&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.FirstRow&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;savedFirstRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.FirstCol&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;savedFirstCol&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.LastCol&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;savedLastCol&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.LastRow&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;savedLastRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Scale&nbsp;the&nbsp;table&nbsp;to&nbsp;half&nbsp;size:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.ScaleProportionally&nbsp;<span class="visualBasic__number">0.5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;it&nbsp;back&nbsp;the&nbsp;way&nbsp;it&nbsp;was&nbsp;(twice&nbsp;as&nbsp;big&nbsp;as&nbsp;it&nbsp;is&nbsp;currently):</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.ScaleProportionally&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;things&nbsp;back&nbsp;the&nbsp;way&nbsp;they&nbsp;were:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.HorizBanding&nbsp;=&nbsp;savedHorizBanding&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.VertBanding&nbsp;=&nbsp;savedVertBanding&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.FirstRow&nbsp;=&nbsp;savedFirstRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.FirstCol&nbsp;=&nbsp;savedFirstCol&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.LastCol&nbsp;=&nbsp;savedLastCol&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.LastRow&nbsp;=&nbsp;savedLastRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;FillTable(tbl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Table)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;a&nbsp;table&nbsp;with&nbsp;sample&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;row&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;col&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;col&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;tbl.Columns.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Cell(<span class="visualBasic__number">1</span>,&nbsp;col).Shape.TextFrame.TextRange.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Heading&nbsp;&quot;</span>&nbsp;&amp;&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;row&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;tbl.Rows.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;col&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;tbl.Columns.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Cell(row,&nbsp;col).Shape.TextFrame.TextRange.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Cell&nbsp;&quot;</span>&nbsp;&amp;&nbsp;row&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;row&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26179" href="/site/view/file/26179/1/PPT.TableProperties.txt">PPT.TableProperties.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26180" href="/site/view/file/26180/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
