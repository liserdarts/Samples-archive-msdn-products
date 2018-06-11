# Lync Server: Extend Unified Communications Services of UCMA Bots to PIC Clients
## Requires
* Visual Studio 2010
## License
* Apache License, Version 2.0
## Technologies
* Lync Server 2010
* Lync Server 2013
## Topics
* unified communication services
* bot
## IsPublished
* True
## ModifiedDate
* 2013-08-12 07:46:16
## Description

<div id="header">
<table id="bottomTable" cellpadding="0" cellspacing="0">
<tbody>
<tr id="headerTableRow1">
<td align="left"><span id="runningHeaderText"></span></td>
</tr>
<tr id="headerTableRow2">
<td align="left"><span id="nsrTitle">Lync Server: Extending Unified Communications Services of UCMA bots to PIC clients</span>
</td>
</tr>
</tbody>
</table>
</div>
<div id="mainSection">
<div id="mainBody">
<div>
<p>This sample shows how to extend Microsoft Lync Server-supported unified communications services provided by a trusted Microsoft Unified Communications Managed API (UCMA) bot application to Public Internet Cloud (PIC) clients.</p>
</div>
<div>
<p>The application runs as a server-side application and intercepts SUBSCRIBE requests from PIC Clients. If the subscription request is for a bot and non-terminating, the application returns an 'online' ('open') presence state.</p>
</div>
<h1>Description</h1>
<div id="sectionSection0" name="collapseableSection">
<p><b>Problem</b>: Lync clients subscribing to an automaton ('bot'), have a special logic to handle bots' presence updates. PIC clients don't have this special logic to present presence information for bots to PIC clients.</p>
<p><b>Solution</b>: This application runs as a server-side application and intercepts SUBSCRIBE requests from PIC Clients. If the subscription request is for a bot and non-terminating, this application returns an 'online' ('open') presence state.</p>
<p>This application also periodically checks for newly added application endpoints (bots). When a subscription request from a PIC client comes in, the application checks the SIPUri for the target and if the target of the subscription is an application endpoint,
 then this application returns 'online' for the presence state.</p>
</div>
<h1>Features</h1>
<div id="sectionSection1" name="collapseableSection">
<p>Server-side MSPL application running as a UCMA ApplicationEndpoint to send presence update on behalf of a bot:</p>
<ul>
<li>
<p>Intercepting presence SUBSCRIBE by PIC client in an MSPL script</p>
</li><li>
<p>Handling the SUBSCRIBE requests in a managed Lync Server API application</p>
</li><li>
<p>Sending SIP 200 OK response using Microsoft.Rtc.Sip namespace</p>
</li><li>
<p>Sending NOTIFY message containing bot's presence using Microsoft.Rtc.Collaboration namespace</p>
</li><li>
<p>Obtaining installed bots using Lync Server Management Shell cmdlets with the help of the System.Management.Automation namespace</p>
</li><li>
<p>Maintaining cached list of installed bots in a background thread</p>
</li></ul>
</div>
<h1>Prerequisites</h1>
<div id="sectionSection2" name="collapseableSection">
<ul>
<li>
<p>Lync Server 2013 or Lync Server 2010 deployment with federated access</p>
</li><li>
<p>PIC Client (Skype)</p>
</li><li>
<p>Public IM enabled on the Lync Server</p>
</li></ul>
</div>
<h1>Running the sample</h1>
<div id="sectionSection3" name="collapseableSection">
<p>Prerequisites to compile the sample:</p>
<ul>
<li>
<p>UCMA 4.0 SDK or UCMA 3.0 Core SDK must be installed on the development machine.</p>
</li><li>
<p>Lync Server 2013 SDK or Lync Server 2010 SDK must be installed on the development machine.</p>
</li></ul>
</div>
<h1>Run and test the sample</h1>
<div id="sectionSection5" name="collapseableSection">
<ol>
<li>
<p>Compile the sample - Set up the references to the UCMA 4.0 SDK/UCMA 3.0 Core SDK and the Lync Server 2013 SDK/Lync Server 2010 SDK.</p>
</li><li>
<p>The account that the application runs as must be a member of the 'RTC Server Applications' local group and a member of the 'Local Administrators' group.</p>
</li><li>
<p>You must then register the Server Application with the Lync Server. To do this perform the following steps:
</p>
<ol>
<li>
<p>Open the Lync Server Management Shell console.</p>
</li><li>
<p>Run the following Windows PowerShell command:</p>
</li></ol>
</li></ol>
<p><span>New-CsServerApplication |</span> <br>
<span>-Uri http://www.microsoft.com/lyncSever/sdk/samples/PresenceSubInterceptorForBot |</span>
<br>
<span>-Critical $false |</span> <br>
<span>-Priority 2 |</span> <br>
<span>-Identity &quot;Service:Registrar:&lt;yourRegistrarFqdn&gt;/presenceInterceptor&quot; |</span>
<br>
<span>-enabled $true</span> <br>
</p>
<p>The above Windows PowerShell script creates the server application and installs it on the Lync Server to which the &quot;user&quot; is homed. The Uri parameter value must match that of the appUri in the corresponding MSPL application manifest file. The Priority value
 (2) could well be higher than that of the UserServices. This is acceptable because the &lt;allowRegistrationBeforeUserServices/&gt; element is present in the corresponding application manifest.</p>
