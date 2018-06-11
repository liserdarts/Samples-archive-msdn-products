# Hyper-V Extensible Switch extension filter driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* ndis
* Windows Driver
## Topics
* Networking
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:45:56
## Description

<div id="mainSection">
<p>This sample contains a base library used to implement a Hyper-V Extensible Switch extension filter driver. This sample also contains two different extension filter drivers that were developed by using the library.
</p>
<p>Hyper-V Extensible Switch extension filter drivers use the NDIS filter model. Every Hyper-V Extensible Switch has a corresponding Extension Protocol and Miniport instance. NDIS OIDs are leveraged to inform filter drivers on the driver stack about switch/port/NIC
 information. All packets originating from a switch port (External NIC, Synthetic NIC, Emulated NIC and Internal NIC) are first issued as a send from the Extension Protocol, which populates the packet with source information. This corresponds to the ingress
 data flow. Source based filtering, packet modification, packet queuing, and destination definition can occur at this stage. If the packets arrive at the Extension Miniport, they pass through the built in filtering/forwarding logic. If the packets pass filtering
 and must be delivered to any destination ports, they are issued as an indication (receive) from the Extension Miniport, which populate the packet with the source and destination information. This corresponds to the egress data flow. Destination based filtering
 can occur at this stage. If the packets arrive at the Extension Protocol, they are delivered to the defined destinations that were not marked as excluded by filtering logic. The packets then are completed back in the reverse order, as a receive completion
 first and then a send completion. Extension filter drivers are free to generate new packets on the ingress data path by issuing a send from their filter.
</p>
<p>The base library provided, <i>SxBase.lib</i>, implements the necessary NDIS functionality common to all types of extension filter drivers. It is not necessary to make any changes to this base library. To implement your own extension filter driver, you only
 need to define all global variables and implement all functions that are found in
<i>SxApi.h</i>.</p>
<p>MsPassthroughExt is a basic filtering extension filter driver that is implemented by using SxBase.lib. This demonstrates the bare minimum that must be implemented to use
<i>SxBase.lib</i>. Installing and enabling MsPassthroughExt on the Hyper-V Extensible Switch does not affect switch behavior.
</p>
<p>MsForwardExt is a basic forwarding extension filter driver that is implemented by using
<i>SxBase.lib</i>. MsForwardExt uses basic MAC forwarding and custom switch policy to allow sends from given MAC addresses. This forwarding sample implements Hybrid Forwarding, which means that the destination table is not populated by this sample if the packet
 is flagged as a Hyper-V Network Virtualization (HNV) packet. HNV flagged packets' destination tables are computed by the vSwitch HNV policies instead. If this extension filter driver is unconfigured, it will block sends from all VMs, but will maintain connectivity
 to the host. Each switch policy, which is defined in <i>MsForwardExtPolicy.mof</i>, is a MAC address. Applying a switch policy to MsForwardExt allows packets to be sent from the MAC address that is defined in the policy.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>None supported </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>Use the<i> install.cmd</i> script provided with each extension filter driver. The
<i>install.cmd</i> uses <b>netcfg</b> to install the extension and <b>mofcomp</b> to register any required mof files. The PowerShell cmdlet
<i>Enable-VmSwitchExtension</i> can then be used to enable the extension filter driver on a Hyper-V Extensible Switch.</p>
<p>For more information on Hyper-V Extensible Switch extensions, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh598161">
Hyper-V Extensible Switch</a>.</p>
</div>
