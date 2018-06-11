# Near-Field Proximity Sample Driver (UMDF Version 1)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* UMDF
* Windows Driver
## Topics
* Near Field Proximity (NFP)
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:24:43
## Description

<div id="mainSection">
<p>This sample demonstrates how to use User-Mode Driver Framework (UMDF) version 1 to write a near-field proximity driver.
</p>
<p>Typically, a near-field proximity driver would use near-field technologies such as Near Field Communication (NFC), TransferJet, or Bump. However, this sample uses a TCP/IPv6 network connection and a static configuration between two machines to simulate near-field
 interaction.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Related technologies</h2>
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560456">User-Mode Driver Framework</a>
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
<p>The sample driver can be built using any of the build environments from Windows&nbsp;8.1 WDK.</p>
<p>Load NetNfpProvider.vcxproj in Microsoft Visual Studio. Use Visual Studio to build the sample. If the build succeeds, you will find the driver, NetNfpProvider.dll in the binary output directory specified for the build environment.
</p>
<p>For more information about building driver solutions using Visual Studio, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">
Building a Driver</a>.</p>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>To install the NetNfpProvider driver:</p>
<ol>
<li>Copy the driver binary, NetNfpProvider.inf, and the UMDF coinstaller, WUDFUpdate_<i>MMmmmm</i>.dll to a directory on your test machine (for example c:\NetNfpProvider).
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
</li><li>Change to the directory containing the inf and binaries (for example <b>cd /d c:\NetNfpProvider</b>.)
</li><li>Next run devcon.exe as follows:
<pre class="syntax"><code>devcon.exe install NetNfpProvider.inf WUDF\NetNfpProvider</code></pre>
<p>DevCon can be found in the tools directory of your WDK enlistment (for example: c:\winddk\&lt;bld&gt;\tools\devcon\i386\devcon.exe.)</p>
</li><li>Create a Windows Firewall rule to allow the NetNfpProvider to receive proximity simulation requests over the network. For example, run WF.msc, and create a new inbound rule that opens port 9299.
</li></ol>
<p>To update the NetNfpProvider driver after making any changes:</p>
<ol>
<li>Increment the version number found in the INF. While this is not strictly necessary, it will help ensure PnP selects your new driver as a better match for the device.
</li><li>Copy the updated driver binary and the NetNfpProvider.inf file to a directory on your test machine (for example
<b>c:\ NetNfpProvider</b>.) </li><li>Change to the directory containing the inf and binaries (for example <b>cd /d c:\ NetNfpProvider</b>.)
</li><li>Next run devcon.exe as follows:
<pre class="syntax"><code>devcon.exe update NetNfpProvider.inf WUDF\NetNfpProvider</code></pre>
</li></ol>
<h2><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p>To test the NetNfpProvider driver, you can run NetNfpControl.exe which is built from the src\nfp\net\exe.</p>
<p>First install the device as described above. Then run NetNfpControl.exe from one command window.
</p>
<p>NetNfpControl console app allows control of the NetNfpProvider test driver. Both the local and remote machine must have the NetNfpProvider driver installed.
</p>
<p>USAGE: </p>
<div class="code"><span>
<table>
<tbody>
<tr>
<th></th>
</tr>
<tr>
<td>
<pre>Windows Near-field Proximity Test tool. Designed for simulating proximity hardware.
NetNfpControl.exe [&lt;remoteMachine&gt;] [/k]
</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
Example: <b>NetNfpControl.exe John-PC1</b>
<p></p>
<p>The first operating mode allows the user to specify a remote machine name (or IPv6 address) that the local machine should connect to and simulate proximity with. After it's connected, a simple key-press ends the simulated proximity link. The console app
 then exits. </p>
<p>Example: <b>NetNfpControl.exe John-PC1 /k </b></p>
<p>A second operating mode keeps the console app running with a Ctrl-F1 hotkey registered (even when running in the background). When the hot key is intercepted, a near-field proximity event is simulated directly with the specified remote machine.
</p>
<p></p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The console app needs to be running (only on one machine) to intercept the system hot key.</p>
Example: <b>NetNfpControl.exe /k </b>
<p></p>
<p>A third operating mode also keeps the console app running with a Ctrl-F1 hotkey registered. However, you'll have to run this on two or more machines at the same time. Pressing Ctrl-F1 on any two machines at the same time causes the machines to exchange their
 network name via a file on a private share with a special file name.</p>
<ul>
<li>The server share used is hard coded to: \\scratch2\scratch\travm\proxrendezvous\. Either create a file server with these folders shared, or change this to match yours.
</li><li>The file has an effective lifetime of 2 seconds. </li><li>In the event of a collision (two clients posting an event during the same interval), only one client 'wins'.
</li></ul>
</div>
