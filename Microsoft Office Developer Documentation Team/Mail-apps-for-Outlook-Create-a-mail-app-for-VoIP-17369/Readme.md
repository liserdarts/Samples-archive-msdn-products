# Mail apps for Outlook: Create a mail app for VoIP dialing
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
* mail app
## IsPublished
* True
## ModifiedDate
* 2013-08-28 05:47:19
## Description

<p><span style="font-size:small">This mail app allows users to conveniently dial a telephone number in the app pane in Outlook, if the selected email message or appointment contains one or more telephone numbers. The following figure is a screen shot of the
 Phone Dialer mail app activated for a message in the Reading Pane. The accompanying topic,
<a href="http://msdn.microsoft.com/en-us/library/office/apps/fp142164(v=office.15).aspx">
How to: Build a mail app for Voice Over IP dialing</a>,&nbsp;also shows a video of this mail app in action.</span></p>
<p><img id="60114" src="http://i1.code.msdn.s-msft.com/mail-apps-for-outlook-dc28341f/image/file/60114/1/mailapp_small.png" alt="" width="645" height="454"></p>
<p><span style="font-size:small">The main code files for this mail app are manifest.xml, dialer.htm, and dialer.js, along with logo image files for this app and the JavaScript library and string files for apps for Office. The following is a high level summary
 of how the mail app works:</span></p>
<p style="padding-left:30px"><span style="font-size:small">1.&nbsp;This mail app specifies in the manifest.xml file that it requires a host application that supports the mailbox capability:</span></p>
<p style="padding-left:30px"><span style="font-size:small">&lt;Capabilities&gt;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;Capability Name=&quot;Mailbox&quot;/&gt;</span><br>
<span style="font-size:small">&lt;/Capabilities&gt;</span></p>
<p style="padding-left:30px"><span style="font-size:small">In Office 2013,&nbsp;mail apps are supported in the Outlook rich client and&nbsp;Outlook Web App.</span></p>
<p style="padding-left:30px"><span style="font-size:small">2.&nbsp;The mail app specifies in the manifest file its support for the desktop form factor. This further determines that in Office 2013, the applications that can host this mail app are the Outlook
 rich client and Outlook Web App.</span></p>
<p style="padding-left:30px"><span style="font-size:small">&lt;DesktopSettings&gt;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;!-- Change the following line to specify the web server where the HTML file is hosted. --&gt;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;SourceLocation DefaultValue=&quot;https://webserver/PhoneDialer/dialer.htm&quot; /&gt;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; &lt;RequestedHeight&gt;150&lt;/RequestedHeight&gt;</span><br>
<span style="font-size:small">&lt;/DesktopSettings&gt;</span></p>
<p style="padding-left:30px"><span style="font-size:small">3.&nbsp;The mail requests the
<strong>ReadItem</strong> permission in the manifest file.</span></p>
<p style="padding-left:30px"><span style="font-size:small">&lt;Permissions&gt;ReadItem&lt;/Permissions&gt;&nbsp;</span></p>
<p style="padding-left:30px"><span style="font-size:small">4.&nbsp;The host application activates this mail app when the selected message or appointment contains a telephone number. It does so by first reading on startup the manifest.xml file which specifies
 an <strong><a href="http://msdn.microsoft.com/en-us/library/fp161166(v=office.15)">ItemHasKnownEntity</a></strong> activation rule:</span></p>
<p style="padding-left:30px"><span style="font-size:small">&lt;Rule xsi:type=&quot;ItemHasKnownEntity&quot; EntityType=&quot;PhoneNumber&quot;/&gt;</span></p>
<p style="padding-left:30px"><span style="font-size:small">5.&nbsp;The mail app defines an initialize function which is an event handler for the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp161139.aspx">initialize</a></strong> event. The initialize function obtains the selected item by first using the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp142162(v=office.15)">Mailbox</a></strong> object from the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp161058(v=office.15)">Office.context.mailbox</a></strong> property, then using the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp142196(v=office.15)">item</a>&nbsp;</strong>property of the
<strong>Mailbox</strong> object. At the end of the initialize event handler, it calls the onInitializeComplete function.</span></p>
<p style="padding-left:30px"><span style="font-size:small">// The initialize function is required for all apps.</span><br>
<span style="font-size:small">Office.initialize = function () {</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; item = Office.context.mailbox.item;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; onInitializeComplete();</span><br>
<span style="font-size:small">}</span></p>
<p style="padding-left:30px"><span style="font-size:small">6.&nbsp;This mail app makes sure that the main function of the mail app, initDialer, is called only when both the run-time environment is loaded and the DOM is ready. It does so with the help of the
 onInitializeComplete and loadcomplete functions. The loadcomplete function is an event handler of the
