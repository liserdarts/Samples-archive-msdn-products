# Excel 2010: Apply Conditional Formatting Using the Excel.DataBar Method
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* databars
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:28:27
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to work with data bars in Microsoft Excel 2010 to include fill, axis position, and the color of negative and positive values.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, copy this code into the Sheet1 class. Place the cursor inside the TestDataBars procedure, and press F8 to single-step through the code. Place the VBA and Excel windows side by side so you
 can see the results of running the code as you step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestDataBars()
    ' Demonstrate DataBar:
    '   BarFillType               
    '   AxisPosition
    '   AxisColor
    '   Direction
    '   NegativeBarFormat
    '   BarBorder

    Dim rng As Range
    Set rng = FillRange(-25, 25, 20)
   
    rng.FormatConditions.Delete
   
    Dim db As dataBar
    Set db = rng.FormatConditions.AddDatabar
    ' Set the endpoints for the data bars:
    db.MinPoint.Modify xlConditionValueNumber, -25
    db.MaxPoint.Modify xlConditionValueNumber, 25
   
    ' Set the axis position.
    ' The options are automatic, midpoint, or none.
    ' By default, if there are negative values, the
    ' axis will appear in the middle. Force it to
    ' use the midpoint:
    db.AxisPosition = xlDataBarAxisMidpoint
   
    ' You can use a gradient or a solid fill type.
    db.BarFillType = xlDataBarFillSolid
    Dim fc As FormatColor
    With db.BarColor
        .Color = vbBlue
        .TintAndShade = -0.2
    End With
   
    ' Modify the behavior of positive and negative
    ' bar borders:
    With db.BarBorder
        .Type = xlDataBarBorderSolid
        ' Unfortunately, the BarBorder.Color property returns
        ' a ColorFormat object, so you'll end up setting
        ' the Color property of the Color property:
        .Color.Color = vbGreen
    End With
   
    ' Don't be mislead: The AxisColor property is read-only.
    ' But it is, itself, a FormatColor object. Therefore, the
    ' reference to the FormatColor object is read-only, but the
    ' properties of that object are not read-only. You can
    ' modify any of the FormatColor object's properties:
    db.AxisColor.Color = vbRed
   
    ' You can set the direction of the bars.
    ' The default value is xlContext, which uses the
    ' direction of the current locale. If you want to
    ' force right-to-left direction (in other words, force
    ' the values to be reversed for left-to-right languages such
    ' as English), specify xlRTL. To do the same for RTL
    ' language such as Arabic or Hebrew, specify xlLTR.
    ' The following forces the negative/positive values to
    ' be reverse from their normal direction, for LTR languages:
    db.Direction = xlRTL
   
    ' You can set negative bars to appear different than positive bars.
    ' You can modify BorderColor, BorderColorType, Color, ColorType:
    With db.NegativeBarFormat
        ' Specify that you want to use the same, or a a different color, than the positive bars.
        ' Note that you must specify this before you specify the BorderColor
        ' property value, if you want to alter the color:
        .BorderColorType = xlDataBarSameAsPositive
       
        ' Specify that you want to use the same, or a a different color, than the positive bars.
        ' Note that you must specify this before you specify the BorderColor
        ' property value, if you want to alter the color:
        .ColorType = xlDataBarColor
        .Color.Color = vbRed
    End With
End Sub

