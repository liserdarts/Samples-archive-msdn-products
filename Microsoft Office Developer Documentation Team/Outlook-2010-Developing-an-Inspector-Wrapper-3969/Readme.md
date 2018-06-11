# Outlook 2010: Developing an Inspector Wrapper
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Outlook 2010
* Office 2010
## Topics
* inspector wrappers
## IsPublished
* True
## ModifiedDate
* 2011-08-02 09:59:43
## Description

<h2><strong>Introduction</strong></h2>
<p>This code sample show how to implement an inspector wrapper for Microsoft Outlook 2010. An inspector wrapper handles multiple instances of Outlook 2010 inspector windows. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff973716.aspx">Developing an Inspector Wrapper for Outlook 2010</a> in the MSDN library.</p>
<h2><strong>Description</strong></h2>
<p>The code in this sample demonstrates the following solutions:</p>
<ul>
<li>Identifying in code the window that the user clicked. </li><li>Ensuring that no events were lost. </li><li>Handling memory cleanup correctly. </li><li>Independently handling <strong>Inspector</strong> objects for different Outlook item types.
</li></ul>
<p>The sample shows how to implement wrapper classes that handle different Outlook item types, such as appointments, contacts, and messages. Each of the specialized classes is derived from the abstract
<strong>InspectorWrapper</strong> class. The code also shows how to override the <strong>
Initialize</strong> method and register events that are specific to contact items. The
<strong>Open</strong> and <strong>Write</strong> event notifications of the item are handled, and the
<strong>Close</strong> method is overwritten and must be used to clean up the memory and COM references that you acquired in code.</p>
<p>The inspector wrapper technique is very useful in enforcing business logic that requires certain fields (for example, a contact form that requires a business address and phone number before the form can be saved). See the article
<a href="http://msdn.microsoft.com/en-us/library/ff973715.aspx">Enforcing Business Rules in Outlook 2010</a> for a sample application that validates two fields in a contact form, and displays a message box to the user if the requirements are not met.</p>
