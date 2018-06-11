# Visio 2010: Manipulate Connected Shapes Using Visio.Page.DropConnected
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
* 2011-08-05 04:30:19
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>DropConnected
</strong>method to manipulate connected shapes within a Microsoft Visio 2010 document.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Visio 2010 allows you to create connected shapes as you drop them on to a page using the DropConnected method. The following code creates a new Visio document and adds three shapes--a rectangle, circle, and triangle--to the
 first page of the document. As it adds the circle and the triangle, it creates them as connected items to the previously added shape using the Page's DropConnected method.<br>
<br>
To run this demo, open Visio 2010 and create a blank document. Open the VBA editor and paste this code into the existing&nbsp; ThisDocument module. With the cursor inside the ConnectShapesWithDropConnected method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub ConnectShapesWithDropConnected()
    ' Access objects on the current document.
    Dim doc As Visio.Document
    Set doc = Application.ActiveDocument
    Dim thePage As Visio.Page
    Set thePage = doc.Pages(1)
    
    ' Open the built-in Basic Shapes stencil.
    ' If the stencil is already open, the code
    ' activates the stencil.
    Dim stencil As Visio.Document
    Dim stencilName As String
    stencilName = &quot;Basic Shapes (US units).vss&quot;
    Set stencil = Application.Documents.OpenEx(stencilName, visOpenDocked)
    
    ' Create a rectangle master shape from the stencil.
    Dim rectangleMaster As Visio.Master
    Set rectangleMaster = stencil.Masters(&quot;Rectangle&quot;)
    ' Create a circle master sharpe from the stencil
    Dim circleMaster As Visio.Master
    Set circleMaster = stencil.Masters(&quot;Circle&quot;)
    ' Create a triangle master sharpe from the stencil
    Dim triangleMaster As Visio.Master
    Set triangleMaster = stencil.Masters(&quot;Triangle&quot;)
    
    ' Define and create three shapes on the document's first page.
    Dim rectangle1 As Visio.Shape
    Dim circle1 As Visio.Shape
    Dim triangle1 As Visio.Shape
    
    ' Add the first object with the Drop method.
    Set rectangle1 = thePage.Drop(rectangleMaster, 2, 10)
    ' Add the second and third objects using the new DropConnected method.
    Set circle1 = thePage.DropConnected(circleMaster, rectangle1, visAutoConnectDirRight)
    Set triangle1 = thePage.DropConnected(triangleMaster, circle1, visAutoConnectDirDown)
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;ConnectShapesWithDropConnected()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Access&nbsp;objects&nbsp;on&nbsp;the&nbsp;current&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc&nbsp;=&nbsp;Application.ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;thePage&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Page&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;thePage&nbsp;=&nbsp;doc.Pages(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Open&nbsp;the&nbsp;built-in&nbsp;Basic&nbsp;Shapes&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;If&nbsp;the&nbsp;stencil&nbsp;is&nbsp;already&nbsp;open,&nbsp;the&nbsp;code</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;activates&nbsp;the&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;stencil&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;stencilName&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;stencilName&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Basic&nbsp;Shapes&nbsp;(US&nbsp;units).vss&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;stencil&nbsp;=&nbsp;Application.Documents.OpenEx(stencilName,&nbsp;visOpenDocked)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;rectangle&nbsp;master&nbsp;shape&nbsp;from&nbsp;the&nbsp;stencil.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rectangleMaster&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Master&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rectangleMaster&nbsp;=&nbsp;stencil.Masters(<span class="visualBasic__string">&quot;Rectangle&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;circle&nbsp;master&nbsp;sharpe&nbsp;from&nbsp;the&nbsp;stencil</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;circleMaster&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Master&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;circleMaster&nbsp;=&nbsp;stencil.Masters(<span class="visualBasic__string">&quot;Circle&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;triangle&nbsp;master&nbsp;sharpe&nbsp;from&nbsp;the&nbsp;stencil</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;triangleMaster&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Master&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;triangleMaster&nbsp;=&nbsp;stencil.Masters(<span class="visualBasic__string">&quot;Triangle&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Define&nbsp;and&nbsp;create&nbsp;three&nbsp;shapes&nbsp;on&nbsp;the&nbsp;document's&nbsp;first&nbsp;page.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rectangle1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;circle1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;triangle1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;the&nbsp;first&nbsp;object&nbsp;with&nbsp;the&nbsp;Drop&nbsp;method.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rectangle1&nbsp;=&nbsp;thePage.Drop(rectangleMaster,&nbsp;<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__number">10</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;the&nbsp;second&nbsp;and&nbsp;third&nbsp;objects&nbsp;using&nbsp;the&nbsp;new&nbsp;DropConnected&nbsp;method.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;circle1&nbsp;=&nbsp;thePage.DropConnected(circleMaster,&nbsp;rectangle1,&nbsp;visAutoConnectDirRight)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;triangle1&nbsp;=&nbsp;thePage.DropConnected(triangleMaster,&nbsp;circle1,&nbsp;visAutoConnectDirDown)&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26204" href="/site/view/file/26204/1/Visio.Page.DropConnected.txt">Visio.Page.DropConnected.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26205" href="/site/view/file/26205/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905478">Visio Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
