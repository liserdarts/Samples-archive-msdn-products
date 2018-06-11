# SharePoint 2013: Employee directory app
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
* Search
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-04-23 05:01:29
## Description

<div id="header">Learn how to create a search-based app that queries people content in SharePoint 2013.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p><span class="label">Provided by:</span><a href="http://mvp.microsoft.com/en-us/mvp/Scot%20Hillier-33471" target="_blank">Scot Hillier</a>,
<a href="http://www.criticalpathtraining.com/Pages/default.aspx" target="_blank">
Critical Path</a></p>
<p>This sample uses the RESTful endpoint to perform a search against the people content source in SharePoint 2013. The search queries for people by last name beginning with the letter selected. The results are displayed in alphabetical order along with contact
 information.</p>
<p>The app presents a tabbed A-though-Z interface. When a lettered tab is clicked, a search is performed by last name, and the results are displayed along with contact information.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>The User Profile Service Application properly set up and configured</p>
</li><li>
<p>The Search Service Application properly set up and configured</p>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection1">
<p>The sample is a SharePoint-hosted app written entirely in JavaScript. The app is structured using the Model-View-ViewModel (MVVM) architecture with the knockout-2.1.0.js providing support for binding elements to the webpage. The jQuery library is used extensively
 to perform REST calls.</p>
<p>The key code for the sample can be found in the library ContactViewModel.js located in the Scripts/ViewModels folder. The view model contains a load function that performs the search query.</p>
<p>The App.js library contains the start-up code for the app.</p>
<p>The ToolbarViewModel.js library contains the code to respond to a user clicking one of the lettered tabs.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection2">
<p>The app requires that the managed property <span><span class="keyword">LastName</span></span> be designated as
<span><span class="keyword">Sortable</span></span>. Follow these steps to configure the managed property in the Search Service Application.</p>
<div class="subSection">
<ol>
<li>
<p>Open SharePoint Central Administration.</p>
</li><li>
<p>Choose <span class="ui">Manage Service Applications</span>.</p>
</li><li>
<p>Choose <span class="ui">Search Service Application</span>.</p>
</li><li>
<p>Choose <span class="ui">Search Schema</span>.</p>
</li><li>
<p>Type <span class="input">LastName</span> in the <span class="ui">Managed Property</span> search box, and then choose
<span class="ui">Search</span>.</p>
</li><li>
<p>Choose <span class="ui">LastName</span> in the <span class="ui">Managed Property</span>.</p>
</li><li>
<p>Set the <span><span class="keyword">Sortable</span></span> property to <span class="ui">
Yes - Active</span>, and then choose <span class="ui">OK</span>.</p>
</li><li>
<p>Perform a <span class="ui">Full Crawl</span>.</p>
</li></ol>
</div>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection3">
<p>&nbsp;</p>
<div class="subSection">
<ol>
<li>
<p>Open <span class="ui">EmployeeDirectory.sln</span> in Visual Studio 2012.</p>
</li><li>
<p>Edit the <span><span class="keyword">Site URL</span></span> property to refer to a test site where you will deploy the solution.</p>
</li><li>
<p>Press F5.</p>
</li><li>
<p>When prompted, grant permissions for the app to use the search service.</p>
</li><li>
<p>When the app appears, click a letter, and verify that results populate.</p>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection4">
<p>If the app throws an error, the most likely cause is that the <span><span class="keyword">LastName</span></span> managed property is not correctly configured to allow sorting.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection5">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<td>
<p>First release</p>
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
<div class="section" id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/ff798339.aspx" target="_blank">Using the REST Interface</a></p>
</li><li>
<p><a href="http://rest4sharepoint.codeplex.com/" target="_blank">REST API for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163300.aspx" target="_blank">Search in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ee534981.aspx" target="_blank">SharePoint 2013 Search server class library</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/Apps-for-SharePoint-sample-64c80184" target="_blank">Apps for SharePoint sample pack</a></p>
</li></ul>
</div>
</div>
</div>
