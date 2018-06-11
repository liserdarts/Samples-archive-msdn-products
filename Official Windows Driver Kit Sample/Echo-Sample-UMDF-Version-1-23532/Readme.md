# Echo Sample (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:38
## Description

<div id="mainSection">
<p>This sample demonstrates how to use User-Mode Driver Framework (UMDF) version 1 to write a driver and demonstrates best practices.
</p>
<p>It also demonstrates the use of a default Serial Dispatch I/O Queue, its request start events, cancellation event, and synchronizing with another thread. The preferred I/O retrieval mode is set to Direct I/O. So, whenever a request is received by the framework,
 UMDF looks at the size of the buffer and determines, whether it should copy the buffer (if the length is less than 2 full pages) or map it (if the length is greater or equal to 2 full pages).
</p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the User-Mode Driver Framework. It is not intended for use in a production environment.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560456">User-Mode Driver Framework</a>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>In Visual Studio, you can press F5 to build the sample and then deploy it to a target machine. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>.</p>
<h2><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p>To test the Echo driver, you can run echoapp.exe which is built from src\general\echo\exe.
</p>
<p>First install the device as described above. Then run echoapp.exe.</p>
<pre class="syntax"><code>D:\&gt;echoapp /?
Usage:
Echoapp.exe --- Send single write and read request synchronously
Echoapp.exe -Async --- Send 100 reads and writes asynchronously
Exit the app anytime by pressing Ctrl-C
 
D:\&gt;echoapp
DevicePath: \\?\root#sample#0000#{cdc35b6e-0be4-4936-bf5f-5537380a7c1a}
Opened device successfully
512 Pattern Bytes Written successfully
512 Pattern Bytes Read successfully
Pattern Verified successfully
 
D:\&gt;echoapp -Async
DevicePath: \\?\root#sample#0000#{cdc35b6e-0be4-4936-bf5f-5537380a7c1a}
Opened device successfully
Starting AsyncIo
Number of bytes written by request number 0 is 1024
Number of bytes read by request number 0 is 1024
Number of bytes read by request number 1 is 1024
Number of bytes written by request number 2 is 1024
Number of bytes read by request number 2 is 1024
Number of bytes written by request number 3 is 1024
Number of bytes read by request number 3 is 1024
Number of bytes written by request number 4 is 1024
Number of bytes read by request number 4 is 1024
Number of bytes written by request number 5 is 1024
Number of bytes read by request number 5 is 1024
Number of bytes written by request number 6 is 1024
Number of bytes read by request number 6 is 1024
Number of bytes written by request number 7 is 1024
Number of bytes read by request number 7 is 1024
Number of bytes written by request number 8 is 1024
Number of bytes read by request number 8 is 1024
Number of bytes written by request number 9 is 1024
Number of bytes read by request number 9 is 1024
Number of bytes written by request number 10 is 1024
Number of bytes read by request number 10 is 1024
Number of bytes written by request number 11 is 1024
...
</code></pre>
<p>Note that the reads and writes are performed by independent threads in the echo test application. As a result the order of the output may not exactly match what you see above.
</p>
<h2><a id="_______File_Manifest"></a><a id="_______file_manifest"></a><a id="_______FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File </th>
<th>Description </th>
</tr>
<tr>
<td>
<p>comsup.cpp &amp; comsup.h </p>
</td>
<td>
<p>COM Support code - specifically base classes which provide implementations for the standard COM interfaces IUnknown and IClassFactory which are used throughout this sample.
</p>
<p>The implementation of IClassFactory is designed to create instances of the CMyDriver class. If you should change the name of your base driver class, you would also need to modify this file.
</p>
</td>
</tr>
<tr>
<td>
<p>dllsup.cpp </p>
</td>
<td>
<p>DLL Support code - provides the DLL's entry point as well as the single required export (DllGetClassObject).
</p>
<p>These depend on comsup.cpp to perform the necessary class creation. </p>
</td>
</tr>
<tr>
<td>
<p>exports.def </p>
</td>
<td>
<p>This file lists the functions that the driver DLL exports. </p>
</td>
</tr>
<tr>
<td>
<p>internal.h</p>
</td>
<td>
<p>This is the main header file for this driver. </p>
</td>
</tr>
<tr>
<td>
<p>Driver.cpp and Driver.h</p>
</td>
<td>
<p>DriverEntry and events on the driver object.</p>
</td>
</tr>
<tr>
<td>
<p>Device.cpp and Device.h</p>
</td>
<td>
<p>The Events on the device object.</p>
</td>
</tr>
<tr>
<td>
<p>Queue.cpp and Queue.h</p>
</td>
<td>
<p>Contains Events on the I/O Queue Objects. </p>
</td>
</tr>
<tr>
<td>
<p>Echo.rc </p>
</td>
<td>
<p>Resource file for the driver. </p>
</td>
</tr>
<tr>
<td>
<p>WUDFEchoDriver.inx </p>
</td>
<td>
<p>File that describes the installation of this driver. The build process converts this into an INF file.</p>
</td>
</tr>
<tr>
<td>
<p>makefile.inc </p>
</td>
<td>
<p>A makefile that defines custom build actions. This includes the conversion of the .INX file into a .INF file
</p>
</td>
</tr>
<tr>
<td>
<p>echodriver.ctl </p>
</td>
<td>
<p>This file lists the WPP trace control GUID(s) for the sample driver. This file can be used with the tracelog command's -guid flag to enable the collection of these trace events within an established trace session.</p>
<p>These GUIDs must remain in sync with the trace control GUIDs defined in internal.h.</p>
</td>
</tr>
</tbody>
</table>
</div>
