# Windows Filtering Platform MSN Messenger Monitor Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WFP
* Windows Driver
## Topics
* Networking
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:34
## Description

<div id="mainSection">
<p>This sample application and driver demonstrate the stream inspection capabilities of the Windows Filtering Platform (WFP).
</p>
<p>The sample consists of a user mode application (Monitor.exe) that registers traffic of interest. In this case, all Transmission Control Protocol (TCP) data segments that are sent and received by an application of your choice.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Originally this sample was written to monitor the MSN Messenger application. Now it can monitor any application that you specify.</p>
<p>Monitor.exe adds filters and callouts to Windows through the Windows Filtering Platform (WFP) Win32 API. A kernel-mode WFP callout driver (Msnmntr.sys) intercepts TCP traffic and parses out communication patterns. Monitor.exe controls the operations of the
 callout driver through I/O controls (IOCTLs).</p>
<p>The filters and callouts added by Monitor.exe are persistent across system restarts and removed only by Monitor.exe. Adding filters and callouts requires administrator privileges. Therefore, Monitor.exe must be run from an elevated command prompt.</p>
<p>Msnmntr.sys registers itself at two different WFP layers: FLOW-ESTABLISHED and STREAM. For simplicity, only Internet Protocol version 4 (IPv4) traffic is inspected. Msnmntr.sys registers at the FLOW-ESTABLISHED layer to associate a callout driver-specific
 data structure with application identity (that is, path) recorded such that the STREAM layer will only be invoked if traffic is sent or received from that particular application.</p>
<p>After the filters and callouts are in place and registered, WFP indicates TCP data segments to the Msnmntr.sys for inspection. As the data flows through Msnmntr.sys, it copies them (described by a chain of NET_BUFFER_LIST structures) to a flat buffer, parses
 out the communication patterns (such as client-to-server/client-to-client), and sends them to the Windows Software Trace Preprocessor (WPP) for tracing.</p>
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
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click msnmntr.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\msnmntr.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, msnmntr.sln. In Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has three projects. There is a driver project named
<b>msnmntr</b>, a user-mode application project named <b>monitor</b>, and a package project named
<b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘msnmntr’ (3 projects)</b>, and choose
<b>Configuration Manager</b>. Set the configuration and the platform. Make sure that the configuration and platform are the same for all three projects. Do not check the
<b>Deploy</b> boxes. Here are some examples of configuration and platform settings.</p>
<table>
<tbody>
<tr>
<th>Configuration</th>
<th>Platform</th>
<th>Description</th>
</tr>
<tr>
<td>Win8.1 Debug</td>
<td>x64</td>
<td>The driver will run on an x64 hardware platform that is running Windows&nbsp;8.1. The driver will not run on any earlier versions of Windows.</td>
</tr>
<tr>
<td>Win7 Debug</td>
<td>x64</td>
<td>The driver will run on an x64 hardware platform that is running Windows&nbsp;7 or a later version of Windows.</td>
</tr>
</tbody>
</table>
<h2><a id="Set_the_KMDF_version"></a><a id="set_the_kmdf_version"></a><a id="SET_THE_KMDF_VERSION"></a>Set the KMDF version</h2>
<p>The operating system that you specified in your configuration is called the <i>
target operating system</i>. For example, if you specified Win7 Debug in your configuration, your target operating system is Windows 7. In Solution Explorer, right click
<b>msnmntr</b> (the driver project), and choose <b>Properties</b>. Navigate to <b>
Configuration Properties &gt; Driver Model Settings</b>. Set <b>KMDF Version Major</b> to 1. Set
<b>KMDF Version Minor</b> according to your target operating system.</p>
<table>
<tbody>
<tr>
<th>Target operating system</th>
<th>KMDF minor version</th>
</tr>
<tr>
<td>Windows 7</td>
<td>9</td>
</tr>
<tr>
<td>Windows 8</td>
<td>11</td>
</tr>
<tr>
<td>Windows 8.1</td>
<td>13</td>
</tr>
</tbody>
</table>
<h2><a id="Set_the_runtime_library_for_the_user-mode_application"></a><a id="set_the_runtime_library_for_the_user-mode_application"></a><a id="SET_THE_RUNTIME_LIBRARY_FOR_THE_USER-MODE_APPLICATION"></a>Set the runtime library for the user-mode application</h2>
<p>In Solution Explorer, right click <b>monitor</b>, and choose <b>Properties.</b> Navigate to
<b>Configuration Properties &gt; C/C&#43;&#43; &gt; Code Generation</b>. For <b>Runtime Library</b>, select
<b>Multi-threaded Debug (/MTd)</b>.</p>
<h2><a id="Build_the_sample_using_Visual_Studio"></a><a id="build_the_sample_using_visual_studio"></a><a id="BUILD_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Build the sample using Visual Studio</h2>
<p>In Visual Studio, on the <b>Build</b> menu, choose <b>Build Solution</b>. </p>
<p>For more information about using Microsoft Visual Studio to build a driver package, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2><a id="Locate_the_built_driver_package"></a><a id="locate_the_built_driver_package"></a><a id="LOCATE_THE_BUILT_DRIVER_PACKAGE"></a>Locate the built driver package</h2>
<p>In File Explorer, navigate to the folder that contains your built driver package. The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, the package is in your sample
 folder under x64\Win7Debug\package.</p>
