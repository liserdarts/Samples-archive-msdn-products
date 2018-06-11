# Windows Filtering Platform Packet Modification Sample
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
* 2013-06-25 10:14:34
## Description

<div id="mainSection">
<p>The sample driver demonstrates the packet modification capabilities of the Windows Filtering Platform (WFP).
</p>
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
<p>This sample driver consists of a kernel-mode Windows Filtering Platform (WFP) callout driver (Ddproxy.sys) that intercepts User Datatgram Protocol (UDP) and nonerror Internet Control Message Protocol (ICMP) traffic of interest and acts as a redirector. For
 outbound traffic, Ddproxy.sys redirects the traffic to a new destination address and, for UDP, a new UDP port. For inbound traffic, Ddproxy.sys redirects the traffic back to the original address and UDP port values. This redirection is transparent to the application.</p>
<p>Packet modification is done out-of-band by a system worker thread by using the reference-drop-clone-modify-reinject mechanism. Therefore, the sample can serve as a basis for scenarios in which the filtering/modification decision cannot be made within the
<code>classifyFn()</code> callout, but instead must be made, for example, by a user-mode application.</p>
<p>Ddproxy.sys acts as a redirector for both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6) traffic.</p>
<p>To start the driver, type <b>net start ddproxy</b> from an elevated command prompt. To stop the driver, type
<b>net stop ddproxy</b> from an elevated command prompt.</p>
<p>You can configure traffic inspection and proxy settings with the following registry values in
<b>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\ddproxy</b>:</p>
<ul>
<li>InspectUdp (REG_DWORD type): 0 for ICMP and 1 for UDP (default) </li><li>DestinationAddressToIntercept (REG_SZ type) : literal IPv4 or IPv6 address string (example: &quot;10.0.0.1&quot;)
</li><li>DestinationPortToIntercept (REG_DWORD type): UDP port number (applicable if InspectUdp is set to 1)
</li><li>NewDestinationAddress (REG_SZ type) : literal IPv4 or IPv6 address string </li><li>NewDestinationPort (REG_DWORD type): UDP port number (applicable if InspectUdp is set to 1)
</li></ul>
<p>For more information on creating a Windows Filtering Platform Callout Driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571068">Windows Filtering Platform Callout Drivers</a>.</p>
</div>
