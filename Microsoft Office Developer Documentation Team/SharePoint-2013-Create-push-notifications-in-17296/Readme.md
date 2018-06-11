# SharePoint 2013: Create push notifications in SharePoint for Windows Phone apps
## Requires
* Visual Studio 2012
## License
* Apache License, Version 2.0
## Technologies
* C#
* SharePoint Server 2013
* SharePoint Foundation 2013
* apps for SharePoint
## Topics
* Mobile
## IsPublished
* True
## ModifiedDate
* 2013-03-20 11:21:25
## Description

<p><span style="font-size:small">Using the Microsoft Push Notification Service (MPNS), Windows Phone apps can receive notifications through the Internet of events triggered by SharePoint Server. The phone app doesn't have to poll the server for changes, for
 example, to the items in a list on which the phone app is based. The app can be registered to receive notifications from the server, and an event receiver can initiate a notification and send it to the receiving app for handling. The push notification is relayed
 to Windows Phone devices by MPNS.</span></p>
<p><span style="font-size:small">An event relevant to a given phone app might occur (for example, a list item being added to a list) when the app isn't running in the foreground on the phone (that is, when the app is tombstoned or closed). You could develop
 a background service on the phone with a periodic task that might check for changes to the list on the server, but this approach consumes resources (such as network bandwidth and battery power) on the phone. With MPNS and the components that support notifications
 built into the Windows Phone 7 OS, the phone itself can receive a notification relevant to the context of a given app&mdash;even when that app isn't running&mdash;and the user can be given the opportunity to start the relevant app in response to the notification.
 (For more information about push notifications, see <a href="http://msdn.microsoft.com/en-us/library/ff402558(VS.92).aspx">
Push Notifications Overview for Windows Phone</a>.)</span></p>
<h1>Description</h1>
<p><span style="font-size:small">The server-side solution can be either an app for SharePoint deployed in an isolated
<strong>SPWeb</strong> object, or a SharePoint farm solution packaged as a SharePoint solution package (that is, a .wsp file) that contains a web-scoped Feature. In the procedures in this code sample, you develop a simple SharePoint solution that creates a
 target list that activates the push notification mechanism on the server. After creating a server-side SharePoint list with notification, a developer can create a mobile app that receives push notifications on mobile devices. For more information about creating
 mobile app to receive push notification on mobile devices, see <a href="http://msdn.microsoft.com/en-us/library/jj163784.aspx" target="_blank">
How to: Configure and use push notifications in SharePoint 2013 apps for Windows Phone</a>.</span></p>
<h1>Prerequisites</h1>
<p><span style="font-size:small">This sample requires the following:</span></p>
<ul>
<li><span style="font-size:small">Visual Studio 2012</span> </li><li><span style="font-size:small">An installation of SharePoint 2013 with administrative privileges</span>
</li></ul>
<h1>Key components of the sample</h1>
<p><span style="font-size:small">The sample contains the following:</span></p>
<ul>
<li><span style="font-size:small">PushNotificationsList project, which contains ListItemEventReceiver.cs, PushNotification.cs, and the PushNotificationsList.ps1 Windows PowerShell script.</span>
<ul>
<li><span style="font-size:small">The ListItemEventReceiver.cs file, which contains the event-receiver class for a list.</span>
</li><li><span style="font-size:small">The PushNotification.cs file, which contains classes for managing push notifications.</span>
</li><li><span style="font-size:small">The PushNotificationsList.ps1 Windows PowerShell script. You can run the CreateProductOrdersList.ps1 Windows PowerShell script from the SharePoint Management Shell on your target server to create the SharePoint list on which
 this project is based.</span> </li></ul>
</li></ul>
<h1>Configure the sample</h1>
<p><span style="font-size:small">To configure the sample:</span></p>
<ul>
<li><span style="font-size:small">Change the value of the solution property from &quot;http://localhost&quot; to the URL of the home page of your SharePoint website.</span>
</li></ul>
<h1>Run and test the sample</h1>
<ol>
<li><span style="font-size:small">Choose the F5 key to build.</span> </li><li><span style="font-size:small">Deploy the solution on SharePoint Server 2013.</span>
</li></ol>
<h1>Change log</h1>
<p><span style="font-size:small">First version:&nbsp;July 16, 2012</span></p>
<h1>Related content</h1>
<ul>
<li><a href="http://msdn.microsoft.com/en-us/library/jj163784.aspx"><span style="font-size:small">How to: Configure and use push notifications in SharePoint 2013 apps for Windows Phone</span>
</a></li><li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/ff402558(VS.92).aspx">Push Notifications Overview for Windows Phone</a></span>
</li><li><span style="font-size:small"><a href="http://create.msdn.com/en-US/education/quickstarts/push_notifications">Push Notifications foe Windows Phone development</a></span>
</li></ul>
