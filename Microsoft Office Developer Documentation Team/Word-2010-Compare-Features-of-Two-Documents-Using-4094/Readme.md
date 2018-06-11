# Word 2010: Compare Features of Two Documents Using Word.DemoCompareDocuments
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* compare versions
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:20:39
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to compare various features of two Microsoft Word 2010 documents programmatically.</span></p>
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
This action inserts 5 paragraphs with 5 sentences each into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within the DemoCompare procedure and press F8 to single step through the code. Arrange
 the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub DemoCompareDocuments()
    ' Save the current document, including this code.
    Const path1 As String = &quot;C:\Temp\Doc1.docm&quot;
    Const path2 As String = &quot;C:\Temp\Doc2.docm&quot;
    Const path3 As String = &quot;C:\Temp\Doc3.docm&quot;
   
    Dim doc1 As Document
    Dim doc2 As Document
    Dim doc3 As Document
   
    ' Save with macros enabled, because this code exists within
    ' the document you're saving. If the document didn't contain
    ' code, you would not need to specify the file format.
    Set doc1 = ActiveDocument
    doc1.SaveAs path1, wdFormatXMLDocumentMacroEnabled
   
    ' Make some changes to the current document:
    Set doc2 = ActiveDocument
    doc2.ApplyQuickStyleSet2 &quot;Elegant&quot;
    ChangeTheDocument doc2
   
    ' Save the document as Doc2
    doc2.SaveAs2 path2, wdFormatFlatXMLMacroEnabled
   
    ' Open the original document
    Set doc1 = Documents.Open(path1)
   
    Set doc3 = Application.CompareDocuments(doc1, doc2, _
     Destination:=wdCompareDestinationNew, _
     Granularity:=wdGranularityWordLevel, _
     CompareFormatting:=True, _
     CompareCaseChanges:=True, _
     CompareWhiteSpace:=True)
    
    doc3.SaveAs2 path3, wdFormatFlatXMLMacroEnabled
    
End Sub

Private Sub ChangeTheDocument(doc As Document)
    ' Make some changes to the active document:
    ' Delete paragraph 3:
    doc.Paragraphs(3).Range.Delete
   
    ' Change a few words:
    doc.Paragraphs(1).Range.Words(3).Case = wdUpperCase
    doc.Paragraphs(2).Range.Words(6).Case = wdUpperCase
    doc.Paragraphs(3).Range.Sentences(2).Case = wdUpperCase
   
    ' Delete a few words:
    doc.Paragraphs(1).Range.Words(8).Delete
    doc.Paragraphs(1).Range.Words(12).Delete
    doc.Paragraphs(2).Range.Words(6).Delete
    doc.Paragraphs(3).Range.Words(10).Delete
   
    ' Format a few words:
    doc.Paragraphs(1).Range.Words(10).Bold = True
    doc.Paragraphs(2).Range.Words(5).Italic = True
    doc.Paragraphs(3).Range.Words(6).Underline = True
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;DemoCompareDocuments()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Save&nbsp;the&nbsp;current&nbsp;document,&nbsp;including&nbsp;this&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;path1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Temp\Doc1.docm&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;path2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Temp\Doc2.docm&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;path3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Temp\Doc3.docm&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Save&nbsp;with&nbsp;macros&nbsp;enabled,&nbsp;because&nbsp;this&nbsp;code&nbsp;exists&nbsp;within</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;document&nbsp;you're&nbsp;saving.&nbsp;If&nbsp;the&nbsp;document&nbsp;didn't&nbsp;contain</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;code,&nbsp;you&nbsp;would&nbsp;not&nbsp;need&nbsp;to&nbsp;specify&nbsp;the&nbsp;file&nbsp;format.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc1&nbsp;=&nbsp;ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc1.SaveAs&nbsp;path1,&nbsp;wdFormatXMLDocumentMacroEnabled&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;some&nbsp;changes&nbsp;to&nbsp;the&nbsp;current&nbsp;document:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc2&nbsp;=&nbsp;ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc2.ApplyQuickStyleSet2&nbsp;<span class="visualBasic__string">&quot;Elegant&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ChangeTheDocument&nbsp;doc2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Save&nbsp;the&nbsp;document&nbsp;as&nbsp;Doc2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc2.SaveAs2&nbsp;path2,&nbsp;wdFormatFlatXMLMacroEnabled&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Open&nbsp;the&nbsp;original&nbsp;document</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc1&nbsp;=&nbsp;Documents.Open(path1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc3&nbsp;=&nbsp;Application.CompareDocuments(doc1,&nbsp;doc2,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Destination:=wdCompareDestinationNew,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Granularity:=wdGranularityWordLevel,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CompareFormatting:=<span class="visualBasic__keyword">True</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CompareCaseChanges:=<span class="visualBasic__keyword">True</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CompareWhiteSpace:=<span class="visualBasic__keyword">True</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc3.SaveAs2&nbsp;path3,&nbsp;wdFormatFlatXMLMacroEnabled&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;ChangeTheDocument(doc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Document)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;some&nbsp;changes&nbsp;to&nbsp;the&nbsp;active&nbsp;document:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Delete&nbsp;paragraph&nbsp;3:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">3</span>).Range.Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;a&nbsp;few&nbsp;words:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">1</span>).Range.Words(<span class="visualBasic__number">3</span>).<span class="visualBasic__keyword">Case</span>&nbsp;=&nbsp;wdUpperCase&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">2</span>).Range.Words(<span class="visualBasic__number">6</span>).<span class="visualBasic__keyword">Case</span>&nbsp;=&nbsp;wdUpperCase&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">3</span>).Range.Sentences(<span class="visualBasic__number">2</span>).<span class="visualBasic__keyword">Case</span>&nbsp;=&nbsp;wdUpperCase&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Delete&nbsp;a&nbsp;few&nbsp;words:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">1</span>).Range.Words(<span class="visualBasic__number">8</span>).Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">1</span>).Range.Words(<span class="visualBasic__number">12</span>).Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">2</span>).Range.Words(<span class="visualBasic__number">6</span>).Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">3</span>).Range.Words(<span class="visualBasic__number">10</span>).Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Format&nbsp;a&nbsp;few&nbsp;words:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">1</span>).Range.Words(<span class="visualBasic__number">10</span>).Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">2</span>).Range.Words(<span class="visualBasic__number">5</span>).Italic&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Paragraphs(<span class="visualBasic__number">3</span>).Range.Words(<span class="visualBasic__number">6</span>).Underline&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26224" href="/site/view/file/26224/1/Word.DemoCompareDocuments.txt">Word.DemoCompareDocuments.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26225" href="/site/view/file/26225/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
