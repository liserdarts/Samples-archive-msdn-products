# PowerPoint 2010: Modify Aspects of Videos Using PPT.MediaFormatProperties
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
* 2011-08-05 12:48:49
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to modify various aspects of a video in a Microsoft PowerPoint 2010 presentation by using the
<strong>MediaFormat </strong>property and how to list several of the video&rsquo;s properties settings.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Place the following code in a new presentation. On the first slide, add a video (on the Insert tab, in the Media Group, select Video and then Video from File). Play the video to ensure everything works, and then place your cursor
 in the following procedure. Single step through the code, if you like. The values of the properties will appear in the Immediate window. The code changes the starting and ending points of the video (modify these values if your video isn't longer than 5 seconds),
 and changes the volume and fade in/out duration.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub MediaFormatInfo()
    Dim shp As Shape
    For Each shp In ActivePresentation.Slides(1).Shapes
        ' Is it a media shape?
        If shp.Type = msoMedia Then
            Debug.Print &quot;Media Element: &quot; &amp; shp.Name
            DisplayMediaFormatInfo shp
           
            ' Work with the MediaFormat property:
            With shp.MediaFormat
                ' Modify the values that can be modified:
                ' Start at 2 seconds, end at 5 seconds,
                ' fade in and out for 1/2 second:
                .StartPoint = 2000
                .EndPoint = 5000
                .FadeInDuration = 500
                .FadeOutDuration = 500
               
                ' Ensure the video isn't muted, and
                ' that the volume is less than the max:
                .Muted = False
                .Volume = 0.25
            End With
        Debug.Print &quot;================&quot;
        DisplayMediaFormatInfo shp
        End If
    Next shp
End Sub

Private Sub DisplayMediaFormatInfo(shp As Shape)
    With shp.MediaFormat
        ' Retrieve all the useful properties:
        Debug.Print vbTab &amp; &quot;AudioCompressionType: &quot; &amp; .AudioCompressionType
        Debug.Print vbTab &amp; &quot;AudioSamplingRate: &quot; &amp; .AudioSamplingRate
        Debug.Print vbTab &amp; &quot;EndPoint: &quot; &amp; .EndPoint
        Debug.Print vbTab &amp; &quot;FadeInDuration: &quot; &amp; .FadeInDuration
        Debug.Print vbTab &amp; &quot;FadeOutDuration: &quot; &amp; .FadeOutDuration
        Debug.Print vbTab &amp; &quot;IsEmbedded: &quot; &amp; .IsEmbedded
        Debug.Print vbTab &amp; &quot;IsLinked: &quot; &amp; .IsLinked
        Debug.Print vbTab &amp; &quot;Length: &quot; &amp; .Length
        Debug.Print vbTab &amp; &quot;Muted: &quot; &amp; .Muted
        Debug.Print vbTab &amp; &quot;ResamplingStatus: &quot; &amp; .ResamplingStatus
        Debug.Print vbTab &amp; &quot;SampleHeight: &quot; &amp; .SampleHeight
        Debug.Print vbTab &amp; &quot;SampleWidth: &quot; &amp; .SampleWidth
        Debug.Print vbTab &amp; &quot;StartPoint: &quot; &amp; .StartPoint
        Debug.Print vbTab &amp; &quot;VideoCompressionType: &quot; &amp; .VideoCompressionType
        Debug.Print vbTab &amp; &quot;VideoFrameRate: &quot; &amp; .VideoFrameRate
        Debug.Print vbTab &amp; &quot;Volume: &quot; &amp; .Volume
    End With
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;MediaFormatInfo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>).Shapes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Is&nbsp;it&nbsp;a&nbsp;media&nbsp;shape?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;shp.Type&nbsp;=&nbsp;msoMedia&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Media&nbsp;Element:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;shp.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayMediaFormatInfo&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;MediaFormat&nbsp;property:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.MediaFormat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;the&nbsp;values&nbsp;that&nbsp;can&nbsp;be&nbsp;modified:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Start&nbsp;at&nbsp;2&nbsp;seconds,&nbsp;end&nbsp;at&nbsp;5&nbsp;seconds,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;fade&nbsp;in&nbsp;and&nbsp;out&nbsp;for&nbsp;1/2&nbsp;second:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.StartPoint&nbsp;=&nbsp;<span class="visualBasic__number">2000</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.EndPoint&nbsp;=&nbsp;<span class="visualBasic__number">5000</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FadeInDuration&nbsp;=&nbsp;<span class="visualBasic__number">500</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FadeOutDuration&nbsp;=&nbsp;<span class="visualBasic__number">500</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Ensure&nbsp;the&nbsp;video&nbsp;isn't&nbsp;muted,&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;that&nbsp;the&nbsp;volume&nbsp;is&nbsp;less&nbsp;than&nbsp;the&nbsp;max:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Muted&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Volume&nbsp;=&nbsp;<span class="visualBasic__number">0.25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;================&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DisplayMediaFormatInfo&nbsp;shp&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;shp&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DisplayMediaFormatInfo(shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;shp.MediaFormat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;all&nbsp;the&nbsp;useful&nbsp;properties:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;AudioCompressionType:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.AudioCompressionType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;AudioSamplingRate:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.AudioSamplingRate&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;EndPoint:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.EndPoint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;FadeInDuration:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.FadeInDuration&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;FadeOutDuration:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.FadeOutDuration&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;IsEmbedded:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.IsEmbedded&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;IsLinked:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.IsLinked&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Length:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.Length&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Muted:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.Muted&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;ResamplingStatus:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.ResamplingStatus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;SampleHeight:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.SampleHeight&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;SampleWidth:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.SampleWidth&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;StartPoint:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.StartPoint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;VideoCompressionType:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.VideoCompressionType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;VideoFrameRate:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.VideoFrameRate&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;vbTab&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Volume:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.Volume&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26154" href="/site/view/file/26154/1/PPT.MediaFormatProperties.txt">PPT.MediaFormatProperties.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26155" href="/site/view/file/26155/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
