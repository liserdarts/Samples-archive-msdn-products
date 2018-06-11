# Apps for Office: Generate documents using Windows Azure
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Azure
* Office 365
* Word
## Topics
* apps for Office
## IsPublished
* True
## ModifiedDate
* 2014-03-01 11:09:12
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Apps for Office: Generate documents using Windows Azure</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p><strong>Last modified: </strong>February 06, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#sectionSection0">Description</a> <br>
<a href="#sectionSection1">Prerequisites</a> <br>
<a href="#sectionSection2">Key components</a> <br>
<a href="#sectionSection3">Configure the sample</a> <br>
<a href="#sectionSection4">Build the sample</a> <br>
<a href="#sectionSection5">Run and test the sample</a> <br>
<a href="#sectionSection6">Troubleshooting</a> <br>
<a href="#sectionSection7">Change log</a> <br>
<a href="#sectionSection8">Related content</a></p>
<p><span>Summary:</span> Demonstrates an app for Word that allows you to create and insert user-defined blocks of content into a document using Windows Azure as the storage backend.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0">
<p><span>Provided by:</span> <a href="http://3sharp.com/" target="_blank">Thomas Mechelke</a>, 3Sharp</p>
<p>The app in this code sample inserts blocks of content into an Office document. The app provides sample content that you can insert, but also lets you create and store your own blocks of content that you can insert into a document. You can also categorize
 your blocks of content by using categories that you define. To insert a block of content, the user chooses the label that corresponds to the kind of content that they want.</p>
<p>The sample content blocks are stored as Office Open XML code inside the app. The user-defined blocks of content are stored as Windows Azure Blobs. For this sample, the Windows Azure storage container name and key are hardcoded in the Web.config file for
 the app.</p>
<p>Figure 1 shows the completed app after it was inserted into the Office application.</p>
<strong>
<div class="caption">Figure 1. The document generation app user interface when you first insert the app.</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/109614/1/image.png" alt="">
<p>Figure 2 shows a Smart Art diagram on the document surface that was inserted using the app.</p>
<strong>
<div class="caption">Figure 2. A Smart Art diagram displayed on the document surface.</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/109613/1/image.png" alt=""></div>
<h1>Prerequisites</h1>
<div id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Word 2013</p>
</li><li>
<p>Visual Studio 2012 with Office Developer Tools for Visual Studio 2012, or Visual Studio 2013</p>
</li><li>
<p>Internet Explorer 9 or later</p>
</li><li>
<p>Windows Azure subscription.</p>
<p>For information about how to get a Windows Azure subscription, see the <a href="http://www.windowsazure.com/en-us/pricing/free-trial/" target="_blank">
Windows Azure portal</a>.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection2">
<p>&nbsp;</p>
<ul>
<li>
<p>DocGenAppForOffice project</p>
<ul>
<li>
<p>DocGenAppForOffice.xml manifest file</p>
</li></ul>
</li><li>
<p>DocGenAppForOfficeWeb project</p>
<ul>
<li>
<p>Home.html file, which contains the HTML control for the app's user interface.</p>
</li><li>
<p>Home.js file, which contains the event handler for the <strong><span class="keyword">Office.initialize</span></strong> event of the app, and handles the button click event for the app's button.</p>
</li><li>
<p>Home.css file, which contains the styling for the elements shown in the app UI.</p>
</li><li>
<p>Controllers folder</p>
<ul>
<li>
<p>CategoriesController.cs</p>
</li><li>
<p>DocSectionsController.cs</p>
</li><li>
<p>SampleDataController.cs</p>
</li></ul>
<p>These files define classes that provide access to the Windows Azure Storage Container and to the sample data that is hardcoded into the app.</p>
</li><li>
<p>SampleData folder, which contains the Office Open XML for each of the sample content types included in the app.</p>
</li></ul>
</li></ul>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection3">
<p>To configure the app, you have to create a Windows Azure Storage Container and then copy the Storage Container name and a Storage Container key to the Web.config file for the app. The app reads and writes to the Windows Azure Storage Container to store document
 section categories and sections.</p>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>The description of the Windows Azure management portal that follows was accurate as of the time of publishing this app to the MSDN Code Gallery. The Windows Azure portal website may have changed by the time that you read this but the basic steps to create
 a Windows Azure Storage Container should resemble the steps given in this section.</p>
</td>
</tr>
</tbody>
</table>
</div>
<div>
<ol>
<li>
<p>Sign in to the Windows Azure<a href="https://manage.windowsazure.com" target="_blank">management portal</a>.</p>
</li><li>
<p>At the bottom of the page, choose <strong><span class="ui">NEW</span></strong>.</p>
</li><li>
<p>Under <strong><span class="ui">NEW</span></strong>, choose <strong><span class="ui">DATA SERVICE</span></strong>INVALID USE OF SYMBOLS<strong><span class="ui">STORAGE</span></strong>INVALID USE OF SYMBOLS<strong><span class="ui">QUICK CREATE</span></strong>.</p>
</li><li>
<p>For <strong><span class="ui">URL</span></strong>, enter a storage name. Windows Azure makes sure the name that you enter is unique across the system and will notify you if the name has already been selected by another customer.</p>
</li><li>
<p>For <strong><span class="ui">LOCATION/AFFINITY GROUP</span></strong>, choose a geographic location near you.</p>
</li><li>
<p>Accept the default choice for <strong><span class="ui">REPLICATION</span></strong>.</p>
</li><li>
<p>Choose <strong><span class="ui">CREATE STORAGE ACCOUNT</span></strong>.</p>
</li><li>
<p>After a few moments, Windows Azure finishes creating the storage container and sets the container status to
<strong><span class="ui">Online</span></strong>.</p>
</li><li>
<p>At the bottom of the page, choose <strong><span class="ui">MANAGE ACCESS KEYS</span></strong>.</p>
</li><li>
<p>Choose the copy button for either the primary or secondary access key</p>
</li><li>
<p>In Solution Explorer in Visual Studio, expand <strong><span class="ui">DocGenAppForOfficeWeb</span></strong>, and then double-click
<strong><span class="ui">Web.config</span></strong>.</p>
</li><li>
<p>In the editor pane for Web.config, find the child element, <span><strong>[add]</strong></span>, in the
<span><strong>[connectionStrings]</strong></span> element.</p>
</li><li>
<p>Replace the entry for the <span><strong>[AccountName]</strong></span> attribute with your Windows Azure Storage Container name and the
<span><strong>[AccountKey]</strong></span> attribute with the access key for the container.</p>
</li><li>
<p>Choose FileINVALID USE OF SYMBOLS Save All to save your changes.</p>
</li></ol>
</div>
</div>
<h1>Build the sample</h1>
<div id="sectionSection4">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5">
<p>Choose the F5 key to build and deploy the app.</p>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection6">
<p>If the app is not installed, ensure that the XML in your <strong>AppManifest.xml</strong> file parses correctly.</p>
<p>Check that you have replaced the placeholders in the app's Web.Config file with your Windows Azure Storage Container and Storage Container key.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection7">
<p>First release: Feb 2014</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp123513.aspx" target="_blank">Reading and writing data to the active selection in a document or spreadsheet</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/library/c5bad651-a42f-4e57-bc60-c9b27eb2383b(Office.15).aspx" target="_blank">Creating better apps for Word with Office Open XML</a></p>
</li><li>
<p><span><a href="1561bf17-122e-4510-b1ce-7c7c346cdec9.htm">How to: Host an app for Office on Windows Azure</a>
</span></p>
</li></ul>
</div>
</div>
</div>
