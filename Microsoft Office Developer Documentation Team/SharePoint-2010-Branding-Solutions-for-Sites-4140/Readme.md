# SharePoint 2010: Branding Solutions for Sites Using Sandboxed Solutions
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* branding solutions
* sandboxed solutions
## IsPublished
* True
## ModifiedDate
* 2011-08-09 02:10:38
## Description

<h2><strong>Introduction</strong></h2>
<p>Learn how to create sandbox-compatible branding solutions by using custom master pages, CSS files, and images that can be deployed to Microsoft SharePoint 2010 farms. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/gg447066.aspx">Deploying Branding Solutions for SharePoint 2010 Sites Using Sandboxed Solutions</a> in the MSDN Library.</p>
<h2><strong>Description</strong></h2>
<p>The SharePoint development tools in Microsoft Visual Studio 2010 provide a simple and effective approach to packaging and deploying the files and code that are required to apply branding to Microsoft SharePoint 2010 sites using a sandboxed solution. This
 sample and the accompanying article demonstrate a best practice for creating sandbox-compatible branding solutions by using custom master pages, cascading style sheets (CSS files), and images that can be deployed to SharePoint 2010 farms that are running either
 SharePoint Foundation 2010 or SharePoint Server 2010.</p>
<p>There is only one way to deploy a business solution that is developed for the preceding version of SharePoint Products and Technologies. A SharePoint solution that targets a Windows SharePoint Services 3.0 or an Office SharePoint Server 2007 farm must be
 deployed at farm-level scope by a farm administrator. Because farm solution deployment requires scoping custom files to front-end web servers, it poses certain risks to the health of the farm. Furthermore, most farm solutions install custom assembly DLLs in
 the global assembly cache on the web server, which allows the code inside to run with full trust. Therefore, many farm administrators require SharePoint solutions to undergo lengthy code reviews and rigid testing procedures before they can be deployed in a
 SharePoint farm. Farm administrators in some SharePoint environments go one step further and prohibit farm-level deployment of SharePoint solutions altogether.</p>
<p>SharePoint 2010 introduces new sandboxing architecture that provides a valuable alternative to farm-level deployment. Unlike farm solution deployment, sandboxed solution deployment does not require a farm administrator. Instead, a SharePoint solution that
 is developed as a sandboxed solution can be uploaded and activated by a business user within the scope of a site collection. This can significantly speed up the process of getting a custom SharePoint solution into service. Sandboxed solutions also let you
 develop custom SharePoint solutions in Visual Studio 2010 that target environments that do not allow for farm solution deployment, such as SharePoint Online.</p>
<p>There is an important point about the new sandbox architecture that you should fully understand. Designing and developing SharePoint solutions that are compatible with the sandbox increases your options for deployment. This is because you can deploy a SharePoint
 solution that has been developed to target the sandbox as either a sandboxed solution or as a farm solution. Just because you develop a SharePoint solution as a sandboxed solution does not mean that you always have to deploy it as a sandboxed solution.</p>
<p>This sample and the accompanying article shows how a sandboxed solution provides greater flexibility. In a SharePoint farm that does not allow for farm-level solution deployment, users can upload and activate your SharePoint solution within the context of
 a site collection. In another SharePoint farm that does not pose these restrictions, the same SharePoint solution can be deployed as a farm solution, which makes it possible to obtain higher levels of performance and maintainability. This provides a design
 motivation for always targeting the sandbox when the constraints of the sandbox do not prevent you from accomplishing what you want.</p>
