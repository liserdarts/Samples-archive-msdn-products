# Excel 2010: Change Colors to Indicate Values Above and Below Average in Ranges
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
* 2011-08-03 12:21:46
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to manipulate the formatted colors above and below an average within a given a range of values in an Microsoft Excel 2010 workbook.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, are offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, copy this code into the Sheet1 class module. Place the cursor inside the TestAboveAverage procedure, and press F5 to run the procedure.</span></p>
<p><span style="font-size:small">This code fills a range with random integers between -50 and 50, and then makes items above the average bold and red, and items below average appear blue.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">' Excel 2010

' Demonstrate the AddAboveAverage method

Sub TestAboveAverage()
    ' Fill the range with random numbers between
    ' -50 and 50.
    Dim rng As Range
    Set rng = Range(&quot;A1&quot;, &quot;A20&quot;)
    SetupRandomData rng
  
    ' Create a conditional format for values above average.
    Dim aa As AboveAverage
    Set aa = rng.FormatConditions.AddAboveAverage
    aa.AboveBelow = xlAboveAverage
    aa.Font.Bold = True
    aa.Font.Color = vbRed
  
    ' Create a conditional format for values below average.
    Dim ba As AboveAverage
    Set ba = rng.FormatConditions.AddAboveAverage
    ba.AboveBelow = xlBelowAverage
    ba.Font.Color = vbBlue
End Sub

Sub SetupRandomData(rng As Range)
    rng.Formula = &quot;=RANDBETWEEN(-50, 50)&quot;
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__com">'&nbsp;Excel&nbsp;2010</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;Demonstrate&nbsp;the&nbsp;AddAboveAverage&nbsp;method</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;TestAboveAverage()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;the&nbsp;range&nbsp;with&nbsp;random&nbsp;numbers&nbsp;between</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;-50&nbsp;and&nbsp;50.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;A20&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SetupRandomData&nbsp;rng&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;conditional&nbsp;format&nbsp;for&nbsp;values&nbsp;above&nbsp;average.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;aa&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;AboveAverage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;aa&nbsp;=&nbsp;rng.FormatConditions.AddAboveAverage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;aa.AboveBelow&nbsp;=&nbsp;xlAboveAverage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;aa.Font.Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;aa.Font.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;conditional&nbsp;format&nbsp;for&nbsp;values&nbsp;below&nbsp;average.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ba&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;AboveAverage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ba&nbsp;=&nbsp;rng.FormatConditions.AddAboveAverage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ba.AboveBelow&nbsp;=&nbsp;xlBelowAverage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ba.Font.Color&nbsp;=&nbsp;vbBlue&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;SetupRandomData(rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=RANDBETWEEN(-50,&nbsp;50)&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><strong>&nbsp;</strong><strong><a id="25857" href="/site/view/file/25857/1/Excel.AboveAverage.txt">Excel.AboveAverage.txt</a></strong>. Download this sample only.</em></span>
</li></ul>
<ul>
<li><span style="font-size:small"><em><em><em><strong><a id="25858" href="/site/view/file/25858/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>.</strong></em><em> Download all the samples.</em></em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
<p><br>
<br>
</p>
