# Print Provider Sample Template
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:24
## Description

<div id="mainSection">
<p>This sample is a template that contains skeletal implementations of the functions that print providers should define. This sample is not a functional print provider.
</p>
<p>Print providers are responsible for directing print jobs to local or remote print devices. They are also responsible for print queue management operations, such as starting, stopping, and enumerating a server's print queues. They define a high-level, machine-independent,
 operating system-independent view of a print server. For more information, see the WDK documentation.</p>
<p>This sample is intended as an educational example that illustrates the functionality that a basic print provider should implement.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The sample is a very basic template only. The Print Provider sample is for the v3 print driver model only. Starting with Windows&nbsp;8, use of print providers is not recommended. Incompatibilities exist in some Windows Store application
 printing scenarios.</p>
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
<h3>Run the sample</h3>
<h3><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h3>
<p>You do not need an INF file to install print providers. You should install print providers by using the AddPrintProvidor() API routine.
</p>
<h3><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h3>
<p>To test for the sample, write a short test applet that installs Pp.dll by calling the AddPrintProvidor() routine. A check of the
<code>HKLM\SYSTEM\CurrentControlSet\Control\Print\Providers</code> registry key should show the new provider in the providers list.
</p>
</div>
