# Lync 2010: Add, Remove, and Rename Custom Groups
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Microsoft Lync 2010
* Microsoft Lync Server 2010
## Topics
* Groups
* Custom Groups
## IsPublished
* True
## ModifiedDate
* 2011-10-11 12:35:35
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">This topic demonstrates how to add, remove, and rename custom groups. A custom group is a user-defined instance of
<span class="unresolvedLink">Group</span> whose membership is defined by a user.</span></p>
<p><span style="font-size:small">When a local user performs any of the group operations described in this topic using Lync 2010, the result of the operation must be visible to the local user immediately on any signed-in endpoint. To make this possible, the
 API sends the group operation request to Microsoft Lync Server 2010&nbsp; and result is sent to all of the user&rsquo;s signed in endpoints.&nbsp; For this reason, group operations are&nbsp; asynchronous even though only the local user sees the result of the
 operation.&nbsp; For information about asynchronous coding patterns with Microsoft Lync 2010 API, see
<a href="http://msdn.microsoft.com/en-us/library/hh345259.aspx">Lync Asynchronous Programming</a>.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<h2><span class="LW_CollapsibleArea_Title">Add a Custom Group</span></h2>
<div class="LW_CollapsibleArea_HrDiv"><span style="font-size:small">To add a new custom group, you call the
<span class="unresolvedLink">BeginAddGroup</span> method on the contacts and groups manager, supplying the name of the new group. You must call
<span class="unresolvedLink">EndAddGroup</span> to complete the operation. You receive the
<span class="unresolvedLink">GroupAdded</span> event when the new custom group is added. Read the
<span class="unresolvedLink">Group</span> property to get an instance of the new custom group that is added.</span></div>
<div class="sectionblock">
<h2><img class="cl_CollapsibleArea_expanding LW_CollapsibleArea_Img" src="http://i.msdn.microsoft.com/Hash/030c41d9079671d09a62d8e2c1db6973.gif" alt=""><span class="LW_CollapsibleArea_Title">Add a Distribution Group</span></h2>
</div>
<div>
<div class="LW_CollapsibleArea_HrDiv"><span style="font-size:small">A distribution group is created outside of the scope of this API.&nbsp; It is obtained and added to a user&rsquo;s contact list using the Microsoft Lync 2010 API.&nbsp; See
<a href="http://msdn.microsoft.com/en-us/library/hh378560.aspx">Walkthrough: Search For a Contact</a> for information about obtaining an existing distribution group.</span></div>
<div class="sectionblock">
<p><span style="font-size:small">Once you have obtained a distribution group in a set of search results, you add the distribution group to the contact list by calling into
<span class="unresolvedLink">BeginAddGroup</span>, passing the distribution group as the first argument.</span></p>
<p><span style="font-size:small">See the previous walkthrough for the walkthrough steps to add the distribution group.</span></p>
</div>
</div>
<div>
<div class="LW_CollapsibleArea_TitleDiv">
<h2><img class="cl_CollapsibleArea_expanding LW_CollapsibleArea_Img" src="http://i.msdn.microsoft.com/Hash/030c41d9079671d09a62d8e2c1db6973.gif" alt=""><span class="LW_CollapsibleArea_Title">Remove a Custom Group</span></h2>
</div>
<div class="sectionblock">
<p><span style="font-size:small">To remove a custom group, you call the <span class="unresolvedLink">
BeginRemoveGroup</span> method on the contacts and groups manager, supplying the group to be removed. If the group is removed, you receive the
<span class="unresolvedLink">GroupRemoved</span> event on the <span class="unresolvedLink">
ContactManager</span> instance.</span></p>
<h2><img class="cl_CollapsibleArea_expanding LW_CollapsibleArea_Img" src="http://i.msdn.microsoft.com/Hash/030c41d9079671d09a62d8e2c1db6973.gif" alt=""><span class="LW_CollapsibleArea_Title">Rename a Custom Group</span></h2>
</div>
</div>
<div>
<div class="LW_CollapsibleArea_HrDiv"><span style="font-size:small">The group rename operation is restricted to groups of type
<span class="unresolvedLink">Microsoft.Lync.Model.Group.GroupType</span>.<span class="label">CustomGroup</span></span></div>
<h2><img class="cl_CollapsibleArea_expanding LW_CollapsibleArea_Img" src="http://i.msdn.microsoft.com/Hash/030c41d9079671d09a62d8e2c1db6973.gif" alt=""><span class="LW_CollapsibleArea_Title">Handle Events</span></h2>
</div>
<div>
<div class="LW_CollapsibleArea_HrDiv"><span style="font-size:small">Each of the procedures in this topic includes registration for events triggered by group operations. The
<span class="unresolvedLink">ContactManager.GroupAdded</span> and <span class="unresolvedLink">
ContactManager.GroupRemoved</span> events are raised by <span class="unresolvedLink">
Microsoft.Lync.Model.ContactManager</span>, while the <span class="unresolvedLink">
Group.NameChanged</span> event is specific to the instance of <span class="unresolvedLink">
Microsoft.Lync.Model.Group.Group</span> that is renamed.</span></div>
<div class="sectionblock">
<div class="alert"></div>
<div class="alert"><span style="font-size:small">Methods to handle these events are useful in triggering user interface updates and for registering and removing registration for events related to the affected groups. Examples of event handlers for these events
 are included later in this topic. For more information about handling these events, see
<a href="http://msdn.microsoft.com/en-us/library/hh345201.aspx">Handle Events for a Group</a> and
<a href="http://msdn.microsoft.com/en-us/library/hh378532.aspx">Handle Events for ContactManager</a>.</span></div>
<div class="alert"><span style="font-size:small">&nbsp;</span></div>
</div>
</div>
<h1>More Information</h1>
<p><span style="font-size:small">For more information on how to add, remove, and rename custom groups, see the technical article on MSDN,
<a href="http://msdn.microsoft.com/en-us/library/hh378598.aspx">Walkthrough: Add, Remove, and Rename Custom Groups</a>&nbsp;and the video&nbsp;<a href="http://channel9.msdn.com/posts/Add-Rename-and-Delete-Custom-Groups-in-Lync">Add, Rename, and Delete Custom
 Groups in Lync</a>. </span></p>
