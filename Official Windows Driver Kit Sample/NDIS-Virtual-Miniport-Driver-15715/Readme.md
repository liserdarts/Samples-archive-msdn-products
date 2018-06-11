# NDIS Virtual Miniport Driver
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
* 2014-04-02 12:47:59
## Description

<div id="mainSection">
<p>The NDIS Virtual Miniport Driver sample illustrates the functionality of an NDIS miniport driver without requiring a physical network adapter.
</p>
<p>Because the driver does not interact with any hardware, it makes it easier to understand the miniport interface and the usage of various NDIS functions without the clutter of hardware-specific code that is normally found in a fully functional driver. The
 driver can be installed either manually using the Add Hardware wizard as a root enumerated virtual miniport driver or on a virtual bus (like toaster bus).
</p>
<p>This sample driver demonstrates an NDIS virtual miniport driver. If a single instance of the virtual miniport exists, it simply drops the send packets and completes the send operation successfully. If there are multiple virtual miniport instances, the instances
 behave as if they were multiple network interface cards (NICs) plugged into a single Ethernet hub. This &quot;hub&quot; indicates the incoming send packets to all of the virtual miniport instances.</p>
<p>To test the miniport driver, install more than one miniport driver instance. You can repeat the installation to install more than one instance of the miniport.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample provides an example of minimal driver intended for education purposes. The driver and its sample test programs are not intended for use in a production environment.</p>
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
<h2><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>netVmini60.inf</td>
<td>INF file for installing the NDIS 6.0 version of the driver.</td>
</tr>
<tr>
<td>netVmini620.inf</td>
<td>INF file for installing the NDIS 6.20 version of the driver.</td>
</tr>
<tr>
<td>netVmini630.inf</td>
<td>INF file for installing the NDIS 6.30 version of the driver.</td>
</tr>
<tr>
<td>netVmini60.RC</td>
<td>Resource file for the NDIS 6.0 version of the driver.</td>
</tr>
<tr>
<td>netVmini620.RC</td>
<td>Resource file for the NDIS 6.20 version of the driver.</td>
</tr>
<tr>
<td>netVmini630.RC</td>
<td>Resource file for the NDIS 6.30 version of the driver.</td>
</tr>
<tr>
<td>adapter.c</td>
<td>Contains <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559389">
<i>MiniportInitializeEx</i></a> and other miniport adapter functions.</td>
</tr>
<tr>
<td>adapter.h</td>
<td>Include file for defining miniport adapter structures, constants, and function prototypes.</td>
</tr>
<tr>
<td>ctrlpath.c</td>
<td>Contains <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559416">
<i>MiniportOidRequest</i></a> and other OID-related functions.</td>
</tr>
<tr>
<td>ctrlpath.h</td>
<td>Include file for defining miniport OID-related structures, constants, and function prototypes.</td>
</tr>
<tr>
<td>datapath.c</td>
<td>Contains <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559440">
<i>MiniportSendNetBufferLists</i></a> and other data path-related functions.</td>
</tr>
<tr>
<td>datapath.h</td>
<td>Include file for defining miniport data path-related structures, constants, and function prototypes.</td>
</tr>
<tr>
<td>hardware.h</td>
<td>Include file for defining characteristics of the network adapter's virtual hardware.</td>
</tr>
<tr>
<td>miniport.c</td>
<td>Main file that contains <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff548818">
<b>DriverEntry</b></a> and other miniport driver functions.</td>
</tr>
<tr>
<td>miniport.h</td>
<td>Include file for defining miniport driver structures, constants, and function prototypes.</td>
</tr>
<tr>
<td>mphal.c</td>
<td>Contains functions that implement the network adapter's virtual hardware.</td>
</tr>
<tr>
<td>mphal.h</td>
<td>Include file for defining structures, constants, and function prototypes that are used in mphal.c.</td>
</tr>
<tr>
<td>netvmin6.h</td>
<td>Master include file that lists all the header files needed to compile this sample driver.</td>
</tr>
<tr>
<td>qos.c</td>
<td>Contains functions that implement the network adapter's QOS functionality.</td>
</tr>
<tr>
<td>qos.h</td>
<td>Include file for defining structures, constants, and function prototypes that are used in qos.c.</td>
</tr>
<tr>
<td>tcbrcb.c</td>
<td>Contains functions that implement the miniport driver's send and receive code path.</td>
</tr>
<tr>
<td>tcbrcb.h</td>
<td>Include file for defining structures, constants, and function prototypes that are used in tcbrcb.c.</td>
</tr>
<tr>
<td>trace.h</td>
<td>Include file for defining debug support macros.</td>
</tr>
<tr>
<td>vmq.c</td>
<td>Contains functions that implement the network adapter's VMQ functionality.</td>
</tr>
<tr>
<td>vmq.h</td>
<td>Include file for defining structures, constants, and function prototypes that are used in vmq.c.</td>
</tr>
</tbody>
</table>
<p>For more information on creating NDIS Miniport Drivers, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff565949">
NDIS Miniport Drivers</a>.</p>
</div>
