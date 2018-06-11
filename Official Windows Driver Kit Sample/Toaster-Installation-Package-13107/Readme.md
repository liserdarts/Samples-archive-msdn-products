# Toaster Installation Package
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* WDM
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2011-09-14 12:20:50
## Description

<h3>Toaster Installation Package</h3>
<p>This document discusses the different approaches that end users take when adding new hardware to their computer, and describes an approach that addresses these scenarios in a consistent, robust manner that works cooperatively with Plug and Play in Windows
 2000 and Windows XP. It also outlines the mechanisms provided to facilitate additional vendor requirements such as the installation of value-added software.</p>
<p>The installation of software to support an instance of a given device (known as &quot;device installation&quot; or &quot;driver installation&quot;) is done in a device-centric fashion in Windows operating systems. A device INF that matches up with one of the device's hardware
 or compatible IDs is used to identify the required driver file(s), registry modifications, etc., that are needed to make the device fully operational. This INF, along with the files copied thereby and a catalog that contains the digital signatures of the INF
 and these other files, constitute what is known as a &quot;driver package&quot;.</p>
<p>Because device installation is done for a specific instance of a device, the &quot;natural&quot; method of adding devices to a computer running a Plug and Play operating system is by plugging in the device first, letting Plug and Play find the device and automatically
 initiate an installation for that device. The device installation may then proceed using a driver package supplied with the OS, or a &quot;3rd-party&quot; driver package (supplied via CD-ROM, the Internet, or some other distribution mechanism). When the device installation
 is initiated by the addition of hardware, this is termed a &quot;hardware-first&quot; device installation.</p>
<p>Users may, however, take an alternate approach to adding hardware to their computer. In this scenario, they first run a setup program (perhaps launched as an autorun application when the vendor-supplied CD-ROM is inserted). This setup program may perform
 installation activities, and then prompt the user to insert their hardware. Upon the hardware's insertion, the vendor-supplied driver package (which was &quot;pre-installed&quot; by the setup program) is then found by Plug and Play, and the installation proceeds as
 in the hardware-first scenario. When the device installation is initiated by running a setup program, this is termed a &quot;software-first&quot; device installation. This approach to adding new hardware is just as valid as the hardware-first scenario, and some vendors
 may even instruct their users (via documentation that ships with the hardware) that this is the preferred method.</p>
<p>Vendors must support the hardware-first scenario (by providing a driver package that may be supplied to the &quot;Found New Hardware&quot; wizard with no &quot;pre-configuration&quot; performed by a setup program or other mechanism). Vendors may optionally support the software-first
 scenario as well, but the actual installation of the device instance is done by Plug and Play upon the device's arrival, as described above.</p>
<p>Vendors may also wish to perform additional activities as part of the device installation. For example, the vendor may want to allow the user to optionally install one or more applications that ship with the device (e.g., a scanner that ships with an image
 processing application). Such software is termed &quot;value-added software&quot;. Value-added software is distinct from the files that comprise the driver package because, unlike the core driver files, the device does not require value-added software to function properly.
 In the previous example of a scanner, for instance, perhaps the user already has an image processing application that they prefer. The user should be given the option of whether or not they want to install any value-added software. Additional activities (such
 as allowing the user to select value-added software offerings) may be accomplished by using a vendor-supplied device-specific co-installer.</p>
<p>The Windows Developer Preview Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available for the BUILD conference. These downloadable samples are provided as compressed
 ZIP files that contain a Visual Studio Express (BUILD release) solution (SLN) file for the sample, along with the code pages, assets, and metadata necessary to successfully compile and run the sample. For more information on the programming models, platforms,
 languages and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference provided in the Windows Developer Preview documentation available in the BUILD-specific version of the Windows Developer Center. This sample is not the final
 shipping version of the sample, and is provided &ldquo;as-is&rdquo; in order to indicate or demonstrate the early functionality of the programming models and feature APIs for a forthcoming version of Windows. Please provide feedback on this sample.</p>
