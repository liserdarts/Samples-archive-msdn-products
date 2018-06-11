# SharePoint 2010: Creating Claims Providers for Forms-Based Authentication
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* claims providers
* forms-based authentication
## IsPublished
* True
## ModifiedDate
* 2011-08-09 02:57:02
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a claims provider for a Microsoft SharePoint 2010 forms-based authentication web application. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg481769.aspx">Claims Walkthrough: Creating Claims Providers for Forms-Based Authentication Web Applications for SharePoint 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>This sample and the accompanying walkthrough demonstrate how to implement a sample customer relationship management (CRM) claims provider for a forms-based web application. Then, you modify the claims provider to provide additional CRM claims (by using claims
 augmentation) that are used to control access to SharePoint resources by using an access control list (ACL).</p>
<p>By default, claims search and resolve work with membership and role providers that implement the interfaces that Microsoft SharePoint Foundation 2010 and Microsoft SharePoint Server 2010 call from the default claims provider for forms-based authentication.
 There may be cases where additional claims are needed (for example, for CRM as described in the scenario section). In such cases, you can use an augmentation claims provider.</p>
<p>The term claims augmentation applies to any type of claims authentication: Windows claims authentication, Security Assertion Markup Language (SAML) sign-in, and forms-based authentication.</p>
