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
* 2012-11-20 02:53:25
## Description
<style><!-- pre.syntax { font-size: 110 background: #dddddd; padding: 4px,8px; cursor: text; color: #000000; width: 97 } body{font-family:Verdana,Arial,Helvetica,sans-serif;color:#000;font-size:80%} H1{font-size:150%;font-weight:bold} H1.heading{font-size:110%;font-family:Verdana,Arial,Helvetica,sans-serif;font-weight:bold;line-height:120%}
 H2{font-size:115%;font-weight:700} H2.subtitle{font-size:180%;font-weight:400;margin-bottom:.6em} H3{font-size:110%;font-weight:700} H4,H5,H6{font-size:100%;font-weight:700} h4.subHeading{font-size:100%} dl{margin:0 0 10px;padding:0 0 0 1px} dt{font-style:normal;margin:0}
 li{margin-bottom:3px;margin-left:0} ol{line-height:140%;list-style-type:decimal;margin-bottom:15px;margin-left:24px} ol ol{line-height:140%;list-style-type:lower-alpha;margin-bottom:4px;margin-left:24px;margin-top:3px} ol ul,ul ol{line-height:140%;margin-bottom:15px;margin-top:15px}
 p{margin:0 0 10px;padding:0} div.section p{margin-bottom:15px;margin-top:0} ul{line-height:140%;list-style-position:outside;list-style-type:disc;margin-bottom:15px} ul ul{line-height:140%;list-style-type:disc;margin-bottom:4px;margin-left:17px;margin-top:3px}
 .heading{font-weight:700;margin-bottom:8px;margin-top:18px} .subHeading{font-size:100%;font-weight:700;margin:0} div#mainSection table{border:1px solid #ddd;font-size:100%;margin-bottom:5px;margin-left:5px;margin-top:5px;width:97%;clear:both} div#mainSection
 table tr{vertical-align:top} div#mainSection table th{border-bottom:1px solid #c8cdde;color:#006;padding-left:5px;padding-right:5px;text-align:left} div#mainSection table td{border:1px solid #d5d5d3;margin:1px;padding-left:5px;padding-right:5px} div#mainSection
 table td.imageCell{white-space:nowrap} /* These are the original lines from global-bn1945 div.ContentArea table th,div.ContentArea table td{background:#fff;border:0 solid #ccc;font-family:Verdana;padding:5px;text-align:left;vertical-align:top} div.ContentArea
 table th{background:#ccc none repeat scroll 0% 50%;vertical-align:bottom} div.ContentArea table{border-collapse:collapse;width:auto} */ /* Removing ContentArea class requirement from commented out lines from global-bn1945 above */ table th,table td{background:#fff;border:0
 solid #ccc;font-family:Verdana;padding:5px;text-align:left;vertical-align:top} table th{background:#ccc none repeat scroll 0% 50%;vertical-align:bottom} table{border-collapse:collapse;width:auto} div.clsNote{background-color:#eee;margin-bottom:4px;padding:2px}
 div.code{width:98.9%} code{font-family:Monospace,Courier New,Courier;font-size:105%;color:#006} span.label{font-weight:bold} div.caption{font-weight:bold;font-size:100%;color:#039} .procedureSubHeading{font-size:110%;font-weight:bold} span.sub{vertical-align:sub}
 span.sup{vertical-align:super} span.big{font-size:larger} span.small{font-size:smaller} span.tt{font-family:Courier,"Courier New",Consolas,monospace} .CCE_Message{color:Red;font-size:10pt} --></style>
<div id="mainSection">
<p>The HID sample driver for sensors demonstrates how an OEM or IHV could write a UMDF driver to support sensors that are compliant with the HID Sensor Class Driver specification. This driver is also helpful for IHVs who must design and test firmware for a
 HID-based sensor.</p>
<p>This sample driver is generally identical the HID Sensor Class Driver that is available in Windows&nbsp;8 and provides developers with the following:</p>
<ul>
<li>Source code to help build and compile a working driver for Windows&nbsp;7. </li><li>Source code to help debug the hardware/firmware during development. </li></ul>
<p>There are three instances where you may need to modify the sample driver.</p>
<ol>
<li>The sample does not support your sensor. </li><li>The sample supports your sensor; but, it does not support unique functionality on your particular device.
</li><li>The sample needs to run on an earlier version of Windows. </li></ol>
<p>The sample driver is based on the HID protocol. It supports the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Hh406644">
eighteen common sensors</a>. In addition, it supports a Custom class that lets you integrate any HID sensor not found in this list.</p>
<p>For a complete description of this sample and its underlying code and functionality, refer to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Hh406647">Sensors HID driver sample</a> description in the Windows Driver Kit documentation.</p>
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff545810">Sensors Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Hh406596">Sensors Programming Guide</a>
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
</li><li>From the File/Open/Project/Solution&hellip; menu, navigate to the VcxProj or sln file and load the project
</li><li>From the Build menu, select Build Solution. </li></ol>
<p>If the build succeeds, you will find the driver DLL and INF files in a subdirectory of your project directory. For example, if you built the Debug configuration and Win32 architecture, the DLL and INF files will be placed in projectDirectory\Win8 Debug\x86
 directory.</p>
<h3>Run the sample</h3>
<h3><a id="Installing_the_sample"></a><a id="installing_the_sample"></a><a id="INSTALLING_THE_SAMPLE"></a>Installing the sample</h3>
<p>To install the sensors driver sample:</p>
<ol>
<li>Ensure that the driver builds without errors. </li><li>Enable test signing by running the command &ldquo;bcdedit /set testsigning on&rdquo; from an elevated command prompt. (You&rsquo;ll need to reboot your machine after you enable test signing.)
</li><li>Edit the following line in the sample INF file, replacing the vendor identifier, &quot;VID_C1CA&quot;, with the identifier supplied by your firmware in the USB device descriptor. In addition, replace the product identifier, &quot;PID_0020&quot;, with the identifier supplied
 by your firmware in the USB device descriptor.
<pre class="syntax"><code>%SensorsHIDDriverSample.Collection.DeviceDesc% = SensorsHIDDriverSample_Install,HID\VID_C1CA&amp;PID_0020</code></pre>
</li><li>Copy the DLL and INF files to a separate folder </li><li>Install your HID sensor board. Locate this newly installed board in the Device Manager under Human Interface Devices &ndash; it will be a &ldquo;HID-compliant device.&rdquo;
</li><li>Right-click on the device and select &ldquo;Update Driver Software&hellip;&quot; </li><li>Select &ldquo;Browse my computer for driver software&rdquo; </li><li>Select &ldquo;Let me pick from a list of device drivers on my computer&rdquo;
</li><li>Select &ldquo;Have disk&hellip;&rdquo; </li><li>Select &ldquo;Browse&hellip;&rdquo; </li><li>Navigate to the folder in which the DLL and INF files were placed in step 2. Select the INF and select &ldquo;Open&rdquo;
</li><li>Select OK </li><li>Select Next </li><li>If a Windows security prompt appears, select &ldquo;Install this driver software anyway&rdquo;
</li><li>The device will appear in Device Manager under Sensors. </li></ol>
</div>
