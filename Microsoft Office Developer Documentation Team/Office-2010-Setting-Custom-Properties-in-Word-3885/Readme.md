# Office 2010: Setting Custom Properties in Word 2010 Documents
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Open XML SDK 2.0
* Word 2010
* Office 2010
## Topics
* setting properties
* custom properties
## IsPublished
* True
## ModifiedDate
* 2011-07-26 12:19:29
## Description

<h1><strong>Introduction</strong></h1>
<p>Learn how to use strongly typed classes in the Open XML SDK 2.0 to modify custom document properties in a Word 2007 or Word 2010 document, without loading the document into Microsoft Word. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff936167.aspx">Setting Custom Properties in Word 2010 Documents by Using the Open XML SDK 2.0</a> in the MSDN Library.</p>
<h1><strong>Description</strong></h1>
<p>The Open XML file formats enable you to modify custom document properties in a Word 2007 or Word 2010 document. The Open XML SDK 2.0 adds strongly typed classes to simplify access to the Open XML file formats. The SDK is designed to simplify the task of
 modifying custom document properties, and this download shows you how to use the SDK to do that.</p>
<p>This sample code creates and modifies custom document properties in a Word 2007 or Word 2010 document. To use the sample code, you must install the Open XML SDK 2.0. The sample code is included as part of a set of code examples for the Open XML SDK 2.0.
 The associated article includes a link to the full set of code examples, although you can use the sample code without downloading and installing the code examples.</p>
<p>The sample application modifies custom properties in a document that you supply, calling the
<strong>WDSetCustomProperty</strong> method in the sample to do the work. The method enables you to set a custom property, and returns the current value of the property, if it exists.</p>
