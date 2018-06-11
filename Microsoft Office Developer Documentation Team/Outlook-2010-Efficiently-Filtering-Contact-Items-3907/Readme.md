# Outlook 2010: Efficiently Filtering Contact Items in a Contact Folder
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* filtering contacts
## IsPublished
* True
## ModifiedDate
* 2011-07-27 10:51:53
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the <strong>Table</strong> object and how to use the <strong>
GetTable</strong> method of the <strong>Folder</strong> object to get and filter information for contact items in Microsoft Outlook 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg262880.aspx">Efficiently Filtering Contact Items in a Contact Folder in Outlook 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The <strong>Table</strong> object represents a read-only dynamic rowset of data in a
<strong>Folder</strong> or <strong>Search </strong>object, and you can use the <strong>
GetTable</strong> method of the <strong>Folder</strong> object to get a <strong>Table</strong> object that represents items in a folder. The
<strong>Table</strong> object allows for for fast enumeration and filtering of items in the folder. Each row of a
<strong>Table</strong> represents an item in the folder, and each column represents a property of an item. The initial
<strong>Table</strong> object contains a default subset of the available properties. This C# Microsoft Outlook 2010 add-in project shows how to use this object. To use this sample, you should already be familiar with C# and creating custom forms and add-ins
 for Outlook.</p>
<p>In addition to the Visual Studio project files, the code sample uses two additional files,
<a id="25513" href="/site/view/file/25513/1/Sales%20Opportunity.oft">Sales Opportunity.oft</a> and
<a id="25508" href="/site/view/file/25508/1/ContactData.csv">ContactData.csv</a>, which are attached to this description. For more information about these two files, see the accompanying article.</p>
