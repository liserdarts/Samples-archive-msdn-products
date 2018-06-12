# Lync 2013: UCWA Windows Store app to start and and accept IM calls
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Lync Server 2013
* Lync 2013
## Topics
* Windows Store app
* UCWA events
* instant message
## IsPublished
* True
## ModifiedDate
* 2014-06-30 03:01:28
## Description

<p id="header">This is a Visual Studio solution of a sample application to illustrate how to make an outgoing IM call and answer an incoming call in a UCWA Windows Store app using C#/XAML and XML.</p>
<div id="mainSection">
<div id="mainBody">
<div id="sectionSection0">
<h3>Problem</h3>
<div>
<p>You want to initiate or accept an IM call invitation and send or receive messages in a conversation.</p>
</div>
<h3>Solution</h3>
<div>
<p>This application illustrates the basic steps to start an IM invitation, monitor the invitation status changes before proceding to send an IM message. It also shows the basic stpes to accept an IM invitation and receiving incoming IM messages, through the
 UCWA event channel in a UCWA Windows Store app using C#/XAML and XML.</p>
</div>
<h3>Features</h3>
<div>
<p>The application project continues from the UcwaWinStoreEvents sample app and adds the following programming tasks.</p>
<ul>
<li>Send an IM invitation with a HTTP POST request on the <strong>startMessaging</strong> resource.
</li><li>Monitor the invitation status by handling the <strong>mesageInvitation</strong> events sent by the
<strong>communication</strong> and <strong>conversation</strong> resoures. </li><li>Send an IM message with an HTTP POST request against the <strong>sendMessage</strong> resource.
</li><li>Accept an incoming invitation by handling the <strong>mesageInvitation</strong> events from
<strong>communication</strong>. </li><li>Receive an incoming message by handling <strong>mesage</strong> event from <strong>
conversation</strong> </li><li>Parse messages in plain text and of the HTML format. </li></ul>
</div>
<h3>Prerequisites</h3>
<div>
<ul>
<li>Microsoft Lync Server 2013 deployment </li><li>Windows 8.1 </li><li>Microsoft Visual Studio 2013, with Microsoft .Net Framework 4.5 </li></ul>
</div>
<h3>Installing and running the application</h3>
<div>
<ol>
<li>Unzip the download package and open it in Visual Studio
<ul>
<li>Compile and build the project. </li></ul>
</li><li>Obtain a Windows Store app development license. </li><li>Run the application.
<ul>
<li>In Visual Studio, hit F5 to run in the simulator. </li></ul>
</li></ol>
</div>
</div>
</div>
</div>
