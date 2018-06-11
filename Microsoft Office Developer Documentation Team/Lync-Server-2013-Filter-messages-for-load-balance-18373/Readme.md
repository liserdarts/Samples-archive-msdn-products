# Lync Server 2013: Filter messages for load balance using file-based policy
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2013
* Microsoft Lync Server 2013
## Topics
* filter messages
* load balance
## IsPublished
* True
## ModifiedDate
* 2012-09-13 05:29:22
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The PublicIM application shows how to use a use a script-only SIP application to route incoming SIP messages from external networks to an internal Microsoft Lync Server 2013 computer.</span></p>
<p><span style="font-size:small">As a routing application, the <strong>strictRoute</strong> attribute must be set to
<strong>false</strong> on the <strong>responseFilter</strong> element in the application manifest.</span></p>
<p><span style="font-size:small">The following logic is implemented by the Microsoft SIP Processing Language (MSPL) script:</span></p>
<p><span style="font-size:small">1.&nbsp;Verify that a SIP request originates from an external network by comparing the sipRequest.Origin against Messageorigin.NetworkExternal.
</span><br>
<span style="font-size:small">2.&nbsp;Selects a fully qualified domain name (FQDN) of a computer that is running Lync Server 2013. The selection is based on a hash string of the SIP URI of the requester.</span><br>
<span style="font-size:small">3.&nbsp;Forward the incoming request to the selected server by calling the
<strong>ProxyRequest</strong> function with a new SIP URI that is constructed by using the following format: &quot;&lt;original request Uri&gt;;maddr=&lt;selected server address&gt;;transport=tls&quot;.</span><br>
<span style="font-size:small">4.&nbsp;For other messages, forward the message as is, by calling ProxyRequest(&quot;&quot;) or ProxyResponse().</span></p>
<p><span style="font-size:small">You may need to change occurrences of Log(&quot;Debug&quot;, &hellip;); in the application manifest to Log(&quot;Debugr&quot;, &hellip;);, if they are not already updated.</span></p>
<p><span style="font-size:small">The Microsoft Lync Server 2013 SDK includes three Lync Server 2013 SIP Application API references that can be used to create Session Initiation Protocol (SIP) server applications that customize and extend the functionality of
 Microsoft Lync Server 2013:</span></p>
<ul>
<li><span style="font-size:small">SIP application manifest</span> </li><li><span style="font-size:small">Microsoft SIP Processing Language (MSPL)</span>
</li><li><span style="font-size:small"><strong>Microsoft.Rtc.Sip </strong>namespace</span>
</li></ul>
<p><span style="font-size:small">The API supports two ways to handle and proxy a SIP message. The simplest way to handle and proxy a message involves using an application manifest and setting the value of the
<strong>proxyByDefault</strong> element to <strong>true</strong>. This amounts to creating a script-only Lync Server 2013 SIP Application API application. The more advanced approach involves using the
<strong>Microsoft.Rtc.Sip </strong>namespace to process messages and manage transactions.
</span></p>
<p><span style="font-size:small">The Lync Server 2013 SDK is intended for the following audiences:</span></p>
<ul>
<li><span style="font-size:small">Developers who want to use application manifests and MSPL scripts to implement simple custom SIP message filtering and routing on computers in a Lync Server 2013 deployment.</span>
</li><li><span style="font-size:small">Experienced SIP developers who want to create SIP-based managed code server applications that implement real-time content delivery or instant messaging infrastructure. This includes applications that work directly with SIP
 transaction objects or support multithreaded transactions.</span> </li></ul>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/gg455051">Lync (MSDN Library)</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/lync/gg132942.aspx">Lync Developer Center</a></span>
</li><li><span style="font-size:small"><a href="http://www.microsoft.com/resources/msdn/en-us/office/media/video/video.html?cid=ldc&from=mscomldc&VideoID=522f8500-03ec-46db-968d-871945535571">Video: How to use a chat room</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh506337.aspx">Office 365 Developer Hub</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905340.aspx">Office Developer Center</a><br>
</span></li></ul>
