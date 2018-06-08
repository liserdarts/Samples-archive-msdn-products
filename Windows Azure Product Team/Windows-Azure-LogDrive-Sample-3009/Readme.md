# Windows Azure LogDrive Sample
## Requires
* Visual Studio 2010
## License
* Custom
## Technologies
* Windows Service
* IIS
* Microsoft Azure
* Cloud Drive
## Topics
* Windows Azure Adapter
* Cloud Drive with VM Role
## IsPublished
* True
## ModifiedDate
* 2011-04-23 12:30:45
## Description

<div><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>Before you install and use&nbsp;Windows Azure&nbsp;LogDrive Sample you must:</strong></span></div>
<ol>
<li><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>Review the&nbsp;Windows Azure&nbsp;LogDrive Sample&nbsp;license terms by clicking&nbsp;the Custom link above.</strong></span>
</li><li><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>Print and retain a copy of the license terms for your records.</strong></span>
</li></ol>
<div><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>By downloading and using&nbsp;the&nbsp;Windows Azure&nbsp;LogDrive Sample,&nbsp;you agree to such license terms.&nbsp; If you do not accept them, do not use the software.</strong></span></div>
<div><span style="font-family:arial,helvetica,sans-serif; font-size:x-small"><strong>&nbsp;</strong></span></div>
<h1><span style="font-size:large">Introduction</span></h1>
<div><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%">The Windows Azure LogDrive sample shows how to build an adapter that demonstrates services that support a VM role. The sample is a Windows service that mounts a
 Windows Azure drive when the operating system starts, and then configures Internet Information Services (IIS) to write HTTP log files to the drive. To do this, local resources are used, the status of role instances is checked, and Windows Service support is
 provided. For simplicity, each role instance uses its own storage blob as a drive; no sharing between instances is shown.</span></span></div>
<div><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%">Y</span></span><span style="line-height:150%"><span style="line-height:150%">ou can use the following information to understand and
 run the Windows Azure LogDrive sample:</span></span></span></div>
<ul>
<li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><a href="http://go.microsoft.com/?linkid=9710117">Windows Azure Drives white paper</a></span></span></span>
</span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/microsoft.windowsazure.storageclient.clouddrive.aspx">Cloud
 Drive Class</a></span></span></span> </span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/gg466226.aspx">How
 to Develop an Adapter for a VM Role in Windows Azure</a></span></span></span></span></span>
</span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%"><a href="http://www.microsoft.com/windowsazure/sdk/">Windows
 Azure Tools</a></span></span></span></span></span></span></span> </span></p>
</li></ul>
<h1><span style="font-size:large">Prerequisites</span></h1>
<div><span style="font-family:verdana,geneva; font-size:x-small"><span class="LabelEmbedded"><span style="line-height:150%">The following tools and tasks are required before the Windows Azure LogDrive sample can be installed and run:</span></span></span></div>
<ul>
<li>
<div><span style="font-family:verdana,geneva; font-size:x-small"><span class="LabelEmbedded"><span style="line-height:150%"><strong>Storage account</strong></span></span></span><span style="font-size:x-small"><span class="LabelEmbedded"><span style="line-height:150%"><strong>
 and storage <span style="font-family:verdana,geneva">co</span></strong></span></span></span><span class="LabelEmbedded" style="font-family:verdana,geneva"><span style="line-height:150%"><strong><span style="font-size:x-small">ntainer</span></strong></span></span><span class="LabelEmbedded"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small">
 -</span> <span style="line-height:150%">the adapter uses blob storage to facilitate the storing of log data in the Windows Azure drive. You must have access to a storage account in Windows Azure, and you must create a container that is used by the adapter.
 You can use your favorite tool for creating a container in Windows Azure storage with any name that you choose. You will use the name of the container when you configure the hosted service.</span></span></span></div>
</li><li>
<div><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><strong>Windows Azure SDK</strong> -
</span><span style="line-height:150%">provides tools and files that are needed to complete the development process of an application for Windows Azure.</span></span></span></span></span></span></div>
</li><li>
<div><span class="LabelEmbedded" style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><strong>Hyper-V
 Manager</strong> - <span style="line-height:150%">the&nbsp;Windows Azure LogDrive sample is considered to be an adapter that is installed on a server image used for a VM role in Windows Azure. You typically install an adapter while creating the base virtual
 hard drive (VHD) that is uploaded to Windows Azure. You can use Hyper-V Manager to create the base VHD.</span></span></span></span></span></span></span></span></span></div>
</li><li><span class="LabelEmbedded" style="font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><span style="line-height:150%"><span class="LabelEmbedded"><span style="line-height:150%"><strong>Visual
 Studio 2010</strong> - <span style="line-height:150%">the sample provides a Visual Studio 2010 project that you can use to deploy a Windows Azure application that uses the Windows Azure LogDrive sample.</span></span></span></span></span></span></span></span></span></span></span></span>
</li></ul>
<h1><span style="font-size:large; font-weight:bold">Installing and Running the Sample</span></h1>
<div><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%">The Windows Azure LogDrive sample runs as a Windows service</span></span><span style="font-size:x-small"><span style="line-height:150%"> on a virtual machine in
 Windows Azure. The </span></span><span style="font-size:x-small"><span style="line-height:150%">installation of this sample occurs during the creation of the base VHD for a VM role in Windows Azure. You must have completed the&nbsp;beginning steps in
