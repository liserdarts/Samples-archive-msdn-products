# Mail apps for Outlook: Debug item properties
## Requires
* 
## License
* Apache License, Version 2.0
## Technologies
* Outlook Web App
* Outlook 2013
* apps for Office
* Exchange Server 2013
## Topics
* error checking
* mail app
## IsPublished
* True
## ModifiedDate
* 2013-08-28 05:49:30
## Description

<p><span style="font-size:small"><span style="line-height:115%; font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;">This sample accompanies the topic
</span><span style="line-height:115%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"><span style="font-family:&quot;Segoe UI&quot;,&quot;sans-serif&quot;"><a href="http://msdn.microsoft.com/library/office/apps/e3f14f07-cf35-4896-aab0-cb7625ca09b5(office.15)">How to: Debug item properties</a></span></span>.</span></p>
<p><span style="font-size:small">Properties for items and user profile are accessible to a mail app based on the permission level of the mail app. Mail apps follow a three-tier permission model, with restricted permission being the most basic level, read item
 is the second level that includes the restricted permission, and read/write mailbox permission is the superset of the three levels. While the restricted permission allows access to many item-level properties, the read item and read/write mailbox permissions
 allow additional access to the profile of the current user, and properties that reveal details like display name and email address of a sender, recipient, organizer, attendee, or a resource.</span></p>
<p><span style="font-size:small">This set of mail apps includes the <strong>Debug (Restricted)</strong> mail app and
<strong>Debug (Other)</strong> mail app. The <strong>Debug (Restricted)</strong> mail app displays properties of the selected message or appointment under the restricted level of permission. The
<strong>Debug (Other)</strong> mail app displays properties of the selected item under the read item permission. Often a developer may need to debug item-level properties in a custom mail app. The developer can use one of the debug mail apps that matches the
 same level of permission as the custom mail app, to conveniently display the item-level properties and their values that are accessible at that permission level.</span></p>
<p><span style="font-size:small">The following screen shot shows the <strong>Debug (Restricted)</strong> mail app that is activated for a message titled &ldquo;Hello&rdquo; in the Reading Pane. In this example, if you are testing a
<strong>MyApp (Restricted)</strong> mail app that requires restricted permission on a particular email message, you can choose the
<strong>Debug (Restricted)</strong> mail app to view the accessible item properties for that email message.</span></p>
<h1><img id="67535" src="/office/site/view/file/67535/1/Debug%20mail%20app.jpg" alt="" width="720" height="520"></h1>
<p><span style="font-size:small">For each of the two debug mail apps, the JavaScript code accesses and displays properties that are accessible under the corresponding permission level. The following lists the points of interest:</span></p>
<ul>
<li><span style="font-size:small">The properties of <a href="http://msdn.microsoft.com/en-us/library/fp161126(office.15).aspx">
UserProfile</a> are accessible under the read item and read/write mailbox permissions independent of item type. They include
<a href="http://msdn.microsoft.com/en-us/library/fp160967(office.15).aspx">displayName</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142204(office.15).aspx">emailAddress</a>, and
<a href="http://msdn.microsoft.com/en-us/library/fp161109(office.15).aspx">timeZone</a>.
</span></li><li><span style="font-size:small">The item properties that are available for all permission levels and item types are as follows:
<a href="http://msdn.microsoft.com/en-us/library/fp161128(office.15).aspx">dateTimeCreated</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161161(office.15).aspx">dateTimeModified</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161090(office.15).aspx">itemClass</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142193(office.15).aspx">itemId</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161138(office.15).aspx">itemType</a>,
<strong>subject</strong>, and <strong>normalizedSubject</strong>. </span></li><li><span style="font-size:small">The item properties that are accessible only under read item and read/write mailbox permissions are as follows:
<a href="http://msdn.microsoft.com/en-us/library/fp161163(office.15).aspx">Message.cc</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142209(office.15).aspx">Message.from</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161039(office.15).aspx">Message.sender</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142279(office.15).aspx">Message.to</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161085(office.15).aspx">Appointment.optionalAttendees</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161059(office.15).aspx">Appointment.organizer</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp160965(office.15).aspx">Appointment.requiredAttendees</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142169(office.15).aspx">Appointment.resources</a>, and the child properties, such as
<a href="http://msdn.microsoft.com/en-us/library/jj569164(office.15).aspx">appointmentResponse</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142205(office.15).aspx">displayName</a>, and
<a href="http://msdn.microsoft.com/en-us/library/fp142275(office.15).aspx">emailAddress</a>, of the
<a href="http://msdn.microsoft.com/en-us/library/fp161105(office.15).aspx">EmailAddressDetails</a> object returned by each of these properties. In particular, because the responses of attendees and resources are only accessible to the appointment organizer,
 the JavaScript code uses the following _isOrganizer function to verify if the current user is the organizer of an appointment, before enumerating responses:<br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">// Check if the current user is the organizer of a meeting.
