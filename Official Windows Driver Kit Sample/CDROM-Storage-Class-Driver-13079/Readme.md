# CDROM Storage Class Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* StorPort
* Windows Driver
## Topics
* Storage
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:45:12
## Description

<div id="mainSection">
<p>The CD ROM driver is used to provide access to CD, DVD and Blu-ray drives. It supports Plug and Play, Power Management, and AutoRun (media change notification).
</p>
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
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h2><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h2>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Visual Studio Debug and Win32.</p>
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
<p>The in-box CD ROM driver is protected by the system, and thus a normal device driver update attempt through the Device Manager will fail. Users are not encouraged to replace the in-box CD ROM driver. The following work-around is provided in case there is
 a need, but the users are warned that this may harm the system. </p>
<ol>
<li>Locate the &quot;cdrom.inf&quot; file in the binary output directory, and update the file by replacing all &quot;cdrom.sys&quot; occurrences with &quot;mycdrom.sys&quot;.
</li><li>Rename the &quot;cdrom.inf&quot; file to &quot;mycdrom.inf&quot;. </li><li>Copy &quot;mycdrom.sys&quot; and &quot;mycdrom.inf&quot; from the binary output directory to the test machine, if applicable.
</li><li>Launch the Device Manager </li><li>Select the appropriate device under the &quot;DVD/CD-ROM drives&quot; category. </li><li>On the right-click menu, select &quot;Update Driver Software...&quot;. </li><li>Select &quot;Browse my computer for driver software&quot;. </li><li>Select &quot;Let me pick from a list of device drivers on my computer&quot;. </li><li>Click &quot;Have Disk...&quot;, and point to the directory that contains &quot;mycdrom.inf&quot; and &quot;mycdrom.sys&quot;.
</li><li>Click &quot;Next&quot;. If you get a warning dialog about installing unsigned driver, click &quot;Yes&quot;.
</li><li>Click &quot;Next&quot; to complete the driver upgrade. </li><li>After installation completes successfully, &quot;mycdrom.sys&quot; will be the effective driver for the device, &quot;cdrom.sys&quot; will no longer be used.
</li></ol>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff551391">
CD-ROM Drivers</a> in the storage technologies design guide.</p>
</div>
