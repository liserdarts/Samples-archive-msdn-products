# Relayed Messaging Bindings: NetTcp MsgSec UserName
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
* 2011-11-15 01:49:43
## Description

<h1>Introduction</h1>
<div>This sample demonstrates using the <strong>NetTcpRelayBinding</strong> binding with message security.</div>
<div><strong>&nbsp;</strong>&nbsp;</div>
<h1><strong>Prerequisites</strong></h1>
<div>If you haven't already done so, read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment. Before running the sample, you must run the Setup.bat script from the solution directory in
 a Visual Studio 2010 (or above) or a Windows SDK command prompt running with administrator privileges. The setup script creates and installs an X.509 test certificate that is used as the service identity. After running the sample, you should run the Cleanup.bat
 script to remove the certificate.</div>
<div><strong>&nbsp;</strong>&nbsp;</div>
<h1><strong>Echo Service</strong></h1>
<div>The service implements a simple contract with a single operation named Echo. The Echo service accepts a string and echoes the string back.</div>
<div>&nbsp;</div>
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
</div>
<div>The service configuration contains one active and two additional optional service settings. The default endpoint configuration refers to a
<strong>NetTcpRelayBinding </strong>binding configuration that uses a UserName credential for message security. The alternate
<strong>relayClientAuthenticationNone </strong>configuration refers to a&nbsp; <strong>
NetTcpRelayBinding </strong>binding configuration that uses a UserName credential for message security, and also disables the relay client authentication. The second alternate configuration,
<strong>transportWithMessageCredential</strong>, uses a message credential for end-to-end authentication/authorization, but relies on SSL for message protection.</div>
<div>&nbsp;</div>
<div>To secure the endpoint, the service is configured with the <strong>usernamePasswordServiceBehavior
</strong>behavior. This behavior contains the service credentials (backed by the test certificate generated and installed by the Setup.bat script) and refers to the SimpleUserNamePasswordValidator in the service project that authenticates the credentials. This
 validator recognizes two hard-coded username and password combinations: test1/1tset and test2/2tset. Refer to the WCF Authentication documentation section for information about implementing other credential validators.<span style="font-family:Times New Roman; font-size:small">&nbsp;</span></div>
