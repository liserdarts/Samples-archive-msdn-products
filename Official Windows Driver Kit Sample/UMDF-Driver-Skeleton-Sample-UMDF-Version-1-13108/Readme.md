# UMDF Driver Skeleton Sample (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:41
## Description

<div id="mainSection">
<p>This sample demonstrates how to use version 1 of the User-Mode Driver Framework to write a minimal driver.
</p>
<p>The Skeleton driver will successfully load on a device (either root enumerated or a real hardware device) but does not support any I/O operations.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560456">User-Mode Driver Framework</a>
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
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click umdfSkeleton.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\UmdfSkeleton.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, UmdfSkeleton.sln. In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has two projects. There is a driver project named
<b>UMDFSkeleton</b> and a package project named <b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘UMDFSkeleton’ (2 projects)</b>, and choose
<b>Configuration Manager</b>. Set the configuration and the platform. Make sure that the configuration and platform are the same for both the driver project and the package project. Do not check the
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
<h2><a id="Build_the_sample_using_Visual_Studio"></a><a id="build_the_sample_using_visual_studio"></a><a id="BUILD_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Build the sample using Visual Studio</h2>
<p>In Visual Studio, on the <b>Build</b> menu, choose <b>Build Solution</b>.</p>
<p>For more information about using Visual Studio to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2><a id="Locate_the_built_driver_package"></a><a id="locate_the_built_driver_package"></a><a id="LOCATE_THE_BUILT_DRIVER_PACKAGE"></a>Locate the built driver package</h2>
<p>In File Explorer, navigate to the folder that contains your built driver package. The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, the package is in your solution
 folder under x64\Win7Debug\Package.</p>
<p>The package contains these files:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>UMDFSkeleton.dll</td>
<td>The driver file.</td>
</tr>
<tr>
<td>UMDFSkeleton_OSR.inf</td>
<td>An information (INF) file that contains information needed to install the driver for the OSR USB-FX2 board.</td>
</tr>
<tr>
<td>UMDFSkeleton_Root.inf</td>
<td>An INF file that contains information needed to install the driver as a root enumerated software driver.</td>
</tr>
<tr>
<td>wudf.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
</tbody>
</table>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy the UMDF Driver Skeleton Sample automatically or manually.</p>
<h3><a id="Automatic_deployment__root_enumerated_"></a><a id="automatic_deployment__root_enumerated_"></a><a id="AUTOMATIC_DEPLOYMENT__ROOT_ENUMERATED_"></a>Automatic deployment (root enumerated)</h3>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Hardware ID Driver Update</b>, and enter <b>UMDFSamples\Skeleton</b> for the hardware ID. Click
<b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>.
</li></ol>
<h3><a id="Automatic_deployment__FX2_board_"></a><a id="automatic_deployment__fx2_board_"></a><a id="AUTOMATIC_DEPLOYMENT__FX2_BOARD_"></a>Automatic deployment (FX2 board)</h3>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>Plug in the OSR USB-FX2 board to the target computer. </li><li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Install and Verify</b>. Click <b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>.
</li></ol>
<h3><a id="Manual_deployment__root_enumerated_"></a><a id="manual_deployment__root_enumerated_"></a><a id="MANUAL_DEPLOYMENT__ROOT_ENUMERATED_"></a>Manual deployment (root enumerated)</h3>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\WfpMsnMessengerMonitorSamplePackage).
</li><li>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter the following command:</p>
<p><b>devcon install UMDFSkeleton_Root.inf UMDFSamples\Skeleton </b></p>
</li></ol>
<h3><a id="Manual_deployment__FX2_board_"></a><a id="manual_deployment__fx2_board_"></a><a id="MANUAL_DEPLOYMENT__FX2_BOARD_"></a>Manual deployment (FX2 board)</h3>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\WfpMsnMessengerMonitorSamplePackage).
</li><li>
<p>Plug in the OSR USB-FX2 board to the target computer. Open a Command Prompt window and enter
<b>dvmgmt</b> to open Device Manager. In Device Manager, locate the node for the OSR USB-FX2 board. Right click the node, and choose
<b>Properties</b>. In the <b>Details</b> tab, under <b>Properties</b>, choose <b>
Hardware Ids</b>. Note the hardware IDs listed for your FX2 board. One of these IDs should match one of the IDs in the UmdfSkeleton_OSR.inf file. For example, Device Manager might show an ID of USB\VID_0547&amp;PID_1002, which matches one of the IDs in the
 [Microsoft.<i>xxx</i>] section of UmdfSkeleton_OSR.inf.</p>