<p>The package folder contains these files:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Msnmntr.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
<tr>
<td>Msnmntr.inf</td>
<td>An information (INF) file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>Msnmntr.sys</td>
<td>The driver file.</td>
</tr>
</tbody>
</table>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">The build process might also put WdfCoinstaller010<i>xx</i>.dll in the package folder, but this file is not really part of the driver package. The INF file does not reference any coinstallers. The driver package, which is test-signed, actually
 contains only three files:</p>
<ul>
<li>Msnmntr.cat </li><li>Msnmntr.inf </li><li>Msnmntr.sys </li></ul>
<p class="note">Because the package does not contain a KMDF coinstaller, it is important that you set the KMDF minor version (as described previously) according to your target operating system.</p>
<p></p>
<h2><a id="Locate_the_symbol_file__PDB__for_the_driver"></a><a id="locate_the_symbol_file__pdb__for_the_driver"></a><a id="LOCATE_THE_SYMBOL_FILE__PDB__FOR_THE_DRIVER"></a>Locate the symbol file (PDB) for the driver</h2>
<p>In File Explorer, locate the symbol file, msnmntr.pdb. The location of this file varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, the PDB file is in your sample folder under sys\x64\Win7Debug.</p>
<h2><a id="Locate_the_user-mode_application"></a><a id="locate_the_user-mode_application"></a><a id="LOCATE_THE_USER-MODE_APPLICATION"></a>Locate the user-mode application</h2>
<p>In File Explorer, locate the user-mode application, monitor.exe. The location of this file varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, monitor.exe is in your sample folder under exe\x64\Win7Debug.</p>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy the WFP MSN Messenger Monitor Sample driver automatically or manually.</p>
<h2><a id="Automatic_deployment"></a><a id="automatic_deployment"></a><a id="AUTOMATIC_DEPLOYMENT"></a>Automatic deployment</h2>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>. After you have provisioned the target computer, continue with these steps:</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Do not install</b>. Click <b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Build Solution</b>. </li><li>On the target computer, navigate to DriverTest\Drivers, and locate the file msnmntr.inf. Right click msnmntr.inf, and choose
<b>Install</b>. </li></ol>
<h2><a id="Manual_deployment"></a><a id="manual_deployment"></a><a id="MANUAL_DEPLOYMENT"></a>Manual deployment</h2>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>. After you have prepared the target computer for manual deployment, continue with these steps:</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\WfpMsnMessengerMonitorSamplePackage).
</li><li>
<p>On the target computer, navigate to your driver package folder. Right click msnmntr.inf, and choose
<b>Install</b></p>
</li></ol>
<h2><a id="Copy_additional_files_to_the_target_computer"></a><a id="copy_additional_files_to_the_target_computer"></a><a id="COPY_ADDITIONAL_FILES_TO_THE_TARGET_COMPUTER"></a>Copy additional files to the target computer</h2>
<p>Copy the user-mode application, monitor.exe to a folder on the target computer (for example, c:\WfpMsnMessengerMonitorSampleApp).</p>
<p>Copy the PDB file, msnmntr.pdb to a folder on the target computer (for example, c:\Symbols).</p>
<p>Copy the tool TraceView.exe to a folder on the target computer (for example c:\Tools). TraceView.exe comes with the WDK. You can find it in your WDK installation folder under Tools (for example, c:\Program Files (x86)\Windows Kits\8.1\Tools\x64\TraceView.exe).
</p>
<h2><a id="Start_the_msnmntr_service"></a><a id="start_the_msnmntr_service"></a><a id="START_THE_MSNMNTR_SERVICE"></a>Start the msnmntr service</h2>
<p>On the target computer, open a Command Prompt window as Administrator, and enter
<b>net start msnmntr</b>. (To stop the driver, enter <b>net stop msnmntr</b>.)</p>
<h2><a id="Running_the_user-mode_application"></a><a id="running_the_user-mode_application"></a><a id="RUNNING_THE_USER-MODE_APPLICATION"></a>Running the user-mode application</h2>
<p>On the target computer, open a Command Prompt window as Administrator, and navigate to the folder that contains monitor.exe. Enter
<b>monitor.exe addcallouts</b>. Then enter <b>monitor.exe monitor </b><i>TargetAppPath</i>, where
<i>TargetAppPath</i> is the path to the application that you want to monitor. Here is an example that initiates monitoring of Internet Explorer.</p>
<pre class="syntax"><code>monitor.exe addcallouts
monitor.exe monitor &quot;C:\Program Files (x86)\Internet Explorer\iexplore.exe&quot;</code></pre>
<h2><a id="Start_a_logging_session_in_TraceView"></a><a id="start_a_logging_session_in_traceview"></a><a id="START_A_LOGGING_SESSION_IN_TRACEVIEW"></a>Start a logging session in TraceView</h2>
<p>On the target computer, open TraceView.exe as Administrator. On the <b>File</b> menu, choose
<b>Create New Log Session</b>. Click <b>Add Provider</b>. Select <b>PDB (Debug Information File)</b>, and enter the path to your PDB file, msnmntr.pdb. Click
<b>OK</b>, and finish working through the setup procedure. Open Internet Explorer, and watch the communication patterns being displayed in the Traceview.exe tool.</p>
<p>Tracing for the sample driver can be started at any time before the driver is started or while the driver is already running.</p>
<p>For more information on creating a Windows Filtering Platform Callout Driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571068">Windows Filtering Platform Callout Drivers</a>.</p>
<h2><a id="Using_MSBuild"></a><a id="using_msbuild"></a><a id="USING_MSBUILD"></a>Using MSBuild</h2>
<p>As an alternative to building the WFP MSN Messenger Monitor Sample in Visual Studio, you can build it in a Visual Studio Command Prompt window. In Visual Studio, on the
<b>Tools</b> menu, choose <b>Visual Studio Command Prompt</b>. In the Visual Studio Command Prompt window, navigate to the folder that has the solution file, msnmntr.sln. Use the
<a href="http://go.microsoft.com/fwlink/p/?linkID=262804">MSBuild</a> command to build the solution. Here are some examples:</p>
<p><b>msbuild /p:configuration=”Win7 Debug” /p:platform=”x64” msnmntr.sln</b></p>
<p><b>msbuild /p:configuration=”Win8 Release” /p:platform=”win32” msnmntr.sln</b></p>
<p>For more information about using <a href="http://go.microsoft.com/fwlink/p/?linkID=262804">
MSBuild</a> to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
</div>
