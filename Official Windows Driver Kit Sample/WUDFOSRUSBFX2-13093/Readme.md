# WUDFOSRUSBFX2
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* UMDF
## Topics
* usb
## IsPublished
* False
## ModifiedDate
* 2011-09-14 12:45:06
## Description

<h3>WUDFOSRUSBFX2</h3>
<p>The OSRUSBFX2 sample demonstrates how to perform bulk and interrupt data transfers to an USB device using User-Mode Driver Framework. This sample is written for OSR USB-FX2 Learning Kit. The specification for the device is at http://www.osronline.com/hardware/OSRFX2_32.pdf.
</p>
<p>Here is the overview of the device: </p>
<ul>
<li>Device is loosely based on the development board supplied with the Cypress EZ-USB FX2 Development Kit (CY3681)
</li><li>Contains 1 interface and 3 endpoints (Interrupt IN, Bulk Out, Bulk IN) </li><li>Firmware supports vendor commands to query or set LED Bar graph display, 7-segment LED display and query toggle switch states
</li><li>Interrupt Endpoint:
<ul>
<li>Sends an 8-bit value that represents the state of the switches </li><li>Sent on startup, resume from suspend, and whenever the switch pack setting changes
</li><li>Firmware does not de-bounce the switch pack </li><li>One switch change can result in multiple bytes being sent </li><li>Bits are in the reverse order of the labels on the pack E.g. bit 0x80 is labeled 1 on the pack
</li></ul>
</li><li>Bulk Endpoints are configured for loopback:
<ul>
<li>Device moves data from IN endpoint to OUT endpoint </li><li>Device does not change the values of the data it receives nor does it internally create any data
</li><li>Endpoints are always double buffered </li><li>Max packet size depends on speed (64 Full speed, 512 High speed) </li></ul>
</li></ul>
<p></p>
<p>The Windows Developer Preview Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available for the BUILD conference. These downloadable samples are provided as compressed
 ZIP files that contain a Visual Studio Express (BUILD release) solution (SLN) file for the sample, along with the code pages, assets, and metadata necessary to successfully compile and run the sample. For more information on the programming models, platforms,
 languages and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference provided in the Windows Developer Preview documentation available in the BUILD-specific version of the Windows Developer Center. This sample is not the final
 shipping version of the sample, and is provided “as-is” in order to indicate or demonstrate the early functionality of the programming models and feature APIs for a forthcoming version of Windows. Please provide feedback on this sample.
</p>
<p><b>Installation</b></p>
<p>To test the sample you will need to setup your test system as described Building and Testing the Samples the UMDF Readme file. You will also need to copy the co-installer for the Kernel-Mode Driver Framework into the directory with your driver binary and
 INF (and include it in the catalog file if you're signing the driver package). This sample driver makes use of WinUSB, which is a KMDF based driver and so requires the KMDF co-installer for proper installation.
</p>
<p>This sample's INF allows you to install the driver on the OSR USB-FX2 Learning Kit board from OSR.</p>
<p class="proch"><b>To install the USB-FX2 driver on Windows XP (SP2 and above), Vista and later operating systems</b></p>
<ol>
<li>Copy the driver binary (WUDFOsrUsbFx2.dll for the final step) and the inf file (WUDFOsrUsbFx2.inf for the final step) to a directory on your test machine (for example c:\usbSample).
</li><li>Copy the required coinstallers for this sample from c:\winddk\&lt;bld&gt;\redist to the same directory (for example c:\skeletonSample)
<ul>
<li>wdf\WUDFUpdate_01009.dll - the UMDF 1.9 coinstaller </li><li>winusb\WinUsbCoinstaller2.dll - the WinUsb coinstaller (required for USB support)
</li><li>wdf\WdfCoinstaller01009.dll - the KMDF 1.9 coinstaller (required by WinUsb) </li></ul>
</li><li>Attach the OSR USB-FX2 device to your computer. </li><li>At the &quot;Welcome to the Found New Hardware Wizard&quot; choose &quot;No, not this time&quot; and click Next.
</li><li>Select &quot;Install from a list or specific location (Advanced)&quot; and click Next. </li><li>Select &quot;Search for the best driver in these locations&quot;. Clear the &quot;Search removable media (floppy, CD-ROM ...)&quot; check box.
</li><li>Select the &quot;Include this location in the search&quot; check box. </li><li>Enter the path to the files (for example: c:\usbSample) under that check box and click Next.
</li><li>Click Finish at 'Completing the Found New Hardware Wizard.'
<p class="note"><b>Note</b>&nbsp;&nbsp;Note: Between steps 8 and 9 you may see a &quot;Please select the best match for your hardware from the list below&quot; page. This happens when there are multiple INFs which match your hardware. If this happens you should select an entry
 with &quot;Microsoft OSR USB Driver&quot; in the 'Description' column and the directory which holds the INF (for example c:\usbSample) in the 'Location' column. If you have multiple versions of an INF in that directory, you may also need to check the 'Version' column
 to find the highest version number. Occasionally you may be told that no newer driver could be found for your device in this case. If that should happen use the alternate installation instructions below.
</p>
</li></ol>
<p class="proch"><b>To install the sample driver for the OSR USB device</b></p>
<ol>
<li>Copy the driver binary (WUDFOsrUsbFx2.dll for the final step) and the inf file (WUDFOsrUsbFx2.inf for the final step) to a directory on your test machine (for example c:\usbSample).
</li><li>Copy the required coinstallers for this sample from c:\winddk\&lt;bld&gt;\redist to the same directory (for example c:\skeletonSample)
<ul>
<li>wdf\WUDFUpdate_01009.dll - the UMDF 1.9 coinstaller </li><li>winusb\WinUsbCoinstaller2.dll - the WinUsb coinstaller (required for USB support)
</li><li>wdf\WdfCoinstaller01009.dll - the KMDF 1.9 coinstaller (required by WinUsb) </li></ul>
</li><li>Attach the OSR USB-FX2 device to your computer. </li><li>At the &quot;Welcome to the Found New Hardware Wizard&quot; click Cancel. </li><li>Change to the directory containing the inf and binaries (for example cd /d c:\usbSample.)
</li><li>Run devcon.exe as follows: devcon.exe update &lt;inf name&gt; &quot;USB\Vid_0547&amp;Pid_1002&quot; (note: the quotes are important because of the &amp;)
</li></ol>
<p>DevCon can be found in the tools directory of your WDK enlistment (for example: d:\winddk\tools\devcon\i386\devcon.exe.)</p>
<p class="proch"><b>To update the sample driver after making any changes</b></p>
<ol>
<li>Increment the version number found in the INF. While this is not strictly necessary, it will help ensure PnP selects your new driver as a better match for the device.
</li><li>Copy the driver binary and the WUDFOsrUsbDriver.inf file to a directory on your test machine (for example c:\usbSample).
</li><li>Follow instructions 4 and 5 above. </li></ol>
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
<p class="note"><b>Note</b>&nbsp;&nbsp;For this sample, the device metadata package and source files appear in the &quot;devicemetadatapackage&quot; folder at the root of the downloaded project directory. Note that these files do not appear in the Visual Studio Solution.</p>
<p></p>
<h3>Run the Sample</h3>