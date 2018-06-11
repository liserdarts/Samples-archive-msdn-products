# Native Wi-Fi miniport sample driver - USB
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* KMDF
* Windows Driver
## Topics
* Network
* usb
* wireless networking
## IsPublished
* False
## ModifiedDate
* 2012-02-29 04:33:29
## Description

<h3>Native Wi-Fi miniport sample driver - USB</h3>
<p>This sample shows the typical behavior of a Native Wi-Fi USB miniport driver that uses KMDF. This driver is an NDIS 6.0 miniport driver that conforms to the Microsoft Native Wi-Fi miniport driver specification, operating in the Extensible Station (ExtSTA)
 mode. The driver is fully operational and runs on the Realtek RTL8187 chipset. </p>
<p><b>Theory of Operation</b> </p>
<p></p>
<p>The sample driver is logically partitioned into four layers based on functionality:
</p>
<ul>
<li>Miniport layer: Implements NDIS 6.0 and basic Native Wi-Fi miniport driver functionality.
</li><li>Station layer: Implements 802.11 station MAC and state management functionality.
</li><li>Bus-independent hardware layer: Implements bus-independent WLAN hardware-specific functionality like handling encryption/decryption logic, and so on.
</li><li>Bus-dependent hardware layer: Implements bus-dependent hardware-specific functionality such as reading/writing device registers, handling reads/writes, configuring the device, and so on.
</li></ul>
In addition to these four layers, a number of interface functions are defined for interfacing between the various modules. The following sections further describe the four layers and their interfaces.
<p></p>
<p><b>Miniport Layer</b> </p>
<p>The miniport layer consists of functions that are responsible for interacting with the operating system, including handling NDIS 6.0 miniport driver requirements, for example Pause/Restart and Plug and Play (PnP), and some of the basic Native Wi-Fi miniport
 driver requirements such as reassembly of fragmented packets). </p>
<p>The miniport layer functions implement all of the basic NDIS driver functionality and non-hardware specific requirements of a Native Wi-Fi miniport driver. This implementation enables all vendors to reuse the miniport functions as a starting framework for
 their driver. </p>
<p>All functions that belong to the miniport layer have the Mp prefix. All miniport layer functions are present in files that have the Mp_ prefix. For example, Mp_Send.c will handle the Mp portion of sending 802.11 frames.
</p>
<p>All data that the miniport layer functions access resides in the ADAPTER data structure.
</p>
<p><b>Station Layer</b> </p>
<p>The station layer contains functions that maintain Wi-Fi miniport driver state and implement 802.11 MAC functionality, including tracking access points and IBSS stations in the network, connecting to wireless networks, and roaming between networks.
</p>
<p>The sample driver demonstrates one way to implement 802.11 station functionality in a miniport driver. It provides a fully functional station implementation that is simple to understand and modify. All functions that belong to the station layer have the
 Sta prefix. </p>
<p>All station functions are present in files with the St_ prefix. </p>
<p>For example, St_Scan.c implements the scanning logic in the miniport driver. All data that the station functions access reside in the STATION data structure.
</p>
<p><b>Bus-Independent Hardware Layer</b> </p>
<p>The hardware layer contains functions that interact with firmware/hardware, such as programming hardware and sending and receiving 802.11 frames.
</p>
<p>The hardware portion of the sample driver is specific to the Realtek RTL8187 chipset.</p>
<p>All functions that belong to the hardware layer have the Hw prefix. All hardware functions are present in files that have the Hw_ prefix. For example, Hw_Send.c handles the Hw portion of sending 802.11 frames.
</p>
<p>All data that the hardware functions access resides in the NIC data structure.
</p>
<p><b>Bus-Dependent Hardware Layer</b> </p>
<p>The bus-dependent hardware portion deals with bus-specific resources like interrupts, scatter-gather lists for the PCI bus and URBs, KMDF requests, and so on for USB.</p>
<p>All bus-dependent hardware functions are prefixed with HwBus and are defined in the hw_bus_interface.h file. For example, HwBusStopNotification is the function to disable notifications, which would disable interrupts on PCI and stop delivery of completion
 callbacks on USB (hwusbstopnotification). All bus-specific files are prefixed with hw_&lt;bus&gt;_ (for example, Hw_usb_transmit.c). Most of the USB-specific data lies in the USB_DEVICE_CONTEXT data structure.
