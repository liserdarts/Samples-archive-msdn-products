# Relayed Messaging: Load Balance
## Requires
* Visual Studio 2013
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
* 2014-05-08 07:49:59
## Description

<h1>Introduction</h1>
<div>This sample demonstrates how to use the Windows Azure Service Bus to route messages to multiple receivers.&nbsp; It shows multiple instances of a simple service communicating with a client via the NetTcpRelayBinding binding. When each instance of the service
 application is started, it prompts for your credentials and opens a unique endpoint on the Service Bus. Once opened, this endpoint has a well-known URI on the Service Bus and is reachable from anywhere, even if your computer resides behind a firewall or Network
 Address Translation (NAT).</div>
<div>&nbsp;</div>
<div>Clients accessing an endpoint must have permission to communicate with that endpoint. Therefore, the client application also prompts for your credentials, authenticates with the Service Bus Access Control (AC) service, and acquires an access token that
 proves to the Service Bus infrastructure that the client is authorized to access the endpoint. Once the client is connected, you can type messages into the client application which will be echoed back by any one of the running instances of the service.</div>
<div>&nbsp;</div>
<h1>Prerequisites</h1>
<div><br>
If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<div>&nbsp;</div>
<h1>LoadBalance Service</h1>
<div><br>
The service is similar to the Echo Sample and implements a simple contract with a single operation named Echo. Every running instance of the service accepts a string and echoes the string back.</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceBehavior(Name = &quot;EchoService&quot;, Namespace = &quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;)]
 class EchoService : IEchoContract
 {
    public string Echo(string text)
    {
       Console.WriteLine(&quot;Echoing: {0}&quot;, text);
       return text; 
   }
 }</pre>
