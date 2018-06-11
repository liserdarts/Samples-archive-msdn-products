# PowerPoint 2010: Create Videos Programmatically
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
* 2011-07-26 01:59:25
## Description

<h1>Introduction</h1>
<p>This sample shows how to create videos in Microsoft Office 2010 programmatically using Microsoft Visual Basic for Applications (VBA). This code snippet is part of the
<strong>Office 2010 101 code samples project</strong>. This sample, along with others, are offered here to incorporate directly in your code.</p>
<p>Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample&nbsp;includes comments describing the sample, and setup code so
 that&nbsp;you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</p>
<p class="Text">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions or as a
 starting point to create more complex solutions.</p>
<h1><span>Building the Sample</span></h1>
<p>Place this code in a module within a PowerPoint 2010 presentation&nbsp;and run the&nbsp;test procedure.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">' PowerPoint 2010

' Place this code in a module within a PowerPoint 2010 presentation,
' and run the following test procedure. Modify the output file name
' as necessary:
Sub TestCreateSampleVideo()
    CreateSampleVideo ActivePresentation, &quot;C:\Temp\Video.wmv&quot;
End Sub

Sub CreateSampleVideo(pres As Presentation, fileName As String)
    ' Presentation.CreateVideo does its work asynchronously.
    ' You can use the Presentation.CreateVideoStatus property
    ' to periodically check the status, and react accordingly.
   
    ' Besides the file name, the CreateVideo method accepts the following
    ' parameters:
   
    ' UseTimingsAndNarration indicates whether to use the presentation's
    '   timings and narrations that you have supplied. If false, the
    '   conversion disregards this information. The default is True.
    ' DefaultSlideDuration indicates the default timing for each slide,
    '   if you have haven't specified a timing or if you set
    '   UseTimingsAndNarration to false. The default value is 5 seconds.
    ' VertResolution indicates the vertical resolution for your movie. The
    '   default is 720. Regular options include 720, 480, and 240, although you
    '   can specify any reasonable value you like (200 would work, for example,
    '   although it's not a standard vertical resolution.)
    ' FramesPerSecond indicates the number of frames per second in the
    '   output video. The default value is 30, and unless you have a reason
    '   to change this, leave it alone.
    ' Quality indicates a relative quality of the video, and the default
    '   value is 85. The larger the number, the larger the output and the longer
    '   it takes to create the video. Unless you have a reason, leave this
    '   at the default value. Try setting the value to a low number: You'll see
    '   a definite degradation in output quality.
   
    pres.CreateVideo fileName, DefaultSlideDuration:=1, VertResolution:=480
   
    ' Now wait for the conversion to be complete:
    Do
        ' Don't tie up the user interface, add DoEvents
        ' to give the mouse and keyboard time to keep up.
        DoEvents
        Select Case pres.CreateVideoStatus
            Case PpMediaTaskStatus.ppMediaTaskStatusDone
                MsgBox &quot;Conversion complete!&quot;
                Exit Do
            Case PpMediaTaskStatus.ppMediaTaskStatusFailed
                MsgBox &quot;Conversion failed!&quot;
                Exit Do
            Case PpMediaTaskStatus.ppMediaTaskStatusInProgress
                Debug.Print &quot;Conversion in progress&quot;
            Case PpMediaTaskStatus.ppMediaTaskStatusNone
                ' This shouldn't happen--you'll get this value
                ' when you ask for the status and no conversion
                ' is happening or has completed.
            Case PpMediaTaskStatus.ppMediaTaskStatusQueued
                Debug.Print &quot;Conversion queued&quot;
        End Select
    Loop
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__com">'&nbsp;PowerPoint&nbsp;2010</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;Place&nbsp;this&nbsp;code&nbsp;in&nbsp;a&nbsp;module&nbsp;within&nbsp;a&nbsp;PowerPoint&nbsp;2010&nbsp;presentation,</span>&nbsp;
<span class="visualBasic__com">'&nbsp;and&nbsp;run&nbsp;the&nbsp;following&nbsp;test&nbsp;procedure.&nbsp;Modify&nbsp;the&nbsp;output&nbsp;file&nbsp;name</span>&nbsp;
<span class="visualBasic__com">'&nbsp;as&nbsp;necessary:</span>&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;TestCreateSampleVideo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CreateSampleVideo&nbsp;ActivePresentation,&nbsp;<span class="visualBasic__string">&quot;C:\Temp\Video.wmv&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;CreateSampleVideo(pres&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Presentation,&nbsp;fileName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Presentation.CreateVideo&nbsp;does&nbsp;its&nbsp;work&nbsp;asynchronously.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;use&nbsp;the&nbsp;Presentation.CreateVideoStatus&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;periodically&nbsp;check&nbsp;the&nbsp;status,&nbsp;and&nbsp;react&nbsp;accordingly.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Besides&nbsp;the&nbsp;file&nbsp;name,&nbsp;the&nbsp;CreateVideo&nbsp;method&nbsp;accepts&nbsp;the&nbsp;following</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;parameters:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;UseTimingsAndNarration&nbsp;indicates&nbsp;whether&nbsp;to&nbsp;use&nbsp;the&nbsp;presentation's</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;timings&nbsp;and&nbsp;narrations&nbsp;that&nbsp;you&nbsp;have&nbsp;supplied.&nbsp;If&nbsp;false,&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;conversion&nbsp;disregards&nbsp;this&nbsp;information.&nbsp;The&nbsp;default&nbsp;is&nbsp;True.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;DefaultSlideDuration&nbsp;indicates&nbsp;the&nbsp;default&nbsp;timing&nbsp;for&nbsp;each&nbsp;slide,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;if&nbsp;you&nbsp;have&nbsp;haven't&nbsp;specified&nbsp;a&nbsp;timing&nbsp;or&nbsp;if&nbsp;you&nbsp;set</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;UseTimingsAndNarration&nbsp;to&nbsp;false.&nbsp;The&nbsp;default&nbsp;value&nbsp;is&nbsp;5&nbsp;seconds.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;VertResolution&nbsp;indicates&nbsp;the&nbsp;vertical&nbsp;resolution&nbsp;for&nbsp;your&nbsp;movie.&nbsp;The</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;default&nbsp;is&nbsp;720.&nbsp;Regular&nbsp;options&nbsp;include&nbsp;720,&nbsp;480,&nbsp;and&nbsp;240,&nbsp;although&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;can&nbsp;specify&nbsp;any&nbsp;reasonable&nbsp;value&nbsp;you&nbsp;like&nbsp;(200&nbsp;would&nbsp;work,&nbsp;for&nbsp;example,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;although&nbsp;it's&nbsp;not&nbsp;a&nbsp;standard&nbsp;vertical&nbsp;resolution.)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;FramesPerSecond&nbsp;indicates&nbsp;the&nbsp;number&nbsp;of&nbsp;frames&nbsp;per&nbsp;second&nbsp;in&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;output&nbsp;video.&nbsp;The&nbsp;default&nbsp;value&nbsp;is&nbsp;30,&nbsp;and&nbsp;unless&nbsp;you&nbsp;have&nbsp;a&nbsp;reason</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;to&nbsp;change&nbsp;this,&nbsp;leave&nbsp;it&nbsp;alone.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Quality&nbsp;indicates&nbsp;a&nbsp;relative&nbsp;quality&nbsp;of&nbsp;the&nbsp;video,&nbsp;and&nbsp;the&nbsp;default</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;value&nbsp;is&nbsp;85.&nbsp;The&nbsp;larger&nbsp;the&nbsp;number,&nbsp;the&nbsp;larger&nbsp;the&nbsp;output&nbsp;and&nbsp;the&nbsp;longer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;it&nbsp;takes&nbsp;to&nbsp;create&nbsp;the&nbsp;video.&nbsp;Unless&nbsp;you&nbsp;have&nbsp;a&nbsp;reason,&nbsp;leave&nbsp;this</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;at&nbsp;the&nbsp;default&nbsp;value.&nbsp;Try&nbsp;setting&nbsp;the&nbsp;value&nbsp;to&nbsp;a&nbsp;low&nbsp;number:&nbsp;You'll&nbsp;see</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;a&nbsp;definite&nbsp;degradation&nbsp;in&nbsp;output&nbsp;quality.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pres.CreateVideo&nbsp;fileName,&nbsp;DefaultSlideDuration:=<span class="visualBasic__number">1</span>,&nbsp;VertResolution:=<span class="visualBasic__number">480</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;wait&nbsp;for&nbsp;the&nbsp;conversion&nbsp;to&nbsp;be&nbsp;complete:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Don't&nbsp;tie&nbsp;up&nbsp;the&nbsp;user&nbsp;interface,&nbsp;add&nbsp;DoEvents</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;give&nbsp;the&nbsp;mouse&nbsp;and&nbsp;keyboard&nbsp;time&nbsp;to&nbsp;keep&nbsp;up.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DoEvents&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;pres.CreateVideoStatus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PpMediaTaskStatus.ppMediaTaskStatusDone&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;Conversion&nbsp;complete!&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PpMediaTaskStatus.ppMediaTaskStatusFailed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;Conversion&nbsp;failed!&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">Do</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PpMediaTaskStatus.ppMediaTaskStatusInProgress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Conversion&nbsp;in&nbsp;progress&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PpMediaTaskStatus.ppMediaTaskStatusNone&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;shouldn't&nbsp;happen--you'll&nbsp;get&nbsp;this&nbsp;value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;when&nbsp;you&nbsp;ask&nbsp;for&nbsp;the&nbsp;status&nbsp;and&nbsp;no&nbsp;conversion</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;is&nbsp;happening&nbsp;or&nbsp;has&nbsp;completed.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;PpMediaTaskStatus.ppMediaTaskStatusQueued&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Conversion&nbsp;queued&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Loop</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code File</span></h1>
<p><em><a id="25426" href="/site/view/file/25426/1/01.Samples.PPT.CreateVideo.txt">01.Samples.PPT.CreateVideo.txt</a>. Download this sample only.</em></p>
<h1>More Information</h1>
<ul>
<li>For more information about developing for PowerPoint, see the MSDN PowerPoint:
<a href="http://msdn.microsoft.com/en-us/office/aa905467.aspx">http://msdn.microsoft.com/en-us/office/aa905467.aspx</a>
</li><li>You can also find more downloads at the MSDN Office Developer Center download page:
<a href="http://msdn.microsoft.com/en-us/office/aa905351">http://msdn.microsoft.com/en-us/office/aa905351</a>
</li></ul>
