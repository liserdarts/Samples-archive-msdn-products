# HClient sample application
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* hid
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:46:27
## Description

<div id="mainSection">
<p>The <i>HClient</i> sample demonstrates how to write a user-mode client application that communicates with HID devices. (These are devices that conform to the HID device class specification.)
</p>
<p>You will find this sample useful if you need to develop an application that communicates with, or extracts information from, a HID device. This sample illustrates a method for detecting a connected HID, opening that device for communication, and extracting
 or formatting the data into, or from, device reports.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539952">Human Input Devices Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539956">Human Input Devices Reference</a>
</dt></dl>
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
<p>Starting in the WDK, you can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe).</p>
<h3><a id="Building_the_sample_using_Visual_Studio"></a><a id="building_the_sample_using_visual_studio"></a><a id="BUILDING_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Building the sample using Visual Studio</h3>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\hid\hclient and open the hclient.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8.1 Debug or Windows&nbsp;8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the WDK, you can use the Visual Studio Command Prompt window for all build
 configurations.</p>
<h3><a id="Building_the_sample_using_the_command_line__MSBuild_"></a><a id="building_the_sample_using_the_command_line__msbuild_"></a><a id="BUILDING_THE_SAMPLE_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building the sample using the command line (MSBuild)</h3>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called hclient.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\hclient.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (hclient.exe) in the binary output directory corresponding to the target platform, for example src\hid\hclient\Windows&nbsp;8.1 Debug.
</li></ol>
<h2>Run the sample</h2>
<h2><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h2>
<p>The HID class consists primarily of devices that are used by humans to control the operation of computer systems. Typical HID devices include keyboards, mouse devices, and joysticks. Non-typical devices might include front-panel controls (knobs, switches,
 or buttons) or controls found on devices such as telephones, VCR remote controls, games, and simulation devices. The underlying common feature of all HID devices is the need for guaranteed delivery of small amounts of non-periodic data.</p>
<p>The basic communication mechanism for HID class devices is the HID report. Every HID device must supply a report descriptor that details the format of the different reports that it creates for its device. The HID class drivers and HID.DLL are provided an
 interface for extracting the relevant data from these reports.</p>
<p>Although the HClient sample is a user-mode application, many of the functions that are available in HID.DLL are available to kernel-mode HID clients as well. The functions that HID.DLL exports have a prefix of either HidD_ or HidP_. All functions with a
 HidP_ prefix are available to kernel-mode clients. However, the mechanism for opening HID devices and obtaining the necessary information such as preparsed data is different in this context.</p>
<p>This sample should be used when testing a HID device or when developing an application that communicates with a HID device.
</p>
<h2><a id="Command_Line_features"></a><a id="command_line_features"></a><a id="COMMAND_LINE_FEATURES"></a>Command Line features</h2>
<p>In addition to running HClient as a Windows application, you can also run it from the command line to:
</p>
<ul>
<li>List all connected HID devices. </li><li>Retrieve specific device information. </li><li>Perform synchronous read operations. </li><li>Perform asynchronous read operations. </li></ul>
<p>The following shows the syntax for each command:</p>
<pre class="syntax"><code>HCLIENT.EXE /?

Syntax:  HCLIENT.EXE &lt;action&gt; [arguments]

action: 1 - List HID devices.

         2 - Show device info.
             arguments: device_num

         3 - Do sync reads by calling HidD_GetInputReport.
             arguments: device_num report_id [msec_to_sleep] [num_of_reads

         4 - Do async reads by calling ReadFile.
             arguments: device_num [num_of_reads]
Press enter to exit.
</code></pre>
<p class="note"><b>Note</b>&nbsp;&nbsp;The command line features do not exist in versions of the app that shipped before Windows&nbsp;8.1.</p>
</div>
