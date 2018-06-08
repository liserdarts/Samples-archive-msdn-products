# Brokered Messaging: Async Messaging
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
* 2014-09-02 05:27:26
## Description

<h1>Introduction</h1>
<p>This sample demonstrates how to use Windows Azure Service Bus to send and receive messages asynchronously from a queue. The queue provides decoupled, asynchronous communication between a sender and any number of receivers (here, a single receiver).</p>
<h1>Prerequisites</h1>
<p>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account.</p>
<h1>Sample Flow</h1>
<p>The sample flows in the following manner:&nbsp;</p>
<ol>
<li>Start a sender.
<ol>
<li>Sender prompts for Service Bus connection string. </li><li>Sender creates queue. </li><li>Sender sends messages to queue. </li></ol>
</li><li>Start a receiver.
<ol>
<li>Receiver prompts for Service Bus connection string. </li><li>Receiver retrieves messages from the queue. </li><li>Close receiver. </li></ol>
</li><li>Close sender (deletes queue). </li></ol>
<h1><br>
Running the Sample</h1>
<p><br>
To run the sample:</p>
<ol>
<li>Build the solution in Visual Studio. </li><li>Run the sender and provide the Service Bus connection string. </li><li>Run the receiver and provide the Service Bus connection string. </li></ol>
<p><strong>&nbsp;</strong></p>
<h2><strong>Expected Output - Sender</strong></h2>
<p>Please provide a connection string to Service Bus (/? for help): &lt;connection string&gt;</p>
<p>Creating Queue 'IssueTrackingQueue'...</p>
<p>Sending messages to queue...</p>
<p>Asynchronous Message Send Begin: Id = 1, Body = First Package</p>
<p>Asynchronous Message Send Begin: Id = 2, Body = Second Package</p>
<p>Asynchronous Message Send Begin: Id = 3, Body = Third Package</p>
<p>After all messages are sent, press ENTER to clean up and exit.</p>
<p>Asynchronous Message Send for Id = 3 Successful</p>
<p>Asynchronous Message Send for Id = 2 Successful</p>
<p>Asynchronous Message Send for Id = 1 Successful</p>
<h2><strong><span style="color:black">Expected Output - Receiver</span></strong></h2>
<p><br>
Please provide a connection string to Service Bus (/? for help): &lt;connection string&gt;</p>
<p>Receiving messages from queue...</p>
<p>After all messages are received, press ENTER to exit.</p>
<p>Message Received: Id = 3, Body = Third Package</p>
<p>Message Received: Id = 1, Body = First Package</p>
<p>Message Received: Id = 2, Body = Second Package</p>
<p>Asynchronous Message Receive Completed for Id = 1</p>
<p>Asynchronous Message Receive Completed for Id = 2</p>
<p>Asynchronous Message Receive Completed for Id = 3</p>
