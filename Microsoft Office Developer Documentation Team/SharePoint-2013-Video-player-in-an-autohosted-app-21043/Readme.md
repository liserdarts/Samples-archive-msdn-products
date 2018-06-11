# SharePoint 2013: Video player in an autohosted app for SharePoint
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Media
* User Experience
## IsPublished
* False
## ModifiedDate
* 2013-02-28 09:42:57
## Description

<p id="header">This sample app demonstrates how to use the client object model (CSOM) in an app for SharePoint to retrieve and work with videos from SharePoint.</p>
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
<p>This solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution iterates over each document library in the host website and lists the tiles for each MP4 video in the app. When the user clicks a given title, the app
 dynamically renders an HTML5 video player and plays the video.</p>
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
<div>SharePoint Server 2013 configured to host apps, and with a Developer Site Collection already created; or,</div>
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
<div>The <strong>MetroPlay.png</strong> image which is used as the background in the tile styles.</div>
</li><li>
<div>The <strong>AppManifest.xml</strong> file, which has been edited to specify that the app requests Full Control permissions for the hosting web.</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_AutohostedVideoPlayer_cs.sln</span> file using Visual Studio 2012.</div>
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
<div>Press F5 to run the app.</div>
</li><li>
<div>Sign in to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site if you are prompted to do so by the browser.</div>
</li><li>
<div>When prompted to do so, indicate that you trust the app.</div>
</li></ol>
<p>Note that the app does not upload and store the MP4 videos that it plays; rather, it plays videos that you have uploaded to a SharePoint list. With no videos on the hosting site, the app appears as shown in Figure 1.</p>
<p class="caption"><strong>Figure 1. The app with no videos on the host site</strong></p>
<p><img id="76819" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-video-9ad869fb/image/file/76819/1/1b-1.png" alt="Figure 1" width="619" height="329"></p>
<p>Once the videos are loaded to a SharePoint list, the app then renders them as tiles, as shown in Figure 2.</p>
<p class="caption"><strong>Figure 2. The app with videos uploaded to SharePoint list</strong></p>
<p><img id="76820" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-video-9ad869fb/image/file/76820/1/1b-2.png" alt="Figure 2" width="620" height="321"></p>
<p>Finally, when the user clicks the tile, the associated video is rendered in the frame, as shown in Figure 3.</p>
<p class="caption"><strong>Figure 3. The video rendered in the app frame</strong></p>
<p><img id="76821" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-video-9ad869fb/image/file/76821/1/1b-3.png" alt="Figure 3" width="648" height="322"></p>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930(v=office.15)" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179886.aspx" target="_blank">How to: Create a basic autohosted app in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163794.aspx" target="_blank">Develop apps for SharePoint</a></div>
</li></ul>
</div>
</div>
</div>
</div>
