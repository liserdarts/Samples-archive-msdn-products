# Using Word to publish content for SharePoint and web applications
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* Microsoft Office Word
## Topics
* customization
* Open XML
## IsPublished
* True
## ModifiedDate
* 2013-04-11 05:11:15
## Description

<div id="header"><span class="label">Summary:</span>&nbsp;&nbsp;Demonstrates how to use Microsoft Word to publish formatted contents for SharePoint and web applications by customizing PowerTools for Open XML.</div>
<div id="mainSection">
<div id="mainBody">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p>This sample implements a simple solution that uses Word to publish formatted contents, and accompanies an article by the same name on MSDN,
<a href="http://msdn.microsoft.com/en-us/library/dn151787(v=office.14).aspx" target="_blank">
Using Word to publish content for SharePoint and web applications</a>. The solution provides an effective application design alternative when Word is the preferred publication tool for an application, and makes it easy for content authors to prepare contents
 offline. By customizing the <span><span class="keyword">HtmlConverter</span></span> class in PowerTools for Open XML, the solution lets you convert a specific section of a Word document to HTML. The sample includes a custom helper class that you can use
 to provide better HTML conversion control and enable users to download the published contents back to another Word document.</p>
<p>This sample code contains a solution with two projects, a class library and a web project. It also contains the following folders:</p>
<ul>
<li>
<p><strong>External</strong> folder, which contains the Open XML libraries</p>
</li><li>
<p><strong>OpenXMLHtmlConverter</strong> folder, which contains the sample code</p>
</li><li>
<p><strong>OpenXmlPowerTools_Customized</strong> folder, which contains the customized Open XML power tools</p>
</li><li>
<p><strong>Packages</strong> folder, which contains all the libraries for the solution</p>
</li><li>
<p><strong>Template</strong> folder, which contains two Word documents: UploadTemplate.docx and DownloadTemplate.docx</p>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2010 with SP1 for Visual Studio</p>
</li><li>
<p>ASP.NET MVC 4</p>
</li><li>
<p>Microsoft Word 2010 or Microsoft Word 2013</p>
</li></ul>
</div>
<h1 class="heading">Key components</h1>
<div class="section" id="sectionSection2">
<p>This sample has one solution that has the following components:</p>
<ul>
<li>
<p>OpenXML Power Tools Class Library Project</p>
</li><li>
<p>OpenXMLHtmlConverter ASP.NET MVC 4 Web Project</p>
</li></ul>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample:</p>
<ol>
<li>
<p>Extract the SampleCode.zip file to your hard drive.</p>
</li><li>
<p>Open Visual Studio 2010.</p>
</li><li>
<p>Click <span class="ui">File</span>, point to <span class="ui">Open</span>, and then click
<span class="ui">Project/Solution...</span> to open the OpenXMLHtmlConverter.sln file.</p>
</li></ol>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>Do the following to build the sample.</p>
<ul>
<li>
<p>Right click the solution, and then click <span class="ui">Build Solution</span> or Press F6.</p>
</li></ul>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>Follow these steps to run and test the sample.</p>
<ol>
<li>
<p>Press F5 to run the OpenXMLHtmlConverter web project.</p>
</li><li>
<p>After the browser opens, you can click the <span class="ui">Upload</span> or
<span class="ui">Download</span> tabs.</p>
<ul>
<li>
<p>In the <span class="ui">Upload</span> tab, click the <span class="ui">Browse</span> button, and then choose UploadTemplate.docx in the
<span class="ui">...\OpenXMLHtmlConverter\Template</span> folder.</p>
</li><li>
<p>Next, click the <span class="ui">Upload UploadTemplate.docx and convert to HTML</span> button. A new browser tab opens.</p>
</li></ul>
</li><li>
<p>On the home page, click the <span class="ui">Download</span> tab.</p>
</li><li>
<p>Click the <span class="ui">Download and Convert to .docx</span> button. This prompts you to open, save, or save as the DownloadConvertedDoc.docx word document.</p>
</li><li>
<p>Click <span class="ui">Open</span> to see the generated Word document.</p>
</li></ol>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<ol>
<li>
<p>Verify that you have Word 2010 or Word 2013 installed.</p>
</li><li>
<p>Verify that you have ASP.NET MVC 4 installed.</p>
</li><li>
<p>Verify that you have SP1 for Visual Studio 2010 installed.</p>
</li></ol>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<p>Accompanying article on MSDN: <a href="http://msdn.microsoft.com/en-us/library/dn151787(v=office.14).aspx" target="_blank">
Using Word to publish content for SharePoint and web applications</a></p>
</li><li>
<p><a href="http://powertools.codeplex.com/" target="_blank">PowerTools for Open XML</a></p>
</li><li>
<p><a href="http://www.asp.net/mvc/mvc4" target="_blank">ASP.NET MVC 4 Installer for Visual Studio 2010</a></p>
</li></ul>
</div>
</div>
</div>