</p>
<p><b>Interface Functions Between Layers </b></p>
<p>The Miniport/Station/Hardware functions must interact with each other to implement Wi-Fi miniport driver functionality. To maintain a modular organization, interface functions are defined at the entry points of the various modules. The three modules interact
 with each other only through interface functions. The interface functions into the miniport layer have an Mp11 prefix and are defined in the MP_IntF.h file. The interface functions into the station layer have the Sta11 prefix and are defined in the ST_IntF.h
 file. The interface functions into the hardware layer have the Hw11 prefix and are defined in the HW_IntF.h file. These functions are independent of the underlying Bus (PCI, USB, or so on).The interface functions into the bus-dependent hardware layer have
 the hw_bus prefix and are defined in the hw_bus_interface.h file. </p>
<p><b>Implementation and Design</b> </p>
<p>The sample should be used as a reference for a production Native Wi-Fi model Wireless LAN miniport driver. PnP Handling The miniport layer handles all of the Plug and Play (PNP) operations.
</p>
<p>The miniport layer is the entry point for Initialize/Halt/Shutdown and Pause/Restart operations.
</p>
<p>During initialization, the miniport performs the following operations: </p>
<ol>
<li>Allocates and initializes ADAPTER, NIC, and STATION structures. </li><li>Sets up Send and Receive descriptors and sets up mapping between the MP/STA/HW structures for send/receive.
</li><li>Registers Completion callbacks with USB but does not enable the USB target. </li><li>Performs hardware power up and initialization of PHY. </li><li>Performs post-hardware ready initialization in the station layer. </li><li>Enables UsbTarget. </li><li>During halt, the miniport cleans up by undoing the operations that were performed during initialization.
</li><li>Pause and restart operations are handled in the Mp functions. The Mp functions wait for pending send and receive operations to be completed.
</li><li>Reset is also handled by the miniport layer. It will always schedule NDIS_IO_WORKITEM for the Reset and complete it asynchronously. In the work item, the pending send and receive operations will be flushed first and then the three layers will be reset to
 clean up the state. A Dot11 reset request performs similar driver state resets as an NdisReset call. Pause/Restart operations are synchronized with reset operations.
</li></ol>
<p></p>
<p><b>Request Processing </b></p>
<p>The Native Wi-Fi sample driver processes the mandatory OIDs to configure the miniport driver. All OIDs enter the miniport driver at the MPRequest handler. The miniport layer performs verification on each OID. It then calls an appropriate Hw11 or Sta11 function
 requesting it to perform whatever action the OID needs. Some OIDs, like OID_DOT11_RESET_REQUEST, are implemented by miniport layer functions with calls into the appropriate hardware/station functions for the reset operation.
</p>
<p><b>Data Transfer - Send </b></p>
<p>The miniport driver treats operating system-sent packets differently from packets sent internally from within the driver (that is, the association requests). Packet send requests initiated by the operating system enter the miniport through the Mp layer and
 are sent by using the lower-priority queue on a Realtek hardware (in the hardware layer). Internal send requests are directly forwarded to the Hw layer where they are sent using the normal priority queue on the Realtek hardware.
</p>
<ul>
<li>
<p>Operating System Packet Send Requests. The Send Engine in the sample miniport driver is based on a circular queue of transmit descriptors (Tx descriptors). Each descriptor corresponds to an MSDU that must be transmitted. There are two data structures for
 a Tx descriptor, MP_TX_MSDU and NIC_TX_MSDU, that correspond to the Mp layer and Hw layer of the TX_MSDU, respectively. The NIC_TX_MSDU is an opaque data structure and is accessed only by the Hw portion. However, all three layers can look inside an MP_TX_MSDU
 and use any field it might need during a send operation. During the initialization of the miniport, an association will be made between a MP_TX_MSDU and a NIC_TX_MSDU. This binding/association will remain static for the lifetime of the miniport.
</p>
<p>Each time the Mp layer needs to do a packet send, it first calls the Sta layer for preprocessing of the request. The sample driver's station implementation does not perform any modification on the send side. The Mp layer then calls into the Hw layer and
 passes the corresponding MSDU to be transmitted. </p>
