# Cancel-Safe IRP Queue Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:14:05
## Description

<div id="mainSection">
<p>This sample demonstrates the use of the cancel-safe queue routines <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff549054">
<b>IoCsqInitialize</b></a>, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff549066">
<b>IoCsqInsertIrp</b></a>, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff549070">
<b>IoCsqRemoveIrp</b></a>, <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff549072">
<b>IoCsqRemoveNextIrp</b></a>. These routines were introduced in Windows XP for queuing IRPs in the driver's internal device queue. By using these routines, driver developers do not have to worry about IRP cancellation race conditions. A common problem with
 cancellation of IRPs in a driver is synchronization between the cancel lock or the InterlockedExchange in the I/O Manager with the driver's queue lock. The
<b>IoCsq<i>Xxx</i></b> routines abstract the cancel logic while allowing the driver to implement the queue and associated synchronization.
</p>
<p>The sample is accompanied by a simple multithreaded Win32 console application to stress-test the driver's cancel and cleanup routines.</p>
<p>This driver is written for an hypothetical data-acquisition device that requires polling at a regular interval. The device has some settling period between two successive reads. On a user request, the driver reads data and records the time. When the next
 read request comes in, the driver checks the interval to see if it's reading the device too soon. If so, it pends the IRP and sleeps for a while, and then tries again. On arrival, IRPs are queued in a cancel-safe queue and a semaphore is signaled. A polling
 thread that waits indefinitely on the semaphore wakes up to the signal and processes queued IRPs sequentially.</p>
<p>The building and installation instructions given here apply to Windows 2000 and later versions of Windows.</p>
<p>This sample driver is not a Plug and Play driver. This is a minimal driver meant to demonstrate a feature of the operating system. Neither this driver nor its sample programs are intended for use in a production environment. Instead, they are intended for
 educational purposes and as a skeleton driver.</p>
<p>Look in the Startio directory for another version of the sample driver that shows how to use cancel-safe IRP queues to implement I/O queuing functionality similar to the
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff550370"><b>IoStartPacket</b></a> and
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff550358"><b>IoStartNextPacket</b></a> routines. The same test application works with this driver as well.</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff540755">
Cancel-Safe IRP Queues</a>.</p>
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
<h3>Run the sample</h3>
<p>To test this driver, run Testapp.exe, which is a simple Win32 multithreaded console application. The driver will automatically load and start. When you exit the application, the driver will stop and be removed.</p>
<p><code>Usage: testapp &lt;NumberOfThreads&gt;</code> </p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The <code>NumberOfThreads</code> command-line parameter is limited to a maximum of 10 threads; the default value if no parameter is specified is 1. The main thread waits for user input. If you press Q, the application exits gracefully;
 otherwise, it exits the process abruptly and forces all the threads to be terminated and all pending I/O operations to be canceled. Other threads perform I/O asynchronously in a loop. After every overlapped read, the thread goes into an alertable sleep and
 wakes as soon as the completion routine runs, which occurs when the driver completes the read IRP. You should run multiple instances of the application to stress test the driver.</p>
</div>
