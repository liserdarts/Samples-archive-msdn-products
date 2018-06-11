# Exchange 2013: Use streaming notifications to synchronize a mailbox
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Exchange Online
* Exchange 2013
* EWS Managed API
## Topics
* Synchronization
* Notification
## IsPublished
* True
## ModifiedDate
* 2013-10-28 11:09:38
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Exchange 2013: Use streaming notifications to synchronize a mailbox readme</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This sample shows you how to use the Exchange Web Services (EWS) Managed API to create a streaming notification subscription to the Drafts folder, including its child folders, and use the notifications to synchronize the contents of those folders.</p>
</div>
<h1>Description of the Exchange 2013: Use streaming notifications to synchronize a mailbox sample</h1>
<div id="sectionSection0">
<p>This sample authenticates an email address and password entered from the console, subscribes to all notifications in the specified folder and all child folders, synchronizes the contents of the folder and all child folders, and then waits for changes and
 notifications to those folders. After a notification is received, the folder is synchronized to retrieve the change type of the item event or folder event. Based on the change type of the event, the properties for the new or updated item or event are retrieved
 by the client. The root folder is set to the Drafts folder by default, but you can change this to any other folder by updating the
<strong><span class="keyword">RootSyncFolder</span></strong> property.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A target server that is running a version of Exchange starting with Exchange Server 2007 Service Pack 1 (SP1), including Exchange Online.</p>
</li><li>
<p>The .NET Framework 4.</p>
</li><li>
<p>The EWS Managed API assembly file, Microsoft.Exchange.WebServices.dll. You can download the assembly from the
<a href="http://go.microsoft.com/fwlink/?LinkID=255472" target="_blank">Microsoft Download Center</a>.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
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
<p>Visual Studio 2012 with the Visual Web Developer and C# components and an open Visual Studio 2012 solution.</p>
<p>Or</p>
</li><li>
<p>A text editor to create and edit source code files and a command prompt window to run a .NET Framework command line compiler.</p>
</li><li>
<p>The <a href="http://code.msdn.microsoft.com/exchange/Exchange-2013-Authenticate-d603a261" target="_blank">
Exchange 2013: Authenticate with EWS</a> sample for authentication.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>This sample contains the following files:</p>
<ul>
<li>
<p>Ex15_NotifyAndSync_CS.sln &mdash; The Visual Studio 2012 solution file for the Ex15_NotifyAndSync_CS project.</p>
</li><li>
<p>Ex15_NotifyAndSync_CS.csproj &mdash; The Visual Studio 2012 project file for the Ex15_NotifyAndSync_CS project.</p>
</li><li>
<p>app.config &mdash; Contains configuration data for the Ex15_NotifyAndSync_CS project.</p>
</li><li>
<p>Ex15_NotifyAndSync_CS.cs &mdash; Contains the using statements, namespace, class, and functions to synchronize folders and subscribe to notifications.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>Follow these steps to configure the Exchange 2013: Use streaming notifications to synchronize a mailbox sample.</p>
<ol>
<li>
<p>Ensure that the reference path for the Microsoft.Exchange.WebServices.dll points to where the DLL is installed on your local computer.</p>
</li><li>
<p>Set the startup project to Ex15_NotifyAndSync_CS by selecting the project in the Solution Explorer and choosing &quot;Set as StartUp Project&quot; from the
<strong><span class="ui">Project</span></strong> menu.</p>
</li></ol>
<p>Follow these steps to set the references to the Exchange 2013: Authenticate with EWS sample.</p>
<p>If you already have the Exchange 2013: Authenticate with EWS sample installed on your computer:</p>
<ol>
<li>
<p>In Solution Explorer, right-click the Ex15_NotifyAndSync_CS solution, point to
<strong><span class="ui">Add</span></strong>, and then select <strong><span class="ui">Existing Project&hellip;</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Add Existing Project</span></strong> dialog box, locate and select the previously downloaded Ex15_Authentication_CS.csproj file to add.</p>
</li><li>
<p>Choose the <strong><span class="ui">Open</span></strong> button to add the Ex15_Authentication_CS project to the Ex15_NotifyAndSync_CS solution.</p>
</li><li>
<p>In Solution Explorer, right-click the Ex15_NotifyAndSync_CS project, and then select
<strong><span class="ui">Add Reference</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Reference Manager</span></strong> dialog box, select
<strong><span class="ui">Solution</span></strong>, and then locate and select the Ex15_Authentication_CS project.</p>
</li><li>
<p>Choose the <strong><span class="ui">OK</span></strong> button to add the reference to the Ex15_Authetication_CS project to the Ex15_NotifyAndSync_CS project.</p>
</li></ol>
<p>If you do not have the Exchange 2013: Authenticate with EWS sample built on your computer:</p>
<ol>
<li>
<p>Download the <a href="http://code.msdn.microsoft.com/exchange/Exchange-2013-Authenticate-d603a261" target="_blank">
Exchange 2013: Authenticate with EWS</a> project and extract the files.</p>
</li><li>
<p>In Solution Explorer, right-click the Ex15_NotifyAndSync_CS solution, point to
<strong><span class="ui">Add</span></strong>, and then select <strong><span class="ui">Existing Project&hellip;</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Add Existing Project</span></strong> dialog box, locate and select the previously downloaded Ex15_Authentication_CS.csproj file to add.</p>
</li><li>
<p>Choose the <strong><span class="ui">Open</span></strong> button to add the Ex15_Authentication_CS project to the Ex15_NotifyAndSync_CS solution.</p>
</li><li>
<p>In Solution Explorer, right-click the Ex15_NotifyAndSync_CS project, and then select
<strong><span class="ui">Add Reference</span></strong>.</p>
</li><li>
<p>In the <strong><span class="ui">Reference Manager</span></strong> dialog box, select
<strong><span class="ui">Solution</span></strong>, and then locate and select the Ex15_Authentication_CS project.</p>
</li><li>
<p>Choose the <strong><span class="ui">OK</span></strong> button to add the reference to the Ex15_Authetication_CS project to the Ex15_NotifyAndSync_CS project.</p>
</li></ol>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Press or select F6 to build and deploy the sample.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>Press or select F5 to run the sample.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection6">
<p><a href="http://msdn.microsoft.com/library/76136f28-0dad-4ecc-9dd7-a45a1861e4b0(Office.15).aspx" target="_blank">Notification subscriptions, mailbox events, and EWS in Exchange</a></p>
<p><a href="http://msdn.microsoft.com/library/decf1eee-9743-44f3-9333-b3a01af3683e(Office.15).aspx" target="_blank">Mailbox synchronization and EWS in Exchange</a></p>
<p><a href="http://msdn.microsoft.com/en-us/library/jj220499(EXCHG.80).aspx" target="_blank">Get started with the EWS Managed API</a></p>
<p><a href="http://channel9.msdn.com/Events/Open-Specifications-Plugfests/Windows-Identity-and-Exchange-Protocols-Plugfest-2012/Exchange-Web-Services-Best-Practices-Part-2" target="_blank">Exchange Web Services Best Practices Part 2</a></p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release.</p>
</div>
</div>
</div>
