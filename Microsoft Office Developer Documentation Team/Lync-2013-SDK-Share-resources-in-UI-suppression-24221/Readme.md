# Lync 2013 SDK: Share resources in UI suppression
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Lync Server 2013
* Lync 2013
* Lync 2013 SDK CU2
## Topics
* UI suppression
* remote sharing
## IsPublished
* True
## ModifiedDate
* 2013-08-19 03:10:15
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left">&nbsp;</td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Lync 2013 SDK: ShareResources in UI suppression</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This code sample shows you how to view a remotely shared application or share a local application in a Microsoft Lync 2013 API-enabled application in UI suppression mode.</p>
<p><strong>Important!</strong> The sample will not work unless you have downloaded and installed the
<a href="http://www.microsoft.com/en-us/download/details.aspx?id=36824" target="_blank">
cumulative update for the Lync 2013 SDK: July 2013</a>.</p>
</div>
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>This sample shows you how to build a Windows Forms application that signs in to Microsoft Lync 2013, starts a conversation with a user in your contact list, shows an application sharing stage when another user shares an application, and lets you share one
 of your running applications.</p>
<p>The Lync 2013 API now supports application sharing in UI suppression mode and this sample is optimized for this scenario. When sharing a local application, you can grant or deny another user's request to control the application. If you have granted a control
 request, you can revoke it. To give a user a visual hint that an application is being shared, the sample creates a yellow border around the shared application window.</p>
<p>If you accept an invitation to view a shared window, the sample window expands to show a sharing view of the remote application. You can configure the sample to adjust its dimensions to match the dimensions of the shared application. This feature lets you
 keep the entire shared application workspace in view at all times. If you don't want to let the sample application dimensions change when the shared application dimensions change, you can configure the view to show the shared application in a different resolution
 within a fixed view area. Figure 1 shows the sample hosting a conversation where the user is sharing an Excel workbook.</p>
<strong>
<div class="caption">Figure 1. Screen image of the sample and a shared Excel workbook.</div>
</strong><br>
&nbsp;
<p><img src="/site/view/file/94406/1/image.png" alt=""></p>
<h3>Prerequisites for running and compiling the sample in Visual Studio</h3>
<div>
<p>This sample requires the following:</p>
<ul>
<li>
<p>.NET Framework 4.0 or newer versions of .NET Framework.</p>
</li><li>
<p>Visual Studio 2010 or newer versions of Visual Studio.</p>
</li><li>
<p>Microsoft Lync 2013 SDK, <a href="http://www.microsoft.com/en-us/download/details.aspx?id=36824" target="_blank">
cumulative update for Lync 2013 SDK: July 2013</a> or newer.</p>
</li></ul>
</div>
<h3>Prerequisites for running installed sample on client computer</h3>
<div>
<p>This sample requires the following:</p>
<ul>
<li>
<p>A running instance of Microsoft Lync 2013; <a href="http://support.microsoft.com/kb/2768004" target="_blank">
May, 2013 update</a>.</p>
</li><li>
<p>The Lync runtime that is installed in the <span>\Program files\Microsoft Office\Office15\LyncSDK\Redist</span> folder.</p>
</li></ul>
</div>
<h3>Run and test the sample</h3>
<div>
<p>This sample was designed to be run locally on the computer running Lync 2013.</p>
<ol>
<li>
<p>Open ShareResources.csproj.</p>
</li><li>
<p>In Visual Studio, press F5.</p>
</li></ol>
</div>
</div>
<h1>Additional resources</h1>
<div id="sectionSection1">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/0a6f5019-cffe-4e0c-aeae-22878f48e868.aspx" target="_blank">Lync 2013 SDK general reference</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/9626409f-cf49-4f1e-9212-045d990f1923.aspx" target="_blank">How to: Get a shareable resource and share it in a conversation</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d07626a9-aa8a-4331-8a60-17ec09d77020.aspx" target="_blank">How to: Show a bright border around a locally shared resource</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/Displaying-a-highlighted-30039fd2" target="_blank">Displaying a highlighted border around a process window or desktop</a></p>
</li></ul>
<p>&nbsp;</p>
</div>
</div>
</div>
<p>&nbsp;</p>
