# Word 2010: Using the Drag-and-Drop Operation with VSTO Add-ins in Word 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* VSTO
* Drag and Drop
## IsPublished
* True
## ModifiedDate
* 2012-01-23 09:32:43
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Use Microsoft Visual Studio 2010 and Microsoft Visual Studio Tools for Office to create an add-in to Microsoft Word 2010 that makes it possible to respond when a user drags text onto the Word document. The approach shown in
 the sample is also </span><span style="font-size:small">applicable to objects other than text, for example, images. This sample accompanies the article &ldquo;<em>Using the Drag-and-Drop Operation with VSTO Add-ins in Word 2010</em>&rdquo; in the MSDN Library.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">To build the sample, you must have the following applications:</span></p>
<ul>
<li><span style="font-size:small">Microsoft Word 2010</span> </li><li><span style="font-size:small">Microsoft Visual Studio 2010</span> </li><li><span style="font-size:small">Microsoft .Net Framework 4.0 </span></li><li><span style="font-size:small">Microsoft Visual Studio 2010 Tools for Office Runtime
</span></li></ul>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">If you use only the Word object model, the only way that you can recognize a user&rsquo;s attempt to drop text onto a Word document is by listening for the
<strong>Application.WindowSelectionChange</strong> event. Unfortunately, this event fires any time the selection changes in the document, making it difficult to identify and respond specifically to a drag-and-drop operation. This article presents a simple way
 to capture and respond to this operation. </span></p>
<p><span style="font-size:small">The sample includes the code necessary to create a Visual Studio add-in for Word that creates a custom task pane that contains a list box that in turn contains some sample text. When a user drags text from the list box onto
 the Word document, the add-in registers this action and responds by drawing a transparent Windows form on the Word page. When the user drops the text on the transparent form, the add-in uses the
<strong>System.Windows.Forms.Control.DragDrop</strong> event of the Windows Forms API to capture the coordinates of the location in the Windows form where the drop is to occur. It then passes those coordinates to the Word object model, as shown in the following
 code:</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">Microsoft.Office.Interop.Word.Range 
range=(Microsoft.Office.Interop.Word.Range)Globals.ThisAddIn.Application.ActiveWindow.RangeFromPoint(e.X, e.Y);</pre>
<div class="preview">
<pre class="csharp">Microsoft.Office.Interop.Word.Range&nbsp;&nbsp;
range=(Microsoft.Office.Interop.Word.Range)Globals.ThisAddIn.Application.ActiveWindow.RangeFromPoint(e.X,&nbsp;e.Y);</pre>
</div>
</div>
</div>
<p><span style="font-size:small">The add-in hides the form, and then creates a Word table at these coordinates that holds a copy of the dragged text in each of its cells.</span></p>
<h1><span>Source Code Files</span></h1>
<p><span style="font-size:small">The sample consists of a Visual Studio 2010 solution,
<em>dragdrop.sln</em>, which contains a number of source files. After downloading the compressed file, save it to your computer, extract all the compressed files, and then double-click the solution file to open it in Visual Studio.</span><em><em></em></em></p>
<h1>More Information</h1>
<p><span style="font-size:small">For more information about drag and drop operations in Windows Forms, see the MSDN article
<a href="http://msdn.microsoft.com/en-us/library/aa984430(v=vs.71).aspx">Performing Drag-and-Drop Operations in Windows Forms</a>.</span></p>
<p><span style="font-size:small">For more information on some of the Windows Application UI Window functions used in the add-in, please see the following MSDN articles:</span></p>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633500(v=vs.85).aspx">FindWindowEx function</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633519(v=vs.85).aspx">GetWindowRect function</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633541(v=vs.85).aspx">SetParent function</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633545(v=vs.85).aspx">SetWindowPos function</a></span>
</li></ul>
