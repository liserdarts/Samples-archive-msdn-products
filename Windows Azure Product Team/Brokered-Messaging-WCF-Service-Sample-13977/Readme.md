# Brokered Messaging: WCF Service Sample
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
* 2011-11-16 11:49:55
## Description

<h1>Introduction</h1>
<div>&nbsp;</div>
<div>This sample demonstrates how to use the Windows Azure Service Bus using the WCF service model.</div>
<div><br>
The sample shows the use of the WCF service model to perform non-session communication via a Service Bus queue. The sample demonstrates this using a Ping service scenario. In this scenario, different senders send messages to the Ping service. All the messages
 are processed by the service. The service creates only one instance and processes all the messages in the same instance.</div>
<div>The sample prompts for service namespace credentials for the purpose of creating and deleting the queues. The credentials are used to authenticate with the Access Control service, and acquire an access token that proves to the Service Bus infrastructure
 that the client is authorized to create or delete the queue. The sender and service use the credentials defined in the config file.</div>
<div>&nbsp;</div>
<h1>Prerequisites</h1>
<div><br>
If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<div>&nbsp;</div>
<h1>Configuring the Sample</h1>
<div>When the solution is opened in Visual Studio, update the &lt;behaviors&gt; and &lt;client&gt; sections in the App.config file of the PingClient project. Also, update the &lt;behaviors&gt; and &lt;services&gt; sections in the App.config file of the PingService
 project.<br>
The value for &lsquo;issuerSecret&rsquo; should be available upon signup for a Windows Azure account and upon configuring your environment. Please read the release notes for details.</div>
<div><br>
The value for &lsquo;address&rsquo; in the &lt;client&gt; and &lt;services&gt; sections is a Service Bus Uri that points to the queue entity. The Uri should be of type sb://&lt;ServiceBus Namespace&gt;.servicebus.windows.net/PingQueue where the &lsquo;PingQueue&rsquo;
 is the entity name. Note that the Uri scheme &lsquo;sb&rsquo; is mandatory for all runtime operations such as send/receive.</div>
<div>&nbsp;</div>
<div><strong>PingClient App.Config</strong></div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;behaviors&gt;
  &lt;endpointBehaviors&gt;
    &lt;behavior name=&quot;securityBehavior&quot;&gt;
      &lt;transportClientEndpointBehavior&gt;
        &lt;tokenProvider&gt;
          &lt;sharedSecret issuerName=&quot;owner&quot; issuerSecret=&quot;[Issuer key]&quot; /&gt;
        &lt;/tokenProvider&gt;
      &lt;/transportClientEndpointBehavior&gt;
    &lt;/behavior&gt;
  &lt;/endpointBehaviors&gt;
&lt;/behaviors&gt;
&lt;client&gt;
  &lt;endpoint name=&quot;pingClient&quot;
            address=&quot;sb://[ServiceBus Namespace].servicebus.windows.net/PingQueue&quot;
            binding=&quot;netMessagingBinding&quot; bindingConfiguration=&quot;messagingBinding&quot;
            contract=&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;
            behaviorConfiguration=&quot;securityBehavior&quot;/&gt;
&lt;/client&gt;</pre>
<div class="preview">
<pre class="js">&lt;behaviors&gt;&nbsp;
&nbsp;&nbsp;&lt;endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;securityBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;tokenProvider&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;sharedSecret&nbsp;issuerName=<span class="js__string">&quot;owner&quot;</span>&nbsp;issuerSecret=<span class="js__string">&quot;[Issuer&nbsp;key]&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/tokenProvider&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&lt;/endpointBehaviors&gt;&nbsp;
&lt;/behaviors&gt;&nbsp;
&lt;client&gt;&nbsp;
&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;pingClient&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;sb://[ServiceBus&nbsp;Namespace].servicebus.windows.net/PingQueue&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netMessagingBinding&quot;</span>&nbsp;bindingConfiguration=<span class="js__string">&quot;messagingBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;securityBehavior&quot;</span>/&gt;&nbsp;
&lt;/client&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong>&nbsp;PingService App.Config</strong></div>
</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;behaviors&gt;
  &lt;endpointBehaviors&gt;
    &lt;behavior name=&quot;securityBehavior&quot;&gt;
      &lt;transportClientEndpointBehavior&gt;
        &lt;tokenProvider&gt;
          &lt;sharedSecret issuerName=&quot;owner&quot; issuerSecret=&quot;[Issuer key]&quot; /&gt;
        &lt;/tokenProvider&gt;
      &lt;/transportClientEndpointBehavior&gt;
    &lt;/behavior&gt;
  &lt;/endpointBehaviors&gt;
