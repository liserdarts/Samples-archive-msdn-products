# Office 2010: Replacing the Styles Parts in Word 2010 Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* replacing styles
## IsPublished
* True
## ModifiedDate
* 2011-07-28 03:55:20
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the strongly typed classes in the Open XML SDK 2.0 to replace the styles in a Word document with an
<strong>XDocument</strong> instance that contains the styles or <strong>stylesWithEffects</strong> part from a Microsoft Word document, without loading the document into Word. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg605191.aspx">Replacing the Styles Parts in Word 2010 Documents by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Open XML file formats make it possible to retrieve and modify blocks of content from Word documents, but doing so requires some effort. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Open XML file formats. The SDK simplifies
 the tasks of retrieving and replacing, for example, an <strong>XDocument </strong>
instance that contains the document styles or <strong>stylesWithEffects</strong> part. Given the XML content, you could archive the information, modify and reapply it, or apply it to a new document.</p>
<p>The sample includes the code necessary to replace the styles or <strong>styleWithEffects</strong> part in a document, given an
<strong>XDocument</strong> instance that contains the same part for a Word 2007 or Word 2010 document. The code includes the procedure that retrieves the part as an
<strong>XDocument</strong> instance as well.</p>
<p>Note that, for a document created in Word 2007, there is exactly one styles part; Word 2010 adds a second
<strong>stylesWithEffects</strong> part. To provide for relaying a document from Word 2010 to Word 2007 and back, Word 2010 maintains both the original styles part and the new styles part. The Open XML specification requires that Microsoft Word disregard any
 parts that it does not recognize; Word 2007 does not recognize the <strong>stylesWithEffects</strong> part that Word 2010 adds to the document. This code example shows how to extract and replace both parts, although the code is effectively the same for both
 parts. The sample code replaces both style parts with the style information in the specified
<strong>XDocument</strong>.</p>
<p>You need to put the attached&nbsp;documents, <a id="25659" href="/site/view/file/25659/1/StylesFrom.docx">
StylesFrom.docx</a> and <a id="25661" href="/site/view/file/25661/1/StylesTo.docx">
StylesTo.docx</a>, into your C:\temp directory before you run the sample code.</p>