<div class="preview">
<pre class="js">[ServiceBehavior(Name&nbsp;=&nbsp;<span class="js__string">&quot;EchoService&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="js__string">&quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;</span>)]&nbsp;
&nbsp;class&nbsp;EchoService&nbsp;:&nbsp;IEchoContract&nbsp;
&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;string&nbsp;Echo(string&nbsp;text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Echoing:&nbsp;{0}&quot;</span>,&nbsp;text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;text;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h1 class="endscriptcode">LoadBalance Client</h1>
</div>
<div>When started, the client asks for the service namespace, creates a channel to the logical address of the router, and sends requests. Instead of the simple ChannelFactory used in the Echo sample, the LoadBalance sample uses a
<strong>BalancingChannelFactory</strong>, which facilitates load balancing across the listener instances to which the router routes the client messages. Once the interaction is complete, the client closes the channel and exits.</div>
<div>&nbsp;<br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">{
        Console.Write(&quot;Your Service Namespace: &quot;);
        string serviceNamespace = Console.ReadLine();
        Console.Write(&quot;Your Issuer Name: &quot;);
        string issuerName = Console.ReadLine();
        Console.Write(&quot;Your Issuer Secret: &quot;);
        string issuerSecret = Console.ReadLine();

        // create the service URI based on the service namespace
        Uri serviceUri = ServiceBusEnvironment.CreateServiceUri(&quot;sb&quot;, issuerName, &quot;EchoService&quot;);

        // create the credentials object for the endpoint
        TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
        sharedSecretServiceBusCredential.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

        // create the channel factory loading the configuration
        BalancingChannelFactory&lt;IEchoChannel&gt; channelFactory =
           new BalancingChannelFactory&lt;IEchoChannel&gt;(
            new NetTcpRelayBinding(EndToEndSecurityMode.None, RelayClientAuthenticationType.RelayAccessToken),
           new EndpointAddress(serviceUri));

        // apply the Service Bus credentials
        channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

        Console.WriteLine(&quot;Enter text to echo (or [Enter] to exit):&quot;);

        string input = Console.ReadLine();

        while (input != String.Empty)
        {
            IEchoChannel channel = channelFactory.CreateChannel();
            channel.Open();

            try
            {
                // create and open the client channel
                Console.WriteLine(&quot;Server echoed: {0}&quot;, channel.Echo(input));
                channel.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(&quot;Error: &quot; &#43; e.Message);
                channel.Abort();
            }

            input = Console.ReadLine();
        }

        channelFactory.Close();

    }</pre>
<div class="preview">
<pre class="js"><span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Write(<span class="js__string">&quot;Your&nbsp;Service&nbsp;Namespace:&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;serviceNamespace&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Write(<span class="js__string">&quot;Your&nbsp;Issuer&nbsp;Name:&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;issuerName&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Write(<span class="js__string">&quot;Your&nbsp;Issuer&nbsp;Secret:&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;issuerSecret&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;create&nbsp;the&nbsp;service&nbsp;URI&nbsp;based&nbsp;on&nbsp;the&nbsp;service&nbsp;namespace</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Uri&nbsp;serviceUri&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="js__string">&quot;sb&quot;</span>,&nbsp;issuerName,&nbsp;<span class="js__string">&quot;EchoService&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;create&nbsp;the&nbsp;credentials&nbsp;object&nbsp;for&nbsp;the&nbsp;endpoint</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransportClientEndpointBehavior&nbsp;sharedSecretServiceBusCredential&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;TransportClientEndpointBehavior();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sharedSecretServiceBusCredential.TokenProvider&nbsp;=&nbsp;TokenProvider.CreateSharedSecretTokenProvider(issuerName,&nbsp;issuerSecret);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;create&nbsp;the&nbsp;channel&nbsp;factory&nbsp;loading&nbsp;the&nbsp;configuration</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BalancingChannelFactory&lt;IEchoChannel&gt;&nbsp;channelFactory&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">new</span>&nbsp;BalancingChannelFactory&lt;IEchoChannel&gt;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">new</span>&nbsp;NetTcpRelayBinding(EndToEndSecurityMode.None,&nbsp;RelayClientAuthenticationType.RelayAccessToken),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">new</span>&nbsp;EndpointAddress(serviceUri));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;apply&nbsp;the&nbsp;Service&nbsp;Bus&nbsp;credentials</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Enter&nbsp;text&nbsp;to&nbsp;echo&nbsp;(or&nbsp;[Enter]&nbsp;to&nbsp;exit):&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;input&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">while</span>&nbsp;(input&nbsp;!=&nbsp;<span class="js__object">String</span>.Empty)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IEchoChannel&nbsp;channel&nbsp;=&nbsp;channelFactory.CreateChannel();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;channel.Open();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;create&nbsp;and&nbsp;open&nbsp;the&nbsp;client&nbsp;channel</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Server&nbsp;echoed:&nbsp;{0}&quot;</span>,&nbsp;channel.Echo(input));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;channel.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">catch</span>&nbsp;(Exception&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Error:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;e.Message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;channel.Abort();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;input&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;channelFactory.Close();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<h1>Running the Sample</h1>
<div><br>
&nbsp;To run the sample, build the solution in Visual Studio or from the command line. Run several instances of the service application first, then run one instance of the client.</div>
<div>When the service and the client are running, you can start typing messages into the client application. These messages are received by the service application and echoed to each instance in a round-robin fashion.</div>
<div>&nbsp;</div>
<h2>Expected Output - Service</h2>
<div>&nbsp;</div>
<div>Your Service Namespace: &lt;service-namespace&gt;<br>
Your Issuer Name: &lt;issuer-name&gt;<br>
Your Issuer Secret: &lt;issuer-secret&gt;<br>
Service address: sb://&lt;service-namespace&gt;.servicebus.windows.net/EchoService/<br>
Listen address: sb://&lt;service-namespace&gt;.servicebus.windows.net/EchoService/<br>
Press [Enter] to exit<br>
Echoing: Hello, World!</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<h2>Expected Output - Client</h2>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>Your Service Namespace: &lt;service-namespace&gt;<br>
Your Issuer Name: &lt;issuer-name&gt;<br>
Your Issuer Secret: &lt;issuer-secret&gt;<br>
Enter text to echo (or [Enter] to exit): Hello, World!<br>
Server echoed: Hello, World!</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
