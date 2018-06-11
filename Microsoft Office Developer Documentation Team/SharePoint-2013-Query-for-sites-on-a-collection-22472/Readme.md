# SharePoint 2013: Query for sites on a site collection
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-06-06 03:01:28
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Query for sites on a site collection</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>Demonstrates how to use app-only permissions from a provider-hosted app to perform operations the user would not otherwise be allowed to perform.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">
Scott Hillier</a>, <a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path Training</a></p>
<p>This sample lets a user search for Community sites within the current site collection. The results of the search indicate whether the user is a member of each individual Community site. The search returns the top 25 sites based on the total number of members.
 This allows the most-popular sites to appear at the top of the results. If the user wants to see a sample of the activity in the site, they can display the top 10 questions from the site by order of popularity. After looking over the information, the user
 can link to the site and become a member.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This provider app is written using the <a href="http://www.asp.net/mvc/mvc4" target="_blank">
ASP.NET MVC4</a> project template. The <span value="CommunitiesController.cs"><b><span class="keyword">CommunitiesController.cs</span></b></span> file contains the majority of the sample code. The app executes searches to discover Community sites within the
 current site collection. It then uses the managed client object model to read the membership list for the site and determine whether the current user is a member. Because the current user may not have sufficient rights to read the membership list, the app
 uses <a href="http://msdn.microsoft.com/en-us/library/fp142384.aspx" target="_blank">
app-only permissions</a> and requests the right to read all lists in the site collection.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The sample does not need to be configured, but it requires access to one or more Community sites. You may need to create a set of Community sites before testing this app.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<div>
<ol>
<li>
<p>Open the <span value="JoinACommunity.sln"><b><span class="keyword">JoinACommunity.sln</span></b></span> solution in Visual Studio 2012.</p>
</li><li>
<p>Edit the Site URL property to refer to a test site where you will deploy the solution.</p>
</li><li>
<p>Edit the <span value="web.config"><b><span class="keyword">web.config</span></b></span><span value="appSettings"><b><span class="keyword">appSettings</span></b></span> to refer to your test certificate.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When prompted, grant permissions for the app.</p>
</li><li>
<p>When the app appears, you will see default search results showing the Top 25 Community sites by membership.</p>
</li><li>
<p>Use the text box to search for Community sites containing the given keywords.</p>
</li><li>
<p>Click the &quot;<b><span class="ui">Top Questions</span></b>&quot; link to view the top questions from a Community site.</p>
</li><li>
<p>Click the associated site link to join a site.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Ensure that your <span value="web.config"><b><span class="keyword">web.config</span></b></span> settings are correct for your environment.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5" name="collapseableSection"><b>
<div class="caption"></div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
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
<p>May 7, 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179887.aspx#Self_hosted" target="_blank">SharePoint 2013 provider-hosted apps</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142384.aspx" target="_blank">App-only permissions</a>
</p>
</li><li>
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC4</a> </p>
</li></ul>
</div>
</div>
</div>
