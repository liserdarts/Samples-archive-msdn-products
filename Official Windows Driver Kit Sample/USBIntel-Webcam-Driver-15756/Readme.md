# USBIntel Webcam Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* WDM
* Windows Driver
## Topics
* WDK
* videocap
## IsPublished
* False
## ModifiedDate
* 2012-02-29 04:33:24
## Description

<h3>USBIntel Webcam Driver</h3>
<p>The USBIntel Webcam Driver sample is a Microsoft Windows Driver Model (WDM) stream class video capture driver.
</p>
<p><b>Theory of Operation</b> </p>
<p>USBIntel supports two Intel Universal Serial Bus (USB) digital cameras:</p>
<ul>
<li>Intel Create &amp; Share USB camera, model number YC76 </li><li>Intel Create &amp; Share USB camera, model number YC72 </li></ul>
<p></p>
<p>The main difference between the two camera models is the snapshot button for acquiring still images. This button is available only on the newer model (YC76).</p>
<p>The digital camera that Usbintel.sys supports is a data source that produces digital image data without any other input connection. This camera appears in a Microsoft DirectShow graph as a WDM Streaming Capture Device and as a capture filter with an output
 capture stream that supports image sizes of 320 x 240, 160 x 120, and 176 x 144 pixels, with IYUV/I420 color space. The camera's decompressor, IYUV_32.dll, is part of the operating system, and it can convert the image data format from IYUV to RGB16 or RGB8,
 or to a Microsoft DirectDraw surface if the video card supports IYUV format.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;2000 Professional </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2003 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<h3>Run the sample</h3>
<h3><a name="implementation_and_design"></a>Implementation and Design</h3>
<p>The USBIntel driver requires a helper kernel library (Usbcamd.sys or, beginning with Windows&nbsp;2000, Usbcamd2.sys) in order to work. For more information, see the WDK documentation topic,
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568621">USBCAMD Minidriver Library</a>.</p>
<p>In the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff558717">
<b>DriverEntry</b> </a>function, the driver initializes the hardware initialization structure
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff559682"><b>HW_INITIALIZATION_DATA</b>
</a>and registers its entry point functions.</p>
<p>The <b>HwReceivePacket</b> member of <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff559682">
<b>HW_INITIALIZATION_DATA</b> </a>describes the entry point for receiving Stream Request Packet (SRBs) from a stream class driver. The USBIntel driver might receive the following sequence of SRBs:
</p>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568185"><b>SRB_INITIALIZE_DEVICE</b>
</a></dt><dd>
<p>Initialize the device. This SRB is called after <b>DriverEntry</b>.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568173"><b>SRB_GET_STREAM_INFO</b>
</a></dt><dd>
<p>Get the supported stream format.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568168"><b>SRB_GET_DATA_INTERSECTION</b>
</a></dt><dd>
<p>Query a supported format that some key fields use.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568191"><b>SRB_OPEN_STREAM</b>
</a></dt><dd>
<p>Open the device. USBCAMD handles this SRB.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568165"><b>SRB_CLOSE_STREAM</b>
</a></dt><dd>
<p>Close the open stream. In USBCAMD, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568215">
<b>SRB_UNINITIALIZE_DEVICE</b> </a>handles this SRB to indicate that a device has been unloaded or removed.</p>
</dd></dl>
<p></p>
<h3><a name="limitations"></a>Limitations</h3>
<p>If your hardware supports USB-based video capture devices, consider using the system-supplied
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568651">USB Video Class (UVC) driver</a>. Microsoft&nbsp;Windows support for USB Video Class is described in the WDK documentation.</p>
<p>The USBIntel sample compiles on Windows&nbsp;98 and later versions of the operating system on 32-bit platforms.</p>
<h3><a name="installation_notes"></a>Installation notes</h3>
<p>USBIntel supports Plug and Play (PnP) and uses the Image.inf file that is part of the Windows operating system to install detected devices. You cannot manually install detected devices. You can use the Usbintel.inf file, which is a subset of Image.inf, as
 an installation file template. This file illustrates the registry modifications needed to make the minidriver visible to other WDM and kernel streaming (KS) components.</p>
<h3><a name="code_tour"></a>Code tour</h3>
<p><b>File manifest</b> </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Camqi.c</td>
<td>
<p>PnP routines</p>
</td>
</tr>
<tr>
<td>Intelcam.c</td>
<td>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff558717"><b>DriverEntry</b>
</a>and device initialization and uninitialization functions</p>
</td>
</tr>
<tr>
<td>Intelcam.rc</td>
<td>
<p>Resource file</p>
</td>
</tr>
<tr>
<td>Prpget.c</td>
<td>
<p>Handles all get property functions</p>
</td>
</tr>
<tr>
<td>Prpmanf.c </td>
<td>
<p>Custom property sets</p>
</td>
</tr>
<tr>
<td>Prpobj.c</td>
<td>
<p>Definitions of property sets</p>
</td>
</tr>
<tr>
<td>Prpset.c</td>
<td>
<p>Contains Set property handlers for device properties</p>
</td>
</tr>
<tr>
<td>Sources</td>
<td>
<p>Generic file for building the code sample</p>
</td>
</tr>
<tr>
<td>Usbintel.inf </td>
<td>
<p>Sample installation file</p>
</td>
</tr>
</tbody>
</table>
