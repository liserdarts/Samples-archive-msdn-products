# AvScan File System Minifilter Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* File System
* WDM
* Windows Driver
## Topics
* File Systems
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:45
## Description

<div id="mainSection">
<p>The AvScan minifilter is a transaction-aware file scanner. This is an example for developers who intend to write filters that examine data in files. Typically, anti-virus products fall into this category.
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
<p>The default Solution build configuration is Windows&nbsp;8.1 Debug and Win32.</p>
<h3><a id="To_select_a_configuration_and_build_a_driver"></a><a id="to_select_a_configuration_and_build_a_driver"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER"></a>To select a configuration and build a driver</h3>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8.1 Debug or Windows&nbsp;8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h2><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h2>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h3><a id="To_select_a_configuration_and_build_a_driver"></a><a id="to_select_a_configuration_and_build_a_driver"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER"></a>To select a configuration and build a driver</h3>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
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
<h2><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h2>
<p>The <i>AvScan</i> minifilter comprises both kernel-mode and user-mode components. The kernel-mode component recognizes appropriate moments for scanning a file's data and passes it to the user-mode component for further validation. The user-mode component
 creates a number of threads that await validation requests and corresponding data from the kernel-mode component. After scanning the data for occurrences of a &quot;foul&quot; string, the user-mode component sends an appropriate response to the kernel-mode component.
 The kernel mode component will fail requests to open files that are determined to be “infected”.</p>
<p>The kernel-mode component scans files with specific extensions only. The file is first scanned on a successful open. If the file was opened with write access, it is scanned again before a close.
</p>
<p>For more information on file system minifilter design, start with the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff540402">
File System Minifilter Drivers</a> section in the Installable File Systems Design Guide.</p>
</div>