<p>On every completion, the MP layer calls the HW and STA functions to undo any modifications/initializations that would have been done during send requests before completing the send request to the operating system.
</p>
<p>If packets cannot be sent immediately for any reason (for example, scanning, not enough hardware resources), the Mp layer queues the packets. The pending send queue will be checked and processed when a send complete happens.
</p>
</li><li>
<p>Internal Sends. Internal send requests are used for packets generated from within the driver. For example, this type of request is used when sending association packets. The driver creates the packet and forwards it directly to the hardware layer for sending.
 The sample driver's hardware layer copies the packet data into the hardware descriptor and directly sends them out. There is no send complete notification for the internal packet sends.
</p>
</li><li>
<p>Notes for the vendor. The sample miniport driver can differentiate between internal and operating system send requests because of the different packet layer queues that the hardware supports. If the hardware does not support separate queues, the Hw layer
 would need to synchronize access to descriptors between send requests initiated by the operating system and ones initiated internally. Alternatively, you can pipe the internal send requests through the operating system send code path in the miniport layer.
</p>
</li></ul>
<p>You can choose to implement NIC_TX_MSDU chain as a circular list, circular array, or any other implementation as long as it fits the model described earlier.
</p>
<p><b>USB Transmit Path </b></p>
<p>The Realtek 8187 device supports 2 BULK OUT endpoints. One is for sending all Management and Data frames and the other one is for sending the higher priority beacon frames. The driver preallocates the USB transmit resources like the KMDF memory for coalesce
 buffers, USB URBs, and KMDF requests and timers that are associated with the requests.
</p>
<p>The sample coalesces all of the MDLs in the Net Buffers into the coalesce buffer. That is because the USB URBs cannot handle chained MDLs. Also, another reason for coalescing is that the Transmits have a Tx Descriptor that needs to be appended at the beginning
 of the frame so it does not work when there is only 1 MDL. </p>
<p>All Transmit requests have a timer associated with them. Cancellation of request for reset/halt is very simple with KMDF.
</p>
<p><b>Data Transfer - Receive</b> </p>
<p>The Receive Engine for the sample miniport driver is based on a singly-linked list of fragments. Each fragment corresponds to one 802.11 MPDU received. There are two data structures for a fragment: MP_RX_FRAGMENT and NIC_RX_FRAGMENT, which correspond to
 the Mp/Sta layers and the Hw layer of the received MPDU, respectively. For the Mp/Sta layers, the NIC_RX_FRAGMENT is an opaque data structure that can be accessed only through Hw Interface functions. However, all portions can look inside an MP_RX_FRAGMENT
 and use any fields they might need. During the initialization of the miniport driver, an association will be made between an MP_RX_FRAGMENT and a NIC_RX_FRAGMENT, which will remain static for the lifetime of the miniport driver.
</p>
<p>Each time a frame is received, an Mp function will ask the Hw layer to provide a pointer to the NIC_RX_FRAGMENT in which the received packet is present. It will forward all received packets to the Sta function for processing. Given the NIC_RX_FRAGMENT, Mp/Sta
 functions can then query all of the information needed (like the associated MP_RX_FRAGMENT, length of the received frame, pointer to the received frame, and so on) using Hw11 interface functions.
</p>
<p>All received packets will be forwarded to the Sta functions, but depending on NDIS packet filter, the Mp layer will not forward some packets to NDIS. For packets that are indicated to NDIS, the Mp layer will handle reassembling of received MPDUs into an
 MSDU before indicating it to NDIS. The Mp layer stores the reassembled fragments in an MP_RX_MSDU structure that maintains the list of MP_RX_FRAGMENTs that form the MSDU. To ensure correct reassembly, the Hw functions need to ensure that 802.11 frames are
 received and indicated up in order. </p>
<p>On a receive return, the MP function returns the NIC_RX_FRAGMENT to the Hw layer. Because NET_BUFFER_LISTs can be returned out of order, the ordering of NIC_RX_FRAGMENTs returned might not be the same as the order of receive indicates. The Hw portion does
 not try to keep a static ordering of NIC_RX_FRAGMENTs, adding the returned fragments to the end of the list and updating the hardware descriptors appropriately to handle this out of order behavior.
