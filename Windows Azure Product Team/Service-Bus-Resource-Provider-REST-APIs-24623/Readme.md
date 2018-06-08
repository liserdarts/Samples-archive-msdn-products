# Service Bus Resource Provider REST APIs
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* Service Bus
* Windows Azure Service Bus
## Topics
* Service Bus
* Windows Azure Service Bus
## IsPublished
* True
## ModifiedDate
* 2013-08-28 12:45:46
## Description

<h1>Introduction</h1>
<p>This sample demonstrates how to use the new Service Bus Resource Provider REST APIs to manage namespaces and messaging entities. You can use these APIs to create, update, or delete queues, topics, subscriptions, and notification hubs. You can read more about
 the Service Bus Resource Provider REST APIs <a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj856298.aspx" target="_blank">
here</a>.</p>
<p>Specifically, this sample demonstrates the following operations on queues, topics, subscriptions, and notification hubs:</p>
<ul>
<li>Create </li><li>Get </li><li>Delete </li></ul>
<h1><span>Sample Prerequisites</span></h1>
<p>The Service Bus Resource Provider REST APIs, like other Windows Azure Service Management APIs, use Windows Azure management certificate-based authentication. To access Service Bus messaging entities, you create a certificate and upload it to the portal.
 This enables Service Bus to trust applications that use that certificate. To do so, follow these steps:</p>
<ol>
<li>If you don&rsquo;t already have a Windows Azure subscription, you can create one on
<a href="http://www.windowsAzure.com">http://www.windowsazure.com</a>. </li><li>Using the <em>Makecert </em>tool, create a new certificate to upload to your Windows Azure account. For information about how to create and upload a certificate (including a sample Makecert.exe command line), see
<a href="http://msdn.microsoft.com/en-us/library/windowsazure/gg551722.aspx" target="_blank">
Create and Upload a Management Certificate for Windows Azure</a>. This sample uses this certificate as a trusted program to access Windows Azure services and perform the API calls. If this step fails, the sample will not work.<br>
<br>
Management certificates must have a key length of at least 2048 bits and should reside in the Personal certificate store. When the certificate is installed on the client, it should contain the private key of the certificate. To upload the certificate to the
 Windows Azure Management Portal, you must export it as a .cer format file that does not contain the private key.
</li><li>Upload your certificate to <a href="http://manage.windowsAzure.com">http://manage.windowsazure.com</a>.<br>
<br>
Note that the maximum number of certificates per subscription is 25. Make sure you do not exceed this number.
</li></ol>
<h1><span style="font-size:20px; font-weight:bold">Running the Sample</span></h1>
<ol>
<li>When you upload the certificate to your WindowsAzure.com subscription, copy the certificate thumbprint and subscription ID (GUID) values from the portal to the
<strong>azureCertificateThumbprint </strong>and <strong>SubscriptionID </strong>constants in Program.cs.
</li><li>Run the application.&nbsp; </li></ol>
<h1><span>Expected Output</span></h1>
<p>Hitting Windows Azure Management (RDFE) Endpoint: <a href="https://management.core.windows.net">
https://management.core.windows.net</a><br>
Creating Service Bus Namespace: &hellip;&hellip;<br>
Created Service Bus Namespace: &hellip;..<br>
Created Queue: IssueQueue<br>
Created Notification Hub<br>
Name: NotificationHub<br>
Created the Topic: CoolTopic<br>
Created Subscription SimpleSubscription in Topic CoolTopic<br>
Nameespace.GET<br>
Name: &hellip;..<br>
Region: South Central US<br>
ConnectionString: &hellip;&hellip; <br>
ServiceBusEndpoint: &hellip;&hellip;.<br>
Status: Active<br>
AcsManagementEndpoint: &hellip;&hellip;.<br>
Topic.Get&nbsp;<br>
Name: Cooltopic<br>
MaxSizeinMegabytes: 1024<br>
IsAnonymousAccessible: False<br>
Status: Active<br>
SubscriptionCount: 1<br>
ForwardTo:</p>
<p>Queue.GET:<br>
Name: IssueQueue<br>
MaxSizeinMegabytes: 1024<br>
IsAnonymousAccessible: False<br>
Status: Active<br>
MessageCount: 0<br>
MaxDeliveryCount: 1-<br>
ForwardTo:</p>
<p>Deleting namespace: &hellip;&hellip;..</p>
