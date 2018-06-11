# Lync Server 2013: Build enhanced access control list
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2013
* Microsoft Lync Server 2013
## Topics
* access control list
## IsPublished
* True
## ModifiedDate
* 2012-09-13 04:48:23
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The EnhancedAllowList application shows how to use a managed SIP application to build an allow/block list onto the enhanced federation-enabled access proxy to support domain authorization and message filtering. In addition,
 the application shows how to limit communications with external domains unless the external domains are authorized explicitly by the administrator or internal users send messages to those domains.</span></p>
<p><span style="font-size:small">Application run-time tasks:</span></p>
<ol>
<li><span style="font-size:small">Maintain an application specific allow/block list that is used to contain known external domains that can be allowed or rejected by the Microsoft SIP Processing Language (MSPL) script.</span>
</li><li><span style="font-size:small">Maintain an application specific unknown domain configuration that is used to control access when the unknown domain is encountered. An unknown domain is one that is not present in the server's default configuration or the
 application specific list.</span> </li><li><span style="font-size:small">Attempt to authorize the source domain for external edge requests by checking it against the internal server list and the application specific list. A message can go through if its source domain is present and marked as allow
 in the internal server list or in the application specific list. Otherwise, the message is rejected.</span>
</li><li><span style="font-size:small">Attempt to authorize the target domain for internal edge requests by checking it against the internal server list and the application specific list. A message can go through if its target domain is present and marked as allow.
 Otherwise, the message is rejected. </span></li><li><span style="font-size:small">Determine the permission for auto-authorization requests from the internal edge of an unknown domain by checking the global configuration in the managed code message handler. If auto authorization is permitted, the domain is
 added to the application specific list and the request can proceed. Additional requests from this domain are automatically allowed.</span>
</li><li><span style="font-size:small">Generate log files, one for each edge.</span> </li></ol>
<p><span style="font-size:small">The Microsoft Lync Server 2013 SDK includes three Lync Server 2013 SIP Application API references that can be used to create Session Initiation Protocol (SIP) server applications that customize and extend the functionality of
 Microsoft Lync Server 2013:</span></p>
<ul>
<li><span style="font-size:small">SIP application manifest</span> </li><li><span style="font-size:small">Microsoft SIP Processing Language (MSPL)</span>
</li><li><span style="font-size:small"><strong>Microsoft.Rtc.Sip </strong>namespace</span>
</li></ul>
<p><span style="font-size:small">The API supports two ways to handle and proxy a SIP message. The simplest way to handle and proxy a message involves using an application manifest and setting the value of the<strong>proxyByDefault</strong> element to
<strong>true</strong>. This amounts to creating a script-only Lync Server 2013 SIP Application API application. The more advanced approach involves using the<strong>Microsoft.Rtc.Sip
</strong>namespace to process messages and manage transactions. </span></p>
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
