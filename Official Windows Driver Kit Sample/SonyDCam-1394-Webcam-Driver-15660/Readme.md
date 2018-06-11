# SonyDCam 1394 Webcam Driver
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
* 2012-02-29 04:26:32
## Description

<h3>SonyDCam 1394 Webcam Driver</h3>
<p>The SonyDCam 1394 Webcam Driver sample is a Microsoft Windows Driver Model (WDM) Stream class video capture driver that supports IEEE 1394-based digital cameras that conform to the Digital Camera Specification from the
<a href="http://www.1394ta.org/index.html">1394 Trade Association</a>. Such cameras include the Sony 1394 CCM-DS250 and Texas Instruments 1394 MC680-DCC desktop cameras.
</p>
<p><b>Theory of Operation</b> </p>
<p>Sonydcam.sys supports a digital camera that is a data source that produces digital image data without any other input connection. SonyDCam displays as a WDM Streaming Capture Device, and its capture filter produces a 320 x 240 pixel capture stream in a UYVY
 color space. You can convert the output capture stream to another format by using one of the filters that the Msyuv.dll decompressor provides. Msyuv.dll is part of the Microsoft&nbsp;Windows operating system, and can convert the image data format from UYVY to RGB16
 or RGB8 or to a Microsoft DirectDraw surface if the video card supports UYVY format.</p>
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
<h3><a name="operation"></a>Operation</h3>
<p>The <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff558717">
<b>DriverEntry</b> </a>routine in SonyDCam.c initializes a <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff559682">
<b>HW_INITIALIZATION_DATA</b> </a>structure with the entry points of driver-implemented functions.</p>
<p>The <b>HwReceivePacket</b> member of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff559682">
<b>HW_INITIALIZATION_DATA</b> </a>structure points to the driver-implemented entry point for receiving Stream Request Packets or Blocks (SRBs) from the Stream class driver. For example, SonyDCam might receive the following sequence of SRBs:
</p>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568185"><b>SRB_INITIALIZE_DEVICE</b>
</a></dt><dd>
<p>Initialize the device. This SRB is called after <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff558717">
<b>DriverEntry</b> </a>.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568173"><b>SRB_GET_STREAM_INFO</b>
</a></dt><dd>
<p>Get supported stream formats.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568168"><b>SRB_GET_DATA_INTERSECTION</b>
</a></dt><dd>
<p>Query a supported format that some specific data types use.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568191"><b>SRB_OPEN_STREAM</b>
</a></dt><dd>
<p>Open a stream with a format that <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568168">
<b>SRB_GET_DATA_INTERSECTION</b> </a>supplied. SonyDCam registers two additional entry points for the newly opened stream to control the streaming state (<i>pSrb</i>-&gt;<i>StreamObject</i>-&gt;<b>ReceiveControlPacket</b>) and streaming data (<i>pSrb</i>-&gt;<i>StreamObject</i>-&gt;<b>ReceiveDataPacket</b>).</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568165"><b>SRB_CLOSE_STREAM</b>
</a></dt><dd>
<p>Close the open stream.</p>
</dd><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568215"><b>SRB_UNINITIALIZE_DEVICE</b>
</a></dt><dd>
<p>Indicate that a device has been unloaded or removed.</p>
</dd></dl>
<p></p>
<p>The <b>HwCancelPacket</b> member of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff559682">
<b>HW_INITIALIZATION_DATA</b> </a>structure points to the driver-implemented entry point that the Stream class driver calls in order to cancel packets that it has already sent to SonyDCam. If a packet has been queued for too long and has timed out, the
<b>HwRequestTimeoutHandler</b> member of the <b>HW_INITIALIZATION_DATA</b> structure points to the driver-implemented entry point to call.</p>
<p>SonyDCam implements two additional callback functions. The 1394 controller driver calls
<b>DCamIsochCallback</b> when an attached buffer is filled with data, and the 1394 controller driver calls
<b>DCamBusResetNotification</b> to indicate that a 1394 bus reset has occurred. Both of these functions execute at IRQL =
<b>DISPATCH_LEVEL</b>.</p>
<h3><a name="bus_reset"></a>Bus reset</h3>
<p>There are some special cases that SonyCDam must handle because it supports a Plug and Play (PnP) device. This special cases include unplugging the camera while streaming, and plugging a second device into the same 1394 controller where a device is already
 streaming. In either of these cases, the 1394 controller triggers a bus reset. After a bus reset, the controller invalidates its clients' bandwidth and channel. However, after the bus reset, the streaming device should continue to source isochronous video
 data according to the IEEE 1394 digital camera specification.</p>
