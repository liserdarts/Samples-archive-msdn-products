# Print Driver Constraints Sample
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
* 2013-06-25 10:22:46
## Description

<div id="mainSection">
<p>This sample demonstrates how to implement advanced constraint handling, and also PrintTicket/PrintCapabilities handling using JavaScript.
</p>
<p>The Constraints.js file in this sample demonstrates the implementation of JavaScript-based constraints to be used with a v4 print driver. The file implements the following two of the four functions used by JavaScript constraint files, as well as several
 helper functions:</p>
<ul>
<li>ValidatePrintTicket() takes a given <a href="http://msdn.microsoft.com/en-us/library/hh451398(v=vs.85).aspx">
IPrintSchemaTicket</a> object and validates it for the current printer. The function may determine that the Print Ticket was already valid, modify the Print Ticket to make it valid, or determine that the Print Ticket is invalid and could not be made valid.
</li><li>CompletePrintCapabilities() takes a given <b>IPrintSchemaTicket</b> object and the
<a href="http://msdn.microsoft.com/en-us/library/hh451256(v=vs.85).aspx">IPrintSchemaCapabilities</a> object that was produced by the configuration module and augments it as needed. This can be used to establish positive constraint situations.
</li></ul>
<p></p>
<p>This sample does not demonstrate ConvertPrintTicketToDevMode() or ConvertDevModeToPrintTicket(), which utilize a property bag to store data in the private section of the DEVMODE structure.
</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;This sample is for the v4 print driver model.</p>
<h3><a id="related_topics"></a>Related topics</h3>
<dl><dt><a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/hh451256(v=vs.85).aspx">IPrintSchemaCapabilities</a>
</dt><dt><a href="http://msdn.microsoft.com/en-us/library/hh451398(v=vs.85).aspx">IPrintSchemaTicket</a>
</dt></dl>
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
<p>You can run the sample by loading the files into Visual Studio Professional&nbsp;2012 or Visual Studio Ultimate&nbsp;2012, and then pressing
<b>F5</b>, or by selecting <b>Debug</b> &gt; <b>Start Debugging</b>.</p>
</div>