</p>
<p>The driver also supports growing and shrinking of the number of allocated fragments if the number of available receive buffer (receive buffer not indicated) goes low.
</p>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp; Notes for the vendor. Make sure that any synchronization that is needed between receive indication and return of fragments is handled appropriately. Also, if the miniport driver starts to run low on fragments, the fragment pool
 can be dynamically grown. The Hw11 interface function is called to increase the number of fragments in the pool and must handle synchronization between return of fragments and allocation of new fragments.
</p>
<p></p>
<p>The data structure that used to maintain the NIC_RX_FRAGMENT is at your discretion.
</p>
<p><b>USB Receive Path</b> </p>
<p>KMDF offers a feature called the Continuous reader for doing USB reads that enables reading from the IN endpoint very easy because KMDF allocates the buffers (whose size is specified at configuration time) and it takes over the job of reposting the BULK
 read request. When it had data read, it calls the drivers read completion callback.In the read completion callback, KMDF handles the driver a WDFMEMORY object for the read buffer that the framework allocates and then it reclaims at the end of the callback.
 To prevent the framework from freeing the buffer, the driver adds a reference to the WDFMEMORY object and allocates an MDL structure at the same time. There is some space dedicated to the MDL being returned to NDIS as the header of the Memory Object.
</p>
<p>The device's Rx Descriptor is at the end of the 802.11 frame, so it has to be subtracted while reporting the frame size to NDIS.
</p>
<p>After NDIS returns the receive buffer back, the MDL and the reference on the buffer are freed so that it can be reused.
</p>
<p><b>Reading/writing USB device registers</b> </p>
<p>In PCI, reading/writing device registers can be done synchronously at DIRQL; this is not the case with USB. Sending synchronous vendor control request to read/write device registers can occur only at IRQL PASSIVE_LEVEL. Hence synchronously reading from and
 writing to registers cannot be done from DPCs and timer DPCs. </p>
<p><b>Bus-specific functions</b> </p>
<p>Because a lot of the hardware code is independent of the type of bus used and a lot of vendors support wireless devices that use different bus technologies like PCI, USB, and so on, this abstraction allows reuse of bus independent code. The hw_bus_interface.h
 file has the bus-specific functions defined: </p>
<p></p>
<ul>
<li>Allocation of transmit/receive resources for USB include URBs, KMDF request, timers, and so on, whereas for PCI that includes the Scatter gather lists.
</li><li>Enabling /disabling notifications, which is Completion callbacks on USB and interrupts on PCI.
</li><li>Reading//writing device registers, which would be reading and writing to ports on PCI and using vendor-defined control commands on USB.
</li></ul>
NDIS control device Control device is created per device, which enables an application to communicate with the driver by using IOCTLs. In this driver, the control device is used to service read/write device registers IOCTLs. This tool is very useful in debugging.
 Dumpregister.exe is the application under the /exe folder that shows how to do this.
<p></p>
<p><b>BSS Discovery</b> </p>
<p>The Sta layer of the driver is responsible for keeping track of discovered access points and IBSS stations. The Sta layer also periodically initiates the scan operation to collect information about other stations. The Hw layer performs the actual scan by
 switching through the various channels and sending probe requests and so on. Scanning is started either by operating system requests or internally for roaming purposes.
</p>
<p>The Sta layer is continuously listening for beacons and probe responses (even when not performing a scan). When it receives a beacon/probe response, the Sta layer copies the BSS information from the packet into a STA_BSS_ENTRY data structure. A list of these
 entries is kept in a linked list and accessed during association, roaming, or enumeration operations.
</p>
<p><b>Association - Infrastructure</b> </p>
<p>Association in the miniport driver is implemented in the Sta layer. The infrastructure association process consists of the following steps:
</p>
<ol>
<li>Synchronize hardware timers with the access point timer. </li><li>Exchange 1 pair or 2 pairs of authentication packets with the access point depending on open or shared key authentication.
</li><li>Send association request packet and receive association response packet from the access point.
</li></ol>
This process is asynchronous and the driver needs to handle the normal/expected sequence of packet exchange with the access point and handle access points that do not respond to requests, sending unexpected responses to packets (deauthentication in response
 to an association request packet), and so on. Additionally, the operating system might reset/halt the driver because the association is taking too long, or some user action like a surprise removal or driver halt.
