# PowerPoint 2010: Link Videos and Embedded Audio Files Using PPT.AddMedia
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
* 2011-08-04 04:50:01
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to link a video file and embedded audio file in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Start with an empty PowerPoint presentation. Add a module, and insert this code. Modify the two constants to represent two files in your file system. Put the cursor in the AddMedia procedure, and press F5 to run the code. You
 should end up with video and audio media elements on the first slide. Note the linking/embedding information in the Immediate window. Note that the new AddMediaObject2 method allows you to specify whether the media object is linked or embedded.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Const videoFileName As String = &quot;C:\Users\Public\Videos\Sample Videos\WildLife.wmv&quot;
Const audioFileName As String = &quot;C:\Users\Public\Music\Sample Music\Sleep Away.mp3&quot;
Sub AddMedia()
    With ActivePresentation.Slides(1).Shapes
        Dim shp As Shape
        ' Set only one of height and width--PowerPoint will maintain the correct
        ' aspect ratio for the video. This video is linked (as opposed to embedded).
        Set shp = .AddMediaObject2(videoFileName, msoTrue, msoFalse, 10, 10, 320)
        DisplayMediaInfo shp
       
        ' This audio is embedded, not linked.
        Set shp = .AddMediaObject2(audioFileName, msoFalse, msoTrue, 350, 10)
        DisplayMediaInfo shp
    End With
End Sub

Private Sub DisplayMediaInfo(shp As Shape)
    If shp.Type = msoMedia Then
        Debug.Print &quot;Embedded: &quot; &amp; shp.MediaFormat.IsEmbedded
        Debug.Print &quot;Linked: &quot; &amp; shp.MediaFormat.IsLinked
    End If
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Const</span>&nbsp;videoFileName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Users\Public\Videos\Sample&nbsp;Videos\WildLife.wmv&quot;</span>&nbsp;
<span class="visualBasic__keyword">Const</span>&nbsp;audioFileName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Users\Public\Music\Sample&nbsp;Music\Sleep&nbsp;Away.mp3&quot;</span>&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;AddMedia()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;only&nbsp;one&nbsp;of&nbsp;height&nbsp;and&nbsp;width--PowerPoint&nbsp;will&nbsp;maintain&nbsp;the&nbsp;correct</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;aspect&nbsp;ratio&nbsp;for&nbsp;the&nbsp;video.&nbsp;This&nbsp;video&nbsp;is&nbsp;linked&nbsp;(as&nbsp;opposed&nbsp;to&nbsp;embedded).</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;.AddMediaObject2(videoFileName,&nbsp;msoTrue,&nbsp;msoFalse,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">320</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayMediaInfo&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;audio&nbsp;is&nbsp;embedded,&nbsp;not&nbsp;linked.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;.AddMediaObject2(audioFileName,&nbsp;msoFalse,&nbsp;msoTrue,&nbsp;<span class="visualBasic__number">350</span>,&nbsp;<span class="visualBasic__number">10</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayMediaInfo&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DisplayMediaInfo(shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;=&nbsp;msoMedia&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Embedded:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.MediaFormat.IsEmbedded&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Linked:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.MediaFormat.IsLinked&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26111" href="/site/view/file/26111/1/PPT.AddMedia.txt">PPT.AddMedia.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26112" href="/site/view/file/26112/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
