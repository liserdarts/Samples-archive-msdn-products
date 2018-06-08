# Brokered Messaging: Management Operations Using REST
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
* 2014-08-14 12:16:12
## Description

<h1>Introduction</h1>
<div>The Windows Azure Service Bus offers a REST API for runtime and management (AtomPub) operations. By using REST, you can write applications in any language that supports HTTP requests, without the need for a client SDK. The Service Bus is a multi-protocol
 service. You can send and receive messages to or from the service using REST or .NET managed API; mixing and matching clients using different protocols in a given scenario. For example, you can send a message to a queue using one protocol and consume it using
 a different protocol.</div>
<div></div>
<div>This sample demonstrates how to perform management operations on the Service Bus using REST, specifically:</div>
<ul>
<li>Create a queue </li><li>Send a message to a queue </li><li>Receive a message from a queue </li><li>List queues </li><li>Delete a queue </li><li>Create a topic </li><li>Create a subscription </li><li>Send a message to a topic </li><li>Receive a message from a subscription </li><li>List topics </li><li>List subscriptions </li><li>Delete a subscription </li><li>Delete a topic </li></ul>
<div>&nbsp;</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection0">
<div>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<ul>
<li>Open the sample in Visual Studio. </li><li>Supply your namespace name. </li><li>Supply either a SAS key and key name, or an ACS identity and secret. If you want to use ACS, make sure you have an ACS namespace. ALso, upcomment the line that calls the GetAcsToken method.
</li><li>Build and run the sample. </li></ul>
</div>
<h1 class="heading">API Documentation</h1>
<div class="section" id="DIV1">
<div>For more information about the REST API, see the <a href="http://msdn.microsoft.com/en-us/library/gg278338.aspx">
Service Bus REST API Reference</a> page on MSDN.</div>
<div>&nbsp;</div>
<h1>Expected Output</h1>
<div>&nbsp;</div>
<div>
<p>Creating queue <a href="https://&lt;&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f">
https://&lt;service namespace&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f</a></p>
<p>Sending message msg1 - to address <a href="https://&lt;&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f/">
https://&lt;service namespace&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f/</a><br>
messages?timeout=60</p>
<p>Retrieving message from <a href="https://&lt;&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f/messages/h">
https://&lt;service namespace&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f/messages/h</a><br>
ead?timeout=60<br>
msg1</p>
<p>&nbsp;</p>
<p>Creating topic <a href="https://&lt;&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558</a></p>
<p>Creating subscription <a href="https://&lt;&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscription">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscription</a><br>
s/Subscription42a592fa-1819-4620-a957-91cc1e649b64</p>
<p>Sending message msg2 - to address <a href="https://&lt;&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/</a><br>
messages?timeout=60</p>
<p>Retrieving message from <a href="https://&lt;&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscripti">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscripti</a><br>
ons/Subscription42a592fa-1819-4620-a957-91cc1e649b64/messages/head?timeout=60<br>
msg2<br>
msg2</p>
<p>&nbsp;</p>
<p>Getting resources from <a href="https://OGFNamespace.servicebus.windows.net/$Resources/Queues">
https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a><br>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;<br>
&lt;feed xmlns=&quot;<a href="http://www.w3.org/2005/Atom">http://www.w3.org/2005/Atom</a>&quot;&gt;<br>
&nbsp; &lt;title type=&quot;text&quot;&gt;Queues&lt;/title&gt;<br>
&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues&lt;/id&gt;<br>
&nbsp; &lt;updated&gt;2011-11-16T19:28:40Z&lt;/updated&gt;<br>
&nbsp; &lt;link rel=&quot;self&quot; href=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Queues">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a>&quot; /&gt;<br>
&nbsp; &lt;entry xml:base=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Queues">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a>&quot;&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/another&lt;/id&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;title type=&quot;text&quot;&gt;another&lt;/title&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;published&gt;2011-11-12T01:44:48Z&lt;/published&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;updated&gt;2011-11-12T01:44:48Z&lt;/updated&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;author&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;name&gt;&lt;service namespace&gt;&lt;/name&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/author&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;link rel=&quot;self&quot; href=&quot;../another&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;content type=&quot;application/xml&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;QueueDescription xmlns=&quot;<a href="http://schemas.microsoft.com/netservices/2010/10/servicebus/connect">http://schemas.microsoft.com/netservices/2010/10/servicebus/connect</a>&quot; xmlns:i=&quot;<a href="http://www">http://www</a>.<br>
w3.org/2001/XMLSchema-instance&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;LockDuration&gt;PT1M&lt;/LockDuration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxSizeInMegabytes&gt;1024&lt;/MaxSizeInMegabytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresDuplicateDetection&gt;false&lt;/RequiresDuplicateDetection&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresSession&gt;false&lt;/RequiresSession&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DefaultMessageTimeToLive&gt;P10675199DT2H48M5S&lt;/DefaultMessageTimeToLive&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DeadLetteringOnMessageExpiration&gt;false&lt;/DeadLetteringOnMessageExpiration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DuplicateDetectionHistoryTimeWindow&gt;PT10M&lt;/DuplicateDetectionHistoryTimeWindow&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxDeliveryCount&gt;10&lt;/MaxDeliveryCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;EnableBatchedOperations&gt;true&lt;/EnableBatchedOperations&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;SizeInBytes&gt;0&lt;/SizeInBytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MessageCount&gt;0&lt;/MessageCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/QueueDescription&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/content&gt;<br>
&nbsp; &lt;/entry&gt;<br>
&nbsp; &lt;entry xml:base=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Queues">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a>&quot;&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/queuee800a4c4-0d02-4695-8044-42324c56211f&lt;/id&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;title type=&quot;text&quot;&gt;queuee800a4c4-0d02-4695-8044-42324c56211f&lt;/title&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;published&gt;2011-11-16T19:28:37Z&lt;/published&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;updated&gt;2011-11-16T19:28:37Z&lt;/updated&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;author&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;name&gt;&lt;service namespace&gt;&lt;/name&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/author&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;link rel=&quot;self&quot; href=&quot;../queuee800a4c4-0d02-4695-8044-42324c56211f&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;content type=&quot;application/xml&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;QueueDescription xmlns=&quot;<a href="http://schemas.microsoft.com/netservices/2010/10/servicebus/connect">http://schemas.microsoft.com/netservices/2010/10/servicebus/connect</a>&quot; xmlns:i=&quot;<a href="http://www">http://www</a>.<br>
w3.org/2001/XMLSchema-instance&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;LockDuration&gt;PT1M&lt;/LockDuration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxSizeInMegabytes&gt;1024&lt;/MaxSizeInMegabytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresDuplicateDetection&gt;false&lt;/RequiresDuplicateDetection&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresSession&gt;false&lt;/RequiresSession&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DefaultMessageTimeToLive&gt;P10675199DT2H48M5.4775807S&lt;/DefaultMessageTimeToLive&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DeadLetteringOnMessageExpiration&gt;false&lt;/DeadLetteringOnMessageExpiration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DuplicateDetectionHistoryTimeWindow&gt;PT10M&lt;/DuplicateDetectionHistoryTimeWindow&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxDeliveryCount&gt;10&lt;/MaxDeliveryCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;EnableBatchedOperations&gt;true&lt;/EnableBatchedOperations&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;SizeInBytes&gt;0&lt;/SizeInBytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MessageCount&gt;0&lt;/MessageCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/QueueDescription&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/content&gt;<br>
&nbsp; &lt;/entry&gt;<br>
&lt;/feed&gt;</p>
<p><br>
Getting resources from <a href="https://OGFNamespace.servicebus.windows.net/$Resources/Topics">
https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics</a><br>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;<br>
&lt;feed xmlns=&quot;<a href="http://www.w3.org/2005/Atom">http://www.w3.org/2005/Atom</a>&quot;&gt;<br>
&nbsp; &lt;title type=&quot;text&quot;&gt;Topics&lt;/title&gt;<br>
&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics&lt;/id&gt;<br>
&nbsp; &lt;updated&gt;2011-11-16T19:28:41Z&lt;/updated&gt;<br>
&nbsp; &lt;link rel=&quot;self&quot; href=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Topics">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics</a>&quot; /&gt;<br>
&nbsp; &lt;entry xml:base=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Topics">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics</a>&quot;&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/topicb17fae35-c649-49f4-89b1-4dffd800a558&lt;/id&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;title type=&quot;text&quot;&gt;topicb17fae35-c649-49f4-89b1-4dffd800a558&lt;/title&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;published&gt;2011-11-16T19:28:38Z&lt;/published&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;updated&gt;2011-11-16T19:28:38Z&lt;/updated&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;author&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;name&gt;&lt;service namespace&gt;&lt;/name&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/author&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;link rel=&quot;self&quot; href=&quot;../topicb17fae35-c649-49f4-89b1-4dffd800a558&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;content type=&quot;application/xml&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;TopicDescription xmlns=&quot;<a href="http://schemas.microsoft.com/netservices/2010/10/servicebus/connect">http://schemas.microsoft.com/netservices/2010/10/servicebus/connect</a>&quot; xmlns:i=&quot;<a href="http://www">http://www</a>.<br>
w3.org/2001/XMLSchema-instance&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DefaultMessageTimeToLive&gt;P10675199DT2H48M5.4775807S&lt;/DefaultMessageTimeToLive&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxSizeInMegabytes&gt;1024&lt;/MaxSizeInMegabytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresDuplicateDetection&gt;false&lt;/RequiresDuplicateDetection&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DuplicateDetectionHistoryTimeWindow&gt;PT10M&lt;/DuplicateDetectionHistoryTimeWindow&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;EnableBatchedOperations&gt;true&lt;/EnableBatchedOperations&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;SizeInBytes&gt;0&lt;/SizeInBytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/TopicDescription&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/content&gt;<br>
&nbsp; &lt;/entry&gt;<br>
&lt;/feed&gt;</p>
<p>&nbsp;</p>
<p>Getting resources from <a href="https://OGFNamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptio">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptio</a><br>
ns<br>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;<br>
&lt;feed xmlns=&quot;<a href="http://www.w3.org/2005/Atom">http://www.w3.org/2005/Atom</a>&quot;&gt;<br>
&nbsp; &lt;title type=&quot;text&quot;&gt;Subscriptions&lt;/title&gt;<br>
&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions&lt;/id&gt;<br>
&nbsp; &lt;updated&gt;2011-11-16T19:28:41Z&lt;/updated&gt;<br>
&nbsp; &lt;link rel=&quot;self&quot; href=&quot;<a href="https://ogfnamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscript">https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscript</a><br>
ions&quot; /&gt;<br>
&nbsp; &lt;entry xml:base=&quot;<a href="https://ogfnamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions">https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions</a>&quot;&gt;</p>
<p>&nbsp;&nbsp;&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions/Subscription<br>
42a592fa-1819-4620-a957-91cc1e649b64&lt;/id&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;title type=&quot;text&quot;&gt;Subscription42a592fa-1819-4620-a957-91cc1e649b64&lt;/title&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;published&gt;2011-11-16T19:28:40Z&lt;/published&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;updated&gt;2011-11-16T19:28:40Z&lt;/updated&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;link rel=&quot;self&quot; href=&quot;Subscriptions/Subscription42a592fa-1819-4620-a957-91cc1e649b64&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;content type=&quot;application/xml&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;SubscriptionDescription xmlns=&quot;<a href="http://schemas.microsoft.com/netservices/2010/10/servicebus/connect">http://schemas.microsoft.com/netservices/2010/10/servicebus/connect</a>&quot; xmlns:i=&quot;http<br>
://www.w3.org/2001/XMLSchema-instance&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;LockDuration&gt;PT1M&lt;/LockDuration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresSession&gt;false&lt;/RequiresSession&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DefaultMessageTimeToLive&gt;P10675199DT2H48M5.4775807S&lt;/DefaultMessageTimeToLive&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DeadLetteringOnMessageExpiration&gt;false&lt;/DeadLetteringOnMessageExpiration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DeadLetteringOnFilterEvaluationExceptions&gt;true&lt;/DeadLetteringOnFilterEvaluationExceptions&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MessageCount&gt;0&lt;/MessageCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxDeliveryCount&gt;10&lt;/MaxDeliveryCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;EnableBatchedOperations&gt;true&lt;/EnableBatchedOperations&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/SubscriptionDescription&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/content&gt;<br>
&nbsp; &lt;/entry&gt;<br>
&lt;/feed&gt;</p>
<p>&nbsp;</p>
<p>Getting resources from <a href="https://OGFNamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptio">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptio</a><br>
ns/Subscription42a592fa-1819-4620-a957-91cc1e649b64/Rules<br>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;<br>
&lt;feed xmlns=&quot;<a href="http://www.w3.org/2005/Atom">http://www.w3.org/2005/Atom</a>&quot;&gt;<br>
&nbsp; &lt;title type=&quot;text&quot;&gt;Rules&lt;/title&gt;<br>
&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions/Subscription42<br>
a592fa-1819-4620-a957-91cc1e649b64/Rules&lt;/id&gt;<br>
&nbsp; &lt;updated&gt;2011-11-16T19:28:41Z&lt;/updated&gt;<br>
&nbsp; &lt;link rel=&quot;self&quot; href=&quot;<a href="https://ogfnamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscript">https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscript</a><br>
ions/Subscription42a592fa-1819-4620-a957-91cc1e649b64/Rules&quot; /&gt;<br>
&nbsp; &lt;entry xml:base=&quot;<a href="https://ogfnamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions/S">https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions/S</a><br>
ubscription42a592fa-1819-4620-a957-91cc1e649b64/Rules&quot;&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558/Subscriptions/Subscription<br>
42a592fa-1819-4620-a957-91cc1e649b64/Rules/$Default&lt;/id&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;title type=&quot;text&quot;&gt;$Default&lt;/title&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;published&gt;2011-11-16T19:28:40Z&lt;/published&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;updated&gt;2011-11-16T19:28:40Z&lt;/updated&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;link rel=&quot;self&quot; href=&quot;Rules/$Default&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;content type=&quot;application/xml&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RuleDescription xmlns=&quot;<a href="http://schemas.microsoft.com/netservices/2010/10/servicebus/connect">http://schemas.microsoft.com/netservices/2010/10/servicebus/connect</a>&quot; xmlns:i=&quot;<a href="http://www.w">http://www.w</a><br>
3.org/2001/XMLSchema-instance&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Filter i:type=&quot;TrueFilter&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;SqlExpression&gt;1=1&lt;/SqlExpression&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;CompatibilityLevel&gt;20&lt;/CompatibilityLevel&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/Filter&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Action i:type=&quot;EmptyRuleAction&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/RuleDescription&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/content&gt;<br>
&nbsp; &lt;/entry&gt;<br>
&lt;/feed&gt;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>Deleting resource at <a href="https://OGFNamespace.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f">
https://&lt;service namespace&gt;.servicebus.windows.net/Queuee800a4c4-0d02-4695-8044-42324c56211f</a></p>
<p>Deleting resource at <a href="https://OGFNamespace.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558">
https://&lt;service namespace&gt;.servicebus.windows.net/Topicb17fae35-c649-49f4-89b1-4dffd800a558</a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>Getting resources from <a href="https://OGFNamespace.servicebus.windows.net/$Resources/Topics">
https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics</a><br>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;<br>
&lt;feed xmlns=&quot;<a href="http://www.w3.org/2005/Atom">http://www.w3.org/2005/Atom</a>&quot;&gt;<br>
&nbsp; &lt;title type=&quot;text&quot;&gt;Topics&lt;/title&gt;<br>
&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics&lt;/id&gt;<br>
&nbsp; &lt;updated&gt;2011-11-16T19:28:41Z&lt;/updated&gt;<br>
&nbsp; &lt;link rel=&quot;self&quot; href=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Topics">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Topics</a>&quot; /&gt;<br>
&lt;/feed&gt;</p>
<p>&nbsp;</p>
<p><br>
Getting resources from <a href="https://OGFNamespace.servicebus.windows.net/$Resources/Queues">
https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a><br>
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-16&quot;?&gt;<br>
&lt;feed xmlns=&quot;<a href="http://www.w3.org/2005/Atom">http://www.w3.org/2005/Atom</a>&quot;&gt;<br>
&nbsp; &lt;title type=&quot;text&quot;&gt;Queues&lt;/title&gt;<br>
&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues&lt;/id&gt;<br>
&nbsp; &lt;updated&gt;2011-11-16T19:28:41Z&lt;/updated&gt;<br>
&nbsp; &lt;link rel=&quot;self&quot; href=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Queues">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a>&quot; /&gt;<br>
&nbsp; &lt;entry xml:base=&quot;<a href="https://ogfnamespace.servicebus.windows.net/$Resources/Queues">https://&lt;service namespace&gt;.servicebus.windows.net/$Resources/Queues</a>&quot;&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;id&gt;https://&lt;service namespace&gt;.servicebus.windows.net/another&lt;/id&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;title type=&quot;text&quot;&gt;another&lt;/title&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;published&gt;2011-11-12T01:44:48Z&lt;/published&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;updated&gt;2011-11-12T01:44:48Z&lt;/updated&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;author&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;name&gt;&lt;service namespace&gt;&lt;/name&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/author&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;link rel=&quot;self&quot; href=&quot;../another&quot; /&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;content type=&quot;application/xml&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;QueueDescription xmlns=&quot;<a href="http://schemas.microsoft.com/netservices/2010/10/servicebus/connect">http://schemas.microsoft.com/netservices/2010/10/servicebus/connect</a>&quot; xmlns:i=&quot;<a href="http://www">http://www</a>.<br>
w3.org/2001/XMLSchema-instance&quot;&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;LockDuration&gt;PT1M&lt;/LockDuration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxSizeInMegabytes&gt;1024&lt;/MaxSizeInMegabytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresDuplicateDetection&gt;false&lt;/RequiresDuplicateDetection&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RequiresSession&gt;false&lt;/RequiresSession&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DefaultMessageTimeToLive&gt;P10675199DT2H48M5S&lt;/DefaultMessageTimeToLive&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DeadLetteringOnMessageExpiration&gt;false&lt;/DeadLetteringOnMessageExpiration&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;DuplicateDetectionHistoryTimeWindow&gt;PT10M&lt;/DuplicateDetectionHistoryTimeWindow&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MaxDeliveryCount&gt;10&lt;/MaxDeliveryCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;EnableBatchedOperations&gt;true&lt;/EnableBatchedOperations&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;SizeInBytes&gt;0&lt;/SizeInBytes&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MessageCount&gt;0&lt;/MessageCount&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/QueueDescription&gt;<br>
&nbsp;&nbsp;&nbsp; &lt;/content&gt;<br>
&nbsp; &lt;/entry&gt;<br>
&lt;/feed&gt;</p>
<p>Press ENTER to exit.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
</div>
<div>&nbsp;</div>
</div>
