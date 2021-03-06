# PCIDRV - WDF Driver for PCI Device
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:48:14
## Description

<div id="mainSection">
<p>This sample demonstrates how to write a KMDF driver for a PCI device. The sample works with the Intel 82557/82558 based PCI Ethernet Adapter (10/100) and Intel compatibles.
</p>
<p>This adapter supports scatter-gather DMA, wake on external event (Wait-Wake), and idle power down. The hardware specification is publicly available, and the source code to interface with the hardware is included in the WDK.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff557565">Kernel-Mode Driver Framework</a>
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
<p>The sample driver can be built using any of the build environments from Windows&nbsp;8.1 WDK.</p>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p>If the build succeeds, the driver will be placed in the binary output directory specified for the build environment.</p>
<h2><a id="Overview"></a><a id="overview"></a><a id="OVERVIEW"></a>Overview</h2>
<p>The following is a list of key KMDF interfaces demonstrated in this sample:</p>
<ul>
<li>
<p>Handling PnP &amp; Power Events</p>
</li><li>
<p>Registering Device Interface</p>
</li><li>
<p>Hardware resource mapping: Port, Memory &amp; Interrupt</p>
</li><li>
<p>DMA Interfaces </p>
</li><li>
<p>Parallel default queue for write requests. If the write cannot be satisfied immediately, the request is put into a manual parallel queue.</p>
</li><li>
<p>Parallel manual queue for Read requests</p>
</li><li>
<p>Parallelc default queue for IOCTL requests. If the ioctl cannot be satisfied immediately, the request is put into a manual parallel queue.</p>
</li><li>
<p>Request cancelation</p>
</li><li>
<p>Handling Interrupt &amp; DPC</p>
</li><li>
<p>Watchdog Timer DPC to monitor the device state.</p>
</li><li>
<p>Event Tracing &amp; HEXDUMP</p>
</li><li>
<p>Reading &amp; Writing to the registry </p>
</li></ul>
<p>Note: This sample provides an example of a minimal driver intended for educational purposes. Neither the driver nor its sample test programs are intended for use in a production environment.
</p>
<p>As stated earlier, this sample is meant to demonstrate how to write a KMDF driver for a generic PCI device and not for PCI network controllers. For network controllers, you should write a monolithic NDIS miniport driver based on the samples given under the
 src\network\ndis directory. </p>
<p>Note that it is still possible to use a subset of KMDF APIs when writing a NDIS miniport (see src\network\ndis\usbnwifi directory for a sample on how to use KMDF interfaces to talk to USB device in an NDIS miniport).</p>
<p>The sample driver has been tested on the following Intel Ethernet controllers:
</p>
<table>
<tbody>
<tr>
<th>Device Desc</th>
<th>Hardware ID</th>
</tr>
<tr>
<td>
<p>IBM Netfinity 10/100 Ethernet Adapter</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1229&amp;SUBSYS_005C1014&amp;REV_05</p>
</td>
</tr>
<tr>
<td>
<p>Intel(R) PRO/100&#43; Management Adapter with Alert On LAN</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1229&amp;SUBSYS_000E8086&amp;REV_08</p>
</td>
</tr>
<tr>
<td>
<p>Intel 8255x-based PCI Ethernet Adapter (10/100)</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1229&amp;SUBSYS_00000000&amp;REV_01</p>
</td>
</tr>
<tr>
<td>
<p>Intel Pro/100 S Server Adapter</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1229&amp;SUBSYS_00508086&amp;REV_0D</p>
</td>
</tr>
<tr>
<td>
<p>Intel 8255x-based PCI Ethernet Adapter (10/100)</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1229&amp;SUBSYS_00031179&amp;REV_08</p>
</td>
</tr>
<tr>
<td>
<p>Intel(R) PRO/100 VE Network Connection</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_103D&amp;SUBSYS_00011179&amp;REV_83</p>
</td>
</tr>
<tr>
<td>
<p>Intel(R) PRO/100 VM Network Connection</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1031&amp;REV_42</p>
</td>
</tr>
<tr>
<td>
<p>Intel(R) PRO/100 VE Network Connection</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1038&amp;REV_41</p>
</td>
</tr>
<tr>
<td>
<p>Intel(R) PRO/100 SR Mobile Adapter</p>
</td>
<td>
<p>PCI\VEN_8086&amp;DEV_1229</p>
</td>
</tr>
</tbody>
</table>
<h2><a id="_______Using_this_sample_as_a_standalone_driver"></a><a id="_______using_this_sample_as_a_standalone_driver"></a><a id="_______USING_THIS_SAMPLE_AS_A_STANDALONE_DRIVER"></a>Using this sample as a standalone driver</h2>
<pre class="syntax"><code>
          ---------------------
         |                     |
         |        MYPING       | &lt;-- Usermode test application
         |                     |
          ---------------------
                   ^
                   |                                     UserMode
-------------------------------------------------------------------
                   |                                     KernelMode
                   V
          ---------------------
         |                     |
         |        PCIDRV       | &lt;-- Installed as a function driver
         |                     |
          ---------------------
                   ^
                   | &lt;-----Talk to the hardware using I/O resources
                   V
             ---------------
            |    H/W NIC    |
             ---------------
                    |||||||
                    -------</code></pre>
