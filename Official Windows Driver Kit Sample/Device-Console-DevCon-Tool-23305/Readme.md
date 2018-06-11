# Device Console (DevCon) Tool
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Setup
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:14:40
## Description

<div id="mainSection">
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544707">DevCon</a> is a command-line tool that displays detailed information about devices, and lets you search for and manipulate devices from the command line. DevCon enables, disables,
 installs, configures, and removes devices on the local computer and displays detailed information about devices on local and remote computers. DevCon is included in the WDK. The DevCon source code is included in the WDK in the \samples\setup\devcon directory.
</p>
<p>This document explains the DevCon design, and how to use the SetupAPI and device installation functions to enumerate devices and perform device operations in a console application. For a complete description of DevCon features and instructions for using
 them, see the DevCon help file included with the WDK documentation in Driver Development Tools/Tools for Testing Drivers/DevCon.
</p>
<p>DevCon is provided in ready-to-run form in tools\devcon. For usage, refer to the document provided with devcon.exe. DevCon is a command line utility with built-in documentation available by typing &quot;devcon help&quot;.
</p>
<p>These instructions pertain to Windows XP and Windows Server 2003. DevCon was designed for use on Windows 2000, Windows XP, and Windows Server 2003. It will not work on Windows 95, Windows 98, or Windows ME.
</p>
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
<p class="proch"><b>Building the sample using Visual Studio</b></p>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to samples\setup\devcon and open the devcon.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the Visual Studio Professional&nbsp;2012 WDK, you can use the Visual Studio Command
 Prompt window for all build configurations.</p>
<p class="proch"><b>Building the sample using the command line (MSBuild)</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called devcon.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\devcon.vcxproj</b>. </li><li>If the build succeeds, you will find the tools (devcon.exe) in the binary output directory corresponding to the target platform, for example samples\setup\devcon\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<p>Type &quot;devcon find *&quot; to list device instances of all present devices on the local machine.</p>
<p>Type &quot;devcon status @root\rdp_mou\0000&quot; to list status of the terminal server mouse driver.</p>
<p>Type &quot;devcon status *PNP05*&quot; to list status of all COM ports.</p>
<p>How DevCon works:</p>
<p>Running &quot;devcon help&quot; will provide a list of commands along with short descriptions of what each command does. &quot;devcon help &lt;command&gt;&quot; will give more detailed help on that command. The interpretation of each command is done via a dispatch table &quot;DispatchTable&quot;
 that is at the bottom of &quot;cmds.cpp&quot;. Some of the commands make use of a generic device enumerator &quot;EnumerateDevices&quot;. A few of these commands will work when given a remote target computer, and will also work if using the 32-bit devcon on Wow64. A description
 of some of the more interesting functions and the APIs they use follows:</p>
<p>cmdClasses</p>
<p>This command demonstrates the use of SetupDiBuildClassInfoListEx to enumerate all device class GUID's. The function SetupDiClassNameFromGuidEx and SetupDiGetClassDescriptionEx are used to obtain more information about each device class.</p>
<p>cmdListClass</p>
<p>This command demonstrates the use of SetupDiClassGuidsFromNameEx to enumerate one or more class GUID's that match the class name. This command also demonstrates the use of SetupDiGetClassDevsEx to list all the devices for each class GUID.</p>
<p>cmdFind cmdFindAll cmdStatus</p>
<p>A simple use of EnumerateDevices (explained below) to list devices and display different levels of information about each device. Note that all but cmdFindAll use DIGCF_PRESENT to only list information about devices that are currently present. The main functionality
 for these and related devices is done inside FindCallback.</p>
<p>cmdEnable cmdDisable cmdRestart</p>
<p>These commands show how to issue DIF_PROPERTYCHANGE to enable a device, disable a device, or restart a device. The main functionality for each of these commands is done inside ControlCallback. These operations cannot be done on a remote machine or in the
 context of Wow64. CFGMGR32 API's should not be used as they skip class and co-installers.</p>
<p>cmdUpdate</p>
<p>This command shows how to use UpdateDriverForPlugAndPlayDevices to update the driver for all devices to a specific driver. Normally INSTALLFLAG_FORCE would not be specified allowing UpdateDriverForPlugAndPlayDevices to determine if there is a better match
 already known. It's specified in DevCon to allow DevCon to be used more effectively as a debugging/testing tool. This cannot be done on a remote machine or in the context of Wow64.</p>
<p>cmdInstall</p>
<p>A variation of cmdUpdate to install a driver when there is no associated hardware. It creates a new root-enumerated device instance and associates it with a made up hardware ID specified on the command line (which should correspond to a hardware ID in the
 INF). This cannot be done on a remote machine or in the context of Wow64.</p>
<p>cmdRemove</p>
<p>A command to remove devices. Plug &amp; Play devices that are removed will reappear in response to cmdRescan. The main functionality of this command is in RemoveCallback that demonstrates the use of DIF_REMOVE. This cannot be done on a remote machine or
 in the context of Wow64. CFGMGR32 API's should not be used as they skip class and co-installers.</p>
