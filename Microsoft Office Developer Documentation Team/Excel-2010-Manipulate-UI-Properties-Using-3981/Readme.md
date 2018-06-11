# Excel 2010: Manipulate UI Properties Using Excel.ApplicationProperties
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* User Interface
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:26:30
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to manipulate various application properties that apply to the user interface in a Microsoft Excel 2010 workbook.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In Excel 2010, in a new workbook, copy all this code into the Sheet1 class module. Place the cursor in the TestApplicationProperties procedure, and then press F8 to single-step through the code. Arrange the VBA and Excel windows
 side by side so you can follow the instructions in the comments.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestApplicationProperties()
  Dim saveDisplayFormulaAutoComplete As Boolean
  Dim saveShowDevTools As Boolean
  Dim saveShowMenuFloaties As Boolean
  Dim saveShowSelectionFloaties As Boolean
  Dim saveDisplayDocumentInformationPanel As Boolean
  Dim saveEnableLivePreview As Boolean
  Dim saveFormulaBarHeight As Integer
 
  With Application
    ' Preserve the current state of things, so the code
    ' can put them back when done.
    saveDisplayFormulaAutoComplete = .DisplayFormulaAutoComplete
    saveShowDevTools = .ShowDevTools
    saveShowMenuFloaties = .ShowMenuFloaties
    saveShowSelectionFloaties = .ShowMenuFloaties
    saveDisplayDocumentInformationPanel = .DisplayDocumentInformationPanel
    saveEnableLivePreview = .EnableLivePreview
    saveWarnOnFunctionNameConflict = .WarnOnFunctionNameConflict
    saveFormulaBarHeight = .FormulaBarHeight
   
    ' Try out the various features added in Excel 2007:
    .DisplayFormulaAutoComplete = True
    ' In Excel, select cell A1, and then in the formula bar,
    ' start entering a formula like =RandBetween(1, 100), and
    ' watch the help you get in a formula drop-down list as you type.
    ' Execute the next line of code, and repeat the test: This time
    ' you won't see the dropdown:
    .DisplayFormulaAutoComplete = False
   
    .ShowDevTools = True
    ' In Excel, note that the Development Ribbon item
    ' now is visible. Execute the next line, and note that it's
    ' now hidden:
    .ShowDevTools = False
   
   
    .ShowMenuFloaties = False
    ' In Excel, right-click in a cell. Note the &quot;floatie&quot; menu
    ' that appears. Execute the next line, and verify that you
    ' no longer see that menu when you right-click. Note that
    ' the boolean value is the opposite of what you would expect.
    ' If the ShowMenuFloaties property is True, the floaties
    ' DO NOT appear; if False, they do.
    .ShowMenuFloaties = True
   
    .ShowSelectionFloaties = False
    ' Enter text into a cell, and then select a few characters. You
    ' see a floatie menu with formatting options. Then execute
    ' the next line, and verify that the same actions do not display
    ' the floatie menu. Note that the boolean value is the
    ' opposite of what you would expect.
    ' If the ShowSelectionFloaties property is True, the floaties
    ' DO NOT appear; if False, they do.
    .ShowSelectionFloaties = True
   
    .DisplayDocumentInformationPanel = True
    ' After executing the previous line of code, you see the
    ' document information panel. Execute the next line to hide this panel:
    .DisplayDocumentInformationPanel = False
   
    .EnableLivePreview = False
    ' After executing the previous line of code, enter some text
    ' into a cell, and select a font. As you scroll through the font list,
    ' the formatting won't update as you select fonts. When you select
    ' a font and dismiss the menu, the font will change in the cell. Execute
    ' the following line, and try again--now the font updates as you
    ' scroll through the font menu. The same concept applies to all
    ' other formatting changes.
    .EnableLivePreview = True
   
    .FormulaBarHeight = 2
    ' After executing the previous line of code, the formula bar
    ' height should be changed. Execute the following line to
    ' set it to 1, the default height.
    .FormulaBarHeight = 1
   
   
    ' Put things back the way they were originally.
    .DisplayFormulaAutoComplete = saveDisplayFormulaAutoComplete
    .ShowDevTools = saveShowDevTools
    .ShowMenuFloaties = saveShowMenuFloaties
    .ShowMenuFloaties = saveShowSelectionFloaties
    .DisplayDocumentInformationPanel = saveDisplayDocumentInformationPanel
    .EnableLivePreview = saveEnableLivePreview
    .FormulaBarHeight = saveFormulaBarHeight
  End With
