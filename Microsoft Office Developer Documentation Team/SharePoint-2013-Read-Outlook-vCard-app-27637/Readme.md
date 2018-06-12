# SharePoint 2013: Read Outlook vCard app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Sharepoint Online
* SharePoint Server 2013
* SharePoint Foundation 2013
## Topics
* User Profile
## IsPublished
* True
## ModifiedDate
* 2014-03-11 02:26:45
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText">&nbsp;</span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">SharePoint 2013 read Outlook vCard app</span></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>Demonstrates a SharePoint-hosted app for SharePoint that allows the user to download Microsoft Outlook profile information.</p>
</div>
<div>
<p><strong>Last modified: </strong>February 10, 2014</p>
<p><strong>In this article</strong> <br>
<a href="#sectionSection0">Prerequisites</a> <br>
<a href="#sectionSection1">Key components</a> <br>
<a href="#sectionSection2">Build the sample</a> <br>
<a href="#sectionSection3">Run and test the sample</a> <br>
<a href="#sectionSection4">Troubleshooting</a> <br>
<a href="#sectionSection5">Change log</a> <br>
<a href="#sectionSection6">Related content</a></p>
<p><span>Provided by:</span> Vivek Soni, <a href="http://www.microsoft.com/india/msindia/msindia_aboutus_msgd.aspx" target="_blank">
Microsoft Services Global Delivery</a></p>
<p>This app has two roles, administrators and users. An administrator defines which user profile properties will be visible, and attaches this app's web part to the SharePoint MySite landing page (<strong><span class="keyword">Person.aspx</span></strong>).</p>
<p>A user visits a colleague's MySite profile page and can download the Outlook vCard (.vcf file) of that colleague. The downloaded vCard contains only the information that the administrator chose to include. The user can then save the vCard to his or her Outlook
 contacts. This app also posts a note to the colleague's news feed, letting them know their vCard was downloaded.</p>
<p>This app demonstrates how to use JavaScript and jQuery with the SharePoint User Profile Service and a MySite collection, and how to post to a news feed. This app also shows client-side rendering using JSLink, JsRender, and HTML5.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample app requires the following:</p>
<ul>
<li>
<p>Microsoft Visual Studio 2012</p>
</li><li>
<p>Microsoft Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Internet Explorer (version 10 or greater), or Google Chrome browser</p>
</li><li>
<p>Either of the following:</p>
<ul>
<li>
<p>Access to an Office 365 Enterprise site that has been configured to host apps (recommended).</p>
</li><li>
<p>SharePoint Server 2013 configured to host apps.</p>
</li></ul>
</li><li>
<p>In the Office 365 Enterprise or SharePoint Server 2013 site:</p>
<ul>
<li>
<p>SharePoint User Profiles must be configured.</p>
</li><li>
<p>Users must have their MySite pages created.</p>
</li></ul>
</li><li>
<p>Optional: an Outlook client to open the vCard.</p>
</li></ul>
</div>
<h1>Key components</h1>
<div id="sectionSection1">
<p>This sample app contains the following key components:</p>
<ul>
<li>
<p>A set of pages in the <strong><span class="keyword">pages</span></strong> folder that specify the administrator and user interfaces:</p>
<ul>
<li>
<p><strong><span class="keyword">Default.aspx</span> </strong></p>
<p>The landing page for the administrator, which provides access to the app functionality.</p>
</li><li>
<p><strong><span class="keyword">viewmappingdata.aspx</span> </strong></p>
<p>Part of the administrator console, this page allows the administrator to view the values for the configured profile properties for a specified user. Administrators can use this page to preview which values are returned for the configured set of profile properties.</p>
</li><li>
<p><strong><span class="keyword">Configurations.aspx</span> </strong></p>
<p>Part of the administrator console, this page allows an Administrator to configure the app-level settings, as shown in Figure 5, and save them in the Configurations list.</p>
</li><li>
<p><strong><span class="keyword">DownloadvCard.aspx</span> </strong></p>
<p>This page is hosted in the app part, allowing the end user to download the vCard.</p>
</li></ul>
</li><li>
<p>A set of scripts in the <strong><span class="keyword">scripts</span></strong> folder that implement the administrator and user actions:</p>
<ul>
<li>
<p><strong><span class="keyword">vCard_landing.js</span> </strong></p>
<p>Contains the basic JavaScript functions to get the site URL, and to set the URLs of the New and All Items forms for the vCard Properties list for the buttons on the landing page.</p>
</li><li>
<p><strong><span class="keyword">vCard_helper.js</span> </strong></p>
<p>Contains the helper JavaScript functions to read the configured user profile properties from the vCard Properties list, and then query the User Profile Service to fetch the values for those properties.</p>
</li><li>
<p><strong><span class="keyword">vCard_configurations.js</span> </strong></p>
<p>Contains the JavaScript functions to create and attach data (such as the profile property dropdown) to the HTML controls in the Configurations page. Other functions read and write configuration values to the Configurations list.</p>
</li><li>
<p><strong><span class="keyword">vCard_viewDataMappings.js</span> </strong></p>
<p>Assists the functioning of the <strong><span class="keyword">viewmappingdata.aspx</span></strong> page. This file contains the JavaScript functions to create a client-side people picker control, to query the User Profile Service to get property values
 for the selected user in the people picker, and then to use JsRender to write those values to the UI in tabular format.</p>
