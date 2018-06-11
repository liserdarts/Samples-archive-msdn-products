# Toaster
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* UMDF
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:04
## Description

<div id="mainSection">
<p>The Toaster collection is an iterative series of samples that demonstrate fundamental aspects of Windows driver development.
</p>
<p>All the samples are built for a hypothetical computer bus, called the “Toaster Bus”, over which toaster devices can be connected to a PC.</p>
<p>The Toaster sample collection comprises driver projects (.vcxproj files) that are contained in the toaster.sln solution file (in general\toaster\toastdrv).</p>
<p>Understanding the structure and working of all these various samples will provide a basic understanding of the Windows driver model and get you started on driver development. At a high level, these samples are meant to teach you how to write:
</p>
<p></p>
<ul>
<li>Bus driver – one that controls the toaster bus and enumerates devices. </li><li>Function driver – one that talks to a toaster device connected to toaster bus.
</li><li>Filter driver – how to write class &amp; device filters that layer above and below the function driver to enhance or alter the I/O packets.
</li><li>Coinstaller – Used to customize the installation of a specific instance of a device. It can also used to configure device properties through device-manager property page.
</li><li>Classinstaller – Used to customize or provide additional properties to all the devices that plug to toaster bus.
</li><li>Device metadata – Used to display device-specific information to the user in ‘Devices and Printers’ folder on Windows7 and later OSes.
</li><li>How to interact from one driver to another driver (disjoint stack) in PNP friendly way.
</li><li>How to enumerate PnP devices in usermode application, and how to interact with them in pnp friendly way.
</li><li>How to write INF files to install all the various drivers described above. </li><li>How to install custom application along with driver installation. </li></ul>
<p></p>
<p>Some of the sample drivers are written using different technologies (KMDF, UMDF) and placed side-by-side to show the difference in the programming model. The table below describes what’s contained in each subdirectory of the toaster sample package.</p>
<table>
<tbody>
<tr>
<th>Directory </th>
<th>Description </th>
</tr>
<tr>
<td>
<p>toastDrv\kmdf</p>
</td>
<td>
<p>This folder contains sample drivers written using Kernel mode driver framework. Please see the
<a href="#kmdf_toaster">KMDF Toaster Overview</a> to learn more about the drivers under this folder.
</p>
</td>
</tr>
<tr>
<td>
<p>toastDrv\umdf </p>
</td>
<td>
<p>This folder contains sample drivers written using User mode driver framework. There is no bus driver sample in UMDF because UMDF doesn’t support writing bus drivers. Please read the Windows Driver Foundation book to learn the difference between KMDF and
 UMDF and how to decide which framework to use. Please see the <a href="#umdf_toaster">
UMDF Toaster Overview</a> to learn more about the drivers under this folder. </p>
</td>
</tr>
<tr>
<td>
<p>toastDrv\classinstaller</p>
</td>
<td>
<p>This folder contains sample code that demonstrates how to write a class installer DLL. This DLL provides a custom icon for “Toaster” class and provides a customer property sheet in the device manager to change the friendly name of the device. This DLL is
 referred in the INF file used to install driver for toaster device. </p>
</td>
</tr>
<tr>
<td>
<p>toastDrv\coinstaller </p>
</td>
<td>
<p>This folder contains sample code that demonstrates how to write a coinstaller DLL. This DLL shows how to create a friendly name based on the instance number of the device and also how parse custom section in the INF. This DLL is referred in the INF file
 used to install driver for toaster device. </p>
</td>
</tr>
<tr>
<td>
<p>devicemetadatapackage </p>
</td>
<td>
<p>The sample files under this folder show how to create a device metadata package for Toaster devices.</p>
<p>Device metadata contains the information that describes the device, including the following:</p>
<p></p>
<ul>
<li>The vendor name. </li><li>The model name and description of the device. </li><li>One or more device categories. </li></ul>
<p></p>
<p>A device metadata package consists of multiple XML documents, with each document specifying various components of the device’s attributes. Starting with Windows&nbsp;7, the Devices and Printers folder uses these XML documents to display device-specific information
 to the user. Through these XML documents, the vendor can customize how this information appears, as well as the specific information that is included.</p>
