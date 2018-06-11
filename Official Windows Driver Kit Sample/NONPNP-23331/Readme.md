# NONPNP
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:17:29
## Description

<div id="mainSection">
<p>This sample is primarily meant to demonstrate how to write a NON-PNP driver using the Kernel Mode Driver Framework.
</p>
<p>It also illustrates several other important framework interfaces. Following table gives a typical usage scenario and summarizes all the features used in this sample.</p>
<table>
<tbody>
<tr>
<th>Scenario</th>
<th>Features demonstrated</th>
</tr>
<tr>
<td>
<p>This sample would be useful for writing a driver that does not interact with any hardware. Typically, such drivers are written to provide some kernel-level services to a user application. These drivers are dynamically loaded by the application when it is
 run and unloaded when it exits. </p>
<p>(Examples: FileMon, Regmon, DeviceTree are examples of tools that use this type of driver.)</p>
</td>
<td>
<ul>
<li>
<p>How to Write a NON PNP driver</p>
</li><li>
<p>How to register EvtPreProcessCallback to handle requests in the context of the calling thread</p>
</li><li>
<p>Show how to probe and lock buffers in the preprocess callback for METHOD_NEITHER IOCTL requests
</p>
</li><li>
<p>Also show how to handle other 3 types of IOCTLs (METHOD_BUFFERED, METHOD_IN_DIRECT &amp; METHOD_OUT_DIRECT)</p>
</li><li>
<p>How to open a file in Kernel-mode and Read &amp; Write to it</p>
</li><li>
<p>Finally show event tracing and dumping variable length data in the tracelog using HEXDUMP format.</p>
</li></ul>
</td>
</tr>
</tbody>
</table>
<p>The sample is accompanied by a simple multithreaded Win32 console application to test the driver.</p>
<p>This sample is adapted from the original IOCTL sample present in WDK (src\general\ioctl).</p>
<p>Disclaimer: This is a minimal driver meant to demonstrate an OS feature. Neither it nor its sample programs are intended for use in a production environment. Rather, they are intended for educational purposes and as a skeleton driver.</p>
<h3>Related technologies</h3>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544396">Kernel-Mode Driver Framework</a>
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
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p>If the build succeeds, you will find the driver, nonpnp.sys, and the test application, nonpnpapp.exe, in the binary output directory specified for the build environment.</p>
<p>To test this driver, copy the nonpnp.inf into the same folder as the nonpnpapp.exe and the wdfcoinstaller&lt;version&gt;.dll .</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; </p>
<p class="note">You can obtain redistributable framework updates by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed. You can verify that the redistributables have been
 installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<p></p>
<p>Next, run nonpnpapp.exe, a simple Win32 multithreaded console mode application. The driver will be automatically loaded and started. When you exit the app, the driver will be stopped and removed.</p>
<p>Usage: nonpnpapp.exe (-l) (-v version)</p>
<p><b>Note</b>: This application first tries to open the device (\Device\FileIo). If the device doesn't exist, it takes that as a hint that the driver is not loaded and tries to load the driver using service control manager API. If the service is loaded successfully,
 it tries to open the device again. If successful, it makes all four different types of DeviceControl calls to the driver. After that it makes a WriteFile call with an arbitrary size buffer. The driver, in response, writes that buffer to a file opened in the
 Create request. The name of the file was provided by the application as part of the device name and the directory path is hardcoded to %WINDIR%\temp. When the WriteFile returns, the application makes a ReadFile call to read the file through the driver, and
 then compares the data returned by the driver with the one it originally wrote. If you specify -l option in command line, the application does this Write and Read operation in an infinite loop. The -v command line option is used to specify the version of the
 KMDF coinstaller (wdfcoinstaller&lt;version&gt;.dll) to load. If none is specified then it loads the coinstaller for v1.0 (wdfcoinstaller01000.dll)</p>
<p><b>File Manifest</b> </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>nonpnp.htm</p>
</td>
<td>
<p>Documentation for this sample (this file).</p>
</td>
</tr>
<tr>
<td>
<p>nonpnp.c</p>
</td>
<td>
<p>Source file of the sample driver.</p>
</td>
</tr>
<tr>
<td>
<p>nonpnp.h </p>
</td>
<td>
<p>Header file of the sample driver.</p>
</td>
</tr>
<tr>
<td>
<p>localwpp.ini</p>
</td>
<td>
<p>File used for event tracing and dumping variable length data in the tracelog using HEXDUMP format.</p>
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
<tr>
<td>
<p>Testapp.c</p>
</td>
<td>
<p>Source file of the test application.</p>
</td>
</tr>
<tr>
<td>
<p>Install.c</p>
</td>
<td>
<p>Contains functions to load/start/stop/remove the driver.</p>
</td>
</tr>
<tr>
<td>
<p>Testapp.h</p>
</td>
<td>
<p>Header file for common definitions and function prototypes.</p>
</td>
</tr>
<tr>
<td>
<p>nonpnp.inf</p>
</td>
<td>
<p>Inf file which contains the wdf section. The wdf section is used by the KMDF coinstaller to determine the KMDF library version being used by the nonpnp driver and to get the service name of the driver.</p>
</td>
</tr>
<tr>
<td>
<p>nonpnp.rc</p>
</td>
<td>
<p>Resource file that specifies information such as file type, version, etc.</p>
</td>
</tr>
<tr>
<td>
<p>Makefile</p>
</td>
<td>
<p>This file merely indirects to the real makefile that is shared by all the driver components of the Windows NT DDK.</p>
</td>
</tr>
<tr>
<td>
<p>trace.h</p>
</td>
<td>
<p>Header file for debug tracing related functions and macros</p>
</td>
</tr>
<tr>
<td>
<p>public.h </p>
</td>
<td>
<p>Header file containing definitions shared by the sample driver and test application.</p>
</td>
</tr>
</tbody>
</table>
<h4><a id="wdf_section"></a><a id="WDF_SECTION"></a><b>WDF SECTION</b> </h4>
<p>Nonpnp drivers typically don't need an INF file to install. Since we are using framework interfaces, we have to use the Kmdf coinstaller to install the framework binaries on the target machine. The Kmdf coinstaller needs a WDF specific section in the INF
 to get the driver service name and the version of the Kmdf library the driver is bound to. The syntax and description of the section is given below. Any non inf based driver using Kmdf library will need to have a dummy inf file with the wdf section in it.
 The format of the Wdf section is given below: </p>
<p></p>
<dl><dt>[Version] </dt><dt>Signature=&quot;$WINDOWS NT$&quot; </dt><dt>[&lt;driver name&gt;.NT.Wdf] </dt><dt>KmdfService = &lt;your driver service name here&gt;, &lt;driver service install subsection&gt;
</dt><dt>[&lt;driver service install subsection&gt;] </dt><dt>KmdfLibraryVersion = &lt;version bound to here, in Major.minor format&gt; </dt></dl>
<p></p>
<p>For example, for V1.0 KmdfLibraryVersion is &quot;1.0&quot;</p>
</div>
