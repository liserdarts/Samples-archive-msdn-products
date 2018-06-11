# Excel 2010: Programming with Sparklines
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Sparklines
## IsPublished
* True
## ModifiedDate
* 2011-08-12 10:02:10
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to programmatically add Sparklines to a Microsoft Excel 2010 worksheet. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff521866.aspx">Programming with Sparklines in Excel 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Excel 2010 introduces Sparklines, which enable you can create small charts in a single cell to quickly discover patterns in your data. It is a quick way to highlight important data trends such as seasonal increases or decreases.</p>
<p>This sample code shows how to use the Excel 2010 primary interop assembly to create a worksheet, add sales data, and add Sparklines to show trends in the sales data.</p>
<p>The following code example shows how to use the Excel 2010 primary interop assembly to add a Sparkline to a cell by using the
<strong>Range.SparklineGroups.Add</strong> method. The first parameter uses the <strong>
XlSparkType.xlSparkLine</strong> value to create a Line Sparkline; other kinds of Sparklines include Column and Win/Loss. The second parameter defines the range of cells that contain the data for the Sparkline.</p>
<p>The Sparkline is then formatted. The color is set by using the <strong>SparklineGroup.SeriesColor.ThemeColor</strong> property. The
<strong>SparklineGroup.Points.Highpoint.Visible</strong> property and the <strong>
SparklineGroup.Points.Lowpoint.Visible</strong> property are set to <strong>true</strong> to highlight the High data points and Low data points in the Sparkline. Other data points that could be highlighted include the First, Last, and Negative data points.
 It is also possible to display a marker for all data points on a Line Sparkline.</p>
<p>After the initial Sparkline is created and formatted, use the <strong>Range.Copy</strong> method to copy the Sparkline to the end of the remaining rows.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// Add a Sparkline to the cell at the end of the first row.
sparklineLocation = sheet.get_Range(&quot;F2&quot;);
sparkline = sparklineLocation.SparklineGroups.Add(
  XlSparkType.xlSparkLine, &quot;B2:E2&quot;);

// Format the Sparkline by defining a color theme.
sparkline.SeriesColor.ThemeColor = 5;

// Display a mark for the high and low data point.
sparkline.Points.Highpoint.Visible = true;
sparkline.Points.Lowpoint.Visible = true;

// Copy the Sparkline to the end of the remaining rows.
sparklineLocation.Copy(sheet.get_Range(&quot;F3:F6&quot;));
// Add a Sparkline to the cell at the end of the first row. 
sparklineLocation = sheet.get_Range(&quot;F2&quot;); 
sparkline = sparklineLocation.SparklineGroups.Add( 
  XlSparkType.xlSparkLine, &quot;B2:E2&quot;); 
 
// Format the Sparkline by defining a color theme. 
sparkline.SeriesColor.ThemeColor = 5; 
 
// Display a mark for the high and low data point. 
sparkline.Points.Highpoint.Visible = true; 
sparkline.Points.Lowpoint.Visible = true; 
 
// Copy the Sparkline to the end of the remaining rows. 
sparklineLocation.Copy(sheet.get_Range(&quot;F3:F6&quot;));</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Add&nbsp;a&nbsp;Sparkline&nbsp;to&nbsp;the&nbsp;cell&nbsp;at&nbsp;the&nbsp;end&nbsp;of&nbsp;the&nbsp;first&nbsp;row.</span>&nbsp;
sparklineLocation&nbsp;=&nbsp;sheet.get_Range(<span class="cs__string">&quot;F2&quot;</span>);&nbsp;
sparkline&nbsp;=&nbsp;sparklineLocation.SparklineGroups.Add(&nbsp;
&nbsp;&nbsp;XlSparkType.xlSparkLine,&nbsp;<span class="cs__string">&quot;B2:E2&quot;</span>);&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Format&nbsp;the&nbsp;Sparkline&nbsp;by&nbsp;defining&nbsp;a&nbsp;color&nbsp;theme.</span>&nbsp;
sparkline.SeriesColor.ThemeColor&nbsp;=&nbsp;<span class="cs__number">5</span>;&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Display&nbsp;a&nbsp;mark&nbsp;for&nbsp;the&nbsp;high&nbsp;and&nbsp;low&nbsp;data&nbsp;point.</span>&nbsp;
sparkline.Points.Highpoint.Visible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
sparkline.Points.Lowpoint.Visible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Copy&nbsp;the&nbsp;Sparkline&nbsp;to&nbsp;the&nbsp;end&nbsp;of&nbsp;the&nbsp;remaining&nbsp;rows.</span>&nbsp;
sparklineLocation.Copy(sheet.get_Range(<span class="cs__string">&quot;F3:F6&quot;</span>));&nbsp;
<span class="cs__com">//&nbsp;Add&nbsp;a&nbsp;Sparkline&nbsp;to&nbsp;the&nbsp;cell&nbsp;at&nbsp;the&nbsp;end&nbsp;of&nbsp;the&nbsp;first&nbsp;row.&nbsp;</span>&nbsp;
sparklineLocation&nbsp;=&nbsp;sheet.get_Range(<span class="cs__string">&quot;F2&quot;</span>);&nbsp;&nbsp;
sparkline&nbsp;=&nbsp;sparklineLocation.SparklineGroups.Add(&nbsp;&nbsp;
&nbsp;&nbsp;XlSparkType.xlSparkLine,&nbsp;<span class="cs__string">&quot;B2:E2&quot;</span>);&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//&nbsp;Format&nbsp;the&nbsp;Sparkline&nbsp;by&nbsp;defining&nbsp;a&nbsp;color&nbsp;theme.&nbsp;</span>&nbsp;
sparkline.SeriesColor.ThemeColor&nbsp;=&nbsp;<span class="cs__number">5</span>;&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//&nbsp;Display&nbsp;a&nbsp;mark&nbsp;for&nbsp;the&nbsp;high&nbsp;and&nbsp;low&nbsp;data&nbsp;point.&nbsp;</span>&nbsp;
sparkline.Points.Highpoint.Visible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;
sparkline.Points.Lowpoint.Visible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;
&nbsp;&nbsp;
<span class="cs__com">//&nbsp;Copy&nbsp;the&nbsp;Sparkline&nbsp;to&nbsp;the&nbsp;end&nbsp;of&nbsp;the&nbsp;remaining&nbsp;rows.&nbsp;</span>&nbsp;
sparklineLocation.Copy(sheet.get_Range(<span class="cs__string">&quot;F3:F6&quot;</span>));</pre>
</div>
</div>
</div>
<div class="endscriptcode">The sample code creates a workbook named Excel2010Sparklines.xlsx in the C:\Temp folder. (A sample
<a href="http://code.msdn.microsoft.com/Excel-2010-Programming-45837a56/file/41297/1/Excel2010Sparklines.xlsx">
Excel2010Sparklines.xlsx</a> workbook is attached to this description.) The code then adds trending data and a Sparkline to each row to show the sales trend.</div>
