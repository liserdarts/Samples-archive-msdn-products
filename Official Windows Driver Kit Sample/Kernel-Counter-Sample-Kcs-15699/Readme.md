# Kernel Counter Sample (Kcs)
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
* 2014-04-02 12:46:56
## Description

<div id="mainSection">
<p>The Kcs sample driver demonstrates the use of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff548159">
kernel-mode performance library</a>. The sample driver does not control any hardware; it simply provides example code that demonstrates how to provide counter data from a kernel-mode driver. The code contains comments to explain what each function does. The
 sample creates geometric wave and trigonometric wave counter sets. </p>
<p>This module contains sample code to demonstrate how to provide counter data from a kernel driver.
</p>
<p>This sample driver should not be used in a production environment.</p>
<p>Kcs is designed for Windows 7 and later versions of Windows.</p>
<p>The Microsoft Windows operating system allows system components and third parties to expose performance metrics in a standard way by using
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/aa373083">Performance Counters</a>. Kernel-mode PCW providers are installed in the system as Performance Counter Library (PERFLIB) (Version 2 providers), which allows their counters to be browsed,
 and allows for data collection and instance enumeration. Consumers can query KM PCW providers by using PDH and PERFLIB Version 1 without any modification to the consumer code.
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
