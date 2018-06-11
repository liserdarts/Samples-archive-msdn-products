# FileHistory Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* File System
* Windows Driver
## Topics
* general
## IsPublished
* True
## ModifiedDate
* 2014-04-04 11:24:27
## Description

<div id="mainSection">
<p>The FileHistory sample is a console application that starts the file history service, if it is stopped, and schedules regular backups. The application requires, as a command-line parameter, the path name of a storage device to use as the default backup target.
</p>
<p>This sample application uses the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh829789">
File History API</a>, which is available starting with Windows&nbsp;8.1. The File History API enables third parties to automatically configure the File History feature on a Windows platform and customize it in accordance with their unique needs.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. This sample will not build with Microsoft Visual Studio Express&nbsp;2013 for Windows Desktop, because the sample
 uses Active Template Library (ATL). You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
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
<td><dt>None supported </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p>The name of the built sample application is Fhsetup.exe. To run this application, open a command window and enter a command that has the following format:</p>
<p><code>fhsetup &lt;path&gt;</code> </p>
<p>The <code>path</code> command-line parameter is the path name of a storage device to use as the default backup target. The following are examples:</p>
<p><code>fhsetup D:\</code> </p>
<p><code>fhsetup \\server\share</code> </p>
<p>If the specified target is inaccessible, read-only, an invalid drive type (such as a CD), already being used for file history, or part of the protected namespace, the application fails the request and does not enable file history on the target.</p>
</div>
