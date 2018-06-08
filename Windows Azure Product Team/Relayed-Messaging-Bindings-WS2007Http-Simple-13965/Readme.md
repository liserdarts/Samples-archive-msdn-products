# Relayed Messaging Bindings: WS2007Http Simple
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
* 2011-11-15 04:06:14
## Description

<h1>Introduction</h1>
<div>This sample demonstrates using the <strong>WS2007HttpRelayBinding</strong> binding. It demonstrates a simple service that uses no security options and does not require clients to authenticate.</div>
<div>&nbsp;</div>
<h1>Prerequisites</h1>
<div>If you haven't already done so, read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<div>&nbsp;</div>
<h1>Service</h1>
<div>The service project defines a simple contract, IEchoContract, with a single operation named Echo. The Echo service accepts a string and echoes it back.</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
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
}
</pre>
<div class="preview">
<pre class="js">[ServiceBehavior(Name&nbsp;=&nbsp;<span class="js__string">&quot;EchoService&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="js__string">&quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;</span>)]&nbsp;
class&nbsp;EchoService&nbsp;:&nbsp;IEchoContract&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;string&nbsp;Echo(string&nbsp;text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;Echoing:&nbsp;{0}&quot;</span>,&nbsp;text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;text;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="color:black; line-height:115%">The endpoints for this service are defined in the application configuration file. Specifically, the following endpoint is defined:</span></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">XML</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;system.serviceModel&gt;
    
    &lt;bindings&gt;
      &lt;!-- Binding configuration--&gt;
      &lt;ws2007HttpRelayBinding&gt;
        &lt;binding name=&quot;NoSecNoAuth&quot;&gt;
          &lt;!-- No message or transport security. Allow unauthenticated clients. --&gt;
          &lt;security mode=&quot;None&quot; relayClientAuthenticationType=&quot;None&quot;/&gt;
        &lt;/binding&gt;
      &lt;/ws2007HttpRelayBinding&gt;
    &lt;/bindings&gt;

    &lt;services&gt;
      &lt;!-- Service configuration--&gt;
      &lt;service name=&quot;Microsoft.ServiceBus.Samples.EchoService&quot;&gt;
        &lt;endpoint name=&quot;ServiceBusEndpoint&quot;
                  contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
                  binding=&quot;ws2007HttpRelayBinding&quot;
                  bindingConfiguration=&quot;NoSecNoAuth&quot; /&gt;        
      &lt;/service&gt;
    &lt;/services&gt;

&lt;/system.serviceModel&gt;
</pre>
<div class="preview">
<pre class="js">&lt;system.serviceModel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;bindings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Binding&nbsp;configuration--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;ws2007HttpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;NoSecNoAuth&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;No&nbsp;message&nbsp;or&nbsp;transport&nbsp;security.&nbsp;Allow&nbsp;unauthenticated&nbsp;clients.&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;None&quot;</span>&nbsp;relayClientAuthenticationType=<span class="js__string">&quot;None&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/ws2007HttpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/bindings&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;services&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Service&nbsp;configuration--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;service&nbsp;name=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.EchoService&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;ServiceBusEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;ws2007HttpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;NoSecNoAuth&quot;</span>&nbsp;/&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/service&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/services&gt;&nbsp;
&nbsp;
&lt;/system.serviceModel&gt;&nbsp;
</pre>
</div>
</div>
</div>
</div>
</div>
<h1><strong>Client</strong></h1>
<div>The client is configured (also in the application configuration file) with the following endpoint:<span style="font-family:Times New Roman; font-size:small">
</span></div>
<div class="MsoNormal" style="margin:13.5pt 0in 6pt; line-height:normal"><span style="color:black; line-height:115%">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">XML</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;system.serviceModel&gt;
    
    &lt;bindings&gt;
      &lt;!-- Binding configuration--&gt;
      &lt;ws2007HttpRelayBinding&gt;
        &lt;binding name=&quot;NoSecNoAuth&quot;&gt;
          &lt;!-- No message or transport security. Allow unauthenticated clients. --&gt;
          &lt;security mode=&quot;None&quot; relayClientAuthenticationType=&quot;None&quot;/&gt;
        &lt;/binding&gt;
      &lt;/ws2007HttpRelayBinding&gt;
    &lt;/bindings&gt;

    &lt;client&gt;
      &lt;!-- Client endpoint configuration --&gt;
      &lt;endpoint name=&quot;ServiceBusEndpoint&quot;
                contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
                binding=&quot;ws2007HttpRelayBinding&quot;
                bindingConfiguration=&quot;NoSecNoAuth&quot; /&gt;
    &lt;/client&gt;

&lt;/system.serviceModel&gt;
</pre>
<div class="preview">
<pre class="js">&lt;system.serviceModel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;bindings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Binding&nbsp;configuration--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;ws2007HttpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;NoSecNoAuth&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;No&nbsp;message&nbsp;or&nbsp;transport&nbsp;security.&nbsp;Allow&nbsp;unauthenticated&nbsp;clients.&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;None&quot;</span>&nbsp;relayClientAuthenticationType=<span class="js__string">&quot;None&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/ws2007HttpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/bindings&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;client&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Client&nbsp;endpoint&nbsp;configuration&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;ServiceBusEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;ws2007HttpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;NoSecNoAuth&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/client&gt;&nbsp;
&nbsp;
&lt;/system.serviceModel&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</span>
<h1 class="endscriptcode">Running the Sample</h1>
<div class="endscriptcode">To run the sample, build the solution in Visual Studio or from the command line, then run the two resulting executable files. Start the service first using a command prompt with administrator privileges, then run the client. You
 would be prompted to enter the Issuer Name and Issuer Secret of the namespace you are using to run this sample.</div>
<div class="endscriptcode">When the service and the client are running, you can start typing messages into the client application. These messages are echoed by the service.</div>
<h2 class="endscriptcode"><br>
Expected Output &ndash; Client</h2>
</div>
<div>Your Service Namespace: &lt;service namespace&gt;<br>
Enter text to echo (or [Enter] to exit): <br>
Hello, World!<br>
Server echoed: Hello, World!</div>
<div>&nbsp;</div>
<h2>Expected Output &ndash; Service</h2>
<div>Your Service Namespace: &lt;service namespace&gt;<br>
Your Issuer Name: &lt;issuer name&gt;<br>
Your Issuer Secret: &lt;issuer secret&gt;<br>
Service address: <a href="http://&lt;service">http://&lt;service</a> namespace&gt;.servicebus.windows.net/HttpEchoService/<br>
Press [Enter] to exit: Hello, World!</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
