# WSK TCP Echo Server
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* ndis
* Windows Driver
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:10:04
## Description

<div id="mainSection">
<p>This sample driver is a minimal driver meant to demonstrate the usage of the Winsock Kernel (WSK) programming interface.
</p>
<p>The sample implements a simple kernel-mode application by using the Winsock Kernel (WSK) programming interface. The application accepts incoming TCP connection requests on port 40007 over both IPv4 and IPv6 and, on each connection, it echoes all received
 data back to the peer until the connection is closed by the peer. The application is designed to use a single worker thread to perform all of its processing. For better performance on a multi-processor computer, the sample can be enhanced to use more worker
 threads. This sample is designed such that operations on a given connection should always be processed by the same worker thread. This provides a simple form of synchronization that ensures proper socket closure in a setting where multiple operations might
 be outstanding and completed asynchronously on a given connection. For the sake of simplicity, this sample does not enforce any limit on the number of connections accepted (other than the natural limit imposed by the available system memory) or on the amount
 of time that a connection stays alive. A production server application should be designed with these security points in mind.</p>
<p>This sample is not intended for use in a production environment.</p>
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
<h3><a id="WPP_SOFTWARE_TRACING"></a><a id="wpp_software_tracing"></a>WPP SOFTWARE TRACING</h3>
<p>This sample driver uses WPP Software Tracing in order to log its actions. You can find detailed information on WPP Software Tracing in the WDK documentation. Here is a quick overview of one way to collect trace logs from the sample driver by using the tracing
 tools that are available in the \tools\tracing directory in the WDK. All code for this sample is located in the src\network\WSK\echosrv directory.</p>
<ol>
<li>In a Command Prompt window, copy Echosrv.ctl and Echosrv.pdb into a directory and change to that directory (cd).
</li><li>
<p>Start software tracing for the sample driver by typing the following command:</p>
<p><b>tracelog -start echosrvtrace -guid echosrv.ctl -f logfile.etl -flags 0x3</b>
</p>
<p>The value that is provided for the -flags option determines which events will be logged by the sample driver. The sample currently has two event types denoted by the TRCERROR and TRCINFO macros where TRCERROR is 0x1 and TRCINFO is 0x2. Thus, a flag value
 of 0x3 (0x1 combined in a bitwise OR with 0x2) in the previous tracelog command tells the sample driver to log both TRCERROR and TRCINFO events.</p>
</li><li>
<p>In order to stop tracing, type the following command:</p>
<p>tracelog -stop echosrvtrace</p>
</li><li>
<p>Convert the trace logs in Logfile.etl into a human-readable format by typing the following command:</p>
<p><b>tracefmt -o logfile.txt -f logfile.etl -r . -i \</b><i>full-path</i><b>\ echosrv.sys</b>
</p>
</li><li>Open Logfile.txt to view the trace logs. </li></ol>
<p>Be aware that tracing for the sample driver can be started at any time before the driver is started or while the driver is already running.</p>
<h3><a id="To_run_the_sample"></a><a id="to_run_the_sample"></a><a id="TO_RUN_THE_SAMPLE"></a>To run the sample</h3>
<p>Install and run this sample driver by using the following steps:</p>
<ol>
<li>Copy the Echosrv.sys file to a directory on the test machine. </li><li>
<p>In a Command Prompt window, type the following command:</p>
<p><b>sc create echosrv type= kernel binpath= \</b><i>full-path</i><b>\ echosrv.sys</b>
</p>
<p>where \<i>full-path</i>\ is the directory that contains the Echosrv.sys file.</p>
</li><li>
<p>To start the driver, type:</p>
<p><b>sc start echosrv</b> </p>
</li><li>
<p>To stop the driver, type:</p>
<p><b>sc stop echosrv</b> </p>
</li></ol>
<p>After the driver is installed and started, it will listen for incoming TCP connection requests on port 40007 over both IPv4 and IPv6 protocols until the driver is stopped. On each connection, the driver will echo all the received data back to the peer until
 the connection is closed by the peer.</p>
<p>For more information on the usage of the Winsock Kernel (WSK) programming interface, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff571084">Winsock Kernel</a>.</p>
</div>
