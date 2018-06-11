# GetOneDrive files SharePoint 2013 sample for Visual Studio 2013
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* apps for SharePoint
* Office 365 Enterprise
## Topics
* SharePoint
## IsPublished
* True
## ModifiedDate
* 2014-02-13 10:58:46
## Description

<div id="header">Demonstrates how to build and deploy a provider hosted SharePoint app using the .NET client API for SharePoint 2013 to get OneDrive Pro folder contents.</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>The project is organized into view (pages folder), view model (ViewModel folder), and model (DataModel folder). The code that uses the managed SharePoint APIs is in the OneDriveProDocs.cs file. The Scripts folder contains the ChromeLoader.js file for showing
 SP chrome on the UI.</p>
<p>The view model is composed of an Item class that describes an individual file from OneDrive Pro and an Items class that encapsulates an observable collection of Item that is consumed by the view layer. In the model layer, each OneDrive Pro folder is queried
 for contained files. The combined file collection is displayed in a web UI. The logic in this solution works with your OneDrive Pro site and your team site without any code change.</p>
<p>The UI logic instantiates the view model items class by calling the items constructor. The constructor calls the model _oneDriveModel.Run() method. Run connects to OneDrive Pro and gets the files and folders to fill the Items observable collection. A list
 on the UI is bound to the observable collection and fills when the collection fills.</p>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection0">
