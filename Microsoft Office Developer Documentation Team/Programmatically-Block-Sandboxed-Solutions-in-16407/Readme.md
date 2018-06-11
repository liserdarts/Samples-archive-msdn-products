# Programmatically Block Sandboxed Solutions in SharePoint 2010
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2010
* SharePoint Foundation 2010
## Topics
* sandboxed solutions
## IsPublished
* True
## ModifiedDate
* 2013-04-10 12:01:16
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample accompanies the article <a title="Programmatically Block Sandboxed Solutions in SharePoint 2010" href="http://msdn.microsoft.com/en-us/library/hh921948.aspx" target="_blank">
Programmatically block sandboxed solutions in SharePoint 2010</a>. It contains two solutions which are referred to in the article.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">The Contoso.SharePoint.Administration sample is a custom sandboxed solution validator. Solution validators can analyze the entire WSP file contents to determine whether a solution is valid or not. For example, while activating
 solutions, you might want to check whether the solution&rsquo;s assemblies are signed by using a specific certificate, or by your own licensing mechanism. You may also want to block solutions that contain specific types of files such as ECMAScript (JavaScript,
 Jscript) files as is the case in this solution. Note that solutions are validated each time they are activated. This sample gives you more granular control over solutions that are uploaded and activated over the site collection.<br>
<br>
The SolutionWithJavaScript sample is a sandboxed solution with one JavaScript file. It is used to test the custom solution validator (Contoso.SharePoint.Administration). You can test a custom validator in Visual Studio 2010 or by using the Activate option in
 the Site Collection Solutions Gallery.</span></p>
<p>&nbsp;</p>
