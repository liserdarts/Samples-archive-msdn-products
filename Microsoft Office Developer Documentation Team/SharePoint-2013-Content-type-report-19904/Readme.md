# SharePoint 2013: Content type report
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
## Topics
* Extensibility
* metadata
* enterprise content management (ECM)
* taxonomy
* content management
* knowledge management
* web content management
## IsPublished
* True
## ModifiedDate
* 2013-02-06 03:47:30
## Description

<h1 id="header">Description of the sample</h1>
<div id="mainSection">
<div id="mainBody">
<div class="section" id="sectionSection0">
<p>This sample is part of a series of custom reports and tools that can help you manage enterprise content types at scale. It demonstrates how to create a content type report that shows how content types are used on a specific SharePoint site. Use the report
 to learn which content types are on which lists, how content types relate to parent content types, and to get a count of how many list items use a specific content item. The count can be useful when determining whether a specific content type has to be updated.</p>
<p>&nbsp;</p>
<h3 class="subHeading">Prerequisites</h3>
<div class="subsection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012 or</p>
</li><li>
<p>A SharePoint 2013 development environment</p>
</li><li>
<p>A publishing site</p>
</li></ul>
</div>
<h3 class="subHeading">Key components of the sample</h3>
<div class="subsection">
<p>This sample contains the following files:</p>
<ul>
<li>
<p>Program.cs, which contains C# code that specifies the URL of the <span><span class="keyword">SPSite</span></span> object for which to generate the report, builds the report structure, and writes data from the specified
<span><span class="keyword">SPSite</span></span> object to the <span><span class="keyword">ContentTypeReport.csv</span></span> file</p>
</li><li>
<p>Report.cs, which contains C# code that defines the report structure, and populates it with data</p>
</li><li>
<p>ReportField.cs, which contains C# code that gets the field label, identity number, and field identity in normalized XML for every
<span><span class="keyword">SPField</span></span> object found on the target site</p>
</li><li>
<p>ReportRow.cs, which contains C# code that returns content types, lists, webs, URLs, and fields for every row</p>
</li></ul>
</div>
<h3 class="subHeading">Configure the sample</h3>
<div class="subsection">
<p>Follow these steps to configure the sample:</p>
<ul>
<li>
<p>Open the Program.cs file and, if applicable, change the URL of the SharePoint <span>
<span class="keyword">SPSite</span></span> object from <span class="code">http://intranet.contoso.com/</span> to your site's URL.</p>
</li></ul>
</div>
<h3 class="subHeading">Run and test the sample</h3>
<div class="subsection">
<p>This sample was designed to be run locally on the computer running SharePoint 2013.</p>
<ul>
<li>
<p>Press <span class="ui">F5</span> to compile and run the sample.</p>
</li></ul>
<p>The sample creates a <span><span class="keyword">ContentTypeReport.csv</span></span> file and populates it with data about site columns, for the site with the URL specified in the program.cs file.</p>
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
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Column-5bfc9643" target="_blank">SharePoint 2013: Column usage report</a></span></div>
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-XML-object-20d85b6f" target="_blank">SharePoint 2013: XML object snapshot</a></span></div>
</div>
</div>
</div>
