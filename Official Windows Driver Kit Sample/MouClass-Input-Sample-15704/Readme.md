# MouClass Input Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* WDM
* Windows Driver
## Topics
* input
* WDK
## IsPublished
* False
## ModifiedDate
* 2012-02-29 04:29:26
## Description

<h3>MouClass Input Sample</h3>
<p>The Mouclass sample driver is a mouse class driver that is compliant with Plug and Play (PnP) on Windows&nbsp;XP and later operating systems.
</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows 8 Consumer Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server 8 Beta </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>Starting in the Visual Studio&nbsp;11 Professional Beta WDK, you can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build
 Engine (MSBuild.exe).</p>
<p class="proch"><b>Building the sample using Visual Studio</b> </p>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\input\mouclass and open the mouclass.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Consumer Preview Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the Visual Studio&nbsp;11 Professional Beta WDK, you can use the Visual Studio
 Command Prompt window for all build configurations.</p>
<p class="proch"><b>Building the sample using the command line (MSBuild)</b> </p>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called mouclass.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\mouclass.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (mouclass.sys) in the binary output directory corresponding to the target platform, for example src\input\mouclass\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<h3><a name="installation"></a>Installation</h3>
<p>This sample is the complete source code for the shipping mouse class driver, which runs always on Windows. This driver is, therefore, always installed. To run a customized driver, rather than the one that the build produces, you will need to replace it in
 the %Windir%\System32\Drivers directory. Please note that mouclass.sys is a system driver and is protected by Windows File Protection.</p>
