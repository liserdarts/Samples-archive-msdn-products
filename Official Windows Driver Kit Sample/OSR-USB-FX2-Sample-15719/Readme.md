# OSR USB-FX2 Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* KMDF
* UMDF
* Windows Driver
## Topics
* WDK
* usb
* KMDF
* UMDF
## IsPublished
* False
## ModifiedDate
* 2012-02-29 04:30:03
## Description

<h3>OSR USB-FX2 Sample </h3>
<p>The OSRUSBFX2 sample demonstrates how to perform bulk and interrupt data transfers to an USB device using Windows Driver Framework (WDF). This sample is written for OSR USB-FX2 Learning Kit. The specification for the device is at
<a href="http://www.osronline.com/hardware/OSRFX2_32.pdf">http://www.osronline.com/hardware/OSRFX2_32.pdf</a>.
</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;Vista </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>You can build the sample in two ways: using Visual Studio&nbsp;11 Ultimate Beta or the command line (<i>MSBuild</i>).</p>
<h3><a name="building_a_driver_using_visual_studio"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Consumer Preview Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver</b> </p>
<ol>
<li>Open the driver project or solution in Visual Studio&nbsp;11 Ultimate Beta (find <i>
filtername</i>.sln or <i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a name="building_a_driver_using_the_command_line__msbuild_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver</b> </p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b> <i>filtername</i> <b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<h3><a name="overview"></a>Overview</h3>
<p>Here is the overview of the device: </p>
<ul>
<li>Device is based on the development board supplied with the Cypress EZ-USB FX2 Development Kit (CY3681).
</li><li>Contains 1 interface and 3 endpoints (Interrupt IN, Bulk Out, Bulk IN). </li><li>Firmware supports vendor commands to query or set LED Bar graph display, 7-segment LED display and query toggle switch states.
</li><li>Interrupt Endpoint:
<ul>
<li>Sends an 8-bit value that represents the state of the switches. </li><li>Sent on startup, resume from suspend, and whenever the switch pack setting changes.
</li><li>Firmware does not de-bounce the switch pack. </li><li>One switch change can result in multiple bytes being sent. </li><li>Bits are in the reverse order of the labels on the pack
<p>E.g. bit 0x80 is labeled 1 on the pack</p>
</li></ul>
</li><li>Bulk Endpoints are configured for loopback:
<ul>
<li>Device moves data from IN endpoint to OUT endpoint. </li><li>Device does not change the values of the data it receives nor does it internally create any data.
</li><li>Endpoints are always double buffered. </li><li>Maximum packet size depends on speed (64 Full speed, 512 High speed). </li></ul>
</li><li>ETW events:
<ul>
<li>Included osrusbfx2.man which describes events added. </li><li>Three events are targeted to the event log:
<ul>
<li>Failure during the add device routine. </li><li>Failure to start the OSR device on a USB 1.1 controller. </li><li>Invocation of the “re-enumerate device” IOCTL. </li></ul>
</li><li>There are read/write start/stop events that can be used to measure the time taken.
</li><li>See Unified Tracing later in this document for more information. </li></ul>
</li></ul>
<p></p>
<h3><a name="osrusbfx2__kmdf_"></a>OSRUSBFX2 (KMDF)</h3>
<p>This sample contains a console test application and a series of drivers. The driver is iterative as a series of steps - starting with a most basic &quot;Hello World&quot; driver to fully functional driver in the 'Final' step. The description of what is demonstrated
 in each step is given in the table below. </p>
<table>
<tbody>
<tr>
<th>Folder</th>
<th>Description</th>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\step1</td>
<td>The is the most basic step. The source file contains absolutely minimal amount of code to get the driver loaded in memory and respond to PNP and Power events. You can install, uninstall, disable, enable, suspend and resume the system and everything will
 work fine. </td>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\step2</td>
<td>
<ol>
<li>Creates a context with the WDFDEVICE object. </li><li>Initializes the USB device by registering a <i>EvtPrepareHardware</i> callback.
</li><li>Registers an interface so that application can open an handle to the device. </li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\step3</td>
<td>
<ol>
<li>Creates a default parallel queue to receive an IOCTL requests to set bar graph display.
</li><li>Retrieves memory handle from the requests and use that to send a vendor command to the USB device.
</li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\step4</td>
<td>
<ol>
<li>Registers read and write events on the default queue. </li><li>Retrieves memory from read and write request, format the requests and send it to USB target.
</li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\step5</td>
<td>WPP tracing.</td>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\final</td>
<td>This steps includes all the features list in the above steps and the following:
<ol>
<li>Creates two separate sequential queues and configures them to dispatch read and write requests directly.
</li><li>Enables wait-wake and selective suspend support. </li><li>Configures a USB target continuous reader to read toggle switch states asynchronously from the interrupt endpoint.
</li><li>Supports additional IOCTLs to get &amp; set the 7-segment display, toggle switches, reset and re-enumerate device.
</li><li>Added ETW provider to log two events to the event log, and read/write start stop events.
</li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\kmdf\sys\enumswitches</td>
<td>This is a basic version of the final version. It's meant to demonstrate child device enumeration support of WDF. This sample configures a continuous reader on the interrupt pipe to read toggle switch states and then enumerates each &quot;ON&quot; switch as a child
 device in the completion callback of the continuous reader. The child devices are enumerated as raw PDOs, so no INF file is required to install them.
</td>
</tr>
</tbody>
</table>
<h3><a name="testing_the_driver"></a>Testing the driver</h3>
<p>The sample includes a test application, osrusbfx2.exe, that you can use to test the device. This console application enumerates the interface registered by the driver and opens the device to send read, write or IOCTL requests based on the command line options.
</p>
<p>Usage for Read/Write test: </p>
<ul>
<li>-r [<i>n</i>], where <i>n</i> is number of bytes to read. </li><li>-w [<i>n</i>], where <i>n</i> is number of bytes to write. </li><li>-c [<i>n</i>], where <i>n</i> is number of iterations (default = 1). </li><li>-v, shows verbose read data. </li><li>-p, plays with Bar Display, Dip Switch, 7-Segment Display. </li><li>-a, performs asynchronous I/O operation. </li><li>-u, dumps USB configuration and pipe information. </li></ul>
<p><b>Playing with the 7 segment display, toggle switches and bar graph display</b>
</p>
<p>The command, <b>osrusbfx2.exe -p</b> options 1-9 allows you to set and clear bar graph display, set and get 7 segment state, and read the toggle switch states. The following shows the function options:</p>
<ol>
<li>Light Bar </li><li>Clear Bar </li><li>Light entire Bar graph </li><li>Clear entire Bar graph </li><li>Get bar graph state </li><li>Get Switch state </li><li>Get Switch Interrupt Message </li><li>Get 7 segment state </li><li>Set 7 segment state </li><li>Reset the device </li><li>Re-enumerate the device </li></ol>
<p></p>
<p>0. Exit</p>
<p>Selection: </p>
<p><b>Reset and re-enumerate the device</b> </p>
<p>Use the command, osrusbfx2.exe -p with option 10 and 11, to either reset the device or re-enumerate the device.
</p>
<p><b>Read and write to bulk endpoints</b> </p>
<p>The following commands send read and write requests to the device's bulk endpoint.</p>
<p></p>
<ul>
<li><code>osrusbfx2.exe -r 64</code>
<p>The preceding command reads 64 bytes to the bulk IN endpoint.</p>
</li><li><code>osrusbfx2.exe -w 64 </code>
<p>The preceding command writes 64 bytes to the bulk OUT endpoint.</p>
</li><li><code>osrusbfx2.exe -r 64 -w 64 -c 100 -v</code>
<p>The preceding command first writes 64 bytes of data to bulk OUT endpoint (Pipe 1), and then reads 64 bytes from bulk IN endpoint (Pipe 2), and compares the read buffer with write buffer to see if they match. If the buffer contents match, it repeats this
 operation 100 times.</p>
</li><li><code>osrusbfx2.exe -a</code>
<p>The preceding command reads and writes to the device asynchronously in an infinite loop.</p>
</li></ul>
<p></p>
<p>The bulk endpoints are double buffered. Depending on the operational speed, full or high, the buffer size is either 64 bytes or 512 bytes respectively. A request to read data wouldn't complete if the buffers are empty. If the buffers are full, a request
 to write data does not complete until the buffers are emptied. So when you are doing a synchronous read, you should make sure the endpoint buffer has data. For example, when you send 512 bytes write request to the device operating in full speed mode. Because
 the endpoints are double buffered, the total buffer capacity is 256 bytes. So the first 256 bytes fills the buffer and the write request waits in the USB stack until the buffers are emptied. So if you run another instance of the application to read 512 bytes
 of data, both write and read requests complete successfully. </p>
<p><b>Displaying descriptors</b> </p>
<p>The following command displays all the descriptors and endpoint information. </p>
<p><b>osrusbfx2.exe -u</b> </p>
<p>If the device is operating in high speed mode, you will get the following information:</p>
<p><code>=================== </code></p>
<p><code>USB_CONFIGURATION_DESCRIPTOR </code></p>
<p><code>bLength = 0x9, decimal 9 </code></p>
<p><code>bDescriptorType = 0x2 ( USB_CONFIGURATION_DESCRIPTOR_TYPE ) </code></p>
<p><code>wTotalLength = 0x27, decimal 39 </code></p>
<p><code>bNumInterfaces = 0x1, decimal 1 </code></p>
<p><code>bConfigurationValue = 0x1, decimal 1 </code></p>
<p><code>iConfiguration = 0x4, decimal 4 </code></p>
<p><code>bmAttributes = 0xa0 ( USB_CONFIG_BUS_POWERED ) </code></p>
<p><code>MaxPower = 0x32, decimal 50</code> </p>
<p><code>----------------------------- </code></p>
<p><code>USB_INTERFACE_DESCRIPTOR #0 </code></p>
<p><code>bLength = 0x9 </code></p>
<p><code>bDescriptorType = 0x4 ( USB_INTERFACE_DESCRIPTOR_TYPE ) </code></p>
<p><code>bInterfaceNumber = 0x0 </code></p>
<p><code>bAlternateSetting = 0x0 </code></p>
<p><code>bNumEndpoints = 0x3 </code></p>
<p><code>bInterfaceClass = 0xff </code></p>
<p><code>bInterfaceSubClass = 0x0 </code></p>
<p><code>bInterfaceProtocol = 0x0 </code></p>
<p><code>bInterface = 0x0</code> </p>
<p><code>------------------------------ </code></p>
<p><code>USB_ENDPOINT_DESCRIPTOR for Pipe00 </code></p>
<p><code>bLength = 0x7 </code></p>
<p><code>bDescriptorType = 0x5 ( USB_ENDPOINT_DESCRIPTOR_TYPE ) </code></p>
<p><code>bEndpointAddress= 0x81 ( INPUT ) </code></p>
<p><code>bmAttributes= 0x3 ( USB_ENDPOINT_TYPE_INTERRUPT ) </code></p>
<p><code>wMaxPacketSize= 0x49, decimal 73 </code></p>
<p><code>bInterval = 0x1, decimal 1 </code></p>
<p><code>------------------------------ </code></p>
<p><code>USB_ENDPOINT_DESCRIPTOR for Pipe01 </code></p>
<p><code>bLength = 0x7 </code></p>
<p><code>bDescriptorType = 0x5 ( USB_ENDPOINT_DESCRIPTOR_TYPE ) </code></p>
<p><code>bEndpointAddress= 0x6 ( OUTPUT ) </code></p>
<p><code>bmAttributes= 0x2 ( USB_ENDPOINT_TYPE_BULK ) </code></p>
<p><code>wMaxPacketSize= 0x200, </code></p>
<p><code>decimal 512 bInterval = 0x0, </code></p>
<p><code>decimal 0 </code></p>
<p><code>------------------------------ </code></p>
<p><code>USB_ENDPOINT_DESCRIPTOR for Pipe02 </code></p>
<p><code>bLength = 0x7 </code></p>
<p><code>bDescriptorType = 0x5 ( USB_ENDPOINT_DESCRIPTOR_TYPE ) </code></p>
<p><code>bEndpointAddress= 0x88 ( INPUT ) </code></p>
<p><code>bmAttributes= 0x2 ( USB_ENDPOINT_TYPE_BULK ) </code></p>
<p><code>wMaxPacketSize= 0x200, decimal 512 </code></p>
<p><code>bInterval = 0x0, decimal 0 </code></p>
<p>If the device is operating in low speed mode, you will get the following information:</p>
<p><code>=================== </code></p>
<p><code>USB_CONFIGURATION_DESCRIPTOR </code></p>
<p><code>bLength = 0x9, decimal 9 </code></p>
<p><code>bDescriptorType = 0x2 ( USB_CONFIGURATION_DESCRIPTOR_TYPE ) </code></p>
<p><code>wTotalLength = 0x27, decimal 39 </code></p>
<p><code>bNumInterfaces = 0x1, decimal 1 </code></p>
<p><code>bConfigurationValue = 0x1, decimal 1 </code></p>
<p><code>iConfiguration = 0x3, decimal 3 </code></p>
<p><code>bmAttributes = 0xa0 ( USB_CONFIG_BUS_POWERED ) </code></p>
<p><code>MaxPower = 0x32, decimal 50 </code></p>
<p><code>----------------------------- </code></p>
<p><code>USB_INTERFACE_DESCRIPTOR #0 </code></p>
<p><code>bLength = 0x9 </code></p>
<p><code>bDescriptorType = 0x4 ( USB_INTERFACE_DESCRIPTOR_TYPE ) </code></p>
<p><code>bInterfaceNumber = 0x0 bAlternateSetting = 0x0 </code></p>
<p><code>bNumEndpoints = 0x3 </code></p>
<p><code>bInterfaceClass = 0xff </code></p>
<p><code>bInterfaceSubClass = 0x0 </code></p>
<p><code>bInterfaceProtocol = 0x0 </code></p>
<p><code>bInterface = 0x0 </code></p>
<p><code>------------------------------ </code></p>
<p><code>USB_ENDPOINT_DESCRIPTOR for Pipe00 </code></p>
<p><code>bLength = 0x7 </code></p>
<p><code>bDescriptorType = 0x5 ( USB_ENDPOINT_DESCRIPTOR_TYPE ) </code></p>
<p><code>bEndpointAddress= 0x81 ( INPUT ) </code></p>
<p><code>bmAttributes= 0x3 ( USB_ENDPOINT_TYPE_INTERRUPT ) </code></p>
<p><code>wMaxPacketSize= 0x49, decimal 73 </code></p>
<p><code>bInterval = 0x1, decimal 1 </code></p>
<p><code>------- ----------------------- </code></p>
<p><code>USB_ENDPOINT_DESCRIPTOR for Pipe01 </code></p>
<p><code>bLength = 0x7 </code></p>
<p><code>bDescriptorType = 0x5 ( USB_ENDPOINT_DESCRIPTOR_TYPE ) </code></p>
<p><code>bEndpointAddress= 0x6 ( OUTPUT ) </code></p>
<p><code>bmAttributes= 0x2 ( USB_ENDPOINT_TYPE_BULK ) </code></p>
<p><code>wMaxPacketSize= 0x40, decimal 64 </code></p>
<p><code>bInterval = 0x0, decimal 0 </code></p>
<p><code>------------------------------ </code></p>
<p><code>USB_ENDPOINT_DESCRIPTOR for Pipe02 </code></p>
<p><code>bLength = 0x7 </code></p>
<p><code>bDescriptorType = 0x5 ( USB_ENDPOINT_DESCRIPTOR_TYPE ) </code></p>
<p><code>bEndpointAddress= 0x88 ( INPUT ) </code></p>
<p><code>bmAttributes= 0x2 ( USB_ENDPOINT_TYPE_BULK ) </code></p>
<p><code>wMaxPacketSize= 0x40, decimal 64 </code></p>
<p><code>bInterval = 0x0, decimal 0 </code></p>
<h3><a name="unified_tracing"></a>Unified tracing</h3>
<p>To view events the provider manifest will need to be installed. As part of the installation do the following; from an elevated prompt run:
<code>wevtutil im osrusbfx2.man</code> </p>
<p>Registering the manifest will set up the appropriate paths where the system can find information for decoding the events. The OSR event log can be found in the system event viewer under Event Viewer\Applications and Services Logs\OSRUSBFx2\Operational channel
 eventlog. Triggering a device re-enumeration through osrusbfx2.exe will send an event to this log.
</p>
<p>To trace you can use the inbox tools, logman and tracerpt or download XPerf (Windows Performance Toolkit) from Microsoft.</p>
<p><b>Using inbox tools</b> </p>
<p class="proch"><b>To start/stop the trace by using logman: </b></p>
<ol>
<li>Start tracing by using the following command:
<p><code>logman start sample -o osrusbfx2.etl -ets -p OSRUSBFX2</code> </p>
</li><li>Generate some activity through the osrusbfx2 test application, such as <code>
osrusbfx2.exe –a</code>. </li><li>Stop tracing by using the following command:
<p><code>Logman stop sample</code> </p>
</li><li>View the trace file using tracerpt:
<p><code>tracerpt -of csv OSRUSBFX2.etl </code></p>
</li></ol>
<p class="proch"><b>To start/stop the trace by using Xperf (Windows Performance Toolkit) :
</b></p>
<ol>
<li>Start tracing by using the following command:
<p><code>xperf -start sample -f osrusbfx2.etl -on OSRUSBFX2</code> </p>
</li><li>Generate some activity through the osrusbfx2 test application, such as <code>
osrusbfx2.exe –a</code>. </li><li>Stop tracing by using the following command:
<p><code>xperf -stop sample</code> </p>
</li><li>View the trace file using Xperf:
<p><code>xperfview OSRUSBFX2.etl</code> </p>
</li></ol>
<h3><a name="osrusbfx2__umdf_"></a>OSRUSBFX2 (UMDF)</h3>
<p>This sample driver is developed iteratively as a series of steps - starting with a most basic &quot;Hello World&quot; driver to fully functional driver in the 'Final' step. Each step progressively adds functionality to the previous step. The description of what is
 demonstrated in each step is given in the table below. </p>
<table>
<tbody>
<tr>
<th>Folder</th>
<th>Description</th>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\step1</td>
<td>The is the most basic step. The source file contains absolutely minimal amount of code to get the driver loaded in memory and respond to PNP and Power events. You can install, uninstall, disable, enable, suspend and resume the system and everything will
 work fine. </td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\step2</td>
<td>
<ol>
<li>Device registers a Pnp device interface so that application can open an handle to the device.
</li><li>Device object implements <b>IPnpCallbackHardware</b> interface and initializes USB I/O targets in
<b>IPnpCallbackHardware::OnPrepareHardware</b> method. </li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\step3</td>
<td>
<ol>
<li>Creates a sequential queue for handling IOCTL requests. </li><li>Adds code to handle the IOCTL to set bar graph display. </li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\step4</td>
<td>
<ol>
<li>Creates a parallel queue for handling read and write requests. </li><li>Retrieves memory from read and write request, format the requests and send it to USB target.
</li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\final</td>
<td>
<p>This steps adds the following functionality to complete the driver: </p>
<ol>
<li>Supports additional IOCTLs to get &amp; set the 7-segment display, get bar graph display and to get config descriptor.
</li><li>Sets power policy for the device. </li><li>Adds code to indicate that the device is ready by lighting up the period on seven- segment display.
</li></ol>
</td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\impersonation</td>
<td>This adds to Final &amp; demonstrates some additional UMDF features. In particular this sample shows how to escape to
<b>SetupDi</b> functions to determine the &quot;BusTypeGUID&quot; of the device, and how to use impersonation to access resources that only the caller has access to.
</td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\IdleWake\IdleWake_PPO</td>
<td>This adds idle and wake functionality to ‘Final’ using the framework(UMDF) as the power policy owner. It uses power managed queues and UMDF DDIs, AssignS0IdleSettings and AssignSxWakeSettings to achieve this. This driver code is shared with IdleWake_Non-PPO
 and parts of it are conditionally compiled. </td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\IdleWake\IdleWake_Non-PPO</td>
<td>This sample shows how to leverage WinUsb.sys for idle and wake functionality. It shows the corresponding changes needed in the UMDF driver to support this. UMDF is not the power policy owner in this device stack and the driver should not use power managed
 queues. This driver code is shared with IdleWake_PPO and parts of it are conditionally compiled. Both Final and IdleWake samples demonstrate implementation of a continuous reader.
</td>
</tr>
<tr>
<td>osrusbfx2\umdf\fx2_driver\exe</td>
<td>This directory contains a test application which can be used to drive the UMDF driver and Fx2 device. This is a modified version of the test application for the KMDF Fx2 driver.
</td>
</tr>
</tbody>
</table>
<p>This sample driver makes use of WinUSB, which is a KMDF based driver and so requires the KMDF co-installer for proper installation. Driver binary name and the inf name depend upon the step, as described in the following table:
<table>
<tbody>
<tr>
<th>Step</th>
<th>Driver binary</th>
<th>INF</th>
</tr>
<tr>
<td>Step 1-4 </td>
<td>WUDFOsrUsbFx2_N.dll, where N is the step number </td>
<td>WUDFOsrUsbFx2_N.inf, where N is the step number </td>
</tr>
<tr>
<td>Final </td>
<td>WUDFOsrUsbFx2.dll </td>
<td>WUDFOsrUsbFx2.inf</td>
</tr>
<tr>
<td>Impersonation </td>
<td>WUDFOsrUsbFx2_Imp.dll </td>
<td>WUDFOsrUsbFx2_Imp.inf </td>
</tr>
<tr>
<td>IdleWake_PPO</td>
<td>WUDFOsrUsbFx2_IdleWake_PPO.dll </td>
<td>WUDFOsrUsbFx2_IdleWake_PPO.inf </td>
</tr>
<tr>
<td>IdleWake_Non-PPO</td>
<td>WUDFOsrUsbFx2_IdleWake_Non-PPO.dll </td>
<td>WUDFOsrUsbFx2_IdleWakeNon-PPO.inf </td>
</tr>
</tbody>
</table>
</p>
<p><b>Code tour</b> </p>
<p>
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
<td>Definition and implementation of the driver callback class (CMyDriver) for the sample.</td>
</tr>
<tr>
<td>device.cpp &amp; driver.h </td>
<td>Definition and implementation of the device callback class (CMyDriver) for the sample.
</td>
</tr>
<tr>
<td>queue.cpp &amp; queue.h</td>
<td>Definition and implementation of the base queue callback class (CMyQueue). CMyControlQueue and CMyReadWriteQueue derive from CMyQueue. CMyQueue contains the common code.</td>
</tr>
<tr>
<td>ControlQueue.cpp &amp; ControlQueue.h </td>
<td>Definition and implementation of the queue callback class (CMyControlQueue) for IOCTL requests.
</td>
</tr>
<tr>
<td>osrusbdriver.rc </td>
<td>This file defines resource information for the sample driver. </td>
</tr>
<tr>
<td>WUDFOsrUsbDriver.inf </td>
<td>Sample INF for installing the sample driver to control the OSR USB-FX2 Learning Kit device.</td>
</tr>
<tr>
<td>osrusbdriver.ctl </td>
<td>This file lists the WPP trace control GUID(s) for the sample driver. This file can be used with the tracelog command's -guid flag to enable the collection of these trace events within an established trace session.
<p>These GUIDs must remain in sync with the trace control guids defined in internal.h.</p>
</td>
</tr>
</tbody>
</table>
</p>
