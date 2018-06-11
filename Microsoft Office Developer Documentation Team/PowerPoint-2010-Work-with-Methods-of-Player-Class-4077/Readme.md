# PowerPoint 2010: Work with Methods of Player Class Using PPT.WorkWithMediaPlayer
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* video
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 03:47:46
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use various methods of the
<strong>Player </strong>class in a Microsoft PowerPoint 2010 presentation which contains a video.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Place this code into a module in a new presentation. On the first slide, add a video (on the Insert tab, in the Media Group, select Video and then Video from File). Play the video to ensure everything works, and then place your
 cursor in the TestPlayer procedure. Single step through the code, if you like.</span></p>
<p><span style="font-size:small">Arrange the VBA window and the PowerPoint window so you can see both simultaneously.</span></p>
<p><span style="font-size:small">Once you have run the TestPlayer procedure, put the cursor in any of the other procedures, and press F5 to run the selected procedure. Each procedure calls one of the methods of the Player class. (You could, of course, create
 action buttons on the presentation to accomplish this same goal, but it's easier for demonstration purposes to simply run the procedures directly.)</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Private shapeID As Integer
Private plyr As Player

Sub TestPlayer()
    Dim shp As Shape
    For Each shp In ActivePresentation.Slides(1).Shapes
        ' Find the first media shape on the slide:
        If shp.Type = msoMedia Then
            shapeID = shp.id
            Exit For
        End If
    Next shp
   
    ' Add some bookmarks to the media. Delete all existing bookmarks
    ' first, so you can execute this code multiple times.
    Dim i As Integer
    With shp.MediaFormat
        For i = .MediaBookmarks.Count To 1 Step -1
            .MediaBookmarks(i).Delete
        Next i
    End With
   
    With shp.MediaFormat.MediaBookmarks
        .Add 1000, &quot;Bookmark 1&quot;
        .Add 5000, &quot;Bookmark 2&quot;
        .Add 8000, &quot;Bookmark 3&quot;
    End With
   
    ' Store away a reference to the Player instance.
    Set plyr = Application.Windows(1).View.Player(shapeID)
End Sub

Sub Play()
    plyr.Play
End Sub

Sub Pause()
    plyr.Pause
End Sub

Sub GotoNextBookmark()
    plyr.GotoNextBookmark
End Sub

Sub GotoPreviousBookmark()
    plyr.GotoPreviousBookmark
End Sub

Sub GotoPosition()
    plyr.CurrentPosition = 4000
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Private</span>&nbsp;shapeID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;plyr&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Player&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;TestPlayer()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Find&nbsp;the&nbsp;first&nbsp;media&nbsp;shape&nbsp;on&nbsp;the&nbsp;slide:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;=&nbsp;msoMedia&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shapeID&nbsp;=&nbsp;shp.id&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;some&nbsp;bookmarks&nbsp;to&nbsp;the&nbsp;media.&nbsp;Delete&nbsp;all&nbsp;existing&nbsp;bookmarks</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;first,&nbsp;so&nbsp;you&nbsp;can&nbsp;execute&nbsp;this&nbsp;code&nbsp;multiple&nbsp;times.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.MediaFormat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;.MediaBookmarks.Count&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Step</span>&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.MediaBookmarks(i).Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.MediaFormat.MediaBookmarks&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Add&nbsp;<span class="visualBasic__number">1000</span>,&nbsp;<span class="visualBasic__string">&quot;Bookmark&nbsp;1&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Add&nbsp;<span class="visualBasic__number">5000</span>,&nbsp;<span class="visualBasic__string">&quot;Bookmark&nbsp;2&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Add&nbsp;<span class="visualBasic__number">8000</span>,&nbsp;<span class="visualBasic__string">&quot;Bookmark&nbsp;3&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Store&nbsp;away&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;Player&nbsp;instance.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;plyr&nbsp;=&nbsp;Application.Windows(<span class="visualBasic__number">1</span>).View.Player(shapeID)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;Play()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;plyr.Play&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;Pause()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;plyr.Pause&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;GotoNextBookmark()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;plyr.GotoNextBookmark&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;GotoPreviousBookmark()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;plyr.GotoPreviousBookmark&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;GotoPosition()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;plyr.CurrentPosition&nbsp;=&nbsp;<span class="visualBasic__number">4000</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26181" href="/site/view/file/26181/1/PPT.WorkWithMediaPlayer.txt">PPT.WorkWithMediaPlayer.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26182" href="/site/view/file/26182/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
