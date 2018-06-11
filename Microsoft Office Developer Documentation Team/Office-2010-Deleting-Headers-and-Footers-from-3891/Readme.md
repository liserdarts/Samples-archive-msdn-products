# Office 2010: Deleting Headers and Footers from Word Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* deleting headers
* deleting footers
## IsPublished
* True
## ModifiedDate
* 2011-07-26 04:07:46
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the strongly typed classes in the Open XML SDK 2.0 to delete all headers and footers in a Microsoft Word document, without loading the document into Word. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg591269.aspx">Deleting Headers and Footers from Word 2010 Documents by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Open XML file formats make it possible to retrieve and modify blocks of content from Word documents, but doing so requires some effort. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Open XML file formats. For example, the
 SDK simplifies the tasks of working with the header and footer parts and references to those parts in the document.</p>
<p>This code sample shows how to the use the SDK to delete all the header and footer parts, along with references to those parts in the main document part, all without requiring you to open the document in Microsoft Word. The sample includes the code necessary
 to delete all of the header and footer information in a Word 2007 or Word 2010 document. The associated article describes the code in detail. You should note that it is not enough to simply delete the header and footer parts from the document storage; you
 must also delete references to those parts from within the document itself. The sample code demonstrates both steps in the operation.</p>
