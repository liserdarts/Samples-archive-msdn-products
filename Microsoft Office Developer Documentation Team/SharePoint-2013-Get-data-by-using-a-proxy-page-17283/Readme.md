# SharePoint 2013: Get data by using a proxy page for the cross-domain library
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
* data and storage
## IsPublished
* True
## ModifiedDate
* 2013-02-06 02:43:32
## Description

<p><span style="font-size:small">This sample developer-hosted app demonstrates how to create and use a custom proxy page for the cross-domain library in SharePoint 2013 to read data from a remote service. The app renders a string returned by a remote service
 in a SharePoint webpage.</span></p>
<p><span style="font-size:small">The code for the custom proxy page is in the CustomProxy.aspx file, and the code for the content page is in the SimpleContent.aspx file of the CustomProxyWeb project. The SharePoint webpage is in the ReadText.aspx file of the
 CustomProxyApp project. Figure 1 shows the ReadText.aspx page of the app after you install and run the app.</span></p>
<p><strong><span style="font-size:small">Figure 1. SharePoint page with data from a remote service</span></strong></p>
<p><span style="font-size:small"><img id="60372" src="http://i1.code.msdn.s-msft.com/sharepoint-2013-get-data-10039ff1/image/file/60372/1/fig1.jpg" alt="" width="355" height="302"></span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">SharePoint development tools in Visual Studio 2012</span>
</li><li><span style="font-size:small">SharePoint 2013 development environment (app isolation required for on-premises scenarios).</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">CustomProxyApp project</span>
<ul>
<li><span style="font-size:small">ReadText.aspx file, which contains a reference to the cross-domain library and issues the call to the remote service</span>
</li><li><span style="font-size:small">AppManifest.xml file</span> </li></ul>
</li><li><span style="font-size:small">CustomProxyWeb project</span>
<ul>
<li><span style="font-size:small">CustomProxy.aspx file, which contains a reference to the cross-domain library and initializes the
<strong>RequestExecutorMessageProcessor</strong> object</span> </li><li><span style="font-size:small">SimpleContent.aspx file, which serves a simple string in plain text</span>
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
</li></ol>
<p><span style="font-size:small">You should see a SharePoint webpage with the text
<strong>Data from the remote domain:</strong> followed by a string served by the SimpleContent.aspx page.</span></p>
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
<td><span style="font-size:small">Unhandled exception <strong>SP is undefined</strong>.</span></td>
<td><span style="font-size:small">Make sure you can access the SP.RequestExecutor.js file from a browser window.</span></td>
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
<p>&nbsp;</p>
<h1>Change log</h1>
<p><span style="font-size:small">First version: July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" href="http://msdn.microsoft.com/en-us/library/b0878c12-27c9-4eea-ae3b-7e79e5a8838d" target="_blank">How to: Set up an on-premises&nbsp;development
 environment for apps for SharePoint</a></span> </li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/1534a5f4-1d83-45b4-9714-3a1995677d85" target="_blank">Work with data in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/f3f87cdf-5cbf-47c9-9ce1-1ab65cd598de" target="_blank">How to: Create a custom proxy page for the cross-domain library</a></span>
</li><li><span style="font-size:small"><a title="http://msdn.microsoft.com/en-us/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" href="http://msdn.microsoft.com/en-us/library/26f2999e-db7f-4fe7-a00f-05b009b1927d" target="_blank">What you can do in an app for SharePoint</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/bde5647a-fff1-4b51-b67b-2139de79ce4a" target="_blank">Authorization and authentication for apps in SharePoint 2013</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/0942fdce-3227-496a-8873-399fc1dbb72c" target="_blank">Three ways to think about design options for apps for SharePoint</a></span>
</li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ae96572b-8f06-4fd3-854f-fc312f7f2d88" target="_blank">Important aspects of the app for SharePoint architecture and development landscape</a></span>
</li><li><a href="http://msdn.microsoft.com/en-us/library/3034f03c-2d5a-46de-9cb8-2c101ff194fa" target="_blank"><span style="font-size:small">Data storage options in apps for SharePoint</span></a>
</li></ul>
