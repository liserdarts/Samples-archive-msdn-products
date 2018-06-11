# PowerPoint 2010: Display Media Control Properties
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* media controls
## IsPublished
* True
## ModifiedDate
* 2011-08-05 03:19:17
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to&nbsp; display information about various properties of a media control in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Place the following code in a standard module.</span></p>
<p><span style="font-size:small">In PowerPoint, delete all the shapes from the first slide, and then insert a video file into the slide.</span></p>
<p><span style="font-size:small">In PowerPoint, press F5 to start the slide show. On the slide show starts, wait a few seconds for the media to load, then click on the media to ensure that the media controls have loaded. While the video plays, use Windows'
 Alt-Tab key combination to come back to this window, and press F8 within this procedure to step through the code. When the media controls are visible, you can gather information about them. When they're not, every property returns 0.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Public Sub WorkWithMediaControls()
    With ActivePresentation.SlideShowWindow.View
        ActivePresentation.SlideShowSettings.ShowMediaControls = msoTrue
        Debug.Print &quot;MediaControlsWidth = &quot; &amp; .MediaControlsWidth
        Debug.Print &quot;MediaControlsHeight = &quot; &amp; .MediaControlsHeight
        Debug.Print &quot;MediaControlsLeft = &quot; &amp; .MediaControlsLeft
        Debug.Print &quot;MediaControlsTop = &quot; &amp; .MediaControlsTop
        Debug.Print &quot;MediaControlsVisible = &quot; &amp; .MediaControlsVisible
    End With
  
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithMediaControls()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ActivePresentation.SlideShowWindow.View&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.SlideShowSettings.ShowMediaControls&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;MediaControlsWidth&nbsp;=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.MediaControlsWidth&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;MediaControlsHeight&nbsp;=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.MediaControlsHeight&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;MediaControlsLeft&nbsp;=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.MediaControlsLeft&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;MediaControlsTop&nbsp;=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.MediaControlsTop&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;MediaControlsVisible&nbsp;=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.MediaControlsVisible&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26166" href="/site/view/file/26166/1/PPT.RetrieveInformationAboutMediaControls.txt">PPT.RetrieveInformationAboutMediaControls.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26167" href="/site/view/file/26167/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
