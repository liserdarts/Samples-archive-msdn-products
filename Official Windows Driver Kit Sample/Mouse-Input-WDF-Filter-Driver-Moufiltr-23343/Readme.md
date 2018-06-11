# Mouse Input WDF Filter Driver (Moufiltr)
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
* 2013-06-25 10:18:10
## Description

<div id="mainSection">
<p>The Moufiltr sample is an example of a mouse input filter driver. </p>
<p>This sample is WDF version of the original WDM filter driver sample. The WDM version of this sample has been deprecated.</p>
<p>This driver filters input for a particular mouse on the system. In its current state, it only hooks into the mouse packet report chain and the mouse ISR, and does not do any processing of the data that it sees. (The hooking of the ISR is only available in
 the i8042prt stack.) With additions to this current filter-only code base, the filter could conceivably add, remove, or modify input as needed.
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>This sample is installed via an .inf file. The .inf file included in this sample is designed to filter a PS/2 mouse.
</p>
<p>The .inf file must install the class driver (Mouclass) and the port driver (i8042prt, Mouhid, Sermouse, etc.) by using Msmouse.inf and the INF directives &quot;Needs&quot; and &quot;Include&quot;.</p>
<p>The .inf file must add the correct registry values for the class and port driver, as well as using the new directives.</p>
</div>
