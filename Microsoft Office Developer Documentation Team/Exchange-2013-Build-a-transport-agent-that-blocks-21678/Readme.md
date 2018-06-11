# Exchange 2013: Build a transport agent that blocks senders temporarily
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
* 2013-05-20 07:36:38
## Description

<div id="header">This sample shows you how to use the <strong>SmtpReceiveAgent</strong> classes to implement a transport agent that blocks senders temporarily (also known as graylisting).</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the Exchange 2013: Build a transport agent that blocks senders temporarily sample</h1>
<div class="section" id="sectionSection0">
<p>This agent reduces unsolicited email by rejecting email messages from senders the first time a message from that sender is sent to your server. This sample agent responds to the
<strong>OnEndOfHeaders</strong> and <strong>OnRcptCommand</strong> events and determines whether the message sender previously sent messages to the transport server. If the agent determines that the sender did not previously send a message, the sender is added
 to a list of senders and the message is rejected with an SMTP response telling the client to &quot;Please try again later.&quot; If the sender is in the list of previous senders, the agent does not reject the message and it is delivered.</p>
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
<p>MailFilterAgent.csproj &mdash; The Visual Studio C# project file for the agent.</p>
</li><li>
<p>MailFilterAgent.cs &mdash; The C# source code that contains the class that derives from the
<span><strong>[Microsoft.Exchange.Data.Transport.Smtp.SmtpReceiveAgent]</strong></span> class.</p>
</li><li>
<p>MailFilterAgentFactory.cs &mdash; The C# source code that contains the class that derives from the
<span><strong>[Microsoft.Exchange.Data.Transport.Smtp.SmtpReceiveAgentFactory]</strong></span> class.</p>
</li><li>
<p>MailFilterConfig.xml &mdash; An XML file that contains configuration settings for the agent.</p>
</li><li>
<p>MailFilterDatabase.cs &mdash; The C# source code that manages the MailFilter entries for the agent while the agent is in memory.</p>
</li><li>
<p>MailFilterEntry.cs &mdash; The C# source code that contains a class that represents one entry in the MailFilter database.</p>
</li><li>
<p>MailFilterSettings.cs &mdash; The C# source code that manages all the settings the agent uses.</p>
</li></ul>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to build the Exchange 2013: Build a transport agent that blocks senders temporarily sample.</p>
<ol>
<li>
<p>Open the MailFilterAgent.csproj C# project by using Visual Studio.</p>
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
<p>This sample agent will be installed into the C:\TransportAgentSamples\MailFilter folder. If you want to install it in a different location, edit the install.ps1 script to ensure that the $EXDIR variable is set to the correct folder.</p>
</li><li>
<p>Copy the following files from your client to the folder C:\TransportAgentSamples\Setup\MailFilter on the Exchange server:</p>
<ul>
<li>
<p>MailFilterAgent.dll</p>
</li><li>
<p>MailFilterConfig.xml</p>
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
<p>Follow these steps to uninstall the transport agent on your Exchange&nbsp;server.</p>
<ol>
<li>
<p>If you updated the $EXDIR variable in the install script, ensure that the $EXDIR variable is updated in the uninstall.ps1 script to point to the same directory.</p>
</li><li>
<p>Copy the following file from your client to the folder C:\TransportAgentSamples\Setup\MailFilter on the Exchange&nbsp;server:</p>
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
