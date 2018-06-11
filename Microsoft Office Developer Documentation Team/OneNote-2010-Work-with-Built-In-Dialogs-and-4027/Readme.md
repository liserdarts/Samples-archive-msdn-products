# OneNote 2010: Work with Built-In Dialogs and Properties Using QuickFiling Dialog
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
* OneNote 2010
## Topics
* Office 2010 101 code samples
* OneNote notebooks
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:32:49
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates the <strong>QuickFiling
</strong>dialog to select an object, such as a <strong>Page </strong>object, in Microsoft OneNote 2010 by using a built-in dialog and return various properties.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Use any VBA host including Excel 2010, PowerPoint 2010, or Word 2010. OneNote 2010 is not a VBA host.</span></p>
<p><span style="font-size:small">In your VBA host, add references to the following external libraries using the Add References dialog:</span><br>
<span style="font-size:small">&nbsp;- Microsoft OneNote 14.0 Object Library</span></p>
<p><span style="font-size:small">This sample has two blocks of code. To add Block 1, you first Insert a VBA Class Module. Change the (Name) of the class to QFHandler. Mark the Instancing property of the class as 2-PublicNotCreatable. Then place the contents
 of Block 1 into the class you created and configured. Add Block 2 to the default module of your VBA host.</span></p>
<p><span style="font-size:small">To run the sample, place the cursor within the RunQuickFilingDemo procedure, and press F5. You will need to minimize the VBA host to see the OneNote dialog.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">' *** Begin Block 1 ***
Option Explicit

Implements OneNote14.IQuickFilingDialogCallback

Private oneNote As OneNote14.Application
    
Public Sub Demo()
    ' Connect to OneNote 2010.
    ' OneNote will be started if it's not running.
    Set oneNote = New OneNote14.Application
    
    ' Create an instance of the QuickFiling dialog
    Dim qf As OneNote14.IQuickFilingDialog
    Set qf = oneNote.QuickFiling
    
    ' Set some properties
    qf.Title = &quot;Pick Data (Demo)&quot;
    qf.Description = &quot;Select the page whose data you want to read.&quot;
    ' if you were to run this from a form
    ' you could provide the form's handle to control focus
    ' to the WindowHandle property
    ' qf.WindowHandle = &lt;ID of your window&gt;
    
    ' Show the dialog (you'll need to switch to the running screen)
    qf.Run Me
End Sub

' When the user clicks OK or Cancel
' the dialog calls this code.
Private Sub IQuickFilingDialogCallback_OnDialogClosed( _
    ByVal dialog As OneNote14.IQuickFilingDialog)
    
    If dialog.SelectedItem &lt;&gt; &quot;&quot; Then
        ' if the user selected something try and read it
        On Error Resume Next
        ' There isn't an easy way to check if the ID
        ' returned is a page or not so continue on error.
        Dim pagesXml As String
        oneNote.GetHierarchy dialog.SelectedItem, hsSelf, pagesXml, xs2010
    
        Debug.Print pagesXml
    Else
        Debug.Print &quot;Nothing was selected&quot;
    End If
End Sub
' *** End Block 1 ***

' *** Begin Block 2 ***
' This procedure calls code in the QFHandler class.
' The QuickFiling dialog allows the user to select an object
' in OneNote using a built-in OneNote dialog.
' This allows the user to select an item from OneNote, like a Page,
' and pass the selected item back to your code.
Option Explicit 

Sub RunQuickFilingDemo()
    Dim cb As QFHandler
    Set cb = New QFHandler
    cb.Demo
