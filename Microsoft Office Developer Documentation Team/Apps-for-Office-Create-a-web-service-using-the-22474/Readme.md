# Apps for Office:  Create a web service using the ASP.NET Web API
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Word 2013
* Excel 2013
* apps for Office
* PowerPoint 2013
* Project 2013
## Topics
* web service
## IsPublished
* True
## ModifiedDate
* 2013-06-07 04:31:46
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Create a web service for an app for Office using the ASP.NET Web API</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p></p>
<div>
<p><span>Summary:</span> This sample demonstrates how to create and query an ASP.NET Web API service from an apps for Office. The example app is comprised of a &quot;Send Feedback&quot; page, which lets a user submit feedback, and uses a Web API service to send it to
 the developer team. </p>
</div>
<div>
<h1>Description of the sample</h1>
<div id="sectionSection0" name="collapseableSection">
<p>This sample demonstrates how to create and query an ASP.NET Web API service. The app for Office makes an AJAX request to the web service, passing in data from the client-side JavaScript code. The Web API controller receives the data, performs an action,
 and returns the results back to the caller. The AJAX call then completes, displaying the results or showing an error message. For a walkthrough blog post that describes the sample, see
<a href="http://blogs.msdn.com/b/officeapps/archive/2013/06/05/create-a-web-service-for-an-app-for-office-using-the-asp-net-web-api.aspx" target="_blank">
Walkthrough: Create a web service for an app for Office using the ASP.NET Web API</a>.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1" name="collapseableSection">
<p>This sample requires:</p>
<ul>
<li>
<p>Excel 2013, Word 2013, PowerPoint 2013, or Project 2013</p>
</li><li>
<p>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Internet Explorer 9 or Internet Explorer 10</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2" name="collapseableSection">
<p>The sample app contains:</p>
<ul>
<li>
<p>Home.html and Home.js (in the App/Home folder of the web project) for the client-side page and logic.</p>
</li><li>
<p>SendFeedbackController.cs (in the Controllers folder of the web project) for the ASP.NET Web API controller.</p>
</li><li>
<p>Globals.asax (in the root of the web project), which specifies the default routing for Web API.</p>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>The sample will work and run right out of the box - but it won't be able to really send feedback unless you configure it with appropriate credentials for sending the feedback. To configure it for real use, open the &quot;SendFeedbackController.cs&quot; file (in the
 Controllers folder of the web project), and update the following constants:</p>
<div><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th>C# </th>
<th></th>
</tr>
<tr>
<td colspan="2">
<pre>const string MailingAddressFrom = &quot;app_name@contoso.com &quot;;
    const string MailingAddressTo = &quot;dev_team@contoso.com&quot;;
    const string SmtpHost = &quot;smtp.contoso.com&quot;;
    const int SmtpPort = 587;
    const bool SmtpEnableSsl = true;
    const string SmtpCredentialsUsername = &quot;username&quot;;
    const string SmtpCredentialsPassword = &quot;password&quot;;</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4" name="collapseableSection">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6" name="collapseableSection">
<p>If the app fails to send feedback, showing a notification message with &quot;Sorry, your feedback could not be sent&quot;, be sure that you have configured an appropriate email address as described in &quot;Configure the sample&quot; above. Alternatively, you can remove the
 mail-sending code, and/or replace it with a different form of sending feedback (e.g., logging to a database).</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection">
<p>First release: June 10, 2013.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8" name="collapseableSection">
<ul>
<li>
<p><a href="http://blogs.msdn.com/b/officeapps/archive/2013/06/05/create-a-web-service-for-an-app-for-office-using-the-asp-net-web-api.aspx" target="_blank">Walkthrough: Create a web service for an app for Office using the ASP.NET Web API</a>
</p>
</li><li>
<p><a href="http://www.asp.net/web-api" target="_blank">ASP.NET Web API</a> </p>
</li></ul>
</div>
</div>
</div>
</div>
