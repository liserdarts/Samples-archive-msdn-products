# USB Host-Based Print Driver Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* print
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:56
## Description

<div id="mainSection">
<p>This driver sample demonstrates how to support host-based devices that use the v4 print driver model, and are connected via USB.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample is for the v4 print driver model.</p>
<p>Windows enables manufacturers to support Bidirectional Communication (Bidi) for USB devices, by using a combination of both a Bidi XML file and a Javascript file known as a USB Bidi extender. The
<i>usb_host_based_sample.js</i> file that is included with the sample, plays the role of the USB Bidi extender.</p>
<p>The USB Bidi extender allows apps to use Bidi with USB as the transport mechanism. The Javascript implementation does not support any device flow control, or any multiplexing of control information with print jobs during printing.</p>
<p>By default, Bidi queries and status requests are routed over the USB device interface that is used for printing.</p>
<p>In addition to extending Bidi communication, this driver sample also specifies the schema elements that it supports. The
<i>usb_host_based_sample_extension.xml</i> file that is included with the sample, provides information about the supported schema elements.</p>
<p>The Bidi schema is a hierarchy of printer attributes, some of which are properties and others that are values (or value entries).</p>
<dl><dt><i>Property</i>
<dl><dd>A property is a node in the schema hierarchy. A property can have one or more children, and these children can be other properties or values.
</dd></dl>
</dt><dt><i>Value</i>
<dl><dd>A value is a leaf in the schema hierarchy that represents either a single data item or a list of related data items. A value has a name, a data type, and a data value. A value cannot have child elements.
</dd></dl>
</dt></dl>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/jj659903(v=vs.85).aspx">
USB Bidi Extender</a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545169(v=vs.85).aspx">
Bidi Communication Schema</a>.</p>
<p>Here are the core files that you will find in this sample:</p>
<table>
<tbody>
<tr>
<th>File name</th>
<th>Description</th>
</tr>
<tr>
<td>usb_host_based_sample.js</td>
<td>A USB Bidi Extension JavaScript file which includes support for controlling printing for host-based devices. This is the only code in the driver sample. It is invoked by USBMon and it communicates with the device to do the following:
<ul>
<li>Determine if the device is ready to receive data </li><li>Check to see if there is an error condition </li><li>Read the device status </li></ul>
</td>
</tr>
<tr>
<td>
<p>usb_host_based_sample_events.xml</p>
</td>
<td>A 'driver events' XML file that specifies an event which detects when the user needs to flip over the paper in the tray.</td>
</tr>
<tr>
<td>
<p>usb_host_based_sample_extension.xml</p>
</td>
<td>A USB Bidi Extension XML file that specifies the supported Bidi Schema elements for this driver.</td>
</tr>
</tbody>
</table>
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
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information and instructions about how to test and deploy drivers, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
<h2>Run the sample</h2>
<p>To understand how to run this sample as a Windows driver, see the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh706306(v=vs.85).aspx">
v4 Printer Driver</a> collection of topics.</p>
</div>
