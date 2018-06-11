# Sensors Geolocation Sample Driver (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* Sensors and Location
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:24:57
## Description

<div id="mainSection">
<p>The geolocation sample driver demonstrates a minimal UMDF driver that emulates a Global Positioning System (GPS) device. Note that this sample is applicable to other global navigation satellite system (GNSS) solutions.
</p>
<p>This driver is minimal in the sense that it does not connect to hardware; otherwise, it is fully compliant with our best practices for building a UMDF sensor driver. Instead of sending the user's current coordinates, this sample implements a pseudo sensor
 that issues simulated altitude, latitude, longitude, and other GPS data. In addition, this sample issues a timestamp that is useful when testing and debugging.</p>
<p>This sample serves three purposes: First, it demonstrates the minimal functionality required by a UMDF sensor driver. Second, it provides a skeleton on which you can build a working driver. Third, it includes support for the Radio Management API that provides
 notifications of radio state changes for devices like a GPS.</p>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh768273">Sensors geolocation driver sample</a> description in the Windows Driver Kit documentation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545810">Sensors Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406596">Sensors Programming Guide</a>
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
<td><dt>None supported </dt></td>
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
<h2><a id="Installing_the_sample_driver"></a><a id="installing_the_sample_driver"></a><a id="INSTALLING_THE_SAMPLE_DRIVER"></a>Installing the sample driver</h2>
<p>To install the sensor Geolocation sample driver:</p>
<ol>
<li>Ensure that the driver builds without errors. </li><li>Enable test signing by running the command “bcdedit /set testsigning on” from an elevated command prompt. (You’ll need to reboot your machine after you enable test signing.)
</li><li>Copy the DLL and INF files to a separate folder </li><li>Copy the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll, from the \redist\wdf\&lt;architecture&gt; directory to the same folder.
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8.1, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading and installing the “Windows Driver Framework (WDF)” package from
<a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">WDK 8 Redistributable Components</a>.</p>
</li><li>From an elevated command prompt, run <b>devcon.exe</b>. You can find this program in the tools\devcon folder where you installed the WDK. Type the following command in the command window:
<b>devcon.exe install SensorGeolocation.inf &quot;Sensors\SensorsGeolocationDriverSample&quot;</b>
</li></ol>
<h2><a id="Installing_the_sample_DLL"></a><a id="installing_the_sample_dll"></a><a id="INSTALLING_THE_SAMPLE_DLL"></a>Installing the sample DLL</h2>
<p>To install the radio-manager sample DLL:</p>
<ol>
<li>Make sure that the DLL builds without errors. </li><li>Copy the files install.cmd and SampleRM.reg from the <b>\C&#43;&#43;\RadioManagerGPS</b> folder to a separate folder.
</li><li>Copy the file SampleRM.dll from the build folder to the separate folder that you created in step 2 (above).
</li><li>From an elevated command prompt, run install.cmd. </li></ol>
</div>
