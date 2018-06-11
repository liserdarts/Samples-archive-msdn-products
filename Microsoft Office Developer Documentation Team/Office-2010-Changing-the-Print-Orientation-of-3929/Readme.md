# Office 2010: Changing the Print Orientation of Word 2010 Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* print orientation
## IsPublished
* True
## ModifiedDate
* 2011-07-28 02:44:00
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the strongly typed classes in the Open XML SDK 2.0 to specify the print orientation for a Word document, without loading the document into Microsoft Word. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg308472.aspx">Changing the Print Orientation of Word 2010 Documents by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The Open XML file formats make it possible to specify print orientation in Microsoft Word documents. The Open XML SDK 2.0 adds strongly typed classes that simplify access to the Open XML file formats; the SDK simplifies the tasks of working with print orientation
 because it makes it easier to interact with the elements within the Open XML content. This sample shows how to the use the SDK to achieve this goal. Specifically, the sample code specifies print orientation in a Word 2007 or Word 2010 document.</p>
<p>To use the sample, install the Open XML SDK 2.0. The sample also uses a modified version of code included as part of a set of code examples for the Open XML SDK 2.0. The accompanying article also includes a link to the full set of code examples, although
 you can use the sample without downloading and installing the code examples.</p>
<p>To use the code from the Open XML SDK 2.0, you must add some references to your project. The sample project already includes these references, but in your own code, you would have to explicitly reference the following assemblies:</p>
<ul>
<li><strong>WindowsBase</strong>─This reference may be set for you, depending on the kind of project that you create.
</li><li><strong>DocumentFormat.OpenXml</strong>─This assembly is installed by the Open XML SDK 2.0.
</li></ul>
