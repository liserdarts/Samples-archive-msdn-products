# Relayed Messaging: Cloud Trace
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
* 2011-11-16 02:04:56
## Description

<h1>Introduction</h1>
<div>This sample demonstrates a System.Diagnostics.TraceListener which sends tracing information over the Windows Azure Service Bus to a remote receiver.</div>
<h1><br>
Prerequisites</h1>
<div><br>
If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<h1><br>
Running the Sample</h1>
<div><br>
First configure the application configuration (App.config) files of both the TraceConsole and TraceTest applications (see below). Start the TraceConsole application, then start the TraceTest application. The TraceTest application sends tracing information (via
 the Service Bus). The TraceConsole application receives this information and outputs it to the console. As an additional test, you can run the TraceConsole and TraceTest applications on different computers, or even on different networks.</div>
<h1><br>
TraceListener</h1>
<div><br>
In the <strong>TraceListener </strong>project a <strong>System.Diagnostics.TraceListener
</strong>is defined. This listener sends tracing information over the Service Bus using the
<strong>NetEventRelayBinding</strong>.</div>
<h1><br>
TraceConsole</h1>
<div><br>
The <strong>TraceConsole </strong>listens on a <strong>NetEventRelayBinding </strong>
URI and prints the trace information it receives to the console.<br>
You must configure the TraceConsole App.config file with your account information. The following example demonstrates this configuration:</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
&lt;configuration&gt;
  &lt;appSettings&gt;
    &lt;add key=&quot;CloudTraceServicePath&quot; value=&quot;samples/traces&quot;/&gt;
    &lt;add key=&quot;CloudTraceServiceNamespace&quot; value=&quot;myexample&quot;/&gt;
    &lt;add key=&quot;CloudTraceIssuerName&quot; value=&quot;myexample&quot;/&gt;
    &lt;add key=&quot;CloudTraceIssuerSecret&quot; value=&quot;aflqn&#43;lr64pVtENA/UayGC49&#43;ImzwY5EmJHeAQJSnSY=&quot;/&gt;
  &lt;/appSettings&gt;
&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="js">&lt;?xml&nbsp;version=<span class="js__string">&quot;1.0&quot;</span>&nbsp;encoding=<span class="js__string">&quot;utf-8&quot;</span>&nbsp;?&gt;&nbsp;
&lt;configuration&gt;&nbsp;
&nbsp;&nbsp;&lt;appSettings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceServicePath&quot;</span>&nbsp;value=<span class="js__string">&quot;samples/traces&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceServiceNamespace&quot;</span>&nbsp;value=<span class="js__string">&quot;myexample&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceIssuerName&quot;</span>&nbsp;value=<span class="js__string">&quot;myexample&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceIssuerSecret&quot;</span>&nbsp;value=<span class="js__string">&quot;aflqn&#43;lr64pVtENA/UayGC49&#43;ImzwY5EmJHeAQJSnSY=&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&lt;/appSettings&gt;&nbsp;
&lt;/configuration&gt;</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">TraceTest</h1>
</div>
<div>The TraceTest project adds the CloudTrace listener to its list of active trace listeners and generates sample tracing data.</div>
<div>You must add your account information to the TraceTest App.config file. You can refer to the TraceListener configuration file. The following example demonstrates this configuration:</div>
<div><span style="font-family:Consolas; font-size:x-small"><span style="font-family:Consolas; font-size:x-small">
<div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
&lt;configuration&gt;
  &lt;appSettings&gt;
    &lt;add key=&quot;CloudTraceServicePath&quot; value=&quot;samples/traces&quot;/&gt;
    &lt;add key=&quot;CloudTraceServiceNamespace&quot; value=&quot;myexample&quot;/&gt;
    &lt;add key=&quot;CloudTraceIssuerName&quot; value=&quot;myexample&quot;/&gt;
    &lt;add key=&quot;CloudTraceIssuerSecret&quot; value=&quot;aflqn&#43;lr64pVtENA/UayGC49&#43;ImzwY5EmJHeAQJSnSY=&quot;/&gt;
  &lt;/appSettings&gt;

  &lt;system.diagnostics&gt;
    &lt;trace&gt;
      &lt;listeners&gt;
        &lt;add name=&quot;CloudTrace&quot; type=&quot;Microsoft.ServiceBus.Samples.CloudTraceListener,Microsoft.ServiceBus.Samples.CloudTraceListener&quot;&gt;&lt;/add&gt;
      &lt;/listeners&gt;
    &lt;/trace&gt;
  &lt;/system.diagnostics&gt;

&lt;/configuration&gt;</pre>
<div class="preview">
<pre class="js">&lt;?xml&nbsp;version=<span class="js__string">&quot;1.0&quot;</span>&nbsp;encoding=<span class="js__string">&quot;utf-8&quot;</span>&nbsp;?&gt;&nbsp;
&lt;configuration&gt;&nbsp;
&nbsp;&nbsp;&lt;appSettings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceServicePath&quot;</span>&nbsp;value=<span class="js__string">&quot;samples/traces&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceServiceNamespace&quot;</span>&nbsp;value=<span class="js__string">&quot;myexample&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceIssuerName&quot;</span>&nbsp;value=<span class="js__string">&quot;myexample&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;CloudTraceIssuerSecret&quot;</span>&nbsp;value=<span class="js__string">&quot;aflqn&#43;lr64pVtENA/UayGC49&#43;ImzwY5EmJHeAQJSnSY=&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&lt;/appSettings&gt;&nbsp;
&nbsp;
&nbsp;&nbsp;&lt;system.diagnostics&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;trace&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;listeners&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;CloudTrace&quot;</span>&nbsp;type=<span class="js__string">&quot;Microsoft.ServiceBus.Samples.CloudTraceListener,Microsoft.ServiceBus.Samples.CloudTraceListener&quot;</span>&gt;&lt;/add&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/listeners&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/trace&gt;&nbsp;
&nbsp;&nbsp;&lt;/system.diagnostics&gt;&nbsp;
&nbsp;
&lt;/configuration&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
</div>
</span>
<div>
<div>
<div class="endscriptcode"></div>
</div>
</div>
</span>
<div>
<div>
<div class="endscriptcode"></div>
</div>
</div>
</div>
<div>&nbsp;</div>
<div>&nbsp;</div>
