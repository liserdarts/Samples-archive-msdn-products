# ObCallback Callback Registration Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:03
## Description

<div id="mainSection">
<p>The ObCallback sample driver demonstrates the use of registered callbacks for process protection. The driver registers control callbacks which are called at process creation.
</p>
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
<h4><a id="Building_the_sample_using_Visual_Studio"></a><a id="building_the_sample_using_visual_studio"></a><a id="BUILDING_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Building the sample using Visual Studio</h4>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\general\obcallback and open the obcallback.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the Visual Studio Professional&nbsp;2012 WDK, you can use the Visual Studio Command
 Prompt window for all build configurations.</p>
<h4><a id="Building_the_sample_using_the_command_line__MSBuild_"></a><a id="building_the_sample_using_the_command_line__msbuild_"></a><a id="BUILDING_THE_SAMPLE_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building the sample using the command line (MSBuild)</h4>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called ObCallbackTest.vcxproj, navigate to the project directory and enter the following MSBuild
 command: <b>msbuild /t:clean /t:build .\ObCallbackTest.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (ObCallbackTest.sys) in the binary output directory corresponding to the target platform, for example src\general\obcallback\driver\Windows&nbsp;8 Debug.
</li></ol>
<h3>Run the sample</h3>
<h3><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h3>
<p>The sample exercises both the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559951">
<b>PsSetCreateProcessNotifyRoutineEx</b></a> and the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff558692">
<b>ObRegisterCallbacks</b></a> routines. The first example uses the <b>ObRegisterCallbacks</b> routine and a callback to restrict requested access rights during a open process action. The second example uses the
<b>PsSetCreateProcessNotifyRoutineEx</b> routine to reject a process creation by examining the command line.</p>
<p>The following is a command line usage scenario to exercise access restriction:</p>
<pre class="syntax"><code>C:\&gt; obcallbacktestctrl.exe  -?                      (for command line help)
C:\&gt; obcallbacktestctrl.exe  -install                (installs the kernel driver)
C:\&gt; obcallbacktestctrl.exe  -name  notepad          (specifies that the string “notepad”  will be watched as a protected executable)
                                                     (now you can start up  “notepad.exe”)
C:\&gt; notepad

C:\&gt; tlist                                           (locate the process ID of notepad.exe)

C:\&gt; kill –f  2329                                   (attempt to kill off the notepad.exe with a PID of 2329)
process notepad.exe (2329) – ‘Untitled – Notepad’ could not be killed

C:\&gt; obcallbacktestctrl.exe  -deprotect              (remove the protections on the notepad process)

C:\&gt; kill –f  2329                                   (attempt to kill off the process – which will succeed)
C:\&gt; obcallbacktestctrl.exe  -uninstall              (uninstall the kernel driver)
 
 </code></pre>
<p>The following is another sample test you can run to prevent a process from being created:</p>
<pre class="syntax"><code>C:\&gt; obcallbacktestctrl.exe  -install                (installs the kernel driver)
C:\&gt; obcallbacktestctrl.exe  -reject  notepad        (specifies that the string “notepad”  will be watched and prevented from starting as a process)

C:\&gt; notepad                                         (now you can start up  “notepad.exe”)
Access is denied.</code></pre>
</div>
