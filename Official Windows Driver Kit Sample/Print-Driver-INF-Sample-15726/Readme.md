# Print Driver INF Sample
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
* 2014-04-02 12:48:31
## Description

<div id="mainSection">
<p>This sample shows how to build an information file (INF) for a v3 print driver. The guidance provided in this sample can be used to build INFs for all the other print samples that require INFs. We recommend that all v3 drivers implement INFs based on the
 Package-aware sample that is part of the collection of print samples. </p>
<p>Package-aware print drivers have entries in their INF files that support point and print with packages. These entries make it possible for point and print to accommodate print driver dependencies on other files. The PackageAware keyword is used in the INF
 file.</p>
<p>Example INF files are provided for the Unidrv and Pscript minidrivers. Replace the place-holder file names with the names of your driver files. INF files for the Package-aware format are included as well.</p>
<p>For more information see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560902">
Printer INF Files</a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff559698">
Package-Aware Print Drivers</a>.</p>
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
<p>See the PackageAware sample for how to implement the INF file that you built for your v3 print driver.</p>
<h2>Run the sample</h2>
<p>This is a sample INF file, it is not an executable file that can be run.</p>
</div>
