# DiskPerf Storage Filter Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* StorPort
* Windows Driver
## Topics
* Storage
## IsPublished
* False
## ModifiedDate
* 2012-10-26 11:23:42
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
<p>The DiskPerf filter driver monitors disk access and captures performance data. This driver supports Plug and Play (PnP), power management, and Microsoft Windows Management Instrumentation (WMI).
</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;Vista </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32.</p>
<h4><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h4>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>samplename</i>.sln or
<i>samplename</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h4><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h4>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>samplename</i><b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<h3><a id="Installation_and_Operation"></a><a id="installation_and_operation"></a><a id="INSTALLATION_AND_OPERATION"></a>Installation and Operation</h3>
<p>The <i>DiskPerf</i> filter driver should be installed as an upper filter driver for a disk or volume device. To install this driver, open Explorer, locate and right-click Diskperf.inf, and then click
<b>Install</b>. </p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff566980">
Storage Filter Drivers</a> in the storage technologies design guide.</p>
</div>
