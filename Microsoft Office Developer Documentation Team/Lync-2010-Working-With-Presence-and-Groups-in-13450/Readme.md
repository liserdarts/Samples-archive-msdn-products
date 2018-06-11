# Lync 2010: Working With Presence and Groups in UCMA 3.0
## Requires
* Visual Studio 2008
## License
* Apache License, Version 2.0
## Technologies
* Unified Communications Managed API (UCMA) 3.0
* Microsoft Lync 2010
## Topics
* Contacts
* Groups
* Presence
## IsPublished
* True
## ModifiedDate
* 2011-09-19 11:00:42
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Learn how to use presence to find an available member of a Microsoft Lync 2010 contact group, and then add the contact to a multiparty group conversation.</span></p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><span style="font-size:small">The fictitious Contoso Corporation deploys a UCMA 3.0 application that can be accessed by Lync 2010 users. The endpoint that represents this application has two contact groups, one for the Service Department and one for the
 Sales Department. Each of these contact groups lists the employees who are in that department.</span></p>
<p><span style="font-size:small"><em>Note: </em>The application as implemented in this article series requires two user-defined contact groups to be present in Lync 2010 on the computer that runs the application. Each of these groups must have at least one
 contact.</span></p>
<p><span style="font-size:small">A Lync 2010 user sends an instant message to the UCMA 3.0 application. The UCMA 3.0 application responds with a
</span><span style="font-size:small">simple text menu, requesting the Lync 2010 user to press 1 to be connected to someone in the Service Department, and to press 2 to be connected to someone in the Sales Department.</span></p>
<p><span style="font-size:small">Based on the user&rsquo;s choice, the UCMA 3.0 application checks either the Service Department contact group or the Sales
</span><span style="font-size:small">Department contact group. If a contact in the appropriate group is available, the UCMA 3.0 application escalates the existing conversation between itself and the Lync 2010 user to a conference (the Lync 2010 terminology,
 a group </span><span style="font-size:small">conversation), and then invites the contact to join the conference. After the contact accepts the conference invitation, the UCMA 3.0 application ends, and drops out of the conference. The two other parties remain
 in the conference, </span><span style="font-size:small">which is now a two-person group conversation.</span></p>
<p><span style="font-size:small">In the implementation that is described in this series of articles, the UCMA 3.0 application accepts only instant
</span><span style="font-size:small">message calls (instances of the <a href="http://msdn.microsoft.com/en-us/library/microsoft.rtc.collaboration.instantmessagingcall_di_3_uc_ocs14mreflyncuc3cr.aspx">
InstantMessagingCall</a> class). A different implementation might accept voice calls (instances of the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.rtc.collaboration.audiovideo.audiovideocall_di_3_uc_ocs14mreflyncuc3cr.aspx">
AudioVideoCall</a> class), and then transfer the incoming call to the appropriate contact. Because
</span><span style="font-size:small">an <span class="label">InstantMessagingCall</span> instance cannot be transferred, the workaround as implemented in this series of articles is to escalate the two-way instant messaging conversation to a conference, and
 then invite into the conference the contact that was previously found.</span></p>
<p><span style="font-size:small">The most important tasks that the UCMA 3.0 application performs are the following.</span></p>
<ul>
<li>
<p><span style="font-size:small">Based on the user&rsquo;s choice, it finds an available person in a contact
</span><span style="font-size:small">group.</span></p>
</li><li>
<p><span style="font-size:small">Escalates the two-way conversation to a conference (a group conversation),
</span><span style="font-size:small">and invites the sales or service person into the conference.</span></p>
</li></ul>
<h1>More Information</h1>
<p><span style="font-size:small">For information about finding an available contact, see
<a href="http://msdn.microsoft.com/en-us/library/hh456404.aspx">Working With Presence and Groups in UCMA 3.0: Finding a Contact (Part 3 of 5)</a>. For information about escalating the conversation, see
<a href="http://msdn.microsoft.com/en-us/library/hh456408.aspx">Working With Presence and Groups in UCMA 3.0: Adding the Contact to the Conversation (Part 4 of 5)</a>.</span></p>
