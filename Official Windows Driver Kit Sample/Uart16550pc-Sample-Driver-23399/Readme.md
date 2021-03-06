# Uart16550pc Sample Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* KMDF
* Windows Driver
## Topics
* serial
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:22:18
## Description

<div id="mainSection">
<p>The Uart16550pc sample demonstrates how to design an extension-based <a href="serports.serial_controller_driver_design_guide">
serial controller driver</a> for a 16550 UART. This driver works in conjunction with the
<a href="serports.serial_framework_extension_overview">serial framework extension</a> (SerCx) to handle
<a href="serports.serial_device_control_requests"><b>IOCTL_SERIAL_<i>XXX</i></b></a> requests for a peripheral device that is connected to a port on the UART. The sample driver uses the Kernel-Mode Driver Framework (KMDF), and communicates with SerCx through
 the SerCx device driver interface (DDI). This DDI provides an abstraction layer to separate the hardware-dependent tasks that are performed by the driver from the hardware-independent tasks, such as establishing device connections and managing queues of I/O
 requests, that are performed by SerCx. </p>
<p>Instead of retrieving buffers directly from requests, extension-based serial controller drivers obtain software buffers used for I/O from SerCx, which enables the driver to utilize the SerCx ring buffer. The Uart16550pc sample implements a functional UART
 controller driver. The sample performs transmit and receive data transfers. It supports RTS/CTS and DTR/DSR hardware flow control, but does not implement XON/XOFF software flow control.</p>
<p>Without modification, the Uart16550pc sample driver provides many basic functions of the legacy serial driver, Serial.sys, but cannot be installed as a simple replacement for Serial.sys. For a sample driver that can be installed in place of Serial.sys, see
 the Serial sample in the src\serial\serial folder.</p>
<p>Although the Uart16550pc and Serial sample drivers are similar in function, the Uart16550pc sample driver is significantly smaller because SerCx offloads from this driver much of the work required to manage queues of I/O requests. In contrast, the Serial
 sample driver must perform this work itself, without such assistance.</p>
<p>There are other differences between the Serial and Uart16550pc sample. The ports controlled by the Uart16550pc sample driver are unnamed, whereas the Serial sample driver controls serial ports with names such as COM1, COM2, and so on. The Uart16550pc sample
 driver is intended for use with a serial port that has a dedicated connection to a peripheral device. The Plug and Play manager obtains the information about this port from the ACPI firmware, and assigns the port to the peripheral device driver as a dedicated
 hardware resource. The Serial sample driver is designed to support the Serenum sample filter driver, but the Uart16550pc sample does not. The Serenum sample is located in the src\serial\serenum folder.</p>
<p>The Uart16550pc sample is targeted to run using the 16550D UART. Other versions of this UART exist, so the sample might need to be adapted to a different version by adding hardware-specific code. The Uart16550pc sample is designed to accommodate system DMA
 with minimal modification. If DMA is used, the sample driver expects to be provided with a DMA resource for each channel used. Additionally, the UART must implement automatic hardware flow control, and support for flow control must be added to the sample driver.
 If either of these prerequisites is not met, processor I/O (PIO) will be used instead of DMA. The supplied DMA controller initialization parameters used in the sample might need to be modified.</p>
<p><b>PIO and DMA code paths</b> </p>
<p>If PIO is used for a data transmit request, the driver will cache a buffer obtained from SerCx, and return it after the last byte is transmitted in the ISR DPC. The THR interrupt is enabled after caching the buffer, and will be re-enabled after each byte
 sent until transmit is complete. If PIO is used for a data receive request, the driver will cache a buffer obtained from SerCx if flow control is used. Otherwise, a buffer will be obtained only when data is received in the ISR DPC, and returned once all data
 is read from the FIFO or the buffer is filled. The RDA interrupt signals the driver to read one or more bytes and to de-assert any necessary flow control lines, and will be re-enabled once data is read from the FIFO.</p>
<p>If DMA is used for transmit or receive, interrupts are disabled for the corresponding direction before the transfer is initialized. Automatic hardware flow control must be implemented for each direction DMA is to be used. The sample does not implement any
 automatic hardware flow control. DMA will only be used for an I/O request if automatic hardware flow control is enabled, and if no bytes for that request have been read from the SerCx ring buffer (see the UartUseDma function in the device.cpp sample file).</p>
