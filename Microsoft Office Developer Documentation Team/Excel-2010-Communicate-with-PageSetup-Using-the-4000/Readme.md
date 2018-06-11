# Excel 2010: Communicate with PageSetup Using the Excel.PrintCommunication Method
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* page setup object
## IsPublished
* True
## ModifiedDate
* 2011-08-03 12:48:34
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>Application.PrintCommunication
</strong>property in Microsoft Excel 2010 to communicate with the printer <strong>
PageSetup </strong>object.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">The Application.PrintCommunication property toggles communication with the printer, so that you can execute multiple statements that interact with the PageSetup object faster. Setting the PrintCommunication property to False
 halts Excel from sending commands to the printer, and setting it to True sends all the cached commands at once. This property behaves much like the Application.ScreenUpdating property, which allows you to perform multiple actions on screen without needing
 to actually update the screen.</span></p>
<p><span style="font-size:small">To test out this code, in a new workbook, copy the entire sample into the Sheet1 module in the VBA editor. Place the cursor inside the TestPrintCommunication procedure, and press F5. The sample code doesn't perform any timings
 to verify that using the PrintCommunication property speeds up the code, but it does show how to make use of the property.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestPrintCommunication()
    On Error GoTo HandleErrors
    Application.PrintCommunication = False
    With Me.PageSetup
        .LeftMargin = Application.InchesToPoints(0.5)
        .RightMargin = Application.InchesToPoints(0.75)
        .TopMargin = Application.InchesToPoints(1.5)
        .BottomMargin = Application.InchesToPoints(1)
        .HeaderMargin = Application.InchesToPoints(0.5)
        .FooterMargin = Application.InchesToPoints(0.5)
    End With
    Application.PrintCommunication = True
   
ExitHere:
    Exit Sub
   
HandleErrors:
    ' If an error occurs, make sure you reset
    ' print communications.
    Application.PrintCommunication = True
    Resume ExitHere
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestPrintCommunication()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;HandleErrors&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Application.PrintCommunication&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;<span class="visualBasic__keyword">Me</span>.PageSetup&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.LeftMargin&nbsp;=&nbsp;Application.InchesToPoints(<span class="visualBasic__number">0.5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.RightMargin&nbsp;=&nbsp;Application.InchesToPoints(<span class="visualBasic__number">0.75</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.TopMargin&nbsp;=&nbsp;Application.InchesToPoints(<span class="visualBasic__number">1.5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.BottomMargin&nbsp;=&nbsp;Application.InchesToPoints(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.HeaderMargin&nbsp;=&nbsp;Application.InchesToPoints(<span class="visualBasic__number">0.5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FooterMargin&nbsp;=&nbsp;Application.InchesToPoints(<span class="visualBasic__number">0.5</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">With</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Application.PrintCommunication&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
ExitHere:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
HandleErrors:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;an&nbsp;error&nbsp;occurs,&nbsp;make&nbsp;sure&nbsp;you&nbsp;reset</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;print&nbsp;communications.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Application.PrintCommunication&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Resume</span>&nbsp;ExitHere&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><a id="25921" href="/Excel-2010-Communicate-cad0ff38/file/25921/1/Excel.PrintCommunication.txt">Excel.PrintCommunication.txt</a>&nbsp;- Download this sample only.</em></span>
</li><li><span style="font-size:small"><em><a id="25922" href="/Excel-2010-Communicate-cad0ff38/file/25922/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
