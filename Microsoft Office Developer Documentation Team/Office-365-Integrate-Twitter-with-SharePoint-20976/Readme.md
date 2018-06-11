# Office 365: Integrate Twitter with SharePoint Online
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
* Sharepoint Online
## Topics
* jQuery
* SharePoint Lists
## IsPublished
* True
## ModifiedDate
* 2013-02-27 10:02:32
## Description

<p id="header"><span class="label">Summary:</span> Learn how to create a basic SharePoint-hosted app for SharePoint that allows you to enter a search string and query Twitter for recent posts that match the search string. The app is hosted on an Office
 365 Developer Site.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div>&nbsp;</div>
</div>
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>This sample shows you how to upload a SharePoint-hosted app for SharePoint that displays a page allowing a user to search recent Twitter posts for a search string.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>An Office 365 Developer Site.</div>
</li><li>
<div>Visual Studio 2012 installed on your computer.</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012 installed on your computer.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<div>The sample zip file contains the following:</div>
<ul>
<li>
<div>An O365_TwitterIntegration Visual Studio 2012 solution file that contains the AppManifest.xml file and one project, O365_TwitterIntegration.</div>
</li><li>
<div>The O365_TwitterIntegration project includes the following:</div>
<ul>
<li>
<div>The Default.aspx file that contains the HTML and ASP.NET controls for the user interface of the app for SharePoint.</div>
</li><li>
<div>The App.js file that contains JavaScript code for the app. This code takes the string entered by the user and uses the Twitter public web service at https://search.twitter.com/search.json to search for matching tweets. This web service can return more
 information than is used in this sample, including tweet text, user ID, user name, and much more. You can extend this sample to display this information. The AJAX command uses the JSONP protocol instead of JSON to enable cross-domain calls:</div>
</li></ul>
</li></ul>
</div>
<div class="section" id="sectionSection2">
<ul>
<li>
<ul>
<li>
<div><a href="https://search.twitter.com/search.json?q=@SearchKey" target="_blank">https://search.twitter.com/search.json?q=@SearchKey</a>.</div>
<div>You can find more information about the Twitter web service and the information it can return at:
<a href="https://dev.twitter.com/docs/api/1/get/search" target="_blank">https://dev.twitter.com/docs/api/1/get/search</a>.</div>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<ol>
<li>
<div>Open Visual Studio 2012 with administrator privileges.</div>
</li><li>
<div>On the <span class="ui">File</span> menu, click <span class="ui">Open</span>, and then click
<span class="ui">Project/Solution</span>.</div>
</li><li>
<div>Navigate to the folder containing the unzipped O365_TwitterIntegration.sln file, select it, and click
<span class="ui">Open</span>.</div>
</li><li>
<div>Click to select the O365_TwitterIntegration project in Solution Explorer, and look for the
<span class="ui">SiteURL</span> property in the <span class="ui">Properties</span> window. Set the
<span class="ui">Site URL</span> property to the URL of your Office 365 Developer Site.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<div>Press F6 to build the app for SharePoint. The app for SharePoint can now be deployed to the App Catalog of a SharePoint Online tenancy. This can be done in several ways, including the following:</div>
<ul>
<li>
<div>For debugging, you can deploy the app directly from Visual Studio 2012 to a SharePoint Online website by using the following procedure:</div>
<ol>
<li>
<div>Right-click the O365_TwitterIntegration project in Solution Explorer and select
<span class="ui">Deploy</span>.</div>
</li><li>
<div>The first time you deploy, you will see the <span class="ui">Connect to SharePoint</span> dialog box. Enter your
<span class="ui">UserID</span> and password, and click <span class="ui">Sign In</span>.</div>
</li><li>
<div>The Office 365 Developer Site will open and display a page that asks you if you want to trust the app. Select
<span class="ui">Yes</span>.</div>
</li></ol>
</li><li>
<div>Tenant administrators can install the app for SharePoint on their Office 365 Developer Site tenancy by using the following procedure:</div>
<ol>
<li>
<div>Right-click the app for SharePoint project and select <span class="ui">Publish</span>.</div>
</li><li>
<div>On the <span class="ui">Publish apps for Office and SharePoint</span> dialog box, select the
<span class="ui">Open output folder after successful packaging</span> option, and then click
<span class="ui">Finish</span>.</div>
</li><li>
<div>A folder that contains the O365_TwitterIntegration.app file opens; note the file location. The file path will be similar to
<span class="placeholder">&lt;Your application root folder name&gt;</span>\O365_TwitterIntegration\O365_TwitterIntegration\bin\Debug\app.publish\1.0.0.0\O365_TwitterIntegration.app.</div>
</li><li>
<div>Open your Office 365 Developer Site.</div>
</li><li>
<div>On the Developer Site, in the <span class="ui">Apps in Testing</span> list, click the plus sign next to the text
<span class="ui">new app to deploy</span>.</div>
</li><li>
<div>On the <span class="ui">Deploy App</span> page, click the <span class="ui">
upload</span> link.</div>
</li><li>
<div>Navigate to the location of O365_TwitterIntegration.app, select it, and then click
<span class="ui">OK</span>.</div>
</li><li>
<div>Click <span class="ui">Deploy</span>, and on the next dialog box click <span class="ui">
Trust it</span>.</div>
</li><li>
<div>The app will be uploaded to your Office 365 Developer Site.</div>
</li></ol>
</li></ul>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<ol>
<li>
<div>On your Developer Site, in the <span class="ui">Apps in Testing</span> section of the page, click the O365_TwitterIntegration app to run it.</div>
</li><li>
<div>You will be redirected to the TwitterFeeds page, which contains a <span class="ui">
Search Keyword</span> text field and a <span class="ui">Search Feeds</span> button. Enter your search keywords and click the
<span class="ui">Search Feeds</span> button. A list of matching Twitter feeds will appear in the
<span class="ui">Twitter Feeds</span> section.</div>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how you can solve them.</p>
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<div>Problem</div>
</th>
<th>
<div>Solution</div>
</th>
</tr>
<tr>
<td>
<div>The app fails to upload to the Developer Site.</div>
</td>
<td>
<div>Make sure you entered the Site URL correctly in the configuration steps.</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<div>Version</div>
</th>
<th>
<div>Date</div>
</th>
</tr>
<tr>
<td>
<div>First version</div>
</td>
<td>
<div>February 28, 2013</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Getting started developing apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></div>
</li></ul>
</div>
</div>
</div>
