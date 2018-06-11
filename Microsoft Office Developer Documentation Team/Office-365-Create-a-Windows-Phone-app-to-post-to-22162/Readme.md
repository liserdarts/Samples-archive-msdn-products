# Office 365: Create a Windows Phone app to post to your My Site page
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Office 365
## Topics
* Windows Phone
* My Site
## IsPublished
* True
## ModifiedDate
* 2013-05-20 01:25:10
## Description

<div id="header"><span>Summary:</span> Build a basic Windows Phone 8 app that uses the SharePoint SDK for Windows Phone 8 to read and publish posts to your My Site page of an Office 365 Developer Site from a Windows Phone 8.&nbsp;</div>
<div id="mainSection">
<div id="mainBody">
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>By using this code sample and the SharePoint SDK for Windows Phone 8, you can authenticate yourself as a user of an Office 365 Developer Site and then read and post feeds to your My Site page.</p>
<p>The process of authenticating a SharePoint user on a Windows Phone application is a little different from the same process on a client computer. On a Windows Phone you must first create an object of the Authenticator class, which is used as the user's credentials.
 After the authentication is done, the Client Object Model is used to read and post feeds to an Office 365 My Site page.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An Office 365 Developer Site.</p>
</li><li>
<p>A Windows 8 development workstation.</p>
</li><li>
<p>Either a workstation with Hyper-V support for the phone emulator, or a Windows Phone 8 device that has been registered with a
<a href="http://msdn.microsoft.com/library/windowsphone/develop/ff769508(v=vs.105).aspx" target="_blank">
Windows Phone developer account</a>.</p>
</li><li>
<p>Visual Studio 2012.</p>
</li><li>
<p><a href="http://dev.windowsphone.com/en-us/downloadsdk" target="_blank">Windows Phone SDK 8.0</a>.</p>
</li><li>
<p><a href="http://www.microsoft.com/en-my/download/details.aspx?id=36818" target="_blank">Microsoft SharePoint SDK for Windows Phone 8</a>.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample zip file contains the following:</p>
<ul>
<li>
<p>A Windows Phone 8Visual Studio 2012 solution file that contains the AppManifest.xml file.</p>
</li><li>
<p>One project, O365_ReadPostFeedsWP8.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>On a Windows 8 workstation, open Visual Studio 2012 with administrator privileges.</p>
</li><li>
<p>On the <strong><span class="ui">File</span></strong> menu, click <strong><span class="ui">Open</span></strong>,
<strong><span class="ui">Project/Solution</span></strong> and then navigate to the folder where you unzipped the O365_ReadPostFeedsWP8.sln file. Select the O365_ReadPostFeedsWP8.sln file and click
<strong><span class="ui">Open</span></strong>.</p>
</li><li>
<p>In <strong><span class="ui">Solution Explorer</span></strong>, click to expand
<strong><span class="ui">References</span></strong>. Verify whether references to the following SharePoint SDK for Windows Phone 8 libraries are added to the project:</p>
<ul>
<li>
<p><strong><span class="ui">Microsoft.SharePoint.Client.Phone</span> </strong></p>
</li><li>
<p><strong><span class="ui">Microsoft.SharePoint.Client.Phone.Auth.UI</span> </strong>
</p>
</li><li>
<p><strong><span class="ui">Microsoft.SharePoint.Client.Phone.Runtime</span> </strong>
</p>
</li><li>
<p><strong><span class="ui">Microsoft.SharePoint.Client.UserProfiles.Phone</span>
</strong></p>
</li></ul>
</li><li>
<p>Open the <strong><span class="ui">Office365.cs</span></strong> file and change the value of
<span>Office365SiteUrl</span> to your Office 365 Developer Site URL.</p>
</li><li>
<p>Configure the debugger to use the Windows Phone emulator (Emulator 720p is recommended) or to use a Windows Phone 8 device that has been registered with a Windows Phone developer account. The developer account is not needed to use the phone emulator.</p>
</li></ol>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Right-click the solution in <strong><span class="ui">Solution Explorer</span></strong>, and click
<strong><span class="ui">Build Solution</span></strong>.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>Note: You must have Wi-Fi and Internet access enabled on either the Windows Phone emulator or the Windows Phone device that you will use to run the sample.</p>
<p>When the new application is displayed on the Windows Phone 8 or Windows Phone emulator:</p>
<ol>
<li>
<p>Run the solution in debug mode to explore the code sample.</p>
</li><li>
<p>On the <strong><span class="ui">Feeds O365</span></strong> page, enter the <strong>
<span class="ui">Username</span></strong> associated with the feeds you want to retrieve and click
<strong><span class="ui">Get Feeds</span></strong>.</p>
</li><li>
<p>You will be redirected to the Office 365 login page. Enter your Office 365 credentials and sign in to the Office 365 portal.</p>
</li><li>
<p>After you are signed in successfully, you will again be redirected to your application page. You will see a list of all the feeds on the
<strong><span class="ui">Feeds O365</span></strong> page.</p>
</li><li>
<p>Click <strong><span class="ui">Go to Post Feed</span></strong> to navigate to the
<strong><span class="ui">Post Feed</span></strong> page.</p>
</li><li>
<p>In the <strong><span class="ui">Post Text</span></strong> box, enter the text you want to post to your Office 365 My Site.</p>
</li><li>
<p>If the post succeeds, you will see a message saying <strong><span class="ui">Post published to Office 365 My Site successfully</span></strong>.</p>
</li><li>
<p>Navigate back to the <strong><span class="ui">Feeds O365</span></strong> page where you will now see the new post in the list.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how you can solve them.</p>
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
<p>The app fails to upload to your Office 365 Developer Site.</p>
</td>
<td>
<p>In <strong><span class="ui">Configure the sample</span></strong> step 4, make sure you change the value of
<span>Office365SiteUrl</span> to your Office 365 Developer Site URL before executing the program.</p>
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
<p>February, 28 2013</p>
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
<p><a href="http://www.microsoft.com/en-my/download/details.aspx?id=36818" target="_blank">Windows Phone SDK 8.0</a></p>
</li><li>
<p><a href="http://www.microsoft.com/en-my/download/details.aspx?id=36818" target="_blank">Microsoft SharePoint SDK for Windows Phone 8</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/windowsphone/develop/ff402563(v=vs.105).aspx" target="_blank">Windows Phone Emulator</a></p>
</li></ul>
</div>
</div>
</div>