<p><strong>Introduction to the Toaster Installation Sample</strong></p>
<p>This sample contains inf, toastapp, toastcd, toastco, and toastva directories. The following summary describes the directories and key files.</p>
<ul>
<li>inf--This directory contains the device INF (toastpkg.inf) that is used in device installation of toaster devices enumerated by the toaster bus enumerator. It registers a device-specific co-installer (tostrco2.dll) that provides additional functionality
 during the installation. Also contained in this directory is the autorun.inf file that would be placed in the root of the vendor's setup media (e.g., CD-ROM). This INF will launch the toastva.exe application (described below) when the media is inserted in
 an autorun-capable drive, or when the user double-clicks on the drive icon in Explorer. Additionally, this INF illustrates the [DeviceInstall] section that is a new feature introduced in Windows XP for autorun INF files: [DeviceInstall] DriverPath=\
<p>The DriverPath entry in a [DeviceInstall] section indicates to the Found New Hardware and Update Driver wizards that Plug and Play should search for device INFs in the specified directory only. This provides a performance improvement for media layouts that
 contain many subdirectories, since the default behavior is for Plug and Play to recursively search the entire CD. Additionally, specifying no DriverPath entry in this section indicates that the CD contains no drivers, thus Plug and Play can quickly determine
 that it can ignore this media. Absence of the [DeviceInstall] section will cause the media to be treated as it was in previous versions of Windows (e.g., as in Windows Me and Windows 2000, the entire media will be searched recursively for applicable INFs).</p>
</li><li>toastapp--This directory contains the source code for the toastapp.exe program that is used as value-added software that may optionally be installed by the user as part of device installation. This program is installed via the InstallShield(tm) (Microsoft
 Installer-based) setup program in the toastcd\ToastApp directory. </li><li>toastcd--This directory contains the CD image that would be used by the vendor to distribute the toaster package. In the root of the image, we have the autorun.inf which launches either the x86 or ia64 version of toastva.exe (depending on platform). Also
 in the root we have the device INF, toastpkg.inf, and the two associated catalogs, tostx86.cat and tostia64.cat, for the x86 and ia64 platforms respectively. Finally, in the root we also have the zero-length tag file toastpkg.tag, which is used to identify
 the media in case it must be prompted for. This tagfile is referenced in the SourceDisksNames sections of toastpkg.inf:
<p>[SourceDisksNames.x86]</p>
<p>1 = %DiskId1%, toastpkg.tag,,\i386</p>
<p>[SourceDisksNames.ia64]</p>
<p>1 = %DiskId1%, toastpkg.tag,,\ia64</p>
<p>The i386 and ia64 subdirectories contain the driver files that, along with the INF and CAT, constitute the driver package. These files are toaster.sys, tostrcls.dll, and tostrco2.dll. Their corresponding debug (.PDB) files are located there as well, although
 these aren&rsquo;t installed. Additionally, as mentioned above, the toastva.exe program (and its .PDB file) is located here. Note that this is not part of the driver package&mdash;it is merely used to facilitate software-first installation. Since this executable
 is not part of the driver package, it may be modified without affecting the digital signature of the package.</p>
<p>Finally, the ToastApp subdirectory contains the setup.exe program that installs the value-added software package, toastapp.exe. This setup.exe is a standalone package that contains the files it installs within this single binary. This is meant to represent
 an example of the kind of software that might be supplied to a vendor by an ISV for distribution with that vendor's hardware. While this particular setup program was authored with InstallShield(tm), other installation methods are equally valid. For example,
 the setup program could have been written using C/C&#43;&#43;, directly leveraging the Microsoft Installer (MSI) application programming interfaces (API).</p>
<p>The tostrco2.dll module is constructed so that there is no requirement on how the value-added software is installed--it simply creates a process to initiate the setup program, then waits for that process to terminate before continuing. For the sample, the
 description of the value-added software offerings and details of launching the value-added setup application are hard-coded, but in a real-world scenario, the value-added software supplied might be listed in some sort of data file (perhaps an INF file) and
 the value-added software selection page would be completely data driven, using the information contained in that data file. This would be advantageous, for example, because a vendor could submit just their core driver package for Windows Logo testing without
 any value-added software (and without this data file). The vendor could then construct setup media containing the core driver package plus value-added software as they see fit. The contents of their setup media could be modified at any time by adding or removing
 value-added software, and no changes to the core driver package would be required (hence, no need to re-submit for Windows Logo testing). This works because the data file containing details about the value-added software offerings isn't a part of the driver
 package, and thus can be changed without affecting that package. A co-installer DLL written to accommodate such a scheme could have default behavior that would not display the value-added software selection page in the absence of this data file.</p>
