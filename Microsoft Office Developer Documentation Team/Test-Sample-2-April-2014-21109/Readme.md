# Test Sample 2 - April-2014
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
## Topics
* Search
## IsPublished
* False
## ModifiedDate
* 2014-04-15 12:31:35
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Exchange 2013: Set streaming notifications for applications programmatically sample readme</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>This sample shows you how to use the Exchange Web Services (EWS) Managed API to create a streaming subscription.</p>
</div>
<h1>Description of the Exchange 2013: Set streaming notifications for applications programmatically sample</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample authenticates an email address and password entered from the console, creates a streaming subscription in the authenticated user's Inbox folder on the Exchange server, and monitors the Inbox for new mail, items created, and items deleted.
</p>
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
<p>The EWS Managed API assembly file, Microsoft.Exchange.WebServices.dll. You can download the assembly from the
<a href="http://go.microsoft.com/fwlink/?LinkID=255472" target="_blank">Microsoft Download Center</a>.</p>
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
<p>A version of Visual Studio starting with Visual Studio 2010.</p>
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
<p>Ex15_SetStreamingNotifications_CS.sln — The Visual Studio solution file for the Ex15_SetStreamingNotifications_CS project.</p>
</li><li>
<p>Ex15_SetStreamingNotifications_CS.csproj — The Visual Studio project file for the
<b>SubscribeToStreamingNotifications</b> function.</p>
</li><li>
<p>app.config — Contains configuration data for the Ex15_SetPullNotifications_CS project.</p>
</li><li>
<p>Ex15_SetStreamingNotifications_CS.cs — Contains the using statements, namespace, class, and functions to create a streaming subscription.</p>
</li><li>
<p>Ex15_Authentication_CS.csproj — The Visual Studio project file for the dependent authentication code.</p>
</li><li>
<p>CredentialHelper.cs — Contains the using directives, namespace, class, and functions to prompt for credentials, verify credentials, and store credentials for an application that uses EWS.</p>
</li><li>
<p>TextFileTraceListener.cs — Contains the using statements, namespace, class, and code to write the XML request and response to a text file.</p>
</li><li>
<p>Service.cs — Contains the using statements, namespace, class, and functions necessary to acquire an
<b>ExchangeService</b> object used in the Ex15_SetStreamingNotifications_CS project.</p>
</li><li>
<p>CertificateCallback.cs — Contains the using statements, namespace, class, and code to acquire an X509 certificate.</p>
</li><li>
<p>UserData.cs — Contains the using statements, namespace, class, and functions necessary to acquire user information required by the service object.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>Follow these steps to configure the Exchange 2013: Set streaming notifications for applications programmatically sample.</p>
<ol>
<li>
<p>Set the startup project to Ex15_SetStreamingNotifications_CS by selecting the project in the Solution Explorer and choosing &quot;Set as StartUp Project&quot; from the
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
<p>When you are prompted to press or select Enter, wait to press Enter until you want the program to end. Send an email to the Inbox that you are monitoring and you will see the notifications pop up in the command window. After the timeout of one minute, you
 can choose whether you want to reestablish the connection.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection6" name="collapseableSection">
<ul>
<li>
<p><a href="http://go.microsoft.com/fwlink/?LinkID=301827" target="_blank">Get started with the EWS Managed API</a>
</p>
</li></ul>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection"><b>
<div class="caption"></div>
</b>
<div>
<table width="50%" cellspacing="2" cellpadding="5" frame="lhs">
<tbody>
<tr>
<th>
<p>Date</p>
</th>
<th>
<p>Description</p>
</th>
</tr>
<tr>
<td>
<p>December 10, 2013</p>
</td>
<td>
<p>Updated the sample to accept user input after the subscription times out.</p>
</td>
</tr>
<tr>
<td>
<p>July 22, 2013</p>
</td>
<td>
<p>First release.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
</div>
</div>
