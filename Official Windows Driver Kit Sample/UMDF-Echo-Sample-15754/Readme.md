# UMDF Echo Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2012-10-26 11:28:32
## Description
<style> pre.syntax { font-size: 110 background: #dddddd; padding: 4px,8px; cursor: text; color: #000000; width: 97 } body{font-family:Verdana,Arial,Helvetica,sans-serif;color:#000;font-size:80%} H1{font-size:150%;font-weight:bold} H1.heading{font-size:110%;font-family:Verdana,Arial,Helvetica,sans-serif;font-weight:bold;line-height:120%}
 H2{font-size:115%;font-weight:700} H2.subtitle{font-size:180%;font-weight:400;margin-bottom:.6em} H3{font-size:110%;font-weight:700} H4,H5,H6{font-size:100%;font-weight:700} h4.subHeading{font-size:100%} dl{margin:0 0 10px;padding:0 0 0 1px} dt{font-style:normal;margin:0}
 li{margin-bottom:3px;margin-left:0} ol{line-height:140%;list-style-type:decimal;margin-bottom:15px;margin-left:24px} ol ol{line-height:140%;list-style-type:lower-alpha;margin-bottom:4px;margin-left:24px;margin-top:3px} ol ul,ul ol{line-height:140%;margin-bottom:15px;margin-top:15px}
 p{margin:0 0 10px;padding:0} div.section p{margin-bottom:15px;margin-top:0} ul{line-height:140%;list-style-position:outside;list-style-type:disc;margin-bottom:15px} ul ul{line-height:140%;list-style-type:disc;margin-bottom:4px;margin-left:17px;margin-top:3px}
 .heading{font-weight:700;margin-bottom:8px;margin-top:18px} .subHeading{font-size:100%;font-weight:700;margin:0} div#mainSection table{border:1px solid #ddd;font-size:100%;margin-bottom:5px;margin-left:5px;margin-top:5px;width:97%;clear:both} div#mainSection
 table tr{vertical-align:top} div#mainSection table th{border-bottom:1px solid #c8cdde;color:#006;padding-left:5px;padding-right:5px;text-align:left} div#mainSection table td{border:1px solid #d5d5d3;margin:1px;padding-left:5px;padding-right:5px} div#mainSection
 table td.imageCell{white-space:nowrap} /* These are the original lines from global-bn1945 div.ContentArea table th,div.ContentArea table td{background:#fff;border:0 solid #ccc;font-family:Verdana;padding:5px;text-align:left;vertical-align:top} div.ContentArea
 table th{background:#ccc none repeat scroll 0% 50%;vertical-align:bottom} div.ContentArea table{border-collapse:collapse;width:auto} */ /* Removing ContentArea class requirement from commented out lines from global-bn1945 above */ table th,table td{background:#fff;border:0
 solid #ccc;font-family:Verdana;padding:5px;text-align:left;vertical-align:top} table th{background:#ccc none repeat scroll 0% 50%;vertical-align:bottom} table{border-collapse:collapse;width:auto} div.clsNote{background-color:#eee;margin-bottom:4px;padding:2px}
 div.code{width:98.9%} code{font-family:Monospace,Courier New,Courier;font-size:105%;color:#006} span.label{font-weight:bold} div.caption{font-weight:bold;font-size:100%;color:#039} .procedureSubHeading{font-size:110%;font-weight:bold} span.sub{vertical-align:sub}
 span.sup{vertical-align:super} span.big{font-size:larger} span.small{font-size:smaller} span.tt{font-family:Courier,"Courier New",Consolas,monospace} .CCE_Message{color:Red;font-size:10pt} </style>
<div id="mainSection">
<p>This sample demonstrates how to use the User-Mode Driver Framework to write a driver and demonstrates best practices.
</p>
<p>It also demonstrates the use of a default Serial Dispatch I/O Queue, its request start events, cancellation event, and synchronizing with another thread. The preferred I/O retrieval mode is set to Direct I/O. So, whenever a request is received by the framework,
 UMDF looks at the size of the buffer and determines, whether it should copy the buffer (if the length is less than 2 full pages) or map it (if the length is greater or equal to 2 full pages).
</p>
<p>This sample driver is a minimal driver meant to demonstrate the usage of the User-Mode Driver Framework. It is not intended for use in a production environment.
</p>
<h3>Related technologies</h3>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff560456">User-Mode Driver Framework</a>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;Vista </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff554644">Building a Driver</a>.</p>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>In Visual Studio, you can press F5 to build the sample and then deploy it to a target machine. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Hh454834">Deploying a Driver to a Test Computer</a>.</p>
<h3><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h3>
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
<h3><a id="_______File_Manifest"></a><a id="_______file_manifest"></a><a id="_______FILE_MANIFEST"></a>File Manifest</h3>
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