<div class="MsoNormal" style="margin:0in 0in 11.25pt; line-height:normal">&nbsp;</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
&lt;configuration&gt;
   &lt;system.serviceModel&gt;
      &lt;behaviors&gt;
         &lt;serviceBehaviors&gt;
            &lt;behavior name=&quot;usernamePasswordServiceBehavior&quot;&gt;
               &lt;serviceCredentials&gt;
                  &lt;serviceCertificate findValue=&quot;localhost&quot; storeLocation=&quot;LocalMachine&quot; storeName=&quot;My&quot; x509FindType=&quot;FindBySubjectName&quot; /&gt;
                  &lt;userNameAuthentication userNamePasswordValidationMode=&quot;Custom&quot; 
                         includeWindowsGroups=&quot;false&quot; customUserNamePasswordValidatorType=&quot;Microsoft.ServiceBus.Samples.SimpleUsernamePasswordValidator, 
                         NetTcpRelayMsgSecUserNameService&quot; /&gt;
               &lt;/serviceCredentials&gt;
            &lt;/behavior&gt;
         &lt;/serviceBehaviors&gt;
         &lt;endpointBehaviors&gt;
            &lt;behavior name=&quot;sharedSecretEndpointBehavior&quot;&gt;
               &lt;transportClientEndpointBehavior credentialType=&quot;SharedSecret&quot;&gt;
                  &lt;clientCredentials&gt;
                     &lt;sharedSecret issuerName=&quot;ISSUER_NAME&quot; issuerSecret=&quot;ISSUER_SECRET&quot; /&gt;
                  &lt;/clientCredentials&gt;
               &lt;/transportClientEndpointBehavior&gt; 
            &lt;/behavior&gt;
         &lt;/endpointBehaviors&gt;
      &lt;/behaviors&gt;
      &lt;bindings&gt;
         &lt;netTcpRelayBinding&gt;
            &lt;!-- Default Binding Configuration--&gt;
            &lt;binding name=&quot;default&quot;&gt;
               &lt;security mode=&quot;Message&quot;&gt;
                  &lt;message clientCredentialType=&quot;UserName&quot;/&gt;
               &lt;/security&gt;
            &lt;/binding&gt;
            &lt;!-- Alternate Binding Configuration #1: Disabling Client Relay Authentication --&gt;
            &lt;binding name=&quot;relayClientAuthenticationNone&quot;&gt;
               &lt;security mode=&quot;Message&quot; relayClientAuthenticationType=&quot;None&quot;&gt;
                  &lt;message clientCredentialType=&quot;UserName&quot;/&gt;
               &lt;/security&gt;
            &lt;/binding&gt;
            &lt;!-- Alternate Binding Configuration #2: Transport With Message Credential --&gt;
            &lt;binding name=&quot;transportWithMessageCredential&quot;&gt;
               &lt;security mode=&quot;TransportWithMessageCredential&quot;&gt;
                  &lt;message clientCredentialType=&quot;UserName&quot;/&gt;
               &lt;/security&gt;
            &lt;/binding&gt;
         &lt;/netTcpRelayBinding&gt;
      &lt;/bindings&gt;

      &lt;services&gt;
      &lt;!-- Application Service --&gt;
         &lt;service name=&quot;Microsoft.ServiceBus.Samples.EchoService&quot; behaviorConfiguration=&quot;usernamePasswordServiceBehavior&quot;&gt;
            &lt;!-- 
               Default configuration. You must comment out the following declaration whenever you want to use any of the alternate configurations below. 
            --&gt;
            &lt;endpoint name=&quot;RelayEndpoint&quot;
               contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
               binding=&quot;netTcpRelayBinding&quot;
               bindingConfiguration=&quot;default&quot; 
               behaviorConfiguration=&quot;sharedSecretEndpointBehavior&quot; 
               address=&quot;&quot; /&gt;

             &lt;!-- Alternatively use the endpoint configuration below to enable alternate configuration #1 --&gt;
             &lt;!--
             &lt;endpoint name=&quot;RelayEndpoint&quot;
               contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
               binding=&quot;netTcpRelayBinding&quot;
               bindingConfiguration=&quot;relayClientAuthenticationNone&quot;
               behaviorConfiguration=&quot;sharedSecretEndpointBehavior&quot;
               address=&quot;&quot; /&gt;
             --&gt;

            &lt;!-- Alternatively use the endpoint configuration below to enable alternate configuration #2 --&gt;
            &lt;!--
            &lt;endpoint name=&quot;RelayEndpoint&quot;
              contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
              binding=&quot;netTcpRelayBinding&quot;
              bindingConfiguration=&quot;transportWithMessageCredential&quot;
              behaviorConfiguration=&quot;sharedSecretEndpointBehavior&quot;
              address=&quot;&quot; /&gt;
            --&gt;
         &lt;/service&gt;
      &lt;/services&gt;
   &lt;/system.serviceModel&gt;
