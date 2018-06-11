# Lync 2010: Transferring Files in Lync 2010 SDK Applications with UI Suppression
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Lync 2013 Preview
## Topics
* SDK
* Lync 2010
* application development
## IsPublished
* True
## ModifiedDate
* 2012-04-25 02:03:53
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample contains a pair of applications that demonstrate how to use C# code and the Microsoft Lync 2010 SDK to perform file transfer from a Lync client application when the Lync UI is suppressed.</span></p>
<p><span style="font-size:small">Using Lync 2010 SDK it is not possible to transfer files when both the sending and receiving clients are UI suppressed. However, it is possible when the receiving client, as in this demonstration, is not UI suppressed.</span></p>
<p><span style="font-size:small">This is how the applications perform the file transfer.</span></p>
<ol>
<li><span style="font-size:small">The sending application uses the <strong>InstantMessageModality.BeginSendMessage()</strong> method to open a conversation between the clients on the two computers. This allows the target application to get the
<strong>Conversation</strong> object for the conversation.</span> </li><li><span style="font-size:small">The sending application uses the <strong>Conversation.BeginSendInitialContext()</strong> method to set up the contextual data session.</span>
</li><li><span style="font-size:small">The sending application uses the <strong>Convert.ToBase64String()</strong> method to convert a selected file to a string.</span>
</li><li><span style="font-size:small">The sending application uses the <strong>Conversation.BeginSendContextData()</strong> method to send the file to the target computer.</span>
</li><li><span style="font-size:small">The target computer uses the <strong>Convert.FromBase64String()</strong> method to convert the base 64 string received from the sender computer into a .zip or .xlsx file.</span>
</li></ol>
<h1><span>Prerequisites</span></h1>
<ul>
<li><span style="font-size:small">Microsoft Lync 2010 SDK&lt; <a href="http://www.microsoft.com/download/en/details.aspx?id=18898">
http://www.microsoft.com/download/en/details.aspx?id=18898</a> &gt;.</span> </li><li><span style="font-size:small">Microsoft Visual Studio 2010.</span> </li><li><span style="font-size:small">Two computers running the Lync 2010 client. One computer hosts the sender application. The second computer hosts the target application.</span>
</li></ul>
<p><span style="font-size:small"></span><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">This code sample provides the basis for performing file transfer from a Lync application when its UI is suppressed. For more information, see the following procedures and the items listed under More Information.</span></p>
<h2><span style="font-size:small">To set up the sender application</span></h2>
<ol>
<li><span style="font-size:small">On a computer where the user is signed into Lync 2010 and the Lync 2010 SDK is installed, download the code by clicking the
<a id="56786" href="/site/view/file/56786/1/FileTransferApplications.zip">FileTransferApplications.zip</a> link, and then unpack the sender application.</span>
</li><li><span style="font-size:small">Open and build the sender application.</span> </li><li><span style="font-size:small">Make the appropriate changes to App.config.</span>
</li><li><span style="font-size:small">Register the application. For more information, see Register Contextual Conversation Packages in Lync 2010 &lt;<a href="http://msdn.microsoft.com/en-us/library/hh378557.aspx">http://msdn.microsoft.com/en-us/library/hh378557.aspx</a>&gt;.</span>
</li></ol>
<h2><span style="font-size:small">To set up the target application</span></h2>
<ol>
<li><span style="font-size:small">On a separate computer where the user is signed into Lync 2010 and the Lync 2010 SDK is installed, download the code by clicking the
<a id="56786" href="/site/view/file/56786/1/FileTransferApplications.zip">FileTransferApplications.zip</a> link, and then unpack the target application.</span>
</li><li><span style="font-size:small">Open and build the target application.</span> </li><li><span style="font-size:small">Register the application. Use the same GUID as the sender application.</span>
</li></ol>
<h2><span style="font-size:small">To try out the file transfer applications</span></h2>
<ol>
<li><span style="font-size:small">On the sender computer, create sample Microsoft Excel and .zip files to transfer.</span>
</li><li><span style="font-size:small">On the sender computer, set Lync UI suppression to true. For more information, see Understanding UI Suppression in Lync 2010 SDK&lt;
<a href="http://msdn.microsoft.com/en-us/library/hh345230.aspx">http://msdn.microsoft.com/en-us/library/hh345230.aspx</a> &gt;.</span>
</li><li><span style="font-size:small">On the sender computer, close the Lync client.</span>
</li><li><span style="font-size:small">On the target computer, start the FileTransferTarget sample application.</span>
</li><li><span style="font-size:small">On the sender computer, start the FileTransferSender sample application.</span>
</li><li><span style="font-size:small">In the FileTransferSender application, enter your password in the
<strong>Password</strong> text box, and then click <strong>Sign In</strong>.</span>
</li><li><span style="font-size:small">Click <strong>Send IM</strong>.</span> </li><li><span style="font-size:small">On the target computer, click the IM toast. Receiving the IM allows the application to get the
<strong>Conversation</strong> object.</span> </li><li><span style="font-size:small">On the sender computer, in the FileTransferSender application, enter some brief text in the
<strong>Initial Context Data</strong> text box, and then click <strong>Send Initial Data</strong>. Sending initial context data sets up the data session.</span>
</li><li><span style="font-size:small">On the target computer, in the FileTransferTarget application, click
<strong>Display Initial Context</strong>. The text matches the text sent from the sender computer.</span>
</li><li><span style="font-size:small">On the sender computer, in the FileTransferSender application, click
<strong>File Transfer</strong>. In the <strong>Open</strong> dialog box, browse to the sample Excel file created in step 1 and then click
<strong>Open</strong>.</span> </li><li><span style="font-size:small">On the target computer, in the FileTransferTarget application, click
<strong>Transfer Excel</strong>.</span> </li><li><span style="font-size:small">On the sender computer, in the FileTransferSender application, click
<strong>File Transfer</strong>. In the <strong>Open</strong> dialog box, browse to the sample zip file created in step 1 and then click
<strong>Open</strong>.</span> </li><li><span style="font-size:small">On the target computer, in the FileTransferTarget application, click
<strong>Transfer Zip</strong>.</span> </li><li><span style="font-size:small">On the target computer, open and test the files transferred from the sender computer.</span>
</li></ol>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small">MSDN Library: Sharing Image Files as Contextual Data in Lync 2010 Conversations &lt;<a href="http://msdn.microsoft.com/en-us/library/hh771125.aspx">http://msdn.microsoft.com/en-us/library/hh771125.aspx</a>&gt;&gt;</span>
</li><li><span style="font-size:small">MSDN Library: Walkthrough: Sign In to Lync with UI Suppressed (Lync 2010 SDK)&lt;<a href="http://msdn.microsoft.com/en-us/library/hh378603.aspx">http://msdn.microsoft.com/en-us/library/hh378603.aspx</a>&gt;</span>
</li><li><span style="font-size:small">Video: Sign In to Lync with UI Suppressed&lt;<a href="http://channel9.msdn.com/posts/Sign-In-to-Lync-with-UI-Suppressed">http://channel9.msdn.com/posts/Sign-In-to-Lync-with-UI-Suppressed</a>&gt;</span>
</li><li><span style="font-size:small">Video: Lync Contextual Data Part 3: Passing an Image File&lt;<a href="http://channel9.msdn.com/posts/Lync-Contextual-Data-Part-3-Passing-an-Image-File">http://channel9.msdn.com/posts/Lync-Contextual-Data-Part-3-Passing-an-Image-File</a>&gt;</span><em><br>
</em></li></ul>
