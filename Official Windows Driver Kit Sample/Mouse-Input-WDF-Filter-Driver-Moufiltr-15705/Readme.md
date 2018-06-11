# Mouse Input WDF Filter Driver (Moufiltr)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* input
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:47:25
## Description

<div id="mainSection">
<p>The Moufiltr sample is an example of a mouse input filter driver. </p>
<p>This sample is WDF version of the original WDM filter driver sample. The WDM version of this sample has been deprecated.</p>
<p>This driver filters input for a particular mouse on the system. In its current state, it only hooks into the mouse packet report chain and the mouse ISR, and does not do any processing of the data that it sees. (The hooking of the ISR is only available in
 the i8042prt stack.) With additions to this current filter-only code base, the filter could conceivably add, remove, or modify input as needed.
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>This sample is installed via an .inf file. The .inf file included in this sample is designed to filter a PS/2 mouse.
</p>
<p>The .inf file must install the class driver (Mouclass) and the port driver (i8042prt, Mouhid, Sermouse, etc.) by using Msmouse.inf and the INF directives &quot;Needs&quot; and &quot;Include&quot;.</p>
<p>The .inf file must add the correct registry values for the class and port driver, as well as using the new directives.</p>
</div>
