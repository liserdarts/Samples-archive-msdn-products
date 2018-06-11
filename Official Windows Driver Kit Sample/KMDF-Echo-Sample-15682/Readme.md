# KMDF Echo Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:04
## Description

<div id="mainSection">
<p>The ECHO (KMDF) sample demonstrates how to use a sequential queue to serialize read and write requests presented to the driver.
</p>
<p>It also shows how to synchronize execution of these events with other asynchronous events such as request cancellation and DPC.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff557565">Kernel-Mode Driver Framework</a>
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
<p>The version of the driver under the directory AutoSync demonstrates how the driver uses framework-provided synchronization to manage the ownership of the request between the I/O callbacks, timer DPC and the cancel routine. The version of the driver under
 the directory DriverSync demonstrates how the driver uses its own lock to manage the same.</p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the Windows Driver Framework. It is not intended for use in a production environment.
</p>
<p>If the build succeeds, you will find the driver, echo.sys, in the binary output directory specified for the build environment. You can get the output path from the buildxxx.log file (for example buildfre_win7_x86.log). If it fails you can find errors and
 warnings in the buildxxx.err and buildxxx.wrn respectively. </p>
<h2><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>In Visual Studio, you can press F5 to build the sample and then deploy it to a target machine. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>.</p>
<h2><a id="code_tour"></a><a id="CODE_TOUR"></a>Code Tour</h2>
<p><u>DriverEntry</u> - Creates a framework driver object.</p>
<p><u>EvtDeviceAdd</u>: Creates a device and registers self managed I/O callbacks so that it can start and stop the periodic timer when the device is entering and leaving D0 state. It registers a device interface so that application can find the device and
 send I/O. For managing I/O requests, the driver creates a default queue to receive only read &amp; write requests. All other requests sent to the driver will be failed by the framework. Then the driver creates a periodic timer to simulate asynchronous event.
 The purpose of this timer would be to complete the currently pending request. </p>
<p>In the AutoSync version of the sample, the queue is created with WdfSynchronizationScopeQueue so that I/O callbacks including cancel routine are synchronized with a queue-level lock. Since timer is parented to queue and by default timer objects are created
 with AutomaticSerialization set to <b>TRUE</b>, timer DPC callbacks will be serialized with EvtIoRead, EvtIoWrite and Cancel Routine.</p>
<p>In the DriverSync version of the sample, the queue is created with WdfSynchronizationScopeNone, so that the framework does not provide any synchronization. The driver synchronizes the I/O callbacks, cancel routine and the timer DPC using a spinlock that
 it creates for this purpose.</p>
<p><u>EvtIoWrite</u>: Allocates an internal buffer as big as the size of buffer in the write request and copies the data from the request buffer to internal buffer. The internal buffer address is saved in the queue context. If the driver receives another write
 request, it will free this one and allocate a new buffer to match the size of the incoming request. After copying the data, it will mark the request cancelable and return. The request will be eventually completed either by the timer or by the cancel routine
 if the application exits.</p>
<p><u>EvtIoRead</u>: Retrieves request memory buffer and copies the data from the buffer created by the write handler to the request buffer, and marks the request cancelable. The request will be completed by the timer DPC callback.</p>
<p>Since the queue is a sequential queue, only one request is outstanding in the driver.
</p>
<h2><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p></p>
<dl><dt><b>Usage:</b> </dt><dt>Echoapp.exe --- Send single write and read request synchronously </dt><dt>Echoapp.exe -Async --- Send 100 reads and writes asynchronously </dt><dt>Exit the app anytime by pressing Ctrl-C </dt></dl>
<p></p>
<h2><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>Echo.htm</p>
</td>
<td>
<p>Documentation for this sample (this file).</p>
</td>
</tr>
<tr>
<td colspan="2">
<p><b><i>(The AutoSync and DriverSync versions of the sample each have their own version of the following files)</i>
</b></p>
</td>
</tr>
<tr>
<td>
<p>Driver.h, Driver.c</p>
</td>
<td>
<p>DriverEntry and Events on the Driver Object.</p>
</td>
</tr>
<tr>
<td>
<p>Device.h, Device.c</p>
</td>
<td>
<p>Events on the Device Object.</p>
</td>
</tr>
<tr>
<td>
<p>Queue.h, Queue.c</p>
</td>
<td>
<p>Contains Events on the I/O Queue Objects.</p>
</td>
</tr>
<tr>
<td>
<p>Echo.inx</p>
</td>
<td>
<p>File that describes the installation of this driver. The build process converts this into an INF file.</p>
</td>
</tr>
<tr>
<td>
<p>Makefile.inc</p>
</td>
<td>
<p>A makefile that defines custom build actions. This includes the conversion of the .INX file into a .INF file</p>
</td>
</tr>
<tr>
<td>
<p>Makefile</p>
</td>
<td>
<p>This file merely redirects to the real makefile that is shared by all the driver components of the Windows NT DDK.</p>
</td>
</tr>
<tr>
<td>
<p>Sources</p>
</td>
<td>
<p>Generic file that lists source files and all the build options.</p>
</td>
</tr>
</tbody>
</table>
</div>
