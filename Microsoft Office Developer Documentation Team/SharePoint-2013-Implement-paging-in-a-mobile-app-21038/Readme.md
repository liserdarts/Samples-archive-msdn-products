# SharePoint 2013: Implement paging in a mobile app for SharePoint
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Mobile
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-28 09:28:55
## Description

<p id="header">This sample app demonstrates how to enable a user to page through list data from a SharePoint 2013 site using a Windows Phone app.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>The solution is based on the Windows Phone SharePoint List Application template provided by Visual Studio 2010 Express for Windows Phone with the SharePoint 2013 SDK for Windows Phone 7.1 installed. The solution enables the user to page through list data
 from SharePoint 2013 in a Windows Phone app. Figure 1 shows the completed solution.</p>
<p>The sample also demonstrates how to use Collaborative Application Markup Language (CAML) queries to sort and segment the list data, and how to build paged views for the results of those CAML queries.</p>
<p class="caption"><strong>Figure 1. Custom list definition</strong></p>
<br>
<img id="76812" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-43cafaa1/image/file/76812/1/3b-1.png" alt="Figure 1" width="190" height="354"></div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>Visual Studio 2010 Express for Windows Phone.</div>
</li><li>
<div>SharePoint 2013 SDK for Windows Phone 7.1</div>
</li><li>
<div>Excel 2013</div>
</li><li>
<div>Access to either an Office 365 Developer Site (recommended) or SharePoint Server 2013 configured to support forms-based authentication.</div>
</li></ul>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<div>At the time of development, it was not possible to develop this type of solution by using Visual Studio 2012, nor for Windows Phone 8.</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<div>The <strong>Populations.xlsx</strong> workbook that you will use to create the list in SharePoint.</div>
</li><li>
<div>The <strong>App.xaml.cs</strong> file that you will use to edit as per the &quot;Configure the sample&quot; section.</div>
</li><li>
<div>The <strong>ListDataProvider.cs</strong> file that is used to define CAML queries that represent the paged views. You do not need to edit this file, but you should review the CAML queries to help you understand how the solution is implemented. The key
 part of this implementation is the <span><span class="keyword">CamlQueryBuilderClass</span></span> near the end of the file.</div>
</li></ul>
<p>All other files are automatically provided by the Windows Phone SharePoint List Application template, and they have not been modified in the development of this sample app.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Browse to the SharePoint 2013 site where you want to create the list.</div>
</li><li>
<div>In the left pane, click <span class="ui">Site Contents</span>.</div>
</li><li>
<div>Click <span class="ui">add an app</span>.</div>
</li><li>
<div>Browse through apps until you locate the <span class="ui">Import Spreadsheet</span> app; click that item.</div>
</li><li>
<div>In the <span class="ui">Name</span> textbox, type <span class="input">Populations</span>.</div>
</li><li>
<div>Click <span class="ui">Browse</span>, and then locate the Populations.xlsx workbook that is provided with this solution.</div>
</li><li>
<div>Click <span class="ui">Import</span>.</div>
</li><li>
<div>In the <span class="ui">Range Type</span> drop-down list, click <span class="ui">
Table Range</span>.</div>
</li><li>
<div>In the <span class="ui">Select Range</span> drop-down list, click <span class="ui">
Sheet1!Table1</span>.</div>
</li><li>
<div>Click <span class="ui">Import</span>. A new list named Populations is created based on the data in the workbook.</div>
</li><li>
<div>Open the <span class="ui">SP_MobilePaging_cs.sln</span> file in Visual Studio 2010 Express for Windows Phone.</div>
</li><li>
<div>Open the <span class="ui">App.xaml.cs</span> file and locate the line that reads
<span class="code">private const string YourSharePointUrl = &quot;&quot;;</span>.</div>
</li><li>
<div>Insert the URL to the SharePoint site where you created the Population list, and then save your changes.</div>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Press F5 to run and test the sample. The following images show examples of the application as rendered in the Windows Phone Emulator. Figure 2 shows the sign-in page.</p>
<p class="caption"><strong>Figure 2. The sign-in screen on launch</strong></p>
<p><img id="76813" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-43cafaa1/image/file/76813/1/3b-2.png" alt="Figure 2" width="190" height="354"></p>
<p>Figure 3 shows that all list items are shown by default.</p>
<p class="caption"><strong>Figure 3. All list items in the default view</strong></p>
<p><img id="76814" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-implement-43cafaa1/image/file/76814/1/3b-3.png" alt="Figure 3" width="190" height="354"></p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 (RTM) configured to support forms-based authentication, or that you have signed up for an Office 365 Developer Site. Also, ensure that you are using Visual Studio 2010 Express for Windows Phone and SharePoint 2013
 SDK for Windows Phone 7.1.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163228.aspx" target="_blank">Build mobile apps for SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163786.aspx" target="_blank">Overview of Windows Phone SharePoint 2013 application templates in Visual Studio</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163209.aspx" target="_blank">Architecture of the Windows Phone SharePoint List Application template</a></div>
</li><li>
<div><a href="http://www.microsoft.com/en-us/download/details.aspx?id=27570" target="_blank">Windows Phone SDK 7.1</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163943.aspx" target="_blank">How to: Set up an environment for developing mobile apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/ms457529.aspx" target="_blank">Authentication, authorization, and security in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
