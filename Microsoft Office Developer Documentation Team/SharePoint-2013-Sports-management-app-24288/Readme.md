# SharePoint 2013:  Sports team management app
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
* 2013-08-08 05:58:13
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Sports team management app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates how to use JavaScript and jQuery in an app for SharePoint that manages soccer leagues, teams, matches, results, match reports, and media attachments such as pictures and videos.</p>
</div>
<div>
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-us/mvp/Martin%20Harwar-4025664" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020.com</a></p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution uses the JavaScript implementation of the client object model to read, create, update, and delete data from lists based on user actions.</p>
<p>The lists included in this solution represent league tables, their teams, and their matches (including results, match reports, and associated media items). The lists are related to each other through lookup fields, and the user interface (UI) ensures that
 all data operations synchronize with their list items so that the relationships are maintained.</p>
<p>The UI is implemented with HTML elements and cascading style sheet (CSS) styles to present a modern look and feel. JavaScript and jQuery are used to control all aspects of the UI, and the solution contains no server-side code.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either of the following:</p>
<ul>
<li>
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended).</p>
</li><li>
<p>SharePoint Server 2013 configured to host apps, and with a Developer Site Collection already created.</p>
</li></ul>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong><span class="keyword">Default.aspx</span></strong> webpage, which is used to present the leagues, teams, and results, and league tables.</p>
</li><li>
<p>The <strong><span class="keyword">App.js</span></strong> file in the <strong>
<span class="keyword">scripts</span></strong> folder, which is used to retrieve and manage leagues, teams, and results data by using the JavaScript implementation of the client object model (JSOM). The
<strong><span class="keyword">App.js</span></strong> file also contains the UI logic that is implemented in
<strong><span class="keyword">Default.aspx</span></strong>.</p>
</li><li>
<p>The <strong><span class="keyword">App.css</span></strong> file in the <strong>
<span class="keyword">contents</span></strong> folder, which contains style definitions used by the elements in
<strong><span class="keyword">Default.aspx</span></strong>.</p>
</li><li>
<p>Three list definitions and instances: one for league tables, one for teams, and one for matches. The lists are linked together by lookup fields.</p>
</li><li>
<p>All other files are automatically provided by the Visual Studio 2012 project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to configure the sample.</p>
<div>
<ol>
<li>
<p>Open the <strong><span class="keyword">SP_SoccerLeagueManager_js.sln</span></strong> file in Visual Studio 2012.</p>
</li><li>
<p>In the <strong><span class="keyword">Properties</span></strong> window, add the full URL to your Office 365 Enterprise site or SharePoint Server 2013 Developer Site Collection to the
<strong><span class="keyword">Site URL</span></strong> property. You may be prompted to provide credentials if you add a URL to an Office 365 site.</p>
</li><li>
<p>No other configuration is necessary.</p>
</li></ol>
</div>
</div>
<h1>Build, run, and test the sample</h1>
<div id="sectionSection3">
<p>&nbsp;</p>
<div>
<ol>
<li>
<p>Press Ctrl&#43;Shift&#43;B to build the solution.</p>
</li><li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 or Office 365 Enterprise site if you are prompted to do so by the browser.</p>
</li><li>
<p>When the app appears, the starting screen resembles Figure 1.</p>
<strong>
<div class="caption">Figure 1. Start screen</div>
</strong><br>
&nbsp;<img src="/site/view/file/94154/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Create New League</span></strong> to enter a new league name as shown in Figure 2.</p>
<strong>
<div class="caption">Figure 2. Adding a new league</div>
</strong><br>
&nbsp;<img src="/site/view/file/94155/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">&#43;Advanced Options</span></strong> to set the league metrics for points to be awarded for different results, as shown in Figure 3. The default metrics are typical of a soccer league.</p>
<strong>
<div class="caption">Figure 3. Editing league metrics</div>
</strong><br>
&nbsp;<img src="/site/view/file/94156/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Add League</span></strong> to save the league as shown in Figure 4. Initially there are no teams or results.</p>
<strong>
<div class="caption">Figure 4. Saved league</div>
</strong><br>
&nbsp;<img src="/site/view/file/94157/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">View League</span></strong> to see the league statistics table, which is initially empty, as shown in Figure 5.</p>
<strong>
<div class="caption">Figure 5. Initial league statistics table</div>
</strong><br>
&nbsp;<img src="/site/view/file/94158/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Add Teams</span></strong> to enter a team name as shown in Figure 6.</p>
<strong>
<div class="caption">Figure 6. Adding a team</div>
</strong><br>
&nbsp;<img src="/site/view/file/94159/1/image.png" alt=""> </li><li>
<p>Figure 7 shows that multiple teams have been newly added to the league.</p>
<strong>
<div class="caption">Figure 7. Newly added teams</div>
</strong><br>
&nbsp;<img src="/site/view/file/94160/1/image.png" alt=""> </li><li>
<p>Now that there are multiple teams, click <strong><span class="ui">View League</span></strong> to see the current league table as shown in Figure 8. No games have been played yet.</p>
<strong>
<div class="caption">Figure 8. League statistics table with teams</div>
</strong><br>
&nbsp;<img src="/site/view/file/94161/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Add Results</span></strong> to choose a Home team, an Away team, and enter the score and date. Optionally, you can also provide a match summary and call out any notable performances, as shown in Figure 9.</p>
<strong>
<div class="caption">Figure 9. Adding a match result</div>
</strong><br>
&nbsp;<img src="/site/view/file/94162/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Submit</span></strong> to save the match details. You can then choose to upload media files (such as photos of the game or video clips) using the
<strong><span class="ui">Add Assets</span></strong> file control in Figure 10.</p>
<strong>
<div class="caption">Figure 10. Adding a media asset</div>
</strong><br>
&nbsp;<img src="/site/view/file/94163/1/image.png" alt=""> </li><li>
<p>Figure 11 shows that a photo and a video were uploaded as part of this match report.</p>
<strong>
<div class="caption">Figure 11. Match results with media assets</div>
</strong><br>
&nbsp;<img src="/site/view/file/94164/1/image.png" alt=""> </li><li>
<p>Figure 12 shows the league table after a few matches have been played and recorded.</p>
<strong>
<div class="caption">Figure 12. League table with scores</div>
</strong><br>
&nbsp;<img src="/site/view/file/94165/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Fixture</span></strong> for a given team to see the match reports of all the games they played. For example, Figure 13 shows the summary for the SharePoint PM team.</p>
<strong>
<div class="caption">Figure 13. Team match reports</div>
</strong><br>
&nbsp;<img src="/site/view/file/94166/1/image.png" alt=""> </li><li>
<p>Click <strong><span class="ui">Assets</span></strong> for a given match to see any media files that were uploaded as part of a match report. Figure 14 shows that each asset is numbered, and linked to its media file. The first asset is a photo of a goal
 being scored.</p>
<strong>
<div class="caption">Figure 14. Viewing media assets</div>
</strong><br>
&nbsp;<img src="/site/view/file/94167/1/image.png" alt=""> </li><li>
<p>Figure 15 shows a video for the same match.</p>
<strong>
<div class="caption">Figure 15. Match video asset</div>
</strong><br>
&nbsp;<img src="/site/view/file/94168/1/image.png" alt=""> </li><li>
<p>Finally, Figure 16 shows that a second league was created from the home page, and you can now choose which league to view.</p>
<strong>
<div class="caption">Figure 16. View of multiple leagues</div>
</strong><br>
&nbsp;<img src="/site/view/file/94169/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Ensure that you have SharePoint Server 2013 properly configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Enterprise site configured to host apps.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5"><strong>
<div class="caption"></div>
</strong>
<div>
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
<p>August 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://www.jQuery.com" target="_blank">jQuery</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
