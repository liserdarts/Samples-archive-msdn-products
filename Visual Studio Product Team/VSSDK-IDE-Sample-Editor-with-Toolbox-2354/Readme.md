# VSSDK IDE Sample: Editor with Toolbox
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010 SDK
## Topics
* Visual Studio 2010 Shell
* VSX
## IsPublished
* True
## ModifiedDate
* 2011-02-21 04:18:30
## Description

<p>This sample demonstrates how to create a package that provides an Editor type extended with Toolbox support.<br>
<br>
In this sample we implement an editor for a .tbx file and construct toolbox items that are available for the document.
<a href="http://archive.msdn.microsoft.com/EditorwithToolbox/Wiki/View.aspx?title=EditorWithToolbox&referringTitle=Home">
EditorWithToolbox</a><br>
<br>
</p>
<h3>Goals</h3>
<ul>
<li>Provide an editor factory. </li><li>Document integration and persistence. </li><li>Toolbox integration </li><li>Handle source-controlled and read-only files. </li></ul>
<p><br>
This sample implements an editor for a &quot;.tbx&quot; plain text file. The actual UI of this designer is simply a RichTextBox control. This sample demonstrates how to implement integration with Visual Studio Toolbox within the Editor.<br>
<br>
The Editor uses SVsToolbox service and implements IVsToolboxUser in order to support Toolbox integration. It handles toolbox items availability and supports drag and drop text from the toolbox.<br>
<br>
<br>
</p>
<h3>To start the sample:</h3>
<ol>
<li>Build the solution </li><li>Open Visual Studio under experimental hive by pressing F5 </li></ol>
<p>&nbsp;</p>
<h3>To test the samples functionality</h3>
<ol>
<li>On the <strong>File</strong> menu, click <strong>New</strong> and then click <strong>
File</strong>. </li><li>In the <strong>Categories</strong> box, select <strong>Test Editor</strong>. In the
<strong>Templates</strong> box, select <strong>Test Editor</strong>. Both come from the sample VSPackage. Then click
<strong>Open</strong>. The VSPackage opens a new file tab with the embedded rich textbox.
</li><li>Click <strong>Toolbox</strong> from the <strong>View</strong> menu. Expand the
<strong>Toolbox Test</strong> tab. As with all toolbox tabs, it has a Pointer tool. Drag the
<strong>Toolbox Sample Item</strong> tool into the .tbx file window. <strong>NOTE:</strong> If your toolbox does not have a
<strong>Toolbox Test</strong> tab or <strong>Toolbox Sample Item</strong> tool, make sure you have a .tbx file as the active file. The tab and tool are registered specifically to the .tbx file editor.
</li></ol>
<p><br>
&nbsp;</p>
<h3>Screenshot</h3>
<h3>&nbsp;<img src="/VSSDK-IDE-Sample-Editor-685aa6d9/image/file/18571/1/Example.EditorWithToolbox.jpg" alt="" width="784" height="606"></h3>
<h3>&nbsp;</h3>
<h3>Additional Resources</h3>
<p>&nbsp;</p>
<h4>Unit Tests:</h4>
<p>&nbsp;</p>
<ul>
<li>Verify that the package can be created and sited and that it is instantiates.
</li><li>Test exist for all command functions&nbsp;&nbsp; </li><li>Test the pane properties&nbsp;&nbsp; </li><li>Test the factory properties </li></ul>
