# Outlook 2010: Adding Solution-Specific Folders to the Solutions Module
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* Solutions Module
## IsPublished
* True
## ModifiedDate
* 2011-08-01 01:38:35
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a Visual Studio Tools for Office add-in to add custom solution-specific folders to the Microsoft Outlook 2010 Solutions Module. This code sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ee909467.aspx">Adding Solution-Specific Folders to the Solutions Module in Outlook 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Outlook 2010 introduced a feature called the Solutions Module that you can use to add a custom navigation module to the Outlook Navigation Pane programmatically. Once you add your module, you can add your own custom Outlook folders to it and provide
 custom icons for each to distinguish them from the built-in folders in Outlook.</p>
<p>This code sample shows how to create a Visual Studio Tools for Office add-in that creates a custom solution. In the process, you customize the ribbon component of the Microsoft Office Fluent user interface with a button that invokes your code, and add custom
 icons to your folders. The sample code includes helper functions for your Visual Studio project that convert icons to the correct format for Outlook.</p>
<p style="padding-left:30px"><strong>Note: </strong>If you do not have your own custom icons, you can use one of the icons included with Visual Studio 2010 in the zip file VS2010ImageLibrary.zip that is located&nbsp;by default&nbsp;in %Program Files x86%\Microsoft
 Visual Studio 10.0\Common7\VS2010ImageLibrary\%localid%.</p>
<p>You can use a Visual Studio Tools for Office add-in to run your compiled code inside Outlook and to interact with the Outlook object model.</p>
