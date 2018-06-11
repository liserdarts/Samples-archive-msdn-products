# WPD WUDF sample driver
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
* 2014-04-04 11:25:39
## Description

<div id="mainSection">
<p>The comprehensive WPD sample driver (WpdWudfSampleDriver) demonstrates virtually all aspects of the Microsoft Windows Portable Devides (WPD) device driver interface (DDI). This driver is built as a normal User-Mode Driver Framework (UMDF) driver that also
 processes the WPD command set. Although this driver does not interact with actual hardware, it simulates communicating with a device that supports phone contacts, pictures, music, and video.
</p>
<p>This driver was written in the simplest way to demonstrate concepts. Therefore, the sample driver might perform operations or be structured in a way that are inefficient in a production driver. Additionally, this sample does not use real hardware. Instead,
 it simulates a device by using data structures in memory. Therefore, the driver might be implemented in a way that is unrealistic for production hardware.</p>
<p>Some of the tasks that are accomplished by the WpdWudfSampleDriver are written for the advanced Windows Portable Devices (WPD) driver developer.</p>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597723">WPD WUDF Sample Driver</a> description in the Windows Driver Kit documentation.</p>
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
<p>To install the WpdWudfSampleDriver sample, do the following:</p>
<ol>
<li>
<p>Copy the driver binary and the WpdWudfSampleDriver.inf file to a directory on your test computer (for example, C:\WpdWudfSampleDriver.)
</p>
</li><li>
<p>Copy the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll, from the \redist\wdf\&lt;architecture&gt; directory to the same directory (for example, C:\WpdWudfSampleDriver).
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading and installing the “Windows Driver Framework (WDF)” package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>
<dl><dt>Navigate to the directory that contains the INF file and binaries (for example, cd /d c:\WpdWudfSampleDriver), and run DevCon.exe as follows:
</dt><dt><b>devcon.exe install WpdWudfSampleDriver.inf WUDF\Basic</b> </dt><dt>You can find DevCon.exe in the \tools directory of the WDK (for example, \tools\devcon\i386\devcon.exe).
</dt></dl>
</li></ol>
</div>