<p>This sample has the following:</p>
<p></p>
<ul>
<li>The source XML metadata files and device icon file for the English and Japanese locales.
</li><li>The compiled device metadata package for both the English and Japanese locales.
</li></ul>
<p></p>
<p>For more information on compiling metadata packages, refer to the “Building Device Metadata Packages” section in the WDK.</p>
<p><b>To install the device metadata package on Windows&nbsp;7:</b> </p>
<p></p>
<ol>
<li>Copy the device metadata package from the floppy drive or temporary file in the target system.
</li><li>If the target system is in the Japanese locale, paste it into %programdata%\microsoft\windows\devicemetadatastore\ja-jp.
</li><li>If the target system is not in the Japanese locale, paste it into %programdata%\microsoft\windows\devicemetadatastore\en-us.
</li><li>Note that this action requires administrator privilege. Elevate the privilege when you are prompted by entering the administrator’s user name and password.
</li></ol>
<p></p>
<p>After installing a toaster device, open <b>Devices and Printers</b>. You will see the sample toaster icon and sample information for the toaster sample.
</p>
<p>More information on how to install device metadata packages is introduced in the device metadata package section in the WDK documentation.
</p>
</td>
</tr>
<tr>
<td>
<p>toastDrv\Exe </p>
</td>
<td>
<p>Executable files in this folder are used to interact with the toaster bus driver and function driver. Because the KMDF and UMDF toaster versions are functionally equivalent and expose the same interfaces, these applications work with both samples.</p>
<p>This directory contains three subdirectories:</p>
<p></p>
<ul>
<li>Enum.exe is a user-mode enumerator, a simple console application. Because the toaster bus is not a physical bus, you can use this application to cause the bus driver to plug in, unplug, and eject devices from the system. Type Enum.exe to see the usage.
</li><li>Toast.exe: This is a user-mode console application to control the toaster. This application enumerates toaster devices, opens the last enumerated device, and sends a read request to it.
</li><li>Notify.exe: This GUI application combines the functionality of Enum.exe and toast.exe and also shows how to handle PnP notification in user mode. You can install the coinstaller for the toaster device by using toastco.inf to see a meaningful display of
 PnP notification. You can also use Notify.exe to specify some other HW ID (instead of the default toaster device id) and cause other drivers to be loaded as a function driver.
</li></ul>
<p></p>
</td>
</tr>
<tr>
<td>
<p>ToastPkg – Toaster Installation Package </p>
</td>
<td>
<p>The Toaster Installation Package comprises driver projects (.vcxproj files) that are contained in the toastpkg.sln solution file (in general/toaster/toastpkg).</p>
<p>Please see Toastpkg for more information on this sample. </p>
</td>
</tr>
</tbody>
</table>
<h3>Related technologies</h3>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544396">Kernel-Mode Driver Framework</a> ,
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
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp; </p>
<p class="note">You can obtain redistributable framework updates by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed. You can verify that the redistributables have been
 installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<p></p>
<p></p>
<h3><a id="Installing_the_sample"></a><a id="installing_the_sample"></a><a id="INSTALLING_THE_SAMPLE"></a>Installing the sample</h3>
<p>In Visual Studio, you can press F5 to build the sample and then deploy it to a target machine. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh454834">Deploying a Driver to a Test Computer</a>.</p>
<h3><a id="kmdf_toaster"></a><a id="KMDF_TOASTER"></a>KMDF Toaster Overview</h3>
<p>The Toaster sample provides a starting point for Windows driver development. It contains annotated code to illustrate the functionality of a KMDF-based bus driver, function driver, and filter driver for a hypothetical Toaster bus and its devices.</p>
<p>The general\toaster\toastDrv\kmdf directory contains bus, func, filter, and toastmon subdirectories.
</p>
<dl><dt><b><i>bus </i></b></dt><dd>
<p>The job of this driver is to service the TOASTER bus controller, enumerate devices that are plugged in, and perform bus-level power management. The bus driver supports D0 and D3 power states. It also has a WMI interface. This directory contains two subdirectories
 that show two different implementation of the Toaster bus driver (Busenum.sys).</p>
