# Virtual serial driver sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* serial
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:52:11
## Description

<div id="mainSection">
<p>This sample demonstrates these two serial drivers: </p>
<ul>
<li>A simple virtual serial driver (ComPort) </li><li>A controller-less modem driver (FakeModem).This driver supports sending and receiving AT commands using the ReadFile and WriteFile calls or via a TAPI interface using an application such as, HyperTerminal.
</li></ul>
<p></p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the User-Mode Driver Framework. It is not intended for use in a production environment.
</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546939">
Serial Controller and Device Drivers</a> in the WDK documentation.</p>
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
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click Virtual serial driver sample.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\VirtualSerialDriverSample.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, VirtualSerial.sln. In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has two driver projects named
<b>ComPort</b> and <b>FakeModem</b> and a package project named <b>package</b> (lower case).</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘VirtualSerial’ (3 projects)</b>, and choose
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
<td>fakemodem.dll</td>
<td>The driver file for the modem.</td>
</tr>
<tr>
<td>fakemodem.inf</td>
<td>An information (INF) file that contains information needed to install the driver for the modem.</td>
</tr>
<tr>
<td>virtualserial.dll</td>
<td>The driver file for the virtual serial port.</td>
</tr>
<tr>
<td>virtualserial.dll</td>
<td>An INF file that contains information needed to install the driver as a root enumerated software driver for the virtual serial port.</td>
</tr>
<tr>
<td>WdfCoinstaller010<i>xx</i>.dll</td>
<td>The coinstaller for version 1.<i>xx</i> of KMDF.</td>
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
<h3><a id="Installing_the_VirtualSerial_driver"></a><a id="installing_the_virtualserial_driver"></a><a id="INSTALLING_THE_VIRTUALSERIAL_DRIVER"></a>Installing the VirtualSerial driver</h3>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Hardware ID Driver Update</b>, and enter <b>UMDF\VirtualSerial</b> Note that regardless of the hardware ID specified here, both drivers are deployed but only the specified device is installed on the target computer. Click
<b>OK</b>. </li><li>On the <b>Build</b> menu, choose <b>Deploy Package</b> or <b>Build Solution</b>.
</li></ol>
<p>On the target computer, in a Command Prompt window, enter <b>devmgmt</b> to open Device Manager. In Device Manager, on the
<b>View</b> menu, choose <b>Devices by type</b>. In the device tree, locate <b>Microsoft VirtualSerial User-Mode Device Sample (COM<i>n</i>)</b> under the
<b>Ports (COM &amp;LPT)</b> node.</p>
<h3><a id="Installing_the_FakeModem_driver"></a><a id="installing_the_fakemodem_driver"></a><a id="INSTALLING_THE_FAKEMODEM_DRIVER"></a>Installing the FakeModem driver</h3>
<p>Before you automatically deploy a driver, you must provision the target computer. For instructions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698272">Configuring a Computer for Driver Deployment, Testing, and Debugging</a>.</p>
<ol>
<li>Install the toaster bus driver (dynambus.sys). Information on how to install the toaster bus driver, see
<a href="http://code.msdn.microsoft.com/windowshardware/Toaster-7d256224#content">
Toaster</a>. </li><li>On the target computer, open a Command Prompt window and enter <b>dvmgmt</b> to open Device Manager.
</li><li>In Device Manager, make sure that the <b>Toaster Dynamic Bus Enumerator</b> node appears under
<b>System Devices</b>. </li><li>Copy notify.exe (general\toaster\exe\notify) to the target computer and run the application as administrator.
</li><li>Choose <b>Bus</b> &gt; <b>Plugin</b>. </li><li>In the <b>Plug in Device</b> dialog, specify <b>Serial Number</b> as 1 and <b>
Device Id</b> as {b85b7c50-6a01-11d2-b841-00c04fad5171}\fakemodem. </li><li>Click <b>OK</b> to start device enumeration. </li><li>In Device Manager, on the <b>View</b> menu, choose <b>Devices by connection</b>. Locate
<b>Toaster Dynamic Bus Enumerator</b>. Notice that the child of the node. That node is the raw PDO for the fake modem device.
</li><li>On the host computer, in Visual Studio, in Solution Explorer, right click <b>
package</b> (lower case), and choose <b>Properties</b>. Navigate to <b>Configuration Properties &gt; Driver Install &gt; Deployment</b>.
</li><li>Check <b>Enable deployment</b>, and check <b>Remove previous driver versions before deployment</b>. For
<b>Target Computer Name</b>, select the name of a target computer that you provisioned previously. Select
<b>Hardware ID Driver Update</b>, and enter <b>{b85b7c50-6a01-11d2-b841-00c04fad5171}\fakemodem</b>. Click
<b>OK</b>. </li></ol>
<p>On the target computer, in Device Manager, on the <b>View</b> menu, choose <b>
Devices by type</b>. In the device tree, locate <b>Microsoft Fake Modem User-Mode Device Sample #2</b> under the<b> Modem</b> node.</p>
<p>On the <b>View</b> menu, choose <b>Devices by connection</b>. Locate <b>Microsoft Fake Modem User-Mode Device Sample #2</b> as a child of
<b>Toaster Dynamic Bus Enumerator</b>.</p>
<p>The bus driver, in this case the Toaster Dynamic Bus driver, creates a PDO for the specified hardware ID. At this time, you can view the PDO in Device Manager. It appears as a child node under the bus driver node. No driver is associated with this child
 device object. In the registry you can see the settings associated with it under the HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\{b85b7c50-6a01-11d2-b841-00c04fad5171}\&lt;instance&gt;.
