# Word 2010: List Combo Box Content Information Using Word.ContentControlLists
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* ComboBox
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 05:07:08
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to list information programmatically about each entry of a combo box content control containing a set of colors by using the
<strong>ContentControlListEntries </strong>method in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Create a new Word document, and in the VBA editor, copy the following code into the ThisDocument class. Place the cursor inside the SetPlaceHolderText procedure, and press F5 to run the code. Look in the Immediate window for
 the results. Examine the document, as well, which will contain a combo box content control containing a list of colors.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub SetPlaceholderText()
    ' Delete all the current contents of the document.
    Range.Delete
   
    ' Create a combo box content control:
    Dim ccList As ContentControl
    Set ccList = ContentControls.Add(wdContentControlComboBox)
    
    ccList.Title = &quot;Select a Color&quot;
    ccList.SetPlaceholderText Text:=&quot;Please select the color&quot;
    
    ' Add items to the list:
    ccList.DropdownListEntries.Add &quot;Red&quot;, RGB(255, 0, 0)
    ccList.DropdownListEntries.Add &quot;Orange&quot;, RGB(255, 165, 0)
    ccList.DropdownListEntries.Add &quot;Yellow&quot;, RGB(255, 255, 0)
    ccList.DropdownListEntries.Add &quot;Green&quot;, RGB(0, 255, 0)
    ccList.DropdownListEntries.Add &quot;Blue&quot;, RGB(0, 0, 255)
    ccList.DropdownListEntries.Add &quot;Indigo&quot;, RGB(75, 0, 130)
    ccList.DropdownListEntries.Add &quot;Violet&quot;, RGB(238, 130, 238)

    ' Display the contents of each item:
    Dim ccListEntry As ContentControlListEntry
    For Each ccListEntry In ccList.DropdownListEntries
        Debug.Print ccListEntry.Text, Hex$(ccListEntry.Value)
    Next ccListEntry

    ' Select the first item in the list.
    ccList.DropdownListEntries(1).Select
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;SetPlaceholderText()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Delete&nbsp;all&nbsp;the&nbsp;current&nbsp;contents&nbsp;of&nbsp;the&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range.Delete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;combo&nbsp;box&nbsp;content&nbsp;control:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ccList&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ContentControl&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;ccList&nbsp;=&nbsp;ContentControls.Add(wdContentControlComboBox)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Select&nbsp;a&nbsp;Color&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.SetPlaceholderText&nbsp;Text:=<span class="visualBasic__string">&quot;Please&nbsp;select&nbsp;the&nbsp;color&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;items&nbsp;to&nbsp;the&nbsp;list:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Red&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Orange&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">165</span>,&nbsp;<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Yellow&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Green&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">255</span>,&nbsp;<span class="visualBasic__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Blue&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">255</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Indigo&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">75</span>,&nbsp;<span class="visualBasic__number">0</span>,&nbsp;<span class="visualBasic__number">130</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries.Add&nbsp;<span class="visualBasic__string">&quot;Violet&quot;</span>,&nbsp;RGB(<span class="visualBasic__number">238</span>,&nbsp;<span class="visualBasic__number">130</span>,&nbsp;<span class="visualBasic__number">238</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Display&nbsp;the&nbsp;contents&nbsp;of&nbsp;each&nbsp;item:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ccListEntry&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ContentControlListEntry&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;ccListEntry&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;ccList.DropdownListEntries&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;ccListEntry.Text,&nbsp;Hex$(ccListEntry.Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;ccListEntry&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Select&nbsp;the&nbsp;first&nbsp;item&nbsp;in&nbsp;the&nbsp;list.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ccList.DropdownListEntries(<span class="visualBasic__number">1</span>).<span class="visualBasic__keyword">Select</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26218" href="/site/view/file/26218/1/Word.ContentControlLists.txt">Word.ContentControlLists.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26219" href="/site/view/file/26219/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
