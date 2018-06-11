# SharePoint 2013: Implement NTLM authentication in Windows Phone
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Microsoft SharePoint technologies
## Topics
* SharePoint Foundation 2013
* apps for SharePoint
* mobile devices
## IsPublished
* True
## ModifiedDate
* 2013-04-23 03:19:31
## Description

<div id="header"><span class="label">Summary:</span> This Windows Phone 8 mobile app demonstrates NTLM support for SharePoint 2013.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p>The Microsoft SharePoint SDK for Windows Phone 8 introduces support for NTLM.</p>
</div>
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>The solution is based on the Windows Phone Empty SharePoint Application template provided by Visual Studio Express 2012. It demonstrates how to create a custom logon page on Windows Phone 8 to authenticate against SharePoint 2013 by using NTLM. A custom
 logon page enables a user to enter the URL of a SharePoint site and user credentials through the Windows Phone 8 app.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio Express 2012</p>
</li><li>
<p>Microsoft SharePoint SDK for Windows Phone 8</p>
</li></ul>
<p>Windows Phone 8 Emulator requires the following:</p>
<ul>
<li>
<p>Windows 8 Pro</p>
</li><li>
<p>A processor that supports Second Level Address Translation (SLAT)</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>If your computer meets the hardware and operating-system requirements but does not meet the requirements for the Windows Phone 8 Emulator, the Windows Phone SDK 8.0 will install and run. However, the Windows Phone 8 Emulator will not function and you will
 not be able to deploy or test apps on the Windows Phone 8 Emulator.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample contains the following:</p>
<ul>
<li>
<p>CustomLoginPage.xaml is the custom login page; CustomLoginPage.xaml.cs is the code-behind file.</p>
</li><li>
<p>Constant.cs contains constants used throughout the sample.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>To configure the sample, use Visual Studio Express 2012 to open NTLMSampleCode.sln.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<p>To run and test the sample, choose the F5 key. Figure 1 shows the initial screen.</p>
<div class="caption">Figure 1. Initial screen of the Windows Phone app</div>
<br>
<img id="81080" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-bad237cb/image/file/81080/1/enteryoursharepointsite_fig1.png" alt="Initial screen of Windows Phone app" width="327" height="595">
<p>After entering a URL, the user can provide credentials, as shown in Figure 2.</p>
<div class="caption">Figure 2. Logon page of the app</div>
<br>
<img id="81081" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-bad237cb/image/file/81081/1/loginpage_fig2.png" alt="Logon screen of app" width="326" height="594"></div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection5">
<div class="caption"></div>
<div class="tableSection">
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
<p>April 9, 2013</p>
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
<p><a href="http://www.microsoft.com/en-us/download/details.aspx?id=35471" target="_blank">Windows Phone SDK 8.0</a></p>
</li><li>
<p><a href="http://www.microsoft.com/en-us/download/details.aspx?id=36818" target="_blank">Microsoft SharePoint SDK for Windows Phone 8</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163228.aspx" target="_blank">Build mobile apps for SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163943.aspx" target="_blank">How to: Set up an environment for developing mobile apps for SharePoint</a></p>
</li></ul>
</div>
</div>
</div>
