# Lync 2010: Creating Call Center Application with UCMA 3.0 Core SDK and Lync 2010
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010 SDK
* Unified Communications Managed API (UCMA) 3.0
* Microsoft Lync 2010
## Topics
* Speech recognition
* Speech Synthesis
* application development
## IsPublished
* True
## ModifiedDate
* 2012-03-01 11:13:18
## Description

<h1><span style="font-size:medium">Introduction</span></h1>
<p><span style="font-size:small">This sample contains two applications that demonstrate how to use C# code, the Microsoft Unified Communications Managed API (UCMA) 3.0 Core SDK, and the Microsoft Lync 2010 SDK to build a simple call center application. The
 UCMA 3.0 application is called BotApplication, and the Lync 2010 application is called AgentApplication.</span></p>
<p><span style="font-size:small">Application scenario:</span></p>
<ol>
<li><span style="font-size:small">The customer makes a voice call that is answered by the bot.</span>
</li><li><span style="font-size:small">The bot asks what department the customer wants to speak to, and asks for the customer&rsquo;s name.</span>
</li><li><span style="font-size:small">The bot transfers the voice call to the appropriate agent.</span>
</li><li><span style="font-size:small">The bot completes a simple lookup, and uses the customer name to get the customer ID and account balance.</span>
</li><li><span style="font-size:small">The agent picks up the transferred call, and the Lync client contextual application opens on the agent&rsquo;s desktop. The Lync application displays the customer&rsquo;s name, account balance, and account number.</span>
</li></ol>
<h1><span style="font-size:medium"><span style="font-size:small">Prerequisites</span></span></h1>
<ul>
<li><span style="font-size:small">Microsoft Unified Communications Managed API 3.0 SDK
<a href="http://www.microsoft.com/download/en/details.aspx?id=10566">http://www.microsoft.com/download/en/details.aspx?id=10566</a></span>
</li><li><span style="font-size:small">Microsoft Lync 2010 SDK&lt; <a href="http://www.microsoft.com/download/en/details.aspx?id=18898">
http://www.microsoft.com/download/en/details.aspx?id=18898</a> &gt;.</span> </li><li><span style="font-size:small">Microsoft Silverlight 4 SDK &lt;<a href="http://www.microsoft.com/download/en/details.aspx?id=7335">http://www.microsoft.com/download/en/details.aspx?id=7335</a>&gt;.</span>
</li><li><span style="font-size:small">Microsoft Silverlight 4 Tools for Visual Studio 2010 &lt;<a href="http://www.microsoft.com/download/en/details.aspx?id=18149">http://www.microsoft.com/download/en/details.aspx?id=18149</a>&gt;.</span>
</li><li><span style="font-size:small">Microsoft Speech Platform SDK(x64) Version 10.2 &lt;
<a href="http://www.microsoft.com/download/en/details.aspx?id=14373">http://www.microsoft.com/download/en/details.aspx?id=14373</a>&gt;</span>
</li><li><span style="font-size:small">Microsoft Server Speech Recognition Language &ndash; (MSSpeech_SR_en-US_TELE.msi) &lt;
<a href="http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=21924">
http://www.microsoft.com/download/en/details.aspx?displaylang=en&amp;id=21924</a>&gt;</span>
</li><li><span style="font-size:small">Microsoft Server Speech Text to Speech Voice &ndash; (MSSpeech_TTS_en-US_Helen.msi) &lt;
<a href="http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=21924">
http://www.microsoft.com/download/en/details.aspx?displaylang=en&amp;id=21924</a>&gt;</span>
</li><li><span style="font-size:small">Microsoft Visual Studio 2010 development system.</span>
</li><li><span style="font-size:small">Two computers running the Lync 2010 client. One computer hosts the bot application. The second computer hosts the agent application.</span>
</li></ul>
<h1><span style="font-size:medium">Description</span></h1>
<p><span style="font-size:small">This code sample provides the basis for a simple call center application. For more information, see the following procedures and the items that appear in the More Information section.</span></p>
<h1><span style="font-size:small">To set up the bot application</span></h1>
<ol>
<li><span style="font-size:small">Download the code by clicking the <a id="52532" href="/Lync-2010-Creating-Call-fade42f0/file/52532/1/CallCenterApplications.zip">
CallCenterApplications.zip</a> link, and then unpack the bot application.</span> </li><li><span style="font-size:small">Unpack the bot application on a 64-bit computer where the UCMA 3.0 Core SDK is installed, along with the Speech Platform SDK and the TTS and speech recognition components.</span>
</li><li><span style="font-size:small">Open and build the bot application.</span> </li><li><span style="font-size:small">Make the appropriate changes to App.config.</span>
</li><li><span style="font-size:small">Register the application. For more information, see
<a href="http://msdn.microsoft.com/en-us/library/hh378557.aspx">Register Contextual Conversation Packages in Lync 2010</a>.</span>
</li></ol>
<h1><span style="font-size:small">To set up the agent application</span></h1>
<ol>
<li><span style="font-size:small">On a separate computer where the Lync 2010 SDK is installed, download the code by clicking the
<a id="52532" href="/Lync-2010-Creating-Call-fade42f0/file/52532/1/CallCenterApplications.zip">
CallCenterApplications.zip</a> link, and then unpack the agent application.</span>
</li><li><span style="font-size:small">Open and build the agent application.</span> </li><li><span style="font-size:small">Register the application. Use the same GUID as the bot application.</span>
</li></ol>
<h1><span style="font-size:small">To try out the call center application</span></h1>
<ol>
<li><span style="font-size:small">Run the bot application.</span> </li><li><span style="font-size:small">Make a voice call to the Lync client on the computer running the bot application.</span>
</li><li><span style="font-size:small">When asked which department you want to speak to, answer either sales or billing.</span>
</li><li><span style="font-size:small">When asked for a name, answer either Terry Adams or Toni Poe.</span>
</li><li><span style="font-size:small">On the computer where the agent application is installed, answer the call. The Microsoft Silverlight application opens in the Conversation Window Extension (CWE) and displays the customer&rsquo;s account balance, ID, and name.</span>
</li></ol>
<h1><span style="font-size:medium">More Information</span></h1>
<ul>
<li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh847405.aspx">
Comparing UCMA 3.0 Call Transfers</a></span> </li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh527748.aspx">
Using UCMA 3.0 and Lync 2010 for Contextual Communication</a></span> </li><li><span style="font-size:small">MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/hh538500.aspx">
Creating Speech Recognition Calculators in UCMA 3.0</a></span> </li></ul>
