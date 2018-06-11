# SharePoint 2013: Use the chrome control in a cloud-hosted app
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* Javascript
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* User Experience
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:34:40
## Description

<p><span style="font-size:small">This sample developer-hosted app demonstrates how to add the chrome control to your app for SharePoint. With the chrome control, you can develop a user experience that resembles the user experience in the SharePoint website
 that your app is deployed to.</span></p>
<p><span style="font-size:small">The markup that declares the chrome control is in the ChromeControlHost.html file in the ChromeControlWeb project. Figure 1 shows the chrome control in the ChromeControlHost.html file.</span></p>
<p><strong><span style="font-size:small">Figure 1. Chrome control</span></strong></p>
<p><span style="font-size:small"><img id="60361" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-work-with-089ecc6f/image/file/60361/1/fig1.jpg" alt="" width="566" height="155"></span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Microsoft Visual Studio 2012</span> </li><li><span style="font-size:small">&nbsp;</span><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">A SharePoint 2013 Preview development environment (app isolation required for on-premise scenarios).</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">CustomActionsApp project</span>
<ul>
<li><span style="font-size:small">AppManifest.xml file</span> </li></ul>
</li><li><span style="font-size:small">CustomActionsWeb project</span>
<ul>
<li><span style="font-size:small">ChromeControlHost.html file, which renders the chrome control</span>
</li><li><span style="font-size:small">Web.config file</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the sample.</span></p>
<ul>
<li><span style="font-size:small">Update the <strong>SiteUrl</strong> property of the solution with the URL of the home page of your SharePoint website.</span>
</li></ul>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Press F5 to build and deploy the app.</span> </li><li><span style="font-size:small">Choose <strong>Trust It</strong> on the consent page to grant permissions to the app.</span>
</li><li><span style="font-size:small">Click the <strong>ChromeControlAutohosted</strong> app icon.</span>
</li></ol>
<p><span style="font-size:small">Figure 2 shows the remote webpage.</span></p>
<p><strong><span style="font-size:small">Figure 2. A remote webpage with the chrome control</span></strong></p>
<p><span style="font-size:small"><img id="60362" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-work-with-089ecc6f/image/file/60362/1/fig2.png" alt="" width="570" height="308"></span></p>
<h1>Troubleshooting</h1>
<p><span style="font-size:small">The following table lists common configuration and environment errors that prevent the sample from running or deploying properly and how to solve them.</span></p>
<table border="0" cellspacing="5" cellpadding="5" frame="void" align="left" style="width:601px; height:212px">
<tbody>
<tr style="background-color:#a9a9a9">
<th align="left" scope="col"><strong><span style="font-size:small">Problem </span>
</strong></th>
<th align="left" scope="col"><strong><span style="font-size:small">Solution</span></strong></th>
</tr>
<tr valign="top">
<td><span style="font-size:small">Visual Studio does not open the browser after you press the F5 key.</span></td>
<td><span style="font-size:small">Set the app for SharePoint project as the startup project.</span></td>
</tr>
<tr valign="top">
<td><span style="font-size:small">HTTP error 405 <strong>Method not allowed</strong>.</span></td>
<td><span style="font-size:small">Locate the applicationhost.config file in <em>%userprofile%</em>\Documents\IISExpress\config.</span>
<p><span style="font-size:small">Locate the handler entry for <strong>StaticFile</strong>, and add the verbs
<strong>GET</strong>, <strong>HEAD</strong>, <strong>POST</strong>, <strong>DEBUG</strong>, and
<strong>TRACE</strong>.</span></p>
</td>
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
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d">Setting up a SharePoint 2013 development environment
 for apps</a></span> </li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/bfdd0a58-2cc5-4805-ac89-4bd2fe6f3b09" href="http://msdn.microsoft.com/en-us/library/bfdd0a58-2cc5-4805-ac89-4bd2fe6f3b09">Create UX components</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/d60f409a-b292-4c06-8128-88629091b753" href="http://msdn.microsoft.com/en-us/library/d60f409a-b292-4c06-8128-88629091b753">UX design for apps</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/7c2d0812-76e8-44c1-88bf-4a75eb6f82b1">How to: Use the client chrome control in apps for SharePoint 2013 Preview</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" href="http://msdn.microsoft.com/en-us/library/26f2999e-db7f-4fe7-a00f-05b009b1927d">What you can do in an app for SharePoint</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/0942fdce-3227-496a-8873-399fc1dbb72c" href="http://msdn.microsoft.com/en-us/library/0942fdce-3227-496a-8873-399fc1dbb72c">Design considerations for apps for SharePoint</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" href="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88">Critical aspects of the app for SharePoint architecture
 and development landscape</a></span> </li></ul>
