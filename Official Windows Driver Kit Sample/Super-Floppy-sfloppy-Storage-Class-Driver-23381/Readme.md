# Super Floppy (sfloppy) Storage Class Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* StorPort
* Windows Driver
## Topics
* Storage
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:16
## Description

<div id="mainSection">
<p>The sfloppy sample is a super floppy driver. This driver is a class driver for Super Floppy disk drives.
</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;Vista </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32.</p>
<h4><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h4>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>samplename</i>.sln or
<i>samplename</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h4><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h4>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>samplename</i><b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<h3><a id="Installation_and_Operation"></a><a id="installation_and_operation"></a><a id="INSTALLATION_AND_OPERATION"></a>Installation and Operation</h3>
<p>This sample sits a level above the port driver (ATAPI, USB, and so on) in the driver stack and controls communication between the application level and the port driver. The floppy driver takes requests from file system drivers and then sends the appropriate
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff565393"><b>SCSI_REQUEST_BLOCK</b></a> (SRB) to the port driver.</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559215">
Introduction to Storage Class Drivers</a> in the storage technologies design guide.</p>
</div>
