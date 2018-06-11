# PowerPoint 2010: Use Custom XML Data Using PPT.CustomerDataDemo
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* PowerPoint 2010
* Office 2010
## Topics
* Office 2010 101 code samples
* Custom XML
## IsPublished
* True
## ModifiedDate
* 2011-08-05 12:37:24
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample shows how to use custom XML data in a Microsoft PowerPoint 2010 presentation.</span></p>
<p><span style="font-size:small">This code snippet is part of the Office 2010 101 code samples project. This sample, along with others, is offered here to incorporate directly in your code.</span></p>
<p><span style="font-size:small">Each code sample consists of approximately 5 to 50 lines of code demonstrating a distinct feature or feature set, in either VBA or both VB and C# (created in Visual Studio 2010). Each sample includes comments describing the
 sample, and setup code so that you can run the code with expected results or the comments will explain how to set up the environment so that the sample code runs.)</span></p>
<p><span style="font-size:small">Microsoft&reg; Office 2010 gives you the tools needed to create powerful applications. The Microsoft Visual Basic for Applications (VBA) code samples can assist you in creating your own applications that perform specific functions
 or as a starting point to create more complex solutions.</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">Copy this code into a module in a new presentation. Display the VBA window side-by-side with the PowerPoint window and press F8 to single step through this code for the most effective use of this demonstration.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Sub WorkWithCustomerData()
    ' Although this custom XML part is referred to as &quot;Customer data&quot;,
    ' and the documentation refers to it as being for including information
    ' about customers, that's clearly not the intended use. The &quot;customer&quot;
    ' referred to here is actually just the customer of the CustomLayout,
    ' Master, Presentation, Shape, or Slide. You can add custom data to
    ' any (and all) of these, so that you can retrieve the data at will. The
    ' data needn't (and probably won't) be pertinent to customer information.
   
    ' The important point is that the data &quot;goes with&quot; the host object. If you
    ' add customer data to a shape, and then move the shape to a new presentation,
    ' the customer data tags along. The same can be said for a slide, or any other
    ' place you add customer data.
   
    ' Note that this property works much like the Tag property, except that storing only a
    ' single string (like the Tag property) the CustomerData property stores a block of
    ' XML data.
   
    ' Add customer data to a shape on a presentation. Then create a new slide,
    ' copy the shape over, and verify that the customer data went with the shape.
    ' The same concept could apply to any item to which you can attach a
    ' CustomerData value.
   
    Dim shp As Shape
    Dim sld As Slide
    Set sld = ActivePresentation.Slides(1)
    Set shp = sld.Shapes.AddShape(msoShape7pointStar, 10, 10, 200, 200)
   
    ' Add a CustomXMLPart as the CustomerData property of the shape.
    Dim xmlPart As CustomXMLPart
    Set xmlPart = shp.CustomerData.Add
   
    ' Print out the unique ID:
    Dim customerDataId As String
    customerDataId = xmlPart.Id
    Debug.Print customerDataId
   
    ' Put some well-formed XML into the part. You might get this XML from a
    ' text file, from a database, or from a Web Service:
    xmlPart.LoadXML &quot;&lt;XMLData TextToDisplay='Title' Date='5/15/2012' Author='Lucy'&gt;Here's some text!&lt;/XMLData&gt;&quot;
    Debug.Print xmlPart.XML
   
    ' Make a copy of the shape:
    shp.Copy
   
    ' Create a new slide, and paste the shape into the new slide:
    Dim sld2 As Slide
    Set sld2 = ActivePresentation.Slides.Add(2, ppLayoutBlank)
    sld2.Shapes.Paste
       
    ' Retrieve a reference to the new shape:
    Dim shp2 As Shape
    Set shp2 = sld2.Shapes(1)
       
    ' You can only retrieve CustomerData by its ID, but because
    ' the code made a copy of the shape, the customer data now has
    ' a newly assigned ID. Loop through all the CustomerData items
    ' (each of which is a CustomXMLPart), and find the ID of each.
    ' There should only be one, and given the ID, you can get at the XML:
    Dim cxp As CustomXMLPart
    For Each cxp In shp2.CustomerData
        customerDataId = cxp.Id
        Set xmlPart = shp2.CustomerData.Item(customerDataId)
        ' Verify that the custom XML content in the CustomerData property
        ' has been propogated with the copied shape:
        Debug.Print &quot;Newly copied part: &quot; &amp; xmlPart.XML
    Next cxp
End Sub</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Sub</span>&nbsp;WorkWithCustomerData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Although&nbsp;this&nbsp;custom&nbsp;XML&nbsp;part&nbsp;is&nbsp;referred&nbsp;to&nbsp;as&nbsp;&quot;Customer&nbsp;data&quot;,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;and&nbsp;the&nbsp;documentation&nbsp;refers&nbsp;to&nbsp;it&nbsp;as&nbsp;being&nbsp;for&nbsp;including&nbsp;information</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;about&nbsp;customers,&nbsp;that's&nbsp;clearly&nbsp;not&nbsp;the&nbsp;intended&nbsp;use.&nbsp;The&nbsp;&quot;customer&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;referred&nbsp;to&nbsp;here&nbsp;is&nbsp;actually&nbsp;just&nbsp;the&nbsp;customer&nbsp;of&nbsp;the&nbsp;CustomLayout,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Master,&nbsp;Presentation,&nbsp;Shape,&nbsp;or&nbsp;Slide.&nbsp;You&nbsp;can&nbsp;add&nbsp;custom&nbsp;data&nbsp;to</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;any&nbsp;(and&nbsp;all)&nbsp;of&nbsp;these,&nbsp;so&nbsp;that&nbsp;you&nbsp;can&nbsp;retrieve&nbsp;the&nbsp;data&nbsp;at&nbsp;will.&nbsp;The</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;data&nbsp;needn't&nbsp;(and&nbsp;probably&nbsp;won't)&nbsp;be&nbsp;pertinent&nbsp;to&nbsp;customer&nbsp;information.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;important&nbsp;point&nbsp;is&nbsp;that&nbsp;the&nbsp;data&nbsp;&quot;goes&nbsp;with&quot;&nbsp;the&nbsp;host&nbsp;object.&nbsp;If&nbsp;you</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;add&nbsp;customer&nbsp;data&nbsp;to&nbsp;a&nbsp;shape,&nbsp;and&nbsp;then&nbsp;move&nbsp;the&nbsp;shape&nbsp;to&nbsp;a&nbsp;new&nbsp;presentation,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;customer&nbsp;data&nbsp;tags&nbsp;along.&nbsp;The&nbsp;same&nbsp;can&nbsp;be&nbsp;said&nbsp;for&nbsp;a&nbsp;slide,&nbsp;or&nbsp;any&nbsp;other</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;place&nbsp;you&nbsp;add&nbsp;customer&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Note&nbsp;that&nbsp;this&nbsp;property&nbsp;works&nbsp;much&nbsp;like&nbsp;the&nbsp;Tag&nbsp;property,&nbsp;except&nbsp;that&nbsp;storing&nbsp;only&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;single&nbsp;string&nbsp;(like&nbsp;the&nbsp;Tag&nbsp;property)&nbsp;the&nbsp;CustomerData&nbsp;property&nbsp;stores&nbsp;a&nbsp;block&nbsp;of</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;XML&nbsp;data.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;customer&nbsp;data&nbsp;to&nbsp;a&nbsp;shape&nbsp;on&nbsp;a&nbsp;presentation.&nbsp;Then&nbsp;create&nbsp;a&nbsp;new&nbsp;slide,</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;copy&nbsp;the&nbsp;shape&nbsp;over,&nbsp;and&nbsp;verify&nbsp;that&nbsp;the&nbsp;customer&nbsp;data&nbsp;went&nbsp;with&nbsp;the&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;The&nbsp;same&nbsp;concept&nbsp;could&nbsp;apply&nbsp;to&nbsp;any&nbsp;item&nbsp;to&nbsp;which&nbsp;you&nbsp;can&nbsp;attach&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;CustomerData&nbsp;value.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld&nbsp;=&nbsp;ActivePresentation.Slides(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp&nbsp;=&nbsp;sld.Shapes.AddShape(msoShape7pointStar,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">10</span>,&nbsp;<span class="visualBasic__number">200</span>,&nbsp;<span class="visualBasic__number">200</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Add&nbsp;a&nbsp;CustomXMLPart&nbsp;as&nbsp;the&nbsp;CustomerData&nbsp;property&nbsp;of&nbsp;the&nbsp;shape.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;xmlPart&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CustomXMLPart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;xmlPart&nbsp;=&nbsp;shp.CustomerData.Add&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Print&nbsp;out&nbsp;the&nbsp;unique&nbsp;ID:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;customerDataId&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;customerDataId&nbsp;=&nbsp;xmlPart.Id&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;customerDataId&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Put&nbsp;some&nbsp;well-formed&nbsp;XML&nbsp;into&nbsp;the&nbsp;part.&nbsp;You&nbsp;might&nbsp;get&nbsp;this&nbsp;XML&nbsp;from&nbsp;a</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;text&nbsp;file,&nbsp;from&nbsp;a&nbsp;database,&nbsp;or&nbsp;from&nbsp;a&nbsp;Web&nbsp;Service:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;xmlPart.LoadXML&nbsp;<span class="visualBasic__string">&quot;&lt;XMLData&nbsp;TextToDisplay='Title'&nbsp;Date='5/15/2012'&nbsp;Author='Lucy'&gt;Here's&nbsp;some&nbsp;text!&lt;/XMLData&gt;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;xmlPart.XML&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Make&nbsp;a&nbsp;copy&nbsp;of&nbsp;the&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;shp.Copy&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Create&nbsp;a&nbsp;new&nbsp;slide,&nbsp;and&nbsp;paste&nbsp;the&nbsp;shape&nbsp;into&nbsp;the&nbsp;new&nbsp;slide:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sld2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Slide&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;sld2&nbsp;=&nbsp;ActivePresentation.Slides.Add(<span class="visualBasic__number">2</span>,&nbsp;ppLayoutBlank)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sld2.Shapes.Paste&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Retrieve&nbsp;a&nbsp;reference&nbsp;to&nbsp;the&nbsp;new&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;shp2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Shape&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;shp2&nbsp;=&nbsp;sld2.Shapes(<span class="visualBasic__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;You&nbsp;can&nbsp;only&nbsp;retrieve&nbsp;CustomerData&nbsp;by&nbsp;its&nbsp;ID,&nbsp;but&nbsp;because</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;the&nbsp;code&nbsp;made&nbsp;a&nbsp;copy&nbsp;of&nbsp;the&nbsp;shape,&nbsp;the&nbsp;customer&nbsp;data&nbsp;now&nbsp;has</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;a&nbsp;newly&nbsp;assigned&nbsp;ID.&nbsp;Loop&nbsp;through&nbsp;all&nbsp;the&nbsp;CustomerData&nbsp;items</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;(each&nbsp;of&nbsp;which&nbsp;is&nbsp;a&nbsp;CustomXMLPart),&nbsp;and&nbsp;find&nbsp;the&nbsp;ID&nbsp;of&nbsp;each.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;There&nbsp;should&nbsp;only&nbsp;be&nbsp;one,&nbsp;and&nbsp;given&nbsp;the&nbsp;ID,&nbsp;you&nbsp;can&nbsp;get&nbsp;at&nbsp;the&nbsp;XML:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;cxp&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;CustomXMLPart&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;cxp&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;shp2.CustomerData&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;customerDataId&nbsp;=&nbsp;cxp.Id&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Set</span>&nbsp;xmlPart&nbsp;=&nbsp;shp2.CustomerData.Item(customerDataId)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;Verify&nbsp;that&nbsp;the&nbsp;custom&nbsp;XML&nbsp;content&nbsp;in&nbsp;the&nbsp;CustomerData&nbsp;property</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;has&nbsp;been&nbsp;propogated&nbsp;with&nbsp;the&nbsp;copied&nbsp;shape:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print&nbsp;<span class="visualBasic__string">&quot;Newly&nbsp;copied&nbsp;part:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;xmlPart.XML&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;cxp&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="font-size:small"><em><em><a id="26148" href="/site/view/file/26148/1/PPT.CustomerDataDemo.txt">PPT.CustomerDataDemo.txt</a>&nbsp;- Download this sample only.<br>
</em></em></span></li><li><span style="font-size:small"><em><em><a id="26149" href="/site/view/file/26149/1/Office%202010%20101%20Code%20Samples.zip">Office 2010 101 Code Samples.zip</a>&nbsp;- Download all the samples.</em></em></span>
</li></ul>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/aa905465">PowerPoint Developer Center on MSDN</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh360994">101 Code Samples for Office 2010 Developers</a></span>
</li></ul>
