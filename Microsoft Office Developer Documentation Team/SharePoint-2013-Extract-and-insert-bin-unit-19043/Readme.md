# SharePoint 2013: Extract and insert bin-unit elements in XLIFF files
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
## Topics
* enterprise content management (ECM)
* XLIFF
## IsPublished
* True
## ModifiedDate
* 2013-01-11 04:10:46
## Description

<p><span style="font-size:small"><strong>bin-unit </strong>elements are a construct in XLIFF files that enable you to include content that isn't easy to parse and transform into a
<strong>trans-unit </strong>element. Text files, Microsoft Word documents, and Images are just some of the types of files that might be included in a
<strong>bin-unit</strong>. SharePoint Server 2013 enables users to export this binary content for translation, and embeds it in a
<strong>bin-unit </strong>element. To more easily transfer data, embedded files are encoded using Base64. This sample provides you with a simple way to extract and insert binary content from an XLIFF file.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Basic familiarity with XML files and terminology</span>
</li><li><span style="font-size:small">Basic familiarity with the XLIFF file format</span>
</li><li><span style="font-size:small">Basic familiarity with command line applications.</span>
</li><li><span style="font-size:small">A SharePoint Server 2013 publishing site with the Translation feature enabled</span>
</li><li><span style="font-size:small">An XLIFF file produced by a publishing site that includes at least one
<strong>file</strong> element that contains a <strong>bin-unit </strong>child element</span>
</li><li><span style="font-size:small">Visual Studio 2010 or a later version</span> </li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">This code sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">The BinarySample project.</span> </li><li><span style="font-size:small">Program.cs, which contains all the code you need to extract and insert binary content.</span>
</li></ul>
<h1>Build and test the sample</h1>
<p><span style="font-size:small">Follow these steps to build and test the sample.</span></p>
<ul>
<li><span style="font-size:small">Open the BinarySample project in Visual Studio.</span>
</li><li><span style="font-size:small">On the BUILD menu, click <strong>Build Solution</strong>.</span>
</li><li><span style="font-size:small">Place an XLIFF file produced by SharePoint Server 2013 in the same directory as BinarySample.exe.</span>
</li><li><span style="font-size:small">From a command prompt, navigate to the directory that contains the BinarySample.exe and the XLIFF file.
</span></li><li><span style="font-size:small">Type and run <strong>BinarySample.exe </strong>
to display sample usage.</span> </li></ul>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a title="http://docs.oasis-open.org/xliff/xliff-core/xliff-core.html" href="http://docs.oasis-open.org/xliff/xliff-core/xliff-core.html" target="_blank">XLIFF 1.2 Specification</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/jj163942(v=office.15).aspx" href="http://msdn.microsoft.com/en-us/library/jj163942(v=office.15).aspx" target="_blank">What's new with SharePoint 2013 site development</a></span>
</li></ul>
