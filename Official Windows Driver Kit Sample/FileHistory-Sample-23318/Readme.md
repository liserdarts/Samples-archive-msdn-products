# FileHistory Sample
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* File System
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:15:21
## Description

<div id="mainSection">
<p>The FileHistory sample is a console application that starts the file history service, if it is stopped, and schedules regular backups. The application requires, as a command-line parameter, the path name of a storage device to use as the default backup target.
</p>
<p>This sample application uses the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/hh829789">
File History API</a>, which is available starting with Windows&nbsp;8. The File History API enables third parties to automatically configure the File History feature on a Windows platform and customize it in accordance with their unique needs.</p>
<h3>Operating system requirements</h3>
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
<h3>Build the sample</h3>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h3>Run the sample</h3>
<p>The name of the built sample application is Fhsetup.exe. To run this application, open a command window and enter a command that has the following format:</p>
<p><code>fhsetup &lt;path&gt;</code> </p>
<p>The <code>path</code> command-line parameter is the path name of a storage device to use as the default backup target. The following are examples:</p>
<p><code>fhsetup D:\</code> </p>
<p><code>fhsetup \\server\share</code> </p>
<p>If the specified target is inaccessible, read-only, an invalid drive type (such as a CD), already being used for file history, or part of the protected namespace, the application fails the request and does not enable file history on the target.</p>
</div>
