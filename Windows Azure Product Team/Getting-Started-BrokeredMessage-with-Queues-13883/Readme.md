# Getting Started : BrokeredMessage with Queues
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
* False
## ModifiedDate
* 2011-12-13 12:01:04
## Description

<h1>&nbsp;<br>
Introduction</h1>
<p>This sample demonstrates how to use Windows Azure&nbsp;Service Bus to send and receive messages from a queue.</p>
<p>The queue provides decoupled, asynchronous communication between a sender and any number of receivers (here, a single receiver).</p>
<h1>Prerequisites</h1>
<p>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account.</p>
<p>&nbsp;</p>
<h1>Sample Flow</h1>
<p>The sample flows in the following manner:</p>
<ol>
<li>Start a sender.<br>
<ol>
<li>Sender prompts for user Service Bus credentials. </li><li>Sender creates queue. </li><li>Sender sends messages to queue. </li></ol>
</li><li>Start a receiver.<br>
<ol>
<li>Receiver retrieves messages from the queue. </li><li>Close receiver. </li></ol>
</li><li>Close sender (deletes queue). </li></ol>
<h1>Running the Sample</h1>
<p><br>
To run the sample:</p>
<ol>
<li>Build the solution in Visual Studio. </li><li>Run the sender, inputting service namespace, issuer name, and issuer key when prompted to do so.
</li><li>Run the receiver, inputting service namespace, issuer name, and issuer key when prompted to do so.
</li></ol>
<p>&nbsp;</p>
<p><strong>Expected Output - Sender</strong></p>
<p>Please provide the service namespace to use: &lt;service_namespace&gt;<br>
Please provide the issuer name to use: &lt;issuer_name&gt;<br>
Please provide the issuer key to use: &lt;issuer_key&gt;</p>
<p>Creating Queue 'IssueTrackingQueue'...</p>
<p>Sending messages to queue...<br>
Message sent: Id = 1, Body = Package lost<br>
Message sent: Id = 2, Body = Package damaged<br>
Message sent: Id = 3, Body = Package defective</p>
<p>Finished sending messages, press ENTER to clean up and exit.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p><strong>Expected Output - Receiver</strong></p>
<p>Please provide the service namespace to use: &lt;service_namespace&gt;<br>
Please provide the issuer name to use: &lt;issuer_name&gt;<br>
Please provide the issuer key to use: &lt;issuer_key&gt;</p>
<p>Receiving messages from queue...<br>
Message received: Id = 1, Body = Package lost<br>
Message received: Id = 2, Body = Package damaged<br>
Message received: Id = 3, Body = Package defective</p>
<p>End of scenario, press ENTER to exit.</p>
<p>&nbsp;</p>
