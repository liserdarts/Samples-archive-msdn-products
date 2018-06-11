# Office 2010: Deploying Multiple Office 2010 Projects in One Package
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* ClickOnce
* Word 2010
* Office 2010
## Topics
* Deployment
## IsPublished
* True
## ModifiedDate
* 2011-07-26 10:13:07
## Description

<h1><strong>Introduction</strong></h1>
<p>Learn how to use ClickOnce deployment to combine multiple setup routines so that users can install multiple Office solutions at the same time. This sample accompanies the article
<a href="http://msdn.microsoft.com/en-us/library/ff951700.aspx">Deploying Multiple Office 2010 Projects in One Package</a> in the MSDN Library.</p>
<h1><strong>Description</strong></h1>
<p>Suppose that you create two or more Microsoft Office 2010 solutions by using Microsoft Visual Studio 2010, test the solutions, and then want to deploy them to users. You might think that you can use ClickOnce to deploy the solutions, but that you have to
 publish each of them individually, which requires users to install them separately. Instead, you can deploy the solutions as a single package so that users only have to run one setup routine.</p>
<p>The Visual Studio Tools for Office runtime can deploy multiple Office solutions in a single ClickOnce install. When you publish solutions, you create the ClickOnce files, including setup routines. By using multiple solutions, you have multiple setup routines.
 The accompanying article describes how to combine multiple setup routines so that users can install all the solutions at the same time.</p>
<p>ClickOnce uses an application manifest to determine what to install and how to install it. Each solution that you publish has an application manifest. Visual Studio cannot create a multi-solution application manifest, but you can do this manually. The article
 that accompanies this download describes how to create a multi-solution application manifest and install multiple Office solutions with one Setup routine.</p>
