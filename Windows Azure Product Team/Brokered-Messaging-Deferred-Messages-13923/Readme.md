# Brokered Messaging: Deferred Messages
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
* 2014-08-14 02:29:33
## Description

<h1>Introduction</h1>
<div>This sample demonstrates how to use the message deferral feature of the Windows Azure Service Bus.</div>
<div>The sample shows a simple sender and receiver communicating via a Service Bus queue. Both sender and receiver prompt for service namespace and a SAS key. The sender creates the queue, and sends messages of different priorities into it. The receiver reads
 until the queue is empty, immediately processing the high-priority messages, and deferring the low-priority messages. The receiver processes the low-priority messages once the queue is empty and all high-priority messages have been taken care of.</div>
<div>Message deferral capability is also available for messages received from a subscription. A receiver on a subscription can defer messages in exactly the same way as it would for a queue, and can similarly retrieve messages by message receipt.</div>
<h1><strong>Prerequisites</strong></h1>
<div>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<h1><strong>Sender</strong></h1>
<div>The sender constructs the listening URI using the namespace&nbsp;information. The static ServiceBusEnvironment.CreateServiceUri function is provided to help construct the URI with the correct format and domain name. It is strongly recommended that you
 use this function instead of building the URI from scratch because the URI construction logic and format might change in future releases.</div>
