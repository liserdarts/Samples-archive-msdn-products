# Common Property Sheet UI Sample
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
* 2013-06-25 10:09:33
## Description

<div id="mainSection">
<p>The CPSUISAM application causes the Common Property Sheet User Interface (CPSUI) to call the Windows print spooler to create property sheet pages for the system's default printer.
</p>
<p>Printer interface DLLs must not perform this action and this sample shows how create property sheet pages for a printer.</p>
<p>The application then creates an additional property sheet page to illustrate some of the techniques that you can use when you are using CPSUI to create a new page.</p>
<p>CPSUI is a user-mode DLL that enables you to create property sheet pages that have a standard appearance.</p>
<p>CPSUIAM causes CPSUI to call the Windows print spooler to create property sheet pages for the system's default printer. The application then creates an additional property sheet page to illustrate some of the techniques that you can use when you are using
 CPSUI to create a new page.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The CPSUISAM sample is for the v3 print driver model only.</p>
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
<p>When you build the sample, a DLL is placed in the appropriate platform directory. The sample produces one binary,
<i>Cpsuisam.exe</i>.</p>
</div>
