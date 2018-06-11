# Mail apps for Outlook: Create a mail app to view YouTube videos
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Visual Studio 2012
* Outlook 2013
* apps for Office
* Exchange Server 2013
## Topics
* social computing
* mail app
## IsPublished
* True
## ModifiedDate
* 2013-08-28 05:48:35
## Description

<p><span style="font-size:small">This mail app allows users to conveniently view YouTube videos in the app pane in Outlook, if the selected email message or appointment contains a URL to a video on YouTube. The following figure is a screen shot of the YouTube
 mail app activated for a message in the Reading Pane. The accompanying topic, <a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142216(v=office.15).aspx">
How to: Build a mail app to view YouTube videos</a>,&nbsp;also shows a video of the mail app in action.</span></p>
<p><span style="font-size:small"><img id="60044" src="/site/view/file/60044/1/AppsYouTube1small.png" alt="" width="645" height="314"></span></p>
<p><span style="font-size:small">The main code files for this mail app are manifest.xml and youtube.htm, along with the JavaScript library and string files for apps for Office, and a logo image file. The following is a high level summary of how the mail app
 works:</span></p>
<p style="padding-left:30px"><span style="font-size:small">1.&nbsp;This mail app specifies in the manifest.xml file that it requires a host application that supports the mailbox capability:</span></p>
<p style="padding-left:30px"><span style="font-size:small">&lt;Capabilities&gt;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;Capability Name=&quot;Mailbox&quot;/&gt;</span><br>
<span style="font-size:small">&lt;/Capabilities&gt;</span></p>
<div class="endscriptcode" style="padding-left:30px"><span style="font-size:small">In Office 2013, the mailbox capability is supported in the Outlook rich client&nbsp;and&nbsp;Outlook Web App.</span></div>
<p style="padding-left:30px"><span style="font-size:small">2.&nbsp;The mail specifies in the manifest file its support for the desktop and tablet form factors. This further determines that in Office 2013, the applications that can host this mail app are the
 Outlook rich client and Outlook Web App.</span></p>
<pre style="padding-left:30px"><span style="font-size:small">&lt;DesktopSettings&gt;</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;!-- Change the following line to specify the web server where the HTML file is hosted. --&gt;</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;SourceLocation DefaultValue=&quot;https://webserver/YouTube/YouTube.htm&quot;/&gt;</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;RequestedHeight&gt;216&lt;/RequestedHeight&gt;</span><br><span style="font-size:small">&lt;/DesktopSettings&gt;</span><br><span style="font-size:small">&lt;TabletSettings&gt;</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;!-- Change the following line to specify the web server where the HTML file is hosted. --&gt;</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;SourceLocation DefaultValue=&quot;https://webserver/YouTube/YouTube.htm&quot;/&gt;</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;RequestedHeight&gt;216&lt;/RequestedHeight&gt;</span><br><span style="font-size:small">&lt;/TabletSettings&gt;</span></pre>
<p style="padding-left:30px"><span style="font-size:small">3.&nbsp;The mail also requests the
<strong>ReadItem</strong> permission in the manifest file, so that it can run regular expressions, which is further discussed below.</span></p>
<pre style="padding-left:30px"><span style="font-size:small">&lt;Permissions&gt;ReadItem&lt;/Permissions&gt;</span></pre>
<p style="padding-left:30px"><span style="font-size:small">4.&nbsp;The host application activates this mail app when the selected message or appointment contains a URL to a YouTube video. It does so by first reading on startup the manifest.xml file which specifies
 an activation rule that includes a regular expression to look for such a URL:</span></p>
<pre style="padding-left:30px"><span style="font-size:small">&nbsp; &lt;Rule xsi:type=&quot;ItemHasRegularExpressionMatch&quot; PropertyName=&quot;BodyAsPlaintext&quot; RegExName=&quot;VideoURL&quot; RegExValue=&quot;http://(((www\.)?youtube\.com/watch\?v=)|(youtu\.be/))[a-zA-Z0-9_-]{11}&quot;/&gt;</span></pre>
<p style="padding-left:30px"><span style="font-size:small">5.&nbsp;The mail app defines an initialize function which is an event handler for the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp161139.aspx">initialize</a></strong> event. When the run-time environment is loaded, the
<strong>initialize</strong> event is fired, and the <strong>initialize</strong> function calls the main function of the mail app, init, as shown in the code below:</span></p>
<pre style="padding-left:30px"><span style="font-size:small">Office.initialize = function () {</span><br><span style="font-size:small">&nbsp;&nbsp;&nbsp; init(Office.context.mailbox.item.getRegExMatches().VideoURL);</span><br><span style="font-size:small">}</span></pre>
<p style="padding-left:30px"><span style="font-size:small">The <strong>getRegExMatches</strong> method of the selected item returns an array of strings that match the regular expression VideoURL, which is specified in the manifest.xml file. In this case, that
 array contains URLs to videos on YouTube.</span></p>
<p style="padding-left:30px"><span style="font-size:small">6.&nbsp;The init function and the rest of the youtube.htm file take as an input parameter that array of YouTube URLs and dynamically build the HTML to display the corresponding thumbnail and details
 for each video.</span></p>
<p style="padding-left:30px"><span style="font-size:small">This dynamically built HTML displays the first video in a YouTube embedded player, together with details about the video. The app pane also displays the thumbnails of any subsequent videos. The end
 user can choose a thumbnail to view any of the videos in the YouTube embedded player, without leaving the host application.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">The following are the requirements for the sample:</span></p>
<ul>
<li><span style="font-size:small">Your favorite web development tool and web server to author and host HTML and JavaScript files.</span>
</li><li><span style="font-size:small">An email account on Exchange Server 2013.</span>
</li><li><span style="font-size:small">Client applications that support the mailbox capability in Office 2013, on the desktop and tablet form factors: Outlook 2013, Outlook Web App.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Required technical familiarity: HTML, JavaScript.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The following are the primary files in the sample:</span></p>
<ul>
<li><span style="font-size:small">manifest.xml</span> </li><li><span style="font-size:small">youtube.htm</span> </li><li><span style="font-size:small">YouTubeLogo.png</span> </li><li><span style="font-size:small">strings_en-us.js</span> </li><li><span style="line-height:115%; font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;; font-size:11pt">strings_fr-fr.js</span>
</li><li><span style="font-size:small">Outlook.css</span> </li><li><span style="line-height:115%; font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;; font-size:11pt">MicrosoftAjax.js</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Host the youtube.htm file on a web server, and ensure the paths to the image, JavaScript, css, and string files are specified appropriately in the youtube.htm file so that they are accessible.</span></p>
<p><span style="font-size:small">Copy the manifest.xml to a location that is accessible by the Exchange Server during installation (for example, local drive, on a server) so that when you install the mail app on the Unified Management Console for the mailbox,
 you can specify the path to the manifest. After installation, the manifest is stored on the Exchange Server for the mailbox.</span></p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">Using the Exchange 2013 email account, create an email message or appointment and add one or more URLs to YouTube videos to the body. The mail app activates when you view the item in the read inspector or Reading Pane (in the
 case of a message). Choose the <strong>YouTube</strong> app button to start the app.</span></p>
<h1>Related content</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142216.aspx">How to: Build a mail app to view YouTube videos</a></span>
</li></ul>
