# Outlook 2010: Getting and Setting Custom Properties in a Contact Folder
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* getting custom properties
* setting custom properties
## IsPublished
* True
## ModifiedDate
* 2011-07-27 11:12:20
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to use the <strong>GetProperties</strong> and <strong>SetProperties</strong> methods of the
<strong>PropertyAccessor</strong> object to get and set multiple custom properties for contact items in Microsoft Outlook 2010 at the same time. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg262881.aspx">Efficiently Getting and Setting Custom Properties in a Contact Folder in Outlook 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The <strong>GetProperties</strong> and <strong>SetProperties</strong> methods of the
<strong>PropertyAccessor</strong> object enable you to efficiently get and set multiple built-in and custom properties of an Outlook 2010 item with one method call.</p>
<p>This download contains a C# Outlook add-in project that shows how to use these two methods. To use this sample, you should already be familiar with C# and creating custom forms and add-ins for Outlook.</p>
<p>When Outlook starts, the add-in checks for an Opportunities contacts folder, and creates one if it does not already exist. The add-in enables you to load data from a data file to populate contact items in the Opportunities folder. This folder will contain
 contacts that include custom sales information, which can be accessed through a custom
<strong>Sales Opportunity</strong> form. The add-in uses the <strong>SetProperties</strong> method of the
<strong>PropertyAccessor</strong> object to add the contact information to each new contact, and this method enables you to set built-in and custom properties at the same time.</p>
<p>The <strong>PropertyAccessor</strong> is good for this scenario. Using the <strong>
PropertyAccessor</strong> object incurs some overhead, but it reduces network traffic. The
<strong>GetProperties</strong> and <strong>SetProperties</strong> methods perform faster than using either the
<strong>UserProperties</strong> object when you have to get or set multiple custom properties at the item level, or the
<strong>UserDefinedProperties</strong> object to get or set multiple custom properties at the folder level. Also, one call to the
<strong>GetProperties</strong> or <strong>SetProperties</strong> method is much faster at setting multiple properties than calling the
<strong>GetProperty</strong> or <strong>SetProperty</strong> method multiple times.</p>
<p>In addition to the Visual Studio project files, the code sample uses two additional files,
<a id="25522" href="/site/view/file/25522/1/Sales%20Opportunity.oft">Sales Opportunity.oft</a> and
<a id="25523" href="/site/view/file/25523/1/ContactData.csv">ContactData.csv</a>, which are attached to this description. For more information about these two files, see the accompanying article.</p>
