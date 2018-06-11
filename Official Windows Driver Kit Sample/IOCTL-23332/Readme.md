# IOCTL
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
* 2013-06-25 10:17:32
## Description

<div id="mainSection">
<p>This sample demonstrates the usage of four different types of IOCTLs (METHOD_IN_DIRECT, METHOD_OUT_DIRECT, METHOD_NEITHER, and METHOD_BUFFERED).
</p>
<p>The sample shows how the user input and output buffers specified in the <b>DeviceIoControl</b> function call are handled, in each case, by the I/O subsystem and the driver.</p>
<p>The sample consists of a legacy device driver and a Win32 console test application. The test application opens a handle to the device exposed by the driver and makes all four different
<b>DeviceIoControl</b> calls, one after another. To understand how the IRP fields are set the I/O manager, you should run the checked build version of the driver and look at the debug output.</p>
<p>This driver will work on Windows 2000 and later operating systems.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample driver is not a Plug and Play driver. This is a minimal driver meant to demonstrate a feature of the operating system. Neither this driver nor its sample programs are intended for use in a production environment. Instead,
 they are intended for educational purposes and as a skeleton driver.</p>
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
<p>To test this driver, copy the test app, Ioctlapp.exe, and the driver to the same directory, and run the application. The application will automatically load the driver, if it's not already loaded, and interact with the driver. When you exit the application,
 the driver will be stopped, unloaded and removed.</p>
</div>
