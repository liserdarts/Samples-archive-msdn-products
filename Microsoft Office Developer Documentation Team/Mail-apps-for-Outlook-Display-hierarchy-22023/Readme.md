# Mail apps for Outlook: Display hierarchy information from Active Directory
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Exchange 2013
* Outlook 2013
* apps for Office
## Topics
* ASP.NET
* Extensibility
* web service
## IsPublished
* True
## ModifiedDate
* 2013-05-14 06:00:19
## Description

<div id="header">Learn from this prototype mail app how to access basic hierarchy information from Active Directory. Extend this prototype to customize the mail app for your organization.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description of the Who's Who AD mail app sample</h1>
<div class="section" id="sectionSection0">
<p>This sample accompanies the topic <a href="http://msdn.microsoft.com/library/bb419185-f004-4118-a53d-3b6c8e984c9e.aspx" target="_blank">
How to: Create a mail app to display hierarchy information from Active Directory</a> in the MSDN Library.</p>
<p>When you select an email message in Outlook or Outlook Web App, you can choose the Who's Who AD mail app to display Active Directory information about the sender and other recipients of an email message currently selected in Outlookor Outlook Web App. The
 mail app appears in the app bar when you are viewing an email in the Reading Pane or in the mail explorer.</p>
<p>When you first choose this mail app, it retrieves and displays the sender's detailed professional and hierarchical information from Active Directory&mdash;name, job title, department, alias, office number, telephone number, and a picture thumbnail. If the
 sender has a manager or direct reports, the mail app displays a similar subset of information for each of them, as well. Figure 1 shows an example of the Who's Who AD app. The screen shot displays information for Belinda Newman, her manager, and direct reports.</p>
<div class="caption"><strong>Figure 1. Mail app displays Active Directory information for an email sender in Outlook</strong></div>
<br>
<img src="/site/view/file/81950/1/image.png" alt="">
<p>The mail app provides a navigation bar that allows you to choose a recipient and view detailed professional and hierarchy information that is stored in Active Directory.</p>
<p>Behind the scenes, when you select a sender or recipient, the mail app calls a web service, named Who, to get the person's data from Active Directory. The web service includes an Active Directory wrapper, which uses Active Directory Domain Services (AD DS)
 to access information from Active Directory. After getting the data, the Who web service serializes the data in JSON format and sends it back as the web service response. The mail app then pulls the data and displays it on the app pane. Figure 2 summarizes
 the relationships among the Outlook user, mail app, Who web service, and Active Directory.</p>
<div class="caption"><strong>Figure 2. Relationships among the Outlook user, mail app, Who web service, and Active Directory</strong></div>
<br>
<img src="/site/view/file/81953/1/image.png" alt="">
<p>See the accompanying article <a href="http://msdn.microsoft.com/library/bb419185-f004-4118-a53d-3b6c8e984c9e.aspx" target="_blank">
How to: Create a mail app to display hierarchy information from Active Directory</a> in the MSDN Library for a description of the implementation of the mail app and the Who web service.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>The Who web service serves only as a prototype and shows a few basic features of Active Directory that are familiar to most Active Directory users. Hopefully this example provides a good starting point for you to extend and support features that are specific
 to your organization. For more information, see the section <a href="#ol15_WhosWhoAD_FutureExtension">
