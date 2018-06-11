# AVStream simulated hardware sample driver (Avshws)
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Streaming Media
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:13:49
## Description

<div id="mainSection">
<p>The AVStream simulated hardware sample driver (Avshws) provides a pin-centric <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554240">
AVStream</a> capture driver for a simulated piece of hardware. This streaming media driver performs video captures at 320 x 240 pixels in either RGB24 or YUV422 format using direct memory access (DMA) into capture buffers. The purpose of the sample is to demonstrate
 how to write a pin-centric AVStream minidriver. The sample also shows how to implement DMA by using the related functionality provided by the AVStream class driver.
</p>
<p>This sample features enhanced parameter validation and overflow detection.</p>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;Vista </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008 </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff558717"><b>DriverEntry</b></a> in Device.cpp is the initial point of entry into the driver. This routine passes control to AVStream by calling the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562683"><b>KsInitializeDriver</b></a> function. In this call, the minidriver passes the device descriptor, an AVStream structure that recursively defines the AVStream object hierarchy for a
 driver. This is common behavior for an AVStream minidriver.</p>
<p>At device start time, a simulated piece of capture hardware is created (the <b>
CHardwareSimulation</b> class), and a DMA adapter is acquired from the operating system and is registered with AVStream by calling the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff561687"><b>KsDeviceRegisterAdapterObject</b></a> function. This call is required for a sample that performs DMA access directly into the capture buffers, instead of using DMA access to write
 to a common buffer. The driver creates the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff567644">
KS Filter</a> for this device dynamically by calling the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff561650">
<b>KsCreateFilterFactory</b></a> function.</p>
<p>Filter.cpp is where the sample lays out the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff563534">
<b>KSPIN_DESCRIPTOR_EX</b></a> structure for the single video pin. In addition, a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562554"><b>KSFILTER_DISPATCH</b></a> structure and a
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562553"><b>KSFILTER_DESCRIPTOR</b></a> structure are provided in this source file. The filter dispatch provides only a create dispatch, a routine that is included in Filter.cpp. The process
 dispatch is provided on the pin because this is a pin-centric sample.</p>
<p>Capture.cpp contains source for the video capture pin on the capture filter. This is where the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff563535"><b>KSPIN_DISPATCH</b></a> structure for the unique pin is provided. This dispatch structure specifies a
<i>Process</i> callback routine, also defined in this source file. This routine is where stream pointer manipulation and cloning occurs.</p>
<p>The process callback is one of two routines of interest in Capture.cpp that demonstrate how to perform DMA transfers with AVStream functionality. The other is the
<b>CCapturePin::CompleteMappings</b> method. These two methods show how to use the queue, obtain clone pointers, use scatter/gather lists, and perform other DMA-related tasks.</p>
<p>For more information, see the comments in all .cpp files.</p>
<h3>Run the sample</h3>
<p>To run the sample, follow these steps:</p>
<ol>
<li>After installation has completed, access the driver through the Graphedt tool. Graphedt.exe is available in the
<i>tools</i> directory of the WDK. </li><li>In the Graphedt tool, click the <b>Graph</b> menu and click <b>Insert Filters</b>. The sample appears under &quot;WDM Streaming Capture Devices&quot; as &quot;avshws Source.&quot;
</li><li>Click <b>Insert Filter</b>. The sample appears in the graph as a single filter labeled, &quot;avshws Source.&quot; There is one output pin, which is the video capture pin. This pin emits video in YUY2 format.
</li><li>Attach this filter to either a Microsoft DirectShow Video Renderer or to the VMR default video renderer. Then click
<b>Play</b>. </li></ol>
<p></p>
<p>The output that is produced by the sample is a 320 x 240 pixel image of standard EIA-189-A color bars. In the middle of the image near the bottom, a clock appears over the image. This clock displays the elapsed time since the graph was introduced into the
 run state following the last stop. The clock display format is MINUTES:SECONDS.HUNDREDTHS.</p>
<p>In the upper-left corner of the image, a counter counts the number of frames that have been dropped since the graph was introduced into the run state after the last stop.</p>
<h3><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h3>
<p><b>File manifest</b> </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Avshws.h</td>
<td>
<p>Main header file for the sample</p>
</td>
</tr>
<tr>
<td>Avshws.inf </td>
<td>
<p>Sample installation file</p>
</td>
</tr>
<tr>
<td>Avshws.rc </td>
<td>
<p>Resource file, mainly to provide version information</p>
</td>
</tr>
<tr>
<td>Capture.cpp</td>
<td>
<p>Pin-level code for the capture pin and DMA handling</p>
</td>
</tr>
<tr>
<td>Capture.h </td>
<td>
<p>Header file for Capture.cpp</p>
</td>
</tr>
<tr>
<td>Device.cpp </td>
<td>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff558717"><b>DriverEntry</b></a>, Plug and Play (PnP) handling, initialization, and device level code</p>
</td>
</tr>
<tr>
<td>Device.h </td>
<td>
<p>Header file for Device.cpp</p>
</td>
</tr>
<tr>
<td>Filter.cpp </td>
<td>
<p>Filter level code for the capture filter</p>
</td>
</tr>
<tr>
<td>Filter.h </td>
<td>
<p>Header file for Filter.cpp</p>
</td>
</tr>
<tr>
<td>Hwsim.cpp </td>
<td>
<p>Hardware simulation code that fills scatter/gather mappings and performs other tasks</p>
</td>
</tr>
<tr>
<td>Hwsim.h </td>
<td>
<p>Header file for Hwsim.cpp</p>
</td>
</tr>
<tr>
<td>Image.cpp</td>
<td>
<p>RGB24 and UYVY image synthesis and overlay code</p>
</td>
</tr>
<tr>
<td>Image.h </td>
<td>
<p>Header file for Image.cpp</p>
</td>
</tr>
<tr>
<td>Sources </td>
<td>
<p>Generic file for building the code sample</p>
</td>
</tr>
</tbody>
</table>
</div>
