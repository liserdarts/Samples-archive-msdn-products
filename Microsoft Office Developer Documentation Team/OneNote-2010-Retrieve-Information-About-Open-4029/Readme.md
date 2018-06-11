# OneNote 2010: Retrieve Information About Open OneNote 2010 Windows
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
* 2011-08-03 03:42:32
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the Windows and Window objects to retrieve information about the currently open Microsoft OneNote 2010 window(s) such as notebook, page, and section IDs.</span></p>
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
<p><span style="font-size:small">OneNote 2010 provides the ability to interact with the open OneNote User Interface. This procedure gets info about the currently open window(s) and outputs the information to the Immediate window of your VBA host. It then opens
 a new OneNote window.</span></p>
<p><span style="font-size:small">Paste all this code into a module, and place the cursor within the GetWindowInfoAndCurrentPageData procedure, and press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Public Sub GetWindowInfoAndCurrentPageData()
    ' Connect to OneNote 2010.
    ' OneNote will be started if it's not running.
    ' However, for this code to do something interesting,
    ' you should manually start OneNote first so at least
    ' one OneNote window is visible.
    Dim oneNote As OneNote14.Application
    Set oneNote = New OneNote14.Application
    
    Dim intCurrentWindowCount As Integer
    intCurrentWindowCount = 0
        
    ' Walk the list of current windows.
    Dim oneNoteWindow As OneNote14.Window
    For Each oneNoteWindow In oneNote.Windows
        intCurrentWindowCount = intCurrentWindowCount &#43; 1
        With oneNoteWindow
            Debug.Print &quot;Window &quot; &amp; intCurrentWindowCount
            Debug.Print &quot;  Active: &quot; &amp; .Active
            ' You can use the next of IDs to get more information
            ' using the GetHierarchy method.
            Debug.Print &quot;  Current Notebook ID: &quot; &amp; .CurrentNotebookId
            Debug.Print &quot;  Current Page ID: &quot; &amp; .CurrentPageId
            Debug.Print &quot;  Current Section ID: &quot; &amp; .CurrentSectionId
            Debug.Print &quot;  Current Section Group ID: &quot; &amp; .CurrentSectionGroupId
            Debug.Print &quot;  Docked Location: &quot; &amp; .DockedLocation
            Debug.Print &quot;  Full Page View: &quot; &amp; .FullPageView
            Debug.Print &quot;  Side Note: &quot; &amp; .SideNote
        End With
    Next
    
    If intCurrentWindowCount = 0 Then
        Debug.Print &quot;No visible OneNote windows.&quot;
    Else
        ' Get the Current Window.
        Set oneNoteWindow = oneNote.Windows.CurrentWindow
        
        ' If the current Window isn't a SideNote window, continue.
        If Not oneNoteWindow.SideNote Then
            ' Get the active page's xml.
            Dim pageXml As String
            oneNote.GetPageContent oneNoteWindow.CurrentPageId, pageXml, piBasic, xs2010
            
            Dim pageDoc As MSXML2.DOMDocument
            Set pageDoc = New MSXML2.DOMDocument
            
            Dim pageName As String
            If pageDoc.LoadXML(pageXml) Then
                Dim nodes As MSXML2.IXMLDOMNodeList
                Set nodes = pageDoc.DocumentElement.SelectNodes(&quot;//one:Page&quot;)
                        
                If Not nodes Is Nothing Then
                    Dim pageNode As MSXML2.IXMLDOMNode
                    Set pageNode = nodes(0)
        
                    pageName = GetAttributeValueFromNode(pageNode, &quot;name&quot;)
    
                    ' Output information about the current page.
                    Debug.Print &quot;Current Page Name: &quot; &amp; pageName
                    Debug.Print &quot;Current Page ID: &quot; &amp; oneNoteWindow.CurrentPageId
                    Debug.Print &quot;Current Page XML Below: &quot;
                    
                    Debug.Print pageXml
                End If
            End If
        End If
    End If
End Sub

Private Function GetAttributeValueFromNode(node As MSXML2.IXMLDOMNode, attributeName As String) As String
    If node.Attributes.getNamedItem(attributeName) Is Nothing Then
        GetAttributeValueFromNode = &quot;Not found.&quot;
    Else
        GetAttributeValueFromNode = node.Attributes.getNamedItem(attributeName).Text
    End If
End Function</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;GetWindowInfoAndCurrentPageData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Connect&nbsp;to&nbsp;OneNote&nbsp;2010.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;OneNote&nbsp;will&nbsp;be&nbsp;started&nbsp;if&nbsp;it's&nbsp;not&nbsp;running.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;However,&nbsp;for&nbsp;this&nbsp;code&nbsp;to&nbsp;do&nbsp;something&nbsp;interesting,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;should&nbsp;manually&nbsp;start&nbsp;OneNote&nbsp;first&nbsp;so&nbsp;at&nbsp;least</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;one&nbsp;OneNote&nbsp;window&nbsp;is&nbsp;visible.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;oneNote&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;oneNote&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;OneNote14.Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;intCurrentWindowCount&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;intCurrentWindowCount&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Walk&nbsp;the&nbsp;list&nbsp;of&nbsp;current&nbsp;windows.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;oneNoteWindow&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OneNote14.Window&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;oneNoteWindow&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;oneNote.Windows&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;intCurrentWindowCount&nbsp;=&nbsp;intCurrentWindowCount&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;oneNoteWindow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Window&nbsp;&quot;</span>&nbsp;&amp;&nbsp;intCurrentWindowCount&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Active:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.Active&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;use&nbsp;the&nbsp;next&nbsp;of&nbsp;IDs&nbsp;to&nbsp;get&nbsp;more&nbsp;information</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;using&nbsp;the&nbsp;GetHierarchy&nbsp;method.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Current&nbsp;Notebook&nbsp;ID:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.CurrentNotebookId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Current&nbsp;Page&nbsp;ID:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.CurrentPageId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Current&nbsp;Section&nbsp;ID:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.CurrentSectionId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Current&nbsp;Section&nbsp;Group&nbsp;ID:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.CurrentSectionGroupId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Docked&nbsp;Location:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.DockedLocation&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Full&nbsp;Page&nbsp;View:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.FullPageView&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;&nbsp;&nbsp;Side&nbsp;Note:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;.SideNote&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;intCurrentWindowCount&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;No&nbsp;visible&nbsp;OneNote&nbsp;windows.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;Current&nbsp;Window.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;oneNoteWindow&nbsp;=&nbsp;oneNote.Windows.CurrentWindow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;current&nbsp;Window&nbsp;isn't&nbsp;a&nbsp;SideNote&nbsp;window,&nbsp;continue.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;oneNoteWindow.SideNote&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Get&nbsp;the&nbsp;active&nbsp;page's&nbsp;xml.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pageXml&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oneNote.GetPageContent&nbsp;oneNoteWindow.CurrentPageId,&nbsp;pageXml,&nbsp;piBasic,&nbsp;xs2010&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pageDoc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.DOMDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pageDoc&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;MSXML2.DOMDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pageName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;pageDoc.LoadXML(pageXml)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;nodes&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNodeList&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;nodes&nbsp;=&nbsp;pageDoc.DocumentElement.SelectNodes(<span class="visualBasic__string">&quot;//one:Page&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;nodes&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;pageNode&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNode&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;pageNode&nbsp;=&nbsp;nodes(<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pageName&nbsp;=&nbsp;GetAttributeValueFromNode(pageNode,&nbsp;<span class="visualBasic__string">&quot;name&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Output&nbsp;information&nbsp;about&nbsp;the&nbsp;current&nbsp;page.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Current&nbsp;Page&nbsp;Name:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;pageName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Current&nbsp;Page&nbsp;ID:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;oneNoteWindow.CurrentPageId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Current&nbsp;Page&nbsp;XML&nbsp;Below:&nbsp;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;pageXml&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;GetAttributeValueFromNode(node&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;MSXML2.IXMLDOMNode,&nbsp;attributeName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;node.Attributes.getNamedItem(attributeName)&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetAttributeValueFromNode&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Not&nbsp;found.&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetAttributeValueFromNode&nbsp;=&nbsp;node.Attributes.getNamedItem(attributeName).Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="25996" href="/site/view/file/25996/1/OneNote.fromVBA.GetWindowInfoAndCurrentPageData.txt">OneNote.fromVBA.GetWindowInfoAndCurrentPageData.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="25997" href="/site/view/file/25997/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905452">OneNote Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
