# Sensor Driver Skeleton Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* UMDF
## Topics
* SensorsAndLocation
## IsPublished
* False
## ModifiedDate
* 2011-09-14 12:26:45
## Description

<h3>Sensor Driver Skeleton Sample</h3>
<p>The Sensor Driver Skeleton Sample demonstrates how to write a minimal sensor driver and shows best practices, including how to use the sensor class extension. You can use this driver as a template from which you can start your own sensor driver projects.</p>
<p>The sensor Skeleton driver is based upon the UMDF Skeleton driver sample. The sample uses Active Template Library (ATL) to provide COM functionality. The Skeleton driver defines and uses the following classes:</p>
<ul>
<li>CMyDriver: Driver.h, Driver.cpp; Provides an implementation for IDriverEntry. The OnDeviceAdd method creates an instance of the CMyDevice class.
</li><li>CMyDevice: Device.h, Device.cpp; Provides an implementation for IPnPCallbackHardware. The OnPrepareHardware method creates and initializes the sensor class extension and creates an instance of the CSensorDdi class. The OnReleaseHardware method uninitializes
 and frees the class extension object. Implements IFileCallbackCleanup::OnCleanupFile, which notifies the sensor class extension when the file handle to the device is closed.
</li><li>CSensorDdi: SensorDdi.h, Defines.h, SensorDdi.cpp; Provides an implementation for the sensor driver callback interface, ISensorDriver. This class emulates accessing a device that includes a temperatue sensor and a GPS sensor. The header file named Defines.h
 contains defined constants that correspond to device property values, such as the friendly name string that displays in Control Panel. You should change these values to contain your own values. You should also change the implementation in the .cpp file to
 handle your device's values. </li><li>CMyQueue: Queue.h, Queue.cpp; Provides an implementation for IQueueCallbackDeviceIoControl. The Initialize method creates an instance of the I/O queue for the device. The OnDeviceIoControl method manages forwarding WPD I/O requests to the sensor class extension.
</li></ul>
<p>&nbsp;</p>
<p>The Windows Developer Preview Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available for the BUILD conference. These downloadable samples are provided as compressed
 ZIP files that contain a Visual Studio Express (BUILD release) solution (SLN) file for the sample, along with the code pages, assets, and metadata necessary to successfully compile and run the sample. For more information on the programming models, platforms,
 languages and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference provided in the Windows Developer Preview documentation available in the BUILD-specific version of the Windows Developer Center. This sample is not the final
 shipping version of the sample, and is provided &ldquo;as-is&rdquo; in order to indicate or demonstrate the early functionality of the programming models and feature APIs for a forthcoming version of Windows. Please provide feedback on this sample.</p>
<p><strong>Customizing the Sample</strong></p>
<p class="proch"><strong>To create a new driver that is based on the sensor Skeleton sample:</strong></p>
<ol>
<li>Copy all the project files to a new directory. </li><li>Rename SensorSkeleton.rc, SensorSkeleton.ctl, SensorSkeleton.idl, SensorSkelton.def, and SensorSkeleton.inx to appropriate names for your driver.
</li><li>Update the sources file as follows:
<ul>
<li>Change the TARGETNAME to the name of the new driver. </li><li>In the SOURCES variable, change SensorSkeleton.rc to match the new RC file name and change SensorSkeleton.idl to match the new IDL file name.
</li><li>In the NTTARGETFILES variable, change the INF file name to match the new name.
</li><li>Change value for DLLDEF to match the new DEF file name. </li></ul>
</li><li>Update the strings in the RC file to match the new driver name. </li><li>In the DEF file, change the LIBRARY value to the new driver library name. This name must match the value you provided for TARGETNAME.
</li><li>In Internal.h:
<ul>
<li>Update the WPP tracing control GUID at WPP_DEFINE_CONTROL_GUID. You can generate a new GUID by using the uuidgen or guidgen tools in the Microsoft Windows SDK. Be sure to match the format for the new GUID to the existing one.
</li><li>Change the SensorsSkeletonDriverTraceControl string to use the name of your driver.
</li><li>Change the driver tracing ID (choose a string that should be unique to your driver) at MYDRIVER_TRACING_ID.
</li></ul>
</li><li>Update the IDL file as follows:
<ul>
<li>Generate a new GUID and change the type library GUID. </li><li>Generate a new GUID and change the driver class ID. You must use this class ID again (in registry format) in later steps.
</li><li>Change the name of the library. </li><li>Change the name of the coclass. </li><li>Change the help strings. </li></ul>
</li><li>In SensorSkeleton.ctl, change the WPP tracing control GUID and tracing ID to match the changes you made in step 6.
</li><li>Update the constant definitions in Defines.h to contain the correct values for your device. Update the code in SensorDdi.cpp to handle the correct values for your device.
</li><li>Update the INX file, as follows:
<ul>
<li>Change the device's hardware ID in the [Microsoft.NT$ARCH$] section. </li><li>Change the binary name from SensorSkeleton.dll to the name for your driver everywhere it appears in the file.
</li><li>Change the DriverCLSID value set in the UMDFSensorSkeleton_Install section to match your new driver's class ID.
</li><li>Update the strings at the bottom of the file to contain descriptions of your new driver.
</li><li>Change the provider name in the [Version] section to match your company, and update the device's class ID to the installation class of your device.
</li><li>(Optional) Replace SensorSkeleton with the name of your driver in all of the INF section names and strings names. For example, you can replace SensorSkeletonDeviceName with MySensorDriverDeviceName.
</li></ul>
</li><li>Change the name of the driver module in DllMain.cpp. </li><li>Update Driver.h as follows:
<ul>
<li>Change public CComCoClass&lt;CMyDriver, &amp;CLSID_SensorsSkeletonDriver&gt; to contain the correct constant for your driver's class. The MIDL compiler generates this constant from the coclass name you provided in the IDL file.
</li><li>Change the first parameter to OBJECT_ENTRY_AUTO to contain the new driver's class name.
</li></ul>
</li><li>Update Driver.cpp to reference the new header file name that is generated by the IDL instead of SensorSkeleton.h.
</li></ol>
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
