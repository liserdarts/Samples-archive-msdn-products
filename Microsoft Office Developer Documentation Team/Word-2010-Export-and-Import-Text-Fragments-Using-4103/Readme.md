# Word 2010: Export and Import Text Fragments Using Word.RangeImportExportFragment
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
* 2011-08-05 06:01:46
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to export and import the text of a given range in a Microsoft Word 2010 document as fragments for use in other document.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:<br>
<br>
=rand(5, 3)<br>
<br>
This action inserts 5 paragraphs with 3 sentences each into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within the RangeExportImportFragments procedure and press F8 to single step through
 the code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub RangeExportImportFragments()
    ' Word 2007 and later allows you to export ranges of text as a fragments.
    ' You can them reimport these fragments and use them in any document.
   
    ' Given the sample document with 5 paragraphs, try exporting a fragment,
    ' then modifying the format and reimporting the fragment to see the behavior.
   
    ' Change this path to match your own situation.
    Const FRAGMENT_FILE As String = &quot;C:\Temp\Fragment.docx&quot;
   
    Dim rng As Range
    Set rng = Range(15, 150)
    rng.Bold = True
   
    rng.ExportFragment FRAGMENT_FILE, wdFormatDocumentDefault
   
    ' Take a moment and load the fragment file into Microsoft Word.
    ' It should load like any other document.
   
    ' Set formatting back to normal.
    rng.Bold = False
   
    ' Although the second paramter indicates style behavior for the incoming
    ' fragment, it doesn't appear to make any different which option
    ' you choose. Both True and False give the same result. If False, the
    ' fragment should use the formatting from the original document, not the
    ' current document, but this doesn't appear to be the case. The ImportFragment
    ' method uses the formatting from the source document, no matter how this
    ' parameter has been set:
    Words(1).ImportFragment FRAGMENT_FILE, False
    Words(100).ImportFragment FRAGMENT_FILE, True
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;RangeExportImportFragments()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Word&nbsp;2007&nbsp;and&nbsp;later&nbsp;allows&nbsp;you&nbsp;to&nbsp;export&nbsp;ranges&nbsp;of&nbsp;text&nbsp;as&nbsp;a&nbsp;fragments.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;them&nbsp;reimport&nbsp;these&nbsp;fragments&nbsp;and&nbsp;use&nbsp;them&nbsp;in&nbsp;any&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Given&nbsp;the&nbsp;sample&nbsp;document&nbsp;with&nbsp;5&nbsp;paragraphs,&nbsp;try&nbsp;exporting&nbsp;a&nbsp;fragment,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;then&nbsp;modifying&nbsp;the&nbsp;format&nbsp;and&nbsp;reimporting&nbsp;the&nbsp;fragment&nbsp;to&nbsp;see&nbsp;the&nbsp;behavior.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;this&nbsp;path&nbsp;to&nbsp;match&nbsp;your&nbsp;own&nbsp;situation.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;FRAGMENT_FILE&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Temp\Fragment.docx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__number">15</span>,&nbsp;<span class="visualBasic__number">150</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.ExportFragment&nbsp;FRAGMENT_FILE,&nbsp;wdFormatDocumentDefault&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Take&nbsp;a&nbsp;moment&nbsp;and&nbsp;load&nbsp;the&nbsp;fragment&nbsp;file&nbsp;into&nbsp;Microsoft&nbsp;Word.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;It&nbsp;should&nbsp;load&nbsp;like&nbsp;any&nbsp;other&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;formatting&nbsp;back&nbsp;to&nbsp;normal.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Although&nbsp;the&nbsp;second&nbsp;paramter&nbsp;indicates&nbsp;style&nbsp;behavior&nbsp;for&nbsp;the&nbsp;incoming</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;fragment,&nbsp;it&nbsp;doesn't&nbsp;appear&nbsp;to&nbsp;make&nbsp;any&nbsp;different&nbsp;which&nbsp;option</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;choose.&nbsp;Both&nbsp;True&nbsp;and&nbsp;False&nbsp;give&nbsp;the&nbsp;same&nbsp;result.&nbsp;If&nbsp;False,&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;fragment&nbsp;should&nbsp;use&nbsp;the&nbsp;formatting&nbsp;from&nbsp;the&nbsp;original&nbsp;document,&nbsp;not&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;current&nbsp;document,&nbsp;but&nbsp;this&nbsp;doesn't&nbsp;appear&nbsp;to&nbsp;be&nbsp;the&nbsp;case.&nbsp;The&nbsp;ImportFragment</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;method&nbsp;uses&nbsp;the&nbsp;formatting&nbsp;from&nbsp;the&nbsp;source&nbsp;document,&nbsp;no&nbsp;matter&nbsp;how&nbsp;this</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;parameter&nbsp;has&nbsp;been&nbsp;set:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Words(<span class="visualBasic__number">1</span>).ImportFragment&nbsp;FRAGMENT_FILE,&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Words(<span class="visualBasic__number">100</span>).ImportFragment&nbsp;FRAGMENT_FILE,&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26242" href="/site/view/file/26242/1/Word.RangeImportExportFragment.txt">Word.RangeImportExportFragment.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26243" href="/site/view/file/26243/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
