# Brokered Messaging: PowerShell Provider
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
* 2011-11-15 11:22:22
## Description

<h1>Introduction</h1>
<div>This sample demonstrates how to use Windows PowerShell to:</div>
<ul>
<li>Navigate your Service Bus service namespace as you would navigate the file system
</li><li>Use built-in cmdlets to create and delete queues, topics, subscriptions, and rules
</li><li>Use custom cmdlets to send and receive messages. </li></ul>
<h1><br>
Prerequisites</h1>
<div><br>
If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.</div>
<div>&nbsp;</div>
<h1>Setup the ServiceBus Module</h1>
<div><br>
1.Build the ServiceBusPowerShellProvider solution</div>
<div>2. Create a folder for your module under %PSModulePath% C:\Windows\system32\WindowsPowerShell\v1.0\Modules\ServiceBus</div>
<div>3. Copy your debug folder to the folder you just created: copy %PathToSDK%\ServiceBusPowerShellProvider\bin\Debug\* %PSModulePath%\ServiceBus\</div>
<div>4. Open a powershell window</div>
<div>5. Import the module you just created</div>
<div>Import-Module ServiceBus</div>
<div>6. Create a PSDrive</div>
<div>new-psdrive -name ServiceBus -root c:\ps -psprovider ServiceBusPowerShellProvider</div>
<div>7. Move to the PSDrive location you just created</div>
<div>set-location ServiceBus:\</div>
<div>&nbsp;</div>
<h1>PowerShell Output</h1>
<div>Windows PowerShell Copyright (C) 2009 Microsoft Corporation. All rights reserved.
<br>
PS C:\&gt; Import-Module ServiceBus PS C:\&gt; new-psdrive -name ServiceBus -root c:\ps -psprovider ServiceBusPowerShellProvider
<br>
Name Used (GB) Free (GB) Provider &nbsp;&nbsp;Root CurrentLocation <br>
---- --------- --------- -------- &nbsp;&nbsp;---- --------------- <br>
ServiceBus &nbsp;&nbsp;ServiceBus... &nbsp;c:\ps <br>
PS C:\&gt; set-location ServiceBus:\ <br>
PS ServiceBus:\&gt;</div>
<div>There is a sample shortcut that already does the above, so next time you can simply use that. It is convenient to create it by hand the first time, in case any issues arise.<br>
It is possible that you will encounter problems with the .Net version that PowerShell loads by default. If that is the case, you need to add a configuration file to C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe.config. The file should appear as
 follows:</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt; &lt;configuration&gt; 
        &lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt; 
            &lt;supportedRuntime version=&quot;v4.0.30319&quot;/&gt; 
            &lt;supportedRuntime version=&quot;v2.0.50727&quot;/&gt; 
        &lt;/startup&gt; 
    &lt;/configuration&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;?xml</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">&quot;1.0&quot;</span>&nbsp;<span class="xml__attr_name">encoding</span>=<span class="xml__attr_value">&quot;utf-8&quot;</span>&nbsp;<span class="xml__tag_start">?&gt;</span>&nbsp;<span class="xml__tag_start">&lt;configuration</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;startup</span>&nbsp;<span class="xml__attr_name">useLegacyV2RuntimeActivationPolicy</span>=<span class="xml__attr_value">&quot;true&quot;</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;supportedRuntime</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">&quot;v4.0.30319&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;supportedRuntime</span>&nbsp;<span class="xml__attr_name">version</span>=<span class="xml__attr_value">&quot;v2.0.50727&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/startup&gt;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_end">&lt;/configuration&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<h1>Use the ServiceBus Module</h1>
