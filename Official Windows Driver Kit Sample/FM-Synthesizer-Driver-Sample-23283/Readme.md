# FM Synthesizer Driver Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Audio
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:10:34
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
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>. If the build succeeds, it produces the
<i>fmsynth.lib</i> library module, which can be linked into an audio adapter driver.</p>
<h3>Run the sample</h3>
<p>In order to configure driver signing and deployment, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
</div>
