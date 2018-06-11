# Hardware Event Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:45:51
## Description

<div id="mainSection">
<p>This sample demonstrates two different ways a Windows kernel-mode driver can notify an application about a hardware event. One way uses an event-based method, and the other uses an IRP-based method. Because the sample driver is not talking to any real hardware,
 it uses a timer DPC to simulate hardware events. The test application informs the driver whether it wants to be notified by signaling an event or by completing the pending IRP. Additionally, the test application specifies a relative time at which the DPC timer
 must fire. </p>
<p><i>Event-based approach:</i> The application calls the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ms682396">
<b>CreateEvent</b></a> function to create an event. It then passes the event handle to the driver in an I/O control request that uses a private IOCTL code, IOCTL_REGISTER_EVENT. Because the driver is a monolithic, top-level driver, its IRP dispatch routines
 run in the application process context and, as a result, the event handle is still valid in the driver. The driver dereferences the user-mode handle into system space and saves the event object pointer for later use. Next, the driver queues a custom timer
 DPC. When the DPC fires, the driver signals the event by calling the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff553253">
<b>KeSetEvent</b></a> routine at DISPATCH_LEVEL, and deletes the references to the event object. You can't use this approach if your driver is not a monolithic, top-level driver; that is because a driver can't guarantee the process context in a multi-level
 driver stack if the driver is not at the top of the stack.</p>
<p><i>Pending IRP-based approach:</i> The application makes a synchronous IOCTL_REGISTER_EVENT request. The driver sets the status of the device I/O control request to IRP pending, queues a timer DPC, and returns STATUS_PENDING. When the timer fires to indicate
 a hardware event, the driver completes the pending IRP to notify the application about the hardware event.</p>
<p>There are two advantages of IRP-based approach over the event-based approach. First, the driver can send a message to the application along with the event notification. Second, the driver routines don't have to run in the context of the process that made
 the request. Instead, the application can send a synchronous or asynchronous (overlapped) I/O control request to the driver.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample driver is not a Plug and Play driver. This is a minimal driver meant to demonstrate a feature of the operating system. Neither this driver nor its sample programs are intended for use in a production environment. Rather,
 they are intended for educational purposes and as a skeleton driver.</p>
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
<h2>Run the sample</h2>
<p>To test this driver, copy the test application, event.exe, and the driver to the same directory, and run the application. The application will automatically load the driver, if it's not already loaded, and interact with the driver. When you exit the app,
 the driver will be stopped, unloaded, and removed.</p>
<p>To run the test application, enter the following command in the command window:</p>
<p><code>C:\&gt;event.exe &lt;Delay&gt; &lt;0|1&gt;</code> </p>
<p>The first command-line parameter, <code>Delay</code>, equals the time, in seconds, to delay the event signal. For the second command-line parameter, specify 0 for IRP-based notification and 1 for event-based notification.</p>
</div>