&lt;/configuration&gt;
</pre>
<div class="preview">
<pre class="js">&lt;?xml&nbsp;version=<span class="js__string">&quot;1.0&quot;</span>&nbsp;encoding=<span class="js__string">&quot;utf-8&quot;</span>&nbsp;?&gt;&nbsp;
&lt;configuration&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&lt;system.serviceModel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;behaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;serviceBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;usernamePasswordServiceBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;serviceCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;serviceCertificate&nbsp;findValue=<span class="js__string">&quot;localhost&quot;</span>&nbsp;storeLocation=<span class="js__string">&quot;LocalMachine&quot;</span>&nbsp;storeName=<span class="js__string">&quot;My&quot;</span>&nbsp;x509FindType=<span class="js__string">&quot;FindBySubjectName&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;userNameAuthentication&nbsp;userNamePasswordValidationMode=<span class="js__string">&quot;Custom&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;includeWindowsGroups=<span class="js__string">&quot;false&quot;</span>&nbsp;customUserNamePasswordValidatorType=&quot;Microsoft.ServiceBus.Samples.SimpleUsernamePasswordValidator,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NetTcpRelayMsgSecUserNameService&quot;&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/serviceCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/serviceBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;transportClientEndpointBehavior&nbsp;credentialType=<span class="js__string">&quot;SharedSecret&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;sharedSecret&nbsp;issuerName=<span class="js__string">&quot;ISSUER_NAME&quot;</span>&nbsp;issuerSecret=<span class="js__string">&quot;ISSUER_SECRET&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/transportClientEndpointBehavior&gt;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;bindings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;netTcpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Default&nbsp;Binding&nbsp;Configuration--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;default&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;Message&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;message&nbsp;clientCredentialType=<span class="js__string">&quot;UserName&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/security&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternate&nbsp;Binding&nbsp;Configuration&nbsp;#<span class="js__num">1</span>:&nbsp;Disabling&nbsp;Client&nbsp;Relay&nbsp;Authentication&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;relayClientAuthenticationNone&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;Message&quot;</span>&nbsp;relayClientAuthenticationType=<span class="js__string">&quot;None&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;message&nbsp;clientCredentialType=<span class="js__string">&quot;UserName&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/security&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternate&nbsp;Binding&nbsp;Configuration&nbsp;#<span class="js__num">2</span>:&nbsp;Transport&nbsp;With&nbsp;Message&nbsp;Credential&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;transportWithMessageCredential&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;TransportWithMessageCredential&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;message&nbsp;clientCredentialType=<span class="js__string">&quot;UserName&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/security&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/netTcpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/bindings&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;services&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Application&nbsp;Service&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;service&nbsp;name=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.EchoService&quot;</span>&nbsp;behaviorConfiguration=<span class="js__string">&quot;usernamePasswordServiceBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Default&nbsp;configuration.&nbsp;You&nbsp;must&nbsp;comment&nbsp;out&nbsp;the&nbsp;following&nbsp;declaration&nbsp;whenever&nbsp;you&nbsp;want&nbsp;to&nbsp;use&nbsp;any&nbsp;of&nbsp;the&nbsp;alternate&nbsp;configurations&nbsp;below.&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;default&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternatively&nbsp;use&nbsp;the&nbsp;endpoint&nbsp;configuration&nbsp;below&nbsp;to&nbsp;enable&nbsp;alternate&nbsp;configuration&nbsp;#<span class="js__num">1</span>&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;relayClientAuthenticationNone&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternatively&nbsp;use&nbsp;the&nbsp;endpoint&nbsp;configuration&nbsp;below&nbsp;to&nbsp;enable&nbsp;alternate&nbsp;configuration&nbsp;#<span class="js__num">2</span>&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;transportWithMessageCredential&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/service&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/services&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&lt;/system.serviceModel&gt;&nbsp;
&lt;/configuration&gt;&nbsp;
</pre>
</div>
</div>
</div>
</div>
<h1 class="endscriptcode">Echo Client&nbsp;<span style="font-family:Times New Roman; font-size:small">
</span></h1>
<p class="MsoNormal" style="margin:13.5pt 0in 6pt; line-height:normal"><strong></strong><span style="color:black; line-height:115%">The client is similar to the Echo sample client, but differs in configuration and how the channel factory is configured with
 the correct end-to-end credentials.</span></p>
&nbsp;
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">ChannelFactory&lt;IEchoChannel&gt; channelFactory = new ChannelFactory&lt;IEchoChannel&gt;(&quot;RelayEndpoint&quot;, new EndpointAddress(serviceUri, 
    EndpointIdentity.CreateDnsIdentity(&quot;localhost&quot;)));
