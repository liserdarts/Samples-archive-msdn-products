# Word 2010: Apply Themes and Styles Using Word.DocumentApplyThemeQuickStyle
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* themes
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:24:44
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates the <strong>ApplyDocumentTheme
</strong>method and <strong>ApplyQuickStyleSet </strong>method in Microsoft Word 2010.</span></p>
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
This action inserts 5 paragraphs with 5 sentences each into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within the DemoThemeAndQuickStyle procedure and press F8 to single step through the
 code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub DemoThemeAndQuickStyle()
    ' Modify this path for your own installation of Microsoft Office:
    Const themeFolder As String = &quot;C:\Program Files (x86)\Microsoft Office\Document Themes 14\&quot;
   
    ' Single step through this code to see the changes as they occur.
    ' Look at the list of Quick Styles in the user interface
    ' for more quick style names:
    ActiveDocument.ApplyQuickStyleSet &quot;Distinctive&quot;
    ActiveDocument.ApplyQuickStyleSet &quot;Elegant&quot;
    ActiveDocument.ApplyQuickStyleSet &quot;Manuscript&quot;
   
    ' These are just a few of the many available themes:
    ActiveDocument.ApplyDocumentTheme themeFolder &amp; &quot;Verve.thmx&quot;
    ActiveDocument.ApplyDocumentTheme themeFolder &amp; &quot;Clarity.thmx&quot;
    ActiveDocument.ApplyDocumentTheme themeFolder &amp; &quot;Newsprint.thmx&quot;
    ActiveDocument.ApplyDocumentTheme themeFolder &amp; &quot;Solstice.thmx&quot;
   
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;DemoThemeAndQuickStyle()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;this&nbsp;path&nbsp;for&nbsp;your&nbsp;own&nbsp;installation&nbsp;of&nbsp;Microsoft&nbsp;Office:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;themeFolder&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;&quot;C:\Program&nbsp;Files&nbsp;(x86)\Microsoft&nbsp;Office\Document&nbsp;Themes&nbsp;<span class="visualBasic__number">14</span>\&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Single&nbsp;step&nbsp;through&nbsp;this&nbsp;code&nbsp;to&nbsp;see&nbsp;the&nbsp;changes&nbsp;as&nbsp;they&nbsp;occur.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Look&nbsp;at&nbsp;the&nbsp;list&nbsp;of&nbsp;Quick&nbsp;Styles&nbsp;in&nbsp;the&nbsp;user&nbsp;interface</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;more&nbsp;quick&nbsp;style&nbsp;names:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyQuickStyleSet&nbsp;<span class="visualBasic__string">&quot;Distinctive&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyQuickStyleSet&nbsp;<span class="visualBasic__string">&quot;Elegant&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyQuickStyleSet&nbsp;<span class="visualBasic__string">&quot;Manuscript&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;These&nbsp;are&nbsp;just&nbsp;a&nbsp;few&nbsp;of&nbsp;the&nbsp;many&nbsp;available&nbsp;themes:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyDocumentTheme&nbsp;themeFolder&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Verve.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyDocumentTheme&nbsp;themeFolder&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Clarity.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyDocumentTheme&nbsp;themeFolder&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Newsprint.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ApplyDocumentTheme&nbsp;themeFolder&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Solstice.thmx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26226" href="/site/view/file/26226/1/Word.DocumentApplyThemeQuickStyle.txt">Word.DocumentApplyThemeQuickStyle.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26227" href="/site/view/file/26227/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
