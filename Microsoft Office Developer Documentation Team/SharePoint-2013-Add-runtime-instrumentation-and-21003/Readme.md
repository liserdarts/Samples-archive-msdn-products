# SharePoint 2013: Add runtime instrumentation and error logging to apps
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* REST
* Javascript
* apps for SharePoint
## Topics
* Cloud
* User Experience
## IsPublished
* True
## ModifiedDate
* 2013-03-06 10:28:58
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013: Add runtime instrumentation and error logging to apps</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">&lt;font color=&quot;DarkGray&quot;&gt;&lt;/font&gt;
<div>&nbsp;</div>
<div class="summary">
<div>Learn how to add custom errors to the app monitoring system and enable runtime request tracing for apps for SharePoint.</div>
</div>
<div class="introduction">
<div>This sample app for SharePoint shows how to add custom errors to the app monitoring system and how to enable runtime request tracing for the app.</div>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Tip</strong></th>
</tr>
<tr>
<td>
<div>Before working with this sample, read <a href="http://msdn.microsoft.com/en-us/library/jj841104.aspx" target="_blank">
Add troubleshooting instrumentation to an app for SharePoint</a> so that you can understand its intended use.</div>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<a name="O15Readme_Description">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<div>The default.aspx page of the app appears after you install and launch the app. It contains a button labeled
<span class="ui">POPULATE DATA</span>.</div>
<div class="caption">Figure 1. Default page of the app</div>
<br>
<img src="InstrumentationSampleDefaultPage.PNG" alt="The default page with Populate Data button">
<div>&nbsp;</div>
<div>Pressing the button displays the titles of all the lists on the host web and s a link to a diagnostics page.</div>
<div class="caption">Figure 2. Data and link to diagnostics page</div>
<br>
<img src="InstrumentationSamplewithDataandDiagnosticsLink.PNG" alt="Default page with sata and diagnostics link">
<div>&nbsp;</div>
<div>The button's click handler also throws an exception. The user doesn't see this exception because the code catches it and logs it using the</div>
</div>
</a>
<div class="section" id="sectionSection0">
<div><a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.utilities.sputility.logcustomremoteapperror.aspx" target="_blank">LogCustomRemoteAppError</a>. After a wait of about 10-15 minutes, you can see this custom error on the
<span class="ui">App Details</span> page for the app. You can open the <span class="ui">
App Details</span> page by clicking the ellipsis button (<span class="ui">&hellip;</span>) beside the app in the
<span class="ui">Site Contents</span> page of the host web. On the callout that opens, click
<span class="ui">Details</span>. Beside <span class="ui">Runtime Errors</span>, the number of runtime errors appears, as shown in Figure 3.</div>
<div class="caption">Figure 3. App Details page</div>
<br>
<img src="InstrumentationSampleAppDetailsPage.PNG" alt="App details page">
<div>&nbsp;</div>
<div>This number is a link. Choose this link to open a callout that gives details about the error.</div>
<div class="caption">Figure 4. Runtime errors callout</div>
<br>
<img src="InstrumentationSampleRuntimeErrorCallout.PNG" alt="Runtime error callout showing error details">
<div>&nbsp;</div>
<div>Return to the start page of the app, or relaunch the app and choose the <span class="ui">
POPULATE DATA</span> button again. Choose the <span class="ui">DIAGNOSTICS PAGE</span> link. On the page that opens, there is a
<span class="ui">TURN ON TRACING</span> button and a <em>disabled</em> link to the trace log. When the page loads a call to the
<a href="http://msdn.microsoft.com/en-us/library/aa334207(v=vs.71).aspx" target="_blank">
Warn</a> method, it throws a warning that says &quot;This trace warning was thrown when the diagnostics page loaded.&quot;. However, this warning does not appear anywhere unless tracing is turned on. Click the button to turn on tracing and enable the link. The link opens
 the web server's trace.axd file, which logs details about requests to the server. The list is initially empty. Navigate back to the apps start page and choose the