End Sub
' *** End Block 2 ***
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__com">'&nbsp;***&nbsp;Begin&nbsp;Block&nbsp;1&nbsp;***</span>&nbsp;
<span class="visualBasic__keyword">Option</span>&nbsp;Explicit&nbsp;
&nbsp;
<span class="visualBasic__keyword">Implements</span>&nbsp;OneNote14.IQuickFilingDialogCallback&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;oneNote&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Demo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Connect&nbsp;to&nbsp;OneNote&nbsp;2010.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;OneNote&nbsp;will&nbsp;be&nbsp;started&nbsp;if&nbsp;it's&nbsp;not&nbsp;running.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;oneNote&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;an&nbsp;instance&nbsp;of&nbsp;the&nbsp;QuickFiling&nbsp;dialog</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;qf&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.IQuickFilingDialog&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;qf&nbsp;=&nbsp;oneNote.QuickFiling&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;some&nbsp;properties</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;qf.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Pick&nbsp;Data&nbsp;(Demo)&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;qf.Description&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Select&nbsp;the&nbsp;page&nbsp;whose&nbsp;data&nbsp;you&nbsp;want&nbsp;to&nbsp;read.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;if&nbsp;you&nbsp;were&nbsp;to&nbsp;run&nbsp;this&nbsp;from&nbsp;a&nbsp;form</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;could&nbsp;provide&nbsp;the&nbsp;form's&nbsp;handle&nbsp;to&nbsp;control&nbsp;focus</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;the&nbsp;WindowHandle&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;qf.WindowHandle&nbsp;=&nbsp;&lt;ID&nbsp;of&nbsp;your&nbsp;window&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Show&nbsp;the&nbsp;dialog&nbsp;(you'll&nbsp;need&nbsp;to&nbsp;switch&nbsp;to&nbsp;the&nbsp;running&nbsp;screen)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;qf.Run&nbsp;<span class="visualBasic__keyword">Me</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;When&nbsp;the&nbsp;user&nbsp;clicks&nbsp;OK&nbsp;or&nbsp;Cancel</span>&nbsp;
<span class="visualBasic__com">'&nbsp;the&nbsp;dialog&nbsp;calls&nbsp;this&nbsp;code.</span>&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;IQuickFilingDialogCallback_OnDialogClosed(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;dialog&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.IQuickFilingDialog)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;dialog.SelectedItem&nbsp;&lt;&gt;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;if&nbsp;the&nbsp;user&nbsp;selected&nbsp;something&nbsp;try&nbsp;and&nbsp;read&nbsp;it</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">Resume</span>&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;There&nbsp;isn't&nbsp;an&nbsp;easy&nbsp;way&nbsp;to&nbsp;check&nbsp;if&nbsp;the&nbsp;ID</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;returned&nbsp;is&nbsp;a&nbsp;page&nbsp;or&nbsp;not&nbsp;so&nbsp;continue&nbsp;on&nbsp;error.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pagesXml&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetHierarchy&nbsp;dialog.SelectedItem,&nbsp;hsSelf,&nbsp;pagesXml,&nbsp;xs2010&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;pagesXml&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Nothing&nbsp;was&nbsp;selected&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
<span class="visualBasic__com">'&nbsp;***&nbsp;End&nbsp;Block&nbsp;1&nbsp;***</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;***&nbsp;Begin&nbsp;Block&nbsp;2&nbsp;***</span>&nbsp;
<span class="visualBasic__com">'&nbsp;This&nbsp;procedure&nbsp;calls&nbsp;code&nbsp;in&nbsp;the&nbsp;QFHandler&nbsp;class.</span>&nbsp;
<span class="visualBasic__com">'&nbsp;The&nbsp;QuickFiling&nbsp;dialog&nbsp;allows&nbsp;the&nbsp;user&nbsp;to&nbsp;select&nbsp;an&nbsp;object</span>&nbsp;
<span class="visualBasic__com">'&nbsp;in&nbsp;OneNote&nbsp;using&nbsp;a&nbsp;built-in&nbsp;OneNote&nbsp;dialog.</span>&nbsp;
<span class="visualBasic__com">'&nbsp;This&nbsp;allows&nbsp;the&nbsp;user&nbsp;to&nbsp;select&nbsp;an&nbsp;item&nbsp;from&nbsp;OneNote,&nbsp;like&nbsp;a&nbsp;Page,</span>&nbsp;
<span class="visualBasic__com">'&nbsp;and&nbsp;pass&nbsp;the&nbsp;selected&nbsp;item&nbsp;back&nbsp;to&nbsp;your&nbsp;code.</span>&nbsp;
<span class="visualBasic__keyword">Option</span>&nbsp;Explicit&nbsp;&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;RunQuickFilingDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cb&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;QFHandler&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cb&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;QFHandler&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cb.Demo&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
<span class="visualBasic__com">'&nbsp;***&nbsp;End&nbsp;Block&nbsp;2&nbsp;***</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="25990" href="/site/view/file/25990/1/OneNote.fromVBA.GetPageviaQuickFilingDialog.txt">OneNote.fromVBA.GetPageviaQuickFilingDialog.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="25991" href="/site/view/file/25991/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905452">OneNote Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