<p>You can install the driver as a standalone driver of a custom setup class, called Sample Class using GENPCI.INF. The PCI device is not seen as a network controller and as a result no protocol driver is bound to the device. In order to test the read &amp;
 write path of the driver, you can use the specially developed ping application, called MYPING. This test application crafts the entire Ethernet frame in usermode and sends it to the driver to be transferred on the wire. In this configuration, you can only
 ping another machine on the same subnet. The application does all the ARP and AARP resolution in the usermode to get the MAC address of the target machine and sends ICMP ECHO requests.
</p>
<p>The PCIDRV sample acts as a power policy owner of the device and implements all the wait-wake and idle detection logic.
</p>
<h2><a id="_______INSTALLATION_"></a><a id="_______installation_"></a>INSTALLATION
</h2>
<p>The driver can be installed as a Net class driver or as a standalone driver (user defined class). The KMDF versions of the INF files are dynamically generated from .INX file. In addition to the driver files, you have to include the WDF coinstaller DLL from
 the src\redist\wdf folder of the WDK.</p>
<p>You can obtain redistributable framework updates by downloading the <i>wdfcoinstaller.msi</i> package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed.
 You can verify that the redistributables have been installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<h3><a id="testing"></a><a id="TESTING"></a>TESTING</h3>
<p>To test standalone driver configuration: You should use the specially developed ping application, called MYPING that comes with the sample. The Ping.exe provided in the system will not work because in this configuration, the test card is not bound to any
 network protocol - it's not seen as Net device by the system. Currently the test application doesn't have ability to get an IP address from a network DHCP server. As a result, it is better to connect the network device to a private hub and ping another machine
 connected to that hub. For example, let us say you have a test machine A and another machine B (development box).
</p>
<ul>
<li>
<p>Connect machine A and Machine B to a local hub.</p>
</li><li>
<p>Assign a static IP address, say 128.0.0.1 to the NIC on machine B. </p>
</li><li>
<p>Clear the ARP table on machine B by running <b>Arp -d</b> on the command line</p>
</li><li>
<p>Now run Myping.exe. This application enumerates GUID_DEVINTERFACE_PCIDRV and displays the name of the devices with an index number. This number will be used in identifying the interface when you invoke ping dialog.
</p>
</li><li>
<p>In the ping dialog specify the following and click okay:</p>
</li><li>
<p>Device Index: 1 &lt;- number displayed in the list window</p>
</li><li>
<p>Source Ip Address: 128.0.0.4 &lt;- You can make up any valid IP address for test Machine A</p>
</li><li>
<p>Destination IP Address: 128.0.0.1 &lt;- IP address of machine B</p>
</li><li>
<p>Packet Size: 1428 &lt;- Default max size of ping payload. Minimum value is 32 bytes.</p>
</li></ul>
<p>If the machine B has more than one adapter and if the second adapter is connected to the internet (Corporate Network), instead of assigning static IP address to the adapter that's connected to the test machine, you can install Internet Connection Sharing
 (ICS) on it and get an IP address for ICS. This would let you use the test machine to browse the internet when the sample is installed in the miniport configuration and also in the standalone mode without making up or stealing somebody's IP address. For example,
 let us say the machine B has two adapters NIC1 and NIC2. NIC1 is connected to the CorpNet and NIC2 is connected to the private hub. Install ICS on NIC2 as described below:
</p>
<ul>
<li>
<p>Select the NIC2 in the Network Connections Applet. </p>
</li><li>
<p>Click the <b>Properties</b> button.</p>
</li><li>
<p>Go to the Advanced Tab and Check the box &quot;Allow Other network users to connect through this computers internet connection&quot; in the Internet Connection Sharing choice.
</p>
</li><li>
<p>This will assign 192.168.0.1 IP address to NIC2.</p>
</li><li>
<p>Now on machine B, you can assume 192.168.0.2 as the local IP address and run Myping.exe . Or, you can install the sample in the miniport configuration and browse the internet.</p>
</li></ul>
<p>Other menu options of myping applications are:</p>
<ul>
<li>
<p>Reenumerate All Device: This command lets you terminate active ping threads and close handle to all the device and reenumerate the devices again and display their names with index numbers. This might cause the devices to have new index numbers.</p>
</li><li>
<p>Cleanup: This command terminates ping threads and closes handles to all the devices.</p>
</li><li>
<p>Clear Display: Clears the window.</p>
</li><li>
<p>Verbose: Let you get more debug messages. </p>
</li><li>
<p>Exit: Terminate the application.</p>
</li></ul>
<p class="note"><b>Note</b>&nbsp;&nbsp;You can use this application only on a device installed in the standalone configuration. If you run it on a device that's installed as a miniport, you will get an error message. For such devices, you can use the system provided
 ping.exe. </p>
<h2><a id="_______RESOURCES"></a><a id="_______resources"></a>RESOURCES</h2>
<p>For the latest release of the Windows Driver Kit, see <u>http://www.microsoft.com/whdc/</u>.</p>
<p>If you have questions on using or adapting this sample for your project, you can either contact
<u>Microsoft Technical Support</u> or post your questions in the <u>Microsoft driver development newsgroup</u>.
</p>
<h2><a id="_______FILE_MANIFEST"></a><a id="_______file_manifest"></a>FILE MANIFEST</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>KMDF</p>
</td>
<td>
<p>Contains the driver.</p>
</td>
</tr>
<tr>
<td>
<p>KMDF\HW</p>
</td>
<td>
<p>Contains hardware specific code.</p>
</td>
</tr>
<tr>
<td>
<p>TEST</p>
</td>
<td>
<p>Contains source of test application (MYPING).</p>
</td>
</tr>
</tbody>
</table>
</div>
