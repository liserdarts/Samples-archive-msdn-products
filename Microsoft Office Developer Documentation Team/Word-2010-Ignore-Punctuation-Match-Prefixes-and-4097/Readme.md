# Word 2010: Ignore Punctuation, Match Prefixes and Suffixes, Clear Highlighting
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Find object
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:35:05
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates some of the features of the
<strong>Find </strong>object in Microsoft Word 2010 such as ignore punctuation, ignore spaces, match prefixes and suffixes, and clear highlighting.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Demonstrate some of the features new to the Find object starting in Word 2007:</span><br>
<span style="font-size:small">- IgnorePunct</span><br>
<span style="font-size:small">- IgnoreSpace</span><br>
<span style="font-size:small">- MatchPrefix</span><br>
<span style="font-size:small">-&nbsp;MatchSuffix</span><br>
<span style="font-size:small">-&nbsp;ClearHitHighlight</span><br>
<span style="font-size:small">-&nbsp;HitHighlight</span></p>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:</span><br>
<br>
<span style="font-size:small">=rand(5, 5)</span><br>
<br>
<span style="font-size:small">This action inserts 5 paragraphs with 5 sentences each into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within this procedure and press F8 to single step through
 the code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub DemoFind()
    ' Set up a search, in the random text, for
    ' &quot;tab Most&quot;
    Dim fnd As Find
    Set fnd = Content.Find
    fnd.Text = &quot;tab Most&quot;
    ' Ignore punctuation and white space. In the document,
    ' the text appears as &quot;tab. Most&quot;. This will still find
    ' a match.
    fnd.IgnorePunct = True
    fnd.IgnoreSpace = True
   
    ' Highlight the found text.
    fnd.HitHighlight fnd.Text, vbYellow, vbRed
   
    ' Now clear the highlighting. This is only meaningful
    ' if you are single-stepping through the code.
    fnd.ClearHitHighlight
       
    ' Match the text &quot;th&quot; only when it appears at the beginning
    ' of a word:
    fnd.MatchPrefix = True
    fnd.Text = &quot;th&quot;
    fnd.HitHighlight fnd.Text, vbYellow, vbRed
    ' Now clear the highlighting. This is only meaningful
    ' if you are single-stepping through the code.
    fnd.ClearHitHighlight

    ' Match the text &quot;th&quot; only when it appears at the end
    ' of a word:
    fnd.MatchPrefix = False
    fnd.MatchSuffix = True
    fnd.Text = &quot;th&quot;
    fnd.HitHighlight fnd.Text, vbYellow, vbRed
    ' Now clear the highlighting. This is only meaningful
    ' if you are single-stepping through the code.
    fnd.ClearHitHighlight
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;DemoFind()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;a&nbsp;search,&nbsp;in&nbsp;the&nbsp;random&nbsp;text,&nbsp;for</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&quot;tab&nbsp;Most&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;fnd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Find&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;fnd&nbsp;=&nbsp;Content.Find&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;tab&nbsp;Most&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Ignore&nbsp;punctuation&nbsp;and&nbsp;white&nbsp;space.&nbsp;In&nbsp;the&nbsp;document,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;text&nbsp;appears&nbsp;as&nbsp;&quot;tab.&nbsp;Most&quot;.&nbsp;This&nbsp;will&nbsp;still&nbsp;find</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;match.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.IgnorePunct&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.IgnoreSpace&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Highlight&nbsp;the&nbsp;found&nbsp;text.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.HitHighlight&nbsp;fnd.Text,&nbsp;vbYellow,&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;clear&nbsp;the&nbsp;highlighting.&nbsp;This&nbsp;is&nbsp;only&nbsp;meaningful</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;if&nbsp;you&nbsp;are&nbsp;single-stepping&nbsp;through&nbsp;the&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.ClearHitHighlight&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Match&nbsp;the&nbsp;text&nbsp;&quot;th&quot;&nbsp;only&nbsp;when&nbsp;it&nbsp;appears&nbsp;at&nbsp;the&nbsp;beginning</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;of&nbsp;a&nbsp;word:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.MatchPrefix&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;th&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.HitHighlight&nbsp;fnd.Text,&nbsp;vbYellow,&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;clear&nbsp;the&nbsp;highlighting.&nbsp;This&nbsp;is&nbsp;only&nbsp;meaningful</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;if&nbsp;you&nbsp;are&nbsp;single-stepping&nbsp;through&nbsp;the&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.ClearHitHighlight&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Match&nbsp;the&nbsp;text&nbsp;&quot;th&quot;&nbsp;only&nbsp;when&nbsp;it&nbsp;appears&nbsp;at&nbsp;the&nbsp;end</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;of&nbsp;a&nbsp;word:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.MatchPrefix&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.MatchSuffix&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;th&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.HitHighlight&nbsp;fnd.Text,&nbsp;vbYellow,&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;clear&nbsp;the&nbsp;highlighting.&nbsp;This&nbsp;is&nbsp;only&nbsp;meaningful</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;if&nbsp;you&nbsp;are&nbsp;single-stepping&nbsp;through&nbsp;the&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;fnd.ClearHitHighlight&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26230" href="/site/view/file/26230/1/Word.FindObject.txt">Word.FindObject.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26231" href="/site/view/file/26231/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
