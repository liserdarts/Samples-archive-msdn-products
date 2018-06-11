# Virtual serial driver sample
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
* 2013-06-25 10:22:58
## Description

<div id="mainSection">
<p>This sample demonstrates serial drivers. It includes a simple virtual serial driver (ComPort) and a controller-less modem driver (FakeModem). This driver supports sending and receiving AT commands using the ReadFile and WriteFile calls or via a TAPI interface
 using an application such as, HyperTerminal. </p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the User-Mode Driver Framework. It is not intended for use in a production environment.
</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546939">
Serial Controller and Device Drivers</a> in the WDK documentation.</p>
<h3>Operating system requirements</h3>
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
<h3>Build the sample</h3>
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>filtername</i>.sln or
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
<p>To install the FakeModem driver:</p>
<ol>
<li>Install the toaster bus driver. Information on how to install the toaster bus driver is available with toaster's readme file.
</li><li>Run the notify.exe (general\toaster\exe\notify) and choose Plug In a device in the Bus menu.
</li><li>In the plug in dialog, specify {b85b7c50-6a01-11d2-b841-00c04fad5171}\fakemodem as the Hardware Id and click okay.
</li></ol>
<p>The bus driver will enumerate a device with the hardware id provided by the application. Open “Device Manager” (devmgmt.msc) and identify the newly enumerated device. This can be done by selecting
<b>View</b> and then <b>Devices</b> by connection from <b>Device Manager</b> and identifying the device that is the child of the toaster bus enumerator.
</p>
<p>At this point, use the installation package to install the driver through <b>Device Manager</b>. The installation package must contain driver binary (FakeModem.dll), the INF file (FakeModem.inf), and UMDF coinstaller (WUDFUpdate_01011.dll).
</p>
<p>To install the ComPort driver, use the installation package to install the driver through
<b>Device Manager</b>. The installation package must contain driver binary (VirtualSerial.dll), the INF file (VirtualSerial.inf), and UMDF coinstaller (WUDFUpdate_01011.dll).
</p>
<h3><a id="Testing_the_sample"></a><a id="testing_the_sample"></a><a id="TESTING_THE_SAMPLE"></a>Testing the sample</h3>
<p>To test the virtual serial driver, first install the device as described in the preceding section. Then, you can communicate with the driver by opening a handle to the appropriate COM port and calling ReadFile or WriteFile functions. Alternatively, you can
 communicate with the serial driver by running <b>HyperTerminal</b> (hypertrm.exe). The AT command set supported by virtual serial includes:
</p>
<table>
<tbody>
<tr>
<td><b>AT</b></td>
<td>returns <b>OK</b></td>
</tr>
<tr>
<td><b>ATA</b></td>
<td>returns <b>CONNECT</b></td>
</tr>
<tr>
<td><b>ATD&lt;number&gt;</b></td>
<td>returns <b>CONNECT</b></td>
</tr>
</tbody>
</table>
<h3><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h3>
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>comsup.cpp &amp; comsup.h </td>
<td>COM Support code - specifically base classes which provide implementations for the standard COM interfaces
<b>IUnknown</b> and <b>IClassFactory</b> which are used throughout the sample.
<p>The implementation of <b>IClassFactory</b> is designed to create instances of the CMyDriver class. If you should change the name of your base driver class, you would also need to modify this file.
</p>
</td>
</tr>
<tr>
<td>dllsup.cpp </td>
<td>DLL Support code - provides the DLL's entry point as well as the single required export (<b>DllGetClassObject</b>).
<p>These depend on comsup.cpp to perform the necessary class creation. </p>
</td>
</tr>
<tr>
<td>exports.def </td>
<td>This file lists the functions that the driver DLL exports. </td>
</tr>
<tr>
<td>internal.h </td>
<td>This is the main header file for the sample driver. </td>
</tr>
<tr>
<td>driver.cpp &amp; driver.h </td>
<td>Definition and implementation of the driver callback class (CMyDriver) for the sample. This includes
<b>DriverEntry</b> and events on the framework driver object. </td>
</tr>
<tr>
<td>device.cpp &amp; driver.h </td>
<td>Definition and implementation of the device callback class (CMyDriver) for the sample. This includes events on the framework device object.
</td>
</tr>
<tr>
<td>queue.cpp &amp; queue.h</td>
<td>Definition and implementation of the base queue callback class (CMyQueue). This includes events on the framework I/O queue object.
</td>
</tr>
<tr>
<td>VirtualSerial.rc /FakeModem.rc </td>
<td>This file defines resource information for the sample driver. </td>
</tr>
<tr>
<td>VirtualSerial.inf / FakeModem.inf </td>
<td>INF file that contains installation information for this driver.</td>
</tr>
</tbody>
</table>
</div>