<p><b>static</b> </p>
<p>The static version of the bus driver shows how to enumerate child devices using static child list, one per device, provided by the framework.
</p>
<p><i>Static enumeration</i> enables a driver to detect and report the existence of devices during initialization, with a limited ability to report subsequent changes to the system's configuration.
</p>
<p>Bus drivers can use static enumeration if the number and type of devices or functional subunits is predetermined and permanent, and does not depend on the configuration of the system on which the driver is running.</p>
<p>For example, a sound card's driver might act as a bus driver and create separate physical device objects (PDOs) for each of the card's capabilities, such as MIDI, audio, and joystick.
</p>
<p>To enumerate a child, the bus driver:</p>
<ol>
<li>
<p>Calls <b><u>WdfPdoInitAllocate</u></b> to obtain a <b>WDFDEVICE_INIT</b> structure.</p>
</li><li>
<p>Initializes the <b>WDFDEVICE_INIT</b> structure.</p>
</li><li>
<p>Call <b><u>WdfDeviceCreate</u></b> to create a framework device object that represents a PDO.</p>
</li></ol>
<p>After calling <b>WdfDeviceCreate</b>, the driver calls <b><u>WdfFdoAddStaticChild</u></b> to add the child device to the child list.</p>
<p>Because drivers should only use static child lists for device configurations that are predetermined and permanent, there is little need for a driver to modify a static child list after creating it. If the driver determines that a child device has become
 inaccessible, the driver can call <b><u>WdfPdoMarkMissing</u></b>. (If a child device remains accessible but becomes unresponsive and unusable, the driver should set the
<b>Failed</b> member of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff551284">
<b>WDF_DEVICE_STATE</b></a> structure to <b>WdfTrue</b> and then call <b>WdfDeviceSetDeviceState</b>.)</p>
<p>In order to statically enumerate child devices every time the bus driver starts, you can set a registry value in the Toaster Bus driver's device parameter key.</p>
<p><b>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\Root\SYSTEM\&lt;InstanceNumber&gt;\Device Parameters</b>
</p>
<p><b>NumberOfToasters: REG_DWORD: 2 </b></p>
<p>The maximum number of child devices that can be enumerated using this registry setting is 10. You can also configure this value through the Toaster Bus Inf file.</p>
<p><b>dynamic</b> </p>
<p>The dynamic version of the bus driver shows how to enumerate child devices using child list objects.
</p>
<p><i>Dynamic enumeration</i> enables a driver to detect and report changes to the number and type of devices that are connected to the system while the system is running.</p>
<p>Bus drivers must use dynamic enumeration if the number or types of devices that are connected to the parent device depend on a system's configuration. Some of these devices might be always connected to the system, and some might be plugged in and unplugged
 while the system is running.</p>
<p>For example, the number and type of devices that are plugged into a system's PCI bus are system-dependent, but they are permanent unless a user turns off power, opens the case, and adds or removes a device by using a screwdriver. On the other hand, a user
 can add or remove USB devices by plugging in or unplugging a cable while the system is running.</p>
<p>Each time a bus driver identifies a child device, it must add the child device's description to a child list. Driver can either use framework provided device's default child list by calling
<b><u>WdfFdoGetDefaultChildList</u></b>, or can create additional child lists, for grouping children, by calling
<b><u>WdfChildListCreate</u></b>. This sample uses the default child list. A <i>child description</i> consists of a required
<i>identification description</i> and an optional <i>address description</i>.</p>
<p><i>Identification Description</i>: An identification description is a structure that contains information that uniquely identifies each device that the driver enumerates. The driver defines this structure, but its first member must be a
<u>WDF_CHILD_IDENTIFICATION_DESCRIPTION_HEADER</u> structure.</p>
<p><i>Address Description</i>: An address description is a structure that contains information that the driver requires so that it can access the device on its bus, if the information can change while the device is plugged in. The driver defines this structure,
 but its first member must be a <u>WDF_CHILD_ADDRESS_DESCRIPTION_HEADER</u> structure. Address descriptions are optional. This sample does not use address descriptions.</p>
