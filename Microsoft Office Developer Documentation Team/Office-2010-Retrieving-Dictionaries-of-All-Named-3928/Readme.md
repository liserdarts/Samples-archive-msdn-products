# Office 2010: Retrieving Dictionaries of All Named Ranges in Excel 2010 Workbooks
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Excel 2010
* Office 2010
## Topics
* Excel ranges
## IsPublished
* True
## ModifiedDate
* 2011-07-28 02:34:50
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the strongly typed classes in the Open XML SDK 2.0 to retrieve a Dictionary that contains the names and ranges of all defined names in an Excel workbook, without loading the document into Microsoft Excel. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/15441de9-d830-4ea0-80e0-b921d5ac614b.aspx">
Retrieving Dictionaries of All Named Ranges in Excel 2010 Workbooks by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Open XML file formats make it possible to retrieve information about defined names in an Excel workbook, but doing so requires some effort. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Open XML file formats; the SDK simplifies
 the tasks of retrieving, in particular, the list of defined names that each workbook maintains. This sample shows how to use the SDK to accomplish this goal.</p>
<p>Specifically, the sample code retrieves a dictionary that contains information about each defined name in an Excel 2007 or Excel 2010 document. Within the dictionary, each item's key contains the name for the range, and the value contains a string representation
 of the range itself.</p>
<p>To use the sample, install the Open XML SDK 2.0. The sample also uses a modified version of code included as part of a set of code examples for the Open XML SDK 2.0. The accompanying article also includes a link to the full set of code examples, although
 you can use the sample without downloading and installing the code examples.</p>
