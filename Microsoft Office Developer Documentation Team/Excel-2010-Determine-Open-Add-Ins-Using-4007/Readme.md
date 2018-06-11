# Excel 2010: Determine Open Add-Ins Using Excel.TestAddIn.IsOpen
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Excel 2010
* Office 2010
## Topics
* Microsoft Office Add-ins
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-03 02:05:56
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to determine programmatically if an add-in is open in Microsoft Excel 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">In a new workbook, in the VBA editor, create a new module and past in all this code. Place the cursor inside the TestAddIns procedure, and press F5 to run the code. View the results in the Immediate window.</span></p>
<p><span style="font-size:small">Use the AddIn.IsOpen property to determine if an add-in is currently open. To test this property, run the code in the TestAddIns procedure. This code first loops through all the add-ins, indicating whether they're open. Most
 will not be. The code also installs the SOLVER.XLAM add-in. Run the code again, and now you'll see that the Solver add-in is open this time.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestAddIns()
    Dim ai As AddIn
    For Each ai In Application.AddIns
        Debug.Print ai.Name &amp; &quot; &quot; &amp; ai.IsOpen
        ' Install the SOLVER add-in.
        If ai.Name = &quot;SOLVER.XLAM&quot; Then
            ai.Installed = True
        End If
    Next ai
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestAddIns()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ai&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;AddIn&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;ai&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;Application.AddIns&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;ai.Name&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;&quot;</span>&nbsp;&amp;&nbsp;ai.IsOpen&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Install&nbsp;the&nbsp;SOLVER&nbsp;add-in.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;ai.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SOLVER.XLAM&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ai.Installed&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;ai&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><span style="font-size:small"><a id="25942" href="/site/view/file/25942/1/Excel.TestAddIn.IsOpen.txt">Excel.TestAddIn.IsOpen.txt</a>&nbsp;- Download this sample only.</span></em>
</li><li><em><span style="font-size:small"><a id="25943" href="/site/view/file/25943/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</span><em></em></em>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905411">Excel Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
