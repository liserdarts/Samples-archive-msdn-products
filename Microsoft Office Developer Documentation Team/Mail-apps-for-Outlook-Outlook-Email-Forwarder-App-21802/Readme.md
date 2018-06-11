# Mail apps for Outlook: Outlook Email Forwarder App
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Outlook 2013
* apps for Office
## Topics
* mail app
## IsPublished
* True
## ModifiedDate
* 2013-08-28 05:18:14
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Outlook email forwarder app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<p>&nbsp;</p>
<div>
<p><span>Summary:</span> This sample app shows how to use JavaScript and Exchange Web Services in a mail app for Outlook to forward the current mail items to a list of user-defined email addresses, and optionally to include user-defined comments with the forwarded
 email.</p>
</div>
<div>
<h1>Description of the sample</h1>
<div id="sectionSection0">
<p>Figure 1 shows the sample mail app available in the app bar of the user's email in Outlook Web Access.</p>
<strong>
<div class="caption">Figure 1. The sample mail app in the app bar.</div>
</strong><br>
<img src="/site/view/file/95092/1/image.png" alt="">
<p>When the user chooses the mail app in the app bar, the full user interface for the app expands into view. The user can then type email addresses separated by semicolons in the space provided, and optionally any comments they want to send with the forwarded
 email.</p>
<p>Finally, when the user chooses the <strong><span class="ui">Go!</span></strong> Button in the app user interface, JavaScript code forwards the email to each of the addresses that the user enters, and includes any comments from the Comments text box with
 the forwarded email. The user interface for the app then shows a message indicating the success or failure of the operation.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires:</p>
<ul>
<li>
<p>Visual Studio 2012 (RTM).</p>
</li><li>
<p>Office 2013 tools for Visual Studio 2012 (RTM).</p>
</li><li>
<p>Either access to an Office 365 Developer Site (highly recommended) or a local installation of Exchange Server 2013.</p>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection2">
<p>The sample app contains:</p>
<ul>
<li>
<p>The OutlookEmailForwarder project, which contains:</p>
<ul>
<li>
<p>The OutlookEmailForwarder.xml manifest file.</p>
</li></ul>
</li><li>
<p>The OutlookEmailForwarderWeb project, which contains multiple template files. However, files that have been developed as part of this sample solution include:</p>
<ul>
<li>
<p>OutlookEmailForwarder.html (in the Pages folder). This contains the HTML user interface that is displayed in the task pane. It consists of simple HTML, two text input controls, and a button input control.</p>
</li><li>
<p>OutlookEmailForwarder.js (in the Scripts folder). This script file contains code that runs when the app is loaded.</p>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for Office, and they have not been modified in the development of this sample app.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>To test the sample, sign up for an Office 365 Developer site, and then send at least one email to an email account that you have configured there before running this sample. Alternatively, you can set up a local installation of Exchange Server 2013 and ensure
 that at least one mailbox has been configured for you, and then send at least one email to that account before running this sample. No other configuration is necessary.</p>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Choose the Ctrl&#43;Shift&#43;B keys to build the solution.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>&nbsp;</p>
<div>
<ol>
<li>
<p>Choose the F5 key to run the app. The following dialog box appears.</p>
<strong>
<div class="caption">Figure 4. Connect to Exchange email account dialog box</div>
</strong><br>
<img src="/site/view/file/95091/1/image.png" alt=""> </li><li>
<p>Enter your Office 365 Developer site credentials, and then choose the <strong>
<span class="ui">Connect</span></strong> button to enable the app to automatically discover the Exchange Web Services URL and the Outlook Web Access URL that it will need to deploy and run the sample.</p>
</li><li>
<p>You may then be prompted to log on to Outlook Web Access for your Office 365 Developer site. If so, enter the same credentials that you used in Step 2.</p>
</li><li>
<p>You can then follow the steps discussed at the beginning of this document to see the app in action.</p>
</li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>If you are attempting to use a local installation of Exchange Server, ask an Exchange administrator to ensure that Exchange Server 2013 is configured correctly. It is recommended that you sign up for an Office 365 Developer site to test this sample, as a
 local installation of Exchange Server 2013 can be complex and time-consuming to set up.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release: April 15, 2013.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/jj220060.aspx" target="_blank">Build apps for Office</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/fp161135.aspx" target="_blank">Mail apps for Outlook</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li></ul>
</div>
</div>
</div>
</div>