var _isOrganizer = function()
{
    if ( (_item.itemType == 
        Office.MailboxEnums.ItemType.Appointment) &amp;&amp;
        (appOm.userProfile.emailAddress == 
            _item.organizer.emailAddress) )
    {
        return true;
    }
    
    return false;
}
</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;Check&nbsp;if&nbsp;the&nbsp;current&nbsp;user&nbsp;is&nbsp;the&nbsp;organizer&nbsp;of&nbsp;a&nbsp;meeting.</span>&nbsp;
<span class="js__statement">var</span>&nbsp;_isOrganizer&nbsp;=&nbsp;<span class="js__operator">function</span>()&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(&nbsp;(_item.itemType&nbsp;==&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Office.MailboxEnums.ItemType.Appointment)&nbsp;&amp;&amp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(appOm.userProfile.emailAddress&nbsp;==&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_item.organizer.emailAddress)&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;false;&nbsp;
<span class="js__brace">}</span>&nbsp;</pre>
</div>
</div>
</div>
</span></li></ul>
<ul>
<li><span style="font-size:small">The item properties that are available on message items (email items, meeting requests, responses, and cancellations) are as follows:
<a href="http://msdn.microsoft.com/en-us/library/fp142253(office.15).aspx">conversationId</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp160987(office.15).aspx">internetMessageId</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp142209(office.15).aspx">from</a>,
<a href="http://msdn.microsoft.com/en-us/library/fp161039(office.15).aspx">sender</a>, and
<a href="http://msdn.microsoft.com/en-us/library/fp142279(office.15).aspx">to</a>. In particular,
<strong>from</strong> and <strong>sender</strong> differ if a message is sent by a delegate, in which case the
<strong>from</strong> property represents the delegator, and the <strong>sender</strong> property represents the delegate.</span>
</li><li><span style="font-size:small">There are item properties that are available on calendar items (including meeting requests, responses, cancellations, and appointments):
<strong>start</strong>, <strong>end</strong>, and <strong>location</strong>. In particular, the _isCalendarItem function uses the message class of the selected item to verify whether the item is a meeting request, response, or cancellation. Such items share
 the base message class of IPM.Schedule. For more information about message classes, see
