# Windows Azure SQL Reporting Admin Sample
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* Microsoft Azure
* Windows Azure SQL Reporting
## Topics
* Windows Azure SQL Reporting
## IsPublished
* True
## ModifiedDate
* 2012-08-28 02:29:34
## Description

<h1>Introduction</h1>
<p><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">The SQLReportingAdmin sample for Windows Azure SQL Reporting demonstrates the usage of SQL Reporting APIs, and manages (add/update/delete) permissions of SQL
 Reporting users. </span></p>
<p><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">In the current implementation, the login credentials are stored in the configuration. It is recommended to use a mechanism to encrypt and protect the user credentials
 while trying to access the Windows Azure SQL Reporting portal.</span></p>
<h1><span><span>Prerequisites </span></span></h1>
<p><span><span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">You must install the Windows Azure Software Development Kit (SDK) 1.3 or later to run the SQLReportingAdmin sample. You can get the latest version
 at </span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><a href="https://www.windowsazure.com/en-us/develop/net/">Windows Azure SDK Downloads</a></span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">.</span></span></span></p>
<h1><span><span>Building the Sample</span></span></h1>
<ol>
<li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Use administrator privileges to launch either Microsoft Visual Studio 2010 or<br>
Microsoft Visual Web Developer Express 2010.&nbsp;<span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">To do this, in
<strong>Start</strong> | <strong>All Programs</strong> | <strong>Microsoft Visual Studio 2010</strong>, right-click the
<strong>Microsoft Visual Studio 2010</strong> (or Microsoft Visual Web Developer Express 2010) and choose Run as Administrator. If the User Account Control dialog appears, click
<strong>Continue</strong>.</span> <em>The Windows Azure compute emulator requires that Visual Studio be launched with administrator privileges. For more information about Windows Azure Compute Emulator and other SDK tools, see
<a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg432968.aspx">Overview of the Windows Azure SDK Tools</a>.</em></span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">In Visual Studio, on the
<strong>File</strong> menu, click <strong>Open</strong>, and then browse to folder<br>
where you extracted the samples, then browse to the SQLReportingAdminSample folder.</span></span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Open
 SQLReportingAdminSample.sln file.</span></span></span> </li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">In
<strong>Solution Explorer</strong>, under ManagerPermission project, open Web.Config. If
<strong>Solution Explorer</strong> is not already visible, click <strong>Solution Explorer</strong> on the
<strong>View </strong>menu.</span></span></span></span> </li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Update
 the appSettings section with the below code: </span></span></span></span></span></li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">&lt;appSettings&gt;
    &lt;add key=&quot;RSSERVER_NAME&quot; value=&quot;******&quot;/&gt;
    &lt;add key=&quot;RSUSERNAME&quot; value=&quot;******&quot;/&gt;
    &lt;add key=&quot;RSPASSWORD&quot; value=&quot;*****&quot;/&gt;    
&lt;/appSettings&gt;
</pre>
<div class="preview">
<pre class="csharp">&lt;appSettings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="cs__string">&quot;RSSERVER_NAME&quot;</span>&nbsp;<span class="cs__keyword">value</span>=<span class="cs__string">&quot;******&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="cs__string">&quot;RSUSERNAME&quot;</span>&nbsp;<span class="cs__keyword">value</span>=<span class="cs__string">&quot;******&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="cs__string">&quot;RSPASSWORD&quot;</span>&nbsp;<span class="cs__keyword">value</span>=<span class="cs__string">&quot;*****&quot;</span>/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&lt;/appSettings&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">In the above example, consider replacing the values for RSSERVER_NAME, RSUSERNAME, and RSPASSWORD with appropriate values
 from your SQL Reporting<br>
report server.</span></div>
</span></span></span></span></span></li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Press F6 to build the application from Visual Studio. Press F5 to debug the application. When you debug or run the application from Visual Studio, the following
 actions are performed:</span>
<ol>
<li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">The application is packaged.</span></span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">The
 Windows Azure Compute Emulator is started.</span></span></span> </li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">The
 application package is deployed to the Compute Emulator</span></span></span></span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">The
 browser displays the default web page defined by the web role.</span></span></span></span></span>
</li></ol>
</li></ol>
<h1><span><span>Description</span></span></h1>
<h1><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Before you install and use Windows Azure SQLReportingAdmin sample you must:</span></h1>
<ul>
<li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Review the license terms by clicking on Custom link above.</span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Print and retain a copy of the license terms for your records.</span>
</li></ul>
<p><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">By downloading and using the SQLReportingAdmin sample, you agree to such license terms. If you do not accept them, do not use the software.
</span></p>
<ol>
<li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Once you launch the application, you will see all the report items listed along with the path of the report item, and the type of report item.</span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><img id="59527" src="http://i1.code.msdn.s-msft.com/windowsazure/windows-azure-sql-f1caed47/image/file/59527/1/permissions.png" alt="" width="600" height="201"></span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Select the report item for which you want to modify permissions.
 You can modify permissions of any report item by clicking on Permissions button.</span></span>
</li><li><img id="59528" src="http://i1.code.msdn.s-msft.com/windowsazure/windows-azure-sql-f1caed47/image/file/59528/1/sample.png" alt="" width="444" height="300">
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">The
<strong>Manage Permissions</strong> dialog is launched.</span></span> </li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Click
<strong>Edit</strong> button corresponding to the user for whom you want to edit permissions.</span></span>
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Select/Unselect the checkboxes for the corresponding roles that
 are displayed. Click <strong>Update</strong> button to update the permissions.</span></span>
</li><li><img id="59526" src="http://i1.code.msdn.s-msft.com/windowsazure/windows-azure-sql-f1caed47/image/file/59526/1/editperm.png" alt="" width="624" height="101">
</li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:10pt">Click S<strong>ave</strong> button to update the permissions.</span></span>
</li></ol>
<p>&nbsp;</p>
<h1>More Information</h1>
<ul>
<li><em><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg430130">Windows Azure SQL Reporting</a></span></em>
</li><li><em><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a href="http://www.windowsazure.com/en-us/manage/services/other/SQL-reporting/">Getting
 started with Windows Azure SQL Reporting</a></span></span></em> </li><li><em><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a href="http://www.windowsazure.com/en-us/develop/net/how-to-guides/sql-reporting/">Windows
 Azure SQL Reporting for Application developers</a></span></span></span></em> </li><li><em><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt"><a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg430132#UnsupportedAPIs">Guidelines
 and Limitations for Windows Azure SQL Reporting</a></span></span></span></span></em>
</li></ul>
