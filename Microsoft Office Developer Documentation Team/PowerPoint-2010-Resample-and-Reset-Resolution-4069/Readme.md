# PowerPoint 2010: Resample and Reset Resolution Using PPT.ResampleMedia
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
* 2011-08-05 03:15:02
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to resample and reset the resolution of a video in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Place this code into a module in a new presentation. On the first slide, add a video (on the Insert tab, in the Media Group, select Video and then Video from File). Play the video to ensure everything works, and then place your
 cursor in the following procedure. Single step through the code, if you like.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ResampleDemo()
    ' This code works through every shape on the
    ' first slide in the presentation, and for each
    ' video it finds, resamples the video to 240x320. If
    ' the resampling succeeds, the code resizes the video
    ' shape to match the new resolution.
    Dim shp As Shape
    For Each shp In ActivePresentation.Slides(1).Shapes
        ' Is it a media shape?
        If shp.Type = msoMedia Then
            Debug.Print &quot;Media Element: &quot; &amp; shp.Name
           
            Dim newWidth As Integer
            Dim newHeight As Integer
            newHeight = 240
            newWidth = 320
            ' You can specify other parameters, as well, indicating
            ' the video frame rate, the audio sampling rate, and the
            ' video bit rate. For now, just resample and reset
            ' the resolution--the lower the resolution, the smaller the video
            ' content.
            shp.MediaFormat.Resample True, newHeight, newWidth
            Do
                DoEvents
                Pause 1
                Debug.Print &quot;Resample status: &quot; &amp; shp.MediaFormat.ResamplingStatus
            Loop While shp.MediaFormat.ResamplingStatus = ppMediaTaskStatusInProgress
            Debug.Print &quot;Resample status: &quot; &amp; shp.MediaFormat.ResamplingStatus
            If shp.MediaFormat.ResamplingStatus = ppMediaTaskStatusDone Then
                shp.Width = newWidth
                shp.Height = newHeight
            End If
        End If
    Next shp
End Sub

Function Pause(numberOfSeconds As Variant)
    Dim startTime, endTime As Variant

    startTime = Timer
    endTime = startTime &#43; numberOfSeconds
  
    Do While Timer &lt; endTime
        DoEvents
    Loop
End Function
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ResampleDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;code&nbsp;works&nbsp;through&nbsp;every&nbsp;shape&nbsp;on&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;first&nbsp;slide&nbsp;in&nbsp;the&nbsp;presentation,&nbsp;and&nbsp;for&nbsp;each</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;video&nbsp;it&nbsp;finds,&nbsp;resamples&nbsp;the&nbsp;video&nbsp;to&nbsp;240x320.&nbsp;If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;resampling&nbsp;succeeds,&nbsp;the&nbsp;code&nbsp;resizes&nbsp;the&nbsp;video</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;shape&nbsp;to&nbsp;match&nbsp;the&nbsp;new&nbsp;resolution.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Is&nbsp;it&nbsp;a&nbsp;media&nbsp;shape?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;=&nbsp;msoMedia&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Media&nbsp;Element:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;newWidth&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;newHeight&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newHeight&nbsp;=&nbsp;<span class="visualBasic__number">240</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newWidth&nbsp;=&nbsp;<span class="visualBasic__number">320</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;specify&nbsp;other&nbsp;parameters,&nbsp;as&nbsp;well,&nbsp;indicating</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;video&nbsp;frame&nbsp;rate,&nbsp;the&nbsp;audio&nbsp;sampling&nbsp;rate,&nbsp;and&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;video&nbsp;bit&nbsp;rate.&nbsp;For&nbsp;now,&nbsp;just&nbsp;resample&nbsp;and&nbsp;reset</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;resolution--the&nbsp;lower&nbsp;the&nbsp;resolution,&nbsp;the&nbsp;smaller&nbsp;the&nbsp;video</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;content.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.MediaFormat.Resample&nbsp;<span class="visualBasic__keyword">True</span>,&nbsp;newHeight,&nbsp;newWidth&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DoEvents&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pause&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Resample&nbsp;status:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.MediaFormat.ResamplingStatus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Loop</span>&nbsp;<span class="visualBasic__keyword">While</span>&nbsp;shp.MediaFormat.ResamplingStatus&nbsp;=&nbsp;ppMediaTaskStatusInProgress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Resample&nbsp;status:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.MediaFormat.ResamplingStatus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.MediaFormat.ResamplingStatus&nbsp;=&nbsp;ppMediaTaskStatusDone&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Width&nbsp;=&nbsp;newWidth&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;shp.Height&nbsp;=&nbsp;newHeight&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;shp&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Function</span>&nbsp;Pause(numberOfSeconds&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;startTime,&nbsp;endTime&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;startTime&nbsp;=&nbsp;Timer&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;endTime&nbsp;=&nbsp;startTime&nbsp;&#43;&nbsp;numberOfSeconds&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;<span class="visualBasic__keyword">While</span>&nbsp;Timer&nbsp;&lt;&nbsp;endTime&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DoEvents&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Loop</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26164" href="/site/view/file/26164/1/PPT.ResampleMedia.txt">PPT.ResampleMedia.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26165" href="/site/view/file/26165/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
