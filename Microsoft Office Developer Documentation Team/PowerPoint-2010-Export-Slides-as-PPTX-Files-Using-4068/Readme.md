# PowerPoint 2010: Export Slides as PPTX Files Using PPT.PublishSlides
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Export slides
## IsPublished
* True
## ModifiedDate
* 2011-08-05 01:21:56
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to export each slide in a Microsoft PowerPoint 2010 presentation as a separate PPTX file, in order to view them in a browser.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Demonstrate the PublishSlides method of the Presentation and SlideRange classes. The documentation indicates that this method creates a Web presentation in HTML format. Not so--it simply exports each slide as a separate PPTX
 file, so that you can view them in a browser if you like.&nbsp; The separate files are named PublishSlides_001.pptx, PublishSlides_002.pptx, and so on. The final number represents the position of the slide within the presentation.</span></p>
<p><span style="font-size:small">Copy the following code into a module in an existing, previously saved PowerPoint presentation. (The PublishSlides method fails silently if you haven't already saved the presentation.) Modify the PublishSlidesDemo procedure
 to show off the features you want to examine, and press F5 to run the procedure. Look in the folder you specify in the libraryUrl constant to see the published slides.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub PublishSlidesDemo()
    ' Demonstrate the Presentation.PublishSlides method.
   
    ' The library URL can either be a file system path, or
    ' a link to a SharePoint Slide Library. For this demonstration,
    ' use a local file path.
   
    ' Modify this path to meet your own needs:
    Const libraryUrl As String = &quot;C:\Temp\&quot;
    ' Publish all the slides:
    PublishSlideRange libraryUrl, True
   
    ' Publish a range of slides:
    ' PublishSlideRange libraryUrl, True, 2, 5
End Sub

Sub PublishSlideRange(libraryUrl As String, Optional OverWrite As Boolean = True, _
Optional startSlide As Variant, Optional endSlide As Variant)
   
    If IsMissing(startSlide) And IsMissing(endSlide) Then
        ' Neither endpoint specified. Use ActivePresentation.PublishSlides
        ActivePresentation.PublishSlides libraryUrl, OverWrite
    Else
        If IsMissing(startSlide) Then
            startSlide = 1
        End If
        If IsMissing(endSlide) Then
            endSlide = ActivePresentation.Slides.Count
        End If
       
        ' Make sure the values are in a reasonable range:
        If startSlide &lt; 1 Then
            startSlide = 1
        End If
        If endSlide &gt; ActivePresentation.Slides.Count Then
            endSlide = ActivePresentation.Slides.Count
        End If
       
        Dim rng As SlideRange
       
        ' Create an array containing a list of all the slides to publish:
        ReDim slidesToPublish(1 To (endSlide - startSlide &#43; 1)) As Integer
        Dim counter As Integer
        counter = 1
        Dim i As Integer
        For i = startSlide To endSlide
            slidesToPublish(counter) = i
            counter = counter &#43; 1
        Next i
       
        ' Given the array of slide numbers, publish the slides:
        Set rng = ActivePresentation.Slides.Range(slidesToPublish)
        rng.PublishSlides libraryUrl, OverWrite
    End If
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;PublishSlidesDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Demonstrate&nbsp;the&nbsp;Presentation.PublishSlides&nbsp;method.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;library&nbsp;URL&nbsp;can&nbsp;either&nbsp;be&nbsp;a&nbsp;file&nbsp;system&nbsp;path,&nbsp;or</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;link&nbsp;to&nbsp;a&nbsp;SharePoint&nbsp;Slide&nbsp;Library.&nbsp;For&nbsp;this&nbsp;demonstration,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;use&nbsp;a&nbsp;local&nbsp;file&nbsp;path.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;this&nbsp;path&nbsp;to&nbsp;meet&nbsp;your&nbsp;own&nbsp;needs:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;libraryUrl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;&quot;C:\Temp\&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Publish&nbsp;all&nbsp;the&nbsp;slides:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;PublishSlideRange&nbsp;libraryUrl,&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Publish&nbsp;a&nbsp;range&nbsp;of&nbsp;slides:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;PublishSlideRange&nbsp;libraryUrl,&nbsp;True,&nbsp;2,&nbsp;5</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;PublishSlideRange(libraryUrl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>,&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;OverWrite&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>,&nbsp;_&nbsp;
<span class="visualBasic__keyword">Optional</span>&nbsp;startSlide&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>,&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;endSlide&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Variant</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;IsMissing(startSlide)&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;IsMissing(endSlide)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Neither&nbsp;endpoint&nbsp;specified.&nbsp;Use&nbsp;ActivePresentation.PublishSlides</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.PublishSlides&nbsp;libraryUrl,&nbsp;OverWrite&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;IsMissing(startSlide)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;startSlide&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;IsMissing(endSlide)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endSlide&nbsp;=&nbsp;ActivePresentation.Slides.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;sure&nbsp;the&nbsp;values&nbsp;are&nbsp;in&nbsp;a&nbsp;reasonable&nbsp;range:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;startSlide&nbsp;&lt;&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;startSlide&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;endSlide&nbsp;&gt;&nbsp;ActivePresentation.Slides.Count&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endSlide&nbsp;=&nbsp;ActivePresentation.Slides.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;SlideRange&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;an&nbsp;array&nbsp;containing&nbsp;a&nbsp;list&nbsp;of&nbsp;all&nbsp;the&nbsp;slides&nbsp;to&nbsp;publish:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ReDim</span>&nbsp;slidesToPublish(<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;(endSlide&nbsp;-&nbsp;startSlide&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>))&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;counter&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;counter&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;startSlide&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;endSlide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;slidesToPublish(counter)&nbsp;=&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;counter&nbsp;=&nbsp;counter&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Given&nbsp;the&nbsp;array&nbsp;of&nbsp;slide&nbsp;numbers,&nbsp;publish&nbsp;the&nbsp;slides:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;ActivePresentation.Slides.Range(slidesToPublish)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;rng.PublishSlides&nbsp;libraryUrl,&nbsp;OverWrite&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26162" href="/site/view/file/26162/1/PPT.PublishSlides.txt">PPT.PublishSlides.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26163" href="/site/view/file/26163/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
