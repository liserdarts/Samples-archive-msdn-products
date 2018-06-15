# Service Bus WinRT and Mobile Services integration - Lottery
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* Service Bus
* Windows Azure Service Bus
* Windows Azure Mobile Services
## Topics
* Messaging
* Publish Subscribe
## IsPublished
* True
## ModifiedDate
* 2012-11-08 06:50:19
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Service Bus Lottery sample is a simple application that
</span><span style="font-size:small">showcases the Azure Messaging WinRT client library and&nbsp;interactions between Azure Mobile Services and Service Bus.<br>
</span><span style="font-size:small">The app uses a table in Azure Mobile Services called
</span><span style="font-size:small">&ldquo;Prizes&rdquo;. Various clients can submit prizes to be distributed. The distribution
</span><span style="font-size:small">happens via a Service Bus Queue that load balances the messages across all
</span><span style="font-size:small">clients that are currently connected.<br>
</span><span style="font-size:small">Messages are sent through the azure node.js module from the
</span><span style="font-size:small">Insert script of the Prizes table inside Mobile Services. Client uses the
</span><span style="font-size:small">Mobile Services Messaging SDK to start a receiving loop as soon as the app is
</span><span style="font-size:small">started.</span></p>
<h1>&nbsp;<img id="70352" src="70352-sbzumo.png" alt="" width="727" height="499"><span>Building the Sample</span></h1>
<p><span style="font-size:small">If you haven&rsquo;t </span><span style="font-size:small">already, we strongly recommend you look at the
<a href="https://www.windowsazure.com/en-us/develop/net/how-to-guides/service-bus-queues/">
Service Bus Queues tutorial</a> to understand the Service Bus feature we will be using
</span><span style="font-size:small">in the sample, and the <a href="http://go.microsoft.com/fwlink/?LinkID=262854&clcid=0x409">
Mobile Services getting started guide</a> to familiarize yourself with Mobile</span><br>
<span style="font-size:small">Services.</span></p>
<ol>
<li><span style="font-size:small">Make sure you have the necessary</span><span style="font-size:small">&nbsp; pre-requisites:</span>
<ul>
<li><span style="font-size:small">Windows Azure account and access to the Mobile </span>
<span style="font-size:small">Servies preview. <a href="http://aka.ms/mobileservices" target="_blank">
Sign up<br>
for the Free Trial</a></span> </li><li><span style="font-size:small"><a href="http://go.microsoft.com/fwlink/?LinkId=257546" target="_blank">Visual Studio 2012 Express for Windows 8</a></span>
</li><li><span style="font-size:small"><a href="http://go.microsoft.com/fwlink/?LinkID=257545&clcid=0x409" target="_blank">Mobile Services SDK</a></span>
</li></ul>
</li><li><span style="font-size:small">Open a browser and sign&nbsp;</span><span style="font-size:small">in to the
<a href="https://manage.windowsazure.com/">Windows Azure management portal</a>,&nbsp;</span><span style="font-size:small">click on the
<strong>New</strong> button at the bottom left, select <strong>App Services</strong>, choose
<strong>Service Bus Queue</strong> and select <strong>Quick</strong> <strong>Create</strong>. Specify a&nbsp;</span><span style="font-size:small">queue name and a region.</span>
</li><li><span style="font-size:small">Click again on the <strong>New&nbsp;</strong></span><span style="font-size:small">button at the bottom left, choose
<strong>Mobile Service</strong> and select <strong>Create</strong>.&nbsp;</span><span style="font-size:small">Complete the steps to create your application.</span>
</li><li><span style="font-size:small">Click on the <strong>Data</strong></span><span style="font-size:small"> tab and create a table called
<strong>Prizes</strong>.</span> </li><li><span style="font-size:small">Open the <strong>ServerScripts</strong></span><span style="font-size:small"> folder inside the download zip and paste the contents of Prizes_Insert.js&nbsp;i</span><span style="font-size:small">nto the insert script of the
 table Prizes in the Portal.</span> </li><li><span style="font-size:small">In the script replace the following placeholders:</span>&nbsp;
<ul>
<li><span style="font-size:small">&lt;namespace&nbsp;</span><span style="font-size:small">name&gt;: the name of the Service Bus Namespace as reported in the
<strong>All Items</strong> page of the portal.</span> </li><li><span style="font-size:small">&lt;namespace key&gt;:&nbsp;</span><span style="font-size:small">the
<strong>Default Key</strong> found by&nbsp;</span><span style="font-size:small">clicking on
<strong>Access Key</strong> at the&nbsp;</span><span style="font-size:small">bottom the namespace detail page.&nbsp;</span>
</li><li><span style="font-size:small">&lt;queue name&gt;:&nbsp;</span><span style="font-size:small">the name of the queue created at step 2</span>
</li></ul>
</li><li><span style="font-size:small">Open the solution (.sln</span><span style="font-size:small"> file) inside the zip with Visual Studio 2012 (make sure you have first&nbsp;</span><span style="font-size:small">extracted the zip file).</span>
</li><li><span style="font-size:small">Configure your client app to point at your new</span><span style="font-size:small"> mobile service endpoint. Inside the Window Azure management portal,
</span><span style="font-size:small">navigate to the Quick Start in your application:<br>
</span><img id="70301" src="70301-mobile.png" alt="" width="405" height="372">
</li><li><span style="font-size:small">Click &ldquo;Connect an existing Windows 8 application&rdquo;&nbsp;</span><span style="font-size:small">and copy the url and application key from the code sample. Open App.xaml.cs&nbsp;</span><span style="font-size:small">and
 paste the url and key into the appropriate fields.</span> </li><li><span style="font-size:small">Open MainPage.xaml.cs Replace the following&nbsp;</span><span style="font-size:small">placeholders:</span>
