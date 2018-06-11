# HidMapper Sample Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDK
* KMDF
* UMDF
* Windows Driver
## Topics
* WDK
* hid
## IsPublished
* False
## ModifiedDate
* 2012-02-29 04:28:49
## Description

<h3>HidMapper Sample Driver</h3>
<p>The HidMapper is a generalized filter driver that provides a bidirectional interface for I/O requests between a non-HIDClass driver and the HID class driver.
</p>
<p>The HidMapper sample uses KMDF and UMDF shim drivers sample code (hidkmdf.sys and hidumdf.sys, respectively). For more information about these samples, see WudfVhidmini and HIDUSBFX2.</p>
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
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/Ff554644">Building a Driver</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;Starting in Windows&nbsp;8 Consumer Preview, the WDK no longer contains the co-installers by default. You can obtain the co-installers by downloading the
<i>wdfcoinstaller.msi</i> package from <a href="http://go.microsoft.com/fwlink/p/?LinkID=226396">
WDK Developer Preview Redistributable Components</a>.</p>
<p>---------------------------------------------------------------------------------------------------------</p>
<p>The Windows Samples Gallery contains a variety of code samples that exercise the various new programming models, platforms, features, and components available in Windows 8 Consumer Preview and/or Windows Server 8 Beta. These downloadable samples are provided
 as compressed ZIP files that contain a Visual Studio solution (SLN) file for the sample, along with the source files, assets, resources, and metadata necessary to successfully compile and run the sample. For more information about the programming models, platforms,
 languages, and APIs demonstrated in this sample, please refer to the guidance, tutorials, and reference topics provided in the Windows 8 documentation available in the Windows Developer Center. This sample is provided as-is in order to indicate or demonstrate
 the functionality of the programming models and feature APIs for Windows 8 and/or Windows Server 8 Beta. Please provide feedback on this sample!</p>