<a href="http://msdn.microsoft.com/en-us/library/ff861573(office.15).aspx">Item Types and Message Classes</a>.<br>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">// Check if an item is an appointment or meeting request.
var _isCalendarItem = function()
{
    if ( (_item.itemType == 
        Office.MailboxEnums.ItemType.Appointment) ||
        (_item.itemClass.indexOf(&quot;IPM.Schedule&quot;) != -1) )
    {
        return true;
    }
        
    return false;
}
</pre>
<div class="preview">
<pre class="js"><span class="js__sl_comment">//&nbsp;Check&nbsp;if&nbsp;an&nbsp;item&nbsp;is&nbsp;an&nbsp;appointment&nbsp;or&nbsp;meeting&nbsp;request.</span>&nbsp;
<span class="js__statement">var</span>&nbsp;_isCalendarItem&nbsp;=&nbsp;<span class="js__operator">function</span>()&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(&nbsp;(_item.itemType&nbsp;==&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Office.MailboxEnums.ItemType.Appointment)&nbsp;||&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(_item.itemClass.indexOf(<span class="js__string">&quot;IPM.Schedule&quot;</span>)&nbsp;!=&nbsp;-<span class="js__num">1</span>)&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;false;&nbsp;
<span class="js__brace">}</span>&nbsp;</pre>
</div>
</div>
</div>
</span></li></ul>
<p><span style="font-size:medium"><strong>Prerequisites</strong></span></p>
<p><span style="font-size:small">The following are the requirements for the sample:</span></p>
<ul>
<li><span style="font-size:small">Your favorite web development tool and web server to write and host HTML and JavaScript files.</span>
</li><li><span style="font-size:small">An email account on Exchange Server 2013.</span>
</li><li><span style="font-size:small">Client applications that support the mailbox capability in the Office 2013 release, on the desktop and tablet form factors: Outlook 2013, Outlook Web App 2013.</span>
</li><li><span style="font-size:small">Internet Explorer 9 or Internet Explorer 10 Preview.</span>
</li><li><span style="font-size:small">Required technical familiarity: HTML, JavaScript.</span>
</li></ul>
<p><span style="font-size:medium"><strong>Key components of the sample</strong></span></p>
<p><span style="font-size:small">This sample contains two mail apps. The folder ItemPropertiesMailAppRestricted contains the following files specific to the Debug (Restricted) mail app:</span></p>
<ul>
<li><span style="font-size:small">itemPropsRS.htm</span> </li><li><span style="font-size:small">itemPropsRS.xml</span> </li><li><span style="font-size:small">css\itemPropsRS.css</span> </li><li><span style="font-size:small">js\itemPropsRS.js</span> </li></ul>
<p><span style="font-size:small">The folder ItemPropertiesMailAppOther contains the following files specific to the Debug (Other) mail app:</span></p>
<ul>
<li><span style="font-size:small">itemPropsRD.htm</span> </li><li><span style="font-size:small">itemPropsRD.xml</span> </li><li><span style="font-size:small">css\itemPropsRD.css</span> </li><li><span style="font-size:small">js\itemPropsRD.js</span> </li></ul>
<p><span style="font-size:small">Each of the two mail apps uses the following files that other apps for Office use as well:</span></p>
<ul>
<li><span style="font-size:small">js\jquery\jquery-1.7.2.min.js</span> </li><li><span style="font-size:small">js\office\MicrosoftAjax.js</span> </li></ul>
<p><span style="font-size:medium"><strong>Configure the sample</strong></span></p>
<ol>
<li><span style="font-size:small">Copy the two manifest XML files, itemPropsRS.xml and itemPropsRD.xml, to a local C:\DebugManifests folder.</span>
</li><li><span style="font-size:small">Copy the code files of each of the two debug mail apps to the corresponding folder on the web server:
</span>
<ul>
<li><span style="font-size:small">Create a folder called ItemPropertiesMailAppRestricted on your web server and copy the hierarchy of the CSS and JavaScript library files of the
<strong>Debug (Restricted)</strong> mail app listed below to that ItemPropertiesMailAppRestricted folder:
</span>
<ul>
<li><span style="font-size:small">itemPropsRS.htm</span> </li><li><span style="font-size:small">css\itemPropsRS.css</span> </li><li><span style="font-size:small">js\itemPropsRS.js</span> </li><li><span style="font-size:small">js\jquery\jquery-1.7.2.min.js</span> </li><li><span style="font-size:small">js\office\MicrosoftAjax.js</span> </li></ul>
</li><li><span style="font-size:small">Create a folder called ItemPropertiesMailAppOtherPerms on your web server and copy the hierarchy of the CSS and JavaScript library files of the
<strong>Debug (Other)</strong> mail app listed below to that ItemPropertiesMailAppOtherPerms folder:
</span>
<ul>
<li><span style="font-size:small">itemPropsRD.htm</span> </li><li><span style="font-size:small">css\itemPropsRD.css</span> </li><li><span style="font-size:small">js\itemPropsRD.js</span> </li><li><span style="font-size:small">js\jquery\jquery-1.7.2.min.js</span> </li><li><span style="font-size:small">js\office\MicrosoftAjax.js</span> </li></ul>
</li></ul>
</li><li><span style="font-size:small">Update the manifest files to reflect the location of the corresponding HTML file in the
</span><a href="http://msdn.microsoft.com/en-us/library/fp123668(office.15).aspx" style="font-size:small">SourceLocation</a><span style="font-size:small"> element in the manifest.</span>
</li><li><span style="font-size:small">In the Outlook rich client, choose <strong>File</strong>, and then choose
<strong>Manage Apps</strong>. </span></li><li><span style="font-size:small">This opens a browser for you to log on to Outlook Web App to go to the Exchange Admin Center (EAC).</span>
</li><li><span style="font-size:small">Log on to your Exchange account.</span> </li><li><span style="font-size:small">In the EAC, choose the drop-down box that is adjacent to the
<strong>&#43;</strong> button, and then choose <strong>Add from file</strong>.</span>
</li><li><span style="font-size:small">In the <strong>add from file dialog box</strong>, choose
<strong>Browse</strong> to navigate to the location of the manifest files in C:\DebugManifests. Install the two manifest files one after another, by choosing one manifest file each time, then
<strong>Open</strong>, and then <strong>Next</strong>.</span> </li><li><span style="font-size:small">You should then see the <strong>Debug (Restricted)</strong> and
<strong>Debug (Other)</strong> mail apps in the list of apps for Outlook.</span> </li></ol>
<p><span style="font-size:medium"><strong>Run and test the sample</strong></span></p>
<p><span style="font-size:small">When you are testing your own custom mail app on a selected item, you can use one of the two debug mail apps to display property values of that item, by first selecting that item and then choosing the Debug mail app that corresponds
 to the same level of permission as your custom mail app. The following table summarizes the choice.</span></p>
