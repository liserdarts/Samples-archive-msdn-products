# HClient sample application
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* hid
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:10:49
## Description

<div id="mainSection">
<p>The <i>HClient</i> sample demonstrates how to write a user-mode client application that communicates with HID devices. (These are devices that conform to the HID device class specification.)
</p>
<p>You will find this sample useful if you need to develop an application that communicates with, or extracts information from, a HID device. This sample illustrates a method for detecting a connected HID, opening that device for communication, and extracting
 or formatting the data into, or from, device reports.</p>
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539952">Human Input Devices Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539956">Human Input Devices Reference</a>
</dt></dl>
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
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\hid\hclient and open the hclient.sln project file.
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
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called hclient.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\hclient.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (hclient.exe) in the binary output directory corresponding to the target platform, for example src\hid\hclient\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<h3><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h3>
<p>The HID class consists primarily of devices that are used by humans to control the operation of computer systems. Typical HID devices include keyboards, mouse devices, and joysticks. Non-typical devices might include front-panel controls (knobs, switches,
 or buttons) or controls found on devices such as telephones, VCR remote controls, games, and simulation devices. The underlying common feature of all HID devices is the need for guaranteed delivery of small amounts of non-periodic data.</p>
<p>The basic communication mechanism for HID class devices is the HID report. Every HID device must supply a report descriptor that details the format of the different reports that it creates for its device. The HID class drivers and HID.DLL are provided an
 interface for extracting the relevant data from these reports.</p>
<p>Although the HClient sample is a user-mode application, many of the functions that are available in HID.DLL are available to kernel-mode HID clients as well. The functions that HID.DLL exports have a prefix of either HidD_ or HidP_. All functions with a
 HidP_ prefix are available to kernel-mode clients. However, the mechanism for opening HID devices and obtaining the necessary information such as preparsed data is different in this context.</p>
<p>This sample should be used when testing a HID device or when developing an application that communicates with a HID device.
</p>
</div>
