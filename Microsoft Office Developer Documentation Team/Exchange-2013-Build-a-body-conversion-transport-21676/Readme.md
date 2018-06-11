# Exchange 2013: Build a body conversion transport agent
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
* 2013-05-20 07:22:20
## Description

<div id="header">This sample shows you how to use the <strong>SmtpReceiveAgent</strong> classes to implement a body conversion transport agent.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the Exchange 2013: Build a body conversion transport agent sample</h1>
<div class="section" id="sectionSection0">
<p>This sample shows how to filter scripts out of email messages. The sample determines the format of the incoming message and decides whether it needs to filter the message. If it needs to filter the message, it converts the content to HTML, filters the message,
 and then converts it back to the source format.</p>
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
<p>Microsoft.Exchange.Data.dll</p>
</li><li>
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
<p>BodyConversion.csproj &mdash; The Visual Studio C# project file.</p>
</li><li>
<p>BodyConversion.cs &mdash; The C# source code for the sample.</p>
</li></ul>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to build the Exchange 2013: Build a body conversion transport agent sample.</p>
<ol>
<li>
<p>Open the BodyConversion.csproj C# project by using Visual Studio.</p>
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
<p>This sample agent will be installed into the C:\TransportAgentSamples\BodyConversion folder. If you want to install it in a different location, edit the install.ps1 script to ensure that the $EXDIR variable is set to the correct folder.</p>
</li><li>
<p>Copy the following files from your client to the folder C:\TransportAgentSamples\Setup\BodyConversion on the Exchange&nbsp;server:</p>
<ul>
<li>
<p>BodyConversion.dll</p>
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
<p>If you updated the $EXDIR variable in the install script, ensure that the $EXDIR variable is updated in the uninstall.ps1 script to point to the same directory.</p>
</li><li>
<p>Copy the following file from your client to the folder C:\TransportAgentSamples\Setup\BodyConversion on the Exchange&nbsp;server:</p>
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
</li><li>
<p><a href="http://msdn.microsoft.com/library/cdc7c462-74a7-49d6-95b2-155d783840e9.aspx" target="_blank">How to: Create an SmtpReceiveAgent transport agent for Exchange 2013</a></p>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release.</p>
</div>
</div>
</div>
