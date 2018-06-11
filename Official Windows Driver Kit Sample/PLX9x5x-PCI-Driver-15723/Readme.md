# PLX9x5x PCI Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:48:20
## Description

<div id="mainSection">
<p>This sample demonstrates how to write driver for a generic PCI device using Windows Driver Framework. The target hardware for this driver is PLX9656/9653RDK-LITE board. The product kit and the hardware specification are available at
<a href="http://www.plxtech.com">http://www.plxtech.com</a>. </p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537451">
Peripheral Component Interconnect (PCI) Bus Drivers</a>.</p>
<p>The device is a PCI device with port, memory, interrupt and DMA resources. Device can be stopped and started at run-time and also supports low power states. The driver is capable of doing concurrent read and write operations to the device but it can handle
 only one read or write request at any time. The following lists the driver framework interfaces demonstrated in this sample:
</p>
<ul>
<li>Handling PnP &amp; Power Events </li><li>Registering a Device Interface </li><li>Hardware resource mapping: Port, Memory &amp; Interrupt </li><li>DMA Interfaces </li><li>Serialized Default Queue for Write requests </li><li>Serialized custom Queue for Read requests </li><li>Handling Interrupt &amp; DPC </li></ul>
To test the driver, run the PLX.EXE test application.
<p></p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the Windows Driver Framework. It is not intended for use in a production environment.
</p>
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
</div>
