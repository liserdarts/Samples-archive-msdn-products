# Excel 2010: Sort Data Programmatically Using Excel.WorksheetSort
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Sorting
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 02:30:09
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to sort a range of data in ascending and descending order programmatically in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the TestWorksheetSort procedure, and then press F8 to single-step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestWorksheetSort()
 
  ' Create sample data:
  Range(&quot;A1:D1&quot;) = Array(&quot;Name&quot;, &quot;City&quot;, &quot;State&quot;, &quot;Age&quot;)
  Range(&quot;A2:D2&quot;) = Array(&quot;Pam&quot;, &quot;Los Angeles&quot;, &quot;CA&quot;, &quot;16&quot;)
  Range(&quot;A3:D3&quot;) = Array(&quot;Jerry&quot;, &quot;Boston&quot;, &quot;MA&quot;, &quot;14&quot;)
  Range(&quot;A4:D4&quot;) = Array(&quot;Juanita&quot;, &quot;San Francisco&quot;, &quot;CA&quot;, &quot;15&quot;)
  Range(&quot;A5:D5&quot;) = Array(&quot;Xochitl&quot;, &quot;Houston&quot;, &quot;TX&quot;, &quot;11&quot;)
  Range(&quot;A6:D6&quot;) = Array(&quot;Jozi&quot;, &quot;New York&quot;, &quot;NY&quot;, &quot;7&quot;)
  Range(&quot;A7:D7&quot;) = Array(&quot;Aneka&quot;, &quot;Houston&quot;, &quot;TX&quot;, &quot;18&quot;)
  Range(&quot;A8:D8&quot;) = Array(&quot;Brie&quot;, &quot;Boston&quot;, &quot;MA&quot;, &quot;22&quot;)
  Range(&quot;A9:D9&quot;) = Array(&quot;Andrew&quot;, &quot;Seattle&quot;, &quot;WA&quot;, &quot;23&quot;)
  Range(&quot;A10:D10&quot;) = Array(&quot;Grace&quot;, &quot;Boston&quot;, &quot;MA&quot;, &quot;35&quot;)
  Range(&quot;A11:D11&quot;) = Array(&quot;Tom&quot;, &quot;Houston&quot;, &quot;TX&quot;, &quot;12&quot;)

  ' Retrieve a reference to the used range:
  Dim rng As Range
  Set rng = UsedRange

  ' Create sort:
  Dim srt As Sort
  ' Include these two lines to make sure you get
  ' IntelliSense help as you work with the Sort object:
  Dim sht As Worksheet
  Set sht = ActiveSheet
 
  Set srt = sht.Sort
 
  ' Sort first by state ascending, and then by age descending.
  srt.SortFields.Clear
  srt.SortFields.Add Key:=Columns(&quot;C&quot;), _
   SortOn:=xlSortOnValues, Order:=xlAscending
  srt.SortFields.Add Key:=Columns(&quot;D&quot;), _
   SortOn:=xlSortOnValues, Order:=xlDescending
  ' Set the sort range:
  srt.SetRange rng
  srt.Header = xlYes
  srt.MatchCase = True
  ' Apply the sort:
  srt.Apply
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestWorksheetSort()&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;sample&nbsp;data:</span>&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1:D1&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Name&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;City&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;State&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Age&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A2:D2&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Pam&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Los&nbsp;Angeles&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;CA&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;16&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A3:D3&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Jerry&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Boston&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;MA&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;14&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A4:D4&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Juanita&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;San&nbsp;Francisco&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;CA&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;15&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A5:D5&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Xochitl&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Houston&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;TX&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;11&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A6:D6&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Jozi&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;New&nbsp;York&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;NY&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;7&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A7:D7&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Aneka&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Houston&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;TX&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;18&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A8:D8&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Brie&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Boston&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;MA&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;22&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A9:D9&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Andrew&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Seattle&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;WA&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;23&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A10:D10&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Grace&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Boston&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;MA&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;35&quot;</span>)&nbsp;
&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A11:D11&quot;</span>)&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Tom&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Houston&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;TX&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;12&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;used&nbsp;range:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;UsedRange&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;sort:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;srt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Sort&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Include&nbsp;these&nbsp;two&nbsp;lines&nbsp;to&nbsp;make&nbsp;sure&nbsp;you&nbsp;get</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;IntelliSense&nbsp;help&nbsp;as&nbsp;you&nbsp;work&nbsp;with&nbsp;the&nbsp;Sort&nbsp;object:</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sht&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Worksheet&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sht&nbsp;=&nbsp;ActiveSheet&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;srt&nbsp;=&nbsp;sht.Sort&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Sort&nbsp;first&nbsp;by&nbsp;state&nbsp;ascending,&nbsp;and&nbsp;then&nbsp;by&nbsp;age&nbsp;descending.</span>&nbsp;
&nbsp;&nbsp;srt.SortFields.Clear&nbsp;
&nbsp;&nbsp;srt.SortFields.Add&nbsp;Key:=Columns(<span class="visualBasic__string">&quot;C&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;SortOn:=xlSortOnValues,&nbsp;Order:=xlAscending&nbsp;
&nbsp;&nbsp;srt.SortFields.Add&nbsp;Key:=Columns(<span class="visualBasic__string">&quot;D&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;SortOn:=xlSortOnValues,&nbsp;Order:=xlDescending&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;sort&nbsp;range:</span>&nbsp;
&nbsp;&nbsp;srt.SetRange&nbsp;rng&nbsp;
&nbsp;&nbsp;srt.Header&nbsp;=&nbsp;xlYes&nbsp;
&nbsp;&nbsp;srt.MatchCase&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;the&nbsp;sort:</span>&nbsp;
&nbsp;&nbsp;srt.Apply&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25955" href="/site/view/file/25955/1/Excel.WorksheetSort.txt">Excel.WorksheetSort.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25956" href="/site/view/file/25956/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
