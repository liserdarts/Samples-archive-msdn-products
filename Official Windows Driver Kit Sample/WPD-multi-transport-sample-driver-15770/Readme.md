# WPD multi-transport sample driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* wpd
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:25:27
## Description

<div id="mainSection">
<p>The WpdMultiTransportDriver sample demonstrates how you could extend the WpdHelloWorldDriver for a device that supports multiple transports. A transport is a protocol over which a portable device communicates with a computer. Example transports include Internet
 Protocol (IP), Bluetooth, and USB. </p>
<p>A number of portable devices now support multiple transports. For example, a number of cell phones support both Bluetooth and USB. Before Windows 7, if a user connected a portable device that supported multiple transports to their computer, the Windows Device
 Manager displayed a unique node for each transport. This implied that multiple devices had been installed and left the user confused. To resolve this, Windows now supports a multitransport driver model. This model ensures that only one node appears for each
 device.</p>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597709">WPD MultiTransport Driver</a> description in the Windows Driver Kit documentation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
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
<p>To install the WpdMultiTransportDriver sample, do the following:</p>
<ol>
<li>
<p>Copy the driver binary and the WpdMultiTransportDriver.inf file to a directory on your test computer (for example, C:\WpdMultiTransportDriver.)
</p>
</li><li>
<p>Copy the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll, from the \redist\wdf\&lt;architecture&gt; directory to the same directory (for example, C:\WpdMultiTransportDriver).
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading and installing the “Windows Driver Framework (WDF)” package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>
<dl><dt>Navigate to the directory that contains the INF file and binaries (for example, cd /d c:\WpdMultiTransportDriver), and run DevCon.exe as follows:
</dt><dt><b>devcon.exe install WpdMultiTransportDriver.inf WUDF\MultiTransport</b> </dt><dt>You can find DevCon.exe in the \tools directory of the WDK (for example, \tools\devcon\i386\devcon.exe).
</dt></dl>
</li></ol>
</div>
