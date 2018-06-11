# Exchange 2010: Implementing Exchange ActiveSync Folder Sync
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Exchange Server 2010
## Topics
* ActiveSync
* protocol
## IsPublished
* True
## ModifiedDate
* 2012-07-20 01:09:44
## Description

<p><span style="font-size:small">This sample extends the sample code from the <a href="http://msdn.microsoft.com/en-us/library/hh361570(EXCHG.140).aspx">
Implementing an Exchange ActiveSync client: the transport mechanism</a> sample, and the
<a href="http://msdn.microsoft.com/en-us/library/hh531590(EXCHG.140).aspx">Implementing an Exchange ActiveSync client: provisioning</a> sample to add the following functionality:</span></p>
<ul>
<li><span style="font-size:small">The ability to synchronize additions, deletes, and updates to the folder hierarchy from the server using the
<strong>FolderSync</strong> command.</span> </li><li><span style="font-size:small">The ability to synchronize additions to the contents of a folder from the server using the
<strong>Sync</strong> command.</span> </li></ul>
<p><span style="font-size:small">This sample uses information in the <a href="http://msdn.microsoft.com/en-us/library/dd299441(EXCHG.80).aspx">
[MS-ASCMD]: ActiveSync Command Reference Protocol Specification</a>&nbsp;and the <a href="http://msdn.microsoft.com/en-us/library/dd299454(EXCHG.80).aspx">
[MS-ASAIRS]: ActiveSync AirSyncBase Namespace Protocol Specification</a> to implement the functionality described previously.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">&bull;&nbsp;A target server that is running Microsoft Exchange Server 2010 Service Pack 1 (SP1) or a later version of Exchange.</span>
</li><li><span style="font-size:small">The .NET Framework version 4.0.</span> </li><li><span style="font-size:small">Visual Studio 2010 with the C# component.</span><br>
<span style="font-size:small">Or</span> </li><li><span style="font-size:small">A text editor to create and edit source code files and a command prompt window to run a .NET Framework command-line compiler.</span>
</li></ul>
<h1>Sample components</h1>
<p><span style="font-size:small">This sample contains the following files:</span></p>
<ul>
<li><span style="font-size:small">EX2010_activesyncfolder_cs.sln &mdash; The Visual Studio 2010 solution file for the EX2010_activesyncfolder_cs project.</span>
</li><li><span style="font-size:small">EX2010_activesyncfolder_cs.csproj &mdash; The Visual Studio 2010 project file for the sample application.</span>
</li><li><span style="font-size:small">ASCommandRequest.cs &mdash; Contains the using statements, namespace, class, and functions to send a generic Exchange ActiveSync command request to the server.</span>
</li><li><span style="font-size:small">ASCommandResponse.cs &mdash; Contains the using statements, namespace, class, and functions to parse an Exchange ActiveSync command response from the server.</span>
</li><li><span style="font-size:small">ASError.cs &mdash; Contains the using statements, namespace, class, and functions to display an exception to the user.</span>
</li><li><span style="font-size:small">ASFolderSyncRequest.cs &mdash; Contains the using statements, namespace, class, and functions to send a
<strong>FolderSync</strong> command request to the server.</span> </li><li><span style="font-size:small">ASFolderSyncResponse.cs &mdash; Contains the using statements, namespace, class, and functions to parse a
<strong>FolderSync</strong> command response from the server.</span> </li><li><span style="font-size:small">ASOptionsRequest.cs &mdash; Contains the using statements, namespace, class, and functions to send an HTTP OPTIONS request to the server.</span>
</li><li><span style="font-size:small">ASOptionsResponse.cs &mdash; Contains the using statements, namespace, class, and functions to parse an HTTP OPTIONS response from the server.</span>
</li><li><span style="font-size:small">ASPolicy.cs &mdash; Contains the using statements, namespace, class, and functions to parse an XML document containing an Exchange ActiveSync policy.</span>
</li><li><span style="font-size:small">ASProvisionRequest.cs &mdash; Contains the using statements, namespace, class, and functions to send a
<strong>Provision</strong> command request to the server.</span> </li><li><span style="font-size:small">ASProvisionResponse.cs &mdash; Contains the using statements, namespace, class, and functions to parse a
<strong>Provision</strong> command response from the server.</span> </li><li><span style="font-size:small">ASSyncRequest.cs &mdash; Contains the using statements, namespace, class, and functions to send a
<strong>Sync</strong> command request to the server.</span> </li><li><span style="font-size:small">ASSyncResponse.cs &mdash; Contains the using statements, namespace, class, and functions to parse a
<strong>Sync</strong> command response from the server.</span> </li><li><span style="font-size:small">ASWBXML.cs &mdash; Contains the using statements, namespace, class, and functions to encode an XML document into a WBXML binary stream, and vice-versa.</span>
</li><li><span style="font-size:small">ASWBXMLByteQueue.cs &mdash; Contains the using statements, namespace, class, and functions to manage a WBXML binary stream as a
<strong>.NET Queue </strong>object.</span> </li><li><span style="font-size:small">ASWBXMLCodePage.cs &mdash; Contains the using statements, namespace, class, and functions to manage WBXML code pages.</span>
</li><li><span style="font-size:small">Device.cs &mdash; Contains the using statements, namespace, class, and functions to generate a &lt;DeviceInformation&gt; element.</span>
</li><li><span style="font-size:small">EncodedRequest.cs &mdash; Contains the using statements, namespace, class, and functions to generate a base64-encoded request URI for ActiveSync command requests.</span>
</li><li><span style="font-size:small">Folder.cs &mdash; Contains the using statements, namespace, class, and functions to manage the client's local copy of a folder in a user's mailbox.</span>
</li><li><span style="font-size:small">Program.cs &mdash; Contains the using statements, namespace, class, and functions to send an OPTIONS request, provision, sync the folder hierarchy, and download the contents in the user's inbox.</span>
</li><li><span style="font-size:small">ServerSyncCommand.cs &mdash; Contains the using statements, namespace, class, and functions to parse an
<strong>Add</strong> element within a <strong>Command</strong> element in a <strong>
Sync</strong> command response from the server.</span> </li><li><span style="font-size:small">Utilities.cs &mdash; Contains the using statements, namespace, class, and functions to display binary data as a hexadecimal string and to convert a hexadecimal string into binary data.</span>
</li></ul>
<h1>Configuring the sample</h1>
<p><span style="font-size:small">Follow these steps to configure the Exchange ActiveSync folder sync sample.</span></p>
<ol>
<li><span style="font-size:small">Replace the value of the <strong>activeSyncServer</strong> variable in the Program.cs file with the fully-qualified domain name of your Exchange 2010 SP1 server.</span>
</li><li><span style="font-size:small">Replace the value of the <strong>userName</strong> variable in the Program.cs file with the username of the mailbox you are using.</span>
</li><li><span style="font-size:small">Replace the value of the <strong>password</strong> variable in the Program.cs file with the password of the user account indicated in the
<strong>userName</strong> variable.</span> </li><li><span style="font-size:small">Replace the value of the <strong>domainName</strong> variable in the Program.cs file with the domain name of the user account indicated in the
<strong>userName</strong> variable.</span> </li><li><span style="font-size:small">Replace the value of the <strong>mailboxCacheLocation</strong> variable in the Program.cs file with the full path to a local directory on your computer where you have write access.</span>
</li></ol>
<h1>Building the sample</h1>
<p><span style="font-size:small">Press <strong>F6</strong> to build and deploy the sample.</span></p>
<h1>Running and testing the sample</h1>
<p><span style="font-size:small">Press <strong>F5</strong> to run the sample.</span></p>
<h1>Related topics</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/3718e941-b25a-4760-bc0a-7b650e4825c1">Implementing an Exchange ActiveSync client: folder synchronization</a></span>
</li></ul>
<h1>Change log</h1>
<p><span style="font-size:small">First release.</span></p>
