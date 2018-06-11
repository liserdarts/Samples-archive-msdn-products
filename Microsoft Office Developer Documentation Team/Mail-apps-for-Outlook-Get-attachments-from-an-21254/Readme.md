# Mail apps for Outlook: Get attachments from an Exchange server
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Exchange Online
* Outlook Web App
* Exchange 2013
* Outlook 2013
* apps for Office
## Topics
* attachments
## IsPublished
* True
## ModifiedDate
* 2014-03-25 01:19:43
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Mail apps for Outlook: Get attachments from an Exchange server</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p>This sample shows you how to get attachments from an Exchange mailbox.</p>
</div>
<h1>Description of the Mail apps for Outlook: Get attachments from an Exchange server sample</h1>
<div id="sectionSection0">
<p>This sample shows you how to retrieve attachments from a web service that supports your mail app. For example, you can create a service that uploads photos to a sharing site, or a service that stores documents into a repository. The service gets the attachments
 directly from the Exchange server, and doesn't require the client to perform extra processing to get the attachment and then send it along to the service.</p>
<p>The sample has two parts. The first part, the mail app, runs in the email client. The mail app is shown whenever a message or an appointment is the active item. When you select the
<strong><span class="ui">Process attachments</span></strong> button, the mail app sends details about the attachment to the web service that processes the request. The service uses the following steps to process attachments:</p>
<ul>
<li>
<p>Sends a <a href="http://msdn.microsoft.com/en-us/library/aa494316(v=exchg.150).aspx" target="_blank">
GetAttachment operation</a> request to the Exchange server that hosts the mailbox. The server responds by sending the attachment to the service. In this sample, the service simply writes the XML from the server to trace output.</p>
</li><li>
<p>Returns the number of attachments processed to the mail app.</p>
</li></ul>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires that you have the following:</p>
<ul>
<li>
<p>Visual Studio 2012, with the apps for Office project templates.</p>
</li><li>
<p>A computer running cumulative update 1 (CU1) for Exchange 2013 and at least one email account, or an Office 365 developer account.</p>
</li><li>
<p>Familiarity with JavaScript programming and web services.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample solution contains the following files:</p>
<ul>
<li>
<p>Attachments project:</p>
<ul>
<li>
<p>Attachments.xml - The manifest file for the mail app.</p>
</li></ul>
</li><li>
<p>The AttachmentService project defines a REST service by using the WCF API. The project was created by using the WebAPI wizard in Visual Studio 2012. The project contains the following files:</p>
<ul>
<li>
<p>Controllers\AttachmentServiceController.cs - The service object that provides the business logic for the sample service.</p>
</li><li>
<p>Models\ServiceRequest - The object that represents a web request. The contents of the object are created from a JSON request object sent from your mail app.</p>
</li><li>
<p>Models\Attachment.cs - The utility object that helps deserialize the JSON object that is sent by the mail app.</p>
</li><li>
<p>Models\AttachmentDetails.cs - The object that represents the details of each attachment. It provides a .NET Framework object that matches the mail apps
<a target="_blank">AttachmentDetails object</a>.</p>
</li><li>
<p>Models\ServiceResponse - The object that represents a response from the web service. The contents of the object are serialized to a JSON object when they are sent back to the mail app.</p>
</li><li>
<p>Web.config - Binds the sample service to the web server endpoint.</p>
</li></ul>
</li><li>
<p>The AttachmentsWeb project contains the UI and JavaScript code for the mail app. The project was created in Visual Studio 2012 and is based on the mail app project template. The following files were modified for this sample:</p>
<ul>
<li>
<p>App\Home\Home.html - The HTML user interface for the mail app.</p>
</li><li>
<p>App\Home\Home.js - The JavaScript file that handles sending the attachment information to the remote service.</p>
</li><li>
<p>Scripts\Office\1.0\ - The mail app API.</p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>The mail app will be activated on any item in the user's Inbox that has one or more attachments. You can make it easier to test the app by sending one or more email items to your test account before you run the sample app.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Press F5 to build and deploy the sample application. Complete the following tasks to deploy the application:</p>
<ol>
<li>
<p>Connect to an Exchange account by providing the email address and password for an Exchange mailbox.</p>
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
<p>In the app bar, choose <strong><span class="ui">Attachments</span></strong>.</p>
</li><li>
<p>When the EWS Request mail app appears, choose the <strong><span class="ui">Test attachments</span></strong> button to send a request to the Exchange server.</p>
</li><li>
<p>The server will respond with the number of attachments processed for the item. This should equal the number of attachments that the item contains.</p>
</li></ol>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>The following are common errors that can occur when you use Outlook Web App to test a mail app for Outlook:</p>
<ul>
<li>
<p>The app bar does not appear when a message is selected. If this occurs, restart the application by selecting
<strong><span class="ui">Debug - Stop Debugging</span></strong> in the Visual Studio window, then select F5 to rebuild and deploy the app.</p>
</li><li>
<p>Changes to the JavaScript code might not be picked up when you deploy and run the app. If the changes are not picked up, clear the cache on the web browser by selecting
<strong><span class="ui">Tools - Internet options</span></strong> and choosing the
<strong><span class="ui">Delete&hellip;</span></strong> button. Delete the temporary Internet files and then restart the app.</p>
</li></ul>
</div>
<h1>Related content</h1>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://www.asp.net/web-api" target="_blank">Web API: The Official Microsoft ASP.NET Site</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/dn148008.aspx" target="_blank">How to: Get attachments from an Exchange server</a></p>
</li></ul>
</div>
<h1>Change log</h1>
<div id="sectionSection8">
<p>Updated the sample to use the latest API release.</p>
</div>
</div>
</div>