channelFactory.Credentials.UserName.UserName = &quot;test1&quot;;
channelFactory.Credentials.UserName.Password = &quot;1tset&quot;;
</pre>
<div class="preview">
<pre class="js">ChannelFactory&lt;IEchoChannel&gt;&nbsp;channelFactory&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ChannelFactory&lt;IEchoChannel&gt;(<span class="js__string">&quot;RelayEndpoint&quot;</span>,&nbsp;<span class="js__operator">new</span>&nbsp;EndpointAddress(serviceUri,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;EndpointIdentity.CreateDnsIdentity(<span class="js__string">&quot;localhost&quot;</span>)));&nbsp;
channelFactory.Credentials.UserName.UserName&nbsp;=&nbsp;<span class="js__string">&quot;test1&quot;</span>;&nbsp;
channelFactory.Credentials.UserName.Password&nbsp;=&nbsp;<span class="js__string">&quot;1tset&quot;</span>;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;&nbsp;</div>
</div>
<div>
<p>Note that the <strong>ChannelFactory </strong>is constructed using an <strong>
EndpointAddress </strong>that has an explicit DNS <strong>EndpointIdentity</strong>. However, the identity is not directly related to DNS but rather to the certificate subject name. The identity name (<strong>localhost
</strong>in this case) refers directly to the subject name of the certificate that is specified for the service identity in the
<strong>usernamePasswordServiceBehavior </strong>behavior in the service. For an actual implementation, the service identity should be backed by a production certificate issued by a trusted certificate authority (CA) and the
<strong>EndpointIdentity </strong>must refer to its subject name.</p>
<p>The client configuration mirrors the service configuration, with a few exceptions. The client endpoints are configured with the
<strong>usernamePasswordEndpointBehavior </strong>behavior with &lt;<strong>clientCredentials</strong>&gt; settings that disable certificate validation specifically for the test certificate being used. For an actual implementation that uses a CA-issued certificate,
 you should omit this override.</p>
