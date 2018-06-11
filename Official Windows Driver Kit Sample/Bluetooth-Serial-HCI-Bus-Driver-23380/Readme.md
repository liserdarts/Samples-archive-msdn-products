# Bluetooth Serial HCI Bus Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Bluetooth
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:13
## Description

<div id="mainSection">
<p>The purpose of this sample is to demonstrate how to implement a basic bus driver to support the new
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536585">Bluetooth Extensibility transport DDIs</a> over the UART transport. Such a serial bus driver can support a multi-radio device over the UART transport and utilize a common Bluetooth
 HCI packet for communication. The lower edge of this driver interfaces with a UART controller following the Bluetooth SIG’s UART (H4) transport protocol.
</p>
<p><b>Note</b>: This sample driver is generic—i.e. it is not designed for a specific device and allows for a vendor to adopt and enhance it for supporting Bluetooth
</p>
<p>This sample driver, as is, may not properly function for a device until all vendor-specific device requirements (e.g. device initialization) have been incorporated
</p>
<p>It is recommended to use WDK whose version matches the target Windows build version or newer for the development of the serial bus driver.
</p>
<p><b>FILE MANIFEST</b> </p>
<p><b>WDK header file</b> </p>
<p>BthXDDI.h – this has the constants, struct, and IOCTL definitions for the Bluetooth extensibility transport. This header file is included in WDK.</p>
<p><b>Common code section</b> </p>
<p>driver.c – driver initialization</p>
<p>driver.h – common header file for driver.c and includes other header files</p>
<p>Fdo.c – functions for function device object (FDO) and BTHX DDI processing</p>
<p>io.c – functions that perform IO read pump via UART controller</p>
<p>Io.h – header for io.c</p>
<p>pdo.c – PDO (Bluetooth function) enumeration and IOCTL processing</p>
<p>public.h – header to share with application to support Radio On/Off (“Airplane mode”)</p>
<p>Note: The goal is to keep the common code section the same, so the vendor will only need to update those code sections in the device specific directory.</p>
<p><b>Device-specific code section</b> </p>
<p>Debugdef.h – WPP trace GUID; user should use a new GUID (unique per driver)</p>
<p>device.c – device specific functions to implement:</p>
<p>--DeviceInitialize() – to perform UART and Bluetooth device initialization;</p>
<p>--DeviceEnable() – (optional) to bring serial bus device out of disable/reset state.</p>
<p>--DevicePowerOn() – (optional) to power on the device.</p>
<p>--DeviceEnableWakeControl() – (optional) to arm for device wake signal</p>
<p>--DeviceDisableWakeControl() – (optional) to disarm for device wake signal</p>
<p>device.h – header file for device.c</p>
<p>driver.rc – driver version and name</p>
<p>SerialBusWdk.inx – device specific INF file to install this driver. The vendor will need to add the hardware ID to match the “_HID” for the Serial Bus Device (Bluetooth) in the DSDT.asl file. For example, in SerialBusWDK.inx, the hardware ID is “ACPI\&lt;Vendor&gt;_BTH0”
 where “&lt;vendor&gt;” could be a 4 digit vendor name</p>
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
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\bluetooth\serialhcibus and open the serialhcibus.sln project file.
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
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called SerialBusWdk.vcxproj, navigate to the project directory and enter the following MSBuild
 command: <b>msbuild /t:clean /t:build .\SerialBusWdk.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (SerialBusWDK.sys) in the binary output directory corresponding to the target platform, for example src\bluetooth\serialhcibus\Package\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<p><b>INSTALLATION</b> </p>
<p>The vendor can adopt this driver to add device specific functions, e.g. vendor-specific device initialization. After this is completed, the following steps can be followed to perform device installation in order to test the driver:</p>
<p>Copy driver and inf file to a directory on the test system</p>
<p>Perform “Update Driver” on the devnode corresponding to the hardware ID in the DSDT.asl file for this multifunction serial bus driver</p>
<p>Point the installation to the directory to find the INF and sys files</p>
<p>Upon successful installation, you will see “Serial Bus Driver over UART Bus Enumerator&quot; in Device Manager under the Ports device class</p>
<p>Upon successful installation of the serial bus driver, a PDO will be created with a compat ID of “MS_BTHX_BTHMINI.” This will cause PnP to find the match in bth.inf to perform the installation of the Bluetooth core components with its extensibility transport</p>
<p><b>Pre-requisite: DSDT.ASL</b> </p>
<p>A multifunction serial bus device is a peripheral device on a SoC platform. Its configuration is often defined in the DSDT.asl (Differentiated System Description Table) file to reflect its configuration and interconnection with a UART controller and other
 optional dependent controllers, e.g. I2C, GPIO, etc. for additional control.</p>
<p>Here is an (incomplete) sample system bus device section of a DSDT.ASL file for a UART controller and a multifunction serial bus device that supports Bluetooth. For details, please reference the document “Minimum WOA ACPI Requirements.docx” from Microsoft.
</p>
<pre class="syntax"><code>//
// UART where a serial bus device is connected to
//

Device (UAR1)
{
    Name (_HID, &quot;ABCD_UART&quot;)      // where “ABCD” is vendor name;
    // …
 
    Name (_CID, &quot;ACPI\ABCD_UART&quot;)
    Name (_UID, 6)
 
    Method (_CRS, 0x0, NotSerialized) {
        Name (RBUF, ResourceTemplate ()
        {
                     …
        })
        Return (RBUF)
    }
}
 
//
// Multifunction serial bus device to support Bluetooth function
//
Device(BTH0) 
{
    // ACPI enumerate will generate a hardware ID “ACPI\ABCD_BTH0”
    Name (_HID, &quot;ABCD_BTH0&quot;) // where “ABCD” is vendor name;  
        
    Method(_CRS, 0x0, NotSerialized)
    {
        Name (RBUF, ResourceTemplate ()
        {
        // GPIO Descriptor 
        // (optional)
 
        // I2C Serial Bus Descriptor 
        // (optional)
 
        // UART Serial Bus Descriptor
        UARTSerialBus (
      0xC0, // RTS | CTS 
      LittleEndian,
      DataBitsEight,
      StopBitsOne,
      FlowControlHardware,
      115200, // Initial Baudrate
      480, // Rx FIFO size
      32, // Tx FIFO size
      ParityTypeNone,
      &quot;\\_SB.UAR1&quot;, , )
        })
        Return (RBUF)             
    } 
}</code></pre>
<p></p>
<p>RESOURCES</p>
<p><a href="http://msdn.microsoft.com/en-us/windows/hardware/gg463281.aspx">Bus Driver Development Based on KMDF</a>
</p>
</div>
