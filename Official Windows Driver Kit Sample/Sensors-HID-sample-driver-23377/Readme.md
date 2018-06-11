# Sensors HID sample driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* Sensors and Location
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:03
## Description

<div id="mainSection">
<p>The HID sample driver for sensors demonstrates how an OEM or IHV could write a UMDF driver to support sensors that are compliant with the HID Sensor Class Driver specification. This driver is also helpful for IHVs who must design and test firmware for a
 HID-based sensor. </p>
<p>This sample driver is generally identical the HID Sensor Class Driver that is available in Windows&nbsp;8 and provides developers with the following:</p>
<ul>
<li>Source code to help build and compile a working driver for Windows&nbsp;7. </li><li>Source code to help debug the hardware/firmware during development. </li></ul>
<p>There are three instances where you may need to modify the sample driver.</p>
<ol>
<li>The sample does not support your sensor. </li><li>The sample supports your sensor; but, it does not support unique functionality on your particular device.
</li><li>The sample needs to run on an earlier version of Windows. </li></ol>
<p>The sample driver is based on the HID protocol. It supports the <a href="sensors.the_driver_file_list">
eighteen common sensors</a>. In addition, it supports a Custom class that lets you integrate any HID sensor not found in this list.
</p>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="sensors.sensors_hid_driver_sample">Sensors HID driver sample</a> description in the Windows Driver Kit documentation.</p>
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545810">Sensors Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406596">Sensors Programming Guide</a>
</dt></dl>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>None supported </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<ol>
<li>Start the Microsoft Visual Studio&nbsp;2012 development environment. </li><li>Select the build configuration (for example, Win8 Debug) and the architecture (for example Win32).
</li><li>From the File/Open/Project/Solution… menu, navigate to the VcxProj or sln file and load the project
</li><li>From the Build menu, select Build Solution. </li></ol>
<p>If the build succeeds, you will find the driver DLL and INF files in a subdirectory of your project directory. For example, if you built the Debug configuration and Win32 architecture, the DLL and INF files will be placed in projectDirectory\Win8 Debug\x86
 directory.</p>
<h3>Run the sample</h3>
<h3><a id="Installing_the_sample"></a><a id="installing_the_sample"></a><a id="INSTALLING_THE_SAMPLE"></a>Installing the sample</h3>
<p>To install the sensors driver sample:</p>
<ol>
<li>Ensure that the driver builds without errors. </li><li>Enable test signing by running the command “bcdedit /set testsigning on” from an elevated command prompt. (You’ll need to reboot your machine after you enable test signing.)
</li><li>Edit the following line in the sample INF file, replacing the vendor identifier, &quot;VID_C1CA&quot;, with the identifier supplied by your firmware in the USB device descriptor. In addition, replace the product identifier, &quot;PID_0020&quot;, with the identifier supplied
 by your firmware in the USB device descriptor.
<pre class="syntax"><code>%SensorsHIDDriverSample.Collection.DeviceDesc% = SensorsHIDDriverSample_Install,HID\VID_C1CA&amp;PID_0020</code></pre>
</li><li>Copy the DLL and INF files to a separate folder </li><li>Install your HID sensor board. Locate this newly installed board in the Device Manager under Human Interface Devices – it will be a “HID-compliant device.”
</li><li>Right-click on the device and select “Update Driver Software…&quot; </li><li>Select “Browse my computer for driver software” </li><li>Select “Let me pick from a list of device drivers on my computer” </li><li>Select “Have disk…” </li><li>Select “Browse…” </li><li>Navigate to the folder in which the DLL and INF files were placed in step 2. Select the INF and select “Open”
</li><li>Select OK </li><li>Select Next </li><li>If a Windows security prompt appears, select “Install this driver software anyway”
</li><li>The device will appear in Device Manager under Sensors. </li></ol>
</div>
