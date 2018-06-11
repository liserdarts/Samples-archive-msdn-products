# AMCC5933 - PCI Device Driver Using WDF
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
* 2014-04-02 12:44:32
## Description

<div id="mainSection">
<p>This sample demonstrates how to write driver for a generic PCI device by using the Windows Driver Framework.
</p>
<p>The target hardware for this driver is AMCC PCI Matchmaker Developer's kit (S5935DK1) board. The product kit and the hardware specification are available at
<a href="http://go.microsoft.com/fwlink/p/?linkid=65450">https://amcc.com</a>.</p>
<p>The kit contains a PCI development board and an ISA card that connects to the development board via a ribbon cable. The ISA card allows you to access the development board to provide the software side simulation of add-on device.</p>
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
<p>If the build succeeds, you will find the driver, binaries under the respective subdirectories of the source file depending on the build environment. If it fails you can find errors and warnings in the buildxxx.err and buildxxx.err respectively, where xxx
 is either chk or fre depending on the build environment. Copy the KMDF coinstaller (wdfcoinstallerMMmmm.dll), sys, INF and test binaries to a floppy or to a directory of a test machine.</p>
<h2><a id="Overview"></a><a id="overview"></a><a id="OVERVIEW"></a>Overview</h2>
<p>The following table gives a typical device and driver scenario and also lists the driver framework interfaces demonstrated in this sample:</p>
<table>
<tbody>
<tr>
<th>Scenario</th>
<th>Features demonstrated</th>
</tr>
<tr>
<td>
<p>The device is a PCI device with port, memory, interrupt and DMA resources. Device can be stopped and started at run time and also supports low power state.</p>
<p>The driver is capable of doing concurrent single read and write operations at any time.
</p>
</td>
<td>
<ul>
<li>
<p>Handling PnP &amp; Power Events </p>
</li><li>
<p>Registering Device Interface </p>
</li><li>
<p>Hardware resource mapping: Port, Memory &amp; Interrupt </p>
</li><li>
<p>DMA interfaces </p>
</li><li>
<p>Serialized Default Queue for Write requests </p>
</li><li>
<p>Serialized custom Queue for Read requests </p>
</li><li>
<p>Handling Interrupt &amp; DPC </p>
</li><li>
<p>Event Tracing </p>
</li></ul>
</td>
</tr>
</tbody>
</table>
<p>This sample also demonstrates how to use a single driver binary to control two different types (PCI &amp; ISA) of devices.
</p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the Windows Driver Framework. It is not intended for use in a production environment.
</p>
<h2><a id="_______INSTALLATION"></a><a id="_______installation"></a>INSTALLATION</h2>
<p>Plug in the PCI development board and the ISA card and start your system. When you log in an Administrator account, the system will welcome you with a &quot;Found New Hardware Dialog&quot; wizard for &quot;Other Bridge Devices&quot;.</p>
<p class="proch"><b>To install the driver for the PCI device on Windows&nbsp;7 and later operating systems:</b></p>
<ol>
<li>
<p>Launch &quot;Device Manager&quot; by executing command devmgmt.msc in a command window, or from &quot;Hardware and Sound&quot; program group in Control Panel.</p>
</li><li>
<p>Select &quot;PCI Device&quot; device from &quot;Other Devices&quot; category and click &quot;Update Driver Software...&quot; from right-click menu.</p>
</li><li>
<p>Select Browse my computer for software and provide the location of the driver files.</p>
</li><li>
<p>Select &quot;Install this driver software anyway&quot; when the &quot;Windows Security&quot; dialog box appears.</p>
</li><li>
<p>The system will complete the installation, load the driver and start the device.</p>
</li></ol>
<p class="proch"><b>To install the driver for the PCI device on Windows&nbsp;Vista and Windows Server&nbsp;2008</b></p>
<ol>
<li>
<p>In the &quot;Found New Hardware&quot; dialog box, select &quot;Locate and install driver software (recommended)&quot;.
</p>
</li><li>
<p>Select &quot;Don't search online&quot;. </p>
</li><li>
<p>Select &quot;I don't have the disc. Show me other options&quot;. </p>
</li><li>
<p>Select &quot;Browse my computer for driver software (advanced)&quot;. </p>
</li><li>
<p>Enter the location of the driver files, and select &quot;Install this driver software anyway&quot; when the &quot;Windows Security&quot; dialog box appears.</p>
</li><li>
<p>The system will complete the installation, load the driver and start the device.
</p>
</li></ol>
<p>The driver for the ISA device has to be installed manually because it is a non-Plug and Play ISA device.
</p>
<p class="proch"><b>To install the driver for the ISA device on Windows&nbsp;7 and later operating systems:</b></p>
<ol>
<li>
<p>Run &quot;hdwwiz.exe&quot; in a command window or in the &quot;Search programs and files&quot; of the &quot;Start&quot; menu.</p>
</li><li>
<p>At the &quot;Welcome...&quot; click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;The wizard can help...&quot; panel, select &quot;Install...manually...&quot; and then click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;From the list below...&quot; panel, select &quot;Show All Devices&quot; and then click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;Select the device driver...&quot; panel, just click the &quot;Have Disk...&quot; button.</p>
</li><li>
<p>At the &quot;Install From Disk&quot; panel, click the &quot;Browse...&quot; button. From the Locate File panel specify the directory location where the AMCC install files can be found: AMCC5933.INF and AMCC5933.SYS. Click &quot;OK&quot; to return to the &quot;Select the device driver...&quot;
 panel.</p>