Future extension</a> in the accompanying article.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>To get the most out of this code sample, you should be familiar with web development using HTML and JavaScript, and with Windows Communication Foundation (WCF) web services. You do not need prior knowledge of Active Directory Domain Services.</p>
<p>The following are requirements to install and run any mail app, including the Who's Who AD mail app:</p>
<ul>
<li>
<p>The user's mailbox must be on Exchange Server 2013 or a later version.</p>
</li><li>
<p>The mail app must run on Outlook 2013 or a later version, or Outlook Web App.</p>
</li></ul>
<p>You can use any web development tool that you're familiar with to develop the Who's Who AD mail app.</p>
<p>The following tools were used to develop the Who web service, and deploy the mail app and web service:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>.NET Framework 4.0</p>
</li><li>
<p>Windows Server 2008</p>
</li><li>
<p>Internet Information Server (IIS) 7.0</p>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The download for this sample consists of the following files and folders:</p>
<ul>
<li>
<p>Manifest.xml is the manifest file for the Who's Who AD mail app.</p>
</li><li>
<p>WhoMailApp.sln is the Visual Studio solution file for the entire example.</p>
</li><li>
<p>The WhoAgave folder contains files for the mail app (including the HTML, images, and CSS files), and some of the files for the Who web service.</p>
</li><li>
<p>The ActiveDirectory folder contains files for the Active Directory wrapper.</p>
</li><li>
<p>The BuildProcessTemplates folder contains default markup template files for developing WCF web services.</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Use the following steps to obtain the files and modify their references, as appropriate:</p>
<ol>
<li>
<p>On a local drive <em>d</em>:, create a folder called WhosWhoAD and download the sample files there.</p>
</li><li>
<p>Assuming the IIS web server you intend to host the Who's Who AD mail app is called
<em>webserver</em>, create a folder called WhosWhoAD under \\webserver\c$\inetpub\wwwroot\.</p>
</li><li>
<p>Copy the img folder and its contents from d:\WhosWhoAD\WhoAgave\ to \\webserver\c$\inetpub\wwwroot\WhosWhoAD\.</p>
<p>The remaining mail app and web service files will be appropriately copied to webserver when you deploy the web service, as described in the section
<a href="#ol15_WhosWhoADReadme_DeployWebService">Deploy the web service</a> below.</p>
</li><li>
<p>Update the manifest file to reflect the actual location of the mail app HTML file.</p>
<p>The mail app manifest file, manifest.xml, is directly under d:\WhosWhoAD. If your actual web server has a different name than webserver, update manifest.xml to reflect the actual location of the WhoMailApp.html file, by replacing webserver in the following
 line with the server path of the WhosWhoAD folder you created in Step 2.</p>