<span class="ui">DIAGNOSTICS PAGE</span> link again. Notice that when it opens, the button label has changed to
<span class="ui">TURN OFF TRACING</span>. <em>Do not press it.</em> Choose the <span class="ui">
Trace Log</span> link to open the trace log. Because tracing is now turned on, there are some entries in the log.</div>
<div class="caption">Figure 5. The web server's trace log page</div>
<br>
<img src="InstrumentationSampleTraceLog.PNG" alt="Trace log page">
<div>&nbsp;</div>
<div>Choose the <span class="ui">View Details</span> link for a row in which the Diagnostics.aspx page was opened. Familiarize yourself with all the information that was logged about the request. Notice in particular that the custom warning appears in red.
 Users can then send the information on this page to the technical support personnel for the app.</div>
<div class="caption">Figure 6. Details of the GET request for the diagnostics page</div>
<br>
<img src="InstrumentationTraceDetailsPage.PNG" alt="Instrumentation Trace Details Page">
<div>&nbsp;</div>
</div>
<h1 class="heading">Deviations from best practices</h1>
<div class="section" id="sectionSection1">
<div>The sample is focused on demonstrating an app with runtime instrumentation, so it does not conform to all the good practices that should be used in a production app. Among other things, note the following.</div>
<ul>
<li>
<div>The app has a visible link to a diagnostics page where request tracing can be turned on and off. In a production app, users should not be able to turn on tracing except under the direction of someone from the technical support for the app because tracing
 can significantly reduce performance (for all users of the app, not just the person who turns it on). The best practice is that users should be able to open the diagnostics only by manually entering its URL in the address box of their browser. They should
 get this URL only from tech support.</div>
</li><li>
<div>The app has a way for users to turn off tracing, but there is no way to automatically turn it off if they neglect to turn it off. Because tracing can reduce performance, the best practice is to incorporate logic that will automatically turn off tracing
 after a certain period of time.</div>
