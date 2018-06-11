# Word 2010: Make and Save Edits Concurrently Using Word.Coauthoring
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* co-editing
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:03:33
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to make and save edits concurrently in a Microsoft Word 2010 document hosted on a SharePoint 2010 server.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">To demonstrate this code, create a new document on a Sharepoint server. Edit the document, and have a co-worker edit the document at the same time. Both of you should make edits concurrently, saving the document as you edit.
 The results of the sample procedure will vary based on how many authors are currently editing and how many concurrent changes you make. Place the cursor within the following procedure and press F5. The results will show in the Immediate window.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestCoauthoring()
    ' Ensure that you're seeing other authors' changes:
    ActiveDocument.ActiveWindow.View.ShowOtherAuthors = True
   
    ' Work with the document's CoAuthoring property:
    With ActiveDocument.CoAuthoring
        DisplayText &quot;Can merge: &quot; &amp; .CanMerge
        DisplayText &quot;Can share: &quot; &amp; .CanShare
   
        ' Retrieve a reference to the collection
        ' of current authors and display the count:
        Dim currentAuthors As CoAuthors
        Set currentAuthors = .Authors
        DisplayText &quot;There are &quot; &amp; currentAuthors.Count &amp; &quot; current authors.&quot;
       
        ' Iterate through all the current authors:
        Dim currentAuthor As CoAuthor
        For Each currentAuthor In currentAuthors
            DisplayText &quot;Current author: &quot; &amp; currentAuthor.Name &amp; IIf(currentAuthor.IsMe, &quot; (is me)&quot;, &quot;&quot;)
            DisplayText vbTab &amp; currentAuthor.EmailAddress, 1
        Next currentAuthor
       
        ' Are there pending updates?
        If .PendingUpdates Then
            ' Iterate through all the pending changes:
            DisplayText &quot;Pending Updates:&quot;
            Dim upd As CoAuthUpdate
            For Each upd In .Updates
                DisplayText upd.Range.text, 1
            Next upd
        End If
       
        ' Check out any current locks:
        Dim lck As CoAuthLock
        DisplayText &quot;Locks:&quot;
        For Each lck In .Locks
            DisplayText lck.Range.text, 2
        Next lck
       
        ' Check out any existing conflicts:
        Dim cft As Conflict
        DisplayText &quot;Conflicts:&quot;
        For Each cft In .Conflicts
            DisplayText cft.Range.text, 1
        Next cft
    End With
End Sub

Private Sub DisplayText(text As String, Optional indentLevel As Integer = 0)
    Dim i As Integer
    For i = 0 To indentLevel - 1
        Debug.Print vbTab;
    Next i
    Debug.Print text
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestCoauthoring()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Ensure&nbsp;that&nbsp;you're&nbsp;seeing&nbsp;other&nbsp;authors'&nbsp;changes:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.ActiveWindow.View.ShowOtherAuthors&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;document's&nbsp;CoAuthoring&nbsp;property:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActiveDocument.CoAuthoring&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;Can&nbsp;merge:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.CanMerge&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;Can&nbsp;share:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.CanShare&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;collection</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;of&nbsp;current&nbsp;authors&nbsp;and&nbsp;display&nbsp;the&nbsp;count:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;currentAuthors&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CoAuthors&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;currentAuthors&nbsp;=&nbsp;.Authors&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;There&nbsp;are&nbsp;&quot;</span>&nbsp;&amp;&nbsp;currentAuthors.Count&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;current&nbsp;authors.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Iterate&nbsp;through&nbsp;all&nbsp;the&nbsp;current&nbsp;authors:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;currentAuthor&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CoAuthor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;currentAuthor&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;currentAuthors&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;Current&nbsp;author:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;currentAuthor.Name&nbsp;&amp;&nbsp;IIf(currentAuthor.IsMe,&nbsp;<span class="visualBasic__string">&quot;&nbsp;(is&nbsp;me)&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;vbTab&nbsp;&amp;&nbsp;currentAuthor.EmailAddress,&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;currentAuthor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Are&nbsp;there&nbsp;pending&nbsp;updates?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;.PendingUpdates&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Iterate&nbsp;through&nbsp;all&nbsp;the&nbsp;pending&nbsp;changes:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;Pending&nbsp;Updates:&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;upd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CoAuthUpdate&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;upd&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;.Updates&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;upd.Range.text,&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;upd&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Check&nbsp;out&nbsp;any&nbsp;current&nbsp;locks:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;lck&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CoAuthLock&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;Locks:&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;lck&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;.Locks&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;lck.Range.text,&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;lck&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Check&nbsp;out&nbsp;any&nbsp;existing&nbsp;conflicts:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cft&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Conflict&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;<span class="visualBasic__string">&quot;Conflicts:&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;cft&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;.Conflicts&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayText&nbsp;cft.Range.text,&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;cft&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DisplayText(text&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>,&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;indentLevel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;=&nbsp;<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;indentLevel&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;text&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26216" href="/site/view/file/26216/1/Word.Coauthoring.txt">Word.Coauthoring.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26217" href="/site/view/file/26217/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
