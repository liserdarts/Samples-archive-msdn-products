Title
-----
PresenceSubInterceptorForBot

Description
-----------

Problem: 
Lync clients subscribing to an automaton (aka bot), have a special logic to handle bots' presence updates. 
PIC clients don't have this special logic to present presence information for bots to PIC clients.

Solution:
This application runs as a server-side application and intercepts SUBSCRIBE requests from PIC Clients. If 
the subscription request is for a bot and non terminating, this application returns an 'online' (aka 'open')
presence state. 

This application also periodically checks for newly added application endpoints (i.e., bots).  
When a subscription request from a PIC client comes in, the application checks the sipuri for the target and 
if the target of the subscription is an application endpoint, then this application returns 'online' for the 
presence state.

Features
--------
Server-side MSPL application running as an UCMA ApplicationEndpoint to send presence update on behalf of a bot:
Intercepting presence SUBSCRIBE by PIC client in an MSPL script
Handling the SUBSCRIBE requests in a managed Lync Server API application
Sending SIP 200 OK response using Microsoft.Rtc.Sip namespace
Sending NOTIFY message containing bot's presence using Microsoft.Rtc.Collaboration namespace
Obtaining installed bots using Lync Server Management PowerShell cmdlets with the help of the System.Management.Automation namespace
Maintaining cached list of installed bots in a background thread.

Prerequisites
-------------
- Microsoft Lync Server 2013 deployment with federated access
- PIC Client (Skype Messenger)
- Public IM enabled on the Lync Server

Running the sample
------------------

Prerequisites to compile the sample:
1. UCMA 4.0 SDK must be installed on the development machine
2. Lync Server 2013 SDK must be installed on the development machine

Installing the Server Application:
1. Compile the sample
    a) Set up the references to the UCMA 4.0 SDK and the Lync Server 2013 SDK
2. The account that the application runs as, must be a member of the 'RTC Server Applications' local group 
   and a member of the 'Local Administrators' group.
3. You must then register the Server Application with the Lync Server. To do this perform the following steps.
   a) Open the Lync Server Management PowerShell console
   b) Run the following powershell command 
      New-CsServerApplication | 
		-Uri http://www.microsoft.com/lyncSever/sdk/samples/PresenceSubInterceptorForBot | 
		-Critical $false | 
		-Priority 2 |
		-Identity "Service:Registrar:<yourRegistrarFqdn>/presenceInterceptor" |
		-enabled $true
 
   
   The above powershell script creates the server application and installs it on the Lync server to which the  
   "user" is homed. 
   The Uri parameter value must match that of the appUri in the corresponding MSPL applicatoin manifest file.
   The Priority value (2) could well be higher than that of the UserServices. This is acceptable because the
   <allowRegistrationBeforeUserServices/> element is present in the corresponding application manifest.

Creating the UCMA Application:
1. Create the UCMA Application Pool using Powershell
    New-TrustedApplicationPool -Identity <PoolName> -Registrar <Registrar> -Site <SiteName>
    
    Where <Registrar> and <SiteName> are the names of the registrar service and the SiteName
    
2. Create the Trusted Application
   New-CsTrustedApplication -Identity <PoolName>/presencesubinterceptorforbot -Port <PortNumber>
   
    NOTE: In a Mixed (OCS 2007 R2) environment, you will need to run enable-cstopology to ensure that the 
          information for OCS 2007 R2 contact objects is collected as well.
    
3. Create the trusted application endpoint
   New-CsTrustedApplicationEndpoint -SipAddress sip:picinterceptor@<domain>.com -DisplayName "PresenceSubInterceptor" -ApplicationId "urn:application:presencesubinterceptorforbot" -TrustedApplicationPoolFqdn <PoolName>

Running the sample:
1. Copy the presenceSubInterceptorForBot.am file to the folder where presenceSubInterceptorForBot.exe is located.
2. Copy the PresenceSubInterceptorForBot.exe.config file to where the executable is located.
3. Go the folder where the executable is located, run the PresenceSubInterceptor.exe program.







