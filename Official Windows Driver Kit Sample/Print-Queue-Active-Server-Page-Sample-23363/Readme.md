# Print Queue Active Server Page Sample
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
* 2013-06-25 10:19:33
## Description

<div id="mainSection">
<p>This sample creates a custom device status active server page (ASP) for a Web printer. In this case, a modified document queue view is implemented, with e-mail and phone links to a fictitous helpdesk. This works on x86, AMD64, and Itanium-based platforms.
</p>
<p><b>Setup procedure</b> </p>
<p>To set up the Custom Queue View, open the <b>Printers</b> folder and select <b>
Add Printer</b>. When prompted for the port, either select an existing Standard TCP/IP Port or create a new Standard TCP/IP Port. When asked for the model, select Have Disk. Point the browser to the directory that contains the files for this sample, and then
 follow the prompts to continue the installation.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;IIS must be installed on the local machine in order for the sample to work. To install IIS, open the Control Panel, select
<b>Add or Remove Programs</b>, select <b>Add/Remove Windows Components</b>, check
<b>Web Applications Server</b> and click <b>Details</b>. In the dialog box that pops up, check
<b>Internet Information Services (IIS)</b>.</p>
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
<p>You do not need to build this sample. The ASP is already provided for you.</p>
<h3>Run the sample</h3>
<p>After installing the printer, connect to http://machine_name/printers and click the printer that you installed. Then select
<b>Device Status</b> to view the custom page. At this point, the source files will be interpreted the same way as any other ASP.</p>
</div>
