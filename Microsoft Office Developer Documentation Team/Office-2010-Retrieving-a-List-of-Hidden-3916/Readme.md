# Office 2010: Retrieving a List of Hidden Worksheets from Excel 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Excel 2010
* Office 2010
## Topics
* hidden worksheets
## IsPublished
* False
## ModifiedDate
* 2011-09-21 10:28:50
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Use the strongly typed classes in the Open XML SDK 2.0 to retrieve a list of hidden worksheets in an Excel workbook, without loading the document into Microsoft Excel. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/hh370976.aspx" target="_blank">Retrieving a List of Hidden Worksheets from Excel 2010 Workbooks by Using the Open XML SDK 2.0</a> in the MSDN Library.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">The Office Open XML file formats make it possible to retrieve information about Excel workbooks, but doing so requires some effort. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Office Open XML
 file formats. The SDK simplifies the tasks of working with, for example, information about hidden worksheets within a workbook. This code sample and the accompanying article show how to the use the SDK to retrieve a generic list that contains information about
 all the hidden worksheets in a workbook, without requiring you to open the document in Microsoft Excel.</span></p>
<p><span style="font-size:small">To use the code from the Open XML SDK 2.0, you must add a few references to your project. The sample project already includes these references, but in your own code, you would need to explicitly reference the following assemblies:</span></p>
<ul>
<li><span style="font-size:small">WindowsBase (this reference may already be set for you, depending on the type of project you create)</span>
</li><li><span style="font-size:small">DocumentFormat.OpenXml (installed by the Open XML SDK 2.0)</span>
</li></ul>
