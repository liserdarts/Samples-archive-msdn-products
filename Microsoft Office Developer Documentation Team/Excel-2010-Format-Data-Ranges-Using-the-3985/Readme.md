# Excel 2010: Format Data Ranges Using the Excel.DisplayFormat Method
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Range Object
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:29:01
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to apply conditional formatting to a range of data in Microsoft Excel 2010 by using the
<strong>DisplayFormat </strong>class.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in this procedure, and then press F5 to run the procedure full speed. Examine the formatting of the cells in Excel, and then
 examine the results of running the procedure in the Immediate window.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">' Excel 2010

Sub TestDisplayFormat()
    ' From the documentation:
    ' Actions such as changing the conditional formatting or
    ' table style of a range can cause what is displayed in
    ' the current user interface to be inconsistent with the
    ' values in the corresponding properties of the Range object.
    ' Use the properties of the DisplayFormat object to return
    ' the values as they are displayed in the current user interface.
       
    ' In Excel 2010, in a new workbook, copy all this code into
    ' the Sheet1 class module. Place the cursor in this procedure,
    ' and then press F5 to run the procedure full speed. Examine
    ' the formatting of the cells in Excel, and then examine
    ' the results of running the procedure in the Immediate window.
       
    ' This example places some simple data into Sheet1, and
    ' then applies conditional formatting.
    ' Then, it compares the values of various
    ' formatting properties of the range to the
    ' formatting properties of the Range.DisplayFormat object:

   
    Range(&quot;A1&quot;).Value = -20
    Range(&quot;A2&quot;).Value = 10
   
    Dim rng As Range
    Set rng = Range(&quot;A1:A2&quot;)
   
    ' Set up the conditional formatting:
    rng.FormatConditions.Add Type:=xlCellValue, Operator:=xlLess, Formula1:=&quot;=0&quot;
    With rng.FormatConditions(1)
        With .Font
            .Bold = True
            .Italic = True
            .Color = vbRed
        End With
        With .Borders
            .LineStyle = xlContinuous
            .Weight = xlThin
        End With
        With .Interior
            .PatternColorIndex = xlAutomatic
            .Color = vbYellow
        End With
    End With
   
    ' Now compare formatting information for the range, and
    ' what it's actually displaying (using the DisplayFormat property).
    ' Clearly, without the DisplayFormat property, you have no way
    ' to find out the formatting that appears to the user. The
    ' DisplayFormat property is an instance of the DisplayFormat class
    ' and contains many different members. This code only investigates the
    ' properties that might have been changed by conditional formatting.
   
    ' A1 has had conditional formatting changes because it's negative.
    ' A2 has not.
    CompareRangeAndDisplayFormat Range(&quot;A1&quot;)
    CompareRangeAndDisplayFormat Range(&quot;A2&quot;)
End Sub

