# SharePoint 2013: Small business sales pipeline manager
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-05-06 12:31:50
## Description

<div id="header">Demonstrates using JavaScript and jQuery in an app for SharePoint to implement a relatively complex, real-world scenario for managing sales leads as they pass through various stages in a sales pipeline.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p><span class="label">Provided by: </span><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a>.</p>
<p>The solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions. The list data
 included in this solution represents sales leads, opportunities, closed sales, and lost sales.</p>
<p>The user interface is implemented with simple HTML elements and cascading style sheet styles (CSS) to present a modern look and feel.</p>
<p>All aspects of the user interface are controlled using JavaScript and JQuery. The solution contains no server-side code.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012 (full release version; no beta or release candidates)</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012 (full release version; no beta or release candidates)</p>
</li><li>
<p>Either one of the following:</p>
<ul>
<li>
<p>Access to an Office 365 Developer Site configured to host apps (recommended)</p>
</li><li>
<p>SharePoint Server 2013 (RTM) configured to host apps, and with a Developer Site collection already created</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection1">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The Default.aspx webpage, which is used to present the data at different stages through a sales pipeline. This webpage also displays graphical reports and charts that let the user see the entire pipeline at a glance.</p>
</li><li>
<p>The App.js file in the scripts folder, which is used to retrieve and manage the pipeline data by using the JavaScript (JSOM) implementation of the client object model (CSOM). The App.js file also contains the user interface logic that is implemented in Default.aspx.</p>
</li><li>
<p>The App.css file in the contents folder, which contains style definitions used by the elements in Default.aspx.</p>
</li><li>
<p>A list definition and instance named Prospects, which is used to store the sales activity in SharePoint.</p>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection2">
<p>Follow these steps to configure the sample.</p>
<div class="subSection">
<ol>
<li>
<p>Open SP_PipelineManager_js.sln using Visual Studio 2012.</p>
</li><li>
<p>In the <span class="ui">Properties</span> window, add the full URL to your Office 365 Developer Site or SharePoint Server 2013 Developer Site collection to the
<span><span class="keyword">Site URL</span></span> property.</p>
</li><li>
<p>If prompted, provide credentials.</p>
</li></ol>
</div>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection3">
<p>Press CTRL&#43;SHIFT&#43;B to build the solution.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<p>&nbsp;</p>
<div class="subSection">
<ol>
<li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 or Office 365 Developer Site if you are prompted.</p>
</li><li>
<p>When the app opens, the start screen resembles Figure 1. Note that in Figure 1, the user clicked the
<span class="ui">Leads</span> tile, which shows two leads. The leads data was deployed with the sample solution.</p>
<div class="caption"><strong>Figure 1. Pipeline manager start page</strong></div>
<br>
<img src="/site/view/file/81659/1/image.png" alt=""> </li><li>
<p>Users click the <span class="ui">Leads</span> tab to create new leads or to expose existing leads. When viewing existing leads, as shown in Figure 2, you can edit information for the lead.</p>
<div class="caption"><strong>Figure 2. Edit leads page</strong></div>
<br>
<img src="/site/view/file/81660/1/image.png" alt=""> </li><li>
<p>Opportunities are the next stage and represent sales leads that develop into sales opportunities. Clicking the
<span class="ui">Opportunities</span> tab opens the Opportunities page, where you can see sales leads that have been converted to opportunities. Clicking a specific opportunity opens the Edit Opportunity page (shown in Figure 3), where you can do several
 things. You can edit details of the opportunity, you can optionally upload a supporting document, or you can close the opportunity, either as a sale or as a lost sale.</p>
<div class="caption"><strong>Figure 3. Sales opportunity page</strong></div>
<br>
<img src="/site/view/file/81661/1/image.png" alt=""> </li><li>
<p>When you convert a sales opportunity into a sale (or when you click the <span class="ui">
Sale</span> tile), you are shown a list of sales. Click one of the listed sales to see the sale details, as shown in Figure 4. Similarly, you can click the
<span class="ui">Lost Sale</span> tile to show a list of lost sales, and to have access to details of the lost sales.</p>
<div class="caption"><strong>Figure 4. Sales details page</strong></div>
<br>
<img src="/site/view/file/81662/1/image.png" alt=""> </li><li>
<p>Click the <span class="ui">Reports</span> tile to select from two report types: the
<span class="ui">Pipeline Amount($)</span> report or the <span class="ui">Pipeline Number</span> report.</p>
<p>Clicking the Pipeline Amount($) report shows the entire sales pipeline, overlaid with monetary values for each stage. Amounts for both sales and lost sales are also displayed (see Figure 5).</p>
<p>Note that the reports are interactive and clickable, so when you click a given stage in the pipeline, details for that stage are displayed below the chart. For example, if you click the Lead stage, details for sales prospects currently in that stage are
 displayed.</p>
<div class="caption"><strong>Figure 5. Pipeline Amount($) report</strong></div>
<br>
<img src="/site/view/file/81663/1/image.png" alt=""> </li><li>
<p>When you click the Pipeline Number report, the entire sales pipeline is again displayed, but this time it is overlaid with the number of prospects in each stage. The chart shows the numbers of sales won and lost, as shown in Figure 6.</p>
<div class="caption"><strong>Figure 6. Pipeline Number report</strong></div>
<br>
<img src="/site/view/file/81664/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection5">
<p>Make sure that you have a properly configured SharePoint Server 2013 installation that has a Developer Site collection already created; or, that you have signed up for an Office 365 Developer Site configured to host apps. Also, make sure that you are using
 the released versions of Visual Studio 2012 and Office Developer Tools for Visual Studio 2012.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>April 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj692554.aspx" target="_blank">How to: Provision a Developer Site using your existing Office 365 subscription</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/office/apps/fp160950.aspx" target="_blank">Build apps for Office and SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/site/search?f%5B0%5D.Type=Technology&f%5B0%5D.Value=apps%20for%20SharePoint" target="_blank">Apps for Office and SharePoint samples</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/Apps-for-SharePoint-sample-64c80184" target="_blank">Apps for SharePoint sample pack</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