<p>This sample requires the following:</p>
<ul>
<li>
<p>Visual Studio 2012</p>
</li><li>
<p>Office Developer Tools for Visual Studio 2012</p>
</li><li>
<p>Either of the following:</p>
<ul>
<li>
<p>SharePoint Server 2013 configured to host apps, and with a Developer Site Collection already created; or,</p>
</li><li>
<p>Access to an Office 365 developer site configured to host apps.</p>
</li></ul>
</li></ul>
</div>
<h1>Key components of the sample</h1>
<div id="sectionSection1">
<p>The sample app contains the following:</p>
<ul>
<li>
<p>The <strong>getonedrivefilesWeb</strong> folder, which contains the web project and associated source files.</p>
</li><li>
<p>The <strong>getonedriveFilesWeb\DataModel</strong> folder, which contains OneDriveProDics.cs.</p>
</li><li>
<p>The <strong>getonedriveFilesWeb\ViewModel</strong> folder, which contains the source files for the view model classes.</p>
</li></ul>
<p>All other files are automatically provided by the Visual Studio project template for apps for SharePoint, and they have not been modified in the development of this sample.</p>
</div>
<h1>Configure the sample</h1>
<div id="sectionSection2">
<p>Follow these steps to configure the sample.</p>
<div>
<ol>
<li>
<p>Use the <a href="https://manage.windowsazure.com" target="_blank">Windows Azure Management Portal</a> to set up an Azure provider host web site. Copy the new domain URL to notepad. You will need to paste the URL into the App Domain field in the next step.</p>
<strong>
<div class="caption">Figure 3: Create a new website in Azure</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108527/1/image.png" alt="">
<p>&nbsp;</p>
</li><li>
<p>Use the Office 365 development site Appregnew.aspx page to register your SharePoint app with SharePoint. Copy the generated
<span>client Id</span> and <span>client secret</span> values to notepad. Paste the domain URL into the App Domain field.</p>
<strong>
<div class="caption">Figure 4. The SharePoint AppRegNew.aspx page.</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108529/1/image.png" alt="">
<p>&nbsp;</p>
</li><li>
<p>Set the Project Url property to the URL of the provider host domain in the Properties page of the web project.</p>
<strong>
<div class="caption">Figure 7. Visual Studio, new project dialog</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108525/1/image.png" alt="">
<p>&nbsp;</p>
</li><li>
<p>Update the SharePoint web app project Web.config file to hold the client ID and client secret values that you got when you register the SharePoint app.</p>
<strong>
<div class="caption">Figure 8. Web app, web.config XML file</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108530/1/image.png" alt=""> </li></ol>
</div>
</div>
<h1>Deploy the sample to OneDrive Pro</h1>
<div id="sectionSection3">
<p>Deployment of a new provider-hosted SharePoint app includes the following steps:</p>
<ul>
<li>
<p>Download an Azure publish profile from the Azure management portal</p>
</li><li>
<p>In Visual Studio, publish the web app components to the Azure provider host web</p>
</li><li>
<p>In Visual Studio, publish the SharePoint component of the app solution to the SharePoint app catalog</p>
</li><li>
<p>In your Office 365 OneDrive Pro site, deploy the app from the SharePoint catalog to a SharePoint site</p>
</li></ul>
<h3>Download a provider-host publish profile from Azure</h3>
<div>
<p>After Azure creates the new web site and starts it, click the <span>Publish your app</span> link to download the web site publishing profile from
<a href="https://manage.windowsazure.com" target="_blank">Windows Azure Management Portal</a>. The profile is the publication parameter source when the profile is imported into Visual Studio using the web app publication dialog.</p>
<strong>
<div class="caption">Figure 10. Download an Azure publishing profile</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108532/1/image.png" alt=""></div>
<h3>Publish the remote web app to Azure</h3>
<div>
<p>Now that you've configured both projects and registered the SharePoint app, you can publish the remote web app.</p>
<ol>
<li>
<p>Open the project web publish dialog</p>
<strong>
<div class="caption">Figure 11. Open publish dialog for web project</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108533/1/image.png" alt=""> </li><li>
<p>Import the publish settings that you downloaded from Azure</p>
<strong>
<div class="caption">Figure 12. Import the Azure publishing profile</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108534/1/image.png" alt=""> </li><li>
<p>Verify that the connection settings are correct by clicking the Validate Connection button.</p>
<strong>
<div class="caption">Figure 13. Verify Azure publish profile attributes</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108528/1/image.png" alt="">
<p><br>
<span>Note:</span> Be sure to change the web protocol in the provider host URL. Azure creates identical web sites for HTTP and HTTP secure (HTTPS). A provider hosted SharePoint web component must be hosted on an HTTPS site.</p>
</li><li>
<p>Click the publish button and you'll know the web site is published when you see the following web page</p>
<strong>
<div class="caption">Figure 14. Web app is published on Azure</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108531/1/image.png" alt=""> </li></ol>
</div>
<h3>Build the SharePoint app</h3>
<div>
<p>Publishing the SharePoint app component of the solution is a two step process that includes:</p>
<ul>
<li>
<p>Building the publishing app package</p>
</li><li>
<p>Uploading the app to the OneDrive Pro app catalog</p>
</li></ul>
<p>&nbsp;</p>
<ol>
<li>
<p>To build the app, answer the prompts in the Visual Studio SharePoint project publish dialog.</p>
<strong>
<div class="caption">Figure 15. SharePoint app project publish dialog - profile tab</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108539/1/image.png" alt=""> </li><li>
<p>Use the client ID and client secret from SharePoint app registration to fill the prompts on the hosting tab.</p>
<strong>
<div class="caption">Figure 16. SharePoint app project publish dialog - hosting tab</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108540/1/image.png" alt="">
<p><br>
After you click Finish in the publication dialog, Visual Studio builds the app.</p>
</li><li>
<p>Open the build output folder on your development computer</p>
<strong>
<div class="caption">Figure 17. App build output folder</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108538/1/image.png" alt=""> </li></ol>
<p>&nbsp;</p>
</div>
<h3>Publish the app to the OneDrive Pro app catalog</h3>
<div>
<p>Sign in to your Office 365 developer site and go to the Team site. To add the new app to the OneDrive Pro app catalog, open the catalog using the following steps:</p>
<ol>
<li>
<p>From the team site, click <span>OneDrive</span> in the SharePoint navigation ribbon.</p>
</li><li>
<p>Click on the Admin link in the nav. ribbon and then click the <span>SharePoint</span> menu item</p>
</li><li>
<p>Click <span>apps</span> from the list on the left side of the SharePoint admin center page</p>
</li><li>
<p>Click <span>App catalog</span> under apps.</p>
</li></ol>
<p>You can distribute both SharePoint apps and Office apps from the OneDrive Pro app catalog. To distribute your SharePoint app, click
<span>Distribute apps for SharePoint</span> to open the SharePoint app list.</p>
<p>Click <span>new app</span> to open the <span>Add a document</span> dialog. You can also drag the app file from the build output folder onto the Apps for SharePoint page and drop the app in the list on the page.</p>
<strong>
<div class="caption">Figure 18. Add app to SharePoint dialog</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108526/1/image.png" alt="">
<p>After you have chosen your app from the dialog or dragged the app onto the page, SharePoint uploads your app file and then adds it to the list. Once SharePoint has finished adding it to the list, a user can deploy your app.</p>
</div>
<h3>Deploy the SharePoint app to your OneDrive Pro site</h3>
<div>
<p>Now that you have added your app to the SharePoint app catalog, a user can deploy it to their OneDrive Pro site or their team site. The following procedure deploys a an app to a user's OneDrivePro site.</p>
<ol>
<li>
<p>Click the <span>Site Contents</span> link on the left column of the page</p>
</li><li>
<p>Click <span>add an app</span></p>
<strong>
<div class="caption">Figure 19. OneDrive Pro site contents list</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108535/1/image.png" alt=""> </li><li>
<p>Click on the app you want to deploy</p>
<strong>
<div class="caption">Figure 20. Choose the app to deploy</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108537/1/image.png" alt=""> </li><li>
<p>Review the site action permissions that the app is requesting and then click <span>
Trust It</span> if you want to grant the requested permissions.</p>
<strong>
<div class="caption">Figure 21. App trust dialog</div>
</strong><br>
<strong>&nbsp;</strong><img src="/site/view/file/108536/1/image.png" alt=""> </li></ol>
<p>If you chose to trust the app then it is now deployed to your site and you can use it.</p>
</div>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection4">
<p>To run and test the sample, do the following:</p>
<ol>
<li>
<p>Press F5 to run the app.</p>
</li><li>
<p>Sign in to your SharePoint Server 2013 Developer Site collection or Office 365 Developer Site if you are prompted to do so by the browser.</p>
</li></ol>
</div>
<h1>Change log</h1>
<div id="sectionSection6"><strong>
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
<p>July 16, 2012</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h1>Related content</h1>
<div id="sectionSection7">
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179930.aspx" target="_blank">Apps for SharePoint overview</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp161179.aspx" target="_blank">How to: Set up an environment for developing apps for SharePoint on Office 365</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/apps/fp179923.aspx" target="_blank">How to: Set up an on-premises development environment for apps for SharePoint</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/fp179924.aspx" target="_blank">Sign up for an Office 365 Developer Site</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/library/office/jj193041.aspx" target="_blank">.NET client API reference for SharePoint 2013</a></p>
</li></ul>
</div>
</div>
</div>
