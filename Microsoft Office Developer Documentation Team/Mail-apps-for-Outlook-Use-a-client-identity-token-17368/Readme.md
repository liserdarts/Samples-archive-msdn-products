# Mail apps for Outlook: Use a client identity token
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Exchange 2013
* Visual Studio 2012
* Outlook 2013
* apps for Office
## Topics
* Authentication
* Development
## IsPublished
* True
## ModifiedDate
* 2013-07-11 12:01:55
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Mail apps for Outlook: Use a client identity token sample readme</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>This sample shows you how to identify the clients of your mail app for Outlook.</p>
</div>
<h1>Description of the Mail apps for Outlook: Use a client identity token sample</h1>
<div id="sectionSection0">
<p>This sample shows you how to use a client token from the Exchange server to provide authentication for users of your mail app. The Exchange server issues a token that is unique to the mailbox on the server; you can use this token to associate a mailbox with
 services that you provide to a mail app.</p>
<p>The sample is divided in two parts. The first part, the mail app, runs in the email client. It requests an identity token from the Exchange server and sends this token to the second part, a web service that processes the request from the client.</p>
<p>The service uses the following steps to process the token:</p>
<ul>
<li>
<p>It validates the token to make sure that it was sent from an Exchange server, and that the token was intended for this mail app.</p>
</li><li>
<p>It searches a local dictionary to determine whether the unique identifier has been used before. If the unique identifier has not been used, the service requests credentials (service user name and password) from the client. If the unique identifier is present
 in the token cache, the service sends a response.</p>
</li><li>
<p>If the request contains credentials (that is, it is a response to a request for credentials), the service stores the service user name in the token cache with the unique identifier from the token as its key.</p>
</li></ul>
<p>This sample does not validate the service user name and password in any way. A credential request is considered valid if it contains both a user name and password. Credentials do not expire from the cache in this sample; however, all the cached identifiers
 and user names are lost when you stop running the sample application.</p>