<p></p>
<p>To handle these asynchronous events, the driver uses a state machine for the association process. There are two state variables, a connection state that keeps track of whether the operating system expects the driver to be connected to an access point and
 an associate state that tracks the progress of association attempts with the access point. The various possible states are:
</p>
<ul>
<li>Connection State
<p>There are four possible states for the connection state variable:</p>
<ul>
<li>Disconnected: The driver should not attempt to associate and ,if it is associated, should disconnect. The Disconnected state is entered after receiving a DISCONNECT OID. The driver ends any existing connection.
</li><li>In Reset: The operating system is resetting the adapter (either by OID or NdisReset) or halting the adapter. The driver waits for in progress association to complete and then resets back to the disconnected state.
</li><li>OK to connect: The operating system expects the driver to start connecting. The driver would attempt to find and associate with a candidate access point. This state is set on receiving a CONNECT OID and is maintained until the driver has made a connection
 attempt. </li><li>OK to roam: The operating system expects the driver to stay connected and, if the connection is lost, to roam to a new access point.
</li></ul>
<p></p>
<p>Association State</p>
<p>Possible association states are:</p>
<ul>
<li>Not associated: This state is the initial state when the driver is not associated and has not started the association process. While in this state, the driver will not attempt to associate.
</li><li>Ready to associate: Set when the station is ready to start the association process or has failed a previous association and might be restarting the association. While in this state, the driver might attempt to connect or roam between networks. This state
 is also used as a synchronization point between roam and connect. </li><li>Association started: Set when the station has started the association process. This state is set just after it has selected an access point to start the association attempt and has indicated to the operating system the association start status indication.
</li><li>Waiting for join: Set when the station has requested that the hardware function synchronize with the access point and is waiting for the hardware function to return.
</li><li>Joined: Set after the hardware function has returned having successfully synchronized with the access point.
</li><li>Deauthenticated: Special state set when the station receives a deauthenticate packet from the access point while it still had not completed association. This state is not set on normal code path. It is used to ensure that the station does not complete the
 association successfully if it receives a deauthenticate packet from the access point.
</li><li>Waiting for authentication completion: Set when the station is waiting for an authentication packet from the access point. For open authentication, this state is entered while waiting for a packet with sequence number 2. For shared authentication, this
 state is set until a packet with sequence number 4 is received. </li><li>Authenticated: Set when the station has received successful authentication response from the access point.
</li><li>Disassociated: Special state set when the station gets disassociate request from the access point while it still has not completed association. This state is not set on normal code paths. It is used to ensure that the driver does not complete the association
 successfully if it receives a disassociate packet from the access point. </li><li>Waiting for association response: Set when the station is waiting for association response packet from the access point.
</li><li>Associated: Set when the station has received successful association response from the access point.
</li><li>Association completed: Association process completed successfully and association completion status has been indicated. After this state is set, any disassociate/deauthenticate packet from the access point would raise a disassociation status indication.
</li></ul>
<p></p>
<p><b>Association Process </b></p>
<p>The association process is asynchronous. Each step is triggered by the successful completion of the previous step. In case of a failure or time-out, the appropriate cleanup routines are called to perform cleanup and retry. The steps for association process
 in the miniport driver are listed below: </p>
<ol>
<li>Using the operating system-specified parameters like SSID and BSSID, determine the list of candidate access points that the driver can associate with. The driver first determines if all of the required parameters are set and uses defaults for optional parameters
 that are not set to determine the candidate list. The driver then ranks the access points according to the signal strengths with the highest strength access point at the top of the list.
</li><li>Pick the first access point in the list and begin the association process by indicating the association start status indication.
</li><li>Ask the Hw layer to attempt to synchronize timers with the access point. In the sample driver, the Hw layer passively listens for beacons from the access point and also handles time-out of the join by using a timer. Time-out is assumed if join does not
 complete in the specified number of beacon intervals. </li><li>If the hardware timed out waiting for synchronization or failed for some reason, proceed to step 12.
