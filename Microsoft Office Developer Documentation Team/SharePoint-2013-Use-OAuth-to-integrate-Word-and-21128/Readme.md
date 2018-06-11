# SharePoint 2013: Use OAuth to integrate Word and SharePoint 2013
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
* Office 2013
## Topics
* Authentication
## IsPublished
* True
## ModifiedDate
* 2013-03-05 04:07:26
## Description

<p id="header">This sample app demonstrates how to use JavaScript and C# in a Microsoft Word 2013 task pane app to connect to and authenticate a SharePoint Online site by using OAuth. The sample also shows how to create a SharePoint list based on data retrieved
 from the Word document.</p>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<h1 class="heading">Description</h1>
<p id="sectionSection0" class="section"><span class="label">Provided by:</span></p>
<p><a href="http://mvp.microsoft.com/en-US/findanmvp/Pages/profile.aspx?MVPID=c558e0ed-382f-4008-8002-4634a9167b99" target="_blank">Martin Harwar</a>,
<a href="http://point8020.com/Default.aspx" target="_blank">Point8020</a></p>
<p>This app for Office includes a task pane app. It also includes a Word document with sample data, named ListDefiner.docx.</p>
<p>The ListDefiner.docx document is set as the <span><span class="keyword">StartAction</span></span> property of the app for Office. The document has three named
<span><span class="keyword">RichTextContentControl</span></span> objects. The following screen shot shows how the document and the app will appear after you launch the solution.</p>
<p class="caption"><strong>Figure 1. View of ListDefiner.docx showing task pane app</strong></p>
<br>
<img id="77251" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-oauth-47872a28/image/file/77251/1/14-1.png" alt="Figure 1" width="602" height="481">
<p>The sample demonstrates how to use JavaScript to add bindings to the named controls in the document. It also demonstrates how to retrieve the values from those bindings. Then, the sample shows how to authenticate against a SharePoint site, and finally, how
 to create a list in the SharePoint site based on the data retrieved from the Word bindings.</p>
