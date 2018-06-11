# SharePoint 2013: Work with user profile properties in apps for SharePoint
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
* social computing
## IsPublished
* True
## ModifiedDate
* 2014-03-19 04:11:14
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Work with user profile properties in apps for SharePoint</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This sample app demonstrates how to use JavaScript in an app for SharePoint to retrieve and work with user profile properties, including providing links for viewing and editing user profiles.</p>
</div>
<div>
<p><strong>Last modified: </strong>March 04, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#sectionSection0">Description</a> <br>
<a href="#sectionSection1">Prerequisites</a> <br>
<a href="#sectionSection2">Key components of the sample</a> <br>
<a href="#sectionSection3">Configure the sample</a> <br>
<a href="#sectionSection4">Build the sample</a> <br>
<a href="#sectionSection5">Run and test the sample</a> <br>
<a href="#sectionSection6">Troubleshooting</a> <br>
<a href="#sectionSection7">Change log</a> <br>
<a href="#sectionSection8">Related content</a></p>
</div>
<h1>Description</h1>
<div id="sectionSection0">
<p><span>Provided by:</span> <a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">
Martin Harwar</a>, <a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>The solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution retrieves the current user's profile by using the
<strong><span class="keyword">SP.UserProfiles</span></strong> namespace, and displays some of the properties from the profile to the user, along with links for editing and viewing their entire profile.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
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
<p>SharePoint Server 2013 configured to host apps, and with a developer site collection already created; or,</p>
</li><li>
<p>Access to an Office 365 developer site configured to host apps.</p>
</li></ul>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong>Default.aspx</strong> webpage, which is used to present the user profile data and links.</p>
</li><li>
<p>The <strong>App.js</strong> file in the <strong>scripts</strong> folder, which is used to retrieve and manage user profile data by using the JavaScript implementation of the client object model (JSOM).</p>
</li><li>
<p>The <strong>SP.UserProfiles.js</strong> file. This file is provided by SharePoint Server, and has been added to the
<strong>scripts</strong> folder. It has also been added as a script link in the Default.aspx page, because that enables the script in App.js to use
<strong><span class="keyword">UserProfile</span></strong> classes.</p>
</li><li>
<p>The <strong>AppManifest.xml</strong> file, which has been edited to specify that the app requests
<strong>read</strong> permissions for user profiles.</p>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the <strong><span class="ui">SP_UserProfile_js.sln</span></strong> file using Visual Studio 2012.</p>
</li><li>
<p>In the <strong><span class="ui">Properties</span></strong> window, add the full URL to your SharePoint Server 2013 developer site collection or Office 365 developer site to the
<strong><span class="keyword">Site URL</span></strong> property.</p>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 developer site collection or Office 365 developer site if you are prompted to do so by the browser.</p>
</li></ol>
<p>Figures 1 and 2 show screen shots from the resulting app.</p>
<strong>
<div class="caption">Figure 1. App requesting access to user profile objects</div>
</strong><br>
<img src="/site/view/file/110894/1/image.png" alt="">
<p><strong>&nbsp;</strong></p>
<strong>
<div class="caption">Figure 2. Specific properties and links exposed when Get My Profile is clicked</div>
</strong><br>
<img src="/site/view/file/110895/1/image.png" alt="">
<p>&nbsp;</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>Ensure that you have a properly configured SharePoint server that is configured to host apps (with a developer site collection already created), or that you have signed up for an Office 365 developer site configured to host apps.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release: January 30, 2013.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj920104.aspx" target="_blank">How to: Retrieve user profile properties by using the JavaScript object model in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
