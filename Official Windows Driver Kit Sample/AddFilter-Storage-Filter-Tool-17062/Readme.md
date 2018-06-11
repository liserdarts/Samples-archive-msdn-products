# AddFilter Storage Filter Tool
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* StorPort
* Windows Driver
## Topics
* Storage
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:29
## Description

<div id="mainSection">
<p>Addfilter is a command-line application that adds and removes filter drivers for a given drive or volume. This application demonstrates how to insert a filter driver into the driver stack of a device. The sample illustrates how to insert such a filter driver
 by using the SetupDi API. </p>
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
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h2><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h2>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8.1 Debug and Win32.</p>
<h3><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h3>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>samplename</i>.sln or
<i>samplename</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8.1 Debug or Windows&nbsp;8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h2><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h2>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h3><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h3>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>samplename</i><b>.vcxproj</b>. </li></ol>
<h2>Run the sample</h2>
<h2><a id="Installation_and_Operation"></a><a id="installation_and_operation"></a><a id="INSTALLATION_AND_OPERATION"></a>Installation and Operation</h2>
<p>This initial sample does not check the filter for validity before it is added to the driver stack. If an invalid filter is added, the specified device might no longer be accessible.
</p>
<p>The minifilter identifies implicit locks when it sees a non-shared write open request on a volume object. In this scenario, the minifilter closes its metadata file and sets a trigger that corresponds to the volume in its instance object. Later, each close
 operation is examined to identify if the implicit lock on the volume is being released and, if so, a re-open of the minifilter's metadata file is triggered.</p>
<p class="note"><b>Important</b>&nbsp;&nbsp;If you attempt to add a non-existing filter to a boot device and then restart, the system might show the INACCESSIBLE_BOOT_DEVICE error message. If this message appears, you will be unable to start the computer. To fix this
 problem, when the startup menu is displayed when the computer starts up, go to the Advanced Options screen and select
<b>Use Last Known Good Profile</b>. </p>
<p>The sample is intended for use with upper filter drivers only. </p>
<p>When you add a filter to a device, that device needs to be restarted. Depending on the device, you might also need to restart your computer. The RestartDevice function (in
<i>Addfilter.c</i>) stops the specified device and then restarts it. If the device has been stopped but not restarted, and the computer is restarted, the restart will not necessarily restart the device. You will need to call the RestartDevice function to restart
 your device.</p>
<p>Because the sample currently enumerates only disk devices, the sample can operate only on devices of this class. To extend this sample code, you can add another command-line argument that handles other device types.
</p>
<p>The following is the command line usage for addfilter:</p>
<p><b>addfilter [/listdevices] [/device device_name] [/add filter] [/remove filter]</b>
</p>
<p>If the device name is not supplied, settings will apply to all devices. If there is no /add or /remove argument, a list of currently installed drivers will be printed.</p>
</div>
