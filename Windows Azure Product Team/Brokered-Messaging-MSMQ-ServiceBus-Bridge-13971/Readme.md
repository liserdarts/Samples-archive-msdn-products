# Brokered Messaging; MSMQ ServiceBus Bridge
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
* 2011-11-15 10:56:02
## Description

<h1>Introduction</h1>
<p>This sample demonstrates communication between MSMQ and the Service Bus using a bridge.</p>
<p>There are 3 executables in this sample. The intention of the sample is to showcase a simple implementation of MSMQ-to-ServiceBus bridge using the Bridge project. The MsmqTestClient and ServiceBusTestClient projects are client projects used for testing the
 bridge.</p>
<p>The functionalities of the executables are:</p>
<ul>
<li>The MsmqTestClient program acts as a setup utility as well as an MSMQ test client. It creates all the queues (both MSMQ and Service Bus), and sends/receives messages from MSMQ queues using System.Messaging APIs. When all the messages are sent and received,
 the program deletes the queues. </li><li>The ServiceBusTestClient program sends/receives messages from Service Bus queues using Service Bus APIs.
</li><li>The Bridge program is the MSMQ-ServiceBus bridge that can send messages from an MSMQ queue to a Service Bus queue and vice-versa. The bridge in this sample is a simple implementation that is tied to a fixed MsmqQueue and a Service Bus queue. It can be made
 generic in nature by enumerating queues in MSMQ and Service Bus to achieve an automatic bridging capability. However, it is not the intention of this sample to showcase an all-purpose bridge between MSMQ and Service Bus.
</li></ul>
<p>The sequence of steps performed by the sample are as below:</p>
<ul>
<li>The MsmqTestClient program creates all the necessary queues (MSMQ queues - MsmqSendQueue/MsmqReceiveQueue, Service Bus queues - ServiceBusSendQueue/ServiceBusReceiveQueue)
</li><li>The MsmqTestClient program sends a Message to a MSMQ queue (MsmqSendQueue) </li><li>The Bridge reads this MSMQ message from the MSMQ queue (MsmqSendQueue), converts the message a into BrokeredMessage and sends it to a Service Bus queue (ServiceBusSendQueue)
</li><li>The ServiceBusTestClient program receives the BrokeredMessage from the Service Bus queue (ServiceBusSendQueue) and sends the BrokeredMessage back through another ServiceBus queue (ServiceBusReceiveQueue)
</li><li>The Bridge receives the BrokeredMessage from this Service Bus queue (ServiceBusReceiveQueue) and sends an MSMQ message to another MSMQ queue (MsmqReceiveQueue)
</li><li>The MsmqTestClient program receives the MSMQ message from the second MSMQ queue (MsmqReceiveQueue)
</li><li>The MsmqTestClient program deletes all the queues&nbsp; </li></ul>
<p><strong>Note:</strong> The sample demonstrates simple communication between MSMQ and Service Bus. It does not deal with acknowledgements for the messages being sent.</p>
<p>&nbsp;</p>
<h1>Prerequisites</h1>
<p>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</p>
<p>To install MSQM, please refer to <a href="http://msdn.microsoft.com/en-us/library/aa967729.aspx">
Installing Message Queuing (MSMQ).</a></p>
<p>&nbsp;</p>
<h1>Running the Sample</h1>
<p><br>
After building the solution, run the executables in the following order.</p>
<ol>
<li>MsmqTestClient.exe </li><li>Bridge.exe </li><li>ServiceBusTestClient.exe </li></ol>
<p><br>
Each will prompt for a Service Bus service namespace, issuer name (e.g. &quot;owner&quot;), and issuer key.</p>
<p>When finished, the MsmqTestClient will prompt to press ENTER to clean up queues and exit the application.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
