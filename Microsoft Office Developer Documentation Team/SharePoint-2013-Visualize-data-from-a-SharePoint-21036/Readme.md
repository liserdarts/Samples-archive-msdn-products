# SharePoint 2013: Visualize data from a SharePoint list in an app
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
* 2013-02-28 09:47:15
## Description

<p id="header">Demonstrates how to use JavaScript in an app for SharePoint to retrieve data from a SharePoint list, and then render that data in a variety of engaging ways by using JavaScript and HTML.</p>
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
<p>The solution is based on the SharePoint-hosted app template provided by Visual Studio 2012. The solution contains a custom list definition, list instance, and list data that will be deployed with the app. This data will then be visualized by JavaScript in
 the app.</p>
<p>The sample demonstrates how to use JavaScript to retrieve the data from the above custom list and then how to use JavaScript to visualize that data in a variety of ways in HTML, each of which enable the user to compare the sizes of populations very easily.</p>
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
<div>SharePoint Server 2013 configured to host apps, and with a developer site collection already created; or,</div>
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
<div>The <strong>Default.aspx</strong> webpage, which is used to present the visualized data.</div>
</li><li>
<div>The <strong>PopulationData</strong> list definition and instance that contains the data to be charted.</div>
</li><li>
<div>The <strong>App.js</strong> file in the <strong>scripts</strong> folder, which is used to retrieve the data from SharePoint by using the JavaScript implementation of the client object model (JSOM) and render that data as an HTML chart.</div>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Open the <span class="ui">SP_SharePointDataVisualization_js.sln</span> file using Visual Studio 2012.</div>
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
<div>Log in to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site if you are prompted to do so by the browser.</div>
</li></ol>
<p>The following images shows examples of the resulting app. Figure 1 depicts the data displayed as a basic table.</p>
<p class="caption"><strong>Figure 1. SharePoint data rendered as table</strong></p>
<p><img id="76822" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-visualize-1b15aa69/image/file/76822/1/3-1.png" alt="Figure 1" width="567" height="220"></p>
<p>In Figure 2, the user has clicked the <span class="ui">View Stacked</span> button, which depicts the data as a stack of objects whose size represents the relative value of the underlying data set&mdash;in this case, population.</p>
<p class="caption"><strong>Figure 2. SharePoint data rendered as stacked objects</strong></p>
<p><img id="76823" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-visualize-1b15aa69/image/file/76823/1/3-2.png" alt="Figure 2" width="567" height="293"></p>
<p>In Figure 3, the user has clicked the <span class="ui">View Tiled</span> button, which renders the data as tiled objects whose area represents the relative value of the underlying data set.</p>
<p class="caption"><strong>Figure 3. SharePoint data rendered as tiled objects</strong></p>
<br>
<img id="76824" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-visualize-1b15aa69/image/file/76824/1/3-3.png" alt="Figure 3" width="564" height="305"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>Ensure that you have SharePoint Server 2013 configured to host apps (with a Developer Site collection already created), or that you have signed up for an Office 365 Developer Site configured to host apps.</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January, 2013.</p>
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
