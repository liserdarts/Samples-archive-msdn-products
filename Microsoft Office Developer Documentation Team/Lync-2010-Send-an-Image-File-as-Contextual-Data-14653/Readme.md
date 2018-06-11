# Lync 2010:  Send an Image File as Contextual Data
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010 SDK
## Topics
* contextual conversation
## IsPublished
* True
## ModifiedDate
* 2011-12-30 05:13:43
## Description

<h1>Introduction</h1>
<div><span style="font-size:small">This Microsoft Lync 2010 SDK sample code shows how you can send an image file as contextual data in a Microsoft Lync 2010 conversation.</span></div>
<div><span style="font-size:20px; font-weight:bold">Description</span></div>
<div><span style="font-size:small">Contextual conversation is an added feature of the Lync 2010 SDK. It lets you quickly share added data with whoever is involved in the conversation. In the code sample, the data channel is opened by an instant message (IM).
 Then, an image file is packaged and sent from one computer, and unpackaged and displayed on a second computer. The Lync 2010 SDK includes two methods that can be used to send contextual data in a Lync 2010 conversation: BeginSendInitialContext and BeginSendContextData.</span></div>
<ul>
<li><span style="font-size:small">BeginSendInitialContext is the setup method, call it first. This method can be used multiple times in a single conversation, for example, if there is a change in subject and additional data is needed. Data limit is 2,000 characters.</span>
</li><li><span style="font-size:small">Use BeginSendContextData for more advanced scenarios, such as implementing a command and response protocol, or exchanging large amounts of data. It can only be used after a session is established. However, it can be used again
 in an existing conversation as often as needed. Data limit is 64,000 characters.</span>
</li></ul>
<div><span style="font-size:small">Here is an overview of the scenario that this code sample supports:</span></div>
<ol>
<li><span style="font-size:small">Send contextual data in the first IM message to open up the data channel between the two computers.</span>
</li><li><span style="font-size:small">Use the OpenFileDialog class to select and open the image file &ndash; this gets around the Silverlight restriction onaccess to the local file system.</span>
</li><li><span style="font-size:small">Send the image as contextual data, which requires that data be a base-64-encoded string.</span>
</li><li><span style="font-size:small">Use the BeginSendContextData method to send the string, which provides a capacity of 64 KB &ndash; certainly helpful when sending images. For larger files, consider breaking the file into pieces before transmission, and then
 assembling them again on receipt.</span> </li></ol>
<h1>More Information</h1>
<div><span style="font-size:small">Channel 9 Video:&nbsp;<a href="http://channel9.msdn.com/posts/Lync-Contextual-Data-Part-3-Passing-an-Image-File">Lync Contextual Data Part 3: Passing an Image File</a></span></div>
<div><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh378542.aspx">
Contextual Conversations</a></span></div>
<div><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh228155.aspx">
Working with Lync 2010 Contextual Data Methods and Events</a></span></div>
