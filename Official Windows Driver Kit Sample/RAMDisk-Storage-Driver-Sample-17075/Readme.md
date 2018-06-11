# RAMDisk Storage Driver Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* StorPort
* Windows Driver
## Topics
* Storage
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:48:42
## Description

<div id="mainSection">
<p>The RAMDisk storage driver sample demonstrates how to write a software only function driver using the Kernel Mode Driver Framework (KMDF). This driver creates a RAM disk drive. The RAM disk can be used like any other disk, but the contents of the disk will
 be lost when the computer is shut down.</p>
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
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click ramdisk.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\RamDiskStorageDriver.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, ramdisk.sln. In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has two projects. There is a driver project named
<b>WdfRamdisk</b> and a package project named <b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘ramdisk’ (2 projects)</b>, and choose
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
<p>In File Explorer, navigate to the folder that contains your built driver package. The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, the package is your solution
 folder under x64\Win7Debug\Package.</p>
<p>The package contains these files:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Kmdfsamples.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
<tr>
<td>Ramdisk.inf</td>
<td>An information (INF) file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>WdfCoinstaller010<i>xx</i>.dll</td>
<td>The coinstaller for version 1.<i>xx</i> of KMDF.</td>
</tr>
<tr>
<td>WdfRamdisk.sys</td>
<td>The driver file.</td>
</tr>
</tbody>
</table>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy RAMDisk sample driver automatically or manually.</p>
<h2><a id="Automatic_deployment"></a><a id="automatic_deployment"></a><a id="AUTOMATIC_DEPLOYMENT"></a>Automatic deployment</h2>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Hardware ID Driver Update</b>, and enter <b>Ramdisk</b> for the hardware ID. Click
<b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>.
</li></ol>
<h2><a id="Manual_deployment"></a><a id="manual_deployment"></a><a id="MANUAL_DEPLOYMENT"></a>Manual deployment</h2>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\RamdiskStorageDriverPackage).
</li><li>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter the following command:</p>
<p><b>Devcon install ramdisk.inf Ramdisk </b></p>
</li></ol>
<h2><a id="View_the_installed_driver_in_Device_Manager"></a><a id="view_the_installed_driver_in_device_manager"></a><a id="VIEW_THE_INSTALLED_DRIVER_IN_DEVICE_MANAGER"></a>View the installed driver in Device Manager</h2>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <b>WDF Sample RAM disk Driver</b> (for example, this might be under the
<b>Sample Device</b> node).</p>
<p>The RAM disk sample is a root enumerated software driver. To see this in Device Manager, choose
<b>Devices by connection</b> from the <b>View</b> menu. Locate <b>WDF Sample RAM disk Driver</b> as a child of the root node of the device tree.
</p>
<h2><a id="Save_a_file_on_the_RAM_disk"></a><a id="save_a_file_on_the_ram_disk"></a><a id="SAVE_A_FILE_ON_THE_RAM_DISK"></a>Save a file on the RAM disk</h2>
<p>On the target computer, open a Command Prompt window as Administrator. Enter <b>
R:</b> to switch to the RAM disk drive. In your Command Prompt window, enter <b>notepad</b> to open Notepad. Type some text in your notepad document, and then save the document on the R drive. In your Command Prompt window, enter
<b>dir</b> to verify that the file was saved.</p>
<h2><a id="View_Ramdisk_entries_in_the_Registry"></a><a id="view_ramdisk_entries_in_the_registry"></a><a id="VIEW_RAMDISK_ENTRIES_IN_THE_REGISTRY"></a>View Ramdisk entries in the Registry</h2>
<p>The INF file in the RAM disk driver package specifies parameters that get saved in the registry. On the target computer, open the registry editor (Regedit.exe). In the registry editor, locate the Parameters key for the Ramdisk service. For example,</p>
<p><b>HKLM</b>\<b>SYSTEM</b>\<b>CurrentControlSet</b>\<b>Services</b>\<b>Ramdisk</b>\<b>Parameters</b></p>
The registry key has these entries:
<p></p>
<table>
<tbody>
<tr>
<th>Parameter</th>
<th>Value</th>
<th>Description</th>
</tr>
<tr>
<td>DiskSize</td>
<td>0x100000</td>
<td>The size, in bytes, of the RAM disk drive.</td>
</tr>
<tr>
<td>DriveLetter</td>
<td>R:</td>
<td>The driver letter associated with the RAM disk drive.</td>
</tr>
<tr>
<td>RootDirEntries</td>
<td>0x200</td>
<td>The number of entries in the root directory.</td>
</tr>
<tr>
<td>SectorsPerCluster</td>
<td>0x2</td>
<td>The granularity of the allocation unit.</td>
</tr>
</tbody>
</table>
<h2><a id="Using_MSBuild"></a><a id="using_msbuild"></a><a id="USING_MSBUILD"></a>Using MSBuild</h2>
<p>As an alternative to building the RAMDisk Storage Driver sample in Visual Studio, you can build it in a Visual Studio Command Prompt window. In Visual Studio, on the
<b>Tools</b> menu, choose <b>Visual Studio Command Prompt</b>. In the Visual Studio Command Prompt window, navigate to the folder that has the solution file, ramdisk.sln. Use the
<a href="http://go.microsoft.com/fwlink/p/?linkID=262804">MSBuild</a> command to build the solution. Here are some examples:</p>
<p><b>msbuild /p:configuration=”Win7 Debug” /p:platform=”x64” ramdisk.sln</b></p>
<p><b>msbuild /p:configuration=”Win8 Release” /p:platform=”win32” ramdisk.sln</b></p>
<p>For more information about using <a href="http://go.microsoft.com/fwlink/p/?linkID=262804">
MSBuild</a> to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
</div>
