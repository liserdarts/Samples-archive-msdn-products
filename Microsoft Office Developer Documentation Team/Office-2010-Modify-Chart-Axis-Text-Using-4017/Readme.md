# Office 2010: Modify Chart Axis Text Using Office.Chart.WorkWithAxisText
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
* 2011-08-03 03:22:33
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to modify a chart&rsquo;s axis text by manipulating various properties in Microsoft Office 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Microsoft Word 2010/Charting</span></p>
<p><span style="font-size:small">Modify the chart axis text.</span></p>
<p><span style="font-size:small">Add a module to the document, and copy the following code into the new module. Add a breakpoint after the call to the AddChart method and press F5 to run the procedure. After hitting the breakpoint, single step through the code
 (press F8 for each line) to see the behavior of the chart. This will be easiest to see if you arrange the Word window and the VBA window so they're side-by-side on the screen.</span></p>
<p><span style="font-size:small">You could easily modify this demonstration to work with PowerPoint: replace the reference to ActiveDocument with ActivePresentation.Slides(1).</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WorkWithAxisTitles()
    Dim shp As Shape
   
    ' Create a chart.
    Set shp = ActiveDocument.Shapes.AddChart(xlBarClustered)
  
    Dim cht As Chart
    ' You can check the shp.HasChart property to determine if
    ' the shape has a chart before continuing, but you can
    ' be sure this particular shape has a chart because you
    ' just created it.
    Set cht = shp.Chart
   
    ' Warning: The HasAxis method is strange, and requires
    ' you to read the documentation in order to call it.
    ' There is no way to infer its use from the VBA editor.
    cht.HasAxis(xlCategory) = True
   
    ' Warning: The Axes method returns an Object.
    ' If you want to make use of IntelliSense in the
    ' VBA editor, you must create an Axis variable, and
    ' assign it to the result of calling the Axes method.
    Dim ax As Axis
   
    ' Work with the category axis:
    Set ax = cht.Axes(xlCategory)
    With ax
        .CategoryType = xlAutomaticScale
        .MajorTickMark = xlInside
        .TickLabelPosition = xlTickLabelPositionNextToAxis
    End With
    SetTitleProperties ax, &quot;Categories&quot;
   
    ' Work with the value axis:
    cht.HasAxis(xlValue) = True
    Set ax = cht.Axes(xlValue)
    With ax
        .HasDisplayUnitLabel = False
        .DisplayUnit = xlCustom
        .DisplayUnitCustom = 500
        .HasTitle = True
        .AxisTitle.Caption = &quot;Milligrams&quot;
    End With
    SetTitleProperties ax, &quot;Values&quot;
End Sub

Sub SetTitleProperties(ax As Axis, title As String)
    With ax
        .HasTitle = True
        With .AxisTitle
            .Text = title
            With .Characters.Font
                .Size = 14
                .Color = xlRed
            End With
        End With
    End With
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithAxisTitles()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;chart.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;ActiveDocument.Shapes.AddChart(xlBarClustered)&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;check&nbsp;the&nbsp;shp.HasChart&nbsp;property&nbsp;to&nbsp;determine&nbsp;if</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;shape&nbsp;has&nbsp;a&nbsp;chart&nbsp;before&nbsp;continuing,&nbsp;but&nbsp;you&nbsp;can</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;be&nbsp;sure&nbsp;this&nbsp;particular&nbsp;shape&nbsp;has&nbsp;a&nbsp;chart&nbsp;because&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;just&nbsp;created&nbsp;it.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cht&nbsp;=&nbsp;shp.Chart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Warning:&nbsp;The&nbsp;HasAxis&nbsp;method&nbsp;is&nbsp;strange,&nbsp;and&nbsp;requires</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;to&nbsp;read&nbsp;the&nbsp;documentation&nbsp;in&nbsp;order&nbsp;to&nbsp;call&nbsp;it.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;There&nbsp;is&nbsp;no&nbsp;way&nbsp;to&nbsp;infer&nbsp;its&nbsp;use&nbsp;from&nbsp;the&nbsp;VBA&nbsp;editor.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.HasAxis(xlCategory)&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Warning:&nbsp;The&nbsp;Axes&nbsp;method&nbsp;returns&nbsp;an&nbsp;Object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;you&nbsp;want&nbsp;to&nbsp;make&nbsp;use&nbsp;of&nbsp;IntelliSense&nbsp;in&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;VBA&nbsp;editor,&nbsp;you&nbsp;must&nbsp;create&nbsp;an&nbsp;Axis&nbsp;variable,&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;assign&nbsp;it&nbsp;to&nbsp;the&nbsp;result&nbsp;of&nbsp;calling&nbsp;the&nbsp;Axes&nbsp;method.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ax&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Axis&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;category&nbsp;axis:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ax&nbsp;=&nbsp;cht.Axes(xlCategory)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ax&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.CategoryType&nbsp;=&nbsp;xlAutomaticScale&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.MajorTickMark&nbsp;=&nbsp;xlInside&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TickLabelPosition&nbsp;=&nbsp;xlTickLabelPositionNextToAxis&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SetTitleProperties&nbsp;ax,&nbsp;<span class="visualBasic__string">&quot;Categories&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;the&nbsp;value&nbsp;axis:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cht.HasAxis(xlValue)&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ax&nbsp;=&nbsp;cht.Axes(xlValue)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ax&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.HasDisplayUnitLabel&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.DisplayUnit&nbsp;=&nbsp;xlCustom&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.DisplayUnitCustom&nbsp;=&nbsp;<span class="visualBasic__number">500</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.HasTitle&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.AxisTitle.Caption&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Milligrams&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SetTitleProperties&nbsp;ax,&nbsp;<span class="visualBasic__string">&quot;Values&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;SetTitleProperties(ax&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Axis,&nbsp;title&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ax&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.HasTitle&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.AxisTitle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Text&nbsp;=&nbsp;title&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Characters.Font&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;<span class="visualBasic__number">14</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;xlRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25964" href="/site/view/file/25964/1/Office.Chart.WorkWithAxisText.txt">Office.Chart.WorkWithAxisText.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25965" href="/site/view/file/25965/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/">Office Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
