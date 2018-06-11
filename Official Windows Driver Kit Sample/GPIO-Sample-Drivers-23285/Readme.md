# GPIO Sample Drivers
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* gpio
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:10:40
## Description

<div id="mainSection">
<p>The GPIO samples contain annotated code to illustrate how to write a <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh439509">
GPIO controller driver</a> that works in conjunction with the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh439512">
GPIO framework extension</a> (GpioClx) to handle GPIO I/O control requests, and a peripheral driver that runs in kernel mode and uses GPIO resources. For a sample that shows how to write a GPIO peripheral driver that runs in user mode, please refer to the SPB
 accelerometer sample driver (SPB\peripherals\accelerometer). </p>
<p>The GPIO sample set contains the following three samples.</p>
<table>
<tbody>
<tr>
<th>Minifilter Sample</th>
<th>Description</th>
</tr>
<tr>
<td>
<p><i>SimGpio</i> </p>
</td>
<td>
<p>The files in this sample contain the source code for a GPIO controller driver that communicates with GpioClx through the GpioClx device driver interface (DDI). The GPIO controller driver is written for a hypothetical memory-mapped GPIO controller (simgpio).
 The code is meant to be purely instructional. An ASL file illustrates how to specify a GPIO interrupt and I/O descriptor in the ACPI firmware.</p>
</td>
</tr>
<tr>
<td>
<p><i>SimGpio_I2C</i> </p>
</td>
<td>
<p>The files in this sample contain the source code for a GPIO controller driver that communicates with GpioClx through the GpioClx DDI. In contrast to the SimGpio sample, the GPIO controller in this sample is not memory-mapped. The GPIO controller driver is
 written for a hypothetical GPIO controller that resides on an I<sup>2</sup>C bus (simgpio_i2c). The code is meant to be purely instructional. An ASL file illustrates how to specify a GPIO interrupt and I/O descriptor in the ACPI firmware.</p>
</td>
</tr>
<tr>
<td>
<p><i>SimDevice</i> </p>
</td>
<td>
<p>The purpose of this sample is to show how a driver opens a device and performs I/O operations on a GPIO controller in kernel mode. Additionally, this sample demonstrates how the driver connects to a GPIO interrupt resource. The ASL file illustrates how to
 specify a GPIO interrupt and I/O descriptor in the ACPI firmware.</p>
</td>
</tr>
</tbody>
</table>
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
<p>An INF file for each sample driver is provided in the same directory as the sample source. The instructions for how to install these drivers is the same as those given in the readme file for the WDM Toaster sample. Please follow the instructions for that
 sample.</p>
</div>
