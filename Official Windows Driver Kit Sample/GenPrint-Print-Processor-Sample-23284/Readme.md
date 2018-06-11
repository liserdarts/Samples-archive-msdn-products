# GenPrint Print Processor Sample
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
* 2013-06-25 10:10:37
## Description

<div id="mainSection">
<p>The GenPrint Print Processor sample demonstrates how to implement a print processor.
</p>
<p>print processor is a user-mode DLL that is responsible for converting a print job's spooled data into a format that can be sent to a print monitor. For more information about print processors, see the Microsoft Windows Driver Kit (WDK) documentation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; If you use this sample in a mixed Microsoft Windows 2000, Windows XP, and Windows Server 2003 clustering environment, you must select RAW only as data type (no EMF). The GenPrint sample is for the v3 print driver model only.
 Starting with Windows&nbsp;8, use of print processors is not recommended. Incompatibilities exist in some Windows Store application printing scenarios.</p>
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
<p>To install the sample print processor, an installation application must call the spooler's AddPrintProcessor function. You do not need an INF file to install the sample print processor.</p>
</div>
