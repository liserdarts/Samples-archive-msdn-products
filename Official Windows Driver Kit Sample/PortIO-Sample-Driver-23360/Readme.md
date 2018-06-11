# PortIO Sample Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:21
## Description

<div id="mainSection">
<p>This sample driver for a generic port I/O device shows how to use an INF file to reserve I/O ports for a non-Plug and Play device, and how to read and write to this device from a user-mode application.
</p>
<p>The instructions given here for building and installing this driver apply to Windows 2000 and later versions of Windows.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample provides an example of a minimal driver. Neither this driver nor its sample programs are intended for use in a production environment. Instead, they are intended for educational purposes and as a skeleton driver.</p>
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
<p>If the build succeeds, a driver, Genport.sys, and two applications, GpdRead.exe and GpdWrite.exe, are created. To test your Genport driver, enter the following commands in the command window:</p>
<p><code>GpdRead -b 1</code> {This command reads a byte from the <i>assigned base port address</i> &#43; 1.}</p>
<p><code>GpdWrite -b 2 1</code> {This command writes 1 to the <i>assigned base port address</i> &#43; 2.}</p>
</div>
