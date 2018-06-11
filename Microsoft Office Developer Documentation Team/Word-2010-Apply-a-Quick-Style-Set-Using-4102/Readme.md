# Word 2010: Apply a Quick Style Set Using Word.QuickStyleSets
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Quick styles
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:57:09
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to apply a quick style set in Microsoft Word 2010.</span></p>
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
<span style="font-size:small">This action inserts 5 paragraphs with 5 sentences each into the current document. Then, in the VBA editor, create a new module, copy in this code, and place the cursor within this procedure and press F8 to single step through the
 code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:small">Note that quick style sets are stored as .dotx files in this folder in Windows 7: C:\Users\&lt;userName&gt;\AppData\Roaming\Microsoft\QuickStyles</span><br>
<span style="font-size:small">Delete files from this folder to remove them from the list of quick styles. The location will be different in other operating systems, and in non-default installations of Office 2010.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestQuickStyles()
    ' Word 2010 adds the ability to apply a quick style set. Previously
    ' you could only save a quick style set.
   
    ' Apply one of the built-in style sets.
    ActiveDocument.ApplyQuickStyleSet2 &quot;Elegant&quot;
   
    ' Modify the style set:
    With ActiveDocument.Paragraphs(1)
        .LineSpacing = 20
        .LeftIndent = 20
    End With
    ActiveDocument.SaveAsQuickStyleSet &quot;New Style&quot;
   
    ' Load another style set:
    ActiveDocument.ApplyQuickStyleSet2 &quot;Word 2010&quot;
   
    ' Re-apply your new style set:
    ActiveDocument.ApplyQuickStyleSet2 &quot;New Style&quot;
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestQuickStyles()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Word&nbsp;2010&nbsp;adds&nbsp;the&nbsp;ability&nbsp;to&nbsp;apply&nbsp;a&nbsp;quick&nbsp;style&nbsp;set.&nbsp;Previously</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;could&nbsp;only&nbsp;save&nbsp;a&nbsp;quick&nbsp;style&nbsp;set.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;one&nbsp;of&nbsp;the&nbsp;built-in&nbsp;style&nbsp;sets.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyQuickStyleSet2&nbsp;<span class="visualBasic__string">&quot;Elegant&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;the&nbsp;style&nbsp;set:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActiveDocument.Paragraphs(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LineSpacing&nbsp;=&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LeftIndent&nbsp;=&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.SaveAsQuickStyleSet&nbsp;<span class="visualBasic__string">&quot;New&nbsp;Style&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Load&nbsp;another&nbsp;style&nbsp;set:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyQuickStyleSet2&nbsp;<span class="visualBasic__string">&quot;Word&nbsp;2010&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Re-apply&nbsp;your&nbsp;new&nbsp;style&nbsp;set:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyQuickStyleSet2&nbsp;<span class="visualBasic__string">&quot;New&nbsp;Style&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26240" href="/site/view/file/26240/1/Word.QuickStyleSets.txt">Word.QuickStyleSets.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26241" href="/site/view/file/26241/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
