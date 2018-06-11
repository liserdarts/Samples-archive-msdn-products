# WUDF Toaster
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* UMDF
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2011-09-14 12:19:22
## Description

<h3>WUDF Toaster</h3>
<p>This is a featured version of the UMDF toaster function driver. This driver enables a user application (toast/notify.exe) to open the device interface that is registered by the driver and send read, write or ioctl requests. This driver sample also shows
 how to register for PnP and Power events, how to set Power policy ownership and handle I/O requests. This is a minimal driver sample meant to demonstrate the usage of the Windows Driver Framework. It is not intended for use in a production environment.</p>
<p>The Windows Developer Preview Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available for the BUILD conference. These downloadable samples are provided as compressed
 ZIP files that contain a Visual Studio Express (BUILD release) solution (SLN) file for the sample, along with the code pages, assets, and metadata necessary to successfully compile and run the sample. For more information on the programming models, platforms,
 languages and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference provided in the Windows Developer Preview documentation available in the BUILD-specific version of the Windows Developer Center. This sample is not the final
 shipping version of the sample, and is provided &ldquo;as-is&rdquo; in order to indicate or demonstrate the early functionality of the programming models and feature APIs for a forthcoming version of Windows. Please provide feedback on this sample.</p>
<p><strong>Installation </strong></p>
<p>Copy the UMDF coinstaller (WUDFUpdate_01009.dll), driver binary and the WUDFToaster.inf file to a floppy disk or a temp folder.</p>
<p class="proch"><strong>To install the Umdf Toaster driver on Windows XP, Windows Server 2003 or Windows Vista</strong></p>
<ol>
<li>Double-click the &lsquo;Add Hardware&rsquo; wizard in Control Panel. </li><li>At the 'Welcome to the Add Hardware Wizard', click &lsquo;Next&rsquo;. </li><li>Select 'Yes, I have already connected the hardware', then click Next. </li><li>Select &lsquo;Add a new hardware device&rsquo; from the list, then click Next.
</li><li>Select &lsquo;Install the hardware that I manually select from a list(Advanced),' and then click next.
</li><li>Select &lsquo;Show All Devices&rsquo;, then click Next. </li><li>Click 'Have Disk', make sure that 'A:\' is in the &quot;Copy manufacturer's files from:&quot; box, and click OK.
</li><li>Click on the &quot;Sample WUDF Toaster Driver&quot; entry, and then click Next. </li><li>At 'The wizard is ready to install your hardware', click Next. </li><li>Click Finish at 'Completing the Add/Remove Hardware Wizard.'
<p>Another quick way to install the UMDF Toaster driver without going through so many mouse clicks would be to use the Installer sample application present in the src\setup\devcon directory. This sample allows you to root-enumerate a driver with one simple
 command. You have choices to run Enum application with &ndash;p option or to install the driver using devcon.exe by the following command: c:\&gt;devcon.exe install WUDFToaster.inf &quot;root\WUDFToaster&quot;</p>
</li></ol>
<p>&nbsp;</p>
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
<p><strong>Building a Driver Using Visual Studio</strong></p>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Win8-Debug and Win32. In previous versions of the WDK, this build configuration would correspond to building a driver using the Windows 8 x86 Checked Build Environment.</p>
<p class="proch"><strong>To select a configuration and build a driver</strong></p>
<ol>
<li>Open the driver project or solution in Visual Studio. </li><li>Right-click the solution in the Solutions Explorer and select Configuration Manager.
</li><li>From the Configuration Manager, select the Active Solution Configuration (for example, Win8-Debug or Win8-Release) and the Active Solution Platform (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click Build Solution (Ctrl&#43;Shift&#43;B). </li></ol>
<p><strong>Building a Driver Using the Command Line (MSBuild)</strong></p>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><strong>To build a driver using the Visual Studio Command Prompt window</strong></p>
<ol>
<li>Open a Visual Studio Command Prompt window. Click Start, point to All Programs, point to Microsoft Visual Studio, point to Visual Studio Tools, and then click Visual Studio Command Prompt. From this window you can use MsBuild.exe to build any Visual Studio
 project by specifying the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the MSbuild command for your target. For example, to perform a clean build of a Visual Studio driver project called MyDriver.vcxproj, navigate to the project directory and enter the following MSBuild command:
 msbuild /t:clean /t:build .\MyDriver.vcxproj. </li></ol>
<h3>Run the Sample</h3>
