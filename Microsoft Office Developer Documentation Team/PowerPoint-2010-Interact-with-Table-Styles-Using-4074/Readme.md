# PowerPoint 2010: Interact with Table Styles Using PPT.Table.ApplyStyle
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* formatting tables
## IsPublished
* True
## ModifiedDate
* 2011-08-05 03:35:45
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to programmatically interact with table styles in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy this code into a module in a new presentation. Display the VBA window side-by-side with the PowerPoint window and press F8 (and then Shift&#43;F8) to single step through this code for the most effective use of this demonstration.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TableStyleDemo()
    ' Create a new slide with a simple table:
    Dim sld As Slide
    Set sld = ActivePresentation.Slides.Add(2, ppLayoutTable)
    sld.Select
   
    Dim tbl As Table
    Set tbl = sld.Shapes.AddTable(4, 4).Table
    FillTable tbl
   
    ' In order to apply a style to a table, you must know the
    ' StyleID associated with the style. This information is not documented
    ' nor is there any way to retrieve this information for all the styles.
    ' This sample includes a list of all the table styles for PowerPoint 2010
    ' at the end of the sample. These values are not guaranteed to work in
    ' any other version of PowerPoint.
   
    With tbl.Cell(3, 3).Shape.TextFrame.TextRange
        .Font.Bold = msoTrue
        .Font.Size = 24
    End With
   
    ' Apply Light Style 1 - Accent 3, preserving existing styles.
    tbl.ApplyStyle &quot;{C083E6E3-FA7D-4D7B-A595-EF9225AFEA82}&quot;, True
    Debug.Print &quot;Style.Name: &quot; &amp; tbl.Style.Name
    Debug.Print &quot;Style.Id  : &quot; &amp; tbl.Style.Id
   
    ' Apply Dark Style 2 - Accent 5/Accent 6, without preserving styles.
    ' Note that this changes the Bold font back to normal, but doesn't affect
    ' font size:
    tbl.ApplyStyle &quot;{46F890A9-2807-4EBB-B81D-B2AA78EC7F39}&quot;, False
    Debug.Print &quot;Style.Name: &quot; &amp; tbl.Style.Name
    Debug.Print &quot;Style.Id  : &quot; &amp; tbl.Style.Id
End Sub

Sub FillTable(tbl As Table)
    ' Fill a table with sample data.
    Dim row As Integer
    Dim col As Integer
  
    For col = 1 To tbl.Columns.Count
        tbl.Cell(1, col).Shape.TextFrame.TextRange.Text = &quot;Heading &quot; &amp; col
    Next col
  
    For row = 2 To tbl.Rows.Count
        For col = 1 To tbl.Columns.Count
            tbl.Cell(row, col).Shape.TextFrame.TextRange.Text = &quot;Cell &quot; &amp; row &amp; &quot;, &quot; &amp; col
        Next col
    Next row
End Sub

' The following list includes the name and style for each of the available table styles.
' This list is undocumented, and was created using inspection. Any values could
' change in any version, and this list has only been tested for PowerPoint 2010:

' No Style, No Grid: {2D5ABB26-0587-4C30-8999-92F81FD0307C}
' Themed Style 1 - Accent 1: {3C2FFA5D-87B4-456A-9821-1D502468CF0F}
' Themed Style 1 - Accent 2: {284E427A-3D55-4303-BF80-6455036E1DE7}
' Themed Style 1 - Accent 3: {69C7853C-536D-4A76-A0AE-DD22124D55A5}
' Themed Style 1 - Accent 4: {775DCB02-9BB8-47FD-8907-85C794F793BA}
' Themed Style 1 - Accent 5: {35758FB7-9AC5-4552-8A53-C91805E547FA}
' Themed Style 1 - Accent 6: {08FB837D-C827-4EFA-A057-4D05807E0F7C}
' No Style, Table Grid: {5940675A-B579-460E-94D1-54222C63F5DA}
' Themed Style 2 - Accent 1: {D113A9D2-9D6B-4929-AA2D-F23B5EE8CBE7}
' Themed Style 2 - Accent 2: {18603FDC-E32A-4AB5-989C-0864C3EAD2B8}
' Themed Style 2 - Accent 3: {306799F8-075E-4A3A-A7F6-7FBC6576F1A4}
' Themed Style 2 - Accent 4: {E269D01E-BC32-4049-B463-5C60D7B0CCD2}
' Themed Style 2 - Accent 5: {327F97BB-C833-4FB7-BDE5-3F7075034690}
' Themed Style 2 - Accent 6: {638B1855-1B75-4FBE-930C-398BA8C253C6}
' Light Style 1: {9D7B26C5-4107-4FEC-AEDC-1716B250A1EF}
' Light Style 1 - Accent 1: {3B4B98B0-60AC-42C2-AFA5-B58CD77FA1E5}
' Light Style 1 - Accent 2: {0E3FDE45-AF77-4B5C-9715-49D594BDF05E}
' Light Style 1 - Accent 3: {C083E6E3-FA7D-4D7B-A595-EF9225AFEA82}
' Light Style 1 - Accent 4: {D27102A9-8310-4765-A935-A1911B00CA55}
' Light Style 1 - Accent 5: {5FD0F851-EC5A-4D38-B0AD-8093EC10F338}
' Light Style 1 - Accent 6: {68D230F3-CF80-4859-8CE7-A43EE81993B5}
' Light Style 2: {7E9639D4-E3E2-4D34-9284-5A2195B3D0D7}
' Light Style 2 - Accent 1: {69012ECD-51FC-41F1-AA8D-1B2483CD663E}
' Light Style 2 - Accent 2: {72833802-FEF1-4C79-8D5D-14CF1EAF98D9}
' Light Style 2 - Accent 3: {F2DE63D5-997A-4646-A377-4702673A728D}
' Light Style 2 - Accent 4: {17292A2E-F333-43FB-9621-5CBBE7FDCDCB}
' Light Style 2 - Accent 5: {5A111915-BE36-4E01-A7E5-04B1672EAD32}
' Light Style 2 - Accent 6: {912C8C85-51F0-491E-9774-3900AFEF0FD7}
' Light Style 3: {616DA210-FB5B-4158-B5E0-FEB733F419BA}
' Light Style 3 - Accent 1: {BC89EF96-8CEA-46FF-86C4-4CE0E7609802}
' Light Style 3 - Accent 2: {5DA37D80-6434-44D0-A028-1B22A696006F}
' Light Style 3 - Accent 3: {8799B23B-EC83-4686-B30A-512413B5E67A}
' Light Style 3 - Accent 4: {ED083AE6-46FA-4A59-8FB0-9F97EB10719F}
' Light Style 3 - Accent 5: {BDBED569-4797-4DF1-A0F4-6AAB3CD982D8}
' Light Style 3 - Accent 6: {E8B1032C-EA38-4F05-BA0D-38AFFFC7BED3}
' Medium Style 1: {793D81CF-94F2-401A-BA57-92F5A7B2D0C5}
' Medium Style 1 - Accent 1: {B301B821-A1FF-4177-AEE7-76D212191A09}
' Medium Style 1 - Accent 2: {9DCAF9ED-07DC-4A11-8D7F-57B35C25682E}
' Medium Style 1 - Accent 3: {1FECB4D8-DB02-4DC6-A0A2-4F2EBAE1DC90}
' Medium Style 1 - Accent 4: {1E171933-4619-4E11-9A3F-F7608DF75F80}
' Medium Style 1 - Accent 5: {FABFCF23-3B69-468F-B69F-88F6DE6A72F2}
' Medium Style 1 - Accent 6: {10A1B5D5-9B99-4C35-A422-299274C87663}
' Medium Style 2: {073A0DAA-6AF3-43AB-8588-CEC1D06C72B9}
' Medium Style 2 - Accent 1: {5C22544A-7EE6-4342-B048-85BDC9FD1C3A}
' Medium Style 2 - Accent 2: {21E4AEA4-8DFA-4A89-87EB-49C32662AFE0}
' Medium Style 2 - Accent 3: {F5AB1C69-6EDB-4FF4-983F-18BD219EF322}
' Medium Style 2 - Accent 4: {00A15C55-8517-42AA-B614-E9B94910E393}
' Medium Style 2 - Accent 5: {7DF18680-E054-41AD-8BC1-D1AEF772440D}
' Medium Style 2 - Accent 6: {93296810-A885-4BE3-A3E7-6D5BEEA58F35}
' Medium Style 3: {8EC20E35-A176-4012-BC5E-935CFFF8708E}
' Medium Style 3 - Accent 1: {6E25E649-3F16-4E02-A733-19D2CDBF48F0}
' Medium Style 3 - Accent 2: {85BE263C-DBD7-4A20-BB59-AAB30ACAA65A}
' Medium Style 3 - Accent 3: {EB344D84-9AFB-497E-A393-DC336BA19D2E}
' Medium Style 3 - Accent 4: {EB9631B5-78F2-41C9-869B-9F39066F8104}
' Medium Style 3 - Accent 5: {74C1A8A3-306A-4EB7-A6B1-4F7E0EB9C5D6}
' Medium Style 3 - Accent 6: {2A488322-F2BA-4B5B-9748-0D474271808F}
' Medium Style 4: {D7AC3CCA-C797-4891-BE02-D94E43425B78}
' Medium Style 4 - Accent 1: {69CF1AB2-1976-4502-BF36-3FF5EA218861}
' Medium Style 4 - Accent 2: {8A107856-5554-42FB-B03E-39F5DBC370BA}
' Medium Style 4 - Accent 3: {0505E3EF-67EA-436B-97B2-0124C06EBD24}
' Medium Style 4 - Accent 4: {C4B1156A-380E-4F78-BDF5-A606A8083BF9}
' Medium Style 4 - Accent 5: {22838BEF-8BB2-4498-84A7-C5851F593DF1}
' Medium Style 4 - Accent 6: {16D9F66E-5EB9-4882-86FB-DCBF35E3C3E4}
' Dark Style 1: {E8034E78-7F5D-4C2E-B375-FC64B27BC917}
' Dark Style 1 - Accent 1: {125E5076-3810-47DD-B79F-674D7AD40C01}
' Dark Style 1 - Accent 2: {37CE84F3-28C3-443E-9E96-99CF82512B78}
' Dark Style 1 - Accent 3: {D03447BB-5D67-496B-8E87-E561075AD55C}
' Dark Style 1 - Accent 4: {E929F9F4-4A8F-4326-A1B4-22849713DDAB}
' Dark Style 1 - Accent 5:{8FD4443E-F989-4FC4-A0C8-D5A2AF1F390B}
' Dark Style 1 - Accent 6: {AF606853-7671-496A-8E4F-DF71F8EC918B}
' Dark Style 2: {5202B0CA-FC54-4496-8BCA-5EF66A818D29}
' Dark Style 2 - Accent 1/Accent 2: {0660B408-B3CF-4A94-85FC-2B1E0A45F4A2}
' Dark Style 2 - Accent 3/Accent 4: {91EBBBCC-DAD2-459C-BE2E-F6DE35CF9A28}
' Dark Style 2 - Accent 5/Accent 6: {46F890A9-2807-4EBB-B81D-B2AA78EC7F39}</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TableStyleDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;new&nbsp;slide&nbsp;with&nbsp;a&nbsp;simple&nbsp;table:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides.Add(<span class="visualBasic__number">2</span>,&nbsp;ppLayoutTable)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld.<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;tbl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Table&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;tbl&nbsp;=&nbsp;sld.Shapes.AddTable(<span class="visualBasic__number">4</span>,&nbsp;<span class="visualBasic__number">4</span>).Table&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;FillTable&nbsp;tbl&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;order&nbsp;to&nbsp;apply&nbsp;a&nbsp;style&nbsp;to&nbsp;a&nbsp;table,&nbsp;you&nbsp;must&nbsp;know&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;StyleID&nbsp;associated&nbsp;with&nbsp;the&nbsp;style.&nbsp;This&nbsp;information&nbsp;is&nbsp;not&nbsp;documented</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;nor&nbsp;is&nbsp;there&nbsp;any&nbsp;way&nbsp;to&nbsp;retrieve&nbsp;this&nbsp;information&nbsp;for&nbsp;all&nbsp;the&nbsp;styles.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;sample&nbsp;includes&nbsp;a&nbsp;list&nbsp;of&nbsp;all&nbsp;the&nbsp;table&nbsp;styles&nbsp;for&nbsp;PowerPoint&nbsp;2010</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;at&nbsp;the&nbsp;end&nbsp;of&nbsp;the&nbsp;sample.&nbsp;These&nbsp;values&nbsp;are&nbsp;not&nbsp;guaranteed&nbsp;to&nbsp;work&nbsp;in</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;any&nbsp;other&nbsp;version&nbsp;of&nbsp;PowerPoint.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;tbl.Cell(<span class="visualBasic__number">3</span>,&nbsp;<span class="visualBasic__number">3</span>).Shape.TextFrame.TextRange&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Font.Bold&nbsp;=&nbsp;msoTrue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Font.Size&nbsp;=&nbsp;<span class="visualBasic__number">24</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;3,&nbsp;preserving&nbsp;existing&nbsp;styles.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.ApplyStyle&nbsp;<span class="visualBasic__string">&quot;{C083E6E3-FA7D-4D7B-A595-EF9225AFEA82}&quot;</span>,&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Style.Name:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;tbl.Style.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Style.Id&nbsp;&nbsp;:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;tbl.Style.Id&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Apply&nbsp;Dark&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;5/Accent&nbsp;6,&nbsp;without&nbsp;preserving&nbsp;styles.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;this&nbsp;changes&nbsp;the&nbsp;Bold&nbsp;font&nbsp;back&nbsp;to&nbsp;normal,&nbsp;but&nbsp;doesn't&nbsp;affect</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;font&nbsp;size:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;tbl.ApplyStyle&nbsp;<span class="visualBasic__string">&quot;{46F890A9-2807-4EBB-B81D-B2AA78EC7F39}&quot;</span>,&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Style.Name:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;tbl.Style.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Style.Id&nbsp;&nbsp;:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;tbl.Style.Id&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Sub</span>&nbsp;FillTable(tbl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Table)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;a&nbsp;table&nbsp;with&nbsp;sample&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;row&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;col&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;col&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;tbl.Columns.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Cell(<span class="visualBasic__number">1</span>,&nbsp;col).Shape.TextFrame.TextRange.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Heading&nbsp;&quot;</span>&nbsp;&amp;&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;row&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;tbl.Rows.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;col&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;tbl.Columns.Count&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Cell(row,&nbsp;col).Shape.TextFrame.TextRange.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Cell&nbsp;&quot;</span>&nbsp;&amp;&nbsp;row&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;col&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;row&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;The&nbsp;following&nbsp;list&nbsp;includes&nbsp;the&nbsp;name&nbsp;and&nbsp;style&nbsp;for&nbsp;each&nbsp;of&nbsp;the&nbsp;available&nbsp;table&nbsp;styles.</span>&nbsp;
<span class="visualBasic__com">'&nbsp;This&nbsp;list&nbsp;is&nbsp;undocumented,&nbsp;and&nbsp;was&nbsp;created&nbsp;using&nbsp;inspection.&nbsp;Any&nbsp;values&nbsp;could</span>&nbsp;
<span class="visualBasic__com">'&nbsp;change&nbsp;in&nbsp;any&nbsp;version,&nbsp;and&nbsp;this&nbsp;list&nbsp;has&nbsp;only&nbsp;been&nbsp;tested&nbsp;for&nbsp;PowerPoint&nbsp;2010:</span>&nbsp;
&nbsp;
<span class="visualBasic__com">'&nbsp;No&nbsp;Style,&nbsp;No&nbsp;Grid:&nbsp;{2D5ABB26-0587-4C30-8999-92F81FD0307C}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{3C2FFA5D-87B4-456A-9821-1D502468CF0F}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{284E427A-3D55-4303-BF80-6455036E1DE7}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{69C7853C-536D-4A76-A0AE-DD22124D55A5}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{775DCB02-9BB8-47FD-8907-85C794F793BA}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{35758FB7-9AC5-4552-8A53-C91805E547FA}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{08FB837D-C827-4EFA-A057-4D05807E0F7C}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;No&nbsp;Style,&nbsp;Table&nbsp;Grid:&nbsp;{5940675A-B579-460E-94D1-54222C63F5DA}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{D113A9D2-9D6B-4929-AA2D-F23B5EE8CBE7}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{18603FDC-E32A-4AB5-989C-0864C3EAD2B8}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{306799F8-075E-4A3A-A7F6-7FBC6576F1A4}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{E269D01E-BC32-4049-B463-5C60D7B0CCD2}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{327F97BB-C833-4FB7-BDE5-3F7075034690}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Themed&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{638B1855-1B75-4FBE-930C-398BA8C253C6}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1:&nbsp;{9D7B26C5-4107-4FEC-AEDC-1716B250A1EF}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{3B4B98B0-60AC-42C2-AFA5-B58CD77FA1E5}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{0E3FDE45-AF77-4B5C-9715-49D594BDF05E}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{C083E6E3-FA7D-4D7B-A595-EF9225AFEA82}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{D27102A9-8310-4765-A935-A1911B00CA55}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{5FD0F851-EC5A-4D38-B0AD-8093EC10F338}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{68D230F3-CF80-4859-8CE7-A43EE81993B5}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2:&nbsp;{7E9639D4-E3E2-4D34-9284-5A2195B3D0D7}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{69012ECD-51FC-41F1-AA8D-1B2483CD663E}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{72833802-FEF1-4C79-8D5D-14CF1EAF98D9}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{F2DE63D5-997A-4646-A377-4702673A728D}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{17292A2E-F333-43FB-9621-5CBBE7FDCDCB}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{5A111915-BE36-4E01-A7E5-04B1672EAD32}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{912C8C85-51F0-491E-9774-3900AFEF0FD7}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3:&nbsp;{616DA210-FB5B-4158-B5E0-FEB733F419BA}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{BC89EF96-8CEA-46FF-86C4-4CE0E7609802}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{5DA37D80-6434-44D0-A028-1B22A696006F}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{8799B23B-EC83-4686-B30A-512413B5E67A}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{ED083AE6-46FA-4A59-8FB0-9F97EB10719F}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{BDBED569-4797-4DF1-A0F4-6AAB3CD982D8}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Light&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{E8B1032C-EA38-4F05-BA0D-38AFFFC7BED3}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1:&nbsp;{793D81CF-94F2-401A-BA57-92F5A7B2D0C5}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{B301B821-A1FF-4177-AEE7-76D212191A09}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{9DCAF9ED-07DC-4A11-8D7F-57B35C25682E}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{1FECB4D8-DB02-4DC6-A0A2-4F2EBAE1DC90}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{1E171933-4619-4E11-9A3F-F7608DF75F80}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{FABFCF23-3B69-468F-B69F-88F6DE6A72F2}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{10A1B5D5-9B99-4C35-A422-299274C87663}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2:&nbsp;{073A0DAA-6AF3-43AB-8588-CEC1D06C72B9}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{5C22544A-7EE6-4342-B048-85BDC9FD1C3A}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{21E4AEA4-8DFA-4A89-87EB-49C32662AFE0}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{F5AB1C69-6EDB-4FF4-983F-18BD219EF322}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{00A15C55-8517-42AA-B614-E9B94910E393}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{7DF18680-E054-41AD-8BC1-D1AEF772440D}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{93296810-A885-4BE3-A3E7-6D5BEEA58F35}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3:&nbsp;{8EC20E35-A176-4012-BC5E-935CFFF8708E}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{6E25E649-3F16-4E02-A733-19D2CDBF48F0}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{85BE263C-DBD7-4A20-BB59-AAB30ACAA65A}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{EB344D84-9AFB-497E-A393-DC336BA19D2E}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{EB9631B5-78F2-41C9-869B-9F39066F8104}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{74C1A8A3-306A-4EB7-A6B1-4F7E0EB9C5D6}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;3&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{2A488322-F2BA-4B5B-9748-0D474271808F}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4:&nbsp;{D7AC3CCA-C797-4891-BE02-D94E43425B78}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{69CF1AB2-1976-4502-BF36-3FF5EA218861}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{8A107856-5554-42FB-B03E-39F5DBC370BA}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{0505E3EF-67EA-436B-97B2-0124C06EBD24}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{C4B1156A-380E-4F78-BDF5-A606A8083BF9}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4&nbsp;-&nbsp;Accent&nbsp;5:&nbsp;{22838BEF-8BB2-4498-84A7-C5851F593DF1}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Medium&nbsp;Style&nbsp;4&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{16D9F66E-5EB9-4882-86FB-DCBF35E3C3E4}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1:&nbsp;{E8034E78-7F5D-4C2E-B375-FC64B27BC917}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;1:&nbsp;{125E5076-3810-47DD-B79F-674D7AD40C01}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;2:&nbsp;{37CE84F3-28C3-443E-9E96-99CF82512B78}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;3:&nbsp;{D03447BB-5D67-496B-8E87-E561075AD55C}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;4:&nbsp;{E929F9F4-4A8F-4326-A1B4-22849713DDAB}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;5:{8FD4443E-F989-4FC4-A0C8-D5A2AF1F390B}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;1&nbsp;-&nbsp;Accent&nbsp;6:&nbsp;{AF606853-7671-496A-8E4F-DF71F8EC918B}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;2:&nbsp;{5202B0CA-FC54-4496-8BCA-5EF66A818D29}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;1/Accent&nbsp;2:&nbsp;{0660B408-B3CF-4A94-85FC-2B1E0A45F4A2}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;3/Accent&nbsp;4:&nbsp;{91EBBBCC-DAD2-459C-BE2E-F6DE35CF9A28}</span>&nbsp;
<span class="visualBasic__com">'&nbsp;Dark&nbsp;Style&nbsp;2&nbsp;-&nbsp;Accent&nbsp;5/Accent&nbsp;6:&nbsp;{46F890A9-2807-4EBB-B81D-B2AA78EC7F39}</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26175" href="/site/view/file/26175/1/PPT.Table.ApplyStyle.txt">PPT.Table.ApplyStyle.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26176" href="/site/view/file/26176/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