<p>This sample requires a valid server certificate on the Exchange server. If the Exchange server is using its default self-signed certificate, you will need to add the certificate to your local trusted certificate store. You can find instructions for
<a href="http://social.technet.microsoft.com/wiki/contents/articles/13898.how-to-export-a-self-signed-server-certificate-and-import-it-on-a-another-server-in-windows-server-2008-r2.aspx" target="_blank">
exporting and installing a self-signed certificate</a> on TechNet.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires that you have the following:</p>
<ul>
<li>
<p>Visual Studio 2012, with the apps for Office project templates.</p>
</li><li>
<p>A computer running Exchange 2013 with at least one email account, or an Office 365 developer account.</p>
</li><li>
<p>Microsoft.Exchange.WebServices.Auth.dll - This library is included in the Exchange Web Services (EWS) Managed API. You can download the EWS Managed API from the
<a href="http://go.microsoft.com/fwlink/?LinkID=255472" target="_blank">Microsoft Download Center</a>.</p>
</li><li>
<p>Microsoft.IdentityModel.dll - This library is included in the Windows Identity Foundation SDK. You can download the SDK from the
<a href="http://www.microsoft.com/en-us/download/details.aspx?id=4451" target="_blank">
Microsoft Download Center</a>.</p>
</li><li>
<p>Microsoft.IdentityModel.Extensions.dll - This library is available for download from one of the following locations:</p>
<ul>
<li>
<p><a href="http://download.microsoft.com/download/0/1/D/01D06854-CA0C-46F1-ADBA-EBF86010DCC6/MicrosoftIdentityExtensions-32.msi" target="_blank">Windows IdentityModel.Extensions.dll for 32-bit applications</a></p>
</li><li>
<p><a href="http://download.microsoft.com/download/0/1/D/01D06854-CA0C-46F1-ADBA-EBF86010DCC6/MicrosoftIdentityExtensions-64.msi" target="_blank">Windows IdentityModel.Extensions.dll for 64-bit applications</a></p>
</li></ul>
</li><li>
<p>Familiarity with JavaScript programming and web services.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample solution contains the following file in the IdentityToken project:</p>
<ul>
<li>
<p>IdentityToken.xml -Manifest file for the mail app.</p>
</li></ul>
<p>The IdentityTokenService project defines a REST service by using the WCF API. The project was created by using the WebAPI wizard in VisualStudio 2012. The project contains the following files:</p>
<ul>
<li>
<p>Controllers\TokenServiceController.cs - The service object that provides the business logic for the sample service.</p>
</li><li>
<p>Models\Config.cs - Provides string values that must be matched in the client identity token.</p>
</li><li>
<p>Models\ServiceRequest.cs - The object that represents a web request. The contents of the object are created from a JSON request object sent from your mail app.</p>
</li><li>
<p>Models\ServiceResponse.cs - The object that represents a response from the web service. The contents of the object are serialized to a JSON object when they are sent back to the mail app.</p>
</li><li>
<p>Global.asax - The global file for the web service. The route table is configured for the REST service in the Application_Start event handler.</p>
</li><li>
<p>Web.config - Binds the sample service to the web server endpoint.</p>
</li></ul>
<p>The IdentityTokenWeb project contains the UI and JavaScript code for the mail app. The project was created by using Visual Studio 2012 and is based on the mail app project template. The following files were modified for this sample:</p>
<ul>
<li>
<p>App\Home\Home.html - The HTML user interface for the mail app.</p>
</li><li>
<p>App\Home\Home.js - The JavaScript file that handles requesting and using the EWS request.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>The mail app will be activated on any email message in the user's Inbox. You can make it easier to test the app by sending one or more email messages to your test account before you run the sample app.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Press F5 to build and deploy the sample application. Complete the following tasks to deploy the application:</p>
<ol>
<li>
<p>Connect to an Exchange account by providing the email address and password for an Exchange 2013 server.</p>
</li><li>
<p>Allow the server to configure the email account.</p>
</li></ol>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>You run and test the sample in the web browser that is started by Visual Studio when you build and deploy the sample.</p>
<p>Follow these steps to run the sample:</p>
<ol>
<li>
<p>Log on to the email account by entering the account name and password.</p>
</li><li>
<p>Select a message in the Inbox.</p>
</li><li>
<p>Wait for the app bar to appear over the message.</p>
</li><li>
<p>In the app bar, click <strong><span class="ui">IdentityToken</span></strong>.</p>
</li><li>
<p>When the EWS Request mail app appears, click the <strong><span class="ui">Use Identity Token</span></strong> button to send a request to the Exchange server.</p>
</li><li>
<p>The server will ask you to log on. You can type anything in the service user name and password boxes. This sample does not validate the contents of the text boxes.</p>
</li><li>
<p>Select the <strong><span class="ui">Use Identity Token</span></strong> button again. This time a response is returned from the server without a request for a user name and password.</p>
</li><li>
<p>If you have another email message in your Inbox, you can switch to that email message, select
<strong><span class="ui">IdentityToken</span></strong>, and then select the <strong>
<span class="ui">Use Identity Token</span></strong> button. The response will be returned from the server without a request for a user name or password.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>The following are common errors that can occur when you use Outlook Web App to test a mail app:</p>
<ul>
<li>
<p>The App bar does not appear when a message is selected. If this occurs, restart the application by selecting
<strong><span class="ui">Debug - Stop Debugging</span></strong> in the Visual Studio window, then press F5 to rebuild and deploy the app.</p>
</li><li>
<p>Changes to the JavaScript code may not be picked up when you deploy and run the app. If the changes are not picked up, clear the cache on the web browser by selecting
<strong><span class="ui">Tools - Internet options</span></strong> and clicking the
<strong><span class="ui">Delete&hellip;</span></strong> button. Delete the temporary Internet files and then restart the app.</p>
</li></ul>
</div>
<h1>Related content</h1>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/library/c0520a1e-d9ba-495a-a99f-6816d7d2a23e" target="_blank">Authenticating a mail app by using Exchange identity tokens</a></p>
</li></ul>
</div>
<h1>Change log</h1>
<div id="sectionSection8">
<p>Updated the sample to use the latest APIs and templates.</p>
</div>
</div>
</div>
