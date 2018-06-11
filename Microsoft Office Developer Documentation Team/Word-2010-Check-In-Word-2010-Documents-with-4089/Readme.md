# Word 2010: Check-In Word 2010 Documents with Versioning on SharePoint Servers
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Word 2010
* Office 2010
## Topics
* Versioning
* Office 2010 101 code samples
## IsPublished
* True
## ModifiedDate
* 2011-08-05 04:59:58
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample demonstrates Microsoft Word 2010 document check-in with versioning on a server running SharePoint Server 2010.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">To demonstrate this code, create a new document on a Sharepoint 2010 server. Create a module in the document containing the following code. Place the cursor within the procedure and press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub TestCheckInWithVersion()
    ActiveDocument.Content.InsertAfter &quot;Here is a change!&quot;
   
    ' The following code includes parameter names
    ' to make it clearer what's going on. All the parameters
    ' are optional, and you don't need to use named
    ' parameters.
    ActiveDocument.CheckInWithVersion _
        SaveChanges:=True, _
        Comments:=&quot;I made some changes to demonstrate checking in.&quot;, _
        MakePublic:=True, _
        VersionType:=WdCheckInVersionType.wdCheckInMajorVersion
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;TestCheckInWithVersion()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.Content.InsertAfter&nbsp;<span class="visualBasic__string">&quot;Here&nbsp;is&nbsp;a&nbsp;change!&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;following&nbsp;code&nbsp;includes&nbsp;parameter&nbsp;names</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;make&nbsp;it&nbsp;clearer&nbsp;what's&nbsp;going&nbsp;on.&nbsp;All&nbsp;the&nbsp;parameters</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;are&nbsp;optional,&nbsp;and&nbsp;you&nbsp;don't&nbsp;need&nbsp;to&nbsp;use&nbsp;named</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;parameters.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveDocument.CheckInWithVersion&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SaveChanges:=<span class="visualBasic__keyword">True</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Comments:=<span class="visualBasic__string">&quot;I&nbsp;made&nbsp;some&nbsp;changes&nbsp;to&nbsp;demonstrate&nbsp;checking&nbsp;in.&quot;</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MakePublic:=<span class="visualBasic__keyword">True</span>,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VersionType:=WdCheckInVersionType.wdCheckInMajorVersion&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26214" href="/site/view/file/26214/1/Word.CheckInWithVersion.txt">Word.CheckInWithVersion.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26215" href="/site/view/file/26215/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905482">Word Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
