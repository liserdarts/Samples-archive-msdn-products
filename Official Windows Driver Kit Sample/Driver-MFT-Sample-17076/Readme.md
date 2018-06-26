# Driver MFT Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
## Topics
* other
## IsPublished
* True
## ModifiedDate
* 2015-02-11 07:40:40
## Description

<div id="mainSection">
<p>Provides a <em>driver MFT</em> for use with a camera's Windows Store device app. A
<em>driver MFT</em> is a Media Foundation Transform that's used with a specific camera when capturing video. The driver MFT is also known as MFT0 because it is the first MFT applied to the video stream captured from the camera. This MFT can provide a video
 effect or other processing when capturing photos or video from the camera. It can be distributed along with the driver package for a camera.</p>
<p>In this sample, the driver MFT, when enabled, replaces a portion of the captured video with a green box. To test this sample, download the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Windows Store device app for camera sample</a> and the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249441">Camera Capture UI sample</a>. The
<a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Windows Store device app for camera sample</a> provides a
<em>Windows Store device app</em> that controls the effect implemented by the driver MFT. The
<a href="http://go.microsoft.com/fwlink/p/?linkid=249441">Camera Capture UI sample</a> provides a way to invoke the
<em>Windows Store device app</em>.</p>
<p>This sample is designed to be used with a specific camera. To run the sample, you need the your camera's device ID and device metadata package.</p>
<p class="note"><strong>Note</strong>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because
 the sample uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p>&nbsp;</p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><strong>Concepts</strong> </dt><dt><a href="http://go.microsoft.com/fwlink/p/?LinkId=306683">Windows Store device apps for cameras</a>
</dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=241442">Windows 8 device experience</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ms703138">Media Foundation Transforms</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff568130">Roadmap for Developing Streaming Media Drivers</a>
</dt><dt><strong>Samples</strong></dt><dt><a href="http://go.microsoft.com/fwlink/p/?linkid=249441 ">Camera Capture UI sample</a>
</dt></dl>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>The following steps explain how to build the sample and register the DLL so that a Windows Store device app for your camera can use it.</p>
<ol>
<li>Make sure you have the prerequisites installed. This sample requires Visual Studio Professional&nbsp;2013 or Visual Studio Ultimate&nbsp;2013, and the WDK&nbsp;8.1.
</li><li>Open and build the SampleMft0 Solution.
<p class="note"><strong>Note</strong>&nbsp;&nbsp;To build for ARM, follow these steps:</p>
<ol>
<li>Using Configuration Manager, from the drop-down box for <strong>Active solution platform</strong>, select
<strong>&lt;New...&gt;</strong>. In the <strong>New Project Platform</strong> dialog box, under
<strong>New platform</strong>, select <strong>ARM</strong> and click <strong>OK</strong>.
</li><li>Open project properties for the SampleMft0 project, and follow these steps:
<ul>
<li>Under <strong>Configuration Properties &gt; General</strong>, set <strong>Use of ATL</strong> to
<strong>Static Link to ATL</strong>. </li><li>Under <strong>Configuration Properties &gt; C/C&#43;&#43; &gt; Code Generation</strong>, set
<strong>Runtime Library</strong> to <strong>Multi-threaded (/MT)</strong>. </li><li>Under <strong>Linker &gt; Input</strong>, edit <strong>Ignore Specific Default Libraries</strong> to include shlwapi.lib and urlmon.lib.
</li></ul>
</li></ol>
<p>&nbsp;</p>
</li><li>Copy SampleMft0.dll to C:\Program Files (x86)\SampleMft0 (or C:\Program Files\SampleMft0) on a 32-bit system).
</li><li>Open an administrator command prompt and navigate to the new location of DLL.
</li><li>Run regsvr32 on SampleMft0.dll. </li><li>On an x64 system, build the x64 version of the DLL and repeat steps 3 to 5, copying the 64-bit DLL to C:\Program Files\SampleMft0).
</li><li>Run regedit, and open the device registry key for your camera, under <strong>
HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\</strong>.
<ul>
<li>The following example shows a path to a device registry key for a camera. Your camera has a different device ID and may have a different device class:
<div class="code"><span>
<table>
<tbody>
<tr>
<th>Text</th>
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
</li><li>In Registry Editor, under the <strong>GLOBAL#\Device Parameters</strong> key, add a
<strong>CameraPostProcessingPluginCLSID</strong> value, and set its value to <strong>
{7BB640D9-33A4-4759-B290-F41A31DCF848}</strong>. This is the CLSID of the Driver MFT defined in the SampleMFT0 project.
<p><img src="112254-image.png" alt="" align="middle"></p>
</li></ul>
</li></ol>
<h2>Run the sample</h2>
<h2><a id="Install_the_Windows_Store_device_app"></a><a id="install_the_windows_store_device_app"></a><a id="INSTALL_THE_WINDOWS_STORE_DEVICE_APP"></a>Install the Windows Store device app</h2>
<p>Follow the instructions for building and running the <a href="http://go.microsoft.com/fwlink/p/?linkid=249442">
Device app for camera sample</a>. You need a copy of the device metadata package for your camera. If you don&rsquo;t have device metadata, you can build it using the
<strong>Device Metadata Authoring Wizard</strong>. For more info, see Step 2 of the step-by-step guide:
<a href="http://go.microsoft.com/fwlink/p/?LinkId=313644">Create device metadata for your Windows Store device app</a>.</p>
<h2><a id="Test_the_sample"></a><a id="test_the_sample"></a><a id="TEST_THE_SAMPLE"></a>Test the sample</h2>
<ol>
<li>With your camera attached, build and run the <a href="http://go.microsoft.com/fwlink/p/?linkid=249441 ">
Camera Capture UI sample</a>. </li><li>When the Camera Capture UI is displayed, tap the <strong>Camera options</strong> button and then click
<strong>More </strong>in the <strong>Options</strong> flyout.
<ul>
<li>If the Windows Store device app is correctly installed, the <strong>More options</strong> flyout that is shown should contain
<strong>Effect On/Off</strong> and <strong>Effect</strong> controls.. </li><li>If the Driver MFT is installed, the bottom half of the video preview will be green. Modify the effect by adjusting the
<strong>Effect On/Off</strong> and <strong>Effect</strong> switches in the <strong>
More options</strong> flyout.
<p><img src="112255-image.png" alt="" align="middle"></p>
</li></ul>
</li></ol>
<h2><a id="Troubleshooting"></a><a id="troubleshooting"></a><a id="TROUBLESHOOTING"></a>Troubleshooting</h2>
<p>If the <strong>More options</strong> flyout doesn't contain <strong>Effect On/Off</strong> and
<strong>Effect</strong> controls, check the following:</p>
<ul>
<li>Make sure you enabled test signing before installing the <a href="http://go.microsoft.com/fwlink/p/?linkid=249442">
Windows Store device app for camera sample</a>. Enable test signing by running <code>
bcdedit -set testsigning on</code> in a command prompt. </li><li>Make sure Package Name, Publisher Name, and App ID in the device metadata match the fields defined in the package.appxmanifest file of the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249442">Device app for camera sample</a>.
</li><li>Make sure you are using the <a href="http://go.microsoft.com/fwlink/p/?linkid=249441 ">
Camera Capture UI sample</a> to test. </li><li>If you have an internal camera (rather than an externally connected one), follow the requirements for your camera described in &quot;Appendix A: Requirements for identifying internal cameras&quot; in the
<a href="http://go.microsoft.com/fwlink/p/?linkid=249184">Developing Windows Store device apps for Cameras</a> white paper. Note that you don&rsquo;t have to provide the PLD information described in the paper, if your camera does not expose PLD info in its
 ACPI tables. </li><li>If you have an internal camera, after installing the <a href="http://go.microsoft.com/fwlink/p/?linkid=249441">
Device app for camera sample</a>, refresh the PC using the <strong>Devices and Printers</strong> folder. Select the PC in the folder, and click the Refresh button. The camera itself should not be visible in the
<strong>Devices and Printers</strong> folder. This is because internal cameras are enumerated as part of the PC device container.
<p><img src="112256-image.png" alt="" align="middle"></p>
</li></ul>
<p>If the green box in the bottom half of the video preview doesn't appear, check the following:</p>
<ul>
<li>Check that the <strong>Effect On/Off</strong> switch in the <strong>More Options</strong> flyout is set to
<strong>On</strong>. </li><li>Check that the SampleMFT0.DLL is registered and that you have entered the CLSID of the Driver MFT under the device registry key for the camera you are using to capture video, as described in
<strong>Building the sample</strong>. </li><li>Check that SampleMFT0.DLL is in a subdirectory of Program Files. </li></ul>
</div>