Private Sub CompareRangeAndDisplayFormat(rng As Range)
    ' Show off the differences in properties that have been changed
    ' by conditional formatting:
    Debug.Print
    Debug.Print rng.Address &amp; &quot;======&quot;
    Debug.Print
    ' Interior color has been modified for negative values. Therefore,
    ' note that for cells that aren't changed by conditional formatting,
    ' the range values and the DisplayFormat values are the same. For those
    ' that have been altered by conditional formatting, they're different.
    ' This code doesn't check out all the properties; it just works
    ' through enough to prove that the properties of the Range object
    ' may not match those of the Range.DisplayFormat object:
    Debug.Print &quot; rng.Interior.Color: &quot; &amp; rng.Interior.Color
    Debug.Print &quot; rng.DisplayFormat.Interior.Color: &quot; &amp; rng.DisplayFormat.Interior.Color
    Debug.Print
    Debug.Print &quot; rng.Font.Color: &quot; &amp; rng.Font.Color
    Debug.Print &quot; rng.DisplayFormat.Font.Color: &quot; &amp; rng.DisplayFormat.Font.Color
    Debug.Print
    Debug.Print &quot; rng.Font.Italic: &quot; &amp; rng.Font.Italic
    Debug.Print &quot; rng.DisplayFormat.Font.Italic: &quot; &amp; rng.DisplayFormat.Font.Italic
    Debug.Print
    Debug.Print &quot; rng.Font.Color: &quot; &amp; rng.Font.Color
    Debug.Print &quot; rng.DisplayFormat.Font.Color: &quot; &amp; rng.DisplayFormat.Font.Color
    Debug.Print
    Debug.Print &quot; rng.Borders.LineStyle: &quot; &amp; rng.Borders.LineStyle
    Debug.Print &quot; rng.DisplayFormat.Borders.LineStyle: &quot; &amp; rng.DisplayFormat.Borders.LineStyle
   
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__com">'&nbsp;Excel&nbsp;2010</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;TestDisplayFormat()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;From&nbsp;the&nbsp;documentation:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Actions&nbsp;such&nbsp;as&nbsp;changing&nbsp;the&nbsp;conditional&nbsp;formatting&nbsp;or</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;table&nbsp;style&nbsp;of&nbsp;a&nbsp;range&nbsp;can&nbsp;cause&nbsp;what&nbsp;is&nbsp;displayed&nbsp;in</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;current&nbsp;user&nbsp;interface&nbsp;to&nbsp;be&nbsp;inconsistent&nbsp;with&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;values&nbsp;in&nbsp;the&nbsp;corresponding&nbsp;properties&nbsp;of&nbsp;the&nbsp;Range&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;the&nbsp;properties&nbsp;of&nbsp;the&nbsp;DisplayFormat&nbsp;object&nbsp;to&nbsp;return</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;values&nbsp;as&nbsp;they&nbsp;are&nbsp;displayed&nbsp;in&nbsp;the&nbsp;current&nbsp;user&nbsp;interface.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;Excel&nbsp;2010,&nbsp;in&nbsp;a&nbsp;new&nbsp;workbook,&nbsp;copy&nbsp;all&nbsp;this&nbsp;code&nbsp;into</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;Sheet1&nbsp;class&nbsp;module.&nbsp;Place&nbsp;the&nbsp;cursor&nbsp;in&nbsp;this&nbsp;procedure,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;then&nbsp;press&nbsp;F5&nbsp;to&nbsp;run&nbsp;the&nbsp;procedure&nbsp;full&nbsp;speed.&nbsp;Examine</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;formatting&nbsp;of&nbsp;the&nbsp;cells&nbsp;in&nbsp;Excel,&nbsp;and&nbsp;then&nbsp;examine</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;results&nbsp;of&nbsp;running&nbsp;the&nbsp;procedure&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;example&nbsp;places&nbsp;some&nbsp;simple&nbsp;data&nbsp;into&nbsp;Sheet1,&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;then&nbsp;applies&nbsp;conditional&nbsp;formatting.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Then,&nbsp;it&nbsp;compares&nbsp;the&nbsp;values&nbsp;of&nbsp;various</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;formatting&nbsp;properties&nbsp;of&nbsp;the&nbsp;range&nbsp;to&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;formatting&nbsp;properties&nbsp;of&nbsp;the&nbsp;Range.DisplayFormat&nbsp;object:</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>).Value&nbsp;=&nbsp;-<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A2&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;A1:A2&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;the&nbsp;conditional&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.FormatConditions.Add&nbsp;Type:=xlCellValue,&nbsp;<span class="visualBasic__keyword">Operator</span>:=xlLess,&nbsp;Formula1:=<span class="visualBasic__string">&quot;=0&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;rng.FormatConditions(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Font&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Italic&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Borders&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LineStyle&nbsp;=&nbsp;xlContinuous&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Weight&nbsp;=&nbsp;xlThin&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Interior&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.PatternColorIndex&nbsp;=&nbsp;xlAutomatic&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;vbYellow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Now&nbsp;compare&nbsp;formatting&nbsp;information&nbsp;for&nbsp;the&nbsp;range,&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;what&nbsp;it's&nbsp;actually&nbsp;displaying&nbsp;(using&nbsp;the&nbsp;DisplayFormat&nbsp;property).</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Clearly,&nbsp;without&nbsp;the&nbsp;DisplayFormat&nbsp;property,&nbsp;you&nbsp;have&nbsp;no&nbsp;way</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;find&nbsp;out&nbsp;the&nbsp;formatting&nbsp;that&nbsp;appears&nbsp;to&nbsp;the&nbsp;user.&nbsp;The</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;DisplayFormat&nbsp;property&nbsp;is&nbsp;an&nbsp;instance&nbsp;of&nbsp;the&nbsp;DisplayFormat&nbsp;class</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;contains&nbsp;many&nbsp;different&nbsp;members.&nbsp;This&nbsp;code&nbsp;only&nbsp;investigates&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;properties&nbsp;that&nbsp;might&nbsp;have&nbsp;been&nbsp;changed&nbsp;by&nbsp;conditional&nbsp;formatting.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;A1&nbsp;has&nbsp;had&nbsp;conditional&nbsp;formatting&nbsp;changes&nbsp;because&nbsp;it's&nbsp;negative.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;A2&nbsp;has&nbsp;not.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CompareRangeAndDisplayFormat&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CompareRangeAndDisplayFormat&nbsp;Range(<span class="visualBasic__string">&quot;A2&quot;</span>)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;CompareRangeAndDisplayFormat(rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Show&nbsp;off&nbsp;the&nbsp;differences&nbsp;in&nbsp;properties&nbsp;that&nbsp;have&nbsp;been&nbsp;changed</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;by&nbsp;conditional&nbsp;formatting:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;rng.Address&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;======&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Interior&nbsp;color&nbsp;has&nbsp;been&nbsp;modified&nbsp;for&nbsp;negative&nbsp;values.&nbsp;Therefore,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;note&nbsp;that&nbsp;for&nbsp;cells&nbsp;that&nbsp;aren't&nbsp;changed&nbsp;by&nbsp;conditional&nbsp;formatting,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;range&nbsp;values&nbsp;and&nbsp;the&nbsp;DisplayFormat&nbsp;values&nbsp;are&nbsp;the&nbsp;same.&nbsp;For&nbsp;those</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;that&nbsp;have&nbsp;been&nbsp;altered&nbsp;by&nbsp;conditional&nbsp;formatting,&nbsp;they're&nbsp;different.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;code&nbsp;doesn't&nbsp;check&nbsp;out&nbsp;all&nbsp;the&nbsp;properties;&nbsp;it&nbsp;just&nbsp;works</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;through&nbsp;enough&nbsp;to&nbsp;prove&nbsp;that&nbsp;the&nbsp;properties&nbsp;of&nbsp;the&nbsp;Range&nbsp;object</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;may&nbsp;not&nbsp;match&nbsp;those&nbsp;of&nbsp;the&nbsp;Range.DisplayFormat&nbsp;object:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.Interior.Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.Interior.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.DisplayFormat.Interior.Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.DisplayFormat.Interior.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.Font.Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.Font.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.DisplayFormat.Font.Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.DisplayFormat.Font.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.Font.Italic:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.Font.Italic&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.DisplayFormat.Font.Italic:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.DisplayFormat.Font.Italic&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.Font.Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.Font.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.DisplayFormat.Font.Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.DisplayFormat.Font.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.Borders.LineStyle:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.Borders.LineStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;rng.DisplayFormat.Borders.LineStyle:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;rng.DisplayFormat.Borders.LineStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25883" href="/site/view/file/25883/1/Excel.DisplayFormat.txt">Excel.DisplayFormat.txt</a>- Download this sample only.</em>
</span></li><li><span style="font-size:small"><em><a id="25884" href="/site/view/file/25884/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
