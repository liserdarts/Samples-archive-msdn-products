# CDO File System Minifilter Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* File System
* WDM
* Windows Driver
## Topics
* File Systems
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:14:15
## Description

<div id="mainSection">
<p>The CDO minifilter sample is an example if you intend to use a control device object (CDO) with your minifilters.
</p>
<p>Although the filter manager infrastructure provides a message interface for communication between applications and minifilters, you might need explicit CDOs while the minifilters interface with legacy software. This sample shows how to create and use a CDO
 with minifilters. </p>
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
<h4><a id="To_select_a_configuration_and_build_a_driver"></a><a id="to_select_a_configuration_and_build_a_driver"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER"></a>To select a configuration and build a driver</h4>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h4><a id="To_select_a_configuration_and_build_a_driver"></a><a id="to_select_a_configuration_and_build_a_driver"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER"></a>To select a configuration and build a driver</h4>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>The minifilter samples come with an INF file that will install the minifilter. To install the minifilter, do the following:</p>
<ol>
<li>
<p>Make sure that <i>filtername</i>.sys and <i>filtername</i>.inf are in the same directory.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This installation will make the necessary registry updates to register the minifilter service and place
<i>filtername</i>.sys in the %SystemRoot%\system32\drivers directory.</p>
</li><li>
<p>In Windows Explorer, right-click <i>filtername</i>.inf, and click <b>Install</b>.</p>
</li><li>
<p>To load the minifilter, run <b>fltmc load </b><i>filtername</i> or <b>net start
</b><i>filtername</i>. </p>
</li></ol>
<h3><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h3>
<p>When the CDO minifilter is deployed, it creates a CDO object named &quot;FileSystem\Filters\CdoSample&quot; in the Microsoft Windows object namespace and enables applications to open it and perform certain operations on it.
</p>
<p>For more information on file system minifilter design, start with the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff540402">
File System Minifilter Drivers</a> section in the Installable File Systems Design Guide.</p>
</div>
