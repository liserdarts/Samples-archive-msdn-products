# SharePoint 2010: Creating custom tab Web Parts
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Visual Studio 2010
* SharePoint Server 2010
## Topics
* UI Design
* Sharepoint Web Parts
* User Experience
## IsPublished
* True
## ModifiedDate
* 2012-11-12 01:02:21
## Description

<h1>Description</h1>
<p><span style="font-size:small">SharePoint 2010 provides many out-of-the-box Web Parts, but none of those Web Parts provides a tabbed interface. Today&rsquo;s web pages have lots of information; a tab Web Part would help organize that information in far less
 space.</span></p>
<p><span style="font-size:small">This sample code and the accompanying article describe how to create a SharePoint 2010 custom tab Web Part in Visual Studio 2010 that is independent of the page layout. You can add this custom tab Web Part to a SharePoint page
 to provide a tabbed UI without having to modify the page layout. After you have the tabbed UI, you can organize the page information under tabs.</span></p>
<p><span style="font-size:small">This custom tab Web Part uses the Web Part Export/Import methods to &ldquo;clone&rdquo; predefined closed Web Parts, and then display them under tabs.</span></p>
<h1>Running the code sample</h1>
<p><span style="font-size:small">The sample code package contains a SharePoint 2010 custom Web Part solution that you can build and deploy to your SharePoint site. Use the following steps to run the code sample.</span></p>
<ol>
<li><span style="font-size:small">Run Visual Studio 2010 as Administrator.</span>
</li><li><span style="font-size:small">In Visual Studio 2010, click <strong>File</strong>, point to
<strong>Open</strong>, and then click <strong>Project/Solution...</strong> to open the SharePoint2010CustomTabWebPart.sln file.</span>
</li><li><span style="font-size:small">In the Visual Studio <strong>Properties</strong> pane, change the
<strong>Site URL </strong>value to the absolute address of your SharePoint 2010 development test site. For example, &quot;http://MySharePointDevServer/&quot;. Make sure that you include the closing forward slash.</span>
</li><li><span style="font-size:small">Now you can build and deploy your solution to SharePoint, and then test the custom tab Web Part by adding it to a test page. For more information, see the &ldquo;Add a Custom Tab Web Part&rdquo; section in the accompanying
 article on MSDN.</span> </li></ol>
<h1>Additional resources</h1>
<p><span style="font-size:small">For more information, see the following:</span></p>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ff597539.aspx">Create Visual Web Parts in SharePoint 2010</a></span>
</li><li><span style="font-size:small"><a href="http://technet.microsoft.com/en-us/magazine/gg153557.aspx">Know Whether to Delete or Close Web Parts on SharePoint 2010</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/53542h0c">WebPartManager Class</a></span>
</li></ul>
