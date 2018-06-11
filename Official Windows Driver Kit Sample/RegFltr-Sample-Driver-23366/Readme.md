# RegFltr Sample Driver
## Requires
* Visual Studio 2012
## License
* MS-LPL
## Technologies
* WDM
* Windows Driver
## Topics
* general
## IsPublished
* False
## ModifiedDate
* 2013-06-25 10:19:42
## Description

<div id="mainSection">
<p>The RegFltr sample shows how to write a <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545879">
registry filter driver</a>. In addition to providing some basic examples, this sample demonstrates the following:</p>
<ul>
<li>How to handle transactional registry operations. </li><li>How and when to capture input parameters. </li><li>Issues and workarounds for version 1.0 of registry filtering. </li><li>Changes in version 1.1 of registry filtering. </li><li>How to use version 1 of the <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560920">
<b>REG_CREATE_KEY_INFORMATION</b></a> and <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff560957">
<b>REG_OPEN_KEY_INFORMATION</b></a> data structures. </li></ul>
<p>The RegFltr sample demonstrates the registry filtering system on Windows Vista, Windows Server 2008 and later versions of the Windows operating system. It does not work for Windows XP or Windows Server 2003.</p>
<p>The RegFltr sample contains several examples of user-mode and kernel-mode registry-filtering operations. Each example comes with its own corresponding registry callback routine, and performs the following steps:</p>
<ol>
<li>Does some setup work. </li><li>Registers the callback routine. </li><li>Performs one or more registry operations. </li><li>Unregisters the callback routine. </li><li>Verifies that the sample completed correctly. </li></ol>
<p>The sample driver is a minimal driver that is not intended to be used on production systems. To keep the samples simple, the registry callback routines provided do not check for all possible situations and error conditions. This sample is designed to demonstrate
 typical scenarios and no other registry filtering driver is expected to be active.</p>
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
</div>
