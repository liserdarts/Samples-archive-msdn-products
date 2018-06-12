# SharePoint 2013: Branding with display templates using an app for SharePoint
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* apps for SharePoint
## Topics
* Search
* Mobile
* sites and content
## IsPublished
* True
## ModifiedDate
* 2014-11-06 07:01:29
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><strong><span id="nsrTitle" style="font-size:small">SharePoint 2013: Branding with display templates using an app for SharePoint</span></strong></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p><strong><span class="label">Summary:</span>&nbsp;</strong>&nbsp;Learn about the different ways you can use display templates to render a hero image and content rotator in a Content by Search web part.</p>
</div>
<div class="introduction">
<p><strong>Last modified: </strong>September 10, 2014</p>
<p><strong>In this article</strong><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_Description">Description of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_Prereq">Prerequisites</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_components">Key components of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_ConfigureSPSite">Configure the SharePoint site collection</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#sectionSection4">Configure the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_build">Run and test the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#sectionSection6">Next steps</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#sectionSection7">Pages and channels</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_Changelog">Change log</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_RelatedContent">Related content</a></p>
<p>&nbsp;</p>
</div>
<a name="O15Readme_Description">
<h2 class="heading">Description of the sample</h2>
<div class="section" id="sectionSection0">
<p>This sample app for SharePoint demonstrates how to use display templates to render a hero image and content rotator in a Content by Search web part.</p>
<p>Additionally, the display templates target mobile devices using responsive web design (RWD) and device channels.</p>
</div>
</a><a name="O15Readme_Prereq">
<h2 class="heading">Prerequisites</h2>
</a>
<div class="section" id="sectionSection1"><a name="O15Readme_Prereq">
<p>This sample requires the following:</p>
</a>
<ul>
<a name="O15Readme_Prereq">
<li>
<p>Visual Studio 2013</p>
</li><li>
<p>Microsoft Office Developer tools for Visual Studio 2013</p>
</a></li><li><a name="O15Readme_Prereq">
<p>SharePoint 2013 development environment</p>
</a></li><li><a name="O15Readme_Prereq"></a>
<p><a name="O15Readme_Prereq">SharePoint site collection configured as described in the
</a><a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowestDisplayTemplatesSample/5b28db40-9b62-467a-b877-6f1fdea82a0a.htm#O15Readme_ConfigureSPSite">Configure the SharePoint site collection</a>.</p>
</li></ul>
</div>
<a name="O15Readme_components">
<h2 class="heading">Key components of the sample</h2>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p><strong>Branding.DisplayTemplates</strong> project, the app for SharePoint project.</p>
</li><li>
<p><strong>Branding.DisplayTemplatesWeb</strong> project, the ASP.NET web application project.</p>
</li><li>
<p><strong>Default.aspx.</strong> located in the Branding.DisplayTemplates\Branding.DisplayTemplatesWeb\Pages directory, which contains the HTML and ASP.NET controls for the sample's user interface</p>
</li><li>
<p><strong>Default.aspx.cs</strong> located the Branding.DisplayTemplates\Branding.DisplayTemplatesWeb\Pages directory, which contains all the code used to deploy the artifacts which support this sample.</p>
</li><li>
<p><strong>SharePointContext.cs</strong> located in the Branding.DisplayTemplates\Branding.DisplayTemplatesWeb directory, which encapsulates the information that the sample app needs to get from SharePoint.</p>
</li><li>
<p>The following folders and contents, located in the Branding.DisplayTemplates\Branding.DisplayTemplatesWeb directory:</p>
<ul>
<li>
<p><strong>CSS</strong><br>
Contains CSS files with styles specific to the device channel.</p>
</li><li>
<p><strong>Images</strong><br>
Contains images used in the sample.</p>
</li><li>
<p><strong>Masterpages</strong><br>
Contains master pages specific to the device channel.</p>
</li><li>
<p><strong>Scripts</strong><br>
Contains control and item template JavaScript files.</p>
</li><li>
<p><strong>Templates</strong><br>
Contains templates used to create pages added by the sample app.</p>
</li></ul>
</li></ul>
</div>
</a><a name="O15Readme_ConfigureSPSite">
<h2 class="heading">Configure the SharePoint site collection</h2>
<div class="section" id="sectionSection3">
<p>For the display templates sample to render correctly, you must first create and configure your SharePoint site collection.</p>
<h3 class="procedureSubHeading">To configure the SharePoint site collection</h3>
<div class="subSection">
<ol>
<li>
<p>Navigate to your SharePoint tenancy and create a new site collection using the
<span class="ui">Developer Site</span> template in the <strong><span class="ui">Collaboration</span>
</strong>tab.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 1. Create new site collection</span></strong></div>
<br>
<img id="125162" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125162/1/disptemp_01newsitecollection.png" alt="" width="591" height="469">
</li><li>
<p>Once the site collection is created, navigate to <strong><span class="ui">Site Settings</span></strong>, and in
<strong><span class="ui">Site Collection Administration</span></strong> click <strong>
<span class="ui">Site collection features</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 2. Site collection features link</span></strong></div>
<br>
<img id="125163" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125163/1/disptemp_02sitecollectionfeatures.png" alt="" width="393" height="433">
</li><li>
<p>On the <strong>Site Collection Features</strong> page, click <strong><span class="ui">Activate</span>
</strong>for the <span class="ui">SharePoint Server Publishing Infrastructure</span> feature.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>This feature may take several minutes to activate.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<p>Navigate again to <strong><span class="ui">Site Settings</span></strong>, and in
<span class="ui"><strong>Site Actions</strong></span> click <strong><span class="ui">Manage site features</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 3. Manage site features link</span></strong></div>
<br>
<img id="125164" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125164/1/disptemp_03managesitecollectionfeatures.png" alt="" width="422" height="320">
</li><li>
<p>On the <strong>Site Features</strong> page, click <strong><span class="ui">Activate</span>
</strong>for the <span class="ui">SharePoint Server Publishing</span> feature.</p>
</li><li>
<p>Click <strong><span class="ui">Deactivate</span> </strong>for <span class="ui">
Mobile Browser View</span>.</p>
</li></ol>
</div>
</div>
</a><a name="sectionSection4"></a>
<h2 class="heading">Configure the sample</h2>
<div class="section" id="sectionSection4">
<p>Update the <span class="ui">Site URL</span> property of the <strong>Branding.DisplayTemplates</strong> app for SharePoint project with the URL of the SharePoint site collection you configured in the previous section. You may be prompted to enter your credentials
 to access the site.</p>
