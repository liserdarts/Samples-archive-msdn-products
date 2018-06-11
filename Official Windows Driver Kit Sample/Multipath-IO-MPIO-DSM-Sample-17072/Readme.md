# Multipath I/O (MPIO) DSM Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* StorPort
* Windows Driver
## Topics
* Storage
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:28
## Description

<div id="mainSection">
<p>The MPIO DSM Sample is intended to serve as an example to follow when building your own vendor specific device specific modules (DSM). This sample DSM supports iSCSI and Fibre Channel devices.
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
<td><dt>None supported </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>You can build the sample in two ways: using Microsoft Visual Studio or the command line (<i>MSBuild</i>).</p>
<h2><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h2>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8.1 Debug and Win32.</p>
<h3><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h3>
<ol>
<li>Open the driver project or solution in Visual Studio (find <i>samplename</i>.sln or
<i>samplename</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8.1 Debug or Windows&nbsp;8.1 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h2><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h2>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<h3><a id="To_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="to_select_a_configuration_and_build_a_driver_or_an_application"></a><a id="TO_SELECT_A_CONFIGURATION_AND_BUILD_A_DRIVER_OR_AN_APPLICATION"></a>To select a configuration
 and build a driver or an application</h3>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>samplename</i><b>.vcxproj</b>. </li></ol>
<h2>Run the sample</h2>
<h2><a id="Installation_and_Operation"></a><a id="installation_and_operation"></a><a id="INSTALLATION_AND_OPERATION"></a>Installation and Operation</h2>
<p>The installation process depends on proper construction of your DSM's INF as well as an installation program provided by you. These are important aspects of complying with the Designed for Windows logo program. The installation was designed to allow for
 multiple vendors to easily add DSMs and to eliminate rebooting as much as possible. The installation process will require you to update your installation routines and to use the new .INF files. With the new process, you can only modify your DSM's INF file.</p>
<p>The installer sample only needs to be called one time with the INF/driver source path, the name of the DSM INF, and the DSM hardware ID. Typically this would be called from an MSI or setup package, such as one created by InstallShield or other installer
 technology.</p>
<p>The following annotated DSM INF file illustrates the correct format for your DSM. Replace only those items that are in bold italics. Remember, you must not use &quot;GENDSM&quot; or &quot;MSISCDSM&quot; or &quot;MSDSM&quot; as the name of your DSM. Therefore, you must replace any instances
 of those strings with the proper name of your DSM.</p>
<pre class="syntax"><code>;
; Copyright (c) &lt;YOUR COMPANY NAME HERE&gt;.  All rights reserved.
;</code></pre>
<p>In the Version section, make sure the DriverVer is correct for your DSM. Ideally it should match the version in the RC file. You must specify a different catalog file since the MPIO core drivers now come pre-signed:</p>
<pre class="syntax"><code>[Version]
Signature   = &quot;$WINDOWS NT$&quot;
Class       = System
ClassGuid   = {4D36E97D-E325-11CE-BFC1-08002BE10318}
Provider    = %VNDR%
CatalogFile = mydsm.cat
DriverVer   = MM/DD/YYYY,x.x.xxxx

[DestinationDirs]
DefaultDestDir = 12

;
; Multi-Path Device-Specific Module
;

[Manufacturer]
%std_mfg% = std_mfg</code></pre>
<p>Substitute all instances of &quot;gendsm&quot; with the proper name for your DSM. For example, &quot;mydsm&quot;:</p>
<pre class="syntax"><code>[std_mfg]
%mydsm_devicedesc% = mydsm_install, Root\MYDSM

[mydsm_install]
copyfiles = @mydsm.sys

[mydsm_install.Services]
AddService = mydsm, %SPSVCINST_ASSOCSERVICE%, mydsm_service

[mydsm_service]
DisplayName    = %mydsm_desc%
ServiceType    = %SERVICE_KERNEL_DRIVER%
StartType      = %SERVICE_BOOT_START%
ErrorControl   = %SERVICE_ERROR_NORMAL%
ServiceBinary  = %12%\mydsm.sys
LoadOrderGroup = &quot;System Bus Extender&quot;
AddReg         = mydsm_addreg</code></pre>
<p>This next section contains the Hardware ID strings for your devices. You can have more than one. Sample format: &quot;VENDOR PRODUCT &quot; - remember to use spaces in a field (vendor, product ID) to pad this to be eight characters for the vendor name (as registered
 with STA) and sixteen for the product ID (unless the supported devices share a common prefix, in which case the product ID can be less than 16 characters).</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; Underscores that are part of the inquiry string (applies to vendor ID as well as product ID fields) must NOT be replaced with spaces.</p>
<p>In this sample, there are two different strings:</p>
<pre class="syntax"><code>;
; The following cannot be grouped (as above)
;

HKLM, &quot;SYSTEM\CurrentControlSet\Control\MPDEV&quot;, &quot;MPIOSupportedDeviceList&quot;, %REG_MULTI_SZ_APPEND%, &quot;VENDOR1 PRODUCT1        &quot;
HKLM, &quot;SYSTEM\CurrentControlSet\Control\MPDEV&quot;, &quot;MPIOSupportedDeviceList&quot;, %REG_MULTI_SZ_APPEND%, &quot;VENDOR2 PRODUCT2        &quot;</code></pre>
<p>These are valid samples:</p>
<pre class="syntax"><code>HKLM, &quot;SYSTEM\CurrentControlSet\Control\MPDEV&quot;, &quot;MPIOSupportedDeviceList&quot;, %REG_MULTI_SZ_APPEND%, &quot;MAXTOR  ATLASU320_18_WLS&quot;

HKLM, &quot;SYSTEM\CurrentControlSet\Control\MPDEV&quot;, &quot;MPIOSupportedDeviceList&quot;, %REG_MULTI_SZ_APPEND%, &quot;VENDOR3 PROD_PREFIX&quot;</code></pre>
<p>(to replace &quot;VENDOR3 PROD_PREFIX_A &quot;, &quot;VENDOR3 PROD_PREFIX_B &quot; and &quot;VENDOR3 PROD_PREFIX_C &quot;)</p>
<p>In the above example, it is assumed that this sample DSM will be used to support three devices from vendor &quot;VENDOR3&quot; with product IDs &quot;PROD_PREFIX_A&quot;, &quot;PROD_PREFIX_B&quot; and &quot;PROD_PREFIX_C&quot; respectively. Since all the three devices share the common product
 ID sub-string &quot;PROD_PREFIX&quot;, we can replace separate entries (in MPIOSupportedDeviceList) for each one of them with just one entry that uses the product ID sub-string that is common to them, without padding it with spaces to make it 16 characters.</p>
<p>It is advisable to use this format if your storage devices generate product IDs on-the-fly using a known product ID prefix. This can significantly reduce the size of your INF file and makes future changes to the INF file less prone to human error. Large
 INF files can result in very long device installation times and will fill the registry with unnecessary information. Please make sure you take advantage of this new capability as it will improve your customers' experience with MPIO.</p>
<p>Add one entry for each WMI GUID that you use in your DSM. This is required:</p>
<pre class="syntax"><code>HKLM, &quot;SYSTEM\CurrentControlSet\Control\WMI\Security&quot;, &quot;04517f7e-92bb-4ebe-aed0-54339fa5f544&quot;,\%REG_BINARY_NOCLOBBER%,\
        01,00,04,80,14,00,00,00,24,00,00,00,00,00,00,00,\
        34,00,00,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,02,00,20,00,01,00,00,00,00,00,18,00,\
        1f,00,12,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00
HKLM, &quot;SYSTEM\CurrentControlSet\Control\WMI\Security&quot;, &quot;d13373f6-0114-4fe3-b91b-f52c95dfc417&quot;,\%REG_BINARY_NOCLOBBER%,\
        01,00,04,80,14,00,00,00,24,00,00,00,00,00,00,00,\
        34,00,00,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,02,00,48,00,03,00,00,00,00,00,18,00,\
        ff,0f,12,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,00,00,14,00,0d,00,12,00,01,01,00,00,\
        00,00,00,01,00,00,00,00,00,00,14,00,ff,07,12,00,\
        01,01,00,00,00,00,00,05,12,00,00,00
HKLM, &quot;SYSTEM\CurrentControlSet\Control\WMI\Security&quot;, &quot;d6dc1bf0-95fa-4246-afd7-40a030458f48&quot;,\%REG_BINARY_NOCLOBBER%,\
        01,00,04,80,14,00,00,00,24,00,00,00,00,00,00,00,\
        34,00,00,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,02,00,48,00,03,00,00,00,00,00,18,00,\
        ff,0f,12,00,01,02,00,00,00,00,00,05,20,00,00,00,\
        20,02,00,00,00,00,14,00,09,00,12,00,01,01,00,00,\
        00,00,00,01,00,00,00,00,00,00,14,00,09,00,12,00,\
        01,01,00,00,00,00,00,05,12,00,00,00

;
; Localizable Strings
;</code></pre>
<p>Finally, modify the following strings:</p>
<pre class="syntax"><code>[Strings]
VNDR              = &quot;Your Company Name Here&quot;
std_mfg           = &quot;(Standard system devices)&quot;
mydsm_devicedesc = &quot;&lt;Your product&gt; Multi-Path Device Specific Module&quot;</code></pre>
<p>The following string is displayed as the friendly name of your DSM:</p>
<pre class="syntax"><code>mydsm_desc       = &quot;&lt;Your product name&gt; Multi-Path DSM&quot;

;
; Handy macro substitutions (non-localizable)
;

SERVICE_KERNEL_DRIVER  = 1

SERVICE_BOOT_START     = 0
SERVICE_SYSTEM_START   = 1
SERVICE_DEMAND_START   = 3

SERVICE_ERROR_IGNORE   = 0
SERVICE_ERROR_NORMAL   = 1
SERVICE_ERROR_CRITICAL = 3

SPSVCINST_ASSOCSERVICE = 2

REG_MULTI_SZ           = 0x00010000
REG_MULTI_SZ_APPEND    = 0x00010008
REG_EXPAND_SZ          = 0x00020000
REG_DWORD              = 0x00010001
REG_BINARY_NOCLOBBER   = 0x00030003
</code></pre>
<p>You should be aware of the following when you install the MPIO DSM sample:</p>
<ol>
<li>
<p>The install sample assumes that all necessary files have already been copied over to a vendor specific directory (preferably a folder under Program Files) and takes that path as one of the parameters. This eliminates requests for the original media when
 new devices appear.</p>
</li><li>
<p>As the port filter needs to go on top of every adapter that hosts (or might host) a path to the disk, all SCSI adapters are restarted at the end of the install</p>
<p>It is expected that the adapter that hosts the system volumes (boot/paging) will not restart, but that should not be problem if you are not multipathing the boot volume. However, if you are multipathing the boot volume, you will need to restart the system.</p>
</li></ol>
<p class="note"><b>Note</b>&nbsp;&nbsp;Other filter drivers installed as port filters may interfere with the proper operation of the MPIO port filter. Microsoft does not recommend the use of such filter drivers which may be supplied by HBA miniport vendors.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Since your DSM binary is not signed, you will get Unsigned Driver Pop-Ups. Ignore these and accept the installation of the new driver. Once your package has been successfully qualified by WHQL, your binaries will get signed and
 your customers will not get unsigned driver popups.</p>
</div>