</li><li>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter this command:</p>
<p><b>devcon update UMDFSkeleton_OSR.inf &quot;</b><i>HardwareID</i><b>&quot;</b></p>
<p>where <i>HardwareID</i> is the hardware ID of your FX2 board. Here is an example:</p>
<p><b>devcon update UMDFSkeleton_OSR.inf &quot;USB\VID_0547&amp;PID_1002&quot;</b></p>
</li></ol>
<h3><a id="View_the_root_enumerated_driver_in_Device_Manager"></a><a id="view_the_root_enumerated_driver_in_device_manager"></a><a id="VIEW_THE_ROOT_ENUMERATED_DRIVER_IN_DEVICE_MANAGER"></a>View the root enumerated driver in Device Manager</h3>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <b>Microsoft Skeleton User-Mode Device Sample</b> (for example, this might be under the
<b>Sample Device</b> node).</p>
<p>In Device Manager, on the <b>View</b> menu, choose <b>Devices by connection</b>. Locate
<b>Microsoft Skeleton User-Mode Device Sample</b> as a child of the root node of the device tree.
</p>
<h3><a id="View_the_driver_for_the_OSR_USB-FX2_board_in_Device_Manager"></a><a id="view_the_driver_for_the_osr_usb-fx2_board_in_device_manager"></a><a id="VIEW_THE_DRIVER_FOR_THE_OSR_USB-FX2_BOARD_IN_DEVICE_MANAGER"></a>View the driver for the OSR USB-FX2
 board in Device Manager</h3>
<p>On the target computer, in your Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <b>Microsoft Skeleton User-Mode Driver on OSR USB Device Sample</b> (for example, this might be under the<b> Sample Device</b> node).</p>
<p>In Device Manager, on the <b>View</b> menu, choose <b>Devices by connection</b>. Locate
<b>Microsoft Skeleton User-Mode Driver on OSR USB Device Sample</b> as a child of a USB hub node.</p>
<h2><a id="Build_the_sample_using_MSBuild"></a><a id="build_the_sample_using_msbuild"></a><a id="BUILD_THE_SAMPLE_USING_MSBUILD"></a>Build the sample using MSBuild</h2>
<p>As an alternative to building the UMDF Driver Skeleton sample in Visual Studio, you can build it in a Visual Studio Command Prompt window. In Visual Studio, on the
<b>Tools</b> menu, choose <b>Visual Studio Command Prompt</b>. In the Visual Studio Command Prompt window, navigate to the folder that has the solution file, UmdfSkeleton.sln. Use the MSBuild command to build the solution. Here are some examples:</p>
<p><b>msbuild /p:configuration=”Win7 Debug” /p:platform=”x64” UmdfSkeleton.sln</b></p>
<p><b>msbuild /p:configuration=”Win8 Release” /p:platform=”Win32” UmdfSkeleton.sln</b></p>
<p>For more information about using MSBuild to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>. </p>
<h2><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th colspan="2">Description</th>
</tr>
<tr>
<td>
<p>comsup.cpp &amp; comsup.h</p>
</td>
<td colspan="2">
<p>COM Support code - specifically base classes which provide implementations for the standard COM interfaces IUnknown and IClassFactory which are used throughout the skeleton sample.</p>
<p>The implementation of IClassFactory is designed to create instances of the CMyDriver class. If you should change the name of your base driver class, you would also need to modify this file.</p>
</td>
</tr>
<tr>
<td>
<p>dllsup.cpp</p>
</td>
<td colspan="2">
<p>DLL Support code - provides the DLL's entry point as well as the single required export (DllGetClassObject).</p>
<p>These depend on comsup.cpp to perform the necessary class creation.</p>
</td>
</tr>
<tr>
<td>
<p>exports.def</p>
</td>
<td colspan="2">
<p>This file lists the functions that the driver DLL exports.</p>
</td>
</tr>
<tr>
<td>
<p>internal.h</p>
</td>
<td colspan="2">
<p>This is the main header file for the skeleton driver. </p>
</td>
</tr>
<tr>
<td>
<p>driver.cpp &amp; driver.h</p>
</td>
<td colspan="2">
<p>Definition and implementation of the driver callback class for the Skeleton sample.
</p>
</td>
</tr>
<tr>
<td>
<p>device.cpp &amp; driver.h</p>
</td>
<td colspan="2">
<p>Definition and implementation of the device callback class for the Skeleton sample.</p>
</td>
</tr>
<tr>
<td>
<p>skeleton.rc</p>
</td>
<td colspan="2">
<p>This file defines resource information for the Skeleton sample driver.</p>
</td>
</tr>
<tr>
<td>
<p>UMDFSkeleton_OSR.inx</p>
</td>
<td colspan="2">
<p>File for installing the Skeleton driver to control the OSR USB-FX2 Learning Kit device. The build process converts this into an INF file.</p>
</td>
</tr>
<tr>
<td>
<p>UMDFSkeleton_Root.inx</p>
</td>
<td colspan="2">
<p>File for installing the Skeleton driver to control a root enumerated device with a hardware ID of UMDFSamples\Skeleton. The build process converts this into an INF file.</p>
</td>
</tr>
</tbody>
</table>
</div>
