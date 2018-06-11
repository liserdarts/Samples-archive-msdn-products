===========================================================================

    Microsoft Lync Server 2013, SDK Samples

    Copyright (c) Microsoft Corporation.  All rights reserved.

===========================================================================

Sample Name:
=============

	EnhancedAllowList


Sample Description:
=================

	This sample demonstrates how an allow / block list can be built on top of the 
	enhanced federation enabled access proxy to make domain authorization 
	decisions as well as request filtering decisions. It also demonstrates 
	limiting communication with external domains unless authorized explicitly
	by administrator or internal users sending messages to those domains.

	The sample performs the following tasks by default (i.e., unless 
	customized):

	1) The sample maintains an application specific allow/block list that 
	contains known external domains that can be allowed or rejected by the SPL 
	script.

	2) The sample maintains an application specific unknown domain 
	configuration that controls the behavior when an unknown domain is 
	encounted. An unknown domain is a domain that is neither present in the 
	server's default WMI configuration nor in the application specific list.

	3) For external edge requests, it tries to authorize the source domain (by 
	checking the internal server list and the application specific  list). 
	If its present, then the message is allowed to go through. If its present 
	in the application specific  list, and is marked deny, then the 
	message is rejected. 

	4) For internal edge requests, it tries to authorize the target domain (by 
	checking the internal server list and the application specific list). If 
	its present, then the message is allowed to go through. If its present in 
	the application specific list, and is marked deny, then the message is 
	rejected.

	5) When an unknown domain is received from internal edge, the managed 
	handler checks the global configuration to see whether it can auto 
	authorize the request. If auto authorization is permitted, then it adds 
	this domain to the application specific list and proxies the request. Once 
	this auto addition is done, future requests from this external domain will 
	be allowed (because it is now present in the application specific allow 
	list).

	6) Log files are generated one per edge. 
        


How To Run The Sample:
======================

- Copy EnhancedAllowList.exe to the Microsoft Lync Server 2013 directory.


- Copy EnhancedAllowList.exe.config, EnhancedAllowList.am, UnknownDomainConfig.txt,
	and EnhancedAllowListConfig.txt files to the same directory as well.

- Edit EnhancedAllowList.exe.config	to match your setup. At a minimum, check and update
	the following parameters:

		LogPath
		EnhancedAllowListPath 
		ActionForUnknownDomainFromInternalEdge			
	
- Install the service by running the following command line (and make sure
    that EnhancedAllowList.exe and EnhancedAllowList.exe.config are in the same directory)

   %WINDIR%\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe /LogToConsole=true EnhancedAllowList.exe

   When prompted give the account name and password that is already part of "RTC Server Applications"
       local group. By default no user is part of this local group and hence you must first
       add a user account to this group and then supply it to InstallUtil.exe


- Start the Microsoft Lync Server 2013

How to uninstall the service:
=============================

- CD to the directory containing both EnhancedAllowList.exe and EnhancedAllowList.exe.config

- Run

  %WINDIR%\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe /U /LogToConsole=true EnhancedAllowList.exe

Trouble shooting:
=================

1) If you see the "Queue could not be opened Access Denied" error message, check whether
 the runas account of the program is part of the "RTC Server Applications" local group. If not, add it, logout
and login and retry

2) If you see the "Connect timeout" error message, check whether the application is registered
with WMI (Launch compmgmt.msc, goto Services And Applications -> Live Communications Server ->
right click on Applications, choose Properties). In the Available applications dialog, check
whether the application is registered and enabled.


Files in the sample
===================

UnknownDomainConfig.txt 
	Configuration file that controls the SPL script action when an unknown 
	domain is encountered. This file can have exactly two row with two 
	columns in each row. 

	One of the rows should be "external" and contain the action to be carried 
	out when an unknown domain from external edge is encountered.

	The next row should be "internal" and contain the action to be carried out 
	when an unknown domain from internal edge is encountered.

	The action field for these two rows can be respond-403 or respond-503 or 
	dispatch or allow

	respond-403 will generate a 403 Forbidden response.
	respond-503 will generate a 504 Server timeout response.
	dispatch will call into the managed handler OnRequest for appropriate 
	action.
	allow will proxy the request.

EnhancedAllowListConfig.txt

	This configuration file contains all known domains authorized/blocked by 
	the application. It should contain two columns. The first column lists the 
	domain name. The second column lists the action to be carried out when 
	a match is encountered.

	The second column can have values allow | deny. If the value is allow, it 
	is proxied. If the value is deny, it is responded with a 403 Forbidden.

	
EnhancedAllowList.exe.config - Configuration file for the EnhancedAllowList.exe application.

	Parameters

		LogPath - The directory to generate the log files.

		MaxLogFileSize - The size after which logging is stopped. This is in 
		Mega Bytes.

		MaxDomainsLogged - The number of unknown domains written to log file after 
		which logging is stopped.

		EnhancedAllowListPath - Full path to the EnhancedAllowListConfig.txt
			file discussed below.

		SPLScriptPath - Full (or relative path) to the EnhancedAllowList.am
			file discussed below.

		ActionForUnknownDomainFromInternalEdge - This parameter controls
			the action of the OnRequest managed handler when a request is
			received from the internal edge and contains an unknown target domain.

			The values can be auto, manual, custom.

			If the value is auto, the application automatically authorizes this 
			domain and adds it to EnhancedAllowListConfig.txt. This has the 
			effect of allowing subsequent requests from both sides of the 
			network. 

			If the value is manual or custom, the application generates a log 
			entry with this domain. Administrator intervention is necessary to 
			authorize / block the domain.

			Exactly one entry will be generated in the log file per domain for 
			a given run of the program.

		MaxDomainsInEnhancedAllowList - This parameter controls the maximum
			number of domains that can be present in EnhancedAllowListConfig.txt
			file.


Source files (only present in SDK):
===================================

	Main.cs - Service logic.
	Utils.cs - Helper classes for WMI, Parsing, Eventlogs
	SessionManager.cs - Logic to connect to server.
	PolicyManager.cs - Contains the core logic specific to this application. 
		Any customization needs to be done here inside the OnRequest routine.
	LogManager.cs - Logging helpers.
	
