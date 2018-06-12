# Lync 2013: Open an event channel in a UCWA app using C#/XAML and XML
## Requires
* Visual Studio 2013
## License
* Apache License, Version 2.0
## Technologies
* Lync 2010
* Lync Server 2013
* Lync 2013
## Topics
* Windows Store app
* UCWA events
## IsPublished
* True
## ModifiedDate
* 2014-04-09 01:45:34
## Description

<p><strong><span style="font-size:1.17em">Description</span></strong></p>
<div id="mainSection">
<div id="mainBody">
<div id="sectionSection0">
<div>
<p>This is a Visual Studio solution of a sample application to illustrate how to open an event channel in a UCWA Windows Store app using C#/XAML and XML.</p>
</div>
<h3>Problem</h3>
<div>
<p>You want to open the UCWA event channel in order to receive incoming notifications to receive presence subscription, to accept invitations to instant messaging, audio/video calls or online meetings.</p>
</div>
<h3>Solution</h3>
<div>
<p>This application illustrates the basic steps to set up and start the UCWA event channel in a UCWA Windows Store app using C#/XAML and XML.</p>
</div>
<h3>Features</h3>
<div>
<p>The application project continues from the UcwaWinStoreHello sample app and adds the following programming tasks.</p>
<ul>
<li>
<p>Open the event channel by setting up an event loop in a dedicated thread.</p>
</li><li>
<p>Implement synchronous HTTP GET operation to support the required PGET requests.</p>
</li><li>
<p>Handle events by forwarded to any registered upstream event handlers.</p>
</li><li>
<p>Hook up the event channel with other parts of the application.</p>
</li><li>
<p>Put all together to run the application end-to-end.</p>
</li></ul>
<div>
<table cellspacing="0" cellpadding="0" width="100%">
<tbody>
<tr>
<th align="left"><strong>Note</strong> </th>
</tr>
<tr>
<td>
<p>Notice that although this solution contains the same feature sets of the <a href="http://code.msdn.microsoft.com/vstudio/Create-a-UCWA-Windows-2c48d3f9" target="_blank">
UcwaWinStoreHello</a> solution, they have a slightly different implementation detail in this solution. The changes are mainly due to the fact that the
<a href="http://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx" target="_blank">
HttpClient</a> class is used in the current solution, whereas the lower-level <a href="http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest(v=vs.110).aspx" target="_blank">
HttpWebRequest</a> and <a href="http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse(v=vs.110).aspx" target="_blank">
HttpWebResponse</a> classes were used in the <a href="http://code.msdn.microsoft.com/vstudio/Create-a-UCWA-Windows-2c48d3f9" target="_blank">
UcwaWinStoreHello</a> solution.</p>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<h3>Prerequisites</h3>
<div>
<ul>
<li>
<p>Microsoft Lync Server 2013 deployment</p>
</li><li>
<p>Windows 8.1</p>
</li><li>
<p>Microsoft Visual Studio 2013, with Microsoft .Net Framework 4.5</p>
</li></ul>
</div>
<h3>Installing and running the application</h3>
<div>
<ol>
<li>
<p>Unzip the download package and open it in Visual Studio</p>
<ul>
<li>
<p>Compile and build the project.</p>
</li></ul>
</li><li>
<p>Obtain a Windows Store app development license.</p>
</li><li>
<p>Run the application.</p>
<ul>
<li>
<p>In Visual Studio, hit F5 to run in the simulator.</p>
</li></ul>
</li></ol>
</div>
</div>
</div>
</div>
