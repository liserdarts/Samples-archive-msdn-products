# Serial Port Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* serial
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:50:14
## Description

<div id="mainSection">
<p>The Serial (16550-based RS-232) sample driver is a WDF version of the inbox Serial.sys driver in %WINDIR%\system32\drivers.
</p>
<p>This sample driver is functionally equivalent to the inbox driver, with these two exceptions:</p>
<ol>
<li>This sample does not support multi-function serial devices. </li><li>This sample does not support legacy serial ports. Legacy ports are not detected by the BIOS, and are, therefore, not enumerated by the operating system.
</li></ol>
<p></p>
<p>The Serial sample driver runs in kernel mode. </p>
<p>This sample driver supports power management. When a serial port is not in use, the driver places the port hardware in a low-power state. When the port is opened, it receives power and wakes up. The driver supports wake-on-ring for platforms that support
 this function. The driver can be compiled to run on both 32-bit and 64-bit versions of Windows.
</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546505">
Features of Serial and Serenum</a>.</p>
<p>This sample can be used for these hardware IDs without any modification to the .inx file included in the project.
</p>
<ul>
<li>PNP0501 </li><li>PNP0500
<p>If you have other hardware such as an add-in card, then you must add the hardware ID in the .inx as shown in this example. Then, you must build the project as per the instructions given in the Building the sample section in this readme.
</p>
<pre class="syntax"><code>
; For XP and later
[MSFT.NTamd64]
; DisplayName           Section           DeviceId
; -----------           -------           --------
%PNP0500.DevDesc%=       Serial_Inst,      *PNP0500, *PNP0501 ; Communications Port
%PNP0501.DevDesc%=       Serial_Inst,      *PNP0501, *PNP0500 ; Communications Port
%PNP0501.DevDesc%=       Serial_Inst,      MF\PCI9710_COM ; Communications Port</code></pre>
</li></ul>
<p></p>
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
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the Download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click Serial Port Driver.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\Serial Port Driver.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<ol>
<li>Navigate to the folder that has the extracted sample. Double click the solution file, serial.sln under the
<b>C&#43;&#43;</b> folder. </li><li>In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) </li><li>In Solution Explorer, you can see one solution that has two projects. There is a driver project named
<b>WdfSerial</b> and a package project named <b>package</b> (lower case). </li></ol>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<ol>
<li>In Visual Studio, in Solution Explorer, right click <b>Solution ‘serial’ (2 projects)</b>, and choose
<b>Configuration Manager</b>. </li><li>Set the configuration and the platform. Make sure that the configuration and platform are the same for both the driver project and the package project. Do not check the
<b>Deploy</b> boxes. Here are some examples of configuration and platform settings.
<table>
<tbody>
<tr>
<th>Configuration</th>
<th>Platform</th>
<th>Description</th>
</tr>
<tr>
<td>Win8.1 Debug</td>
<td>x64</td>
<td>The driver will run on an x64 hardware platform that is running Windows&nbsp;8.1. The driver will not run on any earlier versions of Windows.</td>
</tr>
<tr>
<td>Win7 Debug</td>
<td>x64</td>
<td>The driver will run on an x64 hardware platform that is running Windows&nbsp;7 or a later version of Windows.</td>
</tr>
</tbody>
</table>
</li></ol>
<h2><a id="Build_the_sample_using_Visual_Studio"></a><a id="build_the_sample_using_visual_studio"></a><a id="BUILD_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Build the sample using Visual Studio</h2>
<p>In Visual Studio, on the <b>Build</b> menu, choose <b>Build Solution</b>.</p>
<p>For more information about using Visual Studio to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2><a id="Locate_the_built_driver_package"></a><a id="locate_the_built_driver_package"></a><a id="LOCATE_THE_BUILT_DRIVER_PACKAGE"></a>Locate the built driver package</h2>
<p>In File Explorer, navigate to the folder that contains your built driver package. The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, the package is your solution
 folder under x64\Win7Debug\Package.</p>
<p>The package contains these files:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Kmdfsamples.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
<tr>
<td>serial.inf</td>
<td>An information (INF) file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>WdfCoinstaller010<i>xx</i>.dll</td>
<td>The coinstaller for version 1.<i>xx</i> of KMDF.</td>
</tr>
<tr>
<td>Wdfserial.sys</td>
<td>The driver file.</td>
</tr>
</tbody>
</table>
<p>You can build the sample in two ways: using Visual Studio&nbsp;2013 or the command line (<i>MSBuild</i>).</p>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy the Serial Port Driver automatically or manually.</p>
<h2><a id="Automatic_deployment"></a><a id="automatic_deployment"></a><a id="AUTOMATIC_DEPLOYMENT"></a>Automatic deployment</h2>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Install and Verify</b>. Click <b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>.
</li></ol>
<h2><a id="Manual_deployment"></a><a id="manual_deployment"></a><a id="MANUAL_DEPLOYMENT"></a>Manual deployment</h2>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\Serial Port Driver Package).
</li><li>Open Device Manager, find <b>Ports (COM &amp; LPT)</b> in the device list. </li><li>Right-click <b>Communication Port (COM<i>n</i>)</b> and select <b>Update driver software...</b> from the context menu.
</li><li>In the wizard, select <b>Browse my computer for driver software</b>. </li><li>Select <b>Let me pick from a list of device drivers on my computer</b>. </li><li>Click <b>Have Disk...</b>. </li><li>Navigate to your driver package folder and select serial.inf and select <b>Open</b> and then
<b>Ok</b>. </li><li>Click <b>Next</b> to load the driver. </li></ol>
<h2><a id="View_the_installed_driver_in_Device_Manager"></a><a id="view_the_installed_driver_in_device_manager"></a><a id="VIEW_THE_INSTALLED_DRIVER_IN_DEVICE_MANAGER"></a>View the installed driver in Device Manager</h2>
<ol>
<li>In Device Manager, locate <b>Ports (COM &amp; LPT)</b> and view its properties.
</li><li>On the <b>Driver</b> tab, click <b>Driver Details</b>. </li><li>Verify that Wdfserial.sys is loaded as the device driver. </li></ol>
<h2><a id="Transfer_files_by_using_the_serial_port"></a><a id="transfer_files_by_using_the_serial_port"></a><a id="TRANSFER_FILES_BY_USING_THE_SERIAL_PORT"></a>Transfer files by using the serial port</h2>
<p>Run Hypertrm.exe or a similar test application to verify that the serial port can be opened, and that it can send and receive data.
</p>
<h2><a id="Using_MSBuild"></a><a id="using_msbuild"></a><a id="USING_MSBUILD"></a>Using MSBuild</h2>
<p>As an alternative to building the Serial Port Driver sample in Visual Studio, you can build it in a Visual Studio Command Prompt window. In Visual Studio, on the
<b>Tools</b> menu, choose <b>Visual Studio Command Prompt</b>. In the Visual Studio Command Prompt window, navigate to the folder that has the solution file, serial.sln. Use the
<a href="http://go.microsoft.com/fwlink/p/?linkID=262804">MSBuild</a> command to build the solution. Here are some examples:</p>
<p><b>msbuild /p:configuration=”Win7 Debug” /p:platform=”x64” serial.sln</b></p>
<p><b>msbuild /p:configuration=”Win8 Release” /p:platform=”win32” serial.sln</b></p>
<p>For more information about using <a href="http://go.microsoft.com/fwlink/p/?linkID=262804">
MSBuild</a> to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
</div>
