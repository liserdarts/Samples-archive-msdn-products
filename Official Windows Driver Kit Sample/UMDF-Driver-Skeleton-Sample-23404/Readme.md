# UMDF Driver Skeleton Sample
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
* 2013-06-25 10:22:33
## Description

<div id="mainSection">
<p>This sample demonstrates how to use the User-Mode Driver Framework to write a minimal driver and shows best practices.
</p>
<p>The Skeleton driver will successfully load on a device (either root enumerated or a real hardware device) but has the minimum PnP functionality and does not support any I/O operations.</p>
<p>The Skeleton driver is a template from which many of the other UMDF sample drivers were constructed. It is intended to serve as a starting point for other UMDF drivers that you may write.</p>
<h3>Related technologies</h3>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560456">User-Mode Driver Framework</a>
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
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>In Visual Studio, you can set a breakpoint in your driver source code and then press F5 to build the sample and then deploy it to a target machine. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>.</p>
<p>Alternatively, you can install the sample from the command line. The Skeleton sample provides two different INX files UMDFSkeleton_Root.inx and UMDFSkeleton_OSR.inx. When you build the sample, the build engine produces UMDFSkeleton_Root.inf and UMDFSkeleton_OSR.inf.</p>
<p>You can use UMDFSkeleton_Root.inf to install the driver as a root-enumerated driver. Or, you can use UMDFSkeleton_OSR.inf to install the driver for use with the OSR USB-FX2 Learning Kit board.</p>
<p class="proch"><b>Installing the sample from the command line</b></p>
<ol>
<li>
<p>Copy the driver binary and either UMDFSkeleton_Root.inf or UMDFSkeleton_OSR.inf to a directory on your test machine (for example
<i>c:\skeletonSample</i>.) </p>
</li><li>
<p>Copy the UMDF coinstaller WUDFUpdate_<i>MMmmm</i>.dll to the same directory (for example
<i>c:\skeletonSample</i>). </p>
<p>If you are installing the sample for use with the OSR USB-FX2 Learning Kit board, you must also copy the KMDF coinstaller (for example WdfCoinstaller01009.dll) and the WinUSB coinstaller (WinUSBCoinstaller2.dll) to the installation directory.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; </p>
<p class="note">You can obtain redistributable framework updates by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed. You can verify that the redistributables have been
 installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<p></p>
</li><li>
<p>If you are installing the driver as a root-enumerated driver, follow this step.</p>
<p>To install using the Devcon.exe tool:</p>
<ol>
<li>Change to the directory containing the inf and binaries (for example <b>cd /d c:\skeletonSample</b>.)
</li><li>Run devcon.exe as follows: <b>devcon.exe install UMDFSkeleton_Root.inf UMDFSamples\Skeleton</b>
</li></ol>
<p>Devcon.exe can be found in the tools directory of your WDK enlistment (tools\&lt;platform&gt;\devcon.exe).</p>
</li><li>If you are installing the driver for use with the OSR USB-FX2 Learning Kit board, follow this step. To install the driver using Device Manager:
<ol>
<li>Plug in your device. </li><li>Launch Device Manager by executing command devmgmt.msc from a command prompt.
</li><li>Select the <b>OSR USB-FX2</b> device from the <b>Other Devices</b> category, right-click, and select
<b>Update Driver Software...</b>. </li><li>Select <b>Browse my computer for driver software</b> and provide the location of the driver files.
</li><li>Select <b>Install this driver software anyway </b>when the <b>Windows Security
</b>dialog box appears. </li><li>After the driver is installed, you should see the device in Device Manager. </li></ol>
</li></ol>
<h3><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th colspan="2">Description</th>
</tr>
<tr>
<td>
<p>Skeleton.htm</p>
</td>
<td colspan="2">
<p>Documentation for this sample (this file).</p>
</td>
</tr>
<tr>
<td>
<p>comsup.cpp &amp; comsup.h</p>
</td>
<td colspan="2">
<p>COM Support code - specifically base classes which provide implementations for the standard COM interfaces IUnknown and IClassFactory which are used throughout the skeleton sample.</p>
<p>The implementation of IClassFactory is designed to create instances of the CMyDriver class. If you should change the name of your base driver class, you would also need to modify this file.</p>
</td>
</tr>
<tr>
<td>
<p>dllsup.cpp</p>
</td>
<td colspan="2">
<p>DLL Support code - provides the DLL's entry point as well as the single required export (DllGetClassObject).</p>
<p>These depend on comsup.cpp to perform the necessary class creation.</p>
</td>
</tr>
<tr>
<td>
<p>exports.def</p>
</td>
<td colspan="2">
<p>This file lists the functions that the driver DLL exports.</p>
</td>
</tr>
<tr>
<td>
<p>makefile</p>
</td>
<td colspan="2">
<p>This file redirects to the real makefile, which is shared by all the driver components of the Windows Driver Kit.</p>
</td>
</tr>
<tr>
<td>
<p>internal.h</p>
</td>
<td colspan="2">
<p>This is the main header file for the skeleton driver. </p>
</td>
</tr>
<tr>
<td>
<p>driver.cpp &amp; driver.h</p>
</td>
<td colspan="2">
<p>Definition and implementation of the driver callback class for the Skeleton sample.
</p>
</td>
</tr>
<tr>
<td>
<p>device.cpp &amp; driver.h</p>
</td>
<td colspan="2">
<p>Definition and implementation of the device callback class for the Skeleton sample.</p>
</td>
</tr>
<tr>
<td>
<p>skeleton.rc</p>
</td>
<td colspan="2">
<p>This file defines resource information for the Skeleton sample driver.</p>
</td>
</tr>
<tr>
<td>
<p>sources</p>
</td>
<td colspan="2">
<p>Generic file that lists source files and all the build options</p>
</td>
</tr>
<tr>
<td>
<p>UMDFSkeleton_OSR.inx</p>
</td>
<td colspan="2">
<p>File for installing the Skeleton driver to control the OSR USB-FX2 Learning Kit device. The build process converts this into an INF file.</p>
</td>
</tr>
<tr>
<td>
<p>UMDFSkeleton_Root.inx</p>
</td>
<td colspan="2">
<p>File for installing the Skeleton driver to control a root enumerated device with a hardware ID of UMDFSamples\Skeleton. The build process converts this into an INF file.</p>
</td>
</tr>
<tr>
<td>
<p>Makefile.inc</p>
</td>
<td colspan="2">
<p>A makefile that defines custom build actions. This includes the conversion of the .INX file into a .INF file.</p>
</td>
</tr>
</tbody>
</table>
</div>
