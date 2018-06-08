# Relayed Messaging Bindings: Net Oneway
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
* 2011-11-16 11:48:31
## Description

<h1>Introduction</h1>
<div><span style="color:black">This sample demonstrates how to expose and consume a service endpoint using the
<strong>NetOnewayRelayBinding </strong>binding. </span></div>
<h1><br>
Prerequisites</h1>
<div><span style="color:black">If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.
</span></div>
<div>&nbsp;</div>
<h1>Service</h1>
<div><span style="color:black">The service and client both use the following simple contract:</span></div>
<div><br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract(Name = &quot;IOnewayContract&quot;, Namespace = &quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;)]
    public interface IOnewayContract
    {
        [OperationContract(IsOneWay = true)]
        void Send(int count);
    } 
</pre>
<div class="preview">
<pre class="csharp">[ServiceContract(Name&nbsp;=&nbsp;<span class="cs__string">&quot;IOnewayContract&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="cs__string">&quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span><span class="cs__keyword">interface</span>&nbsp;IOnewayContract&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract(IsOneWay&nbsp;=&nbsp;<span class="cs__keyword">true</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;Send(<span class="cs__keyword">int</span>&nbsp;count);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;
</pre>
</div>
</div>
</div>
</div>
<div><span style="color:black">The endpoints for this service are defined in the application configuration file, as follows:</span></div>
<div><br>
<br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;service name=&quot;Microsoft.ServiceBus.Samples.OnewayService&quot;&gt;
    &lt;endpoint address=&quot;&quot; behaviorConfiguration=&quot;sharedSecretClientCredentials&quot;binding=&quot;netOnewayRelayBinding&quot; bindingConfiguration=&quot;default&quot;       name=&quot;RelayEndpoint&quot; contract=&quot;Microsoft.ServiceBus.Samples.IOnewayContract&quot; /&gt;
&lt;/service&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;service</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;Microsoft.ServiceBus.Samples.OnewayService&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;endpoint</span>&nbsp;<span class="xml__attr_name">address</span>=<span class="xml__attr_value">&quot;&quot;</span>&nbsp;<span class="xml__attr_name">behaviorConfiguration</span>=<span class="xml__attr_value">&quot;sharedSecretClientCredentials&quot;</span><span class="xml__attr_name">binding</span>=<span class="xml__attr_value">&quot;netOnewayRelayBinding&quot;</span>&nbsp;<span class="xml__attr_name">bindingConfiguration</span>=<span class="xml__attr_value">&quot;default&quot;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;RelayEndpoint&quot;</span>&nbsp;<span class="xml__attr_name">contract</span>=<span class="xml__attr_value">&quot;Microsoft.ServiceBus.Samples.IOnewayContract&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/service&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div><span style="color:black">The client is configured with the following endpoint:</span></div>
<div><br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;client&gt;
    &lt;!-- Application Endpoint --&gt;
    &lt;endpoint name=&quot;RelayEndpoint&quot;
        contract=&quot;Microsoft.ServiceBus.Samples.IOnewayContract&quot;
        binding=&quot;netOnewayRelayBinding&quot;
        bindingConfiguration=&quot;default&quot;
        behaviorConfiguration=&quot;sharedSecretClientCredentials&quot; 
        address=&quot;http://AddressToBeReplacedInCode/&quot; /&gt;
&lt;/client&gt;
</pre>
<div class="preview">
<pre class="js">&lt;client&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Application&nbsp;Endpoint&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IOnewayContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netOnewayRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;default&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;sharedSecretClientCredentials&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;http://AddressToBeReplacedInCode/&quot;</span>&nbsp;/&gt;&nbsp;
&lt;/client&gt;&nbsp;
</pre>
</div>
</div>
</div>
</div>
<div>The address is a placeholder that will be replaced in the application.</div>
<div></div>
<h1>Building and Running the Sample</h1>
<div>Before building the solution, perform the following steps to update the App.config files</div>
<ol>
<li>Open the App.config file in the \Service folder, replace &quot;ISSUER_NAME&quot; with &quot;owner&quot; and &quot;ISSUER_SECRET&quot; with the actual issuer secret.
</li><li>Open the App.config file in the \Client folder, replace &quot;ISSUER_NAME&quot; with &quot;owner&quot; and &quot;ISSUER_SECRET&quot;<br>
with the actual issuer secret. </li></ol>
<div>After building the solution, do the following to run the application:</div>
<ol>
<li>From a command prompt, run the service application from Service\bin\Debug\Service.exe.
</li><li>When prompted, enter the service namespace. At this point, the service should be running and prints the following text &quot;Press [Enter] to exit&quot;.
</li><li>From another command prompt, run the client application from Client\bin\Debug\Client.exe.
</li><li>You will be prompted for the service namespace with which to connect (the service namespace specified in step 2). At this point, the client should start sending messages to the service. Note that by default, the client<br>
sends a total of 25 messages. <br>
<br>
<br>
<br>
<br>
<br>
</li></ol>
<div><br>
<br>
</div>