<p>To add children to a child list, the driver calls <b><u>WdfChildListAddOrUpdateChildDescriptionAsPresent</u></b> for each child device that it finds. This call informs the framework that a driver has discovered a child device that is connected to a parent
 device. When your driver calls <b>WdfChildListAddOrUpdateChildDescriptionAsPresent</b>, it supplies an identification description and, optionally, an address description.</p>
<p>After the driver calls <b>WdfChildListAddOrUpdateChildDescriptionAsPresent</b> to report a new device, the framework informs the PnP manager that the new device exists. The PnP manager then builds a device stack and driver stack for the new device. As part
 of this process, the framework calls the bus driver's <i><u>EvtChildListCreateDevice</u></i> callback function. This callback function must call
<b><u>WdfDeviceCreate</u></b> to create a PDO for the new device.</p>
<p>To report a child device missing, this driver calls WdfChildListUpdateChildDescriptionAsMissing. For further details on dynmaic enumeration, please refer to the framework documentation.</p>
</dd><dt><b><i>Func</i> </b></dt><dd>
<p>The sub-directories under this one contain the source code of the function driver (Toaster.sys) for standard toaster devices. To be illustrative and useful learning sample for beginners to driver development, the sample has been built from almost nothing
 (simple) to fully functional. You can either manually install the driver (root enumerate) using devcon.exe (%winddk%\tools\devcon) or bus enumerate the driver using the toaster bus driver. They all share one common header file present in the
<i>shared</i> directory.</p>
<p><b>Simple</b> </p>
<p>This is a simple form of function driver for toaster device. The driver doesn't handle any PnP and Power events because the framework provides default behavior for those events. This driver has enough support to allow an user application (toast/notify.exe)
 to open the device interface registered by the driver and send read, write or ioctl requests.</p>
<p><b>Featured</b> </p>
<p>This version shows how to register for PNP and Power events, handle create &amp; close file requests, handle WMI set and query events, and fire WMI notification events. By being a power policy owner, it also registers for idle notification so it can put
 the device to low power state when there is no I/O activity.</p>
</dd><dt><b><i>Filter</i> </b></dt><dd>
<p>This directory contains source code of a two filter drivers. The Generic sample is a simple passthru filter driver. The SideBand shows how to provide a sideband ioctl interface to an application by using control-device object. This private interface enables
 application to talk to the filter driver directly; bypassing the functional device stack that filter is attached to. The SideBand sample also demonstrates how to implement a collection of device objects if the driver will handle requests for more than one
 device. You can install these filters on an existing toaster device by using the filter.inf.</p>
</dd><dt><b><i>Toastmon</i> </b></dt><dd>
<p>The purpose of this sample is to show how to open a device and perform I/O in kernel mode using remote I/O target interfaces. This sample registers interface notification with the PNP manager for toaster interface class by using IoRegisterPlugPlayNotification.
 When a toaster device is plugged in, PnP manager notifies the driver by invoking the callback. In the callback, this sample creates a remote target and opens the device by using the symbolic link provided in the callback data. This sample uses a passive timer
 to demonstrate how to perform asynchronous read and write to the target device. It also shows to respond to the device change notification by registering the EvtIoTargetQueryRemove/EvtIoTargetRemoveCanceled/EvtIoTargetRemoveComplete on the I/O target object.
 The technique demonstrated in this sample is useful if you are writing a driver that talks to another device that your driver is not controlling. Even though this sample is based on the WDM toastmon, it has been enhanced to show some additional features of
 the framework. You install this driver as a root-enumerate device using the Wdftoastmon.inf. The steps for installing this sample is identical to installing the toaster bus driver.
