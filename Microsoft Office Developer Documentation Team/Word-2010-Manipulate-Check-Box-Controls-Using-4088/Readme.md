# Word 2010: Manipulate Check Box Controls Using Word.CheckBoxContentControl
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* check boxes
## IsPublished
* True
## ModifiedDate
* 2011-08-05 04:55:11
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to manipulate a check box content control and how to set the new
<strong>Checked </strong>property in Microsoft Word 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">How do you know what number to use for the CharacterNumber property? In Word 2010, select the Insert menu item. In the Symbols group, select Symbol, and choose More Symbols. In the Symbol dialog box, the display of symbols always
 starts with character 32. You can either add 31 to the position of the the character in the list of characters (counting from left to right), or you can set the from: dropdown list in the lower-right corner to ASCII (decimal) and then use the value you see
 in the Character code text box. Althought the Font parameter is optional, it's probably best that you always include it, to avoid any ambiguity.<br>
&nbsp;&nbsp;&nbsp;<br>
The following example sets the checked and unchecked symbols of a new check box content control. It also sets the new Checked property.<br>
&nbsp;&nbsp;&nbsp;<br>
Place this code into a module in a new document, place the cursor inside the procedure, and press F5 to run the demo. Switch to Microsoft Word 2010 to compare the behavior of the two check box content controls.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub CheckedSymbolDemo()
    
    Dim cc As ContentControl
    Set cc = ActiveDocument.ContentControls.Add(wdContentControlCheckBox)
    cc.Title = &quot;Fancy Check Box&quot;
    cc.SetCheckedSymbol CharacterNumber:=74, Font:=&quot;Wingdings&quot;
    cc.SetUncheckedSymbol CharacterNumber:=76, Font:=&quot;Wingdings&quot;
    cc.Checked = True
   
    ' Display a plain check box, too.
    Dim cc1 As ContentControl
    Set cc1 = ActiveDocument.ContentControls.Add(wdContentControlCheckBox)
    cc1.Title = &quot;Plain Old Check Box&quot;
    cc1.Checked = True
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;CheckedSymbolDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ContentControl&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cc&nbsp;=&nbsp;ActiveDocument.ContentControls.Add(wdContentControlCheckBox)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cc.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Fancy&nbsp;Check&nbsp;Box&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cc.SetCheckedSymbol&nbsp;CharacterNumber:=<span class="visualBasic__number">74</span>,&nbsp;Font:=<span class="visualBasic__string">&quot;Wingdings&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cc.SetUncheckedSymbol&nbsp;CharacterNumber:=<span class="visualBasic__number">76</span>,&nbsp;Font:=<span class="visualBasic__string">&quot;Wingdings&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cc.Checked&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Display&nbsp;a&nbsp;plain&nbsp;check&nbsp;box,&nbsp;too.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cc1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ContentControl&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;cc1&nbsp;=&nbsp;ActiveDocument.ContentControls.Add(wdContentControlCheckBox)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cc1.Title&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Plain&nbsp;Old&nbsp;Check&nbsp;Box&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;cc1.Checked&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26212" href="/site/view/file/26212/1/Word.CheckBoxContentControl.txt">Word.CheckBoxContentControl.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26213" href="/site/view/file/26213/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
