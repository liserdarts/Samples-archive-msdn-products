# Word 2010: Work with Nested Undo Records Using Word.NestedCustomUndoRecords
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
* 2011-08-05 05:43:12
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use nested Undo records in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy all of this code into a new module in a Microsoft Word document.</span></p>
<p><span style="font-size:small">You can nest undo records, either intentionally or unintentionally. Only the name of the outermost custom undo record shows up in the list of actions to be undone in Word. When you undo the outermost undo record, you undo all
 the nested ones, as well.</span></p>
<p><span style="font-size:small">You can use the UndoRecord.IsRecordingCustomRecord and CustomRecordLevel properties to determine if you're currently creating a custom undo record, and if so, how many levels deep you are the nesting.</span></p>
<p><span style="font-size:small">The following sample includes three &quot;helper&quot; procedures. One procedure starts recording a custom undo record, and might be called several times in succession (thus, nesting undo records several levels deep). The second helper
 procedure &quot;unwinds&quot; the undo record recording, and ensures that you start a new custom undo record, so that a new item appears in Word on the list of actions to undo. The third procedure simply ends all undo record recording.</span></p>
<p><span style="font-size:small">The example is slightly contrived, but it demonstrates how you can use the various methods of the undo record.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestNestedUndo()
    CreateUndoRecord &quot;Record 1&quot;
    CreateUndoRecord &quot;Record 2&quot;
    CreateUndoRecord &quot;Record 3&quot;
    CreateUndoRecord &quot;Record 4&quot;
    StartNewUndoRecord &quot;Record 5&quot;
    CreateUndoRecord &quot;Record 6&quot;
    CreateUndoRecord &quot;Record 7&quot;
    CreateUndoRecord &quot;Record 8&quot;
    CreateUndoRecord &quot;Record 9&quot;
    StartNewUndoRecord &quot;Record 10&quot;
    EndUndoRecords
End Sub

Sub CreateUndoRecord(urName As String)
    ' Create a new undo record and insert some text.
    Dim ur As UndoRecord

    Set ur = Application.UndoRecord
    ur.StartCustomRecord urName
    ActiveDocument.Range.InsertAfter urName &amp; &quot; started.&quot;
    ActiveDocument.Range.InsertParagraphAfter
End Sub

Sub StartNewUndoRecord(urName As String)
    EndUndoRecords
    CreateUndoRecord urName
End Sub

Sub EndUndoRecords()
    Dim ur As UndoRecord

    Set ur = Application.UndoRecord
    If ur.IsRecordingCustomRecord Then
        Do While ur.CustomRecordLevel &gt; 0
            ' Note that CustomRecordName only returns the
            ' outermost undo record level name.
            ActiveDocument.Range.InsertAfter ur.CustomRecordName &amp; &quot; ended.&quot;
            ActiveDocument.Range.InsertParagraphAfter
            ur.EndCustomRecord
        Loop
    End If
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestNestedUndo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;1&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;2&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;3&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;4&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;StartNewUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;5&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;6&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;7&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;8&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;9&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;StartNewUndoRecord&nbsp;<span class="visualBasic__string">&quot;Record&nbsp;10&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;EndUndoRecords&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;CreateUndoRecord(urName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;new&nbsp;undo&nbsp;record&nbsp;and&nbsp;insert&nbsp;some&nbsp;text.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ur&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;UndoRecord&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ur&nbsp;=&nbsp;Application.UndoRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ur.StartCustomRecord&nbsp;urName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertAfter&nbsp;urName&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;started.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertParagraphAfter&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;StartNewUndoRecord(urName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;EndUndoRecords&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateUndoRecord&nbsp;urName&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;EndUndoRecords()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ur&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;UndoRecord&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ur&nbsp;=&nbsp;Application.UndoRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;ur.IsRecordingCustomRecord&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;<span class="visualBasic__keyword">While</span>&nbsp;ur.CustomRecordLevel&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;CustomRecordName&nbsp;only&nbsp;returns&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;outermost&nbsp;undo&nbsp;record&nbsp;level&nbsp;name.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertAfter&nbsp;ur.CustomRecordName&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;ended.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Range.InsertParagraphAfter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ur.EndCustomRecord&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Loop</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26234" href="/site/view/file/26234/1/Word.NestedCustomUndoRecords.txt">Word.NestedCustomUndoRecords.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26235" href="/site/view/file/26235/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
