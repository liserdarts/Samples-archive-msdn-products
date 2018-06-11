# Excel 2010: Using Custom Data Parts
## Requires
* Visual Studio 2008
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* custom data
## IsPublished
* False
## ModifiedDate
* 2011-08-12 01:18:01
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to build an OLE DB data provider that supports Custom Data Parts in Microsoft Excel 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff477608.aspx">Using Custom Data Parts in Excel 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>A custom data part is a section of the workbook file where providers can store custom data. Custom data parts resemble custom XML parts except the data can be stored in any format.</p>
<p>A custom data part is a mechanism that allows an OLE DB provider that implements the
<strong>IMDEmbeddedData</strong> interface to indicate to Excel that it supports embedded storage in the form of a custom data part. During the instantiation of the data connection, if the provider indicates that it supports
<strong>IMDEmbeddedData</strong>, Excel asks the provider for a unique ID. When the workbook is saved, Excel gives the provider an
<strong>IStream </strong>pointer. The provider can write data into this stream, which is then saved with the workbook. When the workbook loads and updates the data connection, Excel gives the provider an
<strong>IStream</strong> pointer to the saved data. The provider then reads in the data from the stream.</p>
<p>Using the sample code, complete the following steps to create an OLE DB provider that supports custom data parts (for more information, see the associated article):</p>
<ol>
<li>Create an ATL OLE DB Provider and implement <strong>IMDEmbeddedData</strong>.
</li><li>Extend <strong>CMyProviderSource</strong> to implement the necessary interfaces. In
<strong>MyProviderDS.h</strong>, extend <strong>CMyProviderSource</strong> to implement
<strong>IMDEmbeddedData</strong> by removing the direct implementation of <strong>
IPersistImpl&lt;&gt;,</strong> because <strong>IMDEmbeddedData</strong> extends <strong>
IPersistStream</strong>. </li><li>In <strong>CMyProviderSource</strong>, add implementations for the methods defined in
<strong>IMDEmbeddedData</strong>. The methods defined in IMDEmbeddedData are as follows:
<ol>
<li><strong>SetHosted</strong>&mdash;This method indicates if Excel Services is hosting the provider, instead of Excel.
</li><li><strong>SetContainerURL</strong>&mdash;This method tells the provider the path of the workbook to which the provider is returning data, whether the provider is hosted on a server or on a local hard disk drive.
</li><li><strong>GetStreamIdentifier</strong>&mdash;This method instructs Excel how to determine the provider by returning a provider-specific string.
</li><li><strong>SetTempDirPath</strong>&mdash;This method instructs the provider where it can store temporary data.
</li><li><strong>IsDirty</strong>&mdash;This method instructs Excel whether the provider should save its data.
</li><li><strong>Cancel</strong>&mdash;This method is called when Excel cancels the save operation.
</li><li><strong>Load&mdash;</strong>This method is used during the refresh of the associated workbook connection.
</li><li><strong>Save&mdash;</strong>This method is where the provider writes its embedded data to be stored in the Excel workbook.
</li></ol>
</li><li>In <strong>CMyProviderSource</strong>, implement the <strong>IPersistStream</strong> methods
<strong>GetClassID</strong> and <strong>GetSizeMax</strong>.
<ol>
<li><strong>GetClassID</strong>&mdash;This method is called to get the CLSID of the provider.
</li><li><strong>GetSizeMax</strong>&mdash;This method is called to determine how much storage space the provider needs.
</li></ol>
</li><li>Add <strong>IMDEmbeddedData</strong> to the provider COM_MAP. </li><li>Add the necessary properties. </li><li>Update the string table. </li><li>Update the registry resource. </li><li>Build the project. </li><li>Use the provider in Excel 2010. A provider that supports&nbsp;custom data parts&nbsp;connects to Excel through a data connection like a standard data source provider.
</li></ol>
