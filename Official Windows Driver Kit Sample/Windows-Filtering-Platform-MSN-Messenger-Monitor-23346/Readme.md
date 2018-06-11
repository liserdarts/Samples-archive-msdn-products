# Windows Filtering Platform MSN Messenger Monitor Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WFP
* Windows Driver
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:18:19
## Description

<div id="mainSection">
<p>This sample application and driver demonstrate the stream inspection capabilities of the Windows Filtering Platform (WFP).
</p>
<p>The sample consists of a user mode application (Monitor.exe) that registers traffic of interest. In this case, all Transmission Control Protocol (TCP) data segments that are sent and received by the MSN Messenger application. Monitor.exe adds filters and
 callouts to Windows through the Windows Filtering Platform (WFP) Win32 API. A kernel-mode WFP callout driver (Msnmntr.sys) intercepts TCP traffic and parses out communication patterns. Monitor.exe controls the operations of the callout driver through I/O controls
 (IOCTLs).</p>
<p>The filters and callouts added by Monitor.exe are persistent across system restarts and removed only by Monitor.exe. Adding filters and callouts requires administrator privileges. Therefore, Monitor.exe must be run from an elevated command prompt.</p>
<p>Msnmntr.sys registers itself at two different WFP layers: FLOW-ESTABLISHED and STREAM. For simplicity, only Internet Protocol version 4 (IPv4) traffic is inspected. Msnmntr.sys registers at the FLOW-ESTABLISHED layer to associate a callout driver-specific
 data structure with application identity (that is, path) recorded such that the STREAM layer will only be invoked if traffic is sent or received from that particular application.</p>
<p>After the filters and callouts are in place and registered, WFP indicates TCP data segments to the Msnmntr.sys for inspection. As the data flows through Msnmntr.sys, it copies them (described by a chain of NET_BUFFER_LIST structures) to a flat buffer, parses
 out the communication patterns (such as client-to-server/client-to-client), and sends them to the Windows Software Trace Preprocessor (WPP) for tracing.</p>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3>Run the sample</h3>
<p>To start the driver, type <b>net start msnmntr</b> from an elevated command prompt. To stop the driver, type
<b>net stop msnmntr</b> from an elevated command prompt.</p>
<p>After the driver is installed and started, follow the steps below to activate monitoring:</p>
<ol>
<li>Copy the Monitor.exe file to the computer running the Msnmntr.sys driver. </li><li>Open an elevated command prompt and change to the folder containing Monitor.exe.
</li><li>
<p>From the command prompt, type the following:</p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th></th>
</tr>
<tr>
<td>
<pre>monitor addcallouts
monitor monitor</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ol>
<p>Launch MSN Messenger and begin an instant messenger session.</p>
<h4><a id="WPP_Software_Tracing_Example"></a><a id="wpp_software_tracing_example"></a><a id="WPP_SOFTWARE_TRACING_EXAMPLE"></a>WPP Software Tracing Example</h4>
<p>The Msnmntr.sys driver uses WPP Software Tracing to log its actions.</p>
<p>For one way to collect trace logs from the Msnmntr.sys sample driver with the Traceview.exe tool, do the following:</p>
<ol>
<li>From an elevated command prompt, copy Msnmntr.pdb within the build folder of src\network\trans\msnmntr\sys to another folder and then change to that folder.
</li><li>Run the Traceview.exe tool from the appropriate folder in the \tools\tracing folder.
</li><li>In the <b>TraceView</b> window, click <b>File</b>, click <b>Create New Log Session</b>, click
<b>Add Provider</b>, specify the Msnmntr.pdb file, click <b>Open</b>, and then click
<b>OK.</b> </li><li>Click <b>Next</b>, specify log session options, and then click <b>Finish.</b>
</li><li>From the command prompt, run the Monitor.exe application to start monitoring.
</li><li>Watch the communication patterns being displayed in the Traceview.exe tool. </li></ol>
<p>Tracing for the sample driver can be started at any time before the driver is started or while the driver is already running.</p>
<h3><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>src\network\trans\msnmntr\ exe\makefile</td>
<td>Standard WDK build environment makefile.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ sys\makefile</td>
<td>Standard WDK build environment makefile.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ exe\sources</td>
<td>Generic file for building the code sample.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ sys\sources</td>
<td>Generic file for building the code sample.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ exe\monitor.cpp</td>
<td>Adds callouts and filters and controls the callout driver with IOCTLs.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ sys\msnmntr.c</td>
<td>Implements callout driver classify functions.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ sys\notify.c</td>
<td>Contains MSN Messenger commands parsing code.</td>
</tr>
<tr>
<td>src\network\trans\msnmntr\ inc\*.h</td>
<td>Contains shared data types between monitor.cpp and msnmntr.c.</td>
</tr>
</tbody>
</table>
<p>For more information on creating a Windows Filtering Platform Callout Driver, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571068">Windows Filtering Platform Callout Drivers</a>.</p>
</div>
