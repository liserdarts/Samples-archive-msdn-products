# Storage Class Driver Samples
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* WDM
* StorPort
* Windows Driver
## Topics
* WDK
* Storage
## IsPublished
* False
## ModifiedDate
* 2012-02-29 04:32:38
## Description

<h3>Storage Class Driver Samples</h3>
<p>The storage sample package contains storage class drivers. </p>
<p>The following storage driver samples are included:</p>
<table>
<tbody>
<tr>
<th>Minifilter Sample</th>
<th>Description</th>
</tr>
<tr>
<td>
<p><i>cdrom</i> </p>
</td>
<td>
<p>The CD ROM driver is used to provide access to CD, DVD and Blu-ray drives. It supports Plug and Play, Power Management, and AutoRun (media change notification). It is a 64-bit compliant driver.
</p>
</td>
</tr>
<tr>
<td>
<p><i>classpnp</i> </p>
</td>
<td>
<p>This library is the library for all storage drivers. It simplifies writing a storage class driver by implementing 90 percent of the code that you need to support Plug and Play (PnP), power management, and so on. This library is used by disk, CDROM, and the
 tape class drivers. </p>
</td>
</tr>
<tr>
<td>
<p><i>disk</i> </p>
</td>
<td>
<p>The disk class driver sample is used for managing disk devices. </p>
</td>
</tr>
</tbody>
</table>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows 8 Consumer Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server 8 Beta </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>You can build the sample in two ways: using Visual Studio&nbsp;11 Ultimate Beta or the command line (<i>MSBuild</i>).</p>
<h3><a name="building_a_driver_using_visual_studio"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Consumer Preview Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver or an application</b>
</p>
<ol>
<li>Open the driver project or solution in Visual Studio&nbsp;11 Ultimate Beta (find <i>
samplename</i>.sln or <i>samplename</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a name="building_a_driver_using_the_command_line__msbuild_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver or an application</b>
</p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b> <i>samplename</i> <b>.vcxproj</b>. </li></ol>
<h3>Run the sample</h3>
<h3><a name="installation_and_operation"></a>Installation and Operation</h3>
<h4><a name="cd-rom_class_driver"></a>CD-ROM Class Driver</h4>
<p>The in-box CD ROM driver is protected by the system, and thus a normal device driver update attempt through the Device Manager will fail. Users are not encouraged to replace the in-box CD ROM driver. The following work-around is provided in case there is
 a need, but the users are warned that this may harm the system. </p>
<ol>
<li>Locate the &quot;cdrom.inf&quot; file in the binary output directory, and update the file by replacing all &quot;cdrom.sys&quot; occurrences with &quot;mycdrom.sys&quot;.
</li><li>Rename the &quot;cdrom.inf&quot; file to &quot;mycdrom.inf&quot;. </li><li>Copy &quot;mycdrom.sys&quot; and &quot;mycdrom.inf&quot; from the binary output directory to the test machine, if applicable.
</li><li>Launch the Device Manager </li><li>Select the appropriate device under the &quot;DVD/CD-ROM drives&quot; category. </li><li>On the right-click menu, select &quot;Update Driver Software...&quot;. </li><li>Select &quot;Browse my computer for driver software&quot;. </li><li>Select &quot;Let me pick from a list of device drivers on my computer&quot;. </li><li>Click &quot;Have Disk...&quot;, and point to the directory that contains &quot;mycdrom.inf&quot; and &quot;mycdrom.sys&quot;.
</li><li>Click &quot;Next&quot;. If you get a warning dialog about installing unsigned driver, click &quot;Yes&quot;.
</li><li>Click &quot;Next&quot; to complete the driver upgrade. </li><li>After installation completes successfully, &quot;mycdrom.sys&quot; will be the effective driver for the device, &quot;cdrom.sys&quot; will no longer be used.
</li></ol>
<h4><a name="classpnp_class_driver_library"></a>Classpnp Class Driver Library</h4>
<p>The storage class drivers are used to interact with mass storage devices along with appropriate port driver. The class drivers are layered above the port drivers and manage mass storage devices of a specific class, regardless of their bus type. The classpnp
 sample contains the common routines that are required for all storage class drivers such as PnP and power management. It also provides I/O and error handling support.
</p>
<h4><a name="disk_class_driver"></a>Disk Class Driver</h4>
<p>The disk class driver is used to interact with disk devices along with the appropriate port driver. The disk class driver is layered above the port driver and manages disk devices regardless of their bus type. This driver attaches to the disk devices that
 are enumerated by all of the storage port drivers. This driver exposes the required functionality to the file system drivers to access the disk devices.</p>
