# SharePoint 2013: Display images from a SharePoint list in a carousel
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* SharePoint
## IsPublished
* True
## ModifiedDate
* 2013-04-03 05:29:58
## Description

<p id="header">This sample app demonstrates how to use JavaScript in an app for SharePoint to retrieve images from a SharePoint list, and then display those images in a carousel built with JavaScript.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span>&nbsp;&nbsp;<a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>The solution is based on the SharePoint-hosted App template provided by Visual Studio 2012. Additional images have been added to the SharePoint module named Images, and these files are automatically deployed with the solution.</p>
<div class="caption">Figure 1. Additional files in the Images list</div>
<img id="79346" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-display-c7126777/image/file/79346/1/2-1.png" alt="" width="245" height="611"><br>
<p>The sample demonstrates how to use JavaScript to retrieve the images from the Images module and render them as HTML list items. Finally, an open-source jQuery library is used to display those list items in an interactive carousel.</p>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either of the following:</p>
<ul>
<li>
<p>SharePoint Server 2013 configured to host apps, and with a Developer Site Collection already created; or,</p>
</li><li>
<p>Access to an Office 365 developer site configured to host apps.</p>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong>Images</strong> folder, which is the module that contains the image files used in the carousel.</p>
</li><li>
<p>The <strong>Default.aspx</strong> webpage, which is used to render the carousel.</p>
</li><li>
<p>The <span><span class="keyword">liquidcarousel.css</span></span> file in the
<strong>content</strong> folder, which contains some simple CSS styles used by the carousel</p>
</li><li>
<p>The <strong>App.js</strong> file in the <strong>scripts</strong> folder, which is used to render list items for each of the numbered images deployed in the Images module.</p>
</li><li>
<p>The <strong>jquery.liquidcarousel.js</strong> file and the <strong>jquery.liquidcarousel.pack.js</strong> file, which are located in the scripts folder and which are open-source JavaScript files that render list items in a carousel.</p>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<p>Open the SP_SharePointCarousel_js.sln file using Visual Studio 2012.</p>
</li><li>
<p>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 developer site collection or Office 365 developer site to the
<span><span class="keyword">Site URL</span></span> property.</p>
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
<p>Sign in to your SharePoint Server 2013 developer site collection or Office 365 developer site if you are prompted to do so by the browser.</p>
</li></ol>
<p>Figure 2 depicts how the sample app renders images in the carousel.</p>
<div class="caption">Figure 2. Images rendered in the carousel</div>
<br>
<img id="79347" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-display-c7126777/image/file/79347/1/2-2.png" alt="" width="592" height="167"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site Collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January, 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
</div>