<div>The sender then creates a QueueClient, an entity used to create senders/receivers on a queue:</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">private static QueueClient CreateQueueClient(Queue q, TransportClientCredentialBase credentials)
{
       Uri runtimeUri = ServiceBusEnvironment.CreateServiceUri(&quot;sb&quot;, serviceNamespace, string.Empty);
       messagingFactory = MessagingFactory.Create(runtimeUri, credentials);

       return messagingFactory.CreateQueueClient(q);
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">private</span><span class="cs__keyword">static</span>&nbsp;QueueClient&nbsp;CreateQueueClient(Queue&nbsp;q,&nbsp;TransportClientCredentialBase&nbsp;credentials)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Uri&nbsp;runtimeUri&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="cs__string">&quot;sb&quot;</span>,&nbsp;serviceNamespace,&nbsp;<span class="cs__keyword">string</span>.Empty);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;messagingFactory&nbsp;=&nbsp;MessagingFactory.Create(runtimeUri,&nbsp;credentials);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;messagingFactory.CreateQueueClient(q);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
</div>
<div>The sender opens a MessageSender using the QueueClient, generates a few messages of different priorities, and sends them into the queue. The sender waits for user input to close, and deletes the queue (queue messages automatically deleted) to clean up.</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public static void Main()
{
        ...

        // Send messages to queue:
        Console.WriteLine(&quot;Sending messages to queue...&quot;);
        using (MessageSender sender = queueClient.CreateSender())
        {
               BrokeredMessage message1 = CreateOrderMessage(&quot;High&quot;);
               sender.Send(message1);
               Console.WriteLine(&quot;Sent message {0} with high priority.&quot;, message1.MessageId);

               BrokeredMessage message2 = CreateOrderMessage(&quot;Low&quot;);
               sender.Send(message2);
               Console.WriteLine(&quot;Sent message {0} with low priority.&quot;, message2.MessageId);

               BrokeredMessage message3 = CreateOrderMessage(&quot;High&quot;);
               sender.Send(message3);
               Console.WriteLine(&quot;Sent message {0} with high priority.&quot;, message3.MessageId);
        }

        Console.WriteLine();
        Console.WriteLine(&quot;Press [Enter] to delete queue and exit.&quot;);
        Console.ReadLine();

        // Cleanup:
        messagingFactory.Close();
        namespaceClient.DeleteQueue(queue.Path);
}

private static BrokeredMessage CreateOrderMessage(string priority)
{
        BrokeredMessage message = BrokeredMessage.CreateMessage();
        message.MessageId = &quot;Order&quot; &#43; Guid.NewGuid().ToString();
        message.Properties.Add(&quot;Priority&quot;, priority);
        return message;
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Send&nbsp;messages&nbsp;to&nbsp;queue:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sending&nbsp;messages&nbsp;to&nbsp;queue...&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;(MessageSender&nbsp;sender&nbsp;=&nbsp;queueClient.CreateSender())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;message1&nbsp;=&nbsp;CreateOrderMessage(<span class="cs__string">&quot;High&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Send(message1);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sent&nbsp;message&nbsp;{0}&nbsp;with&nbsp;high&nbsp;priority.&quot;</span>,&nbsp;message1.MessageId);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;message2&nbsp;=&nbsp;CreateOrderMessage(<span class="cs__string">&quot;Low&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Send(message2);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sent&nbsp;message&nbsp;{0}&nbsp;with&nbsp;low&nbsp;priority.&quot;</span>,&nbsp;message2.MessageId);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;message3&nbsp;=&nbsp;CreateOrderMessage(<span class="cs__string">&quot;High&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Send(message3);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sent&nbsp;message&nbsp;{0}&nbsp;with&nbsp;high&nbsp;priority.&quot;</span>,&nbsp;message3.MessageId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Press&nbsp;[Enter]&nbsp;to&nbsp;delete&nbsp;queue&nbsp;and&nbsp;exit.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Cleanup:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;messagingFactory.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;namespaceClient.DeleteQueue(queue.Path);&nbsp;
}&nbsp;
&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;BrokeredMessage&nbsp;CreateOrderMessage(<span class="cs__keyword">string</span>&nbsp;priority)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;message&nbsp;=&nbsp;BrokeredMessage.CreateMessage();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;message.MessageId&nbsp;=&nbsp;<span class="cs__string">&quot;Order&quot;</span>&nbsp;&#43;&nbsp;Guid.NewGuid().ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;message.Properties.Add(<span class="cs__string">&quot;Priority&quot;</span>,&nbsp;priority);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;message;&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<br>
<br>
<h1><strong>Receiver</strong></h1>
<p>The receiver opens a MessageReceiver on the queue and keeps on receiving until the queue is empty. Any high-priority messages are immeditately processed. Low-priority messages are deferred, and their message receipts are tracked.<br>
(NOTE: deferred messages can only be retrieved by message receipt, so it is important to keep track of those receipts.) Once the queue is empty and all high-priority messages have been processed, the receiver returns to the deferred messages, retrieves them
 by message receipt, and processes them.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public static void Main()
{
       ...

       // Read messages from queue until queue is empty:
       Console.WriteLine(&quot;Reading messages from queue...&quot;);

       MessageReceiver receiver = queueClient.CreateReceiver();
       List&lt;MessageReceipt&gt; deferredMessageReceipts = new List&lt;MessageReceipt&gt;();

       BrokeredMessage receivedMessage;
       while (receiver.TryReceive(TimeSpan.FromSeconds(10), out receivedMessage))
       {
              // Low-priority messages will be dealt with later:
              if (receivedMessage.Properties[&quot;Priority&quot;].ToString() == &quot;Low&quot;)
              {
                      receivedMessage.Defer();
                      Console.WriteLine(&quot;Deferred message with id {0}.&quot;, receivedMessage.MessageId);
                      // Deferred messages can only be retrieved by message receipt. Here, keeping track of the
                     // message receipt for a later retrieval:
                    deferredMessageReceipts.Add(receivedMessage.MessageReceipt);
               }
               else
               {
                     ProcessMessage(receivedMessage);
               }
         }

         Console.WriteLine();
         Console.WriteLine(&quot;No more messages left in queue. Moving onto deferred messages...&quot;);

        // Process the low-priority messages:
        foreach (MessageReceipt receipt in deferredMessageReceipts)
         {
                ProcessMessage(receiver.Receive(receipt));
          }

          Console.WriteLine();
          Console.WriteLine(&quot;Press [Enter] to exit.&quot;);
          Console.ReadLine();

          receiver.Close();
}

private static void ProcessMessage(BrokeredMessage message)
{
         Console.WriteLine(&quot;Processed {0}-priority order {1}.&quot;, message.Properties[&quot;Priority&quot;], message.MessageId);
         message.Complete();
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Read&nbsp;messages&nbsp;from&nbsp;queue&nbsp;until&nbsp;queue&nbsp;is&nbsp;empty:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Reading&nbsp;messages&nbsp;from&nbsp;queue...&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MessageReceiver&nbsp;receiver&nbsp;=&nbsp;queueClient.CreateReceiver();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;MessageReceipt&gt;&nbsp;deferredMessageReceipts&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;MessageReceipt&gt;();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessage&nbsp;receivedMessage;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(receiver.TryReceive(TimeSpan.FromSeconds(<span class="cs__number">10</span>),&nbsp;<span class="cs__keyword">out</span>&nbsp;receivedMessage))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Low-priority&nbsp;messages&nbsp;will&nbsp;be&nbsp;dealt&nbsp;with&nbsp;later:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(receivedMessage.Properties[<span class="cs__string">&quot;Priority&quot;</span>].ToString()&nbsp;==&nbsp;<span class="cs__string">&quot;Low&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;receivedMessage.Defer();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Deferred&nbsp;message&nbsp;with&nbsp;id&nbsp;{0}.&quot;</span>,&nbsp;receivedMessage.MessageId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Deferred&nbsp;messages&nbsp;can&nbsp;only&nbsp;be&nbsp;retrieved&nbsp;by&nbsp;message&nbsp;receipt.&nbsp;Here,&nbsp;keeping&nbsp;track&nbsp;of&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;message&nbsp;receipt&nbsp;for&nbsp;a&nbsp;later&nbsp;retrieval:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;deferredMessageReceipts.Add(receivedMessage.MessageReceipt);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProcessMessage(receivedMessage);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;No&nbsp;more&nbsp;messages&nbsp;left&nbsp;in&nbsp;queue.&nbsp;Moving&nbsp;onto&nbsp;deferred&nbsp;messages...&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Process&nbsp;the&nbsp;low-priority&nbsp;messages:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(MessageReceipt&nbsp;receipt&nbsp;<span class="cs__keyword">in</span>&nbsp;deferredMessageReceipts)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProcessMessage(receiver.Receive(receipt));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Press&nbsp;[Enter]&nbsp;to&nbsp;exit.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;receiver.Close();&nbsp;
}&nbsp;
&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ProcessMessage(BrokeredMessage&nbsp;message)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Processed&nbsp;{0}-priority&nbsp;order&nbsp;{1}.&quot;</span>,&nbsp;message.Properties[<span class="cs__string">&quot;Priority&quot;</span>],&nbsp;message.MessageId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;message.Complete();&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<h1><strong>Running the Sample</strong></h1>
<p>To run the sample, build the solution in Visual Studio or from the command line, then run the two resulting executable files. Start the sender first, then start the receiver. Once the receiver has completed, close the sender to clean up the<br>
messaging entities. Both programs prompt for your Service Bus namespace and the issuer credentials.</p>
<p>&nbsp;</p>
<h2><strong>Expected Output - Sender</strong></h2>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please provide the namespace: &lt;namespace name&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please provide the&nbsp;key name: &lt;key name&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please provide the key: &lt;key&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Creating queue &quot;OrdersQueue&quot;.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sending messages to queue...<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sent message Order60f16940-ca49-4620-b93a-a770e1566c89 with high priority.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sent message Order6f94cf34-c476-4489-acaf-a6924979f5ab with low priority.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sent message Order0211ceea-b8ff-4bdc-927b-1e2a657401bd with high priority.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Press [Enter] to delete queue and exit.</p>
<p>&nbsp;</p>
<h1><strong>Expected Output - Receiver</strong></h1>
<br>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please provide the namespace: &lt;namespace name&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please provide the&nbsp;key name: &lt;key name&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please provide the key: &lt;key&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reading messages from queue...<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Processed High-priority order Order60f16940-ca49-4620-b93a-a770e1566c89.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Deferred message with id Order6f94cf34-c476-4489-acaf-a6924979f5ab.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Processed High-priority order Order0211ceea-b8ff-4bdc-927b-1e2a657401bd.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No more messages left in queue. Moving onto deferred messages...<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Processed Low-priority order Order6f94cf34-c476-4489-acaf-a6924979f5ab.</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Press [Enter] to exit.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
</div>
</div>
