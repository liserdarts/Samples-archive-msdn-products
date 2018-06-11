# Office 2010: Retrieving Cell Values in Excel 2010 Workbooks
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Excel 2010
* Office 2010
* Microsoft Office 2010
## Topics
* getting cell values
## IsPublished
* True
## ModifiedDate
* 2011-07-26 12:24:06
## Description

<h1>Introduction</h1>
<p>Learn how to use the strongly-typed classes in the Open XML SDK 2.0 to retrieve the value of a cell in an Excel 2007 or Excel 2010 document, without loading the document into Microsoft Excel. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff921204.aspx">Retrieving the Values of Cells in Excel 2010 Workbooks by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>The Open XML file formats enable you to retrieve information about a particular cell in an Excel workbook. The Open XML SDK 2.0 adds strongly-typed classes to simplify access to the Open XML file formats. The SDK simplifies the tasks of retrieving information
 about the workbook, and finding the appropriate XML content.</p>
<p>This code sample shows how to the use the SDK to perform this task, and includes the code that is required to retrieve the value of a specified cell in a specified sheet in an Excel 2007 or Excel 2010 workbook.</p>
<p>To use the code sample, you must install the Open XML SDK 2.0. The sample also uses code that is included as part of a set of code examples for the Open XML SDK 2.0. The associated article includes a link to the full set of code examples, although you can
 use the sample without downloading and installing those examples.</p>
<p>The sample application retrieves the value from several cells in a document that you supply, calling the
<strong>XLGetCellValue </strong>method in the sample to do the work. This method returns the value of the specified cell as a string&mdash;the calling code must interpret the string value.</p>
