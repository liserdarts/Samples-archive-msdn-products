# SharePoint 2013: Access the Social APIs in online apps for SharePoint using CSOM
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
* social computing
## IsPublished
* True
## ModifiedDate
* 2013-02-28 09:02:15
## Description

<p id="header">This sample app demonstrates how to use the client object model (CSOM) in an app for SharePoint to retrieve and work with social feeds, including rendering posts and replies, posting new updates, and replying to existing posts. The app is designed
 to run in SharePoint Online websites to illustrate how to authenticate using claims-based authentication before trying to access the Social API.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction"></div>
<h1 class="heading">Description</h1>
<div class="introduction"></div>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>The solution is based on the autohosted app template provided by Visual Studio 2012. The solution authenticates against SharePoint Online to retrieve the social feeds for the current user. The user can then see the three most recent posts (along with any
 replies). They can also create new posts from the app, and can reply to existing posts.</p>
<p>Figure 1 shows the app after posts have been retrieved and rendered. The user can choose to create a new post; further, they can reply to all currently rendered posts in a single operation.</p>
<strong>Figure 1. View of the app after posts have been received</strong> <br>
<img id="76801" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-access-the-09c99ad1/image/file/76801/1/6b-1.png" alt="Figure 1" width="153" height="505"></div>
<div class="introduction"></div>
<h1 class="heading">Prerequisites</h1>
<div class="introduction"></div>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>Visual Studio 2012</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Access to an Office 365 Developer Site configured to host apps.</div>
</li></ul>
</div>
<div class="introduction"></div>
<h1 class="heading">Key components of the sample</h1>
<div class="introduction"></div>
<div class="section" id="sectionSection2">The sample app contains the following:
<ul>
<li>
<div>The <strong>Default.aspx</strong> webpage, which is used to render the posts, and enables the user to create new posts and reply to existing posts</div>
</li><li>
<div>The <strong>point8020Metro.css</strong> style sheet in the CSS folder, which is used to display items and buttons as tiles</div>
</li><li>
<div>The <strong>AppManifest.xml</strong> file, which has been edited to specify that the app requests Full Control permissions for User Profiles, and Write permissions for the Tenant</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<div class="introduction"></div>
<h1 class="heading">Configure the sample</h1>
<div class="introduction"></div>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_Claims_SPO_Social_cs.sln</span> file using Visual Studio 2012.</div>
</li><li>
<div>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site to the
<span><span class="keyword">Site URL</span></span> property.</div>
</li></ol>
<p>No other configuration is required.</p>
</div>
<div class="introduction"></div>
<h1 class="heading">Build the sample</h1>
<div class="introduction"></div>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<div class="introduction"></div>
<h1 class="heading">Run and test the sample</h1>
<div class="introduction"></div>
<div class="section"></div>
<div class="section">To run and test the sample, do the following:</div>
<div class="section"></div>
<div class="section">
<ol>
<li>
<div>Press F5 to run the app.</div>
</li><li>
<div>Sign in to the Office 365 Developer Site if you are prompted to do so by the browser.</div>
</li><li>
<div>Trust the app when prompted to do so.</div>
</li></ol>
</div>
<div class="introduction"></div>
<p class="introduction">The following images illustrate views of the app. Figure 2 shows that the app displays up to the three most recent posts. Note that if you see no tiles, no posts have yet been made in your SharePoint Online environment. The user can
 then reply to one or all of the posts, or can create a new post.</p>
<div class="introduction"></div>
<p class="caption"><strong>Figure 2. View of the app showing three most recent posts</strong></p>
<div class="introduction"></div>
<p class="introduction"><img id="76802" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-access-the-09c99ad1/image/file/76802/1/6b-2.png" alt="Figure 2" width="253" height="843"></p>
<div class="introduction">Figure 3 shows where the user has browsed to the Newsfeed in SharePoint, where all of the posts and replies that were performed in the app are displayed. Note that the SharePoint user interface renders posts in the order of when
 they were last modified (whereas the app renders posts in in order of when they were created).</div>
<div class="introduction"></div>
<div class="introduction"><strong>Figure 3. Results of app postings rendered in the user's SharePoint Newsfeed</strong>
<p><img id="76803" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-access-the-09c99ad1/image/file/76803/1/6b-3.png" alt="Figure 3" width="602" height="840"></p>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure you have SharePoint Server 2013 that is configured to host apps (with a Developer Site collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">First release: January 2013.</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163280.aspx" target="_blank">Social and collaboration features in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163130.aspx" target="_blank">Follow people in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj193046.aspx" target="_blank">Social client class library</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179912.aspx" target="_blank">How to: Complete basic operations using SharePoint 2013 client library code</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142384.aspx" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
