# Bluetooth Low Energy (LE) Generic Attribute (GATT) Profile Drivers
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
* 2012-10-26 11:29:21
## Description
<style> pre.syntax { font-size: 110 background: #dddddd; padding: 4px,8px; cursor: text; color: #000000; width: 97 } body{font-family:Verdana,Arial,Helvetica,sans-serif;color:#000;font-size:80%} H1{font-size:150%;font-weight:bold} H1.heading{font-size:110%;font-family:Verdana,Arial,Helvetica,sans-serif;font-weight:bold;line-height:120%}
 H2{font-size:115%;font-weight:700} H2.subtitle{font-size:180%;font-weight:400;margin-bottom:.6em} H3{font-size:110%;font-weight:700} H4,H5,H6{font-size:100%;font-weight:700} h4.subHeading{font-size:100%} dl{margin:0 0 10px;padding:0 0 0 1px} dt{font-style:normal;margin:0}
 li{margin-bottom:3px;margin-left:0} ol{line-height:140%;list-style-type:decimal;margin-bottom:15px;margin-left:24px} ol ol{line-height:140%;list-style-type:lower-alpha;margin-bottom:4px;margin-left:24px;margin-top:3px} ol ul,ul ol{line-height:140%;margin-bottom:15px;margin-top:15px}
 p{margin:0 0 10px;padding:0} div.section p{margin-bottom:15px;margin-top:0} ul{line-height:140%;list-style-position:outside;list-style-type:disc;margin-bottom:15px} ul ul{line-height:140%;list-style-type:disc;margin-bottom:4px;margin-left:17px;margin-top:3px}
 .heading{font-weight:700;margin-bottom:8px;margin-top:18px} .subHeading{font-size:100%;font-weight:700;margin:0} div#mainSection table{border:1px solid #ddd;font-size:100%;margin-bottom:5px;margin-left:5px;margin-top:5px;width:97%;clear:both} div#mainSection
 table tr{vertical-align:top} div#mainSection table th{border-bottom:1px solid #c8cdde;color:#006;padding-left:5px;padding-right:5px;text-align:left} div#mainSection table td{border:1px solid #d5d5d3;margin:1px;padding-left:5px;padding-right:5px} div#mainSection
 table td.imageCell{white-space:nowrap} /* These are the original lines from global-bn1945 div.ContentArea table th,div.ContentArea table td{background:#fff;border:0 solid #ccc;font-family:Verdana;padding:5px;text-align:left;vertical-align:top} div.ContentArea
 table th{background:#ccc none repeat scroll 0% 50%;vertical-align:bottom} div.ContentArea table{border-collapse:collapse;width:auto} */ /* Removing ContentArea class requirement from commented out lines from global-bn1945 above */ table th,table td{background:#fff;border:0
 solid #ccc;font-family:Verdana;padding:5px;text-align:left;vertical-align:top} table th{background:#ccc none repeat scroll 0% 50%;vertical-align:bottom} table{border-collapse:collapse;width:auto} div.clsNote{background-color:#eee;margin-bottom:4px;padding:2px}
 div.code{width:98.9%} code{font-family:Monospace,Courier New,Courier;font-size:105%;color:#006} span.label{font-weight:bold} div.caption{font-weight:bold;font-size:100%;color:#039} .procedureSubHeading{font-size:110%;font-weight:bold} span.sub{vertical-align:sub}
 span.sup{vertical-align:super} span.big{font-size:larger} span.small{font-size:smaller} span.tt{font-family:Courier,"Courier New",Consolas,monospace} .CCE_Message{color:Red;font-size:10pt} </style>
<div id="mainSection">
<p>The purpose of each respective GATT sample is to demonstrate how to implement a basic
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff597729">Windows Portable Device (WPD) driver</a> that will work with a blood pressure monitor, a heart rate monitor, thermometer, and the Texas Instrument's CC2540 Mini Development Kit, each
 of which exposes it’s functionality using <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff536598">
Bluetooth Low Energy (LE)</a>, which in turn use the Windows <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff536585">
Bluetooth Low Energy (LE) DDIs</a>. </p>
<p>Each subdirectory contains driver sample files specifically designed to work with devices that implement the blood pressure profile, heart rate profile, thermometer profile, and TI CC2540 keyfob respectively, as defined in the Bluetooth 4.0 – Bluetooth Low
 Energy specification. </p>
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
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\bluetooth\GATT\Wpd and open the WpdSamples.sln project file.
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
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to each of the respective project directories and enter the appropriate
<b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called WpdHealthBloodPressureService.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\WpdHealthBloodPressureService.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (WpdHealthBloodPressureService.dll) in the binary output directory corresponding to the target platform, for example src\bluetooth\GATT\Wpd\WpdHealthBloodPressure\Package\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<p><b>Installation</b> </p>
<p>The following steps are required to install the respective sample drivers:</p>
<p>1. Perform regular pairing operations with the device by going to the Device and Printers folder, Add a Device Wizard, and add the device.</p>
<p>2. Go to Device Manager (Right click on Computer and select Manage).</p>
<p>3. Expand the Bluetooth node.</p>
<p>4. You should see a number of Bluetooth LE Generic Attribute Service nodes there. Select the one that has the following UUID as part of the Hardware ID:</p>
<p>For blood pressure devices use {0000<b>1810</b>-0000-1000-8000-00805f9b34fb}</p>
<p>For heart rate devices use {0000<b>180D</b>-0000-1000-8000-00805f9b34fb}</p>
<p>For thermometer devices use {0000<b>1809</b>-0000-1000-8000-00805f9b34fb}</p>
<p>For the Texas Instrument's CC2540 Mini Development Kit use {0000<b>ffe0</b>-0000-1000-8000-00805f9b34fb}</p>
<p>You can look up the Hardware ID of the node by right clicking on the node and select Properties. Go to the Details tab, and select Hardware Ids in the Property drop down.</p>
<p>5. In the Driver tab of the Properties window, select Update Driver.</p>
<p>6. Select Browse my computer for driver software.</p>
<p>7. Select Let me pick from a list of device drivers on my computer.</p>
<p>8. Select Have Disk.</p>
<p>9. Provide the path to the directory that contains the *.inf files for the sample driver that you have just built.</p>
<p>To view event information or invoke driver service methods on the installed driver, you can use the wpdinfo.exe tool that ships as part of the WDK.</p>
<p>If you plan to access driver functionality from a Windows Store application, you must also install device metadata for the device you’re planning to use.</p>
<p>More on <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff552953(v=vs.85).aspx">
authoring Device Metadata Packages</a>.</p>
<p>The metadata needs to be deployed locally and, for testing purposes, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff553484(v=vs.85).aspx">
testsigning needs to be enabled</a>.</p>
</div>
