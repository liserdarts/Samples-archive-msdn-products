# KMDF OSRUSBFX2
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* KMDF
## Topics
* usb
## IsPublished
* False
## ModifiedDate
* 2011-09-14 12:42:41
## Description

<h3>KMDF OSRUSBFX2</h3>
<p>The OSRUSBFX2 sample demonstrates how to perform bulk and interrupt data transfers to an USB device using Windows Driver Framework (WDF). This sample is written for OSR USB-FX2 Learning Kit. The specification for the device is at http://www.osronline.com/hardware/OSRFX2_32.pdf.
</p>
<p>The Windows Developer Preview Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available for the BUILD conference. These downloadable samples are provided as compressed
 ZIP files that contain a Visual Studio Express (BUILD release) solution (SLN) file for the sample, along with the code pages, assets, and metadata necessary to successfully compile and run the sample. For more information on the programming models, platforms,
 languages and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference provided in the Windows Developer Preview documentation available in the BUILD-specific version of the Windows Developer Center. This sample is not the final
 shipping version of the sample, and is provided “as-is” in order to indicate or demonstrate the early functionality of the programming models and feature APIs for a forthcoming version of Windows. Please provide feedback on this sample.
</p>
<h3>Operating System Requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows Developer Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server Developer Preview </dt></td>
</tr>
</tbody>
</table>
<h3>Build the Sample</h3>
<p>You can build the sample in two ways: using Visual Studio or the command line (MSBuild).</p>
<p><b>Building a Driver Using Visual Studio</b></p>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).
</p>
<p>The default Solution build configuration is Win8-Debug and Win32. In previous versions of the WDK, this build configuration would correspond to building a driver using the Windows 8 x86 Checked Build Environment.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio. </li><li>Right-click the solution in the Solutions Explorer and select Configuration Manager.
</li><li>From the Configuration Manager, select the Active Solution Configuration (for example, Win8-Debug or Win8-Release) and the Active Solution Platform (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click Build Solution (Ctrl&#43;Shift&#43;B). </li></ol>
<p><b>Building a Driver Using the Command Line (MSBuild)</b></p>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.
</p>
<p class="proch"><b>To build a driver using the Visual Studio Command Prompt window</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window. Click Start, point to All Programs, point to Microsoft Visual Studio, point to Visual Studio Tools, and then click Visual Studio Command Prompt. From this window you can use MsBuild.exe to build any Visual Studio
 project by specifying the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the MSbuild command for your target. For example, to perform a clean build of a Visual Studio driver project called MyDriver.vcxproj, navigate to the project directory and enter the following MSBuild command:
 msbuild /t:clean /t:build .\MyDriver.vcxproj. </li></ol>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;For this sample, the device metadata package and source files appear in the “devicemetadatapackage” folder at the root of the downloaded project directory. Note that these files do not appear in the Visual Studio Solution.</p>
<p></p>
<h3>Run the Sample</h3>
