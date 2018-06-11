# Word 2010: Create a New Quick Style Set Using Word.SaveAsQuickStyleSet
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
* 2011-08-05 06:05:42
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>SaveAsQuickStyleSet
</strong>method in Microsoft Word 2010 to create a quick style set with various properties.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:</span><br>
<br>
<span style="font-size:small">=rand(5, 5)</span><br>
<br>
<span style="font-size:small">This action inserts 5 paragraphs with 5 sentences each into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within the DemoSaveAsQuickStyleSet procedure and press
 F8 to single step through the code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub DemoSaveAsQuickStyleSet()
    ' Given the five paragraphs you added (by following the instructions),
    ' modify the color, font, and paragraph layout. Then save the whole
    ' thing as a new quick style.
    
     Dim theme As OfficeTheme
     Set theme = ActiveDocument.DocumentTheme
    
    ' Modify this path for your own installation of Microsoft Office:
    Const themeFolder As String = &quot;C:\Program Files (x86)\Microsoft Office\Document Themes 14\&quot;
   
    ' Modify the name of the new template to meet your needs:
    Const newTemplateName = &quot;C:\temp\Demo1.dotx&quot;
    
    ' Select one of the available theme color sets:
    theme.ThemeColorScheme.Load themeFolder &amp; &quot;Theme Colors\Adjacency.xml&quot;
   
    ' Select one of the available theme fonts:
    theme.ThemeFontScheme.Load themeFolder &amp; &quot;Theme Fonts\Slipstream.xml&quot;
   
    ' Save the settings as a new quick style set. This actually
    ' creates a new document template, which you can use for
    ' creating future documents.
    ActiveDocument.SaveAsQuickStyleSet newTemplateName
   
    ' Verify that in the new document that this statement creates,
    ' the theme color and font correspond to the options you
    ' set when you created the quick set.
    Documents.Add newTemplateName
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;DemoSaveAsQuickStyleSet()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Given&nbsp;the&nbsp;five&nbsp;paragraphs&nbsp;you&nbsp;added&nbsp;(by&nbsp;following&nbsp;the&nbsp;instructions),</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;modify&nbsp;the&nbsp;color,&nbsp;font,&nbsp;and&nbsp;paragraph&nbsp;layout.&nbsp;Then&nbsp;save&nbsp;the&nbsp;whole</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;thing&nbsp;as&nbsp;a&nbsp;new&nbsp;quick&nbsp;style.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;theme&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OfficeTheme&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;theme&nbsp;=&nbsp;ActiveDocument.DocumentTheme&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;this&nbsp;path&nbsp;for&nbsp;your&nbsp;own&nbsp;installation&nbsp;of&nbsp;Microsoft&nbsp;Office:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;themeFolder&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;&quot;C:\Program&nbsp;Files&nbsp;(x86)\Microsoft&nbsp;Office\Document&nbsp;Themes&nbsp;<span class="visualBasic__number">14</span>\&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;the&nbsp;name&nbsp;of&nbsp;the&nbsp;new&nbsp;template&nbsp;to&nbsp;meet&nbsp;your&nbsp;needs:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;newTemplateName&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\temp\Demo1.dotx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Select&nbsp;one&nbsp;of&nbsp;the&nbsp;available&nbsp;theme&nbsp;color&nbsp;sets:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;theme.ThemeColorScheme.Load&nbsp;themeFolder&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Theme&nbsp;Colors\Adjacency.xml&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Select&nbsp;one&nbsp;of&nbsp;the&nbsp;available&nbsp;theme&nbsp;fonts:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;theme.ThemeFontScheme.Load&nbsp;themeFolder&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Theme&nbsp;Fonts\Slipstream.xml&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Save&nbsp;the&nbsp;settings&nbsp;as&nbsp;a&nbsp;new&nbsp;quick&nbsp;style&nbsp;set.&nbsp;This&nbsp;actually</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;creates&nbsp;a&nbsp;new&nbsp;document&nbsp;template,&nbsp;which&nbsp;you&nbsp;can&nbsp;use&nbsp;for</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;creating&nbsp;future&nbsp;documents.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.SaveAsQuickStyleSet&nbsp;newTemplateName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Verify&nbsp;that&nbsp;in&nbsp;the&nbsp;new&nbsp;document&nbsp;that&nbsp;this&nbsp;statement&nbsp;creates,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;theme&nbsp;color&nbsp;and&nbsp;font&nbsp;correspond&nbsp;to&nbsp;the&nbsp;options&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;set&nbsp;when&nbsp;you&nbsp;created&nbsp;the&nbsp;quick&nbsp;set.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Documents.Add&nbsp;newTemplateName&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26244" href="/site/view/file/26244/1/Word.SaveAsQuickStyleSet.txt">Word.SaveAsQuickStyleSet.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26245" href="/site/view/file/26245/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
