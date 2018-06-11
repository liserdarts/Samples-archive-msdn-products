# WSDMon Bidi Extension Sample
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
* 2014-04-02 12:52:01
## Description

<div id="mainSection">
<p>This sample demonstrates how to use an XML extension file to support bidirectional (Bidi) communication with a WSD connected printer.
</p>
<p>The v4 print driver model continues to employ the WSDMon Bidi Extension file format, as well as the SNMP Bidi Extension file format.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">Third-party port monitors and language monitors are not supported in the v4 driver model or with print class drivers.</p>
<p></p>
<p>The WSDMON port monitor is a printer port monitor that supports printing to network printers that comply with the Web Services for Devices (WSD) technology. The WSDMON port monitor listens for WSD events and updates the printer status accordingly.</p>
<p>A Bidi schema is a hierarchy of printer attributes, some of which are properties and others that are values (or value entries).</p>
<p>A <i>property</i> is a node in the schema hierarchy. A property can have one or more children, and these children can be other properties or values.</p>
<p>A <i>value</i> is a leaf in the schema hierarchy that represents either a single data item or a list of related data items. A value has a name, a data type, and a data value. A value cannot have child elements.</p>
<p>The WSDMON port monitor can:</p>
<ul>
<li>
<p>Discover network printers and install them.</p>
</li><li>
<p>Send jobs to WSD printers.</p>
</li><li>
<p>Monitor the status and configuration of the WSD printers and update the printer object status accordingly.</p>
</li><li>
<p>Respond to bidirectional (bidi) queries for supported bidi schemas.</p>
</li><li>
<p>Monitor bidi Schema value changes and send notifications.</p>
</li></ul>
<p>WSDMON supports the following Xcv commands:</p>
<ul>
<li>
<p>CleanupPort</p>
</li><li>
<p>DeviceID</p>
</li><li>
<p>PnPXID</p>
</li><li>
<p>ResetCommunication</p>
</li><li>
<p>ServiceID</p>
</li></ul>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample is for the v4 print driver model.</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">
V4 Driver Connectivity Architecture</a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545169(v=vs.85).aspx">
Bidirectional Communication Schema</a>.</p>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p>The files in the sample should be integrated into a complete v4 print driver project for them to run. For information about developing a v4 print driver, see
<a href="http://msdn.microsoft.com/en-US/library/windows/hardware/hh706306(v=vs.85).aspx">
V4 Print Driver</a>.</p>
<p>To run the sample load the files into Visual Studio&nbsp;2013, and then press <b>F5</b>, or select
<b>Debug</b> &gt; <b>Start Debugging</b>.</p>
</div>
