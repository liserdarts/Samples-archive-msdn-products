# USBView sample application
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* usb
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:51:50
## Description

<div id="mainSection">
<p>Usbview.exe is a Windows GUI application that allows you to browse all USB controllers and connected USB devices on your system. The left pane in the main application window displays a connection-oriented tree view, and the right pane displays the USB data
 structures pertaining to the selected USB device, such as the Device, Configuration, Interface, and Endpoint Descriptors, as well as the current device configuration.
</p>
<p class="note"><b>Important</b>&nbsp;&nbsp;If you need UsbView as a tool, do not download this sample. Instead get UsbView.exe from the
<a href="http://go.microsoft.com/fwlink/p?linkid=391063">Windows Driver Kit (WDK)</a> in the Windows Kits\<i>&lt;version&gt;</i>\Tools\<i>&lt;arch&gt;</i> folder. If you need to see the source code for UsbView, open the
<b>Browse code</b> tab.</p>
<p></p>
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
<td><dt>Windows&nbsp;8.1 </dt><dt>Windows&nbsp;8 </dt><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt><dt>Windows Server&nbsp;2012 </dt><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>You can build the sample in two ways: using Visual Studio&nbsp;2013 or the command line (<i>MSBuild</i>).</p>
<h2><a id="Download_and_extract_the_sample"></a><a id="download_and_extract_the_sample"></a><a id="DOWNLOAD_AND_EXTRACT_THE_SAMPLE"></a>Download and extract the sample</h2>
<p>Click the download button on this page. Click <b>Save</b>, and then click <b>Open Folder</b>. Right click Usbview sample application.zip, and choose
<b>Extract All</b>. Specify or browse to a folder for the extracted files. For example, you could extract to c:\Usbview.</p>
<h2><a id="Open_the_driver_solution_in_Visual_Studio"></a><a id="open_the_driver_solution_in_visual_studio"></a><a id="OPEN_THE_DRIVER_SOLUTION_IN_VISUAL_STUDIO"></a>Open the driver solution in Visual Studio</h2>
<p>Navigate to the folder that has the extracted sample. Double click the solution file, usbview.sln. In Microsoft Visual Studio, locate Solution Explorer. (If this is not already open, choose
<b>Solution Explorer</b> from the <b>View</b> menu.) In Solution Explorer, you can see one solution that has one project. There is an application project named
<b>usbview</b>.</p>
<h2><a id="Set_the_configuration_and_platform_in_Visual_Studio"></a><a id="set_the_configuration_and_platform_in_visual_studio"></a><a id="SET_THE_CONFIGURATION_AND_PLATFORM_IN_VISUAL_STUDIO"></a>Set the configuration and platform in Visual Studio</h2>
<p>In Visual Studio, in Solution Explorer, right click <b>Solution ‘usbview’ (1 project)</b>, and choose
<b>Configuration Manager</b>. Set the configuration and the platform. Do not check the
<b>Deploy</b> boxes. Here are some examples of configuration and platform settings.</p>
<table>
<tbody>
<tr>
<th>Configuration</th>
<th>Platform</th>
<th>Description</th>
</tr>
<tr>
<td>Win8.1 Debug</td>
<td>x64</td>
<td>The app will run on an x64 hardware platform that is running Windows&nbsp;8.1. The app will not run on any earlier versions of Windows.</td>
</tr>
<tr>
<td>Win7 Debug</td>
<td>x64</td>
<td>The app will run on an x64 hardware platform that is running Windows&nbsp;7 or a later version of Windows.</td>
</tr>
</tbody>
</table>
<h2><a id="Build_the_sample_using_Visual_Studio"></a><a id="build_the_sample_using_visual_studio"></a><a id="BUILD_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Build the sample using Visual Studio</h2>
<p>In Visual Studio, on the <b>Build</b> menu, choose <b>Build Solution</b>.</p>
<h2><a id="Locate_the_built_application_executable_package"></a><a id="locate_the_built_application_executable_package"></a><a id="LOCATE_THE_BUILT_APPLICATION_EXECUTABLE_PACKAGE"></a>Locate the built application executable package</h2>
<p>In File Explorer, navigate to the folder that contains your built app (usbview.exe). The location of this folder varies depending on what you set for configuration and platform. For example, if your settings are Win7 Debug and x64, the package is in your
 solution folder under x64\Win7Debug.</p>
<h2><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h2>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h2>Run the sample</h2>
<h2><a id="Local_debugging"></a><a id="local_debugging"></a><a id="LOCAL_DEBUGGING"></a>Local debugging</h2>
<ol>
<li>Change <b>Debugger</b> to launch to <b>Local Windows Debugger</b>. </li><li>On the <b>Debug</b> menu, select <b>Start debugging</b> or hit <b>F5</b>. </li></ol>
<h2><a id="Manual_deployment_to_a_remote_target_computer"></a><a id="manual_deployment_to_a_remote_target_computer"></a><a id="MANUAL_DEPLOYMENT_TO_A_REMOTE_TARGET_COMPUTER"></a>Manual deployment to a remote target computer</h2>
<p>If you want to debug the sample app on a remote computer, </p>
<ol>
<li>Copy the executable to a folder on the remote computer. </li><li>Specify project properties as per the instructions given in <a href="http://msdn.microsoft.com/en-us/library/8x6by8d2.aspx">
Set Up Remote Debugging for a Visual Studio Project</a>. </li><li>Change <b>Debugger</b> to launch to <b>Remote Windows Debugger</b>. </li><li>On the <b>Debug</b> menu, select <b>Start debugging</b> or hit <b>F5</b>. </li></ol>
<p></p>
<h2><a id="View_a_USB_device_in_Usbview"></a><a id="view_a_usb_device_in_usbview"></a><a id="VIEW_A_USB_DEVICE_IN_USBVIEW"></a>View a USB device in Usbview</h2>
<p></p>
<ol>
<li>Attach a USB device to one of USB ports on the computer that has Usbview running.
</li><li>In the device tree, locate the device. For example the device might be under the Intel(R) ICH10 Family USB Universal Host Controller - 3A34 &gt; Root Hub node.
</li><li>View host controller and port properties on the right pane. </li></ol>
<p></p>
<h2><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h2>
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
