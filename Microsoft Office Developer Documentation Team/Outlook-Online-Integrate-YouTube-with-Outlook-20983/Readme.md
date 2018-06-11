# Outlook Online: Integrate YouTube with Outlook Online
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* Office 365
* apps for Office
## Topics
* SharePoint
* apps for Office
## IsPublished
* True
## ModifiedDate
* 2014-03-25 01:23:34
## Description

<div id="header">&nbsp;</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p><span class="label">Summary:</span> Learn how to create a basic SharePoint-hosted app for SharePoint that allows you to preview YouTube videos within email when using the Outlook Web App.</p>
</div>
<div class="introduction">
<div>&nbsp;</div>
</div>
<h1 class="heading">Description of the sample</h1>
<div id="sectionSection0" class="section">
<p>This sample shows you how to upload a SharePoint-hosted app for SharePoint that allows Outlook Web App users to preview the video associated with a YouTube video URL.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div id="sectionSection1" class="section">
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
<div id="sectionSection2" class="section">
<div>The sample zip file contains the following:</div>
<ul>
<li>
<div>A Visual Studio 2012 solution file named O365_YoutubeIntegration_cs Visual Studio 2012 that contains the AppManifest.xml file and the two projects, O365_YoutubeIntegration_cs and O365_YoutubeIntegrationWeb_cs:</div>
<ul>
<li>
<div>The O365_YoutubeIntegration_cs project includes the O365_YoutubeIntegration_cs.xml file that defines the app and includes the regular expression used to identify YouTube URLs in the body of an email message.</div>
</li><li>
<div>The O365_YoutubeIntegrationWeb_cs project contains an HTML and JavaScript-based application that provides the interface and the rest of the functionality for the app.</div>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div id="sectionSection3" class="section">
<ol>
<li>
<div>Open Visual Studio 2012 with administrator privileges.</div>
</li><li>
<div>On the <span class="ui">File</span> menu, click <span class="ui">Open</span>, and then click
<span class="ui">Project/Solution</span>.</div>
</li><li>
<div>Navigate to the folder containing the unzipped O365_YoutubeIntegration_cs.sln file, select it, and click
<span class="ui">Open</span> to finish configuring the app.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div id="sectionSection4" class="section">
<div>Press F6 to build the app for SharePoint. The app for SharePoint can now be deployed to the App Catalog of a SharePoint Online tenancy. This can be done in several ways, including the following:</div>
<ul>
<li>
<div>For debugging, you can deploy the app directly from Visual Studio 2012 by using the following procedure:</div>
<ol>
<li>
<div>Press F5 to run and debug the app. This will open the <span class="ui">Connect to Exchange</span> email account dialog box, which will allow you to enter the name and password for the Exchange Web Services (EWS).</div>
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
<div>A folder that contains the O365_YouTubeIntegration.app file opens; note the file location. The file path will be similar to
<span class="placeholder">&lt;Your application root folder name&gt;</span>\ O365_YouTubeIntegration\O365_YouTubeIntegration\O365_YouTubeIntegration \bin\Debug\app.publish\1.0.0.0\O365_YouTubeIntegration.app.</div>
</li><li>
<div>Open your Office 365 Developer Site.</div>
</li><li>
<div>On the Developer Site, in the <span class="ui">Apps in Testing</span> list, click the plus sign next to the text
<span class="ui">new app to deploy</span>.</div>
</li><li>
<div>On the <span class="ui">Deploy App</span> page, click the <span class="ui">
upload</span> link.</div>
</li><li>
<div>Navigate to the location of O365_YouTubeIntegration.app, select it, and then click
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
<div id="sectionSection5" class="section">
<ol>
<li>
<div>If it is not open yet, open your Office 365 Developer Site.</div>
</li><li>
<div>On the Outlook Web App page, click <span class="ui">New Mail</span>.</div>
</li><li>
<div>In the <span class="ui">To</span> field, enter your own email address.</div>
</li><li>
<div>In the body of your email message, add the following example URL: <a href="http://msdn.microsoft.com/library/http://www.youtube.com/embed/OzkZWvAJUr0.aspx" target="_blank">
http://www.youtube.com/embed/OzkZWvAJUr0</a>.</div>
</li><li>
<div>Click <span class="ui">Send</span>, and wait for the email to appear in your inbox.</div>
</li><li>
<div>When the email arrives, click it to display it in the preview area. Wait a moment and a gray bar will appear with the name of your app listed in it.</div>
</li><li>
<div>Click the app name to turn the YouTube URL into a live preview.</div>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div id="sectionSection6" class="section">
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
<div>The app displays a blank square, not a video.</div>
</td>
<td>
<div>Make sure the URL used for the video was acquired using the Embed button in YouTube. More information can be found on the YouTube support site.</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Change log</h1>
<div id="sectionSection7" class="section">
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
<div id="sectionSection8" class="section">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/library/1b992485-6efe-4ea4-a18c-221689b0b66f.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" target="_blank">SharePoint 2013 development overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" target="_blank">Getting started developing apps for SharePoint
</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/3038dd73-41ee-436f-8c78-ef8e6869bf7b.aspx" target="_blank">How to: Create a basic provider-hosted app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></div>
</li></ul>
</div>
</div>
</div>
