# Excel 2010: Programming with Slicers
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* PivotTable
* adding slicers
* PivotChart
## IsPublished
* True
## ModifiedDate
* 2011-08-10 02:50:59
## Description

<p><strong>Introduction</strong></p>
<p>Learn how to programmatically add slicers to a Microsoft Excel 2010 worksheet. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff467294.aspx">Programming with Slicers in Excel 2010</a> in the MSDN Library.</p>
<p><strong>Description</strong></p>
<p>Microsoft Excel 2010 slicers are easy to use controls that let the user visually filter data by selecting values from a list. Connecting slicers to Microsoft PivotTable dynamic views or Microsoft PivotChart dynamic views allows the user to quickly filter
 data for different scenarios during analysis.</p>
<p>This sample download illustrates how to use the Excel 2010 primary interop assembly to create a worksheet, add data to the worksheet, and add a PivotTable and PivotChart to the worksheet. After the PivotTable and PivotChart are added to the worksheet, slicers
 are added and connected to the PivotTable and PivotChart.</p>
<p>The following code example shows how to use the Excel 2010 primary interop assembly to add a PivotTable to a worksheet based on a preselected data range. The code uses the
<strong>Workbook.PivotTableWizard</strong> method to create the PivotTable. The properties and method of the
<strong>PivotTable </strong>interface and the <strong>PivotTable.PivotFields</strong> collection are then used to format the
<strong>PivotTable </strong>interface.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">//Select a range of data for the PivotTable.
pivotData = sheet.get_Range(&quot;A1&quot;, &quot;D19&quot;);

//Select location of the PivotTable.
pivotDestination = sheet.get_Range(&quot;F2&quot;);

//Add a PivotTable to the worksheet.
sheet.PivotTableWizard(
    XlPivotTableSourceType.xlDatabase,
    pivotData,
    pivotDestination,
    pivotTableName
    );

//Set variables used to manipulate the PivotTable.
pivotTable =
  (PivotTable)sheet.PivotTables(pivotTableName);
salesRegion = ((PivotField)pivotTable.PivotFields(3));
salesAmount = ((PivotField)pivotTable.PivotFields(4));

//Format the PivotTable.
pivotTable.TableStyle2 = &quot;PivotStyleLight16&quot;;
pivotTable.InGridDropZones = false;

//Set Sales Region as a Row Field.
salesRegion.Orientation =
  XlPivotFieldOrientation.xlRowField;

//Set Sum of Sales Amount as a Value Field.
salesAmount.Orientation =
  XlPivotFieldOrientation.xlDataField;
