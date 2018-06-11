# Excel 2010: Show Properties of ListObject Using Excel.ListObjectTableStyles
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* table properties
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:33:55
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows the properties of the <strong>
ListObject </strong>object such as the <strong>TableStyles </strong>property, the
<strong>TableStyle </strong>property, the <strong>TableStyleElements </strong>property, and the
<strong>TableStyleElement </strong>property.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the ListObjectStyleDemo procedure, and then press F8 to start debugging, then Shift&#43;F8 to single-step through the code (stepping
 over any called procedures). Arrange the VBA and Excel windows side by side so you can see the results of running the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ListObjectStyleDemo()
    ' Step over this procedure. It's not terribly interesting.
    FillRandomData
  
    Dim lo As ListObject
    Set lo = ListObjects.Add( _
     SourceType:=xlSrcRange, _
     Source:=Range(&quot;A1:F13&quot;), _
     XlListObjectHasHeaders:=xlYes)
    lo.Name = &quot;SampleData&quot;
  
    ' This code is only interesting if you single-step through it.
    Dim i As Integer
    For i = 1 To 20
        lo.TableStyle = ActiveWorkbook.TableStyles(i)
    Next i
   
    ' Use a named table style:
    lo.TableStyle = &quot;TableStyleMedium9&quot;
   
    Dim ts As TableStyle
    Set ts = lo.TableStyle
    With ts.TableStyleElements(xlHeaderRow)
        .Font.Bold = False
    End With
   
    CopyStyleElement ts.TableStyleElements(xlRowStripe1), Range(&quot;H2&quot;), &quot;xlRowStripe1&quot;
    CopyStyleElement ts.TableStyleElements(xlHeaderRow), Range(&quot;H3&quot;), &quot;xlHeaderRow&quot;
    ' With more complex table styles, could copy more styles to the output cells.
   
    ' Delete the table, so you can re-run the code.
    lo.Delete
End Sub

Sub CopyStyleElement(tse As TableStyleElement, rng As Range, title As String)
    On Error Resume Next
    With rng
        With .Interior
            .ThemeColor = tse.Interior.ThemeColor
            .TintAndShade = tse.Interior.TintAndShade
        End With
        With .Font
            .Color = tse.Font.Color
            .Name = tse.Font.Name
            .Size = tse.Font.Size
            .Bold = tse.Font.Bold
            .Italic = tse.Font.Italic
        End With
        .Value = title
    End With
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

Sub ListTableStyles()
    ' Use this procedure to list all the named table styles in the
    ' Immediate window. Put the cursor in this procedure and press F5
    ' to run it. Look in the Immediate window for the results.
    Dim ts As TableStyle
    For Each ts In ActiveWorkbook.TableStyles
        Debug.Print ts.Name
    Next ts
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ListObjectStyleDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Step&nbsp;over&nbsp;this&nbsp;procedure.&nbsp;It's&nbsp;not&nbsp;terribly&nbsp;interesting.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FillRandomData&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;lo&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ListObject&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;lo&nbsp;=&nbsp;ListObjects.Add(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SourceType:=xlSrcRange,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Source:=Range(<span class="visualBasic__string">&quot;A1:F13&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XlListObjectHasHeaders:=xlYes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;lo.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SampleData&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;code&nbsp;is&nbsp;only&nbsp;interesting&nbsp;if&nbsp;you&nbsp;single-step&nbsp;through&nbsp;it.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lo.TableStyle&nbsp;=&nbsp;ActiveWorkbook.TableStyles(i)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;a&nbsp;named&nbsp;table&nbsp;style:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;lo.TableStyle&nbsp;=&nbsp;<span class="visualBasic__string">&quot;TableStyleMedium9&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ts&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;TableStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ts&nbsp;=&nbsp;lo.TableStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;ts.TableStyleElements(xlHeaderRow)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Font.Bold&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CopyStyleElement&nbsp;ts.TableStyleElements(xlRowStripe1),&nbsp;Range(<span class="visualBasic__string">&quot;H2&quot;</span>),&nbsp;<span class="visualBasic__string">&quot;xlRowStripe1&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;CopyStyleElement&nbsp;ts.TableStyleElements(xlHeaderRow),&nbsp;Range(<span class="visualBasic__string">&quot;H3&quot;</span>),&nbsp;<span class="visualBasic__string">&quot;xlHeaderRow&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;With&nbsp;more&nbsp;complex&nbsp;table&nbsp;styles,&nbsp;could&nbsp;copy&nbsp;more&nbsp;styles&nbsp;to&nbsp;the&nbsp;output&nbsp;cells.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Delete&nbsp;the&nbsp;table,&nbsp;so&nbsp;you&nbsp;can&nbsp;re-run&nbsp;the&nbsp;code.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;lo.Delete&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;CopyStyleElement(tse&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;TableStyleElement,&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range,&nbsp;title&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">Resume</span>&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;rng&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Interior&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ThemeColor&nbsp;=&nbsp;tse.Interior.ThemeColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TintAndShade&nbsp;=&nbsp;tse.Interior.TintAndShade&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;.Font&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Color&nbsp;=&nbsp;tse.Font.Color&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Name&nbsp;=&nbsp;tse.Font.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Size&nbsp;=&nbsp;tse.Font.Size&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Bold&nbsp;=&nbsp;tse.Font.Bold&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Italic&nbsp;=&nbsp;tse.Font.Italic&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Value&nbsp;=&nbsp;title&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;FillRandomData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;No&nbsp;need&nbsp;to&nbsp;stop&nbsp;through&nbsp;this&nbsp;procedure.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;F1&quot;</span>).Value&nbsp;=&nbsp;Array(<span class="visualBasic__string">&quot;Month&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;North&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;South&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;East&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;West&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Total&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;
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
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;ListTableStyles()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;this&nbsp;procedure&nbsp;to&nbsp;list&nbsp;all&nbsp;the&nbsp;named&nbsp;table&nbsp;styles&nbsp;in&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Immediate&nbsp;window.&nbsp;Put&nbsp;the&nbsp;cursor&nbsp;in&nbsp;this&nbsp;procedure&nbsp;and&nbsp;press&nbsp;F5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;run&nbsp;it.&nbsp;Look&nbsp;in&nbsp;the&nbsp;Immediate&nbsp;window&nbsp;for&nbsp;the&nbsp;results.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ts&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;TableStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;ts&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;ActiveWorkbook.TableStyles&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;ts.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;ts&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25898" href="/site/view/file/25898/1/Excel.ListObjectTableStyles.txt">Excel.ListObjectTableStyles.txt</a>&nbsp;-&nbsp; Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25899" href="/site/view/file/25899/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
