# Sample KMDF Bus Driver for OSR USB-FX2
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
* 2013-06-25 10:17:44
## Description

<div id="mainSection">
<p>The kmdf_enumswitches sample demonstrates how to use Kernel-Mode Driver Framework (KMDF) as a bus driver using the OSR USB-FX2 device.
</p>
<p>This sample is written for the OSR USB-FX2 Learning Kit. The specification for the device is at
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
<p>To select a configuration and build a driver using Microsoft Visual Studio, follow these steps:</p>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>
<p>If you are going to install the driver on a computer running a 64-bit version of Windows, you must sign the driver package. For testing purposes, you can test sign the driver package.</p>
<p>To configure Visual Studio to test sign, navigate to the package Project Properties, then under
<b>Configuation Properties &gt; Driver Signing &gt; General</b>, in the <b>Sign Mode</b> drop-down list, select
<b>Test Sign</b>.</p>
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Testing_the_Device"></a><a id="testing_the_device"></a><a id="TESTING_THE_DEVICE"></a>Testing the Device</h3>
<p>To test the device, follow these steps:</p>
<ol>
<li>If you test signed your driver package, you must enable installation of test signed drivers on the target machine. To do so, either press F8 as the target machine comes up from a reboot, or specify
<b>Bcdedit.exe -set TESTSIGNING ON</b> and reboot. If you use F8, the change only applies until the next reboot.
</li><li>Plug in the OSR USB-FX-2 Learning Kit (must be version 2.00 or later). </li><li>In Device Manager, select <b>Update Driver Software</b>, <b>Browse my computer for driver software</b>,
<b>Let me pick from a list of device drivers on my computer</b>, <b>Have Disk</b>. Navigate to the directory that contains your driver package and select the INF file.
</li><li>After the driver installs, verify that the device appears under the <b>Sample Device</b> node in Device Manager.
</li><li>Flip the switches on the OSR USB-FX-2 hardware board and watch the raw PDO entries appear and disappear under
<b>Sample Device</b> in Device Manager. </li><li>
<p>Right-click a raw PDO entry, select <b>Properties</b>, and then click the <b>Events</b> tab. Under
<b>Information</b>, examine the hardware ID for the PDO. It should be something like this:
</p>
<pre class="syntax"><code>6FDE7521-1B65-48ae-B628-80BE62016026}\OsrUsbFxRawPdo\6&amp;227995e2&amp;0&amp;08</code></pre>
<p></p>
<p>The last digit matches the number of the switch that you toggled.</p>
</li></ol>
<h3><a id="Hardware_Overview"></a><a id="hardware_overview"></a><a id="HARDWARE_OVERVIEW"></a>Hardware Overview</h3>
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
<td>usb\kmdf_enumswitches\sys</td>
<td>This directory contains a sample driver that demonstrates the framework's child device enumeration support. The driver configures a continuous reader on the interrupt pipe to read toggle switch states and then enumerates each &quot;ON&quot; switch as a child device
 in the completion callback of the continuous reader. The child devices are enumerated as raw PDOs, so no INF file is required to install them.
</td>
</tr>
</tbody>
</table>
</div>
