# PCMCIA Smart Card Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* Smart Card
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:50:34
## Description

<div id="mainSection">
<p>The PCMCIA Smart Card Driver is used for the SCM PCMCIA smart card reader. This driver is written using Kernel-Mode Driver Framework.
</p>
<p>This driver in its original form was written in WDM. It was converted to KMDF to take advantage of all the benefits provided by KMDF in terms of reducing complexity and making it robust. Since this driver still needs to work with the existing smartcard library
 to handle smartcard specific processing, the driver is not restricted to using only KMDF interfaces. Escaping out of KMDF is necessary for processing I/O requests to get the underlying IRPs and provide that to the smartcard library. The driver also uses advanced
 IRP handling techniques to work around the limitations imposed by the smartcard library. Except for this quirk, the driver is a fully functional KMDF driver. As a sample, it also makes it easier to adapt this driver for USB devices since KMDF has good support
 for interfacing with USB devices. </p>
<p>Power Management is described in detail in the WDK documentation. There is, however, one situation that is specific to smart card readers: how to deal with smart card insertions and removals while the system is in standby or hibernation mode.</p>
<p>A card reader will not see any card insertion or removal events in these modes, because the bus might not even have power. The card state must be saved before the reader goes into standby or hibernation mode. After the system returns from these modes, it
 is necessary to determine what the state of the card is. Card tracking calls must complete whenever there was a card in the reader before standby or hibernation mode or whenever there is a card in the reader after these modes. This step is necessary because
 the user could have changed the card while the system was in a low-power mode. </p>
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
<p>The PSCR.SYS driver is included in-box in Windows or, starting with Windows&nbsp;8.1, available from Windows Update. Therefore, when the SCM 488 PCMCIA reader is inserted, the system will automatically install the driver. However, if you want to customize the
 source code of this driver and replace the in-box driver or the driver from Windows Update with your driver, use the supplied INF file.</p>
<h2><a id="Tools"></a><a id="tools"></a><a id="TOOLS"></a>Tools</h2>
<p>Microsoft offers a test tool (Ifdtest.exe) that allows you to use a smart card reader directly from the command line. Normally, the smart card resource manager is connected to a reader. To use Ifdtest.exe, you must stop the smart card resource manager (Scardsvr.exe)
 by typing net stop scardsvr at the command line. Ifdtest.exe is also used for the smart card reader logo test.
</p>
<p>The driver will not unload as long as you have Ifdtest.exe running and connected to the driver.</p>
<h2><a id="Resources"></a><a id="resources"></a><a id="RESOURCES"></a>Resources</h2>
<p>ISO 7816 Part 3 describes smart cards and smart card protocols in detail. Refer to the PC99 Handbook for smart card reader requirements.
</p>
<p>For more information about Windows smart card reader drivers, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/">
Smart Card Reader Devices Design Guide</a>.</p>
</div>
