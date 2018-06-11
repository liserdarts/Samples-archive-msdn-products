# Print Driver Constraints Sample
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
* 2014-04-02 12:51:53
## Description

<div id="mainSection">
<p>This sample demonstrates how to implement advanced constraint handling, and also PrintTicket/PrintCapabilities handling using JavaScript.
</p>
<p>The Constraints.js file in this sample demonstrates the implementation of JavaScript-based constraints to be used with a v4 print driver. The file implements the following two of the four functions used by JavaScript constraint files, as well as several
 helper functions:</p>
<ul>
<li><b>ValidatePrintTicket</b> takes a given <a href="http://msdn.microsoft.com/en-us/library/hh451398(v=vs.85).aspx">
IPrintSchemaTicket</a> object and validates it for the current printer. The function may determine that the Print Ticket was already valid, modify the Print Ticket to make it valid, or determine that the Print Ticket is invalid and could not be made valid.
</li><li><b>CompletePrintCapabilities</b> takes a given <b>IPrintSchemaTicket</b> object and the
<a href="http://msdn.microsoft.com/en-us/library/hh451256(v=vs.85).aspx">IPrintSchemaCapabilities</a> object that was produced by the configuration module and augments it as needed. This can be used to establish positive constraint situations.
</li></ul>
<p></p>
<p>This sample does not demonstrate <b>ConvertPrintTicketToDevMode</b> or <b>ConvertDevModeToPrintTicket</b>, which utilize a property bag to store data in the private section of the DEVMODE structure.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample is for the v4 print driver model.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2><a id="related_topics"></a>Related topics</h2>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/hh451256(v=vs.85).aspx">IPrintSchemaCapabilities</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/hh451398(v=vs.85).aspx">IPrintSchemaTicket</a>
</dt></dl>
<h2>Operating system requirements</h2>
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
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<p>You can run the sample by loading the files into or Visual Studio&nbsp;2013, and then pressing
<b>F5</b>, or by selecting <b>Debug</b> &gt; <b>Start Debugging</b>.</p>
</div>
