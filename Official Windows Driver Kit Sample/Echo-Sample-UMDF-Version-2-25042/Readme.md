# Echo Sample (UMDF Version 2)
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
* 2014-04-02 12:51:32
## Description

<div id="mainSection">
<p>The ECHO (UMDF version 2) sample demonstrates how to use a sequential queue to serialize read and write requests presented to the driver.
</p>
<p>It also shows how to synchronize execution of these events with other asynchronous events such as request cancellation and DPC.</p>
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
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click the zip file, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\umdf2echo.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file (umdf2echo.sln). In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that contains 3 projects. There is a driver project (Driver-&gt;AutoSync-&gt;echo), an application project (Exe-&gt;echoapp), and a package project named
<b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution</b>, and choose
<b>Configuration Manager</b>. Set the configuration and the platform. Make sure that the configuration and platform are the same for both the driver project and the package project. Do not check the
<b>Deploy</b> boxes. Because this solution uses UMDF version 2, you cannot select a configuration earlier than Windows&nbsp;8.1.</p>
<h2><a id="Build_the_sample_using_Visual_Studio"></a><a id="build_the_sample_using_visual_studio"></a><a id="BUILD_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Build the sample using Visual Studio</h2>
<p>In Visual Studio, on the <b>Build</b> menu, choose <b>Build Solution</b>.</p>
<p>For more information about using Visual Studio to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2><a id="Locate_the_built_driver_package"></a><a id="locate_the_built_driver_package"></a><a id="LOCATE_THE_BUILT_DRIVER_PACKAGE"></a>Locate the built driver package</h2>
<p>In File Explorer, navigate to the folder that contains your built driver package. The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win8.1 Debug and x64, the package is in your
 solution folder under x64\Win8.1Debug\Package.</p>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy a driver sample automatically or manually.</p>
<h3><a id="Automatic_deployment__root_enumerated_"></a><a id="automatic_deployment__root_enumerated_"></a><a id="AUTOMATIC_DEPLOYMENT__ROOT_ENUMERATED_"></a>Automatic deployment (root enumerated)</h3>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Hardware ID Driver Update</b>, and enter <b>root\ECHO</b> for the hardware ID. Click
<b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Build Solution</b>. </li></ol>
<h3><a id="Manual_deployment__root_enumerated_"></a><a id="manual_deployment__root_enumerated_"></a><a id="MANUAL_DEPLOYMENT__ROOT_ENUMERATED_"></a>Manual deployment (root enumerated)</h3>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\umdf2echoPkg).
</li><li>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter the following command:</p>
<p><b>devcon install echoum.inf root\ECHO</b></p>
</li></ol>
<h3><a id="View_the_root_enumerated_driver_in_Device_Manager"></a><a id="view_the_root_enumerated_driver_in_device_manager"></a><a id="VIEW_THE_ROOT_ENUMERATED_DRIVER_IN_DEVICE_MANAGER"></a>View the root enumerated driver in Device Manager</h3>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <b>Sample WDF ECHO Driver</b> (for example, this might be under the
<b>Sample Device</b> node).</p>
<p>In Device Manager, on the <b>View</b> menu, choose <b>Devices by connection</b>. Locate
<b>Sample WDF ECHO Driver</b> as a child of the root node of the device tree.</p>
<h2><a id="Build_the_sample_using_MSBuild"></a><a id="build_the_sample_using_msbuild"></a><a id="BUILD_THE_SAMPLE_USING_MSBUILD"></a>Build the sample using MSBuild</h2>
<p>As an alternative to building the driver sample in Visual Studio, you can build it in a Visual Studio Command Prompt window. In Visual Studio, on the
<b>Tools</b> menu, choose <b>Visual Studio Command Prompt</b>. In the Visual Studio Command Prompt window, navigate to the folder that has the solution file, umdf2echo.sln. Use the MSBuild command to build the solution. Here is an example:</p>
<p><b>msbuild /p:configuration=”Win8 Release” /p:platform=”Win32” umdf2echo.sln</b></p>
<p>For more information about using MSBuild to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>. </p>
</div>
