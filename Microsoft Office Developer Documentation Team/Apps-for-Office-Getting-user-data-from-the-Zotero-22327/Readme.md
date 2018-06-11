# Apps for Office: Getting user data from the Zotero web service
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* apps for Office
## Topics
* OAuth
## IsPublished
* True
## ModifiedDate
* 2013-05-31 11:47:18
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Getting user info from the Zotero web service</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p><span>Summary:</span> This code sample shows how to authenticate to and retrieve data from the Zotero online research service and then shows how to insert that data into a Microsoft Word 2013 document.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0" name="collapseableSection">
<p><span>Provided by:</span> <a href="http://3sharp.com/Pages/Default.aspx" target="_blank">
John Peltonen</a>, 3Sharp</p>
<p>This sample is a Microsoft Word 2013 Taskpane app that uses the Zotero Web Service. Users can use the app to insert citation references in a Word 2013 document. The user interface provides a way to browse your collection of citations stored in the Zotero
 library, select one or more citations, and then insert them at the insertion point.</p>
<p>The sample also shows how to use the Microsoft ASP.NET extension of the DotNetOpenAuth open source library to handle OAuth from a web page and gives a clear example of how to derive and create your own custom OAuth client classes for specific sites.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Microsoft Word 2013</p>
</li><li>
<p>Zotero user account </p>
</li><li>
<p>Zotero API client key and client secret that you insert into Home.aspx.cs prior to running the app</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2" name="collapseableSection">
<p>Key components of the Zotero app include:</p>
<ul>
<li>
<p><b>ZoteroClient.cs</b> - Shows how to implement a custom Microsoft.DotNetOpenAuth client class to enable authentication with a 3rd party using OAuth 1.1.</p>
</li><li>
<p><b>ZoteroApiCaller.svc</b> - Shows how to handle cross-domain API calls when JSONP is not available by wrapping a request from the user and submitting it to the Zotero web service.</p>
</li><li>
<p><b>Home.aspx.cs</b> - Contains the ASP.NET code that utilizes the ZoteroClient class to authenticate with Zotero, get the access token, and store it in a cookie for the user.NOTE: The preferred way of storing Zotero-specific OAuth Access keys would be to
 persist the token in a database. Zotero access tokens do not expire and are intended to last until the user revokes the key for a specific app.</p>
</li><li>
<p><b>Home.js</b> - Uses JQuery to retrieve citation information from the user's Zotero account. It also handles the dynamic display of that information and inserting citations into the Word document at the cursor.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>This sample requires you to set up a Zotero Account and register the application to obtain a client key and client secret that you insert into Home.aspx.cs before running the app. The following code shows the code for Home.aspx.cs where you enter the client
 key and client secret.</p>
<div><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th>C# </th>
<th></th>
</tr>
<tr>
<td colspan="2">
<pre>public partial class Home : System.Web.UI.Page
    {
        // TODO: Enter your Zotero client key and client secret in the respective 
        // constants below.
        private const string ClientKey = &quot;&quot;; //Insert your key
        private const string ClientSecret = &quot;&quot;; //Insert your secret
        private const string CallbackUrl = &quot;~/App/Home/Home.aspx&quot;;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Press F5 to build and run the sample.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<p>Use the following steps to test the sample.</p>
<ol>
<li>
<p>When the app is loaded, log in to Zotero through the app.</p>
</li><li>
<p>In the Zotero page that appears next, authorize the app to access your Zotero account.</p>
</li><li>
<p>Select a single citation and then insert it into the document.</p>
</li><li>
<p>Go to the end of your document. Select more than one citation and then insert them.</p>
</li><li>
<p>Select a different citation style and then repeat the previous steps.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6" name="collapseableSection">
<p>If Word starts, but the app is unable to load, you can take the following actions:</p>
<ol>
<li>
<p>Locate the App Manifest used for Debugging in the App Project folder /Bin/Debug/AppManifests that appears after the first time the app is started from Visual Studio.</p>
</li><li>
<p>Open the manifest in Notepad or your text editor of choice.</p>
</li><li>
<p>Ensure the DefaultUrl attribute of the SourceLocation element matches the SSL URL of the web project.</p>
</li></ol>
<p>If you get a runtime error in Visual Studio that says the variable ClientKey or ClientSecret can't be an empty string, add the client key and client secret that you got from Zotero.
</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection">
<p>May 17, 2013</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp142185.aspx" target="_blank">JavaScript API for Office</a>
</p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/officeapps/Apps-for-Office-code-d04762b7" target="_blank">Apps for Office code sample pack</a>
</p>
</li><li>
<p><a href="http://www.dotnetopenauth.net/" target="_blank">DotNetOpenAuth web site</a>
</p>
</li><li>
<p><a href="http://www.zotero.org" target="_blank">Zotero</a> </p>
</li></ul>
</div>
</div>
</div>