</p>
<p>To install the driver, you must specify the driver install package. Visual Studio builds the package and then deploys it to the target computer, where it's stored under C:\DriverTest\Driver. For the Fake Modem device, its INF specifies the device class as
 Modem and Modem.sys as the function driver. The INF also specifies the Reflector driver (Wudfrd.sys) as a lower device filter driver. The kernel mode device stack is as follows:</p>
<p><img src="/windowshardware/site/view/file/112060/1/image.png" alt="" align="middle">
</p>
<p>In the user-mode stack, FakeModem.dll is loaded as the device driver. When an application that wants to communicate with the device initiates requests, the I/O Manager creates the neccassary IRP and passes it to the reflector. The reflector reroutes the
 request back to device driver in user mode. That driver either handles the request or sends it to the kernel-mode driver, Modem.sys.
</p>
<h2><a id="Testing_the_sample"></a><a id="testing_the_sample"></a><a id="TESTING_THE_SAMPLE"></a>Testing the sample</h2>
<p>To test the virtual serial driver, first install the device as described in the preceding section. Then, you can communicate with the driver by opening a handle to the appropriate COM port and calling ReadFile or WriteFile functions. Alternatively, you can
 communicate with the serial driver by running <b>HyperTerminal</b> (hypertrm.exe). The AT command set supported by virtual serial includes:
</p>
<table>
<tbody>
<tr>
<td><b>AT</b></td>
<td>returns <b>OK</b></td>
</tr>
<tr>
<td><b>ATA</b></td>
<td>returns <b>CONNECT</b></td>
</tr>
<tr>
<td><b>ATD&lt;number&gt;</b></td>
<td>returns <b>CONNECT</b></td>
</tr>
</tbody>
</table>
<h2><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h2>
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>comsup.cpp &amp; comsup.h </td>
<td>COM Support code - specifically base classes which provide implementations for the standard COM interfaces
<b>IUnknown</b> and <b>IClassFactory</b> which are used throughout the sample.
<p>The implementation of <b>IClassFactory</b> is designed to create instances of the CMyDriver class. If you should change the name of your base driver class, you would also need to modify this file.
</p>
</td>
</tr>
<tr>
<td>dllsup.cpp </td>
<td>DLL Support code - provides the DLL's entry point as well as the single required export (<b>DllGetClassObject</b>).
<p>These depend on comsup.cpp to perform the necessary class creation. </p>
</td>
</tr>
<tr>
<td>exports.def </td>
<td>This file lists the functions that the driver DLL exports. </td>
</tr>
<tr>
<td>internal.h </td>
<td>This is the main header file for the sample driver. </td>
</tr>
<tr>
<td>driver.cpp &amp; driver.h </td>
<td>Definition and implementation of the driver callback class (CMyDriver) for the sample. This includes
<b>DriverEntry</b> and events on the framework driver object. </td>
</tr>
<tr>
<td>device.cpp &amp; driver.h </td>
<td>Definition and implementation of the device callback class (CMyDriver) for the sample. This includes events on the framework device object.
</td>
</tr>
<tr>
<td>queue.cpp &amp; queue.h</td>
<td>Definition and implementation of the base queue callback class (CMyQueue). This includes events on the framework I/O queue object.
</td>
</tr>
<tr>
<td>VirtualSerial.rc /FakeModem.rc </td>
<td>This file defines resource information for the sample driver. </td>
</tr>
<tr>
<td>VirtualSerial.inf / FakeModem.inf </td>
<td>INF file that contains installation information for this driver.</td>
</tr>
</tbody>
</table>
</div>
