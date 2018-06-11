# WPD WUDF sample driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* wpd
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:23:45
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
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597864">WPD Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597568">WPD Driver Development Tools</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">WPD Programming Guide</a>
</dt></dl>
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
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>To test this sample, you must have a test computer that is running Windows&nbsp;Vista or later. This test computer can be a second computer or, if necessary, your development computer.</p>
<p>To install the WpdWudfSampleDriver sample, do the following:</p>
<ol>
<li>
<p>Copy the driver binary and the WpdWudfSampleDriver.inf file to a directory on your test computer (for example, C:\WpdWudfSampleDriver.)
</p>
</li><li>
<p>Copy the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll, from the \redist\wdf\&lt;architecture&gt; directory to the same directory (for example, C:\WpdWudfSampleDriver).
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading and installing the “Windows Driver Framework (WDF)” package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>
<dl><dt>Navigate to the directory that contains the INF file and binaries (for example, cd /d c:\WpdWudfSampleDriver), and run DevCon.exe as follows:
</dt><dt><b>devcon.exe install WpdWudfSampleDriver.inf WUDF\Basic</b> </dt><dt>You can find DevCon.exe in the \tools directory of the WDK (for example, \tools\devcon\i386\devcon.exe).
</dt></dl>
</li></ol>
</div>
