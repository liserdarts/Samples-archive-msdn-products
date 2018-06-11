# Print Driver USB Monitor and Bidi Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:49
## Description

<div id="mainSection">
<p>This sample demonstrate how to support bidirectional (Bidi) communication over the USB bus, using JavaScript and XML. This sample supports bidirectional status while not printing, and unsolicited status from the printer while printing.
</p>
<p>The following files are included in the sample:</p>
<ul>
<li>USBMON_Bidi_JavaScript_File.js. This JavaScript file demonstrates the implementation of a Bidi support for USBMon with a v4 print driver. The JavaScript file supports three functions: getSchemas() is used to make Bidi GET queries to a device, setSchema()
 is used to make a single Bidi SET query to the device, and getStatus() is called repeatedly during printing in order to retrieve unsolicited status from the printer using the data from the read channel of the device.
</li><li>USBMON_Bidi_XML_File.xml. This XML file demonstrates how to build a Bidi Schema extension for USB. It describes the supported schema elements that can be queried or set, along with their restrictions.
</li></ul>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample is for the v4 print driver model.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;When you make calls to printerStream.read() in the sample, the printer returns an array which includes an additional element that represents the array length. The following code snippet can be used to copy the returned array into
 a new array, and also to remove the additional element.</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>JavaScript</th>
</tr>
<tr>
<td>
<pre>var readBuffer = [];
var readBytes = 0;
var readSize = 4096;

readBuffer = printerStream.read( readSize );
readBytes = readBuffer.length;


var cleanArray = [];
           
for ( i = 0; i &lt; readBytes; i&#43;&#43; ) {
    cleanArray[i] = readBuffer.shift();
}
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
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
<h3>Build the sample</h3>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3>Run the sample</h3>
<p>To run the sample load the files into Visual Studio Professional&nbsp;2012 or Visual Studio Ultimate&nbsp;2012, and then press
<b>F5</b>, or select <b>Debug</b> &gt; <b>Start Debugging</b>.</p>
</div>
