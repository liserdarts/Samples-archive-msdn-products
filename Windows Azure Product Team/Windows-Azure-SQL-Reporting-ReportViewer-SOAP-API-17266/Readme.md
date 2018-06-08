# Windows Azure SQL Reporting ReportViewer-SOAP API usage sample
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* Microsoft Azure
* Windows Azure SQL Reporting
* Windows Azure SQL Database
## Topics
* Windows Azure SQL Reporting
## IsPublished
* True
## ModifiedDate
* 2012-08-28 02:26:28
## Description

<h1>Introduction</h1>
<p><span style="font-size:x-small">These sample projects demonstrate how to embed a Microsoft ReportViewer control&nbsp;that points to reports hosted on SQL Reporting report servers and how to use SQL&nbsp;Reporting SOAP APIs&nbsp;in your Windows Azure Web
 application.<em> </em></span></p>
<h1><span><span>Prerequisites </span></span></h1>
<ul>
<li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">You must install the Windows Azure Software Development Kit (SDK) 1.3 or later
 to run this sample. You can get the latest version at </span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><a href="https://www.windowsazure.com/en-us/develop/net/">Windows Azure SDK Downloads</a></span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">.
</span></span></li><li><span style="font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">An active account for SQL Reporting with the server name, username,
 and password. See <a class="externalLink" href="http://go.microsoft.com/fwlink/?LinkID=204134&clcid=0x409">
http://go.microsoft.com/fwlink/?LinkID=204134</a>. </span></span></span></li><li><span style="font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">Reports hosted in your SQL Reportinginstance. See the related sample at
<a class="externalLink" href="http://archive.msdn.microsoft.com/SQLAzureReports">
http://archive.msdn.microsoft.com/SQLAzureReports</a>.</span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;
</span></span></li></ul>
<h1><span>Building the Sample</span></h1>
<ul>
<li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small">Use administrator privileges to launch either Microsoft Visual Studio 2010 or Microsoft Visual Web Developer Express 2010.
<span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">To do this, in
<strong>Start</strong> | <strong>All Programs</strong> | <strong>Microsoft Visual Studio 2010</strong>, right-click the
<strong>Microsoft Visual Studio 2010</strong> (or Microsoft Visual Web Developer Express 2010) and choose Run as Administrator. If the User Account Control dialog appears, click<strong>Continue</strong>.</span>
<em>The Windows Azure compute emulator requires that Visual Studio be launched with administrator privileges. For more information about Windows Azure Compute Emulator and other SDK tools, see
<a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg432968.aspx">Overview of the Windows Azure SDK Tools</a>.
</em></span></li><li><span style="font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">In Visual Studio, on the
<strong>File</strong> menu, click <strong>Open</strong>, and then browse to folder where you extracted the samples, then browse to the SQLReportingAdminSample folder.
</span></span><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">Open
 SQLReportingAdminSample.sln file.</span></span>&nbsp;</span> </span></li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">In
<strong>Solution Explorer</strong>, under ManagerPermission project, open Web.Config.
</span></span></span></span></li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">If
<strong>Solution Explorer</strong> is not already visible, click <strong>Solution Explorer</strong> on the
<strong>View </strong>menu.</span></span></span> </span></li><li><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small">Open the Web.Config from ReportViewerRemoteMode and SOAPManagement projects. Update the appSettings section with the values similar to the ones shown below:
 &nbsp; </span></li></ul>
<li>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">&lt;appSettings&gt;
    &lt;!-- Specify the server name of the SQL Reporting instance. This is found in the URL: https://&lt;RSSERVER_NAME&gt;/ReportServer. --&gt;
    &lt;add key=&quot;RSSERVER_NAME&quot; value=&quot;instance_name.account_name.windowsazure.mscds.com&quot; /&gt;
    &lt;!-- Specify the username used to access the SQL Reporting instance. --&gt;
    &lt;add key=&quot;RSUSERNAME&quot; value=&quot;username&quot;/&gt;
    &lt;!-- Specify the password used to access the SQL Reporting instance. --&gt;
    &lt;add key=&quot;RSPASSWORD&quot; value=&quot;password&quot;/&gt;
    &lt;!-- Specify the path of the report hosted in SQL Reporting. This used by the ReportViewer Server Mode project. --&gt;
    &lt;add key=&quot;RSREPORT_PATH&quot; value=&quot;/dir/subdir/report_name&quot; /&gt;
  &lt;/appSettings&gt;</pre>