<p>SonyDCam receives <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568212">
<b>SRB_SURPRISE_REMOVAL</b> </a>when its device has been removed. SonyDCam must then cancel all pending reads and prepare to be unloaded. In this situation, SonyDCam will likely receive
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568215"><b>SRB_UNINITIALIZE_DEVICE</b>
</a>before <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568165">
<b>SRB_CLOSE_STREAM</b> </a>. If a driver still has pending read SRBs, the Stream class driver does not send
<b>SRB_CLOSE_STREAM</b> even after its client application is closed.</p>
<p>For a device that is still plugged in, SonyDCam receives a notification callback from the 1394 controller to restore to its original state, including freeing and reallocating channel and bandwidth (but not the resource), and to restore to its original streaming
 state. SonyDCam then reinitializes and restarts the device. This step is necessary only if the device stops sourcing isochronous video data. SonyDCam also reinitializes the device if a device was attached but suspended (that is, transitioned to a lower device
 power state) and then awakened (transitioned to a higher device power state). In this situation, SonyDCam also receives a bus reset callback notification.</p>
<h3><a name="frame_rate_and_dropped_frames"></a>Frame rate and dropped frames</h3>
<p>Digital cameras support discrete frame rates. However, a client application can send a request to a stream at any rate. WDM video capture drivers must match the requested rate or select the next lower rate from the requested frame rate because oversampling
 the rate can cause synchronization problems.</p>
<p>The number of dropped frames must be calculated because after a buffer is attached to the 1394 controller driver, SonyDCam waits for the data buffer to be filled. If the 1394 controller does not fill the buffer because it misses the data (resulting in a
 dropped frame), the controller does not report that information back to SonyDCam, and SonyDCam cannot detect the number of dropped frames. The calculation of dropped frames is based on the capture rate and the actual count of captured frames.</p>
<h3><a name="limitations"></a>Limitations</h3>
<p>SonyDCam does not save to the registry changes that are made to property settings between restarts. SonyDCam also does not support power management, such as power transitions from device power states D3 to D0, or D0 to D3. The device installation file specifies
 &quot;DontSuspendIfStreamsAreRunning&quot; because of this limitation.</p>
<h3><a name="installation_notes"></a>Installation notes</h3>
<p>You cannot manually install SonyDCam because it is a PnP driver. The Image.inf device installation file that is shipped with the Windows operating system installs detected devices, including SonyDCam, for the cameras that are listed earlier in this topic.
 The sample SonyDCam.inf device installation file is a subset of the Image.inf device installation file. You can use SonyDCam.inf as a template to create a device installation file for your own hardware.</p>
<p>SonyDCam compiles on Windows&nbsp;98 and later versions of the operating system, on either 32-bit or 64-bit platforms.</p>
<h3><a name="code_tour"></a>Code tour</h3>
<p><b>File manifest</b> </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Callback.c </td>
<td>
<p>Process callback functions from controller driver for either a filled buffer or a bus reset</p>
</td>
</tr>
<tr>
<td>CapProp.c </td>
<td>
<p>Process <b>KSPROPERTY_VIDEOPROCAMP_XXX</b> and <b>KSPROPERTY_VIDCAP_CAMERACONTROL</b> Kernel Streaming (KS) properties</p>
</td>
</tr>
<tr>
<td>CapProp.h </td>
<td>
<p>Function prototypes for CapProp.c</p>
</td>
</tr>
<tr>
<td>CtrlPkt.c </td>
<td>
<p>Process stream control packet, such as querying and setting streaming state</p>
</td>
</tr>
<tr>
<td>DataPkt.c </td>
<td>
<p>Process stream read data packet</p>
</td>
</tr>
<tr>
<td>Dbg.c </td>
<td>
<p>Debug functions</p>
</td>
</tr>
<tr>
<td>Dbg.h </td>
<td>
<p>Debug preprocessor</p>
</td>
</tr>
<tr>
<td>DcamDef.h </td>
<td>
<p>Function prototypes</p>
</td>
</tr>
<tr>
<td>DCamPkt.c </td>
<td>
<p>Process Stream class driver's <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff568295">
<b>SRB_Xxx</b> </a>packets </p>
</td>
</tr>
<tr>
<td>DcamPkt.h </td>
<td>
<p>Function prototypes and major structure definition</p>
</td>
</tr>
<tr>
<td>Device.c </td>
<td>
<p>Process reading and writing to device's registers</p>
</td>
</tr>
<tr>
<td>PropData.h </td>
<td>
<p>Static KS properties for a digital camera</p>
</td>
</tr>
<tr>
<td>SonyDCam.c </td>
<td>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff558717"><b>DriverEntry</b>
</a>and device initialization and uninitialization functions</p>
</td>
</tr>
<tr>
<td>SonyDCam.h </td>
<td>
<p>Function prototypes for SonyDCam.c</p>
</td>
</tr>
<tr>
<td>SonyDCam.inf </td>
<td>
<p>Sample installation file</p>
</td>
</tr>
<tr>
<td>SonyDCam.rc </td>
<td>
<p>Resource file, mainly to provide version information</p>
</td>
</tr>
<tr>
<td>SOURCES </td>
<td>
<p>Generic file for building the code sample</p>
</td>
</tr>
<tr>
<td>StrmData.h </td>
<td>
<p>Supported stream formats</p>
</td>
</tr>
</tbody>
</table>
