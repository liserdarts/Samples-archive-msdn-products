# Relayed Messaging Bindings: NetTcp Direct
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
* 2011-11-15 02:38:22
## Description

<h1>Introduction</h1>
<div>This sample demonstrates how to configure the <strong>NetTcpRelayBinding</strong> binding to support the Hybrid connection mode which first establishes a relayed connection, and if possible, switches automatically to a direct connection between a client
 and a service.</div>
<div>&nbsp;</div>
<h1>Prerequisites</h1>
<div>If you haven't already done so, read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<div>&nbsp;</div>
<h1>Service</h1>
<div>In general, there are four steps to building a Service Bus service:</div>
<ol>
<li>Define a contract. </li><li>Implement the contract in a service. </li><li>Define endpoints for the service. </li><li>Host the service. </li></ol>
<div>In this sample, the service project defines <code>HelloService</code> and a simple contract named
<code>IHelloContract</code>:<br>
<br>
<span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract(Name = &quot;IHelloContract&quot;, Namespace = &quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;)]
public interface IHelloContract
{
    [OperationContract(IsOneWay = true)]
    void Hello(string text);
}
</pre>
<div class="preview">
<pre class="js">[ServiceContract(Name&nbsp;=&nbsp;<span class="js__string">&quot;IHelloContract&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="js__string">&quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;</span>)]&nbsp;
public&nbsp;interface&nbsp;IHelloContract&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract(IsOneWay&nbsp;=&nbsp;true)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">void</span>&nbsp;Hello(string&nbsp;text);&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">The service implements this contract in the <strong>
HelloService </strong>class.</div>
<div class="endscriptcode">The endpoints for this service are defined in the application configuration file. Specifically, the following endpoint is defined:&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;service name=&quot;Microsoft.ServiceBus.Samples.HelloService&quot;&gt;
  &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IHelloContract&quot;
            binding=&quot;netTcpRelayBinding&quot;
            bindingConfiguration=&quot;default&quot; 
            address=&quot;&quot; /&gt;
&lt;/service&gt;
</pre>
<div class="preview">
<pre class="csharp">&lt;service&nbsp;name=<span class="cs__string">&quot;Microsoft.ServiceBus.Samples.HelloService&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="cs__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="cs__string">&quot;Microsoft.ServiceBus.Samples.IHelloContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="cs__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="cs__string">&quot;default&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="cs__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&lt;/service&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">This endpoint is configured to use a binding of type
<strong>NetTcpRelayBinding</strong>. It references a binding configuration called &quot;default&quot; and specifies that the connection mode is &quot;Hybrid.&quot;</div>
<div class="endscriptcode"><br>
The Hybrid connection mode first establishes a relayed connection, and if possible, switches automatically to a direct connection between a client and service.</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;bindings&gt;
  &lt;netTcpRelayBinding&gt;
    &lt;binding name=&quot;default&quot; connectionMode=&quot;Hybrid&quot; /&gt;
  &lt;/netTcpRelayBinding&gt;
&lt;/bindings&gt;
</pre>
<div class="preview">
<pre class="js">&lt;bindings&gt;&nbsp;
&nbsp;&nbsp;&lt;netTcpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;default&quot;</span>&nbsp;connectionMode=<span class="js__string">&quot;Hybrid&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&lt;/netTcpRelayBinding&gt;&nbsp;
&lt;/bindings&gt;&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">Client</h1>
<div class="endscriptcode"><br>
The client project defines HelloClient. In the application configuration file, the client is configured with the following endpoint:</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;client&gt;
  &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IHelloContract&quot;
            binding=&quot;netTcpRelayBinding&quot;
            bindingConfiguration=&quot;default&quot; /&gt;
&lt;/client&gt;
</pre>
<div class="preview">
<pre class="js">&lt;client&gt;&nbsp;
&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IHelloContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;default&quot;</span>&nbsp;/&gt;&nbsp;
&lt;/client&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;In the code, an endpoint is opened.</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"></div>
</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">Uri serviceUri = ServiceBusEnvironment.CreateServiceUri(&quot;sb&quot;, serviceNamespace, &quot;HelloService&quot;);
ChannelFactory&lt;IHelloChannel&gt; channelFactory = new ChannelFactory&lt;IHelloChannel&gt;(&quot;RelayEndpoint&quot;, new EndpointAddress(serviceUri));
channelFactory.Endpoint.Behaviors.Add(relayCredentials);
IHelloChannel channel = channelFactory.CreateChannel();
channel.Open();
</pre>
<div class="preview">
<pre class="js">Uri&nbsp;serviceUri&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="js__string">&quot;sb&quot;</span>,&nbsp;serviceNamespace,&nbsp;<span class="js__string">&quot;HelloService&quot;</span>);&nbsp;
ChannelFactory&lt;IHelloChannel&gt;&nbsp;channelFactory&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ChannelFactory&lt;IHelloChannel&gt;(<span class="js__string">&quot;RelayEndpoint&quot;</span>,&nbsp;<span class="js__operator">new</span>&nbsp;EndpointAddress(serviceUri));&nbsp;
channelFactory.Endpoint.Behaviors.Add(relayCredentials);&nbsp;
IHelloChannel&nbsp;channel&nbsp;=&nbsp;channelFactory.CreateChannel();&nbsp;
channel.Open();&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">
<p>After the <strong><code>ChannelFactory</code> </strong>has been created, the client application creates a channel to the service and then proceeds to interact with it by calling
<code>channel.Hello(&quot;Hello&quot;)</code> numerous times and calculating the time between each message response.</p>
<p>When a direct connection is established, throughput is significantly increased</p>
<p>&nbsp;</p>
<h1>Building and Running the Sample</h1>
<p>After building the solution, perform the following steps to run the application:</p>
<ol>
<li>From a command prompt, run the service (Service\bin\Debug\Service.exe). </li><li>When prompted, enter your service namespace, issuer name and issuer key. At this point, the service should indicate that it is listening at the configured address.
</li><li>In another command prompt window, run the client (Client\bin\Debug\Client.exe).
</li><li>You will be prompted for the service namespace, issuer name and issuer key. </li><li>When finished, press ENTER to exit the client and the service. </li></ol>
<p>If a direct connection is established, you will see an increased number of sent messages.</p>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span></span>&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span>&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span>&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span>&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span>&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span>&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span>&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
<div class="endscriptcode"><span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
