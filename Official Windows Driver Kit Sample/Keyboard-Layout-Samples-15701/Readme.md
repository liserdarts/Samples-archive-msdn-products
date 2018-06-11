# Keyboard Layout Samples
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* Windows Driver
* other
## Topics
* input
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:10
## Description

<div id="mainSection">
<p>The keyboard layout samples demonstrate how to generate layouts for various keyboards and locales.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>Starting in the WDK, you can build the sample in two ways: using the Visual Studio Integrated Development Environment (IDE) or from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe).</p>
<h3><a id="Building_the_sample_using_Visual_Studio"></a><a id="building_the_sample_using_visual_studio"></a><a id="BUILDING_THE_SAMPLE_USING_VISUAL_STUDIO"></a>Building the sample using Visual Studio</h3>
<ol>
<li>Open Visual Studio. From the <b>File</b> menu, select <b>Open Project/Solution</b>. Within your WDK installation, navigate to src\input\layout and open the kbd.sln project file.
</li><li>Right-click the solution in the <b>Solution Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8.1 Debug or Windows&nbsp;8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the <b>Build</b> menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<p>Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows for each of the supported build configurations. Starting in the WDK, you can use the Visual Studio Command Prompt window for all build
 configurations.</p>
<h3><a id="Building_the_sample_using_the_command_line__MSBuild_"></a><a id="building_the_sample_using_the_command_line__msbuild_"></a><a id="BUILDING_THE_SAMPLE_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building the sample using the command line (MSBuild)</h3>
<ol>
<li>Open a Visual Studio Command Prompt window. Click <b>Start</b> and search for
<b>Developer Command Prompt</b>. If your project is under %PROGRAMFILES%, you need to open the command prompt window using elevated permissions (<b>Run as administrator</b>). From this window you can use MsBuild.exe to build any Visual Studio project by specifying
 the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called kbdus.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\kbdus.vcxproj</b>. </li><li>If the build succeeds, you will find the driver (kbdus.dll) in the binary output directory corresponding to the target platform, for example src\input\layout\kbdus\Windows&nbsp;8.1 Debug.
</li></ol>
<h2>Run the sample</h2>
<h2><a id="Design_and_Operation"></a><a id="design_and_operation"></a><a id="DESIGN_AND_OPERATION"></a>Design and Operation</h2>
<h3><a id="Keyboard_Layout_Samples"></a><a id="keyboard_layout_samples"></a><a id="KEYBOARD_LAYOUT_SAMPLES"></a>Keyboard Layout Samples</h3>
<p>The layout DLL is loaded by the window manager when needed. One of the examples is the logon. The default set of the input locales is set in the HKCU registry, according to user's preference, which can be customized by the Regional and Language Options application
 in Control Panel. The window manager reads the HKCU registry and loads the keyboard layouts accordingly.
</p>
<p>The samples under input/layout include the following keyboard layouts:</p>
<ul>
<li>
<p>kbdus</p>
<p>US-English keyboard layout</p>
</li><li>
<p>kbdfr</p>
<p>French keyboard layout</p>
</li><li>
<p>kbdgr</p>
<p>German keyboard layout</p>
</li><li>
<p>kbd101</p>
<p>Japanese 101 keyboard layout</p>
</li><li>
<p>kbd106</p>
<p>Japanese 106 keyboard layout</p>
</li></ul>
<h3><a id="Conversion_Tables"></a><a id="conversion_tables"></a><a id="CONVERSION_TABLES"></a>Conversion Tables</h3>
<p>A keyboard layout DLL consists of a set of tables. One of the tables converts the scancode to virtual key code, while the other table provides the conversion rule from the virtual key code to the character. Not all the keys or key combinations generate the
 characters. The modifier keys, such as the SHIFT key or the CTRL key, alter the character generation, but do not generate the characters. The special keys, such as F1-F12 functions keys, the Delete key or the Home key, do not generate the characters either.
</p>
<p>The conversion rule from the scancode to the virtual key code is predefined in kbd.h, but can be customized in the layout-specific header files. The layout-specific headers define the keyboard type as it appears in kbd.h, and may redefine some definitions
 that are specific to each layout. </p>
<p>For the typical keyboard hardware, three types of the scancode to the Virtual Key Code conversion table must be defined in the C source file, including non-extended scancode, E0-prefixed scancode, and E1-prefixed scancode.
</p>
<p>The conversion table for the character generation has multiple columns. Each column represents the modifier status and contains the character corresponding to the status. The number of the columns could vary from layout to layout, depending on the number
 of possible combinations of the modifier keys. Generally speaking, ALT GR-enabled keyboard layouts, such as French or German keyboard layouts, have more columns than non-ALT GR-enabled keyboard, such as US-English keyboard layout.
</p>
<h3><a id="Exports_from_keyboard_layout_DLL"></a><a id="exports_from_keyboard_layout_dll"></a><a id="EXPORTS_FROM_KEYBOARD_LAYOUT_DLL"></a>Exports from keyboard layout DLL</h3>
<p>The layout DLLs export one or more entry points, which are called by the window manager to obtain the table address and the information about the layouts.
</p>
<p>For the East Asian keyboard layouts, the keyboard layout DLL has to provide language-specific entry points. Those additional entries expose the language-specific information, such as the kana conversion table, or the special conversion rule, including the
 VK_KANJI generation.</p>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>To install a customized version of a layout DLL, it is recommended to substitute the existing DLL installed by the operating system with the customized DLL. The layout DLLs should be installed in the %windir%\system32 directory (exceptions may apply on 64-bit
 platforms). You may need to disable the System File Protection (SFP) before replacing the layout DLLs. After the layout DLLs are installed, you may need to reboot the system to ensure the new layouts are used..
</p>
<h2><a id="Loading_the_Sample"></a><a id="loading_the_sample"></a><a id="LOADING_THE_SAMPLE"></a>Loading the Sample</h2>
<p>In order to load the layout DLLs, you may need to turn to the Regional and Language Options application in Control Panel and add input locales that use the keyboard layouts you installed.
</p>
<p>You may also choose to do it programmatically. LoadKeyboardLayout API can be used to load the keyboard layouts. To activate the keyboard layout, you may choose to specify KLF_ACTIVATE flag to LoadKeyboardLayout API, or you may need to call ActivateKeyboardLayout
 API. </p>
</div>