</li><li>If synchronization was successful, start the authentication process. </li><li>Set a timer to handle the authentication process time-out and send the first authentication packet. The driver responds as if a time-out occurred if the authentication process is not completed in a specified number of seconds.
</li><li>If the authentication time-out timer expires before the authentication process completes or authentication fails, proceed to step 12.
</li><li>If all required authentication packet exchanges complete within the time-out interval and the authentication is successful, continue to the association process.
</li><li>Set a timer to handle association process time-out and send the association request packet. The driver responds as if a time-out occurred if the association response is not received in a specified number of seconds.
</li><li>If the association time-out timer expired before an association response was received or the association failed, proceed to step 12.
</li><li>If a successful association response was received before time-out, complete the association process and call the association complete status indication. At this point, the driver is done.
</li><li>In case of a failure, first call an association completion status indication.
</li><li>Check if any other candidate access points are present to association. If yes, go back to step 1, and restart the association process with the new access point.
</li></ol>
Depending on whether the association process was initiated in response to a connect OID or for a roaming scenario, the driver might indicate the appropriate connection or roaming start/complete status indications before and after the association process.
<p></p>
<p><b>Stopping the Association </b></p>
<p>The multi-step association process might need to be stopped because of Halt/Reset/Surprise Removal operations. For simplicity, the driver obeys a rule internally with regards to stopping associations. After an association start status has been indicated,
 the driver will complete the process and indicate the association completion status indication. This indication implies that the Reset/Halt operations would have to wait for the association process to complete. To accelerate the abort (stop) operation, the
 association process will check the connection state variable at safe points in the association process and if connection has to be stopped, it will not proceed to the next step in the association process and will complete the association sooner. The reset
 routine will also manually trigger the time-out timers and cause them to behave as if a time-out has happened to avoid long reset times.
</p>
<p><b>Association - Independent BSS</b> </p>
<p>The following process describes how an IBSS connection works: </p>
<ol>
<li>The driver has a list of beaconing IBSS station through active or passive scanning. The driver goes through the list and finds all of the stations that match the desired SSID and BSSID list.
</li><li>For each IBSS station found in step 1, the miniport driver steps through the following process until the start request succeeds for a station.
<ol>
<li>Initiate a join attempt by using the station's BSSID and SSID. </li><li>If the join completes successfully, do a start request by using the station's BSSID, SSID, and ATIM.
</li></ol>
</li><li>If step 2 fails, the miniport will start its own IBSS network. </li><li>Indicate connection start and connection complete. At this step, no station is indicated as associated.
</li><li>When a beacon frame or probe response frame is received from an IBSS station, if it has not been indicated as associated but it matches with the miniport's BSSID/SSID, indicate association start and association complete for the station. If the station has
 been indicated as associated but its SSID/BSSID does not match, indicate disassociation for the station.
</li><li>The miniport driver has a timer routine that disassociates any station from which no beacon or probe response frame is received for a specified period of time. It also removes the station from the list if no beacon or probe response is received within a
 specified period of time. This period of time is controlled by using the UnreachableThreshold value that the operating system sets. The timer itself runs every 2 seconds.
</li></ol>
<p></p>
<p><b>Roaming</b> </p>
<p>Roaming logic is plugged into the association state machine. The miniport attempts to roam only if the connection state variable indicates that the operating system wants the miniport to be connected.
</p>
<p>Roaming is initiated by an NDIS timer that is periodically run. The timer routine roams when any of the following conditions are true:
</p>
<ul>
<li>The miniport got disconnected from the access point because of disassociation/deauthentication or because it had to drop the connection because of some other reason (AP was added to rejected peer, for example).
</li><li>The miniport has not received a beacon from the access point it is associated with for a period of time. In this case, it will perform a scan and roam only if a better access point is found. If no beacon is received for a long time but no new access points
 are found either, the miniport will perform a forced disconnect to let the user know about lost connectivity.
</li><li>The signal strength of the beacons from the currently associated access point is below a threshold value for a sequence of beacons. In this case, the periodic timer performs a scan and will roam only if a better access point is found.
</li><li>If the decision to roam is made, the periodic timer scans to find a new list of access points that it can associate with. If it finds candidate access points to associate with, it will start the association process at step 1 of the association, after disconnecting
 from any currently associated access point. </li></ul>