<div class="preview">
<pre class="js">&lt;appSettings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Specify&nbsp;the&nbsp;server&nbsp;name&nbsp;of&nbsp;the&nbsp;SQL&nbsp;Reporting&nbsp;instance.&nbsp;This&nbsp;is&nbsp;found&nbsp;<span class="js__operator">in</span>&nbsp;the&nbsp;URL:&nbsp;https:<span class="js__sl_comment">//&lt;RSSERVER_NAME&gt;/ReportServer.&nbsp;--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;RSSERVER_NAME&quot;</span>&nbsp;value=<span class="js__string">&quot;instance_name.account_name.windowsazure.mscds.com&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Specify&nbsp;the&nbsp;username&nbsp;used&nbsp;to&nbsp;access&nbsp;the&nbsp;SQL&nbsp;Reporting&nbsp;instance.&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;RSUSERNAME&quot;</span>&nbsp;value=<span class="js__string">&quot;username&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Specify&nbsp;the&nbsp;password&nbsp;used&nbsp;to&nbsp;access&nbsp;the&nbsp;SQL&nbsp;Reporting&nbsp;instance.&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;RSPASSWORD&quot;</span>&nbsp;value=<span class="js__string">&quot;password&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Specify&nbsp;the&nbsp;path&nbsp;of&nbsp;the&nbsp;report&nbsp;hosted&nbsp;<span class="js__operator">in</span>&nbsp;SQL&nbsp;Reporting.&nbsp;This&nbsp;used&nbsp;by&nbsp;the&nbsp;ReportViewer&nbsp;Server&nbsp;Mode&nbsp;project.&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;RSREPORT_PATH&quot;</span>&nbsp;value=<span class="js__string">&quot;/dir/subdir/report_name&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&lt;/appSettings&gt;</pre>
</div>
</div>
</div>
<ul>
<li>
<div class="endscriptcode"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;"><span lang="EN" style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">In
 the above example, consider replacing the values for RSSERVER_NAME, RSUSERNAME, RSPASSWORD and RSREPORT_PATH with appropriate values from your SQL Reporting report server.</span>&nbsp;</span></span></span></span></span></div>
</li><li>
<div class="endscriptcode"><span style="color:black; line-height:115%; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:x-small">Press F6 to build the application from Visual Studio. Press F5 to debug the application. Set one of the projects, ReportViewerRemoteMode
 or SOAPManagement, as Start UP to see the corresponding results.</span></div>
</li></ul>
</li><li>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<ul>
<li><span style="font-size:x-small">SQL Reporting Reports with ReportViewer control: The ReportViewer controls manage the authentication cookie, making your tasks easier. To display reports deployed to a SQL Reporting report server in the ReportViewer controls,
 you supply the report server URL and the report path as you would for any server report, and implement the IReportServerCredentials interface and use it in ServerReport.ReportServerCredentials. This sample demonstrates the usage of ReportViewer control with
 SQL Reporting reports. </span></li><li><span style="font-size:x-small">SQL Reporting SOAP APIs: The SQL Reporting SOAP API provides several Web service endpoints for developing custom reporting solutions. The management functionality is exposed through the
<a href="http://go.microsoft.com/fwlink/?LinkId=155249">ReportService2005 Namespace</a> and
<a href="http://go.microsoft.com/fwlink/?LinkId=206708">ReportService2010 Namespace</a> endpoints. For the list of unsupported SOAP APIs in SQL Reporting, see
<a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg430132">Guidelines and Limitations for Windows Azure SQL Reporting</a>.&nbsp;This sample demonstrates the usage of SOAP APIs to integrate SQL Reporting with .NET applications.
</span></li><li><span style="font-size:x-small">In the current implementation, the login credentials are stored in the configuration. It is recommended to use a mechanism to encrypt and protect the user credentials while trying to access the Windows Azure SQL Reporting
 portal.</span> </li></ul>
<p>&nbsp;</p>
<h1><span>Source Code Files</span>&nbsp;</h1>
<ul>
<li><span style="font-size:x-small">&quot;SOAP Management demonstrates how to connect to the SOAP endpoint of an SQL Reporting instance and perform management tasks.
</span></li><li><span style="font-size:x-small">&quot;ReportViewer Remote Mode&quot; demonstrates how to host a SQL Reporting report in ReportViewer, running in Windows Azure.
</span>&nbsp; </li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:x-small"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg430130">Windows Azure SQL Reporting</a></span>
</span></li><li><span style="font-size:x-small"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><a href="http://www.windowsazure.com/en-us/manage/services/other/SQL-reporting/">Getting
 started with Windows Azure SQL Reporting</a></span></span>&nbsp; </span></li><li><span style="font-size:x-small"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><a href="http://www.windowsazure.com/en-us/develop/net/how-to-guides/sql-reporting/">Windows
 Azure SQL Reporting for Application developers</a></span></span></span>&nbsp; </span>
</li><li><span style="font-size:x-small">&nbsp;</span><span style="font-size:x-small"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span lang="EN" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg430132#UnsupportedAPIs">Guidelines
 and Limitations for Windows Azure SQL Reporting</a></span></span></span></span></span>
</li></ul>
<p>&nbsp;</p>
</li>