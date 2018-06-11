# Common Property Sheet UI Sample
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
* 2014-04-02 12:45:20
## Description

<div id="mainSection">
<p>The CPSUISAM application causes the Common Property Sheet User Interface (CPSUI) to call the Windows print spooler to create property sheet pages for the system's default printer.
</p>
<p>Printer interface DLLs must not perform this action and this sample shows how create property sheet pages for a printer.</p>
<p>The application then creates an additional property sheet page to illustrate some of the techniques that you can use when you are using CPSUI to create a new page.</p>
<p>CPSUI is a user-mode DLL that enables you to create property sheet pages that have a standard appearance.</p>
<p>CPSUIAM causes CPSUI to call the Windows print spooler to create property sheet pages for the system's default printer. The application then creates an additional property sheet page to illustrate some of the techniques that you can use when you are using
 CPSUI to create a new page. For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff546163(v=vs.85).aspx">
Common Property Sheet User Interface</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The CPSUISAM sample is for the v3 print driver model only.</p>
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
<p>When you build the sample, a DLL is placed in the appropriate platform directory. The sample produces one binary,
<i>Cpsuisam.exe</i>.</p>
</div>
