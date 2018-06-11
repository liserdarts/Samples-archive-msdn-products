# Driver MFT Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* other
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:45
## Description

<div id="mainSection">
<p>Provides a <i>driver MFT</i> for use with a camera's Windows Store device app. A
<i>driver MFT</i> is a Media Foundation Transform that's used with a specific camera when capturing video. The driver MFT is also known as MFT0 because it is the first MFT applied to the video stream captured from the camera. This MFT can provide a video effect
 or other processing when capturing photos or video from the camera. It can be distributed along with the driver package for a camera.</p>
<p>In this sample, the driver MFT, when enabled, replaces a portion of the captured video with a green box. To test this sample, download the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Device app for camera sample</a> and the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249441">Camera Capture UI sample</a>. The
<a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Device app for camera sample</a> provides a
<i>Windows Store device app</i> that controls the effect implemented by the driver MFT. The
<a href="http://go.microsoft.com/fwlink/p/?linkid=249441">Camera Capture UI sample</a> provides a way to invoke the
<i>Windows Store device app</i>.</p>
<p>This sample is designed to be used with a specific camera. To run the sample, you need the your camera's device ID and device metadata package.</p>
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><b>Concepts</b> </dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=249184">Developing Windows Store device apps for Cameras</a>
</dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=249185">Considerations for Driver MFT Implementations on Multi-pin Cameras</a>
</dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=241442">Windows 8 Device Experience: Windows Store Device Apps</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ms703138">Media Foundation Transforms</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff568130">Roadmap for Developing Streaming Media Drivers</a>
</dt><dt><b>Tools</b> </dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=221801">Device Metadata Authoring Wizard</a>
</dt><dt><b>Samples</b> </dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Device app for camera sample</a>
</dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=249441 ">Camera Capture UI sample</a>
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
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>The following steps explain how to build the sample and register the DLL so that a Windows Store device app for your camera can use it.</p>
<ol>
<li>Make sure you have the prerequisites installed. This sample requires Microsoft Visual Studio Ultimate&nbsp;2012 and the Windows Driver Kit (WDK).
</li><li>Open and build the SampleMft0 Solution.
<p class="note"><b>Note</b>&nbsp;&nbsp;To build for ARM, follow these steps:</p>
<ol>
<li>Using Configuration Manager, from the drop-down box for <b>Active solution platform</b>, select
<b>&lt;New...&gt;</b>. In the <b>New Project Platform</b> dialog box, under <b>New platform</b>, select
<b>ARM</b> and click <b>OK</b>. </li><li>Open project properties for the SampleMft0 project, and follow these steps:
<ul>
<li>Under <b>Configuration Properties &gt; General</b>, set <b>Use of ATL</b> to <b>
Static Link to ATL</b>. </li><li>Under <b>Configuration Properties &gt; C/C&#43;&#43; &gt; Code Generation</b>, set <b>
Runtime Library</b> to <b>Multi-threaded (/MT)</b>. </li><li>Under <b>Linker &gt; Input</b>, edit <b>Ignore Specific Default Libraries</b> to include shlwapi.lib and urlmon.lib.
</li></ul>
</li></ol>
<p></p>
</li><li>Copy SampleMft0.dll to C:\Program Files (x86)\SampleMft0 (or C:\Program Files\SampleMft0) on a 32-bit system).
</li><li>Open an administrator command prompt and navigate to the new location of DLL.
</li><li>Run regsvr32 on SampleMft0.dll. </li><li>On an x64 system, build the x64 version of the DLL and repeat steps 3 to 5, copying the 64-bit DLL to C:\Program Files\SampleMft0).
</li><li>Run regedit, and open the device registry key for your camera, under <b>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\</b>.
<ul>
<li>The following example shows a path to a device registry key for a camera. Your camera has a different device ID and may have a different device class:
<div class="code"><span>
<table>
<tbody>
<tr>
<th>text</th>
</tr>
<tr>
<td>
<pre>HKEY_LOCAL_MACHINE\
  SYSTEM\
    CurrentControlSet\
      Control\
        DeviceClasses\
          {E5323777-F976-4f5b-9B55-B94699C46E44}\
             ##?#USB#VID_045E&amp;PID_075D&amp;MI_00#8&amp;23C3DB65&amp;0&amp;0000#{E5323777-F976-4f5b-9B55-B94699C46E44}\
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>In Registry Editor, under the <b>GLOBAL#\Device Parameters</b> key, add a <b>
CameraPostProcessingPluginCLSID</b> value, and set its value to <b>{7BB640D9-33A4-4759-B290-F41A31DCF848}</b>. This is the CLSID of the Driver MFT defined in the SampleMFT0 project.
<p><img src="/windowshardware/site/view/file/86491/1/image.png" alt="" align="middle">
</p>
</li></ul>
</li></ol>
<h3>Run the sample</h3>
<h3><a id="Install_the_Windows_Store_device_app"></a><a id="install_the_windows_store_device_app"></a><a id="INSTALL_THE_WINDOWS_STORE_DEVICE_APP"></a>Install the Windows Store device app</h3>
<p>Follow the instructions for building and running the <a href="http://go.microsoft.com/fwlink/p/?linkid=249442">
Device app for camera sample</a>. You need a copy of the device metadata package for your camera. If you don’t have device metadata, you can build it using the
<a href="http://go.microsoft.com/fwlink/p/?linkid=221801">Device Metadata Authoring Wizard</a>.</p>
<h3><a id="Test_the_sample"></a><a id="test_the_sample"></a><a id="TEST_THE_SAMPLE"></a>Test the sample</h3>
<ol>
<li>With your camera attached, build and run the <a href="http://go.microsoft.com/fwlink/p/?linkid=249441 ">
Camera Capture UI sample</a>. </li><li>When the Camera Capture UI is displayed, tap the <b>Camera options</b> button and then click
<b>More </b>in the <b>Options</b> flyout.
<ul>
<li>If the Windows Store device app is correctly installed, the <b>More options</b> flyout that is shown should contain
<b>Effect On/Off</b> and <b>Effect</b> controls.. </li><li>If the Driver MFT is installed, the bottom half of the video preview will be green. Modify the effect by adjusting the
<b>Effect On/Off</b> and <b>Effect</b> switches in the <b>More options</b> flyout.
<p><img src="/windowshardware/site/view/file/86492/1/image.png" alt="" align="middle">
</p>
</li></ul>
</li></ol>
<h3><a id="Troubleshooting"></a><a id="troubleshooting"></a><a id="TROUBLESHOOTING"></a>Troubleshooting</h3>
<p>If the <b>More options</b> flyout doesn't contain <b>Effect On/Off</b> and <b>
Effect</b> controls, check the following:</p>
<ul>
<li>Make sure you enabled test signing before installing the <a href="http://go.microsoft.com/fwlink/p/?linkid=249442">
Device app for camera sample</a>. Enable test signing by running <code>bcdedit -set testsigning on</code> in a command prompt.
</li><li>Make sure Package Name, Publisher Name, and App ID in the device metadata match the fields defined in the package.appxmanifest file of the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Device app for camera sample</a>.
</li><li>Make sure you are using the <a href="http://go.microsoft.com/fwlink/p/?linkid=249441 ">
Camera Capture UI sample</a> to test. </li><li>If you have an internal camera (rather than an externally connected one), follow the requirements for your camera described in &quot;Appendix A: Requirements for identifying internal cameras&quot; in the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249184">Developing Windows Store device apps for Cameras</a> white paper. Note that you don’t have to provide the PLD information described in the paper, if your camera does not expose PLD info in its ACPI tables.
</li><li>If you have an internal camera, after installing the <a href="http://go.microsoft.com/fwlink/p/?linkid=249441">
Device app for camera sample</a>, refresh the PC using the <b>Devices and Printers</b> folder. Select the PC in the folder, and click the Refresh button. The camera itself should not be visible in the
<b>Devices and Printers</b> folder. This is because internal cameras are enumerated as part of the PC device container.
<p><img src="/windowshardware/site/view/file/86493/1/image.png" alt="" align="middle">
</p>
</li></ul>
<p>If the green box in the bottom half of the video preview doesn't appear, check the following:</p>
<ul>
<li>Check that the <b>Effect On/Off</b> switch in the <b>More Options</b> flyout is set to
<b>On</b>. </li><li>Check that the SampleMFT0.DLL is registered and that you have entered the CLSID of the Driver MFT under the device registry key for the camera you are using to capture video, as described in
<b>Building the sample</b>. </li><li>Check that SampleMFT0.DLL is in a subdirectory of Program Files. </li></ul>
</div>
