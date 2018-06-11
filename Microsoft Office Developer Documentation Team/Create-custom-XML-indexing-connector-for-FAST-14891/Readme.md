# Create custom XML indexing connector for FAST Search for SharePoint
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint
* SharePoint Server 2010
## Topics
* Indexers
## IsPublished
* True
## ModifiedDate
* 2012-01-24 09:51:04
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">The MyFileConnector Custom Indexing Connector sample is a basic indexing connector that crawls all files and folders within a file share on a Windows file system.</span></p>
<h1><span>Prerequisites:</span></h1>
<ul>
<li><span style="font-size:small">Microsoft SharePoint Server 2010</span> </li><li><span style="font-size:small">Microsoft Visual Studio</span> </li></ul>
<p><span style="font-size:20px; font-weight:bold">Files:</span></p>
<ul>
<li><span style="font-size:small">MyFileConnector.sln - MyFileConnector solution file.</span>
</li><li><span style="font-size:small">MyFileConnector.csproj - MyFileConnector project file.</span>
</li><li><span style="font-size:small">Test.snk - MyFileConnector key file.</span> </li><li><span style="font-size:small">MyFile.cs - Defines the file and folder external content types.&nbsp; Provides the method implementations for the Finder and SpecificFinder operations.</span>
</li><li><span style="font-size:small">MyFileConnector.cs - Derives from the StructuredRepositorySystemUtility(Of T) class, which implements the ISystemUtility interface.</span>
</li><li><span style="font-size:small">MyFileLobUri.cs - Derives from the LobUri class, which maps the URLs as they are passed from the Search service application to Microsoft Business Connectivity Services (BCS).</span>
</li><li><span style="font-size:small">MyFileNamingContainer.cs - Implements the INamingContainer interface, and maps the URLs as they are passed from the Business Connectivity Services to the Search service application.</span>
</li><li><span style="font-size:small">MyFileModel.xml - MyFileConnector BCS model file.</span>
</li></ul>
<h1><span>Setup:</span></h1>
<p><span style="font-size:small">See Code Sample: MyFileConnector Custom Indexing Connector (<a href="http://msdn.microsoft.com/en-us/library/ff625800.aspx">http://msdn.microsoft.com/en-us/library/ff625800.aspx</a>) for step-by-step instructions on setting
 up the sample.</span></p>
