# Lync Server 2013: Maintain topology-aware message logs
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2013
* Microsoft Lync Server 2013
## Topics
* Logging
* message logs
## IsPublished
* True
## ModifiedDate
* 2012-09-13 02:35:51
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The Archiver application shows how to use a managed SIP application to build a topology-aware SIP application that uses message stamping for multi-homed application communications. Each Archiver instance logs messages that are
 not yet stored by any instance on another server. The logged requests and responses are stamped to notify downstream Archiver instances that additional logging is not necessary.</span></p>
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
