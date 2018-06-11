# PerformancePoin​t Services 2010: Change Analytic Report Data Source Sample
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2010
* PerformancePoint Services
## Topics
* SharePoint
## IsPublished
* True
## ModifiedDate
* 2011-08-10 11:47:56
## Description

<p><span style="font-size:small">The Change Analytic Report Data Source Sample enables users to change the data source that a PerformancePoint analytic report points to.
</span></p>
<p><span style="font-size:small">The sample calls the following&nbsp;PerformancePoint server-side APIs to perform related operations:&nbsp;</span></p>
<ul>
<li><span style="font-size:small">The SPDataStore.GetListItems method to retrieve list items
</span></li><li><span style="font-size:small">The BIMonitoringServiceApplicationProxy.GetDataSource method to retrieve a data source
</span></li><li><span style="font-size:small">The BIMonitoringServiceApplicationProxy.GetMdx method to retrieve the report's MDX query
</span></li><li><span style="font-size:small">The OLAPReportViewData object to rebuild the report's CustomData property
</span></li><li><span style="font-size:small">The SPDataStore.UpdateReportView method to update the report
</span></li></ul>
<p>&nbsp;<br>
<span style="font-size:small">The following code snippet shows how the sample rebuilds the report's CustomData property and updates the report.</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">foreach  (ReportView report in selectedAnalyticReports)
{
    try 
    {
        // Change the reference to the data source location for the report.
        // Analytic reports store the data source location in the CustomData property.
        string queryMDX = BIMonitoringServiceApplicationProxy.Default.GetMdx(report.Location);

        // Get the report's OLAPReportViewData object, which is the core of an analytic view.
        OLAPReportViewData olapViewData = report.GetOlapReportViewData();

        // Set up the data source in the query state so the &quot;Revert to Design mode&quot; option in
        // Dashboard Designer works correctly.
        olapViewData.QueryState.DataSourceLocation = dataSource.Location;

        // Use an OLAPQueryData object to store the data source location and the MDX query.
        olapViewData.QueryData.DataSourceLocation = dataSource.Location;
        olapViewData.QueryData.TokenizedMDX = queryMDX;

        // Set the CustomData property to the serialized OLAPReportViewData object.
        report.CustomData = OLAPReportViewData.Serialize(olapViewData);

        SPDataStore.GlobalDataStore.UpdateReportView(report);
    }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">foreach</span>&nbsp;&nbsp;(ReportView&nbsp;report&nbsp;<span class="cs__keyword">in</span>&nbsp;selectedAnalyticReports)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Change&nbsp;the&nbsp;reference&nbsp;to&nbsp;the&nbsp;data&nbsp;source&nbsp;location&nbsp;for&nbsp;the&nbsp;report.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Analytic&nbsp;reports&nbsp;store&nbsp;the&nbsp;data&nbsp;source&nbsp;location&nbsp;in&nbsp;the&nbsp;CustomData&nbsp;property.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;queryMDX&nbsp;=&nbsp;BIMonitoringServiceApplicationProxy.Default.GetMdx(report.Location);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;the&nbsp;report's&nbsp;OLAPReportViewData&nbsp;object,&nbsp;which&nbsp;is&nbsp;the&nbsp;core&nbsp;of&nbsp;an&nbsp;analytic&nbsp;view.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OLAPReportViewData&nbsp;olapViewData&nbsp;=&nbsp;report.GetOlapReportViewData();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Set&nbsp;up&nbsp;the&nbsp;data&nbsp;source&nbsp;in&nbsp;the&nbsp;query&nbsp;state&nbsp;so&nbsp;the&nbsp;&quot;Revert&nbsp;to&nbsp;Design&nbsp;mode&quot;&nbsp;option&nbsp;in</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Dashboard&nbsp;Designer&nbsp;works&nbsp;correctly.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;olapViewData.QueryState.DataSourceLocation&nbsp;=&nbsp;dataSource.Location;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Use&nbsp;an&nbsp;OLAPQueryData&nbsp;object&nbsp;to&nbsp;store&nbsp;the&nbsp;data&nbsp;source&nbsp;location&nbsp;and&nbsp;the&nbsp;MDX&nbsp;query.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;olapViewData.QueryData.DataSourceLocation&nbsp;=&nbsp;dataSource.Location;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;olapViewData.QueryData.TokenizedMDX&nbsp;=&nbsp;queryMDX;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Set&nbsp;the&nbsp;CustomData&nbsp;property&nbsp;to&nbsp;the&nbsp;serialized&nbsp;OLAPReportViewData&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;report.CustomData&nbsp;=&nbsp;OLAPReportViewData.Serialize(olapViewData);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SPDataStore.GlobalDataStore.UpdateReportView(report);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small">ChangeDataSource.aspx.cs is the code-behind class for the SharePoint modal dialog box. It uses PerformancePoint and SharePoint server-side APIs to retrieve and save PerformancePoint objects and to find the PerformancePoint
 Data Connections Libraries&nbsp;in the site. </span></li><li><span style="font-size:small">Elements.xml&nbsp;contains the functions that are used by the ribbon button to work with
</span><span style="font-family:verdana,geneva"><span style="font-size:small">the selected PerformancePoint list items.</span></span>
<em><em></em></em></li></ul>
<h1>More Information</h1>
<p><span style="font-size:small">For more information about programming with PerformancePoint Services, see the following resources:</span></p>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/bb848116.aspx">PerformancePoint Services in SharePoint Server 2010</a> (developer documentation)</span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/sharepoint/gg176656.aspx" target="_blank">PerformancePoint Services Resource Center</a></span>
</li></ul>
