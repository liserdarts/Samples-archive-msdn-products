# SharePoint 2013: Render SharePoint list data as a chart in an app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-28 10:20:10
## Description

<p id="header">This sample app demonstrates how to use JavaScript in an app for SharePoint to retrieve data from a SharePoint list, and then render that data as a chart by using JavaScript and HTML.</p>
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
<p>The solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution contains a custom list definition, list instance, and list data that will be deployed with the app. This data will then be rendered as a chart.</p>
<p>The sample demonstrates how to use JavaScript to retrieve the data from the above custom list and then how to use JavaScript to render that data as a chart in HTML</p>
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
<div>Access to an Office 365 developer site configured to host apps.</div>
</li></ul>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<div>The <strong>Images</strong> folder, which is a module that contains an image used as the x-axis of the chart.</div>
</li><li>
<div>The <strong>Default.aspx</strong> webpage, which is used to render the chart.</div>
</li><li>
<div>The <strong>PopulationData</strong> list definition and instance that contains the data to be charted.</div>
</li><li>
<div>The <strong>App.js</strong> file in the <strong>scripts</strong> folder, which is used to retrieve the data from SharePoint by using the JavaScript implementation of the client object model (JSOM) and render that data as an HTML chart</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_SharePointCharting_js.sln</span> file using Visual Studio 2012.</div>
</li><li>
<div>In the <span class="ui">Properties</span> window, add the full URL to your SharePoint Server 2013 developer site collection or Office 365 developer site to the
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
</li></ol>
</div>
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
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142379.aspx" target="_blank">How to: Create a basic SharePoint-hosted app</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
