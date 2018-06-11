# SharePoint 2010: Importing Site Definitions in Visual Studio 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
* SharePoint Designer 2010
## Topics
* site definitions
## IsPublished
* True
## ModifiedDate
* 2011-08-04 02:47:54
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to import a Microsoft SharePoint 2010 site definition using Microsoft Visual Studio 2010 and Microsoft SharePoint Designer 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/5dc47db1-61a9-4d84-be3b-89e153325561.aspx">
Importing SharePoint 2010 Site Definitions in Visual Studio 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Visual Studio 2010 provides a project type that enables you to import a site definition and add code. You can then deploy this site definition to the SharePoint Solutions Gallery and create new sites based on the site definition.</p>
<p>This sample and the accompanying article demonstrate the following tasks:</p>
<ul>
<li>Creating a blank SharePoint 2010 site as a subsite from the SharePoint master site. This site is opened in SharePoint Designer 2010 for customization.
</li><li>Editing the site in SharePoint Designer and creating a new document library for candidate resumes. The new document library has an additional
<strong>Yes/No</strong> column added that indicates whether the person who presented the resume has been interviewed.
</li><li>Saving the modified site as a template in SharePoint Designer. </li><li>Locating the modified template in SharePoint by clicking <strong>Site Settings</strong>, clicking
<strong>Galleries</strong>, and then clicking <strong>Solutions</strong>. The template is saved as a WSP file.
</li><li>Opening and customizing the WSP file in Visual Studio 2010. </li><li>Adding a Web Part to the package that uses a Collaborative Application Markup Language (CAML) query to determine the number of resumes that have resulted in an interview and the number that have not resulted in an interview. These results are displayed
 on a literal control as a percentage. </li><li>Saving the solution as a WSP file in Visual Studio 2010. </li><li>Uploading the new template and removing the existing templates. </li><li>Creating a new site using the customized Recruitment site template. </li><li>Uploading documents to the Resumes document library and setting the Interviewed column appropriately.
</li><li>Editing a page and selecting the Web Part that was added to the package in Visual Studio 2010.
</li></ul>