End Sub
  
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestApplicationProperties()&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveDisplayFormulaAutoComplete&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveShowDevTools&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveShowMenuFloaties&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveShowSelectionFloaties&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveDisplayDocumentInformationPanel&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveEnableLivePreview&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;saveFormulaBarHeight&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Preserve&nbsp;the&nbsp;current&nbsp;state&nbsp;of&nbsp;things,&nbsp;so&nbsp;the&nbsp;code</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;can&nbsp;put&nbsp;them&nbsp;back&nbsp;when&nbsp;done.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveDisplayFormulaAutoComplete&nbsp;=&nbsp;.DisplayFormulaAutoComplete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveShowDevTools&nbsp;=&nbsp;.ShowDevTools&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveShowMenuFloaties&nbsp;=&nbsp;.ShowMenuFloaties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveShowSelectionFloaties&nbsp;=&nbsp;.ShowMenuFloaties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveDisplayDocumentInformationPanel&nbsp;=&nbsp;.DisplayDocumentInformationPanel&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveEnableLivePreview&nbsp;=&nbsp;.EnableLivePreview&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveWarnOnFunctionNameConflict&nbsp;=&nbsp;.WarnOnFunctionNameConflict&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;saveFormulaBarHeight&nbsp;=&nbsp;.FormulaBarHeight&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Try&nbsp;out&nbsp;the&nbsp;various&nbsp;features&nbsp;added&nbsp;in&nbsp;Excel&nbsp;2007:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DisplayFormulaAutoComplete&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;Excel,&nbsp;select&nbsp;cell&nbsp;A1,&nbsp;and&nbsp;then&nbsp;in&nbsp;the&nbsp;formula&nbsp;bar,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;start&nbsp;entering&nbsp;a&nbsp;formula&nbsp;like&nbsp;=RandBetween(1,&nbsp;100),&nbsp;and</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;watch&nbsp;the&nbsp;help&nbsp;you&nbsp;get&nbsp;in&nbsp;a&nbsp;formula&nbsp;drop-down&nbsp;list&nbsp;as&nbsp;you&nbsp;type.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Execute&nbsp;the&nbsp;next&nbsp;line&nbsp;of&nbsp;code,&nbsp;and&nbsp;repeat&nbsp;the&nbsp;test:&nbsp;This&nbsp;time</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;won't&nbsp;see&nbsp;the&nbsp;dropdown:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DisplayFormulaAutoComplete&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowDevTools&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;Excel,&nbsp;note&nbsp;that&nbsp;the&nbsp;Development&nbsp;Ribbon&nbsp;item</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;now&nbsp;is&nbsp;visible.&nbsp;Execute&nbsp;the&nbsp;next&nbsp;line,&nbsp;and&nbsp;note&nbsp;that&nbsp;it's</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;now&nbsp;hidden:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowDevTools&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowMenuFloaties&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;In&nbsp;Excel,&nbsp;right-click&nbsp;in&nbsp;a&nbsp;cell.&nbsp;Note&nbsp;the&nbsp;&quot;floatie&quot;&nbsp;menu</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;that&nbsp;appears.&nbsp;Execute&nbsp;the&nbsp;next&nbsp;line,&nbsp;and&nbsp;verify&nbsp;that&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;no&nbsp;longer&nbsp;see&nbsp;that&nbsp;menu&nbsp;when&nbsp;you&nbsp;right-click.&nbsp;Note&nbsp;that</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;boolean&nbsp;value&nbsp;is&nbsp;the&nbsp;opposite&nbsp;of&nbsp;what&nbsp;you&nbsp;would&nbsp;expect.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;ShowMenuFloaties&nbsp;property&nbsp;is&nbsp;True,&nbsp;the&nbsp;floaties</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;DO&nbsp;NOT&nbsp;appear;&nbsp;if&nbsp;False,&nbsp;they&nbsp;do.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowMenuFloaties&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowSelectionFloaties&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Enter&nbsp;text&nbsp;into&nbsp;a&nbsp;cell,&nbsp;and&nbsp;then&nbsp;select&nbsp;a&nbsp;few&nbsp;characters.&nbsp;You</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;see&nbsp;a&nbsp;floatie&nbsp;menu&nbsp;with&nbsp;formatting&nbsp;options.&nbsp;Then&nbsp;execute</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;next&nbsp;line,&nbsp;and&nbsp;verify&nbsp;that&nbsp;the&nbsp;same&nbsp;actions&nbsp;do&nbsp;not&nbsp;display</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;floatie&nbsp;menu.&nbsp;Note&nbsp;that&nbsp;the&nbsp;boolean&nbsp;value&nbsp;is&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;opposite&nbsp;of&nbsp;what&nbsp;you&nbsp;would&nbsp;expect.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;ShowSelectionFloaties&nbsp;property&nbsp;is&nbsp;True,&nbsp;the&nbsp;floaties</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;DO&nbsp;NOT&nbsp;appear;&nbsp;if&nbsp;False,&nbsp;they&nbsp;do.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowSelectionFloaties&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DisplayDocumentInformationPanel&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;After&nbsp;executing&nbsp;the&nbsp;previous&nbsp;line&nbsp;of&nbsp;code,&nbsp;you&nbsp;see&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;document&nbsp;information&nbsp;panel.&nbsp;Execute&nbsp;the&nbsp;next&nbsp;line&nbsp;to&nbsp;hide&nbsp;this&nbsp;panel:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DisplayDocumentInformationPanel&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.EnableLivePreview&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;After&nbsp;executing&nbsp;the&nbsp;previous&nbsp;line&nbsp;of&nbsp;code,&nbsp;enter&nbsp;some&nbsp;text</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;into&nbsp;a&nbsp;cell,&nbsp;and&nbsp;select&nbsp;a&nbsp;font.&nbsp;As&nbsp;you&nbsp;scroll&nbsp;through&nbsp;the&nbsp;font&nbsp;list,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;formatting&nbsp;won't&nbsp;update&nbsp;as&nbsp;you&nbsp;select&nbsp;fonts.&nbsp;When&nbsp;you&nbsp;select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;font&nbsp;and&nbsp;dismiss&nbsp;the&nbsp;menu,&nbsp;the&nbsp;font&nbsp;will&nbsp;change&nbsp;in&nbsp;the&nbsp;cell.&nbsp;Execute</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;following&nbsp;line,&nbsp;and&nbsp;try&nbsp;again--now&nbsp;the&nbsp;font&nbsp;updates&nbsp;as&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;scroll&nbsp;through&nbsp;the&nbsp;font&nbsp;menu.&nbsp;The&nbsp;same&nbsp;concept&nbsp;applies&nbsp;to&nbsp;all</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;other&nbsp;formatting&nbsp;changes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.EnableLivePreview&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.FormulaBarHeight&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;After&nbsp;executing&nbsp;the&nbsp;previous&nbsp;line&nbsp;of&nbsp;code,&nbsp;the&nbsp;formula&nbsp;bar</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;height&nbsp;should&nbsp;be&nbsp;changed.&nbsp;Execute&nbsp;the&nbsp;following&nbsp;line&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;set&nbsp;it&nbsp;to&nbsp;1,&nbsp;the&nbsp;default&nbsp;height.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.FormulaBarHeight&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;things&nbsp;back&nbsp;the&nbsp;way&nbsp;they&nbsp;were&nbsp;originally.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DisplayFormulaAutoComplete&nbsp;=&nbsp;saveDisplayFormulaAutoComplete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowDevTools&nbsp;=&nbsp;saveShowDevTools&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowMenuFloaties&nbsp;=&nbsp;saveShowMenuFloaties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.ShowMenuFloaties&nbsp;=&nbsp;saveShowSelectionFloaties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.DisplayDocumentInformationPanel&nbsp;=&nbsp;saveDisplayDocumentInformationPanel&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.EnableLivePreview&nbsp;=&nbsp;saveEnableLivePreview&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;.FormulaBarHeight&nbsp;=&nbsp;saveFormulaBarHeight&nbsp;
&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25875" href="/site/view/file/25875/1/Excel.ApplicationProperties.txt">Excel.ApplicationProperties.txt</a>- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25876" href="/site/view/file/25876/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>- Download all the samples.</em><em></em>
</span></li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