<table border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="213" valign="top">
<p><span style="font-size:small"><strong>Debug mail app display name</strong></span></p>
</td>
<td width="213" valign="top">
<p><span style="font-size:small"><strong>Permission specified in manifest</strong></span></p>
</td>
<td width="213" valign="top">
<p><span style="font-size:small"><strong>Use</strong></span></p>
</td>
</tr>
<tr>
<td width="213" valign="top">
<p><span style="font-size:small"><strong>Debug (Restricted)</strong></span></p>
</td>
<td width="213" valign="top">
<p><span style="font-size:small">&lt;Permissions&gt;Restricted&lt;/Permissions&gt;</span></p>
</td>
<td width="213" valign="top">
<p><span style="font-size:small">Choose this Debug mail app if the mail app you&rsquo;re testing specifies
<strong>restricted </strong>permission.</span></p>
</td>
</tr>
<tr>
<td width="213" valign="top">
<p><span style="font-size:small"><strong>Debug (Other)</strong></span></p>
</td>
<td width="213" valign="top">
<p><span style="font-size:small">&lt;Permissions&gt;ReadItem&lt;/Permissions&gt;</span></p>
</td>
<td width="213" valign="top">
<p><span style="font-size:small">Choose this Debug mail app if the mail app you&rsquo;re testing specifies
<strong>read-item</strong> or <strong>read/write mailbox</strong> permission.</span></p>
</td>
</tr>
</tbody>
</table>
<p><span style="font-size:medium"><strong>Related content</strong></span></p>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/library/e3f14f07-cf35-4896-aab0-cb7625ca09b5">How to: Debug item properties</a><strong>&nbsp;</strong></span></p>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp161047(v=office.15).aspx">Privacy and security for mail apps in Outlook</a></span></p>
<p><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/fp161087(v=office.15).aspx">Using the permission model for mail apps in Outlook</a></span></p>
<p>&nbsp;</p>
