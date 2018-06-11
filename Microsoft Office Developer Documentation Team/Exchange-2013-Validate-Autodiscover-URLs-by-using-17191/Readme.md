# Exchange 2013: Validate Autodiscover URLs by using validation callbacks
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Exchange Server 2007
* Exchange Server 2010
* Exchange Online
* EWS Managed API
* Exchange Server 2013
## Topics
* Authentication
## IsPublished
* True
## ModifiedDate
* 2013-06-06 01:01:30
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Exchange 2013: Validate Autodiscover URLs by using validation callbacks</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>This sample shows you how to create a validation method for the Exchange Web Services (EWS) Managed API.</p>
</div>
<h1>Description of the Exchange 2013: Validate Autodiscover URLs by using validation callbacks sample</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample authenticates an email address and password entered from the console, and then creates a method that validates the URLs that your client is redirected to during the process of locating the EWS URL. The validation callback method is used when
 your client is connecting to an Exchange Online server.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>An account on a server that is running Exchange Online as part of Office 365.</p>
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
<p>Ex15_UrlValidationCallback_CS.sln — The Visual Studio 2010 solution file for the Ex15_UrlValidationCallback_CS project.</p>
</li><li>
<p>Ex15_UrlValidationCallback_CS.csproj — The Visual Studio 2010 project file for the
<b>UrlValidationCallback</b> function.</p>
</li><li>
<p>app.config — Contains configuration data for the Ex15_UrlValidationCallback_CS project.</p>
</li><li>
<p>Ex15_UrlValidationCallback_CS.cs — Contains the using directives, namespace, class, and functions to demonstrate the URL validation callback method.</p>
</li><li>
<p>UrlValidator.cs - Implements a method that validates any URL that the client application is redirected to while using the Autodiscover service to find the correct EWS endpoint for an email address.</p>
</li><li>
<p>Authentication.csproj — The Visual Studio 2010 project file for the dependent authentication code.</p>
</li><li>
<p>TextFileTraceListener.cs — Contains the using directives, namespace, class, and code to write the XML request and response to a text file.</p>
</li><li>
<p>Service.cs — Contains the using directives, namespace, class, and functions necessary to acquire an
<b>ExchangeService</b> object used in the Ex15_UrlValidationCallback_CS project.</p>
</li><li>
<p>CertificateCallback.cs — Contains the using directives, namespace, class, and code to acquire an X509 certificate.</p>
</li><li>
<p>UserData.cs — Contains the using directives, namespace, class, and functions necessary to acquire user information required by the service object.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>Follow these steps to configure the Exchange 2013: Validate Autodiscover URLs by using validation callbacks sample.</p>
<ol>
<li>
<p>Set the startup project to Ex15_UrlValidationCallback_CS by selecting the project in the Solution Explorer and choosing &quot;Set as StartUp Project&quot; from the
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
<p><a href="http://go.microsoft.com/fwlink/?LinkId=301827" target="_blank">Get started with the EWS Managed API</a>
</p>
</li></ul>
<p></p>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection">
<p>First release.</p>
</div>
</div>
</div>