</div>
<a name="O15Readme_build">
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection5">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>Press <strong><span class="ui">F5</span> </strong>to run the app.</p>
</li><li>
<p>Sign in to your SharePoint site if you are prompted to do so by the browser.</p>
</li><li>
<p>If you are prompted to trust the self-signed Localhost certificate, click <strong>
<span class="ui">Yes</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 4. Security alert: self-signed certificate</span></strong></div>
<br>
<img id="125165" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125165/1/personalsearch_trustcertificate.png" alt="" width="618" height="273"><br>
<p><br>
You may also be prompted to install the certificate, if so, click <span class="ui">
Yes</span>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 5. Security warning: install certificate</span></strong></div>
<br>
<img id="125166" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125166/1/personalsearch_installcertificate.png" alt="" width="491" height="405"><br>
<p>&nbsp;</p>
</li><li>
<p>On the consent page to grant permissions to the app, select <strong><span class="ui">Trust It</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 6. Grant app permissions</span></strong></div>
<br>
<img id="125167" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125167/1/disptemp_04trustapp.png" alt="" width="793" height="382">
</li><li>
<p><br>
<br>
You should now see the app displayed in the browser. Click <strong><span class="ui">Deploy</span>
</strong>to create the site columns, content type, list, initialize the list with data, and upload the master pages, images, CSS files and display template JavaScript files.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 7. Branding - Display Templates</span></strong></div>
<br>
<img id="125168" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125168/1/disptemp_05brandingdisplaytemplatesui.png" alt="" width="805" height="489"><br>
<p><br>
<br>
Once you've clicked <span class="ui">Deploy</span>, if you want to re-deploy the artifacts, you need to click
<strong><span class="ui">Delete Artifacts</span></strong> to delete the site columns, content type, list, master pages, CSS, images, and display template JavaScript files and then you can click
<span class="ui">Deploy</span> again.</p>
</li></ol>
</div>
</a><a name="sectionSection6"></a>
<h2 class="heading">Next steps</h2>
<div class="section" id="sectionSection6">
<p>Now you're ready to configure the device channels, master pages, and view the results.</p>
<h3 class="procedureSubHeading">To configure the device channels</h3>
<div class="subSection">
<ol>
<li>
<p>Navigate to the SharePoint site and log into the site with Site Owner credentials.</p>
</li><li>
<p>Navigate to <strong><span class="ui">Site Settings</span></strong> and in the
<span class="ui"><strong>Look and Feel</strong></span> section, click <strong><span class="ui">Device Channels</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 8. Device channels link</span></strong><br>
<img id="125169" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125169/1/disptemp_06configuredevicechannels.png" alt="" width="718" height="574"></div>
</li><li>
<p>On the Device Channels page, click <strong><span class="ui">New Item</span></strong>.</p>
</li><li>
<p>Enter information to create the iPad device channel, as shown in figure 7, and click
<strong><span class="ui">Save</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 9. iPad device channel</span></strong></div>
<img id="125170" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125170/1/disptemp_07ipaddevicechannel.png" alt="" width="609" height="566">
</li><li>
<p>Enter information to create the iPhone device channel, as shown in figure 8, and click
<strong><span class="ui">Save</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 10. iPhone device channel</span></strong></div>
<img id="125171" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125171/1/disptemp_08iphonedevicechannel.png" alt="" width="689" height="711">
</li></ol>
</div>
<h3 class="procedureSubHeading">To configure the master pages</h3>
<div class="subSection">
<ol>
<li>
<p>Navigate to the SharePoint site and log into the site with Site Owner credentials.</p>
</li><li>
<p>Navigate to <span class="ui">Site Settings</span> and in the <span class="ui">
<strong>Look and Fee</strong>l</span> section, click <strong><span class="ui">Master page</span></strong>.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 11. Master page link</span></strong></div>
<img id="125172" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125172/1/disptemp_10configuremasterpages.png" alt="" width="360" height="434">
</li><li>
<p>Configure the appropriate master page for each device channel, as shown in figure 10.</p>
<div class="caption"><strong>Figure 12. Configuring the master page for each device channel</strong></div>
<img id="125173" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125173/1/disptemp_10sitemasterpagesettings.png" alt="" width="805" height="236">
</li></ol>
</div>
<p>After you have successfully configured your SharePoint environment and deployed the artifacts using the Branding.DisplayTemplates app for SharePoint, you can view the Home Hero slider in different pages and device channels.</p>
<h3 class="procedureSubHeading">To access the Home Hero slider contents</h3>
<div class="subSection">
<ol>
<li>
<p>Navigate to <strong><span class="ui">Site Contents</span></strong>, as shown in figure 11.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 13. Site contents link</span></strong></div>
<img id="125174" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125174/1/disptemp_11sitecontents.png" alt="" width="216" height="306">
</li><li>
<p>Click the<strong> <span class="ui">Home Hero</span></strong> list, as shown in figure 12.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 14. Home Hero list link</span></strong><br>
<img id="125175" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125175/1/disptemp_12homeherolistlink.png" alt="" width="322" height="175"></div>
<p>Four list items are displayed in the Home Hero slider control as shown in figure 13.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 15. Home Hero list</span></strong></div>
<img id="125176" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125176/1/disptemp_13homeherolistpage.png" alt="" width="457" height="412"><br>
<p><br>
<br>
These list items were created by the Branding.DisplayTemplates app for SharePoint after it provisioned the Home Hero list. You can edit these items or leave the default content. We encourage you to at least view the properties of a list item to see all the
 different things you can configure such as text, images, colors, opacity, and URL.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>If you edit the properties of a list item you must wait until the Search Service crawls the list again to see the changes appear in the Home Hero slider.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li><li>
