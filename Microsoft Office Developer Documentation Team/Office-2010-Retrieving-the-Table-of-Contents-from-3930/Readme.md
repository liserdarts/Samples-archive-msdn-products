# Office 2010: Retrieving the Table of Contents from Word 2010 Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* Word table of contents
## IsPublished
* True
## ModifiedDate
* 2011-07-28 02:59:01
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the strongly typed classes in the Open XML SDK 2.0 to retrieve an XML block that contains the table of contents from a Word document, without loading the document into Microsoft Word. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg308473.aspx">Retrieving the Table of Contents from Word 2010 Documents by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Open XML file formats make it possible to retrieve blocks of content from Word documents. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Open XML file formats; the SDK simplifies the tasks of retrieving, in this case, the
 block of XML that contains the table of contents. This sample shows how to use the SDK to achieve this goal. Specifically, the sample code retrieves the XML block that contains the table of contents for a Word 2007 or Word 2010 document.</p>
<p>Be aware that Word provides at least four distinct means of inserting a table of contents into a document. Two of these techniques result in internal XML code that will work with the code in this sample. Specifically, the following two techniques provide
 a table of contents nested with an <strong>SdtBlock</strong> element:</p>
<ul>
<li>Use the Built-In Designs&mdash;On the <strong>References</strong> tab, in the
<strong>Table of Contents</strong> group, click <strong>Table of Contents</strong>, and select one of the built-in designs from the gallery of choices (currently, three options).
</li><li>Use the <strong>More Table of Contents</strong> from Office.com entry&mdash;On the
<strong>references</strong> tab, in the <strong>Table of Contents</strong> group, click
<strong>Table of Contents</strong>, and then click <strong>More Table of Contents</strong><strong> from Office.com</strong> near the bottom.
</li></ul>
<p>The following two techniques, which also result in a valid table of contents in the document, do not nest the table of contents inside an
<strong>SdtBlock</strong> element so that the code shown in this sample will not work:</p>
<ul>
<li>Use the <strong>Insert Table of Contents</strong> Entry&mdash;On the <strong>
References</strong> tab, in the <strong>Table of Contents Group</strong>, click <strong>
Table of Contents</strong>, and then click <strong>Insert Table of Contents</strong> near the bottom.
</li><li>Use the <strong>Insert Fields</strong> option&mdash;On the <strong>Insert</strong> tab, in the
<strong>Text Group</strong>, click <strong>Quick Parts</strong>, and then click <strong>
Field</strong>. Under <strong>Field Names</strong>, click <strong>TOC</strong>, click
<strong>Table of Contents</strong> (optional), and then click <strong>OK</strong>.
</li></ul>
<p>These two options create free-standing table of contents elements, and would require more complex code to extract the table of contents from the document. If you import a document from a version of Word earlier than Word 2007, the code will also not work,
 because in those versions, Word never used the <strong>SdtBlock</strong> around the table of contents elements.</p>
