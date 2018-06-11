# Excel 2010: Enable Removal of Duplicate Rows Using Excel.RemoveDuplicates
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* remove duplicates
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:59:20
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>RemoveDuplicates
</strong>method to enable removal of duplicate rows from a range of data.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">The Range.RemoveDuplicates method allows you to remove duplicate rows from a range of data. You can specify whether to treat the first row as a header row, and you can specify which columns provide the unique data. In this example,
 generate some data, view the data, and then remove all but the unique rows, based on the Name and Price columns.</span></p>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in this procedure, and then press F8 to single-step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestRemoveDuplicates()
  ' Set up the data:
  Range(&quot;A1:C1&quot;) = Array(&quot;ID&quot;, &quot;Name&quot;, &quot;Price&quot;)
  Range(&quot;A2:C2&quot;) = Array(1, &quot;North&quot;, 12)
  Range(&quot;A3:C3&quot;) = Array(2, &quot;East&quot;, 13)
  Range(&quot;A4:C4&quot;) = Array(3, &quot;South&quot;, 24)
  Range(&quot;A5:C5&quot;) = Array(4, &quot;North&quot;, 12)
  Range(&quot;A6:C6&quot;) = Array(5, &quot;East&quot;, 23)
  Range(&quot;A7:C7&quot;) = Array(6, &quot;South&quot;, 24)
  Range(&quot;A8:C8&quot;) = Array(7, &quot;West&quot;, 10)
  Range(&quot;A9:C9&quot;) = Array(8, &quot;East&quot;, 23)
 
  ' Make sure you look at the current state of the data,
  ' before removing duplicate rows.
 
  ' Remove duplicates, looking for unique values in columns 2 and 3.
  UsedRange.RemoveDuplicates Columns:=Array(2, 3), Header:=xlYes
 
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestRemoveDuplicates()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;the&nbsp;data:</span>&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1:C1&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;ID&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Name&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Price&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A2:C2&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">1</span>,&nbsp;<span class="visualBasic__string">&quot;North&quot;</span>,&nbsp;<span class="visualBasic__number">12</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A3:C3&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__number">13</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A4:C4&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">3</span>,&nbsp;<span class="visualBasic__string">&quot;South&quot;</span>,&nbsp;<span class="visualBasic__number">24</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A5:C5&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">4</span>,&nbsp;<span class="visualBasic__string">&quot;North&quot;</span>,&nbsp;<span class="visualBasic__number">12</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A6:C6&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">5</span>,&nbsp;<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__number">23</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A7:C7&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">6</span>,&nbsp;<span class="visualBasic__string">&quot;South&quot;</span>,&nbsp;<span class="visualBasic__number">24</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A8:C8&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">7</span>,&nbsp;<span class="visualBasic__string">&quot;West&quot;</span>,&nbsp;<span class="visualBasic__number">10</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A9:C9&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__number">8</span>,&nbsp;<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__number">23</span>)&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;sure&nbsp;you&nbsp;look&nbsp;at&nbsp;the&nbsp;current&nbsp;state&nbsp;of&nbsp;the&nbsp;data,</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;before&nbsp;removing&nbsp;duplicate&nbsp;rows.</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Remove&nbsp;duplicates,&nbsp;looking&nbsp;for&nbsp;unique&nbsp;values&nbsp;in&nbsp;columns&nbsp;2&nbsp;and&nbsp;3.</span>&nbsp;
&nbsp;&nbsp;UsedRange.RemoveDuplicates&nbsp;Columns:=Array(<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__number">3</span>),&nbsp;Header:=xlYes&nbsp;
&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25926" href="/site/view/file/25926/1/Excel.RemoveDuplicates.txt">Excel.RemoveDuplicates.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25927" href="/site/view/file/25927/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