<p>Both DMA and PIO are demonstrated by the Uart16550pc sample, so the two methods of data transfer are synchronized using spin locks and a pair of cached buffers.</p>
<p>If your platform does not use DMA, the sample can be simplified to remove these cached buffers and spin locks. The driver can instead only acquire a buffer from SerCx within the ISR DPC and release it when it is full or the FIFO is empty. Additionally, all
 interrupts can be enabled in the global interrupt enable function (UartEnableInterrupts). This driver will also not have to implement support for
<a href="serports.ioctl_serial_get_timeouts"><b>IOCTL_SERIAL_GET_TIMEOUTS</b></a> and
<a href="serports.ioctl_serial_set_timeouts"><b>IOCTL_SERIAL_SET_TIMEOUTS</b></a> requests, which are handled by SerCx.</p>
<p>If your platform uses DMA for all data transfers, the driver can be simplified to not handle transmit work in the ISR DPC. The helper function to read from the FIFO (UartReceiveBytesFromFIFO in uart16550pc.cpp) should be modified to implement DMA receive,
 as it currently uses PIO.</p>
<p>These modification strategies are entirely optional.</p>
<p><b>Power management</b> </p>
<p>Unlike the legacy serial driver, Serial.sys, the Uart16550pc sample is power-managed through the WDF power framework. Several strategies are used to properly handle power state transitions and prevent data loss.</p>
<p>While the UART is transitioning out of the D0 power state, it is possible for SerCx to receive a read and/or write request. The WDF power framework is designed so that a driver cannot fully exit D0 while it owns any requests, so the driver must either complete
 or cancel all requests. If SerCx were allowed to remain in D0 indefinitely with one or more requests pended, it would be possible to interfere with system shutdown or sleep. Therefore, SerCx will cancel all requests received while exiting D0 with STATUS_MORE_ENTRIES.
 The controller driver will terminate DMA transfers and return cached buffers to SerCx. SerCx will also return either the number of bytes that were written, or a buffer with the number of bytes that were received before the cancel. Client drivers must be prepared
 to recover from request cancellation due to power state transitions. Recovery might be as simple as sending another read or write for the remaining bytes, but it might also be necessary to resend the entire request or retry a series of requests.</p>
<p>The behavior of this sample is to exit D0 immediately after the SerCx I/O queues have emptied. Consider adding a system idle timeout in UartDeviceCreate to allow more time for the driver to receive requests before idling.</p>
<p>Before the controller driver exits D0, any bytes remaining in the receive FIFO are saved to memory. If flow control is not enabled, these bytes are saved in the SerCx ring buffer. If flow control is enabled, these bytes are saved in a small, local buffer
 that the driver allocates from nonpaged pool. While any bytes remain in the local buffer, the RTS flow control line stays de-asserted, even after the controller reenters D0. The driver keeps the RTS line de-asserted until the local buffer is emptied into the
 output buffer of a read request.</p>
<p>This local buffer is necessary so that the driver can start the read interval timer when the first byte is copied to the read request buffer. If this first byte is available from the local buffer when the read request arrives, the timer starts immediately.
 If the local buffer is empty, the driver waits to start the timer until the first byte is read from the receive FIFO.</p>
<p>When flow control is used, the driver is responsible for detecting read interval time-outs. Otherwise, SerCx detects these time-outs.</p>
<p><b>Avoiding design issues</b> </p>
<p>To adapt the sample driver to your hardware requirements, follow these guidelines:</p>
<ul>
<li>If your driver supports both DMA and PIO, make sure the two methods of I/O are synchronized.
</li><li>If your UART supports additional interrupts, ensure they are enabled at the correct time. The UartEnableInterrupts function is not synchronized between the read and write paths.
</li><li>Make sure the hardware FIFOs empty before exiting D0. </li><li>If the DMA controller has a minimum transfer unit, read from the FIFOs after DMA completes.
</li><li>Set the appropriate system idle timeout. </li><li>Make sure your driver properly handles interval timeout while caching a buffer retrieved from SerCx.
</li></ul>
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
<p>The Uart16550pc sample can be built, installed, and interacted with via SerCx as-is. You can build the sample in two ways: using Visual Studio Ultimate&nbsp;2012 or the command line (<i>MSBuild</i>).</p>
<h3><a id="Building_a_Driver_Using_Visual_Studio"></a><a id="building_a_driver_using_visual_studio"></a><a id="BUILDING_A_DRIVER_USING_VISUAL_STUDIO"></a>Building a Driver Using Visual Studio</h3>
<p>You build a driver the same way you build any project or solution in Visual Studio. When you create a new driver project using a Windows driver template, the template defines a default (active) project configuration and a default (active) solution build
 configuration. When you create a project from existing driver sources or convert existing driver code that was built with previous versions of the WDK, the conversion process preserves the target version information (operating systems and platform).</p>
