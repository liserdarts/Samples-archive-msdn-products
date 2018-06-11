# Exchange 2013: Get user information by using an Exchange Management Shell cmdlet
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Exchange Server 2007
* Exchange Server 2010
* Exchange Online
* Exchange Server 2013
## Topics
* Exchange Management Shell
## IsPublished
* True
## ModifiedDate
* 2013-06-06 02:46:23
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Exchange 2013: Get user information by using an Exchange Management Shell cmdlet</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>This sample shows you how to call an Exchange Management Shell cmdlet from managed code.</p>
</div>
<h1>Description of the Exchange 2013: Get user information by using an Exchange Management Shell cmdlet sample</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample gets information about one or more users from an Exchange server by using an Exchange Management Shell cmdlet called from managed code.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A target server that is running a version of Exchange starting with Exchange Server 2007 Service Pack 1 (SP1), including Exchange Online as part of Office 365.</p>
</li><li>
<p>The .NET Framework 4.</p>
</li><li>
<p>The System.Management.Automation.dll assembly file. The assembly can be found in one of the following locations:
</p>
<ul>
<li>
<p>For the Windows XP and Windows Vista operating systems, in the Windows PowerShell installation directory ($PSHOME).</p>
</li><li>
<p>For the Windows 7 and Windows 8 operating systems, in the following folder: Windows\assembly\GAC_MSIL\System.Management.Automation.</p>
</li></ul>
<div>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left"><b>Note</b> </th>
</tr>
<tr>
<td>
<p>The sample assumes that the assembly is in the default download directory. You will need to verify the path before you run the solution.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>Visual Studio 2010 with the Visual Web Developer and C# components and an open Visual Studio 2010 solution.</p>
<p>Or</p>
</li><li>
<p>A text editor to create and edit source code files and a command prompt window to run a .NET Framework command line compiler.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>This sample contains the following files:</p>
<ul>
<li>
<p>Ex15_GetUserInformationWithPowerShell_CS.sln — The Visual Studio 2010 solution file for the Ex15_GetUserInformationWithPowerShell_CS project.</p>
</li><li>
<p>Ex15_GetUserInformationWithPowerShell_CS.csproj — The Visual Studio 2010 project file.</p>
</li><li>
<p>app.config — Contains configuration data for the Ex15_GetUserInformationWithPowerShell_CS project.</p>
</li><li>
<p>Ex15_GetUserInformationWithPowerShell_CS.cs — Contains the using directives, namespace, class, and functions to call an Exchange Management Shell cmdlet to get user information from an Exchange server.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>Follow these steps to configure the Exchange 2013: Get user information by using an Exchange Management Shell cmdlet sample.</p>
<ol>
<li>
<p>Set the startup project to Ex15_GetUserInformationWithPowerShell_CS by selecting the project in the Solution Explorer and choosing &quot;Set as StartUp Project&quot; from the
<b><span class="ui">Project</span></b> menu.</p>
</li><li>
<p>Ensure that the reference path for the Microsoft.Exchange.WebServices.dll points to where the DLL is installed on your local computer.</p>
</li></ol>
<p></p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Press F5 to build and deploy the sample.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<p>Press F5 to run the sample.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection6" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/exchange/ff326158(v=exchg.140).aspx" target="_blank">Working with the Exchange Management Shell</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/exchange/ff326159(v=exchg.150).aspx" target="_blank">How to: Get a list of mail users by using the Exchange Management Shell in Exchange 2013</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/exchange/ff326161(v=exchg.150).aspx" target="_blank">How to: Use the Exchange Management Shell cmdlet response in Exchange 2013</a>
</p>
</li></ul>
<p></p>
<p></p>
<p></p>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection">
<p>First release.</p>
</div>
</div>
</div>