&lt;/behaviors&gt;
&lt;services&gt;
  &lt;service name=&quot;Microsoft.Samples.SessionMessages.PingService&quot;&gt;
    &lt;endpoint name=&quot;pingServiceEndPoint&quot;
              address=&quot;sb://[ServiceBus Namespace].servicebus.windows.net/PingQueue&quot;
              binding=&quot;netMessagingBinding&quot; bindingConfiguration=&quot;messagingBinding&quot;
              contract=&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;
              behaviorConfiguration=&quot;securityBehavior&quot; /&gt;
  &lt;/service&gt;
&lt;/services&gt;</pre>
<div class="preview">
<pre class="js">&lt;behaviors&gt;&nbsp;
&nbsp;&nbsp;&lt;endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;securityBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;tokenProvider&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;sharedSecret&nbsp;issuerName=<span class="js__string">&quot;owner&quot;</span>&nbsp;issuerSecret=<span class="js__string">&quot;[Issuer&nbsp;key]&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/tokenProvider&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&lt;/endpointBehaviors&gt;&nbsp;
&lt;/behaviors&gt;&nbsp;
&lt;services&gt;&nbsp;
&nbsp;&nbsp;&lt;service&nbsp;name=<span class="js__string">&quot;Microsoft.Samples.SessionMessages.PingService&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;pingServiceEndPoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;sb://[ServiceBus&nbsp;Namespace].servicebus.windows.net/PingQueue&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netMessagingBinding&quot;</span>&nbsp;bindingConfiguration=<span class="js__string">&quot;messagingBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;securityBehavior&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&lt;/service&gt;&nbsp;
&lt;/services&gt;</pre>
</div>
</div>
</div>
</div>
<h1>Configuration File</h1>
<div><br>
The sender and receiver use NetMessagingBinding, which is defined in the respective App.config files. NetMessagingBinding uses BinaryMessageEncoding as its encoder and NetMessagingTransportBindingElement as its transport. The TransportSettings property of the
 transport binding element represents the runtime factory used by Service Bus. An extension section is required be added to the config file in order to use Service Bus components with WCF.</div>
<div>&nbsp;</div>
<div>In addition to the binding, both the config files have a &lt;behaviors&gt; section, which defines TransportClientEndpointBehavior. Service Bus credentials are passed on to the client and service via this endpoint behavior.</div>
<div>&nbsp;</div>
<div><strong>App.Config - Config Extensions and Binding</strong></div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;extensions&gt;
  &lt;bindingElementExtensions&gt;
    &lt;add name=&quot;netMessagingTransport&quot; type=&quot;Microsoft.ServiceBus.Messaging.Configuration.NetMessagingTransportExtensionElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35&quot; /&gt;
  &lt;/bindingElementExtensions&gt;
  &lt;bindingExtensions&gt;
    &lt;add name=&quot;netMessagingBinding&quot; type=&quot;Microsoft.ServiceBus.Messaging.Configuration.NetMessagingBindingCollectionElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35&quot; /&gt;
  &lt;/bindingExtensions&gt;
&lt;/extensions&gt;
&lt;behaviors&gt;
  &lt;endpointBehaviors&gt;
    &lt;behavior name=&quot;securityBehavior&quot;&gt;
      &lt;transportClientEndpointBehavior&gt;
        &lt;tokenProvider&gt;
          &lt;sharedSecret issuerName=&quot;owner&quot; issuerSecret=&quot;[Issuer key]&quot; /&gt;
        &lt;/tokenProvider&gt;
      &lt;/transportClientEndpointBehavior&gt;
    &lt;/behavior&gt;
  &lt;/endpointBehaviors&gt;
&lt;/behaviors&gt;
&lt;bindings&gt;
  &lt;netMessagingBinding&gt;
    &lt;binding name=&quot;messagingBinding&quot; closeTimeout=&quot;00:03:00&quot; openTimeout=&quot;00:03:00&quot; receiveTimeout=&quot;00:03:00&quot; sendTimeout=&quot;00:03:00&quot; sessionIdleTimeout=&quot;00:01:00&quot; prefetchCount=&quot;-1&quot;&gt;
      &lt;transportSettings batchFlushInterval=&quot;00:00:01&quot;/&gt;
    &lt;/binding&gt;
  &lt;/netMessagingBinding&gt;
