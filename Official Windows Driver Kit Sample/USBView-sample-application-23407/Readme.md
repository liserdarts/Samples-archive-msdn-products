# USBView sample application
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* usb
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:43
## Description

<div id="mainSection">
<p>Usbview.exe is a Windows GUI application that allows you to browse all USB controllers and connected USB devices on your system. The left pane in the main application window displays a connection-oriented tree view, and the right pane displays the USB data
 structures pertaining to the selected USB device, such as the Device, Configuration, Interface, and Endpoint Descriptors, as well as the current device configuration.
</p>
<p>This functional application sample demonstrates how a user-mode application can enumerate USB host controllers, USB hubs, and attached USB devices, and query information about the devices from the registry and through USB requests to the devices.
</p>
<p>The IOCTL calls (see the system include file USBIOCTL.H) demonstrated by this sample include:</p>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537236"><b>IOCTL_GET_HCD_DRIVERKEY_NAME</b></a>
</li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537310"><b>IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION</b></a>
</li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537317"><b>IOCTL_USB_GET_NODE_CONNECTION_DRIVERKEY_NAME</b></a>
</li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537319"><b>IOCTL_USB_GET_NODE_CONNECTION_INFORMATION</b></a>
</li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537323"><b>IOCTL_USB_GET_NODE_CONNECTION_NAME</b></a>
</li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537324"><b>IOCTL_USB_GET_NODE_INFORMATION</b></a>
</li><li><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff537326"><b>IOCTL_USB_GET_ROOT_HUB_NAME</b></a>
</li></ul>
<p></p>
<p>For information about USB, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538930">
Universal Serial Bus (USB) Drivers</a>.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt><dt>Windows&nbsp;7 </dt><dt>Windows&nbsp;Vista with SP1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt><dt>Windows Server&nbsp;2008&nbsp;R2 </dt><dt>Windows Server&nbsp;2008 with SP1 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>You can build the sample in two ways: using Visual Studio Ultimate&nbsp;2012 or the command line (<i>MSBuild</i>).</p>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio Ultimate&nbsp;2012 (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<p>The Usbview sample compiles and links in Visual Studio Ultimate&nbsp;2012, producing a single executable binary Usbview.exe.
</p>
<h3><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h3>
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>Resource.h</td>
<td>ID definitions for GUI controls</td>
</tr>
<tr>
<td>Usbdesc.h</td>
<td>USB descriptor type definitions</td>
</tr>
<tr>
<td>Usbview.h</td>
<td>Main header file for this sample</td>
</tr>
<tr>
<td>Vndrlist.h</td>
<td>List of USB Vendor IDs and vendor names</td>
</tr>
<tr>
<td>Debug.c</td>
<td>Assertion routines for the checked build</td>
</tr>
<tr>
<td>Devnode.c</td>
<td>Routines for accessing DevNode information</td>
</tr>
<tr>
<td>Dispaud.c</td>
<td>Routines for displaying USB audio class device information</td>
</tr>
<tr>
<td>Enum.c</td>
<td>Routines for displaying USB device information</td>
</tr>
<tr>
<td>Usbview.c</td>
<td>Entry point and GUI handling routines</td>
</tr>
</tbody>
</table>
<p></p>
<p>The major topics covered in this tour are: </p>
<ul>
<li>GUI handling routines </li><li>Device enumeration routines </li><li>Device information display routines </li></ul>
The file Usbview.c contains the sample application entry point and GUI handling routines. On entry, the main application window is created, which is actually a dialog box as defined in Usbview.rc. The dialog box consists of a split window with a tree view control
 on the left side and an edit control on the right side.
<p></p>
<p>The routine RefreshTree() is called to enumerate USB host controller, hubs, and attached devices and to populate the device tree view control. RefreshTree() calls the routine EnumerateHostControllers() in Enum.c to enumerate USB host controller, hubs, and
 attached devices. After the device tree view control has been populated, USBView_OnNotify() is called when an item is selected in the device tree view control. This calls UpdateEditControl() in Display.c to display information about the selected item in the
 edit control. </p>
<p>The file Enum.c contains the routines that enumerate the USB bus and populate the tree view control. The USB device enumeration and information collection process is the main point of this sample application. The enumeration process starts at EnumerateHostControllers()
 and goes like this: </p>
<ol>
<li>Enumerate Host Controllers and Root Hubs. Host controllers have symbolic link names of the form HCDx, where x starts at 0. Use CreateFile() to open each host controller symbolic link. Create a node in the tree view to represent each host controller. After
 a host controller has been opened, send the host controller an IOCTL_USB_GET_ROOT_HUB_NAME request to get the symbolic link name of the root hub that is part of the host controller.
</li><li>Enumerate Hubs (Root Hubs and External Hubs). Given the name of a hub, use CreateFile() to open the hub. Send the hub an IOCTL_USB_GET_NODE_INFORMATION request to get info about the hub, such as the number of downstream ports. Create a node in the tree
 view to represent each hub. </li><li>Enumerate Downstream Ports. Given a handle to an open hub and the number of downstream ports on the hub, send the hub an IOCTL_USB_GET_NODE_CONNECTION_INFORMATION request for each downstream port of the hub to get info about the device (if any) attached
 to each port. If there is a device attached to a port, send the hub an IOCTL_USB_GET_NODE_CONNECTION_NAME request to get the symbolic link name of the hub attached to the downstream port. If there is a hub attached to the downstream port, recurse to step (2).
 Create a node in the tree view to represent each hub port and attached device. USB configuration and string descriptors are retrieved from attached devices in GetConfigDescriptor() and GetStringDescriptor() by sending an IOCTL_USB_GET_DESCRIPTOR_FROM_NODE_CONNECTION()
 to the hub to which the device is attached. </li></ol>
The file Display.c contains routines that display information about selected devices in the application edit control. Information about the device was collected during the enumeration of the device tree. This information includes USB device, configuration,
 and string descriptors and connection and configuration information that is maintained by the USB stack. The routines in this file simply parse and print the data structures for the device that were collected when it was enumerated. The file Dispaud.c parses
 and prints data structures that are specific to USB audio class devices.
<p></p>
</div>
