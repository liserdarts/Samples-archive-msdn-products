# Print Pipeline Simple Filter
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
* 2013-06-25 10:21:22
## Description

<div id="mainSection">
<p>The printing system supports a print filter pipeline. The pipeline is run when a print job is consumed by the print spooler and sent to the device.
</p>
<p>This sample shows how to use the print pipeline's filter interfaces.</p>
<p>The filters in the print pipeline consume a certain data type and produce a certain data type. This information is specified in the pipeline configuration file on a per printer driver basis. The WDK print filter sample contains two filter samples: one that
 consumes and produces XPS data type, and the other one consumes and produces opaque byte stream. For more information, see the
<a href="http://msdn.microsoft.com/en-us/windows/hardware/gg463364">XpsDrv</a> whitepaper.</p>
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
<p>The sample filter must be added to a pipeline configuration file. This file is a dependent file for a printer driver. For more information, see the
<i>XpsDrv</i> whitepaper.</p>
</div>
