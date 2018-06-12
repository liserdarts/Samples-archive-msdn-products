# SharePoint 2013: Import and Export search settings for SharePoint Online
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* SharePoint Server 2013
* apps for SharePoint
* CSOM
## Topics
* Search
* Configuration
* Export/Import
## IsPublished
* True
## ModifiedDate
* 2014-08-27 12:44:09
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow2">
<td align="left"><span style="font-size:small"><strong><span id="nsrTitle">SharePoint 2013: Import and Export search settings for SharePoint Online using the search CSOM</span></strong></span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p><strong><span class="label">Summary:</span>&nbsp;&nbsp;</strong>Learn how to use the SharePoint search CSOM to import and export search settings for a SharePoint online site.</p>
</div>
<div class="introduction">
<p><strong>In this article</strong><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Core.SearchSettingsPortability/Core.SearchSettingsPortability.htm#O15Readme_Description">Description of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Core.SearchSettingsPortability/Core.SearchSettingsPortability.htm#O15Readme_Prereq">Prerequisites</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Core.SearchSettingsPortability/Core.SearchSettingsPortability.htm#O15Readme_components">Key components of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Core.SearchSettingsPortability/Core.SearchSettingsPortability.htm#O15Readme_build">Run and test the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Core.SearchSettingsPortability/Core.SearchSettingsPortability.htm#O15Readme_Changelog">Change log</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Core.SearchSettingsPortability/Core.SearchSettingsPortability.htm#O15Readme_RelatedContent">Related content</a></p>
<p>&nbsp;</p>
</div>
<a name="O15Readme_Description">
<h2 class="heading">Description of the sample</h2>
<div class="section" id="sectionSection0">
<p>You can use the SharePoint search CSOM to develop applications that export and import customized search configuration settings between site collections and sites. The settings that you export and import include all customized query rules, result sources,
 result types, ranking models, and site search settings.</p>
<p>This sample console application shows how to use the search CSOM to import and export search configuration settings for a SharePoint Online site.</p>
</div>
</a><a name="O15Readme_Prereq">
<h2 class="heading">Prerequisites</h2>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012 or Visual Studio 2013</p>
</li><li>
<p>Microsoft Office Developer tools for Visual Studio 2012 or Visual Studio 2013</p>
</li><li>
<p>SharePoint Online site or site collection</p>
</li></ul>
</div>
</a><a name="O15Readme_components">
<h2 class="heading">Key components of the sample</h2>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p><strong>Core.SearchSettingsPortability</strong>&nbsp;project, the console application project.</p>
</li><li>
<p><strong>Program.cs</strong>&nbsp;contains the logic that imports or exports the search configuration settings based on the user's input.</p>
</li></ul>
</div>
</a><a name="O15Readme_build">
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection3">
<p>Press F5 to run the sample. A console window opens, and you will be prompted for the following:</p>
<ul>
<li>
<p>The operation type: import or export.</p>
</li><li>
<p>Source/destination path for the import/export file.</p>
</li><li>
<p>SharePoint Online site URL.</p>
</li><li>
<p>Username</p>
</li><li>
<p>Password</p>
</li></ul>
</div>
</a><a name="O15Readme_Changelog">
<h2 class="heading">Change log</h2>
<div class="section" id="sectionSection4">
<p>First release.</p>
</div>
</a><a name="O15Readme_RelatedContent">
<h2 class="heading">Related content</h2>
</a>
<div class="section" id="sectionSection5"><a name="O15Readme_RelatedContent"></a>
<ul>
<a name="O15Readme_RelatedContent"></a>
<li><a name="O15Readme_RelatedContent"></a>
<p><a name="O15Readme_RelatedContent"></a><a href="http://msdn.microsoft.com/en-us/library/office/dn205276(v=office.15).aspx" target="_blank">Exporting and importing search configuration settings in SharePoint 2013</a></p>
</li><li>
<p><a href="http://technet.microsoft.com/en-us/library/jj871675.aspx" target="_blank">Export and import customized search configuration settings in Sharepoint Server 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/microsoft.office.server.search.portability(v=office.15).aspx" target="_blank">Microsoft.Office.Server.Search.Portability namespace</a></p>
</li></ul>
</div>
</div>
</div>
