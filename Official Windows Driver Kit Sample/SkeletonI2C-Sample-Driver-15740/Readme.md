# SkeletonI2C Sample Driver
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* SPB
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:50:31
## Description

<div id="mainSection">
<p>The SkeletonI2C sample demonstrates how to design a KMDF controller driver for Windows that conforms to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450903">simple peripheral bus</a> (SPB) device driver interface (DDI). SPB is an abstraction for low-speed serial buses (for example, I<sup>2</sup>C and SPI) that allows peripheral drivers
 to be developed for cross-platform use without any knowledge of the underlying bus hardware or device connections. While this sample implements an empty I<sup>2</sup>C driver, it could just as easily be the starting point for an SPI driver with only minor
 modifications. </p>
<p>Note that the SkeletonI2C sample is simplified to show the overall structure of an SPB controller, but contains only the code that the driver requires to communicate with the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh406203">SPB framework extension (SpbCx)</a> and KMDF. The SkeletonI2C sample driver omits all hardware-specific code. It does not simulate data transfers or implement request completion asynchronously.
 Pay close attention to code comments marked with &quot;TODO&quot; that refer to blocks of code that must be removed or updated.</p>
<p>The simplified structure of the SkeletonI2C sample driver makes it a convenient starting point for development of a real SPB controller driver that manages the hardware functions in an SPB controller.</p>
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
<td><dt>Windows&nbsp;8 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2012 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>The SkeletonI2C sample can be built, installed, and interacted with via SPB as-is.</p>
<h2>Run the sample</h2>
<p>To install the SkeletonI2C driver, follow these instructions:</p>
<ol>
<li>
<p>Ensure that the driver builds without errors.</p>
</li><li>
<p>Copy the SYS and INF files to a separate folder.</p>
</li><li>
<p>Run devcon.exe. You can find this program in the tools\devcon folder where you installed the WDK. Type one of the following commands in the command window:</p>
<ul>
<li>Use the command &quot;<code>devcon.exe update SkeletonI2C.inf ACPI\&lt;HWID&gt;</code>&quot; to install the driver on an existing device node. This is necessary if a peripheral device node must be linked to this controller via the
<b>I2CSerialBus</b> or <b>SPISerialBus</b> macros. See Skeletoni2c.asl for a sample firmware declaration.
</li><li>Use the command &quot;<code>devcon.exe install SkeletonI2C.inf ACPI\skeletoni2c</code>&quot; to dynamically create a controller device node, and to install the SkeletonI2C driver on this node. When installed this way, a peripheral driver will not be able to &quot;find&quot;
 its I<sup>2</sup>C or SPI controller. </li></ul>
</li></ol>
<h2><a id="Modifying_the_sample"></a><a id="modifying_the_sample"></a><a id="MODIFYING_THE_SAMPLE"></a>Modifying the sample</h2>
<p>Here are some high-level points to consider when modifying the SkeletonI2C sample for use on real hardware:</p>
<ul>
<li>Edit (and likely rename) Skeletoni2c.h to describe your hardware's register set.
</li><li>Modify Controller.cpp and Device.cpp to translate the SPB DDI and primitives into I<sup>2</sup>C or SPI protocol for your hardware. This includes initialization, I/O configuration, and interrupt processing.
</li><li>Address any comments marked with &quot;TODO&quot; in the sample, especially those that short circuit the I/O path to complete requests synchronously.
</li><li>Modify the HWID (<code>ACPI\skeletoni2c</code>) in Skeletoni2c.inf to match the device node in your firmware.
</li><li>Generate and specify a unique trace GUID in I2ctrace.h. </li><li>Refactor the driver name, functions, comments, etc., to better describe your implementation.
</li></ul>
<h2><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h2>
<p>The following are relevant functions in the SkeletonI2C driver for implementing the SPB DDI.</p>
<table>
<tbody>
<tr>
<th>Function</th>
<th>Description</th>
</tr>
<tr>
<td>INITIALIZATION</td>
</tr>
<tr>
<td><code>OnDeviceAdd</code> </td>
<td>
<p>Within <code>OnDeviceAdd</code>, the driver makes several configuration calls for SPB.</p>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450918"><b>SpbDeviceInitConfig</b></a> must be called before creating the WDFDEVICE. Note that SpbCx sets a default security descriptor on the device object, but the controller driver can
 override it by calling <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546035">
