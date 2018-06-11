# Windows Filtering Platform Traffic Inspection Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WFP
* Windows Driver
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:17:23
## Description

<div id="mainSection">
<p>This sample driver demonstrates the traffic inspection capabilities of the Windows Filtering Platform (WFP).
</p>
<p>The sample driver consists of a kernel-mode Windows Filtering Platform (WFP) callout driver (Inspect.sys) that intercepts all transport layer traffic (for example, Transmission Control Protocol (TCP), User Datagram Protocol (UDP), and nonerror Internet Control
 Message Protocol (ICMP)) sent to or received from a configurable remote peer and queues then to a worker thread for out-of-band processing.</p>
<p>Inspect.sys inspects inbound and outbound connections and all packets that belong to those connections. Additionally, Inspect.sys demonstrates the special considerations that are required to be compatible with Internet Protocol security (IPsec) in Windows
 Vista and Windows Server 2008.</p>
<p>Inspect.sys implements the <code>ClassifyFn</code> callout functions for the ALE Connect, Recv-Accept, and Transport callouts. In addition, the system worker thread that performs the actual packet inspection is also implemented along with the event mechanisms
 that are shared between the Classify function and the worker thread.</p>
<p>Connect/Packet inspection is done out-of-band by a system worker thread by using the reference-drop-clone-reinject mechanism as well as the ALE pend/complete mechanism. Therefore, the sample can serve as a basis for scenarios in which a filtering decision
 cannot be made within the <code>classifyFn()</code> callout and instead must be made, for example, by a user-mode application.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3>Run the sample</h3>
<p>To start the driver, type <b>net start inspect</b> from an elevated command prompt. To stop the driver, type
<b>net stop inspect</b> from an elevated command prompt.</p>
<p>You can configure traffic inspection settings with the following registry values in
<b>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Inspect</b>:</p>
<ul>
<li>BlockTraffic (REG_DWORD type): 0 for permit (default) or 1 to block </li><li>RemoteAddressToInspect (REG_SZ type): a literal IPv4 or IPv6 address string (example &quot;10.0.0.1&quot;)
</li></ul>
<p>For more information on creating a Windows Filtering Platform Callout Driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571068">Windows Filtering Platform Callout Drivers</a>.</p>
</div>
