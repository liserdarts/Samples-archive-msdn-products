# SystemTraceProvider
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* none
## Topics
* general
* Windows Driver
## IsPublished
* False
## ModifiedDate
* 2012-05-31 01:49:00
## Description

<div id="mainSection">
<div class="clsServerSDKContent">
<h1><a id="gallery_samples.examplegallerysample"></a>SystemTraceProvider</h1>
</div>
<p class="CCE_Message">[This documentation is preliminary and is subject to change.]</p>
<p>This sample application demonstrates how to use event tracing control APIs to collect events from the system trace provider.
</p>
<p>The sample code provided shows how to start an <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Bb968803">
Event Tracing</a> for Windows trace session and how to enable system events with stacks. When you build and run the application, it collects the trace data for 30 seconds and then stops. The sample application writes the results to a file, Systemtrace.etl.
 For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff552961">
Tools for Software Tracing</a>. </p>
<p>You can process the Systemtrace.etl file using Tracerpt.exe. Tracerpt.exe is a command-line trace tool that formats trace events. It also analyzes the events and generates summary reports. Tracerpt is included in Windows XP and later versions of Windows.
 For more information about how to use this tool, see <a href="http://go.microsoft.com/fwlink/?linkid=179389">
Tracerpt</a> topic on the TechNet website. </p>
<p>You can also process the file using the <a href="http://go.microsoft.com/fwlink/?LinkId=250774">
Windows Performance Toolkit</a> (WPT), which is available in the SDK.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 Release Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<ol>
<li>Start Visual Studio Ultimate&nbsp;2012 RC. </li><li>In the <b>File</b> menu, select <b>Open</b> and chose <b>Project/Solution...</b>
</li><li>Navigate to the directory that contains the sample driver source code and select the solution file (SystemTraceProvider.sln).
</li><li>Build the sample from the <b>Build</b> menu by selecting <b>Build Solution</b>.
</li></ol>
<h3>Run the sample</h3>
<ol>
<li>Using a <b>Command Prompt</b> window or Windows Explorer, navigate to the directory that contains the sample application you just built (SystemTraceControl.exe).
</li><li>Type SystemTraceControl.exe in the Command Prompt window, or double-click the icon for SystemTraceControl.exe to launch it from Windows Explorer.
</li></ol>
</div>
