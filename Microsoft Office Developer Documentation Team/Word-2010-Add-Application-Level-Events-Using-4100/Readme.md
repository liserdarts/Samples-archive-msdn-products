# Word 2010: Add Application-Level Events Using Word.New Application Events
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* events
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:47:41
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to add a series of new Application-level events in Microsoft Word 2010 in order to react to events surrounding the use of protected-view windows.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Word 2010 adds a series of new Application-level events, allowing you to react to events surrounding the use of protected-view windows. Whether you open a document from the internet, or select to open a local document in protected
 view, the following events can occur as you interact with the protected-view document:</span><br>
<br>
<span style="font-size:small">-&nbsp;ProtectedViewWindowActivate</span><br>
<span style="font-size:small">-&nbsp;ProtectedViewWindowBeforeClose</span><br>
<span style="font-size:small">-&nbsp;ProtectedViewWindowBeforeEdit</span><br>
<span style="font-size:small">-&nbsp;ProtectedViewWIndowDeactivate</span><br>
<span style="font-size:small">-&nbsp;ProtectedViewWindowOpen</span><br>
<span style="font-size:small">-&nbsp;ProtectedViewWindowSize</span></p>
<p><span style="font-size:small">In order to test these events, first create a new document and&nbsp; in the VBA editor, add all the following code to the ThisDocument class module. Close the document, and reopen it (running the code in the document's Open
 event, which sets up the necessary event hooks).</span></p>
<p><span style="font-size:small">Then, open one or more documents in protected view. Either open documents from the Internet, or select File and then Open. In the Open dialog box, locate an existing document, but don't open it yet. Click the arrow next to the
 Open button, and select Open in Protected View from the menu (this opens the document in Protected View).</span></p>
<p><span style="font-size:small">Interact with the document that's open in protected view: resize it, switch the focus away and then back to the document, close it, and so on. Watch the text that appears in the current document as you do.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Private WithEvents app As Word.Application
Private doc As Word.Document

Private Sub AddText(text As String)
    doc.Content.InsertParagraphAfter
    doc.Content.InsertAfter (text)
End Sub

Private Sub app_ProtectedViewWindowActivate(ByVal PvWindow As ProtectedViewWindow)
    AddText &quot;ProtectedViewWindowActivate: &quot; &amp; PvWindow.Caption
End Sub

Private Sub app_ProtectedViewWindowBeforeClose(ByVal PvWindow As ProtectedViewWindow, ByVal CloseReason As Long, Cancel As Boolean)
    ' You can cancel the ProtectedViewWindowBeforeClose event.
    Dim reason As String
    reason = &quot;None&quot;

    Select Case CloseReason
      Case wdProtectedViewCloseEdit
        reason = &quot;Edit&quot;
      Case wdProtectedViewCloseForced
        reason = &quot;Forced&quot;
      Case wdProtectedViewCloseNormal
        reason = &quot;Normal&quot;
    End Select
   
    Dim text As String
    text = &quot;ProtectedViewWindowBeforeClose: &quot; &amp; _
        PvWindow.Caption &amp; &quot;(&quot; &amp; reason &amp; &quot;)&quot;

    AddText text
    Cancel = (MsgBox(text, vbOKCancel, &quot;Event&quot;) = vbCancel)
End Sub

Private Sub app_ProtectedViewWindowBeforeEdit(ByVal PvWindow As ProtectedViewWindow, Cancel As Boolean)
    ' You can cancel the ProtectedViewWindowBeforeEdit event.
    Dim text As String
    text = &quot;ProtectedViewWindowBeforeEdit: &quot; &amp; PvWindow.Caption
    AddText text
    Cancel = (MsgBox(text, vbOKCancel, &quot;Event&quot;) = vbCancel)
End Sub

Private Sub app_ProtectedViewWindowDeactivate(ByVal PvWindow As ProtectedViewWindow)
    AddText &quot;ProtectedViewWindowDeactivate: &quot; &amp; PvWindow.Caption
End Sub

Private Sub app_ProtectedViewWindowOpen(ByVal PvWindow As ProtectedViewWindow)
    AddText &quot;ProtectedViewWindowOpen: &quot; &amp; PvWindow.Caption
End Sub

Private Sub app_ProtectedViewWindowSize(ByVal PvWindow As ProtectedViewWindow)
    AddText &quot;ProtectedViewWindowSize: &quot; &amp; PvWindow.Caption
End Sub

Private Sub Document_Open()
    Set doc = Word.ActiveDocument
    Set app = Word.Application
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">WithEvents</span>&nbsp;app&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Word.Application&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;doc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Word.Document&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;AddText(text&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Content.InsertParagraphAfter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doc.Content.InsertAfter&nbsp;(text)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_ProtectedViewWindowActivate(<span class="visualBasic__keyword">ByVal</span>&nbsp;PvWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddText&nbsp;<span class="visualBasic__string">&quot;ProtectedViewWindowActivate:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;PvWindow.Caption&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_ProtectedViewWindowBeforeClose(<span class="visualBasic__keyword">ByVal</span>&nbsp;PvWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;CloseReason&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Long</span>,&nbsp;Cancel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;cancel&nbsp;the&nbsp;ProtectedViewWindowBeforeClose&nbsp;event.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;reason&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;None&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;CloseReason&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;wdProtectedViewCloseEdit&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Edit&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;wdProtectedViewCloseForced&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Forced&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;wdProtectedViewCloseNormal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reason&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Normal&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;text&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;ProtectedViewWindowBeforeClose:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PvWindow.Caption&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;(&quot;</span>&nbsp;&amp;&nbsp;reason&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;)&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddText&nbsp;text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MsgBox(text,&nbsp;vbOKCancel,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>)&nbsp;=&nbsp;vbCancel)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_ProtectedViewWindowBeforeEdit(<span class="visualBasic__keyword">ByVal</span>&nbsp;PvWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow,&nbsp;Cancel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;cancel&nbsp;the&nbsp;ProtectedViewWindowBeforeEdit&nbsp;event.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;text&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;ProtectedViewWindowBeforeEdit:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;PvWindow.Caption&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddText&nbsp;text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Cancel&nbsp;=&nbsp;(MsgBox(text,&nbsp;vbOKCancel,&nbsp;<span class="visualBasic__string">&quot;Event&quot;</span>)&nbsp;=&nbsp;vbCancel)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_ProtectedViewWindowDeactivate(<span class="visualBasic__keyword">ByVal</span>&nbsp;PvWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddText&nbsp;<span class="visualBasic__string">&quot;ProtectedViewWindowDeactivate:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;PvWindow.Caption&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_ProtectedViewWindowOpen(<span class="visualBasic__keyword">ByVal</span>&nbsp;PvWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddText&nbsp;<span class="visualBasic__string">&quot;ProtectedViewWindowOpen:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;PvWindow.Caption&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;app_ProtectedViewWindowSize(<span class="visualBasic__keyword">ByVal</span>&nbsp;PvWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ProtectedViewWindow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;AddText&nbsp;<span class="visualBasic__string">&quot;ProtectedViewWindowSize:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;PvWindow.Caption&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Document_Open()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc&nbsp;=&nbsp;Word.ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;app&nbsp;=&nbsp;Word.Application&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26236" href="/site/view/file/26236/1/Word.New%20Application%20Events.txt">Word.New Application Events.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26237" href="/site/view/file/26237/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
