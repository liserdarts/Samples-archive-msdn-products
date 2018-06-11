# SharePoint 2013: XML object snapshot
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
* 2013-02-06 03:48:16
## Description

<h1 id="header">Description of the sample</h1>
<div id="mainSection">
<div id="mainBody">
<div class="section" id="sectionSection0">
<p>This sample is part of a series of custom reports and tools that can help you manage enterprise content types at scale. It uses the
<span><span class="keyword">XmlSerializer</span></span> class to stream data from the specified SharePoint site and generate the
<span><span class="keyword">ContentTypeSnapshotModel.xml</span></span> file. Use this sample to capture the state of a SharePoint site's objects and properties as a baseline. Then, run the sample again after making changes to the site and compare the two
 reports for differences.</p>
<h3 class="subHeading">Prerequisites</h3>
<div class="subsection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
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
<p>Program.cs, which contains C# code that creates the <span><span class="keyword">ContentTypeSnapshotModel.xml</span></span> file</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>ContentTypeSnapshot.xml is a report that contains all of the SharePoint objects that were crawled via reflection.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>XmlReporter.cs, which defines report types, configures the <span><span class="keyword">XmlReporter</span></span> class and the
<span><span class="keyword">XmlSerializer</span></span> class, and generates the report and its properties</p>
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
<p>The sample creates a ContentSnapshotModel.xml file and populates it with data about a SharePoint site's objects and properties.</p>
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
<div class="seeAlsoStyle"><span><a href="http://code.msdn.microsoft.com/SharePoint-2013-Content-1fe98b75" target="_blank">SharePoint 2013: Content type report</a></span></div>
</div>
</div>
</div>
