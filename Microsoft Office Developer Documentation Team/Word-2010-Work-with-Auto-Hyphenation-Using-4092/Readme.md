# Word 2010: Work with Auto-Hyphenation Using Word.ConvertAutoHyphens
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* hyphenation
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:11:46
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use auto-hyphenation in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:<br>
<br>
=rand(5, 5)<br>
<br>
This action inserts 5 paragraphs with 5 sentences each into the current document. Then, in the VBA editor, place the cursor within this procedure and press F8 to single step through the code. Arrange the VBA and Word windows side by side on screen so you can
 view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub AutoHyphenDemo()
    ' In a new document, type the following text and then press Enter:
    '
    ' =rand(5, 5)
    '
    ' This action inserts 5 paragraphs with 5 sentences each into the
    ' current document. Then, in the VBA editor, place the cursor within
    ' this procedure and press F8 to single step through the code. Arrange
    ' the VBA and Word windows side by side on screen so you can view the
    ' behavior as you step through the code.
   
    With ActiveDocument
        ' Make the margins a little larger than normal.
        .PageSetup.LeftMargin = 100
        .PageSetup.RightMargin = 100
       
        ' Make the text justified so the hyphens stand out more:
        .Range.ParagraphFormat.Alignment = wdAlignParagraphJustify
       
        ' Turn on auto-hyphenation.
        .AutoHyphenation = True
       
        ' In Word, try to select just a hyphen at the end of a line:
        ' you can't because the hyphens aren't really there. Word has
        ' inserted these automatically. You may want real hyphens in your
        ' text, and the following line of code converts automatic
        ' hyphens into real hyphens:
       
        .ConvertAutoHyphens
       
        ' Note that you can now select a hyphen, as if you had inserted
        ' it yourself. Make a mental note where one of the hyphens is,
        ' and the next block of code changes the margins. Note the
        ' behavior.
        .PageSetup.LeftMargin = 15
        .PageSetup.RightMargin = 15
   
        ' Show all content in the window, and find the hyphens, which now
        ' may not appear as hyphens. They're still there!
        ActiveWindow.ActivePane.View.ShowAll = True
   
        ' Put the margins back the way they were, and notice the hyphen behavior:
        ' your hyphens are back!
        .PageSetup.LeftMargin = 100
        .PageSetup.RightMargin = 100
    End With
   
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;AutoHyphenDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;a&nbsp;new&nbsp;document,&nbsp;type&nbsp;the&nbsp;following&nbsp;text&nbsp;and&nbsp;then&nbsp;press&nbsp;Enter:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;=rand(5,&nbsp;5)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;action&nbsp;inserts&nbsp;5&nbsp;paragraphs&nbsp;with&nbsp;5&nbsp;sentences&nbsp;each&nbsp;into&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;current&nbsp;document.&nbsp;Then,&nbsp;in&nbsp;the&nbsp;VBA&nbsp;editor,&nbsp;place&nbsp;the&nbsp;cursor&nbsp;within</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;this&nbsp;procedure&nbsp;and&nbsp;press&nbsp;F8&nbsp;to&nbsp;single&nbsp;step&nbsp;through&nbsp;the&nbsp;code.&nbsp;Arrange</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;VBA&nbsp;and&nbsp;Word&nbsp;windows&nbsp;side&nbsp;by&nbsp;side&nbsp;on&nbsp;screen&nbsp;so&nbsp;you&nbsp;can&nbsp;view&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;behavior&nbsp;as&nbsp;you&nbsp;step&nbsp;through&nbsp;the&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;the&nbsp;margins&nbsp;a&nbsp;little&nbsp;larger&nbsp;than&nbsp;normal.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PageSetup.LeftMargin&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PageSetup.RightMargin&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;the&nbsp;text&nbsp;justified&nbsp;so&nbsp;the&nbsp;hyphens&nbsp;stand&nbsp;out&nbsp;more:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Range.ParagraphFormat.Alignment&nbsp;=&nbsp;wdAlignParagraphJustify&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Turn&nbsp;on&nbsp;auto-hyphenation.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.AutoHyphenation&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;Word,&nbsp;try&nbsp;to&nbsp;select&nbsp;just&nbsp;a&nbsp;hyphen&nbsp;at&nbsp;the&nbsp;end&nbsp;of&nbsp;a&nbsp;line:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;can't&nbsp;because&nbsp;the&nbsp;hyphens&nbsp;aren't&nbsp;really&nbsp;there.&nbsp;Word&nbsp;has</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;inserted&nbsp;these&nbsp;automatically.&nbsp;You&nbsp;may&nbsp;want&nbsp;real&nbsp;hyphens&nbsp;in&nbsp;your</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;text,&nbsp;and&nbsp;the&nbsp;following&nbsp;line&nbsp;of&nbsp;code&nbsp;converts&nbsp;automatic</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;hyphens&nbsp;into&nbsp;real&nbsp;hyphens:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ConvertAutoHyphens&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;you&nbsp;can&nbsp;now&nbsp;select&nbsp;a&nbsp;hyphen,&nbsp;as&nbsp;if&nbsp;you&nbsp;had&nbsp;inserted</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;it&nbsp;yourself.&nbsp;Make&nbsp;a&nbsp;mental&nbsp;note&nbsp;where&nbsp;one&nbsp;of&nbsp;the&nbsp;hyphens&nbsp;is,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;the&nbsp;next&nbsp;block&nbsp;of&nbsp;code&nbsp;changes&nbsp;the&nbsp;margins.&nbsp;Note&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;behavior.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PageSetup.LeftMargin&nbsp;=&nbsp;<span class="visualBasic__number">15</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PageSetup.RightMargin&nbsp;=&nbsp;<span class="visualBasic__number">15</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Show&nbsp;all&nbsp;content&nbsp;in&nbsp;the&nbsp;window,&nbsp;and&nbsp;find&nbsp;the&nbsp;hyphens,&nbsp;which&nbsp;now</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;may&nbsp;not&nbsp;appear&nbsp;as&nbsp;hyphens.&nbsp;They're&nbsp;still&nbsp;there!</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveWindow.ActivePane.View.ShowAll&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;the&nbsp;margins&nbsp;back&nbsp;the&nbsp;way&nbsp;they&nbsp;were,&nbsp;and&nbsp;notice&nbsp;the&nbsp;hyphen&nbsp;behavior:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;your&nbsp;hyphens&nbsp;are&nbsp;back!</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PageSetup.LeftMargin&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PageSetup.RightMargin&nbsp;=&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26220" href="/site/view/file/26220/1/Word.ConvertAutoHyphens.txt">Word.ConvertAutoHyphens.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26221" href="/site/view/file/26221/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
