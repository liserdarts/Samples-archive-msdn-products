# Visio 2010: Bind Two Shapes Together Using Visio.Page.DropCallout
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
* Visio 2010
## Topics
* Office 2010 101 code samples
* shapes
## IsPublished
* True
## ModifiedDate
* 2011-08-05 04:26:54
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>DropCallout
</strong>method in a Microsoft Visio 2010 document to bind two shapes together so that moving one shape moves the other.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Visio 2010 allows you to create a callout shape and bind it to another shape on a page. Once you do this, the callout stays bound to the shape if you move the parent shape on the document.<br>
<br>
To run this demo, open Visio 2010 and create a blank document. Open the VBA editor and paste this code into the existing&nbsp; ThisDocument module. With the cursor inside the AddCallOut method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub AddCallOut()
    ' Access objects on the current document.
    Dim doc As Visio.Document
    Set doc = Application.ActiveDocument
    Dim thePage As Visio.Page
    Set thePage = doc.Pages(1)
    
    ' Open the built-in Basic Shapes stencil.
    ' If the stencil is already open, the code
    ' activates the stencil.
    Dim stencilName As String
    stencilName = &quot;Basic Shapes (US units).vss&quot;
    Dim stencil As Visio.Document
    Set stencil = Application.Documents.OpenEx(stencilName, visOpenDocked)
    
    ' Open the built-in Callouts Shapes stencil.
    ' If the stencil is already open, the code
    ' activates the stencil.
    ' This code opens the stencil hidden unlike
    ' the basic shapes stencil above.
    Dim calloutsFileName As String
    calloutsFileName = Application.GetBuiltInStencilFile(visBuiltInStencilCallouts, visMSUS)
    Dim callouts As Visio.Document
    Set callouts = Application.Documents.OpenEx(calloutsFileName, visOpenHidden)
    
    ' Create a triangle master shape from the stencil.
    Dim triangleMaster As Visio.Master
    Set triangleMaster = stencil.Masters(&quot;Triangle&quot;)
    
    ' Define and create a triangle.
    Dim triangle1 As Visio.Shape
    Set triangle1 = thePage.Drop(triangleMaster, 2, 2)
    ' Define and create a 'cloud callout' master.
    Dim callout As Visio.Master
    Set callout = callouts.Masters(&quot;cloud callout&quot;)
    
    ' Use the new DropCallout method to add the cloud callout
    ' to the page and link it to the triangle.
    Dim results As Visio.Shape
    Set results = thePage.DropCallout(callout, triangle1)
    
    ' Change the Text of the callout.
    results.Text = &quot;Callout for a Triangle&quot;
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;AddCallOut()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Access&nbsp;objects&nbsp;on&nbsp;the&nbsp;current&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc&nbsp;=&nbsp;Application.ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;thePage&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Page&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;thePage&nbsp;=&nbsp;doc.Pages(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Open&nbsp;the&nbsp;built-in&nbsp;Basic&nbsp;Shapes&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;stencil&nbsp;is&nbsp;already&nbsp;open,&nbsp;the&nbsp;code</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;activates&nbsp;the&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;stencilName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;stencilName&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Basic&nbsp;Shapes&nbsp;(US&nbsp;units).vss&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;stencil&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;stencil&nbsp;=&nbsp;Application.Documents.OpenEx(stencilName,&nbsp;visOpenDocked)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Open&nbsp;the&nbsp;built-in&nbsp;Callouts&nbsp;Shapes&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;stencil&nbsp;is&nbsp;already&nbsp;open,&nbsp;the&nbsp;code</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;activates&nbsp;the&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;This&nbsp;code&nbsp;opens&nbsp;the&nbsp;stencil&nbsp;hidden&nbsp;unlike</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;basic&nbsp;shapes&nbsp;stencil&nbsp;above.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;calloutsFileName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;calloutsFileName&nbsp;=&nbsp;Application.GetBuiltInStencilFile(visBuiltInStencilCallouts,&nbsp;visMSUS)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;callouts&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;callouts&nbsp;=&nbsp;Application.Documents.OpenEx(calloutsFileName,&nbsp;visOpenHidden)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;triangle&nbsp;master&nbsp;shape&nbsp;from&nbsp;the&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;triangleMaster&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Master&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;triangleMaster&nbsp;=&nbsp;stencil.Masters(<span class="visualBasic__string">&quot;Triangle&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Define&nbsp;and&nbsp;create&nbsp;a&nbsp;triangle.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;triangle1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;triangle1&nbsp;=&nbsp;thePage.Drop(triangleMaster,&nbsp;<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Define&nbsp;and&nbsp;create&nbsp;a&nbsp;'cloud&nbsp;callout'&nbsp;master.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;callout&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Master&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;callout&nbsp;=&nbsp;callouts.Masters(<span class="visualBasic__string">&quot;cloud&nbsp;callout&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;the&nbsp;new&nbsp;DropCallout&nbsp;method&nbsp;to&nbsp;add&nbsp;the&nbsp;cloud&nbsp;callout</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;to&nbsp;the&nbsp;page&nbsp;and&nbsp;link&nbsp;it&nbsp;to&nbsp;the&nbsp;triangle.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;results&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;results&nbsp;=&nbsp;thePage.DropCallout(callout,&nbsp;triangle1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Change&nbsp;the&nbsp;Text&nbsp;of&nbsp;the&nbsp;callout.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;results.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Callout&nbsp;for&nbsp;a&nbsp;Triangle&quot;</span>&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26202" href="/site/view/file/26202/1/Visio.Page.DropCallout.txt">Visio.Page.DropCallout.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26203" href="/site/view/file/26203/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905478">Visio Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
