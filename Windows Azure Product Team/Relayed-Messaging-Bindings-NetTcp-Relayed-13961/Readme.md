# Relayed Messaging Bindings: NetTcp Relayed
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
* 2011-11-16 11:46:45
## Description

<h1>Introduction</h1>
<p><span style="color:black">This sample demonstrates using the <strong>NetTcpRelayBinding</strong> binding.</span><br>
&nbsp;<br>
</p>
<h1>Prerequisites</h1>
<div>If you haven't already done so, read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<h1><br>
Service</h1>
<div>The service project defines a simple contract, (<strong>IPingContract</strong>):</div>
<div><br>
<br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract(SessionMode = SessionMode.Required, Name = &quot;IPingContract&quot;, Namespace = &quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;)]
public interface IPingContract
{
    [OperationContract(IsInitiating = true, IsTerminating = false)]
    void Open();
 
    [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = false)]
    void Ping(int count);
 
    [OperationContract(IsInitiating = false, IsTerminating = true)]
    void Close();
}
</pre>
<div class="preview">
<pre class="js">[ServiceContract(SessionMode&nbsp;=&nbsp;SessionMode.Required,&nbsp;Name&nbsp;=&nbsp;<span class="js__string">&quot;IPingContract&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="js__string">&quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;</span>)]&nbsp;
public&nbsp;interface&nbsp;IPingContract&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract(IsInitiating&nbsp;=&nbsp;true,&nbsp;IsTerminating&nbsp;=&nbsp;false)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">void</span>&nbsp;Open();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract(IsInitiating&nbsp;=&nbsp;false,&nbsp;IsOneWay&nbsp;=&nbsp;true,&nbsp;IsTerminating&nbsp;=&nbsp;false)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">void</span>&nbsp;Ping(int&nbsp;count);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract(IsInitiating&nbsp;=&nbsp;false,&nbsp;IsTerminating&nbsp;=&nbsp;true)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">void</span>&nbsp;Close();&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">The Open operation is used to initiate an interaction. Ping is a one-way operation that can be called an arbitrary number of times. The
<strong>Close </strong>operation terminates the sequence.</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">The service implements this contract in the <strong>
PingService </strong>class. The endpoints for this service are defined in the application configuration file. Specifically, the following endpoint is defined:</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;service name=&quot;Microsoft.ServiceBus.Samples.PingService&quot;&gt;
  &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IPingContract&quot;
            binding=&quot;netTcpRelayBinding&quot; /&gt;
&lt;/service&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;service</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;Microsoft.ServiceBus.Samples.PingService&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;<span class="xml__tag_start">&lt;endpoint</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">contract</span>=<span class="xml__attr_value">&quot;Microsoft.ServiceBus.Samples.IPingContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">binding</span>=<span class="xml__attr_value">&quot;netTcpRelayBinding&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/service&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">&nbsp;<br>
Client</h1>
<div class="endscriptcode"><span style="color:black">The client is configured (also in the application configuration file) with the following endpoint:</span></div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;client&gt;
  &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IPingContract&quot;
            binding=&quot;netTcpRelayBinding&quot; /&gt;
&lt;/client&gt;
</pre>
<div class="preview">
<pre class="js">&lt;client&gt;&nbsp;
&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IPingContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;/&gt;&nbsp;
&lt;/client&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"><span style="color:black; line-height:115%">In the code, an endpoint is opened.</span></div>
</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">Console.Write(&quot;Your Service Namespace: &quot;);
string serviceNamespace = Console.ReadLine();
Console.Write(&quot;Your Issuer Name: &quot;);
string issuerName = Console.ReadLine();
Console.Write(&quot;Your Issuer Secret: &quot;);
string issuerSecret = Console.ReadLine();

Uri serviceUri = ServiceBusEnvironment.CreateServiceUri(&quot;sb&quot;, serviceNamespace, &quot;PingService&quot;);

TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
sharedSecretServiceBusCredential.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

ChannelFactory&lt;IPingContract&gt; channelFactory = new ChannelFactory&lt;IPingContract&gt;(&quot;RelayEndpoint&quot;, new EndpointAddress(serviceUri));

channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

IPingContract channel = channelFactory.CreateChannel();
Console.WriteLine(&quot;Opening Channel.&quot;);
channel.Open();
</pre>
<div class="preview">
<pre class="js">Console.Write(<span class="js__string">&quot;Your&nbsp;Service&nbsp;Namespace:&nbsp;&quot;</span>);&nbsp;
string&nbsp;serviceNamespace&nbsp;=&nbsp;Console.ReadLine();&nbsp;
Console.Write(<span class="js__string">&quot;Your&nbsp;Issuer&nbsp;Name:&nbsp;&quot;</span>);&nbsp;
string&nbsp;issuerName&nbsp;=&nbsp;Console.ReadLine();&nbsp;
Console.Write(<span class="js__string">&quot;Your&nbsp;Issuer&nbsp;Secret:&nbsp;&quot;</span>);&nbsp;
string&nbsp;issuerSecret&nbsp;=&nbsp;Console.ReadLine();&nbsp;
&nbsp;
Uri&nbsp;serviceUri&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="js__string">&quot;sb&quot;</span>,&nbsp;serviceNamespace,&nbsp;<span class="js__string">&quot;PingService&quot;</span>);&nbsp;
&nbsp;
TransportClientEndpointBehavior&nbsp;sharedSecretServiceBusCredential&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;TransportClientEndpointBehavior();&nbsp;
sharedSecretServiceBusCredential.TokenProvider&nbsp;=&nbsp;TokenProvider.CreateSharedSecretTokenProvider(issuerName,&nbsp;issuerSecret);&nbsp;
&nbsp;
ChannelFactory&lt;IPingContract&gt;&nbsp;channelFactory&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ChannelFactory&lt;IPingContract&gt;(<span class="js__string">&quot;RelayEndpoint&quot;</span>,&nbsp;<span class="js__operator">new</span>&nbsp;EndpointAddress(serviceUri));&nbsp;
&nbsp;
channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);&nbsp;
&nbsp;
IPingContract&nbsp;channel&nbsp;=&nbsp;channelFactory.CreateChannel();&nbsp;
Console.WriteLine(<span class="js__string">&quot;Opening&nbsp;Channel.&quot;</span>);&nbsp;
channel.Open();&nbsp;
</pre>
</div>
</div>
</div>
</div>
<div>After the <code>ChannelFactory</code> is created, the client application creates a channel to the service and interacts with it. After the interaction is complete, the client closes both the channel and the
<code>ChannelFactory</code>, and then exits.</div>
<div></div>
<div></div>
<h1>Building and Running the Sample</h1>
<div>After building the solution, do the following to run the application:</div>
<ol>
<li>From a command prompt, run the service (Service\bin\Debug\Service.exe). You will be prompted for your service namespace, your issuer name and secret.
</li><li>From another command prompt, run the client (Client\bin\Debug\Client.exe). You will be prompted for your service namespace, your issuer name and secret.
</li><li>When finished, press ENTER to exit the client and the service. </li></ol>
<br>
<br>
<br>
<br>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"><br>
<br>
&nbsp;</div>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
</div>
