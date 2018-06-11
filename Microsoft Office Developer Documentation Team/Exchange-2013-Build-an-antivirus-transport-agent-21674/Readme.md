# Exchange 2013: Build an antivirus transport agent
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Exchange Server 2013
## Topics
* Transport Agent
## IsPublished
* True
## ModifiedDate
* 2013-05-20 07:37:19
## Description

<div id="header">This sample shows you how to use the <strong>SmtpReceiveAgent</strong> and
<strong>RoutingAgent</strong> classes to implement an antivirus transport agent.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the Exchange 2013: Build an antivirus transport agent sample</h1>
<div class="section" id="sectionSection0">
<p>This sample agent responds to the <strong>SmtpReceiveAgent.OnEndOfData</strong> and
<strong>RoutingAgent.OnSubmittedMessage</strong> events and sends the incoming message to an out-of-process COM server that asynchronously examines the message and returns a modified version of it.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A target server that is running Exchange Server 2013</p>
</li><li>
<p>Visual Studio 2012</p>
</li><li>
<p>The .NET Framework version 4.5</p>
</li><li>
<p>The following Exchange&nbsp;2013 assemblies, copied from your target server:</p>
<ul>
<li>
<p>Microsoft.Exchange.Data.Common.dll</p>
</li><li>
<p>Microsoft.Exchange.Data.Transport.dll</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>This sample contains the following files:</p>
<ul>
<li>
<p>Install.ps1 &mdash; An Exchange Management Shell script that installs the compiled sample.</p>
</li><li>
<p>Uninstall.ps1 &mdash; An Exchange Management Shell script that uninstalls the compiled sample.</p>
</li><li>
<p>AntivirusAgent.csproj &mdash; The Visual Studio C# project file for the agent.</p>
</li><li>
<p>AntivirusService.vcxproj &mdash; The Visual Studio C&#43;&#43; project file for the service.</p>
</li><li>
<p>HubAntivirusAgent.cs &mdash; The C# source code that contains the class that derives from the
<strong>RoutingAgent</strong> class.</p>
</li><li>
<p>EdgeAntivirusAgent.cs &mdash; The C# source code that contains the class that derives from the
<strong>SmtpReceiveAgent</strong> class.</p>
</li><li>
<p>ComStream.cs &mdash; The C# source code that implements the COM IStream interface over a .NET Framework stream.</p>
</li><li>
<p>DotNetStream.cs &mdash; The C# source code that implements a .NET Framework stream over a COM IStream.</p>
</li></ul>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to build the Exchange 2013: Build an antivirus transport agent sample.</p>
<ol>
<li>
<p>Open the AntivirusService.vcxproj C&#43;&#43; project by using Visual Studio</p>
</li><li>
<p>Build the project and fix any errors you encounter. This will build AntivirusService.exe and ComInterop.dll.</p>
</li><li>
<p>Open the AntivirusAgent.csproj C# project by using Visual Studio.</p>
</li><li>
<p>Update the reference to the ComInterop.dll that was created from the AntivirusService project.</p>
</li><li>
<p>Update the references to the Microsoft.Exchange.Data.* namespaces.</p>
</li><li>
<p>Build the project and fix any errors you encounter.</p>
</li></ol>
</div>
<h1 class="heading">Install the sample</h1>
<div class="section" id="sectionSection4">
<p>Follow these steps to install and activate the transport agent on your Exchange server.</p>
<ol>
<li>
<p>Edit the install.ps1 script to ensure that the $EXDIR variable is properly initialized for your system. The value of the $EXDIR variable should be the path to the Exchange&nbsp;2013 installation directory.</p>
</li><li>
<p>Copy the following files from your client to the folder C:\TransportAgentSamples\Antivirus on the Exchange server:</p>
<ul>
<li>
<p>AntivirusAgent.dll</p>
</li><li>
<p>AntivirusService.exe</p>
</li><li>
<p>ComInterop.dll</p>
</li><li>
<p>Install.ps1</p>
</li></ul>
</li><li>
<p>Open the Exchange Management Shell and run the install.ps1 script.</p>
</li><li>
<p>Exit the Exchange Management Shell.</p>
</li></ol>
</div>
<h1 class="heading">Uninstall the sample</h1>
<div class="section" id="sectionSection5">
<p>Follow these steps to uninstall the transport agent on your Exchange server.</p>
<ol>
<li>
<p>Edit the uninstall.ps1 script to ensure that the $EXDIR variable is properly initialized for your system. The value of the $EXDIR variable should be the path to the Exchange&nbsp;2013 installation directory.</p>
</li><li>
<p>Copy the following file from your client to the folder C:\TransportAgentSamples\Antivirus on the Exchange server:</p>
<ul>
<li>
<p>Uninstall.ps1</p>
</li></ul>
</li><li>
<p>Open the Exchange Management Shell and run the uninstall.ps1 script.</p>
</li><li>
<p>Exit the Exchange Management Shell.</p>
</li></ol>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/exchange/dd877026(v=exchg.150).aspx" target="_blank">Transport agents in Exchange 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d291ab78-7cdd-4dbe-a5f4-9dc8e9650a33.aspx" target="_blank">Creating transport agents for Exchange 2013</a></p>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release.</p>
</div>
</div>
</div>
