# SharePoint 2013: Create clip animations in apps using the animation library
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* User Experience
## IsPublished
* True
## ModifiedDate
* 2013-02-27 07:10:05
## Description

<p id="header">This SharePoint-hosted app uses the <span><span class="keyword">SPAnimation</span></span> engine to produce animation effects to move a photo in an
<span><span class="keyword">img</span></span> tag around inside a <span><span class="keyword">div</span></span> that provides a clipping region.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<div class="section" id="sectionSection0">
<p><span class="label">Provided by:</span></p>
</div>
<div class="section" id="sectionSection0">
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=52a3f2aa-710f-4496-9b78-f240eccc74ad" target="_blank">Ted Pattison</a>,
<a href="http://www.criticalpathtraining.com" target="_blank">Critical Path Training</a></p>
<p>The ClipAnimator sample project demonstrates producing simple animations using the
<span><span class="keyword">SPAnimation</span></span> library. It shows to how move an &lt;img&gt; element that displays a photo around inside a parent &lt;div&gt; element in such a way that the photo is clipped when it moves outside the region defined by
 the parent <span><span class="keyword">div</span></span>.</p>
<div>Key features illustrated in the sample:</div>
<ul>
<li>
<div>Using the SPAnimation library.</div>
</li><li>
<div>Create animation objects based on <span><span class="keyword">SPAnimation.State</span></span> and
<span><span class="keyword">SPAnimation.Object</span></span>.</div>
</li><li>
<div>Performing a <span><span class="keyword">Basic_Move</span></span> operation by setting the
<span><span class="keyword">PositionX</span></span> and <span><span class="keyword">PositionY</span></span> attributes.</div>
</li></ul>
</div>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<div>This sample requires the following:</div>
<ul>
<li>
<div>A SharePoint 2013 development environment using Office 365 or a local SharePoint farm</div>
</li><li>
<div>The SharePoint farm must be configured to support apps for SharePoint</div>
</li><li>
<div>Visual Studio 2012 and Office Developer Tools for Visual Studio 2012</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<div>The sample consists of a Visual Studio 2012 project for a SharePoint-hosted app named ClipAnimator.</div>
<p>The start page includes an &lt;img&gt; tag with a photo loaded from photo.jpg, and also a set of four command buttons that run the code that demonstrates creating animations. When you click each of the four buttons on the start page, it executes JavaScript
 code to produce an animation effect using the <span><span class="keyword">SPAnimation</span></span> library.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<div>Follow these steps to configure the sample.</div>
<ol>
<li>
<div>Make sure that you have a development environment with Office 365 or a local SharePoint farm.</div>
</li><li>
<div>Make sure that the SharePoint farm is configured to support apps for SharePoint.</div>
</li></ol>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection4">
<div class="subSection">
<ol>
<li>
<div>Open the solution <span class="ui">ClipAnimator</span> project.</div>
</li><li>
<div>Configure the project's <span><span class="keyword">Site Url</span></span> property to point to a SharePoint 2013 test site.</div>
</li><li>
<div>Press F5 to test the app in the debugger.</div>
</li><li>
<div>When the start page is displayed, click each of the four buttons to see the animation effects.</div>
</li><li>
<div>Inspect the code in App.js, which produces the animations.</div>
</li></ol>
</div>
</div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection5">
<div>If the app fails to install, troubleshoot the following aspect of your development environment:</div>
<ul>
<li>
<div>Make sure your environment supports apps. In Visual Studio 2012, create a new SharePoint-hosted app, and ensure that you can deploy it in a test site on your farm. If you cannot, your environment is not configured to support apps for SharePoint.</div>
</li></ul>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection6">
<p>First release: January 2013</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection7">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li></ul>
</div>
</div>
</div>
</div>
