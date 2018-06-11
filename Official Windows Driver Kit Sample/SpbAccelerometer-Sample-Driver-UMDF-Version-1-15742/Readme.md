# SpbAccelerometer Sample Driver (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* SPB
## IsPublished
* True
## ModifiedDate
* 2014-07-28 02:59:35
## Description

<div id="mainSection">
<p>The SpbAccelerometer sample shows how to write a UMDF driver to control a peripheral device that is connected to a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450903">simple peripheral bus</a> (SPB). In this sample, the peripheral device is a sensor (an accelerometer) and the SPB is an I<sup>2</sup>C bus. The SpbAccelerometer sample driver uses Windows
 features that are available starting with Windows&nbsp;8.</p>
<p>The SpbAccelerometer sample driver is written for a sensor device that is permanently connected to the I<sup>2</sup>C bus. This sensor is not a Plug and Play device. Instead, the ACPI system firmware for the hardware platform describes the sensor device's
 bus connection. The Plug and Play manager obtains the bus connection information from the ACPI driver, creates a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698216">connection ID</a> to represent the bus connection, and passes the connection ID to the sample driver as a hardware resource. The sample driver uses the connection ID to open a logical
 connection to the sensor device, and obtains a handle to the connection. The driver specifies this handle as the target for I/O requests that it sends to the device.</p>
<p>To communicate with the sensor class extension, the SpbAccelerometer sample driver calls the methods in the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545503"><b>ISensorClassExtension</b></a> interface, and implements the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545566"><b>ISensorDriver</b></a> interface. These interfaces enable applications to use the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dd318953">Sensor API</a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dd464636">Location API</a> to communicate with the sample driver. Additionally, the sample driver implements an interrupt service routine (ISR) using passive-level interrupts that connects to
 the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406467">
GPIO interrupt</a> from the sensor device.</p>
<p>The SpbAccelerometer sample driver can send <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff550794">
<b>IRP_MJ_READ</b></a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff550819">
<b>IRP_MJ_WRITE</b></a> requests to the sensor device. In addition, the driver can send
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450915"><b>IOCTL_SPB_<i>XXX</i></b></a> requests, which are defined in the Spb.h header file and supported by the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406203">SPB framework extension (SpbCx)</a>. SpbCx is available starting with Windows&nbsp;8.</p>
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
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click SpbAccelerometer Sample Driver (UMDF Version 1).zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\SpbAccelerometer.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, SpbAccelerometer.sln. In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has two projects. There is a driver project named
<b>SpbAccelerometer</b> and a package project named <b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘SpbAccelerometer’ (2 projects)</b>, and choose
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
<td>Win8.1 Debug</td>
<td>Win32</td>
<td>The driver will run on a Win32 hardware platform that is running Windows&nbsp;8.1. The driver will not run on any earlier versions of Windows.</td>
</tr>
</tbody>
</table>
<h2><a id="Build_the_sample_using_Visual_Studio"></a><a id="build_the_sample_using_visual_studio"></a><a id="BUILD_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Build the sample using Visual Studio</h2>
<p>In Visual Studio, on the <b>Build</b> menu, choose <b>Build Solution</b>.</p>
<p>For more information about using Visual Studio to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2><a id="Locate_the_built_driver_package"></a><a id="locate_the_built_driver_package"></a><a id="LOCATE_THE_BUILT_DRIVER_PACKAGE"></a>Locate the built driver package</h2>
<p>In File Explorer, navigate to the folder that contains your built driver package. The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win8.1 Debug and x64, the package is your solution
 folder under x64\Win8.1Debug\Package.</p>
<p>The package contains these files:</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Spbsamples.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
<tr>
<td>SpbAccelerometer.inf</td>
<td>An information (INF) file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>SpbAccelerometer.dll</td>
<td>The driver file.</td>
</tr>
<tr>
<td>WudfUpdate_010<i>xx</i>.dll</td>
<td>The coinstaller for UMDF.</td>
</tr>
</tbody>
</table>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy SpbAccelerometer sample driver automatically or manually.</p>
<h2><a id="Automatic_deployment"></a><a id="automatic_deployment"></a><a id="AUTOMATIC_DEPLOYMENT"></a>Automatic deployment</h2>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn745909">Provision a computer for driver deployment and testing (WDK 8.1)</a>.</p>
<p>Before you deploy the SpbAccelerometer sample driver, you must update the Secondary System Description Table (SSDT) on the target computer. For an example of how to do this, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn760710">Install the sample device and driver on your Sharks Cove board</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Install and Verify</b>. Click <b>OK</b>. </li><li>On the <b>Debug</b> menu, choose <b>Start Debugging</b>. Your driver package gets automatically copied to the target computer. Your driver gets automatically installed and loaded. The Windows user-mode debugger (in Visual Studio on the host computer) automatically
 attaches to the instance of Wudfhost.exe (running on the target computer) that is hosting your driver.
</li></ol>
<h2><a id="Manual_deployment"></a><a id="manual_deployment"></a><a id="MANUAL_DEPLOYMENT"></a>Manual deployment</h2>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<p>Before you deploy the SpbAccelerometer sample driver, you must update the Secondary System Description Table (SSDT) on the target computer. For an example of how to do this, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn760710">Install the sample device and driver on your Sharks Cove board</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\SpbAccelerometerPackage).
</li><li>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter the following command:</p>
<p><b>Devcon update SpbAccelerometer.inf </b><i>HardwareId</i></p>
<p><i>HardwareId</i> is in your INF file. For example, in this INF file entry, the hardware ID is ACPI\SpbAccelerometer.</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>C&#43;&#43;</th>
</tr>
<tr>
<td>
<pre>[Microsoft.NTx86]
%SpbAccelerometer.DeviceDesc% = SpbAccelerometer_Install,ACPI\SpbAccelerometer</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ol>
<h2><a id="View_the_installed_driver_in_Device_Manager"></a><a id="view_the_installed_driver_in_device_manager"></a><a id="VIEW_THE_INSTALLED_DRIVER_IN_DEVICE_MANAGER"></a>View the installed driver in Device Manager</h2>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <b>SPB Accelerometer</b> (for example, this might be under the
<b>Sensors</b> node).</p>
<p>Next, choose <b>Devices by connection</b> from the <b>View</b> menu. Locate <b>
SBP Accelerometer</b> as a child of the ACPI node of the device tree.</p>
</div>