<p>The default Solution build configuration is Windows&nbsp;8 Debug and Win32.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open the driver project or solution in Visual Studio Ultimate&nbsp;2012 (find <i>filtername</i>.sln or
<i>filtername</i>.vcxproj). </li><li>Right-click the solution in the <b>Solutions Explorer</b> and select <b>Configuration Manager</b>.
</li><li>From the <b>Configuration Manager</b>, select the <b>Active Solution Configuration</b> (for example, Windows&nbsp;8 Debug or Windows&nbsp;8 Release) and the
<b>Active Solution Platform</b> (for example, Win32) that correspond to the type of build you are interested in.
</li><li>From the Build menu, click <b>Build Solution</b> (Ctrl&#43;Shift&#43;B). </li></ol>
<h3><a id="Building_a_Driver_Using_the_Command_Line__MSBuild_"></a><a id="building_a_driver_using_the_command_line__msbuild_"></a><a id="BUILDING_A_DRIVER_USING_THE_COMMAND_LINE__MSBUILD_"></a>Building a Driver Using the Command Line (MSBuild)</h3>
<p>You can build a driver from the command line using the Visual Studio Command Prompt window and the Microsoft Build Engine (MSBuild.exe) Previous versions of the WDK used the Windows Build utility (Build.exe) and provided separate build environment windows
 for each of the supported build configurations. You can now use the Visual Studio Command Prompt window for all build configurations.</p>
<p class="proch"><b>To select a configuration and build a driver</b></p>
<ol>
<li>Open a Visual Studio Command Prompt window at the <b>Start</b> screen. From this window you can use MsBuild.exe to build any Visual Studio project by specifying the project (.VcxProj) or solutions (.Sln) file.
</li><li>Navigate to the project directory and enter the <b>MSbuild</b> command for your target. For example, to perform a clean build of a Visual Studio driver project called
<i>filtername</i>.vcxproj, navigate to the project directory and enter the following MSBuild command:
<b>msbuild /t:clean /t:build .\</b><i>filtername</i><b>.vcxproj</b>. </li></ol>
<h3><a id="Modifying_the_sample"></a><a id="modifying_the_sample"></a><a id="MODIFYING_THE_SAMPLE"></a>Modifying the sample</h3>
<p>The following steps should be considered to modify the Uart16550pc sample to use with UARTs other than the 16550D:</p>
<ul>
<li>Edit regfile.h to describe your hardware's register set and interrupts. </li><li>Edit the initialization parameters in dma.h to describe your system's DMA controller, if applicable. Double check DMA initialization in regfile.cpp. Ensure that your system firmware is configured to provide DMA resources to the driver. You may otherwise
 hard code the descriptor values. </li><li>Implement automatic hardware flow control, if necessary, in flow.cpp. See the function UartFlowReceiveAvailable.
</li><li>Address any comments marked with TODO in the sample. </li><li>Modify the HWID in uart16550pc.inf to match the device node in your firmware.
</li><li>Refactor the driver name, functions, comments etc. to better describe your implementation.
</li></ul>
<h3><a id="Installing_the_sample"></a><a id="installing_the_sample"></a><a id="INSTALLING_THE_SAMPLE"></a>Installing the sample</h3>
<p>To install the Uart16550pc driver:</p>
<ol>
<li>Ensure that the driver builds without errors. </li><li>Copy the SYS and INF files to a separate folder. </li><li>
<p>Run devcon.exe. You can find this program in the tools\devcon folder where you installed the WDK. Type one of the following commands in the command window:</p>
<ol>
<li>Use the command <code>devcon.exe update Uart16550pc.inf ACPI\&lt;HWID&gt;</code> to install the driver on an existing device node. This is necessary if a peripheral device node must be linked to this controller via the UARTSerialBus macros. For more information
 about these macros, see the <a href="http://go.microsoft.com/fwlink/p/?linkid=57185">
Advanced Configuration and Power Interface Specification 5.0</a>. </li><li>Use the command <code>devcon.exe install Uart16550pc.inf ACPI\uart16550pc</code> to dynamically create a controller device node and install the Uart16550pc driver on it. When installed this way a peripheral driver will not be able to &quot;find&quot; its UART controller.
</li></ol>
</li></ol>
<h3><a id="Common_serial_IOCTLs"></a><a id="common_serial_ioctls"></a><a id="COMMON_SERIAL_IOCTLS"></a>Common serial IOCTLs</h3>
<p>Based on anticipated clients of Bluetooth and GPS, we have implemented the following IOCTLs in the Serial Class Extension (SerCx) and the UART16550 controller driver sample in the WDK. This list is not exhaustive, but our current testing requires the following
 IOCTL support. Serial peripherals from some IHVs may require additional IOCTLs to be implemented to work properly.</p>
<p>In the following table, the <b>NI</b> (not implemented) notation appears in the
<b>Handled by</b> column if neither SerCx nor the sample controller driver implements support for the IOCTL.</p>
<table>
<tbody>
<tr>
<th>IOCTL</th>
<th>Handled by</th>
<th>Notes</th>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_TIMEOUTS</td>
<td>SerCx</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_TIMEOUTS</td>
<td>SerCx</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_QUEUE_SIZE</td>
<td>SerCx</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_WAIT_MASK</td>
<td>SerCx</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_WAIT_MASK</td>
<td>SerCx</td>
<td>Invokes EvtSerCxWaitmask.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_WAIT_ON_MASK</td>
<td>SerCx</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_PURGE</td>
<td>SerCx</td>
<td>Invokes EvtSerCxTransmitCancel and EvtSerCxReceiveCancel only if the controller driver has an outstanding buffer. Invokes EvtSerCxPurge.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_CONFIG_SIZE</td>
<td>SerCx</td>
<td>Returns STATUS_SUCCESS but does nothing. This aligns with the legacy driver.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_APPLY_DEFAULT_CONFIGURATION (new)</td>
<td>SerCx</td>
<td>This IOCTL is new. It instructs the serial driver to apply the default configuration from the connection’s serial resource descriptor. Invokes EvtSerCxApplyConfig.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_RESET_DEVICE</td>
<td>SerCx (NI)</td>
<td>Returns STATUS_NOT_IMPLEMENTED. This returned success in the legacy driver, but we prefer to be more declarative in the behavior.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_IMMEDIATE_CHAR</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_STATS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_CLEAR_STATS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_BAUD_RATE</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_BAUD_RATE</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_MODEM_CONTROL</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_MODEM_CONTROL</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_FIFO_CONTROL</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_LINE_CONTROL</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_LINE_CONTROL </td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_CHARS</td>
<td>Controller</td>
<td>All characters are supported for this set IOCTL (i.e. xon/xoff, break, error, etc.), but the related functionality that these characters imply is not implemented.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_CHARS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_DTR</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_CLR_DTR</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_RTS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_CLR_RTS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_XOFF</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_XON</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_BREAK_ON</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_BREAK_OFF</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_HANDFLOW</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_SET_HANDFLOW</td>
<td>Controller</td>
<td>The following settings are not implemented in the Uart16650pc WDK sample: SERIAL_DSR_SENSITIVITY, SERIAL_ERROR_ABORT, SERIAL_DCD_HANDSHAKE, SERIAL_AUTO_TRANSMIT, SERIAL_AUTO_RECEIVE, SERIAL_ERROR_CHAR, SERIAL_NULL_STRIPPING, SERIAL_BREAK_CHAR, SERIAL_XOFF_CONTINUE</td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_MODEMSTATUS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_DTRRTS</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_COMMSTATUS</td>
<td>Controller</td>
<td>Only SERIAL_STATUS.Errors has been populated. Others are untouched because they are too byte-by-byte focused (conflicts with DMA) or correspond to other not implemented functionality.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_GET_PROPERTIES</td>
<td>Controller</td>
<td></td>
</tr>
<tr>
<td>IOCTL_SERIAL_XOFF_COUNTER</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
<tr>
<td>IOCTL_SERIAL_LSRMST_INSERT</td>
<td>Controller (NI)</td>
<td>Not implemented in the Uart16550pc WDK sample.</td>
</tr>
</tbody>
</table>
<h3><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h3>
<p>The following are relevant functions in the Uart16550pc driver for implementing the SerCx DDI.</p>
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
<td>UartEvtDeviceAdd</td>
<td>
<p>Within UartEvtDeviceAdd() the driver makes several configuration calls for SerCx.</p>
<p>SerCxDeviceInitConfig() must be called before creating the WDFDEVICE. Note SerCx sets a default security descriptor on the device object, but the controller driver can override it by calling WdfDeviceInitAssignSDDLString() after SerCxDeviceInitConfig().</p>
<p>After creating the WDFDEVICE, the driver configures it appropriately for SerCx by calling SerCxInitialize().</p>
<p>Finally the driver configures WDF system managed idle timeout.</p>
</td>
</tr>
<tr>
<td>I/O CALLBACKS AND HELPER METHODS</td>
</tr>
<tr>
<td>UartEvtReceive</td>
<td>SerCx read callback. Initializes DMA transfer if able, otherwise enables PIO. If flow control is used, a buffer obtained from SerCx will be cached until the I/O request is canceled, completed or timed out. Otherwise, the driver will only obtain a buffer
 from SerCx when data receive is handled in the ISR DPC.</td>
</tr>
<tr>
<td>UartEvtReceiveCancel</td>
<td>SerCx read cancel callback. Returns cached receive buffer to SerCx, or schedules an asynchronous DMA transfer cancel.</td>
</tr>
<tr>
<td>UartEvtReceiveDmaTransferComplete</td>
<td>DMA receive completion routine. Called into when a DMA transfer has completed, or was canceled by UartEvtReceiveCancel. Returns cached receive buffer to SerCx if request was fully completed or canceled. Otherwise, initializes PIO to receive remaining data.
 May start interval timeout timer.</td>
</tr>
<tr>
<td>UartEvtReceiveProgramDma</td>
<td>DMA callback invoked before each DMA transaction.</td>
</tr>
<tr>
<td>UartEvtTransmit</td>
<td>SerCx write Initializes DMA transfer if able, otherwise enables PIO. A buffer obtained from SerCx will be cached until the I/O request is canceled, completed or timed out.</td>
</tr>
<tr>
<td>UartEvtTransmitCancel</td>
<td>SerCx transmit cancel callback. Returns cached transmit buffer to SerCx, or schedules an asynchronous DMA transfer cancel.</td>
</tr>
<tr>
<td>UartEvtTransmitDmaTransferComplete</td>
<td>DMA transmit completion routine. Called into when a DMA transfer has completed, or was canceled by UartEvtTransmitCancel. Returns cached transmit buffer to SerCx if request was fully completed or canceled. Otherwise, initializes PIO to transmit remaining
 data.</td>
</tr>
<tr>
<td>UartEvtTransmitProgramDma</td>
<td>DMA callback invoked before each DMA transaction.</td>
</tr>
<tr>
<td>UartIntervalTimeoutTimer</td>
<td>Callback invoked on receive interval timeout. Returns receive buffer to SerCx and indicates request timeout.</td>
</tr>
<tr>
<td>UartReceiveBytesFromRXFIFO</td>
<td>Helper method to read bytes from the RX Fifo. This function may be called during ISR DPC, after DMA receive, and before driver exits D0. If a receive buffer is cached bytes will be read from the Fifo into that buffer, and the buffer will be returned if
 filled. If the buffer is not returned, the interval timeout timer may start. Otherwise, this function might obtain a buffer from SerCx. If a buffer is obtained here, it will also be returned.</td>
</tr>
<tr>
<td>UartUseDma</td>
<td>Returns whether DMA may be used for the current request. May need modification depending on DMA controller.</td>
</tr>
</tbody>
</table>
<p>The following are relevant functions in the Uart16550pc driver for implementing controller-specific functionality. These might need to be changed depending on your hardware.</p>
<table>
<tbody>
<tr>
<th>Function</th>
<th>Description</th>
</tr>
<tr>
<td>FLOW CONTROL</td>
</tr>
<tr>
<td>UartFlowReceiveAvailable</td>
<td>Called to assert flow control lines, enabling the driver to receive data, if RTS/CTS or DTR/DSR handshaking is used. Otherwise has no effect on controller registers. May need to be updated to implement automatic hardware flow control.</td>
</tr>
<tr>
<td>UartFlowReceiveFull</td>
<td>Called to assert flow control lines, preventing the driver from receiving data, if RTS/CTS or DTR/DSR handshaking is used. Otherwise has no effect on controller registers. May need to be updated to implement automatic hardware flow control.</td>
</tr>
<tr>
<td>INITIALIZATION</td>
</tr>
<tr>
<td>UartInitController</td>
<td>One-time controller initialization. Prepare FIFOs, clocks, interrupts, etc.</td>
</tr>
<tr>
<td>UartInitDMA</td>
<td>One-time DMA initialization. Configures one line for system DMA transmit if at least one DMA resource was found, and then configures one line for system DMA receive if at least two DMA resources were found.</td>
</tr>
<tr>
<td>I/O PROCESSING</td>
</tr>
<tr>
<td>UartEnableInterrupts</td>
<td>Enables UART interrupts. Additional interrupts added to the sample will need to be enabled here. This function is called by SerCx I/O, DMA, and WDF interrupt enable callbacks. While this is the sample's global interrupt enable function, case-specific interrupt
 work may also be done at the end of the ISR DPC.</td>
</tr>
<tr>
<td>UartISR</td>
<td>Interrupt callback. Acknowledges interrupts and saves state as necessary. Queues a DPC for processing.</td>
</tr>
<tr>
<td>UartRecordInterrupt</td>
<td>ISR helper method that queries interrupt identification register. Additional interrupts added to the sample will need to be acknowledged here.</td>
</tr>
<tr>
<td>UartTxRxDPCForISR</td>
<td>DPC callback. Processes saved interrupts. Performs processor I/O and checks the line status register for errors.</td>
</tr>
<tr>
<td>UartTxRxDPCWorker</td>
<td>Helper method for UartTxRxDPCForISR that handles the bulk of the work.</td>
</tr>
</tbody>
</table>
<h3><a id="File_manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File manifest</h3>
<p>The Uart16550pc sample folder contains the following files.</p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>driver.h, driver.cpp</td>
<td>DriverEntry and Events on the Driver Object. Contains SerCx initialization.</td>
</tr>
<tr>
<td>device.h, device.cpp</td>
<td>Contains PNP and WDF callbacks. Also contains helper methods for controller transmit and receive buffer caching.</td>
</tr>
<tr>
<td>dma.h</td>
<td>Contains definitions for some DMA initialization parameters.</td>
</tr>
<tr>
<td>flow.h, flow.cpp</td>
<td>Helper methods for handling hardware flow control.</td>
</tr>
<tr>
<td>ioctls.h, ioctls.cpp</td>
<td>Helper methods for handling IOCTLs implemented by the controller driver.</td>
</tr>
<tr>
<td>isr.h, isr.cpp</td>
<td>ISR callback and helper methods. Includes the DPC function where PIO work is done.</td>
</tr>
<tr>
<td>makefile</td>
<td>Redirects to the real makefile that is shared by all components of the WDK.</td>
</tr>
<tr>
<td>makefile.inc</td>
<td>Defines custom build actions. Includes the conversion of the .INX file into a .INF file.</td>
</tr>
<tr>
<td>regfile.h, regfile.cpp</td>
<td>Controller's register set definition and defines, and helper methods for controller initialization and power callbacks.</td>
</tr>
<tr>
<td>regutils.h, regutils.cpp</td>
<td>Helper methods for line status register and setting baud rate.</td>
</tr>
<tr>
<td>resource.rc</td>
<td>Resource descriptor file used for versioning.</td>
</tr>
<tr>
<td>sources</td>
<td>Lists source files and build options.</td>
</tr>
<tr>
<td>sources.dep</td>
<td>Defines build dependencies.</td>
</tr>
<tr>
<td>uart16550pc.h, uart16550pc.cpp</td>
<td>WDF and SerCx event callbacks and helper methods. Includes callbacks for DMA.</td>
</tr>
<tr>
<td>uart16550pc.inx</td>
<td>Describes the installation of the driver. The build process converts this into a .INF.</td>
</tr>
</tbody>
</table>
</div>
