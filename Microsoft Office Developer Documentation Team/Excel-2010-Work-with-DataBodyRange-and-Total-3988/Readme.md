# Excel 2010: Work with DataBodyRange and Total Properties Using Excel.ListColumn
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* list columns
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:30:32
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>DataBodyRange
</strong>property and <strong>Total </strong>property of the <strong>ListColumn </strong>
object in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the ListColumnDemo procedure, and then press F8 to start debugging, then Shift&#43;F8 to single-step through the code (stepping
 over any called procedures). Arrange the VBA and Excel windows side by side so you can see the results of running the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ListColumnDemo()
    ' Step over this procedure. It's not terribly interesting.
    FillRandomData
 
    ' Set up the list object.
    Dim lo As ListObject
    Set lo = ListObjects.Add( _
     SourceType:=xlSrcRange, _
     Source:=Range(&quot;A1:F13&quot;), _
     XlListObjectHasHeaders:=xlYes)
    lo.Name = &quot;SampleData&quot;
    lo.ShowTotals = True
 
    ' Retrieve a reference to the second column in the ListObject:
    Dim lc As ListColumn
    Set lc = lo.ListColumns(2)
   
    ' Retrieve a reference to the ListColumn's data, excluding
    ' headers and footers.
    Dim rng As Range
    Set rng = lc.DataBodyRange
   
    ' Do some things with the data body range:
    rng.Borders.Color = vbRed
    rng.FormatConditions.AddDatabar
   
    ' Retrieve the total for the column and
    ' display it in the totals row.
    ' Obviously, there are other ways to do this:
    Dim totalValue As Long
    totalValue = WorksheetFunction.Sum(rng)
    ' You should verify that the Total property isn't
    ' Nothing--it will be, if the list object isn't
    ' displaying its totals row. In this case, the totals
    ' row is definitely visible, so this check is extraneous.
    If Not lc.Total Is Nothing Then
        lc.Total.Value = totalValue
    End If
End Sub

Sub FillRandomData()
    ' No need to stop through this procedure.
    Range(&quot;A1&quot;, &quot;F1&quot;).Value = Array(&quot;Month&quot;, &quot;North&quot;, &quot;South&quot;, &quot;East&quot;, &quot;West&quot;, &quot;Total&quot;)
 
    ' Fill in twelve rows with random data.
    Dim i As Integer
    Dim j As Integer
    For i = 1 To 12
        Cells(i &#43; 1, 1).Value = MonthName(i, True)
        For j = 2 To 5
            Cells(i &#43; 1, j) = Round(Rnd * 100)
        Next j
    Next i
    Range(&quot;F2&quot;, &quot;F13&quot;).Formula = &quot;=SUM(B2:E2)&quot;
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ListColumnDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Step&nbsp;over&nbsp;this&nbsp;procedure.&nbsp;It's&nbsp;not&nbsp;terribly&nbsp;interesting.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FillRandomData&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;the&nbsp;list&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;lo&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ListObject&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;lo&nbsp;=&nbsp;ListObjects.Add(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SourceType:=xlSrcRange,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Source:=Range(<span class="visualBasic__string">&quot;A1:F13&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XlListObjectHasHeaders:=xlYes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;lo.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SampleData&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;lo.ShowTotals&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;second&nbsp;column&nbsp;in&nbsp;the&nbsp;ListObject:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;lc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ListColumn&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;lc&nbsp;=&nbsp;lo.ListColumns(<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;ListColumn's&nbsp;data,&nbsp;excluding</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;headers&nbsp;and&nbsp;footers.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;lc.DataBodyRange&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Do&nbsp;some&nbsp;things&nbsp;with&nbsp;the&nbsp;data&nbsp;body&nbsp;range:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.Borders.Color&nbsp;=&nbsp;vbRed&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.FormatConditions.AddDatabar&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;the&nbsp;total&nbsp;for&nbsp;the&nbsp;column&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;display&nbsp;it&nbsp;in&nbsp;the&nbsp;totals&nbsp;row.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Obviously,&nbsp;there&nbsp;are&nbsp;other&nbsp;ways&nbsp;to&nbsp;do&nbsp;this:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;totalValue&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Long</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;totalValue&nbsp;=&nbsp;WorksheetFunction.Sum(rng)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;should&nbsp;verify&nbsp;that&nbsp;the&nbsp;Total&nbsp;property&nbsp;isn't</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Nothing--it&nbsp;will&nbsp;be,&nbsp;if&nbsp;the&nbsp;list&nbsp;object&nbsp;isn't</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;displaying&nbsp;its&nbsp;totals&nbsp;row.&nbsp;In&nbsp;this&nbsp;case,&nbsp;the&nbsp;totals</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;row&nbsp;is&nbsp;definitely&nbsp;visible,&nbsp;so&nbsp;this&nbsp;check&nbsp;is&nbsp;extraneous.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;lc.Total&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lc.Total.Value&nbsp;=&nbsp;totalValue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;FillRandomData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;No&nbsp;need&nbsp;to&nbsp;stop&nbsp;through&nbsp;this&nbsp;procedure.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;F1&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Month&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;North&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;South&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;West&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Total&quot;</span>)&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;in&nbsp;twelve&nbsp;rows&nbsp;with&nbsp;random&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;j&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">12</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cells(i&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>,&nbsp;<span class="visualBasic__number">1</span>).Value&nbsp;=&nbsp;MonthName(i,&nbsp;<span class="visualBasic__keyword">True</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;j&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cells(i&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>,&nbsp;j)&nbsp;=&nbsp;Round(Rnd&nbsp;*&nbsp;<span class="visualBasic__number">100</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;j&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;F2&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;F13&quot;</span>).Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=SUM(B2:E2)&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25889" href="/site/view/file/25889/1/Excel.ListColumn.txt">Excel.ListColumn.txt</a>&nbsp;- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25890" href="/site/view/file/25890/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
