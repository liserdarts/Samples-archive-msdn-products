# Usbsamp Generic USB Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* usb
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:47
## Description

<div id="mainSection">
<p>The USBSAMP sample demonstrates how to perform full speed, high speed, and SuperSpeed transfers to and from bulk and isochronous endpoints of a generic USB device. USBSAMP is based on the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff557405">Kernel Mode Driver Framework</a> (KMDF). Superspeed bulk and isochronous transfers only work when the Microsoft USB 3.0 stack is loaded.
</p>
<p>The sample also contains a console test application that initiates bulk (including stream) and isochronous transfers and obtains data from the device's I/O endpoints. The application also demonstrates how to use GUID-based device names and pipe names generated
 by the operating system using the <b>SetupDiXXX</b> user-mode APIs. </p>
<p>For information about USB, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538930">
Universal Serial Bus (USB) Drivers</a>.</p>
<p><b>Hardware requirements</b></p>
<p>The sample driver can be loaded as the function driver for any of these devices:</p>
<ul>
<li>OSR FX2 learning kit. You can get the kit from <a href="http://www.osronline.com/">
OSR Online</a>. </li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn376873">MUTT devices</a>. To order those devices, see
<a href="buses.microsoft_usb_test_tool__mutt__devices#howto">How to get MUTT devices</a>.
</li><li>Intel 82930 USB test board. </li></ul>
If you have a different USB device, you can still use the driver by adding the device's hardware ID to the INX file. Note that the data transfer scenarios will work only with the endpoints supported by the device.
<p></p>
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
<td><dt>Windows&nbsp;8.1 </dt><dt>Windows&nbsp;8 </dt><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt><dt>Windows Server&nbsp;2012 </dt><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click Usbsamp Generic USB Driver.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\Usbsamp Generic USB Driver.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, usbsamp.sln. In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has three projects. There is a driver project named
<b>usbsamp</b>, an application project named <b>usbsamp</b>, and a package project named
<b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘usbsamp’ (3 projects)</b>, and choose
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
<td>usbsamp.sys</td>
<td>The driver file.</td>
</tr>
<tr>
<td>WdfCoinstaller010<i>xx</i>.dll</td>
<td>The coinstaller for version 1.<i>xx</i> of KMDF.</td>
</tr>
<tr>
<td>usbsamp.inf</td>
<td>An INF file that contains information needed to install the driver.</td>
</tr>
<tr>
<td>kmdfsamples.cat</td>
<td>A signed catalog file, which serves as the signature for the entire package.</td>
</tr>
</tbody>
</table>
<h2>Run the sample</h2>
<p>The computer where you install the driver is called the <i>target computer</i> or the
<i>test computer</i>. Typically this is a separate computer from where you develop and build the driver package. The computer where you develop and build the driver is called the
<i>host computer</i>.</p>
<p>The process of moving the driver package to the target computer and installing the driver is called
<i>deploying the driver</i>. You can deploy the USBSAMP sample automatically or manually.</p>
<h2><a id="Automatic_deployment"></a><a id="automatic_deployment"></a><a id="AUTOMATIC_DEPLOYMENT"></a>Automatic deployment</h2>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Install and Verify</b>. Click <b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>.
</li></ol>
<h2><a id="Manual_deployment"></a><a id="manual_deployment"></a><a id="MANUAL_DEPLOYMENT"></a>Manual deployment</h2>
<p>Before you manually deploy a driver, you must turn on test signing and install a certificate on the target computer. You also need to copy the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> tool to the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/dn265571">Preparing a Computer for Manual Driver Deployment</a>.</p>
<ol>
<li>Copy all of the files in your driver package to a folder on the target computer (for example, c:\Usbsamp).
</li><li>
<p>On the target computer, open a Command Prompt window as Administrator. Navigate to your driver package folder, and enter the following command:</p>
<p><b>devcon install usbsamp.inf USB\VID_045E&amp;PID_078F</b></p>
</li></ol>
<h2><a id="View_the_device_in_Device_Manager"></a><a id="view_the_device_in_device_manager"></a><a id="VIEW_THE_DEVICE_IN_DEVICE_MANAGER"></a>View the device in Device Manager</h2>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate the device. For example the device name might be
<b>WDF Sample for FX2 MUTT device</b> under the<b>Sample Device</b> node.</p>
<h2><a id="Build_the_sample_using_MSBuild"></a><a id="build_the_sample_using_msbuild"></a><a id="BUILD_THE_SAMPLE_USING_MSBUILD"></a>Build the sample using MSBuild</h2>
<p>As an alternative to building the USBSAMP sample in Visual Studio, you can build it in a Visual Studio Command Prompt window. In Visual Studio, on the
<b>Tools</b> menu, choose <b>Visual Studio Command Prompt</b>. In the Visual Studio Command Prompt window, navigate to the folder that has the solution file, Usbsamp.sln. Use the MSBuild command to build the solution. Here are some examples:</p>
<p><b>msbuild /p:configuration=”Win7 Debug” /p:platform=”x64” Usbsamp.sln</b></p>
<p><b>msbuild /p:configuration=”Win8 Release” /p:platform=”Win32” Usbsamp.sln</b></p>
<p>For more information about using MSBuild to build a driver package, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>. </p>
<h2><a id="Testing_the_sample"></a><a id="testing_the_sample"></a><a id="TESTING_THE_SAMPLE"></a>Testing the sample</h2>
<p>The sample includes a test application, usbsamp.exe. This console application enumerates the interface registered by the driver and opens the device to send Read, Write, or DeviceIoControl requests based on the command line options. To test the sample,
</p>
<ol>
<li>In Visual Studio, choose <b>Solution Explorer</b> from the <b>View</b> menu. Locate the application project named
<b>usbsamp</b>, under the <b>Exe</b> folder. </li><li>Right-click and choose <b>Build</b>. For example, if your settings are Win8.1 Debug and x64, the application executable is in your solution folder under the exe\x64\Win8.1Debug\usbsamp.exe.
</li><li>Run the executable on the target machine. </li></ol>
<ul>
<li>
<p>To view all descriptors and endpoint information, use the following command.</p>
<p><b>usbsamp.exe -u </b></p>
<p>You can use the preceding command to view pipe numbers for read and write requests.</p>
</li><li>
<p>To send a Read-Write request, use the following command.</p>
<p><b>usbsamp.exe -r 1024 -w 1024 -c 100 -v</b></p>
<p>The preceding command first writes 1024 bytes of data to bulk out endpoint (pipe 1), then reads 1024 bytes from bulk in endpoint (pipe 0), and compares the read buffer with write buffer to see if they match. If the buffer contents match, it performs this
 operation 100 times.</p>
