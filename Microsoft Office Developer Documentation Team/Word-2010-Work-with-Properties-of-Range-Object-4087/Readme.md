# Word 2010: Work with Properties of Range Object Using Word.CharParagraphStyle
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Range Object
## IsPublished
* True
## ModifiedDate
* 2011-08-05 04:49:27
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>CharacterStyle
</strong>and <strong>ParagraphStyle </strong>properties of the <strong>Range </strong>
object in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:<br>
<br>
=rand(1, 5)<br>
<br>
This action inserts 1 paragraph with 5 sentences each into the current document. Then, in the VBA editor, in the ThisDocument&nbsp;class, copy in this code, and place the cursor within the RangeCharacterParagraphStyle procedure and press F8 to single step through
 the code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub RangeCharacterParagraphStyle()
    Dim rng As Range
   
    ' Select a character range that covers the first four words,
    ' using the sample random text:
    Set rng = ActiveDocument.Range
   
    rng.Words(3).Style = &quot;Book Title&quot;
    rng.Words(4).Style = &quot;Emphasis&quot;
    rng.Paragraphs(1).Style = &quot;Heading 3&quot;
   
    Dim charStyle As Style
   
    ' Note that attempting to retrieve the CharacterStyle
    ' property of a range that includes multiple character
    ' styles returns Nothing:
    Set charStyle = rng.CharacterStyle
    ' This should display True in the Immediate window:
    Debug.Print charStyle Is Nothing
   
    ' This should display &quot;Book Title&quot; in the Immediate window:
    Set charStyle = rng.Words(3).CharacterStyle
    Debug.Print charStyle.NameLocal
   
    ' This should display &quot;Emphasis&quot; in the Immediate window:
    Set charStyle = rng.Words(4).CharacterStyle
    Debug.Print charStyle.NameLocal
   
   
    Dim paraStyle As Style
   
    ' Note that attempting to retrieve the ParagraphStyle
    ' property of a range that includes multiple paragraph
    ' styles returns Nothing:
    Set paraStyle = rng.ParagraphStyle
   
    ' This should display True in the Immediate window:
    Debug.Print paraStyle Is Nothing

    ' This should print Heading 3 in the Immediate window.    
    Set paraStyle = rng.Paragraphs(1).Range.ParagraphStyle
    Debug.Print paraStyle.NameLocal
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;RangeCharacterParagraphStyle()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Select&nbsp;a&nbsp;character&nbsp;range&nbsp;that&nbsp;covers&nbsp;the&nbsp;first&nbsp;four&nbsp;words,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;using&nbsp;the&nbsp;sample&nbsp;random&nbsp;text:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;ActiveDocument.Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Words(<span class="visualBasic__number">3</span>).Style&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Book&nbsp;Title&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Words(<span class="visualBasic__number">4</span>).Style&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Emphasis&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Paragraphs(<span class="visualBasic__number">1</span>).Style&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Heading&nbsp;3&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;charStyle&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Style&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;attempting&nbsp;to&nbsp;retrieve&nbsp;the&nbsp;CharacterStyle</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;property&nbsp;of&nbsp;a&nbsp;range&nbsp;that&nbsp;includes&nbsp;multiple&nbsp;character</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;styles&nbsp;returns&nbsp;Nothing:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;charStyle&nbsp;=&nbsp;rng.CharacterStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;should&nbsp;display&nbsp;True&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;charStyle&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;should&nbsp;display&nbsp;&quot;Book&nbsp;Title&quot;&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;charStyle&nbsp;=&nbsp;rng.Words(<span class="visualBasic__number">3</span>).CharacterStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;charStyle.NameLocal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;should&nbsp;display&nbsp;&quot;Emphasis&quot;&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;charStyle&nbsp;=&nbsp;rng.Words(<span class="visualBasic__number">4</span>).CharacterStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;charStyle.NameLocal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;paraStyle&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Style&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;attempting&nbsp;to&nbsp;retrieve&nbsp;the&nbsp;ParagraphStyle</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;property&nbsp;of&nbsp;a&nbsp;range&nbsp;that&nbsp;includes&nbsp;multiple&nbsp;paragraph</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;styles&nbsp;returns&nbsp;Nothing:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;paraStyle&nbsp;=&nbsp;rng.ParagraphStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;should&nbsp;display&nbsp;True&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;paraStyle&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;should&nbsp;print&nbsp;Heading&nbsp;3&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window.&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;paraStyle&nbsp;=&nbsp;rng.Paragraphs(<span class="visualBasic__number">1</span>).Range.ParagraphStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;paraStyle.NameLocal&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26210" href="/site/view/file/26210/1/Word.CharParagraphStyle.txt">Word.CharParagraphStyle.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26211" href="/site/view/file/26211/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