<h1 class="heading">Prerequisites</h1>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<div>Visual Studio 2012</div>
</li><li>
<div>Office Developer Tools for Visual Studio 2012</div>
</li><li>
<div>Word 2013</div>
</li><li>
<div>Access to an Office 365 Developer Site</div>
</li></ul>
</div>
<h1 class="heading">Key components of the sample</h1>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<div>The <strong>WD_SharePointOAuth_cs</strong> project, which contains the following files:</div>
<ul>
<li>
<div>The <strong>WD_SharePointOAuth_cs.xml</strong> manifest file.</div>
</li><li>
<div>The <strong>ListDefiner.docx</strong> document, which is prepopulated with a table containing
<span><span class="keyword">RichTextContentControls</span></span>.</div>
</li></ul>
</li><li>
<div>The <strong>WD_SharePointOAuth_csWeb</strong> project, which contains many template files. However, the files that have been developed as part of this sample solution are the following:</div>
<ul>
<li>
<div><strong>ListCreator.aspx</strong> (in the Pages folder).</div>
</li><li>
<div><strong>OAuth.aspx</strong> (in the Pages folder).</div>
</li><li>
<div><strong>OAuthHelper.js</strong> (in the Scripts folder).</div>
</li><li>
<div><strong>RedirectAccept.js</strong> (in the Scripts folder).</div>
</li><li>
<div><strong>WD_SharePointOAuth_cs.js</strong> (in the Scripts folder).</div>
</li><li>
<div><strong>TokenCache.cs</strong> (in the root folder).</div>
</li><li>
<div><strong>TokenHelper.cs</strong> (in the root folder).</div>
</li><li>
<div><strong>Web.config</strong> (in the root folder).</div>
</li><li>
<div><strong>Web.debug.config</strong> (in the root folder).</div>
</li></ul>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1 class="heading">Configure the sample</h1>
<div class="section" id="sectionSection3">
<p>Follow these steps to configure the sample.</p>
<ol>
<li>
<div>Browse to the <span class="code">/_layouts/15/appregnew.aspx</span> page your Office 365 Developer Site (for example,
<span class="code">https://&lt;YourSiteName&gt;.sharepoint.com/_layouts/15/appregnew.aspx</span>).</div>
</li><li>
<div>Click the <span class="ui">Generate</span> buttons for both <span class="ui">
App Id</span> and <span class="ui">App Secret</span>, and then type the URL of your site as the
<span class="ui">App Domain</span>. Then fill in the other text boxes as shown in Figure 2.</div>
<p class="caption"><strong>Figure 2. AppRegNew page</strong></p>
<br>
<img id="77252" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-oauth-47872a28/image/file/77252/1/14-2.png" alt="Figure 2" width="531" height="268">
</li><li>
<div>Click <span class="ui">Create</span>. Your page should resemble the screen shot in Figure 3.</div>
<p class="caption"><strong>Figure 3. App ID and App Secret properties</strong></p>
<br>
<img id="77253" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-oauth-47872a28/image/file/77253/1/14-3.png" alt="Figure 3" width="647" height="165">
</li><li>
<div>Leave the page open so that you can refer to the data, and switch back to Visual Studio.</div>
</li><li>
<div>Double-click <span class="ui">WD_SharePointOAuth_cs</span> as shown in Figure 4, and then add your App Domain value as shown.</div>
<p class="caption"><strong>Figure 4. Adding the AppDomain to the WD_SharePointOAuth item</strong></p>
<br>
<img id="77254" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-oauth-47872a28/image/file/77254/1/14-4.png" alt="Figure 4" width="630" height="290">
</li><li>
<div>Edit the Web.config and Web.debug.config files so that they contain the <strong>
ClientId</strong> and <strong>ClientSecret</strong> values that you generated from SharePoint.</div>
</li><li>
<div>When you are sure all the values have been configured correctly, save all files in Visual Studio, and then close the browser.</div>
</li></ol>
<p>No other configuration is required.</p>
</div>
<h1 class="heading">Build the sample</h1>
<div class="section" id="sectionSection4">
<p>To build the sample, press CTRL&#43;SHIFT&#43;B.</p>
</div>
<h1 class="heading">Run and test the sample</h1>
<div class="section" id="sectionSection5">
<p>To run and test the sample, press F5.</p>
<p>Figure 5 depicts the app shortly after launching. You have opened the Word document and have entered the URL to your Office 365 Developer Site, and then clicked
<span class="ui">Connect</span>. At this point you are prompted to sign in.</p>
<p class="caption"><strong>Figure 5. Sign-in to the Word app</strong></p>
<br>
<img id="77255" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-use-oauth-47872a28/image/file/77255/1/14-5.png" alt="Figure 5" width="602" height="481"></div>
<h1 class="heading">Troubleshooting</h1>
<div class="section" id="sectionSection6">
<p>If the app starts with a blank document instead of the one shown in Figure 1, ensure that the
<span><span class="keyword">StartAction</span></span> property of the WD_SharePointOAuth_cs project is set to
<strong>ListDefiner.docx</strong> and not just to Microsoft Word.</p>
<p>Ensure you have thoroughly read and followed the instructions in the &quot;Configure the sample&quot; section of this document</p>
</div>
<h1 class="heading">Change log</h1>
<div class="section" id="sectionSection7">
<p>First release: January 2013.</p>
</div>
<h1 class="heading">Related content</h1>
<div class="section" id="sectionSection8">
<ul>
<li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp161507.aspx" target="_blank">Apps for Office and SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/ms457529.aspx" target="_blank">Authentication, authorization, and security in SharePoint 2013</a></div>
</li><li>
<div><a href="http://msdn.microsoft.com/en-us/library/jj163201.aspx" target="_blank">How to: Complete basic operations using JavaScript library code in SharePoint 2013</a></div>
</li></ul>
</div>
</div>
</div>
</div>