<b>WdfDeviceInitAssignSDDLString</b></a> after <b>SpbDeviceInitConfig</b>.</p>
<p>After creating the WDFDEVICE, the driver configures it appropriately for SPB by calling
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450919"><b>SpbDeviceInitialize</b></a>. Here the driver also sets the target and request attributes.</p>
<p>Finally the driver configures a WDF system-managed idle time-out.</p>
</td>
</tr>
<tr>
<td>TARGET CONNECTION</td>
</tr>
<tr>
<td><code>OnTargetConnect</code> </td>
<td>
<p>Invoked when a client opens a handle to the specified SPB target. Queries the I<sup>2</sup>C connection parameters from the resource hub (via SPB) and initializes the target context.</p>
</td>
</tr>
<tr>
<td>SPB I/O CALLBACKS</td>
</tr>
<tr>
<td><code>OnRead</code> </td>
<td>
<p>SPB read callback. Invokes the <code>PbcConfigureForNonSequence</code> function to set up the transfer.</p>
</td>
</tr>
<tr>
<td><code>OnWrite</code> </td>
<td>
<p>SPB write callback. Invokes the <code>PbcConfigureForNonSequence</code> function to set up the transfer.</p>
</td>
</tr>
<tr>
<td><code>OnSequence</code> </td>
<td>
<p>SPB sequence callback. Configures the controller for an atomic transfer*.</p>
</td>
</tr>
<tr>
<td><code>OnControllerLock</code> </td>
<td>
<p>SPB lock controller callback. Configures to handle subsequent I/O as an atomic transfer*. For I<sup>2</sup>C the controller should place a start bit on the bus. For SPI the controller should assert the chip-select line. The driver may choose to carry this
 out as part of this callback or defer until the first I/O operation is received (the next call to
<code>OnRead</code> or <code>OnWrite</code>).</p>
</td>
</tr>
<tr>
<td><code>OnControllerUnlock</code> </td>
<td>
<p>SPB unlock controller callback. Marks the end of an atomic transfer*. For I<sup>2</sup>C, the controller should place a stop bit on the bus. For SPI, the controller should de-assert the chip-select line.</p>
</td>
</tr>
<tr>
<td>SPB HELPER METHODS</td>
</tr>
<tr>
<td><code>PbcConfigureForIndex</code> </td>
<td>
<p>Configures the request context for the specified transfer index. This could be a single I/O or part of a sequence.</p>
</td>
</tr>
<tr>
<td><code>PbcRequestComplete</code> </td>
<td>
<p>Sets the number of bytes completed for a request and invokes the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh450920">
<b>SpbRequestComplete</b></a> method.</p>
</td>
</tr>
</tbody>
</table>
<p>*An atomic transfer in SPB is implemented using Sequence or a Lock/Unlock pair. For I<sup>2</sup>C, this means a set of reads and writes with restarts in between. For SPI, this means a set of reads and writes with the chip select-line asserted throughout.</p>
<p>The following are relevant functions in the SkeletonI2C driver for implementing controller-specific I2C protocol. For the most part, these are placeholders and must be filled in appropriately.</p>
<table>
<tbody>
<tr>
<th>Function</th>
<th>Description</th>
</tr>
<tr>
<td>INITIALIZATION</td>
</tr>
<tr>
<td><code>ControllerInitialize</code> </td>
<td>
<p>One-time controller initialization. Prepare FIFOs, clocks, interrupts, etc.</p>
</td>
</tr>
<tr>
<td><code>ControllerConfigureForTransfer</code> </td>
<td>
<p>Per-I/O controller configuration. Depending on the type of I/O (and whether its part of an ongoing atomic transfer), the driver may need to configure direction, set interrupts, etc.</p>
<p>Additionally, for I<sup>2</sup>C, the driver may need to insert a start, restart, or stop bit as necessary, and for SPI the driver may need to assert or de-assert the chip select line.</p>
</td>
</tr>
<tr>
<td>I/O PROCESSING</td>
</tr>
<tr>
<td><code>OnInterruptIsr</code> </td>
<td>
<p>Interrupt callback. Acknowledges interrupts and saves state as necessary. Queues a DPC for processing.</p>
</td>
</tr>
<tr>
<td><code>OnInterruptDpc</code> </td>
<td>
<p>DPC callback. Processes saved interrupts. If necessary the request is completed.</p>
</td>
</tr>
<tr>
<td><code>ControllerProcessInterrupts</code> </td>
<td>
<p>Handles processing for both normal and error condition interrupts. Invokes <code>
ControllerCompleteTransfer</code>() as appropriate.</p>
</td>
</tr>
<tr>
<td><code>ControllerCompleteTransfer</code> </td>
<td>
<p>Invoked when an I/O completes or an error is detected. If this I/O is part of a sequence,
<code>PbcRequestConfigureForIndex</code>() is called to prepare the next I/O; otherwise, the request is marked for completion.</p>
</td>
</tr>
</tbody>
</table>
<h2><a id="File_manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File manifest</h2>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>controller.h, controller.cpp</td>
<td>
<p>Controller-specific implementation of I<sup>2</sup>C protocol.</p>
</td>
</tr>
<tr>
<td>driver.h, driver.cpp</td>
<td>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff544113"><b>DriverEntry</b></a> and Events on the Driver Object. Contains SpbCx initialization.</p>
</td>
</tr>
<tr>
<td>device.h, device.cpp</td>
<td>
<p>WDF and SPB event callbacks and helper methods. Other than initialization, contains all interaction with SpbCx.</p>
</td>
</tr>
<tr>
<td>hw.h, hw.cpp</td>
<td>
<p>HW register abstraction and access methods.</p>
</td>
</tr>
<tr>
<td>i2ctrace.h</td>
<td>
<p>Sets up WPP tracing.</p>
</td>
</tr>
<tr>
<td>internal.h</td>
<td>
<p>Common includes and typedefs. WDF context definitions.</p>
</td>
</tr>
<tr>
<td>makefile</td>
<td>
<p>Redirects to the real makefile that is shared by all components of the WDK.</p>
</td>
</tr>
<tr>
<td>makefile.inc</td>
<td>
<p>Defines custom build actions. Includes the conversion of the .INX file into a .INF file.</p>
</td>
</tr>
<tr>
<td>resource.rc</td>
<td>
<p>Resource descriptor file used for versioning.</p>
</td>
</tr>
<tr>
<td>sources</td>
<td>
<p>Lists source files and build options.</p>
</td>
</tr>
<tr>
<td>sources.dep</td>
<td>
<p>Defines build dependencies.</p>
</td>
</tr>
<tr>
<td>skeletoni2c.asl</td>
<td>
<p>Empty ASL file for a controller device node. In order for a peripheral driver to find the underlying I<sup>2</sup>C or SPI controller, this ACPI path must be specified in the
<code>I2CSerialBus</code> or <code>SPISerialBus</code> macro.</p>
</td>
</tr>
<tr>
<td>skeletoni2c.h</td>
<td>
<p>Controller's register set definition and defines.</p>
</td>
</tr>
<tr>
<td>skeletoni2c.inx</td>
<td>
<p>Describes the installation of the driver. The build process converts this file to a .INF.</p>
</td>
</tr>
</tbody>
</table>
</div>
