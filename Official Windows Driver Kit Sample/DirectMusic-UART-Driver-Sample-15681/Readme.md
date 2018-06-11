# DirectMusic UART Driver Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Audio
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:45:43
## Description

<div id="mainSection">
<p>The DirectMusic UART (DMusUART) sample driver is a <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536341">
DMus miniport driver</a>. It provides applications that use DirectMusic® or the Microsoft® Windows® multimedia midiInXxx and midiOutXxx functions with an interface to the MPU-401 chip.
</p>
<p>The DMus port driver, which is built into PortCls.sys, performs the generic KS filter functions for this miniport driver and connects it to the rest of the system. The sample driver handles the following:</p>
<ul>
<li>
<p>One input stream visible to either DirectMusic or the midiInXxx functions</p>
</li><li>
<p>One output stream visible to the midiOutXxx functions </p>
</li><li>
<p>Multiple output streams visible to DirectMusic</p>
</li></ul>
<p></p>
<p>The sample driver compiles for both 32- and 64-bit environments. The PortCls system driver automatically handles Plug and Play and power management on behalf of the miniport driver.</p>
<p><b>RESOURCES</b> </p>
<p>The DMusUART sample driver is identical to the system-supplied DMusUART miniport driver in
<i>PortCls.sys</i>. The source code for this sample can serve as a starting point for developers who need to write simple DMus miniport drivers. (For more complex DirectMusic devices, including those that have DLS functionality, see DirectMusic software synthesizer
 sample. This is a DirectMusic kernel-mode software synth sample.) The DMusUART miniport driver interfaces to the DMus port driver in
<i>PortCls.sys</i>.</p>
<p>In addition to the DMus port driver, <i>PortCls.sys</i> contains a MIDI port driver that provides basic MIDI functions but lacks support for the advanced MIDI features in DirectMusic. The DMus port driver provides higher functionality than the MIDI port
 driver, so if you are creating your own miniport driver, consider interfacing to DMus instead of MIDI. A DMus miniport driver has the following advantages:
</p>
<ul>
<li>
<p>A DMus miniport driver is visible to the DirectMusic API, unlike a MIDI miniport driver, which is not</p>
</li><li>
<p>A DMus miniport driver is visible to the older, Windows multimedia midiInXxx and midiOutXxx functions if the driver includes a legacy pin descriptor</p>
</li></ul>
<p></p>
<p>In short, DMus miniport drivers have the benefits of both backward compatibility and advanced functionality.</p>
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
<td><dt>Windows&nbsp;8.1 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>. If the build succeeds, it produces the
<i>dmusuart.lib</i> library module, which can be linked into an audio adapter driver.</p>
<h2>Run the sample</h2>
<p>In order to configure driver signing and deployment, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
</div>
