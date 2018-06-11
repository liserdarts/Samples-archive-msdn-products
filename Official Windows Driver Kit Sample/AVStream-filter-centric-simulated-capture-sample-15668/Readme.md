# AVStream filter-centric simulated capture sample driver (Avssamp)
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* Streaming Media
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:50
## Description

<div id="mainSection">
<p>The AVStream filter-centric simulated capture sample driver (Avssamp) provides a filter-centric
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554240">AVStream</a> capture driver with functional audio. This streaming media driver performs video captures at 320 x 240 pixel resolution in RGB24 or YUV422 format while playing a user-provided
 Pulse Code Modulation (PCM) wave audio file in a loop. The sample demonstrates how to write a filter-centric AVStream minidriver.
</p>
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
<h2>Run the sample</h2>
<h2><a id="Installation_instructions"></a><a id="installation_instructions"></a><a id="INSTALLATION_INSTRUCTIONS"></a>Installation instructions</h2>
<p></p>
<ol>
<li>Copy AVssamp.inf to a directory, for example, C:\Avstream\. </li><li>In this directory, create a new subdirectory named objfre_x86 if the target operating system is x86-based, or objfre_amd64 for an x64-based target operating system, for example, C:\AVstream\objfre_x86\.
</li><li>Copy the processor-appropriate Avssamp.sys file to the objfre_* directory. </li><li>Start a command prompt with administrator privilege and run the processor-specific WDK tool Devcon.exe to launch the installation. For example:
<p><code>C:\WinDDK\7600.16384.0\tools\devcon\i386\devcon.exe install C:\AVstream\avssamp.inf SW\{20698827-7099-4c4e-861A-4879D639A35F}</code></p>
</li></ol>
<p></p>
<h2><a id="Programming_Tour"></a><a id="programming_tour"></a><a id="PROGRAMMING_TOUR"></a>Programming Tour</h2>
<p><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff558717"><b>DriverEntry</b></a> in Avssamp.cpp is the initial point of entry into the driver. This routine passes control to AVStream by calling
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562683"><b>KsInitializeDriver</b></a>. In this call, the minidriver passes the device descriptor, an AVStream structure that recursively defines the AVStream object hierarchy for a driver.
 This is common behavior for an AVStream minidriver.</p>
<p>Filter.cpp is where the sample lays out the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff563534">
<b>KSPIN_DESCRIPTOR_EX</b></a> structure for the single capture pin. Audio.cpp contains the
<b>KSPIN_DESCRIPTOR_EX</b> structure for the audio capture pin. This pin is dynamically created only if C:\avssamp.wav exists and is a valid and readable PCM format wave file.</p>
<p>The filter dispatch structure <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff562554">
<b>KSFILTER_DISPATCH</b></a> in Filter.cpp provides dispatches to create and process data. The
<b>DispatchProcess</b> method is defined inline in Filter.h. It calls the <b>Process</b> method in Filter.cpp in the context of the
<b>CCaptureFilter</b> class. Be aware that the process dispatch is provided in <b>
KSFILTER_DISPATCH</b> because this sample is filter-centric.</p>
<p>Audio.cpp lays out a <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff563535">
<b>KSPIN_DISPATCH</b></a> pin dispatch structure, which contains the dispatch table for the audio pin. Be aware that the
<b>Process</b> member of this structure is <b>NULL</b> because the sample is filter-centric. Similarly, Video.cpp contains the
<b>KSPIN_DISPATCH</b> structure for the video capture pin, again with the <b>Process</b> member set to
<b>NULL</b>. </p>
<p>For more information, see the comments in all .cpp files.</p>
<h2><a id="Code_tour"></a><a id="code_tour"></a><a id="CODE_TOUR"></a>Code tour</h2>
<p><b>File manifest</b> </p>
<table>
<tbody>
<tr>
<th>File</th>
<th>Description</th>
</tr>
<tr>
<td>Audio.cpp</td>
<td>
<p>Audio capture pin implementation</p>
</td>
</tr>
<tr>
<td>Audio.h</td>
<td>
<p>Header file for Audio.cpp</p>
</td>
</tr>
<tr>
<td>Avssamp.cpp </td>
<td>
<p>Main file for the filter-centric sample</p>
</td>
</tr>
<tr>
<td>Avssamp.h </td>
<td>
<p>Main header file for the sample</p>
</td>
</tr>
<tr>
<td>Avssamp.inf </td>
<td>
<p>Sample installation file</p>
</td>
</tr>
<tr>
<td>Avssamp.rc </td>
<td>
<p>Resource file, mainly to provide version information</p>
</td>
</tr>
<tr>
<td>Capture.cpp</td>
<td>
<p>Capture pin implementation for all capture pins on the sample filter</p>
</td>
</tr>
<tr>
<td>Capture.h </td>
<td>
<p>Header file for Capture.cpp</p>
</td>
</tr>
<tr>
<td>Filter.cpp </td>
<td>
<p>Capture filter implementation (including frame synthesis) for the fake capture filter</p>
</td>
</tr>
<tr>
<td>Filter.h </td>
<td>
<p>Header file for Filter.cpp</p>
</td>
</tr>
<tr>
<td>Image.cpp</td>
<td>
<p>Image synthesis and overlay code</p>
</td>
</tr>
<tr>
<td>Image.h </td>
<td>
<p>Header file for Image.cpp</p>
</td>
</tr>
<tr>
<td>Sources </td>
<td>
<p>Generic file for building the code sample</p>
</td>
</tr>
<tr>
<td>Video.cpp </td>
<td>
<p>Video capture pin implementation</p>
</td>
</tr>
<tr>
<td>Video.h </td>
<td>
<p>Header file for Video.cpp</p>
</td>
</tr>
<tr>
<td>Wave.cpp </td>
<td>
<p>Wave object implementation</p>
</td>
</tr>
<tr>
<td>Wave.h </td>
<td>
<p>Header file for Wave.cpp</p>
</td>
</tr>
</tbody>
</table>
</div>