</li><li>
<p>To send Read-Write requests to bulk endpoints, use any of the following commands, simultaneously. If Read-Write requests are sent to a SuperSpeed bulk endpoint with streams, the sample driver always uses the first underlying stream associated with that endpoint.
 The driver is multi-thread safe so it can handle multiple requests at a time. </p>
<p><b>usbsamp.exe -r 65536</b></p>
<p>The preceding command reads 65536 bytes from pipe 0.</p>
<p><b>usbsamp.exe -w 65536</b></p>
<p>The preceding command writes 65536 bytes to pipe 1.</p>
<p><b>usbsamp.exe -r 65536 -i pipe02</b></p>
<p>The preceding command reads 65536 bytes from pipe 2.</p>
<p><b>usbsamp.exe -w 65536 -o pipe03</b></p>
<p>The preceding command writes 65536 bytes to pipe 3.</p>
</li><li>
<p>To send Read and Write requests to isochronous endpoints you can use one or more of these commands simultaneously.
</p>
<p><b>usbsamp.exe -r 512 -i pipe04</b></p>
<p>The preceding command reads 512 bytes from pipe 4.</p>
<p><b>usbsamp.exe -w 512 -o pipe05</b></p>
<p>The preceding command writes 512 bytes to pipe 5.</p>
<p><b>usbsamp.exe -w 1024 -o pipe05 -r 1024 -i pipe04 -c 100 -v</b></p>
<p>The preceding command writes 1024 bytes to pipe 5, then reads 1024 bytes from pipe 4, and compares the buffers to see if they match. If the buffer contents match, it performs this operation 100 times.</p>
</li><li>To skip validation of the data to be read or written in a particular request, use the command with
<b>-x</b> option as follows:
<p><b>usbsamp.exe -r 1024 -w 1024 -c 100 -x</b></p>
</li></ul>
<p></p>
</div>