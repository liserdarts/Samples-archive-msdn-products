# Outlook 2010: Customizing the Context Menu of a Contact Card
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* Ribbon Extensibility
* Context menu
* Contact Card
## IsPublished
* True
## ModifiedDate
* 2011-08-01 02:25:14
## Description

<h2><strong>Introduction</strong></h2>
<p>Create an add-in to add a custom item to the context menu of the new Contact Card feature in Microsoft Outlook 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ee909466.aspx">Customizing the Context Menu of a Contact Card in Outlook 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft Outlook 2010 introduces a new user interface element called the Contact Card. The Contact Card exposes a context menu that you can use to add your own menu items. This code sample illustrates how to create an add-in by using Microsoft Office development
 tools in Microsoft Visual Studio 2010 to customize the Contact Card context menu.</p>
<p>This sample shows how to add a menu item to the <strong>Contact Card</strong> context menu. Choosing the menu item displays the SMTP address of the selected contact.</p>
<p>You can use an Office development tools in Visual Studio 2010 add-in to run your compiled code inside Outlook and to interact with the Outlook object model.</p>
<h3><strong>To create the add-in</strong></h3>
<ol>
<li>In Visual Studio 2010, create an add-in for Outlook 2010. Name the new project
<strong>ExtendCC</strong>. </li><li>In the <strong>Solution Explorer</strong> window, right-click the project, select
<strong>Add</strong> from the context menu, and then click <strong>New Item</strong>. In the
<strong>Add New Item</strong> dialog box, under <strong>Common Items</strong>, select
<strong>Office</strong>, and then select <strong>Ribbon (XML)</strong>. </li><li>Change the <strong>Name</strong> field to <strong>ContactCardContextMenu</strong> and then click
<strong>Add</strong>. Visual Studio adds two files; a source file and an XML file.
</li></ol>
<p>To customize the Microsoft Office Fluent UI context menu, you must use a <strong>
Ribbon (XML)</strong> type, which requires an XML file that describes the user interface elements and a source file that contains your code.</p>
<p>You must modify the existing <strong>GetCustomUI</strong> method so that it passes your XML to Outlook 2010 when Outlook 2010 asks for customizations to the
<strong>Contact Card</strong> context menu.</p>