<h3>Register the UCMA application</h3>
<div>
<ol>
<li>
<p>Create the UCMA Application Pool using Windows PowerShell<br>
<span>New-TrustedApplicationPool -Identity &lt;PoolName&gt; -Registrar &lt;Registrar&gt; -Site &lt;SiteName&gt;</span><br>
where &lt;Registrar&gt; and &lt;SiteName&gt; are the names of the registrar service and the site</p>
</li><li>
<p>Create the Trusted Application<br>
<span>New-CsTrustedApplication -Identity &lt;PoolName&gt;/presencesubinterceptorforbot |</span><br>
<span>-Port &lt;PortNumber&gt;</span></p>
</li><li>
<p>Create the trusted application endpoint<br>
<span>-SipAddress sip:picinterceptor@&lt;domain&gt;.com |</span><br>
<span>-DisplayName &quot;PresenceSubInterceptor&quot; |</span><br>
<span>-ApplicationId &quot;urn:application:presencesubinterceptorforbot&quot; |</span><br>
<span>-TrustedApplicationPoolFqdn &lt;PoolName&gt;</span></p>
</li></ol>
</div>
</div>
<h1>Running the sample</h1>
<div id="sectionSection6" name="collapseableSection">
<ol>
<li>
<p>Copy the presenceSubInterceptorForBot.am file to the folder where <b>presenceSubInterceptorForBot.exe</b> is located.</p>
</li><li>
<p>Copy the <b>PresenceSubInterceptorForBot.exe.config</b> file to where the executable is located.</p>
</li><li>
<p>Go the folder where the executable is located, run the <b>PresenceSubInterceptor.exe</b> program.</p>
</li></ol>
</div>
<h1>Change log</h1>
<div id="sectionSection7" name="collapseableSection">
<p>First release. 10/10/2012</p>
<p>Current release: changed synchronous calls to asynchronous calls using BeginXXX/EndXXX pattern. Explicitly reset the TO and FROM header values on the outgoing NOTIFY messages to the FROM and TO header values of the corresponding SUBSCRIBE messages, respectively.</p>
</div>
<h1>Related content</h1>
<div id="sectionSection8" name="collapseableSection">
<ul>
<li>
<p>MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/jj128288.aspx" target="_blank">
Extending Unified Communications Services of UCMA Bots to PIC Clients</a></p>
</li><li>
<p>MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/gg421042.aspx" target="_blank">
Microsoft Lync Server 2010 SDK Documentation</a></p>
</li><li>
<p>MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/gg421023.aspx" target="_blank">
Unified Communications Managed API 3.0 Core SDK Documentation</a></p>
</li><li>
<p>MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/jj265300.aspx" target="_blank">
Microsoft Lync Server 2013 SDK Documentation</a></p>
</li><li>
<p>MSDN Library: <a href="http://msdn.microsoft.com/en-us/library/jj728784.aspx" target="_blank">
Unified Communications Managed API 4.0 SDK Documentation</a></p>
</li></ul>
</div>
</div>
</div>
