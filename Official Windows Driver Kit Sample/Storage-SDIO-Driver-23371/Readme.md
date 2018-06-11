# Storage SDIO Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* sd
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:20:44
## Description

<div id="mainSection">
<p></p>
<p>This is a sample for a functional Secure Digital (SD) IO driver. The driver is written using the Kernel Mode Driver Framework. It is a driver for a generic mars development board that implements the SDIO protocol without additional functionality.</p>
<p></p>
<p>The mars board driver exemplifies several different functions that are essential for writing an SDIO driver that leverages KDMF and the SDBUS API. It will show how to:</p>
<ul>
<li>
<p>Install and start an SDIO device. </p>
</li><li>
<p>Release an SDIO device.</p>
</li><li>
<p>Perform data transfers.</p>
</li><li>
<p>Alter the settings that the SDIO device uses to communicate with the SD Host Controller.</p>
</li></ul>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537945">
Secure Digital (SD) Card Drivers</a>.</p>
<p>The driver building and installation instructions given here apply only to Windows XP and later operating systems.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; </p>
<p class="note">This sample provides an example of a minimal driver. Neither the driver nor the sample programs are intended for use in a production environment. Rather, they are intended for educational purposes and as a skeleton driver.</p>
<p></p>
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
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>ntddmars.h </p>
</td>
<td>
<p>Header containing global definitions</p>
</td>
</tr>
<tr>
<td>
<p>mars.inx </p>
</td>
<td>
<p>Device installation file</p>
</td>
</tr>
<tr>
<td>
<p>mars.h </p>
</td>
<td>
<p>Header containing driver specific definitions</p>
</td>
</tr>
<tr>
<td>
<p>mars.c </p>
</td>
<td>
<p>Source code for driver functions</p>
</td>
</tr>
<tr>
<td>
<p>makefile.inc </p>
</td>
<td>
<p>The file stamps the inx file with KMDF version and generates the inf file.</p>
</td>
</tr>
</tbody>
</table>
</div>
