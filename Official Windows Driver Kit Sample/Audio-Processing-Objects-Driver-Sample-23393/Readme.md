# Audio Processing Objects Driver Sample
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
* 2013-06-25 10:21:56
## Description

<div id="mainSection">
<p>The audio processing object (APO) driver sample is a collection of two driver samples that demonstrate the way to register and unregister audio processing objects, and also show how to customize a Control Panel property page to reflect the available features
 in the processing object. The sample applies to both local effects (LFX) and global effects (GFX) audio processing objeccts.
</p>
<p>This sample collection contains the following driver samples:
<table>
<tbody>
<tr>
<th>Sample</th>
<th>Desscription</th>
</tr>
<tr>
<td>APO</td>
<td>Shows how to register and initialize an APO, and also how to unregister it and delete the buffers that it was using.
</td>
</tr>
<tr>
<td>PropPageExtensions</td>
<td>Shows how to detect the current states of any installed APOs (enabled or disabled), and how to update the audio Control Panel progerty page with the retrieved information.</td>
</tr>
</tbody>
</table>
</p>
<p>The PropPageExtensions sample works with audio endpoints to enable and disable an APO. For more information about audio processing objects and audio endpoints, see
<a href="audio.system_effects_audio_processing_objects">System Effects Audio Processing Objects</a> and
<a href="audio.audio_endpoints__properties_and_events">Audio Endpoints, Properties and Events</a>.</p>
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
<h3>Run the sample</h3>
<p>In order to configure driver signing and deployment, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
</div>
