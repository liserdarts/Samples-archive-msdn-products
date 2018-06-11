# Print Provider Sample Template
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:48:28
## Description

<div id="mainSection">
<p>This sample is a template that contains skeletal implementations of the functions that print providers should define. This sample is not a functional print provider.
</p>
<p>Print providers are responsible for directing print jobs to local or remote print devices. They are also responsible for print queue management operations, such as starting, stopping, and enumerating a server's print queues. They define a high-level, machine-independent,
 operating system-independent view of a print server. For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff551775(v=vs.85).aspx">
Introduction to Print Providers</a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff549424(v=vs.85).aspx">
Functions Defined by Print Providers</a>.</p>
<p>This sample is intended as an educational example that illustrates the functionality that a basic print provider should implement.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The sample is a very basic template only. The Print Provider sample is for the v3 print driver model only. Starting with Windows&nbsp;8, use of print providers is not recommended. Incompatibilities exist in some Windows Store application
 printing scenarios.</p>
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
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>You do not need an INF file to install print providers. You should install print providers by using the
<b>AddPrintProvidor</b> API routine. </p>
<h2><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p>To test for the sample, write a short test applet that installs Pp.dll by calling the
<b>AddPrintProvidor</b> routine. A check of the <code>HKLM\SYSTEM\CurrentControlSet\Control\Print\Providers</code> registry key should show the new provider in the providers list.
</p>
</div>
