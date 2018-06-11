# SharePoint 2010: Attaching File from Custom Web View to Outlook Message (Client)
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* SharePoint 2010
* Outlook 2010
## Topics
* Web View
## IsPublished
* True
## ModifiedDate
* 2012-04-18 05:02:59
## Description

<h1>Pre-requisites</h1>
<ul>
<li><span style="font-size:small">A SharePoint site installed at the root of a Web Application, with the document id feature turned on (Site Actions-&gt;Site Settings-&gt;Site collection features)</span>
</li></ul>
<p><span style="font-size:small"><img src="http://i1.code.msdn.s-msft.com/sharepoint-2010-attaching-144821a7/image/file/49001/1/pre-req1.png" alt="" width="772" height="43"></span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2010</span> </li><li><span style="font-size:small">Outlook 2010</span> <span style="font-size:small">
<strong></strong></span></li></ul>
<p><span style="font-size:small"><strong>Note</strong>: This sample is to be used in conjunction with the corresponding
<a href="http://code.msdn.microsoft.com/SharePoint-2010-Attaching-144821a7">Server sample</a> and the technical article, Attaching Files from a Custom SharePoint WebView to an Outlook Message.</span></p>
<h1><span>Development Technologies Used</span></h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/office/hh133430">VSTO (Visual Studio Tools for Office)</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/dd30h2yb.aspx">Windows Forms</a></span>
</li></ul>
<p><span style="font-size:20px; font-weight:bold">Solution Overview</span></p>
<p><span style="font-size:small">The <span lang="EN-GB" style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt">
solution Outlook.CustomAttachment contains a single VSTO outlook project which targets the .Net 4 Client Profile. The project contains the components for a custom approach to attaching files from SharePoint</span></span><span style="font-size:small">.</span></p>
<h1><span>Installing the Server</span></h1>
<p><span style="font-size:small">Open the Outlook.CustomAttachment solution and click run &ndash; make sure Outlook is not currently running when this is done. Click New Email and then the Attach Document button in the SharePoint group:</span></p>
<p><span style="font-size:small"><img src="/site/view/file/49002/1/c-install1.png" alt="" width="183" height="101">&nbsp;</span></p>
<p><strong><span style="font-size:small">When the custom dialog loads it will check the registry for the url of the site it should display</span></strong></p>
<p><strong><span style="font-size:small"><img src="/site/view/file/49003/1/c-install2.png" alt="" width="893" height="349">&nbsp;</span></strong><br>
<strong><span style="font-size:small">If it does not find an entry in the registry then the user will be prompted to provide a url, which will then be saved into the registry:</span></strong></p>
<p><strong><span style="font-size:small"><img src="/site/view/file/49004/1/c-install3.png" alt="" width="358" height="286">&nbsp;</span></strong><br>
&nbsp;<br>
<span style="font-size:small">The sample will display child document libraries that are set to appear in the QuickLaunch in SharePoint (On the ribbon:
<span style="background-color:#999999">&lsquo;Library&rsquo;-&gt;&rsquo;Library Settings&rsquo;-&gt;&rsquo;Title, Description and Navigation&rsquo;-&gt;&rsquo;Display this document library on the Quick Launch&rsquo;=Yes</span>):</span></p>
<p><span style="font-size:small"><img src="/site/view/file/49005/1/c-install4.png" alt="" width="581" height="363">&nbsp;</span><br>
&nbsp;<br>
<span style="font-size:small">Double clicking on a child document library will display the documents in that library. If the document library has versioning turned on and documents with versions in it then they can also be displayed by clicking on the individual
 file&rsquo;s version column:</span></p>
<p><span style="font-size:small"><img src="/site/view/file/49006/1/c-install5.png" alt="" width="721" height="574">&nbsp;</span><br>
&nbsp;<br>
<span style="font-size:small">Checking items then clicking the OK button will attach the document to the email.</span><br>
<span style="font-size:small">&nbsp;</span></p>
