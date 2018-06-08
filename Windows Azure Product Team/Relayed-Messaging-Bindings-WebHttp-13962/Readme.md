# Relayed Messaging Bindings: WebHttp
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
* 2014-09-03 11:56:41
## Description

<h1 style="text-align:left">Introduction</h1>
<div>This sample demonstrates how to use the WebHttpRelayBinding binding to return binary data using the Web programming model.</div>
<div>&nbsp;</div>
<h1>Service</h1>
<div>The service project defines a simple contract. The OperationContractAttribute and WebGetAttribute attributes are applied to the GetImage method.</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract(Name = &quot;ImageContract&quot;, Namespace = &quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;)]
public interface IImageContract
{
    [OperationContract, WebGet]
    Stream GetImage();
}
</pre>
<div class="preview">
<pre class="js">[ServiceContract(Name&nbsp;=&nbsp;<span class="js__string">&quot;ImageContract&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="js__string">&quot;http://samples.microsoft.com/ServiceModel/Relay/&quot;</span>)]&nbsp;
public&nbsp;interface&nbsp;IImageContract&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract,&nbsp;WebGet]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Stream&nbsp;GetImage();&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">The ImageContract contract is implemented by the <strong>
ImageService </strong>class. This class reads a bitmap from a file (included in the solution). When
<strong>GetImage </strong>is called, the response returns a message that contains the image. The configuration uses the
<strong>WebHttpRelayBinding </strong>binding. Note that <strong>relayClientAuthenticationType
</strong>is set to None, therefore the client credential is not required when sending an HTTP GET request.</div>
<br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
&lt;configuration&gt;
  &lt;system.serviceModel&gt;
    &lt;bindings&gt;
      &lt;!-- Application Binding --&gt;
      &lt;webHttpRelayBinding&gt;
        &lt;binding name=&quot;default&quot;&gt;
            &lt;security relayClientAuthenticationType=&quot;None&quot; /&gt;
        &lt;/binding&gt;
      &lt;/webHttpRelayBinding&gt;
    &lt;/bindings&gt;

    &lt;services&gt;
      &lt;!-- Application Service --&gt;
      &lt;service name=&quot;Microsoft.ServiceBus.Samples.ImageService&quot;
               behaviorConfiguration=&quot;default&quot;&gt;
        &lt;endpoint name=&quot;RelayEndpoint&quot;
                  contract=&quot;Microsoft.ServiceBus.Samples.IImageContract&quot;
                  binding=&quot;webHttpRelayBinding&quot;
                  bindingConfiguration=&quot;default&quot;
                  behaviorConfiguration=&quot;sharedAccessSignatureCredentials&quot;
                  address=&quot;&quot; /&gt;
      &lt;/service&gt;
    &lt;/services&gt;

    &lt;behaviors&gt;
      &lt;endpointBehaviors&gt;
        &lt;behavior name=&quot;sharedAccessSignatureCredentials&quot;&gt;
          &lt;transportClientEndpointBehavior&gt;
            &lt;tokenProvider&gt;
              &lt;sharedAccessSignature keyName=&quot;SAS_KEY_NAME&quot; key=&quot;SAS_KEY&quot; /&gt;
            &lt;/tokenProvider&gt;
          &lt;/transportClientEndpointBehavior&gt;
        &lt;/behavior&gt;
      &lt;/endpointBehaviors&gt;
      &lt;serviceBehaviors&gt;
        &lt;behavior name=&quot;default&quot;&gt;
          &lt;serviceDebug httpHelpPageEnabled=&quot;false&quot; httpsHelpPageEnabled=&quot;false&quot; /&gt;
        &lt;/behavior&gt;
      &lt;/serviceBehaviors&gt;
    &lt;/behaviors&gt;
  &lt;/system.serviceModel&gt;
&lt;/configuration&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">&quot;1.0&quot;</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">&quot;utf-8&quot;</span>&nbsp;<span class="xml__tag_start">?&gt;</span>&nbsp;
<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;<span class="xml__tag_start">&lt;system</span>.serviceModel<span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;bindings</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__comment">&lt;!--&nbsp;Application&nbsp;Binding&nbsp;--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;webHttpRelayBinding</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;binding</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;default&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;security</span>&nbsp;<span class="xml__attr_name">relayClientAuthenticationType</span>=<span class="xml__attr_value">&quot;None&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/binding&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/webHttpRelayBinding&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/bindings&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;services</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__comment">&lt;!--&nbsp;Application&nbsp;Service&nbsp;--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;service</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;Microsoft.ServiceBus.Samples.ImageService&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">behaviorConfiguration</span>=<span class="xml__attr_value">&quot;default&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;endpoint</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">contract</span>=<span class="xml__attr_value">&quot;Microsoft.ServiceBus.Samples.IImageContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">binding</span>=<span class="xml__attr_value">&quot;webHttpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">bindingConfiguration</span>=<span class="xml__attr_value">&quot;default&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">behaviorConfiguration</span>=<span class="xml__attr_value">&quot;sharedAccessSignatureCredentials&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">address</span>=<span class="xml__attr_value">&quot;&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/service&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/services&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;behaviors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;endpointBehaviors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;behavior</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;sharedAccessSignatureCredentials&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;transportClientEndpointBehavior</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;tokenProvider</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;sharedAccessSignature</span>&nbsp;<span class="xml__attr_name">keyName</span>=<span class="xml__attr_value">&quot;SAS_KEY_NAME&quot;</span>&nbsp;<span class="xml__attr_name">key</span>=<span class="xml__attr_value">&quot;SAS_KEY&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/tokenProvider&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/transportClientEndpointBehavior&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/behavior&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/endpointBehaviors&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;serviceBehaviors</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;behavior</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;default&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;serviceDebug</span>&nbsp;<span class="xml__attr_name">httpHelpPageEnabled</span>=<span class="xml__attr_value">&quot;false&quot;</span>&nbsp;<span class="xml__attr_name">httpsHelpPageEnabled</span>=<span class="xml__attr_value">&quot;false&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/behavior&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/serviceBehaviors&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/behaviors&gt;</span>&nbsp;
&nbsp;&nbsp;&lt;/system.serviceModel&gt;&nbsp;
<span class="xml__tag_end">&lt;/configuration&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">&nbsp;Client</h1>
<div class="endscriptcode"></div>
<p class="endscriptcode">For this sample, the client can be any web browser. The browser can send an HTTP GET to the service, and the request is mapped to a
<strong>GetImage</strong> operation.</p>
<p class="endscriptcode">&nbsp;</p>
<div class="endscriptcode"></div>
<h2 class="endscriptcode">Building and Running the Sample</h2>
<div class="endscriptcode"></div>
<p class="endscriptcode">Before building the solution, do the following to update the App.config file:</p>
<div class="endscriptcode">
<ol>
<li>Open the App.config file under the \Service project. Replace SAS_KEY_NAME with the actual&nbsp;name of the key and&nbsp;SAS_KEY with the actual key.
</li></ol>
</div>
<p>After building the solution, perform the following steps to obtain the image:</p>
<ol>
<li>From a command prompt, run the service (Service\bin\Debug\Service.exe). </li><li>When prompted, enter the service namespace name. </li><li>At this point, the service should indicate that it is listening at the configured address.
</li><li>Navigate to the URL provided by the service using any Web browser, and view the returned image.
</li></ol>
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
</div>
<div><em>&nbsp;</em>&nbsp;</div>
