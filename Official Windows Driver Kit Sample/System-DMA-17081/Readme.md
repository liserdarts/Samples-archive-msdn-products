# System DMA
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:50:54
## Description

<div id="mainSection">
<p>This sample demonstrates the usage of V3 System DMA. It shows how a driver could use a system DMA controller supported by Windows to write data to a hardware location using DMA.
</p>
<p>The sample consists of a legacy device driver and a Win32 console mode test application. The test application opens a handle to the device exposed by the driver and makes a DeviceIoControl call to initiate the example system DMA. To understand how the V3
 system DMA calls are invoked please study SDmaWrite() in SDma.c.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample driver is not a PnP driver. This is a minimal driver meant to demonstrate an OS feature. Neither it nor its sample programs are intended for use in a production environment. Rather, they are intended for educational
 purposes and as a skeleton driver.</p>
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
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>Click the Free Build Environment or Checked Build Environment icon under Development Kits program group to set basic environment variables.</p>
<p>Change to the directory containing the sample source code, such as CD Src\General\sdma\wdm.</p>
<p>Run build -ceZ, or use the macro BLD. This command invokes the Microsoft make routines to build the components. If the build succeeds, you will find the driver, sdma.sys, and the test application, SystemDmaApp.exe, in the binary output directory specified
 for the build environment. You can get the output path from the buildxxx.log file (for example, buildfre_win7_arm.log). If the build fails you can find errors and warnings in the buildxxx.err and buildxxx.wrn respectively, where xxx is either chk or fre depending
 on the build environment.</p>
<h2>Run the sample</h2>
<p>To test this driver, copy the test app, SystemDmaApp.exe, and the driver to the same directory, and run the application. The application will automatically load the driver if it's not already loaded and interact with the driver. When you exit the app, the
 driver will be stopped, unloaded and removed. Because no system DMA controller exists for Windows which uses the advertised DRQ, the sample driver will not proceed any further than failing to acquire a system DMA adapter.</p>
</div>