<p></p>
<p><b>Extension Guidelines</b> </p>
<p>Customizing the sample miniport driver can be a relatively easy and efficient task if the following guidelines are followed.
</p>
<ul>
<li>Minimize changes to the Mp files. Most of the code in Mp files handles interaction with NDIS. Value-added functionalities can be implemented in the Hw or Station layer in the appropriate interface functions. Key Mp functions make calls into the Mp_Events.c
 file, which can be used to modify the Mp behavior. Any data that must be maintained can be done in the NIC/STATION data structure. As a result of not making changes to the Mp files, new features and/or bug fixes introduced in updated versions of the sample
 can easily be integrated in your own driver. </li><li>Use the MpTrace macro for tracing through the code. With MpTrace, you can switch between Event Tracing and traditional debug output with a simple recompile. Event Tracing is far superior, in terms of performance, to debug output and gives the tester a better
 chance to catch synchronization issues in the driver. You can keep it turned on throughout the development cycle of the driver with negligible overhead. If you always use the MpTrace macro (with tracing enabled from the sources file), tracing will automatically
 be provided for the miniport. Make sure to include the corresponding .tmh file in all source files (*.c) that use the tracing macro.
</li><li>Separate private OID/IRP code from Mp layer. If you must add private OIDs, refer to the MP_PRIVATE_OID_LIST macro that is provided in the Mp_Events file. With MP_PRIVATE_OID_LIST, you can add private OIDs without changing the MP files. Similarly, if you
 must add a Device Object to handle IRPs, you can create your own device object during initialization, when both Hw11 and MpEvent interface functions are called.
</li></ul>
<p></p>
<p><b>Synchronization</b> </p>
<p>The following list shows key ways that the miniport driver handles synchronization.
</p>
<ul>
<li>Send Path: The ADAPTER's SendLock is used by the Mp layer to handle synchronization on the send code paths. On a MiniportSend call, the Mp layer holds this lock when reserving TX descriptors for all NET_BUFFER_LISTs in the send. After the descriptors are
 reserved, the SendLock is released while the descriptors are being populated. After the TX descriptors are populated, one send thread walks through all of the descriptors that have been prepared and sends them down to Hw layer for transmission. This submission
 process is done without the SendLock held but uses interlocked operations on the flags in the TX descriptors (MpSendSingleTxMSDU and MpSendReadyTxMSDUs). The SendLock is used during send complete to return the TX descriptors back to the free list as well as
 to submit any queued sends to the Hw layer. The SendLock is also used for synchronizing the Reset operations with send/send complete.
</li><li>Receive Path: The NIC's RxInfo.FragListLock is called on the receive side to synchronize between the ReceiveIndicate, Return, and Reset operations. This path is used by the Hw layer when checking for receives, delinking received descriptors from the fragment
 list, and reinserting returned descriptors to the end of the fragment list. </li><li>Reset/Pause/Restart: The ADAPTER's ResetPnPMutex is used for synchronizing Reset, Pause, and Restart operations. This use is necessary because NDIS cannot guarantee serialization between NdisReset, OID_DOT11_RESET_REQUEST and Pause/Restart operations. The
 Mp layer uses this mutex to serialize between these and only one of the four operations can modify the send/receive states at a time.
</li><li>BSS list: The STATION's BSSList.ListLock is used for synchronizing access to the BSS list that is maintained by the station. The lock is acquired on the receive side for received beacon/probe responses and acquired during association to find candidate BSS
 entries. This lock protects only the linked list and each BSSEntry has its own lock that protects the data inside the entry (the beacon buffer, for example). When acquiring both the BSSList lock and the BSSEntry lock, the sample driver always acquires the
 BSSList lock before it acquires the BSSEntry lock. </li><li>Infrastructure association: The infrastructure association code in the miniport driver uses a state machine and uses the states for synchronization. On each association-related event (authentication response, for example), the driver checks and updates
 the state variable to determine if it can proceed forward and to block out other events.
</li></ul>
For example, the ReadyToAssociate association state is used to synchronize between the roaming and connect threads, and only one thread would proceed forward. The state transitions are all protected by the ConnectContext lock. When acquiring both the ConnectContext
 lock and the BSSList lock, the sample driver always acquires the ConnectContext lock before the BSSList lock.
