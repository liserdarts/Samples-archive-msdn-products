# SDMA
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
* 2013-06-25 10:21:59
## Description

<div id="mainSection">
<p>This sample demonstrates the usage of V3 System DMA. It shows how a driver could use a system DMA controller supported by Windows to write data to a hardware location using DMA.
</p>
<p>The sample consists of a legacy device driver and a Win32 console mode test application. The test application opens a handle to the device exposed by the driver and makes a DeviceIoControl call to initiate the example system DMA. To understand how the V3
 system DMA calls are invoked please study SDmaWrite() in SDma.c.</p>
<p>This driver will work on Windows 2000 and later operating systems.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample driver is not a PnP driver. This is a minimal driver meant to demonstrate an OS feature. Neither it nor its sample programs are intended for use in a production environment. Rather, they are intended for educational
 purposes and as a skeleton driver.</p>
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
<p>Click the Free Build Environment or Checked Build Environment icon under Development Kits program group to set basic environment variables.</p>
<p>Change to the directory containing the sample source code, such as CD Src\General\sdma\wdm.</p>
<p>Run build -ceZ, or use the macro BLD. This command invokes the Microsoft make routines to build the components. If the build succeeds, you will find the driver, sdma.sys, and the test application, SystemDmaApp.exe, in the binary output directory specified
 for the build environment. You can get the output path from the buildxxx.log file (for example, buildfre_win7_arm.log). If the build fails you can find errors and warnings in the buildxxx.err and buildxxx.wrn respectively, where xxx is either chk or fre depending
 on the build environment.</p>
<h3>Run the sample</h3>
<p>To test this driver, copy the test app, SystemDmaApp.exe, and the driver to the same directory, and run the application. The application will automatically load the driver if it's not already loaded and interact with the driver. When you exit the app, the
 driver will be stopped, unloaded and removed. Because no system DMA controller exists for Windows which uses the advertised DRQ, the sample driver will not proceed any further than failing to acquire a system DMA adapter.</p>
<h3><a id="File_manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File manifest</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
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
<p>SDma.c</p>
</td>
<td>
<p>Source file of the sample driver.</p>
</td>
</tr>
<tr>
<td>
<p>SDma.h</p>
</td>
<td>
<p>Header file for defining ioctls; included by driver and the test application.</p>
</td>
</tr>
<tr>
<td>
<p>Testapp.c</p>
</td>
<td>
<p>Source file of the test application. Contains routines to send one IOCTL to the driver.</p>
</td>
</tr>
<tr>
<td>
<p>Install.c</p>
</td>
<td>
<p>Source file of the test application. Contains routines to load and unload the driver.</p>
</td>
</tr>
<tr>
<td>
<p>SDma.rc</p>
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
</tbody>
</table>
</div>
