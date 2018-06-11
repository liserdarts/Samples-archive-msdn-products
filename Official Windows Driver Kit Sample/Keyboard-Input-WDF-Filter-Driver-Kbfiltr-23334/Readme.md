# Keyboard Input WDF Filter Driver (Kbfiltr)
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* input
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:17:38
## Description

<div id="mainSection">
<p>The Kbdfltr sample is an example of a keyboard input filter driver. </p>
<p>This sample is WDF version of the original WDM filter driver sample. The WDM version of this sample has been deprecated.</p>
<p>This is an upper device filter driver sample for PS/2 keyboard. This driver layers in between the KbdClass driver and i8042prt driver and hooks the callback routine that moves keyboard inputs from the port driver to class driver. In its current state, it
 only hooks into the keyboard packet report chain, the keyboard initialization function, and the keyboard ISR, but does not do any processing of the data that it sees. (The hooking of the initialization function and ISR is only available in the i8042prt stack.)
 With additions to this current filter-only code base, the filter could conceivably add, remove, or modify input as needed.
</p>
<p>This sample also creates a raw PDO and registers an interface so that applications can talk to the filter driver directly without going through the PS/2 devicestack. The reason for providing this additional interface is because the keyboard device is an
 exclusive secure device and it's not possible to open the device from usermode and send custom ioctls through it.
</p>
<p>This driver filters input for a particular keyboard on the system. If you want to filter keyboard inputs from all the keyboards plugged into the system, you can install this driver as a class filter below the KbdClass filter driver by adding the service
 name of this filter driver before the KbdClass filter in the registry at: </p>
<p><code>HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4D36E96B-E325-11CE-BFC1-08002BE10318}\UpperFilters</code>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
<p>Once built, the sample produces <i>Kbfiltr.sys</i> and <i>Kbftest.exe</i>.</p>
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>This sample is installed via an .inf file. The .inf file included in this sample is designed to filter a PS/2 keyboard.
</p>
<p>The .inf file must install the class driver (Kbdclass) and the port driver (i8042prt, Kbdhid, etc.) by using Keyboard.inf and the INF directives &quot;Needs&quot; and &quot;Include&quot;.</p>
<p>The .inf file must add the correct registry values for the class and port driver, as well as using the new directives.</p>
<p>To install this driver as a class filter, you have to use registry APIs to directly update the registry with an installer and reboot your machine.</p>
</div>
