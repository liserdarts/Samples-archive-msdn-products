# SharePoint 2013: Personalizing search results in an app for SharePoint
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* apps for SharePoint
## Topics
* Search
* Configuration
* SharePoint client object model (CSOM)
## IsPublished
* True
## ModifiedDate
* 2014-08-29 10:40:35
## Description

<div id="header">
<table id="bottomTable" cellspacing="0" cellpadding="0">
<tbody>
<tr id="headerTableRow2">
<td align="left"><strong><span id="nsrTitle" style="font-size:small">SharePoint 2013: Personalizing search results using the search query CSOM in an app for SharePoint</span></strong></td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div class="summary">
<p><span class="label">Summary:</span>&nbsp;&nbsp;Learn how to use the SharePoint search query CSOM to return search results in an app for SharePoint.</p>
</div>
<div class="introduction">
<p><strong>Last modified:&nbsp;</strong>August 26, 2014</p>
<p><strong>In this article</strong><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#O15Readme_Description">Description of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#O15Readme_Prereq">Prerequisites</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#O15Readme_components">Key components of the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#sectionSection3">Configure the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#O15Readme_build">Run and test the sample</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#sectionSection5">Next steps</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#O15Readme_Changelog">Change log</a><br>
<a href="file://ipoawsfs201/DropZone/Rawhide/FileDropOff/jowest/Search.PersonalizedResults/SearchPersonalizedResults.htm#O15Readme_RelatedContent">Related content</a></p>
<p>&nbsp;</p>
</div>
<a name="O15Readme_Description">
<h2 class="heading">Description of the sample</h2>
<div class="section" id="sectionSection0">
<p>This sample app for SharePoint shows how to use the search query CSOM to return search results, first just based on the user's query, and second, personalized based on the user submitting the query.</p>
<p>The basic search example allows the user to provide a search filter to be used for a tenant-wide search and is looking for sites that apply to the user-supplied filter.</p>
<p>The personalized search results example loads your user profile properties and checks for &quot;Apptest&quot; in the AboutMe profile property. If it is found, a list of site templates of any type is returned in the search results. If it is not found, only STS web
 templates are returned in the search results.</p>
</div>
</a><a name="O15Readme_Prereq">
<h2 class="heading">Prerequisites</h2>
<div class="section" id="sectionSection1">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012 or Visual Studio 2013</p>
</li><li>
<p>Microsoft Office Developer tools for Visual Studio 2012 or Visual Studio 2013</p>
</li><li>
<p>SharePoint 2013 development environment</p>
</li></ul>
</div>
</a><a name="O15Readme_components">
<h2 class="heading">Key components of the sample</h2>
<div class="section" id="sectionSection2">
<p>The sample app contains the following:</p>
<ul>
<li>
<p><strong>Search.PersonalizedResults</strong>&nbsp;project, the app for SharePoint project.</p>
</li><li>
<p><strong>Search.PersonalizedResultsWeb</strong>&nbsp;project, the ASP.NET web application project.</p>
</li><li>
<p><strong>Default.aspx.</strong>&nbsp;located in the Search.PersonalizedSearchResults\PersonalizedSearchResultsWeb\Pages directory, which contains the HTML and ASP.NET controls for the sample's user interface</p>
</li><li>
<p><strong>Default.aspx.cs</strong>&nbsp;located the PersonalizedSearchResultsWeb\Pages directory, which contains the code that submits both the basic search and the personalized search queries to SharePoint. The code also parses and writes out the search results
 returned.</p>
</li><li>
<p><strong>SharePointContext.cs</strong>&nbsp;located in the Search.PersonalizedSearchResults\PersonalizedSearchResultsWeb directory, which encapsulates the information that the sample app needs to get from SharePoint.</p>
</li></ul>
</div>
</a><a name="sectionSection3"></a>
<h2 class="heading">Configure the sample</h2>
<div class="section" id="sectionSection3">
<p>Update the&nbsp;<span class="ui">Site URL</span>&nbsp;property of the&nbsp;<strong>Search.PersonalizedResults</strong>&nbsp;app for SharePoint project with the URL of your SharePoint site. You may be prompted to enter your credentials to access the site.</p>
</div>
<a name="O15Readme_build">
<h2 class="heading">Run and test the sample</h2>
<div class="section" id="sectionSection4">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>Press&nbsp;<span class="ui">F5</span>&nbsp;to run the app.</p>
</li><li>
<p>Sign in to your SharePoint site if you are prompted to do so by the browser.</p>
</li><li>
<p>If you are prompted to trust the self-signed Localhost certificate, click&nbsp;<span class="ui">Yes</span>.</p>
<p class="caption">Figure 1. Security alert: self-signed certificate.</p>
<p><img id="124540" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-personalizi-fb6ddcf9/image/file/124540/1/personalsearch_trustcertificate.png" alt="" width="618" height="273"><br>
<br>
You may also be prompted to install the certificate, if so, click&nbsp;<span class="ui">Yes</span>.</p>
<p class="caption">Figure 2. Security warning: install certificate.</p>
<p><img id="124541" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-personalizi-fb6ddcf9/image/file/124541/1/personalsearch_installcertificate.png" alt="" width="491" height="405"></p>
<p>&nbsp;</p>
</li><li>
<p>On the consent page to grant permissions to the app, select&nbsp;<span class="ui">Trust It</span>.</p>
<div class="caption">Figure 3. Grant app permissions</div>
<br>
<img id="124547" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-personalizi-fb6ddcf9/image/file/124547/1/personalsearch_trustapp.png" alt="" width="708" height="253">
</li></ol>
<p><br>
<br>
You should now see the app displayed in the browser.</p>
<div class="caption">Figure 4. Personalized search results app.</div>
<div class="caption"></div>
<p class="caption"><img id="124543" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-personalizi-fb6ddcf9/image/file/124543/1/personalsearch_apppage.png" alt="" width="1033" height="487"></p>
<br>
<br>
</div>
</a><a name="sectionSection5"></a>
<h2 class="heading">Next steps</h2>
<div class="section" id="sectionSection5">
<p>See&nbsp;<a href="http://msdn.microsoft.com/en-us/library/office/fp179933(v=office.15).aspx" target="_blank">Deploying and installing apps for SharePoint: methods and options</a>&nbsp;for instructions on how to publish your app.</p>
</div>
<a name="O15Readme_Changelog">
<h2 class="heading">Change log</h2>
<div class="section" id="sectionSection6">
<p>First release.</p>
</div>
</a><a name="O15Readme_RelatedContent">
<h2 class="heading">Related content</h2>
</a>
<div class="section" id="sectionSection7"><a name="O15Readme_RelatedContent"></a>
<ul>
<a name="O15Readme_RelatedContent"></a>
<li><a name="O15Readme_RelatedContent"></a>
<p><a name="O15Readme_RelatedContent"></a><a href="http://msdn.microsoft.com/en-us/library/office/dn423226(v=office.15).aspx" target="_blank">Using the SharePoint 2013 search Query APIs</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/dn554260(v=office.15).aspx" target="_blank">Search apps in SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
