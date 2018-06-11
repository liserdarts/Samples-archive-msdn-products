# SharePoint 2010: Converting Word Documents to PDF in SharePoint Server 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* Word 2010
## Topics
* converting DOC/DOCX to PDF
## IsPublished
* True
## ModifiedDate
* 2011-08-10 02:28:15
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn to programmatically convert Microsoft Word documents to PDF format on the server by using Word Automation Services with Microsoft SharePoint Server 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff181518.aspx">Converting Word Documents to PDF Using SharePoint Server 2010 and Word Automation Services</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>SharePoint 2010 Word Automation Services, which is available with SharePoint Server 2010, supports converting Word documents to other formats, including PDF. This sample uses a document library list item event receiver to call Word Automation Services to
 convert Word documents to PDF when they are added to the list. The event receiver checks whether the list item added is a Word document. If so, it creates a conversion job to create a PDF version of the Word document and pushes the conversion job to the Word
 Automation Services conversion job queue.</p>
<p>Word Automation Services provided with SharePoint Server 2010 enables you to create server-based document solutions. Combining the functionality that is provided by Word Automation Services with the document content manipulation support provided with the
 Open XML SDK enables you to create rich document solutions that execute on the server that do not require Automation of the Word client application.</p>
<p>The sample demonstrates the following steps:</p>
<ol>
<li>The <a href="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.spitemeventreceiver.itemadded.aspx">
ItemAdded</a> event handler in the list event handler first verifies that the item added to the document library list is a Word document by checking the name of the document for the .doc or .docx file name extension.
</li><li>If the item is a Word document, the code creates and initializes <a href="http://msdn.microsoft.com/en-us/library/microsoft.office.word.server.conversions.conversionjobsettings.aspx">
ConversionJobSettings</a> and <a href="http://msdn.microsoft.com/en-us/library/microsoft.office.word.server.conversions.conversionjob.aspx">
ConversionJob</a> objects to convert the document to the PDF format. </li><li>The Word document to be converted and the name of the PDF document to be created are added to the
<strong>ConversionJob</strong>. </li><li>Finally the <strong>ConversionJob</strong> is added to the Word Automation Services conversion job queue.
</li></ol>
