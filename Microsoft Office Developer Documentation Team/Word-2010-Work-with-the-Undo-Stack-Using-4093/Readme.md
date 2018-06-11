# Word 2010: Work with the Undo Stack Using Word.CustomUndoRecord
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Undo
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:16:49
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the Undo stack in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy all of this code into a new module in a Microsoft Word document.</span></p>
<p><span style="font-size:small">Demonstrate working with the Undo stack. No matter what you do, Undo actions occur in the opposite order from which they occurred. Normally, each individual action gets added to the Undo stack by Microsoft Word. You can control
 the granularity of the Undo stack by interacting with the Application.UndoRecord object. Remember that undoing any action undoes any actions higher on the stack (that is, undo items added later to the undo stack). Normally, you can interact with the Undo stack
 in the Word 2010 user interace, and can choose one or more actions to undo. By grouping undo actions into a custom record, you can treat a group of actions as a single action.</span></p>
<p><span style="font-size:small">Before running this procedure, enter some text into the document and verify that the Undo toolbar item shows the results of your typing, available for undo. Then place the cursor in this procedure and press F5 to run it. Finally,
 go back to Microsoft Word, and verify that the Undo toolbar item shows &quot;My Second Undo Record&quot; and &quot;My Undo Record&quot;. Try undoing either of them, then repeat and undo the other, so that you understand how the Undo stack works. By creating your own custom undo
 records, you have instructed Microsoft Word to treat the whole group of actions as a single undo item.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub UndoRecordTest()

    Dim ur As UndoRecord
   
    ' The Application object supplies a single
    ' UndoRecord object, which maintains a stack
    ' of undo actions. You can interact with the stack, a little.
    Set ur = Application.UndoRecord
   
    ur.StartCustomRecord &quot;My Undo Record&quot;
        ActiveDocument.Range.InsertAfter &quot;Here is some text. &quot;
        ActiveDocument.Range.InsertAfter &quot;Here is some more text. &quot;
        ActiveDocument.Range.InsertParagraphAfter
        ActiveDocument.Range.InsertAfter &quot;After the chart.&quot;
        ActiveDocument.Shapes.AddChart xl3DPie, 100, 100, 200, 200
    ur.EndCustomRecord
   
    ur.StartCustomRecord &quot;My Second Undo Record&quot;
        ActiveDocument.Range.InsertAfter &quot;Here is some more text. &quot;
        ActiveDocument.Range.InsertParagraphAfter
       
        Dim paras As Paragraphs
        Set paras = ActiveDocument.Paragraphs
        ActiveDocument.Tables.Add paras(paras.Count).Range, 2, 2
    ur.EndCustomRecord
   
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;UndoRecordTest()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ur&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;UndoRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;Application&nbsp;object&nbsp;supplies&nbsp;a&nbsp;single</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;UndoRecord&nbsp;object,&nbsp;which&nbsp;maintains&nbsp;a&nbsp;stack</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;of&nbsp;undo&nbsp;actions.&nbsp;You&nbsp;can&nbsp;interact&nbsp;with&nbsp;the&nbsp;stack,&nbsp;a&nbsp;little.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ur&nbsp;=&nbsp;Application.UndoRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ur.StartCustomRecord&nbsp;<span class="visualBasic__string">&quot;My&nbsp;Undo&nbsp;Record&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertAfter&nbsp;<span class="visualBasic__string">&quot;Here&nbsp;is&nbsp;some&nbsp;text.&nbsp;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertAfter&nbsp;<span class="visualBasic__string">&quot;Here&nbsp;is&nbsp;some&nbsp;more&nbsp;text.&nbsp;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertParagraphAfter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertAfter&nbsp;<span class="visualBasic__string">&quot;After&nbsp;the&nbsp;chart.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Shapes.AddChart&nbsp;xl3DPie,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">100</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ur.EndCustomRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ur.StartCustomRecord&nbsp;<span class="visualBasic__string">&quot;My&nbsp;Second&nbsp;Undo&nbsp;Record&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertAfter&nbsp;<span class="visualBasic__string">&quot;Here&nbsp;is&nbsp;some&nbsp;more&nbsp;text.&nbsp;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertParagraphAfter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;paras&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Paragraphs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;paras&nbsp;=&nbsp;ActiveDocument.Paragraphs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Tables.Add&nbsp;paras(paras.Count).Range,&nbsp;<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ur.EndCustomRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26222" href="/site/view/file/26222/1/Word.CustomUndoRecord.txt">Word.CustomUndoRecord.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26223" href="/site/view/file/26223/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
