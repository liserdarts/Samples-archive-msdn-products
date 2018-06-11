# Generic Text-Only Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:21
## Description

<div id="mainSection">
<p>The Generic Text-Only Driver (TTY) sample demonstrates how to implement a generic, text-only print driver that is based on the Microsoft Universal Printer Driver (Unidrv).
</p>
<p>The TTY driver is a generic, text-only driver. It prints only text, and it prints the text in the native font of the print device, regardless of any formatting in the original document. The TTY driver sample uses the same source files as in the in-box driver,
 which means you can expect the same output as the in-box generic text-only driver when you build and install this sample.</p>
<p>For more information Unidrv, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff551786(v=vs.85).aspx">
Introduction to the Universal Printer Driver</a>.</p>
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
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;To create a version with verbose debug output, add _DEBUG to the compile defines in the project configuration.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>After building the samples, you can install the samples by using the <b>Add Printer</b> wizard. Select the local printer, click
<b>Have Disk</b>, and point to the directory that contains the tty.inf file. Windows&nbsp;XP and later drivers do not need to be copied to the local directory that contains
<i>tty.inf</i>. </p>
</div>