</li><li>toastco--This directory contains the co-installer, tostrco2.dll, which is used during device installation of toaster devices enumerated by the toaster bus enumerator. This co-installer supplies a finish-install wizard page that allows the user to select
 the value-added software, if any, that they wish to install. It does not supply a finish-install wizard page if the user has previously responded to the wizard page (whether or not they opted to install value-added software) so that the user isn't prompted
 multiple times in the case where they plug in multiple toasters (or the same toaster is plugged into multiple ports, which will appear to Plug and Play as different instances of that device).
<p>This DLL also supplies the same value-added software selection page for use by the toastva.exe setup program. This avoids code duplication and ensures a consistent user experience for the hardware-first and software-first scenarios.</p>
<p>Finally, this co-installer performs the same function as its version 1 counterpart (in the coinstaller directory under the main toaster directory). It provides a friendly name for the device that incorporates its serial number. However, this version retrieves
 the friendly name template from the INF, and since the template is stored in the [Strings] section, it may be localized for other languages.</p>
<p>The reason this co-installer&rsquo;s filename includes version number is to illustrate the technique that must be used when updating the driver for a device that may already have a previous version of the co-installer installed. If the name is not changed,
 then the old co-installer will be loaded early-on during the processing of DIF codes, and the updated version won&rsquo;t be able to &ldquo;get in the loop&rdquo; to perform its new actions when installing the device. Thus, co-installers should include a version
 number in their filename, so that the new version can be copied and can participate in the device installation.</p>
</li></ul>
<ul>
<li>toastva--This directory contains the autorun installation application, toastva.exe, which is launched when the user inserts the vendor-provided CD-ROM. This application is used to &quot;pre-install&quot; the necessary INF and catalog, along with any value-added software
 the user chooses. In addition, it uses the UpdateDriverForPlugAndPlayDevices API to update the existing installed drivers for any toasters that may already be present. Examples of scenarios where this would apply are:
<ul>
<li>The user previously plugged in a toaster, and there was no in-box support (i.e., no supporting driver was included with Windows) for it. The user didn't supply a vendor provided driver package at that time (perhaps because they didn't have the media), so
 no driver was installed for the device (this is sometimes referred to as a &quot;null driver install&quot;).
</li><li>The user previously plugged in a toaster, and there was in-box support for it (perhaps using an older or reduced-functionality driver).
</li><li>The user previously plugged in a toaster, and at that time supplied an older version of the driver provided by the vendor.
<p>In all the above cases, the UpdateDriverForPlugAndPlayDevices API will seek out any toaster device instances matching the specified hardware or compatible ID, and determine whether the specified driver package is &quot;better&quot; (i.e., lower rank or newer). If
 so, the device instance will be updated with the specified driver package.</p>
