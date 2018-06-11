# SharePoint 2013: Import a term set from an external source
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
## IsPublished
* True
## ModifiedDate
* 2013-02-06 03:45:34
## Description

<h1 id="header">Description of the sample</h1>
<div id="mainSection">
<div id="mainBody">
<div class="section" id="sectionSection0">
<p>This sample is part of a series that demonstrates how to use the Managed Metadata Service to create taxonomy hierarchies and apply tags to documents. This sample demonstrates how to import a term set from an external data source into SharePoint 2013. Specifically,
 this code parses a custom XML input file that contains a taxonomy hierarchy, and then creates the corresponding objects in the SharePoint 2013 term store.</p>
<h3 class="subHeading">Prerequisites</h3>
<div class="subsection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012 or Visual Studio 2010</p>
</li><li>
<p>SharePoint development tools in Visual Studio 2012, if Visual Studio 2012 is your development environment</p>
</li><li>
<p>A SharePoint 2013 development environment installed on the same computer where Visual Studio 2012 or Visual Studio 2010 is installed</p>
</li><li>
<p>A publishing site</p>
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
</li></ul>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>This sample was developed for learning purposes and does not include standard error-handling code.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h3 class="subHeading">Run and test the sample</h3>
<div class="subsection">
<p>This sample was designed to be run locally on the computer running SharePoint 2013</p>
<ul>
<li>
<p>Press <span class="ui">F5</span> to compile and run the sample.</p>
</li></ul>
<p>The sample parses the taxonomy hierarchy in the ExternalTaxonomyData.xml file and creates the corresponding objects in the SharePoint 2013 term store.</p>
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
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Synchronize-4c191e68" target="_blank">SharePoint 2013: Synchronize term sets with the term store using the server object model</a></span></div>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Synchronize-d40638d1" target="_blank">SharePoint 2013: Synchronize term sets with the term store using the client object model</a></span></div>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Automate-c0f4a10e" target="_blank">SharePoint 2013: Automate tagging fields with terms by using the server object model</a></span></div>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Automate-579cfc54" target="_blank">SharePoint 2013: Automate tagging fields with terms by using the client object model</a></span></div>
<div class="seeAlsoStyle"><a href="http://msdn.microsoft.com/library/113a5d75-ac4d-498b-8436-725e04fb685d(Office.15).aspx" target="_blank">Brief Introduction to Enterprise Metadata Management in SharePoint Server 2010
</a></div>
<div class="seeAlsoStyle"><a href="http://msdn.microsoft.com/library/08e4e4e1-d960-43fa-85df-f3c279ed6927.aspx" target="_blank">Start: Set up the development environment for SharePoint 2013</a></div>
</div>
</div>
</div>
