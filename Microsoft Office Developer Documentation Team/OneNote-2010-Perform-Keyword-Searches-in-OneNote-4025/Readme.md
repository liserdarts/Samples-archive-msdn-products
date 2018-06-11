# OneNote 2010: Perform Keyword Searches in OneNote and Get Results in XML Format
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
* OneNote 2010
## Topics
* Office 2010 101 code samples
* OneNote notebooks
## IsPublished
* True
## ModifiedDate
* 2011-08-03 03:27:50
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to retrieve a Microsoft OneNote 2010 notebook and then perform a keyword search of that notebook, returning the results as XML.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Use any VBA host including Excel 2010, PowerPoint 2010, or Word 2010. OneNote 2010 is not a VBA host.</span></p>
<p><span style="font-size:small">In your VBA host, add references to the following external libraries using the Add References dialog:</span><br>
<span style="font-size:small">&nbsp;- Microsoft OneNote 14.0 Object Library</span><br>
<span style="font-size:small">&nbsp;- Microsoft XML, v6.0</span></p>
<p><span style="font-size:small">OneNote's GetHierarchy method allows you to get meta-data and data about the OneNote Notebooks.</span></p>
<p><span style="font-size:small">Paste all this code into a module and run the FindPagesInTheFirstNoteBook to search for a user provided string in the first notebook.</span></p>
<p><span style="font-size:small">Works with the FindPages method.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub FindPagesInTheFirstNoteBook()
    ' Connect to OneNote 2010
    ' OneNote will be started if it's not running.
    Dim oneNote As OneNote14.Application
    Set oneNote = New OneNote14.Application
    
    ' Get all of the Notebook nodes.
    Dim nodes As MSXML2.IXMLDOMNodeList
    Set nodes = GetFirstOneNoteNotebookNodes(oneNote)
    If Not nodes Is Nothing Then
        ' Get the first notebook found.
        Dim node As MSXML2.IXMLDOMNode
        Set node = nodes(0)
        ' Get the ID.
        Dim notebookID As String
        notebookID = node.Attributes.getNamedItem(&quot;ID&quot;).Text
                
        ' Ask the user for a string for which to search
        ' with a default search string of &quot;Microsoft&quot;.
        Dim searchString As String
        searchString = InputBox$(&quot;Enter a search string.&quot;, &quot;Search&quot;, &quot;Microsoft&quot;)
        
        Dim searchResultsAsXml As String
        ' The FindPages method search a OneNote object (in this example, the first
        ' open Notebook). You provide the search string and the results are
        ' provided as an XML document listing the objects where the search
        ' string is found. You can control whether OneNote searches non-indexed data (this
        ' example passes False). You can also choose whether OneNote enables
        ' the User Interface to show the found items (this example passes False).
        ' This example instructs OneNote to return the XML data in the 2010 schema format.
        oneNote.FindPages notebookID, searchString, searchResultsAsXml, False, False, xs2010
        
        ' Output the returned XML to the Immediate Window.
        ' If no search items are found, the XML contains the
        ' XML hierarchy data for the searched item.
        Debug.Print searchResultsAsXml
    Else
        MsgBox &quot;OneNote 2010 XML data failed to load.&quot;
    End If
    
End Sub

Private Function GetAttributeValueFromNode(node As MSXML2.IXMLDOMNode, attributeName As String) As String
    If node.Attributes.getNamedItem(attributeName) Is Nothing Then
        GetAttributeValueFromNode = &quot;Not found.&quot;
    Else
        GetAttributeValueFromNode = node.Attributes.getNamedItem(attributeName).Text
    End If
End Function