<div><br>
1. Set your servicebus credentials <br>
<strong>&nbsp;</strong></div>
<div><strong>Powershell Output: </strong></div>
<div>&nbsp;</div>
<div>Set-ServiceBusCredentials -IssuerName &lt;issuer_name&gt; -Namespace &lt;service_namespace&gt; -IssuerSecret &lt;issuer_secret&gt;</div>
<div>&nbsp;</div>
<div>2. Navigate through the environment</div>
<div><strong>&nbsp;</strong>&nbsp;</div>
<div><strong>Powershell Output:</strong></div>
<div>&nbsp;</div>
<div>PS ServiceBus:\&gt; dir</div>
<div>&nbsp;</div>
<div>Queues <br>
Topics</div>
<div>&nbsp;</div>
<div>PS ServiceBus:\&gt; cd Queues <br>
PS ServiceBus:\Queues&gt; dir <br>
PS ServiceBus:\Queues&gt;</div>
<div>&nbsp;</div>
<div>3. Create a new queue</div>
<div><strong></strong>&nbsp;</div>
<div><strong>Powershell Output:</strong></div>
<div>&nbsp;</div>
<div>PS ServiceBus:\Queues&gt; new-item demoqueue <br>
PS ServiceBus:\Queues&gt; dir</div>
<div>&nbsp;</div>
<div>PSPath : Microsoft.ServiceBus.Samples.ServiceBusPowerShellPSSnapIn\ServiceBusPowerShell Provider::c:\ps\Queues\demoqueue
<br>
PSParentPath : Microsoft.ServiceBus.Samples.ServiceBusPowerShellPSSnapIn\ServiceBusPowerShell Provider::c:\ps\Queues
<br>
PSChildName : demoqueue PSDrive : ServiceBus <br>
PSProvider : Microsoft.ServiceBus.Samples.ServiceBusPowerShellPSSnapIn\ServiceBusPowerShell Provider
<br>
PSIsContainer : False NamespaceClient : Microsoft.ServiceBus.ServiceBusNamespaceClient
<br>
Path : demoqueue <br>
LockDuration : 00:00:30 <br>
MaxQueueSizeInBytes : 104857600 <br>
RequiresDuplicateDetection : False <br>
RequiresSession : False <br>
DefaultMessageTimeToLive : 10675199.02:48:05.4775807 <br>
EnableDeadLetteringOnMessageExpiration : False<br>
DuplicateDetectionHistoryTimeWindow : 00:10:00 <br>
ExtensionData : System.Runtime.Serialization.ExtensionDataObject</div>
<div>&nbsp;</div>
<div>4. Send/Receive messages from queue</div>
<div><strong></strong>&nbsp;</div>
<div><strong>Powershell Output:</strong></div>
<div>&nbsp;</div>
<div>PS ServiceBus:\Queues&gt; Send-Message -To demoqueue -Message message1 <br>
PS ServiceBus:\Queues&gt; Send-Message -To demoqueue -Message message2 <br>
PS ServiceBus:\Queues&gt; Receive-Message -From demoqueue</div>
<div>&nbsp;</div>
<div>CorrelationId : <br>
SessionId : <br>
ReplyToSessionId : <br>
DeliveryCount : 0 <br>
ExpiresAtUtc : 12/31/9999 11:59:59 PM <br>
LockedUntilUtc : 4/27/2011 11:51:10 PM <br>
LockToken : 5ca26ecf-45e9-4399-923f-d3a65c35707c <br>
MessageId : 55b08788753a49b6951e20949e358921 <br>
MessageReceipt : Microsoft.ServiceBus.Messaging.MessageReceipt <br>
ContentType : <br>
Label : <br>
Properties : {[Body, message1]} <br>
ReplyTo : <br>
EnqueuedTimeUtc : 4/27/2011 11:50:21 PM <br>
ScheduledEnqueueTimeUtc : 1/1/0001 12:00:00 AM <br>
SequenceNumber : 1 <br>
Size : 71 <br>
TimeToLive : 10675199.02:48:05.4775807 <br>
To :</div>
<div>&nbsp;</div>
<div>PS ServiceBus:\Queues&gt; $message = Receive-Message -From demoqueue -PeekLock
<br>
PS ServiceBus:\Queues&gt; $message</div>
<div>&nbsp;</div>
<div>CorrelationId : <br>
SessionId : <br>
ReplyToSessionId : <br>
DeliveryCount : 0 <br>
ExpiresAtUtc : 12/31/9999 11:59:59 PM <br>
LockedUntilUtc : 4/27/2011 11:51:23 PM <br>
LockToken : bb0c842d-c432-4c0c-bfd9-49aae8599434 <br>
MessageId : fa5c8d6c0c8e45a7b37b79ba3d4dafac <br>
MessageReceipt : Microsoft.ServiceBus.Messaging.MessageReceipt <br>
ContentType : <br>
Label : <br>
Properties : {[Body, message2]} <br>
ReplyTo : <br>
EnqueuedTimeUtc : 4/27/2011 11:50:24 PM <br>
ScheduledEnqueueTimeUtc : 1/1/0001 12:00:00 AM <br>
SequenceNumber : 2 Size : 71 <br>
TimeToLive : 10675199.02:48:05.4775807 <br>
To :</div>
<div>&nbsp;</div>
<div>PS ServiceBus:\Queues&gt; $message.Complete() <br>
PS ServiceBus:\Queues&gt;</div>
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
<div></div>
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
