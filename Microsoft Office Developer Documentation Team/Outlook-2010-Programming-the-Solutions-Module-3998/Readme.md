# Outlook 2010: Programming the Solutions Module
## Requires
* Visual Studio 2008
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* Solutions Module
* custom modules
## IsPublished
* True
## ModifiedDate
* 2011-08-03 11:52:29
## Description

<h2><strong>Introduction</strong></h2>
<p>This project contains a sample Microsoft Outlook 2010 add-in that demonstrates how to turn on the Solutions module programmatically and customize the folder icons for the Solutions module. This code sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ee692173(office.14).aspx">Programming the Outlook 2010 Solutions Module</a> in the MSDN library.</p>
<p>There is one method in the sample code, <strong>EnsureSolutionsModule</strong>, that is called from the
<strong>Startup</strong> method of the <strong>ThisAddin</strong> class. The <strong>
Startup</strong> method for the Solutions module add-in typically runs when Outlook starts, and guarantees visibility of your solution during a first-run condition and every time that Outlook starts after that.</p>
<h2><strong>Description</strong></h2>
<p>Over the years, developers have requested the ability to add custom modules to the Navigation Pane in Outlook. Microsoft Outlook 2010 provides that capability through the Solutions module object model. The Solutions module is a navigation module that you
 can add to the Navigation Pane programmatically. In fact, it can be created only through code; you cannot create the module through the user interface. The Solutions module gives users a powerful way to search folder hierarchies or stores that contain customized
 item types. For example, a customer relationship management (CRM) application could expose custom item types such as business contacts, accounts, and opportunities in the Solutions module. Additionally, Outlook 2010 gives the add-in developer a way to set
 a custom folder icon for each folder that contains custom item types.</p>
<p>The Solutions module gives developers an important piece of the user interface in Outlook 2010. Although Outlook supports only one Solutions module for all solutions, as a developer, you can use the Solutions module to integrate your solutions seamlessly
 with the Outlook Navigation Pane in ways that were not possible in previous versions of Outlook.</p>