</li><li>
<p><strong><span class="keyword">vcard_clienttemplates.js</span> </strong></p>
<p>Contains the JavaScript functions to override the client-side rendering of certain fields in the vCard Properties list when the list is rendered in different list forms, such as the New Item or Edit Item forms.</p>
</li><li>
<p><strong><span class="keyword">vCard_vcfCreator.js</span> </strong></p>
<p>This is the main JavaScript file which is rendered as a part of the app part. It contains the functions to read the configured profile property values of the currently visited user, get the user profile image, create the
<strong><span class="keyword">.vcf</span></strong> format for these profile properties along with their values, and then use the HTML5 &quot;blob&quot; API to push this
<strong><span class="keyword">.vcf</span></strong> card as a download when the user clicks it. Also, it contains the functions to post a news feed when a user downloads the vcard.</p>
</li></ul>
</li><li>
<p>Several related folders:</p>
<ul>
<li>
<p><strong><span class="keyword">vCardProperties</span> </strong></p>
<p>Stores the collection of all the configured user profile properties along with the mapping to their corresponding Outlook fields.</p>
</li><li>
<p><strong><span class="keyword">Configurations</span> </strong></p>
<p>Stores the ap- level configuration settings and their values - see Figure 5.</p>
</li><li>
<p><strong><span class="keyword">TileList</span> </strong></p>
<p>This is a Promoted Links list that creates button tiles on the administrator console landing page (<strong><span class="keyword">Default.aspx</span></strong>).</p>
</li></ul>
</li><li>
<p>A set of custom action definitions in the <strong><span class="keyword">CustomActions</span></strong> folder:</p>
<ul>
<li>
<p><strong><span class="keyword">RibbonCustomActions</span> </strong></p>
<p>Overrides the out-of-box ribbon buttons (such as New and Edit) for the vCard Properties list.</p>
</li><li>
<p><strong><span class="keyword">CustomActions</span> </strong></p>
<p>ScriptLink custom action to insert the required scripts into the app pages.</p>
</li></ul>
</li><li>
<p>The following external files are included, containing support functions.</p>
<ul>
<li>
<p>jquery-ui.css</p>
</li><li>
<p>jquery.multiselect.css</p>
</li><li>
<p>jquery-ui.js</p>
</li><li>
<p>jquery-zmultiselect.js</p>
</li><li>
<p>jsrender.js</p>
</li><li>
<p>jquery-1.10.2.min.js</p>
</li></ul>
</li><li>
<p>All other files are automatically provided by the Visual Studio 2012 project template for apps for SharePoint, and they have not been modified in the development of this sample app.</p>
</li></ul>
</div>
<h1>Build the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to build the sample.</p>
<div>
<ol>
<li>
<p>Press Ctrl&#43;Shift&#43;B to build the solution.</p>
</li><li>
<p>Press F5 to run the app.</p>
</li><li>
<p>If you are prompted by the browser, sign in to your SharePoint Server 2013 or Office 365 Enterprise site.</p>
</li></ol>
</div>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection3">
<div>
<ol>
<li>
<p>This app uses the existing SharePoint site privilege level of the incoming user to determine the initial app page. An administrator (admin privilege) sees the screen shown in Figure 1. A non-admin user will see the screen shown in step 7.</p>
<strong>
<div class="caption">Figure 1. Administrator start screen</div>
</strong><br>
<img src="/site/view/file/110460/1/image.png" alt="">
<p>Administrators can perform the following actions.</p>
</li><li>
<p><strong><span class="ui">Add New Mapping</span> </strong>adds a new mapping from a User Profile property (populated from the profile store) to the corresponding vCard file (<strong><span class="keyword">.vcf</span></strong>) property. Figure 2 shows
 an example mapping.</p>
