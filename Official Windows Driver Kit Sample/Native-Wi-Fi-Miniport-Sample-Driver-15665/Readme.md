# Native Wi-Fi Miniport Sample Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Networking
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:39
## Description

<div id="mainSection">
<p>The sample code illustrates the typical behavior of a Native Wi-Fi miniport driver. This driver is an NDIS 6.20 miniport driver that conforms to the Microsoft Native Wi-Fi miniport driver specification. The driver supports the following major functionality:</p>
<ul>
<li>Extensible Station operation mode (ExtSTA) </li><li>Extensible AP operation mode (ExtAP) </li><li>WiFi Virtualization (VWiFi) to support simultaneous ExtSTA &amp; ExtAP operation
</li></ul>
<p>The driver is fully operational and runs on Atheros PCI WiFi chipset.</p>
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
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<h2><a id="Code_Walkthrough"></a><a id="code_walkthrough"></a><a id="CODE_WALKTHROUGH"></a>Code Walkthrough</h2>
<h3><a id="Function_Naming_Convention"></a><a id="function_naming_convention"></a><a id="FUNCTION_NAMING_CONVENTION"></a>Function Naming Convention</h3>
<p>The prefix of the function name identifies which layer the function belongs to as listed below:</p>
<table>
<tbody>
<tr>
<th>Prefix</th>
<th>Layer</th>
</tr>
<tr>
<td>Mp </td>
<td>MP Layer Function</td>
</tr>
<tr>
<td>Hvl </td>
<td>HVL Layer Functionality</td>
</tr>
<tr>
<td>Port </td>
<td>Port Functionality</td>
</tr>
<tr>
<td>Vnic </td>
<td>VNIC Functionality</td>
</tr>
<tr>
<td>Hw </td>
<td>Hardware Layer</td>
</tr>
</tbody>
</table>
<p>Interface Functions Have the letters 11 after the starts with prefix (eg. Mp11, Hvl11, Port11, Vnic11, etc). For calls from outside the driver, the prefix is all capitals (eg. MPHalt)</p>
<p>If the function name has the following string following the prefix/11 characters, it corresponds to:</p>
<table>
<tbody>
<tr>
<th>String</th>
<th>Purpose</th>
</tr>
<tr>
<td>Allocate </td>
<td>Allocate Memory for structures</td>
</tr>
<tr>
<td>Free </td>
<td>Free memory allocated for structures</td>
</tr>
<tr>
<td>Initialize </td>
<td>Initialize state variables, timers, etc but don’t “go” yet</td>
</tr>
<tr>
<td>Terminate </td>
<td>Terminate state variables, timers, etc</td>
</tr>
<tr>
<td>Start </td>
<td>“Go”, eg. Start HW, Start periodic scanning in Station, etc</td>
</tr>
<tr>
<td>Stop</td>
<td>Undo the “Go”</td>
</tr>
</tbody>
</table>
<h3><a id="Directory_Structure"></a><a id="directory_structure"></a><a id="DIRECTORY_STRUCTURE"></a>Directory Structure</h3>
<p>Driver</p>
<ul>
<li>Contains MP Layer </li><li>Contains MP/Port interfaces </li><li>Contains non-ExtSTA or ExtAP specific port logic </li><li>Final driver gets built from under this folder </li><li>Virtualization implementation agnostic </li><li>Contains Helper Port logic </li></ul>
<p>Hvl</p>
<ul>
<li>Contains the virtualization code </li><li>Contains HVL interface and implementation </li><li>Contains VNIC </li><li>Contains utility functions for operations queue management, etc </li></ul>
<p>Hw</p>
<ul>
<li>Root folder contains non-hardware specific HW code
<ul>
<li>Atheros PCI driver specific programming </li></ul>
</li></ul>
<p>Extsta</p>
<ul>
<li>ExtSTA Port
<ul>
<li>Hardware agnostic </li></ul>
</li></ul>
<p>Extap</p>
<ul>
<li>ExtAP Port
<ul>
<li>Hardware agnostic </li></ul>
</li></ul>
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>driver\mp_main.c</td>
<td>Miniport layer entry points from NDIS</td>
</tr>
<tr>
<td>driver\mp_oids.c</td>
<td>Miniport layer OID processing code </td>
</tr>
<tr>
<td>driver\mp_pnp.c</td>
<td>Miniport layer PNP functionality</td>
</tr>
<tr>
<td>driver\base_port_main.c</td>
<td>PNP functionality common for all PORTs</td>
</tr>
<tr>
<td>driver\base_port_oids.c</td>
<td>OID processing code common for all PORTs</td>
</tr>
<tr>
<td>driver\base_port_send.c</td>
<td>TX functionality common across all PORTs</td>
</tr>
<tr>
<td>driver\base_port_recv.c</td>
<td>RX functionality common across all PORTs</td>
</tr>
<tr>
<td>driver\helper_port_main.c</td>
<td>Helper port PNP functionality</td>
</tr>
<tr>
<td>driver\helper_port_bsslist.c</td>
<td>Helper port BSS list management functions</td>
</tr>
<tr>
<td>driver\helper_port_scan.c</td>
<td>Helper port Scan functions</td>
</tr>
<tr>
<td>driver\atheros\netawifi.inx</td>
<td>INF for the sample driver</td>
</tr>
<tr>
<td>extsta\st_oids.c</td>
<td>ExtSTA layer OID processing code</td>
</tr>
<tr>
<td>extsta\st_scan.c</td>
<td>ExtSTA layer OS/internal scan functions</td>
</tr>
<tr>
<td>extsta\st_aplst.c</td>
<td>ExtSTA layer BSS list functionality</td>
</tr>
<tr>
<td>extsta\st_conn.c</td>
<td>ExtSTA layer infrastructure connection/roaming functions</td>
</tr>
<tr>
<td>extsta\st_auth.c</td>
<td>ExtSTA layer authentication frame processing functions</td>
</tr>
<tr>
<td>extsta\st_assoc.c</td>
<td>ExtSTA layer association frame processing functions</td>
</tr>
<tr>
<td>extsta\st_adhoc.c</td>
<td>ExtSTA layer adhoc connection functions</td>
</tr>
<tr>
<td>extap\ap_oids.c</td>
<td>ExtSTA layer OID processing code</td>
</tr>
<tr>
<td>extap\ap_action.c</td>
<td>ExtAP layer actions such as start/stop AP</td>
</tr>
<tr>
<td>extap\ap_assocmgr.c</td>
<td>ExtAP layer association management functions</td>
</tr>
<tr>
<td>extap\ap_config.c</td>
<td>ExtAP layer configuration management functions</td>
</tr>
<tr>
<td>hvl\hvl_context.c</td>
<td>HVL layer virtualization context management functions</td>
</tr>
<tr>
<td>hvl\hvl_main.c</td>
<td>HVL layer initialization/PNP functions</td>
</tr>
<tr>
<td>hvl\hvl_oids.c</td>
<td>HVL layer OID processing code</td>
</tr>
<tr>
<td>hvl\vnic_dot11.c</td>
<td>VNIC layer 802.11 MAC management functions</td>
</tr>
<tr>
<td>hvl\vnic_queue.c</td>
<td>VNIC layer virtualization state management functions</td>
</tr>
<tr>
<td>hvl\vnic_send.c</td>
<td>VNIC layer TX functions</td>
</tr>
<tr>
<td>hvl\vnic_oids.c</td>
<td>VNIC OID processing code</td>
</tr>
<tr>
<td>hw\hw_main.c</td>
<td>Hardware layer PNP functions</td>
</tr>
<tr>
<td>hw\hw_oids.c</td>
<td>Hardware layer OID processing code</td>
</tr>
<tr>
<td>hw\hw_send.c</td>
<td>Hardware layer send functions</td>
</tr>
<tr>
<td>hw\hw_recv.c</td>
<td>Hardware layer receive functions</td>
</tr>
<tr>
<td>hw\hw_isr.c</td>
<td>Interrupt handling routines</td>
</tr>
<tr>
<td>hw\hw_context.c</td>
<td>Hardware layer per port MAC context management functions</td>
</tr>
<tr>
<td>hw\hw_mac.c</td>
<td>Hardware layer 802.11 MAC functionality</td>
</tr>
<tr>
<td>hw\hw_phy.c</td>
<td>Hardware layer 802.11 PHY functionality</td>
</tr>
<tr>
<td>hw\hw_rate.c</td>
<td>Basic rate adaptation logic for the HW layer</td>
</tr>
<tr>
<td>inc\port_def.h</td>
<td>Port layer global defines</td>
</tr>
<tr>
<td>inc\data_glb_defs.h</td>
<td>TX/RX data related defines common to the complete driver</td>
</tr>
<tr>
<td>inc\base_port_intf.h</td>
<td>Contains interfaces into the base port</td>
</tr>
<tr>
<td>inc\port_intf.h</td>
<td>Interface functions into PORT layer</td>
</tr>
<tr>
<td>inc\ap_intf.h</td>
<td>Interface functions into ExtAP layer</td>
</tr>
<tr>
<td>inc\st_intf.h</td>
<td>Interface functions into ExtSTA layer</td>
</tr>
<tr>
<td>inc\vnic_intf.h</td>
<td>Interface functions into VNIC layer</td>
</tr>
<tr>
<td>inc\hvl_intf.h</td>
<td>Interface functions into HVL layer</td>
</tr>
<tr>
<td>inc\hw_intf.h</td>
<td>Interface functions into Hardware layer</td>
</tr>
<tr>
<td>inc\ath_glb_defs.h</td>
<td>Atheros H/W specific defines</td>
</tr>
<tr>
<td>inc\hal_intf.h</td>
<td>Interfaces into Atheros hardware specific driver library</td>
</tr>
<tr>
<td>hal\*\athhal.sys</td>
<td>Driver library containing Atheros hardware specific code</td>
</tr>
</tbody>
</table>
<p>For more information on native Wi-Fi drivers, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560689">
Native 802.11 Wireless LAN</a>.</p>
</div>