<p>&nbsp;</p>
</div>
<div>&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
&lt;configuration&gt;
   &lt;system.serviceModel&gt;
      &lt;behaviors&gt;
         &lt;endpointBehaviors&gt;
            &lt;behavior name=&quot;sharedSecretEndpointBehavior&quot;&gt;
               &lt;transportClientEndpointBehavior credentialType=&quot;SharedSecret&quot;&gt;
                  &lt;clientCredentials&gt;
                     &lt;sharedSecret issuerName=&quot;ISSUER_NAME&quot; issuerSecret=&quot;ISSUER_SECRET&quot; /&gt;
                  &lt;/clientCredentials&gt;
               &lt;/transportClientEndpointBehavior&gt;
               &lt;clientCredentials&gt;
                  &lt;serviceCertificate&gt;
                     &lt;authentication certificateValidationMode=&quot;None&quot; /&gt;
                  &lt;/serviceCertificate&gt;
               &lt;/clientCredentials&gt;
            &lt;/behavior&gt;
            &lt;behavior name=&quot;noCertificateValidationEndpointBehavior&quot;&gt; 
               &lt;clientCredentials&gt;
                  &lt;serviceCertificate&gt;
                     &lt;authentication certificateValidationMode=&quot;None&quot; /&gt;
                  &lt;/serviceCertificate&gt;
               &lt;/clientCredentials&gt;
            &lt;/behavior&gt;
         &lt;/endpointBehaviors&gt;
      &lt;/behaviors&gt;
      &lt;bindings&gt;
      &lt;!-- Application Binding --&gt;
      &lt;netTcpRelayBinding&gt;
         &lt;!-- Default Binding Configuration--&gt;
         &lt;binding name=&quot;default&quot;&gt;
            &lt;security mode=&quot;Message&quot;&gt;
               &lt;message clientCredentialType=&quot;UserName&quot;/&gt;
            &lt;/security&gt;
         &lt;/binding&gt;
         &lt;!-- Alternate Binding Configuration #1: Disabling Client Relay Authentication --&gt;
         &lt;binding name=&quot;relayClientAuthenticationNone&quot;&gt;
            &lt;security mode=&quot;Message&quot; relayClientAuthenticationType=&quot;None&quot;&gt;
               &lt;message clientCredentialType=&quot;UserName&quot;/&gt;
            &lt;/security&gt;
         &lt;/binding&gt;
         &lt;!-- Alternate Binding Configuration #2: Transport With Message Credential --&gt;
         &lt;binding name=&quot;transportWithMessageCredential&quot;&gt;
            &lt;security mode=&quot;TransportWithMessageCredential&quot;&gt;
               &lt;message clientCredentialType=&quot;UserName&quot;/&gt;
            &lt;/security&gt;
         &lt;/binding&gt;
      &lt;/netTcpRelayBinding&gt;
   &lt;/bindings&gt;

     &lt;client&gt;
         &lt;!-- Default configuration. You must comment out the following declaration whenever 
            you want to use any of the alternate configurations below. 
        --&gt;
         &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
            binding=&quot;netTcpRelayBinding&quot;
            bindingConfiguration=&quot;default&quot;
            behaviorConfiguration=&quot;sharedSecretEndpointBehavior&quot;
            address=&quot;&quot; /&gt;

         &lt;!-- Alternatively use the endpoint configuration below to enable alternate configuration #1 --&gt;
         &lt;!--
         &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
            binding=&quot;netTcpRelayBinding&quot;
            bindingConfiguration=&quot;relayClientAuthenticationNone&quot; 
            behaviorConfiguration=&quot;noCertificateValidationEndpointBehavior&quot;
            address=&quot;&quot; /&gt;
         --&gt; 

         &lt;!-- Alternatively use the endpoint configuration below to enable alternate configuration #2 --&gt;
         &lt;!--
         &lt;endpoint name=&quot;RelayEndpoint&quot;
            contract=&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;
            binding=&quot;netTcpRelayBinding&quot;
            bindingConfiguration=&quot;transportWithMessageCredential&quot;
            behaviorConfiguration=&quot;sharedSecretEndpointBehavior&quot;
            address=&quot;&quot; /&gt;
         --&gt;
      &lt;/client&gt;

   &lt;/system.serviceModel&gt;