<div class="caption">If you navigate back to <strong><span class="ui">Site Contents</span></strong> and select the
<strong><span class="ui">Pages</span> </strong>library, you will find three newly created pages:</div>
</li></ol>
</div>
<ul>
<li>
<p>desktop.aspx<span class="ui">&nbsp;Pages</span>&nbsp;</p>
</li><li>
<p>rwd.aspx</p>
</li><li>
<p>channels.aspx</p>
</li></ul>
<div class="caption"><strong><span style="color:#0000ff">Figure 16. New items in the Pages library</span></strong></div>
<img id="125177" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125177/1/disptemp_14pageslibrary.png" alt="" width="320" height="176"><br>
<p><br>
<br>
These pages were created by the Branding.DisplayTemplates app for SharePoint. The next section discusses these pages.</p>
</div>
<a name="sectionSection7"></a>
<h2 class="heading">Pages and channels</h2>
<div class="section" id="sectionSection7">
<p>The pages within the <span class="ui">Pages</span> library render the Home Hero slider with different views. The Home Hero slider is a Content by Search web part. Customized display templates are used to render the data returned via the Search Service.</p>
<div class="alert">
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong></th>
</tr>
<tr>
<td>
<p>The Content by Search web part will only display the data from a SharePoint list after the list items have been crawled and are in the search index.</p>
<p>In an on-premises SharePoint 2013 environment you can manually start a crawl, see
<a href="http://technet.microsoft.com/en-us/library/jj219814(v=office.15).aspx" target="_blank">
Start, pause, resume, or stop a crawl in SharePoint Server 2013</a>.</p>
<p>In a SharePoint online site collection you cannot start the crawl manually, so it may take some time before data from a SharePoint list is in the search index. See this support article for information about how long it may take to crawl the data,
<a href="http://support.microsoft.com/kb/2008449" target="_blank">Search doesn't return all results in SharePoint Online</a>.</p>
</td>
</tr>
</tbody>
</table>
</div>
<p>After crawling is complete the hero control will display properly on pages.</p>
<h3 class="subHeading">Desktop.aspx page</h3>
<div class="subsection">
<p>This page displays the desktop view of the Home Hero slider. Click it to open a page with desktop version of the hero control.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 17. Desktop.aspx page</span></strong></div>
<img id="125178" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125178/1/disptemp_15desktopview.png" alt="" width="807" height="359"><br>
<p><br>
<br>
The Content by Search web part on the desktop.aspx page is configured to use the HomePageHeroControlSlideshow.js control display template. All of the Content by Search web parts use the same control display template.</p>
<p>The Content by Search web part on the desktop.aspx page is configured to use the HomePageHeroItemTemplate.js item display template. The HomePageHeroItemTemplate.js item display template implements a design targeted for desktop web browsers. This approach
 is typically used in an Intranet scenario where the page is not targeting mobile devices. Each Content by Search web part uses a different item template.</p>
