# SharePoint 2010: Building IFilters for SharePoint 2010 Search and Windows Search
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2007
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* Search
* Enterprise Search
## IsPublished
* True
## ModifiedDate
* 2011-12-21 02:06:30
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample accompanies the article <a title="How to Build an IFilter for SharePoint 2010 Search and Windows Search by Using C&#43;&#43;, ATL, and MFC" href="http://msdn.microsoft.com/en-us/library/hh694268.aspx">
Building IFilters for SharePoint 2010 Search and Windows Search by using C&#43;&#43;, ATL, and MFC</a> in the MSDN Library.</span></p>
<p><span style="font-size:small">An IFilter is an interface that enables Windows Desktop Search and Microsoft&reg; SharePoint&reg; search to index the contents of files. Although the default full-text search for documents works well in many situations, it isn't
 always appropriate. For example, indexing a file that is in a binary format or indexing a text file in which you must locate specific information in the document. Windows has built-in IFilters for Microsoft&reg; Office products and filter packs that are available
 for download, and Adobe has an IFilter for PDF files. Although an IFilter is technically an interface, implementations of that interface are also called IFilters, which can be confusing. For clarity, this article always uses the term IFilter interface to refer
 to the interface.</span></p>
<p><span style="font-size:small">As of Windows 7, you can no longer use managed code to implement an IFilter because for any given process, only one version of the .NET Framework runtime can be loaded at a time. This means that if one IFilter developer uses
 the 2.0 version of the .NET Framework and another developer uses the 4.0 version, the two IFilters are incompatible. In Windows 7 and later versions, filters that are written in managed code are explicitly blocked. Filters must be written in native code because
 there are potential Common Language Runtime (CLR) versioning issues with the process that multiple add-ins run in. Although it might be possible to write an IFilter in Microsoft Visual Basic 6.0, it is likely a very bad option considering the throughput demands
 that are required to index thousands, or possibly millions, of files (for example, in SharePoint). Therefore, the best option to develop an IFilter is to implement it by using C&#43;&#43;.</span></p>
<p><span style="font-size:small">To write an IFilter, you must implement several COM interfaces (IFilter, IPersistFile, IPersistStream, and IUnknown). Although you could write COM objects without relying on the Active Template Library (ATL), ATL makes development
 much easier because it provides the COM infrastructure (object creation, object destruction, mapping the interface to the concrete implementation, and so on).</span></p>
<p><span style="font-size:small">Following that reasoning, this sample uses ATL in its implementation of an IFilter. It also uses the Microsoft Foundation Class library (MFC) to implement string manipulation, which is a common feature of IFilters.</span></p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<p><span style="font-size:small">The sample consists of a Visual Studio 2010 solution, MyIFilter.sln, which contains a number of source files. After downloading the compressed file, save it to your computer, extract all the compressed files, and then double-click
 the solution file to open it in Visual Studio.</span></p>
