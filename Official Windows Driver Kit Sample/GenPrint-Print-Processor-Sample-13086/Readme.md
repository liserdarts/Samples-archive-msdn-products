# GenPrint Print Processor Sample
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
* 2014-04-02 12:46:16
## Description

<div id="mainSection">
<p>The GenPrint Print Processor sample demonstrates how to implement a print processor.
</p>
<p>print processor is a user-mode DLL that is responsible for converting a print job's spooled data into a format that can be sent to a print monitor. For more information about print processors, see the Microsoft Windows Driver Kit (WDK) documentation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp; If you use this sample in a mixed Windows clustering environment, you must select RAW only as data type (no EMF). The GenPrint sample is for the v3 print driver model only. Starting with Windows&nbsp;8, use of print processors is
 not recommended. Incompatibilities exist in some Windows Store application printing scenarios.</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff551771(v=vs.85).aspx">
Introduction to Print Processors</a>.</p>
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
<p>To install the sample print processor, an installation application must call the spooler's
<b>AddPrintProcessor</b> function. You do not need an INF file to install the sample print processor.</p>
</div>
