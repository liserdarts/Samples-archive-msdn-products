# Serenum sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* serial
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:50:11
## Description

<div id="mainSection">
<p>Serenum enumerates Plug-n-Play RS-232 devices that are compliant with the current revision of Plug and Play External COM Device. It loads as an upper filter driver to many different RS-232 device drivers that are compliant with its requirements and performs
 this service for them. </p>
<p>Serenum implements the Serenum service; its executable image is serenum.sys.</p>
<p>Serenum is an upper-level device filter driver that is used with a serial port function driver to enumerate the following types of devices that are connected to an RS-232 port:
</p>
<ul>
<li>Plug and Play serial devices that comply with Plug and Play External COM Device Specification, Version 1.00, February 28, 1995.
</li><li>Pointer devices that comply with legacy mouse detection in Windows. </li></ul>
The combined operation of Serial and Serenum provides the function of a Plug and Play bus driver for an RS-232 port. Serenum supports Plug and Play and power management.
<p></p>
<p>Windows provides Serenum to support Serial and other serial port function drivers that need to enumerate an RS-232 port. Hardware vendors do not have to create their own enumerator for RS-232 ports. For example, a device driver can use Serenum to enumerate
 the devices that are attached to the individual RS-232 ports on a multiport device.
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>Enum.c</td>
<td>Functions that enumerate external serial devices—the main purpose of this driver</td>
</tr>
<tr>
<td>Pnp.c</td>
<td>Plug and Play support code</td>
</tr>
<tr>
<td>Power.c </td>
<td>Power support code</td>
</tr>
<tr>
<td>Serenum.c</td>
<td>Basic driver functionality</td>
</tr>
<tr>
<td>Serenum.h</td>
<td>Local header with defines, prototypes</td>
</tr>
<tr>
<td>String.c </td>
<td>String handling support; mainly ASCII to UNICODE functionality</td>
</tr>
<tr>
<td>Serenum.rc </td>
<td>Resource script</td>
</tr>
</tbody>
</table>
For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546505">
Features of Serial and Serenum</a>.</p>
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
<p>You can build the sample in two ways: using Visual Studio&nbsp;2013 or the command line (<i>MSBuild</i>).</p>
<h2><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h2>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8.1 Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio&nbsp;2013 (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8.1 Debug or Windows&nbsp;8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h2><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h2>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
</div>
