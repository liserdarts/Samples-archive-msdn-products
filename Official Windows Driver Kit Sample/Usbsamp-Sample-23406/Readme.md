# Usbsamp Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* usb
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:39
## Description

<div id="mainSection">
<p>The USBSAMP sample demonstrates how to perform full speed, high speed, and SuperSpeed transfers to and from bulk and isochronous endpoints of a generic USB device. USBSAMP is based on the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff557405">Kernel Mode Driver Framework</a> (KMDF). Superspeed bulk and isochronous transfers only work when the Microsoft USB 3.0 stack is loaded. The sample also contains a console test application
 that initiates bulk (including stream) and isochronous transfers and obtains data from the device's I/O endpoints. The application also demonstrates how to use GUID-based device names and pipe names generated by the operating system using the
<b>SetupDiXXX</b> user-mode APIs. </p>
<p>For information about USB, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538930">
Universal Serial Bus (USB) Drivers</a>.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt><dt>Windows&nbsp;7 </dt><dt>Windows&nbsp;Vista with SP1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt><dt>Windows Server&nbsp;2008&nbsp;R2 </dt><dt>Windows Server&nbsp;2008 with SP1 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32. You can change the default configuration to build for Windows&nbsp;7 or Windows&nbsp;Vista version of the operating system.</p>
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
<p>Make sure your device has been programmed with the device VID/PID in the USBSAMP.inf file. If not, edit the device VID, PID, and description text in the INF to match your test board/device.</p>
<p class="proch"><b>To install the driver</b></p>
<ol>
<li>Attach your device to the target computer. </li><li>Start <b>Device Manager</b>, find the device in the tree. </li><li>Right-click on the device and select <b>Update Driver Software...</b>. </li><li>Choose <b>Browse my computer for driver software</b> and select <b>Let me pick from a list of device drivers on my computer</b>.
</li><li>In the <b>Select your device's type from the list below</b> list, select <b>Show All Devices</b>, and click
<b>Next</b>. </li><li>Click <b>Have Disk...</b>, specify the target media or the installation folder that contains the required INF and SYS files, and click
<b>Ok</b>. </li><li>Click <b>Next</b>.
<p>The system will scan the folder and pick up the matching INF and start the installation.
</p>
<p>You will get a <b>Windows Security</b> dialog indicating that the driver is not signed. Select
<b>Continue Anyway</b>. </p>
<p>The system will copy the KMDF coinstaller (wdfcoinstallerMMmmm.dll), usbsamp.sys, and usbsamp.inf, and start the device.
</p>
</li></ol>
<p>If installation completes successfully, you'll see <b>Windows has successfully updated your driver software</b> in the Wizard. Click
<b>Finish</b>. View the device in <b>Device Manager</b> under <b>Samples</b>.</p>
<p>The sample includes a test application, usbsamp.exe. This console application enumerates the interface registered by the driver and opens the device to send Read, Write, or DeviceIoControl requests based on the command line options.
</p>
<ul>
<li>
<p>To view all descriptors and endpoint information, use the following command.</p>
<p><b>usbsamp.exe -u </b></p>
<p>You can use the preceding command to view pipe numbers for read and write requests.</p>
</li><li>
<p>To send a Read-Write request, use the following command.</p>
<p><b>usbsamp.exe -r 1024 -w 1024 -c 100 -v</b></p>
<p>The preceding command first writes 1024 bytes of data to bulk out endpoint (pipe 1), then reads 1024 bytes from bulk in endpoint (pipe 0), and compares the read buffer with write buffer to see if they match. If the buffer contents match, it performs this
 operation 100 times.</p>
</li><li>
<p>To send Read-Write requests to bulk endpoints, use any of the following commands, simultaneously. If Read-Write requests are sent to a SuperSpeed bulk endpoint with streams, the sample driver always uses the first underlying stream associated with that endpoint.
 The driver is multi-thread safe so it can handle multiple requests at a time. </p>
<p><b>usbsamp.exe -r 65536</b></p>
<p>The preceding command reads 65536 bytes from pipe 0.</p>
<p><b>usbsamp.exe -w 65536</b></p>
<p>The preceding command writes 65536 bytes to pipe 1.</p>
<p><b>usbsamp.exe -r 65536 -i pipe02</b></p>
<p>The preceding command reads 65536 bytes from pipe 2.</p>
<p><b>usbsamp.exe -w 65536 -o pipe03</b></p>
<p>The preceding command writes 65536 bytes to pipe 3.</p>
</li><li>
<p>To send Read and Write requests to isochronous endpoints you can use one or more of these commands simultaneously.
</p>
<p><b>usbsamp.exe -r 510 -i pipe04</b></p>
<p>The preceding command reads 510 bytes from pipe 4.</p>
<p><b>usbsamp.exe -w 510 -o pipe05</b></p>
<p>The preceding command writes 510 bytes to pipe 5.</p>
<p><b>usbsamp.exe -w 1024 -o pipe05 -r 1024 -i pipe04 -c 100 -v</b></p>
<p>The preceding command writes 1024 bytes to pipe 5, then reads 1024 bytes from pipe 4, and compares the buffers to see if they match. If the buffer contents match, it performs this operation 100 times.</p>
</li><li>To skip validation of the data to be read or written in a particular request, use the command with
<b>-x</b> option as follows:
<p><b>usbsamp.exe -r 1024 -w 1024 -c 100 -x</b></p>
</li></ul>
<p></p>
</div>