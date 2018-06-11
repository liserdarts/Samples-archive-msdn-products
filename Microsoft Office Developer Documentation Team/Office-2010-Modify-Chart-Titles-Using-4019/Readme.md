# Office 2010: Modify Chart Titles Using Office.Chart.WorkWithTitle
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
## Topics
* Charts
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:23:11
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to modify a chart&rsquo;s title by changing its orientation and color in Microsoft Office 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Microsoft Word 2010/Charting</span></p>
<p><span style="font-size:small">Modify the chart title.</span></p>
<p><span style="font-size:small">Add a module to the document, and copy the following code into the new module. Add a breakpoint after the call to the AddChart method and press F5 to run the procedure. After hitting the breakpoint, single step through the code
 (press F8 for each line) to see the behavior of the chart. This will be easiest to see if you arrange the Word window and the VBA window so they're side-by-side on the screen.</span></p>
<p><span style="font-size:small">You could easily modify this demonstration to work with PowerPoint by simply replacing the reference to ActiveDocument with ActivePresentation.Slides(1) and changing the enumerations that start with &quot;wd&quot; to start with &quot;mso&quot;
 instead.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WorkWithTitles()
    Dim shp As Shape
    Set shp = ActiveDocument.Shapes.AddChart(xlBarClustered)
   
    Dim cht As Chart
    ' You can check the shp.HasChart property to determine if
    ' the shape has a chart before continuing, but you can
    ' be sure this particular shape has a chart because you
    ' just created it.
    Set cht = shp.Chart
   
    ' Add a title.
    cht.HasTitle = True
    With cht.ChartTitle
        .Text = &quot;Sample Chart&quot;
       
        ' Set the orientation of the title, if you like.
        ' Horizontal is the default, but you can change it
        ' to something else.
        .Orientation = XlOrientation.xlHorizontal
       
        ' You can format individual characters, or
        ' the entire title:
        With .Characters(1, 1).Font
            .Size = 24
            .Color = xlRed
        End With
       
        .Characters(2).Font.Size = 14
       
        With .Format
            ' You can modify many other options.
            ' See the ChartFormat class for more information.
            .Fill.ForeColor.ObjectThemeColor = wdThemeColorAccent1
            With .Shadow
                .ForeColor.ObjectThemeColor = wdThemeColorAccent4
                .Style = msoShadowStyleOuterShadow
                .OffsetX = 4
                .OffsetY = 4
            End With
        End With
       
        ' Make sure and leave room for the title in the chart.
        .IncludeInLayout = True
    End With
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithTitles()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActiveDocument.Shapes.AddChart(xlBarClustered)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;check&nbsp;the&nbsp;shp.HasChart&nbsp;property&nbsp;to&nbsp;determine&nbsp;if</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;shape&nbsp;has&nbsp;a&nbsp;chart&nbsp;before&nbsp;continuing,&nbsp;but&nbsp;you&nbsp;can</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;be&nbsp;sure&nbsp;this&nbsp;particular&nbsp;shape&nbsp;has&nbsp;a&nbsp;chart&nbsp;because&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;just&nbsp;created&nbsp;it.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;shp.Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;title.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.HasTitle&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cht.ChartTitle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Sample&nbsp;Chart&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;orientation&nbsp;of&nbsp;the&nbsp;title,&nbsp;if&nbsp;you&nbsp;like.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Horizontal&nbsp;is&nbsp;the&nbsp;default,&nbsp;but&nbsp;you&nbsp;can&nbsp;change&nbsp;it</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;something&nbsp;else.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Orientation&nbsp;=&nbsp;XlOrientation.xlHorizontal&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;format&nbsp;individual&nbsp;characters,&nbsp;or</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;entire&nbsp;title:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Characters(<span class="visualBasic__number">1</span>,&nbsp;<span class="visualBasic__number">1</span>).Font&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">24</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;xlRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Characters(<span class="visualBasic__number">2</span>).Font.Size&nbsp;=&nbsp;<span class="visualBasic__number">14</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Format&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;modify&nbsp;many&nbsp;other&nbsp;options.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;See&nbsp;the&nbsp;ChartFormat&nbsp;class&nbsp;for&nbsp;more&nbsp;information.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Fill.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Shadow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ForeColor.ObjectThemeColor&nbsp;=&nbsp;wdThemeColorAccent4&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Style&nbsp;=&nbsp;msoShadowStyleOuterShadow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OffsetX&nbsp;=&nbsp;<span class="visualBasic__number">4</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OffsetY&nbsp;=&nbsp;<span class="visualBasic__number">4</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;sure&nbsp;and&nbsp;leave&nbsp;room&nbsp;for&nbsp;the&nbsp;title&nbsp;in&nbsp;the&nbsp;chart.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.IncludeInLayout&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25969" href="/site/view/file/25969/1/Office.Chart.WorkWithTitle.txt">Office.Chart.WorkWithTitle.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25970" href="/site/view/file/25970/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/">Office Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
