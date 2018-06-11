# Windows SideShow Sample Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* sideshow
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:19
## Description

<div id="mainSection">
<p>This is the Windows SideShow sample driver. It illustrates how to implement a Windows SideShow driver for a device that is capable of displaying simple bitmaps.
</p>
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
<p>Starting in the Visual Studio Professional&nbsp;2012 WDK, you can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine
 (MSBuild.exe).</p>
<h4><a id="Building_the_sample_using_Visual_Studio"></a><a id="building_the_sample_using_visual_studio"></a><a id="BUILDING_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Building the sample using Visual Studio</h4>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\sideshow and open the
<i>sideshow.sln</i> project file. </li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the Visual Studio Professional&nbsp;2012 WDK, you can use the Visual Studio Command
 Prompt window for all build configurations.</p>
<h4><a id="Building_the_sample_using_the_command_line__MSBuild_"></a><a id="building_the_sample_using_the_command_line__msbuild_"></a><a id="BUILDING_THE_SAMPLE_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building the sample using the command line (MSBuild)</h4>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called WindowsSideShowBasicDriver.vcxproj, navigate to the project directory and enter the following
 MSBuild command: <b>msbuild /t:clean /t:build .\WindowsSideShowBasicDriver.vcxproj</b>.
</li><li>If the build succeeds, you will find the driver (WindowsSideShowBasicDriver.dll) in the binary output directory corresponding to the target platform, for example src\sideshow\NoDevice\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<h3><a id="Operation_and_Design"></a><a id="operation_and_design"></a><a id="OPERATION_AND_DESIGN"></a>Operation and Design</h3>
<p>You can install the sample driver even if you don't have a Windows SideShow-compatible device. Be aware that the sample driver does not interface with any hardware: it merely illustrates how to implement a Windows SideShow driver for a device that can render
 simple bitmaps. </p>
<p>This sample code shows how to create a Windows SideShow driver, and provides comments inline that explain the various aspects of the code. This driver, when compiled, does not do anything. The code contains stubs where you can have the driver talk to your
 device. </p>
<p>The sample code is updated to show how to handle raising events on Windows&nbsp;Vista, Windows&nbsp;7, Windows&nbsp;8 and computers from common source code.</p>
<p>The sample should not be used directly, and must be modified before use. As you begin development of your own Windows SideShow driver, you will want to customize the INF file to match the Plug and Play hardware identifier (HWID) of your specific device.</p>
<p>For more information about Windows SideShow drivers, see <a href="sideshow.windows_sideshow_driver_design_guide">
Windows SideShow Driver Design Guide</a>.</p>
</div>
