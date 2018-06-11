# PortIO Sample Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:48:25
## Description

<div id="mainSection">
<p>This sample driver for a generic port I/O device shows how to use an INF file to reserve I/O ports for a non-Plug and Play device, and how to read and write to this device from a user-mode application.
</p>
<p>The instructions given here for building and installing this driver apply to Windows 2000 and later versions of Windows.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample provides an example of a minimal driver. Neither this driver nor its sample programs are intended for use in a production environment. Instead, they are intended for educational purposes and as a skeleton driver.</p>
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
<p>If the build succeeds, a driver, Genport.sys, and two applications, GpdRead.exe and GpdWrite.exe, are created. To test your Genport driver, enter the following commands in the command window:</p>
<p><code>GpdRead -b 1</code> {This command reads a byte from the <i>assigned base port address</i> &#43; 1.}</p>
<p><code>GpdWrite -b 2 1</code> {This command writes 1 to the <i>assigned base port address</i> &#43; 2.}</p>
</div>
