# Relayed Messaging: Metadata Exchange
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
* 2011-11-15 04:40:23
## Description

<h1>Introduction</h1>
<p>This sample demonstrates how to expose a metadata endpoint that uses the relay binding. MetadataExchange is supported in the following relay bindings: NetTcpRelayBinding, NetOnewayRelayBinding, BasicHttpRelayBinding and WS2007HttpRelayBinding.</p>
<h1>Prerequisites</h1>
<div>If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<div>&nbsp;</div>
<h1>Service</h1>
<div>&nbsp;</div>
<div>The service project is based on the service project in the Echo sample.<br>
To add metadata publishing to the service, modify the application configuration file to include an additional behavior section, as follows:</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">XML</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;behavior name=&quot;serviceMetadata&quot;&gt;
  &lt;serviceMetadata /&gt;
&lt;/behavior&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;behavior</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;serviceMetadata&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;<span class="xml__tag_start">&lt;serviceMetadata</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/behavior&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="color:black; line-height:115%">This behavior section is then referenced from the services configuration section:</span></div>
</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">XML</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;service name=&quot;Microsoft.ServiceBus.Samples.EchoService&quot;
   behaviorConfiguration=&quot;serviceMetadata&quot;&gt;
   &lt;endpoint name=&quot;RelayEndpoint&quot;
      contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
      binding=&quot;netTcpRelayBinding&quot; 
      address=&quot;&quot; /&gt;
   &lt;endpoint name=&quot;MexEndpoint&quot;
      contract=&quot;IMetadataExchange&quot;
      binding=&quot;ws2007HttpRelayBinding&quot; 
      bindingConfiguration=&quot;mexBinding&quot;
      address=&quot;&quot; /&gt;
&lt;/service&gt;
</pre>
<div class="preview">
<pre class="js">&lt;service&nbsp;name=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.EchoService&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;serviceMetadata&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;MexEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;IMetadataExchange&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;ws2007HttpRelayBinding&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;mexBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&lt;/service&gt;&nbsp;
</pre>
</div>
</div>
</div>
</div>
<div>For the service to authenticate with the Service Bus, you are prompted to enter the service namespace and the issuer credentials. The issuer name is used to construct the service URI.Next, the sample creates a service endpoint and a MEX endpoint. It then
 adds the <strong>TransportClientEndpointBehavior </strong>and opens a service endpoint.</div>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">Uri sbAddress = ServiceBusEnvironment.CreateServiceUri(&quot;sb&quot;, serviceNamespace, &quot;Echo/Service&quot;); 
Uri httpAddress = ServiceBusEnvironment.CreateServiceUri(&quot;http&quot;, serviceNamespace, &quot;Echo/mex&quot;);
...
TransportClientEndpointBehavior sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
sharedSecretServiceBusCredential.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);
...
ServiceHost host = new ServiceHost(typeof(EchoService), address);
...
host.Open();
</pre>
<div class="preview">
<pre class="js">Uri&nbsp;sbAddress&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="js__string">&quot;sb&quot;</span>,&nbsp;serviceNamespace,&nbsp;<span class="js__string">&quot;Echo/Service&quot;</span>);&nbsp;&nbsp;
Uri&nbsp;httpAddress&nbsp;=&nbsp;ServiceBusEnvironment.CreateServiceUri(<span class="js__string">&quot;http&quot;</span>,&nbsp;serviceNamespace,&nbsp;<span class="js__string">&quot;Echo/mex&quot;</span>);&nbsp;
...&nbsp;
TransportClientEndpointBehavior&nbsp;sharedSecretServiceBusCredential&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;TransportClientEndpointBehavior();&nbsp;
sharedSecretServiceBusCredential.TokenProvider&nbsp;=&nbsp;TokenProvider.CreateSharedSecretTokenProvider(issuerName,&nbsp;issuerSecret);&nbsp;
...&nbsp;
ServiceHost&nbsp;host&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ServiceHost(<span class="js__operator">typeof</span>(EchoService),&nbsp;address);&nbsp;
...&nbsp;
host.Open();&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">Client</h1>
<div class="endscriptcode">In this sample, the client is the Svcutil.exe tool.</div>
<div class="endscriptcode">&nbsp;<br>
<strong>Note: </strong>Svcutil.exe is installed as part of the Windows SDK. A typical location might be C:\Program Files\Microsoft SDKs\Windows\&lt;version&gt;\Bin\NETFX 4.0 Tools\SvcUtil.exe</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<h1>Building and Running the Sample</h1>
<div>To run the sample, build the solution in Visual Studio or from the command line. After building the solution, do the following to run the application.</div>
<ol>
<li>
<div>From a command prompt, run the service (Service\bin\Debug\Service.exe) as administrator.&nbsp;</div>
</li><li>
<div>Copy the svcutil.exe.config included with this solution in the same directory as svcutil.exe. If svcutil.exe.config already exists, add the bindingExtensions, policyImporters, and wsdlImporters of the attached svcutil.exe.config to it.</div>
</li><li>
<div>From another command prompt with elevated privileges, run the .NET 4.0 svcutil against the mex endpoint opened by the service: svcutil.exe
<a href="http://&lt;service-namespace&gt;.servicebus.windows.net/Echo/mex/">http://&lt;service-namespace&gt;.servicebus.windows.net/Echo/mex/</a>, replacing &lt;service-namespace&gt; with the your service namespace.</div>
</li></ol>
<div></div>
<h2>Expected Output &ndash; SvcUtill</h2>
<div>SvcUtil.exe /d:c:\ /r:&quot;C:\Program Files\WindowsAzureSDK\v1.6\ServiceBus\ref\Microsoft.ServiceBus.dll&quot;
<a href="http://&lt;service-namespace&gt;.servicebus.windows.net/Echo/mex">http://&lt;service-namespace&gt;.servicebus.windows.net/Echo/mex</a><br>
Microsoft (R) Service Model Metadata Tool<br>
[Microsoft .NET Framework, Version 3.0.50727.357]<br>
Copyright (c) Microsoft Corporation.&nbsp; All rights reserved.<br>
&nbsp;<br>
Generating files...<br>
C:\EchoService.cs<br>
C:\output.config</div>
<div>&nbsp;</div>
<h2>Expected Output &ndash; output.config</h2>
<div>&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">XML</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;configuration&gt;  
   &lt;client&gt;
      &lt;endpoint address=&quot;sb://&lt;issuer-name&gt;.servicebus.windows.net/services/Echo/&quot;
         binding=&quot;netTcpRelayBinding&quot; bindingConfiguration=&quot;RelayEndpoint&quot;
         contract=&quot;IEchoContract&quot; name=&quot;RelayEndpoint&quot; /&gt;
      &lt;/client&gt;
   &lt;/system.serviceModel&gt;
&lt;/configuration&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">&quot;1.0&quot;</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">&quot;utf-8&quot;</span><span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;client</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;endpoint</span>&nbsp;address=&quot;sb://&lt;issuer-name<span class="xml__tag_start">&gt;.</span>servicebus.windows.net/services/Echo/&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=&quot;netTcpRelayBinding&quot;&nbsp;bindingConfiguration=&quot;RelayEndpoint&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=&quot;IEchoContract&quot;&nbsp;name=&quot;RelayEndpoint&quot;&nbsp;<span class="xml__tag_end">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/client&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&lt;/system.serviceModel&gt;&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
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
<div>&nbsp;</div>
