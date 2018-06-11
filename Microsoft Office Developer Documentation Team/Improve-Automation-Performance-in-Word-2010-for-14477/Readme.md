# Improve Automation Performance in Word 2010 for Large Amounts Using Open XML
## Requires
* Visual Studio 2008
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Office Word 2007
* Open XML SDK 2.0
* Word 2010
## Topics
* Word Automation
* Office Automation
* Automation
## IsPublished
* True
## ModifiedDate
* 2011-12-13 09:21:06
## Description

<h1>Introduction</h1>
<p>Learn how to insert large amounts of data quickly by using Open XML. On a typical 4-GHz computer with a single processor, you can insert a 300-row table with an image in each cell by using a content control in less than five seconds. Using Word automation,
 this process takes much more than a minute.</p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<div>Prior to Open XML, if you had to perform document manipulation operations in Microsoft Office Word 2007 in an open document, you had to use technologies such as macros and Microsoft Visual Studio Tools for the Microsoft Office system (3.0). With Open XML
 formats, you can alter document contents by using the Open XML SDK 1.0 for Microsoft Office. If you are inserting many objects, then inserting by using the Open XML SDK 1.0 or the Open XML SDK 2.0 for Microsoft Office can give you a significant performance
 improvement.</div>
<div>The following code shows how to insert a large amount of data by using Open XML. This example assumes that you have the Open XML SDK 1.0 or the Open XML SDK 2.0 installed.</div>
<div></div>
<div><strong>Important</strong>: You must download and install the following items to run this code:</div>
<div>
<ul>
<li class="unordered"><a href="http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en">Microsoft .NET Framework 3.5</a>
</li><li class="unordered"><a href="http://www.microsoft.com/downloads/details.aspx?FamilyId=C6E744E5-36E9-45F5-8D8C-331DF206E0D0&displaylang=en">Open XML SDK 2.0 for Microsoft Office</a>
</li></ul>
</div>
<h2 class="subHeading">To run the sample</h2>
<div class="subSection">
<ol class="ordered">
<li>
<div>Build the sample project, and then press F5 to open a Microsoft Word document.</div>
</li><li>
<div>Click <strong>Insert table using Open XML SDK 2.0</strong>.</div>
<div>This inserts 300 table rows and images to the document by using the Open XML SDK 1.0.</div>
</li></ol>
</div>
<h2 class="subHeading">Understanding the Sample Logic</h2>
<div class="subSection">
<div>First, the code sample begins with setting the screen refresh property to <strong>
false</strong>. Next, it prepares the XML, finds a suitable range, and then inserts the Flat OPC XML. Building this kind of Flat OPC XML can be challenging. You can use the
<strong>Range.WordOpenXml</strong> object to get the Flat OPC XML for that range. This is abstracted out, with an extension method as shown in the followg example.</div>
</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// Get stream for the range. This is the package stream.
Stream packageStream = this.Paragraphs[1].Range.GetPackageStreamFromRange();
// Use the Open XML SDK 1.0 to process it.
using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(packageStream, true))
{
    wordDoc.MainDocumentPart.Document.Save();
    // Flush the contents of the package.
    wordDoc.Package.Flush();
    // Convert back to flat OPC by using this in-memory package.
    XDocument xDoc = OpcHelper.OpcToFlatOpc(wordDoc.Package);
    // Return this string.
    openxml = xDoc.ToString();
    this.Application.ScreenUpdating = false;
    Word.Range range = FindRange(&quot;bkflatOpc&quot;);
   // Insert this XML.
    range.InsertXML(openxml, ref missing);
    this.Application.ScreenUpdating = true; 

</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Get&nbsp;stream&nbsp;for&nbsp;the&nbsp;range.&nbsp;This&nbsp;is&nbsp;the&nbsp;package&nbsp;stream.</span>&nbsp;
Stream&nbsp;packageStream&nbsp;=&nbsp;<span class="cs__keyword">this</span>.Paragraphs[<span class="cs__number">1</span>].Range.GetPackageStreamFromRange();&nbsp;
<span class="cs__com">//&nbsp;Use&nbsp;the&nbsp;Open&nbsp;XML&nbsp;SDK&nbsp;1.0&nbsp;to&nbsp;process&nbsp;it.</span>&nbsp;
<span class="cs__keyword">using</span>&nbsp;(WordprocessingDocument&nbsp;wordDoc&nbsp;=&nbsp;WordprocessingDocument.Open(packageStream,&nbsp;<span class="cs__keyword">true</span>))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wordDoc.MainDocumentPart.Document.Save();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Flush&nbsp;the&nbsp;contents&nbsp;of&nbsp;the&nbsp;package.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wordDoc.Package.Flush();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Convert&nbsp;back&nbsp;to&nbsp;flat&nbsp;OPC&nbsp;by&nbsp;using&nbsp;this&nbsp;in-memory&nbsp;package.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;XDocument&nbsp;xDoc&nbsp;=&nbsp;OpcHelper.OpcToFlatOpc(wordDoc.Package);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Return&nbsp;this&nbsp;string.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;openxml&nbsp;=&nbsp;xDoc.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Application.ScreenUpdating&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Word.Range&nbsp;range&nbsp;=&nbsp;FindRange(<span class="cs__string">&quot;bkflatOpc&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Insert&nbsp;this&nbsp;XML.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;range.InsertXML(openxml,&nbsp;<span class="cs__keyword">ref</span>&nbsp;missing);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.Application.ScreenUpdating&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;After you have a stream, you can use the Open XML SDK 1.0 or the Open XML SDK 2.0. With the Open XML SDK 2.0, you can use advanced features such as document validation. To see the difference in performance by using this approach,
 click the <strong>Insert table using Automation</strong> button. This inserts 300 table rows with content controls in each cell by using Word Automation.</div>
</div>
<div><em><em>&nbsp;</em></em></div>
<h1>More Information</h1>
<div><em>For more information on X, see <a title="Using Open XML to Improve Automation Performance in Word 2010 for Large Amounts of Data" href="http://msdn.microsoft.com/en-us/library/ff191178.aspx">
http://msdn.microsoft.com/en-us/library/ff191178.aspx</a></em></div>
