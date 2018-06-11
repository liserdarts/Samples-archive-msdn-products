# Exchange 2013: Build a bandwidth logging transport agent
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
* 2013-05-20 07:21:58
## Description

<div id="header">This sample shows you how to use the <strong>RoutingAgent</strong> classes to implement a bandwidth logging transport agent.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description of the Exchange 2013: Build a bandwidth logging transport agent sample</h1>
<div class="section" id="sectionSection0">
<p>You can keep track of the bandwidth that specific users consume by creating an agent that logs information after a message has been submitted to your server. This sample shows how to create an agent that responds to the
<strong>RoutingAgent.OnSubmittedMessage</strong> and <strong>RoutingAgent.OnRoutedMessage</strong> events, captures bandwidth usage for specified recipients, and logs the bandwidth usage information to a text file.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A target server that is running Exchange Server 2013.</p>
</li><li>
<p>Visual Studio 2012.</p>
</li><li>
<p>The .NET Framework version 4.5.</p>
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
</li><li>
<p>A recipient whose bandwidth is to be logged. You can create and log bandwidth for multiple recipients.</p>
</li><li>
<p>A distribution group that contains the recipients to be logged.</p>
</li><li>
<p>A receive connector to receive email from the Internet.</p>
</li><li>
<p>A send connector for email sent to the Internet.</p>
</li></ul>
<p>You can create a distribution group by using the <a href="http://technet.microsoft.com/en-us/library/aa998856.aspx" target="_blank">
New-DistributionGroup cmdlet</a> cmdlet. Name your distribution group BandwidthLogging. After you create the distribution group, add the recipients for whom you want to track bandwidth usage to the group. You will also need to modify the code in line 59 of
 the BandwidthLogging.cs file to set the full name of the distribution group to include your domain name.</p>
<p>A message must arrive on a receive connector from the Internet and be delivered over a send connector that does not deliver to the Internet in order for the message to count in the inbound bandwidth of a recipient. For more information, see
<a href="http://technet.microsoft.com/en-us/library/jj657447(v=exchg.150).aspx" target="_blank">
Create a Receive Connector to Receive Email from the Internet</a>.</p>
<p>A message must leave the system via a send connector to the Internet to be counted in the outbound bandwidth of a recipient. For more information, see
<a href="http://technet.microsoft.com/en-us/library/jj657457(v=exchg.150).aspx" target="_blank">
Create a Send Connector for Email Sent to the Internet</a>.</p>
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
<p>BandwidthLogging.csproj &mdash; The Visual Studio C# project file.</p>
</li><li>
<p>BandwidthLogging.cs &mdash; The C# source code for the sample.</p>
</li></ul>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to build the Exchange 2013: Build a bandwidth logging transport agent sample.</p>
<ol>
<li>
<p>Open the BandwidthLogging.csproj C# project by using Visual Studio.</p>
</li><li>
<p>Update the distribution group name in line 59 of BandwidthLogging.cs to match the SMTP address for the distribution group you created.</p>
</li><li>
<p>Update the references to the Microsoft.Exchange.Data.* namespaces.</p>
</li><li>
<p>Build the project and fix any errors you encounter.</p>
</li></ol>
<p>&nbsp;</p>
</div>
<h1 class="heading">Install the sample</h1>
<div class="section" id="sectionSection4">
<p>Follow these steps to install and activate the transport agent on your Exchange server.</p>
<ol>
<li>
<p>This sample agent will be installed into the C:\TransportAgentSamples\BandwidthLogging folder. If you want to install it in a different location, edit the install.ps1 script to ensure that the $EXDIR variable is set to the correct folder.</p>
</li><li>
<p>Copy the following files from your client to the folder C:\TransportAgentSamples\Setup\BandwidthLogging on the Exchange&nbsp;server:</p>
<ul>
<li>
<p>BandwidthLogging.dll</p>
</li><li>
<p>Install.ps1</p>
</li></ul>
</li><li>
<p>Open the Exchange Management Shell and run the install.ps1 script.</p>
</li><li>
<p>Exit the Exchange Management Shell.</p>
</li></ol>
<p>&nbsp;</p>
</div>
<h1 class="heading">Uninstall the sample</h1>
<div class="section" id="sectionSection5">
<p>Follow these steps to uninstall the transport agent on your Exchange server.</p>
<ol>
<li>
<p>If you updated the $EXDIR variable in the install script, ensure that the $EXDIR variable is updated in the uninstall.ps1 script to point to the same directory.</p>
</li><li>
<p>Copy the following file from your client to the folder C:\TransportAgentSamples\Setup\BandwidthLogging on the Exchange server:</p>
<ul>
<li>
<p>Uninstall.ps1</p>
</li></ul>
</li><li>
<p>Open the Exchange Management Shell and run the uninstall.ps1 script.</p>
</li><li>
<p>Exit the Exchange Management Shell.</p>
</li></ol>
<p>&nbsp;</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/exchange/dd877026(v=exchg.150).aspx" target="_blank">Transport agents in Exchange 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/d291ab78-7cdd-4dbe-a5f4-9dc8e9650a33.aspx" target="_blank">Creating transport agents for Exchange 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/3f0e745f-9289-4f31-8877-926692a8c133.aspx" target="_blank">How to: Create a RoutingAgent transport agent for Exchange 2013</a></p>
</li><li>
<p><a href="http://technet.microsoft.com/en-us/library/aa998856.aspx" target="_blank">New-DistributionGroup cmdlet</a></p>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release.</p>
</div>
</div>
</div>
