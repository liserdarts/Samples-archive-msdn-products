# Excel 2010: Remove Various Properties Using Excel.RemoveDocumentInformation
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* remove comments
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:54:21
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>RemoveDocumentInformation
</strong>method to remove comments, defined name comments, personal information, and document properties from a Microsoft Excel 2010 worksheet.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Work with the RemoveDocumentInformation method. Add a named range with a comment, some document properties, and a comment.</span></p>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the TestRemoveDocumentInformation procedure, and then press F8 to single-step through the code. Verify that the various items
 get added as you single step, and that they're removed at the end.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestRemoveDocumentInformation()
  ' Set up a named range with a comment:
  Dim nm As Name
  Set nm = Names.Add(Name:=&quot;TestNamedRange&quot;, _
   RefersToR1C1:=&quot;=Sheet1!R1C1:R7C3&quot;)
  nm.Name = &quot;NamedRange&quot;

   ' You can see the comment by clicking the Formulas tab menu and then 
   ' clicking Name Manager.
  nm.Comment = &quot;Here is a comment&quot;
 
  ' Set some document properties:
  Dim props As Office.DocumentProperties
  Set props = ActiveWorkbook.BuiltinDocumentProperties
  props(&quot;Author&quot;).Value = &quot;Author Name&quot;
  props(&quot;Subject&quot;).Value = &quot;Test Document&quot;
 
  ' Add a comment, which will include your name.
  ' Removing information will convert author name to Author
 
  Dim cmt As Comment
  Set cmt = Range(&quot;B3&quot;).AddComment
  cmt.Visible = False
  cmt.Text &quot;This is a test&quot;

  ' Remove comments, defined name comments, personal information, and document properties.
  ActiveWorkbook.RemoveDocumentInformation xlRDIComments
  ActiveWorkbook.RemoveDocumentInformation xlRDIDefinedNameComments
  ActiveWorkbook.RemoveDocumentInformation xlRDIRemovePersonalInformation
  ActiveWorkbook.RemoveDocumentInformation xlRDIDocumentProperties
 
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestRemoveDocumentInformation()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;a&nbsp;named&nbsp;range&nbsp;with&nbsp;a&nbsp;comment:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;nm&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Name&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;nm&nbsp;=&nbsp;Names.Add(Name:=<span class="visualBasic__string">&quot;TestNamedRange&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;RefersToR1C1:=<span class="visualBasic__string">&quot;=Sheet1!R1C1:R7C3&quot;</span>)&nbsp;
&nbsp;&nbsp;nm.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;NamedRange&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;see&nbsp;the&nbsp;comment&nbsp;by&nbsp;clicking&nbsp;the&nbsp;Formulas&nbsp;tab&nbsp;menu&nbsp;and&nbsp;then&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;clicking&nbsp;Name&nbsp;Manager.</span>&nbsp;
&nbsp;&nbsp;nm.Comment&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Here&nbsp;is&nbsp;a&nbsp;comment&quot;</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;some&nbsp;document&nbsp;properties:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;props&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Office.DocumentProperties&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;props&nbsp;=&nbsp;ActiveWorkbook.BuiltinDocumentProperties&nbsp;
&nbsp;&nbsp;props(<span class="visualBasic__string">&quot;Author&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Author&nbsp;Name&quot;</span>&nbsp;
&nbsp;&nbsp;props(<span class="visualBasic__string">&quot;Subject&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Test&nbsp;Document&quot;</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;comment,&nbsp;which&nbsp;will&nbsp;include&nbsp;your&nbsp;name.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Removing&nbsp;information&nbsp;will&nbsp;convert&nbsp;author&nbsp;name&nbsp;to&nbsp;Author</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cmt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Comment&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cmt&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;B3&quot;</span>).AddComment&nbsp;
&nbsp;&nbsp;cmt.Visible&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;cmt.Text&nbsp;<span class="visualBasic__string">&quot;This&nbsp;is&nbsp;a&nbsp;test&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;comments,&nbsp;defined&nbsp;name&nbsp;comments,&nbsp;personal&nbsp;information,&nbsp;and&nbsp;document&nbsp;properties.</span>&nbsp;
&nbsp;&nbsp;ActiveWorkbook.RemoveDocumentInformation&nbsp;xlRDIComments&nbsp;
&nbsp;&nbsp;ActiveWorkbook.RemoveDocumentInformation&nbsp;xlRDIDefinedNameComments&nbsp;
&nbsp;&nbsp;ActiveWorkbook.RemoveDocumentInformation&nbsp;xlRDIRemovePersonalInformation&nbsp;
&nbsp;&nbsp;ActiveWorkbook.RemoveDocumentInformation&nbsp;xlRDIDocumentProperties&nbsp;
&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25923" href="/site/view/file/25923/1/Excel.RemoveDocumentInformation.txt">Excel.RemoveDocumentInformation.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25924" href="/site/view/file/25924/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.
</span></em></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
