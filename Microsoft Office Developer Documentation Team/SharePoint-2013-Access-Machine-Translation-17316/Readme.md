# SharePoint 2013: Access Machine Translation Service using the CSOM
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* sites and content
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:00:11
## Description

<p style="padding-left:30px"><strong><span style="font-size:small">Important</span></strong><span style="font-size:small">&nbsp;</span><br>
<span style="font-size:small">Using machine translation will allow users to send content to Microsoft for translation. Microsoft may use content users send us to improve the quality of translations. If you use the Machine Translation Service in your application,
 you are responsible for informing users that this application will allow users to send content to Microsoft for translation and that Microsoft may use content users send us to improve the quality of translations. See
<a href="http://msdn.microsoft.com/en-us/library/hh464486.aspx">Microsoft Translator Privacy</a> for more information.</span></p>
<p><span style="font-size:small">This sample demonstrates how to translate document libraries, single documents, folders, and text streams using the SharePoint Server 2013 .NET client object model. Machine Translation Service is a new service application in
 SharePoint Server 2013 that provides automatic machine translation of files and sites. Using the client object model, you can submit requests to the Machine Translation Service application asynchronously or synchronously (for instant translation). The Machine
 Translation Service client object model sample is a console application that contains several methods that show you how to submit different types of translation jobs synchronously and asynchronously.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following in your development environment:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio</span> </li><li><span style="font-size:small">&nbsp;</span><span style="font-size:small">SharePoint Server 2013</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The console application contains the methods described in Table 1.</span></p>
<p><strong><span style="font-size:small">Table 1. Console application sample methods</span></strong></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:601px; height:212px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Method name</span></strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Description</span></strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small">Sync </span></td>
<td><span style="font-size:small">Translates a single file synchronously.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">AsyncFile </span>
</span></td>
<td><span style="font-size:small"><span style="font-size:small">Translates a single file asynchronously.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">AsyncFolder </span></td>
<td><span style="font-size:small">Translates a single folder asynchronously.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">AsyncLib</span></span></td>
<td><span style="font-size:small"><span style="font-size:small">Translates a document library asynchronously.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">TestLanguageAndFileExtension
</span></span></td>
<td><span style="font-size:small">Enumerates languages supported by the Machine Translation Serviceand tests support for several common file name extensions.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">TranslationJobStatusGetJob
</span></span></td>
<td><span style="font-size:small"><span style="font-size:small">Retrieves both the list of the all translation jobs and all active translation jobs for the current user.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">TestGetItems </span>
</span></td>
<td><span style="font-size:small"><span style="font-size:small">Retrieves the status of individual items that are part of the specified translation job.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">AddAsyncLibrary </span>
</span></td>
<td><span style="font-size:small"><span style="font-size:small">Translates a document library synchronously.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">TestCancelJob </span>
&nbsp;</span></td>
<td><span style="font-size:small"><span style="font-size:small">Cancels the specified translation job.</span>&nbsp;</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">GetJobItems </span>
&nbsp;</span></td>
<td><span style="font-size:small"><span style="font-size:small">Returns all the items to be translated for the specified translation job.</span>&nbsp;</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">TestIsFileExtensionSupported</span>&nbsp;</span></td>
<td><span style="font-size:small"><span style="font-size:small">Tests if the specified file name extension is supported by the Machine Translation Service.</span>&nbsp;</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">TestIsLanguageSupported
</span></span></td>
<td><span style="font-size:small"><span style="font-size:small">Tests if the specified language is supported by the Machine Translation Service.</span>&nbsp;</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">GetMaximumFileSize
</span></span></td>
<td><span style="font-size:small"><span style="font-size:small">Returns the file size limit for a specified file name extension.</span>&nbsp;</span></td>
</tr>
</tbody>
</table>
<h1><br>
<br>
<span style="font-size:small">&nbsp;</span><br>
<br>
<br>
</h1>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><br>
<br>
<br>
<br>
<br>
<span style="font-size:small">&nbsp;</span><br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
&nbsp;</p>
<p>&nbsp;</p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">Press the <strong>F5</strong> key to compile and run the sample.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First version&nbsp;July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/15a81428-da94-40b8-8ed4-6a12f05661e2.aspx" href="http://msdn.microsoft.com/library/15a81428-da94-40b8-8ed4-6a12f05661e2.aspx">Translation Services in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/Microsoft.Office.TranslationServices.Client" href="http://msdn.microsoft.com/library/Microsoft.Office.TranslationServices.Client">Client Namespace</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx">SharePoint 2013 development overview</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx" href="http://msdn.microsoft.com/library/d07e0a13-1e74-4128-857a-513dedbfef33.aspx">Getting started developing SharePoint apps</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx">Architecture of the app for SharePoint model</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/0e9efadb-aaf2-4c0d-afd5-d6cf25c4e7a8.aspx" href="http://msdn.microsoft.com/library/0e9efadb-aaf2-4c0d-afd5-d6cf25c4e7a8.aspx">Apps for SharePoint vs. classic SharePoint solutions</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx" href="http://msdn.microsoft.com/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88.aspx">Detailed introduction to the SharePoint app model</a></span>
</li></ul>
