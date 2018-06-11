# WDF Hybrid 1394 Virtual Device Sample Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* 1394
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:52:05
## Description

<div id="mainSection">
<p>This sample consists of a KMDF function driver and a UMDF upper filter driver that together represent a 1394 virtual device. The sample demonstrates application to device interaction, as well as use of both UMDF and KMDF in the same device stack.
</p>
<p>The sample consists of two WDF drivers: a UMDF driver (<i>umdf1394vdev.dll</i>) and a KMDF driver (<i>kmdf1394vdev.sys</i>). The sample also includes a user-mode test application (<i>WDF1394.EXE</i>). The application loads the hybrid driver stack and uses
 it to interact with the 1394 bus driver.</p>
<p>The KMDF sample driver interfaces with the upper edge of the 1394 stack. In addition to handling asynchronous data transfers, the sample source code demonstrates how a WDF driver handles Plug and Play (PnP) and power management I/O Request Packets (IRPs).
 You can use <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537211">
IEEE 1394 Bus I/O Requests</a> to modify or extend the virtual device to simulate different configurations of a 1394 physical device.</p>
<p>This sample replaces the 1394 Virtual Device sample driver that shipped in earlier versions of the Windows operating system. The UMDF driver replaces the 1394 diagnostic test routines that were exported by 1394api.dll in the 1394 Virtual Device sample driver.</p>
<p>For more information about 1394 virtual devices, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537065">
Creating IEEE 1394 Virtual Devices</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff557565">Kernel-Mode Driver Framework</a> ,
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560456">User-Mode Driver Framework</a>
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
<h2><a id="building"></a><a id="BUILDING"></a></h2>
<p>For information on how to build a driver solution using , see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<p>Before building this solution, do the following: </p>
<ul>
<li>In , navigate to the package property page and set <b>Configuration Properties &gt; Driver Signing &gt; Sign Mode</b> to
<b>Test Sign</b>. </li><li>Ensure that you have the WDF co-installers. You can download the <i>wdfcoinstaller.msi</i> package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>. The redistributables are installed in a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.
</li></ul>
<p></p>
<p>The sample produces two WDF drivers (<i>kmdf1394vdev.sys</i> and <i>umdf1394vdev.dll</i>), and one executable (<i>WDF1394.EXE</i>).</p>
<p>If you get the following build error: “WindowsApplicationForDrivers8.0 cannot be found”, your WDK installation may be incomplete. Try one of the following:</p>
<ul>
<li>Change the Platform Toolset from the current version of Visual Studio to Visual Studio 2010 (v100) (<b>Configuration Properties &gt; General</b>).
</li><li>Use the repair option on your existing WDK installation. </li><li>Do a clean installation of the WDK. </li></ul>
<h2><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>You can use the following procedure to install a virtual 1394 device.</p>
<p>Your computer must have a 1394 bus to perform this procedure. In <b>Device Manager</b>, verify that you have a
<b>IEEE 1394 Bus host controller</b> top-level node. You cannot simultaneously maintain a debugging connection using the same 1394 bus.</p>
<ol>
<li>On your target machine, open an elevated command prompt and type <b>Bcdedit.exe -set TESTSIGNING ON</b>. After rebooting, you can type
<b>Bcdedit</b> to verify that test signed drivers can be loaded. </li><li>
<p>Copy the files in the Package directory (for example C:\WDK_Samples\1394VDEV and 1394DIAG - WDF Version\C&#43;&#43;\x64\Win7Debug\Package) to a floppy disk or to a temporary directory on the target machine. Also copy
<i>WDF1394.EXE</i> from the corresponding subdirectory under exe, for example C:\WDK_Samples\1394VDEV and 1394DIAG - WDF Version\C&#43;&#43;\exe\x64\Win7Debug.</p>
</li><li>
<p>Run <i>WDF1394.EXE</i> with administrative privileges. If you get an error that MSVCR110.dll or MSVCR110D.dll (the Visual C Runtime library) is missing, do one of the following:</p>
<ul>
<li>Manually supply the missing library file. You can find these DLLs on machines that have installed.
</li><li>Statically link by using /MT (in , select <b>Configuration Properties &gt; C/C&#43;&#43; &gt; Runtime Library &gt; /MT</b>)
</li><li>Install on the target. </li></ul>
<p></p>
</li><li>
<p>On the <b>1394 Devices</b> menu, click <b>Add Virtual Device</b>. The application sends an IOCTL_IEEE1394_API_REQUEST to the 1394 bus driver, requesting it to enumerate a virtual device.</p>
</li><li>
<p>Depending on the configuration of your system, the Found New Hardware wizard may or may not appear.</p>
<p>If the wizard appears, select <b>Install from a list or specific location (Advanced)</b>.</p>
<p>If the wizard does not appear, start Device Manager. In the device hierarchy, open the
<b>Other Devices</b> node and highlight <b>Unknown device</b>. Choose <b>Action &gt; Update Driver Software</b>.</p>
</li><li>
<p>Choose <b>Browse my computer for driver software</b>. </p>
</li><li>
<p>Browse to the directory that contains the INF file and the drivers.</p>
</li><li>
<p>Select <b>1394 Virtual Device</b> in the following page and click <b>Next</b>.
</p>
</li><li>
<p>Click <b>Yes</b> when you get a security alert about installing an unsigned driver. The system then processes the INF file and copies and loads the driver for the device that the bus enumerates. In Device Manager, you should see
<b>WDF Hybrid 1394 Virtual Device</b> under the <b>Sample Device</b> node. Reboot your target machine if instructed to do so.</p>
</li><li>
<p>After the drivers are installed, click <b>Select Virtual Test Device</b> on the
<b>1394 Devices</b> menu. Highlight the virtual device and click <b>Ok</b>. At this point you can use menu options to send commands to the virtual device.</p>
</li></ol>
</div>
