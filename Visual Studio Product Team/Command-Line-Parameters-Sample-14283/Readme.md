# Command Line Parameters Sample
## Requires
* Visual Studio 2010
## License
* MS-LPL
## Technologies
* .NET Framework
## Topics
* Language Samples
* Command Line Parameters
## IsPublished
* True
## ModifiedDate
* 2011-11-28 07:09:15
## Description

<h1>Command Line Parameters Sample</h1>
<div id="mainSection">
<div id="mainBody">
<div id="allHistory" class="saveHistory"></div>
<p></p>
<p>This sample shows how the command line can be accessed and two ways of accessing the array of command-line parameters.</p>
<div class="alert">
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<th align="left">Security Note </th>
</tr>
<tr>
<td>
<p>This sample code is provided to illustrate a concept and should not be used in applications or Web sites, as it may not illustrate the safest coding practices. Microsoft assumes no liability for incidental or consequential damages should the sample code
 be used for purposes other than as intended.</p>
</td>
</tr>
</tbody>
</table>
</div>
<h1 class="heading">To build and run the Command Line Parameters samples within Visual Studio</h1>
<div id="procedureSection1" class="section">
<ol>
<li>
<p>Open the solution (CommandLine.sln).</p>
</li><li>
<p>In <b>Solution Explorer</b>, right-click the CmdLine1 project, and click <b>Set as StartUp Project</b>.</p>
</li><li>
<p>In <b>Solution Explorer</b>, right-click the project, and click <b>Properties</b>.</p>
</li><li>
<p>Open the Configuration Properties folder, and click <b>Debug</b>.</p>
</li><li>
<p>In the Command Line Arguments property, type the command-line parameters (see the tutorial for an example), and click
<b>OK</b>.</p>
</li><li>
<p>From the <b>Debug</b> menu, click <b>Start Without Debugging</b>.</p>
</li><li>
<p>Repeat the preceding steps for CmdLine2.</p>
</li></ol>
</div>
<h1 class="heading">To build and run the Command Line Parameters samples from the Command Line</h1>
<div id="procedureSection2" class="section">
<ol>
<li>
<p>Use the <b>Change Directory</b> command to change to the CmdLine1 directory.</p>
</li><li>
<p>Type the following: </p>
<div class="code"><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td colspan="2">
<pre>csc cmdline1.cs
cmdline1 A B C</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>Use the <b>Change Directory</b> command to change to the CmdLine2 directory.</p>
</li><li>
<p>Type the following: </p>
<div class="code"><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td colspan="2">
<pre>csc cmdline2.cs
cmdline2 John Paul Mary</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ol>
</div>
</div>
<div id="footer">
<div class="footerLine"></div>
To make a suggestion or report a bug about Help or another feature of this product, go to the
<a href="http://go.microsoft.com/fwlink/?LinkId=9790442">feedback site</a>. </div>
</div>
