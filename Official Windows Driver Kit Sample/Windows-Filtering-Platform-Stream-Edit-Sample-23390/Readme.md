# Windows Filtering Platform Stream Edit Sample
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
* 2013-06-25 10:21:46
## Description

<div id="mainSection">
<p>This sample driver demonstrates replacing a string pattern for a Transmission Control Protocol (TCP) connection using the Windows Filtering Platform (WFP).
</p>
<p>The sample consists of a kernel-mode Windows Filtering Platform (WFP) callout driver (Stmedit.sys) that can operate in one of the following modes:</p>
<ul>
<li>Inline editing where all modification is done within the <code>ClassifyFn</code> callout function.
</li><li>Out-of-band editing where all modification is done by a worker thread (the default).
</li></ul>
<p>You can configure the mode setting and other inspection parameters with the following registry values at
<b>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\stmedit</b>:</p>
<ul>
<li>EditInline (REG_DWORD type): 1 for inline editing, 0 for out-of-band editing (the default)
</li><li>StringToFind (REG_SZ type): default = &quot;rainy&quot; </li><li>StringToReplace (REG_SZ type): default = &quot;sunny&quot; </li><li>InspectionPort (REG_DWORD type): TCP port (default = 5001) </li><li>InspectOutbound (REG_DWORD type): TCP port (default = 0) </li></ul>
<p>The sample performs inspection for both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6) traffic.</p>
<p>Before experimenting with the sample, add an exception for the InspectionPort to your host firewall.</p>
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
<p>To start the driver, type <b>net start stmedit</b> from an elevated command prompt. To stop the driver, type
<b>net stop stmedit</b> from an elevated command prompt.</p>
<p>For more information on creating a Windows Filtering Platform Callout Driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571068">Windows Filtering Platform Callout Drivers</a>.</p>
</div>
