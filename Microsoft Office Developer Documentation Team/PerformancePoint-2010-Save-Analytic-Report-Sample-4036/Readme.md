# PerformancePoint Services 2010: Save Analytic Report Sample
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
* 2011-08-03 04:48:04
## Description

<p><span style="font-family:verdana,geneva; font-size:small">The Save Analytic Report Sample creates a &quot;Save as Favorite&quot; feature that enables users to save a copy of a PerformancePoint analytic report in its current navigated state to a PerformancePoint Content
 List within the site. </span></p>
<p><span style="font-family:verdana,geneva; font-size:small">The sample calls the ClientConnectionManager object (from the PerformancePoint JavaScript object model) to retrieve information about the PerformancePoint Web Parts on the page. It also uses the PerformancePoint
 server-side API to retrieve the report in its current navigated state from the back-end database (using the BIMonitoringServiceApplicationProxy.GetAnalyticReportView method) and to save a copy of the report (using the SPDataStore.CreateReportView method).</span></p>
<p><span style="font-family:verdana,geneva; font-size:small">The following code snippet shows how the sample finds PerformancePoint analytic reports on the page and how it gets the lastReportId property, which is used to retrieve the navigated report from the
 back-end database.</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">// Get report information that is stored in ViewState.
function getReportsFromWebPartRecords() { 
    var webPartRecords = PPSMA.ClientConnectionManager.get_instance().get_connectionManagerRecord().WebPartRecords;
    var reports = new Array();
    var index = 0;
    for (var i = 0; i &lt; webPartRecords.length; i&#43;&#43;) {
        var wP = webPartRecords[i];
        if ((wP.ViewState[&quot;D90A8712A3FA4649A25B7AB942FBCF20&quot;]) &amp;&amp;
           ((wP.ViewState[&quot;D90A8712A3FA4649A25B7AB942FBCF20&quot;] == &quot;OLAPGrid&quot;) ||
           (wP.ViewState[&quot;D90A8712A3FA4649A25B7AB942FBCF20&quot;] == &quot;AnalyticChart&quot;))) {

            reports[index] = new Array();

            // The lastReportId property contains the identifier of the current version of the report. For navigated reports, 
            // the ID is a GUID and the report definition is stored in the back-end database.
            reports[index].lastReportId = wP.ViewState[&quot;F775B8BE98A540C2AB08B34D53460E4B&quot;];
            reports[index].clientId = wP.ClientId;

            var clientWP = PPSMA.ClientConnectionManager.get_instance().findClientWebPart(wP.ClientId);
            reports[index].name = clientWP.webPartTitle;
            index&#43;&#43;;
        }
    }
    return reports;
}</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;Get&nbsp;report&nbsp;information&nbsp;that&nbsp;is&nbsp;stored&nbsp;in&nbsp;ViewState.</span>&nbsp;
<span class="js__operator">function</span>&nbsp;getReportsFromWebPartRecords()&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;webPartRecords&nbsp;=&nbsp;PPSMA.ClientConnectionManager.get_instance().get_connectionManagerRecord().WebPartRecords;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;reports&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__object">Array</span>();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;index&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;webPartRecords.length;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;wP&nbsp;=&nbsp;webPartRecords[i];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;((wP.ViewState[<span class="js__string">&quot;D90A8712A3FA4649A25B7AB942FBCF20&quot;</span>])&nbsp;&amp;&amp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((wP.ViewState[<span class="js__string">&quot;D90A8712A3FA4649A25B7AB942FBCF20&quot;</span>]&nbsp;==&nbsp;<span class="js__string">&quot;OLAPGrid&quot;</span>)&nbsp;||&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(wP.ViewState[<span class="js__string">&quot;D90A8712A3FA4649A25B7AB942FBCF20&quot;</span>]&nbsp;==&nbsp;<span class="js__string">&quot;AnalyticChart&quot;</span>)))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reports[index]&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__object">Array</span>();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;The&nbsp;lastReportId&nbsp;property&nbsp;contains&nbsp;the&nbsp;identifier&nbsp;of&nbsp;the&nbsp;current&nbsp;version&nbsp;of&nbsp;the&nbsp;report.&nbsp;For&nbsp;navigated&nbsp;reports,&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;the&nbsp;ID&nbsp;is&nbsp;a&nbsp;GUID&nbsp;and&nbsp;the&nbsp;report&nbsp;definition&nbsp;is&nbsp;stored&nbsp;in&nbsp;the&nbsp;back-end&nbsp;database.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reports[index].lastReportId&nbsp;=&nbsp;wP.ViewState[<span class="js__string">&quot;F775B8BE98A540C2AB08B34D53460E4B&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reports[index].clientId&nbsp;=&nbsp;wP.ClientId;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;clientWP&nbsp;=&nbsp;PPSMA.ClientConnectionManager.get_instance().findClientWebPart(wP.ClientId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reports[index].name&nbsp;=&nbsp;clientWP.webPartTitle;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;index&#43;&#43;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;reports;&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-family:verdana,geneva">SaveReport.aspx.cs is the code-behind class for the SharePoint modal dialog box. It uses PerformancePoint and SharePoint server-side APIs to retrieve and save&nbsp;PerformancePoint analytic reports and to&nbsp;find
 the PerformancePoint Content Lists in the site. </span></li><li><span style="font-family:verdana,geneva">SaveReportFavoriteButtonScript.js contains the functions that are used by the ribbon button. It uses the PerformancePoint JSOM to retrieve information about the PerformancePoint Web Parts on the page.
</span></li></ul>
<h1>More Information</h1>
<p><span style="font-family:verdana,geneva">For more information about programming with PerformancePoint Services, see the following resources:</span></p>
<ul>
<li><span style="font-family:verdana,geneva"><a href="http://msdn.microsoft.com/en-us/library/bb848116.aspx" target="_blank">PerformancePoint Services in SharePoint Server 2010</a> (developer documentation)
</span></li><li><span style="font-family:verdana,geneva"><a href="http://msdn.microsoft.com/en-us/sharepoint/gg176656.aspx" target="_blank">PerformancePoint Services Resource Center</a>
</span></li></ul>