</li><li>
<div>The app has minimal exception handling.</div>
</li><li>
<div>The app uses C# code on the server to toggle the visibility of some controls. Performance might be better if JavaScript was used for this purpose, thereby reducing requests to the server</div>
</li></ul>
</div>
<a name="O15Readme_Prereq">
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection2">
<div>This sample requires the following:</div>
<ul>
<li>
<div>Visual Studio 2012 and SharePoint development tools in Visual Studio 2012.</div>
</li><li>
<div></div>
</li></ul>
</div>
</a>
<div class="section" id="sectionSection2">
<ul>
<li>
<div><a href="http://www.iis.net/download/WebDeploy" target="_blank">Web Deploy 2.0</a> installed on the computer with Visual Studio. The version of Visual Studio and its SharePoint tools available for SharePoint 2013 should install this automatically.</div>
</li><li>
<div>A SharePoint Online (Office 365) Developer Site. For more information, see <a href="http://msdn.microsoft.com/en-us/library/fp179924(office.15).aspx" target="_blank">
Sign up for an Office 365 Developer Site</a>. This is an autohosted app, and it can only be installed on a SharePoint Online Developer Site.</div>
</li></ul>
</div>
<a name="O15Readme_components">
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection3">
<ul>
<li>
<div><strong>Instrumentation</strong> project, which contains the AppManifest.xml file.</div>
</li><li>
<div><strong>InstrumentationWeb</strong> project.</div>
<ul>
<li>
<div>Default.aspx file, which contains the HTML and ASP.NET controls for the user interface of the app.</div>
</li><li>
<div>Default.aspx.cs file, which contains the C# code that uses reads SharePoint data, throws an exception, and catches and logs the exception</div>
</li><li>
<div>Diagnostics.aspx file, which controls the user interface for turning on tracing and viewing the trace log.</div>
</li><li>
<div>Diagnostics.aspx.cs file, which contains the C# code that turns tracing on and off.</div>
</li><li>
<div>ChromeLoader.js file, which gives the web application's pages the look and feel of the SharePoint host web.</div>
</li><li>
<div>Web.config, web.debug.config, and web.release,config files. (The web.config file that is packaged with the app is a merger of web.config and either web.debug.config or web.release.config.)</div>
</li></ul>
</li></ul>
</div>
</a><a name="O15Readme_config">
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection4">
<div>Open the Instrumentation.sln file in Visual Studio 2012. In the <span class="ui">
Properties</span> pane of Visual Studio, change the <span><span class="keyword">Site URL</span></span> property of the app for SharePoint project in Visual Studio to the absolute URL of your SharePoint 2013 developer test site on SharePoint Online (for example,
 https://microsoft555.sharepoint.com/).</div>
</div>
</a><a name="O15Readme_build">
<h1 class="heading">Deploy the sample</h1>
<div class="section" id="sectionSection5">
<div>Follow these steps to deploy the sample.</div>
<div class="subSection">
<ol>
<li>
<div>Choose the <span class="ui">Instrumentation</span> project in <span class="ui">
Solution Explorer</span> (not the top node for the whole Visual Studio solution). On the menu bar, choose
<span class="ui">Publish</span>. (Do not choose the F5 key.)</div>
</li><li>
<div>In the <span class="ui">Publish</span> dialog box, choose the <span class="ui">
Finish</span> button. The resulting app package file has an .app extension and is saved in the
<span class="code">app.publish\version_number</span> subfolder of the bin\Debug folder of the Visual Studio project.</div>
</li></ol>
</div>
</div>
</a><a name="O15Readme_test">
<h1 class="heading">Install and test the sample</h1>
<div class="section" id="sectionSection6">
<div class="subSection">
<ol>
<li>
<div>Sign in to your SharePoint Online 2013 site as a tenant administrator.</div>
</li><li>
<div>At the top of the page, choose <span class="ui">Admin</span>, <span class="ui">
SharePoint</span>.</div>
</li><li>
<div>On the <span class="ui">SharePoint Administration Center</span> page, choose
<span class="ui">apps</span>, and then choose <span class="ui">App Catalog</span>. If you haven't already created an app catalog site collection, you will be prompted to create one.</div>
</li><li>
<div>After the app catalog site collection is created, open it, and select <span class="ui">
Apps for SharePoint</span>.</div>
</li><li>
<div>On the <span class="ui">App Catalog</span> page, choose the <span class="ui">
new item</span> link.</div>
</li><li>
<div>On the <span class="ui">Add a document</span> form, browse to your app for SharePoint package and choose the
<span class="ui">OK</span> button. A property form for new items opens.</div>
</li><li>
<div>Fill out the form as needed and choose the <span class="ui">Save</span> button. The app for SharePoint is saved in the catalog.</div>
</li><li>
<div>Browse to any website in the tenancy and choose <span class="ui">Site Contents</span> to open the
<span class="ui">Site Contents</span> page.</div>
</li><li>
<div>Choose <span class="ui">add an app</span>, and on the <span class="ui">Your Apps</span> page, find the app. If there are too many to scroll through, you can enter any part of the app title (<span class="ui">Instrumentation</span>) into the search
 box.</div>
</li><li>
<div>When you find the app, choose the <span class="ui">Details</span> link beneath it, and then on the app details page that opens, choose
<span class="ui">Add It</span>.</div>
</li><li>
<div>You are prompted to grant permissions to the app. Choose <span class="ui">
Trust It</span>.</div>
</li><li>
<div>The <span class="ui">Site Contents</span> page opens and the app is listed. For a short time, a message below the title indicates that it is being added. When this message disappears, you can choose the app icon to launch the app. (You may need to refresh
 the page to make the message disappear.)</div>
</li><li>
<div>Exercise the app as described in the sample description above.</div>
</li></ol>
</div>
</div>
</a><a name="O15Readme_Changelog">
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<div>First release: February 2013</div>
</div>
</a><a name="O15Readme_RelatedContent">
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div></div>
</li></ul>
</div>
</a>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.utilities.sputility.logcustomremoteapperror.aspx" target="_blank">LogCustomRemoteAppError</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj841104.aspx" target="_blank">Add troubleshooting instrumentation to an app for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924(office.15).aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li></ul>
</div>
<h1 class="heading">Contact Info</h1>
<div class="section" id="sectionSection9">
<div>DocThis@microsoft.com</div>
</div>
</div>
</div>
<p><img src="/site/view/file/76513/1/image.png" alt=""> <img src="/site/view/file/76514/1/image.png" alt="">
<img src="/site/view/file/76515/1/image.png" alt=""> <img src="/site/view/file/76516/1/image.png" alt="">
<img src="/site/view/file/76517/1/image.png" alt=""> <img src="/site/view/file/76518/1/image.png" alt=""></p>
