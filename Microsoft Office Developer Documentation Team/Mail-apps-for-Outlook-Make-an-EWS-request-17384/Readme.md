# Mail apps for Outlook: Make an EWS request
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Outlook 2013
* apps for Office
* Exchange Web Services (EWS)
* Exchange Server 2013
## Topics
* mail
## IsPublished
* True
## ModifiedDate
* 2013-05-06 12:25:00
## Description

<p><span style="font-size:small"><span style="font-size:small">This sample accompanies the MSDN article
<a href="http://msdn.microsoft.com/en-us/library/office/apps/fp160952(v=office.15).aspx">
Calling Web services from a mail app</a>.</span></span></p>
<p><span style="font-size:small">The JavaScript code in this sample shows a simple request for the subject of the current email message. While this is a simple request, it does demonstrate the steps required to create an EWS service request and the best practices
 for making the request.</span><br>
<span style="font-size:small">The code that creates the EWS XML request includes two methods. The first method, getSoapEnvelope(), wraps a SOAP envelope around a web service request. Because the SOAP envelope is standard for all EWS requests, this method can
 be reused to wrap any EWS request.</span></p>
<p><span style="font-size:small">The second method, getSubjectRequest(), returns the EWS request to get the subject field of an item. The id parameter is the Exchange item identifier for the requested item. Note the following about the request:</span></p>
<ul>
<li><span style="font-size:small">The <strong>ItemShape</strong> element is used to restrict the response to the base shape
<strong>IdOnly</strong>. This limits the response to only the item identifier for the item and prevents excessive data from being sent back from the server.</span>
</li><li><span style="font-size:small">The <strong>AdditionalProperties</strong> element is used to add the Subject field to the response. By using the
<strong>IdOnly</strong> base shape and a list of additional properties, you can limit the size of the response from the server to just the data that your app requires.</span>
</li></ul>
<p><span style="font-size:small">The sendRequest() method is called when you click the
<strong>Make EWS request </strong>button in the app UI. It gets the Exchange identifier of the current item and passes it to the getSubjectRequest() and getSoapEnvelope() methods, then makes an asynchronous call to the Exchange server by using the
<a href="http://msdn.microsoft.com/library/ 2ec380e0-4a67-4146-92a6-6a39f65dc6f2">
makeEwsRequestAsync method</a>. The <a href="http://msdn.microsoft.com/library/ 2ec380e0-4a67-4146-92a6-6a39f65dc6f2">
makeEwsRequestAsync method</a> takes two parameters: the EWS request wrapped in its SOAP envelope, and a callback method that is called when the asynchronous request to EWS is complete. You can add a third optional
<em>userContext</em> parameter to the <a href="http://msdn.microsoft.com/library/2ec380e0-4a67-4146-92a6-6a39f65dc6f2">
makeEwsRequestAsync method</a> if you have to provide additional information to the callback method.</span></p>
<p><span style="font-size:small">The callback() method is called with a single parameter,
<em>asynchResult</em>. The <em>asynchResult</em> object has two members:</span></p>
<ul>
<li><span style="font-size:small">value &ndash; The contents of the response from EWS.</span>
</li><li><span style="font-size:small">context &ndash; The <em>userContext</em> parameter passed to the
<a href="http://msdn.microsoft.com/library/2ec380e0-4a67-4146-92a6-6a39f65dc6f2">
makeEwsRequestAsync method</a>.</span> </li></ul>
<p><span style="font-size:small">The callback method in the sample displays the contents of the response in a scrollable
<strong>div</strong> element in the UI, but your code can use the response in more sophisticated ways.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires that you have the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012, with the apps for Office project templates.</span>
</li><li><span style="font-size:small">A computer running Exchange 2013 with at least one email account, or an Office 365 developer account.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Familiarity with JavaScript programming and web services.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample solution contains the following files:</span></p>
<ul>
<li><span style="font-size:small">EWSRequest project</span>
<ul>
<li><span style="font-size:small">EWSRequest.xml &ndash; The manifest file for the mail app for Outlook.</span>
</li></ul>
</li><li><span style="font-size:small">EWSRequestWeb project</span>
<ul>
<li><span style="font-size:small">EWSRequest.html &ndash; The HTML user interface for the mail app for Outlook.</span>
</li><li><span style="font-size:small">Scripts\EWSRequest.js &ndash; The JavaScript file that handles requesting and using the EWS request.</span>
</li><li><span style="font-size:small">Scripts\Lib &ndash; The mail app for Outlook and Outlook Web App API.</span>
</li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">The mail app will be activated on any email message in the user's Inbox. You can make it easier to test the app by sending one or more email messages to your test account before you run the sample app.</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press F5 to build and deploy the sample application. Complete the following tasks to deploy the application:</span></p>
<ol>
<li><span style="font-size:small">Connect to an Exchange account by providing the email address and password on an Exchange 2013 server.</span>
</li><li><span style="font-size:small">Allow the server to configure the email account.</span>
</li></ol>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">You run and test the sample in the web browser that is started by Visual Studio when you build and deploy the sample.</span></p>
<p><span style="font-size:small">If you are running the sample on an Exchange server that is using the default self-signed certificate, you will receive a certificate error when the web browser starts. After you verify that the web browser is opening the correct
 URL by looking at the web address, you can click <strong>Continue to this Web site</strong> to start Outlook Web App.</span></p>
<p><span style="font-size:small">Follow these steps to run the sample:</span></p>
<ol>
<li><span style="font-size:small">Log on to the email account by entering the account name and password.</span>
</li><li><span style="font-size:small">Select a message in the Inbox.</span> </li><li><span style="font-size:small">Wait for the App bar to appear over the message.</span>
</li><li><span style="font-size:small">In the App bar, click <strong>EWSRequest</strong>.</span>
</li><li><span style="font-size:small">When the EWS Request mail app appears, click the
<strong>Make EWS request </strong>button to request the subject of the current message from the Exchange server.</span>
</li><li><span style="font-size:small">You can review the response XML that is returned by the request.</span>
</li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following are common errors that can occur when you use Outlook Web App to test a mail app for Outlook:</span></p>
<ul>
<li><span style="font-size:small">The App bar does not appear when a message is selected. If this occurs, restart the application by selecting
<strong>Debug &ndash; Stop Debugging</strong> in the Visual Studio window, then press F5 to rebuild and deploy the app.</span>
</li><li><span style="font-size:small">Changes to the JavaScript code may not be picked up when you deploy and run the app. If the changes are not picked up, clear the cache on the web browser by selecting
<strong>Tools &ndash; Internet options</strong> and clicking the <strong>Delete&hellip;</strong> button. Delete the temporary Internet files and then restart the app.</span>
</li></ul>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/f49ef705-a5ab-40d3-8a77-b68fe15d91e7">Create EWS Solutions</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/2ec380e0-4a67-4146-92a6-6a39f65dc6f2">makeEwsRequestAsync method</a></span>
</li></ul>