<p>cmdRescan</p>
<p>This command shows the correct way to rescan for all Plug &amp; Play devices that may have previously been removed, or that otherwise require a rescan to detect them.</p>
<p>cmdDPAdd</p>
<p>This command allows you to add a Driver Package to the machine. The main functionality of this command demonstrates the use of SetupCopyOEMInf. Adding a Driver Package to the machine doesn’t mean the drivers are installed on devices, it simply means the
 drivers are available automatically when a new device is plugged in or a existing device is updated.</p>
<p>cmdDPDelete</p>
<p>This command allows you to uninstall a Driver Package from the machine. The main functionality of this command demonstrates the use of SetupUninstallOEMInf. Removing a Driver Package from the machine does not uninstall the drivers associated with a device.
 If you want to accomplish both then use cmdRemove on all the devices using a given Driver Package and then cmdDPDelete to remove the Driver Package itself from the machine. This functionality is not available in Windows 2000 or earlier.</p>
<p>cmdDPEnum</p>
<p>This command allows you to enumerate all of the 3rd party Driver Packages currently installed on the machine and also shows you how to get some common properties from a Driver Package (Provider, Class description, DriverVer date and version).</p>
<p>cmdDPEnumLegacy</p>
<p>This command shows you how to enumerate 3rd party Driver Packages on Windows Server 2003 and earlier operating systems.</p>
<p>Reboot</p>
<p>This function shows how to correctly reboot the machine from a hardware install program. In particular it passes flags to ExitWindowsEx that cause the reboot to be associated with hardware installation. You should never reboot the machine unnecessarily.</p>
<p>EnumerateDevices</p>
<p>Demonstrates the use of SetupDiGetClassDevsEx to enumerate all devices or all present devices, either globally or limited to a specific setup class. Demonstrates the use of SetupDiCreateDeviceInfoListEx to create a blank list associated with a class or not
 (for most cases, a blank list need not be associated with a class). Demonstrates the use of SetupDiOpenDeviceInfo to add a device instance into a device info list. These last two API's are ideal to obtain a DeviceInfoData structure from a device instance and
 machine name when mixing CFGMGR32 API's with SETUPAPI API's. SetupDiGetDeviceInfoListDetail is called to obtain a remote machine handle that may be passed into CFGMGR32 API's. SetupDiEnumDeviceInfo is called to enumerate each and every device that is in the
 device info list (either explicitly added, or determined by the call to SetupDiGetClassDevsEx). The instance ID is obtained by calling CM_Get_Device_ID_Ex, using information in devInfo (obtained from SetupDiEnumerateDeviceInfo) and devInfoListDetail (obtained
 from SetupDiGetDeviceInfoListDetail). GetHwIds is called to obtain a list of hardware and compatible ID's (explained below). Once an interesting device has been determined (typically by checking hardware ID's) then the callback is called to operate on that
 individual device.</p>
<p>GetHwIds</p>
<p>Shows how to get the complete list of hardware ID's or compatible ID's for a device using SetupDiGetDeviceRegistryProperty.</p>
<p>GetDeviceDescription</p>
<p>Shows how to obtain descriptive information about a device. The friendly name is used if it exists, otherwise the device description is used.</p>
<p>DumpDeviceWithInfo</p>
<p>Shows how to obtain an instance ID (or use any CFGMGR32 API) given HDEVINFO (device info list) and PSP_DEVINFO_DATA (device info data).</p>
<p>DumpDeviceStatus</p>
<p>Shows how to interpret the information returned by CM_Get_DevNode_Status_Ex. Refer to cfg.h for information returned by this API.</p>
<p>DumpDeviceResources</p>
<p>Shows how to obtain information about resources used by a device.</p>
<p>DumpDeviceDriverFiles</p>
<p>Provided as a debugging aid, obtains information about the files apparently being used for a device. It uses SetupDiBuildDriverInfoList to obtain information about the driver being used for the specified device. The driver list associated with a device may
 be enumerated by calling SetupDiEnumDriverInfo. In this case, there will be no more than one driver listed. This function proceeds to obtain a list of files that would normally be copied for this driver using DIF_INSTALLDEVICEFILES. SetupScanFileQueue is used
 to enumerate the file queue to display the list of files that are associated with the driver.</p>
<p>DumpDeviceDriverNodes</p>
<p>Provided as a debugging aid, this function determines the list of compatible drivers for a device. It uses SetupDiBuildDriverInfoList to obtain the list of compatible drivers. In this case, all drivers are enumerated, however typically DIF_SELECTBESTCOMPATDRV
 and SetupDiGetSelectedDriver would be used together to find which driver the OS would consider to be the best.</p>
<p>DumpDeviceStack</p>
<p>This function determines class and device upper and lower filters. </p>
</div>
