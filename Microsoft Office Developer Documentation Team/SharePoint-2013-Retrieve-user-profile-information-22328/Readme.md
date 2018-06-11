# SharePoint 2013: Retrieve user profile information in an app part
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* apps for SharePoint
## Topics
* REST
* Javascript
* Search
## IsPublished
* True
## ModifiedDate
* 2013-08-29 04:07:19
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Retrieve user profile information in an app part</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p><span>Summary:</span> Learn how to use the Search REST API and people picker and take advantage of app part dynamic resizing by using the app part edit mode. The sample displays an app part that retrieves information about a SharePoint user.</p>
</div>
<div>
<p><span>Provided by:</span> Yina Arenas, Microsoft Corporation</p>
</div>
<div id="sectionSection0">
<p>When you deploy and start the app, you can add the <strong><span class="ui">Who Is?</span></strong> app part to a page on the host web. Figure 1 shows how the app part will appear on the page. It displays the people picker and uses the search REST API
 to get information about a SharePoint user.</p>
<strong>
<div class="caption">Figure 1. The Who Is? app part displays detailed information about a SharePoint user</div>
</strong><br>
<img src="/site/view/file/83068/1/image.png" alt="">
<p>You can use the Web Part menu to make the app part display less information. Choose the
<strong><span class="ui">Who is? properties</span></strong> link and clear the <strong>
<span class="ui">Include detail information</span></strong> check box.</p>
<strong>
<div class="caption">Figure 2. Use the Web Part menu to display less information in the app part</div>
</strong><br>
<img src="/site/view/file/83069/1/image.png" alt="">
<p>Now the app part will only include user basic information, and it gets dynamically resized.</p>
<strong>
<div class="caption">Figure 3. Edited app part displays only basic information about a SharePoint user</div>
</strong><br>
<img src="/site/view/file/83070/1/image.png" alt=""></div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A SharePoint 2013 Developer Site. This site can be in Office 365 or in an on-premises installation of SharePoint 2013 that is enabled for apps. See
<a href="http://msdn.microsoft.com/en-us/library/sharepoint/fp179923.aspx" target="_blank">
How to: Set up an on-premises development environment for apps for SharePoint</a> to create an on-premises SharePoint Developer Site.</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012 installed on your development computer.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2">
<p>The app's <strong><span class="ui">AppPart</span></strong> project includes the following:</p>
<ul>
<li>
<p>A <strong><span class="ui">Pages</span></strong> folder that contains two .aspx pages:</p>
<ul>
<li>
<p><strong>Default.aspx</strong> is the default page of the SharePoint-hosted app. This is the page that you see when you start the app from SharePoint instead of including it as an app part.</p>
</li><li>
<p><strong>Part.aspx</strong> is the page that appears inside the app part.</p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the <strong>AppPart.sln</strong> file in Visual Studio 2012.</p>
</li><li>
<p>In the <strong><span class="ui">Properties</span></strong> pane, change the <strong>
<span class="ui">Site URL</span></strong> property. This is the absolute URL of your SharePoint site.</p>
</li></ol>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Press F5 to build and deploy the app.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<ol>
<li>
<p>Choose <strong><span class="ui">Trust It</span></strong> on the consent page to grant permissions to the app.</p>
</li><li>
<p>Return to the host web of your SharePoint site. Navigate to a page that can include Web Parts.</p>
</li><li>
<p>In the ribbon above the page, choose <strong><span class="ui">Page</span></strong>,
<strong><span class="ui">Edit</span></strong>, <strong><span class="ui">Insert</span></strong>,
<strong><span class="ui">App Part</span></strong>.</p>
</li><li>
<p>Select the <strong><span class="ui">Who Is?</span></strong> app part and insert it on the page.</p>
</li><li>
<p>In the ribbon, choose the <strong><span class="ui">Save</span></strong> button.</p>
</li><li>
<p>Enter a valid user name in the text box so that the People Picker can resolve it.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and ways to solve them.</p>
<strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Problem</p>
</th>
<th>
<p>Solution</p>
</th>
</tr>
<tr>
<td>
<p>Visual Studio doesn't open the browser after you press F5.</p>
</td>
<td>
<p>Set the app for SharePoint project as the startup project.</p>
</td>
</tr>
<tr>
<td>
<p>HTTP error 405 <strong>Method not allowed</strong>.</p>
</td>
<td>
<p>Locate the <span><strong><span class="keyword">applicationhost.config</span></strong></span> file in
<strong>%userprofile%\Documents\IISExpress\config</strong>.</p>
<p>Locate the handler entry for <strong>StaticFile</strong>, and add the verbs <span>
<strong><span class="keyword">GET</span></strong></span>, <span><strong><span class="keyword">HEAD</span></strong></span>,
<span><strong><span class="keyword">POST</span></strong></span>, <span><strong><span class="keyword">DEBUG</span></strong></span>, and
<span><strong><span class="keyword">TRACE</span></strong></span>.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Change log</h1>
<div id="sectionSection7"><strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<td>
<p>First release</p>
</td>
<td>
<p>June 2013</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://channel9.msdn.com/Series/Reimagine-SharePoint-Development/Migrating-a-SharePoint-Web-Part-to-an-app-for-SharePoint-app-part" target="_blank">Migrating a SharePoint Web Part to an app for SharePoint app part</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/SharePoint-2013-Perform-a-1bf3e87d" target="_blank">SharePoint 2013: Using the search REST service from an app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/sharepoint/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