</p>
</dd></dl>
<h3><a id="umdf_toaster"></a><a id="UMDF_TOASTER"></a>UMDF Toaster Overview</h3>
<p>This is a featured version of the UMDF toaster function driver. This driver enables a user application (toast/notify.exe) to open the device interface that is registered by the driver and send read, write or ioctl requests. This driver sample also shows
 how to register for PnP and Power events, how to set Power policy ownership and handle I/O requests. This is a minimal driver sample meant to demonstrate the usage of the Windows Driver Framework. It is not intended for use in a production environment.
</p>
<p>You can use the WUDF Toaster to leverage the existing KMDF Toastmon to demonstrate a kernel-mode client access to a user-mode driver by using remote I/O targets.
</p>
<p>To do so, add the following line to the .WDF section of the INF for this UMDF driver:
<b>UmdfKernelModeClientPolicy = AllowKernelModeClients</b></p>
<h3><a id="Testing_UMDF_Toaster"></a><a id="testing_umdf_toaster"></a><a id="TESTING_UMDF_TOASTER"></a>Testing UMDF Toaster</h3>
<ol>
<li>Use Toast.exe, Notify.exe or Enum.exe applications – their functionality must be the same as with KMDF Toaster sample.
</li><li>Install KMDF Toastmon driver. Allow kernel mode clients to user mode drivers as described in the advanced section. Install WUDF Toaster driver. Use a Traceview.exe to see the requests sent from Toastmon to the UMDF Toaster.
</li></ol>
<h3><a id="_______UMDF_Toaster_File_Manifest"></a><a id="_______umdf_toaster_file_manifest"></a><a id="_______UMDF_TOASTER_FILE_MANIFEST"></a>UMDF Toaster File Manifest</h3>
<table>
<tbody>
<tr>
<th>File </th>
<th>Description </th>
</tr>
<tr>
<td>
<p>WUDFToaster.idl </p>
</td>
<td>
<p>Component Interface file </p>
</td>
</tr>
<tr>
<td>
<p>WUDFToaster.cpp </p>
</td>
<td>
<p>DLL Support code - provides the DLL's entry point as well as the DllGetClassObject export.
</p>
</td>
</tr>
<tr>
<td>
<p>WUDFToaster.def </p>
</td>
<td>
<p>This file lists the functions that the driver DLL exports. </p>
</td>
</tr>
<tr>
<td>
<p>stdafx.h </p>
</td>
<td>
<p>This is the main header file for the sample driver. </p>
</td>
</tr>
<tr>
<td>
<p>driver.cpp &amp; driver.h </p>
</td>
<td>
<p>Definition and implementation of the IDriverEntry callbacks in CDriver class.</p>
</td>
</tr>
<tr>
<td>
<p>device.cpp &amp; device.h </p>
</td>
<td>
<p>Definition and implementation of various interfaces and their callbacks in CDevice class. Add your PnP and Power interfaces specific for your hardware.
</p>
</td>
</tr>
<tr>
<td>
<p>queue.cpp &amp; queue.h </p>
</td>
<td>
<p>Definition and implementation of the base queue callback class (CQueue). IQueueCallbackDevicekIoControl, IQueueCallbackRead and IQueueCallBackWrite callbacks are implemented to handle I/O control requests.
</p>
</td>
</tr>
<tr>
<td>
<p>WUDFToaster.rc </p>
</td>
<td>
<p>This file defines resource information for the WUDF Toaster sample driver. </p>
</td>
</tr>
<tr>
<td>
<p>WUDFToaster.inf </p>
</td>
<td>
<p>Sample INF for installing the sample WUDF Toaster driver under the Toaster class of devices.
</p>
</td>
</tr>
<tr>
<td>
<p>WUDFtoaster.ctl, internal.h </p>
</td>
<td>
<p>This file lists the WPP trace control GUID(s) for the sample driver. This file can be used with the tracelog command's -guid flag to enable the collection of these trace events within an established trace session.
</p>
<p>These GUIDs must remain in sync with the trace control guids defined in internal.h.</p>
</td>
</tr>
</tbody>
</table>
<h3><a id="umdf_toastmon"></a><a id="UMDF_TOASTMON"></a>UMDF Toastmon Overview</h3>
<p>This sample is a UMDF version of the KMDF ToastMon sample. For information about the KMDF Toastmon sample, see
<a href="#kmdf_toaster">KMDF Toaster</a>.</p>
<p>UMDF Toastmon demonstrates how to use UMDF to write a minimal driver with the User-Mode Driver Framework and shows best practices. The driver will successfully load on a device (either root enumerated or a real hardware device) but has the minimum PnP functionality
 and does not support receiving any I/O operations.</p>
