# WPD Basic Hardware Sample Driver (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* wpd
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:52:42
## Description

<div id="mainSection">
<p>The WpdBasicHardwareDriver is a WPD driver that supports nine devices. These devices were selected because of their simplicity. This simplicity allowed the sample to focus on the tasks that are common to portable devices without getting bogged down in hardware
 complexities. </p>
<p>This sample driver is based on the WpdHelloWorldDriver that is also included in the Windows Driver Kit (WDK). The &quot;Supporting the WPD Infrastructure” sections for this driver show the changes that were made to the WpdHelloWorldDriver source so that it can
 communicate with basic hardware devices. Before you work through the topics in this section of the documentation, be familiar with the WpdHelloWorldDriver.</p>
<p>The sensor devices that are supported by the WpdBasicHardwareDriver are described in the following table.</p>
<table>
<tbody>
<tr>
<td>Device</td>
<td>Description</td>
</tr>
<tr>
<td>Memsic 2125 Accelerometer</td>
<td>Senses &#43;/- 2g along the X-axis and Y-axis.</td>
</tr>
<tr>
<td>Sensiron Temperature and Humidity Sensor</td>
<td>Senses temperature and relative humidity.</td>
</tr>
<tr>
<td>Flexiforce sensor</td>
<td>Senses pressure from 0-25 lbs.</td>
</tr>
<tr>
<td>PING Ultrasonic Sensor</td>
<td>Senses distances from 2-300 cm.</td>
</tr>
<tr>
<td>Passive Infrared (PIR) Sensor</td>
<td>Senses motion.</td>
</tr>
<tr>
<td>Hitachi HM55B Compass </td>
<td>Senses magnetic bearing (0-360 degrees).</td>
</tr>
<tr>
<td>Hitachi H48C Tri-Axis Accelerometer </td>
<td>Senses &#43;/- 3g along the X-axis, Y-axis, and Z-axis.</td>
</tr>
<tr>
<td>Piezo Film Vibration Sensor QTI (light) Sensor </td>
<td>Senses vibration. </td>
</tr>
<tr>
<td>QTI (light) Sensor</td>
<td>Senses light intensity.</td>
</tr>
</tbody>
</table>
<p>These nine sensors are sold by the Parallax Corporation in Rocklin, California.</p>
<p>To use these sensors with the WpdBasicHardwareDriver, you must purchase the sensors, a programmable microcontroller (Parallax BS2), a test board (like the Parallax BASIC Stamp Homework Board), an RS232 cable, and miscellaneous parts. All of this hardware
 is available from Parallax and can be ordered through their Web site. </p>
<p>The circuit designs are based on the sample circuits provided by Parallax in their sensor data sheets. These circuits are designed to integrate each sensor with the Parallax BS2 programmable microcontroller .
</p>
<p>The microcontroller firmware for each of the nine circuits is included in the src\wpd\WpdBasicHardwareDriver\firmware subdirectory in the Windows Driver Kit (WDK).</p>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597697">WPD Basic Hardware Driver</a> description in the Windows Driver Kit documentation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because
 the sample uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597864">WPD Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597568">WPD Driver Development Tools</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">WPD Programming Guide</a>
</dt></dl>
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
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>To test this sample, you must have a test computer that is running Windows&nbsp;Vista or later. This test computer can be a second computer or, if necessary, your development computer.</p>
<p>To install the WpdBasicHardwareDriver sample, do the following:</p>
<ol>
<li>
<p>Copy the driver binary and the wpdbasichardwaredriver.inf file to a directory on your test computer (for example, C:\wpdbasichardwaredriver.)
</p>
</li><li>
<p>Copy the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll, from the \redist\wdf\&lt;architecture&gt; directory to the same directory (for example, C:\wpdbasichardwaredriver).
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading and installing the “Windows Driver Framework (WDF)” package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>
<dl><dt>Navigate to the directory that contains the INF file and binaries (for example, cd /d c:\wpdbasichardwaredriver), and run DevCon.exe as follows:
</dt><dt><b>devcon.exe install wpdbasichardwaredriver.inf WUDF\WpdBasicHardware</b> </dt><dt>You can find DevCon.exe in the \tools directory of the WDK (for example, \tools\devcon\i386\devcon.exe).
</dt></dl>
</li></ol>
</div>
