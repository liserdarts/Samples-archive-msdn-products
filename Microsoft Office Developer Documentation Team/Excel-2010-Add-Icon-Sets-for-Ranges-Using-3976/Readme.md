# Excel 2010: Add Icon Sets for Ranges Using Excel.AddIconSetCondition
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
* 2011-08-18 10:43:02
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to add an icon set for a given range of values in an Excel 2010 workbook.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, are offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, copy this code into the Sheet1 class module. Place the cursor inside the TestAddIconSet procedure, and press F5 to run the procedure. View Sheet1 to see the results.</span></p>
<h1><span style="font-weight:bold">Description</span></h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestAddIconSet()
  Dim i As Integer
  Dim rng As Range
  For i = 1 To 20
    ' Set up ranges
    Set rng = SetupRange(i)
    Select Case i
      Case 1
        SetUpIconSet rng, xl3Arrows
      Case 2
        SetUpIconSet rng, xl3ArrowsGray
      Case 3
        SetUpIconSet rng, xl3Flags
      Case 4
        SetUpIconSet rng, xl3Signs
      Case 5
        SetUpIconSet rng, xl3Stars
      Case 6
        SetUpIconSet rng, xl3Symbols
      Case 7
        SetUpIconSet rng, xl3Symbols2
      Case 8
        SetUpIconSet rng, xl3TrafficLights1
      Case 9
        SetUpIconSet rng, xl3TrafficLights2
      Case 10
        SetUpIconSet rng, xl3Triangles
      Case 11
        SetUpIconSet rng, xl4Arrows
      Case 12
        ' Reverse the order on this one:
        SetUpIconSet rng, xl4ArrowsGray, True
      Case 13
        SetUpIconSet rng, xl4CRV
      Case 14
        SetUpIconSet rng, xl4RedToBlack
      Case 15
        SetUpIconSet rng, xl4TrafficLights
      Case 16
        SetUpIconSet rng, xl5Arrows
      Case 17
        ' Reverse the order on this one:
        SetUpIconSet rng, xl5ArrowsGray, True
      Case 18
        SetUpIconSet rng, xl5Boxes
      Case 19
        SetUpIconSet rng, xl5CRV
      Case 20
        SetUpIconSet rng, xl5Quarters
    End Select
  Next i
End Sub

Function SetupRange(col As Integer) As Range
    ' Set up ranges, filled with numbers from 1 to 10.
    Set rng = Range(Cells(1, col), Cells(10, col))
   
    Dim rng1 As Range
    Set rng1 = Cells(1, col)
    rng1.Value = 1

    Dim rng2 As Range
    Set rng2 = Cells(2, col)
    rng2.Value = 2
   
    Range(rng1, rng2).AutoFill Destination:=rng
    Set SetupRange = rng
End Function

Sub SetUpIconSet(rng As Range, iconSet As XlIconSet, Optional ReverseOrder As Boolean = False)
    ' Set up an icon set for the supplied range.
    rng.FormatConditions.Delete
    Dim isc As IconSetCondition
    Set isc = rng.FormatConditions.AddIconSetCondition
    With isc
        ' If specified, show the icons in the reverse ordering:
        .ReverseOrder = ReverseOrder
        .ShowIconOnly = False
        ' Select the requested icon set:
        .iconSet = ActiveWorkbook.IconSets(iconSet)
    End With
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestAddIconSet()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;ranges</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;SetupRange(i)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Arrows&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3ArrowsGray&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">3</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Flags&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">4</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Signs&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">5</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Stars&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">6</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Symbols&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">7</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Symbols2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">8</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3TrafficLights1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">9</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3TrafficLights2&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">10</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl3Triangles&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">11</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl4Arrows&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">12</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reverse&nbsp;the&nbsp;order&nbsp;on&nbsp;this&nbsp;one:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl4ArrowsGray,&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">13</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl4CRV&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">14</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl4RedToBlack&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">15</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl4TrafficLights&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">16</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl5Arrows&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">17</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Reverse&nbsp;the&nbsp;order&nbsp;on&nbsp;this&nbsp;one:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl5ArrowsGray,&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">18</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl5Boxes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">19</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl5CRV&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__number">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetUpIconSet&nbsp;rng,&nbsp;xl5Quarters&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;i&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Function</span>&nbsp;SetupRange(col&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;ranges,&nbsp;filled&nbsp;with&nbsp;numbers&nbsp;from&nbsp;1&nbsp;to&nbsp;10.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(Cells(<span class="visualBasic__number">1</span>,&nbsp;col),&nbsp;Cells(<span class="visualBasic__number">10</span>,&nbsp;col))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng1&nbsp;=&nbsp;Cells(<span class="visualBasic__number">1</span>,&nbsp;col)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng1.Value&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng2&nbsp;=&nbsp;Cells(<span class="visualBasic__number">2</span>,&nbsp;col)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng2.Value&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(rng1,&nbsp;rng2).AutoFill&nbsp;Destination:=rng&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;SetupRange&nbsp;=&nbsp;rng&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;SetUpIconSet(rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range,&nbsp;iconSet&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;XlIconSet,&nbsp;<span class="visualBasic__keyword">Optional</span>&nbsp;ReverseOrder&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;up&nbsp;an&nbsp;icon&nbsp;set&nbsp;for&nbsp;the&nbsp;supplied&nbsp;range.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.FormatConditions.Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;isc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;IconSetCondition&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;isc&nbsp;=&nbsp;rng.FormatConditions.AddIconSetCondition&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;isc&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;specified,&nbsp;show&nbsp;the&nbsp;icons&nbsp;in&nbsp;the&nbsp;reverse&nbsp;ordering:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ReverseOrder&nbsp;=&nbsp;ReverseOrder&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ShowIconOnly&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Select&nbsp;the&nbsp;requested&nbsp;icon&nbsp;set:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.iconSet&nbsp;=&nbsp;ActiveWorkbook.IconSets(iconSet)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25861" href="/site/view/file/25861/1/Excel.AddIconSetCondition.txt">Excel.AddIconSetCondition.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a href="/site/view/file/25860/1/Office%202010%20101%20Code%20Samples.zip"></a><a id="25862" href="/site/view/file/25862/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a> - Download all
 the samples. </span></em></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span></span>
</li><li><span style="font-size:small"><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span></span>
</li></ul>
