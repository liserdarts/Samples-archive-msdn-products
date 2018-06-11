# Windows Image Acquisition (WIA) Driver Samples
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* Windows Image Acquisition (WIA)
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:23:14
## Description

<div id="mainSection">
<p>The Windows Image Acquisition driver sample set contains samples and test tools for Windows Image Acquisition (WIA), a driver architecture and user interface for acquiring images from still image devices such as scanners and digital still cameras.
</p>
<table>
<tbody>
<tr>
<th>Sample</th>
<th>Description</th>
</tr>
<tr>
<td>
<p>FLATBED SCANNER WIA 1.0 MINI-DRIVER</p>
</td>
<td>
<p>The Wiascanr directory contains a sample WIA 1.0 mini-driver for a flatbed scanner with a document feeder. See the documentation file Wiascanr.htm in the Wiascanr directory for more information.
</p>
</td>
</tr>
<tr>
<td>
<p>SCANNER WIA 1.0 MICRO-DRIVER </p>
</td>
<td>
<p>The Microdrv directory contains a sample WIA 1.0 micro-driver for a scanner. A micro-driver is much easier to develop than a full WIA mini-driver but provides only basic functionality. See the documentation file Testmcro.htm in the Microdrv directory for
 more information.</p>
</td>
</tr>
<tr>
<td>
<p>DIGITAL STILL CAMERA WIA 1.0 MINI-DRIVER </p>
</td>
<td>
<p>The Wiacam and Microcam directories contain a sample WIA 1.0 mini-driver for a digital still camera and the Extend directory contains a sample WIA 1.0 user interface extension. See the documentation file Wiacam.htm in the Wiacam directory, Fakecam.htm in
 the Microcam directory, and Extend.htm in the Extend directory for more information.
</p>
</td>
</tr>
<tr>
<td>
<p>EXTENDED WIA 2.0 MONSTER DRIVER </p>
</td>
<td>
<p>The Wiadriverex directory contains a sample WIA 2.0 mini-driver. This sample shows how to write a WIA 2.0 mini-driver that uses the stream-based WIA 2.0 transfer model. It also shows an implementation of a very simple segmentation filter, image processing
 filter, and error handling extension for the WIA 2.0 mini-driver. See the documentation file Readme.htm in the Wiadriverex directory for more information.
</p>
</td>
</tr>
<tr>
<td>
<p>PRODUCTION SCANNING WIA 2.0 DRIVER </p>
</td>
<td>
<p>The ProdScan directory contains a sample WIA 2.0 mini-driver. This sample shows how to add Production Scanning features to a WIA 2.0 mini-driver. See the documentation file Readme.mht in the ProdScan directory for more information</p>
</td>
</tr>
</tbody>
</table>
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
<p>Starting in the Visual Studio Professional&nbsp;2012 WDK, you can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine
 (MSBuild.exe).</p>
<h4><a id="Building_the_sample_using_Visual_Studio"></a><a id="building_the_sample_using_visual_studio"></a><a id="BUILDING_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Building the sample using Visual Studio</h4>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\wia and open the wia.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the Visual Studio Professional&nbsp;2012 WDK, you can use the Visual Studio Command
 Prompt window for all build configurations.</p>
<h4><a id="Building_the_sample_using_the_command_line__MSBuild_"></a><a id="building_the_sample_using_the_command_line__msbuild_"></a><a id="BUILDING_THE_SAMPLE_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building the sample using the command line (MSBuild)</h4>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called extend.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\extend.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (extend.dll) in the binary output directory corresponding to the target platform, for example src\wia\extend\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<p>Run the &quot;copywia.cmd” batch file to gather all of the binaries into a subdirectory named “wiabins”. The camera and scanner samples can be installed by using the Add Device icon in the Scanners and Cameras control panel. Use the Have Disk button to point
 to the wiabins\drivers or wiabins\drivers folder. Wiatest.exe (from the WDK Tools\Wia directory), MS Paint, the Scanner and Camera Wizard, or any TWAIN application (through the WIA TWAIN compatibility layer) can be used to test the samples.
</p>
</div>
