# Sample UMDF Filter Driver above KMDF Function Driver for OSR USB-FX2
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* UMDF
* Windows Driver
## Topics
* usb
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:24
## Description

<div id="mainSection">
<p>The umdf_filter_kmdf sample demonstrates how to load a UMDF filter driver as an upper filter driver above the kmdf_fx2 sample driver.
</p>
<p>The sample includes Event Tracing for Windows (ETW) tracing support, and is written for the OSR USB-FX2 Learning Kit. The specification for the device is at
<a href="http://www.osronline.com/hardware/OSRFX2_32.pdf">http://www.osronline.com/hardware/OSRFX2_32.pdf</a>.</p>
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
</li><li>Interrupt Endpoint:
<ul>
<li>Sends an 8-bit value that represents the state of the switches. </li><li>Sent on startup, resume from suspend, and whenever the switch pack setting changes.
</li><li>Firmware does not de-bounce the switch pack. </li><li>One switch change can result in multiple bytes being sent. </li><li>Bits are in the reverse order of the labels on the pack (for example, bit 0x80 is labeled 1 on the pack).
</li></ul>
</li><li>Bulk Endpoints are configured for loopback:
<ul>
<li>The device moves data from IN endpoint to OUT endpoint. </li><li>The device does not change the values of the data it receives nor does it internally create any data.
</li><li>Endpoints are always double buffered. </li><li>Maximum packet size depends on speed (64 full speed, 512 high speed). </li></ul>
</li><li>ETW events:
<ul>
<li>Included osrusbfx2.man, which describes events added. </li><li>Three events are targeted to the event log:
<ul>
<li>Failure during the add device routine. </li><li>Failure to start the OSR device on a USB 1.1 controller. </li><li>Invocation of the “re-enumerate device” IOCTL. </li></ul>
</li><li>Read/write start/stop events can be used to measure the time taken. </li></ul>
</li></ul>
<p></p>
<h3><a id="Testing_the_driver"></a><a id="testing_the_driver"></a><a id="TESTING_THE_DRIVER"></a>Testing the driver</h3>
<p>You can test this sample either by using the <a href="http://go.microsoft.com/fwlink/p/?LinkID=248288">
Custom driver access</a> sample application, or by using the osrusbfx2.exe test application. For information on how to build and use the osrusbfx2.exe application, see the test instructions for the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">kmdf_fx2</a> sample.</p>
<h3><a id="Sample_Contents"></a><a id="sample_contents"></a><a id="SAMPLE_CONTENTS"></a>Sample Contents</h3>
<table>
<tbody>
<tr>
<th>Folder</th>
<th>Description</th>
</tr>
<tr>
<td>usb\umdf_filter_kmdf\kmdf_driver</td>
<td>This directory contains source code for the kmdf_fx2 sample driver.</td>
</tr>
<tr>
<td>usb\umdf_filter_kmdf\umdf_filter</td>
<td>This directory contains the UMDF filter driver.</td>
</tr>
</tbody>
</table>
</div>