</li></ul>
</li></ul>
<h3>Operating System Requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows Developer Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server Developer Preview </dt></td>
</tr>
</tbody>
</table>
<h3>Build the Sample</h3>
<p>You can build the sample in two ways: using Visual Studio or the command line (MSBuild).</p>
<p><strong>Building a Driver Using Visual Studio</strong></p>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Win8-Debug and Win32. In previous versions of the WDK, this build configuration would correspond to building a driver using the Windows 8 x86 Checked Build Environment.</p>
<p class="proch"><strong>To select a configuration and build a driver</strong></p>
<ol>
<li>Open the driver project or solution in Visual Studio. </li><li>Right-click the solution in the Solutions Explorer and select Configuration Manager.
</li><li>From the Configuration Manager, select the Active Solution Configuration (for example, Win8-Debug or Win8-Release) and the Active Solution Platform (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click Build Solution (Ctrl&#43;Shift&#43;B). </li></ol>
<p><strong>Building a Driver Using the Command Line (MSBuild)</strong></p>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><strong>To build a driver using the Visual Studio Command Prompt window</strong></p>
<ol>
<li>Open a Visual Studio Command Prompt window. Click Start, point to All Programs, point to Microsoft Visual Studio, point to Visual Studio Tools, and then click Visual Studio Command Prompt. From this window you can use MsBuild.exe to build any Visual Studio
 project by specifying the project (.VcxProj) or solutions (.Sln) file. </li><li>Navigate to the project directory and enter the MSbuild command for your target. For example, to perform a clean build of a Visual Studio driver project called MyDriver.vcxproj, navigate to the project directory and enter the following MSBuild command:
 msbuild /t:clean /t:build .\MyDriver.vcxproj. </li></ol>
<h3>Run the Sample</h3>
<p>The sample can be run in two different ways, corresponding to the hardware-first and software-first scenarios.</p>
<p><strong>Hardware-First Device Installation Walk-Through</strong></p>
<p>Simulate the plugging in of a toaster by typing the following at a command prompt (see the documentation for the toaster sample driver for more information on setting up the toaster bus enumerator and using the &quot;enum&quot; application to simulate plugging and
 unplugging of toaster devices): C:\&gt;enum -p 1</p>
<p>Since there is no in-box driver for this device, the Found New Hardware wizard will appear, prompting for a driver package. If you constructed the setup image on autorun-capable media (e.g., writeable CD or DVD), you may insert that media in the drive, and
 Windows XP will automatically initiate a driver search on that media. On Windows 2000, you must click &quot;Next&quot; to initiate the driver search on that media. If you constructed the setup image in a directory on your hard disk, then you must click the &quot;Install
 from a list or specific location (Advanced)&quot; radio button, then click &quot;Next&quot;. On the next page, click the &quot;Include this location in the search:&quot; checkbox, and type the path to the directory. Clicking &quot;Next&quot; will then initiate a driver search in the directory
 you specified.</p>
<p>The install will proceed using the toastpkg.inf file in the root of the install location. The co-installer, tostrco2.dll, will assign the device a friendly name during DIF_INSTALLDEVICE, and will supply a value-add software selection wizard page in response
 to DIF_NEWDEVICEWIZARD_FINISHINSTALL. This page will only be supplied if the user hasn&rsquo;t been given the option previously of selecting value-added software (of if they&rsquo;ve since uninstalled said software). This allows subsequent toasters to be installed
 server-side, without any user interaction whatsoever (even if a non-admin user or no user is logged in).</p>
<p><strong>Software-First Device Installation Walk-Through</strong></p>
<p>If you constructed the setup image on auto-run capable media (e.g., writeable CD or DVD), you can simply insert the media, and the toastva.exe program will automatically launch. Note that toastva.exe suppresses itself when invoked while a device installation
 is in progress, which is why this application doesn&rsquo;t appear during the hardware-first scenario described above.</p>
<p>After the Welcome page, the wizard will attempt to update the driver for any existing toaster devices (via the UpdateDriverForPlugAndPlayDevices) API. If there aren&rsquo;t any, it will instead &ldquo;pre-install&rdquo; the INF using the SetupCopyOEMInf
 API so that Plug and Play will automatically find a match when the hardware is subsequently inserted. SetupCopyOEMInf also stores away information that allows Plug and Play to know where the INF came from, thus allowing the Found New Hardware wizard to prompt
 for media in the correct location if it ever needs to. In this scenario, it is presumed that the user will plug in their toaster upon completion of the wizard, when they&rsquo;re instructed to do so. As such, they should never actually be prompted to reinsert
 the CD, since they wouldn&rsquo;t have removed it. However, if they had removed it, Plug and Play would prompt them to reinsert it. It is possible to &ldquo;pre-install&rdquo; all the driver files as well so that the user would never be prompted, even if they
 did remove the CD. The Windows XP DDK has a discussion of this technique in the section titled &ldquo;Writing a Device Installation Application&rdquo; in the Device Installation chapter.</p>
<p>Next, the wizard displays the same value-add software selection page as the user would receive if they&rsquo;d performed a hardware-first install, as described above. After clicking &ldquo;Next&rdquo; on this page, any selected applications are installed
 (the wizard is hidden while these setup processes run), then the Finish wizard page appears. If the wizard didn&rsquo;t find any devices during its call to UpdateDriverForPlugAndPlayDevices, it prompts the user to plug in their hardware now. When the user
 does plug in the hardware (i.e., by typing &ldquo;enum &ndash;p 1&rdquo; as described above), then the install will proceed without requiring any user interaction.</p>
