# Audio Adapters Samples
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
* 2014-04-02 12:44:26
## Description

<div id="mainSection">
<p>The Audio Adapters sample contains the Stdunk and MPU-401 driver samples. The Stdunk sample shows how to develop a generalized implementation of an audio driver interface. The MPU-401 sample is an adapter sample for the MPU-401 device.
</p>
<p><b>Stdunk Sample</b> </p>
<p>The Standard IUnknown and CUnknown (Stdunk) sample is the standard implementation of the IUnknown interface and the CUnknown class.
</p>
<p>The Stdunk sample code is not included as an example that you can modify, like the other audio code samples, but simply as code that is required to build any Microsoft Windows Driver Model (WDM) audio driver. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536400">Getting Started with WDM Audio Drivers</a>.</p>
<p><b>MPU-401 Sample</b> </p>
<p>The MPU-401 driver sample is a Microsoft Windows Driver Model (WDM) driver that supports the standard functions in an MPU-401 device.
</p>
<p><b>Theory of Operation</b> </p>
<p>The MPU-401 driver sample, msmpu401.sys, controls audio devices in which the MPU-401 hardware is on a devnode that is separate from other audio functionality. If the MPU-401 hardware shares a devnode with other subdevices (for example, the PCM wave renderer
 in the Sound Blaster 16 device), the system-supplied DMus miniport driver in portcls.sys can still be used, but you must writer an adapter driver that handles multiple subdevices.</p>
<p>If the system-supplied DMus miniport driver is insufficient, you can write a custom miniport driver to support any additional functionality. The Windows Driver Kit (WDK) contains a number of miniport driver samples that you can refer to:</p>
<ul>
<li>
<p>The DirectMusic UART driver sample, DirectMusic software synthesizer sample, and FM synthesizer driver sample show how to build miniport drivers that interface to the DMus and MIDI port drivers.</p>
</li><li>
<p>The AC97 driver sample includes a custom wave miniport driver.</p>
</li><li>
<p>The DirectMusic software synthesizer sample and the AC97 driver sample contain the code that you need to connect your custom miniport driver to the port driver.</p>
</li></ul>
<p></p>
<p>If you choose to write a custom miniport driver, try to follow the sample code as closely as possible. In this way, you can avoid unnecessary complexity by taking advantage of the system-supplied driver support in portcls.sys.</p>
<p>Because the driver is a PortCls client, it is compliant with the Windows requirements for Plug and Play (PnP) and power management.</p>
<p><b>Implementation and Design</b> </p>
<p>The MPU-401 driver sample is extremely small because it leverages driver support that is already built into the PortCls system driver module, portcls.sys. The sample driver simply creates instances of the appropriate port and miniport drivers, which already
 exist in portcls.sys, and binds them together. For more information about PortCls, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff536829">Introduction to Port Class</a>.</p>
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
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p>In order to configure driver signing and deployment, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554651(v=vs.85).aspx">
Developing, Testing, and Deploying Drivers</a>.</p>
</div>