&lt;/bindings&gt;</pre>
<div class="preview">
<pre class="js">&lt;extensions&gt;&nbsp;
&nbsp;&nbsp;&lt;bindingElementExtensions&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;netMessagingTransport&quot;</span>&nbsp;type=<span class="js__string">&quot;Microsoft.ServiceBus.Messaging.Configuration.NetMessagingTransportExtensionElement,&nbsp;Microsoft.ServiceBus,&nbsp;Version=1.6.0.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=31bf3856ad364e35&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&lt;/bindingElementExtensions&gt;&nbsp;
&nbsp;&nbsp;&lt;bindingExtensions&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;netMessagingBinding&quot;</span>&nbsp;type=<span class="js__string">&quot;Microsoft.ServiceBus.Messaging.Configuration.NetMessagingBindingCollectionElement,&nbsp;Microsoft.ServiceBus,&nbsp;Version=1.6.0.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=31bf3856ad364e35&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&lt;/bindingExtensions&gt;&nbsp;
&lt;/extensions&gt;&nbsp;
&lt;behaviors&gt;&nbsp;
&nbsp;&nbsp;&lt;endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;securityBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;tokenProvider&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;sharedSecret&nbsp;issuerName=<span class="js__string">&quot;owner&quot;</span>&nbsp;issuerSecret=<span class="js__string">&quot;[Issuer&nbsp;key]&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/tokenProvider&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&lt;/endpointBehaviors&gt;&nbsp;
&lt;/behaviors&gt;&nbsp;
&lt;bindings&gt;&nbsp;
&nbsp;&nbsp;&lt;netMessagingBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;messagingBinding&quot;</span>&nbsp;closeTimeout=<span class="js__string">&quot;00:03:00&quot;</span>&nbsp;openTimeout=<span class="js__string">&quot;00:03:00&quot;</span>&nbsp;receiveTimeout=<span class="js__string">&quot;00:03:00&quot;</span>&nbsp;sendTimeout=<span class="js__string">&quot;00:03:00&quot;</span>&nbsp;sessionIdleTimeout=<span class="js__string">&quot;00:01:00&quot;</span>&nbsp;prefetchCount=<span class="js__string">&quot;-1&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;transportSettings&nbsp;batchFlushInterval=<span class="js__string">&quot;00:00:01&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&lt;/netMessagingBinding&gt;&nbsp;
&lt;/bindings&gt;</pre>
</div>
</div>
</div>
</div>
<div>In the configuration files, NetMessagingBinding has properties for SessionIdleTimeOut, PrefetchCount, and BatchFlushInterval. SessionIdleTimeOut property allows the ServiceHost dispatcher to close an instance of the service if it is idle for more than
 the specified time interval. The default value for SessionIdleTimeout is 1 minute. BatchFlushInterval is responsible for implicitly batching send operations or complete operations. The Service Bus implicitly batches the send operation from sender or complete
 operation from receiver for the specified time to avoid multiple round-trips. The default value of BatchFlushInterval is 20 milliseconds.</div>
<div>&nbsp;</div>
<div>The Pingclient configuration file defines the client object and the PingService configuration file defines the service object.</div>
<div>&nbsp;</div>
<div><strong>App.Config - Client and Service Definition</strong></div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;client&gt;
  &lt;endpoint name=&quot;pingClient&quot;
            address=&quot;[Enter Endpoint address]&quot;
            binding=&quot;netMessagingBinding&quot; bindingConfiguration=&quot;messagingBinding&quot;
            contract=&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;
            behaviorConfiguration=&quot;securityBehavior&quot;/&gt;
&lt;/client&gt;

&lt;services&gt;
  &lt;service name=&quot;Microsoft.Samples.SessionMessages.PingService&quot;&gt;
    &lt;endpoint name=&quot;pingServiceEndPoint&quot;
              address=&quot;[Enter Endpoint address]&quot;
              binding=&quot;netMessagingBinding&quot; bindingConfiguration=&quot;messagingBinding&quot;
              contract=&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;
              behaviorConfiguration=&quot;securityBehavior&quot; /&gt;
  &lt;/service&gt;
