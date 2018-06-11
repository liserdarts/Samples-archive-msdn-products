# AC97 Driver Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Audio
* WDK
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:24
## Description

<div id="mainSection">
<p>This directory contains a sample AC97 adapter driver and several related code samples. The AC97 driver sample shows how to develop a WDM audio adapter driver that incorporates a wavePci miniport driver for the AC97 controller's wave audio device.
</p>
<p>The samples are contained in the following subdirectories:</p>
<p>
<table>
<tbody>
<tr>
<th>Subdirectory</th>
<th>Description</th>
</tr>
<tr>
<td>cpl</td>
<td>This subdirectory contains the sample code for a control panel application. The application displays the property page for your AC97 device. This application requires that the property page sample and the AC97 WDM audio driver sample be installed.
</td>
</tr>
<tr>
<td>driver</td>
<td>This subdirectory contains the AC97 sample driver. This sample is a WDM audio adapter driver that runs on an Intel® motherboard with an integrated AC97 controller. The adapter driver incorporates a wavePci miniport driver for the AC97 controller's wave
 audio device. </td>
</tr>
<tr>
<td>INFViewer</td>
<td>This subdirectory contains an HTML version of the AC97 driver's INF file. The HTML file supports easy browsing of the INF file's contents by providing hot-linked references to INF sections and to keyword definitions.
</td>
</tr>
<tr>
<td>proppage</td>
<td>This sample shows how to write a property-page DLL that gets loaded by the device manager when a user elects to display the properties of your device. In addition to displaying the default property sheets, the device manager also displays the property sheet
 that is defined in the sample. This sample requires that the AC97 WDM audio driver sample be installed.
</td>
</tr>
</tbody>
</table>
</p>
<p><b>Implementation and Design</b> </p>
<p>The <i>Prvprop.h</i> header file in this directory defines the private property that the samples use. Currently, no AC'97 implementation is available for Itanium-based computers--therefore, the AC97 sample driver does not install on Itanium-based computers.</p>
<p>For more information about writing custom adapter drivers to work with the PortCls system driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536829">Introduction to Port Class</a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538398">Supporting a Device</a>.</p>
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
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p class="note"><b>Note</b>&nbsp;&nbsp;The header file, <i>prvprop.h</i> that ships with this group of samples, defines the private property used by each of the samples. The INF file in the AC97\driver directory installs all the samples in the subdirectories.</p>
<p>For more information about each sample contained in this group, see the <i>readme.htm</i> files in each subdirectory. And for more information about how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p>For information about how to deploy and test the driver, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
</div>
