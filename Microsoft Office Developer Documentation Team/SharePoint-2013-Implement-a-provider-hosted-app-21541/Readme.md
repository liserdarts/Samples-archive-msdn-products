# SharePoint 2013: Implement a provider-hosted app with a mobile companion
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Mobile
## IsPublished
* True
## ModifiedDate
* 2014-06-26 02:39:34
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Implement a provider-hosted app with a mobile companion</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p>This sample app demonstrates how to build a Windows Phone app that is a companion to a provider-hosted app for SharePoint.</p>
</div>
<div class="introduction">
<p><strong>Last modified: </strong>July 01, 2013</p>
<p><strong>In this article</strong><br>
<a href="#O15Readme_Description">Description</a><br>
<a href="#O15Readme_Prereq">Prerequisites</a><br>
<a href="#O15Readme_components">Key components of the sample</a><br>
<a href="#O15Readme_config">Configure the sample</a><br>
<a href="#O15Readme_build">Build the sample</a><br>
<a href="#O15Readme_test">Run and test the sample</a><br>
<a href="#O15Readme_Troubleshoot">Troubleshooting</a><br>
<a href="#O15Readme_Changelog">Change log</a><br>
<a href="#O15Readme_RelatedContent">Related content</a></p>
</div>
<a name="O15Readme_Description"></a>
<h2 class="heading">Description</h2>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span>&nbsp;&nbsp;<a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>There are two parts to this sample, which demonstrates how to use Collaborative Application Markup Language (CAML) queries to sort and segment the list data, and how to build paged views for the results of those CAML queries:</p>
<ul>
<li>
<p>The <span class="ui">SP_AutohostedExpenses_cs</span> solution is a provider-hosted app for SharePoint that deploys an Expenses list and enables the user to view, add, edit, or delete data in that list.</p>
</li><li>
<p>The <span class="ui">SP_AutohostedExpensesMobile_cs</span> solution is based on the Windows Phone SharePoint List Application template provided by Visual Studio 2010 Express for Windows Phone with the SharePoint 2013 SDK for Windows Phone 7.1 installed.
 This app enables the user to view, add, edit, or delete data in the same list.</p>
</li></ul>
</div>
<a name="O15Readme_Prereq"></a>
<h2 class="heading">Prerequisites</h2>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2013</p>
</li><li>
<p>Visual Studio 2010 Express for Windows Phone.</p>
</li><li>
<p>Microsoft SharePoint 2013 SDK for Windows Phone 7.1</p>
</li><li>
<p>Excel 2013</p>
</li><li>
<p>Access to either an Office 365 Developer Site (recommended) or SharePoint Server 2013 that is configured to support forms-based authentication.</p>
</li></ul>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>At the time of development, it was not possible to develop the mobile part of the solution by using Visual Studio 2012, nor for Windows Phone 8.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_components"></a>
<h2 class="heading">Key components of the sample</h2>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong>SP_AutohostedExpenses_cs</strong> solution is a provider-hosted app for SharePoint that deploys an Expenses list and enables the user to view, add, edit, or delete data in that list by using the browser.</p>
</li><li>
<p>The <strong>SP_AutohostedExpensesMobile_cs</strong> solution is based on the Windows Phone SharePoint List Application template provided by Visual Studio 2010 Express for Windows Phone with the SharePoint 2013 SDK for Windows Phone 7.1 installed. This app
 enables the user to view, add, edit, or delete data in the same list.</p>
</li></ul>
</div>
<a name="O15Readme_config"></a>
<h2 class="heading">Configure the sample</h2>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the <span class="ui">SP_AutohostedExpenses_cs.sln</span> file with Visual Studio 2012.</p>
</li><li>
<p>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint 2013 Server Developer Site collection or Office 365 Developer Site to the
<span class="keyword">Site URL</span> property, and save your changes.</p>
</li><li>
<p>Open the <span class="ui">SP_MobilePaging_cs.sln</span> file in Visual Studio 2010 Express for Windows Phone.</p>
</li><li>
<p>Open the <span class="ui">App.xaml.cs</span> file, and locate the line that reads
<span class="code">private const string YourSharePointUrl = &quot;&quot;;</span>.</p>
</li><li>
<p>Insert the URL to the SharePoint site where you created the Population list, and then save your changes.</p>
</li></ol>
<p>No other configuration is required.</p>
</div>
<a name="O15Readme_build"></a>
<h2 class="heading">Build the sample</h2>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<a name="O15Readme_test"></a>
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection5">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>In the <span class="ui">SP_AutohostedExpenses_cs.sln</span> solution in Visual Studio 2012, press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint 2013 Server or Office 365 Developer Site if you are prompted to do so by the browser.</p>
</li><li>
<p>Trust the app when you are prompted to do so. Figure 1 shows a view of the app upon launch.</p>
<div class="caption">Figure 1. Autohosted app in the browser</div>
<br>
<img id="118053" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-db1d0348/image/file/118053/1/4b-1.png" alt="Figure 1" width="571" height="365">
</li><li>
<p>Leave the app running in the browser.</p>
</li><li>
<p>In the <span class="ui">SP_AutohostedExpensesMobile_cs.sln</span> solution in Visual Studio 2010 Express for Windows Phone, press F5 to run the app. Figure 2 shows the sign-in screen upon launching the mobile app.</p>
<div class="caption">Figure 2. Sign-in screen in the Windows Phone Emulator</div>
<br>
<img id="118054" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-db1d0348/image/file/118054/1/4b-2.png" alt="Figure 2" width="190" height="354">
</li></ol>
</div>
<a name="O15Readme_Troubleshoot"></a>
<h2 class="heading">Troubleshooting</h2>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 that is configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
<p>&nbsp;</p>
</div>
<a name="O15Readme_Changelog"></a>
<h2 class="heading">Change log</h2>
<div class="section" id="sectionSection7">
<p>First release: January 2013</p>
</div>
<a name="O15Readme_RelatedContent"></a>
<h2 class="heading">Related content</h2>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj923094.aspx" target="_blank">How to: Create a companion mobile app for an app for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163228.aspx" target="_blank">Build mobile apps for SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163786.aspx" target="_blank">Overview of Windows Phone SharePoint 2013 application templates in Visual Studio</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163209.aspx" target="_blank">Architecture of the Windows Phone SharePoint List Application template</a></p>
</li><li>
<p><a href="http://www.microsoft.com/en-us/download/details.aspx?id=27570" target="_blank">Windows Phone SDK 7.1</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163943.aspx" target="_blank">How to: Set up an environment for developing mobile apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms457529.aspx" target="_blank">Authentication, authorization, and security in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
