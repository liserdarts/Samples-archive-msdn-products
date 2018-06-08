# Relayed Messaging Authentication: Simple WebToken
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
* 2011-11-15 05:20:19
## Description

<h1>Introduction</h1>
<p>This sample demonstrates how to use a simple web token credential to authenticate with the Service Bus.</p>
<p>The sample is similar to the Echo sample, with a few changes. Specifically, this sample adds a behavior in the ServiceHost (service) and ChannelFactory (client) applications.</p>
<p>&nbsp;</p>
<h1>Prerequisites</h1>
<p><br>
&nbsp;If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</p>
<p>&nbsp;</p>
<h1>Service</h1>
<p><br>
Please refer to the Echo sample for information about how the service is defined and configured.</p>
<p>The service authenticates with the Service Bus using a <strong>SimpleWebToken </strong>
credential. It uses a helper method ComputeSimpleWebTokenString(string issuerName, string issuerSecret) to compute the string that is expected by TransportClientEndpointBehavior.</p>
<p>&nbsp;</p>
<h1>Client</h1>
<p><br>
Please refer to the Echo sample for information about how the client is defined and configured.</p>
<p>In this sample, both the client and the service use the same credential; therefore, no additional steps are required to grant the client access to listen or to send messages to the service through the Service Bus.&nbsp; By default, a credential is granted
 with<strong> Listen, Send, and Manage </strong>privileges within its own Service Bus service namespace.&nbsp; If a client is using a different credential than the service, a specific Access Control rule is needed for a client credential to gain<strong> Listen,
 Send or Manage </strong>privileges in the Service Bus service namespace for the service.&nbsp; For more information about creating Access Control rules, please see the
<strong>ManagementService </strong>sample from the <a href="http://acs.codeplex.com/releases/view/57595">
Access Control Samples</a> .</p>
<p>The client also authenticates with the Service Bus using a <strong>SimpleWebToken
</strong>credential. It uses the same <strong>ComputeSimpleWebTokenString(string issuerName, string issuerSecret)</strong> method to compute the web token string.</p>
<p>&nbsp;</p>
<h1>Building and Running the Sample</h1>
<ol>
<li>First, build the solution in Visual Studio or from the command line. To run the application, do the following:
</li><li>From a command prompt, run the service (Service\bin\Debug\Service.exe). </li><li>When prompted, type the service namespace, the issuer name (e.g. &quot;owner&quot;) and the issuer secret key with which you want the service to run. When authorized, the service indicates that it is listening at the configured address.
</li><li>From another command prompt, run the client (Client\bin\Debug\Client.exe). </li><li>When prompted, type the issuer name (e.g. &quot;owner&quot;), the issuer secret key and the service namespace with which you want the client to connect. Enter a line of text to send to the service, then press Enter.
</li><li>When finished, press Enter to exit the client and the service.&nbsp;&nbsp; </li></ol>
<h2>Expected Output &ndash; Service</h2>
<p>Your Service Namespace: &lt;service-namespace&gt;<br>
Your Issuer Name: &lt;issuer-name&gt;<br>
Your Issuer Secret: &lt;issuer-secret&gt;<br>
Service address: sb://&lt;service-namespace&gt;.servicebus.windows.net/SimpleWebTokenAuthenticationService/<br>
Press [Enter] to exit<br>
Echoing: a<br>
Echoing: b<br>
Echoing: c</p>
<p>&nbsp;</p>
<h2>Expected Output &ndash; Client</h2>
<p>Your Service Namespace: &lt;service-namespace&gt;<br>
Your Issuer Name: &lt;issuer-name&gt;<br>
Your Issuer Secret: &lt;issuer-secret&gt;<br>
Enter text to echo (or [Enter] to exit):<br>
a<br>
Server echoed: a<br>
b<br>
Server echoed: b<br>
c<br>
Server echoed: c<br>
&nbsp;<br>
</p>
