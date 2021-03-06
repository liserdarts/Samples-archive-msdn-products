# SimRep File System Minifilter Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* File System
* WDM
* Windows Driver
## Topics
* File Systems
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:21:25
## Description

<div id="mainSection">
<p>SimRep is a sample filter that demonstrates how a file system filter can simulate file-system like reparse-point behavior to redirect a file open to an alternate path.
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
<h4><a id="To_select_a_configuration_and_build_a_driver"></a><a id="to_select_a_configuration_and_build_a_driver"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER"></a>To select a configuration and build a driver</h4>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h4><a id="To_select_a_configuration_and_build_a_driver"></a><a id="to_select_a_configuration_and_build_a_driver"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER"></a>To select a configuration and build a driver</h4>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>The minifilter samples come with an INF file that will install the minifilter. To install the minifilter, do the following:</p>
<ol>
<li>
<p>Make sure that <i>filtername</i>.sys and <i>filtername</i>.inf are in the same directory.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This installation will make the necessary registry updates to register the minifilter service and place
<i>filtername</i>.sys in the %SystemRoot%\system32\drivers directory.</p>
</li><li>
<p>In Windows Explorer, right-click <i>filtername</i>.inf, and click <b>Install</b>.</p>
</li><li>
<p>To load the minifilter, run <b>fltmc load </b><i>filtername</i> or <b>net start
</b><i>filtername</i>. </p>
</li></ol>
<h3><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h3>
<p>Normally, if the file-system sees an open for a file with a reparse-point on it, the filesystem fills out the tag buffer and returns STATUS_REPARSE. Minifilters see the post-operation callback for this create. As the create travels up file system filter
 stack in post-create path, each minifilter has the opportunity to interpret the reparse point if they own the tag. If no file system filter claims the tag, IO Manager will attempt to interpret the tag based on tags known to and serviced by IO Manager. If the
 tag is unknown to IO manager then the create is failed with STATUS_IO_REPARSE_TAG_NOT_HANDLED. SimRep does not demonstrate how to handle the case where the file system hits a reparse-point on the file. Instead it &quot;fakes&quot; encountering a reparse point before
 the create reaches the filesystem. When SimRep detects a create for a path that it is redirecting, SimRep replaces the file name in the file object and completes the open with STATUS_REPARSE. This means we reparse without actually going to the file system.</p>
<p>SimRep decides to reparse according to a mapping. The mapping is made up of a &quot;New Mapping Path&quot; and an &quot;Old Mapping Path&quot;. The old mapping path is the path which SimRep looks for on incoming opens. If the path specified for the create is down the Old Mapping
 Path, then SimRep will strip off the Old Mapping Path, and replace it with the New Mapping Path. By default, the Old Mapping Path is \x\y and the New Mapping Path is \a\b. So an open to \x\y\z will be replaced with an open to \a\b\z. These defaults are defined
 as registry keys at install time and are loaded on DriverEntry. See simrep.inf for details.
</p>
<p>It is important to note that SimRep does not take long and short names into account. It literally does a string comparison to detect overlap with the mapping paths. SimRep also handles IRP_MJ_NETWORK_QUERY_OPEN. Because network query opens are FastIo operations,
 they cannot be reparsed. This means network query opens which need to be redirected must be failed with FLT_PREOP_DISALLOW_FASTIO. This will cause the Io Manager to reissue the open as a regular IRP based open. To prevent performance regression, SimRep only
 fails network query opens which need to be reparsed. </p>
<p>For more information on file system minifilter design, start with the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff540402">
File System Minifilter Drivers</a> section in the Installable File Systems Design Guide.</p>
</div>
