# Relayed Messaging; Windows Azure
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
* 2011-11-16 02:17:13
## Description

<h1>Introduction</h1>
<div>This sample demonstrates how to run a Windows Azure Service Bus client and service on Windows Azure.</div>
<div>This sample configure Service Bus programmatically. Only environment and security information is stored in the configuration files. Also, these samples package the Microsoft.ServiceBus.dll (note that
<strong>Copy Local</strong> is set to <strong>True </strong>for the <strong>Assembly Reference
</strong>setting).</div>
<div>&nbsp;</div>
<h1>Prerequisites</h1>
<div>To run this sample on .Net Framework 4, you must install the Windows Azure SDK 1.5. You would also need the Windows Azure Tools for Visual Studio 1.5 for working with this sample in Visual Studio 2010. This sample works on the local developer fabric (included
 in the Windows Azure SDK) and also in the Windows Azure cloud service. To run the sample in the cloud service, you must also have a valid Windows Azure account. More information about Windows Azure can be found here:
<a href="https://windows.azure.com">https://windows.azure.com</a>. Please note that the Windows Azure SDK also has a number of its own pre-requisites (including IIS and SQL Express).</div>
<div>&nbsp;</div>
<h1>Running the Sample</h1>
<div><br>
You must start Visual Studio in elevated (administrator) mode. Right-click on Visual Studio and then click
<strong>Run as Administrator</strong>. This is required by the Windows Azure simulation environment.</div>
<div>Configure the Web.config and App.config files for the Web and Worker Roles, respectively. Then run the WindowsAzureEcho project in the Windows Azure local developer fabric or package and deploy the solution to the Windows Azure service. For more information
 about running Windows Azure applications locally or in the cloud please refer to the Windows Azure documentation at
<a href="https://windows.azure.com">https://windows.azure.com</a>.</div>
<div>&nbsp;</div>
<h1>WebRole</h1>
<div><br>
The WebRole application sends data over the Service Bus to a listener. You can add your service namespace information to the Web.config file of the WorkerRole applications:</div>
<div><br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden"> &lt;appSettings&gt;
    &lt;add key=&quot;ServicePath&quot; value=&quot;samples/echo&quot;/&gt;
    &lt;add key=&quot;ServiceNamespace&quot; value=&quot;MY_SERVICE_NAMESPACE&quot;/&gt;
    &lt;add key=&quot;IssuerName&quot; value=&quot;owner&quot;/&gt;
    &lt;add key=&quot;IssuerSecret&quot; value=&quot;MY_ISSUER_SECRET&quot;/&gt;
&lt;/appSettings&gt;</pre>
<div class="preview">
<pre class="js">&nbsp;&lt;appSettings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;ServicePath&quot;</span>&nbsp;value=<span class="js__string">&quot;samples/echo&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;ServiceNamespace&quot;</span>&nbsp;value=<span class="js__string">&quot;MY_SERVICE_NAMESPACE&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;IssuerName&quot;</span>&nbsp;value=<span class="js__string">&quot;owner&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;IssuerSecret&quot;</span>&nbsp;value=<span class="js__string">&quot;MY_ISSUER_SECRET&quot;</span>/&gt;&nbsp;
&lt;/appSettings&gt;</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">&nbsp;WorkerRole</h1>
</div>
<div><br>
The WorkerRole application listens to the Service Bus and writes data to the Windows Azure log. You can add your service namespace information to the WorkerRole App.config file:</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;appSettings&gt;
    &lt;add key=&quot;ServicePath&quot; value=&quot;samples/echo&quot;/&gt;
    &lt;add key=&quot;ServiceNamespace&quot; value=&quot;MY_SERVICE_NAMESPACE&quot;/&gt;
    &lt;add key=&quot;IssuerName&quot; value=&quot;owner&quot;/&gt;
    &lt;add key=&quot;IssuerSecret&quot; value=&quot;MY_ISSUER_SECRET&quot;/&gt;
&lt;/appSettings&gt;</pre>
<div class="preview">
<pre class="js">&lt;appSettings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;ServicePath&quot;</span>&nbsp;value=<span class="js__string">&quot;samples/echo&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;ServiceNamespace&quot;</span>&nbsp;value=<span class="js__string">&quot;MY_SERVICE_NAMESPACE&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;IssuerName&quot;</span>&nbsp;value=<span class="js__string">&quot;owner&quot;</span>/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;key=<span class="js__string">&quot;IssuerSecret&quot;</span>&nbsp;value=<span class="js__string">&quot;MY_ISSUER_SECRET&quot;</span>/&gt;&nbsp;
&lt;/appSettings&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div>&nbsp;</div>
