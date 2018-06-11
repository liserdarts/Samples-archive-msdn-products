# PowerPoint 2010: Merge Two Decks into One Using PPT.MergeWithBaseline
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Merging decks
## IsPublished
* True
## ModifiedDate
* 2011-08-05 12:57:52
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to merge two modified Microsoft PowerPoint 2010 presentation decks into a single baseline deck.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">To test the Presentation.MergeWithBaseline method, you must first create three sample presentations. Start by creating Baseline.pptx, so that you end up a title slide and five other slides, titled:</span><br>
<br>
<span style="font-size:small">Title, Slide 1, Slide 2, Slide 3, Slide 4, Slide 5.</span><br>
<br>
<span style="font-size:small">Save Baseline.pptx as BaseLine1.pptx, and delete Slide 1, Slide 4, and Slide 5. In BaseLine1.pptx, modify Slide 3 to be Slide 3a. Once you're done, BaseLine1.pptx contains the following slides:</span><br>
<br>
<span style="font-size:small">Title, Slide 2, Slide 3a</span><br>
<br>
<span style="font-size:small">Close BaseLine1.pptx, and save BaseLine.pptx as BaseLine2.pptx. In BaseLine2.pptx, delete Slide 5, so that you end up with:</span><br>
<br>
<span style="font-size:small">Title, Slide 1, Slide 2, Slide 3, Slide 4</span><br>
<br>
<span style="font-size:small">Run the following code, which demonstrates the Presentation.MergeWithBaseLine, AcceptAll, RejectAll, and InMergeMode members. If you choose to reject all modifications, you won't be able to test the results of the merge.</span><br>
&nbsp;&nbsp;&nbsp;<br>
<span style="font-size:small">Place this code in a module in BaseLine2.pptx, and press F8 with the selection within this procedure to run the code and watch what's going on within PowerPoint.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestMergeWithBaseline()

    ' Fix up these constants to match your own environment:
    Const withPresentation As String = &quot;C:\Temp\BaseLine1.pptx&quot;
    Const baseLinePresentation As String = &quot;C:\Temp\BaseLine.pptx&quot;
       
    If ActivePresentation.InMergeMode Then
        ' If you're currently in Merge mode, you must
        ' end the review before starting the merge.
        ActivePresentation.EndReview
    Else
   
    ActivePresentation.MergeWithBaseline withPresentation, baseLinePresentation
    ' If single-stepping, look now in PowerPoint to see what it has done.
    ' On Slide 1, you'll see an indicator that the slide has been deleted.
    ' On Slide 3, you'll see an indicator that the title has been changed.
    ' In the slide tray, you'll see an indicator that Slide 4 has been deleted and
    ' after Slide 4, you'll see an indicator that Slide 5 has been deleted.
        If ActivePresentation.InMergeMode Then
            ' In case you ended merge mode in the user interface,
            ' there's nothing to do here. Can't hurt to check.
            If MsgBox(&quot;Accept all changes?&quot;, vbOKCancel, &quot;MergeWithBaseline&quot;) = vbOK Then
                ActivePresentation.AcceptAll
            Else
                ActivePresentation.RejectAll
            End If
        End If
    End If
    ' If you accepted revisions, in PowerPoint you can now see the Revision pane
    ' indicating the changes:
    ' Slide 1 was deleted.
    ' Slide 3 has a title change.
    ' Slide 4 and Slide 5 were deleted.
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestMergeWithBaseline()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fix&nbsp;up&nbsp;these&nbsp;constants&nbsp;to&nbsp;match&nbsp;your&nbsp;own&nbsp;environment:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;withPresentation&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Temp\BaseLine1.pptx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Const</span>&nbsp;baseLinePresentation&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;C:\Temp\BaseLine.pptx&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;ActivePresentation.InMergeMode&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you're&nbsp;currently&nbsp;in&nbsp;Merge&nbsp;mode,&nbsp;you&nbsp;must</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;end&nbsp;the&nbsp;review&nbsp;before&nbsp;starting&nbsp;the&nbsp;merge.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.EndReview&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.MergeWithBaseline&nbsp;withPresentation,&nbsp;baseLinePresentation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;single-stepping,&nbsp;look&nbsp;now&nbsp;in&nbsp;PowerPoint&nbsp;to&nbsp;see&nbsp;what&nbsp;it&nbsp;has&nbsp;done.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;On&nbsp;Slide&nbsp;1,&nbsp;you'll&nbsp;see&nbsp;an&nbsp;indicator&nbsp;that&nbsp;the&nbsp;slide&nbsp;has&nbsp;been&nbsp;deleted.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;On&nbsp;Slide&nbsp;3,&nbsp;you'll&nbsp;see&nbsp;an&nbsp;indicator&nbsp;that&nbsp;the&nbsp;title&nbsp;has&nbsp;been&nbsp;changed.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;the&nbsp;slide&nbsp;tray,&nbsp;you'll&nbsp;see&nbsp;an&nbsp;indicator&nbsp;that&nbsp;Slide&nbsp;4&nbsp;has&nbsp;been&nbsp;deleted&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;after&nbsp;Slide&nbsp;4,&nbsp;you'll&nbsp;see&nbsp;an&nbsp;indicator&nbsp;that&nbsp;Slide&nbsp;5&nbsp;has&nbsp;been&nbsp;deleted.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;ActivePresentation.InMergeMode&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;case&nbsp;you&nbsp;ended&nbsp;merge&nbsp;mode&nbsp;in&nbsp;the&nbsp;user&nbsp;interface,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;there's&nbsp;nothing&nbsp;to&nbsp;do&nbsp;here.&nbsp;Can't&nbsp;hurt&nbsp;to&nbsp;check.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;MsgBox(<span class="visualBasic__string">&quot;Accept&nbsp;all&nbsp;changes?&quot;</span>,&nbsp;vbOKCancel,&nbsp;<span class="visualBasic__string">&quot;MergeWithBaseline&quot;</span>)&nbsp;=&nbsp;vbOK&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.AcceptAll&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ActivePresentation.RejectAll&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you&nbsp;accepted&nbsp;revisions,&nbsp;in&nbsp;PowerPoint&nbsp;you&nbsp;can&nbsp;now&nbsp;see&nbsp;the&nbsp;Revision&nbsp;pane</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;indicating&nbsp;the&nbsp;changes:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Slide&nbsp;1&nbsp;was&nbsp;deleted.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Slide&nbsp;3&nbsp;has&nbsp;a&nbsp;title&nbsp;change.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Slide&nbsp;4&nbsp;and&nbsp;Slide&nbsp;5&nbsp;were&nbsp;deleted.</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26156" href="/site/view/file/26156/1/PPT.MergeWithBaseline.txt">PPT.MergeWithBaseline.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26157" href="/site/view/file/26157/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
