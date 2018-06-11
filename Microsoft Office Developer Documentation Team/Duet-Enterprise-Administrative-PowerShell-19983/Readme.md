# Duet Enterprise Administrative PowerShell Scripts
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Duet Enterprise
* SharePoint Server 2013
* Windows PowerShell 3.0
## Topics
* SharePoint Administration
* Upgrade
## IsPublished
* True
## ModifiedDate
* 2012-12-18 09:17:35
## Description

<h1>Description of the scripts</h1>
<div id="mainSection">
<div id="mainBody">
<div class="section" id="sectionSection0">
<p>This ZIP file (<a id="72781" href="/Duet-Enterprise-Administrat-b45e0c63/file/72781/1/DuetAdminScripts.zip">DuetAdminScripts.zip</a>) includes a set of six Windows PowerShell scripts that you can use when you are upgrading Duet Enterprise. Three of the
 scripts can be used when upgrading from Duet Enterprise 1.0 on SharePoint Server 2010 to Duet Enterprise 1.0 on SharePoint Server 2013. You can use the other three scripts when upgrading from Duet Enterprise 1.0 on SharePoint Server 2013 to Duet Enterprise
 2.0 on SharePoint Server 2013.</p>
<p>You must run these scripts in a particular order in relation to each other and also in relation to manual steps that you complete during the upgrade process. We recommend that you run the scripts only when they are referred to in the
<a href="http://technet.microsoft.com/en-us/library/jj853051.aspx" target="_blank">
Duet Enterprise upgrade guide</a>.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>These scripts are an optional way to complete procedures that are provided as manual steps in the Duet Enterprise upgrade guide. Although these scripts have been tested by the Duet Enterprise test team, they are not supported.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>Table 1 describes the scripts that you can use when upgrading from Duet Enterprise 1.0 on SharePoint Server 2010 to Duet Enterprise 1.0 on SharePoint Server 2013. For instructions on when and how to use these scripts, see
<a href="http://technet.microsoft.com/en-us/library/jj853049.aspx" target="_blank">
Upgrade to Duet Enterprise 1.0 on SharePoint Server 2013</a>.</p>
<div class="caption">Table 1. Scripts to use when upgrading from Duet Enterprise 1.0 on SharePoint Server 2010 to Duet Enterprise 1.0 on SharePoint Server 2013</div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Script name</p>
</th>
<th>
<p>Purpose</p>
</th>
</tr>
<tr>
<td>
<p>regkeyadd.ps1</p>
</td>
<td>
<p>Creates a registry key to specify that Duet Enterprise 1.0 should be installed to the 14 hive.</p>
</td>
</tr>
<tr>
<td>
<p>stsadmcopy.ps1</p>
</td>
<td>
<p>Copies the stsadm.exe file from the 15 hive to the 14 hive.</p>
</td>
</tr>
<tr>
<td>
<p>duetconfigmodify.ps1</p>
</td>
<td>
<p>Adds the startup node to the DuetConfig.exe.config file so that Duet Enterprise knows which version of the .NET Framework to use.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>Table 2 describes the scripts that you can use when upgrading from Duet Enterprise 1.0 on SharePoint Server 2013 to Duet Enterprise 2.0 on SharePoint Server 2013. For instructions on when and how to use these scripts, see
<a href="http://technet.microsoft.com/en-us/library/gg185653.aspx" target="_blank">
Upgrade to Duet Enterprise 2.0 on SharePoint Server 2013</a>.</p>
<div class="caption">Table 2. Scripts to use when upgrading from Duet Enterprise 1.0 to Duet Enterprise 2.0 on SharePoint Server 2013</div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Script name</p>
</th>
<th>
<p>Purpose</p>
</th>
</tr>
<tr>
<td>
<p>uninstalllangpacks.ps1</p>
</td>
<td>
<p>Uninstalls Duet Enterprise 1.0 language packs.</p>
</td>
</tr>
<tr>
<td>
<p>healthrule.ps1</p>
</td>
<td>
<p>Deletes the health rule named &quot;Duet Enterprise Solutions Health&quot;.</p>
</td>
</tr>
<tr>
<td>
<p>WFAggregatorWP.ps1</p>
</td>
<td>
<p>Removes the Workflow Task Aggregation Web Part from all pages in a given site collection.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_Changelog"></a>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection1">
<p>First release, December 2012.</p>
</div>
<a name="O15Readme_RelatedContent"></a>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection2">
<ul>
<li>
<p><a href="http://technet.microsoft.com/en-us/library/jj853049.aspx" target="_blank">Upgrade to Duet Enterprise 1.0 on SharePoint Server 2013</a></p>
</li><li>
<p><a href="http://technet.microsoft.com/en-us/library/gg185653.aspx" target="_blank">Upgrade to Duet Enterprise 2.0 on SharePoint Server 2013</a></p>
</li></ul>
</div>
</div>
</div>