<ul>
<li><span style="font-size:small">&lt;queue name&gt;: with the queue name created&nbsp;</span><span style="font-size:small">at step 2</span>
</li><li><span style="font-size:small">&lt;service bus connection string&gt;: the <strong>
Connection String</strong> found by&nbsp;</span><span style="font-size:small">clicking on
<strong>Access Key</strong> at the&nbsp;</span><span style="font-size:small">bottom the namespace detail page.</span>
</li></ul>
</li><li><span style="font-size:small">Build and run the app&nbsp;</span><span style="font-size:small">in Visual Studio</span>
</li></ol>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">This sample demonstrates how you can send messages from a moblie services script using the Azure Node.js SDK</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">function insert(item, user, request) {
    var insertInQueue = function() {
        var azure = require('azure');
        var serviceBusService = azure.createServiceBusService('&lt;namespace name&gt;', '&lt;namespace key&gt;');

        serviceBusService.createQueueIfNotExists('&lt;queue name&gt;', function(error) {
            if (!error) {
                serviceBusService.sendQueueMessage('&lt;queue name&gt;', '&quot;' &#43; item.text &#43; '&quot;', function(error) {
                    if (!error) {
                        console.log('sent message: ' &#43; item.id);
                    }
                });
            }
        });
    };

    request.execute({
        success: function () {
            insertInQueue();
            request.respond();
        }
    });
}</pre>
<div class="preview">
<pre class="js"><span class="js__operator">function</span>&nbsp;insert(item,&nbsp;user,&nbsp;request)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;insertInQueue&nbsp;=&nbsp;<span class="js__operator">function</span>()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;azure&nbsp;=&nbsp;require(<span class="js__string">'azure'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;serviceBusService&nbsp;=&nbsp;azure.createServiceBusService(<span class="js__string">'&lt;namespace&nbsp;name&gt;'</span>,&nbsp;<span class="js__string">'&lt;namespace&nbsp;key&gt;'</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceBusService.createQueueIfNotExists(<span class="js__string">'&lt;queue&nbsp;name&gt;'</span>,&nbsp;<span class="js__operator">function</span>(error)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(!error)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;serviceBusService.sendQueueMessage(<span class="js__string">'&lt;queue&nbsp;name&gt;'</span>,&nbsp;<span class="js__string">'&quot;'</span>&nbsp;&#43;&nbsp;item.text&nbsp;&#43;&nbsp;<span class="js__string">'&quot;'</span>,&nbsp;<span class="js__operator">function</span>(error)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(!error)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(<span class="js__string">'sent&nbsp;message:&nbsp;'</span>&nbsp;&#43;&nbsp;item.id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;request.execute(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;success:&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;insertInQueue();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;request.respond();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<p><span style="font-size:small">The Windows store app sample demonstrates how to receive messages from a Service Bus Queue:</span></p>
<h1><span>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// this code connects to the Service Bus queue.
prizesQ = new Queue(&quot;&lt;queue name&gt;&quot;, &quot;&lt;service bus connection string&gt;&quot;);

//receive messages in a Async method
    while (true)
    {
        try
        {
            String prize = await prizesQ.ReceiveAsync&lt;String&gt;();
            // process the message here
         } 
         catch (MessagingException)
         {
            // we need to catch exception thrown when no message is retrieved.
          }
       }</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;this&nbsp;code&nbsp;connects&nbsp;to&nbsp;the&nbsp;Service&nbsp;Bus&nbsp;queue.</span>&nbsp;
prizesQ&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Queue(<span class="cs__string">&quot;&lt;queue&nbsp;name&gt;&quot;</span>,&nbsp;<span class="cs__string">&quot;&lt;service&nbsp;bus&nbsp;connection&nbsp;string&gt;&quot;</span>);&nbsp;
&nbsp;
<span class="cs__com">//receive&nbsp;messages&nbsp;in&nbsp;a&nbsp;Async&nbsp;method</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(<span class="cs__keyword">true</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;String&nbsp;prize&nbsp;=&nbsp;await&nbsp;prizesQ.ReceiveAsync&lt;String&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;process&nbsp;the&nbsp;message&nbsp;here</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(MessagingException)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;we&nbsp;need&nbsp;to&nbsp;catch&nbsp;exception&nbsp;thrown&nbsp;when&nbsp;no&nbsp;message&nbsp;is&nbsp;retrieved.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
</span>More Information</h1>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/microsoft.windowsazure.messaging.aspx">Windows Azure Service Bus WinRT Library reference</a><br>
<a href="http://www.windowsazure.com/en-us/develop/net/fundamentals/hybrid-solutions/">Windows Azure Service Bus</a><br>
<a href="http://www.windowsazure.com/en-us/develop/mobile/">Windows Azure Mobile Services</a><br>
<a href="https://github.com/WindowsAzure/azure-sdk-for-node">Windows Azure Node.js SDK</a><br>
<a href="http://www.windowsazure.com/en-us/">Windows Azure</a></span></p>
