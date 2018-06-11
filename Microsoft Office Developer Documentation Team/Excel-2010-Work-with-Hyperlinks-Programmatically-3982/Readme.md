# Excel 2010: Work with Hyperlinks Programmatically
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* hyperlinks
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:27:11
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to work with hyperlinks in Microsoft Excel 2010 by using the
<strong>Range.ClearHyperlinks </strong>method.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">To investigate the code, in a new workbook, copy this entire sample into the Sheet1 module in the VBA editor. Arrange the VBA window and the Excel window side by side so you can see the code and its actions simultaneously. Place
 the cursor within the TestClearHyperlinks procedure, and press F8 to single step through the code.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestClearHyperlinks()
    ' Add a few hyperlinks, and then clear the hyperlinks in a particular range.
    Hyperlinks.Add Range(&quot;A1&quot;), _
        Address:=&quot;http://www.microsoft.com/&quot;, _
        TextToDisplay:=&quot;Microsoft&quot;
    Hyperlinks.Add Range(&quot;A2&quot;), _
        Address:=&quot;http://msdn.microsoft.com&quot;, _
        TextToDisplay:=&quot;Microsoft Developer Network&quot;
    Hyperlinks.Add Range(&quot;A3&quot;), _
        Address:=&quot;http://msdn.microsoft.com/office&quot;, _
        TextToDisplay:=&quot;Office Developer&quot;
    Dim rng As Range
    Set rng = Range(&quot;A:A&quot;)
    Columns(&quot;A&quot;).AutoFit
   
    ' Work with a subset of the range of links
    ' you created:
    Set rng = Range(&quot;A1:A2&quot;)
    ' Note that this command clears the hyperlinks, but
    ' leaves the cells formatted as if they were hyperlinks.
    rng.ClearHyperlinks
   
    ' In a real application, you might want to also clear the formatting
    ' for cells that had been hyperlinks that are now cleared:
    ' This code is optional, of course:
    rng.ClearFormats
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestClearHyperlinks()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;few&nbsp;hyperlinks,&nbsp;and&nbsp;then&nbsp;clear&nbsp;the&nbsp;hyperlinks&nbsp;in&nbsp;a&nbsp;particular&nbsp;range.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Hyperlinks.Add&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address:=<span class="visualBasic__string">&quot;http://www.microsoft.com/&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TextToDisplay:=<span class="visualBasic__string">&quot;Microsoft&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Hyperlinks.Add&nbsp;Range(<span class="visualBasic__string">&quot;A2&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address:=<span class="visualBasic__string">&quot;http://msdn.microsoft.com&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TextToDisplay:=<span class="visualBasic__string">&quot;Microsoft&nbsp;Developer&nbsp;Network&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Hyperlinks.Add&nbsp;Range(<span class="visualBasic__string">&quot;A3&quot;</span>),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address:=<span class="visualBasic__string">&quot;http://msdn.microsoft.com/office&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TextToDisplay:=<span class="visualBasic__string">&quot;Office&nbsp;Developer&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rng&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Range&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;A:A&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Columns(<span class="visualBasic__string">&quot;A&quot;</span>).AutoFit&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Work&nbsp;with&nbsp;a&nbsp;subset&nbsp;of&nbsp;the&nbsp;range&nbsp;of&nbsp;links</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;created:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rng&nbsp;=&nbsp;Range(<span class="visualBasic__string">&quot;A1:A2&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;this&nbsp;command&nbsp;clears&nbsp;the&nbsp;hyperlinks,&nbsp;but</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;leaves&nbsp;the&nbsp;cells&nbsp;formatted&nbsp;as&nbsp;if&nbsp;they&nbsp;were&nbsp;hyperlinks.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.ClearHyperlinks&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;a&nbsp;real&nbsp;application,&nbsp;you&nbsp;might&nbsp;want&nbsp;to&nbsp;also&nbsp;clear&nbsp;the&nbsp;formatting</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;for&nbsp;cells&nbsp;that&nbsp;had&nbsp;been&nbsp;hyperlinks&nbsp;that&nbsp;are&nbsp;now&nbsp;cleared:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;code&nbsp;is&nbsp;optional,&nbsp;of&nbsp;course:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;rng.ClearFormats&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25877" href="/site/view/file/25877/1/Excel.ClearHyperlinks.txt">Excel.ClearHyperlinks.txt</a>- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25878" href="/site/view/file/25878/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</em><em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