Function FillRange(minValue As Integer, maxValue As Integer, count As Integer) As Range
    Dim i As Integer
   
    For i = 1 To count
        ' Generate random numbers between minValue and maxValue
        Me.Range(&quot;A&quot; &amp; i).Value = Int((maxValue - minValue &#43; 1) * Rnd &#43; minValue)
    Next i
    Set FillRange = Me.Range(&quot;A1&quot;, &quot;A&quot; &amp; count)
End Function</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestDataBars()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Demonstrate&nbsp;DataBar:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;BarFillType&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;AxisPosition</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;AxisColor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;Direction</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;NegativeBarFormat</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;BarBorder</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;FillRange(-<span class="visualBasic__number">25</span>,&nbsp;<span class="visualBasic__number">25</span>,&nbsp;<span class="visualBasic__number">20</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.FormatConditions.Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;db&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;dataBar&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;db&nbsp;=&nbsp;rng.FormatConditions.AddDatabar&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;endpoints&nbsp;for&nbsp;the&nbsp;data&nbsp;bars:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;db.MinPoint.Modify&nbsp;xlConditionValueNumber,&nbsp;-<span class="visualBasic__number">25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;db.MaxPoint.Modify&nbsp;xlConditionValueNumber,&nbsp;<span class="visualBasic__number">25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;axis&nbsp;position.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;options&nbsp;are&nbsp;automatic,&nbsp;midpoint,&nbsp;or&nbsp;none.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;By&nbsp;default,&nbsp;if&nbsp;there&nbsp;are&nbsp;negative&nbsp;values,&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;axis&nbsp;will&nbsp;appear&nbsp;in&nbsp;the&nbsp;middle.&nbsp;Force&nbsp;it&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;use&nbsp;the&nbsp;midpoint:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;db.AxisPosition&nbsp;=&nbsp;xlDataBarAxisMidpoint&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;use&nbsp;a&nbsp;gradient&nbsp;or&nbsp;a&nbsp;solid&nbsp;fill&nbsp;type.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;db.BarFillType&nbsp;=&nbsp;xlDataBarFillSolid&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;fc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;FormatColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;db.BarColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;vbBlue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;-<span class="visualBasic__number">0.2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Modify&nbsp;the&nbsp;behavior&nbsp;of&nbsp;positive&nbsp;and&nbsp;negative</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;bar&nbsp;borders:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;db.BarBorder&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;xlDataBarBorderSolid&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Unfortunately,&nbsp;the&nbsp;BarBorder.Color&nbsp;property&nbsp;returns</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;ColorFormat&nbsp;object,&nbsp;so&nbsp;you'll&nbsp;end&nbsp;up&nbsp;setting</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;Color&nbsp;property&nbsp;of&nbsp;the&nbsp;Color&nbsp;property:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.Color&nbsp;=&nbsp;vbGreen&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Don't&nbsp;be&nbsp;mislead:&nbsp;The&nbsp;AxisColor&nbsp;property&nbsp;is&nbsp;read-only.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;But&nbsp;it&nbsp;is,&nbsp;itself,&nbsp;a&nbsp;FormatColor&nbsp;object.&nbsp;Therefore,&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;reference&nbsp;to&nbsp;the&nbsp;FormatColor&nbsp;object&nbsp;is&nbsp;read-only,&nbsp;but&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;properties&nbsp;of&nbsp;that&nbsp;object&nbsp;are&nbsp;not&nbsp;read-only.&nbsp;You&nbsp;can</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;modify&nbsp;any&nbsp;of&nbsp;the&nbsp;FormatColor&nbsp;object's&nbsp;properties:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;db.AxisColor.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;set&nbsp;the&nbsp;direction&nbsp;of&nbsp;the&nbsp;bars.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;default&nbsp;value&nbsp;is&nbsp;xlContext,&nbsp;which&nbsp;uses&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;direction&nbsp;of&nbsp;the&nbsp;current&nbsp;locale.&nbsp;If&nbsp;you&nbsp;want&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;force&nbsp;right-to-left&nbsp;direction&nbsp;(in&nbsp;other&nbsp;words,&nbsp;force</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;values&nbsp;to&nbsp;be&nbsp;reversed&nbsp;for&nbsp;left-to-right&nbsp;languages&nbsp;such</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;as&nbsp;English),&nbsp;specify&nbsp;xlRTL.&nbsp;To&nbsp;do&nbsp;the&nbsp;same&nbsp;for&nbsp;RTL</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;language&nbsp;such&nbsp;as&nbsp;Arabic&nbsp;or&nbsp;Hebrew,&nbsp;specify&nbsp;xlLTR.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;following&nbsp;forces&nbsp;the&nbsp;negative/positive&nbsp;values&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;be&nbsp;reverse&nbsp;from&nbsp;their&nbsp;normal&nbsp;direction,&nbsp;for&nbsp;LTR&nbsp;languages:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;db.Direction&nbsp;=&nbsp;xlRTL&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;set&nbsp;negative&nbsp;bars&nbsp;to&nbsp;appear&nbsp;different&nbsp;than&nbsp;positive&nbsp;bars.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;modify&nbsp;BorderColor,&nbsp;BorderColorType,&nbsp;Color,&nbsp;ColorType:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;db.NegativeBarFormat&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Specify&nbsp;that&nbsp;you&nbsp;want&nbsp;to&nbsp;use&nbsp;the&nbsp;same,&nbsp;or&nbsp;a&nbsp;a&nbsp;different&nbsp;color,&nbsp;than&nbsp;the&nbsp;positive&nbsp;bars.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;you&nbsp;must&nbsp;specify&nbsp;this&nbsp;before&nbsp;you&nbsp;specify&nbsp;the&nbsp;BorderColor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;property&nbsp;value,&nbsp;if&nbsp;you&nbsp;want&nbsp;to&nbsp;alter&nbsp;the&nbsp;color:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.BorderColorType&nbsp;=&nbsp;xlDataBarSameAsPositive&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Specify&nbsp;that&nbsp;you&nbsp;want&nbsp;to&nbsp;use&nbsp;the&nbsp;same,&nbsp;or&nbsp;a&nbsp;a&nbsp;different&nbsp;color,&nbsp;than&nbsp;the&nbsp;positive&nbsp;bars.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;you&nbsp;must&nbsp;specify&nbsp;this&nbsp;before&nbsp;you&nbsp;specify&nbsp;the&nbsp;BorderColor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;property&nbsp;value,&nbsp;if&nbsp;you&nbsp;want&nbsp;to&nbsp;alter&nbsp;the&nbsp;color:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ColorType&nbsp;=&nbsp;xlDataBarColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Function</span>&nbsp;FillRange(minValue&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;maxValue&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>,&nbsp;count&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Generate&nbsp;random&nbsp;numbers&nbsp;between&nbsp;minValue&nbsp;and&nbsp;maxValue</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Range(<span class="visualBasic__string">&quot;A&quot;</span>&nbsp;&amp;&nbsp;i).Value&nbsp;=&nbsp;Int((maxValue&nbsp;-&nbsp;minValue&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>)&nbsp;*&nbsp;Rnd&nbsp;&#43;&nbsp;minValue)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;FillRange&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;A&quot;</span>&nbsp;&amp;&nbsp;count)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25881" href="/site/view/file/25881/1/Excel.DataBars.txt">Excel.DataBars.txt</a>- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25882" href="/site/view/file/25882/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
