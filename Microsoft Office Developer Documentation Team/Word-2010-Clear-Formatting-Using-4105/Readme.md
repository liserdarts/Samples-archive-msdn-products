# Word 2010: Clear Formatting Using Word.SelectionClearFormatting
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Selection object
## IsPublished
* True
## ModifiedDate
* 2011-08-05 06:09:35
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates the <strong>ClearCharacter
</strong>and <strong>ClearParagraph </strong>methods of the <strong>Selection </strong>
object in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:</span><br>
<br>
<span style="font-size:small">=rand(1, 5)</span><br>
<br>
<span style="font-size:small">This action inserts 1 paragraph with 5 sentences into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within the SelectionClearFormattingDemo procedure and press
 F8 and then Shift&#43;F8 to single step through the code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub SelectionClearFormattingDemo()
    ' Apply character and paragraph formats.
    ' Press F8 to step into this the first time.
    ' Press Shift&#43;F8 to step over subsequent lines of code:
    ApplyFormattingAndSelect Sentences(3)
   
    ' Note that the sentence is now formatted.
    ' Remove the character direct formatting;
    Selection.ClearCharacterDirectFormatting
       
    ' Reapply the formatting:
    ApplyFormattingAndSelect Sentences(3)

    ' Remove the character style formatting:
    Selection.ClearCharacterStyle

    ' Reapply the formatting:
    ApplyFormattingAndSelect Sentences(3)
   
    ' Remove all character formatting (leaving paragraph formatting):
    Selection.ClearCharacterAllFormatting
   
    ' Reapply the formatting:
    ApplyFormattingAndSelect Sentences(3)
   
    ' Remove the paragraph direct formatting:
    Selection.ClearParagraphDirectFormatting
   
    ' Reapply the formatting:
    ApplyFormattingAndSelect Sentences(3)
   
    ' Remove the paragraph style formatting:
    Selection.ClearParagraphStyle
   
    ' Reapply the formatting:
    ApplyFormattingAndSelect Sentences(3)
   
    ' Remove all paragraph formatting (leaving character formatting):
    Selection.ClearParagraphAllFormatting
   
End Sub

Sub ApplyFormattingAndSelect(rng As Range)
    With rng
        ' Apply a paragraph and character style:
        .Style = &quot;Quote&quot;
       
        ' Apply a character style:
        .Style = &quot;Subtle Reference&quot;
       
        ' Apply direct formatting:
        .Font.Bold = True
        .Font.ColorIndex = wdBrightGreen
        .ParagraphFormat.LineSpacing = 20
        .Select
    End With
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;SelectionClearFormattingDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;character&nbsp;and&nbsp;paragraph&nbsp;formats.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Press&nbsp;F8&nbsp;to&nbsp;step&nbsp;into&nbsp;this&nbsp;the&nbsp;first&nbsp;time.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Press&nbsp;Shift&#43;F8&nbsp;to&nbsp;step&nbsp;over&nbsp;subsequent&nbsp;lines&nbsp;of&nbsp;code:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ApplyFormattingAndSelect&nbsp;Sentences(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;the&nbsp;sentence&nbsp;is&nbsp;now&nbsp;formatted.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;the&nbsp;character&nbsp;direct&nbsp;formatting;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Selection.ClearCharacterDirectFormatting&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reapply&nbsp;the&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ApplyFormattingAndSelect&nbsp;Sentences(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;the&nbsp;character&nbsp;style&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Selection.ClearCharacterStyle&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reapply&nbsp;the&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ApplyFormattingAndSelect&nbsp;Sentences(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;all&nbsp;character&nbsp;formatting&nbsp;(leaving&nbsp;paragraph&nbsp;formatting):</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Selection.ClearCharacterAllFormatting&nbsp;

&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reapply&nbsp;the&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ApplyFormattingAndSelect&nbsp;Sentences(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;the&nbsp;paragraph&nbsp;direct&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Selection.ClearParagraphDirectFormatting&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reapply&nbsp;the&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ApplyFormattingAndSelect&nbsp;Sentences(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;the&nbsp;paragraph&nbsp;style&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Selection.ClearParagraphStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reapply&nbsp;the&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ApplyFormattingAndSelect&nbsp;Sentences(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;all&nbsp;paragraph&nbsp;formatting&nbsp;(leaving&nbsp;character&nbsp;formatting):</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Selection.ClearParagraphAllFormatting&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;ApplyFormattingAndSelect(rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;rng&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;a&nbsp;paragraph&nbsp;and&nbsp;character&nbsp;style:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Quote&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;a&nbsp;character&nbsp;style:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Subtle&nbsp;Reference&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;direct&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Font.Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Font.ColorIndex&nbsp;=&nbsp;wdBrightGreen&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ParagraphFormat.LineSpacing&nbsp;=&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26246" href="/site/view/file/26246/1/Word.SelectionClearFormatting.txt">Word.SelectionClearFormatting.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26247" href="/site/view/file/26247/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
