# Service Bus: Message Browse
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Service Bus
* Windows Azure Service Bus
## Topics
* Service Bus
* Windows Azure Service Bus
## IsPublished
* True
## ModifiedDate
* 2013-04-30 02:46:03
## Description

<h1>Message Browse</h1>
<p><span style="font-size:small">This sample demonstrates how to use the Windows Azure Service Bus messages peek feature. See the Service Bus documentation for more information about the Service Bus before exploring the samples.</span></p>
<p><span style="font-size:small">This sample demonstrates how to use the messages peek feature to browse the content of a queue or subscription. The sample asks for connection string as well as entity path and prints out the content of the Service Bus entity.</span></p>
<h1><span>Instructions</span></h1>
<p><span style="font-size:small">If you haven't already done so, please read the release notes document that explains how to sign up for a Windows Azure account and how to configure your environment.&nbsp;
</span></p>
<p><span style="font-size:small">To run the sample:</span></p>
<ol>
<li><span style="font-size:small">Build the solution in Visual Studio and run the sample&nbsp;project.</span>
</li><li><span style="font-size:small">When prompted enter following information:</span>
</li></ol>
<ul>
<li>&nbsp;
<ul>
<li><span style="font-size:small">Service bus connection&nbsp;string;</span> </li><li><span style="font-size:small">Path to&nbsp;entity to&nbsp;browse;</span> </li></ul>
</li></ul>
<p>&nbsp;</p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><span style="font-size:small">The sample flows in the following manner:</span></p>
<ol>
<li><span style="font-size:small">Sample asks for connection string and Service bus&nbsp;entity
</span></li><li><span style="font-size:small">A MessageReceiver is created.</span> </li><li><span style="font-size:small">Iterate over all messages in the entity using the Peek&nbsp;method.</span>
</li></ol>
<ol>
<li>&nbsp;
<ol>
<li><span style="font-size:small">Print some of the built&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; in properties: Time ; Label ; Sequence number</span>
</li><li><span style="font-size:small">Print the custom&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; message properties</span>
</li><li><span style="font-size:small">Print message body</span> </li></ol>
</li></ol>
<p><em>&nbsp;&nbsp;</em></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">MessagingFactory messagingFactory = MessagingFactory.CreateFromConnectionString(ServiceBusConnectionString);
MessageReceiver messageReciever = messagingFactory.CreateMessageReceiver(Program.ServiceBusentityPath);

BrokeredMessage msg;
while (true)
{
    msg = messageReciever.Peek();
    if (msg != null)
    {
        &lt;process message&gt;
    }
    else
    {
        break;
    }
}
</pre>
<div class="preview">
<pre class="csharp">MessagingFactory&nbsp;messagingFactory&nbsp;=&nbsp;MessagingFactory.CreateFromConnectionString(ServiceBusConnectionString);&nbsp;
MessageReceiver&nbsp;messageReciever&nbsp;=&nbsp;messagingFactory.CreateMessageReceiver(Program.ServiceBusentityPath);&nbsp;
&nbsp;
BrokeredMessage&nbsp;msg;&nbsp;
<span class="cs__keyword">while</span>&nbsp;(<span class="cs__keyword">true</span>)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;msg&nbsp;=&nbsp;messageReciever.Peek();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(msg&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;process&nbsp;message&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Program.cs&nbsp;- main sample source code</em> </li><li><em><em>app.config&nbsp;- config file (enter connection string information here)</em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/windowsazure/microsoft.servicebus.messaging.messagereceiver.aspx" target="_blank">MessageReceiver class</a></span>
</li></ul>