<strong>
<div class="caption">Figure 2. Example property mapping</div>
</strong><br>
<img src="/site/view/file/110461/1/image.png" alt=""> </li><li>
<p><strong><span class="ui">View Mappings</span> </strong>lists all current mappings, as shown in Figure 3.</p>
<strong>
<div class="caption">Figure 3. Current mappings</div>
</strong><br>
<img src="/site/view/file/110462/1/image.png" alt=""> </li><li>
<p><strong><span class="ui">View Mapping With User Profile Data</span> </strong>
verifies that the User Profile data is returned in the expected format. Figure 4 shows an example.</p>
<strong>
<div class="caption">Figure 4. Example mapping with user data</div>
</strong><br>
<img src="/site/view/file/110463/1/image.png" alt=""> </li><li>
<p><strong><span class="ui">Manage Configuration</span> </strong>configures the app-level settings, as shown in Figure 5.</p>
<strong>
<div class="caption">Figure 5. App-level configuration settings</div>
</strong><br>
<img src="/site/view/file/110464/1/image.png" alt=""> </li><li>
<p>Finally, after setting up and configuring the app, the administrator adds the app's web part to the public profile page (<strong><span class="keyword">Person.aspx</span></strong>):</p>
<ol>
<li>
<p>Navigate to the <strong><span class="keyword">Person.aspx</span></strong> page.</p>
</li><li>
<p>From the <strong><span class="keyword">Settings</span></strong> link on the top right corner, click
<strong><span class="keyword">Edit page</span></strong>.</p>
</li><li>
<p>Click the <strong><span class="keyword">Add a Web Part</span></strong> link in the desired web part zone.</p>
</li><li>
<p>On the ribbon, under the <strong><span class="keyword">Apps</span></strong> category, select
<strong><span class="keyword">Download vCard</span></strong> and click <strong>
<span class="keyword">Add</span></strong>.</p>
</li><li>
<p>Once the app part is added on the page, turn off the app part title: open the web part properties for the app aart and set the Chrome Type to
<strong><span class="keyword">None</span></strong>.</p>
</li><li>
<p>Exit <strong><span class="keyword">Person.aspx</span></strong> page design mode.</p>
</li></ol>
<p>Figure 6 shows the app icon image.</p>
<strong>
<div class="caption">Figure 6. App icon</div>
</strong><br>
<img src="/site/view/file/110465/1/image.png" alt=""> </li><li>
<p>When a user visits a colleague's profile page, he or she can click on the <strong>
<span class="ui">Download my vCard</span></strong> button in the app part. The app then creates a vCard based on the visited user's profile data and pushes that vCard to the client as a download, as shown in Figure 7.</p>
<p>The user can then open and save the vCard in his or her Outlook contacts.</p>
<strong>
<div class="caption">Figure 7. User view of colleague's profile page</div>
</strong><br>
<img src="/site/view/file/110466/1/image.png" alt=""> </li><li>
<p>If the administrator has configured the app to post to a news feed (see step 5), the colleague will see an item similar to that shown in Figure 8.</p>
<strong>
<div class="caption">Figure 8. Example news feed item</div>
</strong><br>
<img src="/site/view/file/110467/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Troubleshooting</h1>
<div id="sectionSection4">
<p>Verify that you have access to a SharePoint site, and that the user profiles are configured.</p>
</div>
<h1>Change log</h1>
<div id="sectionSection5"><strong>
<div class="caption"></div>
</strong>
<div>
<table cellspacing="2" cellpadding="5" width="50%" frame="lhs">
<tbody>
<tr>
<th>
<p>Version</p>
</th>
<th>
<p>Date</p>
</th>
</tr>
<tr>
<td>
<p>First version</p>
</td>
<td>
<p>February 2014</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection6">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/jj920104.aspx" target="_blank">Retrieve user profile properties by using the JavaScript object model in SharePoint 2013</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/ms970435.aspx" target="_blank">JavaScript</a></p>
</li><li>
<p><a href="http://www.jQuery.com" target="_blank">jQuery</a></p>
</li><li>
<p><a href="http://code.msdn.microsoft.com/office/Client-side-rendering-JS-2ed3538a#content" target="_blank">JSLink</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/magazine/hh882454.aspx" target="_blank">JsRender</a></p>
</li><li>
<p><a href="http://bing.com?q=HTML5" target="_blank">HTML5</a></p>
</li></ul>
</div>
</div>
</div>
<p>&nbsp;</p>
