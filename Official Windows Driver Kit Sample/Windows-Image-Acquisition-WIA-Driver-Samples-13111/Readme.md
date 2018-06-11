# Windows Image Acquisition (WIA) Driver Samples
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* Windows Image Acquisition (WIA)
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:25:18
## Description

<div id="mainSection">
<p>The Windows Image Acquisition driver sample set contains samples and test tools for Windows Image Acquisition (WIA), a driver architecture and user interface for acquiring images from still image devices such as scanners.
</p>
<table>
<tbody>
<tr>
<th>Sample</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>PRODUCTION SCANNING WIA 2.0 DRIVER </p>
</td>
<td>
<p>The ProdScan directory contains a sample WIA 2.0 mini-driver. This sample shows how to add Production Scanning features to a WIA 2.0 mini-driver.</p>
</td>
</tr>
<tr>
<td>
<p>EXTENDED WIA 2.0 MONSTER DRIVER </p>
</td>
<td>
<p>The Wiadriverex directory contains a sample WIA 2.0 mini-driver. This sample shows how to write a WIA 2.0 mini-driver that uses the stream-based WIA 2.0 transfer model. It also shows an implementation of a very simple segmentation filter, image processing
 filter, and error handling extension for the WIA 2.0 mini-driver.</p>
</td>
</tr>
</tbody>
</table>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff542835">
Introduction to WIA</a>.</p>
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
<p>You can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe).</p>
<p><b>Building the sample using Visual Studio</b></p>
<dl><dd>
<p>1.Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\wia and open the wia.sln project file.</p>
</dd><dd>
<p>2.Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.</p>
</dd><dd>
<p>3.From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows 8.1 Debug or Windows 8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.</p>
</dd><dd>
<p>4.From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B).</p>
</dd></dl>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. You can use the Visual Studio Command Prompt window for all build configurations.</p>
<p><b>Building the sample using the command line (MSBuild)</b></p>
<dl><dd>
<p>1.Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </p>
</dd><dd>
<p>2.Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called extend.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\extend.vcxproj</b>.</p>
</dd><dd>
<p>3.If the build succeeds, you will find the driver (extend.dll) in the binary output directory corresponding to the target platform, for example src\wia\extend\Windows 8.1 Debug.
</p>
</dd></dl>
<h2>Run the sample</h2>
<p>Run the &quot;copywia.cmd” batch file to gather all of the binaries into a subdirectory named “wiabins”. The WIA driver sample can be installed by using the Add Device icon in the Scanners and Cameras control panel. Use the Have Disk button to point to the wiabins\drivers
 or wiabins\drivers folder. Wiatest.exe (from the WDK Tools\Wia directory), MS Paint, the Scanner and Camera Wizard, or any TWAIN application (through the WIA TWAIN compatibility layer) can be used to test the samples.</p>
</div>
