# WPDHelloWorld sample driver for portable devices
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* wpd
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:52:45
## Description

<div id="mainSection">
<p>The WpdHelloWorld sample driver supports four objects: a device object, a storage object, a folder object, and a file object. Each object supports corresponding properties. These properties are defined in the file WpdObjectProperties.h.
</p>
<p>The sample driver supports a device object that exposes ten read-only properties. These properties, their types, and their values are listed in the following table.
</p>
<table>
<tbody>
<tr>
<td>Property name</td>
<td>Property type</td>
<td>Value</td>
</tr>
<tr>
<td>DEVICE_PROTOCOL </td>
<td>String </td>
<td>&quot;Hello World Protocol ver 1.00&quot;</td>
</tr>
<tr>
<td>DEVICE_FIRMWARE_VERSION </td>
<td>String</td>
<td>&quot;1.0.0.0&quot;</td>
</tr>
<tr>
<td>DEVICE_POWER_LEVEL</td>
<td>Integer</td>
<td>100</td>
</tr>
<tr>
<td>DEVICE_MODEL</td>
<td>String</td>
<td>&quot;Hello World!&quot;</td>
</tr>
<tr>
<td>DEVICE_MANUFACTURER</td>
<td>String</td>
<td>&quot;Windows Portable Devices Group&quot;</td>
</tr>
<tr>
<td>DEVICE_FRIENDLY</td>
<td>String</td>
<td>&quot;Hello World!&quot;</td>
</tr>
<tr>
<td>DEVICE_SERIAL_NUMBER</td>
<td>String</td>
<td>&quot;01234567890123-45676890123456&quot;</td>
</tr>
<tr>
<td>DEVICE_SUPPORTS_NONCONSUMABLE</td>
<td>Bool</td>
<td>True</td>
</tr>
<tr>
<td>WPD_DEVICE_TYPE</td>
<td>Integer</td>
<td>WPD_DEVICE_TYPE_GENERIC</td>
</tr>
<tr>
<td>WPD_FUNCTIONAL_OBJECT_CATEGORY</td>
<td>GUID</td>
<td>WPD_FUNCTIONAL_CATEGORY_STORAGE</td>
</tr>
</tbody>
</table>
<p>The driver supports a storage object that exposes six read-only properties. These properties, their types, and their values are listed in the following table.</p>
<table>
<tbody>
<tr>
<td>Property name</td>
<td>Property type</td>
<td>Value</td>
</tr>
<tr>
<td>STORAGE_CAPACITY </td>
<td>64-bit Integer</td>
<td>1024 * 1024 </td>
</tr>
<tr>
<td>STORAGE_FREE_SPACE_IN_BYTES </td>
<td>64-bit Integer</td>
<td>(same as above)</td>
</tr>
<tr>
<td>STORAGE_SERIAL_NUMBER </td>
<td>String</td>
<td>98765432109876-54321098765432 </td>
</tr>
<tr>
<td>STORAGE_FILE_SYSTEM_TYPE </td>
<td>String </td>
<td>FAT32 </td>
</tr>
<tr>
<td>STORAGE_DESCRIPTION </td>
<td>String</td>
<td>Hello World! Memory Storage System </td>
</tr>
<tr>
<td>WPD_STORAGE_TYPE </td>
<td>Integer</td>
<td>WPD_STORAGE_TYPE_FIXED_ROM</td>
</tr>
<tr>
<td>WPD_FUNCTIONAL_OBJECT_CATEGORY </td>
<td>GUID</td>
<td>WPD_FUNCTIONAL_CATEGORY_STORAGE</td>
</tr>
</tbody>
</table>
<p>The driver supports a folder object that exposes three read-only properties. These properties, their types, and their values are listed in the following table.
</p>
<table>
<tbody>
<tr>
<td>Property name</td>
<td>Property type</td>
<td>Value</td>
</tr>
<tr>
<td>WPD_OBJECT_DATE_MODIFIED </td>
<td>Date </td>
<td>2006/6/26 5:0:0.0</td>
</tr>
<tr>
<td>WPD_OBJECT_DATE_CREATED </td>
<td>Date</td>
<td>2006/1/25 12:0:0.0</td>
</tr>
<tr>
<td>WPD_OBJECT_ORIGINAL_FILE_NAME_VALUE </td>
<td>String</td>
<td>Documents </td>
</tr>
</tbody>
</table>
<p>The driver supports a file object that exposes three read-only properties. These properties, their types, and their values are listed in the following table.</p>
<table>
<tbody>
<tr>
<td>Property name</td>
<td>Property type</td>
<td>Value</td>
</tr>
<tr>
<td>WPD_OBJECT_DATE_MODIFIED </td>
<td>Date</td>
<td>2006/6/26 5:0:0.0</td>
</tr>
<tr>
<td>WPD_OBJECT_DATE_CREATED </td>
<td>Date</td>
<td>2006/1/25 12:0:0.0 </td>
</tr>
<tr>
<td>WPD_OBJECT_ORIGINAL_FILE_NAME </td>
<td>String</td>
<td>Readme.txt</td>
</tr>
</tbody>
</table>
<p>In addition to the above properties, every object (for example, device, storage, folder, or file) also supports seven common WPD object properties. These are read-only properties that contain object-specific values for the most part. These properties, their
 types, and their values are listed in the following table.</p>
