# Conditional Methods Sample
## Requires
* Visual Studio 2010
## License
* MS-LPL
## Technologies
* .NET Framework
## Topics
* Conditional Methods
* Language Samples
## IsPublished
* True
## ModifiedDate
* 2011-11-28 07:09:24
## Description

<h1>
<h1>Conditional Methods Sample</h1>
<div id="mainSection">
<div id="mainBody">
<div id="allHistory" class="saveHistory"></div>
<p></p>
<p>This sample demonstrates conditional methods, which provide a powerful mechanism by which calls to methods can be included or omitted depending on whether a symbol is defined.</p>
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
<h1 class="heading">To build and run the Conditional Methods sample within Visual Studio</h1>
<div id="procedureSection1" class="section">
<ol>
<li>
<p>In <b>Solution Explorer</b>, right-click the project and click <b>Properties</b>.</p>
</li><li>
<p>Open the Configuration Properties folder, and click <b>Debug</b>.</p>
</li><li>
<p>Set the Command Line Arguments property to &quot;A B C&quot; (without the quotation marks).</p>
</li><li>
<p>In the Configuration Properties folder, click <b>Build</b>.</p>
</li><li>
<p>Modify the Conditional Compilation Constants property (for example, add or delete DEBUG) and click
<b>OK</b>.</p>
</li><li>
<p>From the <b>Debug</b> menu, click <b>Start Without Debugging</b>.</p>
</li></ol>
</div>
<h1 class="heading">To build and run the Conditional Methods sample from the Command Line</h1>
<div id="procedureSection2" class="section">
<ul>
<li>
<p>To include the conditional method, compile and run the sample program by typing the following at the command prompt:
</p>
<div class="code"><span>
<table width="100%" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td colspan="2">
<pre>csc CondMethod.cs tracetest.cs /d:DEBUG
tracetest A B C</pre>
</td>
</tr>
</tbody>
</table>
</span></div>
</li></ul>
</div>
</div>
<div id="footer">
<div class="footerLine"></div>
To make a suggestion or report a bug about Help or another feature of this product, go to the
<a href="http://go.microsoft.com/fwlink/?LinkId=9790442">feedback site</a>. </div>
</div>
</h1>
