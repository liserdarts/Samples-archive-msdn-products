# Lync Server 2013: Log and display processed messages
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2013
* Microsoft Lync Server 2013
## Topics
* Logging
* display messages
## IsPublished
* True
## ModifiedDate
* 2012-09-13 05:55:05
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The SipSnoop application shows how to receive all messages that the Microsoft Lync Server 2013 computer processes. The application displays the messages in a UI and maintains statistics about various SIP messages such as number
 of requests and processed responses.</span></p>
<p><span style="font-size:small">The application uses two application manifests, SipSnoop.am and SipSnoop2.am, to handle the following tasks.</span></p>
<ul>
<li><span style="font-size:small">SipSnoop.am is the basic manifest that handles the following application features:</span>
<ul>
<li><span style="font-size:small">Uses the &lt;allowRegistrationBeforeUserServices/&gt; element to configure the UserServices application run time.</span>
</li><li><span style="font-size:small">Configures server run time through &lt;serverFilter roles=&quot;ALL&quot;/&gt;.</span>
</li><li><span style="font-size:small">Uses the &lt;requestFilter methodNames=&quot;ALL&quot;/&gt; and &lt;responseFilter reasonCodes=&quot;ALL&quot;/&gt; elements to configure how the application receives each request, response, and the corresponding proxy.</span>
</li></ul>
</li><li><span style="font-size:small">SipSnoop2.am uses the <strong>DispatchNotification</strong> function instead of the
<strong>Dispatch</strong> function.</span> </li></ul>
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
<p><span style="font-size:small">&nbsp;</span></p>
