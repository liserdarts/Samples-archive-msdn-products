# Tracedrv
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
* 2014-04-02 12:51:18
## Description

<div id="mainSection">
<p>Tracedrv is a sample driver instrumented for software tracing. The driver does not control any hardware; it simply generates trace messages. It is designed to show how to use WPP software tracing macros in a driver.
</p>
<p>Tracedrv initializes tracing (by using WPP_INIT_TRACING) and, when it receives a DeviceIOControl call, it starts a thread that logs 100 trace messages. The WPP software tracing directives, calls, and macros in the code are accompanied by comments that explain
 their purpose</p>
<p>While examining Tracedrv, read the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff556204">
WPP Software Tracing</a> in the Windows Driver Kit (WDK). This section includes a reference section that describes the directives, macros, and calls required for WPP software tracing.
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
<li>Start Visual Studio&nbsp;2013. </li><li>In the <b>File</b> menu, select <b>Open</b> and chose <b>Project/Solution...</b>
</li><li>Navigate to the directory that contains the sample driver source code and select the solution file (tracedrv.sln).
</li><li>Build the sample from the <b>Build</b> menu by selecting <b>Build Solution</b>. For more about building sample solutions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">Building a Driver</a> in the WDK documentation.
</li></ol>
<h2>Run the sample</h2>
<p>To test the Tracedrv event tracing provider, use the following procedure.</p>
<ol>
<li>Copy the Tracectl.exe file that was created when you built the Tracedrv solution from the Tracectl directory (for example, \Documents\Visual Studio 2013\Projects\tracedrv\tracectl\<i>platform</i>) to the Tracedrv directory (for example, \Documents\Visual
 Studio 2013\Projects\tracedrv\tracedrv\<i>platform</i>). </li><li>
<p>Use Tracepdb to create a trace message format (TMF) file and a trace message control (TMC) file from the Tracedrv.pdb file. Tracepdb is located in the C:\Program Files (x86)\Windows Kits\8.1\bin\<i>platform</i> directory. The PDB file that is used in this
 command is created when you the build the solution. Open a Visual Studio Command prompt window and navigate to the target build platform and configuration directory. Type the following command:
</p>
<p><b>tracepdb -f tracedrv.pdb</b> </p>
</li><li>
<p>In the same Tracedrv target build directory, create a control GUID file for Tracedrv by opening a text file, adding the following content, and saving the file as Tracedrv.ctl.
</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th>Text</th>
</tr>
<tr>
<td>
<pre>d58c126f-b309-11d1-969e-0000f875a5bc 
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Use Tracelog to start a trace session that is called <i>TestTracedrv</i>. Tracelog is located in the C:\Program Files (x86)\Windows Kits\8.1\bin\<i>platform</i> directory. The Tracedrv.ctl file that is used in this command was created in the previous step.
 The following command starts a trace session and creates a trace log file, tracedrv.etl, in the local directory.</p>
<pre class="syntax"><code>tracelog -start TestTracedrv -guid tracedrv.ctl -f tracedrv.etl -flag 1</code></pre>
<p class="note"><b>Note</b>&nbsp;&nbsp;Note: Without the -flag parameter, Tracedrv will not generate any trace messages.</p>
</li><li>To generate trace messages, run Tracectl.exe. This executable file is built when you build the solution. Each time you type a character, other than
<b>Q</b> or <b>q</b>, Tracectl sends an IOCTL to the driver that signals it to generate trace messages. To stop Tracectl, type
<b>Q</b> or <b>q</b>. </li><li>
<p>To stop the trace session, use the following Tracelog command.</p>
<pre class="syntax"><code>tracelog -stop TestTracedrv
</code></pre>
</li><li>
<p>To display the trace messages in the Tracedrv.etl file, use Tracefmt.exe. Tracefmt.exe is located in the C:\Program Files (x86)\Windows Kits\8.1\bin\<i>platform</i>. The TMF file used in this command was created by Tracepdb.exe in step 2. The
<b>-p</b> option specifies the directory of the TMF file. In this case, the TMF file is in the current directory. Type the following command:
</p>
<pre class="syntax"><code>tracefmt tracedrv.etl -p . -o Tracedrv.out</code></pre>
</li></ol>
<p>The resulting Tracedrv.out file is a human-readable text file of the Tracedrv trace messages. To interpret the trace messages, in the Tracedrv.c file, search for the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544918"><b>DoTraceMessage</b></a> macros.</p>
<h2><a id="Notes"></a><a id="notes"></a><a id="NOTES"></a>Notes</h2>
<p>This sample driver should not be used in a production environment. </p>
<p>Tracedrv is designed for Windows XP and later versions of Windows. It does not demonstrate how to add WPP software tracing to a Windows 2000 driver. (For information about adding WPP software tracing to a Windows 2000 driver, see the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff551795">Software Tracing FAQ</a> topic in the Windows DDK documentation.)</p>
<p>Also, because it is not a Plug and Play driver, Tracedrv does not demonstrate tracing in a Plug and Play environment.
</p>
<p>Tracedrv demonstrates the basic elements required for software tracing. It does not demonstrate more advanced tracing techniques, such as writing customized tracing calls (variations of
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544918"><b>DoTraceMessage</b></a>), or the use of WMI calls for software tracing.
</p>
<p>If you are building the Tracedrv sample to test on a 64-bit version of Windows, you need to sign the driver. Starting with Windows Vista, all 64-bit versions of Windows require driver code to have a digital signature for the driver to load. See
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554809">Signing a Driver</a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh967733">Signing a Driver During Development and Testing</a>. You might also need to configure the test computer so that it can load test-signed kernel mode code, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff553484">The TESTSIGNING Boot Configuration Option</a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff542202"><b>BCDEdit /set</b></a>.
</p>
</div>
