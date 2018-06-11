# SharePoint 2013: Automate tagging fields with terms (CSOM)
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
## Topics
* metadata
* enterprise content management (ECM)
* taxonomy
* content management
* knowledge management
## IsPublished
* True
## ModifiedDate
* 2013-02-06 03:47:00
## Description

<h1 id="header">Description of the sample</h1>
<div id="mainSection">
<div id="mainBody">
<div class="section" id="sectionSection0">
<p>This sample is part of a series that demonstrates how to use the managed metadata service to create taxonomy hierarchies and apply tags to documents. This code extends the functionality found in the
<span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Synchronize-4c191e68" target="_blank">SharePoint 2013: Synchronize term sets with the term store using the server object model</a></span> sample by introducing an export method that exports a CSV
 file that you can edit and an import method that imports data from that file.</p>
<h3 class="subHeading">Prerequisites</h3>
<div class="subsection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012 or</p>
</li><li>
<p>SharePoint development tools in Visual Studio 2012</p>
</li><li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>A publishing site</p>
</li><li>
<p>At least one single-valued managed metadata column that is bound to a term set that contains terms.</p>
</li></ul>
</div>
<h3 class="subHeading">Key components of the sample</h3>
<div class="subsection">
<p>This sample contains the following files:</p>
<ul>
<li>
<p>Program.cs, which contains all of the C# code for the sample</p>
</li><li>
<p>ExternalTaxonomyData.xml, which contains test data that represents taxonomy data from an external source</p>
</li></ul>
</div>
<h3 class="subHeading">Configure the sample</h3>
<div class="subsection">
<p>Follow these steps to configure the sample:</p>
<ul>
<li>
<p>Open the Program.cs file and, if applicable, change the URL of the SharePoint <span>
<span class="keyword">SPSite</span></span> object from http://localhost/ to your site URL.</p>
</li><li>
<p>If you want this sample to create a sample input file, uncomment the code block that generates an example XML file. This is useful if the XML schema has changed.</p>
</li><li>
<p>Configure a SharePoint 2013 list that contains the documents to be tagged. Create at least one single-valued managed metadata column that is bound to a term set. That term set must contain at least one term.</p>
</li><li>
<p>To generate the <span><span class="keyword">AutomatedTagging.csv</span></span> file, change the
<span class="code">#if</span> directive in the <span class="code">Main</span> method to enable the
<span class="code">ExportTemplate</span> mode.</p>
</li><li>
<p>Open <span><span class="keyword">AutomatedTagging.csv</span></span> in Excel and enter the name of the taxonomy term next to the URL for each document that you want to tag.</p>
</li></ul>
</div>
<h3 class="subHeading">Run and test the sample</h3>
<div class="subsection">
<p>This sample was designed to be run locally on the computer running SharePoint 2013.</p>
<ul>
<li>
<p>Press <span class="ui">F5</span> to compile and run the sample.</p>
</li></ul>
<p>When configured to export, the sample generates the <span><span class="keyword">AutomatedTagging.csv</span></span> file. When configured to import, the sample applies the tags from the
<span><span class="keyword">AutomatedTagging.csv</span></span> file to the corresponding documents.</p>
</div>
<h3 class="subHeading">Change log</h3>
<div class="subsection">
<div class="caption"></div>
<div class="tableSection">
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First release</p>
</td>
<td>
<p>December 6, 2012</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="seeAlsoSection">
<div class="seeAlsoStyle"><span>
<div class="seeAlsoStyle"><span>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Automate-c0f4a10e" target="_blank">SharePoint 2013: Automate tagging fields with terms by using the server object model</a></span></div>
</span></div>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Synchronize-d40638d1" target="_blank">SharePoint 2013: Synchronize term sets with the term store using the client object model</a></span></div>
</span></div>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Synchronize-4c191e68" target="_blank">SharePoint 2013: Synchronize term sets with the term store using the server object model</a></span></div>
<div class="seeAlsoStyle">
<div class="seeAlsoStyle"><span><a href="714af33e-1bd1-479d-9c28-3fd37dbfe3c4.htm"></a></span></div>
<a href="http://msdn.microsoft.com/library/08e4e4e1-d960-43fa-85df-f3c279ed6927.aspx" target="_blank">Start: Set up the development environment for SharePoint 2013</a></div>
</div>
</div>
</div>
