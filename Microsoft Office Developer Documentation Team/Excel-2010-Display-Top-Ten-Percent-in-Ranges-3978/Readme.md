# Excel 2010: Display Top Ten Percent in Ranges Programmatically
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
* 2011-08-03 12:25:14
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the AddTop10 method to display the top 10% for a range of numbers in a Microsoft Excel 2010 workbook.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, copy this code into the Sheet1 class module. Place the cursor inside the DemoAddTop10 procedure, and press F5 to run the procedure. View Sheet1 to see the results.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub DemoAddTop10()
  ' Fill a range with random numbers.
  ' Mark the top 10% of items in green, and the bottom
  ' 10% of the items in red.
 
  ' Set up a range, and fill it with random numbers.
  Dim rng As Range
  Set rng = Range(&quot;A1:E10&quot;)
  SetupRangeData rng
 
  ' Clear any existing format conditions.
  rng.FormatConditions.Delete
 
  ' Set up a condition that formats the top
  ' 10 percent of items on green.
  Dim fc As Top10
  Set fc = rng.FormatConditions.AddTop10
  fc.Percent = True
  fc.TopBottom = xlTop10Top
  fc.Interior.Color = vbGreen
 
  ' Set up a condition that formats the bottom
  ' 10 percent of items in red.
  Set fc = rng.FormatConditions.AddTop10
  fc.TopBottom = xlTop10Bottom
  fc.Percent = True
  fc.Interior.Color = vbRed
End Sub

Sub SetupRangeData(rng As Range)
  rng.Formula = &quot;=RANDBETWEEN(1, 100)&quot;
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;DemoAddTop10()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;a&nbsp;range&nbsp;with&nbsp;random&nbsp;numbers.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Mark&nbsp;the&nbsp;top&nbsp;10%&nbsp;of&nbsp;items&nbsp;in&nbsp;green,&nbsp;and&nbsp;the&nbsp;bottom</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;10%&nbsp;of&nbsp;the&nbsp;items&nbsp;in&nbsp;red.</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;a&nbsp;range,&nbsp;and&nbsp;fill&nbsp;it&nbsp;with&nbsp;random&nbsp;numbers.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;A1:E10&quot;</span>)&nbsp;
&nbsp;&nbsp;SetupRangeData&nbsp;rng&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Clear&nbsp;any&nbsp;existing&nbsp;format&nbsp;conditions.</span>&nbsp;
&nbsp;&nbsp;rng.FormatConditions.Delete&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;a&nbsp;condition&nbsp;that&nbsp;formats&nbsp;the&nbsp;top</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;10&nbsp;percent&nbsp;of&nbsp;items&nbsp;on&nbsp;green.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;fc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Top10&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;fc&nbsp;=&nbsp;rng.FormatConditions.AddTop10&nbsp;
&nbsp;&nbsp;fc.Percent&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;fc.TopBottom&nbsp;=&nbsp;xlTop10Top&nbsp;
&nbsp;&nbsp;fc.Interior.Color&nbsp;=&nbsp;vbGreen&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;a&nbsp;condition&nbsp;that&nbsp;formats&nbsp;the&nbsp;bottom</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;10&nbsp;percent&nbsp;of&nbsp;items&nbsp;in&nbsp;red.</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;fc&nbsp;=&nbsp;rng.FormatConditions.AddTop10&nbsp;
&nbsp;&nbsp;fc.TopBottom&nbsp;=&nbsp;xlTop10Bottom&nbsp;
&nbsp;&nbsp;fc.Percent&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;fc.Interior.Color&nbsp;=&nbsp;vbRed&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;SetupRangeData(rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range)&nbsp;
&nbsp;&nbsp;rng.Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=RANDBETWEEN(1,&nbsp;100)&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25866" href="/site/view/file/25866/1/Excel.AddTop10.txt">Excel.AddTop10.txt</a> - Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a href="/site/view/file/25860/1/Office%202010%20101%20Code%20Samples.zip"></a><a id="25867" href="/site/view/file/25867/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a> - Download all
 the samples.</em><em></em> </span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
