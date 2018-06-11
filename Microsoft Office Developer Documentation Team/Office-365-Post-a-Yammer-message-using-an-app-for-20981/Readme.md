# Office 365: Post a Yammer message using an app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* Office 365
## Topics
* SharePoint
* social computing
## IsPublished
* True
## ModifiedDate
* 2013-02-27 10:08:59
## Description

<p id="header"><span class="label">Summary:</span> This sample demonstrates how to post a message on Yammer.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<div>&nbsp;</div>
</div>
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>This sample creates a command on the SharePoint ribbon that posts a message on Yammer containing the URL for this sample. In particular, this sample shows how to create a command on the SharePoint ribbon, register an app on Yammer, and post a message on
 Yammer that includes a URL that is dynamically generated when the app is built.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>The solution in this sample requires the following:</div>
<ul>
<li>
<div>Visual Studio 2012.</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012 (use the <a href="http://www.microsoft.com/web/downloads/platform.aspx/" target="_blank">
Microsoft Web Platform Installer</a>).</div>
</li><li>
<div>An Office 365 Developer Site. You can sign up <a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx/" target="_blank">
here</a>.</div>
</li><li>
<div>A Yammer account.</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<div>The sample contains the following:</div>
<ul>
<li>
<div>The O365_YammerFeedIntegration_cs project.</div>
</li><li>
<div>App.js, which includes the code to get the current user's information.</div>
</li><li>
<div>Yammercore.js, which contains the code that posts a message to Yammer.</div>
</li><li>
<div>YammerRibbonCustomAction/Elements.xml, which contains the code to create the custom ribbon button.</div>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div><strong>Register the app on a Yammer account</strong></div>
<ol>
<li>
<div>Log on to your Yammer account.</div>
</li><li>
<div>In the upper right corner of Yammer, open the menu and click <span class="ui">
Created Apps</span>, as shown in the following figure. If that menu item is not found, go to https://www.yammer.com/client_applications to display the
<span class="ui">Registered applications</span> page.</div>
<div><img id="76666" src="http://i1.code.msdn.s-msft.com/office-365-post-a-yammer-4384c98b/image/file/76666/1/yammerapp-.jpg" alt="Yammer - Created Apps menu item" width="329" height="213"></div>
<div>&nbsp;</div>
</li><li>
<div>Click <span class="ui">Register new App</span> as shown in the following figure.</div>
<div><img id="76667" src="http://i1.code.msdn.s-msft.com/office-365-post-a-yammer-4384c98b/image/file/76667/1/yammerapp-registerapp.jpg" alt="Register a new Yammer App" width="629" height="93"></div>
<div>&nbsp;</div>
</li><li>
<div>Enter the application name, organization, and support email.</div>
</li><li>
<div>Select the box to agree with the Yammer API Terms of Service, and then click
<span class="ui">Continue</span>.</div>
</li><li>
<div>Select the <span class="ui">Basic config</span> tab and enter the information for website, support email, and redirect URI. The redirect URI must be the URL for your Office 365 Developer Site.</div>
</li><li>
<div>Click <span class="ui">Save changes</span>.</div>
</li><li>
<div>Copy the <span class="ui">Client ID</span> for use in configuring the O365_YammerFeedIntegration_cs solution, as described in the following paragraphs. You can ignore the rest of the options and details in the Yammer registration process.</div>
</li></ol>
<div><strong>Configure the O365_YammerFeedIntegration_cs solution</strong></div>
<ol>
<li>
<div>Extract the files from O365_YammerFeedIntegration_cs.zip into a folder.</div>
</li><li>
<div>Open Visual Studio 2012 with administrator privileges.</div>
</li><li>
<div>On the <span class="ui">File</span> menu, point to <span class="ui">Open</span>, and then click
<span class="ui">Project</span>.</div>
</li><li>
<div>Navigate to the location of your O365_YammerFeedIntegration_cs solution folder and select the O365_YammerFeedIntegration_cs.sln file.</div>
</li><li>
<div>If you are not signed in, the <span class="ui">Connect to SharePoint</span> window prompts you to enter your logon credentials.</div>
</li><li>
<div>Set the <span class="ui">Site URL</span> property of the O365_YammerFeedIntegration_cs solution to the URL of your SharePoint site collection as shown in following figure.</div>
<div><img id="76668" src="http://i1.code.msdn.s-msft.com/office-365-post-a-yammer-4384c98b/image/file/76668/1/yammerapp-siteurl.jpg" alt="Location of Site Url property" width="543" height="573"></div>
<div>&nbsp;</div>
</li><li>
<div>In Scripts/yammercore.js, in the <span class="ui">postToActivityFeed</span> function, enter your Yammer user name and email address, as shown in the following figure.</div>
<div><img id="76669" src="http://i1.code.msdn.s-msft.com/office-365-post-a-yammer-4384c98b/image/file/76669/1/yammerapp-nameandemail.jpg" alt="yammercore.js Name and Email" width="923" height="191"></div>
<div>&nbsp;</div>
</li><li>
<div>On the Pages/Default.aspx page, in the <span class="ui">data-app-id</span> field, enter the
<span class="ui">Client ID</span> that was generated when you registered the app on Yammer.com.</div>
</li><li>
<div>Press Ctrl&#43;Shift&#43;S to save all the changes.</div>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<div>Follow these steps to build the sample.</div>
<ol>
<li>
<div>Right-click the O365_YammerFeedIntegration_cs solution folder and click <span class="ui">
Build</span>.</div>
</li><li>
<div>Right-click the O365_YammerFeedIntegration_cs solution folder and click <span class="ui">
Deploy</span>. This deploys your solution to the Office 365 specified in the <span class="ui">
Site URL</span> property.</div>
</li><li>
<div>On the next dialog box, click <span class="ui">Trust it</span>.</div>
</li><li>
<div>A message appears, asking you to approve connecting the app to your account. Click
<span class="ui">Allow</span>.</div>
</li><li>
<div>You will receive an email message saying you have successfully logged on to the app.</div>
</li></ol>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<ol>
<li>
<div>On your Office 365 site, click <span class="ui">Documents</span> to open the Documents page.</div>
</li><li>
<div>On the <span class="ui">Library</span> tab, in the <span class="ui">Share &amp; Track</span> group, choose
<span class="ui">Post on Yammer</span>. This command was created by the app to post a message to Yammer that includes the URL of your app.</div>
<div><img id="76670" src="http://i1.code.msdn.s-msft.com/office-365-post-a-yammer-4384c98b/image/file/76670/1/yammerapp-postonyammer.jpg" alt="Post on Yammer ribbon button" width="619" height="267"></div>
<div>&nbsp;</div>
</li><li>
<div>Open your Yammer account. You should see a message with the name from yammercore.js and the text, &quot;Hey ! Have you seen this link: &lt;<em>the link for your app</em>&gt;&quot;.</div>
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
<div>In Visual Studio 2012, when prompted to &quot;Connect to SharePoint&quot;, an error message says, &quot;Access Denied&hellip; You are not a member of this site.&quot;</div>
</td>
<td>
<div>The credentials are not correct for the URL specified in the <span class="ui">
Site URL</span> property of the app. Correct the logon credentials. If the problem persists, try deleting the browsing history and logging on again.</div>
</td>
</tr>
<tr>
<td>
<div>An error occurs when you click <span class="ui">Allow</span> to allow connecting the app to your Yammer account.</div>
</td>
<td>
<div>In Pages/Default.aspx, check that the value for <span class="ui">data-app-id</span> is the same as the
<span class="ui">Client ID</span> that was generated when you registered the app on Yammer.com.</div>
</td>
</tr>
<tr>
<td>
<div>After clicking <span class="ui">Post on Yammer</span>, a blank page appears with the text on the tab saying &quot;Loading&hellip;&quot; and the loading process never completes.</div>
</td>
<td>
<div>Your network does not allow posting a message to Yammer. Connect to a different network and retry the
<span class="ui">Post on Yammer</span> button.</div>
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
<div><a href="http://developer.yammer.com/documentation/" target="_blank">Yammer Developers Documentation</a></div>
</li><li>
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
<p>&nbsp;</p>
