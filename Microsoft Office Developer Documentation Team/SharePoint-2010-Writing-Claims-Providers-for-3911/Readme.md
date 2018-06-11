# SharePoint 2010: Writing Claims Providers for SharePoint 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* claims providers
## IsPublished
* True
## ModifiedDate
* 2011-07-27 12:04:26
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to write a claims provider to augment claims and provide name resolution. By using claims authentication, you can assign rights based on claims without knowing who a user is, or how they are authenticated. You have to know only the attributes of
 the user. This sample accompanies the article <a href="http://msdn.microsoft.com/en-us/library/ff699494.aspx">
Claims Walkthrough: Writing Claims Providers for SharePoint 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>You can use a claims provider in SharePoint 2010 to augment claims and provide name resolution. By using claims authentication, you can assign rights based on claims without having to know who a user is, or how they are authenticated; all that you have to
 know is the attributes of the users. You can, for example, use a piece of corporate metadata that is associated with a person, and have the claims provider do a lookup to some other system to figure out all the different identities that particular person uses&mdash;Windows,
 forms-based authentication, SAP, CRM, and so on&mdash;and map some other identifier or set of claims to that identity. Those claims are then used to grant access to resources.</p>
<h2><strong>Requirements</strong></h2>
<p>To use this sample, you must have the following:</p>
<ul>
<li>Microsoft SharePoint Foundation 2010 </li><li>Microsoft SharePoint Server 2010 </li><li>Microsoft Visual Studio 2010 </li><li>Knowledge of C# </li></ul>
