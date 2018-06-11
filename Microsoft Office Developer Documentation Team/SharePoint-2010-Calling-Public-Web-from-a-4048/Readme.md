# SharePoint 2010: Calling Public Web Services from a Sandboxed Silverlight App
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Silverlight
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* Web Services
* Sandbox solution
## IsPublished
* True
## ModifiedDate
* 2011-08-04 03:48:15
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create sandboxed Microsoft Silverlight solutions that can call public web services in Microsoft SharePoint 2010. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg615590.aspx">Calling Public Web Services from a Sandboxed Silverlight Application in SharePoint 2010</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>Microsoft SharePoint 2010 introduces the concept of the sandboxed solution. Sandboxed solutions run in a separate process outside the w3wp.exe process where SharePoint executes. Isolating a sandboxed solution promotes farm stability by preventing poorly
 performing solutions from affecting the SharePoint farm. As part of the isolation strategy, sandboxed solutions cannot make calls to external resources, such as databases or web services. This restriction can be problematic for SharePoint developers whose
 solutions need access to external resources. One approach to solve this problem is to use a Microsoft Silverlight application that is delivered from a sandboxed solution to call a web service.</p>
<p>The key to developing a sandboxed Silverlight application is to create a Web Part or site page that can be deployed as a sandboxed solution. The Web Part or site page can then use an object tag to reference a Silverlight application. The application is downloaded
 to the client, where it can access public web services.</p>
<p>Sandboxed solutions contribute significantly to the overall stability of SharePoint 2010 farms. However, the stability comes at the cost of being unable to access external resources from inside the sandbox. Silverlight applications offer an elegant solution
 to this challenge because they run on the client computer. This enables the solution to be deployed to the sandbox, but delivered to the client. Because the solution runs on the client computer, it can access public web services. Additionally, the solution
 can use the client object model to interact with SharePoint objects.</p>
