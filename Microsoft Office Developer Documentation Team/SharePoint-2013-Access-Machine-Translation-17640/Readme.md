# SharePoint 2013: Access Machine Translation Service using server object model
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
* 2013-02-06 01:58:35
## Description

<p style="padding-left:30px"><strong><span style="font-size:small">Important</span></strong><br>
<span style="font-size:small">Using machine translation will allow users to send content to Microsoft for translation. Microsoft may use content users send us to improve the quality of translations. If you use the Machine Translation Service in your application,
 you are responsible for informing users that this application will allow users to send content to Microsoft for translation and that Microsoft may use content users send us to improve the quality of translations. See
<a href="http://msdn.microsoft.com/en-us/library/hh464486.aspx">Microsoft Translator Privacy</a> for more information.</span></p>
<p><span style="font-size:small">This sample demonstrates how to translate document libraries, single documents, folders, and text streams using the SharePoint Server 2013 server object model. Machine Translation Service is a new service application in SharePoint
 Server 2013 that provides automatic machine translation of files and sites. Using the object model, you can submit requests to the Machine Translation Service application asynchronously or synchronously (for instant translation). The Machine Translation Service
 server object model sample is a console application that contains several methods that show you how to submit different types of translation jobs synchronously and asynchronously.</span><span style="font-size:small">&nbsp;</span></p>
<p style="padding-left:30px"><strong><span style="font-size:small">Note</span></strong><br>
<span style="font-size:small">Applications that use the server object model must run directly on a server that is running SharePoint Server 2013.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following in your development environment:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio</span> </li><li><span style="font-size:small">SharePoint Server 2013</span> </li></ul>
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
<td><span style="font-size:small">AddSyncFile </span></td>
<td><span style="font-size:small">Translates a single file synchronously.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">AddSyncStream </span>
</span></td>
<td><span style="font-size:small">Translates a stream synchronously.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">AddSyncByte
</span></span></span></td>
<td><span style="font-size:small">Translates an array of bytes synchronously.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:x-small"><span style="font-size:small">AddAsyncFile </span>
</span></td>
<td><span style="font-size:x-small"><span style="font-size:small">Translates a single file asynchronously.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">AddAsyncFolder
</span></span></span></td>
<td><span style="font-size:x-small"><span style="font-size:small">Translates a folder synchronously.</span></span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">AddAsyncLibrary
</span></span></span></td>
<td><span style="font-size:small">Translates a document library synchronously.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:x-small"><span style="font-size:small">GetJobStatus </span>
</span></td>
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">Retrieves a list of the translation jobs with the following statuses:</span></span></span>
<ul>
<li><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">All active jobs for all users.</span></span></span>
</li><li><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">&nbsp;</span><span style="font-size:small">All active jobs for the current user.</span></span></span>
</li><li><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">All jobs for all users.</span></span></span>
</li><li><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">All jobs for the current user.</span></span></span>
</li></ul>
</td>
</tr>
<tr valign="top">
<td><span style="font-size:small">&nbsp;<span style="font-size:small"><span style="font-size:small">GetJobItems
</span></span></span></td>
<td><span style="font-size:small"><span style="font-size:small">Returns all the items to be translated for the specified translation job.</span>
</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">&nbsp;<span style="font-size:small">GetSupportedLanguages
</span></span></td>
<td><span style="font-size:small">Returns all languages that are supported by Machine Translation Service.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small">GetSupportedFileExtensions
</span></span></td>
<td><span style="font-size:small">Returns all the file name extensions that are supported by Machine Translation Service.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">GetMaximumFileSize</span></span></span></span></td>
<td><span style="font-size:small"><span style="font-size:small">Returns the file size limit for a specified file name extension.</span>
</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small"><span style="font-size:small"><span style="font-size:small">CancelJob
</span></span></span></td>
<td><span style="font-size:small">Cancels the specified translation job.</span></td>
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
</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1><br>
Run and test the sample</h1>
<p><span style="font-size:small">Press the <strong>F5</strong> key to compile and run the sample.</span></p>
<h1>Change log</h1>
<p><span style="font-size:small">First version:&nbsp;July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/15a81428-da94-40b8-8ed4-6a12f05661e2.aspx" href="http://msdn.microsoft.com/library/15a81428-da94-40b8-8ed4-6a12f05661e2.aspx">Translation Services in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/Microsoft.Office.TranslationServices.aspx" href="http://msdn.microsoft.com/library/Microsoft.Office.TranslationServices.aspx">TranslationServices Namespace</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx" href="http://msdn.microsoft.com/library/f86e2695-4d7a-4fc5-bc23-689de96c4b06.aspx">SharePoint 2013 development overview</a></span>
</li></ul>