<span class="LinkText" style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/gg433121.aspx">Getting<br>
Started with Developing a Server Image for a VM Role</a>.</span></span></span></span></div>
<div><span style="font-size:x-small"><span style="line-height:150%"><span class="LinkText" style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%">&nbsp;</span></span></span></span></div>
<div><strong><span style="font-size:x-small"><span style="font-family:verdana,geneva"><span style="line-height:150%">To install the sample</span></span></span></strong></div>
<ol>
<li><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%">On the virtual machine where you are installing the sample, open a Command Prompt<br>
window and run the command: <strong><span style="line-height:150%">DISM<br>
/Online /Enable-Feature /FeatureName:NetFx3 /FeatureName:IIS-WebServerRole<br>
/FeatureName:IIS-WebServer /FeatureName:IIS-CommonHttpFeatures<br>
/FeatureName:IIS-HttpErrors /FeatureName:IIS-ApplicationDevelopment<br>
/FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HttpLogging<br>
/FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security<br>
/FeatureName:IIS-RequestFiltering /FeatureName:IIS-Performance<br>
/FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-StaticContent<br>
/FeatureName:IIS-DefaultDocument /FeatureName:IIS-DirectoryBrowsing<br>
/FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-ManagementConsole</span></strong></span></span></span>
</li><li>
<div><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">If you have not already done so, install the Windows Azure Integration
 Components.<br>
For more information about installing these components, see <span class="LinkText">
<span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/gg465409.aspx">How<br>
to Install the Windows Azure Integration Components</a>.</span></span></span></span></span></span></span></div>
</li><li>
<div><span style="line-height:150%; font-size:x-small">Install the sample. To do this:</span></div>
<ol>
<li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%">Open Visual Studio 2010 as an administrator</span></span>.</span></span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%">Browse to the folder where
 you extracted the samples, then browse to the&nbsp;LogDrive folder.</span></span></span></span></span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%">Open
 LogDrive.sln.</span></span></span></span></span></span></span></span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Press
 F6 to build the application from Visual Studio.</span></span></span></span></span></span></span></span></span></span></span></span></p>
</li><li>
<p><span style="font-family:verdana,geneva; font-size:x-small">Browse to the LogDrive\LogDriveService.Setup\Debug folder</span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%">Double-click LogDriveService.msi.</span></span></span></p>
</li><li>
<p><span style="line-height:150%; font-family:verdana,geneva; font-size:x-small"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Finish the installation wizard.</span></span></span></span></p>
</li></ol>
</li><li>
<div><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Finish the process of creating the base VHD, which includes preparing the image and<br>
uploading the VHD. For more information, see <span class="LinkText"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/gg465407.aspx">How to Prepare the Server Image for Uploading to Windows Azure</a></span></span><span style="line-height:150%"><span class="LinkText">.</span></span></span></span></span></span></span></div>
</li><li><span style="line-height:150%">Change the ServiceConfiguration.cscfg file to configure the LogDrive sample to use<br>
your storage account and storage container. To do this:</span>
<ol>
<li><span style="line-height:150%"><span style="line-height:150%">Edit the ServiceConfiguration.cscfg file and&nbsp;add&nbsp;the Setting elements. See the example below.</span></span>
</li><li><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Save the ServiceConfiguration.cscfg file.</span></span></span>
</li></ol>
</li><li><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%">Deploy the application package. For more information, see
<span class="LinkText"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/gg465379.aspx">How to Create and Deploy the VM Role Service Model</a>.</span></span></span></span></span>
</li></ol>
<div><span style="line-height:150%"><span style="line-height:150%"><span style="line-height:150%"><span class="LinkText"><span style="line-height:150%">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;Setting name=&quot;LogDriveService.BlobPath&quot; value=&quot;http://ACCOUNT.blob.core.windows.net/CONTAINER/{0}.vhd&quot;/&gt;
&lt;Setting name=&quot;LogDriveService.AccountName&quot; value=&quot;ACCOUNTNAME&quot;/&gt;
&lt;Setting name=&quot;LogDriveService.AccountKey&quot; value=&quot;ACCOUNTKEY&quot;/&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;Setting</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;LogDriveService.BlobPath&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;http://ACCOUNT.blob.core.windows.net/CONTAINER/{0}.vhd&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;Setting</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;LogDriveService.AccountName&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;ACCOUNTNAME&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;Setting</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;LogDriveService.AccountKey&quot;</span>&nbsp;<span class="xml__attr_name">value</span>=<span class="xml__attr_value">&quot;ACCOUNTKEY&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span class="LabelEmbedded"><span style="line-height:150%; font-size:10pt"><strong>ACCOUNT</strong>
<span style="line-height:150%">is the name that you provided for the storage account address.
<span class="LabelEmbedded"><span style="line-height:150%; font-size:10pt"><strong>ACCOUNTNAME</strong>
<span style="line-height:150%">is<br>
the name of your storage account. <span class="LabelEmbedded"><span style="line-height:150%; font-size:10pt"><strong>ACCOUNTKEY</strong>
<span style="line-height:150%">is the primary key of your storage account. <span class="LabelEmbedded">
<span style="line-height:150%; font-size:10pt"><strong>CONTAINER</strong> <span style="line-height:150%">
is<br>
the storage container that you previously created. For more information about<br>
the configuration of the service model, see <span class="LinkText"><span style="line-height:150%"><a href="http://msdn.microsoft.com/en-us/library/ee758710.aspx">Windows Azure Service Configuration Schema</a>.</span></span></span></span></span></span></span></span></span></span></span></span></span></span></div>
</span></span></span></span></span></div>
<ol>
</ol>
