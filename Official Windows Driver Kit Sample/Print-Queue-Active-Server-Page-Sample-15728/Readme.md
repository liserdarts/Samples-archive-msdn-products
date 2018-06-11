# Print Queue Active Server Page Sample
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
* 2014-04-02 12:48:36
## Description

<div id="mainSection">
<p>This sample creates a custom device status active server page (ASP) for a Web printer. In this case, a modified document queue view is implemented, with e-mail and phone links to a fictitious help desk.
</p>
<p><b>Setup procedure</b> </p>
<p>To set up the Custom Queue View, open the <b>Printers</b> folder and select <b>
Add Printer</b>. When prompted for the port, either select an existing Standard TCP/IP Port or create a new Standard TCP/IP Port. When asked for the model, select Have Disk. Point the browser to the directory that contains the files for this sample, and then
 follow the prompts to continue the installation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;IIS must be installed on the local machine in order for the sample to work. To install IIS, open the Control Panel, select
<b>Add or Remove Programs</b>, select <b>Add/Remove Windows Components</b>, check
<b>Web Applications Server</b> and click <b>Details</b>. In the dialog box that pops up, check
<b>Internet Information Services (IIS)</b>.</p>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545056(v=vs.85).aspx">
ASP Files for Print Web Pages</a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff547320(v=vs.85).aspx">
Customizing the Printer Details Web Page</a>.</p>
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
<p>You do not need to build this sample. The ASP is already provided for you.</p>
<h2>Run the sample</h2>
<p>After installing the printer, connect to http://machine_name/printers and click the printer that you installed. Then select
<b>Device Status</b> to view the custom page. At this point, the source files will be interpreted the same way as any other ASP.</p>
</div>
