# Sample KMDF Function Driver for OSR USB-FX2
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* usb
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:17:47
## Description

<div id="mainSection">
<p>The kmdf_fx2 sample is a Kernel-Mode Driver Framework (KMDF) driver for the OSR USB-FX2 device. It includes a test app and sample device metadata.
</p>
<p>In the Windows Driver Kit (WDK) for Windows&nbsp;7 and earlier versions of Windows, the osrusbfx2 sample demonstrated how to perform bulk and interrupt data transfers to an USB device. The sample was written for the OSR USB-FX2 Learning Kit.</p>
<p>Starting in Windows&nbsp;8, the osrusbfx2 sample has been divided into two samples:</p>
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">wdf_osrfx2</a>: This sample is a series of iterative drivers that demonstrate how to write a &quot;Hello World&quot; driver and adds additional features in each step.</p>
</li><li>
<p>kmdf_fx2: This sample is the final version of <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">
wdf_osrfx2</a> and demonstrates additional features. For information about those features, see Sample Contents in this readme. They are marked as (<i>*kmdf_fx2 only</i>).</p>
</li></ul>
<p></p>
<p>The specification for the device is at <a href="http://www.osronline.com/hardware/OSRFX2_32.pdf">
http://www.osronline.com/hardware/OSRFX2_32.pdf</a>. The driver and sample device metadata also work with the
<a href="http://go.microsoft.com/fwlink/p/?LinkID=248288">Custom driver access</a> sample.</p>
<p>The <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">umdf_fx2</a> sample is a User-Mode Driver Framework (UMDF) driver for the OSR USB-FX2 device.</p>
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><b></b></dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">umdf_fx2</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">wdf_osrfx2</a>
</dt></dl>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt><dt>Windows&nbsp;7 </dt><dt>Windows&nbsp;Vista with SP1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt><dt>Windows Server&nbsp;2008&nbsp;R2 </dt><dt>Windows Server&nbsp;2008 with SP1 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32. You can change the default configuration to build for Windows&nbsp;7 or Windows&nbsp;Vista version of the operating system.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio Ultimate&nbsp;2012 (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Overview"></a><a id="overview"></a><a id="OVERVIEW"></a>Overview</h3>
<p>Here is the overview of the device: </p>
<ul>
<li>The device is based on the development board supplied with the Cypress EZ-USB FX2 Development Kit (CY3681).
</li><li>It contains 1 interface and 3 endpoints (Interrupt IN, Bulk Out, Bulk IN). </li><li>Firmware supports vendor commands to query or set LED Bar graph display and 7-segment LED display, and to query toggle switch states.
</li><li>Interrupt endpoint:
<ul>
<li>Sends an 8-bit value that represents the state of the switches. </li><li>Sent on startup, resume from suspend, and whenever the switch pack setting changes.
</li><li>Firmware does not de-bounce the switch pack. </li><li>One switch change can result in multiple bytes being sent. </li><li>Bits are in the reverse order of the labels on the pack (for example, bit 0x80 is labeled 1 on the pack).
</li></ul>
</li><li>Bulk endpoints are configured for loopback:
<ul>
<li>The device moves data from IN endpoint to OUT endpoint. </li><li>The device does not change the values of the data it receives nor does it internally create any data.
</li><li>Endpoints are always double buffered. </li><li>Maximum packet size depends on speed (64 full speed, 512 high speed). </li></ul>
</li><li>Event Tracing for Windows (ETW) events:
<ul>
<li>Included osrusbfx2.man, which describes events added. </li><li>Three events are targeted to the event log:
<ul>
<li>Failure during the add device routine. </li><li>Failure to start the OSR device on a USB 1.1 controller. </li><li>Invocation of the “re-enumerate device” IOCTL. </li></ul>
</li><li>Read/write start/stop events can be used to measure the time taken. </li><li>For more information, see Unified Tracing later in this document. </li></ul>
</li></ul>
<p></p>
<h3><a id="Sample_Contents"></a><a id="sample_contents"></a><a id="SAMPLE_CONTENTS"></a>Sample Contents</h3>
<table>
<tbody>
<tr>
<th>Folder</th>
<th>Description</th>
</tr>
<tr>
<td>usb\kmdf_fx2\driver</td>
<td>This directory contains driver code that demonstrates the following functionality:
<ul>
<li>Loads the driver and responds to PnP and Power events. You can install, uninstall, disable, enable, suspend, and resume the system.
</li><li>Creates a context with the WDFDEVICE object. </li><li>Initializes the USB device by registering a <i>EvtPrepareHardware</i> callback.
</li><li>Marks the interface restricted so that it can be accessed by a privileged Windows Store device app.
</li><li>Creates a default parallel queue to receive an IOCTL request to set bar graph display.
</li><li>Retrieves memory handle from the requests and uses it to send a vendor command to the USB device.
</li><li>Registers read and write events on the default queue. </li><li>Retrieves memory from read and write requests, formats the requests, and sends it to a USB target.
</li><li>Creates two separate sequential queues and configures them to dispatch read and write requests directly. (<i>*kmdf_fx2 only</i>)
</li><li>Enables wait-wake and selective suspend support. (<i>*kmdf_fx2 only</i>) </li><li>Configures a USB target continuous reader to read toggle switch states asynchronously from the interrupt endpoint. (<i>*kmdf_fx2 only</i>)
</li><li>Supports additional IOCTLs to get and set the 7-segment display and toggle switches, and to reset and re-enumerate the device. (<i>*kmdf_fx2 only</i>)
</li><li>Creates ETW provider to log two events to the event log, and read/write start stop events. (<i>*kmdf_fx2 only</i>)
</li><li>WPP tracing. </li></ul>
</td>
</tr>
<tr>
<td>usb\kmdf_fx2\exe</td>
<td>This directory contains a test application that can be used to drive the KMDF driver and FX2 device.
</td>
</tr>
<tr>
<td>usb\kmdf_fx2\deviceMetadata</td>
<td>This directory contains the device metadata package for the sample. You must copy the device metadata to the system before installing the device. For information on how to update and deploy device metadata, see the
<a href="http://go.microsoft.com/fwlink/p/?LinkID=248288">Custom driver access sample</a>.</td>
</tr>
</tbody>
</table>
<h3><a id="Testing_the_driver"></a><a id="testing_the_driver"></a><a id="TESTING_THE_DRIVER"></a>Testing the driver</h3>
<p>You can use the <a href="http://go.microsoft.com/fwlink/p/?LinkID=248288">Custom driver access</a> sample as a testing method.</p>
<p>The sample also includes a test application, osrusbfx2.exe, that you can use to test the device. This console application enumerates the interface registered by the driver and opens the device to send read, write, or IOCTL requests based on the command line
 options.</p>
<p>Usage for Read/Write test: </p>
<ul>
<li>-r [<i>n</i>], where <i>n</i> is number of bytes to read. </li><li>-w [<i>n</i>], where <i>n</i> is number of bytes to write. </li><li>-c [<i>n</i>], where <i>n</i> is number of iterations (default = 1). </li><li>-v, shows verbose read data. </li><li>-p, plays with Bar Display, Dip Switch, 7-Segment Display. </li><li>-a, performs asynchronous I/O operation. </li><li>-u, dumps USB configuration and pipe information. </li></ul>
<p><b>Playing with the 7 segment display, toggle switches, and bar graph display</b></p>
<p>Use the command, <b>osrusbfx2.exe -p</b> options 1-9, to set and clear bar graph display, set and get 7 segment state, and read the toggle switch states. The following shows the function options:</p>
<ol>
<li>Light bar </li><li>Clear bar </li><li>Light entire bar graph </li><li>Clear entire bar graph </li><li>Get bar graph state </li><li>Get switch state </li><li>Get switch interrupt message </li><li>Get 7 segment state </li><li>Set 7 segment state </li><li>Reset the device </li><li>Re-enumerate the device </li></ol>
<p></p>
<p>0. Exit</p>
<p>Selection: </p>
<p><b>Reset and re-enumerate the device</b></p>
<p>Use the command, osrusbfx2.exe -p with option 10 and 11, to either reset the device or re-enumerate the device.
</p>
<p><b>Read and write to bulk endpoints</b></p>
<p>The following commands send read and write requests to the device's bulk endpoint.</p>
<p></p>
<ul>
<li><code>osrusbfx2.exe -r 64</code>
<p>The preceding command reads 64 bytes to the bulk IN endpoint.</p>
</li><li><code>osrusbfx2.exe -w 64 </code>
<p>The preceding command writes 64 bytes to the bulk OUT endpoint.</p>
</li><li><code>osrusbfx2.exe -r 64 -w 64 -c 100 -v</code>
<p>The preceding command first writes 64 bytes of data to bulk OUT endpoint (Pipe 1), then reads 64 bytes from bulk IN endpoint (Pipe 2), and then compares the read buffer with write buffer to see if they match. If the buffer contents match, it repeats this
 operation 100 times.</p>
</li><li><code>osrusbfx2.exe -a</code>
<p>The preceding command reads and writes to the device asynchronously in an infinite loop.</p>
</li></ul>
<p></p>
<p>The bulk endpoints are double buffered. Depending on the operational speed (full or high), the buffer size is either 64 bytes or 512 bytes, respectively. A request to read data does not complete if the buffers are empty. If the buffers are full, a request
 to write data does not complete until the buffers are emptied. When you are doing a synchronous read, make sure the endpoint buffer has data (for example, when you send a 512 bytes write request to the device operating in full speed mode). Because the endpoints
 are double buffered, the total buffer capacity is 256 bytes. The first 256 bytes fills the buffer, and the write request waits in the USB stack until the buffers are emptied. If you run another instance of the application to read 512 bytes of data, both write
 and read requests complete successfully. </p>
<p><b>Displaying descriptors</b></p>
<p>The following command displays all the descriptors and endpoint information. </p>
<p><b>osrusbfx2.exe -u</b></p>
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
<p><code>MaxPower = 0x32, decimal 50</code></p>
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
<p><code>bInterface = 0x0</code></p>
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
<h3><a id="Unified_tracing"></a><a id="unified_tracing"></a><a id="UNIFIED_TRACING"></a>Unified tracing</h3>
<p>To view events the provider manifest must be installed. As part of the installation, from an elevated prompt run the following:
<code>wevtutil im osrusbfx2.man</code></p>
<p>Registering the manifest sets up the appropriate paths where the system can find information for decoding the events. The OSR event log can be found in the system event viewer under Event Viewer\Applications and Services Logs\OSRUSBFx2\Operational channel
 eventlog. Triggering a device re-enumeration through osrusbfx2.exe sends an event to this log.
</p>
<p>To trace, you can use the in-box tools, logman and tracerpt, or download XPerf (Windows Performance Toolkit) from Microsoft.</p>
<p><b>Using in-box tools</b></p>
<p class="proch"><b>To start/stop the trace by using logman: </b></p>
<ol>
<li>Start tracing by using the following command:
<p><code>logman start sample -o osrusbfx2.etl -ets -p OSRUSBFX2</code></p>
</li><li>Generate activity through the osrusbfx2 test application, such as <code>osrusbfx2.exe –a</code>.
</li><li>Stop tracing by using the following command:
<p><code>Logman stop sample</code></p>
</li><li>View the trace file using tracerpt:
<p><code>tracerpt -of csv OSRUSBFX2.etl </code></p>
</li></ol>
<p class="proch"><b>To start/stop the trace by using Xperf (Windows Performance Toolkit):
</b></p>
<ol>
<li>Start tracing by using the following command:
<p><code>xperf -start sample -f osrusbfx2.etl -on OSRUSBFX2</code></p>
</li><li>Generate activity through the osrusbfx2 test application, such as <code>osrusbfx2.exe –a</code>.
</li><li>Stop tracing by using the following command:
<p><code>xperf -stop sample</code></p>
</li><li>View the trace file using Xperf:
<p><code>xperfview OSRUSBFX2.etl</code></p>
</li></ol>
</div>