<p></p>
</li></ul>
<h3>Operating system requirements</h3>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows 8 Consumer Preview </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server 8 Beta </dt></td>
</tr>
</tbody>
</table>
<h3>Build the sample</h3>
<h3>Run the sample</h3>
<p>Create an /RTLSample directory and, depending on whether you are running 32-bit or 64-bit versions of Windows Vista, copy the Wifiusb.sys or Netwfusb.inf file to the /RTLSample directory.
</p>
<p>INF Changes for a 802.11 Driver </p>
<p>Under the ndi.NT$ARCH$ section in your INF file, add the following lines: </p>
<p>*IfType = 71 ; IF_TYPE_IEEE80211 </p>
<p>*MediaType = 16 ; NdisMediumNative802_11 </p>
<p>*PhysicalMediaType = 9 ; NdisPhysicalMediumNative802_11 </p>
<p>If you are adapting this driver for your device, make sure the INF file has been updated to match the hardware ID (VID, PID), and the device description text to match your device. Make sure you have the KMDF coinstaller (WdfcoinstallerMMmmm.dll) in the same
 folder as the Netwfusb.inf and Wifiusb.sys file. </p>
<p>You must update DriverVer in Netwfusb.inf for different versions of the driver so that the operating system can identify that the driver is new.
</p>
<h3><a name="code_tour"></a>Code tour</h3>
<p>
<table>
<tbody>
<tr>
<th>File manifest</th>
<th>Description</th>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_main.c</td>
<td>Miniport layer entry points from NDIS</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_main.c </td>
<td>Miniport layer entry points from NDIS </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_oids.c </td>
<td>Miniport layer OID processing code </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_recv.c </td>
<td>Miniport layer receive functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_send.c </td>
<td>Miniport layer send functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_power.c </td>
<td>NDIS Power management handling interfaces </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\mp_cntl.c </td>
<td>Handler for user mode IOCTLs </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\netwfusb.inx </td>
<td>INF file to install this driver </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_oids.c </td>
<td>Station layer OID processing code</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_scan.c </td>
<td>Station layer OS/internal scan functions</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_aplst.c </td>
<td>Station layer BSS list functionality</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_conn.c </td>
<td>Station layer infrastructure connection/roaming functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_auth.c </td>
<td>Station layer authentication frame processing functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_assoc.c </td>
<td>Station layer association frame processing functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\extsta\st_adhoc.c </td>
<td>Station layer adhoc connection functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\hw_main.c </td>
<td>Hardware layer PNP functions</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\hw_nic.c </td>
<td>Hardware layer NIC specific function</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\hw_oids.c </td>
<td>Hardware layer OID processing code </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\hw_send.c </td>
<td>Hardware layer send functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\hw_recv.c </td>
<td>Hardware layer receive functions</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\bus_intf.c </td>
<td>Interfaces from the generic hardware layer into the bus-dependent hardware layer
</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\bus_intf.c </td>
<td>Interfaces from the generic hardware layer into the bus-dependent hardware layer
</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\usb_main.c </td>
<td>Bus-dependent layer USB specific function </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\usb_reg.c </td>
<td>USB registry access helper functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\usb_xmit.c </td>
<td>Bus-dependent layer USB specific transmit functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\hw\usb_recv.c </td>
<td>Bus-dependent layer USB specific receive functions </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\mp_def.h </td>
<td>Miniport layer global defines </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\mp_intf.h </td>
<td>Interface functions into Miniport layer</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\st_def.h </td>
<td>Station layer global defines </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\st_intf.h </td>
<td>Interface functions into Station layer </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\hw_def.h </td>
<td>Hardware specific defines common to the complete driver </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\hw_intf.h </td>
<td>Interface functions into Hardware layer </td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\inc\hw_func.h </td>
<td>Interfaces into Realtek hardware specific library</td>
</tr>
<tr>
<td>src\network\ndis\usbnwifi\lib\rtlpriv.lib </td>
<td>Library containing Realtek hardware specific code</td>
</tr>
</tbody>
</table>
</p>