</div>
<div class="section" id="sectionSection7">
<h3 class="subHeading">rwp.aspx page</h3>
<div class="subsection">
<p>Click the rwd.aspx page in the <span class="ui">Pages</span> library. This page utilizes responsive web design and displays different views when a browser's screen size changes. Resize your browser to see the responsive web design adapt to your browser's
 width.</p>
<p>In the largest view, (greater than 768 pixels wide and less than or equal to 1168 pixels), the responsive design renders the view shown in figure 16.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 18. Full size rwd.aspx page</span></strong></div>
<img id="125179" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125179/1/disptemp_17defaultchannel.png" alt="" width="805" height="360"><br>
<p><br>
This view is the same as the desktop version on the desktop.aspx page.</p>
<p>In the smaller view (less than or equal to 768 pixels wide), the responsive design renders the view shown in figure 17. Notice the layout and the content changes as well as the overall width of the display.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 19. Smaller size rwd.aspx page</span></strong></div>
<img id="125180" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125180/1/disptemp_16rwdview2.png" alt="" width="472" height="280"><br>
<p><br>
<br>
The Content by Search web part on the rwd.aspx page is configured to use the HomePageHeroControlSlideshow.js control display template. All of the Content by Search web parts use the same control display template.</p>
<p>The Content by Search Web Part on the rwd.aspx page is configured to use the HomePageHeroItemTemplate_rwd.js item display template. The HomePageHeroItemTemplate_rwd.js item display template implements a responsive web design targeted for desktop and mobile
 web browsers. This approach is typically used in a scenario where the page targets desktop browser and mobile devices.</p>
</div>
<h3 class="subHeading">Channels.aspx page</h3>
<div class="subsection">
<p>This sample includes device channels for the following devices: desktop, iPad, and iPhone. To see the default device channel, click channels.aspx in the
<span class="ui">Pages</span> library.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 20. Channels.aspx page</span></strong></div>
<img id="125181" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125181/1/disptemp_17defaultchannel.png" alt="" width="805" height="360"><br>
<br>
<p>To see the different device channel views, append the <span class="parameter">
devicechannel</span> parameter to the URL while browsing channels.aspx:<br>
<span class="code">?devicechannel=&lt;channel&gt;</span></p>
<p>You could also use a developer tool to swap the user agent string to allow you to access different device channels from a desktop web browser or use an iPad or iPhone to access the different device channels.</p>
<p>To render the iPhone channel browse to https:// <span class="parameter">&lt;site URL&gt;</span>/channels.aspx?devicechannel=iphone.</p>
<p>Notice at the top of the page the master page displays some text to let you know which master page is being rendered by the device channel.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 21. iPhone device channel master page</span></strong><br>
<img id="125182" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125182/1/disptemp_18iphonechannel1.png" alt="" width="305" height="116"></div>
<p><br>
<br>
The following code in the Master Page is used to display this text: <br>
<span class="code">&lt;div style=&quot;background-color:lightgreen; width:220px;text-align:center;&quot;&gt;This is the iPhone Master Page&lt;/div&gt;</span></p>
<p>Notice the layout and the content changes as well as the overall width of the display.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 22. iPhone device channel</span></strong></div>
<img id="125184" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125184/1/disptemp_19iphonechannel2.png" alt="" width="502" height="276"><br>
<p>To render the iPad channel browse to: https:/ <span class="parameter">&lt;site URL&gt;</span>/channels.aspx?devicechannel=ipad. The images on the hero control are rendered larger than those for the iPhone device channel. Also notice that the layout and
 the content changes, as well as the overall width of the display.</p>
