# DirectMusic software synthesizer sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* Audio
* Windows Driver
## IsPublished
* False
## ModifiedDate
* 2012-05-31 01:36:03
## Description

<div id="mainSection">
<div class="clsServerSDKContent">
<h1><a id="gallery_samples.examplegallerysample"></a>DirectMusic software synthesizer sample</h1>
</div>
<p class="CCE_Message">[This documentation is preliminary and is subject to change.]</p>
<p>This sample contains working source code for a kernel-mode software synthesizer that plugs into the Microsoft® DirectMusic® and WDM Audio architectures.
</p>
<p>You can use this sample code to start building your own software synthesizer. Modify the sample to add your own features.If you would like to start with a less complex driver sample that also uses the DirectMusic® architecture, see DirectMusic UART driver
 sample, and refer to <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff536261">
DirectMusic DDI Overview</a>.</p>
<p>If you ship your synthesizer to customers, be sure to use the Guidgen program to create a Globally Unique ID (GUID) for your synthesizer so it won't interfere with other synthesizers. Set CLSID_DDKWDMSynth at the top of
<i>private.h</i> and <i>ddksynth.inf</i> to be your GUID. You should also set the text in
<i>miniport.cpp</i>, <i>ddksynth.rc</i> and <i>ddksynth.inf</i> to describe your synthesizer.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This is a software synthesizer sample, but it is also relevant as a hardware driver sample. If you are writing a hardware driver, you must remember the following:</p>
<ul>
<li>
<p>Do not implement the wave pin</p>
</li><li>
<p>Do not implement a synthsink and do not depend on Render being called from the port (the audio hardware should notify the miniport if it needs additional work done at buffer-rendering time)</p>
</li><li>
<p>Use category RENDER instead of DATATRANSFORM</p>
</li></ul>
<p></p>
<p></p>
<p><b>Creating a User-mode Synthesizer</b> </p>
<p>Although this sample doesn't show you how to create a user-mode synthesizer, you can start with the user-mode sample to learn how synthesis and DLS downloads work. We recommend that you try out your ideas and get things working in user-mode before moving
 to kernel mode. This approach might be easier and save a bit of debugging time.</p>
<p><b>Testing the Synthesizer</b> </p>
<p>To test the synthesizer, open DirectMusic Producer and open the Port Configuration window. (You can do this by right-clicking the button showing a 1 or 2 with a sound wave behind it on the Transit Controls toolbar.) The port name dropdown will now contain
 the option Microsoft DDK Synthesizer (WDM) in addition to the Microsoft Synthesizer (WDM) option. Set one of the configurations to use the Microsoft DDK Synthesizer port.</p>
<p>Then, when you play music through that configuration, it will be played by the synthesizer you built from the DDK sample. The sound and capabilities of the DDK sample -- Microsoft DDK Synthesizer (WDM) -- are virtually identical to the kernel-mode DirectMusic
 synthesizer -- Microsoft Synthesizer (WDM). One major exception is that the DDK sample synthesizer does not support reverb.</p>
<p><b>Supported Configurations</b> </p>
<p>Plug and Play as well as Power Management are supported by PortCls on behalf of the synthesizer.
</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;8 Release Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff554644">Building a Driver</a>.</p>
</div>
