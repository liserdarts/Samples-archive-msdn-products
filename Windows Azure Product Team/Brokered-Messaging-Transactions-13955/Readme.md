# Brokered Messaging: Transactions
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
* 2014-08-13 04:29:04
## Description

<h1>Introduction</h1>
<p>This sample demonstrates how to use the Windows Azure Service Bus messaging features within a transaction scope in order to ensure batches of messaging operations are committed atomically. See the Service Bus documentation for more information about the
 Service Bus before exploring the samples.</p>
<p>This sample demonstrates: sending and completing messages within a transaction scope; committing and aborting transactions.</p>
<h1>Prerequisites</h1>
<p>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</p>
<h1>Sample Flow</h1>
<p>The sample flows in the following manner:</p>
<ol>
<li>Create a new queue on the Service Bus; </li><li>Send and complete messages within a transaction scope:
<ol>
<li>Send a plain text message to the newly created queue; </li><li>Peek lock the message from the queue; </li><li>Within a transaction scope, send a response message; </li><li>Within a transaction scope, complete the initial message; </li><li>Complete the transaction scope; </li><li>Receive the response message. </li></ol>
</li><li>Send and complete messages within a transaction scope that rolls back:
<ol>
<li>Send a plain text message to the queue; </li><li>Peek lock the message from the queue; </li><li>Within a transaction scope, send a response message; </li><li>Within a transaction scope, complete the initial message; </li><li>Abandon the transaction scope; </li><li>Receive from the queue - since the transaction was not completed, the response message is not in the queue and the initial message is returned to the queue when its peek lock times out.
</li></ol>
</li><li>Clean up resources associated with the sample. </li></ol>
<h1><br>
Running the Sample</h1>
<p>To run the sample:</p>
<ol>
<li>Build and run the sample in Visual Studio. </li><li>When prompted, enter your Service Bus connection string. </li></ol>
<h2><strong>&nbsp;</strong></h2>
<h2><strong>Expected Output</strong></h2>
<p>Please provide a connection string to Service Bus (? for help): &lt;Your connection string&gt;</p>
<p>Creating Queues...</p>
<p>Scenario 1: Send/Complete in a Transaction and then Complete<br>
Sending Message 'Message 1'<br>
Peek-Lock the Message... Message 1<br>
Inside Transaction 5378d67d-19f1-4d27-affa-3e93841be2aa:1<br>
Sending Reply in a Transaction<br>
Completing message in a Transaction<br>
Marking the Transaction Scope as Completed<br>
Receive the reply... Reply To - Message 1</p>
<p>Press [Enter] to move to the next scenario.</p>
<p><br>
Scenario 2: Send/Complete in a Transaction and do not Complete<br>
Sending Message 'Message 2'<br>
Peek-Lock the Message... Message 2<br>
Inside Transaction 5378d67d-19f1-4d27-affa-3e93841be2aa:2<br>
Sending Reply in a Transaction<br>
Completing message in a Transaction<br>
Exiting the transaction scope without committing...<br>
Receive the request again (this can take a while, because we're waiting for the<br>
PeekLock to timeout)... Message 2</p>
<p>Press [Enter] to exit.</p>
<p><br>
<br>
</p>
