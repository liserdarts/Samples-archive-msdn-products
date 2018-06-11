# Async Notification Sample
## Requires
* Visual Studio 2013
## License
* MS-LPL
## Technologies
* printer
* Windows Driver
## Topics
* Printing
## IsPublished
* True
## ModifiedDate
* 2014-04-02 12:44:35
## Description

<div id="mainSection">
<p>The Async Notification sample demonstrates how to implement AsyncNotification to communicate between registered applications and printing components that are loaded in the spooler.
</p>
<p>The spooler notification mechanism enables print components that run in the spooler process to display user interface elements in the session in which the print job was initiated. The client part of the sample is a simple application that registers for Async
 print notifications and acts as the listener. The server part of the sample opens a notification channel and acts as the sender. The server part of the sample is compiled as a library to be linked to a port monitor or a print processor. For more information
 about using Async notification, see <a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff545062(v=vs.85).aspx">
Asynchronous Notifications in Print Filters</a>.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;The AsyncNotify sample is for the v3 driver model only. Furthermore, it has not been thoroughly tested and is provided here only as a proof of concept, so it must not be used in a production environment.</p>
<p class="note"><b>Note</b>&nbsp;&nbsp;</p>
<p class="note">To build this sample, you can use Microsoft Visual Studio&nbsp;2013 (Express, Professional, or Ultimate) and Windows Driver Kit (WDK)&nbsp;8.1 Update. You can get Visual Studio&nbsp;2013 and WDK&nbsp;8.1 Update
<a href="http://go.microsoft.com/fwlink/p/?LInkID=239721">here</a>.</p>
<p class="note">You can also build this sample with Visual Studio&nbsp;2013 (Professional or Ultimate) and
<a href="http://go.microsoft.com/fwlink/p/?LInkID=391348">Windows Driver Kit (WDK)&nbsp;8.1</a>.</p>
<p class="note">For Windows Driver Kit (WDK)&nbsp;8 samples, download the <a href=" http://go.microsoft.com/fwlink/?LinkId=317090">
WDK&nbsp;8 samples pack</a>. The samples in the WDK&nbsp;8 samples pack will build only with Microsoft Visual Studio Professional&nbsp;2012 (Professional or Ultimate) and WDK&nbsp;8.</p>
<p></p>
<h2>Operating system requirements</h2>
<table>
<tbody>
<tr>
<th>Client</th>
<td><dt>Windows&nbsp;7 </dt></td>
</tr>
<tr>
<th>Server</th>
<td><dt>Windows Server&nbsp;2008&nbsp;R2 </dt></td>
</tr>
</tbody>
</table>
<h2>Build the sample</h2>
<p>For information on how to build a driver solution using Microsoft Visual Studio, see
<a href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff554644">Building a Driver</a>.</p>
<h2>Run the sample</h2>
<h2><a id="Installation"></a><a id="installation"></a><a id="INSTALLATION"></a>Installation</h2>
<p>After you build both parts of this sample, you can install the following different parts:</p>
<ul>
<li><i>Client:</i> The client sample does not require any installation. You have to copy the executable file to the target computer that you are using for testing. The client application can use the received notifications in any manner that it wants. The sample
 illustrates the most common usage, which is showing user interface that is based on the content of the notification. This scenario becomes the only way that components that are running inside the Print Spooler service can show a user interface in the printing
 process. </li><li><i>Server:</i> To test the server side of the sample, you should develop a port monitor or print processor. These components should link to this sample library file and call the methods in Asyncnotify.cpp file to send notifications at different stages of
 printing. The notifications of type SAMPLE_NOTIFICATION_UI in this sample are only for the purposes of this sample. The notification schema should be designed to fit the purpose of sending them.
</li></ul>
<h2><a id="Testing"></a><a id="testing"></a><a id="TESTING"></a>Testing</h2>
<p>After you install the sample, the steps to test the sample are:</p>
<ul>
<li>Run <i>Ddkasyncnotify.exe</i> from a Command Prompt window. </li><li>Initiate a print job through the print driver/print queue that uses the server-side component.
</li><li>Call the SendAsyncNotification method in the server sample from a port monitor or print processor. This call will cause the client sample application to render a message box.
</li><li>Call the SendAsyncUINotification method in the server sample from a port monitor or print processor. This call will cause the client sample application to render a balloon that shows &quot;This text is an Async UI sample.&quot; inside the balloon.
</li></ul>
</div>
