# Mail apps for Outlook: Validate a client identity token using the .NET Framework
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* apps for Office
* Exchange Server 2013
## Topics
* Authentication
## IsPublished
* True
## ModifiedDate
* 2013-07-08 04:31:28
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Mail apps for Outlook: Validate a client identity token using the .NET Framework</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p>This sample shows you how to create a library to validate Exchange client identity tokens by using the .NET Framework.</p>
</div>
<h1>Description of the Mail apps for Outlook: Validate a client identity token using the .NET Framework sample</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample shows you how to create a .NET Framework service that validates an Exchange client access token. The Exchange server issues a token that is unique to the mailbox on the server; you can use this token to associate a mailbox with services that
 you provide to a mail app for Outlook.</p>
<p>The sample is divided into two parts. The first part, the mail app for Outlook, runs in your email client. It requests an identity token from the Exchange server and sends this token to the second part, a web service that validates the token from the client.
 The web service responds with the contents of the token, which the mail app then displays.</p>
<p>The service uses the following steps to process the token:</p>
<ul>
<li>
<p>Decodes the identity token to get the URL for the Exchange server's authentication metadata document. The service also checks to determine whether the token has expired and checks the version number on the token during this step.</p>
</li><li>
<p>If the identity token passes the first step, the service uses the information in the authentication metadata document to get the certificate that was used to sign the token from the server.</p>
</li><li>
<p>If the token is valid, the service returns it to the mail app for Outlook for display.</p>
</li></ul>
<p>The service does not use the token in any way. It responds with the information contained in the token, or with an error message if the token is not valid.
</p>
<p>This sample also requires an X.509 certificate validation function that allows the service to respond to requests that are signed with a self-signed certificate issued by the Exchange server. The Exchange server will use this self-signed certificate by default;
 if your Exchange server has a valid certificate that traces back to a root provider, this validation function is not required. For more information about the validation function, see
<a href="http://msdn.microsoft.com/en-us/library/bb408523(EXCHG.80).aspx" target="_blank">
Validating X509 Certificates for SSL over HTTP</a>.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires that you have the following:</p>
<ul>
<li>
<p>Visual Studio 2012, with the apps for Office project templates.</p>
</li><li>
<p>A computer running Exchange 2013 with at least one email account, or an Office 365 developer account.</p>
</li><li>
<p>Microsoft.IdentityModel.dll - This library is included in the Windows Identity Foundation SDK. You can download the SDK from the
<a href="http://www.microsoft.com/en-us/download/details.aspx?id=4451" target="_blank">
Microsoft Download Center</a>.</p>
</li><li>
<p>Microsoft.IdentityModel.Extensions.dll - This library is available for download from one of the following locations:
</p>
<ul>
<li>
<p><a href="http://download.microsoft.com/download/0/1/D/01D06854-CA0C-46F1-ADBA-EBF86010DCC6/MicrosoftIdentityExtensions-32.msi" target="_blank">Windows IdentityModel.Extensions.dll for 32-bit applications</a>
</p>
</li><li>
<p><a href="http://download.microsoft.com/download/0/1/D/01D06854-CA0C-46F1-ADBA-EBF86010DCC6/MicrosoftIdentityExtensions-64.msi" target="_blank">Windows IdentityModel.Extensions.dll for 64-bit applications</a>
</p>
</li></ul>
</li><li>
<p>Familiarity with JavaScript programming and web services.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The sample solution contains the following file in the IdentityToken project:</p>
<ul>
<li>
<p>IdentityTokenManifest.xml - The manifest file for the mail app for Outlook.</p>
</li></ul>
<p>The IdentityTokenService project defines a REST service by using the WCF API. The project was created by using the WebAPI wizard in Visual Studio 2012. The project contains the following files:</p>
<ul>
<li>
<p>Controllers\IdentityTestController.cs - The service object that provides the business logic for the sample service.</p>
</li><li>
<p>Models\AuthClaimTypes.cs - The static object that provides identifiers for the parts of the client identity token.</p>
</li><li>
<p>Models\AuthMetadata.cs - The object that represents the authentication metadata document retrieved from the location specified in the client identity token.
</p>
</li><li>
<p>Models\Base64UrlEncoder.cs - The static object that decodes a URL that has been base-64 URL-encoded, as specified in RFC 4648.
</p>
</li><li>
<p>Models\Config.cs - Provides string values that must be matched in the client identity token. Also provides a certificate validation callback suitable for test use.</p>
</li><li>
<p>Models\DecodedJSONToken.cs - Represents a valid JSON Web Token (JWT) decoded from the base-64 URL-encoded client identity token. If the token is not valid, the constructor for the
<span value="DecodedJSONToken"><b><span class="keyword">DecodedJSONToken</span></b></span> object will throw an
<span value="ApplicationException"><b><span class="keyword">ApplicationException</span></b></span> error.</p>
</li><li>
<p>Models\IdentityTokenRequest.cs - The object that represents the REST request from the mail app.</p>
</li><li>
<p>Models\IdentityTokenResponse.cs - The object that represents the REST response from the web service.</p>
</li><li>
<p>Models\IdentityToken.cs - The object that represents the decoded and validated client identity token.</p>
</li><li>
<p>Models\JsonAuthMetadataDocument.cs - The object that represents the authentication metadata document sent from the Exchange server.</p>
</li><li>
<p>Models\JsonTokenDecoder.cs - The static object that decodes the base-64 URL-encoded client identity token from the mail app for Outlook.</p>
</li><li>
<p>Global.asax - The global file for the web service. The web API and route table are configured for the REST service in the
<span value="Application_Start"><b><span class="keyword">Application_Start</span></b></span> event handler.</p>
</li><li>
<p>Web.config - Binds the sample service to the web server endpoint.</p>
</li></ul>
<p>The IdentityTokenWeb project contains the UI and JavaScript code for the mail app for Outlook. The project was created by using Visual Studio 2012 and is based on the mail app for Outlook project template. The following files were modified for this sample:</p>
<ul>
<li>
<p>App\Home\Home.html - The HTML user interface for the mail app for Outlook.</p>
</li><li>
<p>App\Home\Home.js - The JavaScript file that handles requesting and using the identity token.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>The mail app will be activated on any email message in the user's Inbox. You can make it easier to test the app by sending one or more email messages to your test account before you run the sample app.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Press F5 to build the sample application. Complete the following tasks to deploy the application:</p>
<ol>
<li>
<p>Connect to an Exchange account by providing the email address and password for an Exchange 2013 server.</p>
</li><li>
<p>Allow the server to configure the email account.</p>
</li></ol>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<p>You run and test the sample in the web browser that is started by Visual Studio when you build and deploy the sample.</p>
<p>If you are running the sample on an Exchange server that is using the default self-signed certificate, you will receive a certificate error when the web browser starts. After you verify that the web browser is opening the correct URL by looking at the web
 address, you can select <b><span class="ui">Continue to this Web site</span></b> to start Outlook Web App.</p>
