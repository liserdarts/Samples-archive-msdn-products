# SpbAccelerometer
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* SPB
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:36
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
<p>The SpbAcclerometer sample driver can send <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff550794">
<b>IRP_MJ_READ</b></a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff550819">
<b>IRP_MJ_WRITE</b></a> requests to the sensor device. In addition, the driver can send
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450915"><b>IOCTL_SPB_<i>XXX</i></b></a> requests, which are defined in the Spb.h header file and supported by the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406203">SPB framework extension (SpbCx)</a>. SpbCx is available starting with Windows&nbsp;8.</p>
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
<h3>Run the sample</h3>
<p>To install the sample driver, follow these steps:</p>
<ol>
<li>Ensure that the driver builds without errors. </li><li>Copy the DLL and INF files to a separate folder. </li><li>Enter the command &quot;<code>devcon.exe update SpbAccelerometer.inf ACPI\&lt;HWID&gt;</code>&quot; to install the driver on an existing device node. This node must declare I<sup>2</sup>C and GPIO resources in firmware. You can find the devcon.exe program in the
 tools\devcon folder where you installed the WDK. </li></ol>
<h3><a id="File_manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File manifest</h3>
<p>The following source files are in the src\SPB\SpbAccelerometer folder and are used to build the SpbAccelerometer.sys and SpbAccelerometer.inf files.</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>AccelerometerDevice.h, AccelerometerDevice.cpp</p>
</td>
<td>
<p>Device-level methods to configure sensor, set properties, and query data.</p>
</td>
</tr>
<tr>
<td>
<p>Adxl345.h</p>
</td>
<td>
<p>Device's register set definition and defines</p>
</td>
</tr>
<tr>
<td>
<p>ClientManager.h, ClientManager.cpp</p>
</td>
<td>
<p>Implements the driver's client tracking logic, including arbitration of settable properties.</p>
</td>
</tr>
<tr>
<td>
<p>Device.h, Device.cpp</p>
</td>
<td>
<p>WDF PnP event callbacks and helper methods</p>
</td>
</tr>
<tr>
<td>
<p>DllMain.cpp</p>
</td>
<td>
<p>Driver's entry point and exported functions for providing COM support</p>
</td>
</tr>
<tr>
<td>
<p>Driver.h, Driver.cpp</p>
</td>
<td>
<p>WDF driver entry event callbacks and helper methods</p>
</td>
</tr>
<tr>
<td>
<p>Internal.h</p>
</td>
<td>
<p>Common includes</p>
</td>
</tr>
<tr>
<td>
<p>makefile.inc</p>
</td>
<td>
<p>Defines custom build actions. Includes the conversion of the .INX file into a .INF file.</p>
</td>
</tr>
<tr>
<td>
<p>Queue.h, Queue.cpp</p>
</td>
<td>
<p>Implementation of <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff556852">
<b>IQueueCallbackDeviceIoControl</b></a>.</p>
</td>
</tr>
<tr>
<td>
<p>ReportManager.h, ReportManager.cpp</p>
</td>
<td>
<p>Maintains the driver's report interval.</p>
</td>
</tr>
<tr>
<td>
<p>Request.idl</p>
</td>
<td>
<p>Defines the request interface for communicating with the sensor device.</p>
</td>
</tr>
<tr>
<td>
<p>SensorDdi.h, SensorDdi.cpp</p>
</td>
<td>
<p>Implementation of the sensor driver callback interface, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545566">
<b>ISensorDriver</b></a>.</p>
</td>
</tr>
<tr>
<td>
<p>SensorDevice.idl</p>
</td>
<td>
<p>Defines the interface between the sensor DDI and the sensor device implementations.</p>
</td>
</tr>
<tr>
<td>
<p>sources</p>
</td>
<td>
<p>Lists source files and build options</p>
</td>
</tr>
<tr>
<td>
<p>sources.dep</p>
</td>
<td>
<p>Defines build dependencies</p>
</td>
</tr>
<tr>
<td>
<p>SpbAccelerometer.asl</p>
</td>
<td>
<p>Sample ASL file for a peripheral device node. It declares I<sup>2</sup>C and GPIO interrupt resources. Note that each macro specifies an ACPI path to describe direct dependencies.</p>
</td>
</tr>
<tr>
<td>
<p>SpbAccelerometer.ctl</p>
</td>
<td>
<p>Declaration of driver's tracing GUID</p>
</td>
</tr>
<tr>
<td>
<p>SpbAccelerometer.def</p>
</td>
<td>
<p>Declaration of exported functions for providing COM support</p>
</td>
</tr>
<tr>
<td>
<p>SpbAccelerometer.idl</p>
</td>
<td>
<p>Driver's library interface file</p>
</td>
</tr>
<tr>
<td>
<p>SpbAccelerometer.inx</p>
</td>
<td>
<p>Describes the installation of the driver. The build process converts this into a .INF.</p>
</td>
</tr>
<tr>
<td>
<p>SpbAccelerometer.rc</p>
</td>
<td>
<p>Resource file</p>
</td>
</tr>
<tr>
<td>
<p>SpbRequest.h, SpbRequest.cpp</p>
</td>
<td>
<p>Implementation of register-based device accesses via SPB.</p>
</td>
</tr>
<tr>
<td>
<p>Trace.h</p>
</td>
<td>
<p>Sets up WPP tracing.</p>
</td>
</tr>
</tbody>
</table>
</div>
