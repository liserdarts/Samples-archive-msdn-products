# KMDF filter driver for a HID device
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* hid
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:24:31
## Description

<div id="mainSection">
<p>Firefly is a KMDF-based filter driver for a HID device. Along with illustrating how to write a filter driver, this sample shows how to use remote I/O target interfaces to open a HID collection in kernel-mode and send IOCTL requests to set and get feature
 reports, as well as how an application can use WMI interfaces to send commands to a filter driver.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539952">Human Input Devices Design Guide</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539956">Human Input Devices Reference</a>
</dt></dl>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff540774">Creating Framework-based HID Minidrivers</a>
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
<p>For information on how to build a driver using Microsoft Visual Studio, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>. When you build the sample, MSBuild.exe creates luminous.lib, firefly.sys, flicker.exe, and sauron.dll. Copy these files as well as the KMDF coinstaller (wdfcoinstallerMMmmm.dll) and the INF file (firefly.inf) to a floppy disk or a temporary
 directory on the target system.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; </p>
<p class="note">You can obtain redistributable framework updates by downloading the
<b>wdfcoinstaller.msi</b> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>. This package performs a silent install into the directory of your Windows Driver Kit (WDK) installation. You will see no confirmation that the installation has completed. You can verify that the redistributables have been
 installed on top of the WDK by ensuring there is a redist\wdf directory under the root directory of the WDK, %ProgramFiles(x86)%\Windows Kits\8.0.</p>
<p></p>
<h2><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>To install the driver on Windows&nbsp;7 and later operating systems: </p>
<ol>
<li>Plug the Microsoft USB Optical mouse into your target machine and verify that the mouse works. The drivers for this mouse come with the operating system so the device will start working automatically when you plug in.
</li><li>You may need to make Group Policy changes in order to replace the existing mouse driver. If you are unable to perform steps 3-9, do the following:
<ol>
<li>Open <b>gpedit.msd</b>. </li><li>In the Group Policy Object Editor navigation pane, open the Computer Configuration folder. Then open Administrative Templates, open System, open Device Installation, and then open Device Installation Restrictions.
</li><li>Enable <i>Prevent installation of devices not described by other policy settings</i>. This will prevent Windows from automatically installing the default mouse driver so that you can then install Firefly.
</li><li>Enable <i>Allow administrators to override device installation policy</i>. This will allow you to bypass the &quot;&quot;The installation of this device is forbidden by system policy&quot; error that you may otherwise receive when you attempt to install Firefly.
</li><li>You may need to reboot. </li></ol>
</li><li>Bring up the Device Manager (type <b>devmgmt.msc</b> in the Start/Run window and press enter).
</li><li>Find the Microsoft Optical mouse under &quot;Mice and other pointing devices&quot; </li><li>Right click on the device and choose &quot;Update Driver Software.&quot; </li><li>Select &quot;Browse my computer for driver software.&quot; </li><li>Browse to the temporary folder you created earlier. Click Next. Click through the warning.
</li><li>You will see &quot;Windows has successfully updated your driver software&quot; for the &quot;Shiny Things Firefly Mouse&quot; device.
</li><li>The system will copy all the files and restart the mouse device to install the upper filter. Click Close and you are ready to run the test app.
</li></ol>
<p></p>
<h2><a id="_______Testing_the_Sample______"></a><a id="_______testing_the_sample______"></a><a id="_______TESTING_THE_SAMPLE______"></a>Testing the Sample
</h2>
<p>Copy the flicker.exe to the target machine and run it from an elevated command prompt. The usage is:</p>
<p>Usage: Flicker &lt;-0 | -1 | -2&gt;</p>
<p>-0 turns off light</p>
<p>-1 turns on light</p>
<p>-2 flashes light</p>
<p>The following description applies to Windows Media Player 12 running on Windows&nbsp;7:</p>
<p class="proch"><b>Testing the DLL</b></p>
<ol>
<li>Copy the sauron.dll to the Windows Media player Visualization directory (C:\Program Files\Windows Media Player\Visualizations).
</li><li>Register the DLL with COM by calling &quot;regsvr32 sauron.dll&quot; in command shell. </li><li>Run Windows Media Player <i>as administrator</i>. </li><li>Click on the &quot;Switch to Now Playing&quot; button in the lower right of the application.
</li><li>Right click, select Visualizations, and you will see a menu item called &quot;Sauron.&quot;
</li><li>Choose either Firefly Bars or Firefly Flash and play some music. </li><li>You will see the mouse light dancing to the tune of the music. </li><li>You can unregister the DLL by calling &quot;regsvr32 -u sauron.dll&quot;. </li></ol>
<h2><a id="Programming_Tour"></a><a id="programming_tour"></a><a id="PROGRAMMING_TOUR"></a>Programming Tour</h2>
<p>The Firefly sample is installed as an upper filter driver for the Microsoft USB Intellimouse Optical. An application provided with the sample can cause the light of the optical mouse to blink by sending commands to the filter driver using the WMI interface.</p>
<p>The sample consists of: </p>
<ul>
<li>Driver (firefly.sys): The Firefly driver is an upper device filter driver for the mouse driver (mouhid.sys). Firefly is a generic filter driver based on the toaster filter driver sample available in the WDK. During start device, the driver registers a WMI
 class (FireflyDeviceInformation). The user mode application connects to the WMI namespace (root\wmi) and opens this class using COM interfaces. Then the application can make requests to read (&quot;get&quot;) or change (&quot;set&quot;) the current value of the TailLit data value
 from this class. In response to a set WMI request, the driver opens the HID collection using IoTarget and sends
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff541092"><b>IOCTL_HID_GET_COLLECTION_INFORMATION</b></a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff541089"><b>IOCTL_HID_GET_COLLECTION_DESCRIPTOR</b></a> requests to get the preparsed data. The driver then calls
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539715"><b>HidP_GetCaps</b></a> using the preparsed data to retrieve the capabilities of the device. After getting the capabilities of the device, the driver creates a feature report to set
 or clear the feature that causes the light to toggle. </li><li>Library (luminous.lib): The sources for this file are located in the WINDDK\src\hid\firefly\lib folder. You will need to build the library before using it. This library is shared by the WDM and WDF samples. All the interfaces required to access the WMI
 is defined in this library and exposed as CLuminous class. </li><li>Application (flicker.exe): The sources for this file are located in the WINDDK\src\hid\firefly\app folder. You will need to build the application before using it. This application is shared by the WDM and WDF samples. The application links to luminous.lib
 to open the WMI interfaces and send set requests to toggle the light. </li><li>Sauron (sauron.dll): The sources for this file are located in the WINDDK\src\hid\firefly\sauron folder. You will need to build this dll before using it. The library is shared by the WDM and WDF samples. Sauron is a Windows Media Player visualization DLL,
 and is based on a sample from the Windows Media Player SDK kit. By using this DLL, you can cause the mouse lights to blink to the beats of the music.
</li></ul>
<p></p>
</div>