<div class="caption"><strong><span style="color:#0000ff">Figure 23. iPad device channel</span></strong></div>
<img id="125185" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-branding-e25667a5/image/file/125185/1/disptemp_20ipadchannel2.png" alt="" width="604" height="346"><br>
<br>
</div>
<h3 class="subHeading">How the device channels, master pages, CSS, and display templates work together</h3>
<div class="subsection">
<p>The Content by Search web part on the channels.aspx page is configured to use the HomePageHeroControlSlideshow.js control display template. All of the Content by Search Web Parts share the same control display template. This control template sets up the
 overall container for the content displayed in the Content by Search web part. The control template also includes the code and logic used to cycle the different list items like a slide show.</p>
<p>The Content by Search Web Part on the channels.aspx page is configured to use the HomePageHeroItemTemplate_channel.js item display template. The HomePageHeroItemTemplate_channel.js item display template implements a design targeted for desktop and mobile
 web browsers. This approach is typically used in a scenario where the page targets desktop browsers and mobile devices and you wish to deliver the smallest amount of page payload as possible to make pages load as fast as possible.</p>
<p>Three master pages are used to implement this approach.</p>
<ul>
<li>
<p>Desktop.master</p>
</li><li>
<p>iPad.master</p>
</li><li>
<p>iPhone.master</p>
</li></ul>
<p>When the page is loaded in the default device channel, desktop.master is used. Desktop.master is also used for default.aspx and rwd.aspx. This master page loads the hero_desktop.css CSS file. This CSS file contains styles specific to the desktop version
 of the Home Hero slider.</p>
<p>When the page is loaded in the iPad device channel, iPad.master is used. This master page loads the hero_ipad.css CSS file. This CSS file contains styles specific to the iPad version of the Home Hero slider.</p>
<p>When the page is loaded in the iPhone Device Channel, iPhone.master is used. This master page loads the hero_iphone.css CSS file. This CSS file contains styles specific to the iPhone version of the Home Hero slider.</p>
<p>The HomePageHeroItemTemplate_channel.js item display template is used for all three device channels, it uses the CSS styles it inherits from the CSS file the master page includes.</p>
</div>
</div>
<a name="O15Readme_Changelog">
<h2 class="heading">Change log</h2>
<div class="section" id="sectionSection8">
<p>First release.</p>
</div>
</a><a name="O15Readme_RelatedContent">
<h2 class="heading">Related content</h2>
</a>
<div class="section" id="sectionSection9"><a name="O15Readme_RelatedContent"></a>
<ul>
<a name="O15Readme_RelatedContent"></a>
<li><a name="O15Readme_RelatedContent"></a>
<p><a name="O15Readme_RelatedContent"></a><a href="http://msdn.microsoft.com/en-us/library/jj945138.aspx" target="_blank">SharePoint 2013 Design Manager display templates</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj862343(v=office.15).aspx" target="_blank">SharePoint 2013 Design Manager device channels</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj720398(v=office.15).aspx" target="_blank">SharePoint 2013 Design Manager image renditions</a></p>
</li><li>
<p><a href="http://www.microsoft.com/en-us/download/details.aspx?id=42030" target="_blank">SharePoint 2013 and SharePoint Online solution packsSharePoint 2013 and SharePoint Online solution packs</a>:</p>
<ul>
<li>
<p>Search Solution Pack\Search Module 1 - Introduction.docx</p>
</li><li>
<p>Search Solution Pack \Search Module 2 - Customizations.docx</p>
</li><li>
<p>Branding and Site Provisioning Solution Pack Solution Pack\Branding and Site Provisioning Module 7-- Metadata Navigation Publishing.docx</p>
</li></ul>
</li></ul>
</div>
<div class="subsection"></div>
</div>
</div>
</div>
