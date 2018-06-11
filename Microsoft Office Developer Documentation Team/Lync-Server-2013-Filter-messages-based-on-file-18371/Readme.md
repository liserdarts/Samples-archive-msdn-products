# Lync Server 2013: Filter messages based on file-based policy
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2013
* Microsoft Lync Server 2013
## Topics
* filter messages
## IsPublished
* True
## ModifiedDate
* 2012-09-13 05:02:04
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The FilteringApp application shows how to use a script-only SIP application to enforce a simple user-based IM filtering policy. The application consists of the application manifest file (Filter.am) and a policy settings file
 (Policy.txt). The policy settings are specified by a list of whitespace-delimited fields of URI, Policy, and Value. The URI field is a string of the &quot;<a href="mailto:user@host">user@host</a>&quot; format. Use an action verb, for example, allow, filter, or drop,
 to describe the Policy field. The Value column is optional and specifies forbidden text that causes the IM message to be dropped if the message content contains the forbidden text.</span></p>
<p><span style="font-size:small">The following logic is implemented by the Microsoft SIP Processing Language (MSPL) script:</span></p>
<ul>
<li><span style="font-size:small">Parse the To header to obtain the targeted SIP URI of an incoming Request instance.</span>
</li><li><span style="font-size:small">Parse the policy settings in the policy file to determine the routing behavior for the targeted user.</span>
</li><li><span style="font-size:small">If the policy action verb for the targeted user is allow, the message is routed back to the server for normal processing by calling ProxyRequest(&quot;&quot;).</span>
</li><li><span style="font-size:small">If the policy action verb for the targeted user is filter and the incoming message content does not contain the specified forbidden text, the message is routed back to the server. Otherwise, the request is rejected with a 403
 response that contains a &quot;Forbidden (Message filtered)&quot; message.</span> </li><li><span style="font-size:small">If the policy action verb is drop or unknown, the request to the targeted user is dropped and a 403 response with an appropriate error message is sent to the sender of the request.</span>
</li></ul>
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
