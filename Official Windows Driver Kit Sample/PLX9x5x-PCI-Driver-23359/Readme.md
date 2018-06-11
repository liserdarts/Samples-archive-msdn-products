# PLX9x5x PCI Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:18
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
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;Vista </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
</div>
