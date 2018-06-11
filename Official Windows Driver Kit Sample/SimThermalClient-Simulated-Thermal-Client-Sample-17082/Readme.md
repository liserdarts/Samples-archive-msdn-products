# SimThermalClient: Simulated Thermal Client Sample Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* Thermal Sensor
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:03
## Description

<div id="mainSection">
<p>This sample is a driver for a simulated device that is a client of Windows thermal management. This driver publishes a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698265">GUID_THERMAL_COOLING_INTERFACE</a> driver interface. Support for this interface is available starting with Windows&nbsp;8.1. Drivers publish this interface so that they can participate in
 global thermal management under the coordination of the Windows operating system.
</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh698236">
Device-Level Thermal Management</a>.</p>
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
</div>
