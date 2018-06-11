# Word 2010: Work with Themes, Track Formatting, Track Revisions, and Track Moves
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Document object
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:29:32
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use various <strong>Document
</strong>class properties in Microsoft Word 2010 such as themes, track formatting, track revisions, and track moves.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new document, type the following text and then press Enter:<br>
<br>
=rand(1, 5)<br>
<br>
This action inserts 1 paragraph with 5 sentences into the current document. Then, in the VBA editor, in the ThisDocument class, copy in this code, and place the cursor within the DocumentInformationProperties procedure and press F8 to single step through the
 code. Arrange the VBA and Word windows side by side on screen so you can view the behavior as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub DocumentInformationProperties()
    Dim isFinal As Boolean
    Dim doLockQuickStyleSet As Boolean
    Dim doLockTheme As Boolean
    Dim doTrackFormatting As Boolean
    Dim doTrackRevisions As Boolean
    Dim doTrackMoves As Boolean

    With ActiveDocument
        ' Store away the current state:
        isFinal = .Final
        doLockQuickStyleSet = .LockQuickStyleSet
        doLockTheme = .LockTheme
        doTrackFormatting = .TrackFormatting
        doTrackRevisions = .TrackRevisions
        doTrackMoves = .TrackMoves
   
        ' This had better return true!
        Debug.Print .HasVBProject
        
        ' Work with Quick Styles
        .LockQuickStyleSet = False
        ' Now, on the Ribbon, go to the Styles
        ' group, and select Change Styles. Select
        ' Style Set, and note the list of available quick styles.
        .LockQuickStyleSet = True
        ' Repeat the same steps, and now note that the
        ' menu no longer contains a list of available quick styles.
       
        ' Work with the Theme
        .LockTheme = False
        ' On the menu, click the Page Layout menu item.
        ' In the Themes group, note that the Themes drop-down is available.
        ' You can change the document's theme.
       
        .LockTheme = True
        ' Go to the same location, and note that now the Themes drop-down
        ' is no longer available.
       
        .TrackRevisions = True
        .TrackFormatting = True
        ' In the document, change a word to be bold. Note that the
        ' document tracks formatting changes.
        .TrackFormatting = False
        ' Change another word to be bold, and note that the document
        ' no longer tracks formatting changes.
       
        .TrackMoves = True
        ' In the document, move a word. Note that the
        ' document tracks moves.
        .TrackMoves = False
        ' Move another word, and note that the document
        ' no longer tracks moves.
       
        ' Indicate that the current document is final.
        ' This forces a save of the document if it's not
        ' already saved:
        .Final = True
        ' Note the banner that appears in the document.
        ' Click Edit Anyway to continue editing.
       
        ' Put things back the way they were:
        .Final = isFinal
        .LockQuickStyleSet = doLockQuickStyleSet
        .LockTheme = doLockTheme
        .TrackFormatting = doTrackFormatting
        .TrackRevisions = doTrackRevisions
        .TrackMoves = doTrackMoves
       
        ' Retrieve the contents as an XML stream, using the
        ' Open XML file format.
        Dim results As String
        results = .WordOpenXML
        MsgBox results
    End With
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;DocumentInformationProperties()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;isFinal&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doLockQuickStyleSet&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doLockTheme&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doTrackFormatting&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doTrackRevisions&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doTrackMoves&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Store&nbsp;away&nbsp;the&nbsp;current&nbsp;state:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;isFinal&nbsp;=&nbsp;.Final&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doLockQuickStyleSet&nbsp;=&nbsp;.LockQuickStyleSet&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doLockTheme&nbsp;=&nbsp;.LockTheme&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doTrackFormatting&nbsp;=&nbsp;.TrackFormatting&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doTrackRevisions&nbsp;=&nbsp;.TrackRevisions&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doTrackMoves&nbsp;=&nbsp;.TrackMoves&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;had&nbsp;better&nbsp;return&nbsp;true!</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;.HasVBProject&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;Quick&nbsp;Styles</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LockQuickStyleSet&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now,&nbsp;on&nbsp;the&nbsp;Ribbon,&nbsp;go&nbsp;to&nbsp;the&nbsp;Styles</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;group,&nbsp;and&nbsp;select&nbsp;Change&nbsp;Styles.&nbsp;Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Style&nbsp;Set,&nbsp;and&nbsp;note&nbsp;the&nbsp;list&nbsp;of&nbsp;available&nbsp;quick&nbsp;styles.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LockQuickStyleSet&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Repeat&nbsp;the&nbsp;same&nbsp;steps,&nbsp;and&nbsp;now&nbsp;note&nbsp;that&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;menu&nbsp;no&nbsp;longer&nbsp;contains&nbsp;a&nbsp;list&nbsp;of&nbsp;available&nbsp;quick&nbsp;styles.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;Theme</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LockTheme&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;On&nbsp;the&nbsp;menu,&nbsp;click&nbsp;the&nbsp;Page&nbsp;Layout&nbsp;menu&nbsp;item.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;the&nbsp;Themes&nbsp;group,&nbsp;note&nbsp;that&nbsp;the&nbsp;Themes&nbsp;drop-down&nbsp;is&nbsp;available.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;change&nbsp;the&nbsp;document's&nbsp;theme.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LockTheme&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Go&nbsp;to&nbsp;the&nbsp;same&nbsp;location,&nbsp;and&nbsp;note&nbsp;that&nbsp;now&nbsp;the&nbsp;Themes&nbsp;drop-down</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;is&nbsp;no&nbsp;longer&nbsp;available.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackRevisions&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackFormatting&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;the&nbsp;document,&nbsp;change&nbsp;a&nbsp;word&nbsp;to&nbsp;be&nbsp;bold.&nbsp;Note&nbsp;that&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;document&nbsp;tracks&nbsp;formatting&nbsp;changes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackFormatting&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;another&nbsp;word&nbsp;to&nbsp;be&nbsp;bold,&nbsp;and&nbsp;note&nbsp;that&nbsp;the&nbsp;document</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;no&nbsp;longer&nbsp;tracks&nbsp;formatting&nbsp;changes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackMoves&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;the&nbsp;document,&nbsp;move&nbsp;a&nbsp;word.&nbsp;Note&nbsp;that&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;document&nbsp;tracks&nbsp;moves.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackMoves&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Move&nbsp;another&nbsp;word,&nbsp;and&nbsp;note&nbsp;that&nbsp;the&nbsp;document</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;no&nbsp;longer&nbsp;tracks&nbsp;moves.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Indicate&nbsp;that&nbsp;the&nbsp;current&nbsp;document&nbsp;is&nbsp;final.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;forces&nbsp;a&nbsp;save&nbsp;of&nbsp;the&nbsp;document&nbsp;if&nbsp;it's&nbsp;not</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;already&nbsp;saved:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Final&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;the&nbsp;banner&nbsp;that&nbsp;appears&nbsp;in&nbsp;the&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Click&nbsp;Edit&nbsp;Anyway&nbsp;to&nbsp;continue&nbsp;editing.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;things&nbsp;back&nbsp;the&nbsp;way&nbsp;they&nbsp;were:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Final&nbsp;=&nbsp;isFinal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LockQuickStyleSet&nbsp;=&nbsp;doLockQuickStyleSet&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LockTheme&nbsp;=&nbsp;doLockTheme&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackFormatting&nbsp;=&nbsp;doTrackFormatting&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackRevisions&nbsp;=&nbsp;doTrackRevisions&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TrackMoves&nbsp;=&nbsp;doTrackMoves&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;the&nbsp;contents&nbsp;as&nbsp;an&nbsp;XML&nbsp;stream,&nbsp;using&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Open&nbsp;XML&nbsp;file&nbsp;format.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;results&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;results&nbsp;=&nbsp;.WordOpenXML&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;results&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26228" href="/site/view/file/26228/1/Word.DocumentInformationProperties.txt">Word.DocumentInformationProperties.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26229" href="/site/view/file/26229/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
