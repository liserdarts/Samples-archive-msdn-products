# Tracedrv
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:11
## Description

<div id="mainSection">
<p>Tracedrv is a sample driver instrumented for software tracing. The driver does not control any hardware; it simply generates trace messages. It is designed to show how to use WPP software tracing macros in a driver.
</p>
<p>Tracedrv initializes tracing (by using WPP_INIT_TRACING) and, when it receives a DeviceIOControl call, it starts a thread that logs 100 trace messages. The WPP software tracing directives, calls, and macros in the code are accompanied by comments that explain
 their purpose</p>
<p>While examining Tracedrv, read the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff556204">
WPP Software Tracing</a> in the Windows Driver Kit (WDK). This section includes a reference section that describes the directives, macros, and calls required for WPP software tracing.
</p>
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
<ol>
<li>Start Visual Studio Ultimate&nbsp;2012. </li><li>In the <b>File</b> menu, select <b>Open</b> and chose <b>Project/Solution...</b>
</li><li>Navigate to the directory that contains the sample driver source code and select the solution file (tracedrv.sln).
</li><li>Build the sample from the <b>Build</b> menu by selecting <b>Build Solution</b>. For more about building sample solutions, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">Building a Driver</a> in the WDK documentation.
</li></ol>
<h3>Run the sample</h3>
<p>To test the Tracedrv event tracing provider, use the following procedure.</p>
<ol>
<li>Copy the Tracectl.exe file that was created when you built the Tracedrv solution from the Tracectl directory (for example,\Documents\Visual Studio 11\Projects\tracedrv\tracectl\<i>platform</i>) to the Tracedrv directory (for example, \Documents\Visual Studio
 11\Projects\tracedrv\tracedrv\<i>platform</i>). </li><li>
<p>Use Tracepdb to create a trace message format (TMF) file and a trace message control (TMC) file from the Tracedrv.pdb file. Tracepdb is located in the \Program Files\Windows Kits\8.0\bin\<i>platform</i> directory. The PDB file that is used in this command
 is created when you the build the solution. Open a Visual Studio Command prompt window and navigate to the \tools\tracing\<i>platform</i> subdirectory. Type the following command:
</p>
<p><b>tracepdb -f </b><i>path</i><b>\tracedrv.pdb</b> </p>
</li><li>
<p>Create a control GUID file for Tracedrv by opening a text file, adding the following content, and saving the file as Tracedrv.ctl.
</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th></th>
</tr>
<tr>
<td>
<pre>CtlGuid d58c126f-b309-11d1-969e-0000f875a5bc
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Use Tracelog to start a trace session that is called &quot;TestTracedrv&quot;. Tracelog is located in the \Program Files\Windows Kits\8.0\Tools\Tracing\<i>platform</i> directory. The Tracedrv.ctl file that is used in this command was created in the previous step.
 The following command starts a trace session and creates a trace log file, tracedrv.etl, in the local directory.</p>
<pre class="syntax"><code>tracelog -start TestTracedrv -guid tracedrv.ctl -f tracedrv.etl -flag 1</code></pre>
<p class="note"><b>Note</b>&nbsp;&nbsp;Note: Without the -flag parameter, Tracedrv will not generate any trace messages.</p>
</li><li>To generate trace messages, run Tracectl.exe. This executable file is built when you build the solution. Each time you type a character, other than Q or q, Tracectl sends an IOCTL to the driver that signals it to generate trace messages. To stop Tracectl,
 type <b>Q</b> or <b>q</b>. </li><li>
<p>To stop the trace session, use the following Tracelog command.</p>
<pre class="syntax"><code>tracelog -stop TestTracedrv
</code></pre>
</li><li>
<p>To display the trace messages in the Tracedrv.etl file, use Tracefmt. Tracefmt is located in the \Program Files\Windows Kits\8.0\Tools\Tracing\<i>platform</i>. The TMF file used in this command was created by Tracepdb in step 2. Type the following command:
</p>
<pre class="syntax"><code>tracefmt tracedrv.etl -p &lt;Path to TMF file&gt; -o Tracedrv.out</code></pre>
</li></ol>
<p>The resulting Tracedrv.out file is a human-readable text file of the Tracedrv trace messages. To interpret the trace messages, in the Tracedrv.c file, search for the DoTraceMessage macros.</p>
<h3><a id="Notes"></a><a id="notes"></a><a id="NOTES"></a>Notes</h3>
<p>This sample driver should not be used in a production environment. </p>
<p>Tracedrv is designed for Windows XP and later versions of Windows. It does not demonstrate how to add WPP software tracing to a Windows 2000 driver. (For information about adding WPP software tracing to a Windows 2000 driver, see the Tracing FAQ topic in
 the Windows DDK documentation.)</p>
<p>Also, because it is not a Plug and Play driver, Tracedrv does not demonstrate tracing in a Plug and Play environment.
</p>
<p>Tracedrv demonstrates the basic elements required for software tracing. It does not demonstrate more advanced tracing techniques, such as writing customized tracing calls (variations of DoTraceMessage), or the use of WMI calls for software tracing.
</p>
</div>
