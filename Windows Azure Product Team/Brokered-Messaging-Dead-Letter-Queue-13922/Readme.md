# Brokered Messaging: Dead Letter Queue
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
* 2014-08-14 02:14:14
## Description

<h1>Introduction</h1>
<p><span style="color:black; font-size:small">This sample demonstrates how to use the Windows Azure Service Bus and the messaging &quot;dead letter queue&quot; functionality.
</span></p>
<p><span style="color:black; font-size:small">The sample shows a simple sender and receiver communicating via a Service Bus queue. Both sender and receiver prompt for Service Bus connection string, which contains the namespace credentials.&nbsp;The sender creates
 the queue, and sends messages simulating different orders into it. The receiver reads orders until the queue is empty, simulating failure on processing some messages. The failing messages are dead-lettered. At the end of the samples, the dead-lettered messages
 are received and logged.</span></p>
<p><span style="color:black; font-size:small">It is also possible to create a separate receiver application for reading the messages in the dead letter queue, and performing additional actions for each message (such as updating order types to include these
 unknown orders).</span><br>
<span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small"><strong>Note</strong>: Dead-lettering also applies to topics and subscriptions, where each subscription has its own dead letter subqueue. It can be accessed in a similar way to a subscription's dead letter subqueue: subscriptionClient.CreateReceiver(&quot;$DeadLetterQueue&quot;).</span></p>
<p>&nbsp;</p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</span></p>
<h2>Sender</h2>
<p><span style="font-size:small">The sender's flow:</span></p>
<ul>
<li><span style="font-size:small">Obtains user credentials and creates a NamespaceManager</span><br>
<span style="font-size:small">(namespaceClient) and a MessagingFactory (messagingFactory). These entities</span><br>
<span style="font-size:small">hold the credentials and are used for all messaging management and runtime</span><br>
<span style="font-size:small">operations. </span></li><li><span style="font-size:small">Creates queue using the namespaceClient </span>
</li><li><span style="font-size:small">Sends messages to queue </span></li><li><span style="font-size:small">Waits for user input to delete queue</span> </li></ul>
<p><br>
<br>
</p>
<h2>Receiver</h2>
<p><br>
<span style="font-size:small">The receiver's flow:</span></p>
<ul>
<li><span style="font-size:small">Gets user credentials, but only creates a MessagingFactory and a</span><br>
<span style="font-size:small">QueueClient (for runtime operations), since the queue was created by the sender</span>
</li><li><span style="font-size:small">Receives messages from the queue and processes them.
</span></li><li><span style="font-size:small">Processing simulates an error by failing to process random</span><br>
<span style="font-size:small">messages for MaxRetryCount times. Once a message cannot be processed</span><br>
<span style="font-size:small">MaxRetryCount times, the message is dead-lettered. </span>
</li><li><span style="font-size:small">Reads and logs messages from the dead-letter queue (separate</span><br>
<span style="font-size:small">dead-letter queue message receiver created) </span>
</li></ul>
<h1><br>
<span style="color:#000000">Running the Sample</span></h1>
<p><span style="color:black">To run the sample, build the solution in Visual Studio or from the command line, then run the two resulting executable files. Start the sender first, then start the receiver. Once the receiver has completed, close the sender to
 clean up the messaging entities. Both programs prompt for your Service Bus connection string.</span></p>
<p><span style="color:black">Note that the expected output below is a sample only - it may not exactly match your run because the sample randomly decides which messages&nbsp; should be dead-lettered.</span></p>
<h2><strong><span style="color:black">Expected Output - Sender</span></strong><span style="color:black">
</span></h2>
<p><span style="font-size:small"><span style="font-family:Times New Roman">&nbsp;</span></span></p>
<p><span style="font-size:small">Please provide a connection string to Service Bus (/? for help): &lt;connection string&gt;</span></p>
<p><span style="font-size:small">Creating queue 'OrdersService'...</span><br>
<span style="font-size:small">Sending messages to queue...</span><br>
<span style="font-size:small">Sending message of order type DeliveryOrder.</span><br>
<span style="font-size:small">Sending message of order type StayInOrder.</span><br>
<span style="font-size:small">Sending message of order type TakeOutOrder.</span><br>
<span style="font-size:small">Sending message of order type TakeOutOrder.</span><br>
<span style="font-size:small">Sending message of order type DeliveryOrder.</span></p>
<p><span style="font-size:small">Press [Enter] to delete queue and exit.</span></p>
<p>&nbsp;</p>
<h2><strong><span style="color:black">Expected Output - Receiver</span></strong><span style="color:black">
</span></h2>
<p>&nbsp;</p>
<p><span style="font-size:small">Please provide a connection string to Service Bus (/? for help):&nbsp;&lt;connection string&gt;</span></p>
<p><span style="font-size:small">Reading messages from queue...</span></p>
<p><span style="font-size:small">Adding Order 1 with 10 number of items and 15 total to DeadLetter queue</span><br>
<span style="font-size:small">Received Order 2 with 15 number of items and 500 total</span><br>
<span style="font-size:small">Adding Order 3 with 1 number of items and 25 total to DeadLetter queue</span><br>
<span style="font-size:small">Adding Order 5 with 3 number of items and 25 total to DeadLetter queue</span><br>
<span style="font-size:small">Received Order 4 with 100 number of items and 100000 total</span></p>
<p><span style="font-size:small">No more messages left in queue. Logging dead lettered messages...</span></p>
<p><br>
<span style="font-size:small">Order 1 with 10 number of items and 15 total logged from DeadLetter queue. DeadLettering Reason is &quot;UnableToProcess&quot; and Deadlettering error description is &quot;Failed to process in reasonable attempts&quot;</span></p>
<p><span style="font-size:small">Order 3 with 1 number of items and 25 total logged from DeadLetter queue. DeadLettering Reason is &quot;UnableToProcess&quot; and Deadlettering error description is &quot;Failed to process in reasonable attempts&quot;</span></p>
<p><span style="font-size:small">Order 5 with 3 number of items and 25 total logged from DeadLetter queue. DeadLettering Reason is &quot;UnableToProcess&quot; and Deadlettering error description is &quot;Failed to process in reasonable attempts&quot;</span></p>
<p><span style="font-size:small">Press [Enter] to exit.</span></p>