<table>
<tbody>
<tr>
<td>Property name</td>
<td>Property type</td>
<td>Value</td>
</tr>
<tr>
<td>WPD_OBJECT_ID </td>
<td>String</td>
<td>Object-specific </td>
</tr>
<tr>
<td>WPD_OBJECT_PERSISTENT_UNIQUE_ID </td>
<td>String</td>
<td>Object-specific</td>
</tr>
<tr>
<td>WPD_OBJECT_PARENT_ID </td>
<td>String </td>
<td>Object-specific </td>
</tr>
<tr>
<td>WPD_OBJECT_NAME </td>
<td>String</td>
<td>Object-specific </td>
</tr>
<tr>
<td>WPD_OBJECT_FORMAT </td>
<td>GUID</td>
<td>Object-specific </td>
</tr>
<tr>
<td>WPD_OBJECT_CONTENT_TYPE </td>
<td>GUID</td>
<td>Object-specific</td>
</tr>
<tr>
<td>WPD_OBJECT_CAN_DELETE </td>
<td>Bool</td>
<td>False </td>
</tr>
</tbody>
</table>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">WPD HelloWorld Driver</a> description in the Windows Driver Kit documentation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because
 the sample uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597864">WPD Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff597568">WPD Driver Development Tools</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">WPD Programming Guide</a>
</dt></dl>
<h2>Operating system requirements</h2>
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
<h2>Build the sample</h2>
<ol>
<li>Start the Visual Studio&nbsp;2013 development environment. </li><li>Select the build configuration (for example, Win8 Debug) and the architecture (for example Win32).
</li><li>From the File/Open/Project/Solution… menu, navigate to the VcxProj or sln file and load the project
</li><li>From the Build menu, select Build Solution. </li></ol>
<p>If the build succeeds, you will find the driver DLL and INF files in a subdirectory of your project directory. For example, if you built the Debug configuration and Win32 architecture, the DLL and INF files will be placed in projectDirectory\Win8 Debug\x86
 directory.</p>
<h2>Run the sample</h2>
<h2><a id="Installing_the_sample"></a><a id="installing_the_sample"></a><a id="INSTALLING_THE_SAMPLE"></a>Installing the sample</h2>
<p>To test this sample, you must have a test computer that is running Windows&nbsp;Vista or later. This test computer can be a second computer or, if necessary, your development computer.</p>
<p>To install the WpdHelloWorldDriver sample, do the following:</p>
<ol>
<li>
<p>Copy the driver binary and the WpdHelloWorldDriver.inf file to a directory on your test computer (for example, C:\WpdHelloWorldDriver.)
</p>
</li><li>
<p>Copy the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll, from the \redist\wdf\&lt;architecture&gt; directory to the same directory (for example, C:\WpdHelloWorldDriver).
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading and installing the “Windows Driver Framework (WDF)” package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>
<dl><dt>Navigate to the directory that contains the INF file and binaries (for example, cd /d c:\WpdHelloWorldDriver), and run DevCon.exe as follows:
</dt><dt><b>devcon.exe install WpdHelloWorldDriver.inf WUDF\WpdHelloWorld</b> </dt><dt>You can find DevCon.exe in the \tools directory of the WDK (for example, \tools\devcon\i386\devcon.exe).
</dt></dl>
</li></ol>
</div>