</li><li>
<p>In the &quot;Model&quot; list box, you should see a listing for &quot;AMCC S5993 Development Kit ISA Adapter&quot;. Select this entry and then click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;The wizard is ready...&quot; panel, click &quot;Next&quot;.</p>
</li><li>
<p>At the final &quot;Completing the Add Hardware Wizard&quot; panel, click &quot;Finish&quot;.</p>
</li></ol>
<p class="proch"><b>To install the driver for the ISA device on Windows&nbsp;Vista and Windows Server&nbsp;2008</b></p>
<ol>
<li>
<p>Start the control panel and bring up the &quot;Add Hardware&quot; wizard.</p>
</li><li>
<p>At the &quot;Welcome...&quot; click &quot;Next&quot; </p>
</li><li>
<p>At the &quot;The wizard can help...&quot; panel, select &quot;Install...manually...&quot; and then click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;From the list below...&quot; panel, select &quot;Show All Devices&quot; and then click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;Select the device driver...&quot; panel, just click the &quot;Have Disk...&quot; button.</p>
</li><li>
<p>At the &quot;Install From Disk&quot; panel, click the &quot;Browse...&quot; button. From the Locate File panel specify the directory location where the AMCC install files can be found: AMCC5933.INF and AMCC5933.SYS. Click &quot;OK&quot; to return to the &quot;Select the device driver...&quot;
 panel.</p>
</li><li>
<p>In the &quot;Model&quot; list box, you should see a listing for &quot;AMCC S5993 Development Kit ISA Adapter&quot;. Select this entry and then click &quot;Next&quot;.</p>
</li><li>
<p>At the &quot;The wizard is ready...&quot; panel, click &quot;Next&quot;.</p>
</li><li>
<p>At the final &quot;Completing the Add Hardware Wizard&quot; panel, click &quot;Finish&quot;.</p>
</li></ol>
<h2><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p>To test the driver, run the TxTest.exe and RxTest.exe. Please make sure both ISA and PCI devices are installed and started before you run the application.</p>
<h2><a id="_______File_Manifest"></a><a id="_______file_manifest"></a><a id="_______FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p><b>Driver:</b> </p>
</td>
<td></td>
</tr>
<tr>
<td>
<p>Driver.c </p>
</td>
<td>
<p>Entry points for AMCC5933 driver.</p>
</td>
</tr>
<tr>
<td>
<p>AMCC5933.h</p>
</td>
<td>
<p>Prototype file for AMCC5933 PCI driver.</p>
</td>
</tr>
<tr>
<td>
<p>common.h </p>
</td>
<td>
<p>Prototype file common to both the ISA and the PCI driver.</p>
</td>
</tr>
<tr>
<td>
<p>S5933DK1.h</p>
</td>
<td>
<p>Prototype file for AMCC5933 ISA driver.</p>
</td>
</tr>
<tr>
<td>
<p>S5933DK1.c </p>
</td>
<td>
<p>File for AMCC5933 ISA driver.</p>
</td>
</tr>
<tr>
<td>
<p>AMCC5933.c </p>
</td>
<td>
<p>File for AMCC5933 PCI driver.</p>
</td>
</tr>
<tr>
<td>
<p>Reg5933.h</p>
</td>
<td>
<p>Prototype file for AMCC5933 driver.</p>
</td>
</tr>
<tr>
<td>
<p>trace.h</p>
</td>
<td>
<p>Header file for the debug tracing related function definitions and macros for AMCC5933 driver.</p>
</td>
</tr>
<tr>
<td>
<p>Transfer.c</p>
</td>
<td>
<p>File for AMCC5933 driver which deals with transfer related DDI(ISR's, DPC's, DMA).</p>
</td>
</tr>
<tr>
<td>
<p>amcc5933.inx</p>
</td>
<td>
<p></p>
<dl><dt>Installation file for installing the driver. </dt><dt>This file is used to generate amcc5933.inf. </dt></dl>
<p></p>
</td>
</tr>
<tr>
<td>
<p>makefile</p>
</td>
<td>
<p>Standard DDK build environment makefile.</p>
</td>
</tr>
<tr>
<td>
<p>makefile.inc</p>
</td>
<td>
<p>Makefile extensions to handle inx/inf files.</p>
</td>
</tr>
<tr>
<td>
<p>sources</p>
</td>
<td>
<p>Generic file for building the code sample</p>
</td>
</tr>
<tr>
<td>
<p></p>
<dl><dt><b>Application:</b> </dt><dt><b>RxTest:</b> </dt></dl>
<p></p>
</td>
<td></td>
</tr>
<tr>
<td>
<p>RxTest.c </p>
</td>
<td>
<p>Receive Test file for the AMCC5933 driver.</p>
</td>
</tr>
<tr>
<td>
<p>makefile </p>
</td>
<td>
<p>Standard DDK build environment makefile.</p>
</td>
</tr>
<tr>
<td>
<p>sources </p>
</td>
<td>
<p>Generic file for building the code sample.</p>
</td>
</tr>
<tr>
<td>
<p><b>TxTest:</b> </p>
</td>
<td></td>
</tr>
<tr>
<td>
<p>TxTest.c </p>
</td>
<td>
<p>Transmit Test file for the AMCC5933 driver.</p>
</td>
</tr>
<tr>
<td>
<p>makefile</p>
</td>
<td>
<p>Standard DDK build environment makefile.</p>
</td>
</tr>
<tr>
<td>
<p>sources</p>
</td>
<td>
<p>Generic file for building the code sample.</p>
</td>
</tr>
</tbody>
</table>
</div>
