# Excel 2010: Format Colors in Ranges Using Excel.AddColorScale
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Conditional Formatting
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:23:47
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the AddColorScale method to format the color in a range of numbers in Microsoft Microsoft Excel 2010.
</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, are offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, copy this code into the Sheet1 class module. Place the cursor inside the TestColorScale procedure, and press F5 to run the procedure.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestColorScale()
    ' Fill a range with numbers from 1 to 50.
    Dim rng As Range
    Set rng = Range(&quot;A1:A50&quot;)

    Range(&quot;A1&quot;) = 1
    Range(&quot;A2&quot;) = 2
    Range(&quot;A1:A2&quot;).AutoFill Destination:=rng
   
    rng.FormatConditions.Delete
   
   ' Add a 2-color scale.
    Dim cs As ColorScale
    Set cs = rng.FormatConditions.AddColorScale(ColorScaleType:=2)

    ' Format the first color as light red
    With cs.ColorScaleCriteria(1)
        .Type = xlConditionValueLowestValue
        With .FormatColor
            .Color = vbRed
            ' TintAndShade takes a value between -1 and 1.
            ' -1 is darkest, 1 is lightest.
            .TintAndShade = -0.25
        End With
    End With
   
    ' Format the second color as green, at the highest value.
    With cs.ColorScaleCriteria(2)
        .Type = xlConditionValueHighestValue
        With .FormatColor
            .Color = vbGreen
            .TintAndShade = 0
        End With
    End With
   
    ' Try again with a rectangular range of values.
    ' Lowest values should be red, values at the 50th percentile
    ' should be red/green, high values are green.
    Set rng = Range(&quot;D1&quot;, &quot;H10&quot;)
    rng.Formula = &quot;=RANDBETWEEN(1, 99)&quot;
    rng.FormatConditions.Delete
    Set cs = rng.FormatConditions.AddColorScale(ColorScaleType:=3)
   
    ' Set the color of the lowest value, with a range up to
    ' the next scale criteria. The color should be red.
    With cs.ColorScaleCriteria(1)
        .Type = xlConditionValueLowestValue
        With .FormatColor
            .Color = &amp;H6B69F8
            .TintAndShade = 0
        End With
    End With
   
    ' At the 50th percentile, the color should be red/green.
    ' Note that you can't set the Value property for all
    ' values of Type.
    With cs.ColorScaleCriteria(2)
        .Type = xlConditionValuePercentile
        .Value = 50
        With .FormatColor
            .Color = &amp;H84EBFF
            .TintAndShade = 0
        End With
    End With
   
    ' At the highest value, the color should be green.
    With cs.ColorScaleCriteria(3)
        .Type = xlConditionValueHighestValue
        With .FormatColor
            .Color = &amp;H7BBE63
            .TintAndShade = 0
        End With
    End With
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestColorScale()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;a&nbsp;range&nbsp;with&nbsp;numbers&nbsp;from&nbsp;1&nbsp;to&nbsp;50.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;A1:A50&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A2&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1:A2&quot;</span>).AutoFill&nbsp;Destination:=rng&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.FormatConditions.Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;2-color&nbsp;scale.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ColorScale&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cs&nbsp;=&nbsp;rng.FormatConditions.AddColorScale(ColorScaleType:=<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Format&nbsp;the&nbsp;first&nbsp;color&nbsp;as&nbsp;light&nbsp;red</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cs.ColorScaleCriteria(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;xlConditionValueLowestValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.FormatColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;TintAndShade&nbsp;takes&nbsp;a&nbsp;value&nbsp;between&nbsp;-1&nbsp;and&nbsp;1.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;-1&nbsp;is&nbsp;darkest,&nbsp;1&nbsp;is&nbsp;lightest.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;-<span class="visualBasic__number">0.25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Format&nbsp;the&nbsp;second&nbsp;color&nbsp;as&nbsp;green,&nbsp;at&nbsp;the&nbsp;highest&nbsp;value.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cs.ColorScaleCriteria(<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;xlConditionValueHighestValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.FormatColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;vbGreen&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;

&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;again&nbsp;with&nbsp;a&nbsp;rectangular&nbsp;range&nbsp;of&nbsp;values.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Lowest&nbsp;values&nbsp;should&nbsp;be&nbsp;red,&nbsp;values&nbsp;at&nbsp;the&nbsp;50th&nbsp;percentile</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;should&nbsp;be&nbsp;red/green,&nbsp;high&nbsp;values&nbsp;are&nbsp;green.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;D1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;H10&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=RANDBETWEEN(1,&nbsp;99)&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.FormatConditions.Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cs&nbsp;=&nbsp;rng.FormatConditions.AddColorScale(ColorScaleType:=<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;color&nbsp;of&nbsp;the&nbsp;lowest&nbsp;value,&nbsp;with&nbsp;a&nbsp;range&nbsp;up&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;next&nbsp;scale&nbsp;criteria.&nbsp;The&nbsp;color&nbsp;should&nbsp;be&nbsp;red.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cs.ColorScaleCriteria(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;xlConditionValueLowestValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.FormatColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;&amp;H6B69F8&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;At&nbsp;the&nbsp;50th&nbsp;percentile,&nbsp;the&nbsp;color&nbsp;should&nbsp;be&nbsp;red/green.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;you&nbsp;can't&nbsp;set&nbsp;the&nbsp;Value&nbsp;property&nbsp;for&nbsp;all</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;values&nbsp;of&nbsp;Type.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cs.ColorScaleCriteria(<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;xlConditionValuePercentile&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Value&nbsp;=&nbsp;<span class="visualBasic__number">50</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.FormatColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;&amp;H84EBFF&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;At&nbsp;the&nbsp;highest&nbsp;value,&nbsp;the&nbsp;color&nbsp;should&nbsp;be&nbsp;green.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;cs.ColorScaleCriteria(<span class="visualBasic__number">3</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Type&nbsp;=&nbsp;xlConditionValueHighestValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.FormatColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;&amp;H7BBE63&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25859" href="/site/view/file/25859/1/Excel.AddColorScale.txt">Excel.AddColorScale.txt</a> -
<em><span style="line-height:115%">Download this sample only.</span></em></em></span>
</li><li><span style="font-size:small"><em><em><a id="25860" href="/site/view/file/25860/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a> -
</em></em><em>Download all the samples.</em></span> </li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
