# Office 2010: Retrieving Comments from Word 2010 Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* retrieving comments
* Word comments
## IsPublished
* True
## ModifiedDate
* 2011-08-09 11:59:20
## Description

<h2>Introduction</h2>
<p><span style="font-size:small">Use the strongly typed classes in the Open XML SDK 2.0 to retrieve an XML block that contains all the comments from a Word document, without loading the document into Microsoft Word. This sample accompanies the Visual How To&nbsp;<a href="http://msdn.microsoft.com/en-us/library/hh344204.aspx" target="_blank">Retrieving
 Comments from Word 2010 Documents by Using the Open XML SDK 2.0</a>&nbsp;in the MSDN Library.</span></p>
<h2><span>Description</span></h2>
<p><span style="font-size:small">This sample includes the code necessary to retrieve the XML block that contains all the comments from a Word 2007&nbsp;or Word 2010&nbsp;document. When you use the sample code to retrieve the comments, the procedure returns
 an XML element, named w:comments, which contains the XML block of information from the original document. It's up to you (and your application) to interpret the results of retrieving the comments.</span></p>
<p><span style="font-size:small">To use the code from the Open XML SDK 2.0, you must add a few references to your project. The sample project already includes these references, but in your own code, you would need to explicitly reference the following assemblies:</span></p>
<ul>
<li><span style="font-size:small">WindowsBase (this reference may already be set for you, depending on the type of project you create)</span>
</li><li><span style="font-size:small">DocumentFormat.OpenXml (installed by the Open XML SDK 2.0)</span>
</li></ul>
<p>The sample application demonstrates only several of the available properties and methods that are provided by the Open XML SDK 2.0 that you can interact with when you are retrieving and modifying document structure. For more information, see the documentation
 that is included with the Open XML SDK 2.0 Productivity Tool: Click the <strong>
Open XML SDK Documentation</strong> tab in the lower-left corner of the application window, and search for the class that you need to study. Although the documentation does not currently include code examples, given the sample shown here and the documentation,
 you should be able to successfully modify the sample application.</p>
