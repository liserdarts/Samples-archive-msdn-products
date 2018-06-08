# Relayed Messaging Authentication: Shared Secret
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* Service Bus
## Topics
* Service Bus
## IsPublished
* True
## ModifiedDate
* 2014-09-02 07:44:05
## Description

<h1>Introduction</h1>
<p>This sample demonstrates how to use an issuer name and issuer secret to authenticate with the Service Bus. The sample is similar to the Echo sample, with a few changes. Specifically, this sample adds a behavior in the ServiceHost (service side) and ChannelFactory
 (client side).</p>
<h1>Prerequisites</h1>
<p>Create a Service Bus and ACS namespace. For all Service Bus namespaces that were created before August 2014, the Azure Portal automatically created a accompanying ACS namespace. After this date,&nbsp;the Azure Portal no longer creates the ACS namespace.
 Use the New-AzureSBNamespace PowerShell cmdlet to create a Service Bus namespace that is accompanied by an ACS namespace.</p>
<p>&nbsp;</p>
<h1>Service</h1>
<p>Please refer to the Echo sample for information about how the service is defined and configured.</p>
<p>&nbsp;</p>
<h1>Client</h1>
<p>Please refer to the Echo sample for information about how the client is defined and configured.</p>
<p>In this sample, both the client and the service use the same credential; therefore, no additional steps are required to grant the client access to listen or send a message to the service through the Service Bus. By default, a credential is granted with Listen,
 Send, and Manage privileges within its own Service Bus service namespace.&nbsp; If the client is using a different credential than the service, a specific Access Control rule is needed for a client credential to gain Listen, Send or Manage privileges in the
 Service Bus service namespace for the service. For more information about creating Access Control rules, please see the ManagementService sample from the Access Control Samples .</p>
<p>&nbsp;</p>
<h1>Building and Running the Sample</h1>
<ol>
<li>First, build the solution in Visual Studio or from the command line. To run the application, do the following:
</li><li>From a command prompt, run the service (Service\bin\Debug\Service.exe). </li><li>&nbsp;When prompted, enter the Service Namespace, the issuer name (e.g. &quot;owner&quot;) and secret. When authorized, the service indicates that it is listening at the configured address.
</li><li>&nbsp;From another command prompt, run the client (Client\bin\Debug\Client.exe).
</li><li>&nbsp;When prompted, type your issuer name (e.g. &quot;owner&quot;), and the secret and the service namespace to which you want to connect. Enter a line of text to send to the service, then press Enter.
</li><li>&nbsp;When finished, press Enter to exit the client and the service. </li></ol>
<h2><br>
Expected Output &ndash; Client</h2>
<p>Enter the name of the Service Namespace you want to connect to: &lt;service-namespace&gt;<br>
Your Issuer Name: owner<br>
Your Issuer Secret: &lt;issuer-secret&gt;<br>
Enter text to echo (or [Enter] to exit):<br>
a<br>
Server echoed: a<br>
b<br>
Server echoed: b<br>
c<br>
Server echoed: c</p>
<p>&nbsp;</p>
<h2>Expected Output &ndash; Service</h2>
<p>Your Service Namespace: &lt;service-namespace&gt;<br>
Your Issuer Name: owner<br>
Your Issuer Secret: &lt;Issuer Secret&gt;<br>
Service address: sb://&lt;service-namespace&gt;.servicebus.windows.net/SharedSecretAuthenticationService/[Enter] to exit<br>
Echoing: a<br>
Echoing: b<br>
Echoing: c<br>
&nbsp;<br>
</p>