&lt;/configuration&gt;
</pre>
<div class="preview">
<pre class="js">&lt;?xml&nbsp;version=<span class="js__string">&quot;1.0&quot;</span>&nbsp;encoding=<span class="js__string">&quot;utf-8&quot;</span>&nbsp;?&gt;&nbsp;
&lt;configuration&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&lt;system.serviceModel&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;behaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;transportClientEndpointBehavior&nbsp;credentialType=<span class="js__string">&quot;SharedSecret&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;sharedSecret&nbsp;issuerName=<span class="js__string">&quot;ISSUER_NAME&quot;</span>&nbsp;issuerSecret=<span class="js__string">&quot;ISSUER_SECRET&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/transportClientEndpointBehavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;serviceCertificate&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;authentication&nbsp;certificateValidationMode=<span class="js__string">&quot;None&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/serviceCertificate&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;behavior&nbsp;name=<span class="js__string">&quot;noCertificateValidationEndpointBehavior&quot;</span>&gt;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;serviceCertificate&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;authentication&nbsp;certificateValidationMode=<span class="js__string">&quot;None&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/serviceCertificate&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/clientCredentials&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behavior&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/endpointBehaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/behaviors&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;bindings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Application&nbsp;Binding&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;netTcpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Default&nbsp;Binding&nbsp;Configuration--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;default&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;Message&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;message&nbsp;clientCredentialType=<span class="js__string">&quot;UserName&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/security&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternate&nbsp;Binding&nbsp;Configuration&nbsp;#<span class="js__num">1</span>:&nbsp;Disabling&nbsp;Client&nbsp;Relay&nbsp;Authentication&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;relayClientAuthenticationNone&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;Message&quot;</span>&nbsp;relayClientAuthenticationType=<span class="js__string">&quot;None&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;message&nbsp;clientCredentialType=<span class="js__string">&quot;UserName&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/security&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternate&nbsp;Binding&nbsp;Configuration&nbsp;#<span class="js__num">2</span>:&nbsp;Transport&nbsp;With&nbsp;Message&nbsp;Credential&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;binding&nbsp;name=<span class="js__string">&quot;transportWithMessageCredential&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;security&nbsp;mode=<span class="js__string">&quot;TransportWithMessageCredential&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;message&nbsp;clientCredentialType=<span class="js__string">&quot;UserName&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/security&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/binding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/netTcpRelayBinding&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&lt;/bindings&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;client&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Default&nbsp;configuration.&nbsp;You&nbsp;must&nbsp;comment&nbsp;out&nbsp;the&nbsp;following&nbsp;declaration&nbsp;whenever&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;you&nbsp;want&nbsp;to&nbsp;use&nbsp;any&nbsp;of&nbsp;the&nbsp;alternate&nbsp;configurations&nbsp;below.&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;default&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternatively&nbsp;use&nbsp;the&nbsp;endpoint&nbsp;configuration&nbsp;below&nbsp;to&nbsp;enable&nbsp;alternate&nbsp;configuration&nbsp;#<span class="js__num">1</span>&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;relayClientAuthenticationNone&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;noCertificateValidationEndpointBehavior&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--&gt;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;Alternatively&nbsp;use&nbsp;the&nbsp;endpoint&nbsp;configuration&nbsp;below&nbsp;to&nbsp;enable&nbsp;alternate&nbsp;configuration&nbsp;#<span class="js__num">2</span>&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;!--&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;endpoint&nbsp;name=<span class="js__string">&quot;RelayEndpoint&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contract=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.IEchoContract&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;binding=<span class="js__string">&quot;netTcpRelayBinding&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bindingConfiguration=<span class="js__string">&quot;transportWithMessageCredential&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;behaviorConfiguration=<span class="js__string">&quot;sharedSecretEndpointBehavior&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;address=<span class="js__string">&quot;&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/client&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&lt;/system.serviceModel&gt;&nbsp;
&lt;/configuration&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div>
<h1><strong>Running the Sample</strong></h1>
<p>Substitute the ISSUER_NAME and ISSUER_SECRET strings in the service and client App.config files with appropriate values.</p>
<p>To generate and install the self-issued cerificate used by the sample, run the setup.bat file included in the sample solution from a Visual Studio command line window running with Administrator privileges.</p>
<p>To run the sample, build the solution in Visual Studio or from the command line, then run the two resulting executables from a command prompt. Start the service first, then start the client application. When the service and the client are running, you can
 start typing messages into the client application. These messages are echoed by the service. After stopping the client and service you can run cleanup.bat from a Visual Studio command line window with Administrator privileges to remove the sample certificate
 from your computer's local store.</p>
<h2><strong>Expected Output &ndash; Client</strong></h2>
</div>
<div>&nbsp;</div>
<div>Enter the Service Namespace you want to connect to: &lt;Service Namespace&gt;<br>
Enter text to echo (or [Enter] to exit): Hello, World!<br>
Server echoed: Hello, World!<br>
<br>
</div>
<div>
<h2><strong>Expected Output &ndash; Service</strong></h2>
</div>
<div>&nbsp;</div>
<div>Enter the Service Namespace you want to connect to: &lt;Service Namespace&gt;<br>
Service address: sb://&lt;serviceNamespace&gt;.servicebus.windows.net/EchoService/</div>
<div>Press [Enter] to exit</div>
<div>Echoing: Hello, World!</div>
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
