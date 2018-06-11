# Office 2010: Extracting Styles from Word 2010 Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* extracting styles
## IsPublished
* True
## ModifiedDate
* 2011-07-28 03:29:10
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the strongly typed classes in the Open XML SDK 2.0 to retrieve an
<strong>XDocument</strong> instance that contains the styles or <strong>stylesWithEffects</strong> part from a Microsoft Word document, without loading the document into Word. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg615380.aspx">Extracting Styles from Word 2010 Documents by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Open XML file formats make it possible to retrieve blocks of content from Microsoft Word documents, but doing so requires some effort. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Open XML file formats; the SDK simplifies
 the tasks of retrieving, for example, an <strong>XDocument</strong> instance that contains the document styles or
<strong>stylesWithEffects</strong> part. Given the XML content, you could archive the information, modify and reapply it, or apply it to a new document.</p>
<p>This sample code includes the code necessary to retrieve an <strong>XDocument</strong> instance that contains the styles or
<strong>stylesWithEffects</strong> part for a Word 2007 or Word 2010 document. Pushing the styles into a new document is not covered in this code sample.</p>
<p>Note that in a document created in Word 2007, there is exactly one styles part; Word 2010 adds a second
<strong>stylesWithEffects</strong> part. To provide for relaying a document from Word 2010 to Word 2007 and back, Word 2010 maintains both the original styles part and the new styles part. The Open XML specification requires that Microsoft Word disregard parts
 that it does not recognize; Word 2007 does not notice the <strong>stylesWithEffects</strong> part that Word 2010 adds to the document. Your application must interpret the results of retrieving the styles or
<strong>stylesWithEffects</strong> part.</p>
<p>To use the sample, install the Open XML SDK 2.0. The sample also uses a modified version of code included as part of a set of code examples for the Open XML SDK 2.0. The accompanying article also includes a link to the full set of code examples, although
 you can use the sample without downloading and installing the code examples.</p>
<p>The attached&nbsp;document, <a id="25652" href="/Office-2010-Extracting-61cbf162/file/25652/1/StylesFrom.docx">
StylesFrom.docx</a>, contains some sample-modified styles. You will need to put the file into your C:\temp directory before you run the sample code.</p>
