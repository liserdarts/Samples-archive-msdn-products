# OneNote 2010: Retrieve Attribute Data About Sections in OneNote 2010 Notebooks
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
* 2011-08-03 03:47:00
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>GetHierarchy
</strong>method to get attribute data about the sections in Microsoft OneNote 2010 notebooks from any VBA host (excluding OneNote).</span></p>
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
<p><br>
<span style="font-size:small">Paste all this code into a module, place the cursor within the ListOneNoteSectionsFromFirstNotebook procedure, and press F5.</span><br>
&nbsp;<br>
<span style="font-size:small">The ListOneNoteSectionsFromFirstNotebook procedure uses the MSXML library to parse the returned XML from OneNote and output the first Notebook's&nbsp; Section meta-data to the Immediate window of your VBA host.</span></p>
<p><span style="font-size:small">In order to do this, the code loads up the&nbsp;list of available Notebooks and uses&nbsp;the first one returned.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ListOneNoteSectionsFromFirstNotebook()
    ' Connect to OneNote 2010.
    ' OneNote will be started if it's not running.
    Dim oneNote As OneNote14.Application
    Set oneNote = New OneNote14.Application
    
    ' Get all of the Notebook nodes.
    Dim nodes As MSXML2.IXMLDOMNodeList
    Set nodes = GetFirstOneNoteNotebookNodes(oneNote)
    If Not nodes Is Nothing Then
        ' Get the first OneNote Notebook in the XML document.
        Dim node As MSXML2.IXMLDOMNode
        Set node = nodes(0)
        Dim noteBookName As String
        noteBookName = node.Attributes.getNamedItem(&quot;name&quot;).Text
        
        ' Get the ID for the Notebook so the code can retrieve
        ' the list of sections.
        Dim notebookID As String
        notebookID = node.Attributes.getNamedItem(&quot;ID&quot;).Text
        
        ' Load the XML for the Sections for the Notebook requested.
        Dim sectionsXml As String
        oneNote.GetHierarchy notebookID, hsSections, sectionsXml, xs2010
        
        Dim secDoc As MSXML2.DOMDocument
        Set secDoc = New MSXML2.DOMDocument
    
        If secDoc.LoadXML(sectionsXml) Then
            Dim secNodes As MSXML2.IXMLDOMNodeList
            Set secNodes = secDoc.DocumentElement.SelectNodes(&quot;//one:Section&quot;)

            If Not secNodes Is Nothing Then
                Dim secNode As MSXML2.IXMLDOMNode
                ' Walk the collection of Notebook sections.
                ' Read attribute values and write them
                ' out to the Immediate window.
                For Each secNode In secNodes
                    Debug.Print &quot;Notebook Name: &quot; &amp; noteBookName
                    Debug.Print &quot;  Section Name: &quot; &amp; secNode.Attributes.getNamedItem(&quot;name&quot;).Text
                    Debug.Print &quot;    Path: &quot; &amp; GetAttributeValueFromNode(secNode, &quot;path&quot;)
                    Debug.Print &quot;    ID: &quot; &amp; GetAttributeValueFromNode(secNode, &quot;ID&quot;)
                    Debug.Print &quot;    Color: &quot; &amp; GetAttributeValueFromNode(secNode, &quot;color&quot;)
                    Debug.Print &quot;    Is Unread: &quot; &amp; GetAttributeValueFromNode(secNode, &quot;isUnread&quot;)
                    Debug.Print &quot;    Is Currently Viewed: &quot; &amp; GetAttributeValueFromNode(secNode, &quot;isCurrentlyViewed&quot;)
                    Debug.Print &quot;    Last Modified: &quot; &amp; GetAttributeValueFromNode(secNode, &quot;lastModifiedTime&quot;)
                Next
            Else
                MsgBox &quot;OneNote 2010 Section nodes not found.&quot;
            End If
        Else
            MsgBox &quot;OneNote 2010 Section XML Data failed to load.&quot;
        End If
    Else
        MsgBox &quot;OneNote 2010 XML Data failed to load.&quot;
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
    ' OneNote fills notebookXml with an XML document providing information
    ' about what OneNote notebooks are available.
    ' You want all the data and thus are providing an empty string
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
End Function</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ListOneNoteSectionsFromFirstNotebook()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Connect&nbsp;to&nbsp;OneNote&nbsp;2010.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;OneNote&nbsp;will&nbsp;be&nbsp;started&nbsp;if&nbsp;it's&nbsp;not&nbsp;running.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;oneNote&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;oneNote&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;all&nbsp;of&nbsp;the&nbsp;Notebook&nbsp;nodes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;nodes&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNodeList&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;nodes&nbsp;=&nbsp;GetFirstOneNoteNotebookNodes(oneNote)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;nodes&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;first&nbsp;OneNote&nbsp;Notebook&nbsp;in&nbsp;the&nbsp;XML&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;node&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNode&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;node&nbsp;=&nbsp;nodes(<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;noteBookName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;noteBookName&nbsp;=&nbsp;node.Attributes.getNamedItem(<span class="visualBasic__string">&quot;name&quot;</span>).Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;ID&nbsp;for&nbsp;the&nbsp;Notebook&nbsp;so&nbsp;the&nbsp;code&nbsp;can&nbsp;retrieve</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;list&nbsp;of&nbsp;sections.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;notebookID&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;notebookID&nbsp;=&nbsp;node.Attributes.getNamedItem(<span class="visualBasic__string">&quot;ID&quot;</span>).Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Load&nbsp;the&nbsp;XML&nbsp;for&nbsp;the&nbsp;Sections&nbsp;for&nbsp;the&nbsp;Notebook&nbsp;requested.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sectionsXml&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetHierarchy&nbsp;notebookID,&nbsp;hsSections,&nbsp;sectionsXml,&nbsp;xs2010&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;secDoc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.DOMDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;secDoc&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;MSXML2.DOMDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;secDoc.LoadXML(sectionsXml)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;secNodes&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNodeList&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;secNodes&nbsp;=&nbsp;secDoc.DocumentElement.SelectNodes(<span class="visualBasic__string">&quot;//one:Section&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;secNodes&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;secNode&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNode&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Walk&nbsp;the&nbsp;collection&nbsp;of&nbsp;Notebook&nbsp;sections.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Read&nbsp;attribute&nbsp;values&nbsp;and&nbsp;write&nbsp;them</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;out&nbsp;to&nbsp;the&nbsp;Immediate&nbsp;window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;secNode&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;secNodes&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Notebook&nbsp;Name:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;noteBookName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Section&nbsp;Name:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;secNode.Attributes.getNamedItem(<span class="visualBasic__string">&quot;name&quot;</span>).Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;Path:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;GetAttributeValueFromNode(secNode,&nbsp;<span class="visualBasic__string">&quot;path&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;ID:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;GetAttributeValueFromNode(secNode,&nbsp;<span class="visualBasic__string">&quot;ID&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;Color:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;GetAttributeValueFromNode(secNode,&nbsp;<span class="visualBasic__string">&quot;color&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;Is&nbsp;Unread:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;GetAttributeValueFromNode(secNode,&nbsp;<span class="visualBasic__string">&quot;isUnread&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;Is&nbsp;Currently&nbsp;Viewed:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;GetAttributeValueFromNode(secNode,&nbsp;<span class="visualBasic__string">&quot;isCurrentlyViewed&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;Last&nbsp;Modified:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;GetAttributeValueFromNode(secNode,&nbsp;<span class="visualBasic__string">&quot;lastModifiedTime&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;OneNote&nbsp;2010&nbsp;Section&nbsp;nodes&nbsp;not&nbsp;found.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;OneNote&nbsp;2010&nbsp;Section&nbsp;XML&nbsp;Data&nbsp;failed&nbsp;to&nbsp;load.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;OneNote&nbsp;2010&nbsp;XML&nbsp;Data&nbsp;failed&nbsp;to&nbsp;load.&quot;</span>&nbsp;
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
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;OneNote&nbsp;fills&nbsp;notebookXml&nbsp;with&nbsp;an&nbsp;XML&nbsp;document&nbsp;providing&nbsp;information</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;about&nbsp;what&nbsp;OneNote&nbsp;notebooks&nbsp;are&nbsp;available.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;want&nbsp;all&nbsp;the&nbsp;data&nbsp;and&nbsp;thus&nbsp;are&nbsp;providing&nbsp;an&nbsp;empty&nbsp;string</span>&nbsp;
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
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="25998" href="/site/view/file/25998/1/OneNote.fromVBA.ListFirstNotebookSections.txt">OneNote.fromVBA.ListFirstNotebookSections.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="25999" href="/site/view/file/25999/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905452">OneNote Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
