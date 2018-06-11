# Outlook 2010: Enforcing Business Rules
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* business rules
* inspector wrappers
## IsPublished
* True
## ModifiedDate
* 2011-08-01 11:36:04
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to enforce business rules in Microsoft Outlook 2010 by using an add-in created with Microsoft Office development tools in Microsoft Visual Studio 2010. The sample application validates two fields in contact forms and displays a message box to the
 user if the requirements are not met. This sample accompanies the article <a href="http://msdn.microsoft.com/en-us/library/ff973715.aspx">
Enforcing Business Rules in Outlook 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>To enforce business rules, you need to begin by developing an Outlook inspector wrapper template. For more information about how to implement an inspector wrapper template, see
<a href="http://msdn.microsoft.com/en-us/library/ff973716.aspx">Developing an Inspector Wrapper for Outlook 2010</a>. The key to this code sample is to handle each Outlook item individually. When an item is opened within Outlook, it is monitored by the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.outlook.inspector.aspx">
Inspector</a> interface. With the wrapper code, it is easy to handle all the inspectors gracefully. The item behind the inspector is exposed by the
<strong>Inspector.CurrentItem</strong> property. In this code sample, two business rules are enforced for contacts. A
<a href="http://msdn.microsoft.com/en-us/library/microsoft.office.interop.outlook.contactitem.aspx">
ContactItem</a> interface exposes some events that you can use to implement a small framework that checks whether all rules are met before a contact form is closed, or when someone is going to save the contact.</p>
<h2><strong>Requirements</strong></h2>
<ul>
<li>Microsoft Outlook 2010 </li><li>Microsoft Visual Studio 2010 </li></ul>
