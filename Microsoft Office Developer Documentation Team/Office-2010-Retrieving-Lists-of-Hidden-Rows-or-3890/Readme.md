# Office 2010: Retrieving Lists of Hidden Rows or Columns in Excel
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Excel 2010
* Office 2010
## Topics
* getting hidden columns
* getting hidden rows
## IsPublished
* True
## ModifiedDate
* 2011-07-26 03:50:43
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use strongly typed classes in the Open XML SDK 2.0 to retrieve a list of hidden rows or columns in a Microsoft Excel 2007 or Excel 2010 worksheet, without loading the document into Excel. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff956189.aspx">Retrieving Lists of Hidden Rows or Columns in Excel 2010 Workbooks by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>This sample code retrieves a list of hidden rows or columns in a specified sheet in an Excel 2007 or Excel 2010 workbook. To use the sample, you must install the Open XML SDK 2.0. The sample also uses code included as part of a set of code examples for the
 Open XML SDK 2.0. The associated article includes a link to the full set of code examples, although you can use the sample without downloading and installing the code examples.</p>
<p>The sample application retrieves a list of hidden rows in a document that you supply, calling the
<strong>XLGetHiddenRowsOrCols</strong> method in the sample to do the work. This method returns a generic list of unsigned integers. The calling code must interpret and iterate through the returned list.</p>
<p>It is important to understand how Excel stores information about hidden rows and columns. The Open XML SDK 2.0 includes, in its tool directory, a useful application named
<strong>OpenXmlSdkTool.exe</strong> that enables you to open a document and view its various parts and the hierarchy of parts. If you expand a test document in the worksheet node of the tool, the tool displays both the XML for the part and the reflected C#
 code that you could use to generate the contents of the part.</p>