<p>Toastmon is intended to serve as a learning tool for other UMDF drivers that you may write.
</p>
<h3><a id="Toastmon_File_Manifest"></a><a id="toastmon_file_manifest"></a><a id="TOASTMON_FILE_MANIFEST"></a>Toastmon File Manifest</h3>
<table>
<tbody>
<tr>
<th>File </th>
<th>Description </th>
</tr>
<tr>
<td>
<p>comsup.cpp &amp; comsup.h</p>
</td>
<td>
<p>Boilerplate COM Support code - specifically base classes which provide implementations for the standard COM interfaces IUnknown and IClassFactory which are used throughout the sample.</p>
<p>The implementation of IClassFactory is designed to create instances of the CMyDriver class. If you should change the name of your base driver class, you would also need to modify this file.
</p>
</td>
</tr>
<tr>
<td>
<p>dllsup.cpp</p>
</td>
<td>
<p>Boilerplate DLL Support code - provides the DLL's entry point as well as the single required export (DllGetClassObject).
</p>
<p>These depend on comsup.cpp to perform the necessary class creation. </p>
</td>
</tr>
<tr>
<td>
<p>exports.def</p>
</td>
<td>
<p>This file lists the functions that the driver DLL exports. </p>
</td>
</tr>
<tr>
<td>
<p>makefile</p>
</td>
<td>
<p>This file redirects to the real makefile, which is shared by all the driver components of the Windows Driver Kit.</p>
</td>
</tr>
<tr>
<td>
<p>internal.h</p>
</td>
<td>
<p>This is the main header file for the ToastMon driver</p>
</td>
</tr>
<tr>
<td>
<p>driver.cpp &amp; driver.h</p>
</td>
<td>
<p>Definition and implementation of the driver callback class for the ToastMon sample.
</p>
</td>
</tr>
<tr>
<td>
<p>device.cpp &amp; device.h</p>
</td>
<td>
<p>Definition and implementation of the device callback class for the ToastMon sample. This is mostly boilerplate, but also registers for RemoteInterface Arrival notifications. When a RemoteInterface arrival callback occurs, it calls CreateRemoteInterface and
 creates a CMyRemoteTarget callback object to handle I/O on that RemoteInterface.</p>
</td>
</tr>
<tr>
<td>
<p>RemoteTarget.cpp &amp; RemoteTarget.h</p>
</td>
<td>
<p>Definition and implementation of the remote target callback class for the ToastMon sample.</p>
</td>
</tr>
<tr>
<td>
<p>list.h</p>
</td>
<td>
<p>Doubly-linked-list code</p>
</td>
</tr>
<tr>
<td>
<p>ToastMon.rc</p>
</td>
<td>
<p>This file defines resource information for the ToastMon sample driver.</p>
</td>
</tr>
<tr>
<td>
<p>UMDFToastMon.inf</p>
</td>
<td>
<p>Sample INF for installing the Skeleton driver to control a root enumerated device with a hardware ID of UMDFSamples\ToastMon</p>
</td>
</tr>
</tbody>
</table>
</div>
