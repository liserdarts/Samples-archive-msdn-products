# Visio 2010: Manipulate Shape Properties Using Visio.DropContainer
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Office 2010
* Visio 2010
## Topics
* Office 2010 101 code samples
* formatting shapes
## IsPublished
* True
## ModifiedDate
* 2011-08-05 04:22:32
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to manipulate the properties of shapes in a Microsoft Visio 2010 document by using the new Containers feature.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Visio 2010 makes it easy to add a visual boundary around shapes,&nbsp;using the new feature, Containers. The following code creates a new Visio document and adds two rectangles to the first page of the document. It then creates
 a Container object and adds the two rectangles to the Container. Finally, the code changes the default text of the container. To run this demo, open Visio 2010 and create a blank document. Open the VBA editor and paste this code into the existing ThisDocument
 module.&nbsp;With the cursor inside this method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub AddShapesToAContainer()
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
    
    ' Define and create two rectangles on the document's first page.
    Dim rectangle1 As Visio.Shape
    Dim rectangle2 As Visio.Shape
    Set rectangle1 = thePage.Drop(rectangleMaster, 2, 10)
    Set rectangle2 = thePage.Drop(rectangleMaster, 2, 9)
    
    ' Select the two rectangles.
    ' Note: If the document already contains other shapes,
    ' they will also be selected.
    ActiveWindow.SelectAll
    
    ' Access the new Containers collection by opening the built-in
    ' stencil file for containers. You can access these objects in the
    ' Visio user interface by selecting the Insert tab.
    Dim containersDoc As Visio.Document
    Set containersDoc = Application.Documents.OpenEx( _
        Application.GetBuiltInStencilFile(visBuiltInStencilContainers, _
        visMSUS), visOpenHidden)
    
    ' Finally, use the new DropContainer method to add
    ' the container specified by the containerToAdd to the page.
    ' As part of adding the container to the page, the code adds 
    ' the selected items to the container.
    ' The code ends by setting the container's text property and 
    ' then cleans up.
    Dim containerToAdd As String
    containerToAdd = &quot;Container 2&quot;
    Dim containerShape As Visio.Shape
    Set containerShape = thePage.DropContainer( _
        containersDoc.Masters.ItemU(containerToAdd), _
        Application.ActiveWindow.Selection)
    containerShape.Text = &quot;Two Rectangles&quot;
    
    containersDoc.Close
    ActiveWindow.DeselectAll
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;AddShapesToAContainer()&nbsp;
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Define&nbsp;and&nbsp;create&nbsp;two&nbsp;rectangles&nbsp;on&nbsp;the&nbsp;document's&nbsp;first&nbsp;page.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rectangle1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;rectangle2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rectangle1&nbsp;=&nbsp;thePage.Drop(rectangleMaster,&nbsp;<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__number">10</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;rectangle2&nbsp;=&nbsp;thePage.Drop(rectangleMaster,&nbsp;<span class="visualBasic__number">2</span>,&nbsp;<span class="visualBasic__number">9</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Select&nbsp;the&nbsp;two&nbsp;rectangles.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note:&nbsp;If&nbsp;the&nbsp;document&nbsp;already&nbsp;contains&nbsp;other&nbsp;shapes,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;they&nbsp;will&nbsp;also&nbsp;be&nbsp;selected.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveWindow.SelectAll&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Access&nbsp;the&nbsp;new&nbsp;Containers&nbsp;collection&nbsp;by&nbsp;opening&nbsp;the&nbsp;built-in</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;stencil&nbsp;file&nbsp;for&nbsp;containers.&nbsp;You&nbsp;can&nbsp;access&nbsp;these&nbsp;objects&nbsp;in&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Visio&nbsp;user&nbsp;interface&nbsp;by&nbsp;selecting&nbsp;the&nbsp;Insert&nbsp;tab.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;containersDoc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;containersDoc&nbsp;=&nbsp;Application.Documents.OpenEx(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application.GetBuiltInStencilFile(visBuiltInStencilContainers,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;visMSUS),&nbsp;visOpenHidden)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Finally,&nbsp;use&nbsp;the&nbsp;new&nbsp;DropContainer&nbsp;method&nbsp;to&nbsp;add</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;container&nbsp;specified&nbsp;by&nbsp;the&nbsp;containerToAdd&nbsp;to&nbsp;the&nbsp;page.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;As&nbsp;part&nbsp;of&nbsp;adding&nbsp;the&nbsp;container&nbsp;to&nbsp;the&nbsp;page,&nbsp;the&nbsp;code&nbsp;adds&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;selected&nbsp;items&nbsp;to&nbsp;the&nbsp;container.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;code&nbsp;ends&nbsp;by&nbsp;setting&nbsp;the&nbsp;container's&nbsp;text&nbsp;property&nbsp;and&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;then&nbsp;cleans&nbsp;up.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;containerToAdd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;containerToAdd&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Container&nbsp;2&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;containerShape&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;containerShape&nbsp;=&nbsp;thePage.DropContainer(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;containersDoc.Masters.ItemU(containerToAdd),&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application.ActiveWindow.Selection)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;containerShape.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Two&nbsp;Rectangles&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;containersDoc.Close&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ActiveWindow.DeselectAll&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26200" href="/site/view/file/26200/1/Visio.DropContainer.txt">Visio.DropContainer.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26201" href="/site/view/file/26201/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905478">Visio Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
