# WMI ACPI Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Windows Management Interface (WMI)
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:23:21
## Description

<div id="mainSection">
<p>The WMIACPI sample contains ACPI BIOS and Microsoft Windows Management Instrumentation (WMI) sample code that enables instrumentation of the ACPI BIOS from within ACPI Source Language (ASL) code. ASL code can expose data blocks, methods, and events through
 WMI by leveraging the ACPI-WMI mapping driver (Wmiacpi.sys). </p>
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
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\wmi\wmiacpi and open the wmiacpi.sln project file.
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
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called acpimof.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\acpimof.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (acpimofl.dll) in the binary output directory corresponding to the target platform, for example src\wmi\wmiacpi\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<h3><a id="Operation"></a><a id="operation"></a><a id="OPERATION"></a>Operation</h3>
<p>The WMIACPI sample contains files which allow an ACPI BIOS developer to add instrumentation from within ASL code. ASL code can expose data blocks, methods, and events through WMI by leveraging the Wmiacpi.sys driver. For more information about the mechanics
 of writing ASL to expose instrumentation, see the <i>Windows Instrumentation: WMI and ACPI</i> white paper included in this sample and available on the Windows Hardware Developer Central (WHDC) Web site.
</p>
<p>The following table lists the files included in the sample and their function:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>Device.asl</p>
</td>
<td>
<p>ASL code that can be included in the ACPI bios that exposes a set of packages, strings, data, methods and events.</p>
</td>
</tr>
<tr>
<td>
<p>Acpimof.mof</p>
</td>
<td>
<p>Managed object format (MOF) file that contains a description of the data blocks, methods, and events that are exposed. This description is required so that WMI can access the data blocks, methods, and events.
</p>
</td>
</tr>
<tr>
<td>
<p>Acpimof.rc</p>
<p>Acpimof.def</p>
</td>
<td>
<p>Files that are required to build Acpimof.dll, which is a resource-only DLL. </p>
</td>
</tr>
<tr>
<td>
<p>Wmi-Acpi.htm</p>
</td>
<td>
<p>The <i>Windows Instrumentation: WMI and ACPI</i> whitepaper.</p>
</td>
</tr>
<tr>
<td>
<p>acpimov.vcxproj</p>
</td>
<td>
<p>Visual Studio project file for the sample.</p>
</td>
</tr>
<tr>
<td>
<p>acpimof.sln</p>
</td>
<td>
<p>Visual Studio solution file for the sample.</p>
</td>
</tr>
</tbody>
</table>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>To add the sample code to your ACPI bios and access through WMI:</p>
<ol>
<li>Include the contents of <i>Device.asl</i> to your ASL source and rebuild the DSDT. Update the operating system with the new DSDT through reflashing.
</li><li>Build <i>Acpimof.dll</i> in the WMIACPI directory. <i>Acpimof.dll</i> is a resource-only DLL that contains the compiled MOF in a form that WMI can import into its schema.
</li><li>Copy <i>Acpimof.dll</i> to %windir%\system32 and add a value named &quot;MofImagePath&quot; under the HKEY_LOCAL_MACHINE\CurrentControlSet\Services\WmiAcpi key. The contents of the value should be a path to the
<i>Acpimof.dll</i> file. </li><li>Restart your computer. When Plug and Play (PnP) recognizes the new device with a pnpid of pnp0c14, it will install
<i>Wmiacpi.sys</i> automatically and make the MOF resource in Acpimof.dll available to the WMI schema.
</li></ol>
<p>Note that you do not need an INF file because Windows supplies an INF for the ACPI-WMI mapping driver device as part of the operating system.</p>
</div>
