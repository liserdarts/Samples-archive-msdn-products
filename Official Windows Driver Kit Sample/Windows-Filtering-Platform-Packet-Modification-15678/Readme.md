# Windows Filtering Platform Packet Modification Sample
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
* 2014-04-02 12:45:29
## Description

<div id="mainSection">
<p>The sample driver demonstrates the packet modification capabilities of the Windows Filtering Platform (WFP).
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
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click your downloaded zip file, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\ddproxy.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, ddproxy.sln. In Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has two projects. There is a driver project named
<b>ddproxy</b> and a package project named <b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘ddproxy’ (2 projects)</b>, and choose
<b>Configuration Manager</b>. Set the configuration and the platform. Make sure that the configuration and platform are the same for both projects. Do not check the
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
<b>ddproxy</b> (the driver project), and choose <b>Properties</b>. Navigate to <b>
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
<td>ddproxy.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
<tr>
<td>ddproxy.inf</td>
<td>An information (INF) file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>ddproxy.sys</td>
<td>The driver file.</td>
</tr>
</tbody>
</table>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">The build process might also put WdfCoinstaller010<i>xx</i>.dll in the package folder, but this file is not really part of the driver package. The INF file does not reference any coinstallers. The driver package, which is test-signed, actually
 contains only three files:</p>
<ul>
<li>ddproxy.cat </li><li>ddproxy.inf </li><li>ddproxy.sys </li></ul>
<p class="note">Because the package does not contain a KMDF coinstaller, it is important that you set the KMDF minor version (as described previously) according to your target operating system.</p>
<p></p>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy the Windows Filtering Platform Packet Modification Sample driver automatically or manually.</p>
<h2><a id="Automatic_deployment"></a><a id="automatic_deployment"></a><a id="AUTOMATIC_DEPLOYMENT"></a>Automatic deployment</h2>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>. After you have provisioned the target computer, continue with these steps:</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Do not install</b>. Click <b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Build Solution</b>. </li><li>On the target computer, navigate to DriverTest\Drivers, and locate the file ddproxy.inf. Right click ddproxy.inf, and choose
<b>Install</b>. </li></ol>
<h2><a id="Manual_deployment"></a><a id="manual_deployment"></a><a id="MANUAL_DEPLOYMENT"></a>Manual deployment</h2>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>. After you have prepared the target computer for manual deployment, continue with these steps:</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\WfpPacketModificationSamplePackage).
</li><li>
<p>On the target computer, navigate to your driver package folder. Right click ddproxy.inf, and choose
<b>Install</b></p>
</li></ol>
<h2><a id="Create_Registry_values"></a><a id="create_registry_values"></a><a id="CREATE_REGISTRY_VALUES"></a>Create Registry values</h2>
<ol>
<li>
<p>On the target computer, open Regedit, and navigate to this key:</p>
<p><b>HKLM</b>\<b>System</b>\<b>CurrentControlSet</b>\<b>Services</b>\<b>ddproxy</b>\<b>Parameters</b></p>
</li><li>
<p>Create a REG_SZ entry named <b>DestinationAddressToIntercept</b> and set it's value to an IPV4 or IPV6 address (example: 10.0.0.1).</p>
</li><li>
<p>Create a REG_SZ entry named <b>NewDestinationAddress</b>, and set it's value to an IPV4 or IPV6 address (example: 10.0.0.2).</p>
</li></ol>
<p>You can also create and set values for the following registry entries.</p>
<ul>
<li><b>InspectUdp</b> (REG_DWORD type): 0 for ICMP and 1 for UDP (default) </li><li><b>DestinationPortToIntercept</b> (REG_DWORD type): UDP port number (applicable if InspectUdp is set to 1)
</li><li><b>NewDestinationPort</b> (REG_DWORD type): UDP port number (applicable if InspectUdp is set to 1)
</li></ul>
<h2><a id="Start_the_ddproxy_service"></a><a id="start_the_ddproxy_service"></a><a id="START_THE_DDPROXY_SERVICE"></a>Start the ddproxy service</h2>
<p>On the target computer, open a Command Prompt window as Administrator, and enter
<b>net start ddproxy</b>. (To stop the driver, enter <b>net stop ddproxy</b>.)</p>
<h2><a id="Remarks"></a><a id="remarks"></a><a id="REMARKS"></a>Remarks</h2>
<p>This sample driver consists of a kernel-mode Windows Filtering Platform (WFP) callout driver (Ddproxy.sys) that intercepts User Datagram Protocol (UDP) and nonerror Internet Control Message Protocol (ICMP) traffic of interest and acts as a redirector. For
 outbound traffic, Ddproxy.sys redirects the traffic to a new destination address and, for UDP, a new UDP port. For inbound traffic, Ddproxy.sys redirects the traffic back to the original address and UDP port values. This redirection is transparent to the application.</p>
<p>Packet modification is done out-of-band by a system worker thread by using the reference-drop-clone-modify-reinject mechanism. Therefore, the sample can serve as a basis for scenarios in which the filtering/modification decision cannot be made within the
<code>classifyFn()</code> callout, but instead must be made, for example, by a user-mode application.</p>
<p>Ddproxy.sys acts as a redirector for both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6) traffic.</p>
<p>For more information on creating a Windows Filtering Platform Callout Driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571068">Windows Filtering Platform Callout Drivers</a>.</p>
</div>
