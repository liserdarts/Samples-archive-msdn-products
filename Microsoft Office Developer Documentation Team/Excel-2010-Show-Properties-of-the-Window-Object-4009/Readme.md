# Excel 2010: Show Properties of the Window Object Using Excel.WindowProperties
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Custom Views
## IsPublished
* True
## ModifiedDate
* 2011-08-03 02:15:34
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows the <strong>ActiveSheetView </strong>
property, <strong>DisplayRuler </strong>property, <strong>DisplayWhiteSpace </strong>
property of the <strong>Window </strong>object in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Examine some of the Window properties to include ActiveSheetView, DisplayRuler, DisplayWhiteSpace.</span></p>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the WindowPropertiesDemo procedure, and then press F8 to single-step through the code. Arrange the VBA and Excel windows
 side by side so you can follow the instructions in the comments.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WindowPropertiesDemo()

    Range(&quot;A1&quot;, &quot;F10&quot;).Formula = &quot;=RANDBETWEEN(1, 100)&quot;
    Range(&quot;G1&quot;, &quot;G10&quot;).Value = 0
   
    Dim wnd As Window
    Set wnd = ActiveWindow
   
    ' Retrieve a reference to the active sheet view:
    Dim view As WorksheetView
    Set view = wnd.ActiveSheetView
   
    Dim doDisplayFormulas As Boolean
    Dim doDisplayGridLines As Boolean
    Dim doDisplayHeadings As Boolean
    Dim doDisplayZeros As Boolean
   
    ' Store away the original values.
    doDisplayFormulas = view.DisplayFormulas
    doDisplayGridLines = view.DisplayGridlines
    doDisplayHeadings = view.DisplayHeadings
    doDisplayZeros = view.DisplayZeros
   
    ' Change the settings for the active sheet view:
    view.DisplayFormulas = Not view.DisplayFormulas
    view.DisplayGridlines = Not view.DisplayGridlines
    view.DisplayHeadings = Not view.DisplayHeadings
    view.DisplayZeros = Not view.DisplayZeros
   
    ' Put things back the way they were originally.
    view.DisplayFormulas = doDisplayFormulas
    view.DisplayGridlines = doDisplayGridLines
    view.DisplayHeadings = doDisplayHeadings
    view.DisplayZeros = doDisplayZeros
   
    ' The DisplayRuler and DisplayWhiteSpace properties
    ' only have an effect in Page Layout view.
       
    ' Store away the current view.
    Dim currentView As XlWindowView
    currentView = wnd.view
   
    ' Make sure the current window is in Page Layout view.
    wnd.view = xlPageLayoutView
   
    ' Store away the current values:
    Dim currentDisplayRuler As Boolean
    Dim currentDisplayWhiteSpace As Boolean
   
    currentDisplayRuler = wnd.DisplayRuler
    currentDisplayWhiteSpace = wnd.DisplayWhitespace
   
    ' Set the properties to the opposite of their current state:
    wnd.DisplayRuler = Not wnd.DisplayRuler
    wnd.DisplayWhitespace = Not wnd.DisplayWhitespace
   
    ' Put things back:
    wnd.DisplayRuler = currentDisplayRuler
    wnd.DisplayWhitespace = currentDisplayWhiteSpace
    wnd.view = currentView
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WindowPropertiesDemo()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;A1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;F10&quot;</span>).Formula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;=RANDBETWEEN(1,&nbsp;100)&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Range(<span class="visualBasic__string">&quot;G1&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;G10&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;wnd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Window&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;wnd&nbsp;=&nbsp;ActiveWindow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;active&nbsp;sheet&nbsp;view:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;view&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;WorksheetView&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;view&nbsp;=&nbsp;wnd.ActiveSheetView&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doDisplayFormulas&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doDisplayGridLines&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doDisplayHeadings&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doDisplayZeros&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Store&nbsp;away&nbsp;the&nbsp;original&nbsp;values.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doDisplayFormulas&nbsp;=&nbsp;view.DisplayFormulas&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doDisplayGridLines&nbsp;=&nbsp;view.DisplayGridlines&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doDisplayHeadings&nbsp;=&nbsp;view.DisplayHeadings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;doDisplayZeros&nbsp;=&nbsp;view.DisplayZeros&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;the&nbsp;settings&nbsp;for&nbsp;the&nbsp;active&nbsp;sheet&nbsp;view:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayFormulas&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;view.DisplayFormulas&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayGridlines&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;view.DisplayGridlines&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayHeadings&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;view.DisplayHeadings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayZeros&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;view.DisplayZeros&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;things&nbsp;back&nbsp;the&nbsp;way&nbsp;they&nbsp;were&nbsp;originally.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayFormulas&nbsp;=&nbsp;doDisplayFormulas&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayGridlines&nbsp;=&nbsp;doDisplayGridLines&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayHeadings&nbsp;=&nbsp;doDisplayHeadings&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;view.DisplayZeros&nbsp;=&nbsp;doDisplayZeros&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;DisplayRuler&nbsp;and&nbsp;DisplayWhiteSpace&nbsp;properties</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;only&nbsp;have&nbsp;an&nbsp;effect&nbsp;in&nbsp;Page&nbsp;Layout&nbsp;view.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Store&nbsp;away&nbsp;the&nbsp;current&nbsp;view.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;currentView&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;XlWindowView&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;currentView&nbsp;=&nbsp;wnd.view&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;sure&nbsp;the&nbsp;current&nbsp;window&nbsp;is&nbsp;in&nbsp;Page&nbsp;Layout&nbsp;view.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wnd.view&nbsp;=&nbsp;xlPageLayoutView&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Store&nbsp;away&nbsp;the&nbsp;current&nbsp;values:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;currentDisplayRuler&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;currentDisplayWhiteSpace&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;currentDisplayRuler&nbsp;=&nbsp;wnd.DisplayRuler&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;currentDisplayWhiteSpace&nbsp;=&nbsp;wnd.DisplayWhitespace&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Set&nbsp;the&nbsp;properties&nbsp;to&nbsp;the&nbsp;opposite&nbsp;of&nbsp;their&nbsp;current&nbsp;state:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wnd.DisplayRuler&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;wnd.DisplayRuler&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wnd.DisplayWhitespace&nbsp;=&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;wnd.DisplayWhitespace&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;things&nbsp;back:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wnd.DisplayRuler&nbsp;=&nbsp;currentDisplayRuler&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wnd.DisplayWhitespace&nbsp;=&nbsp;currentDisplayWhiteSpace&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;wnd.view&nbsp;=&nbsp;currentView&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25947" href="/site/view/file/25947/1/Excel.WindowProperties.txt">Excel.WindowProperties.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25948" href="/site/view/file/25948/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
