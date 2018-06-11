# Outlook 2010: Manipulating Multiple Exchange Accounts
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
* Microsoft Exchange Server 2010
## Topics
* Ribbon Extensibility
* Exchange accounts
## IsPublished
* True
## ModifiedDate
* 2014-04-22 10:36:53
## Description

<p><strong>Introduction</strong></p>
<p>Learn how to create an add-in to manipulate multiple Exchange accounts that are configured in a single Microsoft Outlook 2010 profile. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ee909468.aspx">Manipulating Multiple Exchange Accounts in Outlook 2010</a> in the MSDN Library.</p>
<p><strong>Description</strong></p>
<p>In Microsoft Outlook 2010, you can assign more than one Exchange account to a profile; updates to the Outlook 2010 object model support the functionality. This code sample illustrates how to create an add-in by using Microsoft Office development tools in
 Microsoft Visual Studio 2010 to access Exchange account information.</p>
<p>Your add-in can use the new Exchange account information to interact more effectively with a user's Outlook 2010 profile.</p>
<p>This sample shows how to add a button to the ribbon component of the Microsoft Office Fluent user interface on the active Outlook Explorer window. Users click the button to open a Windows Forms dialog box that displays information about all of the Exchange
 accounts that are configured for the current profile.</p>
<p>You can use an Office development tools in Visual Studio 2010 add-in to run your compiled code inside Outlook and to interact with the Outlook object model.</p>
<p>The sample code will demonstrate how to do the following:</p>
<ul>
<li>Add methods to the form&rsquo;s code-behind file. </li><li>Add a custom tab to the ribbon. </li><li>Add a click event handler for two buttons. </li><li>Create a context-aware email message. </li></ul>