<p>Follow these steps to run the sample:</p>
<ol>
<li>
<p>Log on to the email account by entering the account name and password.</p>
</li><li>
<p>Select a message in the Inbox.</p>
</li><li>
<p>Wait for the App bar to appear over the message.</p>
</li><li>
<p>In the App bar, select <b><span class="ui">Identity Token Test</span></b>.</p>
</li><li>
<p>When the identity token mail app appears, it will display the contents of the client identity token.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6" name="collapseableSection">
<p>The following are common errors that can occur when you use Outlook Web App to test a mail app for Outlook:</p>
<ul>
<li>
<p>The App bar does not appear when a message is selected. If this occurs, restart the application by selecting
<b><span class="ui">Debug - Stop Debugging</span></b> in the Visual Studio window, then press F5 to rebuild and deploy the app.</p>
</li><li>
<p>Changes to the JavaScript code might not be picked up when you deploy and run the app. If the changes are not picked up, clear the cache on the web browser by selecting
<b><span class="ui">Tools - Internet options</span></b> and selecting the <b><span class="ui">Delete…</span></b> button. Delete the temporary Internet files and then restart the app.</p>
</li></ul>
</div>
<h1>Related content</h1>
<div id="sectionSection7" name="collapseableSection">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/bb408523(EXCHG.80).aspx" target="_blank">Validating X509 Certificates for SSL over HTTP</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/c0520a1e-d9ba-495a-a99f-6816d7d2a23e" target="_blank">Authenticating a mail app by using Exchange 2013 identity tokens</a>
</p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179819(v=office.15)" target="_blank">How to: Validate an Exchange 2013 identity token</a>
</p>
</li><li>
<p><a href="http://www.asp.net/web-api" target="_blank">Web API: The Official Microsoft ASP.NET Site</a>
</p>
</li></ul>
</div>
<h1>Change log</h1>
<div id="sectionSection8" name="collapseableSection">
<p>Updated the sample to use the latest API release.</p>
</div>
</div>
</div>
