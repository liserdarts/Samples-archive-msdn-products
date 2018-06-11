# Eventdrv
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
* 2014-04-02 12:45:54
## Description

<div id="mainSection">
<p></p>
<p>Eventdrv is a sample kernel-mode trace provider and driver. The driver does not control any hardware; it simply generates trace events. It is designed to demonstrate the use of the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545699">Event Tracing for Windows (ETW)</a> API in a driver.
</p>
<p>Evntdrv registers as a provider by calling the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545603">
<b>EtwRegister</b></a> API. If the registration is successful, it logs a StartEvent with the device's name, the length of the name, and the status code. Then, when the sample receives a DeviceIOControl call, it logs a SampleEventA event. Finally, when the driver
 gets unloaded, it logs an UnloadEvent event with a pointer to the device object</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The Windows Pre-Processor (WPP) Tracing tools such as TraceView.exe cannot be used to start, stop, or view traces.
</p>
<p></p>
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
<ol>
<li>Start Visual Studio. </li><li>In the <b>File</b> menu, select <b>Open</b> and chose <b>Project/Solution...</b>
</li><li>Navigate to the directory that contains the sample driver source code and select the solution file (Eventdrv.sln).
</li><li>Build the sample from the <b>Build</b> menu by selecting <b>Build Solution</b>. For more about building sample solutions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">Building a Driver</a> in the WDK documentation.
</li></ol>
<p>When you build the solution, it builds the two projects, Eventdrv.sys and Evntctrl.exe. Evntctrl.exe sends IOCTLs to Eventdrv.sys, which then generates trace messages when it receives the IOCTLs.
</p>
<h2>Run the sample</h2>
<ol>
<li>
<p>Install the manifest (Evntdrv.xml), which is located in the Evntdrv\Eventdrv folder. Open a Visual Studio Command window (Run as administrator) and use the following command:
</p>
<pre class="syntax"><code>wevtutil im evntdrv.xml</code></pre>
<p>Installing the manifest creates registry keys that enable tools to find the resource and message files that contain event provider information. For further details about the WevtUtil.exe tool, see the MSDN Library.
</p>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Using a Visual Studio Command windows sets up the environment variables you need to run the tracing tools for this sample.
</p>
<p></p>
</li><li>
<p>Make a folder in the system directory called ETWDriverSample (for example, C:\ETWDriverSample).
</p>
<p>Copy Eventdrv.sys and Evntctrl.exe to the ETWDriverSample folder.</p>
<p>The ETWDriverSample directory must be created because the path to the resource file that is specified in the evntdrv.xml manifest points to the %SystemRoot%\ETWDriverSample folder. If this folder is not created and the Eventdrv.sys binary is not copied,
 decoding tools cannot find the event information to decode the trace file.</p>
</li><li>
<p>Use Tracelog to start a trace session that is called &quot;TestEventdrv.&quot; The following command starts the trace session and creates a trace log file, Eventdrv.etl, in the local directory.</p>
<pre class="syntax"><code>Tracelog -start TestEventdrv -guid #b5a0bda9-50fe-4d0e-a83d-bae3f58c94d6 -f Eventdrv.etl</code></pre>
</li><li>
<p>To generate trace messages, run Evntctrl.exe. Each time you type a character other than
<b>Q</b> or <b>q</b>, Evntctrl sends an IOCTL to the driver that signals it to generate trace messages. To stop Evntctrl, type
<b>Q</b> or <b>q</b>.</p>
</li><li>
<p>To stop the trace session, run the following command:</p>
<pre class="syntax"><code>tracelog -stop TestEventdrv</code></pre>
</li><li>
<p>To display the traces collected in the Tracedrv.etl file, run the following command:
</p>
<pre class="syntax"><code>tracerpt Eventdrv.etl</code></pre>
<p>This command creates two files: Summary.txt and Dumpfile.xml. Dumpfile.xml will contain the event information in an XML format.</p>
</li><li>
<p>To uninstall the manifest, run the following command: </p>
<pre class="syntax"><code>wevtutil um evntdrv.xml</code></pre>
</li></ol>
<h2><a id="Notes"></a><a id="notes"></a><a id="NOTES"></a>Notes</h2>
<p>If you are building the Eventdrv sample to test on a 64-bit version of Windows, you need to sign the driver. Starting with Windows Vista, all 64-bit versions of Windows require driver code to have a digital signature for the driver to load. See
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554809">Signing a Driver</a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh967733">Signing a Driver During Development and Testing</a>. You might also need to configure the test computer so that it can load test-signed kernel mode code, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff553484">The TESTSIGNING Boot Configuration Option</a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff542202"><b>BCDEdit /set</b></a>.
</p>
</div>
