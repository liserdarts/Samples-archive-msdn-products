# Serial sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* serial
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:10
## Description

<div id="mainSection">
<p>The Serial (16550-based RS-232) sample driver is a WDF version of the inbox Serial.sys driver in %WINDIR%\system32\drivers.
</p>
<p>This sample driver is functionally equivalent to the inbox driver, with these two exceptions:</p>
<ol>
<li>This sample does not support multi-function serial devices. </li><li>This sample does not support legacy serial ports. Legacy ports are not detected by the BIOS, and are, therefore, not enumerated by the operating system.
</li></ol>
<p></p>
<p>The code in this sample works on Microsoft Windows 2000 and later versions of Windows.
</p>
<p>The Serial sample driver runs in kernel mode. </p>
<p>This sample driver supports power management. When a serial port is not in use, the driver places the port hardware in a low-power state. When the port is opened, it receives power and wakes up. The driver supports wake-on-ring for platforms that support
 this function. The driver can be compiled to run on both 32-bit and 64-bit versions of Windows.
</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546505">
Features of Serial and Serenum</a>.</p>
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
<p>You can build the sample in two ways: using Visual Studio Ultimate&nbsp;2012 or the command line (<i>MSBuild</i>).</p>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio Ultimate&nbsp;2012 (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<p>On your test computer, create an installation directory (for example, C:\Serialsample). Into this directory, copy the Wdfserial.sys and Serial.inf files from the build output directory. In addition, copy the KMDF co-installer file (for example, WdfCoInstaller01009.dll
 from the redist\wdf directory in your installed WDK files) to this installation directory.</p>
<p>Open <b>Device Manager</b>, find <b>Ports (COM &amp; LPT)</b> in the device list, and open the list of ports. Right click on
<b>Communication Port (COM1)</b> and select <b>Update Driver Software</b>. In the
<b>Update Driver Software</b> dialog, click Browse my computer for driver software. Next, click Let me pick from a list of device drivers on my computer, and click Have Disk. In the Install From Disk dialog, browse to the installation directory (for example,
 C:\Serialsample), and complete the installation. </p>
<p>Run Hypertrm.exe or a similar test application to verify that the serial port can be opened, and that it can send and receive data.
</p>
<p>See the description of the Kernel-Mode Driver Framework in the WDK documentation.</p>
<h3><a id="code_tour"></a><a id="CODE_TOUR"></a>CODE TOUR</h3>
<p>File Manifest </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>Serial.htm </p>
</td>
<td>
<p>The documentation for this sample (this file)</p>
</td>
</tr>
<tr>
<td>
<p>Sources </p>
</td>
<td>
<p>The generic file for building this code sample</p>
</td>
</tr>
<tr>
<td>
<p>Error.c </p>
</td>
<td>
<p>Error operations</p>
</td>
</tr>
<tr>
<td>
<p>Flush.c </p>
</td>
<td>
<p>Flush operations</p>
</td>
</tr>
<tr>
<td>
<p>Immediat.c </p>
</td>
<td>
<p>Handles the sending of immediate data</p>
</td>
</tr>
<tr>
<td>
<p>Initunlo.c </p>
</td>
<td>
<p>Performs driver initialization and unload</p>
</td>
</tr>
<tr>
<td>
<p>Ioctl.c </p>
</td>
<td>
<p>IOCTL requests</p>
</td>
</tr>
<tr>
<td>
<p>Isr.c </p>
</td>
<td>
<p>Interrupt service routine functionality</p>
</td>
</tr>
<tr>
<td>
<p>Log.c </p>
</td>
<td>
<p>Debugging log support</p>
</td>
</tr>
<tr>
<td>
<p>Log.h </p>
</td>
<td>
<p>Debugging macro definitions</p>
</td>
</tr>
<tr>
<td>
<p>Modmflow.c </p>
</td>
<td>
<p>Flow control functionality</p>
</td>
</tr>
<tr>
<td>
<p>Openclos.c </p>
</td>
<td>
<p>CreateFile and Close functionality</p>
</td>
</tr>
<tr>
<td>
<p>Pnp.c </p>
</td>
<td>
<p>Plug and Play (PnP) support</p>
</td>
</tr>
<tr>
<td>
<p>Power.c </p>
</td>
<td>
<p>Power support</p>
</td>
</tr>
<tr>
<td>
<p>Precomp.h </p>
</td>
<td>
<p>Pre-compiled header file</p>
</td>
</tr>
<tr>
<td>
<p>Purge.c </p>
</td>
<td>
<p>Purge operations</p>
</td>
</tr>
<tr>
<td>
<p>Qsfile.c </p>
</td>
<td>
<p>Query/set file operations</p>
</td>
</tr>
<tr>
<td>
<p>Read.c </p>
</td>
<td>
<p>Read operations</p>
</td>
</tr>
<tr>
<td>
<p>Registry.c </p>
</td>
<td>
<p>Miscellaneous registry access functions</p>
</td>
</tr>
<tr>
<td>
<p>Serial.h </p>
</td>
<td>
<p>Type definitions and data for serial driver</p>
</td>
</tr>
<tr>
<td>
<p>Serial.rc </p>
</td>
<td>
<p>Resource data</p>
</td>
</tr>
<tr>
<td>
<p>Serlog.mc </p>
</td>
<td>
<p>Log messages</p>
</td>
</tr>
<tr>
<td>
<p>Utils.c </p>
</td>
<td>
<p>Generic helper functionality</p>
</td>
</tr>
<tr>
<td>
<p>Waitmask.c </p>
</td>
<td>
<p>Wait-mask functionality</p>
</td>
</tr>
<tr>
<td>
<p>Wmi.c </p>
</td>
<td>
<p>WMI support</p>
</td>
</tr>
<tr>
<td>
<p>Write.c </p>
</td>
<td>
<p>Write operations</p>
</td>
</tr>
</tbody>
</table>
</div>
