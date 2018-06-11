# SharePoint 2010: Attaching File from Custom Web View to Outlook Message (Server)
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint Server 2010
* Outlook 2010
## Topics
* Microsoft Office Outlook AddIn
* Web View
## IsPublished
* True
## ModifiedDate
* 2012-04-18 05:02:10
## Description

<h1>Pre-requisites</h1>
<ul>
<li><span style="font-size:small">A SharePoint site installed at the root of a Web Application, with the document id feature turned on (Site Actions-&gt;Site Settings-&gt;Site collection features)</span>
</li></ul>
<p><span style="font-size:small"><img src="http://i1.code.msdn.s-msft.com/sharepoint-2010-attaching-144821a7/image/file/49001/1/pre-req1.png" alt="" width="772" height="43"></span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2010</span> </li><li><span style="font-size:small">Outlook 2010</span> </li></ul>
<p><span style="font-size:small"><strong>Note</strong>: This sample is to be used in conjunction with the corresponding&nbsp;<a href="http://code.msdn.microsoft.com/SharePoint-2010-Attaching-a1738dee">Client sample</a> and the technical article, Attaching Files
 from a Custom SharePoint WebView to an Outlook Message.</span></p>
<h1><span>Development Technologies Used</span></h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ee556847.aspx">SharePoint 2010 API</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ms767600(v=VS.85).aspx">XML and XSLT</a></span>
</li><li><span style="font-size:small"><a href="http://jquery.com/">JQuery 1.4.1</a></span>
</li></ul>
<p><span style="font-size:20px; font-weight:bold">Solution Overview</span></p>
<p><span style="font-size:small">The solution SharePoint.CustomWebView contains 2 projects with all the components needed to run the server side customisations.</span></p>
<ul>
<li><span style="font-size:small">SharePoint.CustomWebView, a standard c# project that targets the .Net framework 3.5 &ndash; contains a custom FileDialogPostProcessor for creating Web Views for the Common File Dialog</span>
</li><li><span style="font-size:small">SharePointFiles &ndash; a SharePoint project that deploys files that the FileDialogPostProcessor is dependent on.</span>&nbsp;
</li></ul>
<h1><span>Installing the Server</span></h1>
<p><span style="font-size:small">Open the solution SharePoint.CustomWebView in Visual Studio. In the solution explorer click on the SharePointFiles project. Update the Site Url in the project properties to point to the root site in your SharePoint farm. For
 example in the example below the server &lsquo;http://sdg-wks125&rsquo; has a SharePoint site created at its root on port 80.</span></p>
<p><span style="font-size:small">&nbsp;</span><br>
<img src="/site/view/file/48995/1/s-install1.png" alt="" width="354" height="253">&nbsp;</p>
<p><span style="font-size:small">Now right click the project and select deploy.</span></p>
<p><img src="/site/view/file/48996/1/s-install2.png" alt="" width="506" height="153"></p>
<p><span style="font-size:small">This should install 3 files in the style library of the root site that it was deployed into. This can be verified by opening a browser and typing in the url of the site. Click
<span style="background-color:#c0c0c0">&lsquo;Site Actions&rsquo;-&gt;&rsquo;View All Content-&gt;&rsquo;Style Library&rsquo;</span>:</span></p>
<p><span style="font-size:small"><img src="/site/view/file/48997/1/s-install3.png" alt="" width="431" height="80"></span><br>
&nbsp;<br>
<span style="font-size:small">Select the SharePoint.CustomWebView project and rebuild it. Then right click the HelperScript folder and select Open Folder in Windows Explorer.</span></p>
<p><span style="font-size:small"><img src="/site/view/file/48998/1/s-install4.png" alt="" width="395" height="267">&nbsp;</span><br>
&nbsp;<br>
<span style="font-size:small">Start the SharePoint 2010 management shell and change the working directory to the location just opened.&nbsp; The run the scripts in the following sequence:</span></p>
<ul>
<li><span style="font-size:small">RunOnce.Bat &ndash; registers the component in the registry</span>
</li><li><span style="font-size:small">QuickDeploy-Debug.bat &ndash; registers the component in the gac</span>
</li><li><span style="font-size:small">RecyleAppPool80.vbs &ndash; recycles the IIS App Pool for the SharePoint site running on Port 80</span>
</li><li><span style="font-size:small">DialogSwitcher.ps1 &ndash; this last script updates a SharePoint site to use the newly installed component and needs a site&rsquo;s url as a parameter:
</span><br>
<span style="font-size:small"><img src="/site/view/file/48999/1/s-install5.png" alt="" width="314" height="20"></span>
</li></ul>
<p><span style="font-size:small">Now verify the custom FileDialogPostProcessor is working by opening notepad, select
<span style="background-color:#c0c0c0">&lsquo;File&rsquo;-&gt;&rsquo;Open&rsquo;</span> and typing in the url to the same site the DialogSwitcher.ps1 was run on. Using the example above this would be
<a href="http://sdk-wks125">http://sdk-wks125</a>:</span></p>
<p><span style="font-size:small"><img src="/site/view/file/49000/1/s-install6.png" alt="" width="683" height="434">&nbsp;</span></p>
