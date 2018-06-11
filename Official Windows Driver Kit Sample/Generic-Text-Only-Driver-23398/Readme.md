# Generic Text-Only Driver
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
* 2013-06-25 10:22:14
## Description

<div id="mainSection">
<p>The Generic Text-Only Driver (TTY) sample demonstrates how to implement a print driver that is Unidrv-based, generic, and text-only.
</p>
<p>The TTY driver is a generic, text-only driver. It prints only text, and it prints the text in the native font of the print device, regardless of any formatting in the original document. The TTY driver sample uses the same source files as in the in-box driver,
 which means you can expect the same output as the in-box generic text-only driver when you build and install this sample.</p>
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
<p class="note"><b>Note</b>&nbsp;&nbsp;To create a version with verbose debug output, add _DEBUG to the compile defines in the project configuration.</p>
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>After building the samples, you can install the samples by using the <b>Add Printer</b> wizard. Select the local printer, click
<b>Have Disk</b>, and point to the directory that contains the tty.inf file. Windows&nbsp;XP and later drivers do not need to be copied to the local directory that contains
<i>tty.inf</i>. </p>
</div>
