# SharePoint 2010: Creating Claims Providers for Trusted Login Providers
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* trusted logon providers
* security token service
## IsPublished
* True
## ModifiedDate
* 2013-01-21 01:26:06
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create a custom security token service (STS) and then set up a trust relationship between a SharePoint 2010 farm and the custom STS provided in the sample. The custom STS serves as the authentication provider. When users log on to the SharePoint
 site, they are first redirected to the logon page of the custom STS, and then they are redirected back to SharePoint after the authentication. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg251994(office.14).aspx">Creating Claims Providers for Trusted Login Providers for SharePoint 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>A trusted login provider is an external (that is, external to SharePoint) STS that SharePoint trusts. For definitions of claims terms, see
<a href="http://msdn.microsoft.com/en-us/library/ee534975.aspx">Claims-Based Identity Term Definitions</a>.</p>
<p>SAML passive sign-in describes the process of signing in. When a sign-in for a web application is configured to accept tokens from a trusted login provider, this type of sign-in is called SAML passive sign-in. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/ee534967.aspx">Incoming Claims: Signing into SharePoint</a>.</p>
<p>This sample and the accompanying article walk through the following scenario:</p>
<p>The fictitious company Contoso has a SharePoint site hosted on an extranet, which its employees can log on to remotely from home or during business travel. Contoso has a partner company named Wingtip, whose employees are working on a project with Contoso
 and who need to access documents from the SharePoint site.</p>
<p>To enable the Wingtip employees to log on to the SharePoint site that is hosted by Contoso, Wingtip created an STS that can be used to authenticate its employees. On the Contoso site, the farm administrator set up the trust relationship between the SharePoint
 farm and Wingtip's STS. When employees from Wingtip try to log on to the Contoso SharePoint site, they are first redirected to their STS to be authenticated, then the STS redirects the user to the Contoso SharePoint site. Because the SharePoint farm trusts
 the Wingtip STS, it also trusts the security token that is issued by the Wingtip STS.</p>
