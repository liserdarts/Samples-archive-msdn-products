# Msgsm610 Sample Codec
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* Audio Processing
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:10:43
## Description

<div id="mainSection">
<p>The Msgsm610 sample implements a software codec that performs speech compression on a digital audio stream that contains voice content.
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
<p>Starting in the Visual Studio Professional&nbsp;2012 WDK, you can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine
 (MSBuild.exe).</p>
<h4><a id="Building_the_sample_using_Visual_Studio"></a><a id="building_the_sample_using_visual_studio"></a><a id="BUILDING_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Building the sample using Visual Studio</h4>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\mmedia\gsm610 and open the gsm610.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the Visual Studio Professional&nbsp;2012 WDK, you can use the Visual Studio Command
 Prompt window for all build configurations.</p>
<h4><a id="Building_the_sample_using_the_command_line__MSBuild_"></a><a id="building_the_sample_using_the_command_line__msbuild_"></a><a id="BUILDING_THE_SAMPLE_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building the sample using the command line (MSBuild)</h4>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called msgsm32.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\msgsm32.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (msgsm32.acm) in the binary output directory corresponding to the target platform, for example src\mmedia\gsm610\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<h3><a id="Operation"></a><a id="operation"></a><a id="OPERATION"></a>Operation</h3>
<p>The Msgsm610 sample implements the ETSI GSM 06.10 standard for speech compression.</p>
<p>This sample, like the samples for Msfilter and Imaadpcm, illustrates how to implement an Audio Compression Manager (ACM) software codec.</p>
<p>The sample does not support Plug and Play, power management, or OnNow. These features are not applicable to ACM software codecs.</p>
</div>