&lt;/services&gt;</pre>
<div class="preview">
<pre class="js">&lt;client&gt;&nbsp;
&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;pingClient&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;[Enter&nbsp;Endpoint&nbsp;address]&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netMessagingBinding&quot;</span>&nbsp;bindingConfiguration=<span class="js__string">&quot;messagingBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;securityBehavior&quot;</span>/&gt;&nbsp;
&lt;/client&gt;&nbsp;
&nbsp;
&lt;services&gt;&nbsp;
&nbsp;&nbsp;&lt;service&nbsp;name=<span class="js__string">&quot;Microsoft.Samples.SessionMessages.PingService&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;pingServiceEndPoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;[Enter&nbsp;Endpoint&nbsp;address]&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netMessagingBinding&quot;</span>&nbsp;bindingConfiguration=<span class="js__string">&quot;messagingBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.Samples.SessionMessages.IPingServiceContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;securityBehavior&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&lt;/service&gt;&nbsp;
&lt;/services&gt;</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">Credentials</h1>
<div>The sample obtains the user credentials and creates a Service Bus NamespaceManager object. This entity holds the credentials and is used for messaging management operations - in this case, to create and delete queues.</div>
</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public static void GetUserCredentials()
    {
        // User namespace
        Console.WriteLine(&quot;Please provide the namespace to use:&quot;);
        serviceBusNamespace = Console.ReadLine();

        // Issuer name
        Console.WriteLine(&quot;Please provide the Issuer name to use:&quot;);
        serviceBusIssuerName = Console.ReadLine();

        // Issuer key
        Console.WriteLine(&quot;Please provide the Issuer key to use:&quot;);
        serviceBusIssuerKey = Console.ReadLine();
    }

    // Create the NamespaceManager for management operations (queue)
    static void CreateNamespaceManager()
    {
        // Create TokenProvider for access control service
        TokenProvider credentials = TokenProvider.CreateSharedSecretTokenProvider(ServiceBusIssuerName, ServiceBusIssuerKey);

        // Create the management Uri
        Uri managementUri = ServiceBusEnvironment.CreateServiceUri(&quot;sb&quot;, ServiceBusNamespace, string.Empty);
        namespaceClient = new NamespaceManager(managementUri, credentials);
    }

    // Create the entity (queue)
    static QueueDescription CreateQueue(bool session)
    {
        QueueDescription queueDescription = new QueueDescription(PingQueueName) { RequiresSession = session };

        // Try deleting the queue before creation. Ignore exception if queue does not exist.
        try
        {
            namespaceClient.DeleteQueue(queueDescription.Path);
        }
        catch (MessagingEntityNotFoundException)
        {
        }

        return namespaceClient.CreateQueue(queueDescription);
    }  </pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;GetUserCredentials()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;User&nbsp;namespace</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Please&nbsp;provide&nbsp;the&nbsp;namespace&nbsp;to&nbsp;use:&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceBusNamespace&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Issuer&nbsp;name</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Please&nbsp;provide&nbsp;the&nbsp;Issuer&nbsp;name&nbsp;to&nbsp;use:&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceBusIssuerName&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Issuer&nbsp;key</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Please&nbsp;provide&nbsp;the&nbsp;Issuer&nbsp;key&nbsp;to&nbsp;use:&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceBusIssuerKey&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Create&nbsp;the&nbsp;NamespaceManager&nbsp;for&nbsp;management&nbsp;operations&nbsp;(queue)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;CreateNamespaceManager()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Create&nbsp;TokenProvider&nbsp;for&nbsp;access&nbsp;control&nbsp;service</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TokenProvider&nbsp;credentials&nbsp;=&nbsp;TokenProvider.CreateSharedSecretTokenProvider(ServiceBusIssuerName,&nbsp;ServiceBusIssuerKey);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Create&nbsp;the&nbsp;management&nbsp;Uri</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Uri&nbsp;managementUri&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="js__string">&quot;sb&quot;</span>,&nbsp;ServiceBusNamespace,&nbsp;string.Empty);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;namespaceClient&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;NamespaceManager(managementUri,&nbsp;credentials);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Create&nbsp;the&nbsp;entity&nbsp;(queue)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;QueueDescription&nbsp;CreateQueue(bool&nbsp;session)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;QueueDescription&nbsp;queueDescription&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;QueueDescription(PingQueueName)&nbsp;<span class="js__brace">{</span>&nbsp;RequiresSession&nbsp;=&nbsp;session&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Try&nbsp;deleting&nbsp;the&nbsp;queue&nbsp;before&nbsp;creation.&nbsp;Ignore&nbsp;exception&nbsp;if&nbsp;queue&nbsp;does&nbsp;not&nbsp;exist.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;namespaceClient.DeleteQueue(queueDescription.Path);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">catch</span>&nbsp;(MessagingEntityNotFoundException)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;namespaceClient.CreateQueue(queueDescription);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">The preceding code prompts for the issuer credential and then constructs the listening URI using that information. The static ServiceBusEnvironment.CreateServiceUri function is provided to help construct the URI with the correct
 format and domain name. It is strongly recommended that you use this function instead of building the URI from scratch because the URI construction logic and format might change in future releases. At present, the resulting URI is scheme://&lt;service-namespace&gt;.servicebus.windows.net/.</div>
<div>The CreateNamespaceManager() function creates the object to perform management operations, in this case to create and delete queues. Both &lsquo;https&rsquo; and &lsquo;sb&rsquo; Uri schemes are allowed as a part of service Uri.</div>
<div>The CreateQueue(bool session) function creates a queue with the RequireSession property set as per the argument passed.</div>
<div>&nbsp;</div>
<h1>Data Contract</h1>
<div><br>
The sample uses an PingData data contract to communicate between client and service. This data contract has two data members of type string.</div>
</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    [DataContract(Name = &quot;PingDataContract&quot;, Namespace = &quot;Microsoft.Samples.SessionMessages&quot;)]
    public class PingData
    {
        [DataMember]
        public string Message;

        [DataMember]
        public string SenderId;

        public PingData()
            : this(string.Empty, string.Empty)
        {
        }

        public PingData(string message, string senderId)
        {
            this.Message = message;
            this.SenderId = senderId;
        }
    }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;[DataContract(Name&nbsp;=&nbsp;<span class="cs__string">&quot;PingDataContract&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="cs__string">&quot;Microsoft.Samples.SessionMessages&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;PingData&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Message;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;SenderId;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;PingData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">this</span>(<span class="cs__keyword">string</span>.Empty,&nbsp;<span class="cs__keyword">string</span>.Empty)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;PingData(<span class="cs__keyword">string</span>&nbsp;message,&nbsp;<span class="cs__keyword">string</span>&nbsp;senderId)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Message&nbsp;=&nbsp;message;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.SenderId&nbsp;=&nbsp;senderId;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<h1>Sender</h1>
<div><br>
Service Bus supports IOutputChannel for sending messages using NetMessagingBinding. In the sample, the clients create a random message using the RandomString() function and then send the message to the service. The PingClient is defined in its app.config file.</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    static void Main(string[] args)
    {
        ParseArgs(args);

        // Send messages to queue which does not require session
        Console.Title = &quot;Ping Client&quot;;

        // Create sender to Order Service
        ChannelFactory&lt;IPingServiceContract&gt; factory = new ChannelFactory&lt;IPingServiceContract&gt;(SampleManager.PingClientConfigName);
        IPingServiceContract clientChannel = factory.CreateChannel();
        ((IChannel)clientChannel).Open();

        // Send messages
        numberOfMessages = random.Next(10, 30);
        Console.WriteLine(&quot;[Client{0}] Sending {1} messages to {2}...&quot;, senderId, numberOfMessages, SampleManager.PingQueueName);
        SendMessages(clientChannel);

        // Close sender
        ((IChannel)clientChannel).Close();
        factory.Close();

        Console.WriteLine(&quot;\nSender complete.&quot;);
        Console.WriteLine(&quot;\nPress [Enter] to exit.&quot;);
        Console.ReadLine();
    }

    static void SendMessages(IPingServiceContract clientChannel)
    {
        // Send messages to queue which requires session:
        for (int i = 0; i &lt; numberOfMessages; i&#43;&#43;)
        {
            // Send message 
            PingData message = CreatePingData();
            clientChannel.Ping(message);
            SampleManager.OutputMessageInfo(&quot;Send&quot;, message);
            Thread.Sleep(200);
        }
    }

    static PingData CreatePingData()
    {
        // Generating a random message
        return new PingData(RandomString(), senderId);
    }

    // Creates a random string
    static string RandomString()
    {
        StringBuilder builder = new StringBuilder();
        int size = random.Next(5, 15);
        char ch;
        for (int i = 0; i &lt; size; i&#43;&#43;)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() &#43; 65)));
            builder.Append(ch);
        }

        return builder.ToString();
    }</pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;Main(string[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ParseArgs(args);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Send&nbsp;messages&nbsp;to&nbsp;queue&nbsp;which&nbsp;does&nbsp;not&nbsp;require&nbsp;session</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Title&nbsp;=&nbsp;<span class="js__string">&quot;Ping&nbsp;Client&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Create&nbsp;sender&nbsp;to&nbsp;Order&nbsp;Service</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ChannelFactory&lt;IPingServiceContract&gt;&nbsp;factory&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ChannelFactory&lt;IPingServiceContract&gt;(SampleManager.PingClientConfigName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IPingServiceContract&nbsp;clientChannel&nbsp;=&nbsp;factory.CreateChannel();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((IChannel)clientChannel).Open();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Send&nbsp;messages</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;numberOfMessages&nbsp;=&nbsp;random.Next(<span class="js__num">10</span>,&nbsp;<span class="js__num">30</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;[Client{0}]&nbsp;Sending&nbsp;{1}&nbsp;messages&nbsp;to&nbsp;{2}...&quot;</span>,&nbsp;senderId,&nbsp;numberOfMessages,&nbsp;SampleManager.PingQueueName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SendMessages(clientChannel);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Close&nbsp;sender</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((IChannel)clientChannel).Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;factory.Close();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;\nSender&nbsp;complete.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;\nPress&nbsp;[Enter]&nbsp;to&nbsp;exit.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.ReadLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;SendMessages(IPingServiceContract&nbsp;clientChannel)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;Send&nbsp;messages&nbsp;to&nbsp;queue&nbsp;which&nbsp;requires&nbsp;session:</span><span class="js__statement">for</span>&nbsp;(int&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;numberOfMessages;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;Send&nbsp;message&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PingData&nbsp;message&nbsp;=&nbsp;CreatePingData();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;clientChannel.Ping(message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SampleManager.OutputMessageInfo(<span class="js__string">&quot;Send&quot;</span>,&nbsp;message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread.Sleep(<span class="js__num">200</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;PingData&nbsp;CreatePingData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;Generating&nbsp;a&nbsp;random&nbsp;message</span><span class="js__statement">return</span><span class="js__operator">new</span>&nbsp;PingData(RandomString(),&nbsp;senderId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__sl_comment">//&nbsp;Creates&nbsp;a&nbsp;random&nbsp;string</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;string&nbsp;RandomString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StringBuilder&nbsp;builder&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;StringBuilder();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;int&nbsp;size&nbsp;=&nbsp;random.Next(<span class="js__num">5</span>,&nbsp;<span class="js__num">15</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;char&nbsp;ch;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(int&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;size;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ch&nbsp;=&nbsp;Convert.ToChar(Convert.ToInt32(<span class="js__object">Math</span>.Floor(<span class="js__num">26</span>&nbsp;*&nbsp;random.NextDouble()&nbsp;&#43;&nbsp;<span class="js__num">65</span>)));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;builder.Append(ch);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">return</span>&nbsp;builder.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
</div>
<div>&nbsp;</div>
<h1>Service</h1>
<div><br>
The sample illustrates a Ping service as described above. The Ping service implements IPingServiceContract service contract. The operation attribute ReceiveContextEnabled is set with manual control set to true. This requires an explicit ReceiveContext.Complete
 operation to be performed for every message received. The service has behavior InstanceContextMode set to single. The service will only create one instance to process all available messages in the queue.</div>
<div>Note that NetMessagingBinding only supports one-way communication. Therefore, OperationContract must explicitly set the attribute IsOneWay to true. The service is defined in its App.config file.</div>
<div>&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    [ServiceContract]
    public interface IPingServiceContract
    {
        [OperationContract(IsOneWay = true)]
        [ReceiveContextEnabled(ManualControl = true)]
        void Ping(PingData pingData);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class PingService : IPingServiceContract
    {
        [OperationBehavior]
        public void Ping(PingData pingData)
        {
            // Get the message properties
            var incomingProperties = OperationContext.Current.IncomingMessageProperties;
            BrokeredMessageProperty property = (BrokeredMessageProperty)incomingProperties[BrokeredMessageProperty.Name];

            // Print message
            SampleManager.OutputMessageInfo(&quot;Receive&quot;, pingData);

            //Complete the Message
            ReceiveContext receiveContext;
            if (ReceiveContext.TryGet(incomingProperties, out receiveContext))
            {
                receiveContext.Complete(TimeSpan.FromSeconds(10.0d));
            }
            else
            {
                throw new InvalidOperationException(&quot;Receiver is in peek lock mode but receive context is not available!&quot;);
            }
        }
    }</pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;[ServiceContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;interface&nbsp;IPingServiceContract&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract(IsOneWay&nbsp;=&nbsp;true)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[ReceiveContextEnabled(ManualControl&nbsp;=&nbsp;true)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">void</span>&nbsp;Ping(PingData&nbsp;pingData);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ServiceBehavior(InstanceContextMode&nbsp;=&nbsp;InstanceContextMode.Single,&nbsp;ConcurrencyMode&nbsp;=&nbsp;ConcurrencyMode.Single)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;class&nbsp;PingService&nbsp;:&nbsp;IPingServiceContract&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationBehavior]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;Ping(PingData&nbsp;pingData)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Get&nbsp;the&nbsp;message&nbsp;properties</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;incomingProperties&nbsp;=&nbsp;OperationContext.Current.IncomingMessageProperties;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BrokeredMessageProperty&nbsp;property&nbsp;=&nbsp;(BrokeredMessageProperty)incomingProperties[BrokeredMessageProperty.Name];&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Print&nbsp;message</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SampleManager.OutputMessageInfo(<span class="js__string">&quot;Receive&quot;</span>,&nbsp;pingData);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//Complete&nbsp;the&nbsp;Message</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ReceiveContext&nbsp;receiveContext;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(ReceiveContext.TryGet(incomingProperties,&nbsp;out&nbsp;receiveContext))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;receiveContext.Complete(TimeSpan.FromSeconds(<span class="js__num">10</span>.0d));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">throw</span>&nbsp;<span class="js__operator">new</span>&nbsp;InvalidOperationException(<span class="js__string">&quot;Receiver&nbsp;is&nbsp;in&nbsp;peek&nbsp;lock&nbsp;mode&nbsp;but&nbsp;receive&nbsp;context&nbsp;is&nbsp;not&nbsp;available!&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
The service application subscribes to the faulted event. This will notify the service if any fault occurred during execution and can be handled properly. In the sample, the service is aborted on fault.</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    static void Main(string[] args)
    {
        Console.Title = &quot;Ping Service&quot;;
        Console.WriteLine(&quot;Ready to receive messages from {0}...&quot;, SampleManager.PingQueueName);

        // Creating the service host object as defined in config
        ServiceHost serviceHost = new ServiceHost(typeof(PingService));

        // Add ErrorServiceBehavior for handling errors encounter by servicehost during execution.
        serviceHost.Description.Behaviors.Add(new ErrorServiceBehavior());

        // Subscribe to the faulted event.
        serviceHost.Faulted &#43;= new EventHandler(serviceHost_Faulted);

        // Start service
        serviceHost.Open();

        Console.WriteLine(&quot;\nPress [Enter] to Close the ServiceHost.&quot;);
        Console.ReadLine();

        // Close the service
        serviceHost.Close();
    }

    static void serviceHost_Faulted(object sender, EventArgs e)
    {
        Console.WriteLine(&quot;Fault occured. Aborting the service host object ...&quot;);
        ((ServiceHost)sender).Abort();
    } </pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;Main(string[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Title&nbsp;=&nbsp;<span class="js__string">&quot;Ping&nbsp;Service&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Ready&nbsp;to&nbsp;receive&nbsp;messages&nbsp;from&nbsp;{0}...&quot;</span>,&nbsp;SampleManager.PingQueueName);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Creating&nbsp;the&nbsp;service&nbsp;host&nbsp;object&nbsp;as&nbsp;defined&nbsp;in&nbsp;config</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ServiceHost&nbsp;serviceHost&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ServiceHost(<span class="js__operator">typeof</span>(PingService));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;ErrorServiceBehavior&nbsp;for&nbsp;handling&nbsp;errors&nbsp;encounter&nbsp;by&nbsp;servicehost&nbsp;during&nbsp;execution.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceHost.Description.Behaviors.Add(<span class="js__operator">new</span>&nbsp;ErrorServiceBehavior());&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Subscribe&nbsp;to&nbsp;the&nbsp;faulted&nbsp;event.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceHost.Faulted&nbsp;&#43;=&nbsp;<span class="js__operator">new</span>&nbsp;EventHandler(serviceHost_Faulted);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Start&nbsp;service</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceHost.Open();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;\nPress&nbsp;[Enter]&nbsp;to&nbsp;Close&nbsp;the&nbsp;ServiceHost.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Close&nbsp;the&nbsp;service</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceHost.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;serviceHost_Faulted(object&nbsp;sender,&nbsp;EventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Fault&nbsp;occured.&nbsp;Aborting&nbsp;the&nbsp;service&nbsp;host&nbsp;object&nbsp;...&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((ServiceHost)sender).Abort();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
The service also implements an ErrorServiceBehavior for unhandled exceptions during service execution. ErrorServiceBehavior is a service behavior which adds an IErrorHandler object to the dispatcher. This object simply prints out all the exceptions except CommunicationException.</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            if (!error.GetType().Equals(typeof(CommunicationException)))
            {
                // Handle the exception as required by the application
                Console.WriteLine(&quot;Service encountered an exception.&quot;);
                Console.WriteLine(error.ToString());
            }

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }
    }

    public class ErrorServiceBehavior : IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection&lt;ServiceEndpoint&gt; endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(new ErrorHandler());
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }</pre>
<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;class&nbsp;ErrorHandler&nbsp;:&nbsp;IErrorHandler&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;bool&nbsp;HandleError(Exception&nbsp;error)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__statement">if</span>&nbsp;(!error.GetType().Equals(<span class="js__operator">typeof</span>(CommunicationException)))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__sl_comment">//&nbsp;Handle&nbsp;the&nbsp;exception&nbsp;as&nbsp;required&nbsp;by&nbsp;the&nbsp;application</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Service&nbsp;encountered&nbsp;an&nbsp;exception.&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(error.ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;ProvideFault(Exception&nbsp;error,&nbsp;MessageVersion&nbsp;version,&nbsp;ref&nbsp;Message&nbsp;fault)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;class&nbsp;ErrorServiceBehavior&nbsp;:&nbsp;IServiceBehavior&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;AddBindingParameters(ServiceDescription&nbsp;serviceDescription,&nbsp;ServiceHostBase&nbsp;serviceHostBase,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Collection&lt;ServiceEndpoint&gt;&nbsp;endpoints,&nbsp;BindingParameterCollection&nbsp;bindingParameters)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;ApplyDispatchBehavior(ServiceDescription&nbsp;serviceDescription,&nbsp;ServiceHostBase&nbsp;serviceHostBase)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;foreach&nbsp;(ChannelDispatcher&nbsp;dispatcher&nbsp;<span class="js__operator">in</span>&nbsp;serviceHostBase.ChannelDispatchers)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dispatcher.ErrorHandlers.Add(<span class="js__operator">new</span>&nbsp;ErrorHandler());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;Validate(ServiceDescription&nbsp;serviceDescription,&nbsp;ServiceHostBase&nbsp;serviceHostBase)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span><span class="js__brace">}</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
</div>
<h1>Running the Sample</h1>
<div>&nbsp;</div>
<div><br>
To run the sample, build the solution in Visual Studio or from the command line, then run the executable &lsquo;SampleManager.exe&rsquo;. The program prompts for your Service Bus namespace and the issuer credentials. For the issuer secret, be sure to use the
 Default Issuer Key value (typically &quot;owner&quot;) from the Azure portal, rather than one of the management keys.</div>
<div>&nbsp;</div>
<h2>Expected Output - Sample Manager</h2>
<div>&nbsp;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Please provide the namespace to use: &lt;Service Namespace&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Please provide the Issuer name to use: &lt;Issuer Name&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Please provide the Issuer key to use: &lt;Issuer Key&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Creating Queues...<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Created PingQueue, Queue.RequiresSession = False&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Launching senders and receivers...&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Press [Enter] to exit.</div>
<div>&nbsp;</div>
<h2>Expected Output &ndash; Ping Client</h2>
<div>&nbsp;</div>
<div><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Sending 13 messages to PingQueue...<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [FHZRADKBZWL] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [AMBALBZMY] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [OTAKPRFHOSHRH] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [IZBDPXUAXXJN] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [EMDFSRISFRP] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [TWRHTEIFGR] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [AVXBCOVCA] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [ZAVKM] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [AYBDHLPVAC] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [ETAHLNADJVPF] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [KPOMTW] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [XGPIHFNEOGBAA] - Group 0.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Send: Message [QJAUOMUHDTLTX] - Group 0.</div>
<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Sender complete.</div>
<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Press [Enter] to exit.</div>
<div>&nbsp;</div>
<h2>Expected Output - Ping Service</h2>
<div>&nbsp;</div>
<div><br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Ready to receive messages from PingQueue...</div>
<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Press [Enter] to exit.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[FHZRADKBZWL]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[AMBALBZMY]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[OTAKPRFHOSHRH]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[IZBDPXUAXXJN]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[EMDFSRISFRP]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[TWRHTEIFGR]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[AVXBCOVCA]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[ZAVKM]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[AYBDHLPVAC]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[ETAHLNADJVPF]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[KPOMTW]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[XGPIHFNEOGBAA]<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 Receive: Message[QJAUOMUHDTLTX]</div>
<div>&nbsp;</div>
</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
