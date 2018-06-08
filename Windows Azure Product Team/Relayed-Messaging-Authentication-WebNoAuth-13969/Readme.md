# Relayed Messaging Authentication: WebNoAuth
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
* 2014-09-03 11:26:50
## Description

<h1>Introduction</h1>
<div>This sample demonstrates how to expose an HTTP service that does not require client user authentication.</div>
<div>&nbsp;</div>
<h1>Service</h1>
<p>The service in this sample creates a simple RSS feed and returns it using the .NET Framework RSS/Atom features and the extended &quot;Web-Style&quot; service support.</p>
<p>&nbsp;</p>
<h1>Client</h1>
<div>The client calls the syndication service and writes the feed contents to the console. The program first prompts for the service namespace to which to connect:</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">static void Main(string[] args)
{
      Console.Write(&quot;Enter the name of the Service Namespace you want to connect to: &quot;);
      string serviceNamespace = Console.ReadLine();
</pre>
<div class="preview">
<pre class="js">static&nbsp;<span class="js__operator">void</span>&nbsp;Main(string[]&nbsp;args)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.Write(<span class="js__string">&quot;Enter&nbsp;the&nbsp;name&nbsp;of&nbsp;the&nbsp;Service&nbsp;Namespace&nbsp;you&nbsp;want&nbsp;to&nbsp;connect&nbsp;to:&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;string&nbsp;serviceNamespace&nbsp;=&nbsp;Console.ReadLine();&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="color:black; line-height:115%">The client then transmits a request to the URI of the service.</span></div>
</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">C#</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUri.ToString());
    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

    Stream stream = response.GetResponseStream();
    XmlReader reader = XmlReader.Create(stream);
    Rss20FeedFormatter formatter = new Rss20FeedFormatter();
    formatter.ReadFrom(reader);

    Console.WriteLine(&quot;\nThese are the contents of your feed: &quot;);
    Console.WriteLine(&quot; &quot;);
    Console.WriteLine(formatter.Feed.Title.Text);
    foreach (SyndicationItem item in formatter.Feed.Items)
    {
        Console.WriteLine(item.Title.Text &#43; &quot;: &quot; &#43; item.Summary.Text);
    }
</pre>
<div class="preview">
<pre class="js">HttpWebRequest&nbsp;request&nbsp;=&nbsp;(HttpWebRequest)WebRequest.Create(serviceUri.ToString());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;HttpWebResponse&nbsp;response&nbsp;=&nbsp;(HttpWebResponse)request.GetResponse();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Stream&nbsp;stream&nbsp;=&nbsp;response.GetResponseStream();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;XmlReader&nbsp;reader&nbsp;=&nbsp;XmlReader.Create(stream);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Rss20FeedFormatter&nbsp;formatter&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;Rss20FeedFormatter();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;formatter.ReadFrom(reader);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;\nThese&nbsp;are&nbsp;the&nbsp;contents&nbsp;of&nbsp;your&nbsp;feed:&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="js__string">&quot;&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(formatter.Feed.Title.Text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;foreach&nbsp;(SyndicationItem&nbsp;item&nbsp;<span class="js__operator">in</span>&nbsp;formatter.Feed.Items)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(item.Title.Text&nbsp;&#43;&nbsp;<span class="js__string">&quot;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;item.Summary.Text);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">In the service configuration file, the relayClientAuthenticationType setting controls whether or not a client is required to authenticate when accessing the HTTP service. The setting in this example is
<strong>None</strong>, which means that access is granted to anyone.</div>
</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title">XML</div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>
<pre class="hidden">&lt;webHttpRelayBinding&gt;
  &lt;binding name=&quot;default&quot;&gt;
    &lt;security relayClientAuthenticationType=&quot;None&quot; /&gt;
  &lt;/binding&gt;
&lt;/webHttpRelayBinding&gt;
</pre>
<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;webHttpRelayBinding</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;<span class="xml__tag_start">&lt;binding</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;default&quot;</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;security</span>&nbsp;<span class="xml__attr_name">relayClientAuthenticationType</span>=<span class="xml__attr_value">&quot;None&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/binding&gt;</span>&nbsp;
<span class="xml__tag_end">&lt;/webHttpRelayBinding&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<h1>Building and Running the Sample</h1>
<div><br>
First, build the solution in Visual Studio or from the command line. To run the application, do the following:</div>
<ol>
<li>
<div>From a command prompt, run the service (Service\bin\Debug\Service.exe).</div>
</li><li>
<div>When prompted, type the service namespace, the SAS key name (e.g. &quot;RootManageSharedAcccessKey&quot;) and the SAS key with which you want the service to run. When authorized, the service indicates that it is listening at the configured address.</div>
</li><li>
<div>From another command prompt, run the client (Client\bin\Debug\Client.exe).</div>
</li><li>
<div>When prompted, type the service namespace with which you want the client to connect.</div>
</li><li>
<div>When finished, press Enter to exit the client and the service.</div>
</li></ol>
<div><strong>Note:&nbsp;</strong>You can also point the Web browser to the HTTP URI given below to view the feed contents.&nbsp;&nbsp;</div>
<h2>Expected Output &ndash; Service</h2>
<div>Your Service Namespace: &lt;service-namespace&gt;<br>
Your SAS key name (e.g., &quot;RootManageSharedAccessKey&quot;): &lt;key-name&gt;<br>
Your SAS Key: &lt;key&gt;&nbsp;&nbsp;&nbsp; <br>
Service address: <a href="http://&lt;service-namespace&gt;.servicebus.windows.net/services/SyndicationService/">
http://&lt;service-namespace&gt;.servicebus.windows.net/services/SyndicationService/</a><br>
Press [Enter] to exit</div>
<h2>Expected Output &ndash; Client</h2>
<div>Enter the name of the Service Namespace you want to connect to: &lt;service-namespace&gt;<br>
&nbsp;<br>
These are the contents of your feed:<br>
&nbsp;<br>
Microsoft Windows Azure Service Bus Feed<br>
Day 1: Today I woke up and went to work. It was fun.<br>
Day 2: Today I was sick. I didn't go to work. Instead I stayed home and wrote code all day.<br>
Day 3: This is my third entry. Using Microsoft Windows Azure Service Bus is pretty cool!<br>
Press [Enter] to exit</div>
<div>&nbsp;</div>
