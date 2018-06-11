# FM Synthesizer Driver Sample
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
* 2014-04-02 12:46:13
## Description

<div id="mainSection">
<p>The FMSynth sample driver is a MIDI miniport driver. The sample driver provides applications that use the Microsoft® Windows® multimedia midiOutXxx functions with an interface to a device that implements OPL3-style FM synthesis. Because the sample is a synthesizer,
 it handles MIDI output only. </p>
<p>The MIDI port driver, which is built into <i>PortCls.sys</i>, serves as a wrapper for this miniport driver. Because the sample driver is a PortCls client, PortCls handles Plug and Play and power management on behalf of the sample driver.</p>
<p>There are two GUIDs with which an adapter driver can instantiate this miniport driver:</p>
<ul>
<li>
<p>CLSID_MiniportDriverFmSynth</p>
</li><li>
<p>CLSID_MiniportDriverFmSynthWithVol</p>
</li></ul>
<p></p>
<p>The first is the normal mode, which is used by a vast majority of clients. However, if the audio device does not provide an analog volume control in the data path following the FM synthesizer (for example, the WSS device lacks a volume control), then the
 second GUID, which implements an FM synth with a built-in volume control, can be used instead.</p>
<p><b>RESOURCES</b> </p>
<p>The FMSynth sample driver is identical to the system-supplied FMSynth miniport driver in
<i>PortCls.sys</i>. The source code for this sample can serve as a starting point for developers who need to augment it, as well as for those who need to write completely new MIDI miniport drivers.</p>
<p>This sample is a miniport driver that interfaces to the MIDI port driver, which is available in all versions of PortCls.sys. The MIDI port driver lacks support for the advanced MIDI features in DirectMusic®, which are supported by the DMus port driver. If
 you are creating your own miniport driver, consider the advantages of interfacing to DMus instead of MIDI:</p>
<ul>
<li>
<p>The DMus port driver provides higher functionality than the MIDI port driver.</p>
</li><li>
<p>DMus miniport drivers are visible to DirectMusic applications, unlike MIDI miniport drivers. In addition, DMus miniport drivers are still visible to the older, Windows multimedia midiXxx functions.</p>
<p>In short, DMus miniport drivers have the benefits of backward compatibility and advanced functionality. If you want to work with DMus miniport drivers, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536341">DMus Miniport Driver</a> and the DirectMusic UART driver sample.</p>
</li></ul>
<p></p>
<p>For more information about PortCls, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536829">
Introduction to Port Class</a>.</p>
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
<i>fmsynth.lib</i> library module, which can be linked into an audio adapter driver.</p>
<h2>Run the sample</h2>
<p>In order to configure driver signing and deployment, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
</div>
