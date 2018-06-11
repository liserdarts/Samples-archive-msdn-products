# Toaster - Driver Learning Package (KMDF Version)
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* KMDF
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2011-09-14 12:19:32
## Description

<h3>Toaster - Driver Learning Package (KMDF Version)</h3>
<p>The Toaster sample provides a starting point for Windows&reg; driver development. It contains annotated code to illustrate the functionality of bus driver, function driver, filter drivers for a hypothetical Toaster bus and its devices.</p>
<p>The Windows Developer Preview Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available for the BUILD conference. These downloadable samples are provided as compressed
 ZIP files that contain a Visual Studio Express (BUILD release) solution (SLN) file for the sample, along with the code pages, assets, and metadata necessary to successfully compile and run the sample. For more information on the programming models, platforms,
 languages and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference provided in the Windows Developer Preview documentation available in the BUILD-specific version of the Windows Developer Center. This sample is not the final
 shipping version of the sample, and is provided &ldquo;as-is&rdquo; in order to indicate or demonstrate the early functionality of the programming models and feature APIs for a forthcoming version of Windows. Please provide feedback on this sample.</p>
<p><strong>Description of drivers </strong></p>
<p>This sample contains bus, func, filter and toastmon sub directories. INFs files for each sample driver is provided in the same directory as the source. The steps on how to install the bus driver, function driver, filter and toastmon is given in the readme
 file of the WDM toaster sample.</p>
<p><strong>bus</strong></p>
<p>The job of this driver is to service the TOASTER bus controller, enumerate devices that are plugged in, and perform bus-level power management. The bus driver supports D0 and D3 power states. It also has a WMI interface. This directory contains two subdirectories
 that show two different implementation of toaster bus driver (Busenum.sys).</p>
<p><em>static </em></p>
<p>Static version of the bus driver shows how to enumerate child devices using static child list, one per device, provided by the framework. Static enumeration enables a driver to detect and report the existence of devices during initialization, with a limited
 ability to report subsequent changes to the system's configuration. Bus drivers can use static enumeration if the number and type of devices or functional subunits is predetermined and permanent, and does not depend on the configuration of the system on which
 the driver is running. For example, a sound card's driver might act as a bus driver and create separate physical device objects (PDOs) for each of the card's capabilities, such as MIDI, audio, and joystick.</p>
<p class="proch"><strong>To enumerate a child, the bus driver:</strong></p>
<ol>
<li>Call WdfPdoInitAllocate to obtain a WDFDEVICE_INIT structure. </li><li>Initialize the WDFDEVICE_INIT structure. </li><li>Call WdfDeviceCreate to create a framework device object that represents a PDO.
</li></ol>
<p>&nbsp;</p>
<p>After calling WdfDeviceCreate, the driver calls WdfFdoAddStaticChild to add the child device to the child list. Because drivers should only use static child lists for device configurations that are predetermined and permanent, there is little need for a
 driver to modify a static child list after creating it. If the driver determines that a child device has become inaccessible, the driver can call WdfPdoMarkMissing. (If a child device remains accessible but becomes unresponsive and unusable, the driver should
 set the Failed member of the WDF_DEVICE_STATE structure to WdfTrue and then call WdfDeviceSetDeviceState.)</p>
<p><em>dynamic </em></p>
<p>Dynamic version shows how to enumerate child devices using child list objects. Dynamic enumeration enables a driver to detect and report changes to the number and type of devices that are connected to the system while the system is running.</p>
<p>Bus drivers must use dynamic enumeration if the number or types of devices that are connected to the parent device depend on a system's configuration. Some of these devices might be always connected to the system, and some might be plugged in and unplugged
 while the system is running. For example, the number and type of devices that are plugged into a system's PCI bus are system-dependent, but they are permanent unless a user turns off power, opens the case, and adds or removes a device by using a screwdriver.
 On the other hand, a user can add or remove USB devices by plugging in or unplugging a cable while the system is running.</p>
