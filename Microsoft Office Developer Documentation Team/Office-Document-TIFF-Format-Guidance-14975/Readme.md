# Microsoft Office Document: TIFF Format Guidance
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Office 2007
* Microsoft Office 2003
* Microsoft Office XP
## Topics
* TIFF
* Microsoft Office Document Imaging (MODI)
## IsPublished
* True
## ModifiedDate
* 2012-01-30 04:00:36
## Description

<h1>Summary</h1>
<p><span style="font-size:small">This document provides guidance about custom tags that Microsoft&reg; Office Document Imaging (MODI) software may write into the TIFF files it generates.</span></p>
<h1><span>Tags in TIFF Output</span></h1>
<p><span style="font-size:small">Within the TIFF file, MODI includes the following custom tags to store the text of the page, as well as the location and formatting of that text:</span><span style="font-size:small">&nbsp;</span></p>
<ul>
<li><span style="font-size:small"><strong>Tag 37679</strong> contains the text of the file.</span>
</li><li><span style="font-size:small"><strong>Tag 37681</strong> contains information about where the text is as well as other properties associated with the document.</span>
</li></ul>
<p><span style="font-size:small">Data in <strong>Tag 37681 </strong>is dependent on data in
<strong>Tag 37679</strong>; therefore, data in <strong>Tag 37681 </strong>should follow data in
<strong>Tag 37679</strong>. Separate pairs of these tags exist for every page in the document.</span></p>
<h1><span style="font-size:20px; font-weight:bold">Structure of Tag 37681</span></h1>
<p><span style="font-size:small">The information in <strong>Tag 37681 </strong>is stored hierarchically, as follows:</span></p>
<p><img src="/site/view/file/48969/1/Tag37681.png" alt="" width="588" height="95"><br>
<br>
<span style="font-size:small">The four types of elements exist as an array of vectors. Each of these vectors represents all of the corresponding element types in the document. Each item points to the index of its parent one level up in the hierarchy, as well
 as to the least and greatest indices of elements one level lower in the hierarchy that are considered its children.</span></p>
<p><span style="font-size:small">All elements above the character level have associated bounding rectangles used to allow for text selection in MODI.</span></p>
<p><span style="font-size:small"><strong>Note</strong>: In Microsoft Word 2003, the font table is not implemented on exporting to MODI. It reports containing a single element with font size of 12, pitch, family, and character set of 0, and an empty name. All
 character elements reference this as their respective font.</span></p>
<h1><span>Sample Code</span></h1>
<p><span style="font-size:small">The following sample code demonstrates how the data from a TIFF file can be extracted into memory. It makes use of the following Windows APIs.
<strong>Note</strong>: If you are not developing on Windows, MSDN provides detailed documentation on these APIs, which will allow you to modify the sample to use appropriate substitutions.</span></p>
<ul>
<li><span style="font-size:small"><strong><a href="http://msdn.microsoft.com/en-us/library/aa366574(v=VS.85).aspx">GlobalAlloc</a></strong> - Allocates memory (the C runtime function
<strong>malloc </strong>does not return an <strong>HGLOBAL </strong>and thus isn&rsquo;t sufficient for this particular purpose. The sample code uses
<strong>malloc </strong>when it can).</span> </li><li><span style="font-size:small"><strong><a href="http://msdn.microsoft.com/en-us/library/aa366584(VS.85).aspx">GlobalLock</a>
</strong>- Gets a pointer from the handle returned by <strong>GlobalAlloc</strong>.</span>
</li><li><span style="font-size:small"><strong><a href="http://msdn.microsoft.com/en-us/library/aa366595(VS.85).aspx">GlobalUnlock</a>
</strong>- Releases the lock on the given handle after reading data from the file into the pointer returned by
<strong>GlobalLock</strong>.</span> </li><li><span style="font-size:small"><strong><a href="http://msdn.microsoft.com/en-us/library/aa378980(v=VS.85).aspx">CreateStreamOnHGlobal</a>
</strong>&ndash; Creates an <strong>IStream </strong>from raw data.</span> </li></ul>
<p><span style="font-size:small">The sample code also uses the zlib library, which is not included. You must provide a zlib.h file or a library with equivalent data compression functionality.</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
