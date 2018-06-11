# SystemTraceProvider
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:50:57
## Description

<div id="mainSection">
<p>This sample application demonstrates how to use event tracing control APIs to collect events from the system trace provider.
</p>
<p>The sample code provided shows how to start an <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/bb968803">
Event Tracing</a> for Windows trace session and how to enable system events with stacks. When you build and run the application, it collects the trace data for 30 seconds and then stops. The sample application writes the results to a file, Systemtrace.etl.
 For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff552961">
Tools for Software Tracing</a>. </p>
<p>You can process the Systemtrace.etl file using Tracerpt.exe. Tracerpt.exe is a command-line trace tool that formats trace events. It also analyzes the events and generates summary reports. Tracerpt is included in Windows XP and later versions of Windows.
 For more information about how to use this tool, see <a href="http://go.microsoft.com/fwlink/p/?linkid=179389">
Tracerpt</a> topic on the TechNet website. </p>
<p>You can also process the file using the <a href="http://go.microsoft.com/fwlink/p/?linkid=250774">
Windows Performance Toolkit</a> (WPT), which is available in the SDK.</p>
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
</li><li>Navigate to the directory that contains the sample driver source code and select the solution file (SystemTraceProvider.sln).
</li><li>Build the sample from the <b>Build</b> menu by selecting <b>Build Solution</b>.
</li></ol>
<h2>Run the sample</h2>
<ol>
<li>Using a <b>Command Prompt</b> window or Windows Explorer, navigate to the directory that contains the sample application you just built (SystemTraceControl.exe).
</li><li>Type SystemTraceControl.exe in the Command Prompt window, or double-click the icon for SystemTraceControl.exe to launch it from Windows Explorer.
</li></ol>
</div>