salesAmount.Function = XlConsolidationFunction.xlSum;
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//Select&nbsp;a&nbsp;range&nbsp;of&nbsp;data&nbsp;for&nbsp;the&nbsp;PivotTable.</span>&nbsp;
pivotData&nbsp;=&nbsp;sheet.get_Range(<span class="cs__string">&quot;A1&quot;</span>,&nbsp;<span class="cs__string">&quot;D19&quot;</span>);&nbsp;
&nbsp;
<span class="cs__com">//Select&nbsp;location&nbsp;of&nbsp;the&nbsp;PivotTable.</span>&nbsp;
pivotDestination&nbsp;=&nbsp;sheet.get_Range(<span class="cs__string">&quot;F2&quot;</span>);&nbsp;
&nbsp;
<span class="cs__com">//Add&nbsp;a&nbsp;PivotTable&nbsp;to&nbsp;the&nbsp;worksheet.</span>&nbsp;
sheet.PivotTableWizard(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;XlPivotTableSourceType.xlDatabase,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pivotData,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pivotDestination,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;pivotTableName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;
&nbsp;
<span class="cs__com">//Set&nbsp;variables&nbsp;used&nbsp;to&nbsp;manipulate&nbsp;the&nbsp;PivotTable.</span>&nbsp;
pivotTable&nbsp;=&nbsp;
&nbsp;&nbsp;(PivotTable)sheet.PivotTables(pivotTableName);&nbsp;
salesRegion&nbsp;=&nbsp;((PivotField)pivotTable.PivotFields(<span class="cs__number">3</span>));&nbsp;
salesAmount&nbsp;=&nbsp;((PivotField)pivotTable.PivotFields(<span class="cs__number">4</span>));&nbsp;
&nbsp;
<span class="cs__com">//Format&nbsp;the&nbsp;PivotTable.</span>&nbsp;
pivotTable.TableStyle2&nbsp;=&nbsp;<span class="cs__string">&quot;PivotStyleLight16&quot;</span>;&nbsp;
pivotTable.InGridDropZones&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;
<span class="cs__com">//Set&nbsp;Sales&nbsp;Region&nbsp;as&nbsp;a&nbsp;Row&nbsp;Field.</span>&nbsp;
salesRegion.Orientation&nbsp;=&nbsp;
&nbsp;&nbsp;XlPivotFieldOrientation.xlRowField;&nbsp;
&nbsp;
<span class="cs__com">//Set&nbsp;Sum&nbsp;of&nbsp;Sales&nbsp;Amount&nbsp;as&nbsp;a&nbsp;Value&nbsp;Field.</span>&nbsp;
salesAmount.Orientation&nbsp;=&nbsp;
&nbsp;&nbsp;XlPivotFieldOrientation.xlDataField;&nbsp;
salesAmount.Function&nbsp;=&nbsp;XlConsolidationFunction.xlSum;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;
<p>The following code shows how to use the Excel 2010 primary interop assembly to add a PivotChart to a workbook by using the same data range used to create the pivot table. The code uses the
<strong>ChartObjects.Add</strong> method to create the PivotChart. The <strong>ChartObject.Chart</strong> property is then used to format the PivotChart. Named arguments are used because all parameters are not being passed to the
<strong>ChartWizard </strong>method.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">//Add a PivotChart to the work sheet.
chartObjects = (ChartObjects)sheet.ChartObjects();
pivotChart = chartObjects.Add(310, 100, 225, 175);

//Format the PivotChart.
pivotChart.Chart.ChartWizard(pivotData,
  XlChartType.xlColumnClustered,
  Title: &quot;Sales&quot;,
  HasLegend: false,
  CategoryLabels: 3,
  SeriesLabels: 0);
</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//Add&nbsp;a&nbsp;PivotChart&nbsp;to&nbsp;the&nbsp;work&nbsp;sheet.</span>&nbsp;
chartObjects&nbsp;=&nbsp;(ChartObjects)sheet.ChartObjects();&nbsp;
pivotChart&nbsp;=&nbsp;chartObjects.Add(<span class="js__num">310</span>,&nbsp;<span class="js__num">100</span>,&nbsp;<span class="js__num">225</span>,&nbsp;<span class="js__num">175</span>);&nbsp;
&nbsp;
<span class="js__sl_comment">//Format&nbsp;the&nbsp;PivotChart.</span>&nbsp;
pivotChart.Chart.ChartWizard(pivotData,&nbsp;
&nbsp;&nbsp;XlChartType.xlColumnClustered,&nbsp;
&nbsp;&nbsp;Title:&nbsp;<span class="js__string">&quot;Sales&quot;</span>,&nbsp;
&nbsp;&nbsp;HasLegend:&nbsp;false,&nbsp;
&nbsp;&nbsp;CategoryLabels:&nbsp;<span class="js__num">3</span>,&nbsp;
&nbsp;&nbsp;SeriesLabels:&nbsp;<span class="js__num">0</span>);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;
<p>The following code shows how to use the Excel 2010 primary interop assembly to add slicers that are&nbsp;connected to the PivotTable. Because the PivotTable and the PivotChart are using the same data, you do not have to create separate slicers for the PivotChart.
 The PivotTable and PivotChart share the same slicers.</p>
<p>Three slicers are added: Sales Type, Sales Region, and Sales Person. All slicers follow the same creation process. The only difference is the column used for filtering and the location on the worksheet.</p>
<p>The <strong>Workbook.SlicerCaches</strong> method of the workbook creates the slicers. The first parameter of the method associates the slicer with the PivotTable and the second parameter identifies the column to use for filtering. The selection items displayed
 in the slicer&nbsp;are based on the column values.</p>
<p>The <strong>eWorkbook.SlicerCaches.Add</strong> method adds the slicer to the worksheet&nbsp;and defines its position. The example uses named arguments because all parameters are not passed to the
<strong>Add</strong> method.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">//Add slicers to the PivotTable.
salesTypeSlicer =
  eWorkbook.SlicerCaches.Add(pivotTable, &quot;Sales Type&quot;);
salesTypeSlicer.Slicers.Add(sheet,
  Top: 10, Left: 540, Width: 100, Height: 100);
salesRegionSlicer =
  eWorkbook.SlicerCaches.Add(pivotTable, &quot;Sales Region&quot;);
salesRegionSlicer.Slicers.Add(sheet,
  Top: 120, Left: 540, Width: 100, Height: 100);
salesPersonSlicer =
  eWorkbook.SlicerCaches.Add(pivotTable, &quot;Sales Person&quot;);
salesPersonSlicer.Slicers.Add(sheet,
  Top: 10, Left: 645, Width: 100, Height: 200);
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//Add&nbsp;slicers&nbsp;to&nbsp;the&nbsp;PivotTable.</span>&nbsp;
salesTypeSlicer&nbsp;=&nbsp;
&nbsp;&nbsp;eWorkbook.SlicerCaches.Add(pivotTable,&nbsp;<span class="cs__string">&quot;Sales&nbsp;Type&quot;</span>);&nbsp;
salesTypeSlicer.Slicers.Add(sheet,&nbsp;
&nbsp;&nbsp;Top:&nbsp;<span class="cs__number">10</span>,&nbsp;Left:&nbsp;<span class="cs__number">540</span>,&nbsp;Width:&nbsp;<span class="cs__number">100</span>,&nbsp;Height:&nbsp;<span class="cs__number">100</span>);&nbsp;
salesRegionSlicer&nbsp;=&nbsp;
&nbsp;&nbsp;eWorkbook.SlicerCaches.Add(pivotTable,&nbsp;<span class="cs__string">&quot;Sales&nbsp;Region&quot;</span>);&nbsp;
salesRegionSlicer.Slicers.Add(sheet,&nbsp;
&nbsp;&nbsp;Top:&nbsp;<span class="cs__number">120</span>,&nbsp;Left:&nbsp;<span class="cs__number">540</span>,&nbsp;Width:&nbsp;<span class="cs__number">100</span>,&nbsp;Height:&nbsp;<span class="cs__number">100</span>);&nbsp;
salesPersonSlicer&nbsp;=&nbsp;
&nbsp;&nbsp;eWorkbook.SlicerCaches.Add(pivotTable,&nbsp;<span class="cs__string">&quot;Sales&nbsp;Person&quot;</span>);&nbsp;
salesPersonSlicer.Slicers.Add(sheet,&nbsp;
&nbsp;&nbsp;Top:&nbsp;<span class="cs__number">10</span>,&nbsp;Left:&nbsp;<span class="cs__number">645</span>,&nbsp;Width:&nbsp;<span class="cs__number">100</span>,&nbsp;Height:&nbsp;<span class="cs__number">200</span>);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
</div>
