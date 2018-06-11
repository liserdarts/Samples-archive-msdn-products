# Lync 2013: Retrieve and publish Self contact info using the Lync 2013 Model API
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2013
* Microsoft Lync Server 2013
## Topics
* Contacts
* Lync 2013 Model API
## IsPublished
* True
## ModifiedDate
* 2013-02-07 04:46:16
## Description

<div id="header">Summary: Use this code sample to learn how to use the Lync 2013 Model API to retrieve and publish Self contact information. The Self contact is the signed-in user.</div>
<div id="mainSection">
<div id="mainBody">
<div class="introduction">
<p>&nbsp;<a id="75711" href="/Lync-2013-Retrieve-and-91ebdca7/file/75711/1/Lync2013_ContactInformation.zip">Lync2013_ContactInformation.zip</a></p>
</div>
<h1 class="heading">Description of the sample</h1>
<div class="section" id="sectionSection0">
<p>This sample demonstrates how to use the Lync Model API to retrieve and publish information that is associated with the Self contact, such as availability and personal note. It also demonstrates how to sign in to Lync by using the credentials of the active
 user.</p>
<p>Sample features:</p>
<ul>
<li>
<p>Retrieve information for a Lync contact such as the name, photo, current availability, and personal note.</p>
</li><li>
<p>Sign in to Lync by using the credentials of the user that is currently logged in to the computer.</p>
</li><li>
<p>Sign out from Lync.</p>
</li><li>
<p>Handle Lync events to respond to changes in the client state and changes in the contact information.</p>
</li></ul>
<p>This sample, ContactInformation, is distributed with the Lync 2013 SDK. The Lync 2013 SDK includes three development models:</p>
<ul>
<li>
<p>Lync Controls</p>
</li><li>
<p>Lync 2013 API</p>
</li><li>
<p>OCOM Unmanaged COM API</p>
</li></ul>
<p>You can drag-and-drop Microsoft Lync Controls into existing business applications to add Lync functions and user interface. Each Lync Control provides a specific feature like search, presence, instant messaging (IM) calls, and audio calls. The appearance
 of each control replicates the Lync UI for that feature. Use a single control or multiple controls. The programming style is primarily XAML text. However, you can also use C# in the code-behind file to access the Lync 2013 API and the .NET Framework.</p>
<p>Use the Lync 2013 API to start and automate the Lync UI in your business application, or add Lync functionality to new or existing .NET Framework applications and suppress the Lync UI. Lync SDK UI Automation automates the Lync UI, and Lync Controls add separate
 pieces of Lync functionality and UI as XAML controls.</p>
<p>Use the Lync 2010 API Reference to learn about the unmanaged Office Communicator Object Model API (OCOM). The OCOM API contains a subset of the types that are in the Lync 2013 API. You cannot start or carry on conversations with OCOM. But you can access
 a contact list and get contact presence.</p>
<h3 class="subHeading">Prerequisites for running and compiling sample in Visual Studio</h3>
<div class="subsection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>.NET Framework 4.0 and later versions of .NET Framework</p>
</li><li>
<p>Visual Studio 2010 and later versions of Visual Studio</p>
</li><li>
<p>Lync 2010 SDK and later versions of the Lync SDK</p>
</li></ul>
</div>
<h3 class="subHeading">Prerequisites for running installed sample on client computer</h3>
<div class="subsection">
<p>This sample requires the following:</p>
<ul>
<li>
<p>A running instance of Lync</p>
</li></ul>
</div>
<h3 class="subHeading">Run and test the sample</h3>
<div class="subsection">
<p>This sample was designed to be run locally on the computer running Lync 2013.</p>
<ol>
<li>
<p>Open ContactInformation.csproj.</p>
</li><li>
<p>In Visual Studio, press F5.</p>
</li></ol>
</div>
<h3 class="subHeading">Related content</h3>
<div class="subsection">
<p>Explore the following Lync developer resources.</p>
<ul>
<li>
<p><a href="http://msdn.microsoft.com/en-us/library/gg455051.aspx" target="_blank">MSDN Library: Lync</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/lync/gg132942.aspx" target="_blank">Lync Developer Center</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/office/aa905340.aspx" target="_blank">Office Developer Center</a></p>
</li><li>
<p><a href="http://msdn.microsoft.com/en-us/office/hh506337.aspx" target="_blank">Office 365 Developer Hub</a></p>
</li><li>
<p><a href="http://gallery.technet.microsoft.com/Lync-2010-API-Reference-48d2c5c9" target="_blank">Lync 2010 API Reference</a></p>
</li></ul>
</div>
</div>
</div>
</div>