<strong>onload</strong> event which fires when the browser has loaded the HTML body and the DOM is ready. When both the run-time environment is loaded (init is true) and the DOM is ready (bodyload is true), the onInitializeComplete function or loadcomplete
 function, whichever occurs later, calls the main function of the mail app, initDialer, as shown in the code snippet below:</span></p>
<p style="padding-left:30px"><span style="font-size:small">// Handler for initialization complete.</span><br>
<span style="font-size:small">function onInitializeComplete() {</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; init = true;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; if (bodyload &amp;&amp; init) {</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; initDialer();</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; }</span><br>
<span style="font-size:small">}</span></p>
<p style="padding-left:30px"><span style="font-size:small">// Handler for app body onload.</span><br>
<span style="font-size:small">function loadcomplete() {</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; bodyload = true;</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; if (bodyload &amp;&amp; init) {</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; initDialer();</span><br>
<span style="font-size:small">&nbsp;&nbsp;&nbsp; }</span><br>
<span style="font-size:small">}</span></p>
<p style="padding-left:30px"><span style="font-size:small">7.&nbsp;The initDialer function uses the
<strong>getEntities</strong> method of the current item to return an array of supported entities in the subject or body of the current item. initDialer then uses the
<strong><a href="http://msdn.microsoft.com/en-us/library/fp160970(v=office.15)">phoneNumbers</a></strong> property to get an array of one or more phone numbers among the entities. initDialer parses the array, associates each telephone number with a
<strong>callto:tel:</strong> hyperlink. When the user chooses one of these links, Lync opens and the user can proceed to use Lync to dial the number.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">The following are the requirements for the sample:</span></p>
<ul>
<li><span style="font-size:small">Your favorite web development tool and web server to author and host HTML and JavaScript files.</span>
</li><li><span style="font-size:small">An email account on Exchange Server 2013.</span>
</li><li><span style="font-size:small">Client applications that support the mailbox capability in Office 2013, on the desktop and tablet form factors: Outlook 2013&nbsp;and Outlook Web App.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Required technical familiarity: HTML, JavaScript.</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The following are the primary files in the sample:</span></p>
<ul>
<li><span style="font-size:small">manifest.xml</span> </li><li><span style="font-size:small">dialer.htm</span> </li><li><span style="font-size:small">dialer.js</span> </li><li><span style="font-size:small">Icon_narrow.png</span> </li><li><span style="font-size:small">Outlook.css</span> </li><li><span style="line-height:115%; font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;; font-size:11pt">MicrosoftAjax.js</span>
</li><li><span style="line-height:115%; font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;; font-size:11pt">jquery-1.4.2.js</span>
</li><li><span style="line-height:115%; font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;; font-size:11pt">json2.js</span>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">Host the dialer.htm file on a web server, and make sure the paths to the image, CSS and JavaScript files are specified appropriately in the dialer.htm file so that they are accessible.</span></p>
<p><span style="font-size:small">Copy the manifest.xml to a location that is accessible by the Exchange Server during installation, for example, local drive, on a server, so that when you install the mail app on the Unified Management Console for the mailbox,
 you can specify the path to the manifest. After installation, the manifest is stored on the Exchange Server for the mailbox.</span></p>
<h1>Run and test the sample</h1>
<p><span style="font-size:small">Using the Exchange 2013 email account, create an email message or appointment and add one or more telephone numbers to the subject or body. The mail app activates when you view the item in the read inspector or Reading Pane
 (in the case of a message). Choose the <strong>Phone Dialer </strong>app button to start the app.</span></p>
<h1>Related content</h1>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp142164(v=office.15)">How to: Build a mail app for Voice Over IP dialing</a></span></p>