<div class="code"><span>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th>XML </th>
<th>&nbsp;</th>
</tr>
<tr>
<td colspan="2">
<pre>&lt;SourceLocation DefaultValue=&quot;https://webserver/WhosWhoAD/WhoMailApp.html&quot;/&gt;
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ol>
</div>
<h1 class="heading">Installing the mail app</h1>
<div class="section" id="sectionSection4">
<ol>
<li>
<p>In the Outlookrich client, choose File, Manage Apps.</p>
<p>This opens a browser for you to log on to Outlook Web App to go to the Exchange Admin Center (EAC).</p>
</li><li>
<p>Log on to your Exchange account.</p>
</li><li>
<p>In the EAC, select the drop-down box that is adjacent to the <span class="ui">
&#43;</span> button, and then choose <span class="ui">Add from file</span>, as shown in Figure 3.</p>
<div class="caption"><strong>Figure 3. Installing a mail app from a file in the Exchange Admin Center</strong></div>
<br>
<img src="/site/view/file/81949/1/image.png" alt=""> </li><li>
<p>In the <span class="ui">add from file</span> dialog box, browse to the location of manifest.xml in d:\WhosWhoAD, choose
<span class="ui">Open</span>, and then choose <span class="ui">Next</span>.</p>
<p>You should then see the Who's Who AD app in the list of apps for Outlook, as shown in Figure 4.</p>
<div class="caption"><strong>Figure 4. Who's Who AD app installed on the Exchange Admin Center</strong></div>
<br>
<img src="/site/view/file/81951/1/image.png" alt=""> </li><li>
<p>If Outlook is running, close and reopen Outlook.</p>
</li></ol>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>This procedure is applicable only if your Outlook account is on Exchange Server 2013 or a later version.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>Also, in Step 3, if you do not see <span class="ui">Add from file</span> as an option, you need to request that your Exchange administrator provide the necessary permissions for you.</p>
<p>The Exchange administrator can run the following PowerShell cmdlet to assign a single user the necessary permissions. In this example, wendyri is the user's email alias.</p>
<p><span class="input">New-ManagementRoleAssignment -Role &quot;My Custom Apps&quot; -User &quot;wendyri&quot;</span></p>
<p>If necessary, the administrator can run the following cmdlet to assign similar permissions for multiple users:</p>
<p><span class="input">$users = Get-Mailbox *</span></p>
<p><span class="input">$users | ForEach-Object { New-ManagementRoleAssignment -Role &quot;My Custom Apps&quot; -User $_.Alias}</span></p>
<p>For more information about the My Custom Apps role, see <a href="http://msdn.microsoft.com/library/aa0321b3-2ec0-4694-875b-7a93d3d99089(Office.15).aspx" target="_blank">
My Custom Apps role</a>.</p>
</div>
<h1 class="heading">Deploy the web service</h1>
<div class="section" id="sectionSection5">
<p>Do the following to deploy the Who web service and WhoMailApp.html mail app file:</p>
<ol>
<li>
<p>In Visual Studio, open WhoWebService.csproj.</p>
</li><li>
<p>Choose <span class="ui">Build</span>, <span class="ui">Publish WhoWebService</span>.</p>
</li><li>
<p>In the <span class="ui">Profile</span> tab of the <span class="ui">Publish Web</span> dialog box, specify a profile of your choice.</p>
</li><li>
<p>In the <span class="ui">Connection</span> tab, choose <span class="ui">File System</span> as the
<span class="ui">Publish method</span>.</p>
</li><li>
<p>Type <span class="input">\\webserver\c$\inetpub\wwwroot\WhosWhoAD</span> as the
<span class="ui">Target</span> location.</p>
</li><li>
<p>Choose <span class="ui">Publish</span>.</p>
</li><li>
<p>On the webserver computer, start IIS Manager.</p>
</li><li>
<p>In the <span class="ui">Connections</span> pane, choose <span class="ui">Sites</span>,
<span class="ui">Default Web Site</span>.</p>
</li><li>
<p>Right-click the <span class="ui">WhosWhoAD</span> folder, and choose <span class="ui">
Convert to Application</span>.</p>
</li><li>
<p>In the <span class="ui">Add Application</span> dialog box, under <span class="ui">
Application pool</span> with the <span class="ui">DefaultAppPool</span> listed by default, choose
<span class="ui">Select</span>.</p>
</li><li>
<p>In the <span class="ui">Select Application Pool</span> dialog box, under <span class="ui">
Properties</span>, ensure that <span class="ui">.Net Framework Version: 4.0</span> or a later version of the .NET Framework is displayed. Choose a different application pool, if necessary, to ensure that the pool uses at least .NET Framework 4.0. Choose
<span class="ui">OK</span>.</p>
</li><li>
<p>In the <span class="ui">Add Application</span> dialog box, ensure that you see
<span class="ui">Pass-through authentication</span>, as shown in Figure 6. Choose
<span class="ui">OK</span>. Proceed to Step 14.</p>
<div class="caption"><strong>Figure 5. Add Application dialog box to convert the Who web service as an application in the appropriate application pool on IIS</strong></div>
<br>
<img src="/site/view/file/81952/1/image.png" alt=""> </li><li>
<p>As an alternative to steps 10 through 12, you can create a new application pool that uses .NET Framework 4.0 (or a later version) and pass-through authentication. Select that application pool and proceed to Step 14.</p>
</li><li>
<p>In the middle pane of the IIS Manager, choose <span class="ui">Authentication</span>. Verify that
<span class="ui">Windows Authentication</span> is enabled; right-click to enable it, if necessary.</p>
</li></ol>
<p>The deployment procedure copies the following files to \\webserver\c$\inetpub\wwwroot\WhosWhoAD\:</p>
<ul>
<li>
<p>bin\ActiveDirectoryWrapper.dll</p>
</li><li>
<p>bin\WhoWebService.dll</p>
</li><li>
<p>css\WhoMailApp.css</p>
</li><li>
<p>img\anonymous.jpg</p>
</li><li>
<p>img\app_icon.png</p>
</li><li>
<p>img\envelop.png</p>
</li><li>
<p>img\telephone.png</p>
</li><li>
<p>Web.config</p>
</li><li>
<p>WhoMailApp.html</p>
</li><li>
<p>WhoService.svc</p>
</li></ul>
<p>The Who web service can now be accessed on webserver, and you can now use the Who's Who AD mail app in Outlook or Outlook Web App.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection6">
<ol>
<li>
<p>In Outlook, choose an email to read in the Reading Pane.</p>
</li><li>
<p>Choose the Who's Who AD mail app from the app bar.</p>
</li></ol>
<p>You should be able to see the Active Directory data in the app pane, similar to the example in Figure 1.</p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection7">
<p>If the sender or recipient of an email message has an email address of the form &lt;first name&gt;.&lt;last name&gt;@&lt;domain&gt;, the Active Directory wrapper may not be able to search for the appropriate person in Active Directory. Choose a person whose
 email address is simply of the form &lt;alias&gt;@&lt;domain&gt;.</p>
<p>Because the Who's Who AD mail app is intended to serve as a prototype, there is room for you to customize the mail app to fit the requirements of your organization. See the
<a href="#ol15_WhosWhoAD_FutureExtension">Future extension</a> section in the accompanying article for more information.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<p><a href="http://msdn.microsoft.com/library/bb419185-f004-4118-a53d-3b6c8e984c9e.aspx" target="_blank">How to: Create a mail app to display hierarchy information from Active Directory</a></p>
</div>
</div>
</div>
</div>
