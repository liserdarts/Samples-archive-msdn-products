# Visio 2010: Add Containers to Visio Documents Using Visio.ContainerProperties
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
* 2011-08-05 04:14:55
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use the <strong>ContainerProperties
</strong>object by adding a container to a Microsoft Visio 2010 document and then retrieving and setting some of its properties.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Visio 2010 makes it easy to add a visual boundary around shapes, using the new feature, Containers. The following code creates a Container object and adds it to the page. The code then works with the ContainerProperties object.<br>
<br>
To run this demo, open Visio 2010 and create a blank document. Open the VBA editor and paste this code into the existing&nbsp; ThisDocument module. With the cursor inside the WorkWithContainerProps method, press F5.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WorkWithContainerProps()
    ' Access objects on the current document.
    Dim doc As Visio.Document
    Set doc = Application.ActiveDocument
    Dim thePage As Visio.Page
    Set thePage = doc.Pages(1)
    
    ' From VBA, access the new Containers collection by opening the built-in
    ' stencil file for Containers. 
    ' You can access these objects in the
    ' Visio user interface by selecting the Insert tab.
    Dim containersDoc As Visio.Document
    Set containersDoc = Application.Documents.OpenEx( _
        Application.GetBuiltInStencilFile(visBuiltInStencilContainers, _
        visMSUS), visOpenHidden)
    
    ' Use the new DropContainer method to add
    ' the container specified by the containerToAdd text to the page.
    Dim containerToAdd As String
    containerToAdd = &quot;Container 1&quot;
    Dim containerShape As Visio.Shape
    Set containerShape = thePage.DropContainer( _
        containersDoc.Masters.ItemU(containerToAdd), Nothing)
    containersDoc.Close
    
    ' Once you have a container on the page, you
    ' access its ContainerProperties object.
    Dim props As ContainerProperties
    Set props = containerShape.ContainerProperties
    
    ' Once you have the ContainerProperties object,
    ' you can read properties.
    MsgBox &quot;ContainerStyle=&quot; &amp; props.ContainerStyle
    
    ' You can also change properties.
    props.ContainerStyle = 2
    MsgBox &quot;ContainerStyle=&quot; &amp; props.ContainerStyle
End Sub
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithContainerProps()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Access&nbsp;objects&nbsp;on&nbsp;the&nbsp;current&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;doc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;doc&nbsp;=&nbsp;Application.ActiveDocument&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;thePage&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Page&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;thePage&nbsp;=&nbsp;doc.Pages(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;From&nbsp;VBA,&nbsp;access&nbsp;the&nbsp;new&nbsp;Containers&nbsp;collection&nbsp;by&nbsp;opening&nbsp;the&nbsp;built-in</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;stencil&nbsp;file&nbsp;for&nbsp;Containers.&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;access&nbsp;these&nbsp;objects&nbsp;in&nbsp;the</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Visio&nbsp;user&nbsp;interface&nbsp;by&nbsp;selecting&nbsp;the&nbsp;Insert&nbsp;tab.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;containersDoc&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Document&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;containersDoc&nbsp;=&nbsp;Application.Documents.OpenEx(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application.GetBuiltInStencilFile(visBuiltInStencilContainers,&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;visMSUS),&nbsp;visOpenHidden)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Use&nbsp;the&nbsp;new&nbsp;DropContainer&nbsp;method&nbsp;to&nbsp;add</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;container&nbsp;specified&nbsp;by&nbsp;the&nbsp;containerToAdd&nbsp;text&nbsp;to&nbsp;the&nbsp;page.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;containerToAdd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;containerToAdd&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Container&nbsp;1&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;containerShape&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Visio.Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;containerShape&nbsp;=&nbsp;thePage.DropContainer(&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;containersDoc.Masters.ItemU(containerToAdd),&nbsp;<span class="visualBasic__keyword">Nothing</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;containersDoc.Close&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Once&nbsp;you&nbsp;have&nbsp;a&nbsp;container&nbsp;on&nbsp;the&nbsp;page,&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;access&nbsp;its&nbsp;ContainerProperties&nbsp;object.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;props&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;ContainerProperties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;props&nbsp;=&nbsp;containerShape.ContainerProperties&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Once&nbsp;you&nbsp;have&nbsp;the&nbsp;ContainerProperties&nbsp;object,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;you&nbsp;can&nbsp;read&nbsp;properties.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;ContainerStyle=&quot;</span>&nbsp;&amp;&nbsp;props.ContainerStyle&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;also&nbsp;change&nbsp;properties.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;props.ContainerStyle&nbsp;=&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MsgBox&nbsp;<span class="visualBasic__string">&quot;ContainerStyle=&quot;</span>&nbsp;&amp;&nbsp;props.ContainerStyle&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><em><span style="font-size:small"><a id="26196" href="/site/view/file/26196/1/Visio.ContainerProperties.txt">Visio.ContainerProperties.txt</a>&nbsp;- Download this sample only.</span><br>
</em></em></li><li><span style="font-size:small"><em><em><a id="26197" href="/site/view/file/26197/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905478">Visio Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