<p>Each time a bus driver identifies a child device, it must add the child device's description to a child list. Driver can either use framework provided device's default child list by calling WdfFdoGetDefaultChildList, or can create additional child lists,
 for grouping children, by calling WdfChildListCreate. This sample uses the default child list. A child description consists of a required identification description and an optional address description.</p>
<p>Identification Description: An identification description is a structure that contains information that uniquely identifies each device that the driver enumerates. The driver defines this structure, but its first member must be a WDF_CHILD_IDENTIFICATION_DESCRIPTION_HEADER
 structure. Address Description: An address description is a structure that contains information that the driver requires so that it can access the device on its bus, if the information can change while the device is plugged in. The driver defines this structure,
 but its first member must be a WDF_CHILD_ADDRESS_DESCRIPTION_HEADER structure. Address descriptions are optional. This sample does not use address descriptions.</p>
<p>To add children to a child list, the driver calls WdfChildListAddOrUpdateChildDescriptionAsPresent for each child device that it finds. This call informs the framework that a driver has discovered a child device that is connected to a parent device. When
 your driver calls WdfChildListAddOrUpdateChildDescriptionAsPresent, it supplies an identification description and, optionally, an address description.</p>
<p>After the driver calls WdfChildListAddOrUpdateChildDescriptionAsPresent to report a new device, the framework informs the PnP manager that the new device exists. The PnP manager then builds a device stack and driver stack for the new device. As part of this
 process, the framework calls the bus driver's EvtChildListCreateDevice callback function. This callback function must call WdfDeviceCreate to create a PDO for the new device.</p>
<p>To report a child missing, this driver calls WdfChildListUpdateChildDescriptionAsMissing. For further details on dynamic enumeration, please refer to the framework documentation.</p>
<p><em>Func </em></p>
<p>The sub-directories under this one contain the source code of the function driver (Toaster.sys) for standard toaster devices. To be illustrative and useful learning sample for beginners to driver development, the sample has been built from almost nothing
 (simple) to fully functional. You can either manually install the driver (root-enumerations) using devcon.exe (%winddk%\tools\devcon) or bus enumerate the driver using the toaster bus driver. They all share one common header file present in the shared directory.</p>
<p><strong>Simple</strong></p>
<p>This is a simple form of function driver for toaster device. The driver doesn't handle any PnP and Power events because the framework provides default behavior for those events. This driver has enough support to allow an user application (toast/notify.exe)
 to open the device interface registered by the driver and send read, write or ioctl requests.</p>
<p><strong>Featured</strong></p>
<p>This version shows how to register for PNP and Power events, handle create &amp; close file requests, handle WMI set and query events, fire WMI notification events. By being a power policy owner, it also registers for idle notification so it can put the
 device to low power state when there is no I/O activity.</p>
<p><em>Filter </em></p>
<p>This directory contains source code of a two filter drivers. The Generic sample is a simple passthru filter driver. The SideBand shows how to provide a sideband ioctl interface to an application by using control-device object. This private interface enables
 application to talk to the filter driver directly; bypassing the functional device stack that filter is attached to. The SideBand sample also demonstrates how to implement a collection of device objects if the driver will handle requests for more than one
 device. You can install these filters on an existing toaster device by using the filter.inf.</p>
<p><em>Toastmon </em></p>
<p>The purpose of this sample is to show how to open a device and perform I/O in kernel mode using remote I/O target interfaces. This sample registers interface notification with the PNP manager for toaster interface class by using IoRegisterPlugPlayNotification.
 When a toaster device is plugged in, PnP manager notifies the driver by invoking the callback. In the callback, this sample creates a remote target and opens the device by using the symbolic link provided in the callback data. This sample uses a passive timer
 to demonstrate how to perform asynchronous read and write to the target device. It also shows to respond to the device change notification by registering the EvtIoTargetQueryRemove/EvtIoTargetRemoveCanceled/EvtIoTargetRemoveComplete on the I/O target object.
 The technique demonstrated in this sample is useful if you are writing a driver that talks to another device that your driver is not controlling. You install this driver as a root-enumerate device using the Wdftoastmon.inf. The steps for installing this sample
 is identical to installing the toaster bus driver.</p>
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
