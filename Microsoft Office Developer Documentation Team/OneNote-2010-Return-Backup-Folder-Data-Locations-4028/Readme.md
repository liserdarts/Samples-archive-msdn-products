# OneNote 2010: Return Backup Folder Data Locations Using GetSpecialLocation
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
* 2011-08-03 03:38:07
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>GetSpecialLocation
</strong>method to provide data about the location of the backups folder that Microsoft OneNote 2010 uses to store the automatic and manual backups of your OneNote files, and the default local file location for new notebooks.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Use any VBA host including Excel 2010, PowerPoint 2010, or Word 2010. OneNote 2010 is not a VBA host.</span></p>
<p><span style="font-size:small">In your VBA host, add references to the following external libraries using the Add References dialog:</span><br>
<span style="font-size:small">&nbsp;- Microsoft OneNote 14.0 Object Library</span><br>
<span style="font-size:small">&nbsp;- Microsoft XML, v6.0</span></p>
<p><span style="font-size:small">OneNote's GetSpecialLocation provides you with the location of two special folders for OneNote. One is the Backups folder that OneNote uses to store the automatic and manual backups of your OneNote files. The second is the default
 local file location for new OneNote notebooks. In addition, GetSpecialLocation provides you with the full path and filename of the Notebook used for unfiled notes.</span></p>
<p><span style="font-size:small">Paste all this code into a module, place the cursor within the GetSpecialLocationData procedure, and press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub GetSpecialLocationData()
    ' Connect to OneNote 2010.
    ' OneNote starts if it's not running.
    Dim oneNote As OneNote14.Application
    Set oneNote = New OneNote14.Application
    
    Dim backupFolder As String
    Dim defaultNotebookFolder As String
    Dim unfiledNotesSection As String
    
    oneNote.GetSpecialLocation slBackUpFolder, backupFolder
    oneNote.GetSpecialLocation slDefaultNotebookFolder, defaultNotebookFolder
    oneNote.GetSpecialLocation slUnfiledNotesSection, unfiledNotesSection
    
    Dim msg As String
    msg = &quot;OneNote 2010 Special Locations are: &quot; &amp; vbCrLf &amp; _
        &quot;Backup: &quot; &amp; vbCrLf &amp; &quot;  &quot; &amp; backupFolder &amp; vbCrLf &amp; _
        &quot;Default Notebook: &quot; &amp; vbCrLf &amp; &quot;  &quot; &amp; defaultNotebookFolder &amp; vbCrLf &amp; _
        &quot;Unfiled Notes: &quot; &amp; vbCrLf &amp; &quot;  &quot; &amp; unfiledNotesSection
    
    MsgBox msg, vbInformation, &quot;GetSpecialLocation&quot;
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;GetSpecialLocationData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Connect&nbsp;to&nbsp;OneNote&nbsp;2010.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;OneNote&nbsp;starts&nbsp;if&nbsp;it's&nbsp;not&nbsp;running.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;oneNote&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;oneNote&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;backupFolder&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;defaultNotebookFolder&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;unfiledNotesSection&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetSpecialLocation&nbsp;slBackUpFolder,&nbsp;backupFolder&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetSpecialLocation&nbsp;slDefaultNotebookFolder,&nbsp;defaultNotebookFolder&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetSpecialLocation&nbsp;slUnfiledNotesSection,&nbsp;unfiledNotesSection&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;msg&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;=&nbsp;<span class="visualBasic__string">&quot;OneNote&nbsp;2010&nbsp;Special&nbsp;Locations&nbsp;are:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;Backup:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;backupFolder&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;Default&nbsp;Notebook:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;defaultNotebookFolder&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;Unfiled&nbsp;Notes:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;unfiledNotesSection&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;msg,&nbsp;vbInformation,&nbsp;<span class="visualBasic__string">&quot;GetSpecialLocation&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25994" href="/site/view/file/25994/1/OneNote.fromVBA.GetSpecialLocationData.txt">OneNote.fromVBA.GetSpecialLocationData.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25995" href="/site/view/file/25995/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905452">OneNote Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
