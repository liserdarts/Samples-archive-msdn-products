# Mail apps for Outlook: Store custom properties on an Exchange server
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Outlook 2013
* apps for Office
* Exchange Server 2013
## Topics
* mail
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-03-08 03:16:56
## Description

<p><span style="font-size:small">This sample shows how to set a property on an email message and then store that property on your Exchange server so that you can retrieve it the next time the item is returned. For example, if your mail app for Outlook adds
 contacts to an external contacts database, you can set a property on an item to show that a contact was added so that you are not prompted to add the same contact a second time.</span></p>
<p><span style="font-size:small">The <a href="http://msdn.microsoft.com/library/dfbec151-8ea7-4915-b723-09ea1396a261">
loadCustomPropertiesAsync method</a> on the item object returns a <a href="http://msdn.microsoft.com/library/ 95a69bd6-c4dc-429a-8b27-e2b68f74f3e3">
CustomProperties object</a> that contains and manages the custom properties that you've stored for an item. After you loaded the custom properties, you can do the following:</span></p>
<ul>
<li><span style="font-size:small">Use the <a href="http://msdn.microsoft.com/library/3ab90551-138a-482d-9d93-4cdb20db193b">
get method</a> and <a href="http://msdn.microsoft.com/library/03a8b253-b681-4a09-b828-80d9cf46ca9d">
set method</a> to read and write custom properties. </span></li><li><span style="font-size:small">Use the <a href="http://msdn.microsoft.com/library/01983beb-766f-4308-9e23-e840e950f7e3">
remove method</a> to delete custom properties that you've created.</span> </li><li><span style="font-size:small">Use the <a href="http://msdn.microsoft.com/library/690d5aa9-62b5-4e5c-9548-62dfdbb5fa56">
saveAsync method</a> to persist any changes that you've made back to the Exchange server.</span>
</li></ul>
<p><span style="font-size:small">You must call the <a href="http://msdn.microsoft.com/library/690d5aa9-62b5-4e5c-9548-62dfdbb5fa56">
saveAsync method</a> to store the properties on the Exchange server; otherwise, all the changes that you made are discarded when the current item is changed.</span></p>
<p><span style="font-size:small">The sample UI has three pages: one to set the key and value of a custom property, one to retrieve the value of a custom property, and one to remove custom properties or to persist the changes that you make to the Exchange server.</span></p>
<p><span style="font-size:small">The JavaScript file contains click handlers for buttons in the UI to get, set, remove, and save custom properties by using the corresponding methods on the
<a href="http://msdn.microsoft.com/library/ 95a69bd6-c4dc-429a-8b27-e2b68f74f3e3">
CustomProperties object</a>. A local Boolean variable, customPropertiesAreLoaded, is set in the callback function for the
<a href="http://msdn.microsoft.com/library/dfbec151-8ea7-4915-b723-09ea1396a261">
loadCustomPropertiesAsync method</a> to show that the custom properties object is loaded. The handlers check this value to make sure that the
<a href="http://msdn.microsoft.com/library/ 95a69bd6-c4dc-429a-8b27-e2b68f74f3e3">
CustomProperties object</a> is available before calling functions on the object.&nbsp;</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires that you have the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012, with the apps for Office project templates.</span>
</li><li><span style="font-size:small">A computer running Exchange 2013 with at least one email account, or an Office 365 developer account.</span>
</li><li><span style="font-size:small">Familiarity with JavaScript programming and web services.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample solution contains the following files:</span></p>
<ul>
<li><span style="font-size:small">CustomProperties project</span>
<ul>
<li><span style="font-size:small">CustomProperties.xml &ndash; The manifest file for the mail app for Outlook.</span>
</li></ul>
</li><li><span style="font-size:small">CustomPropertiesWeb project</span>
<ul>
<li><span style="font-size:small">CustomProperties.html &ndash; The HTML user interface for the mail app for Outlook.</span>
</li><li><span style="font-size:small">Scripts\ CustomProperties.js &ndash; The JavaScript file that handles requesting and using the Exchange Web Services (EWS) request.</span>
</li><li><span style="font-size:small">Scripts\Lib &ndash; The mail app for Outlook and Outlook Web App API.</span>
</li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">The mail app will be activated on any email message in the user's Inbox. You can make it easier to test the app by sending one or more email messages to your test account before you run the sample app.</span></p>
<h1>Build the sample</h1>
<p><span style="font-size:small">Press F5 to build and deploy the sample application. Complete the following tasks to deploy the application:</span></p>
<ol>
<li><span style="font-size:small">Connect to an Exchange account by providing the email address and password for an Exchange 2013 server.</span>
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
</li><li><span style="font-size:small">In the App bar, click <strong>Custom Properties</strong>.</span>
</li><li><span style="font-size:small">When the Custom Properties mail app appears, type a property name and value into the text boxes and then click the
<strong>Save</strong> button to save the property value.</span> </li><li><span style="font-size:small">Click <strong>Get</strong>, type a property name, and then click the
<strong>Get</strong> button to retrieve a property.</span> </li><li><span style="font-size:small">Click <strong>Manage</strong>, and either click
<strong>Persist</strong> to save the stored properties to the Exchange server, or type a property name and click
<strong>Remove</strong> to delete the property from storage.</span> </li></ol>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following are common errors that can occur when you use Outlook Web App to test a mail app for Outlook:</span></p>
<ul>
<li><span style="font-size:small">The App bar does not appear when a message is selected. If this occurs, restart the application by selecting
<strong>Debug &ndash; Stop Debugging</strong> in the Visual Studio window, then press F5 to rebuild and deploy the app.</span>
</li><li><span style="font-size:small">Changes to the JavaScript code may not be picked up when you deploy and run the app. If the changes are not picked up, clear the cache on the web browser by selecting
<strong>Tools &ndash; Internet options</strong> and clicking the Delete&hellip; button. Delete the temporary Internet files and then restart the app.</span>
</li></ul>
<h1>Additional resources</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/ 95a69bd6-c4dc-429a-8b27-e2b68f74f3e3">CustomProperties object</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/dfbec151-8ea7-4915-b723-09ea1396a261">loadCustomPropertiesAsync method</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/3ab90551-138a-482d-9d93-4cdb20db193b">get method</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/03a8b253-b681-4a09-b828-80d9cf46ca9d">set method</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/01983beb-766f-4308-9e23-e840e950f7e3">remove method</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/library/690d5aa9-62b5-4e5c-9548-62dfdbb5fa56">saveAsync method</a></span>
</li></ul>
