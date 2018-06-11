# Fakemodem Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* ndis
* Windows Driver
## Topics
* Networking
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:15:14
## Description

<div id="mainSection">
<p>The Fakemodem sample demonstrates a simple controller-less modem driver. This driver supports sending and receiving AT commands using the
<code>ReadFile</code>/<code>WriteFile</code> calls or via a TAPI interface using an application such as
<i>HyperTerminal.</i> </p>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK 8 Redistributable Components</a>.</p>
<h3>Run the sample</h3>
<p>To install and test this driver, you do not need any real hardware. You can install this driver either using Devcon.exe (ddk\tools\devcon) utility from the DDK or using toaster bus driver sample.
</p>
<p class="proch"><b>To install the driver using toaster bus:</b></p>
<ol>
<li>Install the toaster bus driver. Information on how to install the toaster bus driver is given in src\general\toaster\toaster.htm readme file.
</li><li>Run the notify.exe (src\general\toaster\exe\notify) and choose Plug In a device in the Bus menu.
</li><li>In the plug in dialog, specify <b>{b85b7c50-6a01-11d2-b841-00c04fad5171}\fakemodem</b> as the Hardware Id and click okay.
</li></ol>
<p>The bus driver will enumerate a device with the hardware id provided by the application.</p>
<p>Once installed, you can talk to the fakemodem driver through HyperTerminal, or via ReadFile/WriteFile calls. The AT command set supported by fakemodem includes:</p>
<table>
<tbody>
<tr>
<td><b>AT</b> </td>
<td>returns <b>OK</b></td>
</tr>
<tr>
<td><b>ATA</b> </td>
<td>returns <b>CONNECT</b></td>
</tr>
<tr>
<td><b>ATD&lt;number&gt;</b> </td>
<td>returns <b>CONNECT</b></td>
</tr>
</tbody>
</table>
<h3><a id="File_Manifest"></a><a id="file_manifest"></a><a id="FILE_MANIFEST"></a>File Manifest</h3>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>readwrit.c</td>
<td>Routines for receiving and sending text. This includes processing AT commands.</td>
</tr>
<tr>
<td>Driver.c </td>
<td>Fakemodem driver initialization entry points and for handling Plug &amp; Play events.</td>
</tr>
<tr>
<td>ioctl.c </td>
<td>Routines to handle IOCTL messages.</td>
</tr>
<tr>
<td>mdmfake.inf </td>
<td>INF file for installing the fakemodem driver</td>
</tr>
</tbody>
</table>
</div>