Private Function GetFirstOneNoteNotebookNodes(oneNote As OneNote14.Application) As MSXML2.IXMLDOMNodeList
    ' Get the XML that represents the OneNote notebooks available.
    Dim notebookXml As String
    ' Fill notebookXml with an XML document providing information
    ' about available OneNote notebooks.
    ' To get all the data, provide an empty string
    ' for the bstrStartNodeID parameter.
    oneNote.GetHierarchy &quot;&quot;, hsNotebooks, notebookXml, xs2010
    
    ' Use the MSXML Library to parse the XML.
    Dim doc As MSXML2.DOMDocument
    Set doc = New MSXML2.DOMDocument
    
    If doc.LoadXML(notebookXml) Then
        Set GetFirstOneNoteNotebookNodes = doc.DocumentElement.SelectNodes(&quot;//one:Notebook&quot;)
    Else
        Set GetFirstOneNoteNotebookNodes = Nothing
    End If
End Function
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;FindPagesInTheFirstNoteBook()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Connect&nbsp;to&nbsp;OneNote&nbsp;2010</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;OneNote&nbsp;will&nbsp;be&nbsp;started&nbsp;if&nbsp;it's&nbsp;not&nbsp;running.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;oneNote&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;oneNote&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;all&nbsp;of&nbsp;the&nbsp;Notebook&nbsp;nodes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;nodes&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNodeList&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;nodes&nbsp;=&nbsp;GetFirstOneNoteNotebookNodes(oneNote)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;nodes&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;first&nbsp;notebook&nbsp;found.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;node&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNode&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;node&nbsp;=&nbsp;nodes(<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;ID.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;notebookID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;notebookID&nbsp;=&nbsp;node.Attributes.getNamedItem(<span class="visualBasic__string">&quot;ID&quot;</span>).Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Ask&nbsp;the&nbsp;user&nbsp;for&nbsp;a&nbsp;string&nbsp;for&nbsp;which&nbsp;to&nbsp;search</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;with&nbsp;a&nbsp;default&nbsp;search&nbsp;string&nbsp;of&nbsp;&quot;Microsoft&quot;.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;searchString&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;searchString&nbsp;=&nbsp;InputBox$(<span class="visualBasic__string">&quot;Enter&nbsp;a&nbsp;search&nbsp;string.&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Search&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;Microsoft&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;searchResultsAsXml&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;FindPages&nbsp;method&nbsp;search&nbsp;a&nbsp;OneNote&nbsp;object&nbsp;(in&nbsp;this&nbsp;example,&nbsp;the&nbsp;first</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;open&nbsp;Notebook).&nbsp;You&nbsp;provide&nbsp;the&nbsp;search&nbsp;string&nbsp;and&nbsp;the&nbsp;results&nbsp;are</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;provided&nbsp;as&nbsp;an&nbsp;XML&nbsp;document&nbsp;listing&nbsp;the&nbsp;objects&nbsp;where&nbsp;the&nbsp;search</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;string&nbsp;is&nbsp;found.&nbsp;You&nbsp;can&nbsp;control&nbsp;whether&nbsp;OneNote&nbsp;searches&nbsp;non-indexed&nbsp;data&nbsp;(this</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;example&nbsp;passes&nbsp;False).&nbsp;You&nbsp;can&nbsp;also&nbsp;choose&nbsp;whether&nbsp;OneNote&nbsp;enables</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;User&nbsp;Interface&nbsp;to&nbsp;show&nbsp;the&nbsp;found&nbsp;items&nbsp;(this&nbsp;example&nbsp;passes&nbsp;False).</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;example&nbsp;instructs&nbsp;OneNote&nbsp;to&nbsp;return&nbsp;the&nbsp;XML&nbsp;data&nbsp;in&nbsp;the&nbsp;2010&nbsp;schema&nbsp;format.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oneNote.FindPages&nbsp;notebookID,&nbsp;searchString,&nbsp;searchResultsAsXml,&nbsp;<span class="visualBasic__keyword">False</span>,&nbsp;<span class="visualBasic__keyword">False</span>,&nbsp;xs2010&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Output&nbsp;the&nbsp;returned&nbsp;XML&nbsp;to&nbsp;the&nbsp;Immediate&nbsp;Window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;no&nbsp;search&nbsp;items&nbsp;are&nbsp;found,&nbsp;the&nbsp;XML&nbsp;contains&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;XML&nbsp;hierarchy&nbsp;data&nbsp;for&nbsp;the&nbsp;searched&nbsp;item.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;searchResultsAsXml&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;OneNote&nbsp;2010&nbsp;XML&nbsp;data&nbsp;failed&nbsp;to&nbsp;load.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;GetAttributeValueFromNode(node&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNode,&nbsp;attributeName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;node.Attributes.getNamedItem(attributeName)&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetAttributeValueFromNode&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Not&nbsp;found.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetAttributeValueFromNode&nbsp;=&nbsp;node.Attributes.getNamedItem(attributeName).Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;GetFirstOneNoteNotebookNodes(oneNote&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Application)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNodeList&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;XML&nbsp;that&nbsp;represents&nbsp;the&nbsp;OneNote&nbsp;notebooks&nbsp;available.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;notebookXml&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Fill&nbsp;notebookXml&nbsp;with&nbsp;an&nbsp;XML&nbsp;document&nbsp;providing&nbsp;information</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;about&nbsp;available&nbsp;OneNote&nbsp;notebooks.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;To&nbsp;get&nbsp;all&nbsp;the&nbsp;data,&nbsp;provide&nbsp;an&nbsp;empty&nbsp;string</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;the&nbsp;bstrStartNodeID&nbsp;parameter.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetHierarchy&nbsp;<span class="visualBasic__string">&quot;&quot;</span>,&nbsp;hsNotebooks,&nbsp;notebookXml,&nbsp;xs2010&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;the&nbsp;MSXML&nbsp;Library&nbsp;to&nbsp;parse&nbsp;the&nbsp;XML.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.DOMDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;MSXML2.DOMDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;doc.LoadXML(notebookXml)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;GetFirstOneNoteNotebookNodes&nbsp;=&nbsp;doc.DocumentElement.SelectNodes(<span class="visualBasic__string">&quot;//one:Notebook&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;GetFirstOneNoteNotebookNodes&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25987" href="/site/view/file/25987/1/OneNote.fromVBA.FindPagesInTheFirstNoteBook.txt">OneNote.fromVBA.FindPagesInTheFirstNoteBook.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25988" href="/site/view/file/25988/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905452">OneNote Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
