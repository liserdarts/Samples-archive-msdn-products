# XML Documentation Sample
## Requires
* Visual Studio 2010
## License
* MS-LPL
## Technologies
* .NET Framework
## Topics
* XML Documentation
* Language Samples
## IsPublished
* True
## ModifiedDate
* 2011-11-28 07:13:37
## Description

<h1>
<h1>XML Documentation Sample</h1>
<div id="mainSection">
<div id="mainBody">
<div id="allHistory" class="saveHistory"></div>
<p></p>
<p>This sample shows how to use XML to document code. See for additional information.</p>
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
<h1 class="heading">To build the XML Documentation sample within Visual Studio</h1>
<div id="procedureSection1" class="section">
<ol>
<li>
<p>In <b>Solution Explorer</b>, right-click the project and click <b>Properties</b>.</p>
</li><li>
<p>Open the Configuration Properties folder and click <b>Build</b>.</p>
</li><li>
<p>Set the XML Documentation File property to XMLsample.xml.</p>
</li><li>
<p>On the <b>Build</b> menu, click <b>Build</b>. The XML output file will be in the debug directory.</p>
</li></ol>
</div>
<h1 class="heading">To build the XML Documentation sample from the Command Line</h1>
<div id="procedureSection2" class="section">
<ol>
<li>
<p>To generate the sample XML documentation, type the following at the command prompt:
</p>
<div class="code"><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td colspan="2">
<pre>csc XMLsample.cs /doc:XMLsample.xml</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li><li>
<p>To see the generated XML, issue the following command: </p>
<div class="code"><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td colspan="2">
<pre>type XMLsample.xml</pre>
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
</h1>
