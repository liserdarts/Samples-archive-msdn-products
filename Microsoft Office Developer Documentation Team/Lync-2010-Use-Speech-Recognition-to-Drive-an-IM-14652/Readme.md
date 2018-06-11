# Lync 2010:  Use Speech Recognition to Drive an IM Broadcast
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010 SDK
* Unified Communications Managed API (UCMA) 3.0
* Microsoft Lync 2010
## Topics
* Speech recognition
* Instant messaging
* IM broadcast
## IsPublished
* True
## ModifiedDate
* 2011-12-30 04:43:18
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This sample code shows how you can use the Microsoft Unified Communications Managed API (UCMA) 3.0 Core SDK to easily and quickly build a Microsoft Lync 2010 middle-tier application that will receive a phone call, and broadcast
 an IM message to a distribution list that is based on the content of the call.</span></p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><span style="font-size:small">In this scenario, the UCMA 3.0 application is running on the application server, waiting for a call. The user places a voice call to the application by using Lync 2010. When the UCMA 3.0 application answers, the user speaks
 words recognized by the application grammar. The application uses the recognized text from the voice call to compose the text of an IM. Finally, the Lync SDK Automation API uses each SIP URI contained in a List object, passed as a parameter to the BeginStartConversation
 method, and the IM is sent.</span></p>
<p><span style="font-size:small">This application features use of the SpeechRecognitionEngine class, provided by the Microsoft Speech Platform SDK, and the SpeechRecognitionConnector and SpeechRecognitionStream classes, provided by the UCMA 3.0 Core SDK. Also,
 the application uses an XML grammar file as input to the Grammar class that is provided by the Speech Platform SDK.</span></p>
<p><span style="font-size:small">The scenario:</span></p>
<ol>
<li><span style="font-size:small">User places a Lync voice call to the application.&nbsp;</span>
</li><li><span style="font-size:small">The UCMA 3.0 application answers.</span> </li><li><span style="font-size:small">The user speaks words recognized by the application grammar.</span>
</li><li><span style="font-size:small">The application performs speech recognition.</span>
</li><li><span style="font-size:small">The application uses the recognized text from the voice call to compose the text of an IM.&nbsp;</span>
</li><li><span style="font-size:small">The Lync SDK Automation API is used to send the IM.
</span></li></ol>
<h1>More Information</h1>
<p><span style="font-size:small">Channel 9 Video: <a href="http://channel9.msdn.com/posts/Use-Speech-Recognition-to-Drive-an-IM-Broadcast">
Use Speech Recognition to Drive an IM Broadcast</a></span></p>
<p><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh538498.aspx">
Broadcasting IM Text Based on Speech Recognition in a UCMA Application</a></span><em><br>
</em></p>
