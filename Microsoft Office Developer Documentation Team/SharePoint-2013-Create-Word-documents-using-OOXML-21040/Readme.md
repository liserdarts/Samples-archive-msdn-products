# SharePoint 2013: Create Word documents using OOXML in apps for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
* Office 2013
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-28 09:13:19
## Description

<p id="header">This sample app demonstrates how to use the Office OpenXML SDK in an app for SharePoint to create Microsoft Word documents in SharePoint libraries.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution enumerates through each document library in the host website, and adds the library to a drop-down list. When the user selects a library and clicks a
 tile, the app creates a sample Word 2013 document by using OOXML in the selected library.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>Visual Studio 2012</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Either of the following:</div>
<ul>
<li>
<div>SharePoint Server 2013 configured to host apps, and with a Developer Site collection already created; or,</div>
</li><li>
<div>Access to an Office 365 Developer Site configured to host apps.</div>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<div>The <strong>Default.aspx</strong> webpage, which is used to enumerate through each document library in the host website, and render tiles for each MP4 video in the app.</div>
</li><li>
<div>The <strong>Point8020Metro.css</strong> style sheet (in the CSS folder) which contains some simple styles for rendering tiles.</div>
</li><li>
<div><strong>The AppManifest.xml</strong> file, which has been edited to specify that the app requests Full Control permissions for the hosting web.</div>
</li><li>
<div>References to the <strong>DocumentFormat.OpenXml</strong> assembly provided by the OpenXML SDK 2.5.</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_Autohosted_OOXML_cs.sln</span> file using Visual Studio 2012.</div>
</li><li>
<div>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site to the
<span><span class="keyword">Site URL</span></span> property.</div>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site if you are prompted to do so by the browser.</p>
</li><li>
<p>Trust the app when you are prompted to do so.</p>
</li></ol>
<p>The following images illustrate the app. In Figure 1 the app has been trusted and libraries added to the drop-down list.</p>
<p class="caption"><strong>Figure 1. View of the app with drop-down list</strong></p>
<p><img id="76806" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-be5b3f39/image/file/76806/1/12-1.png" alt="Figure 1" width="602" height="215"></p>
<p>In Figure 2, the user has clicked the orange tile. The document is created and the red tile provides a link to the appropriate library (Figure 3), which the user reaches by clicking on the red tile.</p>
<p class="caption"><strong>Figure 2. Open XML document creator</strong></p>
<p><img id="76807" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-be5b3f39/image/file/76807/1/12-2.png" alt="Figure 2" width="602" height="171"></p>
&nbsp;
<p class="caption"><strong>Figure 3. Document library</strong></p>
<br>
<img id="76808" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-create-be5b3f39/image/file/76808/1/12-3.png" alt="Figure 3" width="602" height="275"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 30, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj920104.aspx" target="_blank">How to: Retrieve user profile properties by using the JavaScript object model in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
